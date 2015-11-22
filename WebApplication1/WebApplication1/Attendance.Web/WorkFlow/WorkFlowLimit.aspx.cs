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
using System.Web.Script.Serialization;

namespace GDSBG.MiABU.Attendance.Web.WorkFlow
{ 
    public partial class WorkFlowLimit : BasePage
    {
        DataTable tempDataTable = new DataTable();
        WorkFlowSetBll workflowset = new WorkFlowSetBll();
        WorkFlowLimitBll worklimit = new WorkFlowLimitBll();
        JavaScriptSerializer JsSerializer = new JavaScriptSerializer();
        Dictionary<string, string> ClientMessage = null;

        static SynclogModel logmodel = new SynclogModel();

        #region 控件事件
        protected void Page_Load(object sender, EventArgs e)
        {
            if (ClientMessage == null)
            {
                ClientMessage = new Dictionary<string, string>();

                ClientMessage.Add("DepCodeNotNull", Message.DepCodeNotNull);
                ClientMessage.Add("check_delete_data", Message.check_delete_data);
                ClientMessage.Add("SaveConfirm", Message.SaveConfirm);
                ClientMessage.Add("check_doctype", Message.check_doctype);
                ClientMessage.Add("check_leavedays", Message.check_leavedays);

                ClientMessage.Add("check_shiwei", Message.check_shiwei);
                ClientMessage.Add("check_manager", Message.check_manager);
                ClientMessage.Add("check_leavetype", Message.check_leavetype);
                ClientMessage.Add("check_overtimetype", Message.check_overtimetype);
                ClientMessage.Add("check_chucai", Message.check_chucai);

                ClientMessage.Add("check_chucaidays", Message.check_chucaidays);
                ClientMessage.Add("check_limit_add", Message.check_limit_add);
                string clientmsg = JsSerializer.Serialize(ClientMessage);
                Page.ClientScript.RegisterStartupScript(GetType(), "ClientMessage", "var Message=" + clientmsg + ";", true);
            }
            if (!IsPostBack)
            {
                logmodel.ProcessOwner = CurrentUserInfo.Personcode;
                logmodel.TransactionType = (Request.QueryString["ModuleCode"] == null) ? "" : Request.QueryString["ModuleCode"].ToString();
                logmodel.LevelNo = "2";
                logmodel.FromHost = Request.UserHostAddress;

                hidemodelcode.Value = Request.QueryString["modulecode"].ToString();
                SetSelector(imgDepCode, txtOrgCode, tb_unit);
                SetSelector(imgDepCode_edit, txtOrgCode_edit, tb_unit_edit);
                tb_unit.Text = CurrentUserInfo.DepName;
                tb_unit_edit.Text = CurrentUserInfo.DepName;
                ddlDataBind_2("DocNoType", ddl_doctype_1);
            }
            PageHelper.ButtonControlsWF(FuncList, pnlShowPanel.Controls, base.FuncListModule);
            //PageHelper.ButtonControlsWF(FuncList, Panel1.Controls, base.FuncListModule);
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
            string deptid = txtOrgCode_edit.Text;
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
            else if (tr_chucai.Visible)
            {
                list.Add(ddl_chucai.SelectedValue);
                list.Add(ddl_chucaidays.SelectedValue);
            }
            Dictionary<int, List<string>> dciy = new Dictionary<int, List<string>>();

            if (UltraWebGridBill.Rows.Count > 0)
            {
                int i = 0;
                foreach (Infragistics.WebUI.UltraWebGrid.UltraGridRow dr in UltraWebGridBill.Rows)
                {
                    List<string> listdata = new List<string>();
                    string empno = dr.Cells.FromKey("FLOW_EMPNO").Text;
                    listdata.Add(empno);
                    string empname = dr.Cells.FromKey("FLOW_EMPNAME").Text;
                    listdata.Add(empname);
                    string notes = dr.Cells.FromKey("FLOW_NOTES").Text;
                    listdata.Add(notes);
                    string manager = dr.Cells.FromKey("FLOW_MANAGER").Text;
                    listdata.Add(manager);
                    dciy.Add(i, listdata);
                    i++;
                }
            }
            if (worklimit.SaveflowlimitInfo(deptid, formtype, list, dciy, logmodel))
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "show", "alert('" + Message.SaveSuccess + "')", true);
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "show", "alert('" + Message.SaveFailed + "')", true);
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

