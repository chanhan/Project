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
using GDSBG.MiABU.Attendance.BLL.WorkFlow;
using GDSBG.MiABU.Attendance.Model.WorkFlow;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using Resources;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;

namespace GDSBG.MiABU.Attendance.Web.WorkFlow
{
    public partial class WorkFlowProjectApplyAdd : BasePage
    {
        protected System.Data.DataSet dataSet, tempDataSet;
        protected System.Data.DataTable tempDataTable;
        Dictionary<string, string> ClientMessage = null;
        JavaScriptSerializer JsSerializer = new JavaScriptSerializer();
        public string WorkNo;
        private string ShiftNo;
        WFProjectApplyBll wfaddt = new WFProjectApplyBll();
        private string dateFormat = "yyyy/MM/dd";
        static SynclogModel logmodel = new SynclogModel();

        #region Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (ClientMessage == null)
                {
                    ClientMessage = new Dictionary<string, string>();
                    ClientMessage.Add("WorkNoFirst", Message.WorkNoFirst);
                    ClientMessage.Add("common_sunday", Message.common_sunday);
                    ClientMessage.Add("common_monday", Message.common_monday);
                    ClientMessage.Add("common_tuesday", Message.common_tuesday);
                    ClientMessage.Add("common_wednesday", Message.common_wednesday);
                    ClientMessage.Add("common_thursday", Message.common_thursday);
                    ClientMessage.Add("common_friday", Message.common_friday);
                    ClientMessage.Add("common_saturday", Message.common_saturday);

                    string clientmsg = JsSerializer.Serialize(ClientMessage);
                    Page.ClientScript.RegisterStartupScript(GetType(), "ClientMessage", "var Message=" + clientmsg + ";", true);
                }
                if (!this.IsPostBack)
                {

                    logmodel.ProcessOwner = CurrentUserInfo.Personcode;
                    logmodel.TransactionType = (Request.QueryString["ModuleCode"] == null) ? "" : Request.QueryString["ModuleCode"].ToString();
                    logmodel.LevelNo = "2";
                    logmodel.FromHost = Request.UserHostAddress;

                    this.Internationalization();
                    string EmployeeNo = this.Request.QueryString["EmployeeNo"] == null ? "" : this.Request.QueryString["EmployeeNo"].ToString();
                    string ID = this.Request.QueryString["ID"] == null ? "" : this.Request.QueryString["ID"].ToString();
                    string ProcessFlag = this.Request.QueryString["ProcessFlag"] == null ? "" : this.Request.QueryString["ProcessFlag"].ToString();
                    this.HiddenID.Value = ID;
                    this.ProcessFlag.Value = ProcessFlag;
                    this.textBoxApplyDate.Text = System.DateTime.Today.ToShortDateString();
                    if (ProcessFlag == "Add")
                    {
                        this.HiddenID.Value = "";
                        Add();
                    }
                    else
                    {
                        if (EmployeeNo.Length > 0 && ID.Length > 0)
                        {
                            Modify(EmployeeNo, ID);
                        }
                        else
                        {
                            Response.Write("<script type='text/javascript'>alert(\"" + Message.AtLastOneChoose + "\");window.parent.document.all.topTable.style.display='';window.parent.document.all.divEdit.style.display='none';</script>");
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                this.WriteMessage(2, (ex.InnerException == null ? ex.Message : ex.InnerException.Message));
            }
            //AjaxPro.Utility.RegisterTypeForAjax(typeof(KQM_OTM_OTMProjectApplyEditForm));
            SetCalendar(textBoxOTDate);

        }
        #endregion

        #region Internationalization
        private void Internationalization()
        {
            //set button text
          //  ButtonSave.Text = "存儲";
            ButtonReturn.Text = Message.btnBack;
        }
        #endregion

        #region Button Click
        protected void Add()
        {
            if (this.HiddenSave.Value != "Save")
            {
                this.textBoxEmployeeNo.Text = "";
            }
            this.ProcessFlag.Value = "Add";
            this.TextBoxsReset("Add", false);
        }

        protected void Modify(string EmployeeNo, string ID)
        {
            this.textBoxEmployeeNo.Text = EmployeeNo;
            EmpQuery(EmployeeNo, ID);
            this.ProcessFlag.Value = "Modify";
            this.TextBoxsReset("Modify", false);
        }

        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this.CheckData(this.textBoxOTDate.Text.ToString(), Message.overtime_date) ||
                    !this.CheckData(this.textBoxBeginTime.Text, Message.start_time) ||
                    !this.CheckData(this.textBoxEndTime.Text, Message.end_time) ||
                    !this.CheckData(this.textBoxHours.Text, Message.times) ||
                    !this.CheckData(this.textBoxWorkDesc.Text, Message.overtime_desc))
                {
                    return;
                }
                string condition = "";
                string strtemp = "";
                string OTDate = "";
                //Function CommonFun = new Function();
                try
                {
                    OTDate = DateTime.Parse(this.textBoxOTDate.Text.ToString()).ToString("yyyy/MM/dd");
                }
                catch (System.Exception)
                {
                    this.WriteMessage(1, Message.common_message_data_errordate);
                    return;
                }

                WorkNo = this.textBoxEmployeeNo.Text.ToUpper();
                string StrBtime = this.textBoxBeginTime.Text.ToString();
                string StrEtime = this.textBoxEndTime.Text.ToString();
                DateTime dtTempBeginTime = DateTime.Parse(OTDate + " " + StrBtime);
                DateTime dtTempEndTime = DateTime.Parse(OTDate + " " + StrEtime);
                //DateTime dtMidTime = DateTime.Parse(OTDate + " 12:00");

                #region 每天16:30后不讓預報加班,非工作日也不能預報加班  龍華總部周邊 update by xukai 20111014
                string LHZBLimitTime = "";
                string LHZBIsLimitApply = "";
                string workflag = "";
                try
                {
                    LHZBIsLimitApply = System.Configuration.ConfigurationManager.AppSettings["LHZBIsLimitAdvApplyTime"];
                    LHZBLimitTime = System.Configuration.ConfigurationManager.AppSettings["LHZBLimitTime"];
                }
                catch
                {
                    LHZBIsLimitApply = "N";
                    LHZBLimitTime = "";
                }

                if (!string.IsNullOrEmpty(LHZBLimitTime))
                {
                    //每天LHZBLimitTime時間后不能新增、修改加班預報
                    DateTime dtLimit = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd") + " " + LHZBLimitTime);
                    if (dtLimit < DateTime.Now)
                    {
                        this.WriteMessage(1, Message.kqm_otm_advanceapply + LHZBLimitTime + Message.kqm_otm_advanceapply_error);
                        return;
                    }
                }
                if (LHZBIsLimitApply == "Y")
                {
                    //非工作日不能預報加班
                    workflag = wfaddt.GetEmpWorkFlag(WorkNo, DateTime.Now.ToString("yyyy-MM-dd"));
                    if (workflag == "N")
                    {
                        //this.WriteMessage(1, this.GetResouseValue("kqm.otm.advanceapply.workflag"));
                        return;
                    }
                }
                #endregion

