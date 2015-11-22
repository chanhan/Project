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

namespace GDSBG.MiABU.Attendance.Web.Hr.PCM
{
    public partial class PCMLeaveApplyApprovedForm : BasePage
    {
        DataTable dt_temp = new DataTable();
        Bll_LeaveApplyApprove bll_Leave = new Bll_LeaveApplyApprove();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!base.IsPostBack)
                {
                    this.HiddenModuleCode.Value = base.Request["ModuleCode"].ToString();
                    this.Query(true, "Goto");
                }
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

        private void DataUIBind(DataTable dt)
        {
            if (dt!=null)
            {
                this.UltraWebGridLeaveApply.DataSource = dt;
                this.UltraWebGridLeaveApply.DataBind();
                if (dt.Rows.Count > 0)
                {
                    this.UltraWebGridLeaveApply.Rows[0].Selected = true;
                    this.UltraWebGridLeaveApply.Rows[0].Activated = true;
                }
            }
        }


        private void Query(bool WindowOpen, string forwarderType)
        {
            StringBuilder condition = new StringBuilder();
            if (this.ProcessFlag.Value.ToLower().Equals("condition") || WindowOpen)
            {
                condition.Append(" AND a.ProxyWorkNo = '" + CurrentUserInfo.Personcode + "' and a.ProxyStatus>'1'");
                this.ViewState.Add("condition", condition.ToString());
            }
            else
            {
                condition.Append(this.ViewState["condition"]);
            }
            int totalCount = 0;

            DataTable dt_temp = bll_Leave.GetLeaveApplyApproveInfo(condition.ToString(), pager.CurrentPageIndex, pager.PageSize, out  totalCount);
            this.DataUIBind(dt_temp);
            pager.RecordCount = totalCount;
            pager.TextAfterPageIndexBox = "/" + pager.PageCount.ToString();

            //WriteMessage(0,Message.common_message_trans_complete);
        }

        protected void UltraWebGridLeaveApply_DataBound(object sender, EventArgs e)
        {
            string sStatus = "";
            for (int i = 0; i < this.UltraWebGridLeaveApply.Rows.Count; i++)
            {
                sStatus = this.UltraWebGridLeaveApply.Rows[i].Cells.FromKey("Status").Text.Trim();
                this.UltraWebGridLeaveApply.Rows[i].Cells.FromKey("WorkNo").TargetURL = "javascript:ShowBillDetail('" + this.UltraWebGridLeaveApply.Rows[i].Cells.FromKey("ID").Text + "')";
                string temp_Status= sStatus;
                if (temp_Status == null)
                {
                    goto Label_01B5;
                }
                if (!(temp_Status == "0"))
                {
                    if (temp_Status== "1")
                    {
                        goto Label_0116;
                    }
                    if (temp_Status == "3")
                    {
                        goto Label_014B;
                    }
                    if (temp_Status == "4")
                    {
                        goto Label_0180;
                    }
                    goto Label_01B5;
                }
                this.UltraWebGridLeaveApply.Rows[i].Cells.FromKey("StatusName").Style.ForeColor = Color.Green;
                continue;
            Label_0116:
                this.UltraWebGridLeaveApply.Rows[i].Cells.FromKey("StatusName").Style.ForeColor = Color.Blue;
                continue;
            Label_014B:
                this.UltraWebGridLeaveApply.Rows[i].Cells.FromKey("StatusName").Style.ForeColor = Color.Maroon;
                continue;
            Label_0180:
                this.UltraWebGridLeaveApply.Rows[i].Cells.FromKey("StatusName").Style.ForeColor = Color.Red;
                continue;
            Label_01B5:
                this.UltraWebGridLeaveApply.Rows[i].Cells.FromKey("StatusName").Style.ForeColor = Color.Black;
            }
        }
        #region 分頁
        protected void pager_PageChanged(object sender, EventArgs e)
        {
            this.Query(true, "Goto");
        }
        #endregion

    }
}
