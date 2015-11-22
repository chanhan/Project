using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using GDSBG.MiABU.Attendance.Model.WorkFlow;
using GDSBG.MiABU.Attendance.BLL.WorkFlow;
using System.Data;
using System.Collections;
using System.Resources;
using Infragistics.WebUI.WebSchedule;
using Resources;
using System.Web.Script.Serialization;
using System.Web.Profile;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;

namespace GDSBG.MiABU.Attendance.Web.WorkFlowForm
{
    public partial class OTMAdvanceApplyEditForm : BasePage
    {

        protected DataTable tempDataTable;

        private OverTimeBll bll = new OverTimeBll();
        Dictionary<string, string> ClientMessage = null;
        JavaScriptSerializer JsSerializer = new JavaScriptSerializer();
        public string WorkNo;
        private string ShiftNo;
        static SynclogModel logmodel = new SynclogModel();

        protected void Page_Load(object sender, EventArgs e)
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
            if (!IsPostBack)
            {
                logmodel.ProcessOwner = CurrentUserInfo.Personcode;
                logmodel.TransactionType = (Request.QueryString["ModuleCode"] == null) ? "" : Request.QueryString["ModuleCode"].ToString();
                logmodel.LevelNo = "2";
                logmodel.FromHost = Request.UserHostAddress;

                string EmployeeNo = this.Request.QueryString["EmployeeNo"] == null ? "" : this.Request.QueryString["EmployeeNo"].ToString();
                string ID = this.Request.QueryString["ID"] == null ? "" : this.Request.QueryString["ID"].ToString();
                string ProcessFlag = this.Request.QueryString["ProcessFlag"] == null ? "" : this.Request.QueryString["ProcessFlag"].ToString();
                this.HiddenID.Value = ID;
                this.ProcessFlag.Value = ProcessFlag;
                this.textBoxApplyDate.Text = System.DateTime.Today.ToShortDateString();
                #region xukai 2011-10-17  是否調休數據加載
                this.ddlIsMoveLeave.Items.Insert(0, new ListItem("N", "N"));
                this.ddlIsMoveLeave.Items.Insert(1, new ListItem("Y", "Y"));
                this.UserLabelIsMoveLeave.Visible = false;
                this.ddlIsMoveLeave.Visible = false;
                #endregion

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
            SetCalendar(textBoxOTDate);
            string isShowMoveLeaveFlag = "Y";
            if (isShowMoveLeaveFlag.Equals("Y"))
            {
                #region  是否調休顯示
                this.UserLabelIsMoveLeave.Visible = true;
                this.ddlIsMoveLeave.Visible = true;

                this.labelPlanAdjust.Visible = false;
                this.textBoxPlanAdjust.Visible = false;

                #endregion
            }
        }

        protected void Modify(string EmployeeNo, string ID)
        {
            this.textBoxEmployeeNo.Text = EmployeeNo;
            EmpQuery(EmployeeNo, ID);
            this.ProcessFlag.Value = "Modify";
            this.TextBoxsReset("Modify", false);
        }

        protected void Add()
        {
            if (this.HiddenSave.Value != "Save")
            {
                this.textBoxEmployeeNo.Text = "";
            }
            this.ProcessFlag.Value = "Add";
            this.TextBoxsReset("Add", false);
        }

