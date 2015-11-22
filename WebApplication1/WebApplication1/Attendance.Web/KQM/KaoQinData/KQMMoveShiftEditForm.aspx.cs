/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： KQMMoveShiftEditForm.aspx.cs
 * 檔功能描述： 彈性調班
 * 
 * 版本：1.0
 * 創建標識： 張明強 2011.12.28
 * 
 */
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using GDSBG.MiABU.Attendance.Model.KQM.KaoQinData;
using GDSBG.MiABU.Attendance.BLL.KQM.KaoQinData;
using System.Collections.Generic;
using Resources;
using System.Web.Script.Serialization;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;

namespace GDSBG.MiABU.Attendance.Web.KQM.KaoQinData
{
    public partial class KQMMoveShiftEditForm : BasePage
    {
        JavaScriptSerializer JsSerializer = new JavaScriptSerializer();
        Dictionary<string, string> ClientMessage = null;
        MoveShiftModel moveShiftModel = new MoveShiftModel();
        KQMMoveShiftBll moveShiftBll = new KQMMoveShiftBll();
        protected DataTable tempDataTable;
        static SynclogModel logmodel = new SynclogModel();

        #region 頁面載入
        /// <summary>
        /// 頁面載入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            //PageHelper.ButtonControls(base.FuncList, pnlShowPanel.Controls, base.FuncListModule);
            if (!IsPostBack)
            {
                logmodel.ProcessOwner = CurrentUserInfo.Personcode;
                logmodel.TransactionType = (Request.QueryString["ModuleCode"] == null) ? "" : Request.QueryString["ModuleCode"].ToString();
                logmodel.LevelNo = "2";
                logmodel.FromHost = Request.UserHostAddress;
                string EmployeeNo = (base.Request.QueryString["EmployeeNo"] == null) ? "" : base.Request.QueryString["EmployeeNo"].ToString();
                string WorkDate = (base.Request.QueryString["WorkDate"] == null) ? "" : base.Request.QueryString["WorkDate"].ToString();
                string NoWorkDate = (base.Request.QueryString["NoWorkDate"] == null) ? "" : base.Request.QueryString["NoWorkDate"].ToString();
                string ProcessFlag = (base.Request.QueryString["ProcessFlag"] == null) ? "" : base.Request.QueryString["ProcessFlag"].ToString();
                this.HiddenWorkDate.Value = WorkDate;
                this.HiddenNoWorkDate.Value = NoWorkDate;
                this.ProcessFlag.Value = ProcessFlag;
                if (ProcessFlag == "Add")
                {
                    this.Add();
                }
                else if (((EmployeeNo.Length > 0) && (WorkDate.Length > 0)) && (NoWorkDate.Length > 0))
                {
                    this.Modify(EmployeeNo, WorkDate, NoWorkDate);
                }
                else
                {
                    base.Response.Write("<script type='text/javascript'>alert(\"" + base.GetResouseValue("common.message.data.select") + "\");window.parent.document.all.topTable.style.display='';window.parent.document.all.divEdit.style.display='none';</script>");
                }


            }


            if (ClientMessage == null)
            {
                ClientMessage = new Dictionary<string, string>();
                ClientMessage.Add("EmpNotExist", Message.EmpNotExist);
                ClientMessage.Add("TextBoxNotNull", Message.TextBoxNotNull);
                ClientMessage.Add("StartDateAndEndDate", Message.StartDateAndEndDate);
            }
            string clientmsg = JsSerializer.Serialize(ClientMessage);
            Page.ClientScript.RegisterStartupScript(GetType(), "ClientMessage", "var Message=" + clientmsg + ";", true);
            SetCalendar(txtWorkDate, txtNoWorkDate);
        }
        #endregion

        #region 新增/修改保存
        /// <summary>
        /// 新增
        /// </summary>
        protected void Add()
        {
            if (this.HiddenSave.Value != "Save")
            {
                this.txtEmployeeNo.Text = "";
            }
            this.ProcessFlag.Value = "Add";
            this.TextBoxsReset("Add", false);
        }

