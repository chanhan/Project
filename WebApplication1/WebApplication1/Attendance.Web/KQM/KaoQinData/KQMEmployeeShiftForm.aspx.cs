using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using GDSBG.MiABU.Attendance.BLL.KQM.KaoQinData;
using GDSBG.MiABU.Attendance.Model.KQM.KaoQinData;
using Resources;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;

/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： KQMEmployeeShiftForm.aspx.cs
 * 檔功能描述： 排班作業
 * 
 * 版本：1.0
 * 創建標識： 昝望 2011.12.27
 * 
 */

namespace GDSBG.MiABU.Attendance.Web.KQM.KaoQinData
{
    public partial class KQMEmployeeShiftForm : BasePage
    {
        JavaScriptSerializer JsSerializer = new JavaScriptSerializer();
        Dictionary<string, string> ClientMessage = null;
        int totalCount;
        static DataTable dt_global = new DataTable();
        EmployeeShiftBll bll = new EmployeeShiftBll();
        static SynclogModel logmodel = new SynclogModel();

        protected void Page_Load(object sender, EventArgs e)
        {

            pager.CurrentPageIndex = 1;
            PageHelper.ButtonControls(base.FuncList, pnlShowPanel.Controls, base.FuncListModule);
            ModuleCode.Value = Request.QueryString["ModuleCode"].ToString();
            if (ClientMessage == null)
            {
                ClientMessage = new Dictionary<string, string>();
                ClientMessage.Add("DeleteConfirm", Message.DeleteConfirm);
                ClientMessage.Add("SaveConfim", Message.SaveConfim);
                ClientMessage.Add("DataExist", Message.DataExist);
                ClientMessage.Add("WorkNoNotNull", Message.WorkNoNotNull);
                ClientMessage.Add("NotesNotNull", Message.NotesNotNull);
                ClientMessage.Add("EmpNotExist", Message.EmpNotExist);
                ClientMessage.Add("RolesCodeNotNull", Message.RolesCodeNotNull);
                ClientMessage.Add("DepCodeNotNull", Message.DepCodeNotNull);
                ClientMessage.Add("LanguageNotNull", Message.LanguageNotNull);
                ClientMessage.Add("IfAdminNotNull", Message.IfAdminNotNull);
                ClientMessage.Add("AtLastOneChoose", Message.AtLastOneChoose);
                ClientMessage.Add("btnImport", Message.btnImport);
                ClientMessage.Add("btnBack", Message.btnBack);
            }
            string clientmsg = JsSerializer.Serialize(ClientMessage);
            Page.ClientScript.RegisterStartupScript(GetType(), "ClientMessage", "var Message=" + clientmsg + ";", true);
            SetCalendar(txtShiftDate);
            if (!IsPostBack)
            {
                this.txtShiftDate.Text = DateTime.Now.ToShortDateString();
                logmodel.ProcessOwner = CurrentUserInfo.Personcode;
                logmodel.TransactionType = Request.QueryString["ModuleCode"].ToString();
                logmodel.LevelNo = "2";
                logmodel.FromHost = Request.UserHostAddress;
                ImportFlag.Value = "1";
                ExportFlag.Value = "1";
                ddlShiftType.DataSource = bll.GetShiftType();
                ddlShiftType.DataTextField = "datavalue";
                ddlShiftType.DataValueField = "datacode";
                ddlShiftType.DataBind();
                ddlShiftType.Items.Insert(0, new ListItem("", ""));
                ddlShiftType.SelectedValue = "";
                ddlShift.DataSource = bll.GetShift(CurrentUserInfo.DepCode);
                ddlShift.DataTextField = "ShiftDetail";
                ddlShift.DataValueField = "ShiftNo";
                ddlShift.DataBind();
                SetSelector(ImageDepCode, txtDepCode, txtDepName, "DepCode");
                pager.TextAfterPageIndexBox = "/" + pager.PageCount.ToString();
            }
        }