        private void EmpQuery(string EmployeeNo, string ID)
        {
            OverTimeBll bll=new OverTimeBll();
            DataTable dt= bll.GetOverTimeInfo(ID);

            if (dt != null && dt.Rows.Count > 0)
            {
                textBoxEmployeeNo.Text = dt.Rows[0]["WORKNO"].ToString();
                textBoxLocalName.Text = dt.Rows[0]["LOCALNAME"].ToString();
                textBoxDPcode.Text = dt.Rows[0]["DEPNAME"].ToString();
                textBoxPersonType.Text = dt.Rows[0]["OVERTIMETYPE"].ToString();

                if (dt.Rows[0]["APPLYDATE"].ToString() != string.Empty)
                {
                    textBoxApplyDate.Text = Convert.ToDateTime(dt.Rows[0]["APPLYDATE"].ToString()).ToShortDateString();
                }

                GetMonthOverTime(EmployeeNo);
                if (dt.Rows[0]["OTDATE"].ToString() != string.Empty)
                {
                    textBoxOTDate.Text = Convert.ToDateTime(dt.Rows[0]["OTDATE"].ToString()).ToShortDateString();
                }
               
                textBoxWeek.Text = dt.Rows[0]["WEEK"].ToString();
                textBoxOTType.Text = dt.Rows[0]["OTTYPE"].ToString();
                if (dt.Rows[0]["BEGINTIME"].ToString() != string.Empty)
                {
                    textBoxBeginTime.Text = Convert.ToDateTime(dt.Rows[0]["BEGINTIME"].ToString()).ToString("HH:mm");
                }
                if (dt.Rows[0]["ENDTIME"].ToString() != string.Empty)
                {
                    textBoxEndTime.Text = Convert.ToDateTime(dt.Rows[0]["ENDTIME"].ToString()).ToString("HH:mm");
                }
               
                textBoxHours.Text = dt.Rows[0]["HOURS"].ToString();
                textBoxWorkDesc.Text = dt.Rows[0]["WORKDESC"].ToString();
                ddlIsMoveLeave.SelectedValue = dt.Rows[0]["G2ISFORREST"].ToString();
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
                //    this.WriteMessage(1, this.GetResouseValue("bfw.hrm_no_employee_info"));
            }
           

            //string condition = "WHERE a.WorkNO='" + EmployeeNo.ToUpper() + "'";
            //if (this.bPrivileged)
            //{
            //    condition += " AND exists (SELECT 1 FROM (" + this.sqlDep + ") e where e.DepCode=a.DCode)";
            //}
            //this.tempDataTable = ((eBFW.Sys.ServiceLocator)Session["serviceLocator"]).GetFunctionData().GetVDataByCondition(condition).Tables["V_Employee"];
            //if (this.tempDataTable.Rows.Count > 0)
            //{
            //    foreach (System.Data.DataRow newRow in tempDataTable.Rows)
            //    {
            //        this.textBoxLocalName.Text = newRow["LOCALNAME"].ToString();
            //        this.textBoxDPcode.Text = newRow["DName"].ToString();
            //        this.textBoxPersonType.Text = newRow["OverTimeType"].ToString();
            //    }
            //    GetMonthOverTime(EmployeeNo.ToUpper());
            //    Query(EmployeeNo, ID);
            //}
            //else
            //{
            //   
            //}
            //tempDataTable.Clear();
        }

        private void GetMonthOverTime(string EmployeeNo)
        {
            string Month = System.DateTime.Today.Month.ToString();
            if (Month.Length == 1)
                Month = "0" + Month;
            string YearMonth = System.DateTime.Today.Year.ToString() + Month;
            string condition = "WHERE WorkNO='" + EmployeeNo + "' AND YearMonth='" + YearMonth + "'";
            
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
                this.textBoxEmployeeNo.BorderStyle = BorderStyle.NotSet;
                this.textBoxEmployeeNo.Text = "";
                this.textBoxLocalName.Text = "";
                this.textBoxDPcode.Text = "";
                this.textBoxPersonType.Text = "";
                this.textBoxMonthAllHours.Text = "";
                this.textBoxHours.Text = "";
                this.textBoxOTDate.Text = "";
                this.textBoxWeek.Text = "";
                this.textBoxOTType.Text = "";
                this.textBoxBeginTime.Text = "";
                this.textBoxEndTime.Text = "";
                this.textBoxHours.Text = "";
                this.textBoxWorkDesc.Text = "";
            }
        }


        protected bool CheckData(string txtValue, string ReValue)
        {
            if (string.IsNullOrEmpty(txtValue))
            {
                this.WriteMessage(1, ReValue + Message.Required);
                return false;
            }
            return true;
        }

        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            //List<OverTimeModel> list = new List<OverTimeModel>();
            //OverTimeModel mod = new OverTimeModel();
            //mod.Workno = textBoxEmployeeNo.Text.Trim();
            //mod.Applydate = Convert.ToDateTime(textBoxApplyDate.Text.Trim());
            //mod.Otdate = Convert.ToDateTime(textBoxOTDate.Text.Trim());
            //mod.Ottype = textBoxOTType.Text.Trim();
            //mod.Begintime = Convert.ToDateTime(textBoxBeginTime.Text.Trim());
            //mod.Endtime = Convert.ToDateTime(textBoxEndTime.Text.Trim());
            //mod.Hours = Convert.ToInt32(textBoxHours.Text.Trim());
            //mod.Workdesc = textBoxWorkDesc.Text.Trim();
            //mod.G2isforrest = ddlIsMoveLeave.SelectedValue;
            //mod.Otshiftno = textBoxPersonType.Text;
            //mod.Status = "0";
            //list.Add(mod);
            //string ProcessFlag = this.Request.QueryString["ProcessFlag"] == null ? "" : this.Request.QueryString["ProcessFlag"].ToString();
            //string ID = this.Request.QueryString["ID"] == null ? "" : this.Request.QueryString["ID"].ToString();
            //OverTimeBll bll = new OverTimeBll();
            //if (ProcessFlag == "Add")
            //{