                string OTMAdvanceBeforeDays = wfaddt.GetValue("select nvl(max(paravalue),'2') from GDS_SC_PARAMETER where paraname='OTMAdvanceBeforeDays'");
                string appUserIsIn = wfaddt.GetValue("select nvl(max(workno),'Y') from GDS_ATT_EMPLOYEES where workno='" + CurrentUserInfo.Personcode + "'");

                if (!appUserIsIn.Equals("Y"))//允許申報當前日期以前多少天的加班除去非工作日
                {
                    //OTMAdvanceBeforeDays = Convert.ToString(Convert.ToDouble(OTMAdvanceBeforeDays) + 30);
                    int i = 0;
                    int WorkDays = 0;
                    string UserOTType = "";
                    while (i < Convert.ToDouble(OTMAdvanceBeforeDays))
                    {
                        condition = "SELECT workflag FROM GDS_ATT_BGCALENDAR WHERE workday = to_date('" + System.DateTime.Now.AddDays(-1 - i - WorkDays).ToString("yyyy/MM/dd") + "','yyyy/MM/dd') AND bgcode IN (SELECT depcode FROM GDS_SC_DEPARTMENT WHERE levelcode = '0' and companyid='" + CurrentUserInfo.CompanyId + "')";
                        UserOTType = wfaddt.GetValue(condition);

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
                condition = "select trunc(sysdate)-to_date('" + OTDate + "','yyyy/mm/dd') from dual";
                strtemp = wfaddt.GetValue(condition);

                if (Convert.ToDouble(strtemp) > (Convert.ToDouble(OTMAdvanceBeforeDays)))
                {
                    this.WriteMessage(1, Message.otm_message_checkadvancedaysbefore + ":" + OTMAdvanceBeforeDays);
                    return;
                }
                string tmpOTType = FindOTType(OTDate, WorkNo);
                //if (tmpOTType.Length == 0)
                //{
                //    this.WriteMessage(1, "加班類型" + "不能為空.");
                //    return;
                //}

                #region 龍華總部周邊HR。G2，G3名稱更改為G2(休息日上班)，G3(法定假日上班) update by xukai 20111010
                string LHZBIsDisplayG2G3 = "";
                try
                {
                    LHZBIsDisplayG2G3 = System.Configuration.ConfigurationManager.AppSettings["LHZBIsDisplayG2G3"];
                }
                catch
                {
                    LHZBIsDisplayG2G3 = "N";
                }

                if (LHZBIsDisplayG2G3 == "Y")
                {
                    tmpOTType = tmpOTType.Substring(0, 2);
                }
                #endregion

                double OTHours = 0;
                string strOTHours = "";
                try
                {
                    OTHours = Convert.ToDouble(this.textBoxHours.Text.Trim());
                    strOTHours = GetOTHours(WorkNo, OTDate, StrBtime, StrEtime, tmpOTType);
                }
                catch (System.Exception)
                {
                    this.WriteMessage(1, Message.otm_errorhours);
                    return;
                }
             
                //抓取班別
                ShiftNo = wfaddt.GetShiftNo(WorkNo, OTDate);
                if (ShiftNo == null || ShiftNo == "")
                {
                    this.WriteMessage(1, Message.otm_exception_errorshiftno_1);
                    return;
                }
                SortedList list = new SortedList();
                list = wfaddt.ReturnOTTTime(WorkNo, OTDate, dtTempBeginTime, dtTempEndTime, ShiftNo);

                dtTempBeginTime = Convert.ToDateTime(list.GetByIndex(list.IndexOfKey("A")));
                dtTempEndTime = Convert.ToDateTime(list.GetByIndex(list.IndexOfKey("B")));

                //請假中不能申報加班CheckLeaveOverTime
                if (!this.CheckLeaveOverTime(WorkNo, dtTempBeginTime.ToString("yyyy/MM/dd"), dtTempBeginTime.ToString("HH:mm"), dtTempEndTime.ToString("yyyy/MM/dd"), dtTempEndTime.ToString("HH:mm")))
                {
                    this.WriteMessage(1, Message.common_message_Leaveovertime_repeart);
                    return;
                }

                //G3加班后一天如果是正常上班不允許跨天申報Modify by Jackzhang2011/4/2
                if (!this.CheckG3WorkTime(WorkNo, OTDate, dtTempBeginTime.ToString("yyyy/MM/dd HH:mm"), dtTempEndTime.ToString("yyyy/MM/dd HH:mm"), tmpOTType))
                {
                    this.WriteMessage(1, Message.common_message_otm_worktime_checkg3);//加班時間段內含有正常工作時間，零點以后不允許申報加班
                    return;
                }

                //工作時間內不能預報加班CheckWorkTime
                if (!this.CheckWorkTime(WorkNo, OTDate, dtTempBeginTime.ToString("yyyy/MM/dd HH:mm"), dtTempEndTime.ToString("yyyy/MM/dd HH:mm"), ShiftNo))
                {
                    this.WriteMessage(1, Message.common_message_otm_worktime_repeart);
                    return;
                }
                if (Convert.ToDouble(strOTHours) < 0.5)
                {
                    this.WriteMessage(1, Message.otm_othourerror);
                    return;
                }
                //判斷相同時段是否有參加教育訓練
                if (!this.CheckOTOverETM(WorkNo, dtTempBeginTime.ToString("yyyy/MM/dd HH:mm"), dtTempEndTime.ToString("yyyy/MM/dd HH:mm")))
                {
                    this.WriteMessage(1, Message.common_message_otm_etmrepeart);
                    return;
                }
                //獲取加班異常信息
                string OTMSGFlag = wfaddt.GetOTMSGFlag(WorkNo, OTDate, Convert.ToDouble(strOTHours), tmpOTType, "Y", this.HiddenID.Value);
                string tmpRemark = "";
                if (OTMSGFlag != "")
                {
                    tmpRemark = OTMSGFlag.Substring(1, OTMSGFlag.Length - 1);
                    OTMSGFlag = OTMSGFlag.Substring(0, 1);
                }
                if (OTMSGFlag.Equals("A"))
                {
                    this.WriteMessage(1, tmpRemark);
                    return;
                }
                if (this.ProcessFlag.Value.Equals("Add"))
                {
                    //一天内多笔加班时间不能交叉或重复
                    if (!this.CheckOverTime(WorkNo, OTDate, dtTempBeginTime.ToString("yyyy/MM/dd HH:mm"), dtTempEndTime.ToString("yyyy/MM/dd HH:mm"), ShiftNo, true))
                    {
                        this.WriteMessage(1, Message.common_message_otm_multi_repeart);
                        return;
                    }
                    this.tempDataTable = wfaddt.GetDataByCondition_1("and 1=2").Tables["OTM_AdvanceApply"];
                    if (this.tempDataTable.Rows.Count > 0)
                    {
                        this.WriteMessage(1, Message.common_message_duplicate);
                        return;
                    }
                    System.Data.DataRow row = this.tempDataTable.NewRow();
                    row.BeginEdit();
                    row["WORKNO"] = WorkNo;
                    row["OTDate"] = OTDate;
                    row["BeginTime"] = dtTempBeginTime;
                    row["EndTime"] = dtTempEndTime;
                    row["Hours"] = strOTHours;
                    row["OTType"] = tmpOTType;
                    row["WorkDesc"] = MoveSpecailChar(this.textBoxWorkDesc.Text);
                    row["Remark"] = tmpRemark;
                    row["OTMSGFlag"] = OTMSGFlag;
                    row["ApplyDate"] = DateTime.Parse(this.textBoxApplyDate.Text.Trim().ToString()).ToString(this.dateFormat);
                    row["IsProject"] = "Y";
                    row["OTShiftNo"] = ShiftNo;
                    row["UPDATE_USER"] = CurrentUserInfo.Personcode;
                    row["Status"] = "0";
                    row.EndEdit();
                    this.tempDataTable.Rows.Add(row);
                    this.tempDataTable.AcceptChanges();
                    wfaddt.SaveData(this.ProcessFlag.Value, this.tempDataTable,logmodel);
                    this.HiddenSave.Value = "Save";
                }
                else if (ProcessFlag.Value.Equals("Modify"))
                {
                    string M_ID = this.HiddenID.Value;

                    //一天内多笔加班时间不能交叉或重复
                    if (!this.CheckOverTime(M_ID, WorkNo, OTDate, dtTempBeginTime.ToString("yyyy/MM/dd HH:mm"), dtTempEndTime.ToString("yyyy/MM/dd HH:mm"), ShiftNo, true))
                    {
                        this.WriteMessage(1, Message.common_message_otm_multi_repeart);
                        return;
                    }
                    string condid = "and a.ID='" + M_ID + "'";
                    this.tempDataTable = wfaddt.GetDataByCondition_1(condid).Tables["OTM_AdvanceApply"];
                    if (this.tempDataTable.Rows.Count == 0)
                    {
                        this.WriteMessage(1, Message.AtLastOneChoose);
                        return;
                    }
                    System.Data.DataRow row = this.tempDataTable.Rows[0];
                    row.BeginEdit();
                    row["ID"] = M_ID;
                    row["WORKNO"] = WorkNo;
                    row["OTDate"] = OTDate;
                    row["BeginTime"] = dtTempBeginTime;
                    row["EndTime"] = dtTempEndTime;
                    row["Hours"] = strOTHours;
                    row["OTType"] = tmpOTType;
                    row["WorkDesc"] = MoveSpecailChar(this.textBoxWorkDesc.Text);
                    row["Remark"] = tmpRemark;
                    row["OTMSGFlag"] = OTMSGFlag;
                    row["UPDATE_USER"] = CurrentUserInfo.Personcode;
                    row["ApplyDate"] = DateTime.Parse(this.textBoxApplyDate.Text.Trim().ToString()).ToString(this.dateFormat);
                    if (this.HiddenState.Value == "3")
                        row["Status"] = "0";
                    row["OTShiftNo"] = ShiftNo;
                    row.EndEdit();
                    this.tempDataTable.AcceptChanges();
                    wfaddt.SaveData(this.ProcessFlag.Value, this.tempDataTable,logmodel);                   
                }

                if (this.ProcessFlag.Value == "Add")
                {
                    this.WriteMessage(1, Message.kqm_addsessn);
                    Add();
                }
                else
                {
                    Response.Write("<script type='text/javascript'>alert('" + Message.SaveSuccess + "'); window.parent.document.all.ButtonQuery.click();</script>");
                }
            }
            catch (System.Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('" + ex.Message + "')</script>");
                //this.WriteMessage(2, (ex.InnerException == null ? ex.Message : ex.InnerException.Message));
            }
        }
        #endregion

