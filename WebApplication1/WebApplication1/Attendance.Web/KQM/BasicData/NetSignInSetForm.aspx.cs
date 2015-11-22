/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： NetSignInSetForm
 * 檔功能描述： 網上簽到名單設定UI
 * 
 * 版本：1.0
 * 創建標識： 顧陳偉 2011.12.12
 * 
 */
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using GDSBG.MiABU.Attendance.BLL.KQM.BasicData;
using GDSBG.MiABU.Attendance.Model.KQM.BasicData;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;
using Infragistics.WebUI.UltraWebGrid;
using Resources;

namespace GDSBG.MiABU.Attendance.Web.KQM.BasicData
{
    public partial class NetSignInSetForm : BasePage
    {
        EmployeeModel empmodel = new EmployeeModel();
        NetSignInSetModel model = new NetSignInSetModel();
        static SynclogModel logmodel = new SynclogModel();
        EmployeeBll employeeBll = new EmployeeBll();
        NetSignInSetBll netSignInSetBll = new NetSignInSetBll();
        static DataTable dt_global = new DataTable();
        JavaScriptSerializer JsSerializer = new JavaScriptSerializer();
        Dictionary<string, string> ClientMessage = null;

        #region 頁面加載
        protected void Page_Load(object sender, EventArgs e)
        {
            PageHelper.ButtonControls(FuncList, pnlShowPanel.Controls, base.FuncListModule);
            PageHelper.ButtonControls(FuncList, pnlShowPanel2.Controls, base.FuncListModule);
            if (!base.IsPostBack)
            {
                logmodel.ProcessOwner = CurrentUserInfo.Personcode;
                logmodel.TransactionType = Request.QueryString["ModuleCode"] == null ? "" : Request.QueryString["ModuleCode"].ToString();
                logmodel.LevelNo = "2";
                logmodel.FromHost = Request.UserHostAddress;

                pager.CurrentPageIndex = 1;
                GridDataBind();
                this.ModuleCode.Value = base.Request.QueryString["ModuleCode"].ToString();
            }
            if (ClientMessage == null)
            {
                ClientMessage = new Dictionary<string, string>();
                ClientMessage.Add("EmpNotExist", Message.EmpNotExist);
                ClientMessage.Add("EmpDeleteCheck", Message.EmpDeleteCheck);
                ClientMessage.Add("WrongDate", Message.WrongDate);
                ClientMessage.Add("WorkNoNotNull", Message.WorkNoNotNull);
                ClientMessage.Add("RulesDeleteConfirm", Message.RulesDeleteConfirm);
                ClientMessage.Add("AtLastOneChoose", Message.AtLastOneChoose);
                ClientMessage.Add("StartDateNotNull", Message.StartDateNotNull);
                ClientMessage.Add("EndDateNotNull", Message.EndDateNotNull);
                ClientMessage.Add("EndLaterThanStart", Message.EndLaterThanStart);
                ClientMessage.Add("NoItemSelected", Message.NoItemSelected);
            }
            string clientmsg = JsSerializer.Serialize(ClientMessage);
            Page.ClientScript.RegisterStartupScript(GetType(), "ClientMessage", "var Message=" + clientmsg + ";", true);
            SetCalendar(txtStartDate, txtEndDate);           //日曆控件
        }
        #endregion

        #region 綁定數據
        /// <summary>
        /// 綁定數據
        /// </summary>
        private void GridDataBind()
        {
            int totalCount;
            string sql = SqlDep;
            NetSignInSetModel model = PageHelper.GetModel<NetSignInSetModel>(pnlContent.Controls);
            DataTable dt = netSignInSetBll.GetSignEmpPageInfo(model, sql, pager.CurrentPageIndex, pager.PageSize, out totalCount);
            pager.RecordCount = totalCount;
            this.UltraWebGrid.DataSource = dt.DefaultView;
            this.UltraWebGrid.DataBind();
            pager.TextAfterPageIndexBox = "/" + pager.PageCount.ToString();
            //PageHelper.CleanControlsValue(pnlContent.Controls);
        }
        #endregion

        #region 分頁
        /// <summary>
        /// 分頁
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void pager_PageChanged(object sender, EventArgs e)
        {
            GridDataBind();
        }
        #endregion

        #region ajax驗證
        /// <summary>
        /// ajax驗證
        /// </summary>
        protected override void AjaxProcess()
        {
            if (!string.IsNullOrEmpty(Request.Form["WorkNo"]))
            {
                string empJson = null;
                empmodel.WorkNo = Request.Form["WorkNo"];
                string personcode = CurrentUserInfo.Personcode;
                string modulecode = Request.Form["ModuleCode"];
                string processFlag = Request.Form["ProcessFlag"];
                switch (processFlag)
                {
                    case "check":
                        {
                            DataTable dt = employeeBll.GetEmp(empmodel.WorkNo, personcode, modulecode);
                            if (dt.Rows.Count > 0)
                            {
                                empmodel = employeeBll.GetEmpByKey(empmodel);
                                empJson = JsSerializer.Serialize(empmodel);
                                //empJson = "{'LocalName':'" + dt.Rows[0]["localname"].ToString() + "','DepName':'" + dt.Rows[0]["depname"].ToString() + "'}";
                            }
                            break;
                        }

                }
                Response.Clear();
                Response.Write(empJson);
                Response.End();
            }
        }
        #endregion

