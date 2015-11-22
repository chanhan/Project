using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GDSBG.MiABU.Attendance.BLL.WorkFlow;
using System.Data;
using System.Collections;
using Resources;
using System.Text;
using Infragistics.WebUI.UltraWebGrid;
using NPOI.HSSF.Record.Formula.Functions;
using System.Drawing;
using GDSBG.MiABU.Attendance.Model.WorkFlow;
using System.Resources;
using Infragistics.WebUI.WebSchedule;
using System.Web.Script.Serialization;

namespace GDSBG.MiABU.Attendance.Web.WorkFlowForm
{
    public partial class OTMBatchOtApply : BasePage
    {
        OverTimeBll bll = new OverTimeBll();
        private DataTable tempDataTable;
        Dictionary<string, string> ClientMessage = null;
        JavaScriptSerializer JsSerializer = new JavaScriptSerializer();
        protected DataSet tempDataSet;
        private string ShiftNo;
        private string WorkNo;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (ClientMessage == null)
                {
                    ClientMessage = new Dictionary<string, string>();
                    ClientMessage.Add("WorkNoFirst", Message.WorkNoFirst);

                    ClientMessage.Add("common_message_data_no_select_employees", Message.common_message_data_no_select_employees);
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
                if (!base.IsPostBack)
                {
                   // this.Internationalization();
                    this.ddlDataBind();
                   // this.ImageDepCode.Attributes.Add("OnClick", "javascript:GetTreeDataValue('textBoxDepCode','Department','',\"WHERE companyid='" + this.Session["companyID"].ToString() + "' \",'" + base.sAppPath + "','" + base.Request["moduleCode"] + "','textBoxDepName')");
                    this.textBoxOTDate.Attributes.Add("onpropertychange", "javascript:WeekDate();");
                    this.textBoxOTDate.Attributes.Add("onafterpaste", "javascript:WeekDate();");
                    //this.textBoxOTDate.Attributes.Add("onfocus", "calendar('" + base.Language + "','" + base.dateFormat + "');");
                    //this.textBoxOTDate.Attributes.Add("formater", base.dateFormat);

                    
                    //this.textBoxBatchOtDate.Attributes.Add("onfocus", "calendar('" + base.Language + "','" + base.dateFormat + "');");
                    //this.textBoxBatchOtDate.Attributes.Add("formater", base.dateFormat);
                    //this.textBoxBatchOtDate.Text = DateTime.Now.ToString(base.dateFormat);
                    this.textBoxApplyDate.Text = DateTime.Today.ToShortDateString();
                    //this.textBoxApplyDate.Attributes.Add("formater", base.dateFormat);
                    this.ButtonExit.Attributes.Add("OnClick", "javascript:window.close();");
                    SetSelector(ImageDepCode, textBoxDepCode, textBoxDepName, "1", Request.QueryString["ModuleCode"].ToString());
                }
                SetCalendar(textBoxOTDate, textBoxBatchOtDate);
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('" + ex.ToString() + "');</script>");
                //base.WriteMessage(2, (ex.InnerException == null) ? ex.Message : ex.InnerException.Message);
            }
            //Utility.RegisterTypeForAjax(typeof(KQM_OTM_OTMBatchOtApply));
        }

        protected void ddlDataBind()
        {
            this.tempDataSet = new DataSet();
            this.tempDataSet = bll.GetDataByCondition("WHERE DataType='OverTimeType' ORDER BY OrderId");
            this.ddlPersonType.DataSource = this.tempDataSet.Tables[0].DefaultView;
            this.ddlPersonType.DataTextField = "DataValue";
            this.ddlPersonType.DataValueField = "DataCode";
            this.ddlPersonType.DataBind();
            this.ddlPersonType.Items.Insert(0, new ListItem("", ""));
        }

