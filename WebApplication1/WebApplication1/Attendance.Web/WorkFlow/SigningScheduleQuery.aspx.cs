//using ASP;
using GDSBG.MiABU.Attendance.Web.ControlLib;
using GDSBG.MiABU.Attendance.Web.SystemManage;
using Infragistics.WebUI.UltraWebGrid;
using Infragistics.WebUI.UltraWebGrid.ExcelExport;
using Infragistics.WebUI.WebDataInput;
using System;
using System.Data;
using System.Drawing;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using UNLV.IAP.WebControls;
using GDSBG.MiABU.Attendance.BLL.WorkFlow;
using GDSBG.MiABU.Attendance.Model.WorkFlow;
using Resources;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;

namespace GDSBG.MiABU.Attendance.Web.WorkFlow
{
    public partial class SigningScheduleQuery : BasePage
    {
        Bll_SigningScheduleQuery bllSigningSchedule = new Bll_SigningScheduleQuery();
        DataTable tempDataTable = new DataTable();
        JavaScriptSerializer JsSerializer = new JavaScriptSerializer();
        Dictionary<string, string> ClientMessage = null;

        static SynclogModel logmodel = new SynclogModel();

        #region New Code

        private Mod_SigningScheduleQuery GetPageValue()
        {
            Mod_SigningScheduleQuery  pgMode = new Mod_SigningScheduleQuery();

            pgMode.DepCode = textBoxDepCode.Text.Trim();
            pgMode.DepName = textBoxDepName.Text.Trim();

            pgMode.BillTypeCode = this.ddlBillTypeCode.SelectedValuesToString(",");
            pgMode.BillNo = this.textBoxBillNo.Text.Trim();

            pgMode.Status =this.ddlStatus.SelectedValue;
            pgMode.ApplyDateFrom = this.textBoxApplyDateFrom.Text.Trim();

            pgMode.ApplyDateTo = this.textBoxApplyDateTo.Text.Trim();
            //pgMode.AuditMan =this.textBoxAuditMan.Text.Trim();
            pgMode.ApplyMan = this.textBoxApplyMan.Text.Trim();

            pgMode.sqlDep = base.SqlDep;

            return pgMode;
        }

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
        private void WriteMessage(int messageType, string sMessage)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "show", "alert('" + sMessage + "')", true);
        }

        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (ClientMessage == null)
                {
                    ClientMessage = new Dictionary<string, string>();
                    ClientMessage.Add("wfm_message_checkchangeauditflow", Message.wfm_message_checkchangeauditflow);
                    ClientMessage.Add("common_message_data_select", Message.common_message_data_select);
                    ClientMessage.Add("common_message_data_return", Message.common_message_data_return);
                    ClientMessage.Add("wfm_message_checksendnotes", Message.wfm_message_checksendnotes);
                    ClientMessage.Add("wfm_message_checkresendaudit", Message.wfm_message_checkresendaudit);
                    ClientMessage.Add("wfm_message_checkbatchdisaudit", Message.wfm_message_checkbatchdisaudit);
                    ClientMessage.Add("billno_copy_success", Message.billno_copy_success);
                    ClientMessage.Add("choose_defaultvalue", Message.choose_defaultvalue);

                    string clientmsg = JsSerializer.Serialize(ClientMessage);
                    Page.ClientScript.RegisterStartupScript(GetType(), "ClientMessage", "var Message=" + clientmsg + ";", true);
                }

                if (!IsPostBack)
                {
                    logmodel.ProcessOwner = CurrentUserInfo.Personcode;
                    logmodel.TransactionType = (Request.QueryString["ModuleCode"] == null) ? "" : Request.QueryString["ModuleCode"].ToString();
                    logmodel.LevelNo = "2";
                    logmodel.FromHost = Request.UserHostAddress;

                    this.tempDataTable = bllSigningSchedule.GetBillType();
                    this.ddlBillTypeCode.DataSource = this.tempDataTable;
                    this.ddlBillTypeCode.DataTextField = "BillTypeName";
                    this.ddlBillTypeCode.DataValueField = "BillTypeNo";
                    this.ddlBillTypeCode.DataBind();

                    this.tempDataTable =bllSigningSchedule.GetSigningScheduleStatus("WFMBillStatus");

                    this.ddlStatus.DataSource = tempDataTable;
                    this.ddlStatus.DataTextField = "DataValue";
                    this.ddlStatus.DataValueField = "DataCode";
                    this.ddlStatus.DataBind();

                    this.ddlStatus.Items.Insert(0, new ListItem("", ""));

                    this.textBoxApplyDateFrom.Text = DateTime.Now.AddDays((double)(-DateTime.Now.Day + 1)).ToString("yyyy/MM/dd");
                    this.textBoxApplyDateTo.Text = DateTime.Now.AddDays(2.0).ToString("yyyy/MM/dd");
                    this.ModuleCode.Value = base.Request["ModuleCode"].ToString();
                }
                SetCalendar(textBoxApplyDateFrom, textBoxApplyDateTo);
                SetSelector(ImageDepCode, textBoxDepCode, textBoxDepName, "dept", base.Request["ModuleCode"].ToString());
                PageHelper.ButtonControlsWF(FuncList, pnlShowPanel.Controls, base.FuncListModule);
            }
            catch (Exception ex)
            {
                WriteMessage(2, (ex.InnerException == null) ? ex.Message : ex.InnerException.Message);
            }
        }

        protected void ButtonBatchDisAudit_Click(object sender, EventArgs e)
         {
             if (this.UltraWebGridBill.Rows.Count != 0)
             {
                 try
                 {
                     int i;
                     CellItem GridItem;
                     CheckBox chkIsHaveRight;
                     int intDisAuditOk = 0;
                     TemplatedColumn tcol = (TemplatedColumn)this.UltraWebGridBill.Bands[0].Columns[0];
                     for (i = 0; i < this.UltraWebGridBill.Rows.Count; i++)
                     {
                         GridItem = (CellItem)tcol.CellItems[i];
                         chkIsHaveRight = (CheckBox)GridItem.FindControl("CheckBoxCell");
                         if (chkIsHaveRight.Checked && (this.UltraWebGridBill.Rows[i].Cells.FromKey("Status").Text.Trim() != "0"))
                         {
                             WriteMessage(1, Message.wfm_message_checkbatchdisaudit);
                             return;
                         }
                     }
                     string BillNo = "";
                     string BillTypeNo = "";
                     for (i = 0; i < this.UltraWebGridBill.Rows.Count; i++)
                     {
                         GridItem = (CellItem)tcol.CellItems[i];
                         chkIsHaveRight = (CheckBox)GridItem.FindControl("CheckBoxCell");
                         if (chkIsHaveRight.Checked)
                         {
                             BillNo = this.UltraWebGridBill.DisplayLayout.Rows[i].Cells.FromKey("BillNo").Text;
                             //BillTypeNo = BillNo.Substring(0, 3);
                             //BillTypeNo = ((ServiceLocator)this.Session["serviceLocator"]).GetFunctionData().GetBillTypeCode(BillTypeNo);
                             //((ServiceLocator)this.Session["serviceLocator"]).GetWFMBillData().SaveBatchDisAuditData(BillNo, this.Session["AppUser"].ToString(), BillTypeNo);
                             //intDisAuditOk++;
                             
                             BillTypeNo = this.UltraWebGridBill.DisplayLayout.Rows[i].Cells.FromKey("BILLTYPECODE").Text;
                             //BillTypeNo = bllSigningSchedule.GetBillTypeCode(BillTypeNo);
                             bllSigningSchedule.SaveBatchDisAuditData(BillNo, CurrentUserInfo.Personcode, BillTypeNo, logmodel);
                             intDisAuditOk++;
                             
                         }
                     }
                     if (intDisAuditOk > 0)
                     {
                         WriteMessage(1, Message.common_message_successcount + "：" + intDisAuditOk);
                         this.Query(false, "Goto");
                     }
                     else
                     {
                         WriteMessage(1, Message.common_message_data_select);
                     }
                 }
                 catch (Exception ex)
                 {
                     WriteMessage(1, (ex.InnerException == null) ? ex.Message : ex.InnerException.Message);
                 }
             }
        }

        protected void ButtonExport_Click(object sender, EventArgs e)
        {
            if (this.ViewState["condition"] == null)
            {
                WriteMessage(1, Message.common_message_nodataexport);
            }
            else
            {
                try
                {
                    DataTable dt_Result = bllSigningSchedule.GetSigningScheduleInfo((Mod_SigningScheduleQuery)this.ViewState["condition"]);
                    this.DataUIBind(dt_Result);
                    this.UltraWebGridExcelExporter.DownloadName = "WFMBill.xls";
                    this.UltraWebGridExcelExporter.Export(this.UltraWebGridBill);
                }
                catch (Exception ex)
                {
                    WriteMessage(2, (ex.InnerException == null) ? ex.Message : ex.InnerException.Message);
                }
            }
        }

        protected void ButtonQuery_Click(object sender, EventArgs e)
        {
            try
            {
                this.Query(true, "Goto");
                this.ProcessFlag.Value = "";
            }
            catch (Exception ex)
            {
                WriteMessage(2, (ex.InnerException == null) ? ex.Message : ex.InnerException.Message);
            }
        }

        protected void ButtonReSendAudit_Click(object sender, EventArgs e)
        {
            if (this.UltraWebGridBill.Rows.Count != 0)
            {
                try
                {
                    int i;
                    CellItem GridItem;
                    CheckBox chkIsHaveRight;
                    int intDisAuditOk = 0;
                    int intDisAuditError = 0;
                    bool bResult = false;
                    string BillTypeCode;
                    string AuditOrgCode;
                    string BillNo;
                    TemplatedColumn tcol = (TemplatedColumn) this.UltraWebGridBill.Bands[0].Columns[0];
                    for (i = 0; i < this.UltraWebGridBill.Rows.Count; i++)
                    {
                        GridItem = (CellItem) tcol.CellItems[i];
                        chkIsHaveRight = (CheckBox) GridItem.FindControl("CheckBoxCell");
                        if (chkIsHaveRight.Checked && (this.UltraWebGridBill.Rows[i].Cells.FromKey("Status").Text.Trim() != "2"))
                        {
                            WriteMessage(1, Message.wfm_message_checkresendaudit);
                            return;
                        }
                    }
                    string Flow_LevelRemark = Message.flow_levelremark;
                    for (i = 0; i < this.UltraWebGridBill.Rows.Count; i++)
                    {
                        GridItem = (CellItem) tcol.CellItems[i];
                        chkIsHaveRight = (CheckBox) GridItem.FindControl("CheckBoxCell");
                        if (chkIsHaveRight.Checked)
                        {
                            
                            BillNo = this.UltraWebGridBill.DisplayLayout.Rows[i].Cells.FromKey("BillNo").Text;
                            BillTypeCode = this.UltraWebGridBill.DisplayLayout.Rows[i].Cells.FromKey("BILLTYPECODE").Text;
                            AuditOrgCode=this.UltraWebGridBill.DisplayLayout.Rows[i].Cells.FromKey("OrgCode").Text;
                            //bResult = bllSigningSchedule.SaveReSendAuditData(BillNo, BillTypeCode,logmodel);
                              //public bool SaveReSendAuditData(string BillNo, string BillTypeCode, string WorkNo, string ApplyMan, string SendUser, string AuditOrgCode, string Flow_LevelRemark, SynclogModel logmodel)
                            bResult = bllSigningSchedule.SaveReSendAuditData(BillNo, BillTypeCode, CurrentUserInfo.Personcode, AuditOrgCode, Flow_LevelRemark, logmodel);

                            if (bResult)
                            {
                                intDisAuditOk++;
                            }
                            else
                            {
                                intDisAuditError++;
                            }
                        }
                    }
                    if ((intDisAuditOk + intDisAuditError) > 0)
                    {
                        if (intDisAuditError > 0)
                        {
                            WriteMessage(1, string.Concat(new object[] { Message.common_message_successcount, "：", intDisAuditOk, ";", Message.common_message_errorcount, "：", intDisAuditError }));
                        }
                        else
                        {
                            WriteMessage(1, Message.common_message_successcount + "：" + intDisAuditOk);
                        }
                    }
                    else
                    {
                        WriteMessage(1, Message.common_message_data_select);
                        return;
                    }
                    this.Query(false, "Goto");
                }
                catch (Exception ex)
                {
                    WriteMessage(1, (ex.InnerException == null) ? ex.Message : ex.InnerException.Message);
                }
            }
        }

        protected void ButtonReset_Click(object sender, EventArgs e)
        {
            this.textBoxDepCode.Text = "";
            this.textBoxDepName.Text = "";
            this.textBoxApplyMan.Text = "";
            this.textBoxBillNo.Text = "";
        }

        protected void ButtonSendNotes_Click(object sender, EventArgs e)
        {
            if (this.UltraWebGridBill.Rows.Count != 0)
            {
                try
                {
                    int i;
                    CellItem GridItem;
                    CheckBox chkIsHaveRight;
                    int intDisAuditOk = 0;
                    TemplatedColumn tcol = (TemplatedColumn) this.UltraWebGridBill.Bands[0].Columns[0];
                    for (i = 0; i < this.UltraWebGridBill.Rows.Count; i++)
                    {
                        GridItem = (CellItem) tcol.CellItems[i];
                        chkIsHaveRight = (CheckBox) GridItem.FindControl("CheckBoxCell");
                        if (chkIsHaveRight.Checked && (this.UltraWebGridBill.Rows[i].Cells.FromKey("Status").Text.Trim() != "0"))
                        {
                            WriteMessage(1, Message.wfm_message_checksendnotes);
                            return;
                        }
                    }
                    string BillNo = "";
                    for (i = 0; i < this.UltraWebGridBill.Rows.Count; i++)
                    {
                        GridItem = (CellItem) tcol.CellItems[i];
                        chkIsHaveRight = (CheckBox) GridItem.FindControl("CheckBoxCell");
                        if (chkIsHaveRight.Checked)
                        {
                            BillNo = this.UltraWebGridBill.DisplayLayout.Rows[i].Cells.FromKey("BillNo").Text;
                            bllSigningSchedule.SaveSendNotesData(BillNo, logmodel);
                            intDisAuditOk++;
                        }
                    }
                    if (intDisAuditOk > 0)
                    {
                        WriteMessage(1, Message.common_message_successcount + "：" + intDisAuditOk);
                        this.Query(false, "Goto");
                    }
                    else
                    {
                        WriteMessage(1, Message.common_message_data_select);
                    }
                }
                catch (Exception ex)
                {
                    WriteMessage(1, (ex.InnerException == null) ? ex.Message : ex.InnerException.Message);
                }
            }
        }

        private void DataUIBind(DataTable WFM_Bill)
        {
            if(WFM_Bill!=null)
            {
            this.UltraWebGridBill.DataSource = WFM_Bill;
            this.UltraWebGridBill.DataBind();
            if (WFM_Bill.Rows.Count > 0)
            {
                this.UltraWebGridBill.Rows[0].Selected = true;
                this.UltraWebGridBill.Rows[0].Activated = true;
            }
            }
        }

        private void Query(bool WindowOpen, string forwarderType)
        {
            Mod_SigningScheduleQuery AbMode = new Mod_SigningScheduleQuery();

            if (this.ProcessFlag.Value.ToLower().Equals("condition") || WindowOpen)
            {
                AbMode = GetPageValue();//取頁面的值

                this.ViewState.Add("condition", AbMode);
            }
            else
            {
                AbMode = (Mod_SigningScheduleQuery)this.ViewState["condition"];
            }
            int totalCount = 0;
            DataTable dt_result = bllSigningSchedule.GetSigningScheduleInfo(AbMode, pager.CurrentPageIndex, pager.PageSize, out  totalCount);
            this.DataUIBind(dt_result);
            pager.RecordCount = totalCount;
            pager.TextAfterPageIndexBox = "/" + pager.PageCount.ToString();
           // WriteMessage(0, Message.common_message_trans_complete);
        }


        protected void UltraWebGridBill_DataBound(object sender, EventArgs e)
        {
            string BillNo = "";
            string BillTypeCode = "";
            for (int i = 0; i < this.UltraWebGridBill.Rows.Count; i++)
            {
                BillTypeCode = (this.UltraWebGridBill.Rows[i].Cells.FromKey("BillTypeCode").Value == null) ? "" : this.UltraWebGridBill.Rows[i].Cells.FromKey("BillTypeCode").Value.ToString();
                BillNo = this.UltraWebGridBill.Rows[i].Cells.FromKey("BillNo").Value.ToString();
                this.UltraWebGridBill.Rows[i].Cells.FromKey("BillNo").TargetURL = "javascript:ShowBillDetail('" + this.UltraWebGridBill.Rows[i].Cells.FromKey("BillNo").Text + "','" + BillTypeCode + "')";
                if (this.UltraWebGridBill.Rows[i].Cells.FromKey("BillTypeCode").Text.Equals("OTMAdvanceApplyG3"))
                {
                    this.UltraWebGridBill.Rows[i].Cells.FromKey("BillTypeName").Style.ForeColor = Color.Red;
                }
                if (this.UltraWebGridBill.Rows[i].Cells.FromKey("Status").Text.Equals("1"))
                {
                    this.UltraWebGridBill.Rows[i].Cells.FromKey("StatusName").Style.ForeColor = Color.DarkGray;
                }
                if (this.UltraWebGridBill.Rows[i].Cells.FromKey("Status").Text.Equals("2"))
                {
                    this.UltraWebGridBill.Rows[i].Cells.FromKey("StatusName").Style.ForeColor = Color.Red;
                }
                if (this.UltraWebGridBill.Rows[i].Cells.FromKey("Status").Text.Equals("3"))
                {
                    this.UltraWebGridBill.Rows[i].Cells.FromKey("StatusName").Style.ForeColor = Color.SaddleBrown;
                }
                if (this.UltraWebGridBill.Rows[i].Cells.FromKey("Status").Text.Equals("0"))
                {
                    this.UltraWebGridBill.Rows[i].Cells.FromKey("StatusName").Style.ForeColor = Color.Green;
                }
                this.UltraWebGridBill.Rows[i].Cells.FromKey("OrgName").Value = bllSigningSchedule.GetAllAuditDept(this.UltraWebGridBill.Rows[i].Cells.FromKey("OrgCode").Value.ToString());
            }
        }

        protected void UltraWebGridExcelExporter_CellExported(object sender, CellExportedEventArgs e)
        {
            int iRdex = e.CurrentRowIndex;
            int iCdex = e.CurrentColumnIndex;
            if (iRdex != 0)
            {
                if ((((iCdex == 0) || (iCdex == 4)) || ((iCdex == 8) || (iCdex == 11))) && (e.CurrentWorksheet.Rows[iRdex].Cells[iCdex].Value != null))
                {
                    e.CurrentWorksheet.Columns[iCdex].Width = 0x1964;
                }
                e.CurrentWorksheet.Rows[iRdex].Height = 350;
            }
            else if (iRdex != 0)
            {
                e.CurrentWorksheet.Rows[iRdex].Height = 350;
            }
        }

        protected void UltraWebGridExcelExporter_HeaderCellExported(object sender, HeaderCellExportedEventArgs e)
        {
            int iRdex = e.CurrentRowIndex;
            int iCdex = e.CurrentColumnIndex;
            if (iRdex == 0)
            {
                e.CurrentWorksheet.Columns[iCdex].Width = 0xbb8;
                e.CurrentWorksheet.Rows[iRdex].Height = 500;
            }
        }

        protected DefaultProfile Profile
        {
            get
            {
                return (DefaultProfile) this.Context.Profile;
            }
        }
        #region 分頁
        protected void pager_PageChanged(object sender, EventArgs e)
        {
            this.Query(true, "Goto");
        }
        #endregion
    }
}
