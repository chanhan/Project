/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： KQMSelfServiceForm.cs
 * 檔功能描述：員工自助查詢功能模組UI類
 * 
 * 版本：1.0
 * 創建標識： 陈函 2012.02.03
 * 
 */
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Web.Script.Serialization;
using System.Web.UI;
using GDSBG.MiABU.Attendance.BLL.HRM.EmployeeData;
using GDSBG.MiABU.Attendance.BLL.KQM.Query;
using Resources;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;

namespace GDSBG.MiABU.Attendance.Web.KQM.Query
{
    public partial class KQMSelfServiceForm : BasePage
    {
        bool Privileged = true;//組織權限

        JavaScriptSerializer JsSerializer = new JavaScriptSerializer();

        Dictionary<string, string> ClientMessage = null;

        HrmEmpOtherMoveBll hrmEmpOtherMoveBll = new HrmEmpOtherMoveBll();
        KQMSelfServiceBll kqmSelfServiceBll = new KQMSelfServiceBll();
        string strModuleCode;
        static SynclogModel logmodel = new SynclogModel();

        protected void Page_Load(object sender, EventArgs e)
        {
            PageHelper.ButtonControls(FuncList, pnlShowPanel.Controls, base.FuncListModule);
            logmodel.ProcessOwner = CurrentUserInfo.Personcode;
            logmodel.TransactionType = Request.QueryString["ModuleCode"] == null ? "" : Request.QueryString["ModuleCode"].ToString();
            logmodel.LevelNo = "2";
            logmodel.FromHost = Request.UserHostAddress;

            strModuleCode = Request.QueryString["ModuleCode"] == null ? "" : Request.QueryString["ModuleCode"].ToString();
            if (ClientMessage == null)
            {
                ClientMessage = new Dictionary<string, string>();
                ClientMessage.Add("EmpNoNotExist", Message.EmpNoNotExist);
                ClientMessage.Add("EmpNoInput", Message.EmpNoInput);
            }
            string clientmsg = JsSerializer.Serialize(ClientMessage);
            Page.ClientScript.RegisterStartupScript(GetType(), "ClientMessage", "var Message=" + clientmsg + ";", true);
            IsHavePrivileged();
        }

