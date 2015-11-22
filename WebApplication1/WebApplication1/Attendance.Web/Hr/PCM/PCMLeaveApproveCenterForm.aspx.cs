using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
using System.Text;
using GDSBG.MiABU.Attendance.BLL.Hr.PCM;
using Resources;
using System.Web.Script.Serialization;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;

namespace GDSBG.MiABU.Attendance.Web.Hr.PCM
{
    public partial class PCMLeaveApproveCenterForm : BasePage
    {
        DataTable dt_temp = new DataTable();
        Bll_LeaveApplyApprove bll_Leave = new Bll_LeaveApplyApprove();
        JavaScriptSerializer JsSerializer = new JavaScriptSerializer();
        Dictionary<string, string> ClientMessage = null;
        static SynclogModel logmodel = new SynclogModel();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (ClientMessage == null)
                {
                    ClientMessage = new Dictionary<string, string>();
                    ClientMessage.Add("wfm_message_data_disapprove", Message.wfm_message_data_disapprove);
                    ClientMessage.Add("wfm_message_disaudit", Message.wfm_message_disaudit);
                    ClientMessage.Add("common_message_data_return", Message.common_message_data_return);
                

                    string clientmsg = JsSerializer.Serialize(ClientMessage);
                    Page.ClientScript.RegisterStartupScript(GetType(), "ClientMessage", "var Message=" + clientmsg + ";", true);
                }