        protected bool CheckData(string txtValue, string ReValue)
        {
            if (string.IsNullOrEmpty(txtValue))
            {
                this.WriteMessage(1, ReValue + Message.Required);
                return false;
            }
            return true;
        }

        public static string MoveSpecailChar(string str)
        {
            str = str.Trim().Replace("'", "");
            return str;
        }

        #region Query and data bounding and textBox/button reset function
        private void TextBoxsReset(string buttonText, bool read)
        {
            //alterable

            this.textBoxLocalName.BorderStyle = System.Web.UI.WebControls.BorderStyle.None;
            this.textBoxPersonType.BorderStyle = System.Web.UI.WebControls.BorderStyle.None;
            this.textBoxDPcode.BorderStyle = System.Web.UI.WebControls.BorderStyle.None;
            this.textBoxMonthAllHours.BorderStyle = System.Web.UI.WebControls.BorderStyle.None;
            this.textBoxWeek.BorderStyle = System.Web.UI.WebControls.BorderStyle.None;
            this.textBoxApplyDate.BorderStyle = System.Web.UI.WebControls.BorderStyle.None;
            this.textBoxOTType.BorderStyle = System.Web.UI.WebControls.BorderStyle.None;
            this.textBoxHours.BorderStyle = System.Web.UI.WebControls.BorderStyle.None;

            this.textBoxEmployeeNo.BorderStyle = BorderStyle.None;
            this.textBoxOTDate.BorderStyle = read ? BorderStyle.None : BorderStyle.NotSet;
            this.textBoxWorkDesc.BorderStyle = read ? BorderStyle.None : BorderStyle.NotSet;
            this.textBoxBeginTime.BorderStyle = read ? BorderStyle.None : BorderStyle.NotSet;
            this.textBoxEndTime.BorderStyle = read ? BorderStyle.None : BorderStyle.NotSet;

            if (buttonText.Equals("Add"))
            {
                this.textBoxOTDate.Text = "";
                this.textBoxWorkDesc.Text = "";
                this.textBoxBeginTime.Text = "";
                this.textBoxEndTime.Text = "";
                this.textBoxHours.Text = "";
                this.textBoxEmployeeNo.BorderStyle = BorderStyle.NotSet;
            }
        }

