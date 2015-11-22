using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Resources;
using GDSBG.MiABU.Attendance.BLL.WorkFlow;
using GDSBG.MiABU.Attendance.Model.WorkFlow;
using Infragistics.WebUI.UltraWebGrid;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;


namespace GDSBG.MiABU.Attendance.Web.WorkFlow
{
    public partial class WorkFlowProjectApply : BasePage
    {
        WFProjectApplyBll ocmb = new WFProjectApplyBll();
        protected System.Data.DataSet dataSet,tempDataSet;
        protected System.Data.DataTable tempDataTable;
        Dictionary<string, string> ClientMessage = null;
        JavaScriptSerializer JsSerializer = new JavaScriptSerializer();

        static SynclogModel logmodel = new SynclogModel();

        private string dateFormat = "yyyy/MM/dd";
        public string LHZBLimitTime = "";

        #region Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (ClientMessage == null)
            {
                ClientMessage = new Dictionary<string, string>();
                ClientMessage.Add("AtLastOneChoose", Message.AtLastOneChoose);
                ClientMessage.Add("AuditUnaudit", Message.AuditUnaudit);
                ClientMessage.Add("checkapproveflag", Message.checkapproveflag);
                ClientMessage.Add("DeleteApplyovertimeEnd", Message.DeleteApplyovertimeEnd);
                ClientMessage.Add("copy_sesse_1", Message.copy_sesse_1);
                ClientMessage.Add("DataReturn", Message.DataReturn);
                ClientMessage.Add("AuditUncancelaudit", Message.AuditUncancelaudit);
                ClientMessage.Add("NoSelect", Message.NoSelect);
                ClientMessage.Add("DdeleteApplyovertimeEnd", Message.DdeleteApplyovertimeEnd);
               
                //ClientMessage.Add("AuditUnaudit", Message.AuditUnaudit);
                ClientMessage.Add("choess_no", Message.choess_no);
                ClientMessage.Add("common_monday", Message.common_monday);
                ClientMessage.Add("common_tuesday", Message.common_tuesday);
                ClientMessage.Add("common_wednesday", Message.common_wednesday);
                ClientMessage.Add("common_thursday", Message.common_thursday);
                ClientMessage.Add("common_friday", Message.common_friday);
                ClientMessage.Add("common_saturday", Message.common_saturday);

                string clientmsg = JsSerializer.Serialize(ClientMessage);
                Page.ClientScript.RegisterStartupScript(GetType(), "ClientMessage", "var Message=" + clientmsg + ";", true);
            }

            #region
            //try
            //{
            //    ((ImageButton)this.PageNavigator.FindControl("ImageButtonPrevious")).Click += new ImageClickEventHandler(this.ButtonPrevious_Click);
            //    ((ImageButton)this.PageNavigator.FindControl("ImageButtonNext")).Click += new ImageClickEventHandler(this.ButtonNext_Click);
            //    ((ImageButton)this.PageNavigator.FindControl("ImageButtonGoto")).Click += new ImageClickEventHandler(this.ButtonGoto_Click);
            //    try
            //    {
            //        this.LHZBLimitTime = ConfigurationManager.AppSettings["LHZBLimitTime"];
            //    }
            //    catch
            //    {
            //        this.LHZBLimitTime = "";
            //    }
            //    if (!base.IsPostBack)
            //    {
            //        this.Internationalization();
            //        this.ddlDataBind();
            //        this.ImageDepCode.Attributes.Add("OnClick", "javascript:GetTreeDataValue('textBoxDepCode','Department','',\"WHERE companyid='" + this.Session["companyID"].ToString() + "' \",'" + BaseForm.sAppPath + "','" + base.Request["moduleCode"] + "','textBoxDepName')");
            //        this.textBoxOTDateFrom.Attributes.Add("onfocus", "calendar('" + base.Language + "','" + base.dateFormat + "');");
            //        this.textBoxOTDateFrom.Attributes.Add("formater", base.dateFormat);


            //        this.textBoxOTDateFrom.Text = DateTime.Now.AddDays(-2.0).ToString(base.dateFormat);
            //        this.textBoxOTDateTo.Attributes.Add("onfocus", "calendar('" + base.Language + "','" + base.dateFormat + "');");
            //        this.textBoxOTDateTo.Attributes.Add("formater", base.dateFormat);

            //        this.textBoxOTDateTo.Text = DateTime.Now.AddDays(2.0).ToString(base.dateFormat);
            //        this.ModuleCode.Value = base.Request["ModuleCode"].ToString();
            //        this.ImageBatchWorkNo.Attributes.Add("OnClick", "javascript:ShowBatchWorkNo()");
            //        if (!string.IsNullOrEmpty(this.LHZBLimitTime) && (DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd") + " " + this.LHZBLimitTime) < DateTime.Now))
            //        {
            //            this.ButtonSendAudit.Enabled = false;
            //        }
            //    }
            //}
            ////catch (Exception ex)
            ////{
            ////    base.WriteMessage(2, (ex.InnerException == null) ? ex.Message : ex.InnerException.Message);
            ////}
            #endregion
            try
            {
                LHZBLimitTime = System.Configuration.ConfigurationManager.AppSettings["LHZBLimitTime"];
            }
            catch
            {
                LHZBLimitTime = "";
            }
            if (!IsPostBack)
            {
                logmodel.ProcessOwner = CurrentUserInfo.Personcode;
                logmodel.TransactionType = (Request.QueryString["ModuleCode"] == null) ? "" : Request.QueryString["ModuleCode"].ToString();
                logmodel.LevelNo = "2";
                logmodel.FromHost = Request.UserHostAddress;

                //this.ImageDepCode.Attributes.Add("OnClick", "javascript:GetTreeDataValue('textBoxDepCode','Department','',\"WHERE companyid='" + this.Session["companyID"].ToString() + "' \",'" + BaseForm.sAppPath + "','" + base.Request["moduleCode"] + "','textBoxDepName')");
                this.textBoxOTDateFrom.Text = DateTime.Now.AddDays(-2.0).ToString(this.dateFormat);
                this.textBoxOTDateTo.Text = DateTime.Now.AddDays(2.0).ToString(this.dateFormat);
                this.ModuleCode.Value = Request.QueryString["ModuleCode"];
                this.ddlDataBind();

                if (!string.IsNullOrEmpty(LHZBLimitTime))
                {
                    //每天LHZBLimitTime時間后不能新增、修改加班預報

                    DateTime dtLimit = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd") + " " + LHZBLimitTime);
                    if (dtLimit < DateTime.Now)
                    {
                        this.btnSendAudit.Enabled = false;
                    }
                }

                this.Query(true, "Goto");
            }
            //SetSelector(imgDepCode, txtDepCode, txtDepName, "dept", Request.QueryString["ModuleCode"]);
            SetCalendar(textBoxOTDateFrom, textBoxOTDateTo);
            SetSelector(ImageDepCode, textBoxDepCode, textBoxDepName, "dept", Request.QueryString["ModuleCode"]);
            HiddenWorkNo.Value = CurrentUserInfo.Personcode;
            PageHelper.ButtonControlsWF(FuncList, pnlShowPanel.Controls, base.FuncListModule);
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
            ctrlTrigger.Attributes.Add("onclick", string.Format("return setSelector('{0}','{1}','{2}')",
                ctrlCode.ClientID, ctrlName.ClientID, moduleCode));
        }
        #endregion

        #region ddlDataBind
        protected void ddlDataBind()
        {
            tempDataSet = new DataSet();
            //tempDataSet = ((eBFW.Sys.ServiceLocator)Session["serviceLocator"]).GetHrmFixedlTypeData().GetDataByCondition("WHERE DataType='OTMAdvanceApplyStatus' ORDER BY OrderId");
            tempDataSet = ocmb.GetFixedDataByCondition("WHERE DataType='OTMAdvanceApplyStatus' ORDER BY OrderId");
            this.ddlOTStatus.DataSource = tempDataSet.Tables[0].DefaultView;
            this.ddlOTStatus.DataTextField = "DataValue";
            this.ddlOTStatus.DataValueField = "DataCode";
            this.ddlOTStatus.DataBind();
            this.ddlOTStatus.Items.Insert(0, new ListItem("", ""));
            this.ddlOTStatus.SelectedIndex = this.ddlOTStatus.Items.IndexOf(this.ddlOTStatus.Items.FindByValue("0"));

            tempDataSet.Clear();
            //tempDataSet = ((eBFW.Sys.ServiceLocator)Session["serviceLocator"]).GetHrmFixedlTypeData().GetDataByCondition("WHERE DataType='OverTimeType' ORDER BY OrderId");
            tempDataSet = ocmb.GetFixedDataByCondition("WHERE DataType='OverTimeType' ORDER BY OrderId");
            this.ddlPersonType.DataSource = tempDataSet.Tables[0].DefaultView;
            this.ddlPersonType.DataTextField = "DataValue";
            this.ddlPersonType.DataValueField = "DataCode";
            this.ddlPersonType.DataBind();
            this.ddlPersonType.Items.Insert(0, new ListItem("", ""));
        }
        #endregion

