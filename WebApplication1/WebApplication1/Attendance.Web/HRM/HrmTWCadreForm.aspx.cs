/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： HrmTWCadreForm
 * 檔功能描述： 駐派幹部資料UI
 * 
 * 版本：1.0
 * 創建標識： 顧陳偉 2011.12.16
 * 
 */
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using GDSBG.MiABU.Attendance.BLL.HRM;
using GDSBG.MiABU.Attendance.Model.HRM;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;
using Infragistics.WebUI.UltraWebGrid;
using Resources;


namespace GDSBG.MiABU.Attendance.Web.HRM
{
    public partial class HrmTWCadreForm : BasePage
    {
        TWCadreBll tWCadreBll = new TWCadreBll();
        TWCadreModel model = new TWCadreModel();
        static SynclogModel logmodel = new SynclogModel();
        static DataTable dt_global = new DataTable();
        JavaScriptSerializer JsSerializer = new JavaScriptSerializer();
        Dictionary<string, string> ClientMessage = null;

        #region 頁面加載
        protected void Page_Load(object sender, EventArgs e)
        {
            PageHelper.ButtonControls(FuncList, pnlShowPanel.Controls, base.FuncListModule);
            SetCalendar(txtJoinDateFrom, txtJoinDateTo, txtLeaveDateFrom, txtLeaveDateTo);
            if (!base.IsPostBack)
            {
                logmodel.ProcessOwner = CurrentUserInfo.Personcode;
                logmodel.TransactionType = Request.QueryString["ModuleCode"] == null ? "" : Request.QueryString["ModuleCode"].ToString();
                logmodel.LevelNo = "2";
                logmodel.FromHost = Request.UserHostAddress;

                DropDownCheckListBind();
                GridDataBind();
                this.ModuleCode.Value = base.Request.QueryString["ModuleCode"];
            }
            if (ClientMessage == null)
            {
                ClientMessage = new Dictionary<string, string>();
                ClientMessage.Add("RulesDeleteConfirm", Message.RulesDeleteConfirm);
                ClientMessage.Add("WrongDate", Message.WrongDate);
                ClientMessage.Add("NoItemSelected", Message.NoItemSelected);

            }
            string clientmsg = JsSerializer.Serialize(ClientMessage);
            Page.ClientScript.RegisterStartupScript(GetType(), "ClientMessage", "var Message=" + clientmsg + ";", true);
        }
        #endregion

        #region 下拉菜單數據綁定
        protected void DropDownCheckListBind()
        {

            DataTable dt = tWCadreBll.GetLevel();
            this.DropDownCheckListLevelCode.DataSource = dt.DefaultView;
            this.DropDownCheckListLevelCode.DataTextField = "LevelName";
            this.DropDownCheckListLevelCode.DataValueField = "LevelCode";
            this.DropDownCheckListLevelCode.DataBind();
            dt.Clear();
            dt = tWCadreBll.GetManager();
            this.DropDownCheckListManager.DataSource = dt.DefaultView;
            this.DropDownCheckListManager.DataTextField = "ManagerName";
            this.DropDownCheckListManager.DataValueField = "ManagerCode";
            this.DropDownCheckListManager.DataBind();
            dt.Clear();
            dt = tWCadreBll.GetEmpStatus();
            this.DropDownCheckListStatus.DataSource = dt.DefaultView;
            this.DropDownCheckListStatus.DataTextField = "StatusName";
            this.DropDownCheckListStatus.DataValueField = "StatusCode";
            this.DropDownCheckListStatus.DataBind();
            dt.Clear();
        }
        #endregion

