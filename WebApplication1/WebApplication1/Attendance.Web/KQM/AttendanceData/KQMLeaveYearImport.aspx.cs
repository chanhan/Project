/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： KQMLeaveYearImport.cs
 * 檔功能描述： 已休年休假導入UI層
 * 
 * 版本：1.0
 * 創建標識： 高子焱 2012.01.09
 * 
 */
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using GDSBG.MiABU.Attendance.BLL.KQM.AttendanceData;
using GDSBG.MiABU.Attendance.Model.KQM.AttendanceData;
using Infragistics.WebUI.UltraWebGrid;
using Resources;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;

namespace GDSBG.MiABU.Attendance.Web.KQM.AttendanceData
{
    public partial class KQMLeaveYearImport : BasePage
    {
        KQMLeaveYearImportModel model = new KQMLeaveYearImportModel();
        KQMLeaveYearImportBll bll = new KQMLeaveYearImportBll();
        static DataTable dt = new DataTable();
        static SynclogModel logmodel = new SynclogModel();

        #region 刪除
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            int intDeleteOk = 0;
            int intDeleteError = 0;
            TemplatedColumn tcol = (TemplatedColumn)this.UltraWebGrid.Bands[0].Columns[0];
            for (int i = 0; i < this.UltraWebGrid.Rows.Count; i++)
            {
                CellItem GridItem = (CellItem)tcol.CellItems[i];
                CheckBox chkIsHaveRight = (CheckBox)GridItem.FindControl("CheckBoxCell");
                if (chkIsHaveRight.Checked)
                {
                    logmodel.ProcessFlag = "delete";
                    model.WorkNo = this.UltraWebGrid.Rows[i].Cells.FromKey("WorkNo").Text.Trim();
                    model.LeaveYear = Convert.ToInt32(this.UltraWebGrid.Rows[i].Cells.FromKey("LeaveYear").Text.Trim());
                    int flag = bll.DeleteLeaveYearByKey(model, logmodel);
                    if (flag == 1)
                    {
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
                string alertText = "成功刪除：" + intDeleteOk + "," + "刪除失敗：" + intDeleteError;
                Page.ClientScript.RegisterStartupScript(GetType(), "show", "alert('" + alertText + "')", true);
            }
            pager.CurrentPageIndex = 1;
            PageHelper.CleanControlsValue(pnlContent.Controls);
            model = new KQMLeaveYearImportModel();
            int thisYear = DateTime.Now.Year;
            this.ddlLeaveYear.SelectedIndex = this.ddlLeaveYear.Items.IndexOf(this.ddlLeaveYear.Items.FindByValue(Convert.ToString(thisYear)));
            DataUIBind();
        }
        #endregion
        #region  上傳
        protected void btnImportSave_Click(object sender, EventArgs e)
        {
            int succesnum = 0;
            int errornum = 0;
            int flag = ImportModuleExcel();
            if (flag == 1)
            {
                logmodel.ProcessFlag = "insert";
                dt = bll.ImpoertExcel(CurrentUserInfo.Personcode, out succesnum, out errornum, logmodel);
                lblupload.Text = Message.NumberOfSuccessed + succesnum + Message.NumberOfFailed + errornum;
                dt = changeError(dt);
                this.UltraWebGridImport.DataSource = dt;
                this.UltraWebGridImport.DataBind();
            }
            else if (flag == 0)
            {
                lblupload.Text = Message.FailToSaveData;
            }
            else if (flag == -1)
            {
                lblupload.Text = Message.DataFormatError;
            }
            else if (flag == -3)
            {
                lblupload.Text = Message.IsNotExcel;
            }
            this.btnImport.Text = Message.btnBack;
            ImportFlag.Value = "2";
        }
        #endregion
        #region 查詢
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            model = PageHelper.GetModel<KQMLeaveYearImportModel>(pnlContent.Controls);
            ImportFlag.Value = "1";
            pager.CurrentPageIndex = 1;
            DataUIBind();
        }
        #endregion

