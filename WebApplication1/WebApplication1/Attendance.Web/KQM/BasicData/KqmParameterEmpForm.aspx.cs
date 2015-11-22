
/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： KqmParameterEmpForm.aspx.cs
 * 檔功能描述： 考勤參數設定
 * 
 * 版本：1.0
 * 創建標識： 張明強 2011.12.14
 * 
 */
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using GDSBG.MiABU.Attendance.Model.KQM.BasicData;
using GDSBG.MiABU.Attendance.BLL.KQM.BasicData;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;
using System.Collections.Generic;
using Resources;
using System.Web.Script.Serialization;
using System.IO;

namespace GDSBG.MiABU.Attendance.Web.KQM.BasicData
{
    public partial class KqmParameterEmpForm : BasePage
    {
        JavaScriptSerializer JsSerializer = new JavaScriptSerializer();
        Dictionary<string, string> ClientMessage = null;
        static DataTable paramsEmp;
        KqmParameterEmpBll paramsEmpBll = new KqmParameterEmpBll();
        KqmParameterEmpTempBll paramsEmpTempBll = new KqmParameterEmpTempBll();
        AttKQParamsEmpModel model;
        static DataTable dt_global = new DataTable();
        static SynclogModel logmodel = new SynclogModel();
        #region 頁面加載方法
        /// <summary>
        /// 頁面加載方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            //頁面按鈕添加權限
            PageHelper.ButtonControls(base.FuncList, pnlShowPanel.Controls, base.FuncListModule);

            //彈框內容
            if (ClientMessage == null)
            {
                ClientMessage = new Dictionary<string, string>();
                ClientMessage.Add("AddFailed", Message.AddFailed);
                ClientMessage.Add("AddSuccess", Message.AddSuccess);
                ClientMessage.Add("UpdateSuccess", Message.UpdateSuccess);
                ClientMessage.Add("UpdateFailed", Message.UpdateFailed);
                ClientMessage.Add("DeleteFailed", Message.DeleteFailed);
                ClientMessage.Add("DeleteSuccess", Message.DeleteSuccess);
                ClientMessage.Add("StringFormNotRight", Message.StringFormNotRight);
                ClientMessage.Add("AtLastOneChoose", Message.AtLastOneChoose);
                ClientMessage.Add("ConditionNotNull", Message.ConditionNotNull);
                ClientMessage.Add("SaveConfim", Message.SaveConfim);
                ClientMessage.Add("RulesDeleteConfirm", Message.RulesDeleteConfirm);
                ClientMessage.Add("NotOnlyOne", Message.NotOnlyOne);
                ClientMessage.Add("uploading", Message.uploading);
                ClientMessage.Add("WrongFilePath", Message.WrongFilePath);
                ClientMessage.Add("PathIsNull", Message.PathIsNull);
            }
            string clientmsg = JsSerializer.Serialize(ClientMessage);
            Page.ClientScript.RegisterStartupScript(GetType(), "ClientMessage", "var Message=" + clientmsg + ";", true);

            //按鈕顯示導入/返回控制
            if (!this.PanelImport.Visible)
            {
                this.btnImport.Text = Message.btnImport;
            }
            else
            {
                this.btnImport.Text = Message.btnBack;
            }

            //初次加載頁面綁定數據
            if (!base.IsPostBack)
            {
                logmodel.ProcessOwner = CurrentUserInfo.Personcode;
                logmodel.TransactionType = (Request.QueryString["ModuleCode"] == null) ? "" : Request.QueryString["ModuleCode"].ToString();
                logmodel.LevelNo = "2";
                logmodel.FromHost = Request.UserHostAddress;
                string OrgCode = (base.Request.QueryString["OrgCode"] == null) ? "" : base.Request.QueryString["OrgCode"].ToString();
                this.HiddenOrgCode.Value = OrgCode;
                this.ModuleCode.Value = (base.Request.QueryString["ModuleCode"] == null) ? "" : base.Request.QueryString["ModuleCode"].ToString();
                if (OrgCode.ToString().Length != 0)
                {
                    this.Query();
                }
            }

        }
        #endregion

        #region 查詢
        /// <summary>
        /// 查詢
        /// </summary>
        /// <param name="WindowOpen"></param>
        /// <param name="forwarderType"></param>
        private void Query()
        {
            string sqlDep = base.SqlDep;
            model = new AttKQParamsEmpModel();
            model.OrgCode = this.HiddenOrgCode.Value;
            model.WorkNo = this.txtWorkNo.Text.ToString().Trim();
            model.LocalName = this.txtLocalName.Text.ToString().Trim();
            int totalCount = 0;
            paramsEmp = paramsEmpBll.GetParamsEmpList(model, sqlDep, pager.CurrentPageIndex, pager.PageSize, out totalCount);
            pager.RecordCount = totalCount;
            pager.TextAfterPageIndexBox = "/" + pager.PageCount.ToString();
            if (paramsEmp != null)
            {
                this.DataUIBind();
            }
        }

