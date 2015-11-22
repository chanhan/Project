/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： OTMActivityApplyEditForm.aspx.cs
 * 檔功能描述： 免卡人員加班導入
 * 
 * 版本：1.0
 * 創建標識： 張明強 2011.12.9
 * 
 */
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;
using GDSBG.MiABU.Attendance.Model.KQM.OTM;
using GDSBG.MiABU.Attendance.BLL.KQM.OTM;
using System.Collections.Generic;
using Resources;
using System.Web.Script.Serialization;
using System.Configuration;
using System.Collections;

namespace GDSBG.MiABU.Attendance.Web.KQM.OTM
{
    public partial class OTMActivityApplyEditForm : BasePage
    {
        JavaScriptSerializer JsSerializer = new JavaScriptSerializer();
        Dictionary<string, string> ClientMessage = null;
        ActivityModel model = new ActivityModel();
        OTMActivityApplyBll activityApplyBll = new OTMActivityApplyBll();
        protected DataTable tempDataTable;
        static DataTable tempTable;
        static SynclogModel logmodel = new SynclogModel();
        # region 頁面加載方法
        /// <summary>
        /// 頁面加載方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            //PageHelper.ButtonControls(base.FuncList, pnlShowPanel.Controls, base.FuncListModule);
            SetCalendar(txtOTDate);
            if (ClientMessage == null)
            {
                ClientMessage = new Dictionary<string, string>();
                ClientMessage.Add("OtmAdvanceapplyWorksixdaymessage", Message.OtmAdvanceapplyWorksixdaymessage);
                ClientMessage.Add("monday", Message.monday);
                ClientMessage.Add("tuesday", Message.tuesday);
                ClientMessage.Add("wednesday", Message.wednesday);
                ClientMessage.Add("thursday", Message.thursday);
                ClientMessage.Add("friday", Message.friday);
                ClientMessage.Add("saturday", Message.saturday);
                ClientMessage.Add("sunday", Message.sunday);
                ClientMessage.Add("WorkNoFirst", Message.WorkNoFirst);
                ClientMessage.Add("EmpBasicInfoNotExist", Message.EmpBasicInfoNotExist);
                ClientMessage.Add("NotNullOrEmpty", Message.NotNullOrEmpty);
                ClientMessage.Add("ConfirmHoursTooLarge", Message.ConfirmHoursTooLarge);


            }

            string clientmsg = JsSerializer.Serialize(ClientMessage);
            Page.ClientScript.RegisterStartupScript(GetType(), "ClientMessage", "var Message=" + clientmsg + ";", true);
            if (!IsPostBack)
            {
                logmodel.ProcessOwner = CurrentUserInfo.Personcode;
                logmodel.TransactionType = (Request.QueryString["ModuleCode"] == null) ? "" : Request.QueryString["ModuleCode"].ToString();
                logmodel.LevelNo = "2";
                logmodel.FromHost = Request.UserHostAddress;
                string EmployeeNo = (base.Request.QueryString["EmployeeNo"] == null) ? "" : base.Request.QueryString["EmployeeNo"].ToString();
                string ID = (base.Request.QueryString["ID"] == null) ? "" : base.Request.QueryString["ID"].ToString();
                string ProcessFlag = (base.Request.QueryString["ProcessFlag"] == null) ? "" : base.Request.QueryString["ProcessFlag"].ToString();
                string ModuleCode = (base.Request.QueryString["ModuleCode"] == null) ? "" : base.Request.QueryString["ModuleCode"].ToString();
                this.HiddenID.Value = ID;
                this.ProcessFlag.Value = ProcessFlag;

                if (ModuleCode == "PCMSYS110")   //  個人中心資料維護
                {
                    EmployeeNo = CurrentUserInfo.Personcode;
                     //this.txtLocalName.Text = CurrentUserInfo.Cname;  

                    this.txtEmployeeNo.Text = EmployeeNo;
                    this.txtEmployeeNo.Enabled = false;
                }

                if (ProcessFlag == "Add")
                {
                    this.HiddenID.Value = "";
                    this.Add(EmployeeNo);
                }
                else if ((EmployeeNo.Length > 0) && (ID.Length > 0))
                {
                    this.Modify(EmployeeNo, ID);
                }
                else
                {
                    Response.Write("<script type='text/javascript'>alert(\"" + Message.AtLastOneChoose + "\");window.parent.document.all.topTable.style.display='';window.parent.document.all.divEdit.style.display='none';</script>");
                }
                //this.txtEmployeeNo.Attributes.Add("onblur", "GetEmp();");
                this.txtOTDate.Attributes.Add("onpropertychange", "javascript:WeekDate();");
                this.txtOTDate.Attributes.Add("onafterpaste", "javascript:GetLVTotal();");
            }
        }

        #endregion

