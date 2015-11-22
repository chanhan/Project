using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using GDSBG.MiABU.Attendance.BLL.WorkFlow;
using GDSBG.MiABU.Attendance.Model.KQM.KaoQinData;
using GDSBG.MiABU.Attendance.BLL.KQM.KaoQinData;
using GDSBG.MiABU.Attendance.BLL.KQM.Query;
using Resources;
using System.Web.Script.Serialization;
using GDSBG.MiABU.Attendance.Model.WorkFlow;

namespace GDSBG.MiABU.Attendance.Web.WorkFlowForm
{
    public partial class WorkFlowMakeupEditForm : BasePage
    {
        protected DataSet tempDataSet = new DataSet();
        protected DataTable tempDataTable = new DataTable();

        BellCardQueryBll bellCardQueryBll = new BellCardQueryBll();
        JavaScriptSerializer JsSerializer = new JavaScriptSerializer();
        Dictionary<string, string> ClientMessage = null;
        MoveShiftModel moveShiftModel = new MoveShiftModel();
        WorkFlowCardMakeupModel CardMakeupModel = new WorkFlowCardMakeupModel();
        WorkFlowSetBll workflowset = new WorkFlowSetBll();
        WorkFlowCardMakeupBll cardMakeupBll = new WorkFlowCardMakeupBll();


        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            { 

                ddclDataBind("KQMMakeup", this.ddlMakeupType);
                ddlExceptionDataBind();
                string EmployeeNo = (base.Request.QueryString["EmployeeNo"] == null) ? "" : Request.QueryString["EmployeeNo"].ToString();
                string ID = (Request.QueryString["ID"] == null) ? "" : Request.QueryString["ID"].ToString();
                string ProcessFlag = (base.Request.QueryString["ProcessFlag"] == null) ? "" : Request.QueryString["ProcessFlag"].ToString();
                this.HiddenID.Value = ID;
                this.ProcessFlag.Value = ProcessFlag;

                this.HiddenID.Value = ID;
                this.ProcessFlag.Value = ProcessFlag;
                string ModuleCode = Request.QueryString["ModuleCode"].ToString();
                HiddenModuleCode.Value = ModuleCode;

                SetCalendar(textBoxKQDate);
               // if (ModuleCode != "PCMSYS108")  
               // {

                this.textBoxEmployeeNo.Attributes.Add("onblur", "GetEmp();");
                this.textBoxKQDate.Attributes.Add("onpropertychange", "javascript:GetShiftDesc();");

               // }
                 
                
                this.ddlMakeupType.Attributes.Add("onChange", "onChangeMakeupType(this.options[this.selectedIndex].value)");
              //  this.ddlReasonType.Attributes.Add("onChange", "onChangeReasonType(this.options[this.selectedIndex].value)");

               
               
                if (ProcessFlag == "Add")
                {
                    if (ModuleCode == "PCMSYS108")
                    {
                        EmployeeNo = CurrentUserInfo.Personcode;
                    }
                    else
                    {
                        EmployeeNo = "";
                    }

                    this.Add(EmployeeNo);
                }
                else if ((EmployeeNo.Length > 0) && (ID.Length > 0))
                {
                    if (ModuleCode == "PCMSYS108")  //如果為個人中心傳值，則設置工號為當前用戶
                    {
                        EmployeeNo = CurrentUserInfo.Personcode;  //當Link傳遞過來的值不為空時，設置工號為當前用戶工號（以防有用戶惡意刷數據）
                        
                        this.textBoxEmployeeNo.Text = CurrentUserInfo.Personcode;
                        this.textBoxLocalName.Text = CurrentUserInfo.Cname;
                        this.textBoxDepName.Text = CurrentUserInfo.DepName;
                        this.textBoxEmployeeNo.Enabled = false;
                        this.textBoxLocalName.Enabled = false;
                        this.textBoxDepName.Enabled = false;
                    }

                    this.Modify(EmployeeNo, ID);
                   
                }
                else
                {
                   // Response.Write("<script type='text/javascript'>alert(\"沒有數據\");window.parent.document.all.topTable.style.display='';window.parent.document.all.divEdit.style.display='none';</script>");

                    Response.Write("<script type='text/javascript'>alert(\"" +Message.common_message_data_select + "\");window.parent.document.all.topTable.style.display='';window.parent.document.all.divEdit.style.display='none';</script>");
                }

               
            }


