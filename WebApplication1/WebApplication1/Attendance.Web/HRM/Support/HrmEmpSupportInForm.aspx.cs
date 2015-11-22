﻿/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： HrmEmpSupportInForm
 * 檔功能描述： 內部支援UI
 * 
 * 版本：1.0
 * 創建標識： 顧陳偉 2012.01.06
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
using GDSBG.MiABU.Attendance.BLL.HRM.Support;
using GDSBG.MiABU.Attendance.BLL.KQM.BasicData;
using GDSBG.MiABU.Attendance.Model.HRM.Support;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;
using Infragistics.WebUI.UltraWebGrid;
using Resources;

namespace GDSBG.MiABU.Attendance.Web.HRM.Support
{
    public partial class HrmEmpSupportInForm : BasePage
    {
        EmpSupportInModel model = new EmpSupportInModel();
        static SynclogModel logmodel = new SynclogModel();
        EmpSupportInBll empSupportInBll = new EmpSupportInBll();
        TWCadreBll twCadreBll = new TWCadreBll();
        TypeDataBll typeDataBll = new TypeDataBll();
        static DataTable dt_global = new DataTable();
        JavaScriptSerializer JsSerializer = new JavaScriptSerializer();
        Dictionary<string, string> ClientMessage = null;

        #region 頁面加載
        protected void Page_Load(object sender, EventArgs e)
        {
            PageHelper.ButtonControls(FuncList, pnlShowPanel.Controls, base.FuncListModule);
            SetCalendar(txtStartDate1, txtStartDate2, txtPrepEndDate1, txtPrepEndDate2);
            if (!base.IsPostBack)
            {
                logmodel.ProcessOwner = CurrentUserInfo.Personcode;
                logmodel.TransactionType = Request.QueryString["ModuleCode"] == null ? "" : Request.QueryString["ModuleCode"].ToString();
                logmodel.LevelNo = "2";
                logmodel.FromHost = Request.UserHostAddress;

                this.ddlDataBind();
                this.ModuleCode.Value = base.Request.QueryString["ModuleCode"].ToString();
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

        #region 分頁
        protected void pager_PageChanged(object sender, EventArgs e)
        {
            DataUIBind();
        }
        #endregion

        #region 刪除
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            logmodel.ProcessFlag = "delete";
            int intDeleteOk = 0;
            int intDeleteError = 0;
            int count = 0;
            TemplatedColumn tcol = (TemplatedColumn)this.UltraWebGridSupportIn.Bands[0].Columns[0];

            for (int i = 0; i < this.UltraWebGridSupportIn.Rows.Count; i++)
            {
                CellItem GridItem = (CellItem)tcol.CellItems[i];
                CheckBox chkIsHaveRight = (CheckBox)GridItem.FindControl("CheckBoxCell");
                model.WorkNo = this.UltraWebGridSupportIn.DisplayLayout.Rows[i].Cells.FromKey("WorkNo").Text;
                model.SupportOrder = this.UltraWebGridSupportIn.DisplayLayout.Rows[i].Cells.FromKey("SupportOrder").Text;
                if (chkIsHaveRight.Checked)
                {
                    count++;
                    int num = empSupportInBll.DeleteEmpSupportIn(model,logmodel);
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
                DataUIBind();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "isnumber", "alert('" + Message.AtLastOneChoose + "');", true);
                return;
            }
        }
        #endregion

        #region 導出
        protected void btnExport_Click(object sender, EventArgs e)
        {
            if (this.ImportFlag.Value != "Import")
            {
                string LevelCondition = "";
                string ManagerCondition = "";
                model = PageHelper.GetModel<EmpSupportInModel>(pnlContent.Controls, txtDepCode, txtDepName);
                string depname = this.txtDepName.Text;
                if (!string.IsNullOrEmpty(this.ddlLevelName.SelectedValue.ToString()))
                {

                    string[] temVal = ddlLevelName.SelectedValuesToString(",").Split(',');
                    for (int iLoop = 0; iLoop < temVal.Length; iLoop++)
                    {
                        LevelCondition += "'" + temVal[iLoop] + "',";
                    }
                    LevelCondition = LevelCondition.Substring(0, LevelCondition.Length - 1);

                }
                if (!string.IsNullOrEmpty(this.ddlManagerName.SelectedValue.ToString()))
                {
                    string[] temVal = ddlManagerName.SelectedValuesToString(",").Split(',');
                    for (int iLoop = 0; iLoop < temVal.Length; iLoop++)
                    {
                        ManagerCondition += "'" + temVal[iLoop] + "',";
                    }
                    ManagerCondition = ManagerCondition.Substring(0, ManagerCondition.Length - 1);
                }
                string StartDateFrom = this.txtStartDate1.Text.Trim();
                string StartDateTo = this.txtStartDate2.Text.Trim();
                string PrepEndDateFrom = this.txtPrepEndDate1.Text.Trim();
                string PrepEndDateTo = this.txtPrepEndDate2.Text.Trim();
                DataTable dt = empSupportInBll.GetEmpSupportInForExport(model, depname, LevelCondition, ManagerCondition, StartDateFrom, StartDateTo, PrepEndDateFrom, PrepEndDateTo);
                if (dt.Rows.Count == 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "nodataexport", "alert('" + Message.NoDataExport + "');", true);
                }
                else
                {
                    List<EmpSupportInModel> list = empSupportInBll.GetList(dt);
                    string[] header = { ControlText.gvWorkNo, ControlText.gvHeadLocalName, ControlText.gvSex, ControlText.gvSupportOrder, ControlText.gvSupportDeptName, ControlText.gvEmpStartDate, ControlText.gvPrepEndDate, ControlText.gvRealEndDate, ControlText.gvStateName, ControlText.gvRemark };
                    string[] properties = { "WorkNo", "LocalName", "SexName", "SupportOrder", "SupportDept", "StartDate", "PrepEndDate", "EndDate", "StateName", "Remark" };
                    string filePath = MapPath("~/ExportFileTemp/") + DateTime.Now.Ticks + ".xls";
                    NPOIHelper.ExportExcel(list, header, properties, 5000, filePath);
                    PageHelper.ReturnHTTPStream(filePath, true);
                }
            }
            else
            {
                if (dt_global.Rows.Count == 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "nodataexport", "alert('" + Message.NoDataExport + "');", true);
                }
                else
                {
                    List<EmpSupportInModel> list = empSupportInBll.GetList(dt_global);
                    string[] header = { ControlText.gvWorkNo, ControlText.gvCostCode, ControlText.gvSupportDeptName, ControlText.gvEmpStartDate, ControlText.gvPrepEndDate, ControlText.gvRealEndDate, ControlText.gvRemark };
                    string[] properties = { "WorkNo", "CostCode", "SupportDeptName", "StartDate", "PrepEndDate", "EndDate", "Remark" };
                    string filePath = MapPath("~/ExportFileTemp/") + DateTime.Now.Ticks + ".xls";
                    NPOIHelper.ExportExcel(list, header, properties, 5000, filePath);
                    PageHelper.ReturnHTTPStream(filePath, true);
                }
            }
        }
        #endregion
       
