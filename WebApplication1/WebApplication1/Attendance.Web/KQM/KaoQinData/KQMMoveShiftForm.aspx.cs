/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： KQMMoveShiftForm.aspx.cs
 * 檔功能描述： 彈性調班
 * 
 * 版本：1.0
 * 創建標識： 張明強 2011.12.26
 * 
 */
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using GDSBG.MiABU.Attendance.Model.KQM.KaoQinData;
using GDSBG.MiABU.Attendance.BLL.KQM.KaoQinData;
using System.Collections.Generic;
using Resources;
using System.Web.Script.Serialization;
using Infragistics.WebUI.UltraWebGrid;
using System.IO;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;

namespace GDSBG.MiABU.Attendance.Web.KQM.KaoQinData
{
    public partial class KQMMoveShiftForm : BasePage
    {
        JavaScriptSerializer JsSerializer = new JavaScriptSerializer();
        Dictionary<string, string> ClientMessage = null;
        MoveShiftModel moveShiftModel = new MoveShiftModel();
        KQMMoveShiftBll moveShiftBll = new KQMMoveShiftBll();
        static SynclogModel logmodel = new SynclogModel();
        static DataTable moveShiftTable = new DataTable();
        static DataTable dt_global = new DataTable();
        string depCode = "";
        string workDate1 = "";
        string workDate2 = "";
        string noWorkDate1 = "";
        string noWorkDate2 = "";
        #region PageLoad方法
        /// <summary>
        /// PageLoad方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {

            PageHelper.ButtonControls(base.FuncList, pnlShowPanel.Controls, base.FuncListModule);
            if (!IsPostBack)
            {
                logmodel.ProcessOwner = CurrentUserInfo.Personcode;
                logmodel.TransactionType = (Request.QueryString["ModuleCode"] == null) ? "" : Request.QueryString["ModuleCode"].ToString();
                logmodel.LevelNo = "2";
                logmodel.FromHost = Request.UserHostAddress;
                this.txtWorkDate1.Text = DateTime.Now.AddDays((double)(-DateTime.Now.Day + 1)).ToString("yyyy/MM/dd");
                this.txtWorkDate2.Text = DateTime.Now.ToString("yyyy/MM/dd");
                DataUIBind();
            }

            if (ClientMessage == null)
            {
                ClientMessage = new Dictionary<string, string>();
                ClientMessage.Add("AtLastOneChoose", Message.AtLastOneChoose);
                ClientMessage.Add("DeleteFailed", Message.DeleteFailed);
                ClientMessage.Add("DeleteSuccess", Message.DeleteSuccess);
                ClientMessage.Add("DeleteConfirm", Message.DeleteConfirm);
                ClientMessage.Add("SuccessCount", Message.SuccessCount);
                ClientMessage.Add("FailedCount", Message.FailedCount);
                ClientMessage.Add("checkreget", Message.checkreget);
                ClientMessage.Add("uploading", Message.uploading);
                ClientMessage.Add("WrongFilePath", Message.WrongFilePath);
                ClientMessage.Add("PathIsNull", Message.PathIsNull);
            }
            if (!this.PanelImport.Visible)
            {
                this.btnImport.Text = Message.btnImport;
            }
            else
            {
                this.btnImport.Text = Message.btnBack;
            }

            SetSelector(imgDepCode, txtDepCode, txtDepName, "dept", Request.QueryString["ModuleCode"].ToString());
            SetCalendar(txtWorkDate1, txtWorkDate2, txtNoWorkDate1, txtNoWorkDate2);
            this.ModuleCode.Value = Request.QueryString["ModuleCode"].ToString();
            string clientmsg = JsSerializer.Serialize(ClientMessage);
            Page.ClientScript.RegisterStartupScript(GetType(), "ClientMessage", "var Message=" + clientmsg + ";", true);

        }
        #endregion

        #region 頁面按鈕事件
        /// <summary>
        /// 刪除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            logmodel.ProcessFlag = "delete";
            string alert = "";
            int i;
            CellItem GridItem;
            CheckBox chkIsHaveRight;
            int intDeleteOk = 0;
            int intDeleteError = 0;
            TemplatedColumn tcol = (TemplatedColumn)this.UltraWebGrid.Bands[0].Columns[0];

            //每月多少日之後不允許重新計算上個月及以前考勤數據
            string sysKqoQinDays = "";
            sysKqoQinDays = moveShiftBll.GetValue("KQMReGetKaoQin", null);
            string strModifyDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy/MM") + "/" + sysKqoQinDays).ToString("yyyy/MM/dd");