        #region 頁面加載
        protected void Page_Load(object sender, EventArgs e)
        {
            JavaScriptSerializer JsSerializer = new JavaScriptSerializer();
            Dictionary<string, string> ClientMessage = null;
            DataTable dtSelectAll = new DataTable();
            if (!this.PanelImport.Visible)
            {
                this.btnImport.Text = Message.btnImport;
            }
            else
            {
                this.btnImport.Text = Message.btnBack;
            }
            PageHelper.ButtonControls(FuncList, pnlShowPanel.Controls,base.FuncListModule);
            if (!IsPostBack)
            {
                logmodel.ProcessOwner = CurrentUserInfo.Personcode;
                logmodel.TransactionType = Request.QueryString["ModuleCode"];
                logmodel.LevelNo = "2";
                logmodel.FromHost = Request.UserHostAddress;
                SetSelector(imgDepCode, txtDepCode, txtDName, Request.QueryString["ModuleCode"].ToString());
                int thisYear = DateTime.Now.Year;
                for (int i = 0; i < 6; i++)
                {
                    if (i == 0)
                    {
                        this.ddlLeaveYear.Items.Insert(i, new ListItem(""));
                    }
                    else
                    {
                        this.ddlLeaveYear.Items.Insert(i, new ListItem(Convert.ToString((int)(thisYear - i + 1)) + Message.YearName, Convert.ToString((int)(thisYear - i + 1))));
                    }
                }
                this.ddlLeaveYear.SelectedIndex = this.ddlLeaveYear.Items.IndexOf(this.ddlLeaveYear.Items.FindByValue(Convert.ToString(thisYear)));
                DataUIBind();
            }
            if (ClientMessage == null)
            {
                ClientMessage = new Dictionary<string, string>();
                ClientMessage.Add("AtLastOneChoose", Message.AtLastOneChoose);
                ClientMessage.Add("DeleteFailed", Message.DeleteFailed);
                ClientMessage.Add("DeleteSuccess", Message.DeleteSuccess);
                ClientMessage.Add("TextBoxNotNull", Message.TextBoxNotNull);
                ClientMessage.Add("DeleteConfirm", Message.DeleteConfirm);
                ClientMessage.Add("btnImport", Message.btnImport);
                ClientMessage.Add("btnBack", Message.btnBack);
            }
            string clientmsg = JsSerializer.Serialize(ClientMessage);
            Page.ClientScript.RegisterStartupScript(GetType(), "ClientMessage", "var Message=" + clientmsg + ";", true);
        }
        #endregion
        #region 數據綁定
        private void DataUIBind()
        {
            int totalCount;
            string SQLDep = base.SqlDep;
            string depCode = model.DepCode;
            model.DepCode = null;
            model.DName = null;
            model.LeaveYear = Convert.ToInt32(ddlLeaveYear.SelectedValue.ToString() == "" ? null : ddlLeaveYear.SelectedValue.ToString());
            DataTable dtSel = bll.GetLeaveDaysList(model, SQLDep, depCode, pager.CurrentPageIndex, pager.PageSize, out totalCount);
            dt = bll.GetLeaveDaysList(model, SQLDep, depCode);
            pager.RecordCount = totalCount;
            this.UltraWebGrid.DataSource = dtSel;
            this.UltraWebGrid.DataBind();
            pager.TextAfterPageIndexBox = "/" + pager.PageCount.ToString();
        }
        #endregion
        #region 分頁
        protected void pager_PageChanged(object sender, EventArgs e)
        {
            model = PageHelper.GetModel<KQMLeaveYearImportModel>(pnlContent.Controls);
            ImportFlag.Value = "1";
            DataUIBind();
        }
        #endregion
        #region 放大鏡綁定
        /// <summary>
        /// 設置Selector
        /// </summary>
        /// <param name="ctrlTrigger">控件ID--按鈕</param>
        /// <param name="ctrlCode">控件ID--文本框1</param>
        /// <param name="ctrlName">控件ID--文本框2</param>
        public void SetSelector(WebControl ctrlTrigger, Control ctrlCode, Control ctrlName, string moduleCode)
        {
            if (ctrlCode is TextBox) { (ctrlCode as TextBox).Attributes.Add("readonly", "readonly"); }
            if (ctrlName is TextBox) { (ctrlName as TextBox).Attributes.Add("readonly", "readonly"); }
            ctrlTrigger.Attributes.Add("onclick", string.Format("return setSelector('{0}','{1}','{2}')",
                ctrlCode.ClientID, ctrlName.ClientID, moduleCode));
        }
        #endregion
        #region 將文件中的數據導入到數據庫中
        /// <summary>
        /// 將文件中的數據導入到數據庫中
        /// </summary>
        private int ImportModuleExcel()
        {
            int flag;
            bool deFlag;
            string tableName = "gds_att_leaveyearImport_temp";
            string[] columnProperties = { "WorkNo", "LeaveYear", "LeaveDays" };
            string[] columnType = { "varchar", "varchar", "varchar" };
            string createUser = CurrentUserInfo.Personcode;
            deFlag = NPOIHelper.DeleteExcelSql(tableName, createUser);
            string filePath = GetImpFileName();
            if (string.IsNullOrEmpty(filePath))
                return -2;

            flag = NPOIHelper.ImportExcel(columnProperties, columnType, filePath, tableName, createUser);
            return flag;
        }
        #endregion

        #region 獲取上傳文件名,并將文件保存到服務器
        /// <summary>
        /// 獲取導入的文件名,并將文件保存到服務器
        /// </summary>
        /// <returns></returns>
        private string GetImpFileName()
        {
            string filePath = "";
            if (FileUpload.FileName.Trim() != "")
            {
                try
                {
                    filePath = MapPath("~/ExportFileTemp/") + DateTime.Now.ToString("yyyyMMddHHmmss") + Path.GetFileName(FileUpload.FileName);
                    FileUpload.SaveAs(filePath);

                }
                catch
                {
                    lblupload.Text = "Exel上傳到服務器失敗!";
                }
            }
            else
            {
                lblupload.Text = "導入路徑為空,請選擇要匯入的Excel文件！";
            }

            return filePath;
        }
        #endregion