        #region 導入
        protected void btnImportSave_Click(object sender, EventArgs e)
        {
            ImportExcel();
            this.ImportFlag.Value = "Import";
        }
        #endregion

        #region 將文件中的數據導入到數據庫中
        private void ImportExcel()
        {
            int flag;
            bool deFlag;
            string tableName = "gds_att_temp_empsupportin";
            string[] columnProperties = { "workno", "costcode", "supportdeptname", "startdate", "prependdate", "enddate", "remark" };
            string[] columnType = { "varchar", "varchar", "varchar", "varchar", "varchar", "varchar", "varchar" };
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
                DataTable newdt = empSupportInBll.ImpoertExcel(createUser, out successnum, out errornum,logmodel);
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
                            case "ErrCostCodeNull": errorInfo = errorInfo + Message.ErrCostCodeNull;
                                break;
                            case "ErrSupportDeptNameNull": errorInfo = errorInfo + Message.ErrSupportDeptNameNull;
                                break;
                            case "ErrSupportDeptNotExist": errorInfo = errorInfo + Message.ErrSupportDeptNotExist;
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

        #region 查詢
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            pager.CurrentPageIndex = 1;
            DataUIBind();
        }
        #endregion

        #region 數據綁定
        private void DataUIBind()
        {
            int totalCount;
            string LevelCondition = "";
            string ManagerCondition = "";
            model = PageHelper.GetModel<EmpSupportInModel>(pnlContent.Controls,txtDepCode,txtDepName);
            string depname = this.txtDepName.Text;
            if (!string.IsNullOrEmpty(this.ddlLevelName.SelectedValue.ToString()))
            {

                string[] temVal = ddlLevelName.SelectedValuesToString(",").Split(',');
                for (int iLoop = 0; iLoop < temVal.Length; iLoop++)
                {
                    LevelCondition += "'" + temVal[iLoop] + "',";
                }
                LevelCondition = LevelCondition.Substring(0, LevelCondition.Length - 1);

            }
            if (!string.IsNullOrEmpty(this.ddlManagerName.SelectedValue.ToString()))
            {
                string[] temVal = ddlManagerName.SelectedValuesToString(",").Split(',');
                for (int iLoop = 0; iLoop < temVal.Length; iLoop++)
                {
                    ManagerCondition += "'" + temVal[iLoop] + "',";
                }
                ManagerCondition = ManagerCondition.Substring(0, ManagerCondition.Length - 1);
            }
            string StartDateFrom = this.txtStartDate1.Text.Trim();
            string StartDateTo = this.txtStartDate2.Text.Trim();
            string PrepEndDateFrom = this.txtPrepEndDate1.Text.Trim();
            string PrepEndDateTo = this.txtPrepEndDate2.Text.Trim();
            DataTable dt = empSupportInBll.GetEmpSupportInPageInfo(model,depname, LevelCondition, ManagerCondition, StartDateFrom, StartDateTo, PrepEndDateFrom, PrepEndDateTo, pager.CurrentPageIndex, pager.PageSize, out totalCount);
            pager.RecordCount = totalCount;
            this.UltraWebGridSupportIn.DataSource = dt.DefaultView;
            this.UltraWebGridSupportIn.DataBind();
            pager.TextAfterPageIndexBox = "/" + pager.PageCount.ToString();
        }
        #endregion