            if (ClientMessage == null)
            {
                ClientMessage = new Dictionary<string, string>();
                ClientMessage.Add("EmpNotExist", Message.EmpNotExist);
                ClientMessage.Add("TextBoxNotNull", Message.TextBoxNotNull);
            }

        }

        protected void ddlExceptionDataBind()
        {
            this.tempDataTable.Clear();
            // this.tempDataSet = ((ServiceLocator)this.Session["serviceLocator"]).GetKqmExceptReasonData().GetDataByCondition("WHERE EffectFlag='Y' ORDER BY ReasonNo");
            this.tempDataTable = cardMakeupBll.KQMExceptionList(" and EffectFlag='Y' ORDER BY ReasonNo ");
            this.ddlReasonType.DataSource = tempDataTable;
            this.ddlReasonType.DataTextField = "ReasonName";
            this.ddlReasonType.DataValueField = "ReasonNo";
            this.ddlReasonType.DataBind();
            this.ddlReasonType.Items.Insert(0, new ListItem("", ""));
        }
        /// <summary>
        /// 下拉框值綁定
        /// </summary>
        /// <param name="type">綁定的數據類型</param>
        /// <param name="ddcl">菜單名</param>
        private void ddclDataBind(string type, DropDownList ddcl)
        {
            this.tempDataTable.Clear();
            this.tempDataTable = workflowset.GetDocNoTypeList(type);
            ddcl.DataSource = tempDataTable;
            ddcl.DataTextField = "DataValue";
            ddcl.DataValueField = "DataCode";
            ddcl.DataBind();
            ddcl.Items.Insert(0, new ListItem("", ""));
        }

        /// <summary>
        /// 獲取初始數據
        /// </summary>
        protected void DataBinds()
        {

        }

