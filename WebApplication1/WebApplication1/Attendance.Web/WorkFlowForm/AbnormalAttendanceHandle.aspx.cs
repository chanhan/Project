using System;
using System.Collections.Generic;
using System.Collections;
using System.Web;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using GDSBG.MiABU.Attendance.BLL.WorkFlow;
using GDSBG.MiABU.Attendance.Model.WorkFlow;
using Infragistics.WebUI.UltraWebGrid;
using Infragistics.WebUI.UltraWebGrid.ExcelExport;
using Infragistics.WebUI.WebDataInput;
using System.Drawing;
using System.Web.Profile;
using Resources;
using System.Web.Script.Serialization;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;

namespace GDSBG.MiABU.Attendance.Web.WorkFlowForm
{
    public partial class AbnormalAttendanceHandle : BasePage
    {
        Bll_AbnormalAttendanceHandle bll_abnormal = new Bll_AbnormalAttendanceHandle();
        DataTable tempDataTable = new DataTable();
        DataSet dataSet = new DataSet();
        DataSet tempDataSet = new DataSet();
        DataRow row;
        Mod_AbnormalAttendanceHandle mod_tempAbnormal = new Mod_AbnormalAttendanceHandle();
        JavaScriptSerializer JsSerializer = new JavaScriptSerializer();
        Dictionary<string, string> ClientMessage = null;

        static SynclogModel logmodel = new SynclogModel();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (ClientMessage == null)
            {
                ClientMessage = new Dictionary<string, string>();
                ClientMessage.Add("common_message_audit_unsendaudit", Message.common_message_audit_unsendaudit);

                ClientMessage.Add("common_message_sendaudit_noselect", Message.common_message_sendaudit_noselect);
                ClientMessage.Add("common_message_data_select", Message.common_message_data_select);
                ClientMessage.Add("common_message_data_return", Message.common_message_data_return);
                ClientMessage.Add("wfm_nosign_message", Message.wfm_nosign_message);

                string clientmsg = JsSerializer.Serialize(ClientMessage);
                Page.ClientScript.RegisterStartupScript(GetType(), "ClientMessage", "var Message=" + clientmsg + ";", true);
            }

