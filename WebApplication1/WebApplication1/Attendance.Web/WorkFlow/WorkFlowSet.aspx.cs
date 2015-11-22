using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using GDSBG.MiABU.Attendance.BLL.WorkFlow;
using System.Collections;
using Infragistics.WebUI.UltraWebNavigator;
using System.Drawing;
using GDSBG.MiABU.Attendance.BLL.HRM.EmployeeData;
using System.Globalization;
using System.Resources;
using Infragistics.WebUI.WebSchedule;
using Resources;
using GDSBG.MiABU.Attendance.Model.WorkFlow;
using GDSBG.MiABU.Attendance.BLL.SystemManage.SystemSafety;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;
using GDSBG.MiABU.Attendance.BLL.KQM.BasicData;
using System.Web.Script.Serialization;

namespace GDSBG.MiABU.Attendance.Web.WorkFlow
{
    public partial class WorkFlowSet :BasePage
    {

        DataTable tempDataTable = new DataTable();
        WorkFlowSetBll workflowset = new WorkFlowSetBll();

        Dictionary<string, string> ClientMessage = null;
        JavaScriptSerializer JsSerializer = new JavaScriptSerializer();

        #region 事件
        protected void Page_Load(object sender, EventArgs e)
        {
            if (ClientMessage == null)
            {
                ClientMessage = new Dictionary<string, string>();
                ClientMessage.Add("check_limit_add", Message.check_limit_add);

                ClientMessage.Add("check_delete_data", Message.check_delete_data);
                ClientMessage.Add("clear", Message.clear);
                ClientMessage.Add("data", Message.data);
                ClientMessage.Add("notdatacopy", Message.notdatacopy);
                ClientMessage.Add("surecancopyflow", Message.surecancopyflow);
                ClientMessage.Add("chosedept", Message.chosedept);
                ClientMessage.Add("surehaddeletedeptallsigndata", Message.surehaddeletedeptallsigndata);
                ClientMessage.Add("oldempisnotnull", Message.oldempisnotnull);
                ClientMessage.Add("newempisnotnull", Message.newempisnotnull);
                ClientMessage.Add("oldempandnewempnotsame", Message.oldempandnewempnotsame);
                ClientMessage.Add("surereplacethisempno", Message.surereplacethisempno);
                ClientMessage.Add("common_thursday", Message.common_thursday);
                ClientMessage.Add("common_friday", Message.common_friday);
                ClientMessage.Add("common_saturday", Message.common_saturday);

                string clientmsg = JsSerializer.Serialize(ClientMessage);
                Page.ClientScript.RegisterStartupScript(GetType(), "ClientMessage", "var Message=" + clientmsg + ";", true);
            }
            if (!IsPostBack)
            {
                UltraWebTreeDeptDataBind();
                ddclDataBind_2("DocNoType", ddlDocnoType);
                hidemodelcode.Value = Request.QueryString["modulecode"].ToString();
                SetSelector(imgDepCode, txtOrgCode, tb_unit);
                tb_unit.Text = CurrentUserInfo.DepName;
            }
        }

      
        protected void UltraWebTreeDept_NodeClicked(object sender, Infragistics.WebUI.UltraWebNavigator.WebTreeNodeEventArgs e)
        {
            string deptid = UltraWebTreeDept.SelectedNode.Tag.ToString();
            hf_deptid.Value = deptid;
            ddlDataBind_2("DocNoType", ddl_doctype_1);
            ddlDataBind_2("DocNoType", ddl_doctype1);         
            bindgridview1();
            tr_overtimetype.Visible = false;
            tr_leave1.Visible = false;
            tr_leave2.Visible = false;
            tr_chucai.Visible = false;
            tr_overtimetype1.Visible = false;
            tr_leavedaystype11.Visible = false;
            tr_leavedaystype12.Visible = false;
            tr_chucai1.Visible = false;
            
        }
        protected void ddlAuditType_OnInitializeDataSource(object sender, EventArgs e)
        {
            this.tempDataTable.Clear();
            this.tempDataTable = workflowset.GetDocNoTypeList("SIGNTYPE");
            ddlAuditType.DataSource = tempDataTable;
            ddlAuditType.DataTextField = "DataValue";
            ddlAuditType.DataValueField = "DataCode";
            ddlAuditType.DataBind();
        }

      

        protected void ddlAuditManType_OnInitializeDataSource(object sender, EventArgs e)
        {
            this.tempDataTable.Clear();
            this.tempDataTable = workflowset.GetDocNoTypeList("SIGNLEVEL");
            this.ddlAuditManType.DataSource = tempDataTable;
            this.ddlAuditManType.DataTextField = "DataValue";
            this.ddlAuditManType.DataValueField = "DataCode";
            this.ddlAuditManType.DataBind();
        }


    