        protected void Btn_select_edit_Click(object sender, EventArgs e)
        {
            string deptid = txtOrgCode_edit.Text;
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
            else if (tr_chucai.Visible)
            {
                list.Add(ddl_chucai.SelectedValue);
                list.Add(ddl_chucaidays.SelectedValue);
            }
            DataTable dt = worklimit.GetSignLimitInfo(deptid, formtype, list);
            UltraWebGridBill.DataSource = dt;
            UltraWebGridBill.DataBind();
        }


        protected void Btn_Add_Click(object sender, EventArgs e)
        {
            try
            {
                int index = this.UltraWebGridBill.Rows.Count;
                for (int i = 0; i < this.UltraWebGridAudit.Rows.Count; i++)
                {
                    if (this.UltraWebGridAudit.Rows[i].Selected)
                    {
                        for (int t = 0; t < this.UltraWebGridBill.Rows.Count; t++)
                        {
                            if (this.UltraWebGridBill.Rows[t].Cells.FromKey("FLOW_EMPNO").Text == this.UltraWebGridAudit.Rows[i].Cells.FromKey("workno").Text)
                            {
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('" + Message.check_workno + "');</script>");
                                return;
                            }
                        }
                        this.UltraWebGridBill.Rows.Add("FLOW_EMPNO");
                        this.UltraWebGridBill.Rows[index].Cells.FromKey("FLOW_EMPNO").Text = this.UltraWebGridAudit.Rows[i].Cells.FromKey("workno").Text;
                        this.UltraWebGridBill.Rows[index].Cells.FromKey("FLOW_EMPNAME").Text = this.UltraWebGridAudit.Rows[i].Cells.FromKey("localname").Text;
                        this.UltraWebGridBill.Rows[index].Cells.FromKey("FLOW_NOTES").Text = string.IsNullOrEmpty(this.UltraWebGridAudit.Rows[i].Cells.FromKey("notes").Text) ? "" : this.UltraWebGridAudit.Rows[i].Cells.FromKey("notes").Text;
                        this.UltraWebGridBill.Rows[index].Cells.FromKey("FLOW_MANAGER").Text = string.IsNullOrEmpty(this.UltraWebGridAudit.Rows[i].Cells.FromKey("managername").Text) ? "" : this.UltraWebGridAudit.Rows[i].Cells.FromKey("managername").Text;
                        //this.UltraWebGridAudit.Rows.RemoveAt(i);
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

        private void ddlDataBind(string type, DropDownList ddl)
        {
            this.tempDataTable.Clear();
            this.tempDataTable = workflowset.GetDocNoTypeList(type);
            ddl.DataSource = tempDataTable;
            ddl.DataTextField = "DataValue";
            ddl.DataValueField = "DataCode";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("--請選擇--", ""));
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

        private void ddlDataBind_2(string type, DropDownList ddl)
        {
            this.tempDataTable.Clear();
            this.tempDataTable = workflowset.GetDocNoTypeList_new(type);
            ddl.DataSource = tempDataTable;
            ddl.DataTextField = "DataValue";
            ddl.DataValueField = "DataCode";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("--請選擇--", ""));
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
            ddl.Items.Insert(0, new ListItem("--請選擇--", ""));
        }
        private void ddlDataBind_1(string type, DropDownList ddl)
        {
            this.tempDataTable.Clear();
            this.tempDataTable = workflowset.GetOverTimeType(type);
            ddl.DataSource = tempDataTable;
            ddl.DataTextField = "V_NAME";
            ddl.DataValueField = "V_CODE";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("--請選擇--", ""));
        }
        #endregion

        protected void ddl_doctype_1_SelectedIndexChanged(object sender, EventArgs e)
        {
            hf_deptid.Value = this.txtOrgCode_edit.Text;
            if (string.IsNullOrEmpty(hf_deptid.Value))
            {
                ddl_doctype_1.SelectedValue = "";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('" + Message.choess_OrgCode + "');</script>");
                return;
            }
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
        #endregion
    }
}