            //    if (bll.SaveData(list, ProcessFlag, ID))
            //    {
            //        Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('保存成功！');</script>");
            //    }
            //    else
            //    {
            //        Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('保存失敗！');</script>");
            //    }
            //}
            //else 
            //{
            //    if (bll.SaveData(list, ProcessFlag, ID))
            //    {
            //        Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('保存成功！');</script>");
            //    }
            //    else
            //    {
            //        Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('保存失敗！');</script>");
            //    }
            //}


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
               // Function CommonFun = new Function();
                try
                {
                    OTDate = DateTime.Parse(this.textBoxOTDate.Text.ToString()).ToString("yyyy/MM/dd");
                }
                catch (System.Exception)
                {
                    this.WriteMessage(1, Message.common_message_data_errordate);
                    return;
                }
                if (!this.textBoxOTType.Equals("G2"))
                {
                    this.textBoxPlanAdjust.Text = "";
                }
                WorkNo = this.textBoxEmployeeNo.Text.ToUpper();
                string StrBtime = this.textBoxBeginTime.Text.ToString();
                string StrEtime = this.textBoxEndTime.Text.ToString();
                DateTime dtTempBeginTime = DateTime.Parse(OTDate + " " + StrBtime);
                DateTime dtTempEndTime = DateTime.Parse(OTDate + " " + StrEtime);
                //DateTime dtMidTime = DateTime.Parse(OTDate + " 12:00");
                //string IsAllowModifyOTReal = ((eBFW.Sys.ServiceLocator)Session["serviceLocator"]).GetFunctionData().GetValue("select nvl(max(paravalue),'N') from bfw_parameter where paraname='IsAllowModifyOTReal'");

                //助理先取消
                //#region 每天16:30后不讓預報加班,非工作日也不能預報加班  龍華總部周邊 update by xukai 20111014
                //string LHZBLimitTime = "";
                //string LHZBIsLimitApply = "";
                //string workflag = "";
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

                //if (!string.IsNullOrEmpty(LHZBLimitTime))
                //{
                //    //每天LHZBLimitTime時間后不能新增、修改加班預報
                //    DateTime dtLimit = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd") + " " + LHZBLimitTime);
                //    if (dtLimit < DateTime.Now)
                //    {
                //        this.WriteMessage(1, this.GetResouseValue("kqm.otm.advanceapply") + LHZBLimitTime + this.GetResouseValue("kqm.otm.advanceapply.error"));
                //        return;
                //    }
                //}

