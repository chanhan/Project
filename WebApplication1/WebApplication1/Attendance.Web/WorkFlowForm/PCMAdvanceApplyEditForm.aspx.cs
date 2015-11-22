using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GDSBG.MiABU.Attendance.BLL.WorkFlow;
using System.Collections;
using System.Data;
using System.Resources;
using Infragistics.WebUI.WebSchedule;
using Resources;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;
using System.Web.Script.Serialization;

namespace GDSBG.MiABU.Attendance.Web.WorkFlowForm
{
    public partial class PCMAdvanceApplyEditForm : BasePage
    {
        protected System.Data.DataSet dataSet, tempDataSet;
        OverTimeBll bll = new OverTimeBll();
        protected System.Data.DataTable tempDataTable;
        public string WorkNo;
        private string ShiftNo;
        static SynclogModel logmodel = new SynclogModel();

        Dictionary<string, string> ClientMessage = null;
        JavaScriptSerializer JsSerializer = new JavaScriptSerializer();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (ClientMessage == null)
                {
                    ClientMessage = new Dictionary<string, string>();
                    ClientMessage.Add("WorkNoFirst", Message.WorkNoFirst);

                    ClientMessage.Add("otm_nowshiftno", Message.otm_nowshiftno);
                    ClientMessage.Add("overtimebeforesex", Message.overtimebeforesex);
                    //ClientMessage.Add("otm_nowshiftno", Message.otm_nowshiftno);

                    ClientMessage.Add("otm_exception_errorshiftno_1", Message.otm_exception_errorshiftno_1);
                    ClientMessage.Add("EmpBasicInfoNotExist", Message.EmpBasicInfoNotExist);
                 

                    string clientmsg = JsSerializer.Serialize(ClientMessage);
                    Page.ClientScript.RegisterStartupScript(GetType(), "ClientMessage", "var Message=" + clientmsg + ";", true);
                }
                if (!this.IsPostBack)
                {

                    logmodel.ProcessOwner = CurrentUserInfo.Personcode;
                    logmodel.TransactionType = (Request.QueryString["ModuleCode"] == null) ? "" : Request.QueryString["ModuleCode"].ToString();
                    logmodel.LevelNo = "2";
                    logmodel.FromHost = Request.UserHostAddress;

                   // this.Internationalization();
                    this.ddlIsProject.Items.Insert(0, new ListItem("N", "N"));
                    this.ddlIsProject.Items.Insert(1, new ListItem("Y", "Y"));
                  
                    this.ddlIsMoveLeave.Items.Insert(0, new ListItem("N", "N"));
                    this.ddlIsMoveLeave.Items.Insert(1, new ListItem("Y", "Y"));
                    this.UserLabelIsMoveLeave.Visible = false;
                    this.ddlIsMoveLeave.Visible = false;
                   
                    string EmployeeNo = this.Request.QueryString["EmployeeNo"] == null ? "" : this.Request.QueryString["EmployeeNo"].ToString();
                    string ID = this.Request.QueryString["ID"] == null ? "" : this.Request.QueryString["ID"].ToString();
                    string ProcessFlag = this.Request.QueryString["ProcessFlag"] == null ? "" : this.Request.QueryString["ProcessFlag"].ToString();
                    this.HiddenID.Value = ID;
                    this.ProcessFlag.Value = ProcessFlag;
                    this.textBoxApplyDate.Text = System.DateTime.Today.ToString("yyyy/MM/dd");
                    //this.textBoxApplyDate.Attributes.Add("formater", dateFormat);
                    if (ProcessFlag == "Add")
                    {
                        this.HiddenID.Value = "";
                        //選擇申請單需要通知的窗口
                        //修改d.ManagerCode='0003'為員工表中獲取管理職為“無”的字段
                        string condition = "select a.personcode,a.personcode||'['||a.cname||']'||'-'||c.depname localname from   " +
                        "GDS_SC_PERSON a,GDS_SC_AUTHORITY b,GDS_SC_department c,GDS_ATT_employee d " +
                            //"where a.rolecode=b.rolecode and a.depcode=c.depcode and a.personcode=d.workno and d.flag='Local' and d.status='0' and d.ManagerCode='0003' " +
                        "where a.rolecode=b.rolecode and a.depcode=c.depcode and a.personcode=d.workno and d.flag='Local' and d.status='0' and d.ManagerCode in (select managercode from GDS_ATT_MANAGER where managername = '無' and EffectFlag='Y') " +
                        "and b.modulecode='OTMSYS101' " +
                        "and a.depcode in(SELECT DepCode FROM GDS_SC_department  e   " +
                        "START WITH e.depCode =(select dcode from GDS_ATT_employee where workno='" + CurrentUserInfo.Personcode + "')  " +
                        "CONNECT BY e.depcode = PRIOR e.parentdepcode) and a.deleted='N' " +
                        "and exists(select 1 from GDS_SC_role e where e.rolecode=a.rolecode and  e.acceptmsg='Y') " +
                        "order by a.depcode desc";
                        tempDataSet = bll.GetDataSetBySQL(condition);
                        this.ddlWindow.DataSource = tempDataSet.Tables[0].DefaultView;
                        this.ddlWindow.DataTextField = "localname";
                        this.ddlWindow.DataValueField = "personcode";
                        this.ddlWindow.DataBind();
                        this.ddlWindow.Items.Insert(0, new ListItem("", ""));
                        Add();
                        this.textBoxEmployeeNo.Text = CurrentUserInfo.Personcode;
                        setvalue(textBoxEmployeeNo.Text);
                        this.divSystemMSG.Visible = false;
                    }
                    else
                    {
                        this.divSystemMSG.Visible = false;
                        if (EmployeeNo.Length > 0 && ID.Length > 0)
                        {
                            Modify(EmployeeNo, ID);
                        }
                        else
                        {
                            Response.Write("<script type='text/javascript'>alert(\"" + Message.AtLastOneChoose + "\");window.parent.document.all.topTable.style.display='';window.parent.document.all.divEdit.style.display='none';</script>");
                        }
                    }
                    
                    string IsAllowPersonProject = bll.GetValue("select nvl(max(paravalue),'Y') from GDS_SC_PARAMETER where paraname='IsAllowPersonProject'");
                    if (IsAllowPersonProject.Equals("N"))
                    {
                        this.UserLabelIsProject.Visible = false;
                        this.ddlIsProject.Visible = false;
                    }
                    
                   
                    string isShowMoveLeaveFlag = bll.GetValue("select nvl(max(paravalue),'N') from GDS_SC_PARAMETER where paraname='IsShowMoveLeaveFlag'");
                    if (isShowMoveLeaveFlag.Equals("Y"))
                    {
                        
                        this.UserLabelIsMoveLeave.Visible = true;
                        this.ddlIsMoveLeave.Visible = true;
                        // this.ddlIsMoveLeave.Enabled = true;
                       
                    }
                   
                    //this.textBoxEmployeeNo.Attributes.Add("onblur", "GetEmp();");
                    //this.textBoxOTDate.Attributes.Add("onpropertychange", "javascript:WeekDate();");
                    //this.textBoxOTDate.Attributes.Add("onafterpaste", "javascript:GetLVTotal();");
                    //this.textBoxOTDate.Attributes.Add("onfocus", "calendar('" + Language + "','" + dateFormat + "');");
                    //this.textBoxOTDate.Attributes.Add("formater", dateFormat);
                    //this.textBoxBeginTime.Attributes.Add("onfocus", "timecalendar('" + Language + "');");
                    //this.textBoxBeginTime.Attributes.Add("onpropertychange", "javascript:GetLVTotal();");
                    //this.textBoxEndTime.Attributes.Add("onfocus", "timecalendar('" + Language + "');");
                   // this.textBoxEndTime.Attributes.Add("onpropertychange", "javascript:GetLVTotal();");
                }
                SetCalendar(textBoxOTDate);

            }
            catch (System.Exception ex)
            {
                this.WriteMessage(2, (ex.InnerException == null ? ex.Message : ex.InnerException.Message));
            }
           // AjaxPro.Utility.RegisterTypeForAjax(typeof(PCM_PCMAdvanceApplyEdit));
        }

        protected void Add()
        {
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

        private void setvalue(string EmployeeNo)
        {

            string condition1 = "WHERE a.WorkNO='" + EmployeeNo.ToUpper() + "'";
            this.tempDataTable = bll.GetVDataByCondition(condition1).Tables["V_Employee"];
            if (this.tempDataTable.Rows.Count > 0)
            {
                foreach (System.Data.DataRow newRow in tempDataTable.Rows)
                {
                    this.textBoxLocalName.Text = newRow["LOCALNAME"].ToString();
                    this.textBoxDPcode.Text = newRow["DName"].ToString();
                    this.textBoxPersonType.Text = newRow["OverTimeType"].ToString();
                }
                GetMonthOverTime(EmployeeNo.ToUpper());
                textBoxOTDate.Text = DateTime.Today.ToShortDateString();
                DateTime dt = Convert.ToDateTime(textBoxOTDate.Text);
                textBoxWeek.Text = GetWeekCh(dt);
                if (!string.IsNullOrEmpty(textBoxEmployeeNo.Text))
                {
                    textBoxOTType.Text = bll.GetOTType(textBoxEmployeeNo.Text.Trim(), textBoxOTDate.Text.Trim());
                    if (textBoxOTType.Text == "G1" || textBoxOTType.Text == "G3")
                    {
                        ddlIsMoveLeave.Enabled = false;
                    }
                    else
                    {
                        ddlIsMoveLeave.Enabled = true;
                    }
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

        private void EmpQuery(string EmployeeNo, string ID)
        {
            string condition = "WHERE a.WorkNO='" + EmployeeNo.ToUpper() + "'";
            this.tempDataTable = bll.GetVDataByCondition(condition).Tables["V_Employee"];
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

        private void GetMonthOverTime(string EmployeeNo)
        {
            string Month = System.DateTime.Today.Month.ToString();
            if (Month.Length == 1)
                Month = "0" + Month;
            string YearMonth = System.DateTime.Today.Year.ToString() + Month;
            string condition = "WHERE WorkNO='" + EmployeeNo + "' AND YearMonth='" + YearMonth + "'";
            this.tempDataTable.Clear();
            this.tempDataTable = bll.GetMonthAllOverTime(condition).Tables["OTM_MonthDetail"];
            if (this.tempDataTable.Rows.Count > 0)
            {
                this.textBoxMonthAllHours.Text = tempDataTable.Rows[0]["MonthTotal"].ToString();
            }
            else
            {
                this.textBoxMonthAllHours.Text = "0";
            }
        }

        private void Query(string EmployeeNo, string ID)
        {
            string condition = "and a.WorkNO='" + EmployeeNo.ToUpper() + "' AND a.ID='" + ID + "'";
            this.dataSet = bll.GetDataByCondition_1(condition);
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
        private void TextDataBind(bool read)
        {
            if (read)
            {
                try
                {
                    if (this.dataSet.Tables["OTM_AdvanceApply"].Rows[0]["OTDate"].ToString().Trim() != "")
                    {
                        this.textBoxOTDate.Text = DateTime.Parse(this.dataSet.Tables["OTM_AdvanceApply"].Rows[0]["OTDate"].ToString()).ToString("yyyy/MM/dd");
                    }
                    else
                    {
                        this.textBoxOTDate.Text = "";
                    }
                    this.textBoxWeek.Text = this.dataSet.Tables["OTM_AdvanceApply"].Rows[0]["Week"].ToString();
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
                            this.textBoxOTType.Text = this.dataSet.Tables["OTM_AdvanceApply"].Rows[0]["OTType"].ToString() + "(" + "休息日上班" + ")";
                        }
                        else if (this.dataSet.Tables["OTM_AdvanceApply"].Rows[0]["OTType"].ToString() == "G3")
                        {
                            this.textBoxOTType.Text = this.dataSet.Tables["OTM_AdvanceApply"].Rows[0]["OTType"].ToString() + "(" + "法定假日上班" + ")"; ;
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
                    if (this.dataSet.Tables["OTM_AdvanceApply"].Rows[0]["ApplyDate"].ToString().Length > 0)
                    {
                        this.textBoxApplyDate.Text = DateTime.Parse(this.dataSet.Tables["OTM_AdvanceApply"].Rows[0]["ApplyDate"].ToString()).ToString("yyyy/MM/dd");
                    }
                    else
                    {
                        this.textBoxApplyDate.Text = System.DateTime.Today.ToString("yyyy/MM/dd");
                    }
                    this.ddlIsProject.SelectedIndex = ddlIsProject.Items.IndexOf(ddlIsProject.Items.FindByValue(this.dataSet.Tables["OTM_AdvanceApply"].Rows[0]["IsProject"].ToString()));

                    #region   G2加班是否用於調休
                    //string g2isforrest = "";
                    //是否顯示是否調休
                    string isShowMoveLeaveFlag = bll.GetValue("select nvl(max(paravalue),'N') from GDS_SC_PARAMETER where paraname='IsShowMoveLeaveFlag'");
                    if (isShowMoveLeaveFlag.Equals("Y"))
                    {
                        this.ddlIsMoveLeave.SelectedIndex = ddlIsMoveLeave.Items.IndexOf(ddlIsMoveLeave.Items.FindByValue(this.dataSet.Tables["OTM_AdvanceApply"].Rows[0]["G2ISFORREST"].ToString()));
                        if (this.dataSet.Tables["OTM_AdvanceApply"].Rows[0]["OTType"].ToString() != "G2")
                        {
                            this.ddlIsMoveLeave.Enabled = false;
                        }
                        //總部周邊加班類型是L的，如果G2加班，直接調休 update by xukai 20111024
                        if (this.dataSet.Tables["OTM_AdvanceApply"].Rows[0]["OTType"].ToString().Equals("G2") && this.dataSet.Tables["OTM_AdvanceApply"].Rows[0]["OverTimeType"].ToString().IndexOf("L") != -1)
                        {
                            this.ddlIsMoveLeave.SelectedValue = "Y";
                            this.ddlIsMoveLeave.Enabled = false;
                        }
                    }
                    #endregion
                    this.HiddenIsProject.Value = this.dataSet.Tables["OTM_AdvanceApply"].Rows[0]["IsProject"].ToString();
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
                    this.WriteMessage(2, (ex.InnerException == null ? ex.Message : ex.InnerException.Message));
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

            }
        }

        private void OninitOverTimeDate()
        {
            this.textBoxOTDate.Text = System.DateTime.Today.ToShortDateString();
           // this.textBoxOTDate.Attributes.Add("formater", "yyyy/MM/dd");
        }
        private double GetMaxOtHours(string WorkNo, string StatusNo)
        {
            double MaxHours = 0;

            string strSql = "select " + StatusNo + "DLIMIT " +
                " from GDS_ATT_TYPE a,GDS_ATT_employee b where (b.dcode like a.orgcode || '%') and a.OTTypeCode=b.OverTimeType and workNo='" + WorkNo + "' order by a.orgcode desc";
            this.tempDataTable.Clear();
            this.tempDataTable = bll.GetDataSetBySQL(strSql).Tables["TempTable"];
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
            this.tempDataTable = bll.GetDataSetBySQL(tmpSql).Tables["TempTable"];
            if (tempDataTable.Rows.Count > 0)
            {
                rValue = tempDataTable.Rows[0][0].ToString();
            }

            return rValue;
        }
        private bool IsNightWork(string WorkNo)
        {
            bool isNightWork = false;
            string ShiftNo = bll.GetShiftNo(WorkNo, this.textBoxOTDate.Text.Trim());

            if (ShiftNo == "")
            {
                this.WriteMessage(1, Message.otm_exception_errorshiftno);
                return false;
            }
            if (ShiftNo != "" && ShiftNo.Substring(0, 1) == "C")
            {
                isNightWork = true;
               
            }
            return isNightWork;
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

        protected void textBoxEmployeeNo_TextChanged(object sender, EventArgs e)
        {

        }
        protected bool CheckData(string txtValue, string ReValue)
        {
            if (string.IsNullOrEmpty(txtValue))
            {
                this.WriteMessage(1, ReValue + Message.NotNullOrEmpty);
                return false;
            }
            return true;
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


                #region 每天16:30后不讓預報加班,非工作日也不能預報加班  龍華總部周邊 
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
                    workflag = bll.GetEmpWorkFlag(WorkNo, DateTime.Now.ToString("yyyy-MM-dd"));
                    if (workflag == "N")
                    {
                        this.WriteMessage(1, Message.kqm_otm_advanceapply_workflag);
                        return;
                    }
                }
                #endregion

                string OTMAdvanceBeforeDays = bll.GetValue("select nvl(sum(paravalue),'2') from GDS_SC_PARAMETER where paraname='OTMAdvanceBeforeDays'");

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

                condition = "select trunc(sysdate)-to_date('" + OTDate + "','yyyy/mm/dd') from dual";
                strtemp = bll.GetValue(condition);
                if (Convert.ToDecimal(strtemp) > Convert.ToDecimal(OTMAdvanceBeforeDays))
                {
                    this.WriteMessage(1, Message.otm_message_checkadvancedaysbefore + ":" + OTMAdvanceBeforeDays);
                    return;
                }
                string tmpOTType = FindOTType(OTDate, WorkNo);
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
                    tmpOTType = tmpOTType.Substring(0, 2);
                    //G2加班 必須要自己選擇是否調休 
                    if (tmpOTType.Equals("G2"))
                    {
                        //update by xukai 20111024 總部周邊加班類型為L的，G2加班一定是調休
                        if (this.textBoxPersonType.Text.Trim().IndexOf("L") != -1)
                        {
                            this.ddlIsMoveLeave.SelectedValue = "Y"; //dropdownlist前臺disable取不到值
                        }
                        if (!this.CheckData(this.ddlIsMoveLeave.SelectedValue, Message.pcm_advanceapply_ismoveleave))
                        {
                            this.ddlIsMoveLeave.Enabled = true;
                            return;
                        }
                    }
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
                if (Convert.ToDouble(strOTHours) < 0.5)
                {
                    this.WriteMessage(1, Message.otm_othourerror);
                    return;
                }
                if (tmpOTType.Length == 0)
                {
                    this.WriteMessage(1, Message.OTMType + Message.NotNullOrEmpty);
                    return;
                }
                //抓取班別
                ShiftNo = bll.GetShiftNo(WorkNo, OTDate);
                if (ShiftNo == null || ShiftNo == "")
                {
                    this.WriteMessage(1, Message.otm_exception_errorshiftno_1);
                    return;
                }
                SortedList list = new SortedList();
                //Function CommonFun = new Function();
                list = bll.ReturnOTTTime(WorkNo, OTDate, dtTempBeginTime, dtTempEndTime, ShiftNo);
                dtTempBeginTime = Convert.ToDateTime(list.GetByIndex(list.IndexOfKey("A")));
                dtTempEndTime = Convert.ToDateTime(list.GetByIndex(list.IndexOfKey("B")));
                if (this.ProcessFlag.Value.Equals("Add"))
                {
                    //一天内多笔加班时间不能交叉或重复
                    if (!CheckOverTime(WorkNo, OTDate, dtTempBeginTime.ToString(), dtTempEndTime.ToString(), ShiftNo, true))
                    {
                        this.WriteMessage(1, Message.common_message_otm_multi_repeart);
                        return;
                    }
                }
                else
                {
                    //一天内多笔加班时间不能交叉或重复
                    if (!CheckOverTime(this.HiddenID.Value, WorkNo, OTDate, dtTempBeginTime.ToString(), dtTempEndTime.ToString(), ShiftNo, true))
                    {
                        this.WriteMessage(1, Message.common_message_otm_multi_repeart);
                        return;
                    }
                }
                //請假中不能申報加班
                if (!CheckLeaveOverTime(WorkNo, dtTempBeginTime.ToString("yyyy/MM/dd"), dtTempBeginTime.ToString("HH:mm"), dtTempEndTime.ToString("yyyy/MM/dd"), dtTempEndTime.ToString("HH:mm")))
                {
                    this.WriteMessage(1, Message.common_message_Leaveovertime_repeart);
                    return;
                }

                //G3加班后一天如果是正常上班不允許跨天申報Modify by Jackzhang2011/4/2
                if (!CheckG3WorkTime(WorkNo, OTDate, dtTempBeginTime.ToString("yyyy/MM/dd HH:mm"), dtTempEndTime.ToString("yyyy/MM/dd HH:mm"), tmpOTType))
                {
                    this.WriteMessage(1, Message.common_message_otm_worktime_checkg3);//加班時間段內含有正常工作時間，零點以后不允許申報加班
                    return;
                }

                //工作時間內不能預報加班
                if (!CheckWorkTime(WorkNo, OTDate, dtTempBeginTime.ToString(), dtTempEndTime.ToString(), ShiftNo))
                {
                    this.WriteMessage(1, Message.common_message_otm_worktime_repeart);
                    return;
                }
                //判斷相同時段是否有參加教育訓練
                if (!CheckOTOverETM(WorkNo, dtTempBeginTime.ToString("yyyy/MM/dd HH:mm"), dtTempEndTime.ToString("yyyy/MM/dd HH:mm")))
                {
                    this.WriteMessage(1, Message.common_message_otm_etmrepeart);
                    return;
                }
                if (this.ProcessFlag.Value.Equals("Add"))
                {
                    this.HiddenIsProject.Value = this.ddlIsProject.SelectedValue;
                }
                //獲取加班異常信息
                string OTMSGFlag = GetOTMSGFlag(WorkNo, OTDate, Convert.ToDouble(strOTHours), tmpOTType, this.HiddenIsProject.Value, this.HiddenID.Value);
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
                    ////一天内多笔加班时间不能交叉或重复
                    //if (!CommonFun.CheckOverTime(WorkNo, OTDate, dtTempBeginTime.ToString(), dtTempEndTime.ToString(), ShiftNo, true))
                    //{
                    //    this.WriteMessage(1, this.GetResouseValue("common.message.otm.multi.repeart"));
                    //    return;
                    //}
                    //有未核準的加班申請單據,不能再申請
                    //condition = "and a.WorkNo='" + WorkNo + "' and a.Status in ('0','1') and a.ottype='" + tmpOTType + "'";
                    //this.tempDataTable = ((eBFW.Sys.ServiceLocator)Session["serviceLocator"]).GetOTMAdvanceApplyData().GetDataByCondition(condition).Tables["KQM_LeaveApply"];
                    //if (this.tempDataTable.Rows.Count > 0)
                    //{
                    //    this.WriteMessage(1, this.GetResouseValue("otm.advanceapply.checkrepeatstatus"));
                    //    return;
                    //}

                    this.tempDataTable = bll.GetDataByCondition_1("and 1=2").Tables["OTM_AdvanceApply"];
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
                    row["UPDATE_USER"] = CurrentUserInfo.Personcode;
                    row["ApplyDate"] = DateTime.Parse(this.textBoxApplyDate.Text.Trim().ToString()).ToString("yyyy/MM/dd");
                    if (this.ddlIsProject.SelectedValue.Equals("N"))
                    {
                        row["IsProject"] = "N";
                    }
                    else
                    {
                        row["IsProject"] = "Y";
                    }
                    row["OTShiftNo"] = ShiftNo;
                    row["Status"] = "0";
                    #region   G2加班是否用於調休
                    string g2isforrest = "";
                    //是否顯示是否調休
                    string isShowMoveLeaveFlag = bll.GetValue("select nvl(max(paravalue),'N') from GDS_SC_PARAMETER where paraname='IsShowMoveLeaveFlag'");
                    if (isShowMoveLeaveFlag.Equals("Y"))
                    {
                        if (tmpOTType == "G2")
                        {
                            g2isforrest = this.ddlIsMoveLeave.SelectedValue;
                            //update by xukai 20111024 總部周邊加班類型為L的，G2加班一定是調休
                            if (this.textBoxPersonType.Text.Trim().IndexOf("L") != -1)
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
                    #endregion

                    row.EndEdit();
                    this.tempDataTable.Rows.Add(row);
                    this.tempDataTable.AcceptChanges();
                    bll.SaveData(this.ProcessFlag.Value, this.tempDataTable,logmodel);
                    this.HiddenSave.Value = "Save";

                    ////通知考勤窗口
                    //if (this.ddlWindow.SelectedValue.Length > 0)
                    //{
                    //    string RemindContent = this.textBoxDPcode.Text + "：" + WorkNo + this.textBoxLocalName.Text +
                    //        "在" + this.GetResouseValue(this.moduleCode) +
                    //        "中申請了" + this.textBoxOTDate.Text + "加班";
                    //    ((eBFW.Sys.ServiceLocator)Session["serviceLocator"]).GetFunctionData().ExcuteSQL("INSERT INTO WFM_REMIND (WORKNO, REMINDCONTENT, REMINDDATE,FLAG)VALUES('" + this.ddlWindow.SelectedValue + "','" + RemindContent + "',sysdate,'N')");
                    //}
                }
                else if (ProcessFlag.Value.Equals("Modify"))
                {
                    ////一天内多笔加班时间不能交叉或重复
                    //if (!CommonFun.CheckOverTime(this.HiddenID.Value, WorkNo, OTDate, dtTempBeginTime.ToString(), dtTempEndTime.ToString(), ShiftNo, true))
                    //{
                    //    this.WriteMessage(1, this.GetResouseValue("common.message.otm.multi.repeart"));
                    //    return;
                    //}
                    //有未核準的加班申請單據,不能再申請
                    //condition = "and a.WorkNo='" + WorkNo + "' and a.Status in ('0','1') and a.ottype='" + tmpOTType + "' and a.ID<>'" + M_ID + "'";
                    //this.tempDataTable = ((eBFW.Sys.ServiceLocator)Session["serviceLocator"]).GetOTMAdvanceApplyData().GetDataByCondition(condition).Tables["KQM_LeaveApply"];
                    //if (this.tempDataTable.Rows.Count > 0)
                    //{
                    //    this.WriteMessage(1, this.GetResouseValue("otm.advanceapply.checkrepeatstatus"));
                    //    return;
                    //}

                    this.tempDataTable = bll.GetDataByCondition_1("and a.ID='" + this.HiddenID.Value + "'").Tables["OTM_AdvanceApply"];
                    if (this.tempDataTable.Rows.Count == 0)
                    {
                        this.WriteMessage(1, Message.AtLastOneChoose);
                        return;
                    }
                    System.Data.DataRow row = this.tempDataTable.Rows[0];
                    row.BeginEdit();
                    row["ID"] = this.HiddenID.Value;
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
                    row["ApplyDate"] = DateTime.Parse(this.textBoxApplyDate.Text.Trim().ToString()).ToString("yyyy/MM/dd");
                    if (this.HiddenState.Value == "3")
                        row["Status"] = "0";
                    row["OTShiftNo"] = ShiftNo;
                    #region  G2加班是否用於調休
                    string g2isforrest = "";
                    //是否顯示是否調休
                    string isShowMoveLeaveFlag = bll.GetValue("select nvl(max(paravalue),'N') from GDS_SC_PARAMETER where paraname='IsShowMoveLeaveFlag'");
                    if (isShowMoveLeaveFlag.Equals("Y"))
                    {
                        if (tmpOTType == "G2")
                        {
                            g2isforrest = this.ddlIsMoveLeave.SelectedValue;
                            //update by xukai 20111024 總部周邊加班類型為L的，G2加班一定是調休
                            if (this.textBoxPersonType.Text.Trim().IndexOf("L") != -1)
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
                    #endregion
                    row.EndEdit();
                    this.tempDataTable.AcceptChanges();
                    bll.SaveData(this.ProcessFlag.Value, this.tempDataTable,logmodel);
                }
                if (tmpRemark.Length > 0)
                {
                    Response.Write("<script type='text/javascript'>alert('" + Message.DataSaveSuccess + ":" + tmpRemark + "');window.parent.document.all.ButtonQuery.click();</script>");
                }
                else
                {
                    Response.Write("<script type='text/javascript'>alert('" + Message.DataSaveSuccess + "');window.parent.document.all.ButtonQuery.click();</script>");
                }
            }
            catch (System.Exception ex)
            {
                this.WriteMessage(2, (ex.InnerException == null ? ex.Message : ex.InnerException.Message));
            }
        }


       

        public string GetOTMSGFlag(string WorkNo, string OtDate, double OtHours, string OTType, string IsProject, string ModifyID)
        {
            ResourceManager resourceM = new ResourceManager(typeof(Resource));
            return bll.GetOTMSGFlag(WorkNo, OtDate, OtHours, OTType, IsProject, ModifyID, resourceM);
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
            DataTable tempDataTable = bll.GetDataByCondition_2(condition).Tables["OTM_RealApply"];
            if (tempDataTable.Rows.Count > 0)
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
                this.tempDataTable = bll.GetDataByCondition_1(condition).Tables["OTM_AdvanceApply"];
                if (this.tempDataTable.Rows.Count > 0)
                {
                    return false;
                }
                return true;
            }
            condition = " and a.id<>'" + ID + "' and  a.WorkNo='" + WorkNo + "' and a.otdate>=to_date('" + OTDate + "','yyyy/mm/dd')-1  and ((a.BeginTime<=To_Date('" + Convert.ToDateTime(BeginTime).ToString("yyyy/MM/dd HH:mm") + "','yyyy/mm/dd hh24:mi')  and a.EndTime>To_Date('" + Convert.ToDateTime(BeginTime).ToString("yyyy/MM/dd HH:mm") + "','yyyy/mm/dd hh24:mi')) or (a.BeginTime<To_Date('" + Convert.ToDateTime(EndTime).ToString("yyyy/MM/dd HH:mm") + "','yyyy/mm/dd hh24:mi')  and a.EndTime>=To_Date('" + Convert.ToDateTime(EndTime).ToString("yyyy/MM/dd HH:mm") + "','yyyy/mm/dd hh24:mi')) or(a.BeginTime >= to_date('" + Convert.ToDateTime(BeginTime).ToString("yyyy/MM/dd HH:mm") + "','yyyy/mm/dd hh24:mi')  and a.EndTime <= to_date('" + Convert.ToDateTime(EndTime).ToString("yyyy/MM/dd HH:mm") + "','yyyy/mm/dd hh24:mi'))) ";
            DataTable tempDataTable = bll.GetDataByCondition_2(condition).Tables["OTM_RealApply"];
            if (tempDataTable.Rows.Count > 0)
            {
                return false;
            }
            return true;
        }

        public bool CheckLeaveOverTime(string WorkNo, string StartDate, string StartTime, string EndDate, string EndTime)
        {
            string startTime = Convert.ToDateTime(StartDate + " " + StartTime).ToString("yyyy/MM/dd HH:mm");
            string endTime = Convert.ToDateTime(EndDate + " " + EndTime).ToString("yyyy/MM/dd HH:mm");
            string condition = "";
            condition = "SELECT WorkNo from GDS_ATT_LEAVEAPPLY a where a.WorkNo='" + WorkNo + "'  and a.EndDate>=to_date('" + StartDate + "','yyyy/mm/dd')  AND ((to_date(to_char(a.StartDate,'yyyy/mm/dd')||a.StartTime,'yyyy/mm/dd hh24:mi') <= to_date('" + startTime + "','yyyy/mm/dd hh24:mi') AND to_date(to_char(a.EndDate,'yyyy/mm/dd')||a.EndTime,'yyyy/mm/dd hh24:mi') > to_date('" + startTime + "','yyyy/mm/dd hh24:mi')) or (to_date(to_char(a.StartDate,'yyyy/mm/dd')||a.StartTime,'yyyy/mm/dd hh24:mi') < to_date('" + endTime + "','yyyy/mm/dd hh24:mi') AND to_date(to_char(a.EndDate,'yyyy/mm/dd')||a.EndTime,'yyyy/mm/dd hh24:mi') >= to_date('" + endTime + "','yyyy/mm/dd hh24:mi')) or (to_date(to_char(a.StartDate,'yyyy/mm/dd')||a.StartTime,'yyyy/mm/dd hh24:mi') >= to_date('" + startTime + "','yyyy/mm/dd hh24:mi') AND to_date(to_char(a.EndDate,'yyyy/mm/dd')||a.EndTime,'yyyy/mm/dd hh24:mi') <= to_date('" + endTime + "','yyyy/mm/dd hh24:mi')))";
            DataTable tempDataTable = bll.GetDataSetBySQL(condition).Tables["TempTable"];
            if (tempDataTable.Rows.Count > 0)
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
            DataTable tempDataTable1 = bll.GetDataSetBySQL("select a.ShiftType,a.OffDutyTime,nvl(a.PMRestETime,a.OffDutyTime) RestTime from GDS_ATT_WORKSHIFT a where ShiftNo='" + ShiftNo + "'").Tables["TempTable"];
            if (tempDataTable1.Rows.Count > 0)
            {
                ShiftType = Convert.ToString(tempDataTable1.Rows[0]["ShiftType"]);
                sOffDutyTime = Convert.ToString(tempDataTable1.Rows[0]["OffDutyTime"]);
                OffDutyTime = Convert.ToString(tempDataTable1.Rows[0]["RestTime"]);
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
                DataTable tempDataTable = bll.GetDataSetBySQL(tmpSql).Tables["TempTable"];
                if (tempDataTable.Rows.Count > 0)
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
            DataTable tempDataTable = bll.GetDataSetBySQL(condition).Tables["TempTable"];
            if (tempDataTable.Rows.Count > 0)
            {
                return false;
            }
            return true;
        }


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
                hours = bll.GetOtHours(WorkNo.ToUpper(), OTDate, BeginTime, EndTime, OTType);
            }
            return hours.ToString();
        }

        public string FindOTType(string OTDate, string WorkNo)
        {
            string LHZBIsDisplayG2G3 = "";
            string OtStatus = "";
            OtStatus = bll.GetOTType(WorkNo, OTDate);
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
                    return OtStatus + "(" + Message.OtmG3Remark + ")"; ;
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

        public string MoveSpecailChar(string str)
        {
            str = str.Trim().Replace("'", "");
            return str;
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
                    textBoxOTType.Text = bll.GetOTType(textBoxEmployeeNo.Text.Trim(), textBoxOTDate.Text.Trim());
                    if (textBoxOTType.Text == "G1" || textBoxOTType.Text == "G3")
                    {
                        ddlIsMoveLeave.Enabled = false;
                    }
                    else
                    {
                        ddlIsMoveLeave.Enabled = true;
                    }
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

    }
}