        protected void btnCountA_Click(object sender, EventArgs e)
        {

            if (hidWorkNo.Value.Trim().Length > 0)
            {
                try
                {
                    kqmSelfServiceBll.GetEmpLeaveReport(hidWorkNo.Value.Trim(), logmodel);
                }
                catch (Exception)
                {
                }
                DataTable dataTableLeaveReportData = kqmSelfServiceBll.getLeaveReportData(hidWorkNo.Value.Trim());
                UltraWebGridLeaveReport.DataSource = dataTableLeaveReportData;
                UltraWebGridLeaveReport.DataBind();
            }
            else
            {
                string alert = "alert('" + Message.EmpNoInput + "')";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "KQMSelfService1", alert, true);
            }
        }
        protected void btnCountB_Click(object sender, EventArgs e)
        {
            if (hidWorkNo.Value.Trim().Length > 0)
            {
                try
                {
                    kqmSelfServiceBll.GetEmployeeShift(hidWorkNo.Value.Trim(), logmodel);
                }
                catch (Exception)
                {
                }
                DataTable dataTableWorkShiftData = kqmSelfServiceBll.getWorkShiftData(hidWorkNo.Value.Trim());
                UltraWebGridWorkShift.DataSource = dataTableWorkShiftData;
                UltraWebGridWorkShift.DataBind();
            }
            else
            {
                string alert = "alert('" + Message.EmpNoInput + "')";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "KQMSelfService2", alert, true);
            }
        }
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            hidWorkNo.Value = this.txtWorkNo.Text.Trim();
            DataTable dt = kqmSelfServiceBll.getEmpInfo(txtWorkNo.Text.Trim(), this.SqlDep, Privileged);
            string alert = "";
            if (dt != null && dt.Rows.Count > 0)
            {
                this.txtEmployeeNo.Text = dt.Rows[0]["WorkNo"].ToString();
                this.txtLevel.Text = dt.Rows[0]["LevelName"].ToString();
                this.txtTechnical.Text = dt.Rows[0]["TechnicalName"].ToString();
                this.txtLocalName.Text = dt.Rows[0]["LocalName"].ToString();
                this.txtBUDepName.Text = dt.Rows[0]["Syc"].ToString();

                if (dt.Rows[0]["JoinDate"].ToString().Length > 0)
                {
                    txtJoinBGDate.Text = string.Format("{0:yyyy/MM/dd}", Convert.ToDateTime(dt.Rows[0]["JoinDate"].ToString()));
                }
                this.txtSex.Text = dt.Rows[0]["Sex"].ToString();
                this.txtDepName.Text = dt.Rows[0]["DepName"].ToString();
                string PlaceName = "";
                DataTable dataTablePlaceName = kqmSelfServiceBll.getPlaceName(txtWorkNo.Text.Trim());
                for (int i = 0; i < dataTablePlaceName.Rows.Count; i++)
                {
                    if (PlaceName.Length > 0)
                    {
                        PlaceName = PlaceName + "  ";
                    }

                    PlaceName = PlaceName + dataTablePlaceName.Rows[i]["PlaceName"].ToString() + "  [" + dataTablePlaceName.Rows[i]["SCCode"].ToString() + "]";
                }
                this.txtPlaceName.Text = PlaceName;
                string EffectBellNo = "";
                DataTable dataTableEffectBellNo = kqmSelfServiceBll.getEffectBellNo(txtWorkNo.Text.Trim());
                if (dataTableEffectBellNo != null)
                {
                    for (int i = 0; i < dataTableEffectBellNo.Rows.Count; i++)
                    {
                        EffectBellNo = dataTableEffectBellNo.Rows[i]["BellNo"].ToString();
                    }
                    if (EffectBellNo.Length == 0)
                    {
                        DataTable dataTableEffectBellNo2 = kqmSelfServiceBll.getEffectBellNo2(txtWorkNo.Text.Trim());
                        if (dataTableEffectBellNo2 != null)
                        {
                            for (int i = 0; i < dataTableEffectBellNo2.Rows.Count; i++)
                            {
                                EffectBellNo = dataTableEffectBellNo2.Rows[i]["BellNo"].ToString();
                            }
                        }
                    }

                    if (EffectBellNo.Length == 0)
                    {
                        EffectBellNo = "ALL";
                    }
                }

                this.txtEffectBellNo.Text = EffectBellNo;
                DataTable dataTableKaoQinData = kqmSelfServiceBll.getKaoQinData(txtWorkNo.Text.Trim());
                UltraWebGridKaoQinQuery.DataSource = dataTableKaoQinData;
                UltraWebGridKaoQinQuery.DataBind();
                DataTable dataTableScoreItemData = kqmSelfServiceBll.getScoreItemData(txtWorkNo.Text.Trim());
                WebGridScoreItem.DataSource = dataTableScoreItemData;
                WebGridScoreItem.DataBind();
                DataTable dataTableLeaveReportData = kqmSelfServiceBll.getLeaveReportData(txtWorkNo.Text.Trim());
                UltraWebGridLeaveReport.DataSource = dataTableLeaveReportData;
                UltraWebGridLeaveReport.DataBind();
                DataTable dataTableWorkShiftData = kqmSelfServiceBll.getWorkShiftData(txtWorkNo.Text.Trim());
                UltraWebGridWorkShift.DataSource = dataTableWorkShiftData;
                UltraWebGridWorkShift.DataBind();
                DataTable dataTableLeaveDetailData = kqmSelfServiceBll.getLeaveDetailData(txtWorkNo.Text.Trim());
                UltraWebGridLeaveDetail.DataSource = dataTableLeaveDetailData;
                UltraWebGridLeaveDetail.DataBind();
                DataTable dataTableOTMMonthTotalData = kqmSelfServiceBll.getOTMMonthTotalData(txtWorkNo.Text.Trim());
                UltraWebGridOTMMonthTotal.DataSource = dataTableOTMMonthTotalData;
                UltraWebGridOTMMonthTotal.DataBind();
                DataTable dataTableOTMonthDetailData = kqmSelfServiceBll.getOTMonthDetailData(txtWorkNo.Text.Trim());
                UltraWebGridOTMonthDetail.DataSource = dataTableOTMonthDetailData;
                UltraWebGridOTMonthDetail.DataBind();
            }
            else
            {
                alert = "alert('" + Message.EmpNoNotExist + "')";
            }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "KQMSelfService", alert, true);
        }

        #region  是否有組織權限
        /// <summary>
        /// 是否有組織權限
        /// </summary>
        private void IsHavePrivileged()
        {

            if (CurrentUserInfo.Personcode.Equals("internal") || CurrentUserInfo.RoleCode.Equals("Person"))
            {
                Privileged = false;
            }
            else
            {
                DataTable dt = hrmEmpOtherMoveBll.GetDataByCondition(strModuleCode);
                if ((dt != null) && (dt.Rows.Count > 0))
                {
                    Privileged = false;
                }
            }
        }
        #endregion

        protected void UltraWebGridKaoQinQuery_DataBound(object sender, EventArgs e)
        {
            string WorkNo = txtWorkNo.Text.Trim();
            string kqDate = "";
            for (int i = 0; i < this.UltraWebGridKaoQinQuery.Rows.Count; i++)
            {
                kqDate = (UltraWebGridKaoQinQuery.Rows[i].Cells.FromKey("KQDate").Value == null) ? "" : Convert.ToDateTime(UltraWebGridKaoQinQuery.Rows[i].Cells.FromKey("KQDate").Text).ToString("yyyy/MM/dd");
                //    kqDate=UltraWebGridKaoQinQuery.Rows[i].Cells.FromKey("KQDate").Value.ToString();
                DataTable dt = kqmSelfServiceBll.getAbsentTotal(WorkNo, kqDate);
                UltraWebGridKaoQinQuery.Rows[i].Cells.FromKey("AbsentTotal").Value = dt.Rows.Count > 0 ? dt.Rows[0][0].ToString() : "";
            }
        }

        protected void WebGridScoreItem_DataBind(object sender, EventArgs e)
        {
            string sURL = "";
            for (int i = 0; i < this.WebGridScoreItem.Rows.Count; i++)
            {
                sURL = "PAMEmpQryForm.aspx?WorkNo=" + this.WebGridScoreItem.Rows[i].Cells.FromKey("WorkNo").Text + "&Year=" + this.WebGridScoreItem.Rows[i].Cells.FromKey("ANNUALCode").Text;
                this.WebGridScoreItem.Rows[i].Cells.FromKey("LocalName").TargetURL = "javascript:openEditWin('" + sURL + "','PAM',800,600)";
            }
        }

        protected void UltraWebGridLeaveDetail_DataBound(object sender, EventArgs e)
        {
            string ThisLVTotal = "";
            for (int i = 0; i < this.UltraWebGridLeaveDetail.Rows.Count; i++)
            {
                ThisLVTotal = this.UltraWebGridLeaveDetail.Rows[i].Cells.FromKey("LVTotal").Text;
                this.UltraWebGridLeaveDetail.Rows[i].Cells.FromKey("LVTotalDays").Text = Convert.ToString((double)(Convert.ToDouble(ThisLVTotal) / 8.0));
            }
        }

        protected void UltraWebGridOTMonthDetail_DataBound(object sender, EventArgs e)
        {
            for (int i = 0; i < this.UltraWebGridOTMonthDetail.Rows.Count; i++)
            {
                if (this.UltraWebGridOTMonthDetail.Rows[i].Cells.FromKey("Status").Text.Trim() == "0")
                {
                    this.UltraWebGridOTMonthDetail.Rows[i].Cells.FromKey("StatusName").Style.ForeColor = Color.Green;
                }
                if (this.UltraWebGridOTMonthDetail.Rows[i].Cells.FromKey("Status").Text.Trim() == "1")
                {
                    this.UltraWebGridOTMonthDetail.Rows[i].Cells.FromKey("StatusName").Style.ForeColor = Color.Blue;
                }
                if (this.UltraWebGridOTMonthDetail.Rows[i].Cells.FromKey("Status").Text.Trim() == "2")
                {
                    this.UltraWebGridOTMonthDetail.Rows[i].Cells.FromKey("StatusName").Style.ForeColor = Color.Black;
                }
                if (this.UltraWebGridOTMonthDetail.Rows[i].Cells.FromKey("Status").Text.Trim() == "3")
                {
                    this.UltraWebGridOTMonthDetail.Rows[i].Cells.FromKey("StatusName").Style.ForeColor = Color.Maroon;
                }
                if ((this.UltraWebGridOTMonthDetail.Rows[i].Cells.FromKey("IsProject").Value != null) && (this.UltraWebGridOTMonthDetail.Rows[i].Cells.FromKey("IsProject").Text.Trim() == "Y"))
                {
                    this.UltraWebGridOTMonthDetail.Rows[i].Style.ForeColor = Color.Red;
                }
            }
        }
    }
}
