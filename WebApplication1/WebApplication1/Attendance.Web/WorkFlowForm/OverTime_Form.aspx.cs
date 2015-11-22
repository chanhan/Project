using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using GDSBG.MiABU.Attendance.BLL.KQM.Query.OTM;
using System.Text;
using GDSBG.MiABU.Attendance.Model.KQM.Query.OTM;
using Resources;
using System.Configuration;
using System.Drawing;
using Infragistics.WebUI.UltraWebGrid;
using GDSBG.MiABU.Attendance.BLL.WorkFlow;
using System.Collections;
using System.Web.Script.Serialization;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;


namespace GDSBG.MiABU.Attendance.Web.WorkFlowForm
{
    public partial class OverTime_Form : BasePage
    {
        static DataTable ddlTable = new DataTable();
        OTMDetailQryBll detailQryBll = new OTMDetailQryBll();
        static DataTable advanceApplyTable = new DataTable();
        OTMAdvanceApplyQryModel advanceApplyQryModel = new OTMAdvanceApplyQryModel();
        Dictionary<string, string> ClientMessage = null;
        JavaScriptSerializer JsSerializer = new JavaScriptSerializer();
        string dCode = "";
        string batchEmployeeNo = "";
        string dateFrom = "";
        string dateTo = "";
        string hoursCondition = "";
        string hours = "";
        string condition = "";
        private DataTable tempDataTable;
        OverTimeBll bll = new OverTimeBll();
        protected System.Data.DataSet dataSet;
        private string dateFormat = "yyyy/MM/dd";
        public string LHZBLimitTime = "";
        static SynclogModel logmodel = new SynclogModel();

        protected void Page_Load(object sender, EventArgs e)
        {

            if (ClientMessage == null)
            {
                ClientMessage = new Dictionary<string, string>();
                ClientMessage.Add("common_message_checksavechangeauditflow", Message.common_message_checksavechangeauditflow);
                
                ClientMessage.Add("checkapproveflag", Message.checkapproveflag);
                ClientMessage.Add("copy_sesse_1", Message.copy_sesse_1);
                ClientMessage.Add("DataReturn", Message.DataReturn);
                ClientMessage.Add("AuditUncancelaudit", Message.AuditUncancelaudit);
                ClientMessage.Add("NoSelect", Message.NoSelect);
                ClientMessage.Add("DdeleteApplyovertimeEnd", Message.DdeleteApplyovertimeEnd);
                ClientMessage.Add("AuditUnaudit", Message.AuditUnaudit);
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


                this.txtOTDateFrom.Text = DateTime.Now.AddDays((double)(-DateTime.Now.Day + 1)).ToString("yyyy/MM/dd");
                this.txtOTDateTo.Text = DateTime.Now.AddDays(2.0).ToString("yyyy/MM/dd");
                this.ModuleCode.Value = Request.QueryString["ModuleCode"];
              
                ddlDataBind();
                if (!string.IsNullOrEmpty(LHZBLimitTime))
                {
                    //每天LHZBLimitTime時間后不能新增、修改加班預報

                    DateTime dtLimit = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd") + " " + LHZBLimitTime);
                    if (dtLimit < DateTime.Now)
                    {
                        this.btnsendsign.Enabled = false;
                    }
                }
                this.Query(true, "Goto");
            }
            SetSelector(imgDepCode, txtDepCode, txtDepName, "dept", Request.QueryString["ModuleCode"]);
            SetCalendar(txtOTDateFrom, txtOTDateTo);
            HiddenWorkNo.Value = CurrentUserInfo.Personcode;

            PageHelper.ButtonControlsWF(FuncList, pnlShowPanel.Controls, base.FuncListModule);
        }

        /// <summary>
        /// 下拉列表綁定數據
        /// </summary>
        protected void ddlDataBind()
        {
            ddlTable = new DataTable();
            ddlTable = detailQryBll.GetDataByCondition("condition1");
            this.ddlOTStatus.DataSource = ddlTable.DefaultView;
            this.ddlOTStatus.DataTextField = "DataValue";
            this.ddlOTStatus.DataValueField = "DataCode";
            this.ddlOTStatus.DataBind();
            this.ddlOTStatus.Items.Insert(0, new ListItem("", ""));
            this.ddlOTStatus.SelectedValue = "";
            ddlTable.Clear();
            ddlTable = detailQryBll.GetDataByCondition("condition2");
            this.ddlPersonType.DataSource = ddlTable.DefaultView;
            this.ddlPersonType.DataTextField = "DataValue";
            this.ddlPersonType.DataValueField = "DataCode";
            this.ddlPersonType.DataBind();
            this.ddlPersonType.Items.Insert(0, new ListItem("", ""));

        }