        private void EmpQuery(string EmployeeNo, string ID)
        {
            string condition = "WHERE a.WorkNO='" + EmployeeNo.ToUpper() + "'";
            //if (this.bPrivileged)
            //{
            //    condition += " AND exists (SELECT 1 FROM (" + this.sqlDep + ") e where e.DepCode=a.DCode)";
            //}
           // condition += " AND exists (SELECT 1 FROM (" + this.sqlDep + ") e where e.DepCode=a.DCode)";
            this.tempDataTable = wfaddt.GetVDataByCondition(condition).Tables["V_Employee"];
            if (this.tempDataTable.Rows.Count > 0)
            {
                foreach (System.Data.DataRow newRow in tempDataTable.Rows)
                {
                    this.textBoxLocalName.Text = newRow["LOCALNAME"].ToString();
                    this.textBoxDPcode.Text = newRow["DName"].ToString();
                    this.textBoxPersonType.Text = newRow["OverTimeType"].ToString();
                }
                GetMonthOverTime(EmployeeNo.ToUpper());
                Query(EmployeeNo, ID);
            }
            else
            {
                this.textBoxLocalName.Text = "";
                this.textBoxDPcode.Text = "";
                this.textBoxPersonType.Text = "";

                this.textBoxOTDate.Text = "";
                this.textBoxWorkDesc.Text = "";
                this.textBoxBeginTime.Text = "";
                this.textBoxEndTime.Text = "";
                this.textBoxHours.Text = "";

                TextBoxsReset("", true);
                this.WriteMessage(1, Message.EmpBasicInfoNotExist);
            }
            tempDataTable.Clear();
        }
        private void Query(string EmployeeNo, string ID)
        {
            string condition = "and a.WorkNO='" + EmployeeNo.ToUpper() + "' AND a.ID='" + ID + "' and a.isProject='Y' ";
            //if (this.bPrivileged)
            //{
            //    condition += " AND exists (SELECT 1 FROM (" + this.sqlDep + ") e where e.DepCode=b.DCode)";
            //}
            //condition += " AND exists (SELECT 1 FROM (" + this.sqlDep + ") e where e.DepCode=b.DCode)";
            this.dataSet = wfaddt.GetDataByCondition_1(condition);
            if (this.dataSet.Tables["OTM_AdvanceApply"].Rows.Count > 0)
            {

                TextDataBind(true);
            }
            else
            {
                TextDataBind(false);
            }
            this.WriteMessage(0, Message.common_message_trans_complete);
        }

        private void GetMonthOverTime(string EmployeeNo)
        {
            string Month = System.DateTime.Today.Month.ToString();
            if (Month.Length == 1)
                Month = "0" + Month;
            string YearMonth = System.DateTime.Today.Year.ToString() + Month;
            string condition = "WHERE WorkNO='" + EmployeeNo + "' AND YearMonth='" + YearMonth + "'";
            //this.tempDataTable.Clear();

            this.tempDataTable = wfaddt.GetMonthAllOverTime(condition).Tables["OTM_MonthDetail"];
            if (this.tempDataTable.Rows.Count > 0)
            {
                this.textBoxMonthAllHours.Text = tempDataTable.Rows[0]["MonthTotal"].ToString();
            }
            else
            {
                this.textBoxMonthAllHours.Text = "0";
            }
        }
        //textbox default value
        private void TextDataBind(bool read)
        {
            if (read)
            {
                try
                {
                    if (this.dataSet.Tables["OTM_AdvanceApply"].Rows[0]["OTDate"].ToString().Trim() != "")
                    {
                        this.textBoxOTDate.Text = DateTime.Parse(this.dataSet.Tables["OTM_AdvanceApply"].Rows[0]["OTDate"].ToString()).ToString(dateFormat);
                    }
                    else
                    {
                        this.textBoxOTDate.Text = "";
                    }
                    this.textBoxWeek.Text = this.dataSet.Tables["OTM_AdvanceApply"].Rows[0]["Week"].ToString();

                    //this.textBoxOTType.Text = this.dataSet.Tables["OTM_AdvanceApply"].Rows[0]["OTType"].ToString();

                    #region 龍華總部周邊HR。G2，G3名稱更改為G2(休息日上班)，G3(法定假日上班) 
                    string LHZBIsDisplayG2G3 = "";
                    try
                    {
                        LHZBIsDisplayG2G3 = System.Configuration.ConfigurationManager.AppSettings["LHZBIsDisplayG2G3"];
                    }
                    catch
                    {
                        LHZBIsDisplayG2G3 = "N";
                    }

                    if (LHZBIsDisplayG2G3 == "Y")
                    {
                        if (this.dataSet.Tables["OTM_AdvanceApply"].Rows[0]["OTType"].ToString() == "G2")
                        {
                            this.textBoxOTType.Text = this.dataSet.Tables["OTM_AdvanceApply"].Rows[0]["OTType"].ToString() + "(" + this.GetResouseValue("otm.g2.remark") + ")";
                        }
                        else if (this.dataSet.Tables["OTM_AdvanceApply"].Rows[0]["OTType"].ToString() == "G3")
                        {
                            this.textBoxOTType.Text = this.dataSet.Tables["OTM_AdvanceApply"].Rows[0]["OTType"].ToString() + "(" + this.GetResouseValue("otm.g3.remark") + ")";
                        }
                        else
                        {
                            this.textBoxOTType.Text = this.dataSet.Tables["OTM_AdvanceApply"].Rows[0]["OTType"].ToString();
                        }
                    }
                    else
                    {
                        this.textBoxOTType.Text = this.dataSet.Tables["OTM_AdvanceApply"].Rows[0]["OTType"].ToString();
                    }
                    #endregion


                    DateTime begin = Convert.ToDateTime(this.dataSet.Tables["OTM_AdvanceApply"].Rows[0]["BeginTime"].ToString());
                    this.textBoxBeginTime.Text = begin.ToString("HH:mm");
                    DateTime end = Convert.ToDateTime(this.dataSet.Tables["OTM_AdvanceApply"].Rows[0]["EndTime"].ToString());
                    this.textBoxEndTime.Text = end.ToString("HH:mm");
                    this.textBoxHours.Text = this.dataSet.Tables["OTM_AdvanceApply"].Rows[0]["Hours"].ToString();
                    this.textBoxWorkDesc.Text = this.dataSet.Tables["OTM_AdvanceApply"].Rows[0]["WorkDesc"].ToString();
                    this.HiddenState.Value = this.dataSet.Tables["OTM_AdvanceApply"].Rows[0]["Status"].ToString();
                    if (this.dataSet.Tables["OTM_AdvanceApply"].Rows[0]["ApplyDate"].ToString().Trim() != "")
                    {
                        this.textBoxApplyDate.Text = DateTime.Parse(this.dataSet.Tables["OTM_AdvanceApply"].Rows[0]["ApplyDate"].ToString()).ToString(dateFormat);
                    }
                    else
                    {
                        this.textBoxApplyDate.Text = System.DateTime.Today.ToString(dateFormat);
                    }
                    switch (this.dataSet.Tables["OTM_AdvanceApply"].Rows[0]["Status"].ToString())
                    {
                        //申請狀態
                        case "1":
                        case "2":
                            this.ButtonSave.Enabled = false;
                            break;
                    }
                }
                catch (System.Exception ex)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('" + ex.Message + "')</script>");
                }
            }
            else
            {

                this.textBoxOTDate.Text = "";
                this.textBoxWeek.Text = "";
                this.textBoxOTType.Text = "";
                this.textBoxBeginTime.Text = "";
                this.textBoxEndTime.Text = "";
                this.textBoxHours.Text = "";
                this.textBoxWorkDesc.Text = "";

            }
        }