        //protected void ddl_overtimetype_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    string deptid = hf_deptid.Value;
        //    string doctype = ddl_doctype_1.SelectedValue;
        //    string overtimetype = ddl_overtimetype.SelectedValue;
        //    if (deptid != string.Empty && doctype != string.Empty && overtimetype != string.Empty)
        //    {
        //        List<string> list=new List<string>();
        //        list.Add(overtimetype);
        //        WorkFlowSetBll bll = new WorkFlowSetBll();
        //        DataTable dt= bll.GetSignPath(deptid, doctype, list);
        //        UltraWebGridBill.DataSource = dt;
        //        UltraWebGridBill.DataBind();
        //    }
        //}

        //上面DDL事件的改變事件
        protected void ddl_doctype_1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (ddl_doctype_1.SelectedValue)
            {
                case "D001":
                case "OTMProjectApply":
                    tr_overtimetype.Visible = true;
                    tr_leave1.Visible = false;
                    tr_leave2.Visible = false;
                    tr_chucai.Visible = false;
                    DdlClear();
                    ddlDataBind_1("OVERTIMETYPE", ddl_overtimetype);                   
                    break;
                case "D002":
                    tr_overtimetype.Visible = false;
                    tr_leave1.Visible = true;
                    tr_leave2.Visible = true;
                    tr_chucai.Visible = false;
                    DdlClear();
                    ddlDataBind_new("LeaveDayType", ddl_leavedays);
                    ddlDataBind_new("ShiweiType", ddl_shiwei);
                    ddlDataBind_new("GlzType", ddl_manager);                 
                    ddlDataBind(ddl_leavetype);
                    break;
                case "D003":
                    tr_overtimetype.Visible = false;
                    tr_leave1.Visible = false;
                    tr_leave2.Visible = false;
                    tr_chucai.Visible = true;
                    DdlClear();
                    ddlDataBind("CHUCAI", ddl_chucai);
                    ddlDataBind_new("OutType", ddl_chucaidays);
                    break;
                default:
                    tr_overtimetype.Visible = false;
                    tr_leave1.Visible = false;
                    tr_leave2.Visible = false;
                    tr_chucai.Visible = false;
                    break;
            }
        }

     
        //下面DDCL控件的改變事件
        protected void ddl_doctype1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (ddl_doctype1.SelectedValue)
            {
                case "D001":
                case "OTMProjectApply":
                    tr_overtimetype1.Visible = true;
                    tr_leavedaystype11.Visible = false;
                    tr_leavedaystype12.Visible = false;
                    tr_chucai1.Visible = false;
                    DdclClear();
                    ddclDataBind_1("OVERTIMETYPE", ddlCopyBillType);
                    break;
                case "D002":
                    tr_overtimetype1.Visible = false;
                    tr_leavedaystype11.Visible = true;
                    tr_leavedaystype12.Visible = true;
                    tr_chucai1.Visible = false;
                    DdclClear();
                    
                    ddclDataBind_new("LeaveDayType", ddcl_leavedaystype1);
                    ddclDataBind_new("ShiweiType", ddcl_shiwei1);
                    ddclDataBind_new("GlzType", ddcl_manager);        
                    ddclDataBind_new(ddcl_leavetype);
                    break;
                case "D003":
                    tr_overtimetype1.Visible = false;
                    tr_leavedaystype11.Visible = false;
                    tr_leavedaystype12.Visible = false;
                    tr_chucai1.Visible = true;
                    DdclClear();
                    ddclDataBind("CHUCAI", ddcl_chucai1);
                    ddclDataBind_new("OutType", ddcl_chucaidays1);
                    break;
                default:
                    tr_overtimetype1.Visible = false;
                    tr_leavedaystype11.Visible = false;
                    tr_leavedaystype12.Visible = false;
                    tr_chucai1.Visible = false;
                    break;
            }
        }

    

        protected void Btn_delete_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < this.UltraWebGridBill.Rows.Count; i++)
                {
                    if (this.UltraWebGridBill.Rows[i].Selected)
                    {
                        this.UltraWebGridBill.Rows[i].Delete();
                        break;
                    }
                }
                Page.ClientScript.RegisterStartupScript(this.GetType(), "show", "alert('" + Message.DeleteSuccess + "')", true);
                //WriteMessage(0, base.GetResouseValue("common.message.trans.complete"));
            }
            catch (Exception ex)
            {
                WriteMessage(1, (ex.InnerException == null) ? ex.Message : ex.InnerException.Message);
            }
        }

      

        

        protected void Btn_save_Click(object sender, EventArgs e)
        {
            string deptid = hf_deptid.Value;
            string formtype = ddl_doctype_1.SelectedValue;
            List<string> list = new List<string>();
            if (tr_overtimetype.Visible)
            {
                list.Add(ddl_overtimetype.SelectedValue);
            }
            else if (tr_leave1.Visible)
            {
                list.Add(ddl_leavedays.SelectedValue);
                list.Add(ddl_shiwei.SelectedValue);
                list.Add(ddl_manager.SelectedValue);
                list.Add(ddl_leavetype.SelectedValue);
            }
            else if(tr_chucai.Visible)
            {
                list.Add(ddl_chucai.SelectedValue);
                list.Add(ddl_chucaidays.SelectedValue);
            }
            Dictionary<int, List<string>> dciy = new Dictionary<int, List<string>>();
            List<string> exit= new List<string>();
            if (UltraWebGridBill.Rows.Count > 0)
            {
                int i = 0;
                foreach (Infragistics.WebUI.UltraWebGrid.UltraGridRow dr in UltraWebGridBill.Rows)
                {
                    List<string> listdata = new List<string>();
                    string empno = dr.Cells[0].Text == null ? string.Empty : dr.Cells[0].Text;
                    exit.Add(empno);
                    listdata.Add(empno);
                    string empname = dr.Cells[1].Text == null ? string.Empty : dr.Cells[1].Text;
                    listdata.Add(empname);
                    string notes = dr.Cells[2].Text == null ? string.Empty : dr.Cells[2].Text;
                    listdata.Add(notes);
                    string manager = dr.Cells[3].Text == null ? string.Empty : dr.Cells[3].Text;
                    listdata.Add(manager);
                    string signlevel = dr.Cells[4].Text == null ? string.Empty : dr.Cells[4].Text;
                    listdata.Add(signlevel);
                    string signtype = dr.Cells[5].Text == null ? string.Empty : dr.Cells[5].Text;
                    listdata.Add(signtype);
                    dciy.Add(i, listdata);
                    i++;
                }
            }
           
            WorkFlowLimitBll worklimit = new WorkFlowLimitBll();
            DataTable dt = worklimit.GetSignLimitInfo(deptid, formtype, list);
            List<string> sup = new List<string>();
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int l = 0; l < dt.Rows.Count; l++)
                {
                    string temp = dt.Rows[l]["flow_empno"].ToString();
                    if (!exit.Contains(temp))
                    {
                        sup.Add(temp);
                    }
                }
            }
            if (sup.Count > 0)
            {
                string temp1 = string.Empty;
                foreach (string item in sup)
                {
                    temp1 += item + ",";
                }
                if (temp1 != string.Empty)
                {
                    temp1 = temp1.Substring(0, temp1.Length - 1);
                }
                Page.ClientScript.RegisterStartupScript(GetType(), "show", "alert('" + Message.thisdeptcanhavesup + "：" + temp1 + " " + Message.signdoc + "')", true);
            }
            else
            {
                if (workflowset.SaveData(deptid, formtype, list, dciy))
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "show", "alert('" + Message.SaveSuccess + "')", true);
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "show", "alert('" + Message.SaveFailed + "')", true);
                }
            }
        }
        //導出
        protected void btn_Exp_Click(object sender, EventArgs e)
        {
            if (hf_deptid.Value != string.Empty)
            {
                string deptid = hf_deptid.Value;
                List<WorkFlowSetModel> list = new List<WorkFlowSetModel>();

                list = workflowset.GetExpAllSignPath(deptid);
                if (!(list != null && list.Count > 0))
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('" + Message.NotDataExp + "');</script>");
                    return;
                }
                string[] header = { ControlText.orgcode, ControlText.billtypename, ControlText.Reason1, ControlText.Reason2, ControlText.Reason3, ControlText.Reason4, ControlText.auditman1, ControlText.localname1, ControlText.audittypename1, ControlText.auditmantype1, ControlText.auditman2, ControlText.localname2, ControlText.audittypename2, ControlText.auditmantype2, 
                                  ControlText.auditman3, ControlText.localname3, ControlText.audittypename3, ControlText.auditmantype3,ControlText.auditman4, ControlText.localname4, ControlText.audittypename4, ControlText.auditmantype4,ControlText.auditman5, ControlText.localname5, ControlText.audittypename5, ControlText.auditmantype5,
                                  ControlText.auditman6, ControlText.localname6, ControlText.audittypename6, ControlText.auditmantype6,ControlText.auditman7, ControlText.localname7, ControlText.audittypename7, ControlText.auditmantype7,ControlText.auditman8, ControlText.localname8, ControlText.audittypename8, ControlText.auditmantype8,
                                  ControlText.auditman9, ControlText.localname9, ControlText.audittypename9, ControlText.auditmantype9,ControlText.auditman10, ControlText.localname10, ControlText.audittypename10, ControlText.auditmantype10,ControlText.auditman11, ControlText.localname11, ControlText.audittypename11, ControlText.auditmantype11,
                                  ControlText.auditman12, ControlText.localname12, ControlText.audittypename12, ControlText.auditmantype12,ControlText.auditman13, ControlText.localname13, ControlText.audittypename13, ControlText.auditmantype13,ControlText.auditman14, ControlText.localname14, ControlText.audittypename14, ControlText.auditmantype14,
                                  ControlText.auditman15, ControlText.localname15, ControlText.audittypename15, ControlText.auditmantype15,ControlText.auditman16, ControlText.localname16, ControlText.audittypename16, ControlText.auditmantype16,ControlText.auditman17, ControlText.localname17, ControlText.audittypename17, ControlText.auditmantype17,
                                  ControlText.auditman18, ControlText.localname18, ControlText.audittypename18, ControlText.auditmantype18,ControlText.auditman19, ControlText.localname19, ControlText.audittypename19, ControlText.auditmantype19,ControlText.auditman20, ControlText.localname20, ControlText.audittypename20, ControlText.auditmantype20};
                string[] properties = { "Orgcode", "Billtypename", "Reason1", "Reason2", "Reason3", "Reason4", "Auditman1", "Localname1", "Audittypename1", "Auditmantype1", "Auditman2", "Localname2", "Audittypename2", "Auditmantype2", "Auditman3", "Localname3", "Audittypename3", "Auditmantype3",
                                      "Auditman4", "Localname4", "Audittypename4", "Auditmantype4","Auditman5", "Localname5", "Audittypename5", "Auditmantype5","Auditman6", "Localname6", "Audittypename6", "Auditmantype6","Auditman7", "Localname7", "Audittypename7", "Auditmantype7",
                                      "Auditman8", "Localname8", "Audittypename8", "Auditmantype8","Auditman9", "Localname9", "Audittypename9", "Auditmantype9","Auditman10", "Localname10", "Audittypename10", "Auditmantype10","Auditman11", "Localname11", "Audittypename11", "Auditmantype11",
                                      "Auditman12", "Localname12", "Audittypename12", "Auditmantype12","Auditman13", "Localname13", "Audittypename13", "Auditmantype13","Auditman14", "Localname14", "Audittypename14", "Auditmantype14","Auditman15", "Localname15", "Audittypename15", "Auditmantype15",
                                      "Auditman16", "Localname16", "Audittypename16", "Auditmantype16","Auditman17", "Localname17", "Audittypename17", "Auditmantype17","Auditman18", "Localname18", "Audittypename18", "Auditmantype18","Auditman19", "Localname19", "Audittypename19", "Auditmantype19",
                                      "Auditman20", "Localname20", "Audittypename20", "Auditmantype20"};
                string filePath = MapPath("~/ExportFileTemp/") + DateTime.Now.Ticks + ".xls";
                NPOIHelper.ExportExcel(list, header, properties, 5000, filePath);
                PageHelper.ReturnHTTPStream(filePath, true);
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('" + Message.selecedDept + "');</script>");
            }
        }

        //復制流程
        protected void btn_copy_Click(object sender, EventArgs e)
        {
            try
            {
                string deptid = hf_deptid.Value;
                Dictionary<int, List<string>> dciy = new Dictionary<int, List<string>>();
                Dictionary<int, List<string>> dciy1 = new Dictionary<int, List<string>>();
                if (UltraWebGridBill.Rows.Count > 0)
                {
                    int i = 0;
                    foreach (Infragistics.WebUI.UltraWebGrid.UltraGridRow dr in UltraWebGridBill.Rows)
                    {
                        List<string> listdata = new List<string>();
                        string empno = dr.Cells[0].Text == null ? string.Empty : dr.Cells[0].Text;
                        listdata.Add(empno);
                        string empname = dr.Cells[1].Text == null ? string.Empty : dr.Cells[1].Text;
                        listdata.Add(empname);
                        string notes = dr.Cells[2].Text == null ? string.Empty : dr.Cells[2].Text;
                        listdata.Add(notes);
                        string manager = dr.Cells[3].Text == null ? string.Empty : dr.Cells[3].Text;
                        listdata.Add(manager);
                        string signlevel = dr.Cells[4].Text == null ? string.Empty : dr.Cells[4].Text;
                        listdata.Add(signlevel);
                        string signtype = dr.Cells[5].Text == null ? string.Empty : dr.Cells[5].Text;
                        listdata.Add(signtype);
                        dciy.Add(i, listdata);
                        i++;
                    }
                }
                string formtype = ddl_doctype1.SelectedValue;       
                if (tr_overtimetype1.Visible)
                {
                    int k = 0;
                    string[] info = this.ddlCopyBillType.SelectedValuesToString(",").Split(new char[] { ',' });
                    foreach (string item in info)
                    {
                        List<string> list = new List<string>();
                        list.Add(item);
                        dciy1.Add(k, list);
                        k++;
                    }


                }
                else if (tr_leavedaystype11.Visible && tr_leavedaystype12.Visible)
                {
                    int k = 0;
                    string[] info = this.ddcl_leavedaystype1.SelectedValuesToString(",").Split(new char[] { ',' });
                    string[] info1 = this.ddcl_shiwei1.SelectedValuesToString(",").Split(new char[] { ',' });
                    string[] info2 = this.ddcl_manager.SelectedValuesToString(",").Split(new char[] { ',' });
                    string[] info3 = this.ddcl_leavetype.SelectedValuesToString(",").Split(new char[] { ',' });
                    foreach (string item in info)
                    {
                        foreach (string item1 in info1)
                        {
                            foreach (string item2 in info2)
                            {
                                foreach (string item3 in info3)
                                {
                                    List<string> list = new List<string>();
                                    list.Add(item);
                                    list.Add(item1);
                                    list.Add(item2);
                                    list.Add(item3);
                                    dciy1.Add(k, list);
                                    k++;
                                }
                            }
                        }
                    }
                }
                else if (tr_chucai1.Visible)
                {
                    int k = 0;
                    string[] info = this.ddcl_chucai1.SelectedValuesToString(",").Split(new char[] { ',' });
                    string[] info1 = this.ddcl_chucaidays1.SelectedValuesToString(",").Split(new char[] { ',' });
                    foreach (string item in info)
                    {
                        foreach (string item1 in info1)
                        {
                            List<string> list = new List<string>();
                            list.Add(item);
                            list.Add(item1);
                            dciy1.Add(k, list);
                            k++;
                        }
                    }


                }
                int sessec = 0;
                int error = 0;
                foreach (int i in dciy1.Keys)
                {
                    if (workflowset.SaveData(deptid, formtype, dciy1[i], dciy))
                    {
                        sessec++;
                    }
                    else
                    {
                        error++;
                    }
                }
                string copyok = string.Format(Message.copyok, sessec, error);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('" + copyok + "');</script>");
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('"+ex.Message+"');</script>");
            }


        }
        //全部清除
        protected void btn_allclear_Click(object sender, EventArgs e)
        {
            string deptid = hf_deptid.Value;
            if (deptid != string.Empty)
            {
                if (workflowset.DeleteAllWorkFlowData(deptid))
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('" + Message.DeleteSuccess + "');</script>");
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('" + Message.DeleteFailed + "');</script>");
                }
            }

        }
        //導出
        protected void btn_ExpA_Click(object sender, EventArgs e)
        {
            if (hf_deptid.Value != string.Empty)
            {
                string deptid = hf_deptid.Value;
                List<WorkFlowSignModel> list = new List<WorkFlowSignModel>();
                list = workflowset.GetSignExpData(deptid);
                if (!(list != null && list.Count > 0))
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('" + Message.NotDataExp + "');</script>");
                    return;
                }
                string[] header = { ControlText.orgcode, ControlText.billtypename, ControlText.Reason1, ControlText.Reason2, 
                                      ControlText.Reason3, ControlText.Reason4, ControlText.gvHeadOrderID, ControlText.lblToPersoncode,
                                      ControlText.lblToPersonName, ControlText.Notes, ControlText.Manger, ControlText.gvHeadtype, 
                                      ControlText.gvHeadJuese
                               };
                string[] properties = { "Deptcode", "Formtype", "Reason1", "Reason2", "Reason3", "Reason4",
                                          "Orderid", "Flow_empno", "Flow_empname", "Flow_notes", "Flow_manager", "Flow_type", "Flow_level"};
                string filePath = MapPath("~/ExportFileTemp/") + DateTime.Now.Ticks + ".xls";
                NPOIHelper.ExportExcel(list, header, properties, 5000, filePath);
                PageHelper.ReturnHTTPStream(filePath, true);
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('" + Message.selecedDept + "');</script>");
            }
        }
        //查詢
        protected void Btn_All_Click(object sender, EventArgs e)
        {
            bindgridview1();
        }

        //替換工號
        protected void btn_replace_Click(object sender, EventArgs e)
        {
            string[] info = ddlDocnoType.SelectedValuesToString(",").Split(new char[] { ',' });
            string y = tb_y_empno.Text.Trim();
            string n = tb_n_newpno.Text.Trim();
            PersonBll person = new PersonBll();
            List<PersonModel> listperson = person.GetPersonUserId(y);
            List<PersonModel> listperson1 = person.GetPersonUserId(n);
            if (!(listperson != null && listperson.Count > 0))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('" + Message.YempnonotExit + "');</script>");
                return;
            }
            if (!(listperson1 != null && listperson1.Count > 0))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('" + Message.NempnonoExit + "');</script>");
                return;
            }

            if (workflowset.ReplaceAllEmpno(info, y, n, listperson1[0].Cname, listperson1[0].Mail, workflowset.GetManager(n)))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('" + Message.ReplaceOK + "');</script>");
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('" + Message.DeleteFailed + "');</script>");
            }


        }     
        
        protected void Btn_select_Click(object sender, EventArgs e)
        {
            string deptid = txtOrgCode.Text;
            string empno = tb_empno.Text;
            string empname = tb_name.Text;
            if (deptid == string.Empty)
            {
                deptid = CurrentUserInfo.DepCode;
            }
            List<string> list = new List<string>();
            list.Add(deptid);
            DataTable dt = workflowset.GetDeptPerson(list, empno, empname);
            UltraWebGridAudit.DataSource = dt;
            UltraWebGridAudit.DataBind();
        }
   
        //添加人員
        protected void Btn_Add_Click(object sender, EventArgs e)
        {
            try
            {
                int index = this.UltraWebGridBill.Rows.Count;
                for (int i = 0; i < this.UltraWebGridAudit.Rows.Count; i++)
                {
                    if (this.UltraWebGridAudit.Rows[i].Selected)
                    {                       
                        this.UltraWebGridBill.Rows.Add("FLOW_EMPNO");
                        this.UltraWebGridBill.Rows[index].Cells.FromKey("FLOW_EMPNO").Text = this.UltraWebGridAudit.Rows[i].Cells.FromKey("workno").Text;
                        this.UltraWebGridBill.Rows[index].Cells.FromKey("FLOW_EMPNAME").Text = this.UltraWebGridAudit.Rows[i].Cells.FromKey("localname").Text;
                        this.UltraWebGridBill.Rows[index].Cells.FromKey("FLOW_NOTES").Text = this.UltraWebGridAudit.Rows[i].Cells.FromKey("notes").Text;
                        this.UltraWebGridBill.Rows[index].Cells.FromKey("FLOW_MANAGER").Text = this.UltraWebGridAudit.Rows[i].Cells.FromKey("managername").Text;
                        this.UltraWebGridBill.Rows[index].Cells.FromKey("FLOW_LEVEL").Value = "";
                        this.UltraWebGridBill.Rows[index].Cells.FromKey("FLOW_TYPE").Value = "";                      
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('" + ex.Message + "');</script>");
            }
        }
        #endregion

        #region 放大鏡綁定

        /// <summary>
        /// 設置Selector
        /// </summary>
        /// <param name="ctrlTrigger">控件ID--按鈕</param>
        /// <param name="ctrlCode">控件ID--文本框1</param>
        /// <param name="ctrlName">控件ID--文本框2</param>
        public void SetSelector(WebControl ctrlTrigger, Control ctrlCode, Control ctrlName)
        {
            if (ctrlCode is TextBox) { (ctrlCode as TextBox).Attributes.Add("readonly", "readonly"); }
            if (ctrlName is TextBox) { (ctrlName as TextBox).Attributes.Add("readonly", "readonly"); }
            ctrlTrigger.Attributes.Add("onclick", string.Format("return setSelector('{0}','{1}','{2}')",
                ctrlCode.ClientID, ctrlName.ClientID, Request.QueryString["modulecode"]));
        }
        #endregion

        #region 數據綁定
      

        private void bindgridview1()
        {
            string deptid = hf_deptid.Value;
            string doctype = ddl_doctype_1.SelectedValue;
            DataTable dt = new DataTable();
            List<string> list = new List<string>();
            if (tr_overtimetype.Visible == true)
            {
                if (deptid != string.Empty && doctype != string.Empty && ddl_overtimetype.SelectedValue!=string.Empty)
                {
                    list.Add(ddl_overtimetype.SelectedValue);
                }
            }
            if (tr_chucai.Visible == true)
            {
                if (deptid != string.Empty && doctype != string.Empty && ddl_chucai.SelectedValue != string.Empty && ddl_chucaidays.SelectedValue != string.Empty)
                {

                    list.Add(ddl_chucai.SelectedValue);
                    list.Add(ddl_chucaidays.SelectedValue);

                }
            }
            if (tr_leave1.Visible == true && tr_leave2.Visible == true)
            {
                if (deptid != string.Empty && doctype != string.Empty && ddl_leavedays.SelectedValue != string.Empty && ddl_shiwei.SelectedValue != string.Empty && ddl_manager.SelectedValue != string.Empty && ddl_leavetype.SelectedValue != string.Empty)
                {
                    list.Add(ddl_leavedays.SelectedValue);
                    list.Add(ddl_shiwei.SelectedValue);
                    list.Add(ddl_manager.SelectedValue);
                    list.Add(ddl_leavetype.SelectedValue);
                }
            }
            WorkFlowSetBll bll = new WorkFlowSetBll();
            dt = bll.GetSignPath(deptid, doctype, list);  
            UltraWebGridBill.DataSource = dt;
            UltraWebGridBill.DataBind();
        }

        private void ddclDataBind(string type, UNLV.IAP.WebControls.DropDownCheckList ddcl)
        {
            this.tempDataTable.Clear();
            this.tempDataTable = workflowset.GetDocNoTypeList(type);
            ddcl.DataSource = tempDataTable;
            ddcl.DataTextField = "DataValue";
            ddcl.DataValueField = "DataCode";
            ddcl.DataBind();
        }

        private void ddclDataBind_1(string type, UNLV.IAP.WebControls.DropDownCheckList ddcl)
        {
            this.tempDataTable.Clear();
            this.tempDataTable = workflowset.GetOverTimeType(type);
            ddcl.DataSource = tempDataTable;
            ddcl.DataTextField = "V_NAME";
            ddcl.DataValueField = "V_CODE";
            ddcl.DataBind();
        }

        private void ddclDataBind_2(string type, UNLV.IAP.WebControls.DropDownCheckList ddcl)
        {
            this.tempDataTable.Clear();
            this.tempDataTable = workflowset.GetDocNoTypeList_new(type);
            ddcl.DataSource = tempDataTable;
            ddcl.DataTextField = "DataValue";
            ddcl.DataValueField = "DataCode";
            ddcl.DataBind();
           // ddcl.Items.Insert(0, new ListItem(Message.choose_defaultvalue, ""));
        }

        private void ddclDataBind_new(string type, UNLV.IAP.WebControls.DropDownCheckList ddcl)
        {
            string deptid = hf_deptid.Value;
            this.tempDataTable.Clear();
            this.tempDataTable = workflowset.GetKeyValue(deptid,type);
            ddcl.DataSource = tempDataTable;
            ddcl.DataTextField = "DAY_NAME";
            ddcl.DataValueField = "DAY_CODE";
            ddcl.DataBind();
        }

        private void ddclDataBind_new(UNLV.IAP.WebControls.DropDownCheckList ddcl)
        {
            this.tempDataTable.Clear();
            this.tempDataTable = workflowset.GetDocNoTypeList();
            ddcl.DataSource = tempDataTable;
            ddcl.DataTextField = "LVTYPENAME";
            ddcl.DataValueField = "LVTYPECODE";
            ddcl.DataBind();
            ddcl.Items.Insert(0, new ListItem(Message.choose_defaultvalue, ""));
        }

        private void ddlDataBind(string type, DropDownList ddl)
        {
            this.tempDataTable.Clear();
            this.tempDataTable = workflowset.GetDocNoTypeList(type);
            ddl.DataSource = tempDataTable;
            ddl.DataTextField = "DataValue";
            ddl.DataValueField = "DataCode";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem(Message.choose_defaultvalue, ""));
        }

        private void ddlDataBind(DropDownList ddl)
        {
            this.tempDataTable.Clear();
            this.tempDataTable = workflowset.GetDocNoTypeList();
            ddl.DataSource = tempDataTable;
            ddl.DataTextField = "LVTYPENAME";
            ddl.DataValueField = "LVTYPECODE";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem(Message.choose_defaultvalue, ""));
        }

        private void ddlDataBind_1(string type, DropDownList ddl)
        {
            this.tempDataTable.Clear();
            this.tempDataTable = workflowset.GetOverTimeType(type);
            ddl.DataSource = tempDataTable;
            ddl.DataTextField = "V_NAME";
            ddl.DataValueField = "V_CODE";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem(Message.choose_defaultvalue, ""));
        }

        private void ddlDataBind_2(string type, DropDownList ddl)
        {
            this.tempDataTable.Clear();
            this.tempDataTable = workflowset.GetDocNoTypeList_new(type);
            ddl.DataSource = tempDataTable;
            ddl.DataTextField = "DataValue";
            ddl.DataValueField = "DataCode";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem(Message.choose_defaultvalue, ""));
        }

        private void ddlDataBind_new(string type, DropDownList ddl)
        {
            string deptid = hf_deptid.Value;
            this.tempDataTable.Clear();
            this.tempDataTable = workflowset.GetKeyValue(deptid, type);
            ddl.DataSource = tempDataTable;
            ddl.DataTextField = "DAY_NAME";
            ddl.DataValueField = "DAY_CODE";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem(Message.choose_defaultvalue, ""));
        }
        #endregion

        #region 組織樹

        private void UltraWebTreeDeptDataBind()
        {
            HrmEmpOtherMoveBll hrmEmpOtherMoveBll = new HrmEmpOtherMoveBll();
            string strDepName;
            string moudelCode = Request.QueryString["modulecode"].ToString();
           // DataTable dt = hrmEmpOtherMoveBll.GetAuthorizedTreeDept(CurrentUserInfo.Personcode, CurrentUserInfo.CompanyId, moudelCode);
            RelationSelectorBll bll = new RelationSelectorBll();
            DataTable dt = bll.GetTypeDataList(CurrentUserInfo.Personcode, CurrentUserInfo.CompanyId, moudelCode, "N");
            this.UltraWebTreeDept.Nodes.Clear();
            SortedList allTreeNodes = new SortedList();
            foreach (DataRow row in dt.Rows)
            {
                strDepName = Convert.ToString(row["depname"]) + "[" + Convert.ToString(row["depcode"]) + "]";
                if (Convert.ToString(row["costcode"]).Trim().Length > 0)
                {
                    strDepName = strDepName + "-" + Convert.ToString(row["costcode"]);
                }
                Node node = base.CreateNode(Convert.ToString(row["depcode"]), strDepName, false, Convert.ToDecimal(dt.Compute("count(depcode)", "parentdepcode='" + row["depcode"] + "'")) == 0M);
                if (Convert.ToString(row["deleted"]).Equals("Y"))
                {
                    node.Style.BackColor = Color.Red;
                }
                allTreeNodes.Add(Convert.ToString(row["depcode"]), node);
                if (row["parentdepcode"].ToString().Trim().Length > 0)
                {
                    if (allTreeNodes.IndexOfKey(row["parentdepcode"]) >= 0)
                    {
                        ((Node)allTreeNodes.GetByIndex(allTreeNodes.IndexOfKey(row["parentdepcode"]))).Nodes.Add((Node)allTreeNodes.GetByIndex(allTreeNodes.IndexOfKey(row["depcode"])));
                    }
                    else
                    {
                        this.UltraWebTreeDept.Nodes.Add((Node)allTreeNodes.GetByIndex(allTreeNodes.IndexOfKey(Convert.ToString(row["depcode"]))));
                    }
                }
                else
                {
                    this.UltraWebTreeDept.Nodes.Add((Node)allTreeNodes.GetByIndex(allTreeNodes.IndexOfKey(Convert.ToString(row["depcode"]))));
                }
            }
        }
        #endregion

        #region 其他方法
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

       

        private void ddclDataClear(UNLV.IAP.WebControls.DropDownCheckList ddcl)
        {
            ddcl.ClearSelection();
        }

      
       

        private void DdlClear()
        {
            ddl_overtimetype.Items.Clear();
            ddl_leavedays.Items.Clear();
            ddl_shiwei.Items.Clear();
            ddl_manager.Items.Clear();
            ddl_leavetype.Items.Clear();
            ddl_chucai.Items.Clear();
            ddl_chucaidays.Items.Clear();
        }

        private void DdclClear()
        {
            ddlCopyBillType.Items.Clear();
            ddcl_leavedaystype1.Items.Clear();
            ddcl_shiwei1.Items.Clear();
            ddcl_manager.Items.Clear();
            ddcl_leavetype.Items.Clear();
            ddcl_chucai1.Items.Clear();
            ddcl_chucaidays1.Items.Clear();
        }
       
        #endregion

    }
}
