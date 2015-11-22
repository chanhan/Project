/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： OTMActivityApplyForm.aspx.cs
 * 檔功能描述： 免卡加班人員導入
 * 
 * 版本：1.0
 * 創建標識： 張明強 2011.12.9
 * 
 * 修改：增加【送簽】與【組織送簽】功能
 * 修改人：劉小明 2012-02-24
 * 
 * 修改：增加組織送簽功能
 * 修改人：劉小明 2012.03.06
 * 
 * 
 */
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;
using GDSBG.MiABU.Attendance.Model.KQM.OTM;
using GDSBG.MiABU.Attendance.BLL.KQM.OTM;
using System.Collections.Generic;
using Resources;
using System.Web.Script.Serialization;
using System.IO;
using Infragistics.WebUI.UltraWebGrid;
using System.Collections;
using GDSBG.MiABU.Attendance.BLL.WorkFlow;


namespace GDSBG.MiABU.Attendance.Web.KQM.OTM
{
    public partial class OTMActivityApplyForm : BasePage
    {

        JavaScriptSerializer JsSerializer = new JavaScriptSerializer();
        Dictionary<string, string> ClientMessage = null;
        ActivityModel model = new ActivityModel();
        ActivityTempModel tempModel = new ActivityTempModel();
        OTMActivityApplyBll activityApplyBll = new OTMActivityApplyBll();
        OTMActivityApplyTempBll activityApplyTempBll = new OTMActivityApplyTempBll();
        static DataTable tempActivityApplyTable = new DataTable();
        WorkFlowCardMakeupBll cardMakeupBll = new WorkFlowCardMakeupBll();
        Bll_AbnormalAttendanceHandle bll_abnormal = new Bll_AbnormalAttendanceHandle();
        static DataTable dt_global = new DataTable();
        DataTable tempDataTable = new DataTable();
        string alert = "";
        string depCode = "";
        string workNoStrings = "";
        string dateFrom = "";
        string dateTo = "";
        static SynclogModel logmodel = new SynclogModel(); 
        string sFlow_LevelRemark = Message.flow_levelremark;
        #region 頁面載入方法
        protected void Page_Load(object sender, System.EventArgs e)
        {
            //頁面按鈕添加權限
            PageHelper.ButtonControls(base.FuncList, pnlShowPanel.Controls, base.FuncListModule);

            //彈框提示
            if (ClientMessage == null)
            {
                ClientMessage = new Dictionary<string, string>();
                ClientMessage.Add("ShiftIsUsedNoDelete", Message.ShiftIsUsedNoDelete);
                ClientMessage.Add("WrongDate", Message.WrongDate);
                ClientMessage.Add("ToLaterThanFrom", Message.ToLaterThanFrom);
                ClientMessage.Add("AuditUnaudit", Message.AuditUnaudit);
                ClientMessage.Add("AtLastOneChoose", Message.AtLastOneChoose);
                ClientMessage.Add("DataReturn", Message.DataReturn);
                ClientMessage.Add("DeleteApplyovertimeEnd", Message.DeleteApplyovertimeEnd);
                ClientMessage.Add("AuditUncancelaudit", Message.AuditUncancelaudit);
                ClientMessage.Add("uploading", Message.uploading);
                ClientMessage.Add("WrongFilePath", Message.WrongFilePath);
                ClientMessage.Add("PathIsNull", Message.PathIsNull);
                ClientMessage.Add("UpdateStatusNotIs03", Message.UpdateStatusNotIs03);
            }
            string clientmsg = JsSerializer.Serialize(ClientMessage);
            Page.ClientScript.RegisterStartupScript(GetType(), "ClientMessage", "var Message=" + clientmsg + ";", true);

            //放大鏡--組織樹
            SetSelector(imgDepCode, txtDepCode, txtDepName, "dept", Request.QueryString["ModuleCode"].ToString());

            //文本框--日曆控件
            SetCalendar(txtOTDateFrom, txtOTDateTo);
            this.ModuleCode.Value = (base.Request.QueryString["ModuleCode"] == null) ? "" : base.Request.QueryString["ModuleCode"].ToString();
            if (!IsPostBack)
            {
                logmodel.ProcessOwner = CurrentUserInfo.Personcode;
                logmodel.TransactionType = (Request.QueryString["ModuleCode"] == null) ? "" : Request.QueryString["ModuleCode"].ToString();
                logmodel.LevelNo = "2";
                logmodel.FromHost = Request.UserHostAddress;
                this.txtOTDateFrom.Text = DateTime.Now.AddDays(-1).ToString("yyyy/MM/dd");
                this.txtOTDateTo.Text = DateTime.Now.ToString("yyyy/MM/dd");
                ddlDataBind();
                DataUIBind();
            }

            //按鈕"導入"/"返回"控制
            if (!this.PanelImport.Visible)
            {
                this.btnImport.Text = Message.btnImport;
            }
            else
            {
                this.btnImport.Text = Message.btnBack;
            }
        }
        #endregion