        /// <summary>
        /// 存儲
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            try
            {

                string makeTypeStr;
                DataRow row;
                //  if (!(((CheckData(this.textBoxEmployeeNo.Text, "common.employeeno") && CheckData(this.textBoxKQDate.Text, "bfw.bfw_kqm_KaoQinQuery.KQDate")) && (CheckData(this.textBoxCardTime.Text, "bfw.kqm_kaoqindata.cardtime") && CheckData(this.ddlMakeupType.SelectedValue, "kqm.makeup.makeuptype"))) && CheckData(this.ddlReasonType.SelectedValue, "bfw.bfw_kqm_KaoQinQuery.ReasonType")))
                //  {
                //     return;
                //  }
                string CardTime = "";
                string sKQDate = "";
                try
                {
                    sKQDate = Convert.ToDateTime(this.textBoxKQDate.Text).ToString("yyyy/MM/dd");
                }
                catch (Exception)
                {
                    WriteMessage(1,Message.common_message_data_errordate);
                    return;
                }
                try
                {
                    CardTime = DateTime.Parse(this.textBoxCardTime.Text).ToString("HH:mm");
                }
                catch (Exception)
                {

                    WriteMessage(1, Message.CardTime + Message.timeerror);
                    return;
                }
                string sysKqoQinDays = GetsysKqoQinDays();
                string condition = "";
                string nCardTime = "";
                string WorkNo = this.textBoxEmployeeNo.Text;

                if (!cardMakeupBll.GetValue("SELECT NVL(max(paravalue),'N') FROM gds_sc_parameter WHERE paraname='IsAllowMakeUpByIsNotKaoQin'").Equals("Y"))
                {
                    goto Label_03EF;
                }


                string condIsEvction = "SELECT NVL(count(0),0) FROM gds_att_employee a WHERE a.status = '0' AND NVL (a.joindate, TO_Date('" + sKQDate + "','yyyy/MM/dd') - 1) < TO_Date('" + sKQDate + "','yyyy/MM/dd') AND EXISTS ( SELECT 1 FROM gds_att_evectionapply e WHERE e.workno = a.workno AND (e.status = '2' OR e.status = '4') AND e.startdate <= TO_Date('" + sKQDate + "','yyyy/MM/dd') AND TO_Date('" + sKQDate + "','yyyy/MM/dd')  <= e.enddate AND e.isnotkaoqin = 'Y') AND a.workno='" + WorkNo + "'";
                if (cardMakeupBll.GetValue(condIsEvction).Equals("1"))
                {
                    condition = " and a.WorkNo='" + WorkNo + "' And a.KQDATE=to_date('" + sKQDate + "','yyyy/MM/dd')  and a.status<>'2'  and trunc(sysdate)-a.kqdate<=" + sysKqoQinDays;
                }
                else
                {
                    makeTypeStr = this.ddlMakeupType.SelectedValue;
                    if (makeTypeStr != null)
                    {
                        if (!(makeTypeStr == "0"))
                        {
                            if (makeTypeStr == "1")
                            {
                                goto Label_0322;
                            }
                            if (makeTypeStr == "2")
                            {
                                goto Label_0364;
                            }
                            if (makeTypeStr == "3")
                            {
                                goto Label_03A6;
                            }
                        }
                        else
                        {
                            condition = " and a.WorkNo='" + WorkNo + "' And a.KQDATE=to_date('" + sKQDate + "','yyyy/MM/dd') and a.ExceptionType in('D','C','O','F') and a.status<>'2' and a.OnDutyTime is null and trunc(sysdate)-a.kqdate<=" + sysKqoQinDays;
                        }
                    }
                }
                goto Label_0556;
            Label_0322: ;
                condition = " and a.WorkNo='" + WorkNo + "' And a.KQDATE=to_date('" + sKQDate + "','yyyy/mm/dd') and a.ExceptionType in('D','C','O','F') and a.status<>'2' and a.OffDutyTime is null and trunc(sysdate)-a.kqdate<=" + sysKqoQinDays;
                goto Label_0556;
            Label_0364: ;
                condition = " and a.WorkNo='" + WorkNo + "' And a.KQDATE=to_date('" + sKQDate + "','yyyy/mm/dd') and (a.ExceptionType not in('O','F') or a.ExcepTionType is null) and a.OTOnDutyTime is null and trunc(sysdate)-a.kqdate<=" + sysKqoQinDays;
                goto Label_0556;
            Label_03A6: ;
                condition = " and a.WorkNo='" + WorkNo + "' And a.KQDATE=to_date('" + sKQDate + "','yyyy/mm/dd') and (a.ExceptionType not in('O','F') or a.ExcepTionType is null) and a.OTOffDutyTime is null and trunc(sysdate)-a.kqdate<=" + sysKqoQinDays;
                goto Label_0556;
            Label_03EF:
                makeTypeStr = this.ddlMakeupType.SelectedValue;
                if (makeTypeStr != null)
                {
                    if (!(makeTypeStr == "0"))
                    {
                        if (makeTypeStr == "1")
                        {
                            goto Label_048F;
                        }
                        if (makeTypeStr == "2")
                        {
                            goto Label_04D1;
                        }
                        if (makeTypeStr == "3")
                        {
                            goto Label_0513;
                        }
                    }
                    else
                    {
                        condition = " and a.WorkNo='" + WorkNo + "' And a.KQDATE=to_date('" + sKQDate + "','yyyy/mm/dd') and a.ExceptionType in('D','C','O','F') and a.status<>'2' and a.OnDutyTime is null and trunc(sysdate)-a.kqdate<=" + sysKqoQinDays;
                    }
                }
                goto Label_0556;
            Label_048F: ;
                condition = " and a.WorkNo='" + WorkNo + "' And a.KQDATE=to_date('" + sKQDate + "','yyyy/mm/dd') and a.ExceptionType in('D','C','O','F') and a.status<>'2' and a.OffDutyTime is null and trunc(sysdate)-a.kqdate<=" + sysKqoQinDays;
                goto Label_0556;
            Label_04D1: ;
                condition = " and a.WorkNo='" + WorkNo + "' And a.KQDATE=to_date('" + sKQDate + "','yyyy/mm/dd') and (a.ExceptionType not in('O','F') or a.ExcepTionType is null) and a.OTOnDutyTime is null and trunc(sysdate)-a.kqdate<=" + sysKqoQinDays;
                goto Label_0556;
            Label_0513: ;
                condition = " and a.WorkNo='" + WorkNo + "' And a.KQDATE=to_date('" + sKQDate + "','yyyy/mm/dd') and (a.ExceptionType not in('O','F') or a.ExcepTionType is null) and a.OTOffDutyTime is null and trunc(sysdate)-a.kqdate<=" + sysKqoQinDays;
            Label_0556:
                this.tempDataTable = cardMakeupBll.GetKaoQinDataByCondition(condition);
                DateTime DToDay = Convert.ToDateTime(DateTime.Now.ToString("yyyy/MM/dd"));
                if (this.tempDataTable.Rows.Count > 0)
                {
                    if (cardMakeupBll.GetValue("select nvl(MAX(paravalue),'N') from gds_sc_parameter where paraname='IsOpenCardDataNumber'") != "N")
                    {
                        string startDate = DateTime.Now.AddDays((double)(-DateTime.Now.Day + 1)).ToString("yyyy-MM-dd");
                        string endDate = DateTime.Now.ToString("yyyy-MM-dd");
                        string EmployeeNo = base.Request.QueryString["EmployeeNo"].ToString();
                        string sql = " select count(1) from GDS_att_Makeup where workno = '" + EmployeeNo.ToUpper() + "' and kqdate >= to_date('" + startDate + "','yyyy-MM-dd') and kqdate <= to_date('" + endDate + "','yyyy-MM-dd') and reasontype in (select reasonno from gds_att_EXCEPTREASON where effectflag = 'Y' and salaryflag = 'Y')";
                        string count = cardMakeupBll.GetValue(sql);
                        string number = cardMakeupBll.GetValue("select nvl(MAX(paravalue),'0') from gds_sc_parameter where paraname='AddCardDataNumber'");
                        if ((number != "0") && (count == number))
                        {
                            WriteMessage(0, Message.forgetcardnumber + number + Message.common_times);
                            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "show", "alert('" + Message.forgetcardnumber + number + Message.common_times + "')", true);
                            return;
                        }
                    }
                    string ShiftNo = this.tempDataTable.Rows[0]["ShiftNo"].ToString();
                    nCardTime = string.Format(sKQDate + " " + CardTime, "yyyy/MM/dd HH:mm");
                    nCardTime = cardMakeupBll.ReturnCardTime(WorkNo, sKQDate, nCardTime, ShiftNo, this.ddlMakeupType.SelectedValue);
                    if (((DToDay.CompareTo(Convert.ToDateTime(sKQDate)) == 0) && ShiftNo.StartsWith("C")) && (TimeSpan.Parse(DateTime.Now.ToString("HH:mm")) < TimeSpan.Parse("19:00")))
                    {
                        WriteMessage(1,Message.kqm_kaoqindata_checknightkqdata);
                        return;
                    }
                    if (string.Compare(nCardTime, DateTime.Now.ToString("yyyy/MM/dd HH:mm")) > 0)
                    {
                        WriteMessage(1, Message.CardTime + Message.timeerror);
                        return;
                    }
                    goto Label_094E;
                }
                makeTypeStr = this.ddlMakeupType.SelectedValue;
                if (makeTypeStr != null)
                {
                    if (!(makeTypeStr == "0"))
                    {
                        if (makeTypeStr == "1")
                        {
                            goto Label_08EC;
                        }
                        if (makeTypeStr == "2")
                        {
                            goto Label_090B;
                        }
                        if (makeTypeStr == "3")
                        {
                            goto Label_092A;
                        }
                    }
                    else
                    {
                        WriteMessage(1,Message.kqm_kaoqindata_errorrkaoqindata + sysKqoQinDays);
                    }
                }
                return;
            Label_08EC:
                WriteMessage(1,Message.kqm_kaoqindata_errorrkaoqindata + sysKqoQinDays);
                return;
            Label_090B:
                WriteMessage(1, Message.kqm_kaoqindata_errorrkaoqindata + sysKqoQinDays);
                return;
            Label_092A:
                WriteMessage(1, Message.kqm_kaoqindata_errorrkaoqindata + sysKqoQinDays);
                return;
            Label_094E:
                //查看指定的時間是否已經存在刷卡數據
                /////// this.tempDataTable = bellCardQueryBll.GetDataByCondition("gds_att_BELLCARDDATA", "and b.WorkNo='" + WorkNo + "' and a.CardTime = to_date('" + DateTime.Parse(nCardTime).ToString("yyyy/MM/dd HH:mm") + "','yyyy/mm/dd hh24:mi:ss')").Tables["KQM_BellCardQuery"];
                // if (this.tempDataTable.Rows.Count > 0)
                if (cardMakeupBll.GetValue("select nvl(MAX(PROCESSFLAG),'N') from gds_att_BELLCARDDATA a where a.WorkNo='" + WorkNo + "' and a.CardTime = to_date('" + DateTime.Parse(nCardTime).ToString("yyyy/MM/dd HH:mm") + "','yyyy/mm/dd hh24:mi:ss')") != "N")
                {
                    WriteMessage(1, Message.makeup_checkrepeat);
                    return;
                }