                //if (LHZBIsLimitApply == "Y")
                //{
                //    //非工作日不能預報加班
                //    workflag = ((eBFW.Sys.ServiceLocator)Session["serviceLocator"]).GetFunctionData().GetEmpWorkFlag(WorkNo, DateTime.Now.ToString("yyyy-MM-dd"));
                //    if (workflag == "N")
                //    {
                //        this.WriteMessage(1, this.GetResouseValue("kqm.otm.advanceapply.workflag"));
                //        return;
                //    }
                //}
                //#endregion
                string OTMAdvanceBeforeDays = bll.GetValue("select nvl(max(paravalue),'2') from GDS_SC_PARAMETER where paraname='OTMAdvanceBeforeDays'");
                string appUserIsIn = bll.GetValue("select nvl(max(workno),'Y') from GDS_ATT_employee where workno='" + CurrentUserInfo.Personcode + "'");
                if (!appUserIsIn.Equals("Y"))//允許申報當前日期以前多少天的加班除去非工作日
                {
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
                condition = "select trunc(sysdate)-to_date('" + OTDate + "','yyyy/mm/dd') from dual";
                strtemp = bll.GetValue(condition);
                if (Convert.ToDouble(strtemp) > (Convert.ToDouble(OTMAdvanceBeforeDays)))
                {
                    this.WriteMessage(1, Message.otm_message_checkadvancedaysbefore + ":" + OTMAdvanceBeforeDays);
                    return;
                }
                string tmpOTType = FindOTType(OTDate, WorkNo);
                if (tmpOTType.Length == 0)
                {
                    this.WriteMessage(1, Message.OTMType + Message.Required);
                    return;
                }

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
                ShiftNo = bll.GetShiftNo(WorkNo, OTDate);
                if (ShiftNo == null || ShiftNo == "")
                {
                    this.WriteMessage(1, Message.otm_exception_errorshiftno_1);
                    return;
                }
                SortedList list = new SortedList();
                list = bll.ReturnOTTTime(WorkNo, OTDate, dtTempBeginTime, dtTempEndTime, ShiftNo);
                dtTempBeginTime = Convert.ToDateTime(list.GetByIndex(list.IndexOfKey("A")));
                dtTempEndTime = Convert.ToDateTime(list.GetByIndex(list.IndexOfKey("B")));

                if (this.ProcessFlag.Value.Equals("Add"))
                {
                    //一天内多笔加班时间不能交叉或重复
                    if (!CheckOverTime(WorkNo, OTDate, dtTempBeginTime.ToString("yyyy/MM/dd HH:mm"), dtTempEndTime.ToString("yyyy/MM/dd HH:mm"), ShiftNo, true))
                    {
                        this.WriteMessage(1, Message.common_message_otm_multi_repeart);
                        return;
                    }
                }
                else
                {
                    //一天内多笔加班时间不能交叉或重复
                    if (!CheckOverTime(this.HiddenID.Value, WorkNo, OTDate, dtTempBeginTime.ToString("yyyy/MM/dd HH:mm"), dtTempEndTime.ToString("yyyy/MM/dd HH:mm"), ShiftNo, true))
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

                //工作時間內不能預報加班
                if (!CheckWorkTime(WorkNo, OTDate, dtTempBeginTime.ToString("yyyy/MM/dd HH:mm"), dtTempEndTime.ToString("yyyy/MM/dd HH:mm"), ShiftNo))
                {
                    this.WriteMessage(1, Message.common_message_otm_worktime_repeart);
                    return;
                }

                //G3加班后一天不允許跨天申報Modify by Jackzhang2011/4/2
                if (!CheckG3WorkTime(WorkNo, OTDate, dtTempBeginTime.ToString("yyyy/MM/dd HH:mm"), dtTempEndTime.ToString("yyyy/MM/dd HH:mm"), tmpOTType))
                {
                    this.WriteMessage(1, Message.kqm_otm_g3kuantian);//加班時間段內含有正常工作時間，零點以后不允許申報加班
                    return;
                }

                if (Convert.ToDouble(strOTHours) < 0.5)
                {
                    this.WriteMessage(1, Message.otm_othourerror);
                    return;
                }

                //判斷相同時段是否有參加教育訓練
                if (!CheckOTOverETM(WorkNo, dtTempBeginTime.ToString("yyyy/MM/dd HH:mm"), dtTempEndTime.ToString("yyyy/MM/dd HH:mm")))
                {
                    this.WriteMessage(1, Message.common_message_otm_etmrepeart);
                    return;
                }
                //獲取加班異常信息
                string OTMSGFlag = GetOTMSGFlag(WorkNo, OTDate, Convert.ToDouble(strOTHours), tmpOTType, "N", this.HiddenID.Value);               
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
                    //if (!CommonFun.CheckOverTime(WorkNo, OTDate, dtTempBeginTime.ToString("yyyy/MM/dd HH:mm"), dtTempEndTime.ToString("yyyy/MM/dd HH:mm"), ShiftNo, true))
                    //{
                    //    this.WriteMessage(1, this.GetResouseValue("common.message.otm.multi.repeart"));
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
                    row["ApplyDate"] = DateTime.Parse(this.textBoxApplyDate.Text.Trim().ToString()).ToString("yyyy/MM/dd");
                    row["IsProject"] = "N";
                    row["Status"] = "0";
                    row["OTShiftNo"] = ShiftNo;
                    row["UPDATE_USER"] = CurrentUserInfo.Personcode;
                   
                    if (this.textBoxPlanAdjust.Text.Length == 0)
                    {
                        row["PlanAdjust"] = DBNull.Value;
                    }
                    else
                    {
                        row["PlanAdjust"] = this.textBoxPlanAdjust.Text;
                    }

                    #region xukai  2011-10-17  G2加班是否用於調休
                    string g2isforrest = "";
                    //是否顯示是否調休
                    string isShowMoveLeaveFlag = bll.GetValue("select nvl(max(paravalue),'N') from GDS_SC_PARAMETER where paraname='IsShowMoveLeaveFlag'");
                    if (isShowMoveLeaveFlag.Equals("Y"))
                    {
                        if (tmpOTType.Equals("G2"))
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
                }
                else if (ProcessFlag.Value.Equals("Modify"))
                {
                    //一天内多笔加班时间不能交叉或重复
                    //if (!CommonFun.CheckOverTime(this.HiddenID.Value, WorkNo, OTDate, dtTempBeginTime.ToString("yyyy/MM/dd HH:mm"), dtTempEndTime.ToString("yyyy/MM/dd HH:mm"), ShiftNo, true))
                    //{
                    //    this.WriteMessage(1, this.GetResouseValue("common.message.otm.multi.repeart"));
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
                    row["ApplyDate"] = DateTime.Parse(this.textBoxApplyDate.Text.Trim().ToString()).ToString("yyyy/MM/dd");
                    if (this.HiddenState.Value == "3")
                        row["Status"] = "0";
                    row["OTShiftNo"] = ShiftNo;
                    row["UPDATE_USER"] = CurrentUserInfo.Personcode;
                    if (this.textBoxPlanAdjust.Text.Length == 0)
                    {
                        row["PlanAdjust"] = DBNull.Value;
                    }
                    else
                    {
                        row["PlanAdjust"] = this.textBoxPlanAdjust.Text;
                    }
                    #region xukai  2011-10-17  G2加班是否用於調休
                    string g2isforrest = "";
                    //是否顯示是否調休
                    string isShowMoveLeaveFlag = bll.GetValue("select nvl(max(paravalue),'N') from GDS_SC_PARAMETER where paraname='IsShowMoveLeaveFlag'");
                    if (isShowMoveLeaveFlag.Equals("Y"))
                    {
                        if (tmpOTType.Equals("G2"))
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
                    this.WriteMessage(0, Message.SaveSuccess);
                }

                if (this.ProcessFlag.Value == "Add")
                {
                    if (tmpRemark.Length > 0)
                    {
                        this.WriteMessage(1, Message.kqm_addsessn + ":" + tmpRemark);
                    }
                    else
                    {
                        this.WriteMessage(1, Message.kqm_addsessn);
                    }
                    Add();
                }
                else
                {
                
                   Response.Write("<script type='text/javascript'>window.parent.document.all.btnQuery.click();</script>");
                 
                }
            }
            catch (System.Exception ex)
            {
                this.WriteMessage(2, (ex.InnerException == null ? ex.Message : ex.InnerException.Message));
            }
        }

        public  string MoveSpecailChar(string str)
        {
            str = str.Trim().Replace("'", "");
            return str;
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
            DataTable  tempDataTable = bll.GetDataSetBySQL(condition).Tables["TempTable"];
            if (tempDataTable.Rows.Count > 0)
            {
                return false;
            }
            return true;
        }



        protected void ButtonReturn_Click(object sender, EventArgs e)
        {

        }

        protected void textBoxEmployeeNo_TextChanged(object sender, EventArgs e)
        {
            OverTimeBll bll = new OverTimeBll();
            if (!string.IsNullOrEmpty(textBoxEmployeeNo.Text.Trim()) && !string.IsNullOrEmpty(textBoxApplyDate.Text.Trim()))
            {
                DataTable dt = bll.GetEmpinfo(textBoxEmployeeNo.Text.Trim().ToUpper());
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
                //textBoxPersonType.Text = bll.GetShiftNo(textBoxEmployeeNo.Text.Trim(), textBoxApplyDate.Text.Trim());
                //if (string.IsNullOrEmpty(textBoxPersonType.Text))
                //{
                //    this.WriteMessage(1, textBoxEmployeeNo.Text+"沒有排班,請先排班");
                //    textBoxEmployeeNo.Text = string.Empty;
                //    textBoxLocalName.Text = string.Empty;
                //    textBoxDPcode.Text = string.Empty;
                //   // textBoxApplyDate.Text = string.Empty;
                //    textBoxMonthAllHours.Text = string.Empty;
                //    return;
                //}
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




        public string GetOTHours(string WorkNo, string OTDate, string BeginTime, string EndTime, string OTType)
        {
            double hours = 0;
            string LHZBIsDisplayG2G3 = "";
            if (OTType.Length == 0)
            {
                OTType = FindOTType(OTDate, WorkNo);
                #region 龍華總部周邊HR。G2，G3名稱更改為G2(休息日上班)，G3(法定假日上班) 
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
            OverTimeBll bll = new OverTimeBll();
            if (BeginTime != EndTime)
            {
                hours = bll.GetOtHours(WorkNo.ToUpper(), OTDate, BeginTime, EndTime, OTType);
            }
            return hours.ToString();
        }
       
        public string FindOTType(string OTDate, string WorkNo)
        {
            OverTimeBll bll = new OverTimeBll();
            string LHZBIsDisplayG2G3 = "";
            string OtStatus = "";
            OtStatus = bll.GetOTType(WorkNo, OTDate);
            #region 龍華總部周邊HR。G2，G3名稱更改為G2(休息日上班)，G3(法定假日上班) 
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
    }
}