        /// <summary>
        /// 查詢按鈕事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            this.Query();
        }

        #endregion

        #region 刪除
        /// <summary>
        /// 刪除按鈕事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            logmodel.ProcessFlag = "delete";
            string alert = "";
            model = new AttKQParamsEmpModel();
            model.WorkNo = this.UltraWebGrid.Rows[this.UltraWebGrid.DisplayLayout.SelectedRows[0].Index].Cells[0].Text.Trim();
            if (paramsEmpBll.DeleteKQMParamsEmpData(model, logmodel) > 0)
            {
                alert = "alert('" + Message.DeleteSuccess + "')";
            }
            else
            {
                alert = "alert('" + Message.DeleteFailed + "')";
            }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "show", alert, true);
            model = new AttKQParamsEmpModel();
            this.Query();

        }
        #endregion

        #region 導入
        /// <summary>
        /// 導入按鈕事件
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
                //this.btnQuery.Enabled = false;
                //this.btnReset.Enabled = false;
                //this.btnDelete.Enabled = false;
                this.btnImport.Text = Message.btnBack;
                //this.lbluploadMsg.Text = "";
                this.btnImportSave.Enabled = true;
            }
            else
            {
                this.PanelImport.Visible = false;
                this.PanelData.Visible = true;
                //this.btnQuery.Enabled = true;
                //this.btnReset.Enabled = true;
                //this.btnDelete.Enabled = true;
                this.btnImport.Text = Message.btnImport;
                this.btnImportSave.Enabled = false;
            }
        }

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
            string tableName = "GDS_ATT_KQPARAMSEMP_TEMP";
            string[] columnProperties = { "WorkNo", "BellNo", "IsNotKaoQin" };
            string[] columnType = { "varchar", "varchar", "varchar" };
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
            DataTable dt = paramsEmpTempBll.GetTempTableErrorData(CurrentUserInfo.Personcode, out succesnum, out errornum, logmodel);
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
                            case "ErrIsNotKaoQin": errorInfo = errorInfo + Message.ErrIsNotKaoQin + ",";
                                break;
                            case "ErrWorknoNotFind": errorInfo = errorInfo + Message.ErrWorknoNotFind + ",";
                                break;
                            case "ErrBellNoNotExist": errorInfo = errorInfo + Message.ErrBellNoNotExist + ",";
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

        #region 頁面綁定數據
        /// <summary>
        /// 頁面綁定數據
        /// </summary>
        private void DataUIBind()
        {
            this.UltraWebGrid.DataSource = paramsEmp.DefaultView;
            this.UltraWebGrid.DataBind();

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
                    model = new AttKQParamsEmpModel();
                    model.OrgCode = this.HiddenOrgCode.Value;
                    model.WorkNo = this.txtWorkNo.Text.ToString().Trim();
                    model.LocalName = this.txtLocalName.Text.ToString().Trim();
                    List<AttKQParamsEmpModel> list = paramsEmpBll.GetParamsEmpList(model, base.SqlDep.ToString());
                    string[] header = { ControlText.gvHeadParamsWorkNo, ControlText.gvHeadParamsLocalName, ControlText.gvHeadParamsBellNo, ControlText.gvHeadParamsIsNotKaoQin };
                    string[] properties = { "WorkNo", "LocalName", "BellNo", "IsNotKaoQin" };
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
                    List<AttKQParamsEmpTempModel> list = paramsEmpTempBll.GetList(dt_global);
                    string[] header = { ControlText.gvHeadParamsErrorMsg, ControlText.gvHeadParamsWorkNo, ControlText.gvHeadParamsLocalName, ControlText.gvHeadParamsBellNo, ControlText.gvHeadParamsIsNotKaoQin };
                    string[] properties = { "ErrorMsg", "WorkNo", "LocalName", "BellNo", "IsNotKaoQin" };
                    string filePath = MapPath("~/ExportFileTemp/") + DateTime.Now.Ticks + ".xls";
                    NPOIHelper.ExportExcel(list, header, properties, 5000, filePath);
                    PageHelper.ReturnHTTPStream(filePath, true);
                }

            }
        }
        #endregion

        #region 返回頁面狀態
        /// <summary>
        /// 返回頁面狀態
        /// </summary>
        /// <returns></returns>
        public int GetTableNum()
        {
            int num = 0;
            if (!this.PanelImport.Visible)
            {
                string OrgCode = (base.Request.QueryString["OrgCode"] == null) ? "" : base.Request.QueryString["OrgCode"].ToString();
                if (OrgCode.ToString().Length != 0)
                {
                    num = 1;
                }
                else
                {
                    num = 0;
                }
            }
            else
            {
                num = -1;
            }
            return num;
        }
        #endregion

        #region 分頁
        protected void pager_PageChanged(object sender, EventArgs e)
        {
            Query();
        }
        #endregion
    }
}