        #region GridView數據綁定
        protected void GridDataBind()
        {
            int totalCount;
            string LevelCondition = "";
            string ManagerCondition = "";
            string StatusCondition = "";
            string sql = SqlDep;
            model = PageHelper.GetModel<TWCadreModel>(pnlContent.Controls);
            if (!string.IsNullOrEmpty(this.DropDownCheckListLevelCode.SelectedValue.ToString()))
            {

                string[] temVal = DropDownCheckListLevelCode.SelectedValuesToString(",").Split(',');
                for (int iLoop = 0; iLoop < temVal.Length; iLoop++)
                {
                    LevelCondition += "'" + temVal[iLoop] + "',";
                }
                LevelCondition = LevelCondition.Substring(0, LevelCondition.Length - 1);

            }
            if (!string.IsNullOrEmpty(this.DropDownCheckListManager.SelectedValue.ToString()))
            {
                string[] temVal = DropDownCheckListManager.SelectedValuesToString(",").Split(',');
                for (int iLoop = 0; iLoop < temVal.Length; iLoop++)
                {
                    ManagerCondition += "'" + temVal[iLoop] + "',";
                }
                ManagerCondition = ManagerCondition.Substring(0, ManagerCondition.Length - 1);
            }
            if (!string.IsNullOrEmpty(this.DropDownCheckListStatus.SelectedValue.ToString()))
            {
                string[] temVal = DropDownCheckListStatus.SelectedValuesToString(",").Split(',');
                for (int iLoop = 0; iLoop < temVal.Length; iLoop++)
                {
                    StatusCondition += "'" + temVal[iLoop] + "',";
                }
                StatusCondition = StatusCondition.Substring(0, StatusCondition.Length - 1);
            }
            string JoinDateFrom = this.txtJoinDateFrom.Text.Trim();
            string JoinDateTo = this.txtJoinDateTo.Text.Trim();
            string LeaveDateFrom = this.txtLeaveDateFrom.Text.Trim();
            string LeaveDateTo = this.txtLeaveDateTo.Text.Trim();
            DataTable dt = tWCadreBll.GetTWCadrePageInfo(model, sql, LevelCondition, ManagerCondition, StatusCondition, JoinDateFrom, JoinDateTo, LeaveDateFrom, LeaveDateTo, pager.CurrentPageIndex, pager.PageSize, out totalCount);
            pager.RecordCount = totalCount;
            this.UltraWebGrid.DataSource = dt.DefaultView;
            this.UltraWebGrid.DataBind();
            pager.TextAfterPageIndexBox = "/" + pager.PageCount.ToString();
        }
        #endregion

        #region 分頁
        protected void pager_PageChanged(object sender, EventArgs e)
        {
            GridDataBind();
        }
        #endregion

        #region 查詢
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            pager.CurrentPageIndex = 1;
            GridDataBind();
        }
        #endregion

        #region 重置
        protected void btnReset_Click(object sender, EventArgs e)
        {
            this.txtDepCode.Text = "";
            this.txtDepName.Text = "";
            this.txtWorkNo.Text = "";
            this.txtLocalName.Text = "";
            this.txtJoinDateFrom.Text = "";
            this.txtJoinDateTo.Text = "";
            this.txtLeaveDateFrom.Text = "";
            this.txtLeaveDateTo.Text = "";
            this.txtIdentityNo.Text = "";
            this.ddlSex.SelectedIndex = -1;
            this.DropDownCheckListStatus.SelectedIndex = -1;
            this.DropDownCheckListManager.SelectedIndex = -1;
            this.DropDownCheckListLevelCode.SelectedIndex = -1;
        }
        #endregion

        #region 刪除
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            logmodel.ProcessFlag = "delete";
            int intDeleteOk = 0;
            int intDeleteError = 0;
            int count = 0;
            TemplatedColumn tcol = (TemplatedColumn)this.UltraWebGrid.Bands[0].Columns[0];