        #region 保存
        /// <summary>
        /// 保存按鈕事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
                string OTDate = "";
                string YearMonth = "";
                double strLvTotal = Convert.ToDouble(this.txtHours.Text);
                string OTType = this.txtOTType.Text;
                if (OTType.Length == 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "show", "alert('" + Message.OTMType + Message.Required + "')", true);
                }
                else
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
                    if (LHZBIsDisplayG2G3 == "Y")
                    {
                        OTType = OTType.Substring(0, 2);
                    }
                    try
                    {
                        OTDate = DateTime.Parse(this.txtOTDate.Text.ToString()).ToString("yyyy/MM/dd");
                        YearMonth = DateTime.Parse(this.txtOTDate.Text.ToString()).ToString("yyyy-MM").Replace("-", "");
                    }
                    catch (Exception)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "show", "alert('" + Message.ErrDate + "')", true);
                        return;
                    }
                    string WorkNo = this.txtEmployeeNo.Text.Trim().ToUpper();
                    if (strLvTotal < 0.5)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "show", "alert('" + Message.ActivityHoursError + "')", true);
                    }
                    else if ((OTType == "G1") && (strLvTotal > 16.0))
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "show", "alert('" + Message.ActivityHoursG1Error + "')", true);
                    }
                    else if (((OTType == "G2") || (OTType == "G3")) && (strLvTotal > 24.0))
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "show", "alert('" + Message.ActivityHoursG2Error + "')", true);
                    }
                    else
                    {
                        DataRow row;
                        if (this.ProcessFlag.Value.Equals("Add"))
                        {
                            model = new ActivityModel();
                            model.WorkNo = this.txtEmployeeNo.Text.Trim().ToUpper();
                            model.Remark = OTDate;
                            if (int.Parse(activityApplyBll.GetValue("condition15", model)) == 1)
                            {

                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "show", "alert('" + Message.ActivityHoursCountError + "')", true);
                                return;
                            }
                            tempDataTable = activityApplyBll.GetLHZBData("", "", "", "condition3");
                            if (tempDataTable.Rows.Count >= 1)
                            {
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "show", "alert('" + Message.duplicate + "')", true);
                                return;
                            }
                            row = tempDataTable.NewRow();
                            tempDataTable.Columns["StartTime"].DataType = typeof(string);
                            tempDataTable.Columns["EndTime"].DataType = typeof(string);
                            row.BeginEdit();
                            row["WorkNo"] = WorkNo;
                            row["OTDate"] = OTDate;
                            row["ConfirmHours"] = strLvTotal;
                            row["OTType"] = OTType;
                            row["WorkDesc"] = this.txtWorkDesc.Text.Trim().Replace("'", "");
                            row["Status"] = "0";
                            row["YearMonth"] = YearMonth;
                            row["UPDATE_USER"] = CurrentUserInfo.Personcode;
                            row["UPDATE_DATE"] = DateTime.Now.ToString("yyyy/MM/dd");
                            row["StartTime"] = this.txtStartTime.Text.Trim();
                            row["EndTime"] = this.txtEndTime.Text.Trim();
                            row.EndEdit();
                            this.tempDataTable.Rows.Add(row);
                            this.tempDataTable.AcceptChanges();
                            logmodel.ProcessFlag = "insert";
                            activityApplyBll.LHZBSaveData(this.ProcessFlag.Value, tempDataTable, CurrentUserInfo.Personcode,logmodel);
                            this.HiddenSave.Value = "Save";
                        }
                        else if (this.ProcessFlag.Value.Equals("Modify"))
                        {
                            string M_ID = this.HiddenID.Value;
                            tempDataTable = activityApplyBll.GetLHZBData(WorkNo, M_ID, OTDate, "condition4");
                            if (this.tempDataTable.Rows.Count > 0)
                            {
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "show", "alert('" + Message.ActivityHoursCountError + "')", true);
                                return;
                            }
                            tempDataTable = activityApplyBll.GetLHZBData("", M_ID, "", "condition5");
                            if (this.tempDataTable.Rows.Count == 0)
                            {
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "show", "alert('" + Message.AtLastOneChoose + "')", true);
                                return;
                            }
                            row = this.tempDataTable.Rows[0];
                            tempDataTable.Columns["StartTime"].DataType = typeof(string);
                            tempDataTable.Columns["EndTime"].DataType = typeof(string);
                            row.BeginEdit();
                            row["ID"] = M_ID;
                            row["WORKNO"] = WorkNo;
                            row["OTDate"] = OTDate;
                            row["ConfirmHours"] = strLvTotal;
                            row["OTType"] = OTType;
                            row["WorkDesc"] = this.txtWorkDesc.Text.Trim().Replace("'", "");
                            row["YearMonth"] = YearMonth;
                            row["StartTime"] = this.txtStartTime.Text.Trim();
                            row["EndTime"] = this.txtEndTime.Text.Trim();
                            row.EndEdit();
                            this.tempDataTable.AcceptChanges();
                            logmodel.ProcessFlag = "update";
                            activityApplyBll.LHZBSaveData(this.ProcessFlag.Value, this.tempDataTable, CurrentUserInfo.Personcode,logmodel);
                        }
                        //if (this.ProcessFlag.Value == "Add")
                        //{
                        //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "show", "alert('" + Message.AddSuccess + "')", true);
                        //    this.Add();
                        //}
                        //else
                        //{
                        //    base.Response.Write("<script type='text/javascript'>window.parent.document.all.btnQuery.click();</script>");
                        //}
                        //新增和修改之後直接返回上頁面
                        //base.Response.Write("<script type='text/javascript'>window.parent.document.all.btnQuery.click();</script>");
                        //新增修改之後添加提示再返回之前頁面
                        if (this.ProcessFlag.Value == "Add")
                        {
                            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "show", "alert('" + Message.AddSuccess + "');window.parent.document.all.btnQuery.click();", true);
                            base.Response.Write("<script type='text/javascript'>alert('" + Message.AddSuccess + "');window.parent.document.all.btnQuery.click();</script>");
                            
                        }
                        else
                        {
                            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "show", "alert('" + Message.UpdateSuccess + "')", true);
                            base.Response.Write("<script type='text/javascript'>alert('" + Message.UpdateSuccess + "');window.parent.document.all.btnQuery.click();</script>");
                        }
                    }

                }
       


        }

        /// <summary>
        /// 新增輔助
        /// </summary>
        protected void Add(string EmployeeNo)
        {
            if (this.HiddenSave.Value != "Save" && EmployeeNo.Trim().Length==0)
            {
                this.txtEmployeeNo.Text = "";
            }
            this.ProcessFlag.Value = "Add";
            this.TextBoxsReset("Add", false);
        }
        /// <summary>
        ///  修改輔助
        /// </summary>
        /// <param name="EmployeeNo"></param>
        /// <param name="ID"></param>
        protected void Modify(string EmployeeNo, string ID)
        {
            this.txtEmployeeNo.Text = EmployeeNo;
            this.EmpQuery(EmployeeNo, ID);
            this.ProcessFlag.Value = "Modify";
            this.TextBoxsReset("Modify", false);
        }

        #endregion

        #region 查詢員工資料
        /// <summary>
        /// 查詢員工資料
        /// </summary>
        /// <param name="EmployeeNo"></param>
        /// <param name="ID"></param>
        private void EmpQuery(string EmployeeNo, string ID)
        {

            tempDataTable = activityApplyBll.GetVData(EmployeeNo, base.SqlDep.ToString());
            if (tempDataTable.Rows.Count > 0)
            {
                foreach (DataRow newRow in tempDataTable.Rows)
                {
                    this.txtLocalName.Text = newRow["LOCALNAME"].ToString();
                    this.txtDPcode.Text = newRow["DName"].ToString();
                    this.txtPersonType.Text = newRow["flag"].ToString(); 
                }
                this.GetMonthOverTime(EmployeeNo.ToUpper());
                this.Query(EmployeeNo, ID);
            }
            else
            {
                this.txtLocalName.Text = "";
                this.txtDPcode.Text = "";
                this.txtPersonType.Text = "";
                this.txtOTDate.Text = "";
                this.txtWorkDesc.Text = "";
                this.txtHours.Text = "";
                this.TextBoxsReset("", true);
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "show", Message.WorkNONotExist, true);
            }
            this.tempDataTable.Clear();
        }
        #endregion

        #region Ajax事件
        /// <summary>
        /// 重載Ajax方法
        /// </summary>
        protected override void AjaxProcess()
        {
            string noticeJson = null;
            if (Request.Form["ActionFlag"] == "FindOTType")
            {
                string OTDate = Request.Form["OTDate"].ToString();
                string WorkNo = Request.Form["WorkNo"].ToString();
                string OTType = FindOTType(OTDate, WorkNo);
                Response.Clear();
                Response.Write(OTType);
                Response.End();


            }

            if (Request.Form["ActionFlag"] == "GetOTMFlag")
            {
                string OTDate = Request.Form["OTDate"].ToString();
                string WorkNo = Request.Form["WorkNo"].ToString();
                string OTMFlag = GetOTMFlag(WorkNo, OTDate);
                Response.Clear();
                Response.Write(OTMFlag);
                Response.End();
            }


            if (Request.Form["ActionFlag"] == "GetEmp")
            {
                if (!string.IsNullOrEmpty(Request.Form["WorkNo"]))
                {
                    string WorkNo = Request.Form["WorkNo"].ToString();
                    string sqlDep = Request.Form["SqlDep"];
                    DataTable temp = new DataTable();
                    temp = GetEmp(WorkNo, sqlDep);
                    ActivityModel model = new ActivityModel();
                    if (temp.Rows.Count != 0)
                    {
                        model.WorkNo = temp.Rows[0]["WorkNo"].ToString();
                        model.LocalName = temp.Rows[0]["LocalName"].ToString();
                        model.DepName = temp.Rows[0]["DepName"].ToString();
                        model.OverTimeType = temp.Rows[0]["Flag"].ToString();
                    }
                    if (model != null)
                    {
                        noticeJson = JsSerializer.Serialize(model);
                    }
                    Response.Clear();
                    Response.Write(noticeJson);
                    Response.End();

                }
            }

            if (Request.Form["ActionFlag"] == "Get_MonthOverTime")
            {
                if (!string.IsNullOrEmpty(Request.Form["WorkNo"]))
                {
                    string WorkNo = Request.Form["WorkNo"].ToString();
                    string MonthTotal = Get_MonthOverTime(WorkNo);
                    Response.Clear();
                    Response.Write(MonthTotal);
                    Response.End();

                }
            }
            if (Request.Form["ActionFlag"] == "GetOTHours")
            {
                if (!string.IsNullOrEmpty(Request.Form["WorkNo"]))
                {

                    string WorkNo = Request.Form["WorkNo"].ToString();
                    string OTDate = Request.Form["OTDate"].ToString();
                    string BeginTime = Request.Form["BeginTime"].ToString();
                    string EndTime = Request.Form["EndTime"].ToString();
                    string OTType = Request.Form["OTType"].ToString();
                    string OTHours = GetOTHours(WorkNo, OTDate, BeginTime, EndTime, OTType);
                    Response.Clear();
                    Response.Write(OTHours);
                    Response.End();

                }
            }


        }
        #endregion

        #region 根據條件查詢所需參數
        /// <summary>
        /// FindOTType
        /// </summary>
        /// <param name="OTDate"></param>
        /// <param name="WorkNo"></param>
        /// <returns></returns>
        public string FindOTType(string OTDate, string WorkNo)
        {
            string LHZBIsDisplayG2G3 = "";
            string OtStatus = "";
            OtStatus = activityApplyBll.GetOTType(WorkNo, OTDate);
            try
            {
                LHZBIsDisplayG2G3 = ConfigurationManager.AppSettings["LHZBIsDisplayG2G3"];
            }
            catch
            {
                LHZBIsDisplayG2G3 = "N";
            }
            if (LHZBIsDisplayG2G3 == "Y")
            {
                switch (OtStatus)
                {
                    case "G2":
                        return (OtStatus + "(" + Message.OtmG2Remark + ")");

                    case "G3":
                        return (OtStatus + "(" + Message.OtmG3Remark + ")");
                }
                return OtStatus;
            }
            return OtStatus;
        }

        /// <summary>
        /// Get_MonthOverTime
        /// </summary>
        /// <param name="EmployeeNo"></param>
        /// <returns></returns>
        public string Get_MonthOverTime(string EmployeeNo)
        {
            string Month = DateTime.Today.Month.ToString();
            if (Month.Length == 1)
            {
                Month = "0" + Month;
            }
            string YearMonth = DateTime.Today.Year.ToString() + Month;
            tempDataTable = activityApplyBll.GetMonthAllOverTime(EmployeeNo, YearMonth);
            string monthtotal = "0";
            if (tempDataTable.Rows.Count > 0)
            {
                monthtotal = this.tempDataTable.Rows[0]["MonthTotal"].ToString();
            }
            return monthtotal;
        }


        /// <summary>
        /// GetEmp
        /// </summary>
        /// <param name="EmployeeNo"></param>
        /// <returns></returns>
        public DataTable GetEmp(string EmployeeNo, string sqlDep)
        {

            tempDataTable = new DataTable();
            tempDataTable.Clear();
            tempDataTable = activityApplyBll.GetEmp(EmployeeNo, sqlDep);
            return tempDataTable;
        }

        /// <summary>
        /// GetMonthOverTime
        /// </summary>
        /// <param name="EmployeeNo"></param>
        private void GetMonthOverTime(string EmployeeNo)
        {
            string Month = DateTime.Today.Month.ToString();
            if (Month.Length == 1)
            {
                Month = "0" + Month;
            }
            string YearMonth = DateTime.Today.Year.ToString() + Month;
            this.tempDataTable.Clear();
            this.tempDataTable = activityApplyBll.GetMonthAllOverTime(EmployeeNo, YearMonth);
            if (this.tempDataTable.Rows.Count > 0)
            {
                this.txtMonthAllHours.Text = this.tempDataTable.Rows[0]["MonthTotal"].ToString();
            }
            else
            {
                this.txtMonthAllHours.Text = "0";
            }
        }

        /// <summary>
        /// GetOTHours
        /// </summary>
        /// <param name="WorkNo"></param>
        /// <param name="OTDate"></param>
        /// <param name="BeginTime"></param>
        /// <param name="EndTime"></param>
        /// <param name="OTType"></param>
        /// <returns></returns>
        public string GetOTHours(string WorkNo, string OTDate, string BeginTime, string EndTime, string OTType)
        {

            double hours = 0.0;
            string LHZBIsDisplayG2G3 = "";
            if (OTType.Length == 0)
            {
                OTType = FindOTType(OTDate, WorkNo);
                try
                {
                    LHZBIsDisplayG2G3 = ConfigurationManager.AppSettings["LHZBIsDisplayG2G3"];
                }
                catch
                {
                    LHZBIsDisplayG2G3 = "N";
                }
                if (LHZBIsDisplayG2G3 == "Y")
                {
                    OTType = OTType.Substring(0, 2);
                }
            }
            if (BeginTime != EndTime)
            {
                hours = fun_GetOtHours(WorkNo.ToUpper(), OTDate, BeginTime, EndTime, OTType);
            }
            return hours.ToString();
        }

        /// <summary>
        /// GetOTMFlag
        /// </summary>
        /// <param name="WorkNo"></param>
        /// <param name="OtDate"></param>
        /// <returns></returns>
        public string GetOTMFlag(string WorkNo, string OtDate)
        {

            return activityApplyBll.GetOTMFlag(WorkNo, OtDate);

        }

        
        /// <summary>
        /// 初始化加班日期
        /// </summary>
        private void OninitOverTimeDate()
        {
            this.txtOTDate.Text = DateTime.Today.ToString("yyyy/MM/dd");
            //this.txtOTDate.Attributes.Add("formater", base.dateFormat);
        }


        /// <summary>
        /// 查詢
        /// </summary>
        /// <param name="EmployeeNo"></param>
        /// <param name="ID"></param>
        private void Query(string EmployeeNo, string ID)
        {

            tempTable = activityApplyBll.GetLHZBData(EmployeeNo, ID, base.SqlDep.ToString(), "condition2");
            if (tempTable.Rows.Count > 0)
            {
                this.TextDataBind(true);
            }
            else
            {
                this.TextDataBind(false);
            }

        }

        # endregion

        #region 文本框
        /// <summary>
        /// 文本框管理
        /// </summary>
        /// <param name="buttonText"></param>
        /// <param name="read"></param>
        private void TextBoxsReset(string buttonText, bool read)
        {
            this.txtLocalName.BorderStyle = BorderStyle.None;
            this.txtPersonType.BorderStyle = BorderStyle.None;
            this.txtDPcode.BorderStyle = BorderStyle.None;
            this.txtMonthAllHours.BorderStyle = BorderStyle.None;
            this.txtWeek.BorderStyle = BorderStyle.None;
            this.txtOTType.BorderStyle = BorderStyle.None;
            this.txtEmployeeNo.BorderStyle = BorderStyle.None;
            this.txtOTDate.BorderStyle = read ? BorderStyle.None : BorderStyle.NotSet;
            this.txtWorkDesc.BorderStyle = read ? BorderStyle.None : BorderStyle.NotSet;
            if (buttonText.Equals("Add"))
            {
                this.txtOTDate.Text = "";
                this.txtWorkDesc.Text = "";
                this.txtHours.Text = "";
                this.txtEmployeeNo.BorderStyle = BorderStyle.NotSet;
            }
        }


        /// <summary>
        /// 文本框數據綁定
        /// </summary>
        /// <param name="read"></param>
        private void TextDataBind(bool read)
        {
            if (read)
            {

                if (tempTable.Rows[0]["OTDate"].ToString().Trim() != "")
                {
                    this.txtOTDate.Text = DateTime.Parse(tempTable.Rows[0]["OTDate"].ToString()).ToString("yyyy/MM/dd");
                }
                else
                {
                    this.txtOTDate.Text = "";
                }
                this.txtWeek.Text = tempTable.Rows[0]["Week"].ToString();
                string LHZBIsDisplayG2G3 = "";
                try
                {
                    LHZBIsDisplayG2G3 = ConfigurationManager.AppSettings["LHZBIsDisplayG2G3"];
                }
                catch
                {
                    LHZBIsDisplayG2G3 = "N";
                }
                if (LHZBIsDisplayG2G3 == "Y")
                {
                    if (tempTable.Rows[0]["OTType"].ToString() == "G2")
                    {
                        this.txtOTType.Text = tempTable.Rows[0]["OTType"].ToString() + "(" + base.GetResouseValue("otm.g2.remark") + ")";
                    }
                    else if (tempTable.Rows[0]["OTType"].ToString() == "G3")
                    {
                        this.txtOTType.Text = tempTable.Rows[0]["OTType"].ToString() + "(" + base.GetResouseValue("otm.g3.remark") + ")";
                    }
                    else
                    {
                        this.txtOTType.Text = tempTable.Rows[0]["OTType"].ToString();
                    }
                }
                else
                {
                    this.txtOTType.Text = tempTable.Rows[0]["OTType"].ToString();
                }
                this.txtHours.Text = tempTable.Rows[0]["ConfirmHours"].ToString();
                this.txtWorkDesc.Text = tempTable.Rows[0]["WorkDesc"].ToString();
                this.txtStartTime.Text = tempTable.Rows[0]["StartTime"].ToString();
                this.txtEndTime.Text = tempTable.Rows[0]["EndTime"].ToString();
                this.HiddenState.Value = tempTable.Rows[0]["Status"].ToString();
                string CSS4S0002 = tempTable.Rows[0]["Status"].ToString();
                if ((CSS4S0002 != null) && (CSS4S0002 == "2"))
                {
                    this.btnSave.Enabled = false;
                }


            }
            else
            {
                this.txtOTDate.Text = "";
                this.txtWeek.Text = "";
                this.txtOTType.Text = "";
                this.txtHours.Text = "";
                this.txtWorkDesc.Text = "";
            }
        }

        #endregion

        #region function.cs中方法(暫未使用)

        public double fun_GetOtHours(string WorkNo, string OTDate, string StrBtime, string StrEtime, string OTType)
        {
            try
            {
                double OtHours = 0.0;
                double RestHours = 0.0;

                if (OTType.Length != 0)
                {
                    DateTime dtTempBeginTime;
                    DateTime dtTempEndTime;
                    string dtShiftOnTime;
                    string dtShiftOffTime;
                    string dtAMRestSTime;
                    string dtAMRestETime;
                    TimeSpan tsOTHours;
                    if (OTType.Equals("G4") && (TimeSpan.Parse(Convert.ToDateTime(StrBtime).ToString("HH:mm")) < TimeSpan.Parse("06:30")))
                    {
                        OTDate = Convert.ToDateTime(OTDate).AddDays(-1.0).ToString("yyyy/MM/dd");
                    }
                    BLL.KQM.Query.OTM.OTMExceptionQryBll exceptionQryBll = new BLL.KQM.Query.OTM.OTMExceptionQryBll();
                    string strShiftNo = exceptionQryBll.GetShiftNo(WorkNo.ToUpper(), OTDate);
                    if (strShiftNo.Length == 0)
                    {
                        return OtHours;
                    }
                    if ((StrBtime.Length > 8) & (StrEtime.Length > 8))
                    {
                        dtTempBeginTime = DateTime.Parse(StrBtime);
                        dtTempEndTime = DateTime.Parse(StrEtime);
                    }
                    else
                    {
                        dtTempBeginTime = DateTime.Parse(OTDate + " " + StrBtime);
                        dtTempEndTime = DateTime.Parse(OTDate + " " + StrEtime);
                        SortedList list = new SortedList();
                        list = this.ReturnOTTTime(WorkNo, OTDate, dtTempBeginTime, dtTempEndTime, strShiftNo);
                        dtTempBeginTime = Convert.ToDateTime(list.GetByIndex(list.IndexOfKey("A")));
                        dtTempEndTime = Convert.ToDateTime(list.GetByIndex(list.IndexOfKey("B")));
                    }
                    if (OTType.Equals("G4") && !(dtTempBeginTime.ToString("yyyy/MM/dd").Equals(dtTempEndTime.ToString("yyyy/MM/dd")) || Convert.ToDateTime(StrBtime).ToString("HH:mm").Equals("00:00")))
                    {
                        return OtHours;
                    }
                    string dtBtime = dtTempBeginTime.ToString("yyyy/MM/dd HH:mm");
                    string dtEtime = dtTempEndTime.ToString("yyyy/MM/dd HH:mm");

                    DataTable sdt = exceptionQryBll.GetDataTableBySQL(strShiftNo);
                    if (sdt.Rows.Count == 0)
                    {
                        return OtHours;
                    }
                    string ShiftOnTime = Convert.ToString(sdt.Rows[0]["OnDutyTime"]);
                    string ShiftOffTime = Convert.ToString(sdt.Rows[0]["OffDutyTime"]);
                    string AMRestSTime = Convert.ToString(sdt.Rows[0]["AMRestSTime"]);
                    string AMRestETime = Convert.ToString(sdt.Rows[0]["AMRestETime"]);
                    string PMRestSTime = Convert.ToString(sdt.Rows[0]["PMRestSTime"]);
                    string PMRestETime = Convert.ToString(sdt.Rows[0]["PMRestETime"]);
                    string ShiftType = Convert.ToString(sdt.Rows[0]["ShiftType"]);
                    string dtPMRestSTime = "";
                    string dtPMRestETime = "";
                    if (TimeSpan.Parse(ShiftOnTime) < TimeSpan.Parse(ShiftOffTime))
                    {
                        dtShiftOnTime = DateTime.Parse(OTDate + " " + ShiftOnTime).ToString("yyyy/MM/dd HH:mm");
                        dtShiftOffTime = DateTime.Parse(OTDate + " " + ShiftOffTime).ToString("yyyy/MM/dd HH:mm");
                        dtAMRestSTime = DateTime.Parse(OTDate + " " + AMRestSTime).ToString("yyyy/MM/dd HH:mm");
                        dtAMRestETime = DateTime.Parse(OTDate + " " + AMRestETime).ToString("yyyy/MM/dd HH:mm");
                        if ((PMRestSTime.Length > 0) && (PMRestETime.Length > 0))
                        {
                            if (TimeSpan.Parse(PMRestSTime) <= TimeSpan.Parse(PMRestETime))
                            {
                                dtPMRestSTime = DateTime.Parse(OTDate + " " + PMRestSTime).ToString("yyyy/MM/dd HH:mm");
                                dtPMRestETime = DateTime.Parse(OTDate + " " + PMRestETime).ToString("yyyy/MM/dd HH:mm");
                            }
                            else
                            {
                                dtPMRestSTime = DateTime.Parse(OTDate + " " + PMRestSTime).ToString("yyyy/MM/dd HH:mm");
                                dtPMRestETime = DateTime.Parse(OTDate + " " + PMRestETime).AddDays(1.0).ToString("yyyy/MM/dd HH:mm");
                            }
                        }
                    }
                    else
                    {
                        dtShiftOnTime = DateTime.Parse(OTDate + " " + ShiftOnTime).ToString("yyyy/MM/dd HH:mm");
                        dtShiftOffTime = DateTime.Parse(OTDate + " " + ShiftOffTime).AddDays(1.0).ToString("yyyy/MM/dd HH:mm");
                        if ((TimeSpan.Parse(AMRestSTime) <= TimeSpan.Parse(AMRestETime)) && (TimeSpan.Parse(ShiftOnTime) < TimeSpan.Parse(AMRestETime)))
                        {
                            dtAMRestSTime = DateTime.Parse(OTDate + " " + AMRestSTime).ToString("yyyy/MM/dd HH:mm");
                            dtAMRestETime = DateTime.Parse(OTDate + " " + AMRestETime).ToString("yyyy/MM/dd HH:mm");
                        }
                        else if ((TimeSpan.Parse(AMRestSTime) <= TimeSpan.Parse(AMRestETime)) && (TimeSpan.Parse(ShiftOnTime) > TimeSpan.Parse(AMRestETime)))
                        {
                            dtAMRestSTime = DateTime.Parse(OTDate + " " + AMRestSTime).AddDays(1.0).ToString("yyyy/MM/dd HH:mm");
                            dtAMRestETime = DateTime.Parse(OTDate + " " + AMRestETime).AddDays(1.0).ToString("yyyy/MM/dd HH:mm");
                        }
                        else
                        {
                            dtAMRestSTime = DateTime.Parse(OTDate + " " + AMRestSTime).ToString("yyyy/MM/dd HH:mm");
                            dtAMRestETime = DateTime.Parse(OTDate + " " + AMRestETime).AddDays(1.0).ToString("yyyy/MM/dd HH:mm");
                        }
                        if ((PMRestSTime.Length > 0) && (PMRestETime.Length > 0))
                        {
                            dtPMRestSTime = DateTime.Parse(OTDate + " " + PMRestSTime).AddDays(1.0).ToString("yyyy/MM/dd HH:mm");
                            dtPMRestETime = DateTime.Parse(OTDate + " " + PMRestETime).AddDays(1.0).ToString("yyyy/MM/dd HH:mm");
                        }
                    }
                    if (string.Compare(dtBtime, dtEtime) >= 0)
                    {
                        return OtHours;
                    }
                    if (OTType.Equals("G1"))
                    {
                        if (((string.Compare(dtBtime, dtShiftOnTime) >= 0) && (string.Compare(dtShiftOffTime, dtBtime) > 0)) || ((string.Compare(dtEtime, dtShiftOnTime) > 0) && (string.Compare(dtShiftOffTime, dtEtime) >= 0)))
                        {
                            return OtHours;
                        }
                        if (string.Compare(dtShiftOnTime, dtBtime) > 0)
                        {
                            if (string.Compare(dtEtime, dtShiftOnTime) > 0)
                            {
                                return OtHours;
                            }
                            tsOTHours = (TimeSpan)(dtTempEndTime - dtTempBeginTime);
                            OtHours = tsOTHours.TotalMinutes;
                        }
                        else if (string.Compare(dtBtime, dtShiftOffTime) >= 0)
                        {
                            if (dtPMRestSTime.Length > 0)
                            {
                                if ((string.Compare(dtBtime, dtPMRestSTime) >= 0) && (string.Compare(dtPMRestETime, dtBtime) > 0))
                                {
                                    if ((string.Compare(dtEtime, dtPMRestSTime) > 0) && (string.Compare(dtPMRestETime, dtEtime) >= 0))
                                    {
                                        return OtHours;
                                    }
                                    tsOTHours = (TimeSpan)(dtTempEndTime - dtTempBeginTime);
                                    OtHours = tsOTHours.TotalMinutes;
                                    tsOTHours = (TimeSpan)(Convert.ToDateTime(dtPMRestETime) - dtTempBeginTime);
                                    RestHours += tsOTHours.TotalMinutes;
                                }
                                else if (string.Compare(dtPMRestSTime, dtBtime) >= 0)
                                {
                                    if (string.Compare(dtPMRestSTime, dtEtime) >= 0)
                                    {
                                        tsOTHours = (TimeSpan)(dtTempEndTime - dtTempBeginTime);
                                        OtHours = tsOTHours.TotalMinutes;
                                    }
                                    else if (string.Compare(dtPMRestETime, dtEtime) >= 0)
                                    {
                                        tsOTHours = (TimeSpan)(Convert.ToDateTime(dtPMRestSTime) - dtTempBeginTime);
                                        OtHours = tsOTHours.TotalMinutes;
                                    }
                                    else
                                    {
                                        tsOTHours = (TimeSpan)(dtTempEndTime - dtTempBeginTime);
                                        OtHours = tsOTHours.TotalMinutes;
                                        tsOTHours = (TimeSpan)(Convert.ToDateTime(dtPMRestETime) - Convert.ToDateTime(dtPMRestSTime));
                                        RestHours = tsOTHours.TotalMinutes;
                                    }
                                }
                                else
                                {
                                    tsOTHours = (TimeSpan)(dtTempEndTime - dtTempBeginTime);
                                    OtHours = tsOTHours.TotalMinutes;
                                }
                            }
                            else
                            {
                                tsOTHours = (TimeSpan)(dtTempEndTime - dtTempBeginTime);
                                OtHours = tsOTHours.TotalMinutes;
                            }
                        }
                    }
                    else
                    {
                        if (string.Compare(dtShiftOnTime, dtBtime) > 0)
                        {
                            dtTempBeginTime = Convert.ToDateTime(dtShiftOnTime);
                            dtBtime = dtTempBeginTime.ToString("yyyy/MM/dd HH:mm");
                        }
                        if ((string.Compare(dtAMRestETime, dtBtime) >= 0) && (string.Compare(dtBtime, dtAMRestSTime) >= 0))
                        {
                            dtTempBeginTime = Convert.ToDateTime(dtAMRestETime);
                            dtBtime = dtTempBeginTime.ToString("yyyy/MM/dd HH:mm");
                        }
                        if ((string.Compare(dtAMRestETime, dtEtime) >= 0) && (string.Compare(dtEtime, dtAMRestSTime) >= 0))
                        {
                            dtTempEndTime = Convert.ToDateTime(dtAMRestSTime);
                            dtEtime = dtTempEndTime.ToString("yyyy/MM/dd HH:mm");
                        }
                        if ((string.Compare(dtPMRestETime, dtBtime) >= 0) && (string.Compare(dtBtime, dtPMRestSTime) >= 0))
                        {
                            dtTempBeginTime = Convert.ToDateTime(dtPMRestETime);
                            dtBtime = dtTempBeginTime.ToString("yyyy/MM/dd HH:mm");
                        }
                        if ((string.Compare(dtPMRestETime, dtEtime) >= 0) && (string.Compare(dtEtime, dtPMRestSTime) >= 0))
                        {
                            dtTempEndTime = Convert.ToDateTime(dtPMRestSTime);
                            dtEtime = dtTempEndTime.ToString("yyyy/MM/dd HH:mm");
                        }
                        if (string.Compare(dtBtime, dtEtime) >= 0)
                        {
                            return OtHours;
                        }
                        if ((string.Compare(dtAMRestSTime, dtBtime) >= 0) && (string.Compare(dtEtime, dtAMRestETime) >= 0))
                        {
                            tsOTHours = (TimeSpan)(Convert.ToDateTime(dtAMRestETime) - Convert.ToDateTime(dtAMRestSTime));
                            RestHours += tsOTHours.TotalMinutes;
                            if ((dtPMRestETime.Length > 0) && (string.Compare(dtEtime, dtPMRestETime) > 0))
                            {
                                tsOTHours = (TimeSpan)(Convert.ToDateTime(dtPMRestETime) - Convert.ToDateTime(dtPMRestSTime));
                                RestHours += tsOTHours.TotalMinutes;
                            }
                        }
                        else if ((dtPMRestETime.Length > 0) && ((string.Compare(dtPMRestSTime, dtBtime) > 0) && (string.Compare(dtEtime, dtPMRestETime) > 0)))
                        {
                            tsOTHours = (TimeSpan)(Convert.ToDateTime(dtPMRestETime) - Convert.ToDateTime(dtPMRestSTime));
                            RestHours += tsOTHours.TotalMinutes;
                        }
                        tsOTHours = (TimeSpan)(dtTempEndTime - dtTempBeginTime);
                        OtHours = tsOTHours.TotalMinutes;
                    }
                    if (exceptionQryBll.GetValue("condition5", null, null, null, null).Equals("Y"))
                    {
                        OtHours = Math.Round((double)(Math.Floor((double)((OtHours - RestHours) / 10.0)) / 6.0), 1);
                    }
                    else
                    {
                        OtHours = Math.Round((double)(((OtHours - RestHours) / 60.0) * 100.0)) / 100.0;
                        if ((OtHours % 0.5) != 0.0)
                        {
                            double ihours = Math.Floor(OtHours);
                            if (OtHours > (ihours + 0.5))
                            {
                                OtHours = ihours + 0.5;
                            }
                            else
                            {
                                OtHours = ihours;
                            }
                        }
                    }
                    if ((OtHours >= 20.0) || (OtHours < 0.0))
                    {
                        OtHours = 0.0;
                    }
                }
                return OtHours;
            }
            catch (Exception)
            {
                return 0.0;
            }
        }


        public SortedList ReturnOTTTime(string WorkNo, string OTDate, DateTime dtTempBeginTime, DateTime dtTempEndTime, string ShiftNo)
        {
            string condition = "";
            SortedList list = new SortedList();
            DateTime dtMidTime = DateTime.Parse(OTDate + " 12:00");
            DateTime dtMidTime2 = DateTime.Parse(OTDate + " 08:00");
            string strTempBeginTime = dtTempBeginTime.ToString("yyyy/MM/dd HH:mm");
            string strTempEndTime = dtTempEndTime.ToString("yyyy/MM/dd HH:mm");
            string strMidTime = dtMidTime.ToString("yyyy/MM/dd HH:mm");
            string strMidTime2 = dtMidTime2.ToString("yyyy/MM/dd HH:mm");
            BLL.KQM.Query.OTM.OTMExceptionQryBll exceptionQryBll = new BLL.KQM.Query.OTM.OTMExceptionQryBll();
            try
            {

                if (Convert.ToDecimal(exceptionQryBll.GetValue("condition1", strTempBeginTime, strTempEndTime, strMidTime, strMidTime2)) < 0M)
                {
                    dtTempEndTime = DateTime.Parse(DateTime.Parse(OTDate).AddDays(1.0).ToString("yyyy/MM/dd") + " " + dtTempEndTime.ToString("HH:mm"));
                }
                else if (ShiftNo.StartsWith("C"))
                {

                    if (Convert.ToDecimal(exceptionQryBll.GetValue("condition2", strTempBeginTime, strTempEndTime, strMidTime, strMidTime2)) < 0M)
                    {
                        dtTempBeginTime = DateTime.Parse(DateTime.Parse(OTDate).AddDays(1.0).ToString("yyyy/MM/dd") + " " + dtTempBeginTime.ToString("HH:mm"));
                        dtTempEndTime = DateTime.Parse(DateTime.Parse(OTDate).AddDays(1.0).ToString("yyyy/MM/dd") + " " + dtTempEndTime.ToString("HH:mm"));
                    }
                    else
                    {

                        if (Convert.ToDecimal(exceptionQryBll.GetValue("condition3", strTempBeginTime, strTempEndTime, strMidTime, strMidTime2)) < 0M)
                        {
                            dtTempEndTime = DateTime.Parse(DateTime.Parse(OTDate).AddDays(1.0).ToString("yyyy/MM/dd") + " " + dtTempEndTime.ToString("HH:mm"));
                        }
                    }
                }
                else if (ShiftNo.StartsWith("B"))
                {

                    if (Convert.ToDecimal(exceptionQryBll.GetValue("condition4", strTempBeginTime, strTempEndTime, strMidTime, strMidTime2)) < 0M)
                    {
                        dtTempBeginTime = DateTime.Parse(DateTime.Parse(OTDate).AddDays(1.0).ToString("yyyy/MM/dd") + " " + dtTempBeginTime.ToString("HH:mm"));
                        dtTempEndTime = DateTime.Parse(DateTime.Parse(OTDate).AddDays(1.0).ToString("yyyy/MM/dd") + " " + dtTempEndTime.ToString("HH:mm"));
                    }
                }
                list.Add("A", dtTempBeginTime);
                list.Add("B", dtTempEndTime);
                return list;
            }
            catch (Exception)
            {
                list.Add("A", dtTempBeginTime);
                list.Add("B", dtTempEndTime);
                return list;
            }
        }

        #endregion


        #region 返回sqlDep
        public string GetSqlDep()
        {
            return base.SqlDep.ToString();
        }
        #endregion
    }

}
