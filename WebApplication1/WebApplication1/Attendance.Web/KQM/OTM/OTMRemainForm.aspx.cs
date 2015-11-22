/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： OTMRemainForm
 * 檔功能描述： 剩余加班導入UI
 * 
 * 版本：1.0
 * 創建標識： 顧陳偉 2011.12.23
 * 
 */
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using GDSBG.MiABU.Attendance.BLL.KQM.OTM;
using GDSBG.MiABU.Attendance.Model.KQM.OTM;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;
using Infragistics.WebUI.UltraWebGrid;
using Resources;

namespace GDSBG.MiABU.Attendance.Web.KQM.OTM
{
    public partial class OTMRemainForm : BasePage
    {
        static DataTable dt_global = new DataTable();
        string module_code = "";
        OTMRemainModel model = new OTMRemainModel();
        static SynclogModel logmodel = new SynclogModel();
        OTMRemainBll oTMRemainBll = new OTMRemainBll();
        JavaScriptSerializer JsSerializer = new JavaScriptSerializer();
        Dictionary<string, string> ClientMessage = null;

        #region 頁面加載
        protected void Page_Load(object sender, EventArgs e)
        {
            PageHelper.ButtonControls(FuncList, pnlShowPanel.Controls, base.FuncListModule);
            if (!base.IsPostBack)
            {
                logmodel.ProcessOwner = CurrentUserInfo.Personcode;
                logmodel.TransactionType = Request.QueryString["ModuleCode"] == null ? "" : Request.QueryString["ModuleCode"].ToString();
                logmodel.LevelNo = "2";
                logmodel.FromHost = Request.UserHostAddress;

                module_code = base.Request.QueryString["ModuleCode"];
                this.ModuleCode.Value = module_code;
                this.txtYearMonth.Text = DateTime.Now.AddMonths(-1).ToString("yyyy/MM");
            }
            if (ClientMessage == null)
            {
                ClientMessage = new Dictionary<string, string>();
                ClientMessage.Add("DepNameOrWorkNoNotNull", Message.DepNameOrWorkNoNotNull);
                ClientMessage.Add("DeleteAttTypeConfirm", Message.DeleteAttTypeConfirm);
            }
            string clientmsg = JsSerializer.Serialize(ClientMessage);
            Page.ClientScript.RegisterStartupScript(GetType(), "ClientMessage", "var Message=" + clientmsg + ";", true);
        }
        #endregion

        #region 分頁
        protected void pager_PageChanged(object sender, EventArgs e)
        {
            Query();
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
                string YearMonth = Convert.ToDateTime(this.UltraWebGrid.Rows[i].Cells.FromKey("YearMonth").Text.Insert(4, "/") + "/01").ToString("yyyyMM");
                if (chkIsHaveRight.Checked)
                {
                    count++;
                    int num = oTMRemainBll.DeleteRemain(WorkNo, YearMonth,logmodel);
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
                Query();
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
            if (this.ImportFlag.Value == "Import")
            {
                if (dt_global.Rows.Count == 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "nodataexport", "alert('" + Message.NoDataExport + "');", true);
                }
                else
                {
                    List<OTMRemainModel> list = oTMRemainBll.GetList(dt_global);
                    string[] header = { ControlText.gvWorkNo, ControlText.gvYearMonth, ControlText.gvG1Remain, ControlText.gvG23Remain };
                    string[] properties = { "WorkNo", "YearMonth", "G1Remain", "G23Remain" };
                    string filePath = MapPath("~/ExportFileTemp/") + DateTime.Now.Ticks + ".xls";
                    NPOIHelper.ExportExcel(list, header, properties, 5000, filePath);
                    PageHelper.ReturnHTTPStream(filePath, true);
                }
            }
            else
            {
                string sql = base.SqlDep;
                string YearMonth = this.txtYearMonth.Text.Replace("/", "");
                model = PageHelper.GetModel<OTMRemainModel>(pnlContent.Controls);
                if (model != null)
                {
                    model.YearMonth = this.txtYearMonth.Text.Replace("/", "");
                }
                DataTable dt = oTMRemainBll.GetAllRemainForExport(model, sql);
                if (dt.Rows.Count == 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "nodataexport", "alert('" + Message.NoDataExport + "');", true);
                }
                else
                {
                    List<OTMRemainModel> list = oTMRemainBll.GetList(dt);
                    string[] header = { ControlText.gvWorkNo, ControlText.gvHeadLocalName, ControlText.gvOverTimeType, ControlText.gvYearMonth, ControlText.gvG1Remain, ControlText.gvG23Remain };
                    string[] properties = { "WorkNo", "LocalName", "OverTimeType", "YearMonth", "G1Remain", "G23Remain" };
                    string filePath = MapPath("~/ExportFileTemp/") + DateTime.Now.Ticks + ".xls";
                    NPOIHelper.ExportExcel(list, header, properties, 5000, filePath);
                    PageHelper.ReturnHTTPStream(filePath, true);
                }
            }
        }
        #endregion 