                //新增數據
                if (this.ProcessFlag.Value.Equals("Add"))
                {

                    this.tempDataTable = cardMakeupBll.GetDataByCondition("and a.WorkNo='" + WorkNo + "' and ((a.KQDate = to_date('" + DateTime.Parse(sKQDate).ToString("yyyy/MM/dd") + "','yyyy/mm/dd') and a.MakeupType='" + this.ddlMakeupType.SelectedValue + "') or a.CardTime = to_date('" + DateTime.Parse(nCardTime).ToString("yyyy/MM/dd HH:mm") + "','yyyy/mm/dd hh24:mi:ss')) ");
                    if (this.tempDataTable.Rows.Count > 0)
                    {
                        WriteMessage(1, Message.makeup_checkrepeat);
                        return;
                    }
                    row = this.tempDataTable.NewRow();
                    row.BeginEdit();
                    row["WorkNo"] = WorkNo;
                    row["KQDate"] = sKQDate;
                    row["CardTime"] = nCardTime;
                    row["MakeupType"] = this.ddlMakeupType.SelectedValue;
                    row["ReasonType"] = this.ddlReasonType.SelectedValue;
                    row["ReasonRemark"] = this.textBoxReasonRemark.Text.Trim();
                    row["DecSalary"] = (this.HiddenSalaryFlag.Value == "") ? "Y" : this.HiddenSalaryFlag.Value;
                    row["Modifier"] = CurrentUserInfo.Personcode.ToString();
                    //CurrentUserInfo.
                    row.EndEdit();
                    this.tempDataTable.Rows.Add(row);
                    this.tempDataTable.AcceptChanges();
                    cardMakeupBll.SaveData(this.ProcessFlag.Value, this.tempDataTable);
                    this.HiddenSave.Value = "Save";
                }