        private void OninitOverTimeDate()
        {
            this.textBoxOTDate.Text = System.DateTime.Today.ToShortDateString();
            this.textBoxOTDate.Attributes.Add("formater", dateFormat);
        }

        //獲取加班異常信息
        private string Get_OTMSGFlag(string WorkNo, string OtDate, double OtHours, string StatusNo)
        {
            string tmpSql = "";
            string nwFlag = "";
            string sMonthLMT = "";

            tmpSql = "select NoWorkFlag,MonthLMT from GDS_ATT_CONFIG";
            this.tempDataTable = wfaddt.GetDataSetBySQL(tmpSql).Tables["TempTable"];

            if (tempDataTable.Rows.Count > 0)
            {
                nwFlag = tempDataTable.Rows[0]["NoWorkFlag"].ToString().Trim();
                sMonthLMT = tempDataTable.Rows[0]["MonthLMT"].ToString().Trim();
            }
            if (nwFlag == "Y")
            {
                tmpSql = "select OnDutyTime,OffDutyTime from GDS_ATT_KAOQINDATA where KqDate>=to_date('" + OtDate +
                    "','yyyy/mm/dd')-6 and KqDate<=to_date('" + OtDate + "','yyyy/mm/dd')-1 and WorkNo='" + WorkNo + "'";
                this.tempDataTable.Clear();
                this.tempDataTable = wfaddt.GetDataSetBySQL(tmpSql).Tables["TempTable"];

                if (tempDataTable.Rows.Count >= 6)
                {
                    bool isNoWork = false;
                    foreach (DataRow dr in tempDataTable.Rows)
                    {
                        if (dr[0].ToString().Trim() == "")
                            isNoWork = true;
                    }
                    if (!isNoWork)
                        return "1";
                }
            }

            //判斷是否大于本月加班上限			
            double dMonthLMT = 0;
            double OtMonHours = 0;
            if (sMonthLMT != "")
                dMonthLMT = double.Parse(sMonthLMT);
            if (dMonthLMT > 0 && (StatusNo == "G1" || StatusNo == "G2"))
            {
                tmpSql = "select nvl(sum(ConfirmHours),0) from GDS_ATT_REALAPPLY where to_char(OTDate,'yyyymm')=to_char(to_date('" + OtDate + "','yyyy/mm/dd'),'yyyymm') and  WorkNo='" + WorkNo + "'";
                OtMonHours = double.Parse(getValue(tmpSql));
                if (OtMonHours + OtHours > dMonthLMT)
                    return "2";
                //return "2本月已加班" + OtMonHours.ToString() + "H+預報時數" + OtHours.ToString() + "H>管控上限" + dMonthLMT.ToString() + "H";
            }

            double G12MLimit = 0;
            double G1MLimit = 0;
            double G2MLimit = 0;
            //Sql="select nvl(G12MLimit,0) as G12MLimit,(nvl(G12MLimit,0)-nvl(G2MLimit,0)) as G1MLimit,nvl(G2MLimit,0) as G2MLimit from Ot_Type where OvtTypeNo='"+OtTypeNo+"'";
            tmpSql = "select nvl(G12MLimit,0) as G12MLimit,(nvl(G12MLimit,0)-nvl(G2MLimit,0)) as G1MLimit,nvl(G2MLimit,0) as G2MLimit " +
                " from GDS_ATT_TYPE a,GDS_ATT_employee b where (b.dcode like a.orgcode || '%') and a.OTTypeCode=b.OverTimeType and workNo='" + WorkNo + "' order by a.orgcode desc";
            this.tempDataTable.Clear();
            this.tempDataTable = wfaddt.GetDataSetBySQL(tmpSql).Tables["TempTable"];
            if (tempDataTable.Rows.Count > 0)
            {
                if (tempDataTable.Rows[0]["G12MLimit"].ToString().Trim() != "")
                    G12MLimit = double.Parse(tempDataTable.Rows[0]["G12MLimit"].ToString());
                if (tempDataTable.Rows[0]["G1MLimit"].ToString().Trim() != "")
                    G1MLimit = double.Parse(tempDataTable.Rows[0]["G1MLimit"].ToString());
                if (tempDataTable.Rows[0]["G2MLimit"].ToString().Trim() != "")
                    G2MLimit = double.Parse(tempDataTable.Rows[0]["G2MLimit"].ToString());
            }

            //判斷是否大于本月G1+G2加班上限
            if (StatusNo == "G1" || StatusNo == "G2")
            {
                if (G12MLimit > 0)
                {
                    tmpSql = "select nvl(sum(confHours),0) from GDS_ATT_REALAPPLY where to_char(OTDate,'yyyymm')=to_char(to_date('" + OtDate + "','yyyy/mm/dd'),'yyyymm') and (StatusNo='G1' or StatusNo='G2') and WorkNo='" + WorkNo + "'";
                    OtMonHours = double.Parse(getValue(tmpSql));
                    if (OtMonHours + OtHours > G12MLimit)
                        return "3";
                    //return "3本月已加班" + OtMonHours.ToString() + "H+預報時數" + OtHours.ToString() + "H>管控上限" + G12MLimit.ToString() + "H";
                }
            }

            //判斷是否大于本月G1加班上限
            if (StatusNo == "G1")
            {
                if (G1MLimit > 0)
                {
                    tmpSql = "select nvl(sum(confHours),0) from GDS_ATT_REALAPPLY where to_char(OTDate,'yyyymm')=to_char(to_date('" + OtDate + "','yyyy/mm/dd'),'yyyymm') and StatusNo='G1' and WorkNo='" + WorkNo + "'";
                    sMonthLMT = getValue(tmpSql);
                    if (sMonthLMT != "")
                        OtMonHours = double.Parse(sMonthLMT);
                    else
                        OtMonHours = 0;
                    if (OtMonHours + OtHours > G1MLimit)
                        return "4";
                    //return "4本月G1已加班" + OtMonHours.ToString() + "H+預報時數" + OtHours.ToString() + "H>管控上限" + G1MLimit.ToString() + "H";
                }
            }
            //判斷是否大于本月G2加班上限
            if (StatusNo == "G2")
            {
                if (G2MLimit > 0)
                {
                    tmpSql = "select nvl(sum(confHours),0) from GDS_ATT_REALAPPLY where to_char(OTDate,'yyyymm')=to_char(to_date('" + OtDate + "','yyyy/mm/dd'),'yyyymm') and StatusNo='G2' and WorkNo='" + WorkNo + "'";
                    sMonthLMT = getValue(tmpSql);
                    if (sMonthLMT != "")
                        OtMonHours = double.Parse(sMonthLMT);
                    else
                        OtMonHours = 0;
                    if (OtMonHours + OtHours > G2MLimit)
                        return "5";
                    //return "5本月G2已加班" + OtMonHours.ToString() + "H+預報時數" + OtHours.ToString() + "H>管控上限" + G2MLimit.ToString() + "H";
                }
            }
            //判斷是否大于每天管控加班上限
            double dayMaxOtHours = GetMaxOtHours(WorkNo, StatusNo);
            if (dayMaxOtHours > 0 && OtHours > dayMaxOtHours)
                return "6";
            //return "6已超過當日管控上限" + dayMaxOtHours.ToString() + "H";
            return "0";
        }

