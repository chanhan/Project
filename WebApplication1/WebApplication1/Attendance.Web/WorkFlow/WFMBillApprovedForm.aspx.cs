using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
using GDSBG.MiABU.Attendance.BLL.WorkFlow;

namespace GDSBG.MiABU.Attendance.Web.WorkFlow
{
    public partial class WFMBillApprovedForm : BasePage
    {

        protected DataSet tempDataSet;
        protected DataTable tempDataTable;
        private WFSignCenterBll bll = new WFSignCenterBll();
        int totalCount = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
            //    //((ImageButton)this.PageNavigator.FindControl("ImageButtonPrevious")).Click += new ImageClickEventHandler(this.ButtonPrevious_Click);
            //    //((ImageButton)this.PageNavigator.FindControl("ImageButtonNext")).Click += new ImageClickEventHandler(this.ButtonNext_Click);
            //    //((ImageButton)this.PageNavigator.FindControl("ImageButtonGoto")).Click += new ImageClickEventHandler(this.ButtonGoto_Click);
            if (!base.IsPostBack)
            {
                //this.Internationalization();
                this.ImageDepCode.Attributes.Add("onclick", "javascript:GetTreeDataValue(\"textBoxDepCode\",'" + Request.QueryString["ModuleCode"] + "','textBoxDepName')");
               // this.ImageDepCode.Attributes.Add("onclick", "javascript:GetTreeDataValue('textBoxDepCode','Department','',\"WHERE companyid='" + this.Session["companyID"].ToString() + "'\",'" + BaseForm.sAppPath + "','" + base.Request["moduleCode"] + "','textBoxDepName')");
                this.tempDataSet = new DataSet();
                this.tempDataSet = bll.GetDataSetBySQL("Select BillTypeName,BillTypeNo From GDS_WF_BILLTYPE Where AuditFlag ='Y' order by orderNo");
                this.ddlBillTypeCode.DataSource = this.tempDataSet.Tables["TempTable"].DefaultView;
                this.ddlBillTypeCode.DataTextField = "BillTypeName";
                this.ddlBillTypeCode.DataValueField = "BillTypeNo";
                this.ddlBillTypeCode.DataBind();
                this.tempDataSet.Clear();
                this.tempDataSet = bll.GetDataByCondition_HRM(" WHERE DataType='WFMBillStatus' and DataCode<>'3' ORDER BY OrderId");
                this.ddlStatus.DataSource = this.tempDataSet.Tables[0].DefaultView;
                this.ddlStatus.DataTextField = "DataValue";
                this.ddlStatus.DataValueField = "DataCode";
                this.ddlStatus.DataBind();
                this.ddlStatus.Items.Insert(0, new ListItem("", ""));

                
                //this.textBoxApplyDateFrom.Attributes.Add("onfocus", "calendar('" + base.Language + "','" + base.dateFormat + "');");
                //this.textBoxApplyDateFrom.Attributes.Add("formater", base.dateFormat);
                this.textBoxApplyDateFrom.Text = DateTime.Now.AddDays(-30.0).ToString("yyyy/MM/dd");
                //this.textBoxApplyDateTo.Attributes.Add("onfocus", "calendar('" + base.Language + "','" + base.dateFormat + "');");
                //this.textBoxApplyDateTo.Attributes.Add("formater", "yyyy/MM/dd");
                this.textBoxApplyDateTo.Text = DateTime.Now.ToString("yyyy/MM/dd");
                this.ModuleCode.Value = Request.QueryString["ModuleCode"];
            }
            SetCalendar(textBoxApplyDateFrom, textBoxApplyDateTo);
            }
            catch (Exception ex)
            {
                WriteMessage(2, (ex.InnerException == null) ? ex.Message : ex.InnerException.Message);
            }
        }

        #region 分頁
        protected void pager_PageChanged(object sender, EventArgs e)
        {
            this.Query(true, "Goto");
        }
        #endregion

        protected void ButtonGoto_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                pager.CurrentPageIndex = 1;
                this.Query(false, "Goto");
            }
            catch (Exception ex)
            {
                WriteMessage(2, (ex.InnerException == null) ? ex.Message : ex.InnerException.Message);
            }
        }

        protected void ButtonNext_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                this.Query(false, "Next");
            }
            catch (Exception ex)
            {
                WriteMessage(2, (ex.InnerException == null) ? ex.Message : ex.InnerException.Message);
            }
        }

        protected void ButtonPrevious_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                this.Query(false, "Previous");
            }
            catch (Exception ex)
            {
                WriteMessage(2, (ex.InnerException == null) ? ex.Message : ex.InnerException.Message);
            }
        }

        protected void ButtonQuery_Click(object sender, EventArgs e)
        {
            try
            {
                pager.CurrentPageIndex = 1;
                this.Query(true, "Goto");
                this.ProcessFlag.Value = "";
            }
            catch (Exception ex)
            {
                WriteMessage(2, (ex.InnerException == null) ? ex.Message : ex.InnerException.Message);
            }
        }

    private void DataUIBind()
    {
        this.UltraWebGridBill.DataSource = this.tempDataSet.Tables["WFM_Bill"].DefaultView;
        this.UltraWebGridBill.DataBind();
        if (this.tempDataSet.Tables["WFM_Bill"].Rows.Count > 0)
        {
            this.UltraWebGridBill.Rows[0].Selected = true;
            this.UltraWebGridBill.Rows[0].Activated = true;
        }
    }


    private void Query(bool WindowOpen, string forwarderType)
    {
        string condition = "";
        if (WindowOpen)
        {
            string CSq0q0001;
            string ddlStr = "";
            string[] temVal = null;
            if (CurrentUserInfo.RoleCode.IndexOf("WFM") >= 0)
            {
                CSq0q0001 = condition;
                condition = CSq0q0001 + " and exists (select 1 from GDS_ATT_AuditStatus e where e.BillNo=a.BillNo and e.Auditstatus<>'0' and (e.AuditMan='" + CurrentUserInfo.Personcode + "' or e.OldAuditMan='" + CurrentUserInfo.Personcode + "'))";
            }
            else if (CurrentUserInfo.RoleCode.IndexOf("Admin") >= 0)
            {
                //if (base.bPrivileged)
                //{
                condition = (condition + " AND exists (SELECT 1 FROM (" + base.sqlDep + ") e where e.DepCode=a.OrgCode)") + " and a.Status >'0'";
                //}
            }
            else
            {
                CSq0q0001 = condition;
                condition = CSq0q0001 + " and exists (select 1 from GDS_ATT_AuditStatus e where e.BillNo=a.BillNo and e.Auditstatus<>'0' and (e.AuditMan='" + CurrentUserInfo.Personcode + "' or e.OldAuditMan='" + CurrentUserInfo.Personcode + "'))";
            }
            if (this.textBoxDepName.Text.Trim().Length != 0)
            {
                condition = condition + " AND a.OrgCode in (SELECT DepCode FROM GDS_SC_DEPARTMENT START WITH depname LIKE '" + this.textBoxDepName.Text.Trim() + "%' CONNECT BY PRIOR depcode = parentdepcode)";
            }
            ddlStr = "";
            if (this.ddlBillTypeCode.SelectedValue != "")
            {
                temVal = this.ddlBillTypeCode.SelectedValuesToString(",").Split(new char[] { ',' });
                for (int iLoop = 0; iLoop < temVal.Length; iLoop++)
                {
                    ddlStr = ddlStr + " a.BillNo like '" + temVal[iLoop] + "%' or";
                }
                ddlStr = ddlStr.Substring(0, ddlStr.Length - 2);
                condition = condition + " and (" + ddlStr + ")";
            }
            if (this.textBoxBillNo.Text.Trim().Length != 0)
            {
                condition = condition + " AND a.BillNo like '" + this.textBoxBillNo.Text.Trim() + "%'";
            }
            if (this.ddlStatus.SelectedValue.Length != 0)
            {
                condition = condition + " AND a.Status = '" + this.ddlStatus.SelectedValue + "'";
            }
            if (this.textBoxApplyDateFrom.Text.Trim().Length != 0)
            {
                condition = condition + " AND a.ApplyDate >= to_date('" + DateTime.Parse(this.textBoxApplyDateFrom.Text.Trim()).ToString("yyyy/MM/dd") + " 00:00','yyyy/mm/dd hh24:mi') ";
            }
            if (this.textBoxApplyDateTo.Text.Trim().Length != 0)
            {
                condition = condition + " AND a.ApplyDate <= to_date('" + DateTime.Parse(this.textBoxApplyDateTo.Text.Trim()).ToString("yyyy/MM/dd") + " 23:59','yyyy/mm/dd hh24:mi') ";
            }
           // condition = condition + " order by a.ApplyDate Desc";
            this.ViewState.Add("condition", condition);
        }
        else
        {
            condition = Convert.ToString(this.ViewState["condition"]);
        }
        //base.SetForwardPage(forwarderType, ((WebNumericEdit) this.PageNavigator.FindControl("WebNumericEditCurrentpage")).Value.ToString());
        this.tempDataSet = bll.GetApprovedDataByCondition(condition, pager.CurrentPageIndex, pager.PageSize, out totalCount);
        pager.RecordCount = totalCount;
       // this.SetPageInfor(base.forwarderPage, base.totalPage, base.totalRecodrs);
        this.DataUIBind();
        //Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('事務處理完成.');</script>");
        WriteMessage(0, "事務處理完成.");
        pager.TextAfterPageIndexBox = "/" + pager.PageCount.ToString();
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

    protected void SetPageInfor(int currentPage, int totalPage, int totalRecodrs)
    {
        //((WebNumericEdit)this.PageNavigator.FindControl("WebNumericEditCurrentpage")).Text = Convert.ToString(currentPage);
        //((Label)this.PageNavigator.FindControl("LabelTotalpage")).Text = Convert.ToString(totalPage);
        //((Label)this.PageNavigator.FindControl("LabelTotalrecords")).Text = Convert.ToString(totalRecodrs);
    }

    protected void UltraWebGridBill_DataBound(object sender, EventArgs e)
    {
        string BillTypeCode = "";
        for (int i = 0; i < this.UltraWebGridBill.Rows.Count; i++)
        {
            BillTypeCode = (this.UltraWebGridBill.Rows[i].Cells.FromKey("BillTypeCode").Value == null) ? "" : this.UltraWebGridBill.Rows[i].Cells.FromKey("BillTypeCode").Value.ToString();
            this.UltraWebGridBill.Rows[i].Cells.FromKey("BillNo").TargetURL = "javascript:ShowBillDetail('" + this.UltraWebGridBill.Rows[i].Cells.FromKey("BillNo").Text + "','" + BillTypeCode + "')";
            if (this.UltraWebGridBill.Rows[i].Cells.FromKey("BillTypeCode").Text.Equals("OTMAdvanceApplyG3"))
            {
                this.UltraWebGridBill.Rows[i].Cells.FromKey("BillTypeName").Style.ForeColor = Color.Red;
            }
            if (this.UltraWebGridBill.Rows[i].Cells.FromKey("Status").Text.Equals("1"))
            {
                this.UltraWebGridBill.Rows[i].Cells.FromKey("StatusName").Style.ForeColor = Color.DarkGray;
            }
            if (this.UltraWebGridBill.Rows[i].Cells.FromKey("Status").Text.Equals("2"))
            {
                this.UltraWebGridBill.Rows[i].Cells.FromKey("StatusName").Style.ForeColor = Color.Red;
            }
            if (this.UltraWebGridBill.Rows[i].Cells.FromKey("Status").Text.Equals("3"))
            {
                this.UltraWebGridBill.Rows[i].Cells.FromKey("StatusName").Style.ForeColor = Color.SaddleBrown;
            }
            if (this.UltraWebGridBill.Rows[i].Cells.FromKey("Status").Text.Equals("0"))
            {
                this.UltraWebGridBill.Rows[i].Cells.FromKey("StatusName").Style.ForeColor = Color.Green;
            }
           // this.UltraWebGridBill.Rows[i].Cells.FromKey("OrgName").Value = ((ServiceLocator) this.Session["serviceLocator"]).GetFunctionData().GetAllAuditDept(this.UltraWebGridBill.Rows[i].Cells.FromKey("OrgCode").Value.ToString());
        }
    }
    }
}
