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
using System.Collections.Generic;
using System.Web.Script.Serialization;
using GDSBG.MiABU.Attendance.Model.KQM.Query.OTM;
using GDSBG.MiABU.Attendance.BLL.KQM.Query.OTM;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;
using Resources;

namespace GDSBG.MiABU.Attendance.Web.KQM.OTM
{
    public partial class OTMMonthTotalEditForm : BasePage
    {
        DataTable tempDataTable = new DataTable();
        static DataTable dt = new DataTable();
        OTMTotalQryModel model = new OTMTotalQryModel();
        OTMTotalQryBll bllOTMQry = new OTMTotalQryBll();
        SynclogModel logmodel = new SynclogModel();
        protected void btnSave_Click(object sender, EventArgs e)
        {
                if (!string.IsNullOrEmpty(this.txtG2RelSalary.Text.Trim()))
                {
                    if (!base.IsDouble1(this.txtG2RelSalary.Text))
                    {
                        Page.ClientScript.RegisterStartupScript(GetType(), "show", "alert('小數點後隻能精確到.5')", true);
                    }
                    else
                    {
                        this.ProcessFlag.Value = "Modify";
                        model.WorkNo = HiddenWorkNo.Value;
                        model.YearMonth = HiddenYearMonth.Value;
                        this.tempDataTable = bllOTMQry.GetOTMQryList(model);
                        if (this.tempDataTable.Rows.Count > 0)
                        {
                            DataRow row = this.tempDataTable.Rows[0];
                            row.BeginEdit();
                            row["G2RelSalary"] = (this.txtG2RelSalary.Text == "") ? "0" : this.txtG2RelSalary.Value.ToString();
                            row["SPECG2SALARY"] = (this.txtSpecG2RelSalary.Text == "") ? "0" : this.txtSpecG2RelSalary.Value.ToString();
                            row["MRelAdjust"] = (this.txtMRelAdjust.Text == "") ? "0" : this.txtMRelAdjust.Text;
                            row.EndEdit();
                            this.tempDataTable.AcceptChanges();
                            if (!bllOTMQry.SaveData(this.ProcessFlag.Value,CurrentUserInfo.Personcode, this.tempDataTable))
                            {
                                Page.ClientScript.RegisterStartupScript(GetType(), "show", "alert('實發加班時數不能大於管控')", true);
                                return;
                            }
                           // ((ServiceLocator)this.Session["serviceLocator"]).GetFunctionData().CountCanAdjlasthy(this.HiddenWorkNo.Value, this.HiddenYearMonth.Value);
                            bllOTMQry.CountCanAdjlasthy(this.HiddenWorkNo.Value, this.HiddenYearMonth.Value);
                        }
                        base.Response.Write("<script type='text/javascript'>window.opener.document.all.btnQuery.click();window.close();</script>");
                    }
                }
        }

        private void Internationalization()
        {
            this.txtWorkNo.BorderStyle = BorderStyle.None;
            this.txtLocalName.BorderStyle = BorderStyle.None;
            this.txtG1Apply.BorderStyle = BorderStyle.None;
            this.txtG2Apply.BorderStyle = BorderStyle.None;
            this.txtG3Apply.BorderStyle = BorderStyle.None;
            this.txtG1RelSalary.BorderStyle = BorderStyle.None;
            this.txtG3RelSalary.BorderStyle = BorderStyle.None;
            this.txtOverTimeType.BorderStyle = BorderStyle.None;
            this.txtMRelAdjust.BorderStyle = BorderStyle.None;
            this.txtG1SpecApply.BorderStyle = BorderStyle.None;
            this.txtG2SpecApply.BorderStyle = BorderStyle.None;
            this.txtG3SpecApply.BorderStyle = BorderStyle.None;
            this.txtSpecG1RelSalary.BorderStyle = BorderStyle.None;
            this.txtSpecG3RelSalary.BorderStyle = BorderStyle.None;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                this.Internationalization();
                string WorkNo = (base.Request.QueryString["WorkNo"] == null) ? "" : base.Request.QueryString["WorkNo"].ToString();
                string YearMonth = (base.Request.QueryString["YearMonth"] == null) ? "" : base.Request.QueryString["YearMonth"].ToString();
                if ((WorkNo.Length == 0) || (YearMonth.Length == 0))
                {
                    base.Response.Write("<script type='text/javascript'>window.close();</script>");
                }
                else
                {
                    this.HiddenWorkNo.Value = WorkNo;
                    this.HiddenYearMonth.Value = YearMonth;
                    this.Query();
                    this.HiddenIsSpec.Value = bllOTMQry.GetParemeterValue();
                    this.btnSave.Attributes.Add("onclick", "return SaveRelSalary()");
                    this.txtG2RelSalary.Attributes.Add("onpropertychange", "javascript:CheckRelSalary();");
                    this.txtG3RelSalary.Attributes.Add("onpropertychange", "javascript:CheckRelSalary();");
                    this.txtSpecG2RelSalary.Attributes.Add("onpropertychange", "javascript:CheckRelSalary2();");
                }
            }
        }

