using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Infragistics.WebUI.UltraWebGrid;
using System.Drawing;
using System.Data;
using GDSBG.MiABU.Attendance.BLL.WorkFlow;
using System.Web.Profile;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;

namespace GDSBG.MiABU.Attendance.Web.WorkFlow
{
    public partial class WFMBillCenterForm : BasePage
    {
        protected DataSet tempDataSet;

        private WFSignCenterBll bll = new WFSignCenterBll();
        static SynclogModel logmodel = new SynclogModel();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!base.IsPostBack)
                {
                    logmodel.ProcessOwner = CurrentUserInfo.Personcode;
                    logmodel.TransactionType = (Request.QueryString["ModuleCode"] == null) ? "" : Request.QueryString["ModuleCode"].ToString();
                    logmodel.LevelNo = "2";
                    logmodel.FromHost = Request.UserHostAddress;

                    this.Context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                   // this.Internationalization();
                    this.Query();
                    this.ModuleCode.Value = Request.QueryString["ModuleCode"];
                    //if (this.Session["roleCode"].ToString().IndexOf("WFM") >= 0)
                    //{
                        //this.ButtonManagerLeave.Visible = true;
                        this.BatchAudit.Visible = true;
                        //this.ButtonLoginLog.Visible = true;
                        //this.ButtonChangePassWord.Visible = true;
                    //}
                    //else
                    //{
                        //this.ButtonManagerLeave.Visible = false;
                       // this.ButtonLoginLog.Visible = false;
                       // this.ButtonChangePassWord.Visible = false;
                        //this.BatchAudit.Visible = false;
                    //}
                }
            }
            catch (Exception ex)
            {
                //base.WriteMessage(2, (ex.InnerException == null) ? ex.Message : ex.InnerException.Message);
            }
        }

        protected void ButtonBatchAudit_Click(object sender, EventArgs e)
        {
            try
            {
                int intAuditOk = 0;
                TemplatedColumn tcol = (TemplatedColumn)this.UltraWebGridBill.Bands[0].Columns[0];
                string BillNo = "";
                string BillTypeNo = "";
                for (int i = 0; i < this.UltraWebGridBill.Rows.Count; i++)
                {
                    CellItem GridItem = (CellItem)tcol.CellItems[i];
                    CheckBox chkIsHaveRight = (CheckBox)GridItem.FindControl("CheckBoxCell");
                    if (chkIsHaveRight.Checked)
                    {
                        BillNo = this.UltraWebGridBill.DisplayLayout.Rows[i].Cells.FromKey("BillNo").Text;
                        BillTypeNo = BillNo.Substring(0, 3);
                       // BillTypeNo = ((ServiceLocator)this.Session["serviceLocator"]).GetFunctionData().GetBillTypeCode(BillTypeNo);
                        bll.SaveBatchAuditData(BillNo, CurrentUserInfo.Personcode, BillTypeNo,logmodel);
                        intAuditOk++;
                    }
                }
                if (intAuditOk > 0)
                {
                    //base.WriteMessage(0, base.GetResouseValue("common.message.successcount") + "：" + intAuditOk);
                    this.Query();
                }
                else
                {
                    //base.WriteMessage(1, base.GetResouseValue("common.message.data.select"));
                }
            }
            catch (Exception ex)
            {
               // base.WriteMessage(1, (ex.InnerException == null) ? ex.Message : ex.InnerException.Message);
            }
        }

        protected void ButtonReset_Click(object sender, EventArgs e)
        {
            try
            {
                this.Query();
            }
            catch (Exception ex)
            {
               // base.WriteMessage(1, (ex.InnerException == null) ? ex.Message : ex.InnerException.Message);
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

        private void Query()
        {
            string condition = "";
            condition = condition + " and a.Status ='0'" + " and b.OrderNo=(select nvl(min(e.OrderNo),'-1') from gds_att_auditstatus e where e.BillNo=a.BillNo and e.Auditstatus='0')";
            //if (this.Session["roleCode"].ToString().IndexOf("WFM") >= 0)
            //{
            //    condition = condition + " and b.AuditMan='" + CurrentUserInfo.Personcode + "'";
            //}
            //else if (this.Session["roleCode"].ToString().IndexOf("Admin") >= 0)
            //{
            //    condition = condition + " and ROWNUM<=50 and b.AuditMan='" + CurrentUserInfo.Personcode + "'";
            //}
            //else
            {
                condition = condition + " and b.AuditMan='" + CurrentUserInfo.Personcode + "'";
            }
            this.tempDataSet = bll.GetAuditCenterDataByCondition(condition);
            this.DataUIBind();
            //base.WriteMessage(0, base.GetResouseValue("common.message.trans.complete"));
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
                this.UltraWebGridBill.Rows[i].Cells.FromKey("OrgName").Value = bll.GetAllAuditDept(Convert.ToString(this.UltraWebGridBill.Rows[i].Cells.FromKey("OrgCode").Value));
                if ((((BillTypeCode.Equals("LeaveTypeAE") || BillTypeCode.Equals("LeaveTypeAF")) || (BillTypeCode.Equals("LeaveTypeAG") || BillTypeCode.Equals("LeaveTypeAH"))) || ((BillTypeCode.Equals("LeaveTypeAI") || BillTypeCode.Equals("LeaveTypeAJ")) || (BillTypeCode.Equals("LeaveTypeAK") || BillTypeCode.Equals("LeaveTypeM")))) || BillTypeCode.Equals("LeaveTypeN"))
                {
                    ((CheckBox)((CellItem)((TemplatedColumn)this.UltraWebGridBill.Columns[0]).CellItems[i]).FindControl("CheckBoxCell")).Enabled = false;
                }
            }
        }

       

        protected DefaultProfile Profile
        {
            get
            {
                return (DefaultProfile)this.Context.Profile;
            }
        }
    }
}