        #region 查詢和綁定

        /// <summary>
        /// 查詢
        /// </summary>
        /// <param name="WindowOpen"></param>
        /// <param name="forwarderType"></param>
        private void Query(bool WindowOpen, string forwarderType)
        {
            this.DataUIBind();
        }

        /// <summary>
        /// 獲取頁面控件值
        /// </summary>
        private void GetPageValue()
        {
            model.WorkNo = this.txtEmployeeNo.Text.Trim();
            model.LocalName = this.txtName.Text.Trim();
            model.YearMonth = this.txtYearMonth.Text.Trim().Replace("/", "");
            model.Status = this.ddlOTStatus.SelectedValue;
            model.DepName = this.txtDepName.Text.Trim();
            depCode = this.txtDepCode.Text.Trim();
            workNoStrings = (this.txtEmployeeNo.Text.Trim() != "") ? "" : this.txtBatchEmployeeNo.Text.Trim();
            dateFrom = this.txtOTDateFrom.Text.Trim();
            dateTo = this.txtOTDateTo.Text.Trim();
        }

        /// <summary>
        /// 查詢-WebGrid控件綁定數據
        /// </summary>
        private void DataUIBind()
        {
            GetPageValue();
            int totalCount = 0;
            string condition = "";
            if (ddlOTStatus.SelectedValue.ToString() != "")
            {
                condition += " and a.STATUS='"+ddlOTStatus.SelectedValue.ToString()+"' ";
            }
            tempActivityApplyTable = activityApplyBll.getActivityApplyList(model.DepName.ToString(), base.SqlDep.ToString(), depCode, workNoStrings, dateFrom, dateTo, condition, pager.CurrentPageIndex, pager.PageSize, out totalCount);
            pager.RecordCount = totalCount;
            UltraWebGrid.DataSource = tempActivityApplyTable;//.DefaultView;
            UltraWebGrid.DataBind();
            pager.TextAfterPageIndexBox = "/" + pager.PageCount.ToString();
        }

        #endregion