                if (!base.IsPostBack)
                {
                    string BillNo = (base.Request.QueryString["BillNo"] == null) ? "" : base.Request.QueryString["BillNo"].ToString();
                    string OPENTYPE = (base.Request.QueryString["OPENTYPE"] == null) ? "" : base.Request.QueryString["OPENTYPE"].ToString();
                    if (BillNo.Length == 0)
                    {
                        base.Response.Write("<script type='text/javascript'>alert('" +Message.wfm_message_billdeleted + "');window.close();</script>");
                    }
                    else
                    {
                        this.textBoxKQLBillNo.Text = BillNo;
                        this.Query(BillNo);
                        if (OPENTYPE == "PCMLeaveApprovedCenter")
                        {
                            this.ButtonApproveAgree.Enabled = false;
                            this.ButtonDisApprove.Enabled = false;
                            this.ApproveBTN.Visible = false;
                        }
                    }
                }
                PageHelper.ButtonControlsWF(FuncList, pnlShowPanel.Controls, base.FuncListModule);
            }
            catch (Exception ex)
            {
                WriteMessage(2, (ex.InnerException == null) ? ex.Message : ex.InnerException.Message);
            }
        }
        private void Query(string BillNo)
        {
            string condition = "and a.ID ='" + BillNo + "'";
            DataTable dt_temps = bll_Leave.GetLeaveApplyApproveInfo(condition);
            if (dt_temps != null && dt_temps.Rows.Count > 0)
            {
                this.textBoxStartDate.Text = (dt_temps.Rows[0]["StartDate"].ToString().Trim() == null) ? "" : string.Format("{0:" + "yyyy-MM-dd" + "}", Convert.ToDateTime(dt_temps.Rows[0]["StartDate"].ToString().Trim()));
                this.textBoxStartTime.Text = (dt_temps.Rows[0]["StartTime"].ToString().Trim() == null) ? "" : dt_temps.Rows[0]["StartTime"].ToString().Trim();
                this.textBoxEndDate.Text = (dt_temps.Rows[0]["EndDate"].ToString().Trim() == null) ? "" : string.Format("{0:" + "yyyy-MM-dd" + "}", Convert.ToDateTime(dt_temps.Rows[0]["EndDate"].ToString().Trim()));
                this.textBoxEndTime.Text = (dt_temps.Rows[0]["EndTime"].ToString().Trim() == null) ? "" : dt_temps.Rows[0]["EndTime"].ToString().Trim();
                this.textBoxLVTotal.Text = (dt_temps.Rows[0]["LVTotal"].ToString().Trim() == null) ? "" : Convert.ToString((double)(Convert.ToDouble(dt_temps.Rows[0]["LVTotal"].ToString().Trim()) / 8.0));
                this.textBoxReason.Text = (dt_temps.Rows[0]["Reason"].ToString().Trim() == null) ? "" : dt_temps.Rows[0]["Reason"].ToString().Trim();
                this.textBoxProxy.Text = (dt_temps.Rows[0]["Proxy"].ToString().Trim() == null) ? "" : dt_temps.Rows[0]["Proxy"].ToString().Trim();
                this.textBoxRemark.Text = (dt_temps.Rows[0]["Remark"].ToString().Trim() == null) ? "" : dt_temps.Rows[0]["Remark"].ToString().Trim();
                this.textBoxLVTypeName.Text = (dt_temps.Rows[0]["LVTypeName"].ToString().Trim() == null) ? "" : dt_temps.Rows[0]["LVTypeName"].ToString().Trim();
                this.textBoxApplyType.Text = (dt_temps.Rows[0]["ApplyTypeName"].ToString().Trim() == null) ? "" : dt_temps.Rows[0]["ApplyTypeName"].ToString().Trim();
                this.EmpQuery(dt_temps.Rows[0]["WorkNo"].ToString());
            }
            else
            {
                base.Response.Write("<script type='text/javascript'>alert('" + Message.wfm_message_billdeleted + "');window.close();</script>");
            }
        }
        protected void ButtonApprove_Click(object sender, EventArgs e)
        {
            try
            {
                string ApRemark = this.textBoxApRemark.Text.Trim();
                string BillNo = this.textBoxKQLBillNo.Text;
                string Flow_LevelRemark = Message.flow_levelremark;
                if (bll_Leave.SaveZBLHLeaveAuditData(BillNo, CurrentUserInfo.Personcode, ApRemark, Flow_LevelRemark,logmodel))
                {
                    base.Response.Write("<script type='text/javascript'>alert('" +Message.wfm_message_approvecomplete+ "');window.opener.document.all.ButtonReset.click();window.close();</script>");
                }
                else
                {
                    base.Response.Write("<script type='text/javascript'>alert('" + Message.wfm_message_approveerror + "');window.opener.document.all.ButtonReset.click();window.close();</script>");
                }
            }
            catch (Exception ex)
            {
                WriteMessage(2, (ex.InnerException == null) ? ex.Message : ex.InnerException.Message);
            }
        }
        protected void ButtonDisApprove_Click(object sender, EventArgs e)
        {
            try
            {
                string BillNo = "";
                bool bResult = false;
                string ApRemark = this.textBoxApRemark.Text.Trim();

                BillNo = this.textBoxKQLBillNo.Text;
                bResult=bll_Leave.SaveDisLeaveAuditData(BillNo, CurrentUserInfo.Personcode, ApRemark,logmodel);
                base.Response.Write("<script type='text/javascript'>alert('" +Message.wfm_message_disapprovecomplete+ "');window.opener.document.all.ButtonReset.click();window.close();</script>");
            }
            catch (Exception ex)
            {
                WriteMessage(2, (ex.InnerException == null) ? ex.Message : ex.InnerException.Message);
            }
        }
        private void EmpQuery(string sEmployeeNo)
        {
            string condition = "WHERE a.WorkNO='" + sEmployeeNo.ToUpper() + "'";
            dt_temp = bll_Leave.GetVDataByCondition(condition);
            if (dt_temp.Rows.Count > 0)
            {
                foreach (DataRow newRow in dt_temp.Rows)
                {
                    this.textBoxEmployeeNo.Text = newRow["WORKNO"].ToString();
                    this.textBoxName.Text = newRow["LOCALNAME"].ToString();
                    this.textBoxDPcode.Text = newRow["DName"].ToString();
                    this.textBoxSex.Text = newRow["SEX"].ToString();
                    this.textBoxJoinDate.Text = (newRow["JoinDate"].ToString().Trim() == "") ? "" : string.Format("{0:" + "yyyy-MM-dd" + "}", newRow["JoinDate"]);
                    this.textBoxManager.Text = newRow["managername"].ToString();
                    this.textBoxLevelCode.Text = newRow["levelname"].ToString();
                    this.textBoxComeYears.Text = newRow["comeyears"].ToString();
                    this.GetEmpLeave(sEmployeeNo.ToUpper());
                }
            }
        }
        private void GetEmpLeave(string sEmpNo)
        {
            string strSex = bll_Leave.GetSex(sEmpNo);
            StringBuilder sbHeader = new StringBuilder();
            StringBuilder sbAble = new StringBuilder();
            StringBuilder sbAlready = new StringBuilder();
            string sql = "";
            string sTime = "";
            try
            {
                if (strSex.Equals("1"))
                {
                    dt_temp = bll_Leave.GetDataByCondition(" WHERE EffectFlag='Y' and LVTypeCode<>'S' and LVTypeCode not in('d','e','f','g','h','s') ORDER BY LVTypeCode");
                }
                else
                {
                    dt_temp = bll_Leave.GetDataByCondition(" WHERE EffectFlag='Y' and LVTypeCode<>'L' ORDER BY LVTypeCode");
                }
                int LeaveTypeCount = dt_temp.Rows.Count;
                string intWidth = Convert.ToString(Math.Round((double)((100 / LeaveTypeCount) - 0.5)));
                sbHeader.Append("<table class='inner_table' cellspacing='0' cellpadding='0' width='100%'>");
                sbHeader.Append(string.Concat(new object[] { " <tr><td class='td_label' style='width:", intWidth, "%'><font style='FONT-WEIGHT: bold; FONT-SIZE: 12pt; COLOR: navy'>", DateTime.Now.Year, "</font></td>" }));
                sbAble.Append(" <tr><td class='td_label' style='width:" + intWidth + "%'><font style='FONT-WEIGHT: bold; FONT-SIZE: 12pt; COLOR: navy'>" + Message.bfw_kqm_leaveapply_ablerest + "</font></td>");
                sbAlready.Append(" <tr><td class='td_label' style='width:" + intWidth + "%'><font style='FONT-WEIGHT: bold; FONT-SIZE: 12pt; COLOR: navy'>" +Message.bfw_kqm_leaveapply_alreadyrest+ "</font></td>");
                if ((dt_temp != null) && (dt_temp.Rows.Count > 0))
                {
                    for (int i = 0; i < dt_temp.Rows.Count; i++)
                    {
                        if (dt_temp.Rows[i]["LVTypeCode"].ToString().Equals("Y"))
                        {
                            string[] temVal = bll_Leave.GetYearLeaveDays(sEmpNo, DateTime.Now.Year.ToString(), DateTime.Now.ToString("yyyy-MM-dd")).Split(new char[] { '|' });
                            sbAble.Append("<td class='td_label' style='width:" + intWidth + "%'><font style='FONT-WEIGHT: bold; FONT-SIZE: 12pt; COLOR: navy'>" + Convert.ToString((double)(Convert.ToDouble(temVal[0].ToString()) * 8.0)) + "</font></td>");
                            sbHeader.Append("<td class='td_label' style='width:" + intWidth + "%'><font style='FONT-WEIGHT: bold; FONT-SIZE: 12pt; COLOR: navy'>" + dt_temp.Rows[i]["LVTypeName"].ToString() + "(H)</font></td>");
                            sbAlready.Append(" <td class='td_label' style='width:" + intWidth + "%'><font style='FONT-WEIGHT: bold; FONT-SIZE: 12pt; COLOR: navy'>" + Convert.ToString((double)(Convert.ToDouble(temVal[2].ToString()) * 8.0)) + "</font></td>");
                        }
                        else
                        {
                            sbHeader.Append("<td class='td_label' style='width:" + intWidth + "%'><font style='FONT-WEIGHT: bold; FONT-SIZE: 12pt; COLOR: navy'>" + dt_temp.Rows[i]["LVTypeName"].ToString() + "(H)</font></td>");
                            if (!(dt_temp.Rows[i]["LimitDays"].ToString().Equals("") || dt_temp.Rows[i]["LVTypeCode"].ToString().Equals("U")))
                            {
                                sbAble.Append("<td class='td_label' style='width:" + intWidth + "%'><font style='FONT-WEIGHT: bold; FONT-SIZE: 12pt; COLOR: navy'>" + Convert.ToString((double)(Convert.ToDouble(dt_temp.Rows[i]["LimitDays"].ToString()) * 8.0)) + "</font></td>");
                            }
                            else if (dt_temp.Rows[i]["LVTypeCode"].ToString().Equals("U"))
                            {
                                sql = "SELECT MRelAdjust FROM GDS_ATT_MONTHTOTAL WHERE workno='" + sEmpNo + "' AND YearMonth=TO_CHAR(SYSDATE,'yyyymm')";
                                sTime = bll_Leave.GetValue(sql);
                                sTime = sTime.Equals("") ? "-" : sTime;
                                sbAble.Append("<td class='td_label' style='width:" + intWidth + "%'><font style='FONT-WEIGHT: bold; FONT-SIZE: 12pt; COLOR: navy'>" + sTime + "</font></td>");
                            }
                            else
                            {
                                sbAble.Append("<td class='td_label' style='width:" + intWidth + "%'><font style='FONT-WEIGHT: bold; FONT-SIZE: 12pt; COLOR: navy'>-</font></td>");
                            }
                            if (dt_temp.Rows[i]["LVTypeCode"].ToString().Equals("U"))
                            {
                                sql = "SELECT MAdjust1 FROM GDS_ATT_MONTHTOTAL WHERE workno='" + sEmpNo + "' AND YearMonth=TO_CHAR(SYSDATE,'yyyymm')";
                            }
                            else
                            {
                                sql = string.Concat(new object[] { "SELECT SUM(LVTOTal) FROM GDS_ATT_LEAVEAPPLY WHERE WorkNo='", sEmpNo, "' AND (TO_CHAR(StartDate,'yyyy')=TO_CHAR(SYSDATE,'yyyy')  OR TO_CHAR(EndDate,'yyyy')=TO_CHAR(SYSDATE,'yyyy')) AND LVTypeCode='", dt_temp.Rows[i]["LVTypeCode"], "' AND Status='2'" });
                            }
                            sTime = bll_Leave.GetValue(sql);
                            sTime = sTime.Equals("") ? "-" : sTime;
                            sbAlready.Append(" <td class='td_label' style='width:" + intWidth + "%'><font style='FONT-WEIGHT: bold; FONT-SIZE: 12pt; COLOR: navy'>" + sTime + "</font></td>");
                        }
                    }
                }
                sbAlready.Append("</tr></table>");
                sbAble.Append("</tr>");
                sbHeader.Append("</tr>");
                this.divEmpLeave.InnerHtml = sbHeader.ToString() + sbAble.ToString() + sbAlready.ToString();
            }
            catch (Exception ex)
            {
                WriteMessage(2, (ex.InnerException == null) ? ex.Message : ex.InnerException.Message);
            }
        }
        private void WriteMessage(int messageType, string sMessage)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "show", "alert('" + sMessage + "')", true);
        }

    }
}