        #region 查詢
        /// <summary>
        /// 查詢
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void btnQuery_Click(object sender, EventArgs e)
        {
            pager.CurrentPageIndex = 1;
            GridDataBind();
        }
        #endregion

        #region 刪除
        /// <summary>
        /// 刪除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                string Startenddate = this.UltraWebGrid.DisplayLayout.Rows[i].Cells.FromKey("Startenddate").Text;
                if (chkIsHaveRight.Checked)
                {
                    count++;
                    int num = netSignInSetBll.DeleteNetSignInEmployee(WorkNo, Startenddate,logmodel);
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
            string tableName = "gds_att_temp_signemp";
            string[] columnProperties = { "workno", "localname", "startdate", "enddate" };
            string[] columnType = { "varchar", "varchar", "varchar", "varchar" };
            string createUser = CurrentUserInfo.Personcode;
            string modulecode = this.ModuleCode.Value;
            deFlag = NPOIHelper.DeleteExcelSql(tableName, createUser);
            string filePath = GetImpFileName();
            if (string.IsNullOrEmpty(filePath))
                return;

            flag = NPOIHelper.ImportExcel(columnProperties, columnType, filePath, tableName, createUser);
            if (flag == 1)
            {
                int successnum = 0;
                int errornum = 0;
                DataTable newdt = netSignInSetBll.ImportExcel(createUser, modulecode, out successnum, out errornum,logmodel);
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
                            case "NetSignDateExit": errorInfo = errorInfo + Message.NetSignDateExit;
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

        #region 保存
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            bool flag = false;
            string alert = "";
            model.WorkNo = this.txtEditWorkNo.Text;
            model.Localname = this.txtEditLocalName.Text;
            model.DepName = this.txtEditDepName.Text;
            model.StartDate = Convert.ToDateTime(this.txtStartDate.Text);
            model.EndDate = Convert.ToDateTime(this.txtEndDate.Text);
            string sql = SqlDep;
            if (hidOperate.Value == "Add")
            {
                logmodel.ProcessFlag = "insert";
                DataTable dt = netSignInSetBll.GetWorkNoInfoByUser(model.WorkNo, sql);
                if (dt.Rows[0][0].ToString() == "0")
                {
                    base.Response.Write("<script type='text/javascript'>alert(\"" + Message.EmpNotExist + "\");</script>");
                    return;
                }
                //NetSignInSetModel newmodel = new NetSignInSetModel();
                //newmodel = netSignInSetBll.GetNetSignInEmpByKey(model);
                //if (newmodel!=null)
                //{
                //    alert = "alert('" + Message.DataIsExist + "')";
                //}
                DataTable newdt = netSignInSetBll.GetNetSignInEmp(model.WorkNo, model.StartDate, model.EndDate);
                if (newdt.Rows.Count > 0)
                {
                    alert = "alert('" + Message.NetSignDateExit + "')";
                }
                else
                {
                    model.CreateDate = System.DateTime.Now;
                    model.CreateUser = base.CurrentUserInfo.Personcode;
                    flag = netSignInSetBll.AddNetSignInEmp(model,logmodel);
                    if (flag == true)
                    {
                        alert = "alert('" + Message.AddSuccess + "')";
                    }
                    else
                    {
                        alert = "alert('" + Message.AddFailed + "')";
                    }
                }
            }
            if (hidOperate.Value == "Modify")
            {
                logmodel.ProcessFlag = "update";
                model.UpdateDate = System.DateTime.Now;
                model.UpdateUser = base.CurrentUserInfo.Personcode;
                model.Flag = Convert.ToString('Y');
                DateTime OldStartDate = Convert.ToDateTime(this.StartDate.Value);
                DateTime OldEndDate = Convert.ToDateTime(this.EndDate.Value);
                NetSignInSetModel oldmodel = new NetSignInSetModel();
                oldmodel.WorkNo=this.txtEditWorkNo.Text;
                oldmodel.StartDate = OldStartDate;
                oldmodel.EndDate = OldEndDate;
                DataTable newdt = netSignInSetBll.GetNetSignInEmpForModify(model.WorkNo, model.StartDate, model.EndDate, OldStartDate, OldEndDate);
                if (newdt.Rows.Count > 0)
                {
                    alert = "alert('" + Message.NetSignDateExit + "')";
                }
                else
                {
                    flag = netSignInSetBll.UpdateNetSignInEmpByKey(oldmodel,model,logmodel);
                    if (flag == true)
                    {
                        alert = "alert('" + Message.UpdateSuccess + "')";
                    }
                    else
                    {
                        alert = "alert('" + Message.UpdateFailed + "')";
                    }
                }
            }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Update", alert, true);
            GridDataBind();
        }
        #endregion

        #region 導出
        protected void btnExport_Click(object sender, EventArgs e)
        {
            if (dt_global.Rows.Count == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "nodataexport", "alert('" + Message.NoDataExport + "');", true);
            }
            else
            {
                List<NetSignInSetModel> list = netSignInSetBll.GetList(dt_global);
                string[] header = { ControlText.gvWorkNo, ControlText.gvHeadLocalName, ControlText.gvStartDate, ControlText.gvEndDate };
                string[] properties = { "WorkNo", "Localname", "StartDate", "EndDate" };
                string filePath = MapPath("~/ExportFileTemp/") + DateTime.Now.Ticks + ".xls";
                NPOIHelper.ExportExcel(list, header, properties, 5000, filePath);
                PageHelper.ReturnHTTPStream(filePath, true);
            }
        }
        #endregion

    }
}