        #region Query
        private void Query(bool WindowOpen, string forwarderType)
        {
            string condition = " and a.isProject='Y' ";
            if (this.ProcessFlag.Value.ToLower().Equals("condition") || WindowOpen)
            {
                #region 
                //if (this.textBoxDepName.Text.Trim().Length > 0)
                //{
                ////    if (base.bPrivileged)
                ////    {
                //    string CS00001 = condition;
                //    string varcsd = condition;
                //    condition = varcsd + " AND b.dCode IN ((" + base.SqlDep + ") INTERSECT SELECT DepCode FROM GDS_SC_DEPARTMENT START WITH depname = '" + this.textBoxDepName.Text.Trim() + "' CONNECT BY PRIOR depcode = parentdepcode) ";
                ////    }
                ////    else
                ////    {
                ////        condition = condition + " AND b.dCode IN (SELECT DepCode FROM bfw_department START WITH depname = '" + this.textBoxDepName.Text.Trim() + "' CONNECT BY PRIOR depcode = parentdepcode) ";
                ////    }
                //}
                //else
                //{
                //    condition = condition + " AND b.dcode in (" + base.SqlDep + ")";
                //}
                #endregion

                if (this.textBoxDepName.Text.Trim().Length > 0)
                {
                    condition = condition + " AND b.dCode IN (SELECT DepCode FROM GDS_SC_DEPARTMENT START WITH depname = '" + this.textBoxDepName.Text.Trim() + "' CONNECT BY PRIOR depcode = parentdepcode) ";
                }
                if (this.textBoxBillNo.Text.Trim().Length != 0)
                {
                    condition += " AND a.BillNo like '" + this.textBoxBillNo.Text.Trim() + "%'";
                }
                if (this.textBoxEmployeeNo.Text.Trim().Length != 0)
                {
                    condition += " AND b.WorkNO = '" + this.textBoxEmployeeNo.Text.Trim().ToUpper() + "'";
                }

                if (this.textBoxBatchEmployeeNo.Text.Trim().Length != 0)
                {
                    string[] workNoList = this.textBoxBatchEmployeeNo.Text.Trim().Split(Convert.ToChar((char)13));
                    string ddlStr = "";
                    for (int i = 0; i < workNoList.Length; i++)
                    {
                        if (workNoList[i].ToString().Length > 0)
                        {
                            ddlStr += " b.WorkNo='" + workNoList[i].ToString().Trim().ToUpper().Replace("\n", "") + "' or";
                        }
                    }
                    ddlStr = ddlStr.Substring(0, ddlStr.Length - 2);
                    condition += " and (" + ddlStr + ")";
                }
                if (this.textBoxName.Text.Trim().Length != 0)
                {
                    condition += " AND b.LocalName like '" + this.textBoxName.Text.Trim() + "%'";
                }
                if (this.ddlPersonType.SelectedValue.Length != 0)
                {
                    condition += " AND b.OverTimeType = '" + this.ddlPersonType.SelectedValue + "'";
                }
                if (this.ddlOTType.SelectedValue.Length != 0)
                {
                    condition += " AND a.OTType = '" + this.ddlOTType.SelectedValue + "'";
                }
                if (this.ddlOTStatus.SelectedValue.Length != 0)
                {
                    condition += " AND a.Status='" + this.ddlOTStatus.SelectedValue + "'";
                }
                if (this.textBoxEmployeeNo.Text.Trim().Length == 0 &&
                    this.textBoxName.Text.Trim().Length == 0 &&
                    this.textBoxBillNo.Text.Trim().Length == 0)
                {
                    if (this.textBoxOTDateFrom.Text.Trim().Length == 0 || this.textBoxOTDateTo.Text.Trim().Length == 0)
                    {
                        this.WriteMessage(1, Message.common_message_datenotnull);
                        return;
                    }
                    else
                    {
                        if (!CheckDateMonths(this.textBoxOTDateFrom.Text.Trim(), this.textBoxOTDateTo.Text.Trim()))
                        {
                            return;
                        }
                    }
                }
                if (this.textBoxOTDateFrom.Text.Trim().Length > 0)
                {
                    condition += " AND a.OTDate >= to_date('" + DateTime.Parse(this.textBoxOTDateFrom.Text.Trim()).ToString("yyyy/MM/dd") + "','yyyy/mm/dd') ";
                }
                if (this.textBoxOTDateTo.Text.Trim().Length > 0)
                {
                    condition += " AND a.OTDate <= to_date('" + DateTime.Parse(this.textBoxOTDateTo.Text.Trim()).ToString("yyyy/MM/dd") + "','yyyy/mm/dd') ";
                }
                this.ViewState.Add("condition", condition);

            }
            else
            {
                condition = Convert.ToString(this.ViewState["condition"]);
            }

            #region
            //base.SetForwardPage(forwarderType, ((WebNumericEdit)this.PageNavigator.FindControl("WebNumericEditCurrentpage")).Value.ToString());
            //this.dataSet = ((ServiceLocator)this.Session["serviceLocator"]).GetOTMAdvanceApplyData().GetDataByCondition(condition, Convert.ToInt32(this.Session["PageSize"]), ref this.forwarderPage, ref this.totalPage, ref this.totalRecodrs);
            //this.SetPageInfor(base.forwarderPage, base.totalPage, base.totalRecodrs);
            //this.DataUIBind();
            //base.WriteMessage(0, base.GetResouseValue("common.message.trans.complete"));
            #endregion

            DataTable dt = new DataTable();
            int totalCount = 0;
            dt = ocmb.GetWFProjectApplyList(condition, pager.CurrentPageIndex, pager.PageSize, out  totalCount);
            this.UltraWebGrid.DataSource = dt;
            this.UltraWebGrid.DataBind();
        }
        #endregion

        #region 分頁
        protected void pager_PageChanged(object sender, EventArgs e)
        {
            this.Query(true, "Goto");
        }
        #endregion