            //依次判斷選中的數據是否已超出系統設定操作日期範圍，在此日期之後不允許再變更上月及以前的考勤數據
            for (i = 0; i < this.UltraWebGrid.Rows.Count; i++)
            {
                GridItem = (CellItem)tcol.CellItems[i];
                chkIsHaveRight = (CheckBox)GridItem.FindControl("CheckBoxCell");
                if (chkIsHaveRight.Checked && !CurrentUserInfo.RoleCode.ToString().Equals("Admin"))
                {
                    //上班日期(雙休日)小於當前月一日    且   當前日期大於允許重新計算的日期
                    if ((DateTime.Parse(this.UltraWebGrid.Rows[i].Cells.FromKey("NoWorkDate").Text.Trim()).ToString("yyyy/MM/dd").CompareTo(DateTime.Now.ToString("yyyy/MM") + "/01") == -1) && (strModifyDate.CompareTo(DateTime.Now.ToString("yyyy/MM/dd")) <= 0))
                    {
                        alert = "alert('" + this.UltraWebGrid.Rows[i].Cells.FromKey("WorkNo").Text.Trim() + Message.checkreget + ":" + strModifyDate + "')";
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "show", alert, true);
                        return;
                    }
                    //上班日期(雙休日)小於上個月一日
                    if (DateTime.Parse(this.UltraWebGrid.Rows[i].Cells.FromKey("NoWorkDate").Text.Trim()).ToString("yyyy/MM/dd").CompareTo(DateTime.Now.AddMonths(-1).ToString("yyyy/MM") + "/01") == -1)
                    {
                        alert = "alert('" + this.UltraWebGrid.Rows[i].Cells.FromKey("WorkNo").Text.Trim() + Message.checkreget + ":" + strModifyDate + "')";
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "show", alert, true);
                        return;
                    }