        #region 頁面按鈕事件
        /// <summary>
        /// 查詢
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnQuery_Click(object sender, System.EventArgs e)
        {

            if (this.txtOTDateFrom.Text.Trim() == "" || this.txtOTDateTo.Text.Trim() == "")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "isnumber", "alert('" + Message.ErrWorkDayNull + "');", true);
                return;
            }
            pager.CurrentPageIndex = 1;
            this.Query(true, "Goto");
            this.ProcessFlag.Value = "";
            this.HiddenWorkNo.Value = this.txtEmployeeNo.Text.Trim().ToUpper();


        }


        /// <summary>
        /// 刪除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDelete_Click(object sender, System.EventArgs e)
        {

            int intDeleteOk = 0;
            int intDeleteError = 0;
            Infragistics.WebUI.UltraWebGrid.TemplatedColumn tcol = (TemplatedColumn)UltraWebGrid.Bands[0].Columns[0];
            for (int i = 0; i < UltraWebGrid.Rows.Count; i++)
            {
                CellItem GridItem = (CellItem)tcol.CellItems[i];
                CheckBox chkIsHaveRight = (CheckBox)(GridItem.FindControl("CheckBoxCell"));
                if (chkIsHaveRight.Checked)
                {
                    //非未核准狀態的記錄不能刪除
                    if (UltraWebGrid.Rows[i].Cells.FromKey("Status").Text.Trim() != "0" && UltraWebGrid.Rows[i].Cells.FromKey("Status").Text.Trim() != "3")
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "show", "alert('" + Message.DdeleteApplyovertimeEnd + "')", true);
                        return;
                    }
                }
            }
            for (int i = 0; i < UltraWebGrid.Rows.Count; i++)
            {
                CellItem GridItem = (CellItem)tcol.CellItems[i];
                CheckBox chkIsHaveRight = (CheckBox)(GridItem.FindControl("CheckBoxCell"));
                if (chkIsHaveRight.Checked)
                {
                    tempDataTable = activityApplyBll.GetLHZBData("", this.UltraWebGrid.DisplayLayout.Rows[i].Cells.FromKey("id").Value.ToString().Trim(), "", "condition1");
                    if (tempDataTable.Rows.Count > 0)
                    {
                        logmodel.ProcessFlag = "delete";
                        activityApplyBll.LHZBDeleteData(tempDataTable, logmodel);
                        intDeleteOk += 1;
                    }
                    else
                    {
                        intDeleteError += 1;
                    }
                }
            }
            if (intDeleteOk + intDeleteError > 0)
            {
                alert = "alert('" + Message.SuccessCount + ":" + intDeleteOk + ";" + Message.FailedCount + ":" + intDeleteError + "')";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "show", alert, true);
                this.Query(false, "Goto");
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "show", "alert('" + Message.AtLastOneChoose + "')", true);
                return;
            }
            this.ProcessFlag.Value = "";

        }


        /// <summary>
        /// 核准
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAudit_Click(object sender, EventArgs e)
        {

            int intDeleteOk = 0;
            int intDeleteError = 0;
            Infragistics.WebUI.UltraWebGrid.TemplatedColumn tcol = (TemplatedColumn)UltraWebGrid.Bands[0].Columns[0];
            for (int i = 0; i < UltraWebGrid.Rows.Count; i++)
            {
                CellItem GridItem = (CellItem)tcol.CellItems[i];
                CheckBox chkIsHaveRight = (CheckBox)(GridItem.FindControl("CheckBoxCell"));
                if (chkIsHaveRight.Checked)
                {
                    tempDataTable = activityApplyBll.GetLHZBData("", this.UltraWebGrid.DisplayLayout.Rows[i].Cells.FromKey("id").Value.ToString().Trim(), "", "condition5");
                    if (this.tempDataTable.Rows.Count > 0)
                    {
                        logmodel.ProcessFlag = "update";
                        activityApplyBll.LHZBAudit(tempDataTable, logmodel);
                        intDeleteOk += 1;
                    }
                    else
                    {
                        intDeleteError += 1;
                    }
                }
            }
            if (intDeleteOk + intDeleteError > 0)
            {

                alert = "alert('" + Message.SuccessCount + ":" + intDeleteOk + ";" + Message.FailedCount + ":" + intDeleteError + "')";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "show", alert, true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "show", "alert('" + Message.AtLastOneChoose + "')", true);
                return;
            }
            this.ProcessFlag.Value = "";
            this.Query(false, "Goto");


        }

        /// <summary>
        /// 取消核准
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCancelAudit_Click(object sender, EventArgs e)
        {

            string sysKqoQinDays = activityApplyBll.GetValue("KQMReGetKaoQin", null);
            string strModifyDate = Convert.ToDateTime(System.DateTime.Now.ToString("yyyy/MM") + "/" + sysKqoQinDays).ToString("yyyy/MM/dd");
            int intDeleteOk = 0;
            int intDeleteError = 0;
            string sFromKQDate = "";
            Infragistics.WebUI.UltraWebGrid.TemplatedColumn tcol = (TemplatedColumn)UltraWebGrid.Bands[0].Columns[0];
            for (int i = 0; i < UltraWebGrid.Rows.Count; i++)
            {
                CellItem GridItem = (CellItem)tcol.CellItems[i];
                CheckBox chkIsHaveRight = (CheckBox)(GridItem.FindControl("CheckBoxCell"));
                if (chkIsHaveRight.Checked)
                {
                    sFromKQDate = Convert.ToDateTime(UltraWebGrid.Rows[i].Cells.FromKey("OTDate").Text).ToString("yyyy/MM/dd");
                    //非已核准的申請單不能取消
                    if (UltraWebGrid.Rows[i].Cells.FromKey("Status").Text.Trim() != "2")
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "show", "alert('" + Message.AuditUncancelaudit + "')", true);
                        return;
                    }
                    //判斷是否超出系統設定操作的日期範圍
                    if (sFromKQDate.CompareTo(System.DateTime.Now.ToString("yyyy/MM") + "/01") == -1 && strModifyDate.CompareTo(System.DateTime.Now.ToString("yyyy/MM/dd")) <= 0)
                    {
                        alert = "alert('" + this.UltraWebGrid.Rows[i].Cells.FromKey("WorkNo").Text.Trim() + Message.checkreget + ":" + strModifyDate + "')";
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "show", alert, true);
                        return;
                    }
                    if (sFromKQDate.CompareTo(System.DateTime.Now.AddMonths(-1).ToString("yyyy/MM") + "/01") == -1)
                    {
                        alert = "alert('" + this.UltraWebGrid.Rows[i].Cells.FromKey("WorkNo").Text.Trim() + Message.checkreget + ":" + strModifyDate + "')";
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "show", alert, true);
                        return;
                    }
                }
            }
            for (int i = 0; i < UltraWebGrid.Rows.Count; i++)
            {
                CellItem GridItem = (CellItem)tcol.CellItems[i];
                CheckBox chkIsHaveRight = (CheckBox)(GridItem.FindControl("CheckBoxCell"));
                if (chkIsHaveRight.Checked)
                {
                    tempDataTable = activityApplyBll.GetLHZBData("", this.UltraWebGrid.DisplayLayout.Rows[i].Cells.FromKey("id").Value.ToString().Trim(), "", "condition5");
                    if (this.tempDataTable.Rows.Count > 0)
                    {
                        logmodel.ProcessFlag = "update";
                        activityApplyBll.LHZBCancelAudit(tempDataTable, logmodel);
                        intDeleteOk += 1;
                    }
                    else
                    {
                        intDeleteError += 1;
                    }
                }
            }
            if (intDeleteOk + intDeleteError > 0)
            {
                alert = "alert('" + Message.SuccessCount + ":" + intDeleteOk + ";" + Message.FailedCount + ":" + intDeleteError + "')";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "show", alert, true);
                this.Query(false, "Goto");
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "show", "alert('" + Message.AtLastOneChoose + "')", true);
                return;
            }
            this.ProcessFlag.Value = "";


        }

        /// <summary>
        /// 導入按鈕
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnImport_Click(object sender, System.EventArgs e)
        {
            this.UltraWebGridImport.Rows.Clear();
            if (!this.PanelImport.Visible)
            {
                this.PanelImport.Visible = true;
                this.PanelData.Visible = false;
                this.btnQuery.Enabled = false;
                this.btnReset.Enabled = false;
                this.btnAdd.Enabled = false;
                this.btnModify.Enabled = false;
                this.btnDelete.Enabled = false;
                this.btnAudit.Enabled = false;
                this.btnCancelAudit.Enabled = false;
                this.btnImport.Text = Message.btnBack;
                this.lbluploadMsg.Text = "";
                this.lblupload.Text = "";
            }
            else
            {
                this.PanelImport.Visible = false;
                this.PanelData.Visible = true;
                this.btnQuery.Enabled = true;
                this.btnReset.Enabled = true;
                this.btnAdd.Enabled = true;
                this.btnModify.Enabled = true;
                this.btnDelete.Enabled = true;
                this.btnAudit.Enabled = true;
                this.btnCancelAudit.Enabled = true;
                this.btnImport.Text = Message.btnImport;
            }
        }
        #endregion

        #region 下拉列表綁定數據
        /// <summary>
        /// 下拉列表綁定數據
        /// </summary>
        protected void ddlDataBind()
        {
            DataTable tempOTStatus = new DataTable();
            tempOTStatus = activityApplyBll.GetOTStatus();
            this.ddlOTStatus.DataSource = tempOTStatus.DefaultView;
            this.ddlOTStatus.DataTextField = "DataValue";
            this.ddlOTStatus.DataValueField = "DataCode";
            this.ddlOTStatus.DataBind();
            this.ddlOTStatus.Items.Insert(0, new ListItem("", ""));
            this.ddlOTStatus.SelectedIndex = this.ddlOTStatus.Items.IndexOf(this.ddlOTStatus.Items.FindByValue("0"));


        }
        #endregion

        #region UltraWebGrid的DataBound事件
        /// <summary>
        /// UltraWebGrid的DataBound事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void UltraWebGrid_DataBound(object sender, EventArgs e)
        {
            for (int i = 0; i < UltraWebGrid.Rows.Count; i++)
            {
                if (UltraWebGrid.Rows[i].Cells.FromKey("Status").Text.Trim() == "0")
                {
                    UltraWebGrid.Rows[i].Cells.FromKey("StatusName").Style.ForeColor = System.Drawing.Color.Green;
                }
                if (UltraWebGrid.Rows[i].Cells.FromKey("Status").Text.Trim() == "1")
                {
                    UltraWebGrid.Rows[i].Cells.FromKey("StatusName").Style.ForeColor = System.Drawing.Color.Blue;
                }
                if (UltraWebGrid.Rows[i].Cells.FromKey("Status").Text.Trim() == "2")
                {
                    UltraWebGrid.Rows[i].Cells.FromKey("StatusName").Style.ForeColor = System.Drawing.Color.Black;
                }
                if (UltraWebGrid.Rows[i].Cells.FromKey("Status").Text.Trim() == "3")
                {
                    UltraWebGrid.Rows[i].Cells.FromKey("StatusName").Style.ForeColor = System.Drawing.Color.Maroon;
                }
            }
        }
        #endregion

        #region 設置放大鏡頁面,用於輔助選擇
        /// <summary>
        /// 設置Selector
        /// </summary>
        /// <param name="ctrlTrigger">控件ID--按鈕</param>
        /// <param name="ctrlCode">控件ID--文本框1</param>
        /// <param name="ctrlName">控件ID--文本框2</param>
        /// <param name="flag">Selector區分標誌</param>
        public void SetSelector(WebControl ctrlTrigger, Control ctrlCode, Control ctrlName, string flag, string moduleCode)
        {
            if (ctrlCode is TextBox) { (ctrlCode as TextBox).Attributes.Add("readonly", "readonly"); }
            if (ctrlName is TextBox) { (ctrlName as TextBox).Attributes.Add("readonly", "readonly"); }
            ctrlTrigger.Attributes.Add("onclick", string.Format("return setSelector('{0}','{1}','{2}','{3}')",
                ctrlCode.ClientID, ctrlName.ClientID, flag, moduleCode));
        }

        #endregion

        #region 分頁
        protected void pager_PageChanged(object sender, EventArgs e)
        {
            DataUIBind();
        }
        #endregion

        #region 導入


        /// <summary>
        /// 導入保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnImportSave_Click(object sender, EventArgs e)
        {
            string filePath = "";
            if (FileUpload.FileName.Trim() != "")
            {

                //路徑所指向文件存在,則上傳至服務器
                try
                {
                    filePath = MapPath("~/ExportFileTemp/") + DateTime.Now.ToString("yyyyMMddHHmmss") + Path.GetFileName(FileUpload.FileName);
                    FileUpload.SaveAs(filePath);
                }
                catch
                {
                    lblupload.Text = Message.FailToUpload;
                    return;
                }

                int flag = ImportModuleExcel(filePath);
                switch (flag)
                {
                    case 1: //導入臨時表成功,進行數據驗證,顯示錯誤信息
                        ShowImportData(); break;
                    case 0:
                        lblupload.Text = Message.FailToSaveData; break;
                    case -1:
                        lblupload.Text = Message.DataFormatError; break;
                    case -3:
                        lblupload.Text = Message.IsNotExcel; break;
                }


            }
            else
            {
                lblupload.Text = Message.PathIsNull;
            }
        }


        /// <summary>
        /// 將文件中的數據導入到臨時表中
        /// </summary>
        private int ImportModuleExcel(string filePath)
        {
            int flag;
            bool deFlag;
            string tableName = "GDS_ATT_ACTIVITY_TEMP";
            string[] columnProperties = { "WorkNo", "LocalName", "OTDate", "OTType", "ConfirmHours", "WorkDesc", "StartTime", "EndTime" };
            string[] columnType = { "varchar", "varchar", "varchar", "varchar", "varchar", "varchar", "varchar", "varchar" };
            string createUser = CurrentUserInfo.Personcode;
            deFlag = NPOIHelper.DeleteExcelSql(tableName, createUser);
            flag = NPOIHelper.ImportExcel(columnProperties, columnType, filePath, tableName, createUser);
            return flag;
        }

        /// <summary>
        /// 驗證臨時表數據,將錯誤信息顯示出來
        /// </summary>
        private void ShowImportData()
        {
            int succesnum = 0;
            int errornum = 0;
            logmodel.ProcessFlag = "insert";
            string personCode = CurrentUserInfo.Personcode;
            DataTable dt = activityApplyTempBll.GetTempTableErrorData(personCode, out succesnum, out errornum, logmodel);
            lblupload.Text = Message.NumberOfSuccessed + succesnum + Message.NumberOfFailed + errornum;
            dt_global = changeError(dt);
            this.UltraWebGridImport.DataSource = dt_global;
            this.UltraWebGridImport.DataBind();
        }

        /// <summary>
        /// 轉換錯誤信息
        /// </summary>
        private DataTable changeError(DataTable dt)
        {
            if (dt != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string errorInfo = "";
                    for (int j = 0; j < dt.Rows[i]["ErrorMsg"].ToString().Split('§').Length; j++)
                    {

                        switch (dt.Rows[i]["ErrorMsg"].ToString().Split('§')[j].ToString().Trim())
                        {
                            case "ErrWorknoNull": errorInfo = errorInfo + Message.ErrWorknoNull + ",";
                                break;
                            case "NotOnlyOne": errorInfo = errorInfo + Message.NotOnlyOne + ",";
                                break;

                            case "ErrWorknoNotFind": errorInfo = errorInfo + Message.ErrWorknoNotFind + ",";
                                break;

                            case "ErrWorkNoAndWorkNameNotConsistency": errorInfo = errorInfo + Message.ErrWorkNoAndWorkNameNotConsistency + ",";
                                break;
                            case "ErrDate(2012/01/01)": errorInfo = errorInfo + Message.ErrDate + ",";
                                break;
                            case "KqmOtmTypeError": errorInfo = errorInfo + Message.KqmOtmTypeError + ",";
                                break;

                            case "ErrRemain": errorInfo = errorInfo + Message.ErrRemain + ",";
                                break;
                            case "ErrStartDateNull": errorInfo = errorInfo + Message.ErrStartDateNull + ",";
                                break;
                            case "ErrEndDateNull": errorInfo = errorInfo + Message.ErrEndDateNull + ",";
                                break;
                            case "ErrDateFormat": errorInfo = errorInfo + Message.ErrDateFormat + ",";
                                break;
                            case "ConfirmHoursTooLarge": errorInfo = errorInfo + Message.ConfirmHoursTooLarge + ",";
                                break;
                            default: break;
                        }
                    }
                    dt.Rows[i]["ErrorMsg"] = errorInfo;
                }
            }
            return dt;
        }

        #endregion

        #region 導出EXCEL文檔
        /// <summary>
        /// 輸出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnExport_Click(object sender, EventArgs e)
        {
            if (!this.PanelImport.Visible)
            {
                if (this.UltraWebGrid.Rows.Count == 0)
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "show", "alert('" + Message.NoDataExport + "')", true);
                }
                else
                {
                    GetPageValue();
                    DataTable tempExport = activityApplyBll.GetActivityApplyList(model, base.SqlDep.ToString(), depCode, workNoStrings, dateFrom, dateTo);
                    if (tempExport.Rows.Count != 0)
                    {
                        List<ActivityModel> list = activityApplyBll.GetList(tempExport);
                        string[] header = { ControlText.gvHeadActivityDepName, ControlText.gvHeadActivityWorkNo, ControlText.gvHeadActivityLocalName,ControlText.gvHeadActivityOTDate, 
                                          ControlText.gvHeadActivityOTType, ControlText.gvHeadActivityConfirmHours,ControlText.gvHeadActivityStatusName, 
                                      ControlText.gvHeadActivityWorkDesc,ControlText.gvHeadActivityUpdateUser,ControlText.gvHeadActivityUpdateDate,ControlText.lblStartTime,ControlText.lblEndTime};
                        string[] properties = { "DepName", "WorkNo", "LocalName", "OTDate", "OTType", "ConfirmHours", "StatusName", "WorkDesc", "UpdateUser", "UpdateDate", "StartTime", "EndTime" };
                        string filePath = MapPath("~/ExportFileTemp/") + DateTime.Now.Ticks + ".xls";
                        NPOIHelper.ExportExcel(list, header, properties, 5000, filePath);
                        PageHelper.ReturnHTTPStream(filePath, true);
                    }
                }
            }
            else
            {
                if (this.UltraWebGridImport.Rows.Count == 0)
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "show", "alert('" + Message.NoDataExport + "')", true);
                }
                else
                {
                    List<ActivityTempModel> list = activityApplyTempBll.GetList(dt_global);
                    string[] header = { ControlText.gvHeadActivityErrorMsg, ControlText.gvHeadActivityWorkNo, ControlText.gvHeadActivityLocalName, 
                                          ControlText.gvHeadActivityOTDate, ControlText.gvHeadActivityOTType,ControlText.gvHeadActivityConfirmHours, 
                                      ControlText.gvHeadActivityWorkDesc,ControlText.lblStartTime,ControlText.lblEndTime};
                    string[] properties = { "ErrorMsg", "WorkNo", "LocalName", "OTDate", "OTType", "ConfirmHours", "WorkDesc", "StartTime", "EndTime" };
                    string filePath = MapPath("~/ExportFileTemp/") + DateTime.Now.Ticks + ".xls";
                    NPOIHelper.ExportExcel(list, header, properties, 5000, filePath);
                    PageHelper.ReturnHTTPStream(filePath, true);

                }
            }
        }
        #endregion


        #region 組織送簽
        protected void btnBatchSendSign_Click(object sender, EventArgs e)
        {

        }
        #endregion


        #region 送簽
        protected void btnSendAudit_Click(object sender, EventArgs e)
        {
            SendOrgAudit();
            /**
            try
            {
                int i;
                CellItem GridItem;
                CheckBox chkIsHaveRight;
                int intSendOK = 0;
                int intSendBillNo = 0;
                int intSendError = 0;
                string OrgCode = "";
                string BillNo = "";
                string AuditOrgCode = "";
                string BillTypeCode = "KQMOTMA";
                string BillTypeNo = "TMA";
                SortedList BillNoOrgCode = new SortedList();
                TemplatedColumn tcol = (TemplatedColumn)this.UltraWebGrid.Bands[0].Columns[0];
                bool bResult = false;

                for (i = 0; i < this.UltraWebGrid.Rows.Count; i++)
                {
                    GridItem = (CellItem)tcol.CellItems[i];
                    chkIsHaveRight = (CheckBox)GridItem.FindControl("CheckBoxCell");
                    if (chkIsHaveRight.Checked && ((this.UltraWebGrid.Rows[i].Cells.FromKey("Status").Text != "0") && (this.UltraWebGrid.Rows[i].Cells.FromKey("Status").Text != "3")))
                    {

                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "show", "alert('" + Message.common_message_audit_unsendaudit + "')", true);

                        return;
                    }
                }

                for (i = 0; i < this.UltraWebGrid.Rows.Count; i++)
                {
                    GridItem = (CellItem)tcol.CellItems[i];
                    chkIsHaveRight = (CheckBox)GridItem.FindControl("CheckBoxCell");
                    if (chkIsHaveRight.Checked)
                    {
                        BillNo = "";
                        AuditOrgCode = "";  //獲取部門代碼

                        OrgCode = string.IsNullOrEmpty(this.HiddenOrgCode.Value) ? this.UltraWebGrid.Rows[i].Cells.FromKey("DEPCODE").Text.Trim() : this.HiddenOrgCode.Value.Trim();
                        string senduser = this.UltraWebGrid.Rows[i].Cells.FromKey("WorkNo").Text.Trim();


                        AuditOrgCode = cardMakeupBll.GetWorkFlowOrgCode(OrgCode, BillTypeCode);
                        if (AuditOrgCode.Length > 0)
                        {
                           
                          //  if (BillNoOrgCode.IndexOfKey(AuditOrgCode) >= 0)
                          //  {
                         //       BillNo = BillNoOrgCode.GetByIndex(BillNoOrgCode.IndexOfKey(AuditOrgCode)).ToString();
                         //       activityApplyBll.SaveAuditData("Modify", this.UltraWebGrid.Rows[i].Cells.FromKey("ID").Text.Trim(), BillNo, AuditOrgCode);
                          //      intSendOK++;
                          //  }
                          //  else
                         //   {
                         //       BillNo = activityApplyBll.SaveAuditData("Add", this.UltraWebGrid.Rows[i].Cells.FromKey("ID").Text.Trim(), BillTypeNo, AuditOrgCode);
                         //       //cardMakeupBll.SaveData(BillNo, AuditOrgCode, CurrentUserInfo.Personcode.ToString());
                         //       //cardMakeupBll.SaveAuditStatusData(BillNo, AuditOrgCode, BillTypeCode);//送簽
                         //       bll_abnormal.WFMSaveData(BillNo, AuditOrgCode, CurrentUserInfo.Personcode, logmodel);
                         //       bll_abnormal.WFMSaveAuditStatusData(BillNo, AuditOrgCode, BillTypeCode, senduser, sFlow_LevelRemark,logmodel);
                         //       intSendBillNo++;
                          //      intSendOK++;
                          //      BillNoOrgCode.Add(AuditOrgCode, BillNo);
                          //  }
                            
                            string WorkNo = this.UltraWebGrid.Rows[i].Cells.FromKey("WorkNo").Text.Trim();
                                
                            bResult = activityApplyBll.SaveAuditData(WorkNo, BillTypeNo,BillTypeCode, CurrentUserInfo.Personcode, AuditOrgCode, sFlow_LevelRemark, logmodel);
                            if (bResult)
                            {
                                intSendBillNo++;
                                intSendOK++;
                            }

                        }
                        else
                        {
                            intSendError++;
                        }
                    }

                }
                if ((intSendOK + intSendError) > 0)
                {
                    if (intSendError > 0)
                    {

                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "show", "alert('" + string.Concat(new object[] { Message.common_message_successcount, "：", intSendOK, ";", Message.common_message_billcount, "：", intSendBillNo, ";", Message.common_message_errorcount, "：", intSendError, "(", Message.common_message_noworkflow, ")" }) + "')", true);
                    }
                    else
                    {

                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "show", "alert('" + string.Concat(new object[] { Message.common_message_successcount, "：", intSendOK, ";", Message.common_message_billcount, "：", intSendBillNo }) + "')", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "show", "alert('" + Message.common_message_data_select + "')", true);
                    return;
                }
                this.ProcessFlag.Value = "";
                this.Query(false, "Goto");
            }
            catch (Exception ex)
            {

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "show", "alert('" + ((ex.InnerException == null) ? ex.Message : ex.InnerException.Message) + "')", true);
            }
            **/
        }
        #endregion

        #region 組織送簽 

        /// <summary>
        /// 組織送簽
        /// </summary>
        private void SendOrgAudit()
        {
            try
            {
                int intSendOK = 0;
                int intSendBillNo = 0;
                int intSendError = 0;
                //string OTDate = "";
                string OrgCode = "";
                string AuditOrgCode = ""; 
                string BillNoType = "TMA";
                string BillTypeCode = "KQMOTMA";
                string BillTypeNo = "TMA";
                string OTType = "";
                TemplatedColumn tcol = (TemplatedColumn)UltraWebGrid.Bands[0].Columns[0];

                for (int i = 0; i < UltraWebGrid.Rows.Count; i++)
                {
                    CellItem GridItem = (CellItem)tcol.CellItems[i];
                    CheckBox chkIsHaveRight = (CheckBox)(GridItem.FindControl("CheckBoxCell"));
                    if (chkIsHaveRight.Checked)
                    {
                        if (UltraWebGrid.Rows[i].Cells.FromKey("Status").Text != "0" &&
                            UltraWebGrid.Rows[i].Cells.FromKey("Status").Text != "3")
                        {
                            WriteMessage(1, Message.common_message_audit_unsendaudit); 
                            return;
                        }

                    }
                }

                Dictionary<string, List<string>> dicy = new Dictionary<string, List<string>>();

                for (int i = 0; i < UltraWebGrid.Rows.Count; i++)
                {
                    CellItem GridItem = (CellItem)tcol.CellItems[i];
                    CheckBox chkIsHaveRight = (CheckBox)(GridItem.FindControl("CheckBoxCell"));
                    if (chkIsHaveRight.Checked)
                    {

                        AuditOrgCode = "";  //獲取部門代碼
                        OrgCode = UltraWebGrid.Rows[i].Cells.FromKey("DEPCODE").Text.Trim();
                        //OrgCode = string.IsNullOrEmpty(this.HiddenOrgCode.Value) ? this.UltraWebGrid.Rows[i].Cells.FromKey("DCode").Text.Trim() : this.HiddenOrgCode.Value.Trim();
                        AuditOrgCode = cardMakeupBll.GetWorkFlowOrgCode(OrgCode, BillTypeCode);

                        string key = AuditOrgCode;// +"^" + OTType;
                        List<string> list = new List<string>();
                        if (!dicy.ContainsKey(key) && AuditOrgCode.Length > 0)
                        {
                            dicy.Add(key, list);
                        }
                        else if (AuditOrgCode.Length == 0)
                        {
                            intSendError += 1;

                        }
                        AuditOrgCode = "";
                    }
                }

                for (int i = 0; i < UltraWebGrid.Rows.Count; i++)
                {
                    CellItem GridItem = (CellItem)tcol.CellItems[i];
                    CheckBox chkIsHaveRight = (CheckBox)(GridItem.FindControl("CheckBoxCell"));
                    if (chkIsHaveRight.Checked)
                    {

                        AuditOrgCode = "";  //獲取部門代碼
                        OrgCode = UltraWebGrid.Rows[i].Cells.FromKey("DEPCODE").Text.Trim();
                        AuditOrgCode = cardMakeupBll.GetWorkFlowOrgCode(OrgCode, BillTypeCode);
                        string key = AuditOrgCode;// +"^" + OTType;

                        if (dicy[key] != null)
                        {
                            dicy[key].Add(UltraWebGrid.Rows[i].Cells.FromKey("ID").Text.Trim());
                        }
                    }
                }


                int count = activityApplyBll.SaveOrgAuditData("Add", dicy, BillNoType, BillTypeCode, CurrentUserInfo.Personcode, logmodel);
                intSendBillNo = count;
                intSendOK += 1;

                if (intSendOK + intSendError > 0)
                {
                    if (intSendError > 0)
                    {
                        this.WriteMessage(1, Message.SuccCount + intSendOK + ";" + Message.common_message_billcount + "：" + intSendBillNo + ";" + Message.FaileCount + intSendError + "(" + Message.common_message_noworkflow + ")");
                    }
                    else
                    {
                        this.WriteMessage(1, Message.SuccCount + intSendOK + ";" + Message.common_message_billcount + "：" + intSendBillNo);
                    }
                }
                else
                {
                    this.WriteMessage(1, Message.AtLastOneChoose);
                    return;
                }
                 
                this.Query(false, "Goto");
                this.ProcessFlag.Value = "";
                this.HiddenOrgCode.Value = "";
            }
            catch (Exception ex)
            {
                this.WriteMessage(2, ex.InnerException == null ? ex.Message : ex.InnerException.Message);
            }
        }

        #endregion 


        #region 公共方法

        protected void WriteMessage(int messageType, string message)
        {
            switch (messageType)
            {
                case 0:
                    this.Page.RegisterClientScriptBlock("jump", "<script language='JavaScript'>window.status='" + message.Replace("\"", "'").Replace("\r", "").Replace("\n", "") + "';</script>");
                    break;

                case 1:
                    this.Page.RegisterClientScriptBlock("jump", "<script language='JavaScript'>alert(\"" + message.Replace("\"", "'").Replace("\r", "").Replace("\n", "") + "\");</script>");
                    break;

                case 2:
                    this.Page.RegisterClientScriptBlock("jump", "<script language='JavaScript'>alert(\"" + message.Replace("\"", "'").Replace("\r", "").Replace("\n", "") + "\");</script>");
                    break;
            }
        }
        #endregion
    }

}