                //修改數據
                else if (this.ProcessFlag.Value.Equals("Modify"))
                {

                    this.tempDataTable = cardMakeupBll.GetDataByCondition("and a.ID<>'" + this.HiddenID.Value + "' and a.WorkNo='" + WorkNo + "' and a.KQDate = to_date('" + DateTime.Parse(sKQDate).ToString("yyyy/MM/dd") + "','yyyy/mm/dd') and (a.MakeupType='" + this.ddlMakeupType.SelectedValue + "' or a.CardTime = to_date('" + DateTime.Parse(nCardTime).ToString("yyyy/MM/dd HH:mm") + "','yyyy/mm/dd hh24:mi:ss')) ");
                    if (this.tempDataTable.Rows.Count > 0)
                    {
                        WriteMessage(1, Message.makeup_checkrepeat);
                        return;
                    }
                    this.tempDataTable = cardMakeupBll.GetDataByCondition("and a.ID='" + this.HiddenID.Value + "'");
                    if (this.tempDataTable.Rows.Count == 0)
                    {
                        WriteMessage(1, Message.NoItemSelected);
                        return;
                    }
                    row = this.tempDataTable.Rows[0];
                    row.BeginEdit();
                    row["ID"] = this.HiddenID.Value;
                    row["WorkNo"] = WorkNo;
                    row["KQDate"] = sKQDate;
                    row["CardTime"] = nCardTime;
                    row["MakeupType"] = this.ddlMakeupType.SelectedValue;
                    row["ReasonType"] = this.ddlReasonType.SelectedValue;
                    row["ReasonRemark"] = this.textBoxReasonRemark.Text.Trim();
                    row["DecSalary"] = (this.HiddenSalaryFlag.Value == "") ? "Y" : this.HiddenSalaryFlag.Value;
                    row["Modifier"] = CurrentUserInfo.Personcode.ToString();    //當前用戶工號
                    if (this.HiddenState.Value == "3")
                    {
                        row["Status"] = "0";
                    }
                    row.EndEdit();
                    this.tempDataTable.AcceptChanges();
                    cardMakeupBll.SaveData(this.ProcessFlag.Value, this.tempDataTable);
                }