        private double GetMaxOtHours(string WorkNo, string StatusNo)
        {
            double MaxHours = 0;

            string strSql = "select " + StatusNo + "DLIMIT " +
                " from GDS_ATT_TYPE a,GDS_ATT_employee b where (b.dcode like a.orgcode || '%') and a.OTTypeCode=b.OverTimeType and workNo='" + WorkNo + "' order by a.orgcode desc";
            this.tempDataTable.Clear();
            this.tempDataTable = wfaddt.GetDataSetBySQL(strSql).Tables["TempTable"];
            if (tempDataTable.Rows.Count > 0 && tempDataTable.Rows[0][0].ToString() != "")
                MaxHours = double.Parse(tempDataTable.Rows[0][0].ToString());
            else
                return 0;


            return MaxHours;
        }


        private string getValue(string tmpSql)
        {
            string rValue = "";

            this.tempDataTable.Clear();
            this.tempDataTable = wfaddt.GetDataSetBySQL(tmpSql).Tables["TempTable"];
            if (tempDataTable.Rows.Count > 0)
            {
                rValue = tempDataTable.Rows[0][0].ToString();
            }

            return rValue;
        }

        protected void textBoxEmployeeNo_TextChanged(object sender, EventArgs e)
        {
           // OverTimeBll bll = new OverTimeBll();
            if (!string.IsNullOrEmpty(textBoxEmployeeNo.Text.Trim()) && !string.IsNullOrEmpty(textBoxApplyDate.Text.Trim()))
            {
                DataTable dt = wfaddt.GetEmpinfo(textBoxEmployeeNo.Text.Trim().ToUpper());
                if (dt != null && dt.Rows.Count > 0)
                {
                    textBoxLocalName.Text = dt.Rows[0]["LOCALNAME"].ToString();
                    textBoxDPcode.Text = dt.Rows[0]["DEPNAME"].ToString();
                    textBoxPersonType.Text = dt.Rows[0]["OverTimeType"].ToString();
                }
                else
                {
                    this.WriteMessage(1, "'" + textBoxEmployeeNo.Text + "'" + Message.EmpNoNotExist);
                    textBoxPersonType.Text = string.Empty;
                    textBoxEmployeeNo.Text = string.Empty;
                    textBoxLocalName.Text = string.Empty;
                    textBoxDPcode.Text = string.Empty;
                    //textBoxApplyDate.Text = string.Empty;
                    textBoxMonthAllHours.Text = string.Empty;
                    return;
                }          
                textBoxApplyDate.Text = DateTime.Now.ToShortDateString();
                GetMonthOverTime(textBoxEmployeeNo.Text.Trim());
            }
        }

