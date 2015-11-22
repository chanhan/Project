using System;
using System.Data;
using System.Web.Profile;
using System.Web.UI;
using System.Web.UI.WebControls;
using GDSBG.MiABU.Attendance.BLL.KQM.BasicData;
using GDSBG.MiABU.Attendance.Model.KQM.BasicData;
using Resources;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;

/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： KQMBGCalendarForm.aspx.cs
 * 檔功能描述： BG行事歷
 * 
 * 版本：1.0
 * 創建標識：昝望 2011.12.21
 * 
 */


namespace GDSBG.MiABU.Attendance.Web.KQM.BasicData
{
    public partial class KQMBGCalendarForm : BasePage
    {
        BGCalendarBll bll = new BGCalendarBll();
        static SynclogModel logmodel = new SynclogModel();

        private void BindList()
        {
            int i;
            this.ddlYear.Items.Clear();
            int year = DateTime.Today.Year;
            int iYear = 0;
            for (i = -10; i < 11; i++)
            {
                iYear = year + i;
                this.ddlYear.Items.Add(iYear.ToString());
            }
            this.ddlMonth.Items.Clear();
            for (i = 1; i < 13; i++)
            {
                this.ddlMonth.Items.Add(i.ToString());
            }
            DataTable dt = bll.GetDepartment();
            if (dt != null && dt.Rows.Count > 0)
            {
                ddlBG.DataSource = dt;
                ddlBG.DataTextField = "depname";
                ddlBG.DataValueField = "depcode";
                ddlBG.DataBind();
            }
        }