                //新增
                if (this.ProcessFlag.Value == "Add")
                {
                    base.Response.Write("<script type='text/javascript'>alert(\"" + Message.AddSuccess + "\");window.parent.document.all.btnFormQuery.click();</script>");
                    //WriteMessage(0, Message.AddSuccess);
                   // this.Add("");
                }
                else
                { 
 
                     base.Response.Write("<script type='text/javascript'>alert('" + Message.UpdateSuccess + "');window.parent.document.all.btnFormQuery.click();</script>"); 
                }
            }
            catch (Exception ex)
            {
                WriteMessage(2, (ex.InnerException == null) ? ex.Message : ex.InnerException.Message);
            }
        }

        /// <summary>
        /// 工號改變方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void textBoxEmployeeNo_TextChanged(object sender, EventArgs e)
        {
            string employee_no = this.textBoxEmployeeNo.Text.ToString();

        }
        #region 獲取員工信息
        /// <summary>
        /// 查詢員工信息
        /// </summary>
        /// <param name="EmployeeNo"></param>
        /// <param name="WorkDate"></param>
        /// <param name="NoWorkDate"></param>
        private void EmpQuery(string EmployeeNo, string WorkDate, string NoWorkDate)
        {
            this.tempDataTable = cardMakeupBll.GetVData(EmployeeNo, base.SqlDep.ToString());
            //若員工信息存在,則填充文本框
            if (this.tempDataTable != null)
            {
                foreach (DataRow newRow in this.tempDataTable.Rows)
                {
                    this.textBoxEmployeeNo.Text = newRow["WORKNO"].ToString();
                    this.textBoxLocalName.Text = newRow["LOCALNAME"].ToString();
                    this.textBoxDepName.Text = newRow["DName"].ToString();
                }
                this.tempDataTable.Clear();
            }
            //員工信息不存在,則清空文本框并給出彈窗提示
            else
            {
                this.textBoxEmployeeNo.Text = "";
                this.textBoxLocalName.Text = "";
                this.textBoxDepName.Text = "";
                this.textBoxKQDate.Text = "";
                this.textBoxCardTime.Text = "";
                this.ddlMakeupType.SelectedValue = "";
                this.ddlReasonType.SelectedValue = "";
                this.textBoxReasonRemark.Text = "";

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
                tempDataTable = cardMakeupBll.GetVData(empno, sqlDep);
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

        #region 返回sqlDep
        public string GetSqlDep()
        {
            return base.SqlDep.ToString();
        }
        #endregion

        #region
        private void TextBoxsReset(string buttonText, bool read)
        {
            this.textBoxLocalName.BorderStyle = BorderStyle.None;
            this.textBoxDepName.BorderStyle = BorderStyle.None;
            this.textBoxEmployeeNo.BorderStyle = BorderStyle.None;
            if (buttonText.Equals("Add"))
            {
                this.textBoxKQDate.Text = "";
                this.textBoxCardTime.Text = "";
                this.textBoxReasonRemark.Text = "";
                this.ddlMakeupType.ClearSelection();
                this.ddlReasonType.ClearSelection();
                this.textBoxEmployeeNo.BorderStyle = BorderStyle.NotSet;
            }
        }


        protected void Add(string EmployeeNo)
        {
            //增加個人管理中心判斷
            if (this.HiddenSave.Value != "Save" && EmployeeNo!="")
            {
                this.textBoxEmployeeNo.Text = "";
            }
            if (EmployeeNo.Length>0)
            {

                EmployeeNo = CurrentUserInfo.Personcode;    //當Link傳遞過來的值不為空時，設置工號為當前用戶工號（以防有用戶惡意刷數據）
                this.textBoxEmployeeNo.Text = CurrentUserInfo.Personcode;
                this.textBoxLocalName.Text = CurrentUserInfo.Cname;
                this.textBoxDepName.Text = CurrentUserInfo.DepName;
                this.textBoxEmployeeNo.Enabled = false;
                this.textBoxLocalName.Enabled = false;
                this.textBoxDepName.Enabled = false;
                 
            }

            this.ProcessFlag.Value = "Add";
            this.TextBoxsReset("Add", false);

        }

        protected void Modify(string EmployeeNo, string ID)
        {
            this.textBoxEmployeeNo.Text = EmployeeNo;
            this.EmpQuery(EmployeeNo, ID);
            this.ProcessFlag.Value = "Modify";
            this.TextBoxsReset("Modify", false);
        }


        private void EmpQuery(string EmployeeNo, string ID)
        {
            string condition = "WHERE a.WorkNO='" + EmployeeNo.ToUpper() + "'";

            //角色權限判斷 get Role/Power
            /** if (base.bPrivileged)   //
             {
                 condition = condition + " AND exists (SELECT 1 FROM (" + base.sqlDep + ") e where e.DepCode=a.DCode)";
             } 
             this.tempDataTable = ((ServiceLocator)this.Session["serviceLocator"]).GetFunctionData().GetVDataByCondition(condition).Tables["V_Employee"];
             **/
            this.tempDataTable = cardMakeupBll.GetVData(EmployeeNo, base.SqlDep.ToString());
            if (this.tempDataTable.Rows.Count > 0)
            {
                foreach (DataRow newRow in this.tempDataTable.Rows)
                {
                    this.textBoxLocalName.Text = newRow["LOCALNAME"].ToString();
                    this.textBoxDepName.Text = newRow["DName"].ToString();
                }
                this.Query(EmployeeNo, ID);
            }
            else
            {
                this.textBoxLocalName.Text = "";
                this.textBoxDepName.Text = "";
                this.textBoxKQDate.Text = "";
                this.textBoxCardTime.Text = "";
                this.textBoxReasonRemark.Text = "";
                this.ddlMakeupType.ClearSelection();
                this.ddlReasonType.ClearSelection();
                this.TextBoxsReset("", true);

                WriteMessage(1, Message.EmpBasicInfoNotExist);
            }
            this.tempDataTable.Clear();
        }

        private void Query(string EmployeeNo, string ID)
        {
            string condition = "and a.WorkNO='" + EmployeeNo.ToUpper() + "' AND a.ID='" + ID + "' ";
            /**
              //權限判斷
            if (base.bPrivileged)
            {
                condition = condition + " AND exists (SELECT 1 FROM (" + base.sqlDep + ") e where e.DepCode=b.DCode)";
            }
             **/
            this.tempDataTable = cardMakeupBll.GetDataByCondition(condition);
            if (this.tempDataTable.Rows.Count > 0)
            {
                if (this.tempDataTable.Rows[0]["KQDate"].ToString().Length > 0)
                {
                    this.textBoxKQDate.Text = DateTime.Parse(this.tempDataTable.Rows[0]["KQDate"].ToString()).ToString("yyyy/MM/dd");
                }
                else
                {
                    this.textBoxKQDate.Text = "";
                }
                this.textBoxCardTime.Text = Convert.ToDateTime(this.tempDataTable.Rows[0]["CardTime"].ToString()).ToString("HH:mm");
                this.ddlReasonType.SelectedIndex = this.ddlReasonType.Items.IndexOf(this.ddlReasonType.Items.FindByValue(this.tempDataTable.Rows[0]["ReasonType"].ToString()));
                this.ddlMakeupType.SelectedIndex = this.ddlMakeupType.Items.IndexOf(this.ddlMakeupType.Items.FindByValue(this.tempDataTable.Rows[0]["MakeupType"].ToString()));
                this.HiddenSalaryFlag.Value = Convert.ToString(this.tempDataTable.Rows[0]["SalaryFlag"]);
                this.textBoxReasonRemark.Text = this.tempDataTable.Rows[0]["ReasonRemark"].ToString();
                this.HiddenState.Value = this.tempDataTable.Rows[0]["Status"].ToString();
                string Status = this.tempDataTable.Rows[0]["Status"].ToString();
                if ((Status != null) && ((Status == "1") || (Status == "2")))
                {
                    this.ButtonSave.Enabled = false;
                }
            }
            else
            {
                this.textBoxLocalName.Text = "";
                this.textBoxDepName.Text = "";
                this.textBoxKQDate.Text = "";
                this.textBoxCardTime.Text = "";
                this.textBoxReasonRemark.Text = "";
                this.ddlMakeupType.ClearSelection();
                this.ddlReasonType.ClearSelection();
            }
            //WriteMessage(0,Message.common_message_trans_complete);
        }
        protected void WriteMessage(int messageType, string message)
        {
            switch (messageType)
            {
                case 0:
                  //  this.Page.RegisterClientScriptBlock("jump", "<script language='JavaScript'>window.status='" + message.Replace("\"", "'").Replace("\r", "").Replace("\n", "") + "';</script>");
                     ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "show", "alert(\"" + message.Replace("\"", "'").Replace("\r", "").Replace("\n", "") + "\")", true);
                   
                        
                    break;

                case 1:
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "show", "alert(\"" + message.Replace("\"", "'").Replace("\r", "").Replace("\n", "") + "\")", true);
                  //  this.Page.RegisterClientScriptBlock("jump", "<script language='JavaScript'>alert(\"" + message.Replace("\"", "'").Replace("\r", "").Replace("\n", "") + "\");</script>");
                    break;

                case 2:
                  //  this.Page.RegisterClientScriptBlock("jump", "<script language='JavaScript'>alert(\"" + message.Replace("\"", "'").Replace("\r", "").Replace("\n", "") + "\");</script>");
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "show", "alert(\"" + message.Replace("\"", "'").Replace("\r", "").Replace("\n", "") + "\")", true);
                    break;
            }
        }

        protected void WriteMessage(Control UpdatePanel, string message)
        {
            ScriptManager.RegisterStartupScript(UpdatePanel, base.GetType(), "updateScript", "alert(\"" + message.Replace("\"", "'").Replace("\n", "") + "\");", true);
        }

        protected bool CheckData(string txtValue, string ReValue)
        {
            if (string.IsNullOrEmpty(txtValue))
            {

                this.WriteMessage(1, this.GetResouseValue(ReValue) + Message.Required);
                return false;
            }
            return true;
        }

        public int CheckDate(string date, string compareDate)
        {
            date = Convert.ToDateTime(date).ToString((string)this.Session["dateFormat"]);
            compareDate = Convert.ToDateTime(compareDate).ToString((string)this.Session["dateFormat"]);
            return date.CompareTo(compareDate);
        }

        public string GetsysKqoQinDays()
        {
            string sysKqoQinDays = "";
            string condition = "";
            try
            {
                sysKqoQinDays = cardMakeupBll.GetValue("select nvl(MAX(paravalue),'3') from gds_sc_parameter where paraname='KaoQinDataFLAG'");
                int i = 0;
                int WorkDays = 0;
                string UserOTType = "";
                while (i < Convert.ToDouble(sysKqoQinDays))
                {
                    condition = "SELECT workflag FROM gds_att_bgcalendar WHERE workday = to_date('" + DateTime.Now.AddDays((double)((-1 - i) - WorkDays)).ToString("yyyy/MM/dd") + "','yyyy/MM/dd') AND bgcode IN (SELECT depcode FROM gds_sc_department WHERE levelcode = '0')";
                    UserOTType = cardMakeupBll.GetValue(condition);
                    if (UserOTType.Length == 0)
                    {
                        break;
                    }
                    if (UserOTType.Equals("Y"))
                    {
                        i++;
                    }
                    else
                    {
                        WorkDays++;
                    }
                }
                sysKqoQinDays = Convert.ToString((int)(i + WorkDays));
            }
            catch (Exception)
            {
                sysKqoQinDays = "10";
            }
            // if (this.Session["roleCode"].ToString().Equals("Admin"))
            // {
            sysKqoQinDays = Convert.ToString((int)(Convert.ToInt32(sysKqoQinDays) + 30));
            //  }
            return sysKqoQinDays;
        }
        
        public string GetSALARYFLAG(string ReasonNo)
        {
            return cardMakeupBll.GetValue("SELECT SALARYFLAG FROM gds_att_EXCEPTREASON where ReasonNo='" + ReasonNo + "'");
        }

    
        private void Internationalization()
        { 
             this.ButtonSave.Text = ControlText.Btn_save;
             this.ButtonReturn.Text = ControlText.btnReturn;
             this.labelRemark.Text = ControlText.labelRemark;
        } 
        #endregion

        protected void ddlReasonType_SelectedIndexChanged(object sender, EventArgs e)
        {
            HiddenSalaryFlag.Value = GetSALARYFLAG(ddlReasonType.SelectedValue);
            
        }

        protected void textBoxLocalName_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