        #region 綁定數據
        /// <summary>
        /// 綁定數據
        /// </summary>

        private void DataBind()
        {
            EmployeeShiftModel model = new EmployeeShiftModel();
            UltraWebGridShiftQuery.DataSource = bll.GetShiftInfo(model, pager.CurrentPageIndex, pager.PageSize, out totalCount, "", txtShiftDate.Text.Trim(), "", "", base.SqlDep);
            dt_global = bll.GetShiftInfo(model, "", txtShiftDate.Text.Trim(), "", "", base.SqlDep);
            UltraWebGridShiftQuery.DataBind();
            pager.TextAfterPageIndexBox = "/" + pager.PageCount.ToString();
        }


        private void DataBind(DataTable dt)
        {
            UltraWebGridShiftQuery.DataSource = dt;
            UltraWebGridShiftQuery.DataBind();
            pager.TextAfterPageIndexBox = "/" + pager.PageCount.ToString();
        }

        #endregion

        #region 查詢
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            string BatchEmployeeNo = "";
            string str = txtBatchEmployeeNo.Text.Trim();
            if (str != "")
            {
                for (int i = 0; i < str.Split('\n').Length; i++)
                {
                    BatchEmployeeNo = BatchEmployeeNo + str.Split('\n')[i].Trim() + "§";
                }
            }
            string temVal = "";
            string strTemVal = ddlShift.SelectedValue;
            if (strTemVal != "")
            {
                string temStr = this.ddlShift.SelectedValuesToString(",");
                for (int i = 0; i < temStr.Split(',').Length; i++)
                {
                    temVal = temVal + temStr.Split(',')[i].Trim() + "§";
                }
            }
            if (txtShiftDate.Text.Trim() != "")
            {
                EmployeeShiftModel model = PageHelper.GetModel<EmployeeShiftModel>(pnlContent.Controls);
                model.DepCode = null;
                model.DepName = null;
                DataTable dt = bll.GetShiftInfo(model, pager.CurrentPageIndex, pager.PageSize, out totalCount, txtDepCode.Text.Trim(), txtShiftDate.Text.Trim(), BatchEmployeeNo, temVal, base.SqlDep);
                pager.RecordCount = totalCount;
                dt_global = bll.GetShiftInfo(model, txtDepCode.Text.Trim(), txtShiftDate.Text.Trim(), BatchEmployeeNo, temVal, base.SqlDep);
                DataBind(dt);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "isnumber", "alert('" + Message.ShiftDateNotNull + "');", true);
            }

        }
        #endregion

        #region 重置
        protected void btnReset_Click(object sender, EventArgs e)
        {

            this.txtDepCode.Text = "";
            this.txtDepName.Text = "";
            this.txtShiftDate.Text = DateTime.Now.ToShortDateString();
            this.ddlShift.ClearSelection();
            this.txtWorkNo.Text = "";
            this.txtLocalName.Text = "";
        }
        #endregion

        #region 導入按鈕文字切換
        /// <summary>
        /// 導入按鈕文字切換
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnImport_Click(object sender, EventArgs e)
        {
            if (ImportFlag.Value == "1")
            {
                PanelData.Attributes.Add("class", "img_hidden");
                PanelImport.Attributes.Add("class", "inner_table");
                btnQuery.Attributes.Add("disabled", "true");
                btnReset.Attributes.Add("disabled", "true");
                btnOrgShift.Attributes.Add("disabled", "true");
                btnEmpShift.Attributes.Add("disabled", "true");
                btnImport.Text = Message.btnBack;
                ImportFlag.Value = "2";
                ExportFlag.Value = "2";
            }
            else
            {
                PanelData.Attributes.Add("class", "inner_table");
                PanelImport.Attributes.Add("class", "img_hidden");
                btnQuery.Attributes.Remove("disabled");
                btnReset.Attributes.Remove("disabled");
                btnOrgShift.Attributes.Remove("disabled");
                btnEmpShift.Attributes.Remove("disabled");
                btnImport.Text = Message.btnImport;
                ImportFlag.Value = "1";
                ExportFlag.Value = "1";
            }
        }
        #endregion

        #region 分頁
        protected void pager_PageChanged(object sender, EventArgs e)
        {
            string BatchEmployeeNo = "";
            string str = txtBatchEmployeeNo.Text.Trim();
            if (str != "")
            {
                for (int i = 0; i < str.Split('\n').Length; i++)
                {
                    BatchEmployeeNo = BatchEmployeeNo + str.Split('\n')[i].Trim() + "§";
                }
            }
            string temVal = "";
            string strTemVal = ddlShift.SelectedValue;
            if (strTemVal != "")
            {
                string temStr = this.ddlShift.SelectedValuesToString(",");
                for (int i = 0; i < temStr.Split(',').Length; i++)
                {
                    temVal = temVal + temStr.Split(',')[i].Trim() + "§";
                }
            }
            if (txtShiftDate.Text.Trim() != "")
            {
                EmployeeShiftModel model = PageHelper.GetModel<EmployeeShiftModel>(pnlContent.Controls);
                DataTable dt = bll.GetShiftInfo(model, pager.CurrentPageIndex, pager.PageSize, out totalCount, txtDepCode.Text.Trim(), txtShiftDate.Text.Trim(), BatchEmployeeNo, temVal, base.SqlDep);
                pager.RecordCount = totalCount;
                dt_global = bll.GetShiftInfo(model, txtDepCode.Text.Trim(), txtShiftDate.Text.Trim(), BatchEmployeeNo, temVal, base.SqlDep);
                DataBind(dt);
            }
        }
        #endregion

        #region 導入
        protected void btnImportSave_Click(object sender, EventArgs e)
        {
            logmodel.ProcessFlag = "insert";
            int succesnum = 0;
            int errornum = 0;
            int flag = ImportModuleExcel();
            if (flag == 1)
            {
                DataTable dt = bll.ImpoertExcel(CurrentUserInfo.Personcode, out succesnum, out errornum, logmodel);
                lblupload.Text = "上傳成功筆數：" + succesnum + ",上傳失敗筆數：" + errornum;
                dt_global = changeError(dt);
                this.UltraWebGridImport.DataSource = dt_global;
                this.UltraWebGridImport.DataBind();
            }
            else if (flag == 0)
            {
                lblupload.Text = "數據保存失敗！";
            }
            else
            {
                lblupload.Text = "Excel數據格式錯誤";
            }

            PanelData.Attributes.Add("class", "img_hidden");
            PanelImport.Attributes.Add("class", "inner_table");
            btnQuery.Attributes.Add("disabled", "true");
            btnReset.Attributes.Add("disabled", "true");
            btnOrgShift.Attributes.Add("disabled", "true");
            btnEmpShift.Attributes.Add("disabled", "true");
            btnImport.Text = Message.btnBack;
            ImportFlag.Value = "2";
        }

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
                            case "ErrShiftNoNull": errorInfo = errorInfo + Message.ErrShiftNoNull+";";
                                break;
                            case "ErrWorknoNull": errorInfo = errorInfo + Message.ErrWorknoNull + ";";
                                break;
                            case "ErrStartDateNull": errorInfo = errorInfo + Message.ErrStartDateNull + ";";
                                break;
                            case "ErrEndDateNull": errorInfo = errorInfo + Message.ErrEndDateNull + ";";
                                break;
                            case "ErrDateFormat": errorInfo = errorInfo + Message.ErrDateFormat + ";";
                                break;
                            case "ErrNotLess": errorInfo = errorInfo + Message.ErrNotLess + ";";
                                break;
                            case "ErrNotOnly": errorInfo = errorInfo + Message.ShiftNotOnly + ";";
                                break;
                            case "ErrWorknoNotFind": errorInfo = errorInfo + Message.ErrWorknoNotFind + ";";
                                break;
                            case "NotOnlyOne": errorInfo = errorInfo + Message.NotOnlyOne + ";";
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

        #region 將文件中的數據導入到數據庫中
        /// <summary>
        /// 將文件中的數據導入到數據庫中
        /// </summary>
        private int ImportModuleExcel()
        {
            int flag;
            bool deFlag;
            string tableName = "GDS_ATT_EMPLOYEESHIFT_TEMP";
            string[] columnProperties = { "WORKNO", "SHIFTNO", "STARTDATE", "ENDDATE" };
            string[] columnType = { "varchar", "varchar", "varchar", "varchar" };
            string createUser = CurrentUserInfo.Personcode;
            deFlag = NPOIHelper.DeleteExcelSql(tableName, createUser);
            string filePath = GetImpFileName();
            if (string.IsNullOrEmpty(filePath))
                return 1;

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

        #endregion

        #region 導出報表
        protected void btnExport_Click(object sender, EventArgs e)
        {
            if (ExportFlag.Value == "1")
            {
                EmployeeShiftModel model = PageHelper.GetModel<EmployeeShiftModel>(pnlContent.Controls);
                List<EmployeeShiftModel> list = bll.GetList(dt_global);
                string[] header = { ControlText.gvHeadWorkNo, ControlText.gvHeadLocalName, ControlText.gvDName, ControlText.gvHeadShiftDesc, ControlText.gvHeadStartEndDate };
                string[] properties = { "WorkNo", "LocalName", "DepName", "Shift", "Startenddate" };
                string filePath = MapPath("~/ExportFileTemp/") + DateTime.Now.Ticks + ".xls";
                NPOIHelper.ExportExcel(list, header, properties, 5000, filePath);
                PageHelper.ReturnHTTPStream(filePath, true);
            }
            else
            {
                EmployeeShiftModel model = PageHelper.GetModel<EmployeeShiftModel>(pnlContent.Controls);
                List<EmployeeShiftModel> list = bll.GetList(dt_global);
                string[] header = { ControlText.gvHeadErrorMsg, ControlText.gvHeadWorkNo, ControlText.gvHeadShiftDesc, ControlText.gvHeadStartDate, ControlText.gvHeadEndDate };
                string[] properties = { "ErrorMsg", "WorkNo", "Shift", "StartDate", "EndDate" };
                string filePath = MapPath("~/ExportFileTemp/") + DateTime.Now.Ticks + ".xls";
                NPOIHelper.ExportExcel(list, header, properties, 5000, filePath);
                PageHelper.ReturnHTTPStream(filePath, true);
            }

        }
        #endregion

        #region 設置Selector
        /// <summary>
        /// 設置Selector
        /// </summary>
        /// <param name="ctrlTrigger">控件ID--按鈕</param>
        /// <param name="ctrlCode">控件ID--文本框1</param>
        /// <param name="ctrlName">控件ID--文本框2</param>
        /// <param name="flag">Selector區分標誌</param>
        public static void SetSelector(WebControl ctrlTrigger, Control ctrlCode, Control ctrlName, string flag)
        {
            if (ctrlCode is TextBox) { (ctrlCode as TextBox).Attributes.Add("readonly", "readonly"); }
            if (ctrlName is TextBox) { (ctrlName as TextBox).Attributes.Add("readonly", "readonly"); }
            ctrlTrigger.Attributes.Add("onclick", string.Format("return setSelector('{0}','{1}','{2}')",
                ctrlCode.ClientID, ctrlName.ClientID, flag));
        }

        #endregion

        #region 行綁定

        protected void UltraWebGridShiftQuery_DataBound(object sender, EventArgs e)
        {
            for (int i = 0; i < this.UltraWebGridShiftQuery.Rows.Count; i++)
            {
                this.UltraWebGridShiftQuery.Rows[i].Cells.FromKey("WorkNo").TargetURL = "javascript:OpenEmpWindow('" + this.UltraWebGridShiftQuery.Rows[i].Cells.FromKey("WorkNo").Text + "')";
            }
        }

        #endregion
    }
}