        #region 重置
        protected void btnReset_Click(object sender, EventArgs e)
        {
            this.txtDepCode.Text = "";
            this.txtDepName.Text = "";
            this.txtWorkNo.Text = "";
            this.txtLocalName.Text = "";
            this.txtStartDate1.Text = "";
            this.txtStartDate2.Text = "";
            this.txtPrepEndDate1.Text = "";
            this.txtPrepEndDate2.Text = "";
            this.ddlLevelName.ClearSelection();
            this.ddlManagerName.ClearSelection();
            this.ddlState.ClearSelection();
        }
        #endregion

        #region 下拉菜單綁定
        private void ddlDataBind()
        {
            DataTable dt = new DataTable();
            dt = twCadreBll.GetLevel();
            this.ddlLevelName.DataSource = dt.DefaultView;
            this.ddlLevelName.DataTextField = "LevelName";
            this.ddlLevelName.DataValueField = "LevelCode";
            this.ddlLevelName.DataBind();
            dt.Clear();
            dt = twCadreBll.GetManager();
            this.ddlManagerName.DataSource = dt.DefaultView;
            this.ddlManagerName.DataTextField = "ManagerName";
            this.ddlManagerName.DataValueField = "ManagerCode";
            this.ddlManagerName.DataBind();
            dt.Clear();
            dt = typeDataBll.GetSupportStatusList();
            this.ddlState.DataSource = dt.DefaultView;
            this.ddlState.DataTextField = "DataValue";
            this.ddlState.DataValueField = "DataCode";
            this.ddlState.DataBind();
            this.ddlState.Items.Insert(0, new ListItem("", ""));
        }
        #endregion

    }
}