        private void Query()
        {
            model.WorkNo=HiddenWorkNo.Value;
            model.YearMonth = HiddenYearMonth.Value;
            string sqlDep=base.SqlDep;
            this.tempDataTable = bllOTMQry.GetOTMQryList(model,sqlDep);
            if (this.tempDataTable.Rows.Count > 0)
            {
                this.txtWorkNo.Text = this.tempDataTable.Rows[0]["WorkNo"].ToString();
                this.txtLocalName.Text = this.tempDataTable.Rows[0]["LocalName"].ToString();
                this.txtOverTimeType.Text = this.tempDataTable.Rows[0]["OverTimeType"].ToString();
                this.txtG1Apply.Text = this.tempDataTable.Rows[0]["G1Apply"].ToString();
                this.txtG2Apply.Text = this.tempDataTable.Rows[0]["G2Apply"].ToString();
                this.txtG3Apply.Text = this.tempDataTable.Rows[0]["G3Apply"].ToString();
                this.txtG1SpecApply.Text = this.tempDataTable.Rows[0]["SPECG1APPLY"].ToString();
                this.txtG2SpecApply.Text = this.tempDataTable.Rows[0]["SPECG2APPLY"].ToString();
                this.txtG3SpecApply.Text = this.tempDataTable.Rows[0]["SPECG3APPLY"].ToString();
                this.txtG1RelSalary.Text = this.tempDataTable.Rows[0]["G1RelSalary"].ToString();
                this.txtG2RelSalary.Text = this.tempDataTable.Rows[0]["G2RelSalary"].ToString();
                this.txtG3RelSalary.Text = this.tempDataTable.Rows[0]["G3RelSalary"].ToString();
                this.txtSpecG1RelSalary.Text = this.tempDataTable.Rows[0]["SPECG1SALARY"].ToString();
                this.txtSpecG2RelSalary.Text = this.tempDataTable.Rows[0]["SPECG2SALARY"].ToString();
                this.txtSpecG3RelSalary.Text = this.tempDataTable.Rows[0]["SPECG3SALARY"].ToString();
                this.txtMRelAdjust.Text = this.tempDataTable.Rows[0]["MRelAdjust"].ToString();
                this.HiddenMRelAdjust.Value = this.tempDataTable.Rows[0]["MRelAdjust"].ToString();
                this.HiddenG2RelSalary.Value = (this.tempDataTable.Rows[0]["G2RelSalary"] == null) ? "0" : this.tempDataTable.Rows[0]["G2RelSalary"].ToString();
                this.HiddenG3RelSalary.Value = (this.tempDataTable.Rows[0]["G3RelSalary"] == null) ? "0" : this.tempDataTable.Rows[0]["G3RelSalary"].ToString();
                this.HiddenSpecG2RelSalary.Value = (this.tempDataTable.Rows[0]["SPECG2SALARY"] == null) ? "0" : this.tempDataTable.Rows[0]["SPECG2SALARY"].ToString();
                this.HiddenSpecG3RelSalary.Value = (this.tempDataTable.Rows[0]["SPECG3SALARY"] == null) ? "0" : this.tempDataTable.Rows[0]["SPECG3SALARY"].ToString();
            }
            else
            {
                base.Response.Write("<script type='text/javascript'>alert(\"" + Message.AtLastOneChoose+ "\");window.close();</script>");
            }
        }
    }
}
