using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Infragistics.WebUI.UltraWebGrid;
using GDSBG.MiABU.Attendance.BLL.WorkFlow;
using System.Data;
using Resources;

namespace GDSBG.MiABU.Attendance.Web.WorkFlow
{
    public partial class WorkFlowDayset : BasePage
    {
        WorkFlowSetBll bll = new WorkFlowSetBll();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                hf_show.Value = Request.QueryString["Type"];
                string dept = Request.QueryString["DeptId"];
                BindDaysType(dept, "LeaveDayType", this.UltraWebGridAudit);
                BindDaysType(dept, "OutType", this.UltraWebGridAudit1);
                BindDaysType1(dept, "ShiweiType", this.UltraWebGridAudit2);
                BindDaysType1(dept, "GlzType", this.UltraWebGridAudit3);
                DataTable sw=bll.GetShiwei();
                DataTable zw=bll.GetGlz();
                BindDropDownList(sw, ddl_shiwei_start, "OC_NAME", "OC_CODE");
                BindDropDownList(sw, ddl_shiwei_end, "OC_NAME", "OC_CODE");
                BindDropDownList(zw, ddl_glz_start, "G_NAME", "G_CODE");
                BindDropDownList(zw, ddl_glz_end, "G_NAME", "G_CODE");
            }
        }


        private void BindDaysType(string deptid, string daytype, UltraWebGrid grid)
        {
            DataTable dt = bll.GetDaysType(deptid, daytype);
            grid.DataSource = dt;
            grid.DataBind();
        }

        private void BindDaysType1(string deptid, string daytype, UltraWebGrid grid)
        {
            DataTable dt = bll.GetDaysType_1(deptid, daytype);
            grid.DataSource = dt;
            grid.DataBind();
        }

        protected void btn_add_Click(object sender, EventArgs e)
        {
            int numstart = Convert.ToInt32(tb_days_start.Text.Trim());
            int numend = Convert.ToInt32(tb_days_end.Text.Trim());
            string dept = Request.QueryString["DeptId"];
            DataTable dt = bll.GetDaysType(dept, "LeaveDayType");
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    int i = Convert.ToInt32(dr["day_min"].ToString() == "" ? "0" : dr["day_min"].ToString());
                    int j = Convert.ToInt32(dr["day_max"].ToString() == "" ? "0" : dr["day_max"].ToString());
                    if ((i > numstart && i < numend) || (j > numstart && j < numend) || (numstart > i && numstart < j) || (numend > i && numend < j))
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('輸入的值在類別中存在相同區間！');</script>");
                        return;
                    }
                    else
                    {
                        if (i == numstart && j == numend)
                        {
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('輸入的值在類別中存在相同區間！');</script>");
                            return;
                        }
                    }
                }
            }
            string Dayscode = "Day" + numstart + "_" + numend;
            string DaysName=numstart+"-"+numend+"天";
            if (bll.InsertDayType(dept, Dayscode, DaysName, numstart, numend, "LeaveDayType"))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('" + Message.AddSuccess + "！');</script>");
                BindDaysType(dept, "LeaveDayType", this.UltraWebGridAudit);
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('" + Message.AddFailed + "！');</script>");
            }
        }

        protected void btn_delete_1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.UltraWebGridAudit.Rows.Count; i++)
            {
                if (this.UltraWebGridAudit.Rows[i].Selected)
                {
                    string dept = Request.QueryString["DeptId"];                    
                    string daycode= this.UltraWebGridAudit.Rows[i].Cells[0].ToString();
                    if (bll.DeleteDayType(dept, daycode, "LeaveDayType"))
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('" + Message.DeleteSuccess + "！');</script>");
                        BindDaysType(dept, "LeaveDayType", this.UltraWebGridAudit);
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('" + Message.DeleteFailed + "！');</script>");
                    }


                    break;
                }
            }
        }

        protected void btn_add_2_Click(object sender, EventArgs e)
        {
            int numstart = Convert.ToInt32(tb_out_start.Text.Trim());
            int numend = Convert.ToInt32(tb_out_end.Text.Trim());
            string dept = Request.QueryString["DeptId"];
            DataTable dt = bll.GetDaysType(dept, "OutType");
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    int i = Convert.ToInt32(dr["day_min"].ToString() == "" ? "0" : dr["day_min"].ToString());
                    int j = Convert.ToInt32(dr["day_max"].ToString() == "" ? "0" : dr["day_max"].ToString());
                    if ((i > numstart && i < numend) || (j > numstart && j < numend) || (numstart > i && numstart < j) || (numend > i && numend < j))
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('輸入的值在類別中存在相同區間！');</script>");
                        return;
                    }
                    else
                    {
                        if (i == numstart && j == numend)
                        {
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('輸入的值在類別中存在相同區間！');</script>");
                            return;
                        }
                    }
                }
            }
            string Dayscode = "Out" + numstart + "_" + numend;
            string DaysName = numstart + "-" + numend + "天";
            if (bll.InsertDayType(dept, Dayscode, DaysName, numstart, numend, "OutType"))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('" + Message.AddSuccess + "！');</script>");
                BindDaysType(dept, "OutType", this.UltraWebGridAudit1);
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('" + Message.AddFailed + "！');</script>");
            }
        }

        protected void btn_delete_2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.UltraWebGridAudit1.Rows.Count; i++)
            {
                if (this.UltraWebGridAudit1.Rows[i].Selected)
                {
                    string dept = Request.QueryString["DeptId"];
                    string daycode = this.UltraWebGridAudit1.Rows[i].Cells[0].ToString();
                    if (bll.DeleteDayType(dept, daycode, "OutType"))
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('" + Message.DeleteSuccess + "！');</script>");
                        BindDaysType(dept, "OutType", this.UltraWebGridAudit1);
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('" + Message.DeleteFailed + "！');</script>");
                    }
                    break;
                }
            }
        }

        protected void btn_add_3_Click(object sender, EventArgs e)
        {
            int numstart = Convert.ToInt32(ddl_shiwei_start.SelectedValue.Trim());
            int numend = Convert.ToInt32(ddl_shiwei_end.SelectedValue.Trim());
            string dept = Request.QueryString["DeptId"];
            DataTable dt = bll.GetDaysType(dept, "ShiweiType");
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    int i = Convert.ToInt32(dr["day_min"].ToString() == "" ? "0" : dr["day_min"].ToString());
                    int j = Convert.ToInt32(dr["day_max"].ToString() == "" ? "0" : dr["day_max"].ToString());
                    if ((i >= numstart && i <= numend) || (j >= numstart && j <= numend) || (numstart >= i && numstart <= j) || (numend >= i && numend <= j))
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('輸入的值在類別中存在相同區間！');</script>");
                        return;
                    }
                }
            }
            string Dayscode = "Shiwei" + numstart + "_" + numend;
            string DaysName = ddl_shiwei_start.SelectedItem.Text + "-" + ddl_shiwei_end.SelectedItem.Text;
            if (bll.InsertDayType(dept, Dayscode, DaysName, numstart, numend, "ShiweiType"))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('" + Message.AddSuccess + "！');</script>");
                BindDaysType1(dept, "ShiweiType", this.UltraWebGridAudit2);
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('" + Message.AddFailed + "！');</script>");
            }
        }

        protected void btn_delete_3_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.UltraWebGridAudit2.Rows.Count; i++)
            {
                if (this.UltraWebGridAudit1.Rows[i].Selected)
                {
                    string dept = Request.QueryString["DeptId"];
                    string daycode = this.UltraWebGridAudit1.Rows[i].Cells[0].ToString();
                    if (bll.DeleteDayType(dept, daycode, "ShiweiType"))
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('" + Message.DeleteSuccess + "！');</script>");
                        BindDaysType1(dept, "ShiweiType", this.UltraWebGridAudit2);
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('" + Message.DeleteFailed + "！');</script>");
                    }


                    break;
                }
            }
        }

        protected void btn_add_4_Click(object sender, EventArgs e)
        {
            int numstart = Convert.ToInt32(ddl_glz_start.SelectedValue.Trim());
            int numend = Convert.ToInt32(ddl_glz_end.SelectedValue.Trim());
            string dept = Request.QueryString["DeptId"];
            DataTable dt = bll.GetDaysType(dept, "GlzType");
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    int i = Convert.ToInt32(dr["day_min"].ToString() == "" ? "0" : dr["day_min"].ToString());
                    int j = Convert.ToInt32(dr["day_max"].ToString() == "" ? "0" : dr["day_max"].ToString());
                    if ((i >= numstart && i <= numend) || (j >= numstart && j <= numend) || (numstart >= i && numstart <= j) || (numend >= i && numend <= j))
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('輸入的值在類別中存在相同區間！');</script>");
                        return;
                    }
                }
            }
            string Dayscode = "Glz" + numstart + "_" + numend;
            string DaysName = ddl_glz_start.SelectedItem.Text + "-" + ddl_glz_end.SelectedItem.Text;
            if (bll.InsertDayType(dept, Dayscode, DaysName, numstart, numend, "GlzType"))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('" + Message.AddSuccess + "！');</script>");
                BindDaysType1(dept, "GlzType", this.UltraWebGridAudit3);
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('" + Message.AddFailed + "！');</script>");
            }
        }

        protected void btn_delete_4_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.UltraWebGridAudit3.Rows.Count; i++)
            {
                if (this.UltraWebGridAudit1.Rows[i].Selected)
                {
                    string dept = Request.QueryString["DeptId"];
                    string daycode = this.UltraWebGridAudit3.Rows[i].Cells[0].ToString();
                    if (bll.DeleteDayType(dept, daycode, "GlzType"))
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('" + Message.DeleteSuccess + "！');</script>");
                        BindDaysType1(dept, "GlzType", this.UltraWebGridAudit3);
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('" + Message.DeleteFailed + "！');</script>");
                    }


                    break;
                }
            }
        }

        private void BindDropDownList(DataTable dt, DropDownList ddl,string colname,string colvalue)
        {
            ddl.DataSource = dt;
            ddl.DataTextField = colname;
            ddl.DataValueField = colvalue;
            ddl.Items.Insert(0, new ListItem("---請選擇---", ""));
            ddl.DataBind();
        }
    }
}