            for (int i = 0; i < this.UltraWebGrid.Rows.Count; i++)
            {
                CellItem GridItem = (CellItem)tcol.CellItems[i];
                CheckBox chkIsHaveRight = (CheckBox)GridItem.FindControl("CheckBoxCell");
                string WorkNo = this.UltraWebGrid.DisplayLayout.Rows[i].Cells.FromKey("WorkNo").Text;
                if (chkIsHaveRight.Checked)
                {
                    count++;
                    int num = tWCadreBll.DeleteTWCadre(WorkNo, logmodel);
                    if (num > 0)
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
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "isnumber", "alert('" + Message.DeleteSuccess + ":" + intDeleteOk + ";" + Message.DeleteFailed + ":" + intDeleteError + "');", true);
                GridDataBind();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "isnumber", "alert('" + Message.AtLastOneChoose + "');", true);
                return;
            }
        }
        #endregion

        #region 導入
        /// <summary>
        /// 導入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnImportSave_Click(object sender, EventArgs e)
        {
            ImportExcel();
            this.ImportFlag.Value = "Import";
        }
        #endregion

        #region 將文件中的數據導入到數據庫中
        /// <summary>
        /// 將文件中的數據導入到數據庫中
        /// </summary>
        private void ImportExcel()
        {
            int flag;
            bool deFlag;
            string tableName = "gds_att_temp_twcadre";
            string[] columnProperties = { "workno", "localname", "sex", "identityno", "byname", "levelname", "managername", "depname", "extension", "notes", "joindate", "status", "leavedate", "cardno", "iskaoqin" };
            string[] columnType = { "varchar", "varchar", "varchar", "varchar", "varchar", "varchar", "varchar", "varchar", "varchar", "varchar", "varchar", "varchar", "varchar", "varchar", "varchar" };
            string createUser = CurrentUserInfo.Personcode;
            deFlag = NPOIHelper.DeleteExcelSql(tableName, createUser);
            string filePath = GetImpFileName();
            if (string.IsNullOrEmpty(filePath))
                return;

            flag = NPOIHelper.ImportExcel(columnProperties, columnType, filePath, tableName, createUser);
            if (flag == 1)
            {
                int successnum = 0;
                int errornum = 0;
                DataTable newdt = tWCadreBll.ImpoertExcel(createUser, out successnum, out errornum, logmodel);
                lblUploadMsg.Text = "上傳成功筆數：" + successnum + ",上傳失敗筆數：" + errornum;
                dt_global = changeError(newdt);
                this.UltraWebGridImport.DataSource = dt_global;
                this.UltraWebGridImport.DataBind();
            }
            else if (flag == 0)
            {
                lblUploadMsg.Text = "數據保存失敗！";
            }
            else
            {
                lblUploadMsg.Text = "Excel數據格式錯誤";
            }
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
                    for (int j = 0; j < dt.Rows[i]["errormsg"].ToString().Split('§').Length; j++)
                    {

                        switch (dt.Rows[i]["errormsg"].ToString().Split('§')[j].ToString().Trim())
                        {
                            case "ErrWorkNo": errorInfo = errorInfo + Message.ErrWorkNo;
                                break;
                            case "ErrWorkNoInEmployee": errorInfo = errorInfo + Message.ErrWorkNoInEmployee;
                                break;
                            case "ErrLocalNameNotNull": errorInfo = errorInfo + Message.ErrLocalNameNotNull;
                                break;
                            case "ErrDepNotNull": errorInfo = errorInfo + Message.ErrDepNotNull;
                                break;
                            case "ErrDepNotFind": errorInfo = errorInfo + Message.ErrDepNotFind;
                                break;
                            case "ErrIsKaoQin": errorInfo = errorInfo + Message.ErrIsKaoQin;
                                break;
                            case "ErrLevelNotFind": errorInfo = errorInfo + Message.ErrLevelNotFind;
                                break;
                            case "ErrManagerNotFind": errorInfo = errorInfo + Message.ErrManagerNotFind;
                                break;
                            case "ErrStatusNotFind": errorInfo = errorInfo + Message.ErrStatusNotFind;
                                break;
                            case "ErrSex": errorInfo = errorInfo + Message.ErrSex;
                                break;
                            case "ErrDate(2012/01/01)": errorInfo = errorInfo + Message.ErrDate;
                                break;
                            default: break;
                        }
                    }
                    dt.Rows[i]["errormsg"] = errorInfo;
                }
            }
            return dt;
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
                    lblUploadMsg.Text = "Exel上傳到服務器失敗!";
                }
            }
            else
            {
                lblUploadMsg.Text = "導入路徑為空,請選擇要匯入的Excel文件！";
            }

            return filePath;
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
            if (this.ImportFlag.Value == "Import")
            {
                if (dt_global.Rows.Count == 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "nodataexport", "alert('" + Message.NoDataExport + "');", true);
                }
                else
                {
                    List<TWCadreModel> list = tWCadreBll.GetList(dt_global);
                    string[] header = { ControlText.gvWorkNo, ControlText.gvHeadLocalName, ControlText.gvSex, ControlText.gvIdentityNo, ControlText.gvByName, ControlText.gvLevel, ControlText.gvManager, ControlText.gvHeadDepCode, ControlText.gvExtension, ControlText.gvHeadNotes, ControlText.gvJoinDate, ControlText.gvStatus, ControlText.gvLeaveDate, ControlText.gvCardNo, ControlText.gvIsKaoQin };
                    string[] properties = { "WorkNo", "LocalName", "Sex", "IdentityNo", "ByName", "LevelName", "ManagerName", "DepName", "Extension", "Notes", "JoinDate", "Status", "LeaveDate", "CardNo", "IsKaoQin" };
                    string filePath = MapPath("~/ExportFileTemp/") + DateTime.Now.Ticks + ".xls";
                    NPOIHelper.ExportExcel(list, header, properties, 5000, filePath);
                    PageHelper.ReturnHTTPStream(filePath, true);
                }
            }
            else
            {
                string LevelCondition = "";
                string ManagerCondition = "";
                string StatusCondition = "";
                string sql = SqlDep;
                model = PageHelper.GetModel<TWCadreModel>(pnlContent.Controls);
                if (!string.IsNullOrEmpty(this.DropDownCheckListLevelCode.SelectedValue.ToString()))
                {

                    string[] temVal = DropDownCheckListLevelCode.SelectedValuesToString(",").Split(',');
                    for (int iLoop = 0; iLoop < temVal.Length; iLoop++)
                    {
                        LevelCondition += "'" + temVal[iLoop] + "',";
                    }
                    LevelCondition = LevelCondition.Substring(0, LevelCondition.Length - 1);

                }
                if (!string.IsNullOrEmpty(this.DropDownCheckListManager.SelectedValue.ToString()))
                {
                    string[] temVal = DropDownCheckListManager.SelectedValuesToString(",").Split(',');
                    for (int iLoop = 0; iLoop < temVal.Length; iLoop++)
                    {
                        ManagerCondition += "'" + temVal[iLoop] + "',";
                    }
                    ManagerCondition = ManagerCondition.Substring(0, ManagerCondition.Length - 1);
                }
                if (!string.IsNullOrEmpty(this.DropDownCheckListStatus.SelectedValue.ToString()))
                {
                    string[] temVal = DropDownCheckListStatus.SelectedValuesToString(",").Split(',');
                    for (int iLoop = 0; iLoop < temVal.Length; iLoop++)
                    {
                        StatusCondition += "'" + temVal[iLoop] + "',";
                    }
                    StatusCondition = StatusCondition.Substring(0, StatusCondition.Length - 1);
                }
                string JoinDateFrom = this.txtJoinDateFrom.Text.Trim();
                string JoinDateTo = this.txtJoinDateTo.Text.Trim();
                string LeaveDateFrom = this.txtLeaveDateFrom.Text.Trim();
                string LeaveDateTo = this.txtLeaveDateTo.Text.Trim();
                DataTable newdt = tWCadreBll.GetTWCadreForExport(model, sql, LevelCondition, ManagerCondition, StatusCondition, JoinDateFrom, JoinDateTo, LeaveDateFrom, LeaveDateTo);
                if (newdt.Rows.Count == 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "nodataexport", "alert('" + Message.NoDataExport + "');", true);
                }
                else
                {
                    List<TWCadreModel> list = tWCadreBll.GetList(newdt);
                    string[] header = { ControlText.gvOrgName, ControlText.gvWorkNo, ControlText.gvHeadLocalName, ControlText.gvSex, ControlText.gvIdentityNo, ControlText.gvByName, ControlText.gvLevel, ControlText.gvManager, ControlText.gvExtension, ControlText.gvHeadNotes, ControlText.gvJoinDate, ControlText.gvLeaveDate, ControlText.gvStatus, ControlText.gvCardNo, ControlText.gvIsKaoQin, ControlText.gvModifier, ControlText.gvModifyDate };
                    string[] properties = { "DepName", "WorkNo", "LocalName", "SexName", "IdentityNo", "ByName", "LevelName", "ManagerName", "Extension", "Notes", "JoinDate", "LeaveDate", "StatusName", "CardNo", "IsKaoQin", "UpdateUser", "UpdateDate" };
                    string filePath = MapPath("~/ExportFileTemp/") + DateTime.Now.Ticks + ".xls";
                    NPOIHelper.ExportExcel(list, header, properties, 5000, filePath);
                    PageHelper.ReturnHTTPStream(filePath, true);
                }
            }
        }
        #endregion
    }
}