                    //調班日期小於當前月一日  且    當前日期大於允許重新計算的日期
                    if ((DateTime.Parse(this.UltraWebGrid.Rows[i].Cells.FromKey("WorkDate").Text.Trim()).ToString("yyyy/MM/dd").CompareTo(DateTime.Now.ToString("yyyy/MM") + "/01") == -1) && (strModifyDate.CompareTo(DateTime.Now.ToString("yyyy/MM/dd")) <= 0))
                    {
                        alert = "alert('" + this.UltraWebGrid.Rows[i].Cells.FromKey("WorkNo").Text.Trim() + Message.checkreget + ":" + strModifyDate + "')";
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "show", alert, true);
                        return;
                    }

                    //調班日期小於上個月一日
                    if (DateTime.Parse(this.UltraWebGrid.Rows[i].Cells.FromKey("WorkDate").Text.Trim()).ToString("yyyy/MM/dd").CompareTo(DateTime.Now.AddMonths(-1).ToString("yyyy/MM") + "/01") == -1)
                    {
                        alert = "alert('" + this.UltraWebGrid.Rows[i].Cells.FromKey("WorkNo").Text.Trim() + Message.checkreget + ":" + strModifyDate + "')";
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "show", alert, true);
                        return;
                    }
                }
            }

            //依次執行刪除操作
            for (i = 0; i < this.UltraWebGrid.Rows.Count; i++)
            {
                GridItem = (CellItem)tcol.CellItems[i];
                chkIsHaveRight = (CheckBox)GridItem.FindControl("CheckBoxCell");
                if (chkIsHaveRight.Checked)
                {

                    moveShiftModel = new MoveShiftModel();
                    moveShiftModel.WorkNo = this.UltraWebGrid.Rows[i].Cells.FromKey("WorkNo").Text.Trim();
                    moveShiftModel.WorkDate = DateTime.Parse(this.UltraWebGrid.Rows[i].Cells.FromKey("WorkDate").Text.Trim());
                    moveShiftModel.NoWorkDate = DateTime.Parse(this.UltraWebGrid.Rows[i].Cells.FromKey("NoWorkDate").Text.Trim());
                    moveShiftTable = moveShiftBll.GetData(moveShiftModel, "condition1", "");
                    //選中記錄是否存在
                    if (moveShiftTable.Rows.Count > 0)
                    {
                        //執行刪除操作
                        int delFlag = moveShiftBll.DeleteMoveShift(moveShiftModel, logmodel);
                        //調班日期小於當前日期
                        if (DateTime.Parse(this.UltraWebGrid.Rows[i].Cells.FromKey("WorkDate").Text.Trim()).ToString("yyyy-MM-dd").CompareTo(DateTime.Now.ToString("yyyy/MM/dd")) == -1)
                        {
                            //重新計算考勤結果
                            moveShiftBll.GetKaoQinData(this.UltraWebGrid.Rows[i].Cells.FromKey("WorkNo").Text.Trim(), "null", DateTime.Parse(this.UltraWebGrid.Rows[i].Cells.FromKey("WorkDate").Text.Trim()).ToString("yyyy/MM/dd"), DateTime.Parse(this.UltraWebGrid.Rows[i].Cells.FromKey("WorkDate").Text.Trim()).ToString("yyyy/MM/dd"));
                        }
                        //上班日期(雙休日)小於當前日期
                        if (DateTime.Parse(this.UltraWebGrid.Rows[i].Cells.FromKey("NoWorkDate").Text.Trim()).ToString("yyyy-MM-dd").CompareTo(DateTime.Now.ToString("yyyy/MM/dd")) == -1)
                        {
                            //重新計算考勤結果
                            moveShiftBll.GetKaoQinData(this.UltraWebGrid.Rows[i].Cells.FromKey("WorkNo").Text.Trim(), "null", DateTime.Parse(this.UltraWebGrid.Rows[i].Cells.FromKey("NoWorkDate").Text.Trim()).ToString("yyyy/MM/dd"), DateTime.Parse(this.UltraWebGrid.Rows[i].Cells.FromKey("NoWorkDate").Text.Trim()).ToString("yyyy/MM/dd"));
                        }
                        intDeleteOk++;
                    }
                    else
                    {
                        intDeleteError++;
                    }
                }
            }
            if ((intDeleteOk + intDeleteError) > 0)
            {
                alert = "alert('" + Message.SuccessCount + ":" + intDeleteOk + ";" + Message.FailedCount + ":" + intDeleteError + "')";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "show", alert, true);

            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "show", "alert('" + Message.DeleteFailed + "')", true);
                return;
            }
            this.ProcessFlag.Value = "";
            DataUIBind();

        }


        /// <summary>
        /// 導入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnImport_Click(object sender, EventArgs e)
        {
            this.UltraWebGridImport.Rows.Clear();
            if (!this.PanelImport.Visible)
            {
                this.PanelImport.Visible = true;
                this.PanelData.Visible = false;
                this.btnQuery.Enabled = false;
                this.btnReset.Enabled = false;
                this.btnDelete.Enabled = false;
                this.btnAdd.Enabled = false;
                this.btnModify.Enabled = false;
                this.btnImport.Text = Message.btnBack;
                this.lbluploadMsg.Text = "";
            }
            else
            {
                this.PanelImport.Visible = false;
                this.PanelData.Visible = true;
                this.btnQuery.Enabled = true;
                this.btnReset.Enabled = true;
                this.btnDelete.Enabled = true;
                this.btnAdd.Enabled = true;
                this.btnModify.Enabled = true;
                this.btnImport.Text = Message.btnImport;
            }
        }

        /// <summary>
        /// 查詢
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            pager.CurrentPageIndex = 1;
            this.Query();
            this.ProcessFlag.Value = "";
        }

        /// <summary>
        /// 查詢
        /// </summary>
        private void Query()
        {

            DataUIBind();
        }

        #endregion

        #region 頁面綁定數據
        /// <summary>
        /// 頁面綁定數據
        /// </summary>
        private void DataUIBind()
        {
            GetPageValue();
            int totalCount = 0;
            moveShiftTable = moveShiftBll.GetMoveShiftTable(moveShiftModel, base.SqlDep.ToString(), depCode, workDate1, workDate2, noWorkDate1, noWorkDate2, pager.CurrentPageIndex, pager.PageSize, out totalCount);
            pager.RecordCount = totalCount;
            UltraWebGrid.DataSource = moveShiftTable.DefaultView;
            UltraWebGrid.DataBind();
            pager.TextAfterPageIndexBox = "/" + pager.PageCount.ToString();

        }

        private void GetPageValue()
        {
            moveShiftModel.WorkNo = this.txtWorkNo.Text.ToString().Trim().ToUpper();
            moveShiftModel.LocalName = this.txtLocalName.Text.ToString().Trim().ToUpper();
            moveShiftModel.DepName = this.txtDepName.Text.ToString();
            depCode = this.txtDepCode.Text.ToString().Trim().ToUpper();
            workDate1 = this.txtWorkDate1.Text.ToString().Trim();
            workDate2 = this.txtWorkDate2.Text.ToString().Trim();
            noWorkDate1 = this.txtNoWorkDate1.Text.ToString().Trim();
            noWorkDate2 = this.txtNoWorkDate2.Text.ToString().Trim();
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
            string tableName = "GDS_ATT_MOVESHIFT_TEMP";
            string[] columnProperties = { "WorkNo", "WorkDate", "WorkSTime", "WorkETime", "NoWorkDate", "NoWorkSTime", "NoWorkETime", "TimeQty", "Remark" };
            string[] columnType = { "varchar", "varchar", "varchar", "varchar", "varchar", "varchar", "varchar", "varchar", "varchar" };
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
            DataTable dt = moveShiftBll.GetTempTableErrorData(personCode, out succesnum, out errornum, logmodel);
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
                            case "ErrWorkDateNull": errorInfo = errorInfo + Message.ErrWorkDateNull + ",";
                                break;
                            case "ErrNoWorkDateNull": errorInfo = errorInfo + Message.ErrNoWorkDateNull + ",";
                                break;
                            case "ErrDate(2012/01/01)": errorInfo = errorInfo + Message.ErrDate + ",";
                                break;
                            case "checknoworkdate": errorInfo = errorInfo + Message.checknoworkdate + ",";
                                break;
                            case "checkworkdate": errorInfo = errorInfo + Message.checkworkdate + ",";
                                break;
                            case "checknoworkdateot": errorInfo = errorInfo + Message.checknoworkdateot + ",";
                                break;
                            case "checkworkdateg1": errorInfo = errorInfo + Message.checkworkdateg1 + ",";
                                break;
                            case "checknoworkdateg2": errorInfo = errorInfo + Message.checknoworkdateg2 + ",";
                                break;
                            case "duplicate": errorInfo = errorInfo + Message.duplicate + ",";
                                break;
                            case "checkreget": errorInfo = errorInfo + Message.checkreget + ",";
                                break;
                            case "ErrTimeQtyNull": errorInfo = errorInfo + Message.ErrTimeQtyNull + ",";
                                break;
                            case "ErrRemain": errorInfo = errorInfo + Message.ErrRemain + ",";
                                break;
                            case "ErrTimeQty0": errorInfo = errorInfo + Message.ErrTimeQty0 + ",";
                                break;
                            case "checktimeqty": errorInfo = errorInfo + Message.checktimeqty + ",";
                                break;
                            case "timeerror": errorInfo = errorInfo + Message.timeerror + ",";
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
                    DataTable temp = moveShiftBll.GetMoveShiftTable(moveShiftModel, base.SqlDep.ToString(), depCode, workDate1, workDate2, noWorkDate1, noWorkDate2);

                    List<MoveShiftModel> list = moveShiftBll.GetList(temp);
                    string[] header = { ControlText.gvHeadMoveShiftDepName, ControlText.gvHeadMoveShiftWorkNo, ControlText.gvHeadMoveShiftLocalName,ControlText.gvHeadMoveShiftWorkDate, 
                                          ControlText.gvHeadMoveShiftWorkSTime, ControlText.gvHeadMoveShiftWorkETime,ControlText.gvHeadMoveShiftNoWorkDate, 
                                      ControlText.gvHeadMoveShiftNoWorkSTime,ControlText.gvHeadMoveShiftNoWorkETime,ControlText.gvHeadMoveShiftTimeQty,
                                      ControlText.gvHeadMoveShiftRemark,ControlText.gvHeadMoveShiftUpdateUser,ControlText.gvHeadMoveShiftUpdateDate};
                    string[] properties = { "DepName", "WorkNo", "LocalName", "WorkDate", "WorkSTime", "WorkETime", "NoWorkDate", "NoWorkSTime", "NoWorkETime", "TimeQty", "Remark", "UpdateUser", "UpdateDate" };
                    string filePath = MapPath("~/ExportFileTemp/") + DateTime.Now.Ticks + ".xls";
                    NPOIHelper.ExportExcel(list, header, properties, 5000, filePath);
                    PageHelper.ReturnHTTPStream(filePath, true);
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
                    KQMMoveShiftTempBll moveShiftTempBll = new KQMMoveShiftTempBll();
                    List<MoveShiftTempModel> list = moveShiftTempBll.GetTempList(dt_global);
                    string[] header = { ControlText.gvHeadMoveShiftErrorMsg, ControlText.gvHeadMoveShiftWorkNo, ControlText.gvHeadMoveShiftWorkDate, 
                                          ControlText.gvHeadMoveShiftWorkSTime, ControlText.gvHeadMoveShiftWorkETime,ControlText.gvHeadMoveShiftNoWorkDate, 
                                      ControlText.gvHeadMoveShiftNoWorkSTime,ControlText.gvHeadMoveShiftNoWorkETime,ControlText.gvHeadMoveShiftTimeQty,
                                      ControlText.gvHeadMoveShiftRemark};
                    string[] properties = { "ErrorMsg", "WorkNo", "WorkDate", "WorkSTime", "WorkETime", "NoWorkDate", "NoWorkSTime", "NoWorkETime", "TimeQty", "Remark" };
                    string filePath = MapPath("~/ExportFileTemp/") + DateTime.Now.Ticks + ".xls";
                    NPOIHelper.ExportExcel(list, header, properties, 5000, filePath);
                    PageHelper.ReturnHTTPStream(filePath, true);
                }
            }
        }
        #endregion
    }
}