        protected void textBoxOTDate_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBoxOTDate.Text))
            {
                DateTime dt = Convert.ToDateTime(textBoxOTDate.Text);
                textBoxWeek.Text = GetWeekCh(dt);
                OverTimeBll bll = new OverTimeBll();

                if (!string.IsNullOrEmpty(textBoxEmployeeNo.Text))
                {
                    textBoxOTType.Text = wfaddt.GetOTType(textBoxEmployeeNo.Text.Trim(), textBoxOTDate.Text.Trim());                    
                }
                else
                {
                    this.WriteMessage(1, Message.WorkNoFirst);
                    textBoxEmployeeNo.Focus();
                    textBoxWeek.Text = string.Empty;
                    textBoxOTType.Text = string.Empty;
                    textBoxOTDate.Text = string.Empty;
                }
            }
        }

        private string GetWeekCh(DateTime dt)
        {
            int i = (int)dt.DayOfWeek;
            string xingqi = "";
            switch (i)
            {
                case 0:
                    xingqi = Message.common_sunday;
                    break;
                case 1:
                    xingqi = Message.common_monday;
                    break;
                case 2:
                    xingqi = Message.common_tuesday;
                    break;
                case 3:
                    xingqi = Message.common_wednesday;
                    break;
                case 4:
                    xingqi = Message.common_thursday;
                    break;
                case 5:
                    xingqi = Message.common_friday;
                    break;
                case 6:
                    xingqi = Message.common_saturday;
                    break;

            }
            return xingqi;
        }
        #endregion

        #region Ajax

        public string GetOTHours(string WorkNo, string OTDate, string BeginTime, string EndTime, string OTType)
        {
            double hours = 0;
            string LHZBIsDisplayG2G3 = "";
            if (OTType.Length == 0)
            {
                OTType = FindOTType(OTDate, WorkNo);
                #region 龍華總部周邊HR。G2，G3名稱更改為G2(休息日上班)，G3(法定假日上班) update by xukai 20111010
                try
                {
                    LHZBIsDisplayG2G3 = System.Configuration.ConfigurationManager.AppSettings["LHZBIsDisplayG2G3"];
                }
                catch
                {
                    LHZBIsDisplayG2G3 = "N";
                }
                if (LHZBIsDisplayG2G3 == "Y")
                {
                    OTType = OTType.Substring(0, 2);
                }
                #endregion
            }
            //Function CommonFun = new Function();
            if (BeginTime != EndTime)
            {
                //hours = CommonFun.GetOtHours(WorkNo.ToUpper(), OTDate, BeginTime, EndTime, OTType);
                hours = wfaddt.GetOtHours(WorkNo.ToUpper(), OTDate, BeginTime, EndTime, OTType);
            }
            return hours.ToString();
        }

        public string FindOTType(string OTDate, string WorkNo)
        {
            string LHZBIsDisplayG2G3 = "";
            string OtStatus = "";
            OtStatus = wfaddt.GetOTType(WorkNo, OTDate);

            #region 龍華總部周邊HR。G2，G3名稱更改為G2(休息日上班)，G3(法定假日上班) update by xukai 20111010
            try
            {
                LHZBIsDisplayG2G3 = System.Configuration.ConfigurationManager.AppSettings["LHZBIsDisplayG2G3"];
            }
            catch
            {
                LHZBIsDisplayG2G3 = "N";
            }

            if (LHZBIsDisplayG2G3 == "Y")
            {
                if (OtStatus == "G2")
                {
                    return OtStatus + "(" + Message.OtmG2Remark + ")";
                }
                else if (OtStatus == "G3")
                {
                    return OtStatus + "(" + Message.OtmG3Remark + ")";
                }
                else
                {
                    return OtStatus;
                }
            }
            else
            {
                return OtStatus;
            }
            #endregion
        }

        public string Get_MonthOverTime(string EmployeeNo)
        {
            string Month = System.DateTime.Today.Month.ToString();
            if (Month.Length == 1)
                Month = "0" + Month;
            string YearMonth = System.DateTime.Today.Year.ToString() + Month;
            string condition = "WHERE WorkNO='" + EmployeeNo + "' AND YearMonth='" + YearMonth + "'";
            this.tempDataTable = wfaddt.GetMonthAllOverTime(condition).Tables["OTM_MonthDetail"];
            string monthtotal = "0";
            if (this.tempDataTable.Rows.Count > 0)
            {
                monthtotal = tempDataTable.Rows[0]["MonthTotal"].ToString();
            }
            return monthtotal;

        }

        public DataSet GetEmp(string EmployeeNo, string strsqlDep, string sPrivileged)
        {
            tempDataSet = new DataSet();
            tempDataSet.Clear();
            string condition = "WHERE a.WorkNO='" + EmployeeNo.ToUpper() + "' and a.status='0'";
            if (sPrivileged.Equals("True"))
            {
                condition += " AND a.dcode IN(" + strsqlDep + ")";
            }
            this.tempDataSet = wfaddt.GetVDataByCondition(condition);
            return tempDataSet;
        }

        public string GetShiftDeac(string EmployeeNo, string OTDate)
        {
            string ShiftNo = "";
            try
            {
                Convert.ToDateTime(OTDate);
                ShiftNo = wfaddt.GetShiftNo(EmployeeNo, OTDate);
                if (ShiftNo == null || ShiftNo == "")
                {
                    return "";
                }
                else
                {
                    string strsql = "select ShiftDesc from (select shiftno||shiftdesc||'['||(select dataValue from GDS_ATT_TYPEDATA b where b.datatype='ShiftType' and b.datacode=a.shifttype)||'],'||case when PMRestStime>' ' then PMRestStime||'~'||PMRestEtime end ShiftDesc from GDS_ATT_WORKSHIFT a where shiftno ='" + ShiftNo + "')";
                    ShiftNo = wfaddt.GetValue(strsql);
                }
            }
            catch (System.Exception ex)
            {
                return "";
            }
            return ShiftNo;
        }

        public string GetBeginTime(string OTType, string ShiftNo)
        {
            string BeginTime = "";
            if (ShiftNo.Length > 4)
            {
                ShiftNo = ShiftNo.Substring(0, 4);
                try
                {
                    if (OTType.EndsWith("G1"))
                    {
                        BeginTime = wfaddt.GetValue("select NVL(PMRESTETIME,OFFDUTYTIME) from GDS_ATT_WORKSHIFT where shiftno ='" + ShiftNo + "'");
                    }
                    else
                    {
                        BeginTime = wfaddt.GetValue("select ONDUTYTIME from GDS_ATT_WORKSHIFT where shiftno ='" + ShiftNo + "'");
                    }
                }
                catch (System.Exception ex)
                {
                    return "";
                }
            }

            return BeginTime;
        }

        public string GetOTMFlag(string WorkNo, string OtDate)
        {
            return wfaddt.GetOTMFlag(WorkNo, OtDate);
        }

        public string GetOTMSGFlag(string WorkNo, string OTDate, string OTHours, string IsProject, string ID)
        {
            string tmpOTType = wfaddt.GetOTType(WorkNo, OTDate);

            // Function CommonFun = new Function();

            //獲取加班異常信息
            string OTMSGFlag = wfaddt.GetOTMSGFlag(WorkNo, OTDate, Convert.ToDouble(OTHours), tmpOTType, IsProject, ID);
            string tmpRemark = "";
            if (OTMSGFlag != "")
            {
                tmpRemark = OTMSGFlag.Substring(1, OTMSGFlag.Length - 1);
            }
            return tmpRemark;
        }

        #endregion

        public bool CheckLeaveOverTime(string WorkNo, string StartDate, string StartTime, string EndDate, string EndTime)
        {
            string startTime = Convert.ToDateTime(StartDate + " " + StartTime).ToString("yyyy/MM/dd HH:mm");
            string endTime = Convert.ToDateTime(EndDate + " " + EndTime).ToString("yyyy/MM/dd HH:mm");
            string condition = "";
            condition = "SELECT WorkNo from GDS_ATT_LEAVEAPPLY a where a.WorkNo='" + WorkNo + "'  and a.EndDate>=to_date('" + StartDate + "','yyyy/mm/dd')  AND ((to_date(to_char(a.StartDate,'yyyy/mm/dd')||a.StartTime,'yyyy/mm/dd hh24:mi') <= to_date('" + startTime + "','yyyy/mm/dd hh24:mi') AND to_date(to_char(a.EndDate,'yyyy/mm/dd')||a.EndTime,'yyyy/mm/dd hh24:mi') > to_date('" + startTime + "','yyyy/mm/dd hh24:mi')) or (to_date(to_char(a.StartDate,'yyyy/mm/dd')||a.StartTime,'yyyy/mm/dd hh24:mi') < to_date('" + endTime + "','yyyy/mm/dd hh24:mi') AND to_date(to_char(a.EndDate,'yyyy/mm/dd')||a.EndTime,'yyyy/mm/dd hh24:mi') >= to_date('" + endTime + "','yyyy/mm/dd hh24:mi')) or (to_date(to_char(a.StartDate,'yyyy/mm/dd')||a.StartTime,'yyyy/mm/dd hh24:mi') >= to_date('" + startTime + "','yyyy/mm/dd hh24:mi') AND to_date(to_char(a.EndDate,'yyyy/mm/dd')||a.EndTime,'yyyy/mm/dd hh24:mi') <= to_date('" + endTime + "','yyyy/mm/dd hh24:mi')))";
            //this.tempDataTable = ((ServiceLocator)this.Session["serviceLocator"]).GetFunctionData().GetDataSetBySQL(condition).Tables["TempTable"];
            this.tempDataTable = wfaddt.GetDataSetBySQL(condition).Tables["TempTable"];
            if (this.tempDataTable.Rows.Count > 0)
            {
                return false;
            }
            return true;
        }

        public bool CheckG3WorkTime(string WorkNo, string OTDate, string BeginTime, string EndTime, string OTType)
        {
            if (OTType.Equals("G3") && ((TimeSpan.Parse(Convert.ToDateTime(BeginTime).ToString("HH:mm")) > TimeSpan.Parse(Convert.ToDateTime(EndTime).ToString("HH:mm"))) && !Convert.ToDateTime(EndTime).ToString("HH:mm").Equals("00:00")))
            {
                //string strOTType = ((ServiceLocator)this.Session["serviceLocator"]).GetFunctionData().GetOTType(WorkNo, Convert.ToDateTime(OTDate).AddDays(1.0).ToString("yyyy/MM/dd"));
                string strOTType = wfaddt.GetOTType(WorkNo, Convert.ToDateTime(OTDate).AddDays(1.0).ToString("yyyy/MM/dd"));
                if (strOTType.Equals("G1") || strOTType.Equals("G2"))
                {
                    return false;
                }
            }
            return true;
        }

        public bool CheckWorkTime(string WorkNo, string OTDate, string BeginTime, string EndTime, string ShiftNo)
        {
            string ShiftType = "";
            string sOffDutyTime = "";
            string OffDutyTime = "";
           // string OtStatus = ((ServiceLocator)this.Session["serviceLocator"]).GetFunctionData().GetOTType(WorkNo, Convert.ToDateTime(OTDate).ToString("yyyy/MM/dd"));
            string OtStatus = wfaddt.GetOTType(WorkNo, Convert.ToDateTime(OTDate).ToString("yyyy/MM/dd"));
            //this.tempDataTable = ((ServiceLocator)this.Session["serviceLocator"]).GetFunctionData().GetDataSetBySQL("select a.ShiftType,a.OffDutyTime,nvl(a.PMRestETime,a.OffDutyTime) RestTime from KQM_WorkShift a where ShiftNo='" + ShiftNo + "'").Tables["TempTable"];
            this.tempDataTable = wfaddt.GetDataSetBySQL("select a.ShiftType,a.OffDutyTime,nvl(a.PMRestETime,a.OffDutyTime) RestTime from GDS_ATT_WORKSHIFT a where ShiftNo='" + ShiftNo + "'").Tables["TempTable"];
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
                //this.tempDataTable = ((ServiceLocator)this.Session["serviceLocator"]).GetFunctionData().GetDataSetBySQL(tmpSql).Tables["TempTable"];
                this.tempDataTable = wfaddt.GetDataSetBySQL(tmpSql).Tables["TempTable"];
                if (this.tempDataTable.Rows.Count > 0)
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
            this.tempDataTable = wfaddt.GetDataSetBySQL(condition).Tables["TempTable"];
            //this.tempDataTable = ((ServiceLocator)this.Session["serviceLocator"]).GetFunctionData().GetDataSetBySQL(condition).Tables["TempTable"];
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
                //this.tempDataTable = ((ServiceLocator)this.Session["serviceLocator"]).GetOTMAdvanceApplyData().GetDataByCondition(condition).Tables["OTM_AdvanceApply"];
                this.tempDataTable = wfaddt.GetApplyDataByCondition(condition).Tables["OTM_AdvanceApply"];
                if (this.tempDataTable.Rows.Count > 0)
                {
                    return false;
                }
                return true;
            }
            condition = " and a.WorkNo='" + WorkNo + "' and a.otdate>=to_date('" + OTDate + "','yyyy/mm/dd')-1  and ((a.BeginTime<=To_Date('" + Convert.ToDateTime(BeginTime).ToString("yyyy/MM/dd HH:mm") + "','yyyy/mm/dd hh24:mi')  and a.EndTime>To_Date('" + Convert.ToDateTime(BeginTime).ToString("yyyy/MM/dd HH:mm") + "','yyyy/mm/dd hh24:mi')) or (a.BeginTime<To_Date('" + Convert.ToDateTime(EndTime).ToString("yyyy/MM/dd HH:mm") + "','yyyy/mm/dd hh24:mi')  and a.EndTime>=To_Date('" + Convert.ToDateTime(EndTime).ToString("yyyy/MM/dd HH:mm") + "','yyyy/mm/dd hh24:mi')) or(a.BeginTime >= to_date('" + Convert.ToDateTime(BeginTime).ToString("yyyy/MM/dd HH:mm") + "','yyyy/mm/dd hh24:mi')  and a.EndTime <= to_date('" + Convert.ToDateTime(EndTime).ToString("yyyy/MM/dd HH:mm") + "','yyyy/mm/dd hh24:mi'))) ";
            //this.tempDataTable = ((ServiceLocator)this.Session["serviceLocator"]).GetOTMExceptionApplyData().GetDataByCondition(condition).Tables["OTM_RealApply"];
            this.tempDataTable = wfaddt.GetRealDataByCondition(condition).Tables["OTM_RealApply"];
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
                //this.tempDataTable = ((ServiceLocator)this.Session["serviceLocator"]).GetOTMAdvanceApplyData().GetDataByCondition(condition).Tables["OTM_AdvanceApply"];
                this.tempDataTable = wfaddt.GetApplyDataByCondition(condition).Tables["OTM_AdvanceApply"];
                if (this.tempDataTable.Rows.Count > 0)
                {
                    return false;
                }
                return true;
            }
            condition = " and a.id<>'" + ID + "' and  a.WorkNo='" + WorkNo + "' and a.otdate>=to_date('" + OTDate + "','yyyy/mm/dd')-1  and ((a.BeginTime<=To_Date('" + Convert.ToDateTime(BeginTime).ToString("yyyy/MM/dd HH:mm") + "','yyyy/mm/dd hh24:mi')  and a.EndTime>To_Date('" + Convert.ToDateTime(BeginTime).ToString("yyyy/MM/dd HH:mm") + "','yyyy/mm/dd hh24:mi')) or (a.BeginTime<To_Date('" + Convert.ToDateTime(EndTime).ToString("yyyy/MM/dd HH:mm") + "','yyyy/mm/dd hh24:mi')  and a.EndTime>=To_Date('" + Convert.ToDateTime(EndTime).ToString("yyyy/MM/dd HH:mm") + "','yyyy/mm/dd hh24:mi')) or(a.BeginTime >= to_date('" + Convert.ToDateTime(BeginTime).ToString("yyyy/MM/dd HH:mm") + "','yyyy/mm/dd hh24:mi')  and a.EndTime <= to_date('" + Convert.ToDateTime(EndTime).ToString("yyyy/MM/dd HH:mm") + "','yyyy/mm/dd hh24:mi'))) ";
            //this.tempDataTable = ((ServiceLocator)this.Session["serviceLocator"]).GetOTMExceptionApplyData().GetDataByCondition(condition).Tables["OTM_RealApply"];
            this.tempDataTable = wfaddt.GetRealDataByCondition(condition).Tables["OTM_RealApply"];
            if (this.tempDataTable.Rows.Count > 0)
            {
                return false;
            }
            return true;
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

    }
}