        protected bool CheckDateMonths(string StartDate, string EndDate)
        {
            try
            {
                string sql = "select floor(MONTHS_BETWEEN(to_date('" + Convert.ToDateTime(EndDate).ToString("yyyy/MM/dd") + "','yyyy/MM/dd'),to_date('" + Convert.ToDateTime(StartDate).ToString("yyyy/MM/dd") + "','yyyy/MM/dd'))) sDays from dual";
                if (Convert.ToInt32(ocmb.GetValue(sql)) >= 3)
                {
                    this.WriteMessage(1, Message.common_message_querydatecheck);
                    return false;
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #region 查詢
        protected void ButtonQuery_Click(object sender, EventArgs e)
        {
            try
            {
                if ((this.textBoxOTDateFrom.Text.Trim() == "") || (this.textBoxOTDateTo.Text.Trim() == ""))
                {
                    this.WriteMessage(1, "加班起止日期不能為空");
                }
                else
                {
                    this.Query(true, "Goto");
                    this.ProcessFlag.Value = "";
                    this.HiddenWorkNo.Value = this.textBoxEmployeeNo.Text.Trim().ToUpper();
                }
            }
            catch (Exception ex)
            {
                //this.WriteMessage(2, (ex.InnerException == null) ? ex.Message : ex.InnerException.Message);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('" + ex.Message + "');</script>");
            }
        }
        #endregion

        #region 重置
        protected void ButtonReset_Click(object sender, EventArgs e)
        {
            this.textBoxDepCode.Text = "";
            this.textBoxDepName.Text = "";
            this.textBoxBillNo.Text = "";
            this.textBoxEmployeeNo.Text = "";
            this.textBoxName.Text = "";
            this.ddlPersonType.SelectedValue = "";
            this.ddlOTType.SelectedValue = "";
            this.ddlOTStatus.SelectedValue = "";
            this.textBoxOTDateFrom.Text = DateTime.Now.AddDays(-1.0).ToShortDateString();
            this.textBoxOTDateTo.Text = DateTime.Now.ToShortDateString();
        }
        #endregion

        #region 刪除
        protected void ButtonDelete_Click(object sender, EventArgs e)
        {
            try
            {
                int i;
                CellItem GridItem;
                CheckBox chkIsHaveRight;
                int intDeleteOk = 0;
                int intDeleteError = 0;
                TemplatedColumn tcol = (TemplatedColumn)this.UltraWebGrid.Bands[0].Columns[0];
                for (i = 0; i < UltraWebGrid.Rows.Count; i++)
                {
                    GridItem = (CellItem)tcol.CellItems[i];
                    chkIsHaveRight = (CheckBox)GridItem.FindControl("CheckBoxCell");
                    if (chkIsHaveRight.Checked && ((UltraWebGrid.Rows[i].Cells.FromKey("Status").Text.Trim() != "0") && (UltraWebGrid.Rows[i].Cells.FromKey("Status").Text.Trim() != "3")))
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('" + Message.NotDeleteFlow + "');</script>");
                        return;
                    }
                }
                for (i = 0; i < UltraWebGrid.Rows.Count; i++)
                {
                    GridItem = (CellItem)tcol.CellItems[i];
                    chkIsHaveRight = (CheckBox)GridItem.FindControl("CheckBoxCell");
                    if (chkIsHaveRight.Checked)
                    {
                        //this.tempDataTable = ((ServiceLocator) this.Session["serviceLocator"]).GetOTMAdvanceApplyData().GetDataByCondition("and (a.status='0' or a.status='3') and a.ID='" + this.UltraWebGrid.DisplayLayout.Rows[i].Cells.FromKey("id").Value + "' and INSTR(a.ImportFlag,'N')>0").Tables["OTM_AdvanceApply"];
                        this.tempDataTable = ocmb.GetDataByCondition_1("and (a.status='0' or a.status='3') and a.ID='" + this.UltraWebGrid.DisplayLayout.Rows[i].Cells.FromKey("id").Value + "' and INSTR(a.ImportFlag,'N')>0").Tables[0];
                        if (this.tempDataTable.Rows.Count > 0)
                        {
                            ocmb.DeleteData(this.tempDataTable,logmodel);
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
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('" + Message.SuccCount + "" + intDeleteOk + "," + Message.FaileCount + "" + intDeleteError + "')</script>");
                    this.Query(false, "Goto");
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('" + Message.DeleteNotNull + "')</script>");
                    return;
                }
                this.ProcessFlag.Value = "";
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('" + ex.Message + "')</script>");
            }
        }
        #endregion

        #region 導入
        protected void ButtonImport_Click(object sender, EventArgs e)
        {
            this.UltraWebGridImport.Rows.Clear();
            if (!this.PanelImport.Visible)
            {
                this.PanelImport.Visible = true;
                this.PanelData.Visible = false;
                this.ButtonQuery.Enabled = false;
                this.ButtonReset.Enabled = false;
                this.btnAdd.Enabled = false;
                this.btnModify.Enabled = false;
                this.btnDelete.Enabled = false;
                this.btnAudit.Enabled = false;
                this.btnCancelAudit.Enabled = false;
                this.btnSendAudit.Enabled = false;
                //this.ButtonViewSchedule.Enabled = false;
                //this.ButtonOrgAudit.Enabled = false;
                //this.ButtonImport.Text = base.GetResouseValue("common.button.return");
                this.btnImport.Text = "返回";
                this.labeluploadMsg.Text = "";
            }
            else
            {
                this.PanelImport.Visible = false;
                this.PanelData.Visible = true;
                this.ButtonQuery.Enabled = true;
                this.ButtonReset.Enabled = true;
                this.btnAdd.Enabled = true;
                this.btnModify.Enabled = true;
                this.btnDelete.Enabled = true;
                this.btnAudit.Enabled = true;
                this.btnCancelAudit.Enabled = true;
                this.btnSendAudit.Enabled = true;
                //this.ButtonViewSchedule.Enabled = true;
                //this.ButtonOrgAudit.Enabled = true;
                //this.ButtonImport.Text = base.GetResouseValue("common.button.import");
                this.btnImport.Text = "導入";
            }
        }
           #endregion

        private void DataUIBind()
        {
            //Set datasource
            UltraWebGrid.DataSource = this.dataSet.Tables["OTM_AdvanceApply"].DefaultView;
            UltraWebGrid.DataBind();
        }

        #region 導出
        protected void ButtonExport_Click(object sender, EventArgs e)
        {
            
            Exception ex;
            if (!this.PanelImport.Visible)
            {
                if (this.ViewState["condition"] == null)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('" + Message.common_message_nodataexport + "');</script>");
                }
                else
                {
                    try
                    {
                        this.dataSet = ocmb.GetApplyDataByCondition(this.ViewState["condition"].ToString());
                        this.UltraWebGrid.Bands[0].Columns[0].Hidden = true;
                        //this.DataBind();
                        this.DataUIBind();
                        this.UltraWebGridExcelExporter1.DownloadName = "OverTimeAdvanceApply.xls";
                        this.UltraWebGridExcelExporter1.Export(this.UltraWebGrid);
                    }
                    catch (Exception exception1)
                    {
                        ex = exception1;
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('" + ex.Message + "');</script>");
                    }
                }
            }
            else
            {
                try
                {
                    this.UltraWebGridExcelExporter1.DownloadName = "OverTimeAdvanceApply.xls";
                    this.UltraWebGridExcelExporter1.Export(this.UltraWebGridImport);
                }
                catch (Exception exception2)
                {
                    ex = exception2;
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('" + ex.Message + "');</script>");
                }
            }
        }
        #endregion

        #region ButtonImportSave_Click
        protected void ButtonImportSave_Click(object sender, EventArgs e)
        {
            DataView dv;
            this.tempDataTable = ocmb.GetApplyDataByCondition("and 1=2").Tables["OTM_AdvanceApply"];
            try
            {
                this.ProcessFlag.Value = "Add";
                string strFileName = FileUpload.FileName;
                string strFileSize = FileUpload.PostedFile.ContentLength.ToString();
                string strFileExt = strFileName.Substring(strFileName.LastIndexOf(".") + 1);
                string strTime = string.Format("{0:yyyyMMddHHmmssffff}", System.DateTime.Now);
                string strFilePath = Server.MapPath("..\\") + "ExportFileTemp" + "\\" + strTime + ".xls";
                //string strFilePath = Server.MapPath("..\\") + "ExportFileTemp" + "\\" + strTime + ".xls";
                if (strFileExt == "xls")
                {
                    FileUpload.SaveAs(strFilePath);
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('" + Message.common_message_uploadxls + "');</script>");
                    return;
                }
                dataSet = new System.Data.DataSet();
                dataSet.Clear();
                dataSet.Tables.Add("KQM_Import");
                dataSet.Tables["KQM_Import"].Columns.Add(new System.Data.DataColumn("ErrorMsg", typeof(string)));
                dataSet.Tables["KQM_Import"].Columns.Add(new System.Data.DataColumn("WorkNo", typeof(string)));
                dataSet.Tables["KQM_Import"].Columns.Add(new System.Data.DataColumn("LocalName", typeof(string)));
                dataSet.Tables["KQM_Import"].Columns.Add(new System.Data.DataColumn("OTDate", typeof(string)));
                dataSet.Tables["KQM_Import"].Columns.Add(new System.Data.DataColumn("BeginTime", typeof(string)));
                dataSet.Tables["KQM_Import"].Columns.Add(new System.Data.DataColumn("EndTime", typeof(string)));
                dataSet.Tables["KQM_Import"].Columns.Add(new System.Data.DataColumn("WorkDesc", typeof(string)));
                string WorkNo = "", LocalName = "", OTDate = "", BeginTime = "", EndTime = "", Hours = "", WorkDesc = "", errorMsg = "";
                int index = 0, errorCount = 0;
                string ShiftNo = "", OTMSGFlag = "", tmpRemark = "", OTType = "";
                string strtemp = "", condition = "", nBeginTime = "", nEndTime = "", OverTimeType = "", update_user="";

                string OTMAdvanceBeforeDays = ocmb.GetValue("select nvl(max(paravalue),'2') from GDS_SC_PARAMETER where paraname='OTMAdvanceBeforeDays'");
                string appUserIsIn = ocmb.GetValue("select nvl(max(workno),'Y') from GDS_ATT_EMPLOYEES where workno='" + CurrentUserInfo.Personcode + "'");
                if (!appUserIsIn.Equals("Y"))//允許申報當前日期以前多少天的加班除去非工作日
                {
                    int i = 0;
                    int WorkDays = 0;
                    string UserOTType = "";
                    while (i < Convert.ToDouble(OTMAdvanceBeforeDays))
                    {
                        condition = "SELECT workflag FROM GDS_ATT_BGCALENDAR WHERE workday = to_date('" + System.DateTime.Now.AddDays(-1 - i - WorkDays).ToString("yyyy/MM/dd") + "','yyyy/MM/dd') AND bgcode IN (SELECT depcode FROM GDS_SC_DEPARTMENT WHERE levelcode = '0' and companyid='" + CurrentUserInfo.CompanyId + "')";
                        UserOTType = ocmb.GetValue(condition);
                        if (UserOTType.Length == 0)
                        {
                            break;
                        }
                        if (UserOTType.Equals("Y"))
                        {
                            i += 1;
                        }
                        else
                        {
                            WorkDays += 1;
                        }
                    }
                    OTMAdvanceBeforeDays = Convert.ToString(i + WorkDays);
                }

                int WorkNoCount = 0;
                SortedList list = new SortedList();
                dv = ocmb.ExceltoDataView(strFilePath);
                int inttotal = dv.Table.Rows.Count;
                for (int i = 0; i < inttotal; i++)
                {
                    tmpRemark = "";
                    OTMSGFlag = "";
                    WorkNo = MoveSpecailChar(dv.Table.Rows[i][0].ToString());
                    LocalName = MoveSpecailChar(dv.Table.Rows[i][1].ToString());
                    OTDate = MoveSpecailChar(dv.Table.Rows[i][2].ToString());
                    BeginTime = MoveSpecailChar(dv.Table.Rows[i][3].ToString());
                    EndTime = MoveSpecailChar(dv.Table.Rows[i][4].ToString());
                    WorkDesc = MoveSpecailChar(dv.Table.Rows[i][5].ToString());

                    string strsql=this.sqlDep;
                    if (strsql != "") //sqlDep = " 'D00200004'";
                    {
                        strsql = "and Dcode IN(" + sqlDep + ")";
                    }
                    WorkNoCount = ocmb.GetVWorkNoCount(WorkNo, strsql);
                    if (WorkNoCount == 0)
                    {
                        if (errorMsg.Length > 0)
                        {
                            errorMsg += ",";
                        }
                        errorMsg += Message.bfw_hrm_errorworkno;
                    }
                    else
                    {
                        try//判斷日期
                        {
                            if (OTDate.Length > 0)
                            {
                                OTDate = DateTime.Parse(OTDate).ToString("yyyy/MM/dd");
                                condition = "select trunc(sysdate)-to_date('" + OTDate + "','yyyy/mm/dd') from dual";
                                strtemp = ocmb.GetValue(condition);
                                if (Convert.ToDecimal(strtemp) > (Convert.ToDecimal(OTMAdvanceBeforeDays)))
                                {
                                    if (errorMsg.Length > 0)
                                    {
                                        errorMsg += ",";
                                    }
                                    errorMsg += Message.otm_message_checkadvancedaysbefore + ":" + OTMAdvanceBeforeDays;
                                }
                            }
                            else
                            {
                                if (errorMsg.Length > 0)
                                {
                                    errorMsg += ",";
                                }
                                errorMsg += Message.common_message_data_errordate;
                            }
                        }
                        catch (System.Exception)
                        {
                            OTDate = dv.Table.Rows[i][2].ToString().Trim();
                            if (errorMsg.Length > 0)
                            {
                                errorMsg += ",";
                            }
                            errorMsg += Message.common_message_data_errordate;
                        }
                        try
                        {
                            if (BeginTime.Length > 0)
                            {
                                BeginTime = DateTime.Parse(BeginTime).ToString("HH:mm");
                            }
                            else
                            {
                                if (errorMsg.Length > 0)
                                {
                                    errorMsg += ",";
                                }
                                errorMsg += Message.ErrStartTimeNotRight;
                            }
                        }
                        catch (System.Exception)
                        {
                            BeginTime = dv.Table.Rows[i][3].ToString().Trim();
                            if (errorMsg.Length > 0)
                            {
                                errorMsg += ",";
                            }
                            errorMsg += Message.ErrStartTimeNotRight;
                        }
                        try
                        {
                            if (EndTime.Length > 0)
                            {
                                EndTime = DateTime.Parse(EndTime).ToString("HH:mm");
                            }
                            else
                            {
                                if (errorMsg.Length > 0)
                                {
                                    errorMsg += ",";
                                }
                                errorMsg += Message.ErrEndTimeNotRight;
                            }
                        }
                        catch (System.Exception)
                        {
                            EndTime = dv.Table.Rows[i][4].ToString().Trim();
                            if (errorMsg.Length > 0)
                            {
                                errorMsg += ",";
                            }
                            errorMsg += Message.ErrEndTimeNotRight;
                        }
                        if (errorMsg.Length == 0)
                        {
                            //加班類別
                            //OTType = ((eBFW.Sys.ServiceLocator)Session["serviceLocator"]).GetFunctionData().GetOTType(WorkNo, OTDate);
                            OTType = ocmb.GetOTType(WorkNo, OTDate);
                            //抓取班別
                            //ShiftNo = ((eBFW.Sys.ServiceLocator)Session["serviceLocator"]).GetFunctionData().GetShiftNo(WorkNo, OTDate);
                            ShiftNo = ocmb.GetShiftNo(WorkNo, OTDate);
                            if (ShiftNo == null || ShiftNo == "")
                            {
                                if (errorMsg.Length > 0)
                                {
                                    errorMsg += ",";
                                }
                                errorMsg += Message.otm_exception_errorshiftno_1;
                            }
                            try
                            {
                                //獲取加班異常信息
                                Hours = this.GetOTHours(WorkNo, OTDate, BeginTime, EndTime, OTType);
                                if (Convert.ToDouble(Hours) < 0.5)
                                {
                                    if (errorMsg.Length > 0)
                                    {
                                        errorMsg += ",";
                                    }
                                    errorMsg += Message.otm_othourerror;
                                }
                                //OTMSGFlag = CommonFun.GetOTMSGFlag(WorkNo, OTDate, Convert.ToDouble(Hours), OTType, "Y", "");
                                OTMSGFlag = ocmb.GetOTMSGFlag(WorkNo, OTDate, Convert.ToDouble(Hours), OTType, "Y", "");
                                if (OTMSGFlag != "")
                                {
                                    tmpRemark = OTMSGFlag.Substring(1, OTMSGFlag.Length - 1);
                                    OTMSGFlag = OTMSGFlag.Substring(0, 1);
                                    //OverTimeType = ((eBFW.Sys.ServiceLocator)Session["serviceLocator"]).GetFunctionData().GetValue("SELECT OverTimeType from V_Employee a WHERE WORKNO='" + WorkNo + "'");
                                    OverTimeType = ocmb.GetValue("SELECT OverTimeType from GDS_ATT_EMPLOYEES a WHERE WORKNO='" + WorkNo + "'");
                                    if (OTMSGFlag.Equals("A"))
                                    {
                                        if (errorMsg.Length > 0)
                                        {
                                            errorMsg += ",";
                                        }
                                        errorMsg += tmpRemark;
                                    }
                                }
                                else
                                {
                                    tmpRemark = "";
                                    OTMSGFlag = "";
                                }
                            }
                            catch (System.Exception)
                            {
                                if (errorMsg.Length > 0)
                                {
                                    errorMsg += ",";
                                }
                                errorMsg += Message.otm_errorhours;
                            }
                        }
                    }
                    try
                    {
                        nBeginTime = string.Format(OTDate + " " + BeginTime, "yyyy/MM/dd HH:mm");
                        nEndTime = string.Format(OTDate + " " + EndTime, "yyyy/MM/dd HH:mm");
                        //list = CommonFun.ReturnOTTTime(WorkNo, OTDate, Convert.ToDateTime(nBeginTime), Convert.ToDateTime(nEndTime), ShiftNo);
                        list = ocmb.ReturnOTTTime(WorkNo, OTDate, Convert.ToDateTime(nBeginTime), Convert.ToDateTime(nEndTime), ShiftNo);
                        nBeginTime = Convert.ToDateTime(list.GetByIndex(list.IndexOfKey("A"))).ToString("yyyy/MM/dd HH:mm");
                        nEndTime = Convert.ToDateTime(list.GetByIndex(list.IndexOfKey("B"))).ToString("yyyy/MM/dd HH:mm");

                        //請假中不能申報加班
                        if (!this.CheckLeaveOverTime(WorkNo, Convert.ToDateTime(nBeginTime).ToString("yyyy/MM/dd"), Convert.ToDateTime(nBeginTime).ToString("HH:mm"), Convert.ToDateTime(nEndTime).ToString("yyyy/MM/dd"), Convert.ToDateTime(nEndTime).ToString("HH:mm")))
                        {
                            if (errorMsg.Length > 0)
                            {
                                errorMsg += ",";
                            }
                            errorMsg += Message.common_message_Leaveovertime_repeart;
                        }
                    }
                    catch (System.Exception)
                    {
                    }
                    if (errorMsg.Length == 0)
                    {
                        //工作時間內不能預報加班
                        if (!this.CheckWorkTime(WorkNo, OTDate, nBeginTime, nEndTime, ShiftNo))
                        {
                            if (errorMsg.Length > 0)
                            {
                                errorMsg += ",";
                            }
                            errorMsg += Message.common_message_otm_worktime_repeart;
                        }

                        //G3加班后一天如果是正常上班不允許跨天申報Modify by Jackzhang2011/4/2
                        if (!this.CheckG3WorkTime(WorkNo, OTDate, nBeginTime, nEndTime, OTType))
                        {
                            if (errorMsg.Length > 0)
                            {
                                errorMsg += ",";
                            }
                            errorMsg += Message.common_message_otm_worktime_checkg3;//加班時間段內含有正常工作時間，零點以后不允許申報加班
                        }

                        //一天内多笔加班时间不能交叉或重复
                        if (!this.CheckOverTime(WorkNo, OTDate, nBeginTime, nEndTime, ShiftNo, true))
                        {
                            if (errorMsg.Length > 0)
                            {
                                errorMsg += ",";
                            }
                            errorMsg += Message.common_message_otm_multi_repeart;
                        }
                        //判斷相同時段是否有參加教育訓練
                        if (!this.CheckOTOverETM(WorkNo, nBeginTime, nEndTime))
                        {
                            if (errorMsg.Length > 0)
                            {
                                errorMsg += ",";
                            }
                            errorMsg += Message.common_message_otm_etmrepeart;
                        }

                        #region 每天16:30后不讓預報加班,非工作日也不能預報加班  龍華總部周邊 update by xukai 20111014
                        //if (!string.IsNullOrEmpty(LHZBLimitTime))
                        //{
                        //    //每天LHZBLimitTime時間后不能新增、修改加班預報
                        //    DateTime dtLimit = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd") + " " + LHZBLimitTime);
                        //    if (dtLimit < DateTime.Now)
                        //    {
                        //        if (errorMsg.Length > 0)
                        //        {
                        //            errorMsg += ",";
                        //        }
                        //        errorMsg += this.GetResouseValue("kqm.otm.advanceapply") + LHZBLimitTime + this.GetResouseValue("kqm.otm.advanceapply.error");
                        //    }
                        //}

                        //if (LHZBIsLimitApply == "Y")
                        //{
                        //    //非工作日不能預報加班
                        //    workflag = ((eBFW.Sys.ServiceLocator)Session["serviceLocator"]).GetFunctionData().GetEmpWorkFlag(WorkNo, DateTime.Now.ToString("yyyy-MM-dd"));
                        //    if (workflag == "N")
                        //    {
                        //        //this.WriteMessage(1, this.GetResouseValue("kqm.otm.advanceapply.workflag"));
                        //        //return;
                        //        if (errorMsg.Length > 0)
                        //        {
                        //            errorMsg += ",";
                        //        }
                        //        errorMsg += this.GetResouseValue("kqm.otm.advanceapply.workflag");
                        //    }
                        //}
                        #endregion
                    }
                    if (WorkDesc.Length == 0)
                    {
                        if (errorMsg.Length > 0)
                        {
                            errorMsg += ",";
                        }
                        errorMsg += Message.overtime_desc + Message.Required;
                    }
                    if (errorMsg.Length == 0)
                    {
                        index += 1;
                        System.Data.DataRow row = this.tempDataTable.NewRow();
                        row.BeginEdit();
                        row["WORKNO"] = WorkNo;
                        row["OTDate"] = OTDate;
                        row["BeginTime"] = nBeginTime;
                        row["EndTime"] = nEndTime;
                        row["Hours"] = Hours;
                        row["OTType"] = OTType;
                        row["WorkDesc"] = WorkDesc;
                        row["Remark"] = tmpRemark;
                        row["OTMSGFlag"] = OTMSGFlag;
                        row["ApplyDate"] = DateTime.Now.ToString(this.dateFormat);
                        row["update_user"] = update_user;
                        row["UPDATE_DATE"] = DateTime.Now.ToLongDateString();
                        row["IsProject"] = "Y";
                        row["OTShiftNo"] = ShiftNo;

                        row.EndEdit();
                        this.tempDataTable.Rows.Add(row);
                        this.tempDataTable.AcceptChanges();
                        //((eBFW.Sys.ServiceLocator)Session["serviceLocator"]).GetOTMAdvanceApplyData().SaveData(this.ProcessFlag.Value, this.tempDataTable);
                        ocmb.SaveData(this.ProcessFlag.Value, this.tempDataTable,logmodel);
                        this.tempDataTable.Clear();
                        if (tmpRemark.EndsWith("1"))
                        {
                            this.dataSet.Tables["KQM_Import"].Rows.Add(new string[] { Message.InportSessce, WorkNo, LocalName, OTDate, BeginTime, EndTime, WorkDesc });
                        }
                    }
                    else
                    {
                        errorCount += 1;
                        this.dataSet.Tables["KQM_Import"].Rows.Add(new string[] { errorMsg, WorkNo, LocalName, OTDate, BeginTime, EndTime, WorkDesc });
                    }
                    errorMsg = "";
                }
                labeluploadMsg.Text = Message.NumberOfSuccessed + index + "  ;" + Message.NumberOfFailed + errorCount + " .";
                this.UltraWebGridImport.DataSource = this.dataSet.Tables["KQM_Import"].DefaultView;
                this.UltraWebGridImport.DataBind();
            }
            catch (System.Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('" + ex.Message + "')</script>");
            }
        }
        #endregion

        public static string MoveSpecailChar(string str)
        {
            str = str.Trim().Replace("'", "");
            return str;
        }

        #region 核准
        protected void ButtonAudit_Click(object sender, EventArgs e)
        {
            try
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
                        if ((this.UltraWebGrid.Rows[i].Cells.FromKey("Status").Text != "0"))
                        {
                            //base.WriteMessage(1, base.GetResouseValue("common.message.audit.unsendaudit"));
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('" + Message.common_message_audit_unsendaudit + "');</script>");
                            return;
                        }

                        this.tempDataTable = ocmb.GetApplyDataByCondition("and a.ID='" + this.UltraWebGrid.DisplayLayout.Rows[i].Cells.FromKey("id").Value + "'").Tables["OTM_AdvanceApply"];
                        if (this.tempDataTable.Rows.Count > 0)
                        {
                            ocmb.Audit(this.tempDataTable, CurrentUserInfo.Personcode,logmodel);
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
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('" + Message.SuccCount + "" + intDeleteOk + "," + Message.FaileCount + "" + intDeleteError + "')</script>");
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('" + Message.common_message_data_select + "');</script>");
                    return;
                }
                this.ProcessFlag.Value = "";
                this.Query(false, "Goto");
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('" + ex.Message + "');</script>");
            }
        }
        #endregion

        #region 取消核准
        protected void ButtonCancelAudit_Click(object sender, EventArgs e)
        {
            try
            {
                int i;
                CellItem GridItem;
                CheckBox chkIsHaveRight;
                string sysKqoQinDays = ocmb.GetValue("select nvl(MAX(paravalue),'5') from GDS_SC_PARAMETER where paraname='KQMReGetKaoQin'");
                string strModifyDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy/MM") + "/" + sysKqoQinDays).ToString("yyyy/MM/dd");
                int intDeleteOk = 0;
                int intDeleteError = 0;
                string sFromKQDate = "";
                TemplatedColumn tcol = (TemplatedColumn)this.UltraWebGrid.Bands[0].Columns[0];
                for (i = 0; i < this.UltraWebGrid.Rows.Count; i++)
                {
                    GridItem = (CellItem)tcol.CellItems[i];
                    chkIsHaveRight = (CheckBox)GridItem.FindControl("CheckBoxCell");
                    if (chkIsHaveRight.Checked)
                    {
                        sFromKQDate = Convert.ToDateTime(this.UltraWebGrid.Rows[i].Cells.FromKey("OTDate").Text).ToString("yyyy/MM/dd");
                        if (this.UltraWebGrid.Rows[i].Cells.FromKey("Status").Text.Trim() != "2")
                        {
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('" + Message.UnCancelAudit + "');</script>");
                            return;
                        }
                        if ((sFromKQDate.CompareTo(DateTime.Now.ToString("yyyy/MM") + "/01") == -1) && (strModifyDate.CompareTo(DateTime.Now.ToString("yyyy/MM/dd")) <= 0))
                        {
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('" + Message.kaoqindata_checkreget + "');</script>");
                            return;
                        }
                        if (sFromKQDate.CompareTo(DateTime.Now.AddMonths(-1).ToString("yyyy/MM") + "/01") == -1)
                        {
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('" + Message.kaoqindata_checkreget + "');</script>");
                            return;
                        }
                    }
                }
                for (i = 0; i < this.UltraWebGrid.Rows.Count; i++)
                {
                    GridItem = (CellItem)tcol.CellItems[i];
                    chkIsHaveRight = (CheckBox)GridItem.FindControl("CheckBoxCell");
                    if (chkIsHaveRight.Checked)
                    {
                        this.tempDataTable = ocmb.GetApplyDataByCondition("and a.ID='" + this.UltraWebGrid.DisplayLayout.Rows[i].Cells.FromKey("id").Value + "' and INSTR(a.ImportFlag,'N')>0").Tables["OTM_AdvanceApply"];
                        if (this.tempDataTable.Rows.Count > 0)
                        {
                            ocmb.CancelAudit(this.tempDataTable,logmodel);
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
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('" + Message.SuccCount + "" + intDeleteOk + "," + Message.FaileCount + "" + intDeleteError + "')</script>");
                    this.Query(false, "Goto");
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('" + Message.common_message_data_select + "');</script>");
                    return;
                }
                this.ProcessFlag.Value = "";
            }
            catch (Exception ex)
            {
                //base.WriteMessage(1, (ex.InnerException == null) ? ex.Message : ex.InnerException.Message);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('" + ex.Message + "');</script>");
            }
        }
        #endregion

        #region 送簽
        protected void btnSendAduit_Click(object sender, EventArgs e)
        {
            OrgSendAudit();
        }
        private void SendAudit()
        {
            string LHZBIsDisplayG2G3 = "";
            try
            {
                LHZBIsDisplayG2G3 = ConfigurationManager.AppSettings["LHZBIsDisplayG2G3"];
            }
            catch
            {
                LHZBIsDisplayG2G3 = "N";
            }
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
                string BillTypeCode = "";
                string BillNoType = "OTMProjectApply";
                string OTType = "";
                SortedList BillNoOrgCode = new SortedList();
                TemplatedColumn tcol = (TemplatedColumn)this.UltraWebGrid.Bands[0].Columns[0];
                for (i = 0; i < this.UltraWebGrid.Rows.Count; i++)
                {
                    GridItem = (CellItem)tcol.CellItems[i];
                    chkIsHaveRight = (CheckBox)GridItem.FindControl("CheckBoxCell");
                    if (chkIsHaveRight.Checked)
                    {
                        if ((this.UltraWebGrid.Rows[i].Cells.FromKey("Status").Text != "0"))
                        {
                            //base.WriteMessage(1, base.GetResouseValue("common.message.audit.unsendaudit"));
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('" + Message.common_message_audit_unsendaudit + "');</script>");
                            return;
                        }

                        //add by xukai 20111015
                        if (!string.IsNullOrEmpty(LHZBLimitTime))
                        {
                            //每天LHZBLimitTime時間后不能新增、修改加班預報
                            DateTime dtLimit = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd") + " " + LHZBLimitTime);
                            if (dtLimit < DateTime.Now)
                            {
                                //this.WriteMessage(1, this.GetResouseValue("kqm.otm.advanceapply") + LHZBLimitTime + this.GetResouseValue("kqm.otm.advanceapply.sendaudit"));
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('" + Message.kqm_otm_advanceapply + LHZBLimitTime + Message.kqm_otm_advanceapply_sendaudit + "');</script>");
                                return;
                            }
                        }
                        //end
                    }
                }
                BillTypeCode = "OTMProjectApply";
                for (i = 0; i < this.UltraWebGrid.Rows.Count; i++)
                {
                    GridItem = (CellItem)tcol.CellItems[i];
                    chkIsHaveRight = (CheckBox)GridItem.FindControl("CheckBoxCell");
                    if (chkIsHaveRight.Checked)
                    {
                        if (this.UltraWebGrid.Rows[i].Cells.FromKey("OTType").Value != null)
                        {
                            OTType = this.UltraWebGrid.Rows[i].Cells.FromKey("OTType").Text.Trim();
                            if (LHZBIsDisplayG2G3 == "Y")
                            {
                                OTType = OTType.Substring(0, 2);
                            }
                        }
                        else
                        {
                            OTType = "";
                        }
                        BillNo = "";
                        //OrgCode = string.IsNullOrEmpty(this.HiddenOrgCode.Value) ? this.UltraWebGrid.Rows[i].Cells.FromKey("dCode").Text.Trim() : this.HiddenOrgCode.Value.Trim();
                        OrgCode = String.IsNullOrEmpty(this.HiddenOrgCode.Value) ? this.UltraWebGrid.Rows[i].Cells.FromKey("dCode").Text.Trim() : this.HiddenOrgCode.Value.Trim();
                        AuditOrgCode = ocmb.GetWorkFlowOrgCode(OrgCode, BillTypeCode, OTType);
                        if (AuditOrgCode.Length > 0)
                        {
                            string senduser = this.UltraWebGrid.Rows[i].Cells.FromKey("WorkNo").Text.Trim();
                            this.dataSet = ocmb.GetDataSetBySQL("select workNo from GDS_ATT_ADVANCEAPPLY where Status in('0','3') AND id='" + this.UltraWebGrid.Rows[i].Cells.FromKey("ID").Text.Trim() + "'");
                            if (this.dataSet.Tables["TempTable"].Rows.Count > 0)
                            {
                                if (BillNoOrgCode.IndexOfKey(AuditOrgCode) >= 0)
                                {
                                    BillNo = BillNoOrgCode.GetByIndex(BillNoOrgCode.IndexOfKey(AuditOrgCode)).ToString();
                                    ocmb.SaveAuditData("Modify", this.UltraWebGrid.Rows[i].Cells.FromKey("ID").Text.Trim(), BillNo, AuditOrgCode, BillTypeCode, CurrentUserInfo.Personcode, OTType, senduser, logmodel);
                                    intSendOK++;
                                }
                                else
                                {
                                    BillNo = ocmb.SaveAuditData("Add", this.UltraWebGrid.Rows[i].Cells.FromKey("ID").Text.Trim(), BillNoType, AuditOrgCode, BillTypeCode, CurrentUserInfo.Personcode, OTType, senduser, logmodel);
                                    intSendBillNo++;
                                    intSendOK++;
                                    BillNoOrgCode.Add(AuditOrgCode, BillNo);
                                }
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
                        //base.WriteMessage(1, string.Concat(new object[] { base.GetResouseValue("common.message.successcount"), "：", intSendOK, ";", base.GetResouseValue("common.message.billcount"), "：", intSendBillNo, ";", base.GetResouseValue("common.message.errorcount"), "：", intSendError, "(", base.GetResouseValue("common.message.noworkflow"), ")" }));
                        this.WriteMessage(1, Message.SuccCount + intSendOK + ";" + Message.common_message_billcount + "：" + intSendBillNo + ";" + Message.FaileCount + intSendError + "(" + Message.common_message_noworkflow + ")");
                    }
                    else
                    {
                        //base.WriteMessage(1, string.Concat(new object[] { base.GetResouseValue("common.message.successcount"), "：", intSendOK, ";", base.GetResouseValue("common.message.billcount"), "：", intSendBillNo }));
                        this.WriteMessage(1, Message.SuccCount + intSendOK + ";" + Message.common_message_billcount + "：" + intSendBillNo);
                    }
                }
                else
                {
                    //base.WriteMessage(1, base.GetResouseValue("common.message.data.select"));
                    return;
                }
                this.Query(false, "Goto");
                this.ProcessFlag.Value = "";
                this.HiddenOrgCode.Value = "";
            }
            catch (Exception ex)
            {
                this.WriteMessage(2, (ex.InnerException == null) ? ex.Message : ex.InnerException.Message);
                // this.WriteMessage(1, Message.AtLastOneChoose);
            }
        }

        #endregion

        #region 組織送簽
        protected void ButtonOrgAudit_Click(object sender, EventArgs e)
        {
            OrgSendAudit();
        }

        private void OrgSendAudit()
        {
            string LHZBIsDisplayG2G3 = "";
            try
            {
                LHZBIsDisplayG2G3 = ConfigurationManager.AppSettings["LHZBIsDisplayG2G3"];
            }
            catch
            {
                LHZBIsDisplayG2G3 = "N";
            }
            try
            {
                int intSendOK = 0;
                int intSendBillNo = 0;
                int intSendError = 0;
                string OrgCode = "";
                //string BillNo = "";
                string AuditOrgCode = "";
                string BillTypeCode = "OTMProjectApply";
                string BillNoType = "OTMProjectApply";
                string OTType = "";
                SortedList BillNoOrgCode = new SortedList();
                TemplatedColumn tcol = (TemplatedColumn)this.UltraWebGrid.Bands[0].Columns[0];
                for (int i = 0; i < this.UltraWebGrid.Rows.Count; i++)
                {
                    CellItem GridItem = (CellItem)tcol.CellItems[i];
                    CheckBox chkIsHaveRight = (CheckBox)GridItem.FindControl("CheckBoxCell");
                    if (chkIsHaveRight.Checked)
                    {
                        if ((this.UltraWebGrid.Rows[i].Cells.FromKey("Status").Text != "0"))
                        {
                            //base.WriteMessage(1, base.GetResouseValue("common.message.audit.unsendaudit"));
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('" + Message.common_message_audit_unsendaudit + "');</script>");
                            return;
                        }

                        //add by xukai 20111015
                        if (!string.IsNullOrEmpty(LHZBLimitTime))
                        {
                            //每天LHZBLimitTime時間后不能新增、修改加班預報
                            DateTime dtLimit = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd") + " " + LHZBLimitTime);
                            if (dtLimit < DateTime.Now)
                            {
                                //this.WriteMessage(1, this.GetResouseValue("kqm.otm.advanceapply") + LHZBLimitTime + this.GetResouseValue("kqm.otm.advanceapply.sendaudit"));
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('" + Message.kqm_otm_advanceapply + LHZBLimitTime + Message.kqm_otm_advanceapply_sendaudit + "');</script>");
                                return;
                            }
                        }
                        //end
                    }
                }
                Dictionary<string, List<string>> dicy = new Dictionary<string, List<string>>();

                for (int i = 0; i < UltraWebGrid.Rows.Count; i++)
                {
                    CellItem GridItem = (CellItem)tcol.CellItems[i];
                    CheckBox chkIsHaveRight = (CheckBox)(GridItem.FindControl("CheckBoxCell"));
                    if (chkIsHaveRight.Checked)
                    {
                        if (UltraWebGrid.Rows[i].Cells.FromKey("OTType").Value != null)
                        {
                            OTType = UltraWebGrid.Rows[i].Cells.FromKey("OTType").Text.Trim();
                            //update by xukai 20111012
                            if (LHZBIsDisplayG2G3 == "Y")
                            {
                                OTType = OTType.Substring(0, 2);
                            }
                            //end
                        }
                        else
                        {
                            OTType = "";
                        }

                        OrgCode = UltraWebGrid.Rows[i].Cells.FromKey("dCode").Text.Trim();
                        AuditOrgCode = ocmb.GetWorkFlowOrgCode(OrgCode, BillTypeCode, OTType);
                        string key = AuditOrgCode + "^" + OTType;
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
                        if (UltraWebGrid.Rows[i].Cells.FromKey("OTType").Value != null)
                        {
                            OTType = UltraWebGrid.Rows[i].Cells.FromKey("OTType").Text.Trim();
                            //update by xukai 20111012
                            if (LHZBIsDisplayG2G3 == "Y")
                            {
                                OTType = OTType.Substring(0, 2);
                            }
                            //end
                        }
                        else
                        {
                            OTType = "";
                        }
                        OrgCode = UltraWebGrid.Rows[i].Cells.FromKey("dCode").Text.Trim();
                        AuditOrgCode = ocmb.GetWorkFlowOrgCode(OrgCode, BillTypeCode, OTType);
                        string key = AuditOrgCode + "^" + OTType;
                        if (!string.IsNullOrEmpty(AuditOrgCode))
                        {
                            //AuditOrgCode = bll.GetWorkFlowOrgCode(OrgCode, BillTypeCode, OTType);
                            if (dicy[key] != null)
                            {
                                dicy[key].Add(UltraWebGrid.Rows[i].Cells.FromKey("ID").Text.Trim());
                            }
                        }
                    }
                }
                int count = ocmb.SaveOrgAuditData("Add", dicy, BillNoType, BillTypeCode, CurrentUserInfo.Personcode, logmodel);
                intSendBillNo = count;
                if (intSendBillNo > 0)
                {
                    if (intSendError > 0)
                    {
                        this.WriteMessage(1, Message.SuccCount + intSendBillNo + ";" + Message.common_message_billcount + "：" + intSendBillNo + ";" + Message.FaileCount + intSendError + "(" + Message.common_message_noworkflow + ")");
                    }
                    else
                    {
                        this.WriteMessage(1, Message.SuccCount + intSendBillNo + ";" + Message.common_message_billcount + "：" + intSendBillNo);
                    }
                }
                else
                {
                    if (intSendError > 0)
                    {
                        this.WriteMessage(1, Message.common_message_noworkflow);
                    }
                    else
                    {
                        this.WriteMessage(1, Message.AtLastOneChoose);

                    }
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

        protected void WriteMessage(int messageType, string message)
        {
            switch (messageType)
            {
                case 0:
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "jump", "<script language='JavaScript'>window.status='" + message.Replace("\"", "'").Replace("\r", "").Replace("\n", "") + "';</script>");
                    break;

                case 1:
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "jump", "<script language='JavaScript'>alert(\"" + message.Replace("\"", "'").Replace("\r", "").Replace("\n", "") + "\");</script>");
                    break;

                case 2:
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "jump", "<script language='JavaScript'>alert(\"" + message.Replace("\"", "'").Replace("\r", "").Replace("\n", "") + "\");</script>");
                    break;
            }
        }

        protected void UltraWebGrid_DataBound(object sender, EventArgs e)
        {
            string OTDate = "";
            double AdvanceTotalG1 = 0;
            double AdvanceTotalG2 = 0;
            double AdvanceTotalG3 = 0;
            double RealTotalG1 = 0;
            double RealTotalG2 = 0;
            double RealTotalG3 = 0;
            string LHZBIsDisplayG2G3 = "";
            try
            {
                LHZBIsDisplayG2G3 = System.Configuration.ConfigurationManager.AppSettings["LHZBIsDisplayG2G3"];
            }
            catch
            {
                LHZBIsDisplayG2G3 = "N";
            }
            for (int i = 0; i < UltraWebGrid.Rows.Count; i++)
            {
                //add by xukai 20111010 
                if (LHZBIsDisplayG2G3 == "Y")
                {
                    if (UltraWebGrid.Rows[i].Cells.FromKey("OTType").Text.Trim() == "G2")
                    {
                        UltraWebGrid.Rows[i].Cells.FromKey("OTType").Value = UltraWebGrid.Rows[i].Cells.FromKey("OTType").Value + "(" + this.GetResouseValue("otm.g2.remark") + ")";
                    }
                    else if (UltraWebGrid.Rows[i].Cells.FromKey("OTType").Text.Trim() == "G3")
                    {
                        UltraWebGrid.Rows[i].Cells.FromKey("OTType").Value = UltraWebGrid.Rows[i].Cells.FromKey("OTType").Value + "(" + this.GetResouseValue("otm.g3.remark") + ")";
                    }
                }
                //end
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
                if (UltraWebGrid.Rows[i].Cells.FromKey("OTMSGFlag").Value != null &&
                    UltraWebGrid.Rows[i].Cells.FromKey("WorkDesc").Value != null)
                {
                    switch (UltraWebGrid.Rows[i].Cells.FromKey("OTMSGFlag").Text)
                    {
                        case "1":
                            UltraWebGrid.Rows[i].Style.ForeColor = System.Drawing.Color.Green;
                            break;
                        case "2":
                            UltraWebGrid.Rows[i].Style.ForeColor = System.Drawing.Color.Blue;
                            break;
                        case "3":
                            UltraWebGrid.Rows[i].Style.ForeColor = System.Drawing.Color.SaddleBrown;
                            break;
                        case "4":
                            UltraWebGrid.Rows[i].Style.ForeColor = System.Drawing.Color.Maroon;
                            break;
                        case "5":
                            UltraWebGrid.Rows[i].Style.ForeColor = System.Drawing.Color.Tomato;
                            break;
                        case "6":
                            UltraWebGrid.Rows[i].Style.ForeColor = System.Drawing.Color.Red;
                            break;
                    }
                }
                OTDate = Convert.ToDateTime(UltraWebGrid.Rows[i].Cells.FromKey("OTDate").Text).ToString("yyyy/MM/dd");
                AdvanceTotalG1 = 0;
                AdvanceTotalG2 = 0;
                AdvanceTotalG3 = 0;
                RealTotalG1 = 0;
                RealTotalG2 = 0;
                RealTotalG3 = 0;
                string sql = "select nvl(sum(Decode(OTType,'G1',Hours,0)),0)G1Total,nvl(sum(Decode(OTType,'G2',Hours,0)),0)G2Total,nvl(sum(Decode(OTType,'G3',Hours,0)),0)G3Total from GDS_ATT_ADVANCEAPPLY where WorkNo='" + UltraWebGrid.Rows[i].Cells.FromKey("WorkNo").Text + "' and otdate>last_day(add_months(to_date('" + OTDate + "','yyyy/MM/dd'),-1)) and OTDate<=to_date('" + OTDate + "','yyyy/MM/dd') and status<'3' and importflag='N'";
               // this.tempDataTable = ((eBFW.Sys.ServiceLocator)Session["serviceLocator"]).GetFunctionData().GetDataSetBySQL("select nvl(sum(Decode(OTType,'G1',Hours,0)),0)G1Total,nvl(sum(Decode(OTType,'G2',Hours,0)),0)G2Total,nvl(sum(Decode(OTType,'G3',Hours,0)),0)G3Total from otm_advanceApply where WorkNo='" + UltraWebGrid.Rows[i].Cells.FromKey("WorkNo").Text + "' and otdate>last_day(add_months(to_date('" + OTDate + "','yyyy/MM/dd'),-1)) and OTDate<=to_date('" + OTDate + "','yyyy/MM/dd') and status<'3' and importflag='N'").Tables["TempTable"];
                this.tempDataTable = ocmb.GetDataSetBySQL(sql).Tables["TempTable"];
                if (this.tempDataTable.Rows.Count > 0)
                {
                    AdvanceTotalG1 = Convert.ToDouble(this.tempDataTable.Rows[0]["G1Total"]);
                    AdvanceTotalG2 = Convert.ToDouble(this.tempDataTable.Rows[0]["G2Total"]);
                    AdvanceTotalG3 = Convert.ToDouble(this.tempDataTable.Rows[0]["G3Total"]);
                }
                string strsql = "select nvl(sum(Decode(OTType,'G1',ConfirmHours,0)),0)G1Total,nvl(sum(Decode(OTType,'G2',ConfirmHours,0)),0)G2Total,nvl(sum(Decode(OTType,'G3',ConfirmHours,0)),0)G3Total from GDS_ATT_REALAPPLY where WorkNo='" + UltraWebGrid.Rows[i].Cells.FromKey("WorkNo").Text + "' and otdate>last_day(add_months(to_date('" + OTDate + "','yyyy/MM/dd'),-1)) and OTDate<=to_date('" + OTDate + "','yyyy/MM/dd') and status='2'";
                //this.tempDataTable = ((eBFW.Sys.ServiceLocator)Session["serviceLocator"]).GetFunctionData().GetDataSetBySQL("select nvl(sum(Decode(OTType,'G1',ConfirmHours,0)),0)G1Total,nvl(sum(Decode(OTType,'G2',ConfirmHours,0)),0)G2Total,nvl(sum(Decode(OTType,'G3',ConfirmHours,0)),0)G3Total from otm_realApply where WorkNo='" + UltraWebGrid.Rows[i].Cells.FromKey("WorkNo").Text + "' and otdate>last_day(add_months(to_date('" + OTDate + "','yyyy/MM/dd'),-1)) and OTDate<=to_date('" + OTDate + "','yyyy/MM/dd') and status='2'").Tables["TempTable"];
                this.tempDataTable = ocmb.GetDataSetBySQL(strsql).Tables["TempTable"];
                if (this.tempDataTable.Rows.Count > 0)
                {
                    RealTotalG1 = Convert.ToDouble(this.tempDataTable.Rows[0]["G1Total"]);
                    RealTotalG2 = Convert.ToDouble(this.tempDataTable.Rows[0]["G2Total"]);
                    RealTotalG3 = Convert.ToDouble(this.tempDataTable.Rows[0]["G3Total"]);
                }
                UltraWebGrid.Rows[i].Cells.FromKey("G1Total").Value = AdvanceTotalG1 + RealTotalG1;
                UltraWebGrid.Rows[i].Cells.FromKey("G2Total").Value = AdvanceTotalG2 + RealTotalG2;
                UltraWebGrid.Rows[i].Cells.FromKey("G3Total").Value = AdvanceTotalG3 + RealTotalG3;

            }
        }

        protected void UltraWebGridExcelExporter_CellExported(object sender, Infragistics.WebUI.UltraWebGrid.ExcelExport.CellExportedEventArgs e)
        {
            int iRdex = e.CurrentRowIndex;
            int iCdex = e.CurrentColumnIndex;

            if (!this.PanelImport.Visible)
            {
                if (iRdex != 0)
                {
                    if (e.GridColumn.Key.ToLower() == "otdate" && e.CurrentWorksheet.Rows[iRdex].Cells[iCdex].Value != null)
                    {
                        e.CurrentWorksheet.Rows[iRdex].Cells[iCdex].Value = string.Format("{0:" + dateFormat + "}", Convert.ToDateTime(e.CurrentWorksheet.Rows[iRdex].Cells[iCdex].Value));
                    }
                    e.CurrentWorksheet.Rows[iRdex].Height = 350;
                }
            }
            else
            {
                if (iRdex != 0)
                {
                    e.CurrentWorksheet.Rows[iRdex].Height = 350;
                }
            }
        }

        protected void UltraWebGridExcelExporter_HeaderCellExported(object sender, Infragistics.WebUI.UltraWebGrid.ExcelExport.HeaderCellExportedEventArgs e)
        {
            int iRdex = e.CurrentRowIndex;
            int iCdex = e.CurrentColumnIndex;
            if (!this.PanelImport.Visible)
            {
                if (iRdex == 0)
                {
                    e.CurrentWorksheet.Columns[iCdex].Width = 3000;
                    e.CurrentWorksheet.Rows[iRdex].Height = 500;
                }
            }
            else
            {
                if (iRdex != 0)
                {
                    e.CurrentWorksheet.Rows[iRdex].Height = 500;
                }
            }
        }

        public bool CheckLeaveOverTime(string WorkNo, string StartDate, string StartTime, string EndDate, string EndTime)
        {
            string startTime = Convert.ToDateTime(StartDate + " " + StartTime).ToString("yyyy/MM/dd HH:mm");
            string endTime = Convert.ToDateTime(EndDate + " " + EndTime).ToString("yyyy/MM/dd HH:mm");
            string condition = "";
            DataTable dt = new DataTable();
            condition = "SELECT WorkNo from GDS_ATT_LEAVEAPPLY  a where a.WorkNo='" + WorkNo + "'  and a.EndDate>=to_date('" + StartDate + "','yyyy/mm/dd')  AND ((to_date(to_char(a.StartDate,'yyyy/mm/dd')||a.StartTime,'yyyy/mm/dd hh24:mi') <= to_date('" + startTime + "','yyyy/mm/dd hh24:mi') AND to_date(to_char(a.EndDate,'yyyy/mm/dd')||a.EndTime,'yyyy/mm/dd hh24:mi') > to_date('" + startTime + "','yyyy/mm/dd hh24:mi')) or (to_date(to_char(a.StartDate,'yyyy/mm/dd')||a.StartTime,'yyyy/mm/dd hh24:mi') < to_date('" + endTime + "','yyyy/mm/dd hh24:mi') AND to_date(to_char(a.EndDate,'yyyy/mm/dd')||a.EndTime,'yyyy/mm/dd hh24:mi') >= to_date('" + endTime + "','yyyy/mm/dd hh24:mi')) or (to_date(to_char(a.StartDate,'yyyy/mm/dd')||a.StartTime,'yyyy/mm/dd hh24:mi') >= to_date('" + startTime + "','yyyy/mm/dd hh24:mi') AND to_date(to_char(a.EndDate,'yyyy/mm/dd')||a.EndTime,'yyyy/mm/dd hh24:mi') <= to_date('" + endTime + "','yyyy/mm/dd hh24:mi')))";
           //this.tempDataTable = ((ServiceLocator)this.Session["serviceLocator"]).GetFunctionData().GetDataSetBySQL(condition).Tables["TempTable"];
            dt = ocmb.GetDataSetBySQL(condition).Tables["TempTable"];
            if (dt.Rows.Count > 0)
            {
                return false;
            }
            return true;
        }

        public bool CheckOverTime(string WorkNo, string OTDate, string BeginTime, string EndTime, string ShiftNo, bool isAdvance)
        {
            string AdvDt = "to_date('" + OTDate + "','yyyy/mm/dd')";
            string begTime = "to_date('" + Convert.ToDateTime(BeginTime).ToString("yyyy/MM/dd HH:mm") + "','yyyy/mm/dd hh24:mi')";
            string endTime = "to_date('" + Convert.ToDateTime(EndTime).ToString("yyyy/MM/dd HH:mm") + "','yyyy/mm/dd hh24:mi')";
            string condition = "";
           // DataTable dt = new DataTable();
            if (isAdvance)
            {
                condition = " and a.WorkNo='" + WorkNo + "' and a.otdate>=to_date('" + OTDate + "','yyyy/mm/dd')-1  and ((a.BeginTime<=To_Date('" + Convert.ToDateTime(BeginTime).ToString("yyyy/MM/dd HH:mm") + "','yyyy/mm/dd hh24:mi')  and a.EndTime>To_Date('" + Convert.ToDateTime(BeginTime).ToString("yyyy/MM/dd HH:mm") + "','yyyy/mm/dd hh24:mi')) or (a.BeginTime<To_Date('" + Convert.ToDateTime(EndTime).ToString("yyyy/MM/dd HH:mm") + "','yyyy/mm/dd hh24:mi')  and a.EndTime>=To_Date('" + Convert.ToDateTime(EndTime).ToString("yyyy/MM/dd HH:mm") + "','yyyy/mm/dd hh24:mi')) or(a.BeginTime >= to_date('" + Convert.ToDateTime(BeginTime).ToString("yyyy/MM/dd HH:mm") + "','yyyy/mm/dd hh24:mi')  and a.EndTime <= to_date('" + Convert.ToDateTime(EndTime).ToString("yyyy/MM/dd HH:mm") + "','yyyy/mm/dd hh24:mi'))) ";
                //this.tempDataTable = ((ServiceLocator)this.Session["serviceLocator"]).GetOTMAdvanceApplyData().GetDataByCondition(condition).Tables["OTM_AdvanceApply"];
                this.tempDataTable = ocmb.GetApplyDataByCondition(condition).Tables["OTM_AdvanceApply"];
                if (this.tempDataTable.Rows.Count > 0)
                {
                    return false;
                }
                return true;
            }
            condition = " and a.WorkNo='" + WorkNo + "' and a.otdate>=to_date('" + OTDate + "','yyyy/mm/dd')-1  and ((a.BeginTime<=To_Date('" + Convert.ToDateTime(BeginTime).ToString("yyyy/MM/dd HH:mm") + "','yyyy/mm/dd hh24:mi')  and a.EndTime>To_Date('" + Convert.ToDateTime(BeginTime).ToString("yyyy/MM/dd HH:mm") + "','yyyy/mm/dd hh24:mi')) or (a.BeginTime<To_Date('" + Convert.ToDateTime(EndTime).ToString("yyyy/MM/dd HH:mm") + "','yyyy/mm/dd hh24:mi')  and a.EndTime>=To_Date('" + Convert.ToDateTime(EndTime).ToString("yyyy/MM/dd HH:mm") + "','yyyy/mm/dd hh24:mi')) or(a.BeginTime >= to_date('" + Convert.ToDateTime(BeginTime).ToString("yyyy/MM/dd HH:mm") + "','yyyy/mm/dd hh24:mi')  and a.EndTime <= to_date('" + Convert.ToDateTime(EndTime).ToString("yyyy/MM/dd HH:mm") + "','yyyy/mm/dd hh24:mi'))) ";
            //this.tempDataTable = ((ServiceLocator)this.Session["serviceLocator"]).GetOTMExceptionApplyData().GetDataByCondition(condition).Tables["OTM_RealApply"];
            this.tempDataTable = ocmb.GetRealDataByCondition(condition).Tables["OTM_RealApply"];
            if (this.tempDataTable.Rows.Count > 0)
            {
                return false;
            }
            return true;
        }

        public bool CheckOTOverETM(string WorkNo, string BeginTime, string EndTime)
        {
            string begTime = "to_date('" + Convert.ToDateTime(BeginTime).ToString("yyyy/MM/dd HH:mm") + "','yyyy/mm/dd hh24:mi')";
            string endTime = "to_date('" + Convert.ToDateTime(EndTime).ToString("yyyy/MM/dd HH:mm") + "','yyyy/mm/dd hh24:mi')";
            DataTable dt = new DataTable();
            string condition = " SELECT b.workno from GDS_ATT_CURRICULA a,GDS_ATT_CURRICULAENTER b  WHERE a.cno=b.cno AND (a.Status='Open' or a.Status='Examined' or a.Status='Close')  and b.WorkNo='" + WorkNo + "' and a.cdate >to_date('" + Convert.ToDateTime(BeginTime).AddDays(-1.0).ToString("yyyy/MM/dd") + "','yyyy/mm/dd')  and ((to_date(to_char(a.cdate,'yyyy/MM/dd')||' '||a.starttime,'yyyy/MM/dd hh24:mi')<=" + begTime + " and to_date(to_char(a.cdate,'yyyy/MM/dd')||' '||a.endtime,'yyyy/MM/dd hh24:mi')>" + begTime + ") or (to_date(to_char(a.cdate,'yyyy/MM/dd')||' '||a.starttime,'yyyy/MM/dd hh24:mi')<" + endTime + " and to_date(to_char(a.cdate,'yyyy/MM/dd')||' '||a.endtime,'yyyy/MM/dd hh24:mi')>=" + endTime + ") or(to_date(to_char(a.cdate,'yyyy/MM/dd')||' '||a.starttime,'yyyy/MM/dd hh24:mi') >= " + begTime + " and to_date(to_char(a.cdate,'yyyy/MM/dd')||' '||a.endtime,'yyyy/MM/dd hh24:mi') <= " + endTime + ")) ";
            dt = ocmb.GetDataSetBySQL(condition).Tables["TempTable"];
            if (dt.Rows.Count > 0)
            {
                return false;
            }
            return true;
        }

        public string GetOTHours(string WorkNo, string OTDate, string BeginTime, string EndTime, string OTType)
        {
            double hours = 0;
            //Function CommonFun = new Function();
            if (BeginTime != EndTime)
            {
               // hours = CommonFun.GetOtHours(WorkNo.ToUpper(), OTDate, BeginTime, EndTime, OTType);
                hours = ocmb.GetOtHours(WorkNo.ToUpper(), OTDate, BeginTime, EndTime, OTType);
            }
            return hours.ToString();
        }

        public bool CheckWorkTime(string WorkNo, string OTDate, string BeginTime, string EndTime, string ShiftNo)
        {
            string ShiftType = "";
            string sOffDutyTime = "";
            string OffDutyTime = "";
            DataTable dt = new DataTable();
            // string OtStatus = ((ServiceLocator)this.Session["serviceLocator"]).GetFunctionData().GetOTType(WorkNo, Convert.ToDateTime(OTDate).ToString("yyyy/MM/dd"));
            string OtStatus = ocmb.GetOTType(WorkNo, Convert.ToDateTime(OTDate).ToString("yyyy/MM/dd"));
            //this.tempDataTable = ((ServiceLocator)this.Session["serviceLocator"]).GetFunctionData().GetDataSetBySQL("select a.ShiftType,a.OffDutyTime,nvl(a.PMRestETime,a.OffDutyTime) RestTime from KQM_WorkShift a where ShiftNo='" + ShiftNo + "'").Tables["TempTable"];
            dt = ocmb.GetDataSetBySQL("select a.ShiftType,a.OffDutyTime,nvl(a.PMRestETime,a.OffDutyTime) RestTime from GDS_ATT_WORKSHIFT a where ShiftNo='" + ShiftNo + "'").Tables["TempTable"];
            if (dt.Rows.Count > 0)
            {
                ShiftType = Convert.ToString(dt.Rows[0]["ShiftType"]);
                sOffDutyTime = Convert.ToString(dt.Rows[0]["OffDutyTime"]);
                OffDutyTime = Convert.ToString(dt.Rows[0]["RestTime"]);
            }
            if (OtStatus == "G1")
            {
                string begTime = "to_date('" + Convert.ToDateTime(BeginTime).ToString("yyyy/MM/dd HH:mm") + "','yyyy/mm/dd hh24:mi')";
                string endTime = "to_date('" + Convert.ToDateTime(EndTime).ToString("yyyy/MM/dd HH:mm") + "','yyyy/mm/dd hh24:mi')";
                string DBbegTime = "to_date('" + Convert.ToDateTime(OTDate).ToString("yyyy/MM/dd") + "'||''||ondutytime,'yyyy/mm/dd hh24:mi')";
                string DBendTime = "to_date('" + Convert.ToDateTime(OTDate).ToString("yyyy/MM/dd") + "'||''||'" + sOffDutyTime + "','yyyy/mm/dd hh24:mi')";
                if (ShiftType.Equals("Y"))
                {
                    if (ShiftNo.StartsWith("C"))
                    {
                        DBendTime = "to_date('" + Convert.ToDateTime(OTDate).AddDays(1.0).ToString("yyyy/MM/dd") + "'||''||'" + sOffDutyTime + "','yyyy/mm/dd hh24:mi')";
                    }
                    else
                    {
                        DBendTime = "to_date('" + Convert.ToDateTime(OTDate).ToString("yyyy/MM/dd") + "'||''||'" + sOffDutyTime + "','yyyy/mm/dd hh24:mi')";
                    }
                }
                else if (ShiftNo.StartsWith("C"))
                {
                    DBendTime = "to_date('" + Convert.ToDateTime(OTDate).AddDays(1.0).ToString("yyyy/MM/dd") + "'||''||'" + OffDutyTime + "','yyyy/mm/dd hh24:mi')";
                }
                else
                {
                    DBendTime = "to_date('" + Convert.ToDateTime(OTDate).ToString("yyyy/MM/dd") + "'||''||'" + OffDutyTime + "','yyyy/mm/dd hh24:mi')";
                }
                string tmpSql = "select ShiftNo from GDS_ATT_WORKSHIFT where ShiftNo='" + ShiftNo + "' and ((" + DBbegTime + "<=" + begTime + " and " + DBendTime + ">" + begTime + ") or (" + DBbegTime + "<" + endTime + " and " + DBendTime + ">=" + endTime + ") or (" + DBbegTime + ">=" + begTime + " and " + DBendTime + "<=" + endTime + "))";
                //this.tempDataTable = ((ServiceLocator)this.Session["serviceLocator"]).GetFunctionData().GetDataSetBySQL(tmpSql).Tables["TempTable"];
                dt = ocmb.GetDataSetBySQL(tmpSql).Tables["TempTable"];
                if (dt.Rows.Count > 0)
                {
                    return false;
                }
            }
            return true;
        }

        public bool CheckG3WorkTime(string WorkNo, string OTDate, string BeginTime, string EndTime, string OTType)
        {
            if (OTType.Equals("G3") && ((TimeSpan.Parse(Convert.ToDateTime(BeginTime).ToString("HH:mm")) > TimeSpan.Parse(Convert.ToDateTime(EndTime).ToString("HH:mm"))) && !Convert.ToDateTime(EndTime).ToString("HH:mm").Equals("00:00")))
            {
                //string strOTType = ((ServiceLocator)this.Session["serviceLocator"]).GetFunctionData().GetOTType(WorkNo, Convert.ToDateTime(OTDate).AddDays(1.0).ToString("yyyy/MM/dd"));
                string strOTType = ocmb.GetOTType(WorkNo, Convert.ToDateTime(OTDate).AddDays(1.0).ToString("yyyy/MM/dd"));
                if (strOTType.Equals("G1") || strOTType.Equals("G2"))
                {
                    return false;
                }
            }
            return true;
        }
       
    }
}
