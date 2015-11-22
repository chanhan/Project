using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using GDSBG.MiABU.Attendance.BLL.Hr.KQM.KaoQinData;
using Resources;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;
namespace GDSBG.MiABU.Attendance.Web.Hr.KQM.KaoQinData
{
    public partial class KQMLeaveApplyConfirmForm : BasePage
    {
        KQMLeaveApplyForm_ZBLHBll leaveApply = new KQMLeaveApplyForm_ZBLHBll();
        string moduleCode = "";
        static SynclogModel logmodel = new SynclogModel();
        protected void Page_Load(object sender, EventArgs e)
        {

            SetCalendar(txtStartDate, txtEndDate);
            if (!base.IsPostBack)
            {
                logmodel.ProcessOwner = CurrentUserInfo.Personcode;
                logmodel.TransactionType = Request.QueryString["ModuleCode"] == null ? "" : Request.QueryString["ModuleCode"].ToString();
                logmodel.LevelNo = "2";
                logmodel.FromHost = Request.UserHostAddress;

                moduleCode = Request.QueryString["ModuleCode"] == null ? "" : Request.QueryString["ModuleCode"].ToString();
                bool privileged = Request.QueryString["Privileged"].ToString() == "true" ? true : false;
                string BillNo = Request.QueryString["BillNo"] == null ? "" : Request.QueryString["BillNo"].ToString();
                txtStartDate.Attributes.Add("onpropertychange", "javascript:GetLVTotal();");
                txtStartDate.Attributes.Add("onafterpaste", "javascript:GetLVTotal();");
             
                txtStartTime.EditModeFormat = "HH:mm";
                txtEndDate.Attributes.Add("onpropertychange", "javascript:GetLVTotal();");
                txtEndDate.Attributes.Add("onafterpaste", "javascript:GetLVTotal();");
                txtEndTime.EditModeFormat = "HH:mm";
                if (BillNo.Length > 0)
                {
                    hidBillNo.Value = BillNo;
                    this.Modify(BillNo, privileged);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "enableRole", "alert('"+Message.AtLastOneChoose+"');window.close();", true);
                }
                btnSave.Attributes.Add("onclick", "return confirm('" + Message.ConfirmSaveData + "')");
                this.ddlLVTypeCode.Attributes.Add("onchange", "onChangeLVTypeCode(this.options[this.selectedIndex].value)");
            }
        }
        protected void Modify(string billNo, bool privileged)
        {
            this.EmpQuery(billNo, privileged);
        }
        private void EmpQuery(string billNo, bool privileged)
        {
            //string condition = "and a.ID='" + BillNo.ToString() + "' ";
            //if (privileged)
            //{
            //    condition = condition + " AND exists (SELECT 1 FROM (" + base.sqlDep + ") e where e.DepCode=b.DCode)";
            //}
            DataTable tempDataTable = leaveApply.getLeaveApplyData(billNo, privileged, SqlDep);
            if (tempDataTable.Rows.Count > 0)
            {
                txtBillNo.Text = tempDataTable.Rows[0]["BillNo"].ToString();
                txtEmployeeNo.Text = tempDataTable.Rows[0]["WorkNo"].ToString();
                txtName.Text = tempDataTable.Rows[0]["LocalName"].ToString();
                txtStartDate.Text = (tempDataTable.Rows[0]["StartDate"].ToString().Trim() == null) ? "" : string.Format("{0:yyyy/MM/dd}", Convert.ToDateTime(tempDataTable.Rows[0]["StartDate"].ToString().Trim()));
                txtStartTime.Text = (tempDataTable.Rows[0]["StartTime"].ToString().Trim() == null) ? "" : tempDataTable.Rows[0]["StartTime"].ToString().Trim();
                txtEndDate.Text = (tempDataTable.Rows[0]["EndDate"].ToString().Trim() == null) ? "" : string.Format("{0:yyyy/MM/dd}", Convert.ToDateTime(tempDataTable.Rows[0]["EndDate"].ToString().Trim()));
                txtEndTime.Value = (tempDataTable.Rows[0]["EndTime"].ToString().Trim() == null) ? DateTime.Parse("0001/01/01 00:00") : Convert.ToDateTime(tempDataTable.Rows[0]["EndTime"].ToString().Trim());
                txtLVTotal.Text = (tempDataTable.Rows[0]["LVTotal"].ToString().Trim() == null) ? "" : tempDataTable.Rows[0]["LVTotal"].ToString().Trim();
                hidLVTotal.Value = (tempDataTable.Rows[0]["LVTotal"].ToString().Trim() == null) ? "" : tempDataTable.Rows[0]["LVTotal"].ToString().Trim();
                txtReason.Text = (tempDataTable.Rows[0]["Reason"].ToString().Trim() == null) ? "" : tempDataTable.Rows[0]["Reason"].ToString().Trim();
                txtProxy.Text = (tempDataTable.Rows[0]["Proxy"].ToString().Trim() == null) ? "" : tempDataTable.Rows[0]["Proxy"].ToString().Trim();
                txtRemark.Text = (tempDataTable.Rows[0]["Remark"].ToString().Trim() == null) ? "" : tempDataTable.Rows[0]["Remark"].ToString().Trim();
                string LVTypeCode = (tempDataTable.Rows[0]["LVTypeCode"].ToString().Trim() == null) ? "" : tempDataTable.Rows[0]["LVTypeCode"].ToString().Trim();
                string strSex = (tempDataTable.Rows[0]["Sex"].ToString().Trim() == null) ? "" : tempDataTable.Rows[0]["Sex"].ToString().Trim();
                hidLVTypeCode.Value = LVTypeCode;
                string strYearMonth = Convert.ToDateTime(txtStartDate.Text).ToString("yyyyMM");
                bool boolValue = DateTime.Now.ToString("yyyyMM").Equals(strYearMonth);
                if (boolValue)
                {
                    this.hidBoolYearMonth.Value = "Y";
                }
                //if (LVTypeCode.Equals("Y"))
                //{
                //    this.tempDataSet = ((ServiceLocator) this.Session["serviceLocator"]).GetKQMLeaveTypeData().GetDataByCondition(" WHERE LVTypeCode='Y'");
                //    this.ddlLVTypeCode.DataSource = this.tempDataSet.Tables[0].DefaultView;
                //    this.ddlLVTypeCode.DataTextField = "LVTypeName";
                //    this.ddlLVTypeCode.DataValueField = "LVTypeCode";
                //    this.ddlLVTypeCode.DataBind();
                //}
                //   else
                //      {

                DataTable dt = new DataTable();
                if (boolValue)
                {
                    if (strSex.Equals("1"))
                    {
                        dt = leaveApply.getLVTypeBySexCode("0");
                    }
                    else
                    {
                        dt = leaveApply.getLVTypeBySexCode("1");
                    }
                }
                else
                {
                    dt= leaveApply.getLVTypeByLVTypeCode( LVTypeCode);
                }
                this.ddlLVTypeCode.DataSource =dt.DefaultView;
                this.ddlLVTypeCode.DataTextField = "LVTypeName";
                this.ddlLVTypeCode.DataValueField = "LVTypeCode";
                this.ddlLVTypeCode.DataBind();
                //   }
                this.ddlLVTypeCode.SelectedIndex = this.ddlLVTypeCode.Items.IndexOf(this.ddlLVTypeCode.Items.FindByValue(LVTypeCode));
                string status = tempDataTable.Rows[0]["Status"].ToString();
                if ((status == null) || (status != "2"))
                {
                    btnSave.Enabled = false;
                }
                //if (this.HiddenLVTypeCode.Value == "Y")
                //{
                //    txtStartDate.BorderStyle = BorderStyle.None;
                //    txtStartTime.BorderStyle = BorderStyle.None;
                //    this.divLeaveYear.Visible = true;
                //    string ReaportYear = "";
                //    if (tempDataTable.Rows[0]["IsLastYear"].ToString() == "Y")
                //    {
                //        ReaportYear = Convert.ToDateTime(txtStartDate.Text).AddYears(-1).Year.ToString();
                //    }
                //    else
                //    {
                //        ReaportYear = Convert.ToDateTime(txtStartDate.Text).Year.ToString();
                //    }
                //    string[] temVal = this.GetEmpYearLeaveDays(txtEmployeeNo.Text, ReaportYear, txtStartDate.Text).Split(new char[] { '|' });
                //    this.textBoxAbleRest.Text = temVal[0].ToString();
                //    this.textBoxLeaveReset.Text = temVal[1].ToString();
                //    this.textBoxAlreadyRest.Text = temVal[2].ToString();
                //    if ((temVal[3].ToString() != "0") && (DateTime.Parse(this.textBoxStartDate.Text) < DateTime.Now))
                //    {
                //        this.labelReachLeaveRemark.Text = base.GetResouseValue("kqm.leaveapply.reachleaveremark") + temVal[4].ToString() + base.GetResouseValue("kqm.leaveapply.reachleave") + "：" + temVal[3].ToString();
                //    }
                //}
            }
            else
            {
                Response.Write("<script type='text/javascript'>alert('"+Message.AtLastOneChoose+"');window.close();</script>");
            }
        }
        private bool CheckData(string txtValue, string ReValue)
        {
            if (string.IsNullOrEmpty(txtValue))
            {
                WriteMessage(ReValue);
                return false;
            }
            return true;
        }
        private void WriteMessage(string alert)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "addOrUpDateLeaveApply" + alert, "alert('" + alert + "')", true);
        }

        private bool CheckLeaveOverTime(string empNo, string startDate, string startTime, string endDate, string endTime)
        {
            return leaveApply.CheckLeaveOverTime(empNo, startDate, startTime, endDate, endTime);
        }

        private double CheckLvtotal(string processFlag, string empNo, string startDate, string BillNo)
        {
            return leaveApply.CheckLvtotal(processFlag, empNo, startDate, BillNo);
        }
        private bool CheckLvtotal(string ProcessFlag, string workNo, string lvType, string strDate, string sTotal, string BillNo, string ImportType)
        {
            return leaveApply.CheckLvtotal(ProcessFlag, workNo, lvType, strDate, sTotal, BillNo, ImportType);
        }
        private bool CheckLeaveOverTime(string WorkNo, string StartDate, string StartTime, string EndDate, string EndTime, string BillNo)
        {
            return leaveApply.CheckLeaveOverTime(WorkNo, StartDate, StartTime, EndDate, EndTime, BillNo);
        }

        private bool CheckLvDays(string LVType, string sTotal)
        {
            if (sTotal.Length == 0)
            {
                return false;
            }
            try
            {
                double MinHours = 0.5;
                double StandardHours = 0.5;
                double dTotal = Convert.ToDouble(sTotal);
                if (LVType.Equals("Y"))
                {
                    dTotal *= 8.0;
                }
                DataTable tempDataTable = leaveApply.getHours(LVType);
                if (tempDataTable.Rows.Count > 0)
                {
                    MinHours = Convert.ToDouble(tempDataTable.Rows[0]["MinHours"]);
                    StandardHours = Convert.ToDouble(tempDataTable.Rows[0]["StandardHours"]);
                }
                if (dTotal < MinHours)
                {
                    return false;
                }
                if ((dTotal % StandardHours) != 0.0)
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if ((((CheckData(txtEmployeeNo.Text, Message.WorkNoNotNull) && CheckData(txtStartDate.Text, Message.StartDateNotNull)) && (CheckData(txtStartTime.Text, Message.ErrStartDateNull) && CheckData(txtEndDate.Text, Message.EndDateNotNull))) && CheckData(txtEndTime.Text, Message.ErrEndDateNull)) && CheckData(txtLVTotal.Text, Message.DayNotNull))
                {
                    string strAllowDepLevel = leaveApply.getAllowDepLevel(ddlLVTypeCode.SelectedValue);
                    if (!string.IsNullOrEmpty(strAllowDepLevel) && !strAllowDepLevel.Equals(Convert.ToString(CurrentUserInfo.DepLevel)))
                    {
                        WriteMessage(Message.NoPrivileged + this.ddlLVTypeCode.SelectedItem.Text);
                    }
                    else if (this.ddlLVTypeCode.SelectedValue.Equals("x") && ((Convert.ToInt16(txtLVTotal.Value) / 8) <= 15))
                    {
                        WriteMessage(Message.LeaveDayCountNotRight);
                    }
                    else
                    {
                        bool BoolIsModify = true;
                        if (ddlLVTypeCode.SelectedValue.Equals(hidLVTypeCode.Value))
                        {
                            BoolIsModify = false;
                        }
                        try
                        {
                        //    double LVTotalDouble = Convert.ToDouble(txtLVTotal.Value.ToString()) / 8.0;
                            double LVTotalDouble = 100 / 8.0;

                            if (LVTotalDouble <= 0.0)
                            {
                                WriteMessage(Message.LeaveDayCountNotRight);
                                return;
                            }
                            //if ((this.ddlLVTypeCode.SelectedValue == "Y") && ((LVTotalDouble - (Convert.ToDouble(this.HiddenLVTotal.Value) / 8.0)) > Convert.ToDouble(txtLeaveReset.Text)))
                            //{
                            //    base.WriteMessage(1, base.GetResouseValue("hrm.leaveapply.errordays"));
                            //    return;
                            //}
                        }
                        catch (Exception)
                        {
                            WriteMessage(Message.LeaveDayCountNotRight);
                            return;
                        }
                        try
                        {
                            string nStartDate = string.Format(txtStartDate.Text + " " + txtStartTime.Text, "yyyy/MM/dd HH:mi");
                            string nEndDate = string.Format(txtEndDate.Text + " " + txtEndTime.Text, "yyyy/MM/dd HH:mi");
                            TimeSpan tsLVTotal = (TimeSpan)(Convert.ToDateTime(nEndDate) - Convert.ToDateTime(nStartDate));
                            if (tsLVTotal.TotalHours <= 0.0)
                            {
                                WriteMessage(Message.LeaveDayCountNotRight);
                                return;
                            }
                            //if ((this.ddlLVTypeCode.SelectedValue == "Y") && !Convert.ToDateTime(nEndDate).Year.ToString().Equals(Convert.ToDateTime(nEndDate).Year.ToString()))
                            //{
                            //    base.WriteMessage(1, base.GetResouseValue("common.message.checksameyear"));
                            //    return;
                            //}
                        }
                        catch (Exception)
                        {
                            WriteMessage(Message.LeaveDayCountNotRight);
                            return;
                        }
                       DataTable tempDataTable = leaveApply.getLeaveApply(hidBillNo.Value);
                        if (tempDataTable.Rows.Count == 0)
                        {
                            WriteMessage(Message.AtLastOneChoose);
                        }
                        else if (!CheckLeaveOverTime(txtEmployeeNo.Text, txtStartDate.Text, txtStartTime.Text, txtEndDate.Text, txtEndTime.Text, hidBillNo.Value))
                        {
                            WriteMessage(Message.LeaveDaySame);
                        }
                        else if (!CheckLvDays(this.ddlLVTypeCode.SelectedValue, txtLVTotal.Value.ToString()))
                        {
                            WriteMessage(Message.LeaveDayCountNotRight);
                        }
                        else
                        {
                            string ImportType = "";
                            if (ddlLVTypeCode.SelectedValue.Equals("U"))
                            {
                                if (!Convert.ToString(CurrentUserInfo.RoleCode).Equals("internal"))
                                {
                                    if (leaveApply.getAuthorizedFunctionList(CurrentUserInfo.RoleCode, moduleCode).IndexOfValue("ImportU") >= 0)
                                    {
                                        ImportType = "U";
                                    }
                                }
                                else
                                {
                                    ImportType = "U";
                                }
                            }
                            if (BoolIsModify)
                            {
                                if (CheckLvtotal("Add", txtEmployeeNo.Text.Trim(), this.ddlLVTypeCode.SelectedValue, txtStartDate.Text, txtLVTotal.Value.ToString(), hidBillNo.Value.Trim(), ImportType))
                                {
                                    WriteMessage(Message.LeaveApplyTotalError);
                                    return;
                                }
                            }
                            else if (CheckLvtotal("Modify", txtEmployeeNo.Text.Trim(), this.ddlLVTypeCode.SelectedValue, txtStartDate.Text, txtLVTotal.Value.ToString(), hidBillNo.Value.Trim(), ImportType))
                            {
                                WriteMessage(Message.LeaveApplyTotalError);
                                return;
                            }
                            if (!leaveApply.isLeaveNoAudit().Equals("Y") && !txtEndDate.Text.Equals(txtStartDate.Text))
                            {
                                string startShiftNo = "";
                                string endShiftNo = "";
                                startShiftNo = leaveApply.getLVTotal(txtEmployeeNo.Text.Trim(), txtStartDate.Text);
                                endShiftNo = leaveApply.getLVTotal(txtEmployeeNo.Text.Trim(), txtEndDate.Text);
                                if (!(endShiftNo.StartsWith("C") || !startShiftNo.StartsWith("C")))
                                {
                                    endShiftNo = leaveApply.getLVTotal(txtEmployeeNo.Text.Trim(), Convert.ToDateTime(txtEndDate.Text).AddDays(-1.0).ToString("yyyy/MM/dd"));
                                }
                                if (!(startShiftNo.Equals(endShiftNo) && (startShiftNo.Length != 0)))
                                {
                                    WriteMessage(Message.ShiftNoNotSame);
                                    return;
                                }
                            }
                            DataRow row = tempDataTable.Rows[0];
                            row.BeginEdit();
                            row["ID"] = hidBillNo.Value;
                            row["WorkNo"] = txtEmployeeNo.Text.ToUpper().Trim();
                            row["StartDate"] = txtStartDate.Text;
                            row["StartTime"] = txtStartTime.Text;
                            row["EndDate"] = txtEndDate.Text;
                            row["EndTime"] = txtEndTime.Text;
                            row["LVTotal"] = txtLVTotal.Value.ToString();
                            row["LVTypeCode"] = this.ddlLVTypeCode.SelectedValue;
                            row["Status"] = "4";
                            row["REMARK"] = txtRemark.Text.Trim();
                            row.EndEdit();
                            tempDataTable.AcceptChanges();
                          leaveApply.SaveData("Confirm", tempDataTable,logmodel);
                          leaveApply.getLeaveDetail(hidBillNo.Value);
                            if (ddlLVTypeCode.SelectedValue.Equals("U"))
                            {
                                leaveApply.CountCanAdjlasthy(txtEmployeeNo.Text.ToUpper().Trim());
                            }
                            string WorkNo = txtEmployeeNo.Text;
                            string StartDate = Convert.ToDateTime(txtStartDate.Text).ToString("yyyy/MM/dd");
                            string EndDate = Convert.ToDateTime(txtEndDate.Text).ToString("yyyy/MM/dd");
                            if ((StartDate.CompareTo(DateTime.Now.ToString("yyyy/MM/dd")) == -1) || BoolIsModify)
                            {
                                if (EndDate.CompareTo(DateTime.Now.ToString("yyyy/MM/dd")) > 0)
                                {
                                    EndDate = DateTime.Now.ToString("yyyy/MM/dd");
                                }
                                leaveApply.getKaoQinData(WorkNo, "null", StartDate, EndDate);
                            }
                            Response.Write("<script type='text/javascript'>alert('" + Message.DataSaveSuccess + "');window.opener.document.all.btnQuery.click();window.close();</script>");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
              WriteMessage(ex.InnerException == null ? ex.Message : ex.InnerException.Message);
            }
        }
        /// <summary>
        /// AJAX验证角色代码是否存在
        /// </summary>
        protected override void AjaxProcess()
        {
            if (Request.Form["workno"] != null)
            {
                string strinfo = "";
                if (Request.Form["workno"] != null)
                {
                    strinfo = leaveApply.getLVTotal(Request.Form["workno"], Request.Form["startDate"], Request.Form["endDate"], Request.Form["typecode"]);
                }
                Response.Clear();
                Response.Write(strinfo);
                Response.End();
            }

        }
    }
}