        /// <summary>
        /// 保存按鈕事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            //將頁面信息傳入model用於修改或新增
            moveShiftModel.WorkNo = this.txtEmployeeNo.Text.Trim();
            moveShiftModel.WorkDate = DateTime.Parse(this.txtWorkDate.Text.Trim().ToString());
            moveShiftModel.WorkSTime = this.txtWorkSTime.Text.Trim();
            moveShiftModel.WorkETime = this.txtWorkETime.Text.Trim();
            moveShiftModel.NoWorkDate = DateTime.Parse(this.txtNoWorkDate.Text.Trim().ToString());
            moveShiftModel.NoWorkSTime = this.txtNoWorkSTime.Text.Trim();
            moveShiftModel.NoWorkETime = this.txtNoWorkETime.Text.Trim();
            moveShiftModel.Remark = this.txtRemark.Text.Trim();
            moveShiftModel.TimeQty = Convert.ToDecimal(this.txtTimeQty.Text.Trim());
            moveShiftModel.UpdateUser = CurrentUserInfo.Personcode;
            moveShiftModel.UpdateDate = System.DateTime.Today.Date;
            string alert = "";
            bool WorkDateChanged = true;
            bool NoWorkDateChanged = true;
            if (this.ProcessFlag.Value.Equals("Modify"))
            {
                //調班日期未改變
                if (Convert.ToDateTime(this.txtWorkDate.Text).ToString("yyyy/MM/dd").Equals(Convert.ToDateTime(this.HiddenWorkDate.Value).ToString("yyyy/MM/dd")))
                {
                    WorkDateChanged = false;
                }
                //上班日期未改變
                if (Convert.ToDateTime(this.txtNoWorkDate.Text).ToString("yyyy/MM/dd").Equals(Convert.ToDateTime(this.HiddenNoWorkDate.Value).ToString("yyyy/MM/dd")))
                {
                    NoWorkDateChanged = false;
                }
            }
            //標準時數不能小於零
            if (Convert.ToInt32(this.txtTimeQty.Value) <= 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "show", "alert('" + Message.WorkTime0 + "')", true);
            }
            else
            {
                //獲取工作時數
                double DayWorkHours = Convert.ToDouble(moveShiftBll.GetValue("DayWorkHours", null));
                //調班時數必須為DayWorkHours
                if (Convert.ToDouble(this.txtTimeQty.Value) != DayWorkHours)
                {
                    alert = "alert('" + Message.MoveShiftCheckTimeQty + DayWorkHours.ToString() + Message.Hours + "')";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "show", alert, true);
                }
                else
                {
                    if (this.ProcessFlag.Value.Equals("Add"))
                    {
                        //判斷是否存在相同調班信息
                        this.tempDataTable = moveShiftBll.GetData(moveShiftModel, "condition2", null);
                        if (this.tempDataTable != null)
                        {
                            if (this.tempDataTable.Rows.Count > 0)
                            {
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "show", "alert('" + Message.duplicate + "')", true);
                                return;
                            }
                        }
                    }
                    else
                    {
                        //判斷是否存在其他調班信息
                        this.tempDataTable = moveShiftBll.GetData(moveShiftModel, "condition3", this.HiddenWorkDate.Value.Trim());
                        if (this.tempDataTable != null)
                        {
                            if (tempDataTable.Rows.Count > 0)
                            {
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "show", "alert('" + Message.duplicate + "')", true);
                                return;
                            }
                        }
                    }
                    if (NoWorkDateChanged)
                    {

                        //判斷上班日期是否小於當前日期
                        if (!((((Convert.ToDecimal(moveShiftBll.GetValue("condition1", moveShiftModel)) < 0M) || Convert.ToString(CurrentUserInfo.DepLevel).Equals("0")) || Convert.ToString(CurrentUserInfo.DepLevel).Equals("1")) || Convert.ToString(CurrentUserInfo.DepLevel).Equals("2")))
                        {

                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "show", "alert('" + Message.checknoworkdate + "')", true);
                            return;
                        }
                        ////判斷是否是在上班日期申報G2加班
                        //if (!moveShiftBll.GetValue("condition2", moveShiftModel).Equals("0"))
                        //{
                        //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "show", "alert('" + Message.checknoworkdateot + "')", true);
                        //    return;
                        //}
                        //判斷上班日期/調班日期是否存在加班衝突
                        DataTable tableOTInfoWork = moveShiftBll.GetOTInfo(this.txtEmployeeNo.Text.ToString(), this.txtWorkDate.Text.ToString());
                        DataTable tableOTInfoNoWork = moveShiftBll.GetOTInfo(this.txtEmployeeNo.Text.ToString(), this.txtNoWorkDate.Text.ToString());
                        string workstime = this.txtWorkSTime.Text.ToString();
                        string worketime = this.txtWorkETime.Text.ToString();
                        string noworkstime = this.txtNoWorkSTime.Text.ToString();
                        string noworketime = this.txtNoWorkETime.Text.ToString();
                        if (tableOTInfoWork.Rows.Count > 0)
                        {
                            string otstime = tableOTInfoWork.Rows[0]["begintime"].ToString();
                            string otetime = tableOTInfoWork.Rows[0]["endtime"].ToString();
                            if (string.IsNullOrEmpty(workstime) || string.IsNullOrEmpty(workstime))
                            {
                                //調班日期存在加班信息
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "show", "alert('" + Message.WorkDateHasOT + "')", true);
                                return;
                            }
                            else
                            {
                                if (string.IsNullOrEmpty(otstime) || string.IsNullOrEmpty(otetime))
                                {
                                    //調班日期存在加班信息
                                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "show", "alert('" + Message.WorkDateHasOT + "')", true);
                                    return;
                                }
                                else
                                {
                                    string result = moveShiftBll.GetTimeSpanFlag(workstime, worketime, otstime, otetime);
                                    if (result == "true")
                                    {
                                        //調班時間與當日加班時間衝突
                                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "show", "alert('" + Message.WorTimeBetweenOT + "')", true);
                                        return;
                                    }
                                }

                            }

                        }
                        if (tableOTInfoNoWork.Rows.Count > 0)
                        {
                            string otstime = tableOTInfoNoWork.Rows[0]["begintime"].ToString();
                            string otetime = tableOTInfoNoWork.Rows[0]["endtime"].ToString();
                            if (string.IsNullOrEmpty(noworkstime) || string.IsNullOrEmpty(noworkstime))
                            {
                                //上班日期存在加班信息
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "show", "alert('" + Message.NoWorkDateHasOT + "')", true);
                                return;
                            }
                            else
                            {
                                if (string.IsNullOrEmpty(otstime) || string.IsNullOrEmpty(otetime))
                                {
                                    //上班日期存在加班信息
                                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "show", "alert('" + Message.NoWorkDateHasOT + "')", true);
                                    return;
                                }
                                else
                                {
                                    string result = moveShiftBll.GetTimeSpanFlag(noworkstime, noworketime, otstime, otetime);
                                    if (result == "true")
                                    {
                                        //上班時間與當日加班時間衝突
                                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "show", "alert('" + Message.NoWorTimeBetweenOT + "')", true);
                                        return;
                                    }
                                }

                            }
                        }

                    }
                    //獲取上班日期的加班類型
                    string OTType = moveShiftBll.GetOTType(this.txtEmployeeNo.Text, this.txtNoWorkDate.Text);
                    //判斷上班日期是不是G2
                    if (this.ProcessFlag.Value.Equals("Add"))
                    {
                        if (!OTType.Equals("G2"))
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "show", "alert('" + Message.checknoworkdateg2 + "')", true);
                            return;
                        }
                    }
                    else if (!(OTType.Equals("G2") || !NoWorkDateChanged))
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "show", "alert('" + Message.checknoworkdateg2 + "')", true);
                        return;
                    }
                    //獲取調班日期的加班類型
                    OTType = moveShiftBll.GetOTType(this.txtEmployeeNo.Text, this.txtWorkDate.Text);
                    //判斷調班日期是不是G1
                    if (this.ProcessFlag.Value.Equals("Add"))
                    {
                        if (!OTType.Equals("G1"))
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "show", "alert('" + Message.checkworkdateg1 + "')", true);
                            return;
                        }
                    }
                    else if (!(OTType.Equals("G1") || !WorkDateChanged))
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "show", "alert('" + Message.checkworkdateg1 + "')", true);
                        return;
                    }
                    string sysKqoQinDays = moveShiftBll.GetValue("KQMReGetKaoQin", null);
                    string strModifyDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy/MM") + "/" + sysKqoQinDays).ToString("yyyy/MM/dd");
                    if (NoWorkDateChanged)
                    {
                        //上班日期小於當前月一日  且   當前日期大於允許重新計算的日期  時不允許變更上月及以前考勤記錄
                        if ((DateTime.Parse(this.txtNoWorkDate.Text).ToString("yyyy/MM/dd").CompareTo(DateTime.Now.ToString("yyyy/MM") + "/01") == -1) && (strModifyDate.CompareTo(DateTime.Now.ToString("yyyy/MM/dd")) <= 0))
                        {

                            alert = "alert('" + Message.checkreget + ":" + strModifyDate + "')";
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "show", alert, true);
                            return;
                        }
                        //上班日期小於上個月一日   不允許變更上月及以前考勤記錄
                        if (DateTime.Parse(this.txtNoWorkDate.Text).ToString("yyyy/MM/dd").CompareTo(DateTime.Now.AddMonths(-1).ToString("yyyy/MM") + "/01") == -1)
                        {
                            alert = "alert('" + Message.checkreget + ":" + strModifyDate + "')";
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "show", alert, true);
                            return;
                        }
                    }
                    if (WorkDateChanged)
                    {
                        //調班日期小於當前月一日  且   當前日期大於允許重新計算的日期  時不允許變更上月及以前考勤記錄
                        if ((DateTime.Parse(this.txtWorkDate.Text).ToString("yyyy/MM/dd").CompareTo(DateTime.Now.ToString("yyyy/MM") + "/01") == -1) && (strModifyDate.CompareTo(DateTime.Now.ToString("yyyy/MM/dd")) <= 0))
                        {
                            alert = "alert('" + Message.checkreget + ":" + strModifyDate + "')";
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "show", alert, true);

                            return;
                        }
                        //調班日期小於上個月一日   不允許變更上月及以前考勤記錄
                        if (DateTime.Parse(this.txtWorkDate.Text).ToString("yyyy/MM/dd").CompareTo(DateTime.Now.AddMonths(-1).ToString("yyyy/MM") + "/01") == -1)
                        {
                            alert = "alert('" + Message.checkreget + ":" + strModifyDate + "')";
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "show", alert, true);
                            return;
                        }
                    }


                    if (this.ProcessFlag.Value.Equals("Add"))
                    {
                        //判斷調班日期是否小於當前日期
                        if (!((((Convert.ToDecimal(moveShiftBll.GetValue("condition3", moveShiftModel)) < 0M) || Convert.ToString(CurrentUserInfo.DepLevel.Trim()).Equals("0")) || Convert.ToString(CurrentUserInfo.DepLevel.Trim()).Equals("1")) || Convert.ToString(CurrentUserInfo.DepLevel.Trim()).Equals("2")))
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "show", "alert('" + Message.checkworkdate + "')", true);
                            return;
                        }
                        //判斷是否存在相同調班信息
                        this.tempDataTable = moveShiftBll.GetData(moveShiftModel, "condition2", null);
                        if (this.tempDataTable != null)
                        {
                            if (this.tempDataTable.Rows.Count > 0)
                            {
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "show", "alert('" + Message.duplicate + "')", true);
                                return;
                            }
                        }

                        logmodel.ProcessFlag = "insert";
                        //新增操作
                        bool flag = moveShiftBll.AddMoveShift(moveShiftModel, logmodel);

                        this.HiddenSave.Value = "Save";
                    }
                    else if (this.ProcessFlag.Value.Equals("Modify"))
                    {
                        //判斷是否存在其他調班信息
                        this.tempDataTable = moveShiftBll.GetData(moveShiftModel, "condition3", this.HiddenWorkDate.Value.Trim());
                        if (this.tempDataTable != null)
                        {
                            if (this.tempDataTable.Rows.Count > 0)
                            {
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "show", "alert('" + Message.duplicate + "')", true);
                                return;
                            }
                        }
                        //判斷是否存在相同調班信息
                        this.tempDataTable = moveShiftBll.GetData(moveShiftModel, "condition4", this.HiddenWorkDate.Value.Trim());
                        if (this.tempDataTable != null)
                        {
                            if (this.tempDataTable.Rows.Count == 0)
                            {
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "show", "alert('" + Message.AtLastOneChoose + "')", true);
                                return;
                            }
                        }
                        logmodel.ProcessFlag = "update";
                        //更新操作
                        bool flag = moveShiftBll.UpdateMoveShift(moveShiftModel, this.HiddenWorkDate.Value.Trim(), logmodel);

                    }
                    if (NoWorkDateChanged)
                    {
                        //上班日期小於當前日期時重新計算考勤結果
                        if (DateTime.Parse(this.txtNoWorkDate.Text).ToString("yyyy/MM/dd").CompareTo(DateTime.Now.ToString("yyyy/MM/dd")) == -1)
                        {
                            moveShiftBll.GetKaoQinData(this.txtEmployeeNo.Text.Trim(), "null", DateTime.Parse(this.txtNoWorkDate.Text).ToString("yyyy/MM/dd"), DateTime.Parse(this.txtNoWorkDate.Text).ToString("yyyy/MM/dd"));
                        }
                        //修改時,若原上班日期小於當前日期則重新計算考勤結果
                        if ((this.ProcessFlag.Value == "Modify") && (DateTime.Parse(this.HiddenNoWorkDate.Value).ToString("yyyy/MM/dd").CompareTo(DateTime.Now.ToString("yyyy/MM/dd")) == -1))
                        {
                            moveShiftBll.GetKaoQinData(this.txtEmployeeNo.Text.Trim(), "null", DateTime.Parse(this.HiddenNoWorkDate.Value).ToString("yyyy/MM/dd"), DateTime.Parse(this.HiddenNoWorkDate.Value).ToString("yyyy/MM/dd"));
                        }
                    }
                    if (WorkDateChanged)
                    {
                        //調班日期小於當前日期時重新計算考勤結果
                        if (DateTime.Parse(this.txtWorkDate.Text).ToString("yyyy/MM/dd").CompareTo(DateTime.Now.ToString("yyyy/MM/dd")) == -1)
                        {
                            moveShiftBll.GetKaoQinData(this.txtEmployeeNo.Text.Trim(), "null", DateTime.Parse(this.txtWorkDate.Text).ToString("yyyy/MM/dd"), DateTime.Parse(this.txtWorkDate.Text).ToString("yyyy/MM/dd"));
                        }
                        //修改時,若原調班日期小於當前日期則重新計算考勤結果
                        if ((this.ProcessFlag.Value == "Modify") && (DateTime.Parse(this.HiddenWorkDate.Value).ToString("yyyy/MM/dd").CompareTo(DateTime.Now.ToString("yyyy/MM/dd")) == -1))
                        {
                            moveShiftBll.GetKaoQinData(this.txtEmployeeNo.Text.Trim(), "null", DateTime.Parse(this.HiddenWorkDate.Value).ToString("yyyy/MM/dd"), DateTime.Parse(this.HiddenWorkDate.Value).ToString("yyyy/MM/dd"));
                        }
                    }
                    ////新增成功則給出提示,修改成功則返回前頁
                    //if (this.ProcessFlag.Value == "Add")
                    //{
                    //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "show", "alert('" + Message.AddSuccess + "')", true);
                    //    this.Add();
                    //}
                    //else
                    //{
                    //    base.Response.Write("<script type='text/javascript'>window.parent.document.all.btnQuery.click();</script>");
                    //}
                    //新增或修改之後直接返回前一頁面
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
        /// 修改
        /// </summary>
        /// <param name="EmployeeNo"></param>
        /// <param name="WorkDate"></param>
        /// <param name="NoWorkDate"></param>
        protected void Modify(string EmployeeNo, string WorkDate, string NoWorkDate)
        {
            this.txtEmployeeNo.Text = EmployeeNo;
            this.EmpQuery(EmployeeNo, WorkDate, NoWorkDate);
            this.ProcessFlag.Value = "Modify";
            this.TextBoxsReset("Modify", false);
        }

        #endregion

        #region 獲取員工信息
        /// <summary>
        /// 查詢員工信息
        /// </summary>
        /// <param name="EmployeeNo"></param>
        /// <param name="WorkDate"></param>
        /// <param name="NoWorkDate"></param>
        private void EmpQuery(string EmployeeNo, string WorkDate, string NoWorkDate)
        {
            this.tempDataTable = moveShiftBll.GetVData(EmployeeNo, base.SqlDep.ToString());
            //若員工信息存在,則填充文本框
            if (this.tempDataTable != null)
            {
                foreach (DataRow newRow in this.tempDataTable.Rows)
                {
                    this.txtEmployeeNo.Text = newRow["WORKNO"].ToString();
                    this.txtName.Text = newRow["LOCALNAME"].ToString();
                    this.txtDPcode.Text = newRow["DName"].ToString();
                }
                this.Query(EmployeeNo, WorkDate, NoWorkDate);
                this.tempDataTable.Clear();
            }
            //員工信息不存在,則清空文本框并給出彈窗提示
            else
            {
                this.txtName.Text = "";
                this.txtDPcode.Text = "";
                this.txtWorkDate.Text = "";
                this.txtWorkSTime.Text = "";
                this.txtWorkETime.Text = "";
                this.txtNoWorkDate.Text = "";
                this.txtNoWorkSTime.Text = "";
                this.txtNoWorkETime.Text = "";
                this.txtTimeQty.Value = 0;
                this.txtRemark.Text = "";
                this.TextBoxsReset("", true);
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "show", "alert('" + Message.WorkNONotExist + "')", true);
            }

        }

        /// <summary>
        /// Ajax方法重載
        /// </summary>
        protected override void AjaxProcess()
        {
            string noticeJson = null;
            if (!string.IsNullOrEmpty(Request.Form["Empno"]))
            {
                string empno = Request.Form["Empno"];
                string sqlDep = Request.Form["SqlDep"];
                tempDataTable = moveShiftBll.GetVData(empno, sqlDep);
                if (tempDataTable != null)
                {
                    if (tempDataTable.Rows.Count > 0)
                    {
                        moveShiftModel.WorkNo = tempDataTable.Rows[0]["WorkNo"].ToString();
                        moveShiftModel.LocalName = tempDataTable.Rows[0]["LocalName"].ToString();
                        moveShiftModel.DepName = tempDataTable.Rows[0]["DepName"].ToString();
                    }
                }
                if (moveShiftModel != null)
                {
                    noticeJson = JsSerializer.Serialize(moveShiftModel);
                }
                Response.Clear();
                Response.Write(noticeJson);
                Response.End();
            }
        }

        #endregion

        #region 查詢調班信息
        /// <summary>
        /// 查詢調班信息
        /// </summary>
        /// <param name="EmployeeNo"></param>
        /// <param name="WorkDate"></param>
        /// <param name="NoWorkDate"></param>
        private void Query(string EmployeeNo, string WorkDate, string NoWorkDate)
        {
            moveShiftModel.WorkNo = EmployeeNo.ToString().ToUpper();
            moveShiftModel.WorkDate = DateTime.Parse(WorkDate);
            moveShiftModel.NoWorkDate = DateTime.Parse(NoWorkDate);
            tempDataTable = moveShiftBll.GetData(moveShiftModel, "condition1", base.SqlDep.ToString());
            if (this.tempDataTable != null)
            {
                if (tempDataTable.Rows.Count > 0)
                    this.TextDataBind(true);
            }
            else
            {
                this.TextDataBind(false);
            }

        }

        #endregion

        #region 文本框操作
        /// <summary>
        /// 文本框控制
        /// </summary>
        /// <param name="buttonText"></param>
        /// <param name="read"></param>
        private void TextBoxsReset(string buttonText, bool read)
        {
            this.txtEmployeeNo.BorderStyle = BorderStyle.None;
            this.txtName.BorderStyle = BorderStyle.None;
            this.txtDPcode.BorderStyle = BorderStyle.None;
            this.txtWorkDate.BorderStyle = read ? BorderStyle.None : BorderStyle.NotSet;
            this.txtWorkSTime.BorderStyle = read ? BorderStyle.None : BorderStyle.NotSet;
            this.txtWorkETime.BorderStyle = read ? BorderStyle.None : BorderStyle.NotSet;
            this.txtNoWorkDate.BorderStyle = read ? BorderStyle.None : BorderStyle.NotSet;
            this.txtNoWorkSTime.BorderStyle = read ? BorderStyle.None : BorderStyle.NotSet;
            this.txtNoWorkETime.BorderStyle = read ? BorderStyle.None : BorderStyle.NotSet;
            this.txtTimeQty.BorderStyle = read ? BorderStyle.None : BorderStyle.NotSet;
            this.txtRemark.BorderStyle = read ? BorderStyle.None : BorderStyle.NotSet;
            if (buttonText.Equals("Add"))
            {
                this.txtEmployeeNo.BorderStyle = BorderStyle.NotSet;
                this.txtWorkDate.Text = "";
                this.txtWorkSTime.Text = "";
                this.txtWorkETime.Text = "";
                this.txtNoWorkDate.Text = "";
                this.txtNoWorkSTime.Text = "";
                this.txtNoWorkETime.Text = "";
                this.txtTimeQty.Value = 8;
                this.txtRemark.Text = "";
            }
            if (buttonText.Equals("Modify"))
            {
            }
        }

        /// <summary>
        /// 文本框綁定
        /// </summary>
        /// <param name="read"></param>
        private void TextDataBind(bool read)
        {
            if (read)
            {

                if (this.tempDataTable.Rows[0]["WorkDate"].ToString().Trim() != "")
                {
                    this.txtWorkDate.Text = DateTime.Parse(this.tempDataTable.Rows[0]["WorkDate"].ToString()).ToString("yyyy/MM/dd");
                    this.HiddenWorkDate.Value = DateTime.Parse(this.tempDataTable.Rows[0]["WorkDate"].ToString()).ToString("yyyy/MM/dd");
                }
                else
                {
                    this.txtWorkDate.Text = "";
                }
                if (this.tempDataTable.Rows[0]["WorkSTime"].ToString().Length > 0)
                {
                    this.txtWorkSTime.Text = this.tempDataTable.Rows[0]["WorkSTime"].ToString();
                }
                if (this.tempDataTable.Rows[0]["WorkETime"].ToString().Length > 0)
                {
                    this.txtWorkETime.Text = this.tempDataTable.Rows[0]["WorkETime"].ToString();
                }
                if (this.tempDataTable.Rows[0]["NoWorkDate"].ToString().Trim() != "")
                {
                    this.txtNoWorkDate.Text = DateTime.Parse(this.tempDataTable.Rows[0]["NoWorkDate"].ToString()).ToString("yyyy/MM/dd");
                    this.HiddenNoWorkDate.Value = DateTime.Parse(this.tempDataTable.Rows[0]["NoWorkDate"].ToString()).ToString("yyyy/MM/dd");
                }
                else
                {
                    this.txtNoWorkDate.Text = "";
                }
                if (this.tempDataTable.Rows[0]["NoWorkSTime"].ToString().Length > 0)
                {
                    this.txtNoWorkSTime.Text = this.tempDataTable.Rows[0]["NoWorkSTime"].ToString();
                }
                if (this.tempDataTable.Rows[0]["NoWorkETime"].ToString().Length > 0)
                {
                    this.txtNoWorkETime.Text = this.tempDataTable.Rows[0]["NoWorkETime"].ToString();
                }
                this.txtTimeQty.Value = this.tempDataTable.Rows[0]["TimeQty"].ToString();
                this.txtRemark.Text = this.tempDataTable.Rows[0]["Remark"].ToString();

            }
            else
            {
                this.txtWorkDate.Text = "";
                this.txtWorkSTime.Text = "";
                this.txtWorkETime.Text = "";
                this.txtNoWorkDate.Text = "";
                this.txtNoWorkSTime.Text = "";
                this.txtNoWorkETime.Text = "";
                this.txtTimeQty.Value = 0;
                this.txtRemark.Text = "";
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