            if (!IsPostBack)
            {
                logmodel.ProcessOwner = CurrentUserInfo.Personcode;
                logmodel.TransactionType = (Request.QueryString["ModuleCode"] == null) ? "" : Request.QueryString["ModuleCode"].ToString();
                logmodel.LevelNo = "2";
                logmodel.FromHost = Request.UserHostAddress;

                this.ButtonsReset("Condition");
                this.tempDataSet.Clear();
                this.textBoxKQDateFrom.Text = DateTime.Now.AddDays((double)(-DateTime.Now.Day + 1)).ToString("yyyy/MM/dd");
                this.textBoxKQDateTo.Text = DateTime.Now.AddDays(2.0).ToString("yyyy/MM/dd");
                this.ModuleCode.Value = base.Request.QueryString["ModuleCode"].ToString();

                this.ddlStatus.DataSource = bll_abnormal.GetAbnormalAttendanceHandleStatus("KqmKaoQinStatus");
                this.ddlStatus.DataTextField = "DataValue";
                this.ddlStatus.DataValueField = "DataCode";
                this.ddlStatus.DataBind();
                this.ddlStatus.Items.Insert(0, new ListItem("", ""));
                this.ddlStatus.SelectedIndex = this.ddlStatus.Items.IndexOf(this.ddlStatus.Items.FindByValue("1"));

                this.ddlExceptionType.DataSource = bll_abnormal.GetAbnormalAttendanceHandleStatus("ExceptionType");
                this.ddlExceptionType.DataTextField = "DataValue";
                this.ddlExceptionType.DataValueField = "DataCode";
                this.ddlExceptionType.DataBind();

                this.ddlShiftNo.DataSource = bll_abnormal.GetAbnormalAttendanceHandleStatus("KqmWorkShiftType");
                this.ddlShiftNo.DataTextField = "DataValue";
                this.ddlShiftNo.DataValueField = "DataCode";
                this.ddlShiftNo.DataBind();
                this.ddlShiftNo.Items.Insert(0, new ListItem("", ""));

                this.ButtonSave.Attributes.Add("onclick", "return confirm('" + Message.SaveConfim + "')");
                this.ButtonConfirm.Attributes.Add("onclick", "return confirm('" + Message.ConfirmConfirmData + "')");
                this.ButtonUnConfirm.Attributes.Add("onclick", "return confirm('" + Message.CancelAudit + "')");

                //Page.ClientScript.RegisterStartupScript(this.GetType(), "show", "alert('" + Message.DeleteSuccess + "')", true);
            }
            SetSelector(ImageDepCode, textBoxDepCode, textBoxDepName, "dept", base.Request["ModuleCode"].ToString());
            base.SetCalendar(textBoxKQDateFrom, textBoxKQDateTo);
            PageHelper.ButtonControlsWF(FuncList, pnlShowPanel.Controls, base.FuncListModule);
        }
        public void SetSelector(WebControl ctrlTrigger, Control ctrlCode, Control ctrlName, string flag, string moduleCode)//設置放大鏡頁面,用於輔助選擇
        {
            if (ctrlCode is TextBox) { (ctrlCode as TextBox).Attributes.Add("readonly", "readonly"); }
            if (ctrlName is TextBox) { (ctrlName as TextBox).Attributes.Add("readonly", "readonly"); }
            ctrlTrigger.Attributes.Add("onclick", string.Format("return setSelector('{0}','{1}','{2}','{3}')",
            ctrlCode.ClientID, ctrlName.ClientID, flag, moduleCode));
        }
        private Mod_AbnormalAttendanceHandle GetPageValue()//取頁面的值
        {
            Mod_AbnormalAttendanceHandle pgMode = new Mod_AbnormalAttendanceHandle();

            pgMode.DepCode = textBoxDepCode.Text.Trim();
            pgMode.DepName = textBoxDepName.Text.Trim();

            pgMode.IsFillCard = ddlIsMakeup.SelectedItem.Value;
            pgMode.IsSupporter = ddlIsSupporter.SelectedItem.Value;

            pgMode.EmployeeNo = textBoxEmployeeNo.Text.Trim();
            pgMode.EmpName = textBoxName.Text.Trim();
            pgMode.AttendanceDateFrom = textBoxKQDateFrom.Text.Trim();
            pgMode.AttendanceDateTo = textBoxKQDateTo.Text.Trim();
            pgMode.AttendHandleStatus = ddlStatus.SelectedItem.Value;
            pgMode.ExceptionType = ddlExceptionType.SelectedValuesToString(",");

            pgMode.ShiftNoType = ddlShiftNo.SelectedItem.Text;
            pgMode.ShiftNoCode = ddlShiftNo.SelectedItem.Value;
            pgMode.CheckBoxFlag = CheckBoxFlag.Checked ? "1" : "0";
            pgMode.sqlDep = base.SqlDep;
            return pgMode;
        }
        protected void ButtonReset_Click(object sender, EventArgs e)
        {
            this.textBoxDepCode.Text = "";
            this.textBoxDepName.Text = "";
            this.textBoxEmployeeNo.Text = "";
            this.textBoxKQDateFrom.Text = "";
            this.textBoxKQDateTo.Text = "";
            this.textBoxName.Text = "";
            this.ddlStatus.ClearSelection();
            this.ddlShiftNo.ClearSelection();
            this.ddlExceptionType.ClearSelection();
            this.ddlStatus.SelectedIndex = this.ddlStatus.Items.IndexOf(this.ddlStatus.Items.FindByValue("1"));

            //this.textBoxKQDateFrom.Text = DateTime.Now.AddDays((double)(-DateTime.Now.Day + 1)).ToString("yyyy/MM/dd");
            //this.textBoxKQDateTo.Text = DateTime.Now.ToString("yyyy/MM/dd");
        }
        protected void ButtonQuery_Click(object sender, EventArgs e)
        {
            try
            {
                //查詢時介面條件值管控
                if (this.CheckBoxFlag.Checked)
                {
                    if (this.textBoxKQDateFrom.Text.Trim().Length == 0)
                    {
                        WriteMessage(1, Message.bfw_bfw_kqm_KaoQinQuery_KQDate + Message.common_message_required);
                        return;
                    }
                }
                else if ((this.textBoxKQDateFrom.Text.Trim().Length == 0) || (this.textBoxKQDateTo.Text.Trim().Length == 0))
                {
                    WriteMessage(1, Message.bfw_bfw_kqm_KaoQinQuery_KQDate + Message.common_message_required);
                    return;
                }
                this.Query(true, "Goto");
                this.ProcessFlag.Value = "";
            }
            catch (Exception ex)
            {
                WriteMessage(2, (ex.InnerException == null) ? ex.Message : ex.InnerException.Message);
            }

        }
        private void WriteMessage(int messageType, string sMessage)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "show", "alert('" + sMessage + "')", true);
        }
        private void Query(bool WindowOpen, string forwarderType)
        {
            Mod_AbnormalAttendanceHandle AbMode = new Mod_AbnormalAttendanceHandle();

            if (this.ProcessFlag.Value.ToLower().Equals("condition") || WindowOpen)
            {
                if (this.CheckBoxFlag.Checked)
                {
                    if (((this.textBoxEmployeeNo.Text.Trim().Length == 0) && (this.textBoxName.Text.Trim().Length == 0)) && (this.textBoxKQDateFrom.Text.Trim().Length == 0))
                    {
                        WriteMessage(1, Message.common_message_datenotnull);
                        return;
                    }
                }

                AbMode = GetPageValue();//取頁面的值
                this.ViewState.Add("condition", AbMode);
            }
            else
            {
                AbMode = (Mod_AbnormalAttendanceHandle)this.ViewState["condition"];
            }
            int totalCount = 0;
            DataTable dt_result = bll_abnormal.GetAbnormalAttendanceInfo(AbMode, pager.CurrentPageIndex, pager.PageSize, out  totalCount);
            this.DataUIBind(dt_result);
            pager.RecordCount = totalCount;
            pager.TextAfterPageIndexBox = "/" + pager.PageCount.ToString();
        }
        private void DataUIBind(DataTable dt)
        {
            if (dt != null)
            {
                this.UltraWebGridKaoQinData.DataSource = dt.DefaultView;
                this.UltraWebGridKaoQinData.DataBind();
            }

        }
        protected void ButtonCancel_Click(object sender, EventArgs e)
        {
            this.Query(false, "Goto");
            //由前臺頁面設置是否可編輯
            this.UltraWebGridKaoQinData.DisplayLayout.AllowUpdateDefault = AllowUpdate.No;
            this.UltraWebGridKaoQinData.Bands[0].Columns.FromKey("ReasonRemark").AllowUpdate = AllowUpdate.No;
            this.UltraWebGridKaoQinData.DisplayLayout.CellClickActionDefault = CellClickAction.RowSelect;
            this.ProcessFlag.Value = "";
            this.ButtonsReset("Condition");
        }
        protected void ButtonConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                int i;
                CellItem GridItem;
                CheckBox chkIsHaveRight;
                //UltraWebGridKaoQinData.
                TemplatedColumn tcol = (TemplatedColumn)this.UltraWebGridKaoQinData.Bands[0].Columns[0];
                for (i = 0; i < this.UltraWebGridKaoQinData.Rows.Count; i++)
                {
                    GridItem = (CellItem)tcol.CellItems[i];
                    chkIsHaveRight = (CheckBox)GridItem.FindControl("CheckBoxCell");
                    if (chkIsHaveRight.Checked && (this.UltraWebGridKaoQinData.Rows[i].Cells.FromKey("Status").Text != "2"))
                    {
                        WriteMessage(2, Message.bfw_bfw_kqm_kaoqindata_checkconfirm);
                        return;
                    }
                }
                this.ProcessFlag.Value = "Confirm";
                int intConfirmOk = 0;
                int intConfirmError = 0;
                for (i = 0; i < this.UltraWebGridKaoQinData.Rows.Count; i++)
                {
                    GridItem = (CellItem)tcol.CellItems[i];
                    chkIsHaveRight = (CheckBox)GridItem.FindControl("CheckBoxCell");
                    if (chkIsHaveRight.Checked)
                    {
                        this.tempDataTable = bll_abnormal.GetAbnormalAttendanceInfo(this.UltraWebGridKaoQinData.Rows[i].Cells.FromKey("WorkNo").Text.Trim(), DateTime.Parse(this.UltraWebGridKaoQinData.Rows[i].Cells.FromKey("KQDate").Text.Trim()).ToString("yyyy/MM/dd"));
                        if (this.tempDataTable.Rows.Count > 0)
                        {
                            bll_abnormal.KQMSaveAbnormalAttendanceInfo(this.ProcessFlag.Value, this.tempDataTable, CurrentUserInfo.Personcode,logmodel);
                            intConfirmOk++;
                        }
                        else
                        {
                            intConfirmError++;
                        }
                    }
                }
                if ((intConfirmOk + intConfirmError) > 0)
                {
                    WriteMessage(0, string.Concat(new object[] { Message.common_message_successcount, "：", intConfirmOk, ";", Message.common_message_errorcount, "：", intConfirmError }));
                }
                else
                {
                    WriteMessage(1, Message.common_message_data_select);
                    return;
                }
                this.Query(false, "Goto");
                this.ProcessFlag.Value = "";
            }
            catch (Exception ex)
            {
                WriteMessage(2, (ex.InnerException == null) ? ex.Message : ex.InnerException.Message);
            }
        }
        protected void ButtonExport_Click(object sender, EventArgs e) //導出
        {
            Exception ex;
            if (!this.PanelImport.Visible)
            {
                if (this.ViewState["condition"] == null)
                {
                    WriteMessage(1, Message.common_message_nodataexport);
                }
                else
                {
                    try
                    {
                        //Mod_AbnormalAttendanceHandle AbMode = GetPageValue();//取頁面的值
                        this.tempDataTable = bll_abnormal.GetAbnormalAttendanceInfo((Mod_AbnormalAttendanceHandle)this.ViewState["condition"]);
                        this.UltraWebGridKaoQinData.Bands[0].Columns[0].Hidden = true;
                        foreach (DataRow dr in this.tempDataTable.Rows)
                        {
                            dr["AbsentTotal"] = bll_abnormal.GetExceptionqty(Convert.ToString(dr["WorkNo"]), Convert.ToDateTime(dr["KqDate"]).ToString("yyyy/MM/dd"));
                        }
                        HttpResponse res = this.Page.Response;
                        res.Clear();
                        res.Buffer = true;
                        res.AppendHeader("Content-Disposition", "attachment;filename=KaoQinData.xls");
                        res.Charset = "UTF-8";
                        res.ContentEncoding = Encoding.Default;
                        res.ContentType = "application/ms-excel";
                        this.EnableViewState = false;
                        string colHeaders = "";
                        string ls_item = "";
                        for (int iLoop = 0; iLoop < this.UltraWebGridKaoQinData.Columns.Count; iLoop++)
                        {
                            if (!this.UltraWebGridKaoQinData.Columns[iLoop].Hidden && !string.IsNullOrEmpty(this.UltraWebGridKaoQinData.Columns[iLoop].Key))
                            {
                                colHeaders = colHeaders + this.UltraWebGridKaoQinData.Columns[iLoop].Header.Caption + "\t";
                            }
                        }
                        res.Write(colHeaders);
                        for (int i = 0; i < this.tempDataTable.Rows.Count; i++)
                        {
                            ls_item = "\n";
                            for (int iLop = 0; iLop < this.UltraWebGridKaoQinData.Columns.Count; iLop++)
                            {
                                if (!this.UltraWebGridKaoQinData.Columns[iLop].Hidden && !string.IsNullOrEmpty(this.UltraWebGridKaoQinData.Columns[iLop].Key))
                                {
                                    if (this.UltraWebGridKaoQinData.Columns[iLop].Key.ToLower() == "kqdate")
                                    {
                                        try
                                        {
                                            ls_item = ls_item + string.Format("{0:" + "yyyy-MM-dd" + "}", Convert.ToDateTime(this.tempDataTable.Rows[i][this.UltraWebGridKaoQinData.Columns[iLop].Key])) + "\t";
                                            //ls_item = ls_item + string.Format("{0:}", Convert.ToDateTime(this.tempDataTable.Rows[i][this.UltraWebGridKaoQinData.Columns[iLop].Key])) + "\t";

                                        }
                                        catch
                                        {
                                            ls_item = ls_item + "\t";
                                        }
                                    }
                                    else
                                    {
                                        ls_item = ls_item + this.tempDataTable.Rows[i][this.UltraWebGridKaoQinData.Columns[iLop].Key].ToString() + "\t";
                                    }
                                }
                            }
                            //ls_item = ls_item.Replace("\n", "");
                            res.Write(ls_item);
                            ls_item = "";
                        }
                        res.End();

                        //HttpContext.Current.ApplicationInstance.CompleteRequest();
                    }
                    catch (Exception exception1)
                    {
                        ex = exception1;
                        WriteMessage(2, (ex.InnerException == null) ? ex.Message : ex.InnerException.Message);
                    }
                }
            }
            else
            {
                try
                {
                    this.UltraWebGridExcelExporter.DownloadName = "KaoQinDataError.xls";
                    this.UltraWebGridExcelExporter.Export(this.UltraWebGridImport);
                }
                catch (Exception exception2)
                {
                    ex = exception2;
                    WriteMessage(2, (ex.InnerException == null) ? ex.Message : ex.InnerException.Message);
                }
            }
        }
        protected void ButtonGoto_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                this.Query(false, "Goto");
            }
            catch (Exception ex)
            {
                WriteMessage(2, (ex.InnerException == null) ? ex.Message : ex.InnerException.Message);
            }
        }
        protected void ButtonImport_Click(object sender, EventArgs e)//導入
        {
            this.UltraWebGridImport.Rows.Clear();
            if (!this.PanelImport.Visible)
            {
                this.PanelImport.Visible = true;
                this.PanelData.Visible = false;
                this.ButtonQuery.Enabled = false;
                this.ButtonReset.Enabled = false;
                this.ButtonModify.Enabled = false;
                this.ButtonConfirm.Enabled = false;
                this.ButtonUnConfirm.Enabled = false;
                this.ButtonSendAudit.Enabled = false;
                this.ButtonImport.Text = Message.btnBack;
                this.labeluploadMsg.Text = "";
            }
            else
            {
                this.PanelImport.Visible = false;
                this.PanelData.Visible = true;
                this.ButtonQuery.Enabled = true;
                this.ButtonReset.Enabled = true;
                this.ButtonModify.Enabled = true;
                this.ButtonConfirm.Enabled = true;
                this.ButtonUnConfirm.Enabled = true;
                this.ButtonSendAudit.Enabled = true;
                this.ButtonImport.Text = Message.btnImport;
            }
        }
        protected void ButtonImportSave_Click(object sender, EventArgs e) //保存導入
        {
            this.ProcessFlag.Value = "Modify";
            this.ButtonImport.Text = Message.btnBack;
            try
            {
                string strFileName = this.FileUpload.FileName;
                string strFileSize = this.FileUpload.PostedFile.ContentLength.ToString();
                string strFileExt = strFileName.Substring(strFileName.LastIndexOf(".") + 1);
                string strTime = string.Format("{0:yyyyMMddHHmmssffff}", DateTime.Now);
                string strFilePath = base.Server.MapPath(@"..\") + @"ExportFileTemp\" + strTime + ".xls";
                if (strFileExt == "xls")
                {
                    this.FileUpload.SaveAs(strFilePath);
                }
                else
                {
                    WriteMessage(2, Message.common_message_uploadxls);
                    return;
                }
                this.dataSet = new DataSet();
                this.dataSet.Clear();
                this.dataSet.Tables.Add("HRM_Import");
                this.dataSet.Tables["HRM_Import"].Columns.Add(new DataColumn("ErrorMsg", typeof(string)));
                this.dataSet.Tables["HRM_Import"].Columns.Add(new DataColumn("WorkNo", typeof(string)));
                this.dataSet.Tables["HRM_Import"].Columns.Add(new DataColumn("KQDate", typeof(string)));
                this.dataSet.Tables["HRM_Import"].Columns.Add(new DataColumn("ReasonRemark", typeof(string)));
                string WorkNo = "";
                string KQDate = "";
                string ReasonRemark = "";
                string errorMsg = "";
                string sKQDate = "";
                string ShiftNo = "";
                int index = 0;
                int errorCount = 0;

                //讀取EXC文件返回DataView

                DataTable dt = bll_abnormal.ExceltoDataView(strFilePath);

                int inttotal = 0;
                if (dt != null && dt.Rows.Count > 0)
                {
                    inttotal = dt.Rows.Count;
                }

                //獲取考勤日

                string sysKqoQinDays = bll_abnormal.GetsysKqoQinDays(CurrentUserInfo.CompanyId, CurrentUserInfo.RoleCode);
                string sysKaoQinDataAbsent = bll_abnormal.GetSysKaoqinDataAbsent();
                DateTime DToDay = Convert.ToDateTime(DateTime.Now.ToString("yyyy/MM/dd"));
                for (int i = 0; i < inttotal; i++)
                {
                    WorkNo = dt.Rows[i][0].ToString().Trim().ToUpper();
                    KQDate = dt.Rows[i][1].ToString().Trim();
                    ReasonRemark = dt.Rows[i][2].ToString().Trim();
                    if (bll_abnormal.GetVWorkNoCount(WorkNo, base.SqlDep) == 0)
                    {
                        if (errorMsg.Length > 0)
                        {
                            errorMsg = errorMsg + ",";
                        }
                        errorMsg = errorMsg + Message.bfw_hrm_errorworkno;
                    }
                    try
                    {
                        sKQDate = Convert.ToDateTime(KQDate).ToString("yyyy/MM/dd");
                    }
                    catch (Exception)
                    {
                        sKQDate = KQDate;
                        if (errorMsg.Length > 0)
                        {
                            errorMsg = errorMsg + ",";
                        }
                        errorMsg = errorMsg + Message.common_message_data_errordate;
                    }
                    if (errorMsg.Length == 0)
                    {
                        this.tempDataTable = bll_abnormal.GetAbnormalAttendanceInfo(WorkNo, sKQDate, sysKqoQinDays, sysKaoQinDataAbsent);
                        if (this.tempDataTable!=null)
                        {
                            if (this.tempDataTable.Rows.Count > 0)
                            {
                                this.row = this.tempDataTable.Rows[0];
                                ShiftNo = Convert.ToString(this.row["ShiftNo"]);
                                if (((DToDay.CompareTo(Convert.ToDateTime(sKQDate)) == 0) && ShiftNo.StartsWith("C")) && (TimeSpan.Parse(DateTime.Now.ToString("HH:mm")) < TimeSpan.Parse("19:00")))
                                {
                                    errorMsg = Message.bfw_bfw_kqm_kaoqindata_errorrkaoqindata;
                                }
                                if (errorMsg.Length == 0)
                                {
                                    if ((this.row["Status"].ToString() == "1") || (this.row["Status"].ToString() == "2"))
                                    {
                                        this.row.BeginEdit();
                                        this.row["WorkNo"] = WorkNo;
                                        this.row["KQDate"] = sKQDate;
                                        this.row["ReasonRemark"] = ReasonRemark;
                                        if (ReasonRemark.Length > 0)
                                        {
                                            this.row["Status"] = "2";
                                        }
                                        else
                                        {
                                            this.row["Status"] = "1";
                                        }
                                        this.row.EndEdit();
                                        this.tempDataTable.AcceptChanges();
                                        bll_abnormal.KQMSaveAbnormalAttendanceInfo(this.ProcessFlag.Value, this.tempDataTable, CurrentUserInfo.Personcode, logmodel);

                                        index++;
                                    }
                                    else
                                    {
                                        errorMsg = Message.bfw_bfw_kqm_kaoqindata_errorrkaoqindata;
                                    }
                                }
                            }
                            else
                            {
                                errorMsg = Message.bfw_bfw_kqm_kaoqindata_errorrkaoqindata + sysKqoQinDays;
                            }
                        }
                        else
                        {
                            errorMsg = Message.bfw_bfw_kqm_kaoqindata_errorrkaoqindata + sysKqoQinDays;
                        }
                        this.tempDataTable.Clear();
                    }
                    if (errorMsg.Length > 0)
                    {
                        errorCount++;
                        this.dataSet.Tables["HRM_Import"].Rows.Add(new string[] { errorMsg, WorkNo, sKQDate, ReasonRemark });
                    }
                    errorMsg = "";
                }
                this.labeluploadMsg.Text = string.Concat(new object[] { Message.bfw_hrm_upsuccesscount, "：", index, "  ;", Message.bfw_hrm_upsfailcount, "：", errorCount, " ." });
                this.UltraWebGridImport.DataSource = this.dataSet.Tables["HRM_Import"].DefaultView;
                this.UltraWebGridImport.DataBind();
                this.ProcessFlag.Value = "";
            }
            catch (Exception ex)
            {
                WriteMessage(2, (ex.InnerException == null) ? ex.Message : ex.InnerException.Message);
            }
        }
        protected void ButtonModify_Click(object sender, EventArgs e)
        {
            if (this.UltraWebGridKaoQinData.Rows.Count == 0)
            {
                WriteMessage(1, Message.common_message_data_select);
            }
            else
            {
                this.ButtonsReset("Modify");
                this.ProcessFlag.Value = "Modify";
                this.UltraWebGridKaoQinData.DisplayLayout.AllowUpdateDefault = AllowUpdate.Yes;
                this.UltraWebGridKaoQinData.DisplayLayout.CellClickActionDefault = CellClickAction.Edit;
                string ExceptionType;

                //獲取考勤日
                int sysKqoQinDays = Convert.ToInt32(bll_abnormal.GetsysKqoQinDays(CurrentUserInfo.CompanyId, CurrentUserInfo.RoleCode.ToLower()));
                string sysKaoQinDataAbsent = bll_abnormal.GetSysKaoqinDataAbsent();
                DateTime DToDay = Convert.ToDateTime(DateTime.Now.ToString("yyyy/MM/dd"));
                string ShiftNo = "";
                for (int i = 0; i < this.UltraWebGridKaoQinData.Rows.Count; i++)
                {
                    ExceptionType = (this.UltraWebGridKaoQinData.Rows[i].Cells.FromKey("ExceptionType").Value == null) ? "" : this.UltraWebGridKaoQinData.Rows[i].Cells.FromKey("ExceptionType").Value.ToString();
                    TimeSpan KaoQinDays = (TimeSpan)(DateTime.Now - Convert.ToDateTime(this.UltraWebGridKaoQinData.Rows[i].Cells.FromKey("KQDate").Text));
                    if (sysKaoQinDataAbsent.Equals("N"))
                    {
                        if ((((this.UltraWebGridKaoQinData.Rows[i].Cells.FromKey("Status").Text == "1") || (this.UltraWebGridKaoQinData.Rows[i].Cells.FromKey("Status").Text == "2")) && (ExceptionType.Equals("A") || ExceptionType.Equals("B"))) && (KaoQinDays.TotalDays <= sysKqoQinDays))
                        {
                            ShiftNo = (this.UltraWebGridKaoQinData.Rows[i].Cells.FromKey("ShiftNo").Value == null) ? "" : this.UltraWebGridKaoQinData.Rows[i].Cells.FromKey("ShiftNo").Text;
                            if (((DToDay.CompareTo(Convert.ToDateTime(this.UltraWebGridKaoQinData.Rows[i].Cells.FromKey("KQDate").Text)) == 0) && ShiftNo.StartsWith("C")) && (TimeSpan.Parse(DateTime.Now.ToString("HH:mm")) < TimeSpan.Parse("19:00")))
                            {
                                this.UltraWebGridKaoQinData.Rows[i].Cells.FromKey("ReasonRemark").AllowEditing = AllowEditing.No;
                            }
                            else
                            {
                                this.UltraWebGridKaoQinData.Rows[i].Cells.FromKey("ReasonRemark").AllowEditing = AllowEditing.Yes;
                                this.UltraWebGridKaoQinData.Rows[i].Cells.FromKey("ReasonRemark").Style.BackColor = Color.AliceBlue;
                            }
                        }
                        else
                        {
                            this.UltraWebGridKaoQinData.Rows[i].Cells.FromKey("ReasonRemark").AllowEditing = AllowEditing.No;
                        }
                    }
                    else if ((((this.UltraWebGridKaoQinData.Rows[i].Cells.FromKey("Status").Text == "1") || (this.UltraWebGridKaoQinData.Rows[i].Cells.FromKey("Status").Text == "2")) && ((ExceptionType.Equals("A") || ExceptionType.Equals("B")) || (ExceptionType.Equals("C") || ExceptionType.Equals("D")))) && (KaoQinDays.TotalDays <= sysKqoQinDays))
                    {
                        ShiftNo = (this.UltraWebGridKaoQinData.Rows[i].Cells.FromKey("ShiftNo").Value == null) ? "" : this.UltraWebGridKaoQinData.Rows[i].Cells.FromKey("ShiftNo").Text;
                        if (((DToDay.CompareTo(Convert.ToDateTime(this.UltraWebGridKaoQinData.Rows[i].Cells.FromKey("KQDate").Text)) == 0) && ShiftNo.StartsWith("C")) && (TimeSpan.Parse(DateTime.Now.ToString("HH:mm")) < TimeSpan.Parse("19:00")))
                        {
                            this.UltraWebGridKaoQinData.Rows[i].Cells.FromKey("ReasonRemark").AllowEditing = AllowEditing.No;
                        }
                        else
                        {
                            this.UltraWebGridKaoQinData.Rows[i].Cells.FromKey("ReasonRemark").AllowEditing = AllowEditing.Yes;
                            this.UltraWebGridKaoQinData.Rows[i].Cells.FromKey("ReasonRemark").Style.BackColor = Color.AliceBlue;
                        }
                    }
                    else
                    {
                        this.UltraWebGridKaoQinData.Rows[i].Cells.FromKey("ReasonRemark").AllowEditing = AllowEditing.No;
                    }
                }
            }
        }
        protected void ButtonNext_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                this.Query(false, "Next");
            }
            catch (Exception ex)
            {
                WriteMessage(2, (ex.InnerException == null) ? ex.Message : ex.InnerException.Message);
            }
        }
        protected void ButtonPrevious_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                this.Query(false, "Previous");
            }
            catch (Exception ex)
            {
                WriteMessage(2, (ex.InnerException == null) ? ex.Message : ex.InnerException.Message);
            }
        }
        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            this.ButtonsReset("Condition");
            this.UltraWebGridKaoQinData.DisplayLayout.AllowUpdateDefault = AllowUpdate.No;
            this.UltraWebGridKaoQinData.DisplayLayout.CellClickActionDefault = CellClickAction.RowSelect;
            this.ProcessFlag.Value = "";
            this.Query(false, "Goto");
        }
        protected void ButtonSendAudit_Click(object sender, EventArgs e)
        {
            try
            {
                int i;
                CellItem GridItem;
                CheckBox chkIsHaveRight;
                int intSendOK = 0;
                int intSendBillNo = 0;
                int intSendError = 0;
                string OrgCode = "";
                //string BillNo = "";
                string AuditOrgCode = "";
                string BillTypeCode = "KQMException";//單據類型

                string BillNoType = "KQE";//單據前綴
                string ExceptionType = "";
                SortedList BillNoOrgCode = new SortedList();
                TemplatedColumn tcol = (TemplatedColumn)this.UltraWebGridKaoQinData.Bands[0].Columns[0];
                string sFlow_LevelRemark = Message.flow_levelremark;

                bool bResult=false;
                int iCountNum = 0;

                for (i = 0; i < this.UltraWebGridKaoQinData.Rows.Count; i++)
                {
                    GridItem = (CellItem)tcol.CellItems[i];
                    chkIsHaveRight = (CheckBox)GridItem.FindControl("CheckBoxCell");
                    if (chkIsHaveRight.Checked)
                    {
                        ExceptionType = (this.UltraWebGridKaoQinData.Rows[i].Cells.FromKey("ExceptionType").Value == null) ? "" : this.UltraWebGridKaoQinData.Rows[i].Cells.FromKey("ExceptionType").Value.ToString();
                        if ((this.UltraWebGridKaoQinData.Rows[i].Cells.FromKey("Status").Text != "2") || (!ExceptionType.Equals("A") && !ExceptionType.Equals("B")))
                        {
                            WriteMessage(1, Message.common_message_audit_unsendaudit);
                            return;
                        }
                        iCountNum += 1;
                    }
                }
                if (iCountNum > 1)
                {
                    SendOrgAudit();
                }
                else
                {
                    for (i = 0; i < this.UltraWebGridKaoQinData.Rows.Count; i++)
                    {
                        GridItem = (CellItem)tcol.CellItems[i];
                        chkIsHaveRight = (CheckBox)GridItem.FindControl("CheckBoxCell");
                        if (chkIsHaveRight.Checked)
                        {
                            //BillNo = "";
                            OrgCode = string.IsNullOrEmpty(this.HiddenOrgCode.Value) ? this.UltraWebGridKaoQinData.Rows[i].Cells.FromKey("dCode").Text.Trim() : this.HiddenOrgCode.Value.Trim();
                            AuditOrgCode = bll_abnormal.GetWorkFlowOrgCode(OrgCode, BillTypeCode, "reason1");

                            if (AuditOrgCode.Length > 0)
                            {
                                //if (BillNoOrgCode.IndexOfKey(AuditOrgCode) >= 0)
                                //{
                                //    BillNo = BillNoOrgCode.GetByIndex(BillNoOrgCode.IndexOfKey(AuditOrgCode)).ToString();
                                //    bll_abnormal.KQMSaveAuditData("Modify", this.UltraWebGridKaoQinData.Rows[i].Cells.FromKey("WorkNo").Text.Trim(), this.UltraWebGridKaoQinData.Rows[i].Cells.FromKey("KQDate").Text.Trim(), BillNo, AuditOrgCode, logmodel);
                                //    intSendOK++;
                                //}
                                //else
                                //{

                                //WorkNo,  KQDate,  BillNoType,  BillTypeCode,  ApplyMan,  SendUser,  AuditOrgCode,  Flow_LevelRemark,  logmodel
                                string WorkNo = this.UltraWebGridKaoQinData.Rows[i].Cells.FromKey("WorkNo").Text.Trim();
                                string KQDate = this.UltraWebGridKaoQinData.Rows[i].Cells.FromKey("KQDate").Text.Trim();
                                bResult = bll_abnormal.KQMSaveAuditData(WorkNo, KQDate, BillNoType, BillTypeCode, CurrentUserInfo.Personcode, AuditOrgCode, sFlow_LevelRemark, logmodel);

                                //bll_abnormal.WFMSaveData(BillNo, AuditOrgCode, CurrentUserInfo.Personcode, logmodel);
                                //bll_abnormal.WFMSaveAuditStatusData(BillNo, AuditOrgCode, BillTypeCode,senduser,sFlow_LevelRemark, logmodel);
                                if (bResult)
                                {
                                    intSendBillNo++;
                                    intSendOK++;
                                }
                                //BillNoOrgCode.Add(AuditOrgCode, BillNo);
                                //}
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
                            WriteMessage(1, string.Concat(new object[] { Message.common_message_successcount, "：", intSendOK, ";", Message.common_message_billcount, "：", intSendBillNo, ";", Message.common_message_errorcount, "：", intSendError, "(", Message.common_message_noworkflow, ")" }));
                        }
                        else
                        {
                            WriteMessage(1, string.Concat(new object[] { Message.common_message_successcount, "：", intSendOK, ";", Message.common_message_billcount, "：", intSendBillNo }));
                        }
                    }
                    else
                    {
                        WriteMessage(1, Message.common_message_data_select);
                        return;
                    }
                    this.Query(false, "Goto");
                    this.ProcessFlag.Value = "";
                    this.HiddenOrgCode.Value= "";
                    int s = this.textBoxDepCode.Text.Trim().Length;
                    
                }
            }
            catch (Exception ex)
            {
                WriteMessage(2, (ex.InnerException == null) ? ex.Message : ex.InnerException.Message);
            }
        }
        private   void ButtonsReset(string buttonText)
        {
            this.ButtonQuery.Enabled = true;
            this.ButtonImport.Enabled = true;
            this.ButtonExport.Enabled = true;
            this.ButtonReset.Enabled = true;
            this.ButtonModify.Enabled = true;
            this.ButtonCancel.Enabled = true;
            this.ButtonSave.Enabled = true;
            this.ButtonConfirm.Enabled = true;
            this.ButtonUnConfirm.Enabled = true;
            this.ButtonSendAudit.Enabled = true;
            //this.ButtonOrgAudit.Enabled = true;

            if (buttonText != null)
            {
                if (!(buttonText == "Condition"))
                {
                    if (((buttonText == "Query") || (buttonText == "Cancel")) || (buttonText == "Save"))
                    {
                        this.ButtonCancel.Enabled = false;
                        this.ButtonSave.Enabled = false;
                    }
                    else if (buttonText == "Modify")
                    {
                        this.ButtonQuery.Enabled = false;
                        this.ButtonImport.Enabled = false;
                        this.ButtonExport.Enabled = false;
                        this.ButtonReset.Enabled = false;
                        this.ButtonModify.Enabled = false;
                        this.ButtonSave.Enabled = true;
                        this.ButtonCancel.Enabled = true;
                        this.ButtonConfirm.Enabled = false;
                        this.ButtonUnConfirm.Enabled = false;
                        this.ButtonSendAudit.Enabled = false;
                        //this.ButtonOrgAudit.Enabled = false;
                    }
                }
                else
                {
                    this.ButtonCancel.Enabled = false;
                    this.ButtonSave.Enabled = false;
                }
            }
        }
        protected void ButtonUnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                int i;
                CellItem GridItem;
                CheckBox chkIsHaveRight;
                TemplatedColumn tcol = (TemplatedColumn)this.UltraWebGridKaoQinData.Bands[0].Columns[0];
                for (i = 0; i < this.UltraWebGridKaoQinData.Rows.Count; i++)
                {
                    GridItem = (CellItem)tcol.CellItems[i];
                    chkIsHaveRight = (CheckBox)GridItem.FindControl("CheckBoxCell");
                    if (chkIsHaveRight.Checked && (this.UltraWebGridKaoQinData.Rows[i].Cells.FromKey("Status").Text != "3"))
                    {
                        WriteMessage(2, Message.bfw_bfw_kqm_kaoqindata_checkunconfirm);
                        return;
                    }
                }
                this.ProcessFlag.Value = "UnConfirm";
                int intUnConfirmOk = 0;
                int intUnConfirmError = 0;
                for (i = 0; i < this.UltraWebGridKaoQinData.Rows.Count; i++)
                {
                    GridItem = (CellItem)tcol.CellItems[i];
                    chkIsHaveRight = (CheckBox)GridItem.FindControl("CheckBoxCell");
                    if (chkIsHaveRight.Checked)
                    {
                        this.tempDataTable = bll_abnormal.GetAbnormalAttendanceInfo(this.UltraWebGridKaoQinData.Rows[i].Cells.FromKey("WorkNo").Text.Trim(), DateTime.Parse(this.UltraWebGridKaoQinData.Rows[i].Cells.FromKey("KQDate").Text.Trim()).ToString("yyyy/MM/dd"));
                        if (this.tempDataTable.Rows.Count > 0)
                        {
                            bll_abnormal.KQMSaveAbnormalAttendanceInfo(this.ProcessFlag.Value, this.tempDataTable, CurrentUserInfo.Personcode, logmodel);
                            intUnConfirmOk++;
                        }
                        else
                        {
                            intUnConfirmError++;
                        }
                    }
                }
                if ((intUnConfirmOk + intUnConfirmError) > 0)
                {
                    WriteMessage(0, string.Concat(new object[] { Message.common_message_successcount, "：", intUnConfirmOk, ";", Message.common_message_errorcount, "：", intUnConfirmError }));
                }
                else
                {
                    WriteMessage(1, Message.common_message_data_select);
                    return;
                }
                this.Query(false, "Goto");
                this.ProcessFlag.Value = "";
            }
            catch (Exception ex)
            {
                WriteMessage(2, (ex.InnerException == null) ? ex.Message : ex.InnerException.Message);
            }
        }
        protected void SetPageInfor(int currentPage, int totalPage, int totalRecodrs)
        {
            //((WebNumericEdit) this.PageNavigator.FindControl("WebNumericEditCurrentpage")).Text = Convert.ToString(currentPage);
            //((Label) this.PageNavigator.FindControl("LabelTotalpage")).Text = Convert.ToString(totalPage);
            //((Label) this.PageNavigator.FindControl("LabelTotalrecords")).Text = Convert.ToString(totalRecodrs);
        }
        protected void UltraWebGridExcelExporter_CellExported(object sender, CellExportedEventArgs e)
        {
            int iRdex = e.CurrentRowIndex;
            int iCdex = e.CurrentColumnIndex;
            if (!this.PanelImport.Visible)
            {
                if (iRdex != 0)
                {
                    if ((e.GridColumn.Key.ToLower() == "kqdate") && (e.CurrentWorksheet.Rows[iRdex].Cells[iCdex].Value != null))
                    {
                        //e.CurrentWorksheet.Rows[iRdex].Cells[iCdex].Value = string.Format("{0:" + base.dateFormat + "}", Convert.ToDateTime(e.CurrentWorksheet.Rows[iRdex].Cells[iCdex].Value));
                        e.CurrentWorksheet.Rows[iRdex].Cells[iCdex].Value = string.Format("{0:" + "yyyy-MM-dd" + "}", Convert.ToDateTime(e.CurrentWorksheet.Rows[iRdex].Cells[iCdex].Value));

                    }
                    e.CurrentWorksheet.Rows[iRdex].Height = 350;
                }
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
            if (!this.PanelImport.Visible)
            {
                if (iRdex == 0)
                {
                    e.CurrentWorksheet.Columns[iCdex].Width = 0xbb8;
                    e.CurrentWorksheet.Rows[iRdex].Height = 350;
                }
            }
            else if (iRdex == 0)
            {
                e.CurrentWorksheet.Columns[iCdex].Width = 0xbb8;
                e.CurrentWorksheet.Rows[iRdex].Height = 350;
            }
        }
        protected void UltraWebGridKaoQinData_DataBound(object sender, EventArgs e)
        {
            string WorkNo = "";
            string KqDate = "";
            this.UltraWebGridKaoQinData.DisplayLayout.Bands[0].Columns.FromKey("KQDate").Type = ColumnType.HyperLink;
            for (int i = 0; i < this.UltraWebGridKaoQinData.Rows.Count; i++)
            {
                this.UltraWebGridKaoQinData.Rows[i].Cells.FromKey("KQDate").TargetURL = "javascript:ShowDetail('" + this.UltraWebGridKaoQinData.Rows[i].Cells.FromKey("WorkNo").Text + "','" + Convert.ToDateTime(this.UltraWebGridKaoQinData.Rows[i].Cells.FromKey("KQDate").Text).ToString("yyyy-MM-dd") + "','" + this.UltraWebGridKaoQinData.Rows[i].Cells.FromKey("ShiftNo").Text + "')";
                WorkNo = (this.UltraWebGridKaoQinData.Rows[i].Cells.FromKey("WorkNo").Value == null) ? "" : this.UltraWebGridKaoQinData.Rows[i].Cells.FromKey("WorkNo").Text;
                KqDate = (this.UltraWebGridKaoQinData.Rows[i].Cells.FromKey("KQDate").Value == null) ? "" : Convert.ToDateTime(this.UltraWebGridKaoQinData.Rows[i].Cells.FromKey("KQDate").Text).ToString("yyyy/MM/dd");
                this.UltraWebGridKaoQinData.Rows[i].Cells.FromKey("AbsentTotal").Value = bll_abnormal.GetExceptionqty(WorkNo, KqDate);

            }
        }
        protected void UltraWebGridKaoQinData_UpdateRowBatch(object sender, RowEventArgs e)
        {
            string ReasonRemark = "";
            string ExceptionType = "";
            if (this.ProcessFlag.Value == "Modify")
            {
                ExceptionType = (e.Row.Cells.FromKey("ExceptionType").Value == null) ? "" : e.Row.Cells.FromKey("ExceptionType").Value.ToString();
                if ((((ExceptionType.EndsWith("A") || ExceptionType.EndsWith("B")) || ExceptionType.EndsWith("C")) || ExceptionType.EndsWith("D")) && (e.Row.DataChanged == DataChanged.Modified))
                {
                    ReasonRemark = (e.Row.Cells.FromKey("ReasonRemark").Value == null) ? "" : e.Row.Cells.FromKey("ReasonRemark").Value.ToString();

                    this.tempDataTable = bll_abnormal.GetAbnormalAttendanceInfo(e.Row.Cells.FromKey("WorkNo").Value.ToString(), DateTime.Parse(e.Row.Cells.FromKey("KQDate").Value.ToString()).ToString("yyyy/MM/dd"));
                    if (this.tempDataTable.Rows.Count > 0)
                    {
                        this.row = this.tempDataTable.Rows[0];
                        this.row.BeginEdit();
                        this.row["WorkNo"] = e.Row.Cells.FromKey("WorkNo").Value.ToString();
                        this.row["KQDate"] = e.Row.Cells.FromKey("KQDate").Value.ToString();
                        this.row["ReasonRemark"] = ReasonRemark;
                        if (ReasonRemark.Length > 0)
                        {
                            this.row["Status"] = "2";
                        }
                        else
                        {
                            this.row["Status"] = "1";
                        }
                        this.row.EndEdit();
                        this.tempDataTable.AcceptChanges();
                        bll_abnormal.KQMSaveAbnormalAttendanceInfo(this.ProcessFlag.Value, this.tempDataTable, CurrentUserInfo.Personcode, logmodel);
                    }
                }
            }
        }
        //protected void ButtonOrgAudit_Click(object sender, EventArgs e)
        //{
        //    SendOrgAudit();
        //}
        private void SendOrgAudit()
        {
            try
            {

                string BillTypeCode = "KQMException";//單據類型
                string BillNoType = "KQE";//單據前綴
                int intSendOK = 0;
                int intSendBillNo = 0;
                int intSendError = 0;
                string OrgCode = "", AuditOrgCode = "";
                string OTType = "";
                string ExceptionType = "";
                string Flow_LevelRemark = Message.flow_levelremark;

                TemplatedColumn tcol = (TemplatedColumn)UltraWebGridKaoQinData.Bands[0].Columns[0];

                Dictionary<string, List<string>> dicy = new Dictionary<string, List<string>>();

                for (int i = 0; i < UltraWebGridKaoQinData.Rows.Count; i++)
                {
                    CellItem GridItem = (CellItem)tcol.CellItems[i];
                    CheckBox chkIsHaveRight = (CheckBox)(GridItem.FindControl("CheckBoxCell"));
                    if (chkIsHaveRight.Checked)
                    {
                        ExceptionType = (this.UltraWebGridKaoQinData.Rows[i].Cells.FromKey("ExceptionType").Value == null) ? "" : this.UltraWebGridKaoQinData.Rows[i].Cells.FromKey("ExceptionType").Value.ToString();
                        if ((this.UltraWebGridKaoQinData.Rows[i].Cells.FromKey("Status").Text != "2") || (!ExceptionType.Equals("A") && !ExceptionType.Equals("B")))
                        {
                            WriteMessage(1, Message.common_message_audit_unsendaudit);
                            return;
                        }

                        OrgCode = UltraWebGridKaoQinData.Rows[i].Cells.FromKey("dCode").Text.Trim();
                        AuditOrgCode =bll_abnormal.GetWorkFlowOrgCode(OrgCode, BillTypeCode, "");
                        string key = AuditOrgCode;
                        List<string> list = new List<string>();
                        if (!dicy.ContainsKey(key) && AuditOrgCode.Length > 0)
                        {
                            dicy.Add(key, list);
                        }
                        else if (AuditOrgCode.Length == 0)
                        {
                            intSendError += 1;

                        }

                        if (AuditOrgCode.Length > 0)
                        {
                            intSendOK += 1;
                        }
                        AuditOrgCode = "";
                    }
                }

                for (int i = 0; i < UltraWebGridKaoQinData.Rows.Count; i++)
                {
                    CellItem GridItem = (CellItem)tcol.CellItems[i];
                    CheckBox chkIsHaveRight = (CheckBox)(GridItem.FindControl("CheckBoxCell"));
                    if (chkIsHaveRight.Checked)
                    {
                        OrgCode = UltraWebGridKaoQinData.Rows[i].Cells.FromKey("dCode").Text.Trim();
                        AuditOrgCode = bll_abnormal.GetWorkFlowOrgCode(OrgCode, BillTypeCode, OTType);
                        string key = AuditOrgCode ;
                        if (dicy[key] != null)
                        {
                            dicy[key].Add(UltraWebGridKaoQinData.Rows[i].Cells.FromKey("ID").Text.Trim());
                        }
                    }
                }

                int count = bll_abnormal.SaveOrgAuditData("Add", dicy, BillNoType, BillTypeCode, CurrentUserInfo.Personcode,  Flow_LevelRemark, logmodel);
                intSendBillNo = count;


                if (intSendBillNo > 0)
                {
                    if (intSendError > 0)
                    {
                        this.WriteMessage(1, Message.Succ_sign + " " + Message.common_message_billcount + "：" + intSendBillNo + ";" + Message.FaileCount + intSendError + "(" + Message.common_message_noworkflow + ")");
                    }
                    else
                    {
                        this.WriteMessage(1, Message.Succ_sign + " " + Message.common_message_billcount + "：" + intSendBillNo);
                    }
                }
                else
                {
                    this.WriteMessage(1, Message.Error_sign);
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
        #region 分頁
        protected void pager_PageChanged(object sender, EventArgs e)
        {
            this.Query(true, "Goto");
        }
        #endregion
    }
}