        #region 導入功能
        protected void btnImportSave_Click(object sender, EventArgs e)
        {
            ImportExcel();
            this.ImportFlag.Value = "Import";
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

        #region 將文件中的數據導入到數據庫中
        /// <summary>
        /// 將文件中的數據導入到數據庫中
        /// </summary>
        private void ImportExcel()
        {
            int flag;
            bool deFlag;
            string tableName = "gds_att_temp_remain";
            string[] columnProperties = { "workno", "yearmonth", "g1remain", "g23remain" };
            string[] columnType = { "varchar", "varchar", "varchar", "varchar" };
            string createUser = base.CurrentUserInfo.Personcode;
            string moduleCode = this.ModuleCode.Value;
            deFlag = NPOIHelper.DeleteExcelSql(tableName, createUser);
            string filePath = GetImpFileName();
            if (string.IsNullOrEmpty(filePath))
                return;

            flag = NPOIHelper.ImportExcel(columnProperties, columnType, filePath, tableName, createUser);
            if (flag == 1)
            {
                int successnum = 0;
                int errornum = 0;
                DataTable newdt =oTMRemainBll.ImportExcel(createUser,moduleCode, out successnum, out errornum,logmodel);
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
                            case "ErrWorkNoRepeat": errorInfo = errorInfo + Message.ErrWorkNoRepeat;
                                break;
                            case "ErrYearMonth": errorInfo = errorInfo + Message.ErrYearMonth;
                                break;
                            case "ErrRemain": errorInfo = errorInfo + Message.ErrRemain;
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

        #region 查詢
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            pager.CurrentPageIndex = 1;
            Query();
        }
        #endregion

        #region 重置
        protected void btnReset_Click(object sender, EventArgs e)
        {
            this.txtDepCode.Text = "";
            this.txtDepName.Text = "";
            this.txtWorkNo.Text = "";
            this.txtLocalName.Text = "";
            this.txtYearMonth.Text = DateTime.Now.ToString("yyyy/MM");
        }
        #endregion

        #region 查詢方法
        protected void Query()
        {
            string sql = base.SqlDep;
            string YearMonth = this.txtYearMonth.Text.Replace("/", "");
            int totalCount=0;
            model = PageHelper.GetModel<OTMRemainModel>(pnlContent.Controls);
            if (model != null)
            {
                model.YearMonth=this.txtYearMonth.Text.Replace("/", "");
                DataTable dt = oTMRemainBll.GetAllRemainInfo(model,sql, pager.CurrentPageIndex, pager.PageSize, out totalCount);
                pager.RecordCount = totalCount;
                this.UltraWebGrid.DataSource = dt.DefaultView;
                this.UltraWebGrid.DataBind();
                pager.TextAfterPageIndexBox = "/" + pager.PageCount.ToString();
            }

        }
        #endregion


    }
}