        #region 重置
        /// <summary>
        /// 重置按鈕事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnReset_Click(object sender, EventArgs e)
        {
            this.txtDepCode.Text = "";
            this.txtDepName.Text = "";
            this.txtBillNo.Text = "";
            this.txtEmployeeNo.Text = "";
            this.txtName.Text = "";
            this.ddlPersonType.SelectedValue = "";
            this.ddlOTType.SelectedValue = "";
            this.ddlOTStatus.SelectedValue = "";
            this.ddlIsProject.SelectedValue = "";
            this.txtHours.Text = string.Empty;
            this.txtOTDateFrom.Text = DateTime.Now.AddDays((double)(-DateTime.Now.Day + 1)).ToString("yyyy/MM/dd");
            this.txtOTDateTo.Text = DateTime.Now.ToString("yyyy/MM/dd");
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

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            if ((this.txtOTDateFrom.Text.Trim() == "") || (this.txtOTDateTo.Text.Trim() == ""))
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "isnumber", "alert('" + Message.ErrWorkDayNull + "');", true);
            }
            else
            {
                this.Query(true, "Goto");
                this.ProcessFlag.Value = "";
                this.HiddenWorkNo.Value = this.txtEmployeeNo.Text.Trim().ToUpper();
            }
        }

        /// <summary>
        /// 查詢
        /// </summary>
        /// <param name="WindowOpen"></param>
        /// <param name="forwarderType"></param>
        //private void Query(bool WindowOpen, string forwarderType)
        //{

        //    if (this.ProcessFlag.Value.ToLower().Equals("condition") || WindowOpen)
        //    {
        //        GetPageValue();
        //        this.ViewState.Add("condition", condition);
        //    }
        //    else
        //    {
        //        condition = Convert.ToString(this.ViewState["condition"]);
        //    }
        //    int totalCount = 0;
        //    advanceApplyTable = detailQryBll.GetOTMAdvanceQryList(advanceApplyQryModel,base.SqlDep.ToString(), dCode, batchEmployeeNo, dateFrom, dateTo, hoursCondition, hours, pager.CurrentPageIndex, pager.PageSize, out  totalCount);
        //    this.DataUIBind();
        //    pager.RecordCount = totalCount;
        //    pager.TextAfterPageIndexBox = "/" + pager.PageCount.ToString();
        //}

        protected bool CheckDateMonths(string StartDate, string EndDate)
        {
            try
            {
                if (Convert.ToInt32(bll.GetValue("select floor(MONTHS_BETWEEN(to_date('" + Convert.ToDateTime(EndDate).ToString("yyyy/MM/dd") + "','yyyy/MM/dd'),to_date('" + Convert.ToDateTime(StartDate).ToString("yyyy/MM/dd") + "','yyyy/MM/dd'))) sDays from dual")) >= 3)
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

        /// <summary>
        /// 查詢
        /// </summary>
        /// <param name="WindowOpen"></param>
        /// <param name="forwarderType"></param>
        private void Query(bool WindowOpen, string forwarderType)
        {
            string condition = " and a.isProject='N' ";
            if (this.ProcessFlag.Value.ToLower().Equals("condition") || WindowOpen)
            {
                if (this.txtDepName.Text.Trim().Length > 0)
                {
                    //if (b)
                    //{
                    condition += " AND b.dCode IN ((" + base.SqlDep + ") INTERSECT SELECT DepCode FROM GDS_SC_DEPARTMENT START WITH depname = '"
                                    + this.txtDepName.Text.Trim() + "' CONNECT BY PRIOR depcode = parentdepcode) ";
                    //}
                    //else
                    //{
                    //condition += " AND b.dCode IN (SELECT DepCode FROM GDS_SC_DEPARTMENT START WITH depname = '"
                    //                + this.txtDepName.Text.Trim() + "' CONNECT BY PRIOR depcode = parentdepcode) ";
                    //}
                }
                else
                {
                    //if (this.bPrivileged)
                    //{
                        condition += " AND b.dcode in (" + base.SqlDep + ")";
                    //}
                }
                if (this.txtBillNo.Text.Trim().Length != 0)
                {
                    condition += " AND a.BillNo like '" + this.txtBillNo.Text.Trim() + "%'";
                }
                if (this.txtEmployeeNo.Text.Trim().Length != 0)
                {
                    condition += " AND b.WorkNO = '" + this.txtEmployeeNo.Text.Trim().ToUpper() + "'";
                }
                if (this.txtBatchEmployeeNo.Text.Trim().Length != 0)
                {
                    string[] workNoList = this.txtBatchEmployeeNo.Text.Trim().Split(Convert.ToChar((char)13));
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
                if (this.txtName.Text.Trim().Length != 0)
                {
                    condition += " AND b.LocalName like '" + this.txtName.Text.Trim() + "%'";
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
                if (this.txtEmployeeNo.Text.Trim().Length == 0 && this.txtBatchEmployeeNo.Text.Trim().Length==0&&
                    this.txtName.Text.Trim().Length == 0 &&
                    this.txtBillNo.Text.Trim().Length == 0)
                {
                    if (this.txtOTDateFrom.Text.Trim().Length == 0 || this.txtOTDateTo.Text.Trim().Length == 0)
                    {
                        this.WriteMessage(1, Message.common_message_datenotnull);
                        return;
                    }
                    else
                    {
                        if (!CheckDateMonths(this.txtOTDateFrom.Text.Trim(), this.txtOTDateTo.Text.Trim()))
                        {
                            return;
                        }
                    }
                }
                if (this.txtOTDateFrom.Text.Trim().Length > 0)
                {
                    condition += " AND a.OTDate >= to_date('" + DateTime.Parse(this.txtOTDateFrom.Text.Trim()).ToString("yyyy/MM/dd") + "','yyyy/mm/dd') ";
                }
                if (this.txtOTDateTo.Text.Trim().Length > 0)
                {
                    condition += " AND a.OTDate <= to_date('" + DateTime.Parse(this.txtOTDateTo.Text.Trim()).ToString("yyyy/MM/dd") + "','yyyy/mm/dd') ";
                }
                if (this.txtHours.Text.Trim().Length != 0)
                {
                    condition += " AND a.Hours " + this.ddlHoursCondition.SelectedValue + this.txtHours.Text.Trim();
                }
                if (this.ddlIsProject.SelectedValue.Length > 0)
                {
                    condition += " AND a.ImportFlag ='" + this.ddlIsProject.SelectedValue + "'";
                }
                this.ViewState.Add("condition", condition);
            }
            else
            {
                condition = System.Convert.ToString(this.ViewState["condition"]);
            }
            int totalCount = 0;
            if (this.ddlOTStatus.SelectedValue.Equals("0"))
            {
                condition += " AND rownum<=1500 ";
                
                this.dataSet =bll.GetDataByCondition_pager(condition, pager.CurrentPageIndex, pager.PageSize, out  totalCount);
                advanceApplyTable = this.dataSet.Tables["OTM_AdvanceApply"];
              
                //this.SetPageInfor(1, 1, this.dataSet.Tables["OTM_AdvanceApply"].Rows.Count);
            }
            else
            {
                this.dataSet = bll.GetDataByCondition_pager(condition, pager.CurrentPageIndex, pager.PageSize, out  totalCount);
                advanceApplyTable = this.dataSet.Tables["OTM_AdvanceApply"];              
                //this.SetForwardPage(forwarderType, ((Infragistics.WebUI.WebDataInput.WebNumericEdit)this.PageNavigator.FindControl("WebNumericEditCurrentpage")).Value.ToString());
                //this.dataSet = ((eBFW.Sys.ServiceLocator)Session["serviceLocator"]).GetOTMAdvanceApplyData().GetDataByCondition(condition, System.Convert.ToInt32(Session["PageSize"]), ref this.forwarderPage, ref this.totalPage, ref this.totalRecodrs);
                //this.SetPageInfor(this.forwarderPage, this.totalPage, this.totalRecodrs);
            }

            this.DataUIBind();
            pager.RecordCount = totalCount;
            pager.TextAfterPageIndexBox = "/" + pager.PageCount.ToString();
            this.WriteMessage(0, Message.common_message_trans_complete);
        }

      

        #region 獲取頁面值

        public void GetPageValue()
        {
            dCode = this.txtDepName.Text.Trim();
            batchEmployeeNo = this.txtEmployeeNo.Text.Trim();
            dateFrom = DateTime.Parse(this.txtOTDateFrom.Text.Trim()).ToString("yyyy/MM/dd");
            dateTo = DateTime.Parse(this.txtOTDateTo.Text.Trim()).ToString("yyyy/MM/dd");
            hoursCondition = this.ddlHoursCondition.SelectedValue;
            hours = this.txtHours.Text.Trim();
            advanceApplyQryModel.BillNo = this.txtBillNo.Text.Trim();
            advanceApplyQryModel.LocalName = this.txtName.Text.Trim();
            advanceApplyQryModel.OverTimeType = this.ddlPersonType.SelectedValue;
            advanceApplyQryModel.OTType = this.ddlOTType.SelectedValue;
            advanceApplyQryModel.Status = this.ddlOTStatus.SelectedValue;
            advanceApplyQryModel.IsProject = this.ddlIsProject.SelectedValue;
        }
        #endregion

        #region 頁面綁定數據
        /// <summary>
        /// WebGrid綁定數據
        /// </summary>
        private void DataUIBind()
        {
            if (advanceApplyTable != null)
            {
                this.UltraWebGrid.DataSource = advanceApplyTable.DefaultView;
                this.UltraWebGrid.DataBind();
            }
        }

        #endregion

        #region 分頁
        protected void pager_PageChanged(object sender, EventArgs e)
        {
            this.Query(true, "Goto");
        }
        #endregion


        /// <summary>
        /// WebGrid的DataBound方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void UltraWebGrid_DataBound(object sender, EventArgs e)
        {
            string oTDate = "";
            string LHZBIsDisplayG2G3 = "";
            try
            {
                LHZBIsDisplayG2G3 = ConfigurationManager.AppSettings["LHZBIsDisplayG2G3"];
            }
            catch
            {
                LHZBIsDisplayG2G3 = "N";
            }
            for (int i = 0; i < this.UltraWebGrid.Rows.Count; i++)
            {
                if (LHZBIsDisplayG2G3 == "Y")
                {
                    if (this.UltraWebGrid.Rows[i].Cells.FromKey("OTType").Text.Trim() == "G2")
                    {
                        this.UltraWebGrid.Rows[i].Cells.FromKey("OTType").Value = string.Concat(new object[] { this.UltraWebGrid.Rows[i].Cells.FromKey("OTType").Value, "(", base.GetResouseValue("otm.g2.remark"), ")" });
                    }
                    else if (this.UltraWebGrid.Rows[i].Cells.FromKey("OTType").Text.Trim() == "G3")
                    {
                        this.UltraWebGrid.Rows[i].Cells.FromKey("OTType").Value = string.Concat(new object[] { this.UltraWebGrid.Rows[i].Cells.FromKey("OTType").Value, "(", base.GetResouseValue("otm.g3.remark"), ")" });
                    }
                }
                if (this.UltraWebGrid.Rows[i].Cells.FromKey("Status").Text.Trim() == "0")
                {
                    this.UltraWebGrid.Rows[i].Cells.FromKey("StatusName").Style.ForeColor = Color.Green;
                }
                if (this.UltraWebGrid.Rows[i].Cells.FromKey("Status").Text.Trim() == "1")
                {
                    this.UltraWebGrid.Rows[i].Cells.FromKey("StatusName").Style.ForeColor = Color.Blue;
                }
                if (this.UltraWebGrid.Rows[i].Cells.FromKey("Status").Text.Trim() == "2")
                {
                    this.UltraWebGrid.Rows[i].Cells.FromKey("StatusName").Style.ForeColor = Color.Black;
                }
                if (this.UltraWebGrid.Rows[i].Cells.FromKey("Status").Text.Trim() == "3")
                {
                    this.UltraWebGrid.Rows[i].Cells.FromKey("StatusName").Style.ForeColor = Color.Maroon;
                }
                if ((this.UltraWebGrid.Rows[i].Cells.FromKey("IsProject").Value != null) && (this.UltraWebGrid.Rows[i].Cells.FromKey("IsProject").Text.Trim() == "Y"))
                {
                    this.UltraWebGrid.Rows[i].Style.ForeColor = Color.Red;
                }
                if ((this.UltraWebGrid.Rows[i].Cells.FromKey("OTMSGFlag").Value != null) && (this.UltraWebGrid.Rows[i].Cells.FromKey("WorkDesc").Value != null))
                {
                    string CSS4S0002 = this.UltraWebGrid.Rows[i].Cells.FromKey("OTMSGFlag").Text;
                    if (CSS4S0002 != null)
                    {
                        if (!(CSS4S0002 == "1"))
                        {
                            if (CSS4S0002 == "2")
                            {
                                goto Label_0546;
                            }
                            if (CSS4S0002 == "3")
                            {
                                goto Label_056C;
                            }
                            if (CSS4S0002 == "4")
                            {
                                goto Label_0592;
                            }
                            if (CSS4S0002 == "5")
                            {
                                goto Label_05B8;
                            }
                            if (CSS4S0002 == "6")
                            {
                                goto Label_05DE;
                            }
                        }
                        else
                        {
                            this.UltraWebGrid.Rows[i].Style.ForeColor = Color.Green;
                        }
                    }
                }
                goto Label_0605;
            Label_0546:
                this.UltraWebGrid.Rows[i].Style.ForeColor = Color.Blue;
                goto Label_0605;
            Label_056C:
                this.UltraWebGrid.Rows[i].Style.ForeColor = Color.SaddleBrown;
                goto Label_0605;
            Label_0592:
                this.UltraWebGrid.Rows[i].Style.ForeColor = Color.Maroon;
                goto Label_0605;
            Label_05B8:
                this.UltraWebGrid.Rows[i].Style.ForeColor = Color.Tomato;
                goto Label_0605;
            Label_05DE:
                this.UltraWebGrid.Rows[i].Style.ForeColor = Color.Red;
            Label_0605:
                string workNo = this.UltraWebGrid.Rows[i].Cells.FromKey("WorkNo").Text.Trim();
                oTDate = Convert.ToDateTime(this.UltraWebGrid.Rows[i].Cells.FromKey("OTDate").Text).ToString("yyyy/MM/dd");
                this.tempDataTable = detailQryBll.GetDataTableBySQL(workNo, oTDate);
                if (tempDataTable.Rows.Count > 0)
                {
                    this.UltraWebGrid.Rows[i].Cells.FromKey("G1Total").Value = Convert.ToDouble(this.UltraWebGrid.Rows[i].Cells.FromKey("G1Total").Value) + Convert.ToDouble(tempDataTable.Rows[0]["G1Total"]);
                    this.UltraWebGrid.Rows[i].Cells.FromKey("G2Total").Value = Convert.ToDouble(this.UltraWebGrid.Rows[i].Cells.FromKey("G2Total").Value) + Convert.ToDouble(tempDataTable.Rows[0]["G2Total"]);
                    this.UltraWebGrid.Rows[i].Cells.FromKey("G3Total").Value = Convert.ToDouble(this.UltraWebGrid.Rows[i].Cells.FromKey("G3Total").Value) + Convert.ToDouble(tempDataTable.Rows[0]["G3Total"]);
                }
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
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
                        if (UltraWebGrid.Rows[i].Cells.FromKey("Status").Text.Trim() != "0" && UltraWebGrid.Rows[i].Cells.FromKey("Status").Text.Trim() != "3")
                        {
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('" + Message.OnlyNoAudit + "');</script>");
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
                        this.tempDataTable = bll.GetDataByCondition_1("and (a.status='0' or a.status='3') and a.ID='" + this.UltraWebGrid.DisplayLayout.Rows[i].Cells.FromKey("id").Value + "' and INSTR(a.ImportFlag,'N')>0").Tables[0];
                        if (this.tempDataTable.Rows.Count > 0)
                        {
                            if (bll.DeleteData(this.tempDataTable,logmodel))
                            {
                                intDeleteOk += 1;
                            }
                        }
                        else
                        {
                            intDeleteError += 1;
                        }
                    }
                }
                if (intDeleteOk + intDeleteError > 0)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('" + Message.SuccCount + intDeleteOk + "," + Message.FaileCount + intDeleteError + "')</script>");
                   // this.WriteMessage(0, this.GetResouseValue("common.message.successcount") + "：" + intDeleteOk + ";" + this.GetResouseValue("common.message.errorcount") + "：" + intDeleteError);
                    this.Query(false, "Goto");
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('" + Message.common_message_data_select + "')</script>");
                    //this.WriteMessage(1, this.GetResouseValue("common.message.data.select"));
                    return;
                }
                this.ProcessFlag.Value = "";
            }
            catch (System.Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('" + ex.Message+ "')</script>");
               // this.WriteMessage(2, (ex.InnerException == null ? ex.Message : ex.InnerException.Message));
            }
        }

        protected void Btn_Imp_Click(object sender, EventArgs e)
        {
            this.UltraWebGridImport.Rows.Clear();
            if (!this.PanelImport.Visible)
            {
                this.PanelImport.Visible = true;
                this.PanelData.Visible = false;
                this.btnQuery.Enabled = false;
                this.btnReset.Enabled = false;
                this.Btn_Add.Enabled = false;
                this.btnModify.Enabled = false;
                this.btnDelete.Enabled = false;
                //this.ButtonAuditTable.Enabled = false;
                this.btnAudit.Enabled = false;
                this.Btnalladd.Enabled = false;
                this.btnCancelAudit.Enabled = false;
                this.btnsendsign.Enabled = false;
               // this.ButtonViewSchedule.Enabled = false;
               // this.ButtonOrgAudit.Enabled = false;
                //this.ButtonImportG4.Enabled = false;
                //this.ButtonAddG4.Enabled = false;
                this.Btn_Imp.Text = Message.btnBack;
                this.labeluploadMsg.Text = "";
            }
            else
            {
                this.PanelImport.Visible = false;
                this.PanelData.Visible = true;
                this.btnQuery.Enabled = true;
                this.btnReset.Enabled = true;
                this.Btn_Add.Enabled = true;
                this.btnModify.Enabled = true;
                this.btnDelete.Enabled = true;
              //  this.ButtonAuditTable.Enabled = true;
                this.btnAudit.Enabled = true;
                this.Btnalladd.Enabled = true;
                this.btnCancelAudit.Enabled = true;
                this.btnsendsign.Enabled = true;
               // this.ButtonViewSchedule.Enabled = true;
                //this.ButtonOrgAudit.Enabled = true;
                //this.ButtonImportG4.Enabled = true;
                //this.ButtonAddG4.Enabled = true;
                this.Btn_Imp.Text = Message.btnImport;
            }
            this.HiddenImportType.Value = "";
        }


        protected void ButtonImportSave_Click(object sender, EventArgs e)
        {
            DataView dv;
            try
            {
                string DefaultLanguage = System.Configuration.ConfigurationManager.AppSettings["DefaultLanguage"];
                if (string.IsNullOrEmpty(DefaultLanguage))
                {
                    DefaultLanguage = "zh-tw";
                }
                string CurrentLanguage = DefaultLanguage;
                string strFileName = FileUpload.FileName;
                string strFileSize = FileUpload.PostedFile.ContentLength.ToString();
                string strFileExt = strFileName.Substring(strFileName.LastIndexOf(".") + 1);
                string strTime = string.Format("{0:yyyyMMddHHmmssffff}", System.DateTime.Now);
                string strFilePath = Server.MapPath("..\\") + "ExportFileTemp" + "\\" + strTime + ".xls";
                //
                string isShowMoveLeaveFlag = bll.GetValue("select nvl(max(paravalue),'N') from GDS_SC_PARAMETER where paraname='IsShowMoveLeaveFlag'");
                //

                if (strFileExt == "xls")
                {
                    FileUpload.SaveAs(strFilePath);
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('" + Message.common_message_uploadxls + "');</script>");
                    return;
                }
                string ImportType = this.HiddenImportType.Value;               
               // EncodeRobert edControl = new EncodeRobert();
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
                if (isShowMoveLeaveFlag.Equals("Y")) //總部周邊顯示新功能調休，計劃調休日期取消。

                {
                    dataSet.Tables["KQM_Import"].Columns.Add(new System.Data.DataColumn("G2IsForRest", typeof(string)));
                }
                else
                {
                    dataSet.Tables["KQM_Import"].Columns.Add(new System.Data.DataColumn("PlanAdjust", typeof(string)));
                }
                string WorkNo = "", LocalName = "", OTDate = "", BeginTime = "", EndTime = "", Hours = "", WorkDesc = "", PlanAdjust = "", errorMsg = "";
                int index = 0, errorCount = 0;
                string ShiftNo = "", OTMSGFlag = "", tmpRemark = "", OTType = "";
                string strtemp = "", condition = "", nBeginTime = "", nEndTime = "";
                // add by xukai 20111015 定義字段 用於異化LH總部周邊，管控時間和G2調休
                // string LHZBLimitTime = "";
                // string LHZBIsLimitApply = "";
                //string workflag = "", 
                string G2IsForRest = "", PersonOverType = "";
                //try
                //{
                //    LHZBIsLimitApply = System.Configuration.ConfigurationManager.AppSettings["LHZBIsLimitAdvApplyTime"];
                //    LHZBLimitTime = System.Configuration.ConfigurationManager.AppSettings["LHZBLimitTime"];
                //}
                //catch
                //{
                //    LHZBIsLimitApply = "N";
                //    LHZBLimitTime = "";
                //}
                //end
                string OTMAdvanceBeforeDays = bll.GetValue("select nvl(max(paravalue),'2') from GDS_SC_PARAMETER where paraname='OTMAdvanceBeforeDays'");
                string appUserIsIn = bll.GetValue("select nvl(max(workno),'Y') from GDS_ATT_EMPLOYEE where workno='" + CurrentUserInfo.Personcode + "'");
                if (!appUserIsIn.Equals("Y"))//允許申報當前日期以前多少天的加班除去非工作日
                {
                    //OTMAdvanceBeforeDays = Convert.ToString(Convert.ToDouble(OTMAdvanceBeforeDays) + 30);
                    int i = 0;
                    int WorkDays = 0;
                    string UserOTType = "";
                    while (i < Convert.ToDouble(OTMAdvanceBeforeDays))
                    {
                        condition = "SELECT workflag FROM GDS_ATT_BGCALENDAR WHERE workday = to_date('" + System.DateTime.Now.AddDays(-1 - i - WorkDays).ToString("yyyy/MM/dd") + "','yyyy/MM/dd') AND bgcode IN (SELECT depcode FROM GDS_SC_DEPARTMENT WHERE levelcode = '0' and companyid='" + CurrentUserInfo.CompanyId + "')";
                        UserOTType = bll.GetValue(condition);
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
                //OTMAdvanceBeforeDays = "20";

                int WorkNoCount = 0;
                SortedList list = new SortedList();
                dv = bll.ExceltoDataView(strFilePath);

                int inttotal = dv.Table.Rows.Count;//
                for (int i = 0; i < inttotal; i++)
                {
                    tmpRemark = "";
                    OTMSGFlag = "";
                    WorkNo = dv.Table.Rows[i][0].ToString();
                    LocalName = dv.Table.Rows[i][1].ToString();
                    OTDate = dv.Table.Rows[i][2].ToString();
                    BeginTime = dv.Table.Rows[i][3].ToString();
                    EndTime = dv.Table.Rows[i][4].ToString();
                    WorkDesc = dv.Table.Rows[i][5].ToString();
                    if (isShowMoveLeaveFlag.Equals("Y"))
                    {
                        G2IsForRest = dv.Table.Rows[i][6].ToString();
                        //每插入一筆數據，檢測每個人的加班類別update by xukai 20111024
                        PersonOverType = bll.GetValue("Select OverTimeType From GDS_ATT_EMPLOYEE Where Workno='" + WorkNo + "'");
                    }
                    else
                    {
                        PlanAdjust = dv.Table.Rows[i][6].ToString();
                    }
                    if (WorkNo.Length == 0)
                    {
                        break;
                    }
                    WorkNoCount = bll.GetVWorkNoCount(WorkNo, base.SqlDep);
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
                                strtemp = bll.GetValue(condition);

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
                            OTType = bll.GetOTType(WorkNo, OTDate);
                            if (ImportType.Equals("G4"))
                            {
                                if (OTType.Equals("G3"))
                                {
                                    OTType = "G4";
                                }
                                else
                                {
                                    if (errorMsg.Length > 0)
                                    {
                                        errorMsg += ",";
                                    }
                                    errorMsg += OTDate + Message.common_message_otm_checkadvanceisg3;
                                }
                            }
                            //抓取班別
                            ShiftNo = bll.GetShiftNo(WorkNo, OTDate);
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
                                Hours = GetOTHours(WorkNo, OTDate, BeginTime, EndTime, OTType);
                                if (Convert.ToDouble(Hours) < 0.5)
                                {
                                    if (errorMsg.Length > 0)
                                    {
                                        errorMsg += ",";
                                    }
                                    errorMsg += Message.otm_othourerror;
                                }                              
                                else
                                {
                                    OTMSGFlag = bll.GetOTMSGFlag(WorkNo, OTDate, Convert.ToDouble(Hours), OTType, "N", "");
                                }
                                if (OTMSGFlag != "")
                                {
                                    tmpRemark = OTMSGFlag.Substring(1, OTMSGFlag.Length - 1);
                                    OTMSGFlag = OTMSGFlag.Substring(0, 1);
                                    if (OTMSGFlag.Equals("A"))
                                    {
                                        if (errorMsg.Length > 0)
                                        {
                                            errorMsg += ",";
                                        }
                                        errorMsg += tmpRemark;
                                    }
                                    else if (this.CheckBoxFlag.Checked)
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
                        if (!ImportType.Equals("G4") || EndTime.Equals("00:00"))
                        {
                            list = bll.ReturnOTTTime(WorkNo, OTDate, Convert.ToDateTime(nBeginTime), Convert.ToDateTime(nEndTime), ShiftNo);
                            nBeginTime = Convert.ToDateTime(list.GetByIndex(list.IndexOfKey("A"))).ToString("yyyy/MM/dd HH:mm");
                            nEndTime = Convert.ToDateTime(list.GetByIndex(list.IndexOfKey("B"))).ToString("yyyy/MM/dd HH:mm");
                        }
                        //請假中不能申報加班

                        if (!CheckLeaveOverTime(WorkNo, Convert.ToDateTime(nBeginTime).ToString("yyyy/MM/dd"), Convert.ToDateTime(nBeginTime).ToString("HH:mm"), Convert.ToDateTime(nEndTime).ToString("yyyy/MM/dd"), Convert.ToDateTime(nEndTime).ToString("HH:mm")))
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
                    try
                    {
                        //一天内多笔加班时间不能交叉或重复

                        if (!CheckOverTime(WorkNo, OTDate, nBeginTime, nEndTime, ShiftNo, true))
                        {
                            if (errorMsg.Length > 0)
                            {
                                errorMsg += ",";
                            }
                            errorMsg += Message.common_message_otm_multi_repeart;
                        }

                        //G3加班后一天如果是正常上班不允許跨天申報Modify by Jackzhang2011/4/2
                        if (!CheckG3WorkTime(WorkNo, OTDate, nBeginTime, nEndTime, OTType))
                        {
                            if (errorMsg.Length > 0)
                            {
                                errorMsg += ",";
                            }
                            errorMsg += Message.common_message_otm_worktime_checkg3;//加班時間段內含有正常工作時間，零點以后不允許申報加班
                        }
                        //工作時間內不能預報加班

                        if (!CheckWorkTime(WorkNo, OTDate, nBeginTime, nEndTime, ShiftNo))
                        {
                            if (errorMsg.Length > 0)
                            {
                                errorMsg += ",";
                            }
                            errorMsg += Message.common_message_otm_worktime_repeart;
                        }
                        //判斷相同時段是否有參加教育訓練

                        if (!CheckOTOverETM(WorkNo, nBeginTime, nEndTime))
                        {
                            if (errorMsg.Length > 0)
                            {
                                errorMsg += ",";
                            }
                            errorMsg += Message.common_message_otm_etmrepeart;
                        }

                        //助理預報先取消管控20111018
                        //#region 每天16:30后不讓預報加班,非工作日也不能預報加班  龍華總部周邊 update by xukai 20111014
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
                        //        if (errorMsg.Length > 0)
                        //        {
                        //            errorMsg += ",";
                        //        }
                        //        errorMsg += this.GetResouseValue("kqm.otm.advanceapply.workflag");
                        //    }
                        //}
                        //#endregion
                    }
                    catch (System.Exception)
                    {
                    }
                    if (WorkDesc.Length == 0)
                    {
                        if (errorMsg.Length > 0)
                        {
                            errorMsg += ",";
                        }
                        errorMsg += Message.overtime_desc + Message.Required;
                    }

                    if (isShowMoveLeaveFlag.Equals("Y"))
                    {
                        if (G2IsForRest.Length > 0 && OTType.Equals("G2"))
                        {
                            if (!(G2IsForRest.Equals("Y") || G2IsForRest.Equals("N")))
                            {
                                if (errorMsg.Length > 0)
                                {
                                    errorMsg += ",";
                                }
                                errorMsg += Message.otm_isg2rest_error;
                            }
                        }
                        else
                        {
                            G2IsForRest = "N";
                        }
                    }
                    else
                    {
                        if (PlanAdjust.Length > 0 && OTType.Equals("G2"))
                        {
                            try//判斷日期
                            {
                                PlanAdjust = DateTime.Parse(PlanAdjust).ToString("yyyy/MM/dd");
                                //

                            }
                            catch (System.Exception)
                            {
                                if (errorMsg.Length > 0)
                                {
                                    errorMsg += ",";
                                }
                                errorMsg += Message.kqm_otm_planadjust + Message.common_message_data_errordate;
                            }
                        }
                        else
                        {
                            PlanAdjust = "";
                        }
                    }
                    if (errorMsg.Length == 0 && !this.CheckBoxFlag.Checked)
                    {
                        if (DefaultLanguage.ToLower().Equals("zh-tw") && CurrentLanguage.ToLower().Equals("zh-cn"))
                        {
                            tmpRemark = string.Empty;
                        }
                        this.tempDataTable = bll.GetDataByCondition_1("and 1=2").Tables["OTM_AdvanceApply"];
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
                        row["ApplyDate"] = DateTime.Now.ToString("yyyy/MM/dd");
                        row["UPDATE_USER"] = CurrentUserInfo.Personcode;
                        row["UPDATE_DATE"] = DateTime.Now.ToLongDateString();
                        row["IsProject"] = "N";//非專案
                        row["Status"] = "0";
                        row["OTShiftNo"] = ShiftNo;
                        row["UPDATE_USER"] = CurrentUserInfo.Personcode;
                        #region xukai  2011-10-17  G2加班是否用於調休
                        string g2isforrest = "";
                        //是否顯示是否調休
                        if (isShowMoveLeaveFlag.Equals("Y"))
                        {
                            if (OTType.Equals("G2"))
                            {
                                g2isforrest = G2IsForRest;
                                //update by xukai 20111024 總部周邊加班類型為L的，G2加班一定是調休
                                if (PersonOverType.IndexOf("L") != -1)
                                {
                                    g2isforrest = "Y";
                                }
                                //end
                            }
                            else
                            {
                                g2isforrest = "N";
                            }
                            row["G2ISFORREST"] = g2isforrest;
                        }
                        else
                        {
                            if (PlanAdjust.Length == 0)
                            {
                                row["PlanAdjust"] = DBNull.Value;
                            }
                            else
                            {
                                row["PlanAdjust"] = PlanAdjust;
                            }
                        }

                        #endregion
                        row.EndEdit();
                        this.tempDataTable.Rows.Add(row);
                        this.tempDataTable.AcceptChanges();
                        bll.SaveData("Add", this.tempDataTable,logmodel);
                        if (tmpRemark.StartsWith("1"))
                        {
                            this.dataSet.Tables["KQM_Import"].Rows.Add(new string[] { this.GetResouseValue("otm.advanceapply.worksixdaysavemessage"), WorkNo, LocalName, OTDate, BeginTime, EndTime, WorkDesc });
                        }
                    }
                    else
                    {
                        errorCount += 1;
                        if (isShowMoveLeaveFlag.Equals("Y"))
                        {
                            this.dataSet.Tables["KQM_Import"].Rows.Add(new string[] { errorMsg, WorkNo, LocalName, OTDate, BeginTime, EndTime, WorkDesc, G2IsForRest });
                        }
                        else
                        {
                            this.dataSet.Tables["KQM_Import"].Rows.Add(new string[] { errorMsg, WorkNo, LocalName, OTDate, BeginTime, EndTime, WorkDesc, PlanAdjust });
                        }
                    }
                    errorMsg = "";
                }
                if (!this.CheckBoxFlag.Checked)
                {
                    labeluploadMsg.Text = Message.NumberOfSuccessed + "：" + index + "  ;" + Message.NumberOfFailed + "：" + errorCount + " .";
                }
                this.UltraWebGridImport.DataSource = this.dataSet.Tables["KQM_Import"].DefaultView;
                this.UltraWebGridImport.DataBind();
            }
            catch (System.Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('" + ex.Message + "')</script>");
                //this.WriteMessage(2, (ex.InnerException == null ? ex.Message : ex.InnerException.Message));
            }
        }

        public string GetOTHours(string WorkNo, string OTDate, string BeginTime, string EndTime, string OTType)
        {
            double hours = 0;
            if (BeginTime != EndTime)
            {
                hours = bll.GetOtHours(WorkNo.ToUpper(), OTDate, BeginTime, EndTime, OTType);
            }
            return hours.ToString();
        }
        public bool CheckLeaveOverTime(string WorkNo, string StartDate, string StartTime, string EndDate, string EndTime)
        {
            string startTime = Convert.ToDateTime(StartDate + " " + StartTime).ToString("yyyy/MM/dd HH:mm");
            string endTime = Convert.ToDateTime(EndDate + " " + EndTime).ToString("yyyy/MM/dd HH:mm");
            string condition = "";
            condition = "SELECT WorkNo from GDS_ATT_LEAVEAPPLY a where a.WorkNo='" + WorkNo + "'  and a.EndDate>=to_date('" + StartDate + "','yyyy/mm/dd')  AND ((to_date(to_char(a.StartDate,'yyyy/mm/dd')||a.StartTime,'yyyy/mm/dd hh24:mi') <= to_date('" + startTime + "','yyyy/mm/dd hh24:mi') AND to_date(to_char(a.EndDate,'yyyy/mm/dd')||a.EndTime,'yyyy/mm/dd hh24:mi') > to_date('" + startTime + "','yyyy/mm/dd hh24:mi')) or (to_date(to_char(a.StartDate,'yyyy/mm/dd')||a.StartTime,'yyyy/mm/dd hh24:mi') < to_date('" + endTime + "','yyyy/mm/dd hh24:mi') AND to_date(to_char(a.EndDate,'yyyy/mm/dd')||a.EndTime,'yyyy/mm/dd hh24:mi') >= to_date('" + endTime + "','yyyy/mm/dd hh24:mi')) or (to_date(to_char(a.StartDate,'yyyy/mm/dd')||a.StartTime,'yyyy/mm/dd hh24:mi') >= to_date('" + startTime + "','yyyy/mm/dd hh24:mi') AND to_date(to_char(a.EndDate,'yyyy/mm/dd')||a.EndTime,'yyyy/mm/dd hh24:mi') <= to_date('" + endTime + "','yyyy/mm/dd hh24:mi')))";
            this.tempDataTable = bll.GetDataSetBySQL(condition).Tables["TempTable"];
            if (this.tempDataTable.Rows.Count > 0)
            {
                return false;
            }
            return true;
        }

        public bool CheckLeaveOverTime(string WorkNo, string StartDate, string StartTime, string EndDate, string EndTime, string BillNo)
        {
            string startTime = Convert.ToDateTime(StartDate + " " + StartTime).ToString("yyyy/MM/dd HH:mm");
            string endTime = Convert.ToDateTime(EndDate + " " + EndTime).ToString("yyyy/MM/dd HH:mm");
            string condition = "";
            condition = "SELECT WorkNo from GDS_ATT_LEAVEAPPLY a where a.WorkNo='" + WorkNo + "' and a.ID<>'" + BillNo + "' and a.EndDate>=to_date('" + StartDate + "','yyyy/mm/dd')  AND ((to_date(to_char(a.StartDate,'yyyy/mm/dd')||a.StartTime,'yyyy/mm/dd hh24:mi') <= to_date('" + startTime + "','yyyy/mm/dd hh24:mi') AND to_date(to_char(a.EndDate,'yyyy/mm/dd')||a.EndTime,'yyyy/mm/dd hh24:mi') > to_date('" + startTime + "','yyyy/mm/dd hh24:mi')) or (to_date(to_char(a.StartDate,'yyyy/mm/dd')||a.StartTime,'yyyy/mm/dd hh24:mi') < to_date('" + endTime + "','yyyy/mm/dd hh24:mi') AND to_date(to_char(a.EndDate,'yyyy/mm/dd')||a.EndTime,'yyyy/mm/dd hh24:mi') >= to_date('" + endTime + "','yyyy/mm/dd hh24:mi')) or (to_date(to_char(a.StartDate,'yyyy/mm/dd')||a.StartTime,'yyyy/mm/dd hh24:mi') >= to_date('" + startTime + "','yyyy/mm/dd hh24:mi') AND to_date(to_char(a.EndDate,'yyyy/mm/dd')||a.EndTime,'yyyy/mm/dd hh24:mi') <= to_date('" + endTime + "','yyyy/mm/dd hh24:mi')))";
            this.tempDataTable = bll.GetDataSetBySQL(condition).Tables["TempTable"];
            if (this.tempDataTable.Rows.Count > 0)
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
            if (isAdvance)
            {
                condition = " and a.WorkNo='" + WorkNo + "' and a.otdate>=to_date('" + OTDate + "','yyyy/mm/dd')-1  and ((a.BeginTime<=To_Date('" + Convert.ToDateTime(BeginTime).ToString("yyyy/MM/dd HH:mm") + "','yyyy/mm/dd hh24:mi')  and a.EndTime>To_Date('" + Convert.ToDateTime(BeginTime).ToString("yyyy/MM/dd HH:mm") + "','yyyy/mm/dd hh24:mi')) or (a.BeginTime<To_Date('" + Convert.ToDateTime(EndTime).ToString("yyyy/MM/dd HH:mm") + "','yyyy/mm/dd hh24:mi')  and a.EndTime>=To_Date('" + Convert.ToDateTime(EndTime).ToString("yyyy/MM/dd HH:mm") + "','yyyy/mm/dd hh24:mi')) or(a.BeginTime >= to_date('" + Convert.ToDateTime(BeginTime).ToString("yyyy/MM/dd HH:mm") + "','yyyy/mm/dd hh24:mi')  and a.EndTime <= to_date('" + Convert.ToDateTime(EndTime).ToString("yyyy/MM/dd HH:mm") + "','yyyy/mm/dd hh24:mi'))) ";
                this.tempDataTable = bll.GetDataByCondition_1(condition).Tables["OTM_AdvanceApply"];
                if (this.tempDataTable.Rows.Count > 0)
                {
                    return false;
                }
                return true;
            }
            condition = " and a.WorkNo='" + WorkNo + "' and a.otdate>=to_date('" + OTDate + "','yyyy/mm/dd')-1  and ((a.BeginTime<=To_Date('" + Convert.ToDateTime(BeginTime).ToString("yyyy/MM/dd HH:mm") + "','yyyy/mm/dd hh24:mi')  and a.EndTime>To_Date('" + Convert.ToDateTime(BeginTime).ToString("yyyy/MM/dd HH:mm") + "','yyyy/mm/dd hh24:mi')) or (a.BeginTime<To_Date('" + Convert.ToDateTime(EndTime).ToString("yyyy/MM/dd HH:mm") + "','yyyy/mm/dd hh24:mi')  and a.EndTime>=To_Date('" + Convert.ToDateTime(EndTime).ToString("yyyy/MM/dd HH:mm") + "','yyyy/mm/dd hh24:mi')) or(a.BeginTime >= to_date('" + Convert.ToDateTime(BeginTime).ToString("yyyy/MM/dd HH:mm") + "','yyyy/mm/dd hh24:mi')  and a.EndTime <= to_date('" + Convert.ToDateTime(EndTime).ToString("yyyy/MM/dd HH:mm") + "','yyyy/mm/dd hh24:mi'))) ";
            this.tempDataTable = bll.GetDataByCondition(condition).Tables["OTM_RealApply"];
            if (this.tempDataTable.Rows.Count > 0)
            {
                return false;
            }
            return true;
        }

        public bool CheckOverTime(string ID, string WorkNo, string OTDate, string BeginTime, string EndTime, string ShiftNo, bool isAdvance)
        {
            string AdvDt = "to_date('" + OTDate + "','yyyy/mm/dd')";
            string begTime = "to_date('" + Convert.ToDateTime(BeginTime).ToString("yyyy/MM/dd HH:mm") + "','yyyy/mm/dd hh24:mi')";
            string endTime = "to_date('" + Convert.ToDateTime(EndTime).ToString("yyyy/MM/dd HH:mm") + "','yyyy/mm/dd hh24:mi')";
            string condition = "";
            if (isAdvance)
            {
                condition = " and a.id<>'" + ID + "' and a.WorkNo='" + WorkNo + "' and a.otdate>=to_date('" + OTDate + "','yyyy/mm/dd')-1  and ((a.BeginTime<=To_Date('" + Convert.ToDateTime(BeginTime).ToString("yyyy/MM/dd HH:mm") + "','yyyy/mm/dd hh24:mi')  and a.EndTime>To_Date('" + Convert.ToDateTime(BeginTime).ToString("yyyy/MM/dd HH:mm") + "','yyyy/mm/dd hh24:mi')) or (a.BeginTime<To_Date('" + Convert.ToDateTime(EndTime).ToString("yyyy/MM/dd HH:mm") + "','yyyy/mm/dd hh24:mi')  and a.EndTime>=To_Date('" + Convert.ToDateTime(EndTime).ToString("yyyy/MM/dd HH:mm") + "','yyyy/mm/dd hh24:mi')) or(a.BeginTime >= to_date('" + Convert.ToDateTime(BeginTime).ToString("yyyy/MM/dd HH:mm") + "','yyyy/mm/dd hh24:mi')  and a.EndTime <= to_date('" + Convert.ToDateTime(EndTime).ToString("yyyy/MM/dd HH:mm") + "','yyyy/mm/dd hh24:mi'))) ";
                this.tempDataTable = bll.GetDataByCondition(condition).Tables["OTM_AdvanceApply"];
                if (this.tempDataTable.Rows.Count > 0)
                {
                    return false;
                }
                return true;
            }
            condition = " and a.id<>'" + ID + "' and  a.WorkNo='" + WorkNo + "' and a.otdate>=to_date('" + OTDate + "','yyyy/mm/dd')-1  and ((a.BeginTime<=To_Date('" + Convert.ToDateTime(BeginTime).ToString("yyyy/MM/dd HH:mm") + "','yyyy/mm/dd hh24:mi')  and a.EndTime>To_Date('" + Convert.ToDateTime(BeginTime).ToString("yyyy/MM/dd HH:mm") + "','yyyy/mm/dd hh24:mi')) or (a.BeginTime<To_Date('" + Convert.ToDateTime(EndTime).ToString("yyyy/MM/dd HH:mm") + "','yyyy/mm/dd hh24:mi')  and a.EndTime>=To_Date('" + Convert.ToDateTime(EndTime).ToString("yyyy/MM/dd HH:mm") + "','yyyy/mm/dd hh24:mi')) or(a.BeginTime >= to_date('" + Convert.ToDateTime(BeginTime).ToString("yyyy/MM/dd HH:mm") + "','yyyy/mm/dd hh24:mi')  and a.EndTime <= to_date('" + Convert.ToDateTime(EndTime).ToString("yyyy/MM/dd HH:mm") + "','yyyy/mm/dd hh24:mi'))) ";
            this.tempDataTable = bll.GetDataByCondition_1(condition).Tables["OTM_RealApply"];
            if (this.tempDataTable.Rows.Count > 0)
            {
                return false;
            }
            return true;
        }

        public bool CheckWorkTime(string WorkNo, string OTDate, string BeginTime, string EndTime, string ShiftNo)
        {
            string ShiftType = "";
            string sOffDutyTime = "";
            string OffDutyTime = "";
            string OtStatus = bll.GetOTType(WorkNo, Convert.ToDateTime(OTDate).ToString("yyyy/MM/dd"));
            this.tempDataTable = bll.GetDataSetBySQL("select a.ShiftType,a.OffDutyTime,nvl(a.PMRestETime,a.OffDutyTime) RestTime from GDS_ATT_WORKSHIFT a where ShiftNo='" + ShiftNo + "'").Tables["TempTable"];
            if (this.tempDataTable.Rows.Count > 0)
            {
                ShiftType = Convert.ToString(this.tempDataTable.Rows[0]["ShiftType"]);
                sOffDutyTime = Convert.ToString(this.tempDataTable.Rows[0]["OffDutyTime"]);
                OffDutyTime = Convert.ToString(this.tempDataTable.Rows[0]["RestTime"]);
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
                this.tempDataTable = bll.GetDataSetBySQL(tmpSql).Tables["TempTable"];
                if (this.tempDataTable.Rows.Count > 0)
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
                string strOTType = bll.GetOTType(WorkNo, Convert.ToDateTime(OTDate).AddDays(1.0).ToString("yyyy/MM/dd"));
                if (strOTType.Equals("G1") || strOTType.Equals("G2"))
                {
                    return false;
                }
            }
            return true;
        }

        public bool CheckOTOverETM(string WorkNo, string BeginTime, string EndTime)
        {
            string begTime = "to_date('" + Convert.ToDateTime(BeginTime).ToString("yyyy/MM/dd HH:mm") + "','yyyy/mm/dd hh24:mi')";
            string endTime = "to_date('" + Convert.ToDateTime(EndTime).ToString("yyyy/MM/dd HH:mm") + "','yyyy/mm/dd hh24:mi')";
            string condition = " SELECT b.workno from GDS_ATT_CURRICULA a,GDS_ATT_CURRICULAENTER b  WHERE a.cno=b.cno AND (a.Status='Open' or a.Status='Examined' or a.Status='Close')  and b.WorkNo='" + WorkNo + "' and a.cdate >to_date('" + Convert.ToDateTime(BeginTime).AddDays(-1.0).ToString("yyyy/MM/dd") + "','yyyy/mm/dd')  and ((to_date(to_char(a.cdate,'yyyy/MM/dd')||' '||a.starttime,'yyyy/MM/dd hh24:mi')<=" + begTime + " and to_date(to_char(a.cdate,'yyyy/MM/dd')||' '||a.endtime,'yyyy/MM/dd hh24:mi')>" + begTime + ") or (to_date(to_char(a.cdate,'yyyy/MM/dd')||' '||a.starttime,'yyyy/MM/dd hh24:mi')<" + endTime + " and to_date(to_char(a.cdate,'yyyy/MM/dd')||' '||a.endtime,'yyyy/MM/dd hh24:mi')>=" + endTime + ") or(to_date(to_char(a.cdate,'yyyy/MM/dd')||' '||a.starttime,'yyyy/MM/dd hh24:mi') >= " + begTime + " and to_date(to_char(a.cdate,'yyyy/MM/dd')||' '||a.endtime,'yyyy/MM/dd hh24:mi') <= " + endTime + ")) ";
            this.tempDataTable = bll.GetDataSetBySQL(condition).Tables["TempTable"];
            if (this.tempDataTable.Rows.Count > 0)
            {
                return false;
            }
            return true;
        }

        protected void btn_ExpA_Click(object sender, EventArgs e)
        {
            if (!this.PanelImport.Visible)
            {
                if (this.ViewState["condition"] == null)
                {
                    this.WriteMessage(1, (Message.NoDataExport));
                    return;
                }
                try
                {
                    this.dataSet = bll.GetDataByCondition_1(this.ViewState["condition"].ToString());
                    string WorkNo = "", OTDate = "";
                    if (this.dataSet.Tables["OTM_AdvanceApply"].Rows.Count <= 10000)
                    {
                        foreach (DataRow dr in this.dataSet.Tables["OTM_AdvanceApply"].Rows)
                        {
                            WorkNo = Convert.ToString(dr["WorkNo"]);
                            OTDate = Convert.ToDateTime(dr["OTDate"]).ToString("yyyy/MM/dd");
                            this.tempDataTable = bll.GetDataSetBySQL("select nvl(sum(Decode(OTType,'G1',ConfirmHours,0)),0)G1Total,nvl(sum(Decode(OTType,'G2',ConfirmHours,0)),0)G2Total,nvl(sum(Decode(OTType,'G3',ConfirmHours,0)),0)G3Total from GDS_ATT_REALAPPLY where WorkNo='" + WorkNo + "' and otdate>last_day(add_months(to_date('" + OTDate + "','yyyy/MM/dd'),-1)) and OTDate<=to_date('" + OTDate + "','yyyy/MM/dd') and status<'3'").Tables["TempTable"];
                            if (this.tempDataTable.Rows.Count > 0)
                            {
                                dr["G1Total"] = Convert.ToDouble(dr["G1Total"]) + Convert.ToDouble(this.tempDataTable.Rows[0]["G1Total"]);
                                dr["G2Total"] = Convert.ToDouble(dr["G2Total"]) + Convert.ToDouble(this.tempDataTable.Rows[0]["G2Total"]);
                                dr["G3Total"] = Convert.ToDouble(dr["G3Total"]) + Convert.ToDouble(this.tempDataTable.Rows[0]["G3Total"]);
                            }
                        }
                    }
                    this.tempDataTable = this.dataSet.Tables["OTM_AdvanceApply"];
                    HttpResponse res = Page.Response;
                    
                    res.Clear();
                    res.Buffer = true;
                    res.AppendHeader("Content-Disposition", "attachment;filename=OTMAdvanceApply.xls");
                    res.Charset = "UTF-8";
                    res.ContentEncoding = System.Text.Encoding.Default;
                    res.ContentType = "application/ms-excel";//設置輸出文件類型為excel文件。 
                    this.EnableViewState = false;

                    string colHeaders = "", ls_item = "";

                    for (int iLoop = 1; iLoop < this.UltraWebGrid.Columns.Count; iLoop++)
                    {
                        if (this.UltraWebGrid.Columns[iLoop].Hidden == false && this.UltraWebGrid.Columns[iLoop].Key != "")
                        {
                            colHeaders += this.UltraWebGrid.Columns[iLoop].Header.Caption + "\t";
                        }
                    }
                    res.Write(colHeaders);
                    for (int i = 0; i < this.tempDataTable.Rows.Count; i++)
                    {
                        ls_item = "\n";
                        for (int iLop = 1; iLop < this.UltraWebGrid.Columns.Count; iLop++)
                        {
                            if (!this.UltraWebGrid.Columns[iLop].Hidden && this.UltraWebGrid.Columns[iLop].Key!="")
                            {
                                if (this.UltraWebGrid.Columns[iLop].Key.ToLower() == "otdate")
                                {
                                    try
                                    {
                                        ls_item += string.Format("{0:" + dateFormat + "}", Convert.ToDateTime(this.tempDataTable.Rows[i][this.UltraWebGrid.Columns[iLop].Key])) + "\t";
                                    }
                                    catch
                                    {
                                        ls_item += "\t";
                                    }
                                }
                                else if (this.UltraWebGrid.Columns[iLop].Key.ToLower() == "begintime" ||
                                    this.UltraWebGrid.Columns[iLop].Key.ToLower() == "endtime")
                                {
                                    try
                                    {
                                        ls_item += string.Format("{0:HH:mm}", Convert.ToDateTime(this.tempDataTable.Rows[i][this.UltraWebGrid.Columns[iLop].Key])) + "\t";
                                    }
                                    catch
                                    {
                                        ls_item += "\t";
                                    }
                                }
                                else if (this.UltraWebGrid.Columns[iLop].Key.ToLower() == "approvedate" ||
                                    this.UltraWebGrid.Columns[iLop].Key.ToLower() == "update_date")
                                {
                                    try
                                    {
                                        ls_item += string.Format("{0:" + System.Convert.ToString(Session["datetimeFormat"]) + "}", Convert.ToDateTime(this.tempDataTable.Rows[i][this.UltraWebGrid.Columns[iLop].Key])) + "\t";
                                    }
                                    catch
                                    {
                                        ls_item += "\t";
                                    }
                                }
                                else
                                {
                                    ls_item += this.tempDataTable.Rows[i][this.UltraWebGrid.Columns[iLop].Key].ToString().Replace("\n", "").Replace("\r", "") + "\t";
                                }
                            }
                        }
                        ls_item = ls_item.Replace("\"", "");
                        res.Write(ls_item);
                        ls_item = "";
                    }
                    res.End();              
                }
                catch (Exception ex)
                {
                    this.WriteMessage(2, (ex.InnerException == null ? ex.Message : ex.InnerException.Message));
                }
            }
            else
            {
                try
                {
                    this.UltraWebGridExcelExporter.DownloadName = "OverTimeAdvanceApply.xls";
                    this.UltraWebGridExcelExporter.Export(this.UltraWebGridImport);
                }
                catch (Exception ex)
                {
                    this.WriteMessage(2, (ex.InnerException == null ? ex.Message : ex.InnerException.Message));
                }
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


        protected void ButtonAudit_Click(object sender, EventArgs e)
        {
            try
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
                        if (UltraWebGrid.Rows[i].Cells.FromKey("Status").Text.Trim() != "0")
                        {
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('" + Message.AuditUnaudit + "');</script>");
                            //this.WriteMessage(1, this.GetResouseValue("common.message.audit.unaudit"));
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
                        this.tempDataTable = bll.GetDataByCondition_1("and a.ID='" + this.UltraWebGrid.DisplayLayout.Rows[i].Cells.FromKey("id").Value + "'").Tables["OTM_AdvanceApply"];
                        if (this.tempDataTable.Rows.Count > 0)
                        {
                            bll.Audit(this.tempDataTable, CurrentUserInfo.Personcode,logmodel);
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
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('" + Message.common_message_successcount + ":" + intDeleteOk + "," + Message.common_message_errorcount + ":" + intDeleteError + "')</script>");
                    //this.WriteMessage(0, this.GetResouseValue("common.message.successcount") + "：" + intDeleteOk + ";" + this.GetResouseValue("common.message.errorcount") + "：" + intDeleteError);
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('" + Message.common_message_data_select + "');</script>");
                    //this.WriteMessage(1, this.GetResouseValue("common.message.data.select"));
                    return;
                }
                this.ProcessFlag.Value = "";
                this.Query(false, "Goto");

            }
            catch (System.Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('" + ex.Message + "');</script>");
               // this.WriteMessage(1, (ex.InnerException == null ? ex.Message : ex.InnerException.Message));
            }
        }

        protected void ButtonCancelAudit_Click(object sender, EventArgs e)
        {
            try
            {
                string sysKqoQinDays = bll.GetValue("select nvl(MAX(paravalue),'5') from GDS_SC_PARAMETER where paraname='KQMReGetKaoQin'");
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
                        if (UltraWebGrid.Rows[i].Cells.FromKey("Status").Text.Trim() != "2")
                        {
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('" + Message.UnCancelAudit + "');</script>");
                            //this.WriteMessage(1, this.GetResouseValue("common.message.audit.uncancelaudit"));
                            return;
                        }
                        if (sFromKQDate.CompareTo(System.DateTime.Now.ToString("yyyy/MM") + "/01") == -1 && strModifyDate.CompareTo(System.DateTime.Now.ToString("yyyy/MM/dd")) <= 0)
                        {
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('" + Message.kaoqindata_checkreget + "');</script>");
                            //this.WriteMessage(1, this.GetResouseValue("kqm.kaoqindata.checkreget") + "：" + strModifyDate);
                            return;
                        }

                        if (sFromKQDate.CompareTo(System.DateTime.Now.AddMonths(-1).ToString("yyyy/MM") + "/01") == -1)
                        {
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('" + Message.kaoqindata_checkreget + "');</script>");
                            //this.WriteMessage(1, this.GetResouseValue("kqm.kaoqindata.checkreget") + "：" + strModifyDate);
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
                        this.tempDataTable = bll.GetDataByCondition_1("and a.ID='" + this.UltraWebGrid.DisplayLayout.Rows[i].Cells.FromKey("id").Value + "' and INSTR(a.ImportFlag,'N')>0").Tables["OTM_AdvanceApply"];
                        if (this.tempDataTable.Rows.Count > 0)
                        {
                            bll.CancelAudit(this.tempDataTable,logmodel);
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
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('" + Message.common_message_successcount + ":" + intDeleteOk + "," + Message.common_message_errorcount + ":" + intDeleteError + "')</script>");
                    //this.WriteMessage(0, this.GetResouseValue("common.message.successcount") + "：" + intDeleteOk + ";" + this.GetResouseValue("common.message.errorcount") + "：" + intDeleteError);
                    this.Query(false, "Goto");
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('" + Message.common_message_data_select + "');</script>");
                   // this.WriteMessage(1, this.GetResouseValue("common.message.data.select"));
                    return;
                }
                this.ProcessFlag.Value = "";

            }
            catch (System.Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('" + ex.Message + "');</script>");
               // this.WriteMessage(1, (ex.InnerException == null ? ex.Message : ex.InnerException.Message));
            }
        }

        protected void ButtonSendAudit_Click(object sender, EventArgs e)
        {
            //SendAudit();
            SendOrgAudit();
        }



        private void SendOrgAudit()
        {
            #region  G2、G3修改名稱，送簽管控時間
            string LHZBIsDisplayG2G3 = "";
            //string LHZBIsLimitApply = "";
            try
            {
                LHZBIsDisplayG2G3 = System.Configuration.ConfigurationManager.AppSettings["LHZBIsDisplayG2G3"];
                // LHZBIsLimitApply = System.Configuration.ConfigurationManager.AppSettings["LHZBIsLimitAdvApplyTime"];
            }
            catch
            {
                LHZBIsDisplayG2G3 = "N";
                //  LHZBIsLimitApply = "";
            }
            #endregion
            try
            {
                int intSendOK = 0;
                int intSendBillNo = 0;
                int intSendError = 0;
                //string OTDate = "";
                string OrgCode = "", AuditOrgCode = "", BillNoType = "OTD";
                string OTType = "";
                TemplatedColumn tcol = (TemplatedColumn)UltraWebGrid.Bands[0].Columns[0];

                for (int i = 0; i < UltraWebGrid.Rows.Count; i++)
                {
                    CellItem GridItem = (CellItem)tcol.CellItems[i];
                    CheckBox chkIsHaveRight = (CheckBox)(GridItem.FindControl("CheckBoxCell"));
                    if (chkIsHaveRight.Checked)
                    {
                        if (UltraWebGrid.Rows[i].Cells.FromKey("Status").Text != "0")
                        {
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('" + Message.common_message_audit_unsendaudit + "');</script>");
                            // this.WriteMessage(1, this.GetResouseValue("common.message.audit.unsendaudit"));
                            return;
                        }

                        //add by xukai 20111015
                        if (!string.IsNullOrEmpty(LHZBLimitTime))
                        {
                            //每天LHZBLimitTime時間后不能新增、修改加班預報

                            DateTime dtLimit = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd") + " " + LHZBLimitTime);
                            if (dtLimit < DateTime.Now)
                            {
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('" + Message.kqm_otm_advanceapply + "" + LHZBLimitTime + "" + Message.kqm_otm_advanceapply_sendaudit + "');</script>");
                                //this.WriteMessage(1, this.GetResouseValue("kqm.otm.advanceapply") + LHZBLimitTime + this.GetResouseValue("kqm.otm.advanceapply.sendaudit"));
                                return;
                            }
                        }
                        //end

                    }
                }
                string BillTypeCode = "D001";
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
                        AuditOrgCode = bll.GetWorkFlowOrgCode(OrgCode, BillTypeCode, OTType);
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
                        AuditOrgCode = bll.GetWorkFlowOrgCode(OrgCode, BillTypeCode, OTType);
                        string key = AuditOrgCode + "^" + OTType;
                        if (!string.IsNullOrEmpty(AuditOrgCode))
                        {
                            if (dicy[key] != null)
                            {
                                dicy[key].Add(UltraWebGrid.Rows[i].Cells.FromKey("ID").Text.Trim());
                            }
                        }
                    }
                }
                int count = bll.SaveOrgAuditData("Add", dicy, BillNoType, BillTypeCode, CurrentUserInfo.Personcode,logmodel);
                intSendBillNo = count;
                intSendOK += 1;
               
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

        private void SendAudit()
        {
            #region  G2、G3修改名稱，送簽管控時間 
            string LHZBIsDisplayG2G3 = "";
            //string LHZBIsLimitApply = "";
            try
            {
                LHZBIsDisplayG2G3 = System.Configuration.ConfigurationManager.AppSettings["LHZBIsDisplayG2G3"];
                // LHZBIsLimitApply = System.Configuration.ConfigurationManager.AppSettings["LHZBIsLimitAdvApplyTime"];
            }
            catch
            {
                LHZBIsDisplayG2G3 = "N";
                //  LHZBIsLimitApply = "";
            }
            #endregion
            try
            {
                int intSendOK = 0;
                int intSendBillNo = 0;
                int intSendError = 0;
                //string OTDate = "";
                string OrgCode = "", BillNo = "", AuditOrgCode = "", BillNoType = "OTD";
                string OTType = "";                
                TemplatedColumn tcol = (TemplatedColumn)UltraWebGrid.Bands[0].Columns[0];

                for (int i = 0; i < UltraWebGrid.Rows.Count; i++)
                {
                    CellItem GridItem = (CellItem)tcol.CellItems[i];
                    CheckBox chkIsHaveRight = (CheckBox)(GridItem.FindControl("CheckBoxCell"));
                    if (chkIsHaveRight.Checked)
                    {
                        if (UltraWebGrid.Rows[i].Cells.FromKey("Status").Text != "0" &&
                            UltraWebGrid.Rows[i].Cells.FromKey("Status").Text != "3")
                        {
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('" + Message.common_message_audit_unsendaudit + "');</script>");
                           // this.WriteMessage(1, this.GetResouseValue("common.message.audit.unsendaudit"));
                            return;
                        }

                        //add by xukai 20111015
                        if (!string.IsNullOrEmpty(LHZBLimitTime))
                        {
                            //每天LHZBLimitTime時間后不能新增、修改加班預報

                            DateTime dtLimit = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd") + " " + LHZBLimitTime);
                            if (dtLimit < DateTime.Now)
                            {
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('" + Message.kqm_otm_advanceapply + "" + LHZBLimitTime + "" + Message.kqm_otm_advanceapply_sendaudit + "');</script>");
                                //this.WriteMessage(1, this.GetResouseValue("kqm.otm.advanceapply") + LHZBLimitTime + this.GetResouseValue("kqm.otm.advanceapply.sendaudit"));
                                return;
                            }
                        }
                        //end

                    }
                }
               string  BillTypeCode = "D001";
               
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
                        BillNo = "";
                        OrgCode = String.IsNullOrEmpty(this.HiddenOrgCode.Value) ? UltraWebGrid.Rows[i].Cells.FromKey("dCode").Text.Trim() : this.HiddenOrgCode.Value.Trim();

                        AuditOrgCode = bll.GetWorkFlowOrgCode(OrgCode, BillTypeCode, OTType);
                             
                        if (AuditOrgCode.Length > 0)
                        {

                            this.dataSet = bll.GetDataSetBySQL("select workNo,BILLNO from GDS_ATT_ADVANCEAPPLY where Status in('0','3') AND id='" + UltraWebGrid.Rows[i].Cells.FromKey("ID").Text.Trim() + "'");
                            if (this.dataSet.Tables["TempTable"].Rows.Count > 0)
                            {
                                string senduser = this.UltraWebGrid.Rows[i].Cells.FromKey("WorkNo").Text.Trim();
                                BillNo = this.dataSet.Tables["TempTable"].Rows[0]["BILLNO"].ToString();
                                 //if (!string.IsNullOrEmpty(BillNo))
                                 //{
                                 //    //BillNo = BillNoOrgCodeG1.GetByIndex(BillNoOrgCodeG1.IndexOfKey(AuditOrgCode)).ToString();
                                 //    //更新OTM_AdvanceApply表

                                 //    bll.SaveAuditData("Modify", UltraWebGrid.Rows[i].Cells.FromKey("ID").Text.Trim(), BillNo, AuditOrgCode, BillTypeCode, OTType, CurrentUserInfo.Personcode, senduser,logmodel);
                                 //    intSendOK += 1;
                                 //}
                                 //else
                                 //{
                                     //生產單號，更新OTM_AdvanceApply表

                                     BillNo = bll.SaveAuditData("Add", UltraWebGrid.Rows[i].Cells.FromKey("ID").Text.Trim(), BillNoType, AuditOrgCode, BillTypeCode, OTType, CurrentUserInfo.Personcode, senduser,logmodel);
                                     intSendBillNo += 1;
                                     intSendOK += 1;                                    
                                 //}
                                
                            }
                        }
                        else
                        {
                            //沒有定義簽核流程
                            intSendError += 1;
                        }
                    }
                }
                if (intSendOK + intSendError > 0)
                {
                    if (intSendError > 0)
                    {
                        this.WriteMessage(1, Message.SuccCount + intSendOK + ";" + Message.common_message_billcount + "：" + intSendBillNo + ";" + Message.FaileCount + intSendError + "(" + Message.common_message_noworkflow + ")");
                    }
                    else
                    {
                        this.WriteMessage(1, Message.SuccCount + intSendOK + ";" + Message.common_message_billcount + "：" + intSendBillNo);
                    }
                }
                else
                {
                    this.WriteMessage(1,Message.AtLastOneChoose);
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

        public void SendMail(string empno,string docno)
        { 
            string strHostPath = string.Format("{0}://{1}{2}",
                                    HttpContext.Current.Request.Url.Scheme,
                                    HttpContext.Current.Request.Headers["host"],
                                    HttpContext.Current.Request.ApplicationPath);


        }

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

        protected void ButtonOrgAudit_Click(object sender, EventArgs e)
        {
            SendOrgAudit();
        }


    }
}