        protected void ButtonModify_Click(object sender, EventArgs e)
        {
            if (this.ddlBG.SelectedValue.Trim() == "")
            {
                base.Response.Write("<script>alert('請選擇BG');</script>");
            }
            else
            {
                logmodel.ProcessFlag = "update";
                int wDay = this.getWeekDay() + 1;
                int days = DateTime.DaysInMonth(Convert.ToInt32(this.ddlYear.SelectedValue), Convert.ToInt32(this.ddlMonth.SelectedValue));
                for (int i = wDay; i < (wDay + days); i++)
                {
                    DropDownList dlstWorkFlag = (DropDownList)this.FindControl("ddlWorkFlag" + i.ToString());
                    dlstWorkFlag.Enabled = true;
                    dlstWorkFlag.Visible = true;
                    DropDownList dlstHolidayFlag = (DropDownList)this.FindControl("ddlHolidayFlag" + i.ToString());
                    dlstHolidayFlag.Enabled = true;
                    dlstHolidayFlag.Visible = true;
                    TextBox txt = (TextBox)this.FindControl("txtRemark" + i.ToString());
                    txt.Visible = true;
                    txt.ReadOnly = false;
                }
            }
        }

        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            BGCalendarModel model = new BGCalendarModel();
            int wDay = this.getWeekDay();
            bool succeed = false;
            int index = 0;
            int days = DateTime.DaysInMonth(Convert.ToInt32(this.ddlYear.SelectedValue), Convert.ToInt32(this.ddlMonth.SelectedValue));
            string wFlag = "";
            string remark = "";
            string sDate = "";
            string hFlag = "";
            string BG = this.ddlBG.SelectedValue.Trim();
            try
            {
                int i;
                DropDownList dlstWorkFlag;
                DropDownList dlstHolidayFlag;
                TextBox txt;
                for (i = 1; i < (days + 1); i++)
                {
                    sDate = this.ddlYear.SelectedValue + "/" + this.ddlMonth.SelectedValue + "/" + i.ToString();
                    DateTime workday = Convert.ToDateTime(sDate);
                    index = i + wDay;
                    dlstWorkFlag = (DropDownList)this.FindControl("ddlWorkFlag" + index.ToString());
                    wFlag = dlstWorkFlag.SelectedValue.Trim();
                    dlstHolidayFlag = (DropDownList)this.FindControl("ddlHolidayFlag" + index.ToString());
                    hFlag = dlstHolidayFlag.SelectedValue.Trim();
                    txt = (TextBox)this.FindControl("txtRemark" + index.ToString());
                    remark = txt.Text.Trim();
                    model.BgCode = this.ddlBG.SelectedValue.Trim();
                    model.WorkDay = workday;
                    model.WeekNo = this.GetWeekNo(sDate);
                    model.WeekDay = DateTime.Parse(sDate).DayOfWeek.ToString();
                    model.WorkFlag = wFlag;
                    model.HollDayFlag = hFlag;
                    model.Remark = remark;
                    model.UpdateUser = CurrentUserInfo.Personcode;
                    succeed = bll.UpdateBGCalendarByKey(model,logmodel);

                }
                for (i = wDay + 1; i < ((days + wDay) + 1); i++)
                {
                    dlstWorkFlag = (DropDownList)this.FindControl("ddlWorkFlag" + i.ToString());
                    dlstWorkFlag.Enabled = false;
                    dlstHolidayFlag = (DropDownList)this.FindControl("ddlHolidayFlag" + i.ToString());
                    dlstHolidayFlag.Enabled = false;
                    txt = (TextBox)this.FindControl("txtRemark" + i.ToString());
                    if (txt.Text.Trim() != "")
                    {
                        txt.ReadOnly = true;
                        txt.BorderStyle = BorderStyle.None;
                    }
                    else
                    {
                        txt.Visible = false;
                    }
                }
                if (succeed)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "isnumber", "alert('" + Message.UpdateSuccess + "');", true);
                    DataBind();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "isnumber", "alert('" + Message.UpdateFailed + "');", true);

                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "isnumber", "alert('" + Message.ErrorInfo + "');", true);

            }
        }

        protected void DropDownListBG_SelectedIndexChanged(object sender, EventArgs e)
        {
            int wDay = this.getWeekDay();
            int days = DateTime.DaysInMonth(Convert.ToInt32(this.ddlYear.SelectedValue), Convert.ToInt32(this.ddlMonth.SelectedValue));
            this.getData(wDay, days);
        }

        protected void DropDownListMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.setDate();
        }

        protected void DropDownListYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.setDate();
        }

        private void getData(int iWDay, int nowDays)
        {
            int index = 0;
            string sDate = "";
            string sql = "";
         
            for (int i = 1; i < (nowDays + 1); i++)
            {
                BGCalendarModel model = new BGCalendarModel();
                TextBox txt;
                sDate = this.ddlYear.SelectedValue + "/" + this.ddlMonth.SelectedValue + "/" + i.ToString();
                DateTime workday = Convert.ToDateTime(sDate);
                index = i + iWDay;
                model.WorkDay = workday;
                model.BgCode = ddlBG.SelectedValue;
                DataTable dt = bll.GetBGCalendar(model);
                if (dt.Rows.Count > 0)
                {
                    DropDownList dlstWorkFlag = (DropDownList)this.FindControl("ddlWorkFlag" + index.ToString());
                    dlstWorkFlag.SelectedIndex = dlstWorkFlag.Items.IndexOf(dlstWorkFlag.Items.FindByValue(dt.Rows[0]["WorkFlag"].ToString()));
                    DropDownList dlstHolidayFlag = (DropDownList)this.FindControl("ddlHolidayFlag" + index.ToString());
                    dlstHolidayFlag.SelectedIndex = dlstHolidayFlag.Items.IndexOf(dlstHolidayFlag.Items.FindByValue(dt.Rows[0]["HolidayFlag"].ToString()));
                    if (!dt.Rows[0]["Remark"].ToString().Trim().Equals(""))
                    {
                        txt = (TextBox)this.FindControl("txtRemark" + index.ToString());
                        txt.Visible = true;
                        txt.Text = dt.Rows[0]["Remark"].ToString();
                        txt.ReadOnly = true;
                    }
                    else
                    {
                        txt = (TextBox)this.FindControl("txtRemark" + index.ToString());
                        txt.Visible = false;
                        txt.Text = "";
                        txt.ReadOnly = true;
                    }
                }
                else
                {
                    txt = (TextBox)this.FindControl("txtRemark" + index.ToString());
                    txt.Visible = false;
                    txt.Text = "";
                    txt.ReadOnly = true;
                }
            }
        }

        private int getWeekDay()
        {
            return Convert.ToInt32(Convert.ToDateTime(this.ddlYear.SelectedValue + "/" + this.ddlMonth.SelectedValue + "/1").DayOfWeek);
        }

        private string GetWeekNo(string sDate)
        {
            DataTable dt = bll.GetValue(sDate);
            return dt.Rows[0]["workno"].ToString();
        }

        protected void ImageButtonNext_Click(object sender, ImageClickEventArgs e)
        {
            DateTime nowDate = Convert.ToDateTime(this.ddlYear.SelectedValue + "/" + this.ddlMonth.SelectedValue + "/1").AddMonths(1);
            this.ddlYear.SelectedValue = nowDate.Year.ToString();
            this.ddlMonth.SelectedValue = nowDate.Month.ToString();
            this.setDate();
        }

        protected void ImageButtonPrev_Click(object sender, ImageClickEventArgs e)
        {
            DateTime nowDate = Convert.ToDateTime(this.ddlYear.SelectedValue + "/" + this.ddlMonth.SelectedValue + "/1").AddMonths(-1);
            this.ddlYear.SelectedValue = nowDate.Year.ToString();
            this.ddlMonth.SelectedValue = nowDate.Month.ToString();
            this.setDate();
        }

        private void Internationalization()
        {
            for (int i = 1; i < 0x2b; i++)
            {
                Label lblWorkFlag = (Label)this.FindControl("lblWorkFlag" + i.ToString());
                lblWorkFlag.Text = Message.WorkFlag;
                Label lblHolidayFlag = (Label)this.FindControl("lblHolidayFlag" + i.ToString());
                lblHolidayFlag.Text = Message.HolidayFlag;
            }
            this.ButtonModify.Text ="修改";
            this.ButtonSave.Text = "存儲";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            PageHelper.ButtonControls(base.FuncList, pnlShowPanel.Controls, base.FuncListModule);
            if (!IsPostBack)
            {
                logmodel.ProcessOwner = CurrentUserInfo.Personcode;
                logmodel.TransactionType = Request.QueryString["ModuleCode"].ToString();
                logmodel.LevelNo = "2";
                logmodel.FromHost = Request.UserHostAddress;
                    this.BindList();
                    ddlYear.SelectedValue = DateTime.Today.Year.ToString();
                    ddlMonth.SelectedValue = DateTime.Today.Month.ToString();
                    setDate();
                    Internationalization();   
            }
        }

        private void setDate()
        {
            try
            {
                Label lbl;
                DropDownList dlstWorkFlag;
                DropDownList dlstHolidayFlag;
                TextBox txt;
                Label lblWorkFlag;
                Label lblHolidayFlag;
                int days = DateTime.DaysInMonth(Convert.ToInt32(this.ddlYear.SelectedValue), Convert.ToInt32(this.ddlMonth.SelectedValue));
                int iWeekDay = this.getWeekDay();
                this.getData(iWeekDay, days);
                iWeekDay++;
                if (((iWeekDay > 5) && (days > 30)) || ((iWeekDay > 6) && (days > 0x1d)))
                {
                    this.trLast.Attributes.Add("style", "display:true");
                }
                else
                {
                    this.trLast.Attributes.Add("style", "display:none");
                }
                int i = 1;
                while (i < 0x2b)
                {
                    lbl = (Label)this.FindControl("lblDay" + i.ToString());
                    lbl.Text = "";
                    i++;
                }
                int day = 1;
                for (i = iWeekDay; i < (days + iWeekDay); i++)
                {
                    lbl = (Label)this.FindControl("lblDay" + i.ToString());
                    lbl.Text = day.ToString();
                    day++;
                }
                for (i = 1; i < iWeekDay; i++)
                {
                    dlstWorkFlag = (DropDownList)this.FindControl("ddlWorkFlag" + i.ToString());
                    dlstWorkFlag.Visible = false;
                    dlstHolidayFlag = (DropDownList)this.FindControl("ddlHolidayFlag" + i.ToString());
                    dlstHolidayFlag.Visible = false;
                    txt = (TextBox)this.FindControl("txtRemark" + i.ToString());
                    txt.Visible = false;
                    lblWorkFlag = (Label)this.FindControl("lblWorkFlag" + i.ToString());
                    lblWorkFlag.Visible = false;
                    lblHolidayFlag = (Label)this.FindControl("lblHolidayFlag" + i.ToString());
                    lblHolidayFlag.Visible = false;
                }
                for (i = days + iWeekDay; i < 0x2b; i++)
                {
                    dlstWorkFlag = (DropDownList)this.FindControl("ddlWorkFlag" + i.ToString());
                    dlstWorkFlag.Visible = false;
                    dlstHolidayFlag = (DropDownList)this.FindControl("ddlHolidayFlag" + i.ToString());
                    dlstHolidayFlag.Visible = false;
                    txt = (TextBox)this.FindControl("txtRemark" + i.ToString());
                    txt.Visible = false;
                    lblWorkFlag = (Label)this.FindControl("lblWorkFlag" + i.ToString());
                    lblWorkFlag.Visible = false;
                    lblHolidayFlag = (Label)this.FindControl("lblHolidayFlag" + i.ToString());
                    lblHolidayFlag.Visible = false;
                }
                for (i = iWeekDay; i < (days + iWeekDay); i++)
                {
                    dlstWorkFlag = (DropDownList)this.FindControl("ddlWorkFlag" + i.ToString());
                    dlstWorkFlag.Enabled = false;
                    dlstWorkFlag.Visible = true;
                    dlstHolidayFlag = (DropDownList)this.FindControl("ddlHolidayFlag" + i.ToString());
                    dlstHolidayFlag.Enabled = false;
                    dlstHolidayFlag.Visible = true;
                    lblWorkFlag = (Label)this.FindControl("lblWorkFlag" + i.ToString());
                    lblWorkFlag.Visible = true;
                    lblHolidayFlag = (Label)this.FindControl("lblHolidayFlag" + i.ToString());
                    lblHolidayFlag.Visible = true;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "isnumber", "alert('" + Message.ErrorInfo + "');", true);
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