        /// <summary>
        /// 設置日曆控件到TextBox
        /// </summary>
        /// <param name="textBoxes"></param>
        protected void SetCalendar(params TextBox[] textBoxes)
        {
            if (textBoxes != null)
            {
                StringBuilder calScript = new StringBuilder("$(function(){$.datepicker.setDefaults($.datepicker.regional['")
                    .Append("zh-CN").Append("']);$(\"");
                //StringBuilder calScript = new StringBuilder("$(function(){$.datepicker.setDefaults($.datepicker.regional['")
                //    .Append(CurrentUserInfo.LanguageType).Append("']);$(\"");//2011.12.09 何西  
                foreach (TextBox textBox in textBoxes)
                {
                    calScript.AppendFormat("#{0},", textBox.ClientID);
                }
                calScript.Remove(calScript.Length - 1, 1).Append("\").datepicker();});");
                Page.ClientScript.RegisterStartupScript(this.GetType(), "calendar", calScript.ToString(), true);
            }
        }

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

        protected void ButtonQuery_Click(object sender, EventArgs e)
        {
        //  if (base.CheckData(this.textBoxDepName.Text, "common.organise") && base.CheckData(this.textBoxBatchOtDate.Text, "kqm.otm.date.from"))
        //{
            if (this.textBoxDepCode.Text == string.Empty)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('"+Message.DepCodeNotNull+"');</script>");
                return;
            }
            try
            {
                DateTime.Parse(this.textBoxBatchOtDate.Text.ToString()).ToString("yyyy/MM/dd");
            }
            catch (Exception)
            {
               // base.WriteMessage(1, base.GetResouseValue("common.message.data.errordate"));
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('" + Message.ErrDate + "')</script>");
                return;
            }
            string CS40002 = bll.GetValue("select levelcode from GDS_SC_DEPARTMENT where DepCode='" + this.textBoxDepCode.Text + "'");
            if ((CS40002 != null) && (((CS40002 == "0") || (CS40002 == "1")) || (CS40002 == "2")))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('" + Message.DepartmentNotExist + "')</script>");
               // base.WriteMessage(1, base.GetResouseValue("otm.message.batchotacheckdepcode"));
            }
            else
            {
                this.GetData();
                this.labMessage.Visible = false;
            }
        //}
        }

        private void GetData()
        {
            try
            {
                //string condition = "";
               
                //condition = condition + " AND exists (SELECT 1 FROM (" + base.SqlDep + ") e where e.DepCode=a.DCode)";
                
                //if (this.textBoxDepCode.Text.Trim().Length != 0)
                //{
                //    condition = condition + " AND a.dCode IN (SELECT DepCode FROM GDS_SC_DEPARTMENT e START WITH e.depcode = '" + this.textBoxDepCode.Text.Trim() + "' CONNECT BY PRIOR e.depcode = e.parentdepcode)";
                //}
                //if (this.ddlPersonType.SelectedValue.Length != 0)
                //{
                //    condition = condition + " AND OverTimeType = '" + this.ddlPersonType.SelectedValue + "'";
                //}
                //if (this.textBoxBatchOtDate.Text.Trim().Length > 0)
                //{
                //    condition = condition + " AND not exists(select 1 from GDS_ATT_ADVANCEAPPLY b where b.workno=a.workno and b.otdate=to_date('" + Convert.ToDateTime(this.textBoxBatchOtDate.Text).ToString("yyyy/MM/dd") + "','yyyy/MM/dd') )";
                //}
                //this.tempDataTable = ((ServiceLocator)this.Session["serviceLocator"]).GetOTMAdvanceApplyData().GetEmployees(condition, Convert.ToDateTime(this.textBoxBatchOtDate.Text).ToString("yyyy/MM/dd")).Tables["Employees"];
                this.tempDataTable = bll.GetShiftInfo(this.textBoxDepCode.Text.Trim(), this.textBoxBatchOtDate.Text.Trim(), string.Empty, this.ddlPersonType.SelectedValue, base.SqlDep);
                this.DataUIBind();
                this.GridSelEmployee.Rows.Clear(true);
                this.WebGroupBoxNo.Text = Message.common_org_besel + "(" + this.GridEmployee.Rows.Count.ToString() + ")";
                this.WebGroupBoxYes.Text = Message.common_org_sel + "(" + this.GridSelEmployee.Rows.Count.ToString() + ")";
                this.HiddenOTDate.Value = this.textBoxBatchOtDate.Text.Trim();
                
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('" + ex.ToString() + "');</script>");
            }
        }

        private void DataUIBind()
        {
            string OrgCode = "";
            string ShiftNo = "";
            string ShiftDesc = "";
            string strTemp = "";
            string[] temVal = null;
            SortedList SListOrgCodeShiftDesc = new SortedList();
            string ShiftDate = this.textBoxBatchOtDate.Text;
            try
            {
                ShiftDate = Convert.ToDateTime(ShiftDate).ToString("yyyy/MM/dd");
            }
            catch (Exception)
            {
                ShiftDate = DateTime.Now.ToString("yyyy/MM/dd");
            }
            foreach (DataRow dr in this.tempDataTable.Rows)
            {
                OrgCode = dr["dCode"].ToString();
                if (Convert.ToString(dr["ShiftDesc"]).Length == 0)
                {
                    if (SListOrgCodeShiftDesc.IndexOfKey(OrgCode) >= 0)
                    {
                        ShiftDesc = SListOrgCodeShiftDesc.GetByIndex(SListOrgCodeShiftDesc.IndexOfKey(OrgCode)).ToString();
                        if (ShiftDesc.Length > 0)
                        {
                            dr["ShiftDesc"] = ShiftDesc;
                            dr["ShiftFlag"] = "N";
                        }
                    }
                    else
                    {
                        strTemp = bll.GetEmpOrgShift(OrgCode, ShiftDate);
                        if (strTemp.Length > 0)
                        {
                            temVal = strTemp.Split(new char[] { '|' });
                            ShiftNo = temVal[0].ToString();
                            if (ShiftNo.Length > 0)
                            {
                                ShiftDesc = bll.GetValue("select ShiftDesc from (select ShiftNo||':'||ShiftDesc||'['||(select dataValue from GDS_ATT_TYPEDATA b where b.datatype='ShiftType' and b.datacode=a.shifttype)||']' ShiftDesc from GDS_ATT_WORKSHIFT a  where a.ShiftNo='" + ShiftNo + "')") + "(" + temVal[4].ToString() + ")";
                                dr["ShiftDesc"] = ShiftDesc;
                                dr["ShiftFlag"] = "N";
                                SListOrgCodeShiftDesc.Add(OrgCode, ShiftDesc);
                            }
                            else
                            {
                                SListOrgCodeShiftDesc.Add(OrgCode, "");
                            }
                        }
                        else
                        {
                            SListOrgCodeShiftDesc.Add(OrgCode, "");
                        }
                    }
                }
            }

            if (this.tempDataTable == null || this.tempDataTable.Rows.Count==0)
            {
                DataTable dt = new DataTable();                
                dt = new DataTable();
                dt.Columns.Add("WorkNo", Type.GetType("System.String"));
                dt.Columns.Add("LocalName", Type.GetType("System.String"));
                dt.Columns.Add("OverTimeType", Type.GetType("System.String"));
                dt.Columns.Add("ShiftDesc", Type.GetType("System.String"));                
                DataRow rw01 = dt.NewRow();
                rw01["WorkNo"] = "F3228771 ";
                rw01["LocalName"] = "李玉亭";
                rw01["OverTimeType"] = "A2";
                rw01["ShiftDesc"] = "Test";
                dt.Rows.Add(rw01);
              
                this.tempDataTable = dt;
               
            }

            this.tempDataTable.DefaultView.Sort = "ShiftDesc ASC";
            this.GridEmployee.DataSource = this.tempDataTable;
            this.GridEmployee.DataBind();
        }
        private void SelGridSelEmployee(string operate)
        {
            TemplatedColumn tcol;
            int loop;
            CellItem GridItem;
            CheckBox chkIsHaveRight;
            int iLoop;
            int index = 0;
            if (operate == "Right")
            {
                index = this.GridSelEmployee.Rows.Count;
                tcol = (TemplatedColumn)this.GridEmployee.Bands[0].Columns[3];
                for (loop = 0; loop < this.GridEmployee.Rows.Count; loop++)
                {
                    GridItem = (CellItem)tcol.CellItems[loop];
                    chkIsHaveRight = (CheckBox)GridItem.FindControl("CheckBoxCell");
                    if (chkIsHaveRight.Checked)
                    {
                        this.GridSelEmployee.Rows.Add("WorkNo");
                        this.GridSelEmployee.Rows[index].Cells[0].Text = this.GridEmployee.Rows[loop].Cells[0].Text;
                        this.GridSelEmployee.Rows[index].Cells[1].Text = this.GridEmployee.Rows[loop].Cells[1].Text;
                        this.GridSelEmployee.Rows[index].Cells[2].Text = this.GridEmployee.Rows[loop].Cells[2].Text;
                        this.GridSelEmployee.Rows[index].Cells[4].Text = this.GridEmployee.Rows[loop].Cells[4].Text;
                        index++;
                    }
                }
                for (iLoop = this.GridEmployee.Rows.Count - 1; iLoop >= 0; iLoop--)
                {
                    GridItem = (CellItem)tcol.CellItems[iLoop];
                    chkIsHaveRight = (CheckBox)GridItem.FindControl("CheckBoxCell");
                    if (chkIsHaveRight.Checked)
                    {
                        this.GridEmployee.Rows.RemoveAt(iLoop);
                    }
                }
            }
            if (operate == "Left")
            {
                index = this.GridEmployee.Rows.Count;
                tcol = (TemplatedColumn)this.GridSelEmployee.Bands[0].Columns[3];
                for (loop = 0; loop < this.GridSelEmployee.Rows.Count; loop++)
                {
                    GridItem = (CellItem)tcol.CellItems[loop];
                    chkIsHaveRight = (CheckBox)GridItem.FindControl("CheckBoxSelCell");
                    if (chkIsHaveRight.Checked)
                    {
                        this.GridEmployee.Rows.Add("WorkNo");
                        this.GridEmployee.Rows[index].Cells[0].Text = this.GridSelEmployee.Rows[loop].Cells[0].Text;
                        this.GridEmployee.Rows[index].Cells[1].Text = this.GridSelEmployee.Rows[loop].Cells[1].Text;
                        this.GridEmployee.Rows[index].Cells[2].Text = this.GridSelEmployee.Rows[loop].Cells[2].Text;
                        this.GridEmployee.Rows[index].Cells[4].Text = this.GridSelEmployee.Rows[loop].Cells[4].Text;
                        index++;
                    }
                }
                for (iLoop = this.GridSelEmployee.Rows.Count - 1; iLoop >= 0; iLoop--)
                {
                    GridItem = (CellItem)tcol.CellItems[iLoop];
                    chkIsHaveRight = (CheckBox)GridItem.FindControl("CheckBoxSelCell");
                    if (chkIsHaveRight.Checked)
                    {
                        this.GridSelEmployee.Rows.RemoveAt(iLoop);
                    }
                }
            }
            if (this.GridSelEmployee.Rows.Count > 0)
            {
                this.HiddenWorkNo.Value = this.GridSelEmployee.Rows[0].Cells[0].Text.ToString().Trim();
            }
            else
            {
                this.HiddenWorkNo.Value = "";
            }
            this.WebGroupBoxNo.Text = Message.common_org_besel + "(" + this.GridEmployee.Rows.Count.ToString() + ")";
            this.WebGroupBoxYes.Text = Message.common_org_sel + "(" + this.GridSelEmployee.Rows.Count.ToString() + ")";
        }

        protected void ButtonAdd_Click(object sender, EventArgs e)
        {
            this.SelGridSelEmployee("Right");
            this.HiddenOTDate.Value = this.textBoxBatchOtDate.Text.Trim();
            this.labMessage.Visible = false;
            this.ButtonSave.Enabled = true;
        }

        protected void ButtonDecrease_Click(object sender, EventArgs e)
        {
            this.SelGridSelEmployee("Left");
            this.labMessage.Visible = false;
        }

        protected bool CheckData(string txtValue, string ReValue)
        {
            if (string.IsNullOrEmpty(txtValue))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('" + ReValue + Message.Required + "')</script>");
                //this.WriteMessage(1, this.GetResouseValue(ReValue) + this.GetResouseValue("common.message.required"));
                return false;
            }
            return true;
        }

        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.GridSelEmployee.Rows.Count == 0)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('" + Message.common_message_data_no_select_employees + "')</script>");
                    //base.WriteMessage(1, base.GetResouseValue("common.message.data.no.select.employees"));
                }
                else if (((CheckData(this.textBoxOTDate.Text, Message.overtime_date) && CheckData(this.textBoxBeginTime.Text, Message.start_time)) && (CheckData(this.textBoxEndTime.Text, Message.end_time) && CheckData(this.textBoxHours.Text, Message.times))) && CheckData(this.textBoxWorkDesc.Text, Message.overtime_desc))
                {
                    int i;
                    this.tempDataTable = bll.GetDataByCondition_1("and 1=2").Tables[0];
                    int failCount = 0;
                    int successCount = 0;
                    string OTMSGFlag = "";
                    string tmpRemark = "";
                    string msg = "";
                    string condition = "";
                    string OTDate = "";
                    try
                    {
                        OTDate = DateTime.Parse(this.textBoxOTDate.Text.ToString()).ToString("yyyy/MM/dd");
                    }
                    catch (Exception)
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('"+Message.ErrDate+"')</script>");
                        //base.WriteMessage(1, base.GetResouseValue("common.message.data.errordate"));
                        return;
                    }
                    string tmpOTType = "";
                    string Hour = this.textBoxHours.Text.Trim();
                    string WorkDesc = this.textBoxWorkDesc.Text;
                    string StrBtime = this.textBoxBeginTime.Text.ToString();
                    string StrEtime = this.textBoxEndTime.Text.ToString();
                    DateTime dtTempBeginTime = DateTime.Parse(DateTime.Parse(OTDate).ToString("yyyy/MM/dd") + " " + StrBtime);
                    DateTime dtTempEndTime = DateTime.Parse(DateTime.Parse(OTDate).ToString("yyyy/MM/dd") + " " + StrEtime);
                    DateTime dtMidTime = DateTime.Parse(DateTime.Parse(OTDate).ToString("yyyy/MM/dd") + " 12:00");
                    //Function CommonFun = new Function();
                    SortedList list = new SortedList();
                    string OTMAdvanceBeforeDays = bll.GetValue("select nvl(max(paravalue),'2') from GDS_SC_PARAMETER where paraname='OTMAdvanceBeforeDays'");
                    if (!bll.GetValue("select nvl(max(workno),'Y') from GDS_ATT_EMPLOYEE where workno='" + CurrentUserInfo.Personcode + "'").Equals("Y"))
                    {
                        i = 0;
                        int WorkDays = 0;
                        string UserOTType = "";
                        while (i < Convert.ToDouble(OTMAdvanceBeforeDays))
                        {
                            UserOTType = this.FindOTType(DateTime.Now.AddDays((double)((-1 - i) - WorkDays)).ToString("yyyy/MM/dd"), CurrentUserInfo.Personcode);
                            if (UserOTType.Length == 0)
                            {
                                break;
                            }
                            if (UserOTType.Equals("G1"))
                            {
                                i++;
                            }
                            else
                            {
                                WorkDays++;
                            }
                        }
                        OTMAdvanceBeforeDays = Convert.ToString((int)(i + WorkDays));
                    }
                    condition = "select trunc(sysdate)-to_date('" + OTDate + "','yyyy/mm/dd') from dual";
                    if (Convert.ToDecimal(bll.GetValue(condition)) > Convert.ToDecimal(OTMAdvanceBeforeDays))
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('"+Message.checkapproveflag+"')</script>");
                        //base.WriteMessage(1, base.GetResouseValue("otm.message.checkadvancedaysbefore") + ":" + OTMAdvanceBeforeDays);
                    }
                    else
                    {
                        for (i = 0; i < this.GridSelEmployee.Rows.Count; i++)
                        {
                            tmpRemark = "";
                            OTMSGFlag = "";
                            this.WorkNo = this.GridSelEmployee.Rows[i].Cells.FromKey("WorkNo").Text.ToString().Trim();
                            this.ShiftNo = bll.GetShiftNo(this.WorkNo, OTDate);
                            dtTempBeginTime = DateTime.Parse(DateTime.Parse(OTDate).ToString("yyyy/MM/dd") + " " + StrBtime);
                            dtTempEndTime = DateTime.Parse(DateTime.Parse(OTDate).ToString("yyyy/MM/dd") + " " + StrEtime);
                            dtMidTime = DateTime.Parse(DateTime.Parse(OTDate).ToString("yyyy/MM/dd") + " 12:00");
                            if ((this.ShiftNo == null) || (this.ShiftNo == ""))
                            {
                                msg = msg + "<br>" + this.WorkNo + ": " + Message.otm_exception_errorshiftno_1;
                                failCount++;
                                this.GridSelEmployee.Rows[i].Style.BackColor = Color.Red;
                            }
                            else
                            {
                                tmpOTType = this.FindOTType(OTDate, this.WorkNo);
                                if (tmpOTType.Length == 0)
                                {
                                    msg = msg + "<br>" + this.WorkNo + ": " + Message.OTMType + Message.Required;
                                    failCount++;
                                    this.GridSelEmployee.Rows[i].Style.BackColor = Color.Red;
                                }
                                else
                                {
                                    Hour = this.GetOTHours(this.WorkNo, OTDate, StrBtime, StrEtime, tmpOTType);
                                    if (Convert.ToDouble(Hour) < 0.5)
                                    {
                                        msg = msg + "<br>" + this.WorkNo + ": " + Message.otm_othourerror;
                                        failCount++;
                                        this.GridSelEmployee.Rows[i].Style.BackColor = Color.Red;
                                    }
                                    else
                                    {
                                        list = bll.ReturnOTTTime(this.WorkNo, OTDate, dtTempBeginTime, dtTempEndTime, this.ShiftNo);
                                        dtTempBeginTime = Convert.ToDateTime(list.GetByIndex(list.IndexOfKey("A")));
                                        dtTempEndTime = Convert.ToDateTime(list.GetByIndex(list.IndexOfKey("B")));
                                        if (!CheckLeaveOverTime(this.WorkNo, dtTempBeginTime.ToString("yyyy/MM/dd"), dtTempBeginTime.ToString("HH:mm"), dtTempEndTime.ToString("yyyy/MM/dd"), dtTempEndTime.ToString("HH:mm")))
                                        {
                                            msg = msg + "<br>" + this.WorkNo + ": " + Message.common_message_Leaveovertime_repeart;
                                            failCount++;
                                            this.GridSelEmployee.Rows[i].Style.BackColor = Color.Red;
                                        }
                                        else if (!CheckWorkTime(this.WorkNo, OTDate, dtTempBeginTime.ToString("yyyy/MM/dd HH:mm"), dtTempEndTime.ToString("yyyy/MM/dd HH:mm"), this.ShiftNo))
                                        {
                                            msg = msg + "<br>" + this.WorkNo + ": " + Message.common_message_otm_worktime_repeart;
                                            failCount++;
                                            this.GridSelEmployee.Rows[i].Style.BackColor = Color.Red;
                                        }
                                        else if (!CheckOverTime(this.WorkNo, OTDate, dtTempBeginTime.ToString("yyyy/MM/dd HH:mm"), dtTempEndTime.ToString("yyyy/MM/dd HH:mm"), this.ShiftNo, true))
                                        {
                                            msg = msg + "<br>" + this.WorkNo + ": " + Message.common_message_otm_multi_repeart;
                                            failCount++;
                                            this.GridSelEmployee.Rows[i].Style.BackColor = Color.Red;
                                        }
                                        else if (!CheckOTOverETM(this.WorkNo, dtTempBeginTime.ToString("yyyy/MM/dd HH:mm"), dtTempEndTime.ToString("yyyy/MM/dd HH:mm")))
                                        {
                                            msg = msg + "<br>" + this.WorkNo + ": " + Message.common_message_otm_etmrepeart;
                                            failCount++;
                                            this.GridSelEmployee.Rows[i].Style.BackColor = Color.Red;
                                        }
                                        else
                                        {
                                            OTMSGFlag = GetOTMSGFlag(this.WorkNo, OTDate, Convert.ToDouble(Hour), tmpOTType, "N", "");
                                            if (OTMSGFlag != "")
                                            {
                                                tmpRemark = OTMSGFlag.Substring(1, OTMSGFlag.Length - 1);
                                                OTMSGFlag = OTMSGFlag.Substring(0, 1);
                                            }
                                            if (OTMSGFlag.Equals("A"))
                                            {
                                                msg = msg + "<br>" + this.WorkNo + ": " + tmpRemark;
                                                failCount++;
                                                this.GridSelEmployee.Rows[i].Style.BackColor = Color.Red;
                                            }
                                            else
                                            {
                                                List<OverTimeModel> mod_list = new List<OverTimeModel>();
                                                OverTimeModel mod = new OverTimeModel();
                                                mod.Workno = this.WorkNo;
                                                mod.Applydate = Convert.ToDateTime(this.textBoxApplyDate.Text.Trim());
                                                mod.Otdate =  Convert.ToDateTime(OTDate);
                                                mod.Ottype = tmpOTType;
                                                mod.Begintime = dtTempBeginTime;
                                                mod.Endtime = dtTempEndTime;
                                                mod.Hours = Convert.ToInt32(Hour);
                                                mod.Workdesc = WorkDesc;
                                                mod.G2isforrest = "N";
                                                mod.Otshiftno = this.ShiftNo;
                                                mod.Isproject = "N";
                                                mod.Update_user = CurrentUserInfo.Personcode;
                                                mod.Update_date =  DateTime.Now;
                                                mod.Remark = tmpRemark;
                                                mod.Status = "0";
                                                mod.Otmsgflag = OTMSGFlag;
                                                mod.Update_user = CurrentUserInfo.Personcode;
                                                mod_list.Add(mod);
                                                bll.SaveData(mod_list, "Add", string.Empty);
                                                //DataRow row = this.tempDataTable.NewRow();

                                                //row.BeginEdit();
                                                //row["WORKNO"] = this.WorkNo;
                                                //row["OTDate"] = OTDate;
                                                //row["BeginTime"] = dtTempBeginTime;
                                                //row["EndTime"] = dtTempEndTime;
                                                //row["Hours"] = Hour;
                                                //row["OTType"] = tmpOTType;
                                                //row["WorkDesc"] = WorkDesc;
                                                //row["OTMSGFlag"] = OTMSGFlag;
                                                //row["Remark"] = tmpRemark;
                                                //row["ApplyDate"] = this.textBoxApplyDate.Text.Trim();
                                                //row["Modifier"] = this.Session["appUser"].ToString();
                                                //row["ModifyDate"] = DateTime.Now.ToLongDateString();
                                                //row["IsProject"] = "N";
                                                //row["OTShiftNo"] = this.ShiftNo;
                                                //row.EndEdit();
                                                //this.tempDataTable.Rows.Add(row);
                                                //this.tempDataTable.AcceptChanges();
                                              //  ((ServiceLocator)this.Session["serviceLocator"]).GetOTMAdvanceApplyData().SaveData("Add", this.tempDataTable);
                                               // this.tempDataTable.Clear();
                                                successCount++;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        if ((failCount > 0) || (successCount > 0))
                        {
                            this.labMessage.Text = string.Concat(new object[] { Message.common_success, successCount.ToString(), ";", Message.common_fault, ";", failCount, ";", msg });
                            this.labMessage.Visible = true;
                        }
                        this.ButtonSave.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('" + ex.Message + "')</script>");
               //base.WriteMessage(2, (ex.InnerException == null) ? ex.Message : ex.InnerException.Message);
            }
        }


        public string GetOTMSGFlag(string WorkNo, string OtDate, double OtHours, string OTType, string IsProject, string ModifyID)
        {
            ResourceManager resourceM = new ResourceManager(typeof(Resource));
            return bll.GetOTMSGFlag(WorkNo, OtDate, OtHours, OTType, IsProject, ModifyID, resourceM);
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
             DataTable dt= bll.GetDataByCondition_2(condition).Tables["OTM_RealApply"];
             if (dt.Rows.Count > 0)
             {
                 return false;
             }
            return true;
        }

        public string FindOTType(string OTDate, string WorkNo)
        {
            return bll.GetOTType(WorkNo, OTDate);
        }


        public string GetOTHours(string WorkNo, string OTDate, string BeginTime, string EndTime, string OTType)
        {
            double hours = 0.0;
            if (OTType.Length == 0)
            {
                OTType = this.FindOTType(OTDate, WorkNo);
            }
            if (BeginTime != EndTime)
            {
               hours = bll.GetOtHours(WorkNo.ToUpper(), OTDate, BeginTime, EndTime, OTType);
            }
            return hours.ToString();
        }



    }
}