        #region 轉換錯誤信息
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
                            case "ErrWorkNoNull": errorInfo = errorInfo + Message.ErrWorknoNull + ",";
                                break;
                            case "ErrLeaveYearNull": errorInfo = errorInfo + Message.ErrLeaveYearNull + ",";
                                break;
                            case "ErrLeaveDaysNull": errorInfo = errorInfo + Message.ErrLeaveDaysNull + ",";
                                break;
                            case "NotOnlyOne": errorInfo = errorInfo + Message.NotOnlyOne + ",";
                                break;
                            case "IsUsedNow": errorInfo = errorInfo + Message.IsUsedNow + ",";
                                break;
                            case "YearIsWrong": errorInfo = errorInfo + Message.YearIsWrong + ",";
                                break;
                            case "WorkNoIsWrong": errorInfo = errorInfo + Message.WorkNoIsWrong + ",";
                                break;
                            case "LeaveDaysIsWrong": errorInfo = errorInfo + Message.LeaveDaysIsWrong + ",";
                                break;
                            case "ErrYearFormat": errorInfo = errorInfo + Message.ErrYearFormat + ",";
                                break;
                            case "LeaveDaysMustNUmber": errorInfo = errorInfo + Message.LeaveDaysMustNUmber + ",";
                                break;
                            case "LeaveDaysLessThanCan": errorInfo = errorInfo + Message.LeaveDaysLessThanCan + ",";
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

        #region 導出報表
        protected void btnExport_Click(object sender, EventArgs e)
        {
            if (ImportFlag.Value == "2")
            {
                if (dt.Rows.Count != 0)
                {
                    model = PageHelper.GetModel<KQMLeaveYearImportModel>(pnlContent.Controls);
                    List<KQMLeaveYearImportModel> list = bll.GetList(dt);
                    string[] header = { ControlText.gvErrorMsg, ControlText.gvWorkNo, ControlText.gvLeaveYear, ControlText.gvLeaveDays };
                    string[] properties = { "ErrorMsg", "WorkNo", "LeaveYear", "LeaveDays" };
                    string filePath = MapPath("~/ExportFileTemp/") + DateTime.Now.Ticks + ".xls";
                    NPOIHelper.ExportExcel(list, header, properties, 5000, filePath);
                    PageHelper.ReturnHTTPStream(filePath, true);
                }
                else
                {
                    lblupload.Text =Message.NoDataExport;
                }
            }
            else
            {
                if (dt.Rows.Count != 0)
                {
                    model = PageHelper.GetModel<KQMLeaveYearImportModel>(pnlContent.Controls);
                    string SQLDep = base.SqlDep;
                    string depCode = model.DepCode;
                    model.DepCode = null;
                    model.DName = null;
                    model.LeaveYear = Convert.ToInt32(ddlLeaveYear.SelectedValue.ToString() == "" ? null : ddlLeaveYear.SelectedValue.ToString());
                    dt = bll.GetLeaveDaysList(model, SQLDep, depCode);
                    List<KQMLeaveYearImportModel> list = bll.GetList(dt);
                    string[] header = { ControlText.gvWorkNo, ControlText.gvLocalName, ControlText.gvBuOTMQryName, ControlText.gvDName, 
                                        ControlText.gvLeaveYear, ControlText.gvLeaveDays, ControlText.gvCreateUser, ControlText.gvCreateDate };
                    string[] properties = { "WorkNo", "LocalName", "BuOTMQryName", "DName", "LeaveYear", "LeaveDays", "CreateUser", "CreateDate" };
                    string filePath = MapPath("~/ExportFileTemp/") + DateTime.Now.Ticks + ".xls";
                    NPOIHelper.ExportExcel(list, header, properties, 5000, filePath);
                    PageHelper.ReturnHTTPStream(filePath, true);
                }
                else
                {
                    lblupload.Text = Message.NoDataExport;
                }
            }
        }
        #endregion
        #region 導入按鈕
        protected void btnImport_Click(object sender, EventArgs e)
        {
            this.UltraWebGridImport.Rows.Clear();
            if (!this.PanelImport.Visible)
            {
                this.PanelImport.Visible = true;
                this.PanelData.Visible = false;
                this.btnReset.Enabled = false;
                this.btnQuery.Enabled = false;
                this.btnDelete.Enabled = false;
                this.btnImport.Text = Message.btnBack;
                this.lbluploadMsg.Text = "";
                ImportFlag.Value = "2";
            }
            else
            {
                this.PanelImport.Visible = false;
                this.PanelData.Visible = true;
                this.btnQuery.Enabled = true;
                this.btnReset.Enabled = true;
                this.btnDelete.Enabled = true;
                this.btnImport.Text = Message.btnImport;
                this.lbluploadMsg.Text = "";
                ImportFlag.Value = "1";
            }
        }
        #endregion
    }
}
