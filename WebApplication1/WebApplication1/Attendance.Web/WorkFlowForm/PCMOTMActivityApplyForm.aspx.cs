using System;
using System.Collections.Generic; 
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Serialization;
using GDSBG.MiABU.Attendance.Model.KQM.OTM;
using GDSBG.MiABU.Attendance.BLL.KQM.OTM;
using System.Data;
using GDSBG.MiABU.Attendance.BLL.WorkFlow;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;
using Resources;
using Infragistics.WebUI.UltraWebGrid;

namespace GDSBG.MiABU.Attendance.Web.WorkFlowForm
{
    public partial class PCMOTMActivityApplyForm : BasePage
    {
         
        JavaScriptSerializer JsSerializer = new JavaScriptSerializer();
        Dictionary<string, string> ClientMessage = null;
        ActivityModel model = new ActivityModel();
        ActivityTempModel tempModel = new ActivityTempModel();
        OTMActivityApplyBll activityApplyBll = new OTMActivityApplyBll();
        OTMActivityApplyTempBll activityApplyTempBll = new OTMActivityApplyTempBll();
        static DataTable tempActivityApplyTable = new DataTable();
       // WorkFlowCardMakeupBll cardMakeupBll = new WorkFlowCardMakeupBll();
        Bll_AbnormalAttendanceHandle bll_abnormal = new Bll_AbnormalAttendanceHandle();
        static DataTable dt_global = new DataTable();
        DataTable tempDataTable = new DataTable();
        string alert = "";
        string depCode = "";
        string workNoStrings ="";
        string dateFrom = "";
        string dateTo = "";
        static SynclogModel logmodel = new SynclogModel();
        
        #region 頁面載入方法
        protected void Page_Load(object sender, System.EventArgs e)
        {
            //頁面按鈕添加權限
          //  PageHelper.ButtonControls(base.FuncList, pnlShowPanel.Controls, base.FuncListModule);

            workNoStrings = base.CurrentUserInfo.Personcode;
            


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
            }
            string clientmsg = JsSerializer.Serialize(ClientMessage);
            Page.ClientScript.RegisterStartupScript(GetType(), "ClientMessage", "var Message=" + clientmsg + ";", true);

            //放大鏡--組織樹
          //  SetSelector(imgDepCode, txtDepCode, txtDepName, "dept", Request.QueryString["ModuleCode"].ToString());

            //文本框--日曆控件
            SetCalendar(txtOTDateFrom, txtOTDateTo);
            this.ModuleCode.Value = (base.Request.QueryString["ModuleCode"] == null) ? "" : base.Request.QueryString["ModuleCode"].ToString();
            if (!IsPostBack)
            {
                HiddenWorkNo.Value = CurrentUserInfo.Personcode;
                logmodel.ProcessOwner = CurrentUserInfo.Personcode;
                logmodel.TransactionType = (Request.QueryString["ModuleCode"] == null) ? "" : Request.QueryString["ModuleCode"].ToString();
                logmodel.LevelNo = "2";
                logmodel.FromHost = Request.UserHostAddress;
                this.txtOTDateFrom.Text = DateTime.Now.AddDays(-1).ToString("yyyy/MM/dd");
                this.txtOTDateTo.Text = DateTime.Now.ToString("yyyy/MM/dd");
                ddlDataBind();
                DataUIBind();
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
            model.WorkNo = base.CurrentUserInfo.Personcode;
            model.LocalName = base.CurrentUserInfo.Cname;
            model.YearMonth = this.txtYearMonth.Text.Trim().Replace("/", "");
            model.Status = this.ddlOTStatus.SelectedValue;
            model.DepName = (base.CurrentUserInfo.DepName== null) ? "" :"未知";
            depCode = base.CurrentUserInfo.DepCode.Trim();
            workNoStrings = base.CurrentUserInfo.Personcode;
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
                condition += " and a.STATUS='" + ddlOTStatus.SelectedValue.ToString() + "' ";
            }
           // tempActivityApplyTable = activityApplyBll.GetActivityApplyList(model, base.SqlDep.ToString(), depCode, workNoStrings, dateFrom, dateTo, pager.CurrentPageIndex, pager.PageSize, out totalCount);
            tempActivityApplyTable = activityApplyBll.getActivityApplyList("", "", depCode, workNoStrings, dateFrom, dateTo, condition, pager.CurrentPageIndex, pager.PageSize, out totalCount);
            pager.RecordCount = totalCount;
            UltraWebGrid.DataSource = tempActivityApplyTable.DefaultView;
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
            this.HiddenWorkNo.Value = base.CurrentUserInfo.Personcode;


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
 
