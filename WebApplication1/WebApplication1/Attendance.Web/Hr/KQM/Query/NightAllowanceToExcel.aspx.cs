/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： NightAllowanceToExcel.cs
 * 檔功能描述： 夜宵補助UI層
 * 
 * 版本：1.0
 * 創建標識： 高子焱 2012.01.09
 * 
 */
using System;
using System.Data;
using GDSBG.MiABU.Attendance.BLL.KQM.Query;
using GDSBG.MiABU.Attendance.Model.KQM;

namespace GDSBG.MiABU.Attendance.Web.Hr.KQM.Query
{
    public partial class NightAllowanceToExcel : BasePage
    {
        public string dateFrom;
        public string dateTo;
        public string workNo;
        public string deptCode;
        public string localName;
        DataTable tempDataTable = new DataTable();
        NightAllowanceToExcelModel model = new NightAllowanceToExcelModel();
        NightAllowanceToExcelBll bll = new NightAllowanceToExcelBll();
        public int getDays()
        {
            return (DateTime.Parse(this.dateTo).Subtract(DateTime.Parse(this.dateFrom)).Days + 1);
        }

        private string GetWeek(DateTime SelectYearMonth)
        {
            string getWeek = SelectYearMonth.DayOfWeek.ToString();
            switch (getWeek)
            {
                case "Sunday":
                    return Resources.ControlText.sunday;
                case "Monday":
                    return Resources.ControlText.monday;

                case "Tuesday":
                    return Resources.ControlText.tuesday;

                case "Wednesday":
                    return Resources.ControlText.wednesday; ;

                case "Thursday":
                    return Resources.ControlText.thursday;

                case "Friday":
                    return Resources.ControlText.friday;
                case "Saturday":
                    return Resources.ControlText.saturday;
            }
            return "";
        }

        public bool isHoliday(DateTime date)
        {
            bool bValue = false;
            switch (Convert.ToInt32(date.DayOfWeek))
            {
                case 0:
                case 6:
                    bValue = true;
                    break;
            }
            return bValue;
        }
        public void SetExcel()
        {
            if (tempDataTable != null)
            {

                for (int i = 0; i < tempDataTable.Rows.Count; i++)
                {
                    base.Response.Write("<tr style='font-size: 10pt'>");
                    if (tempDataTable.Rows[i]["num"].ToString().Length > 0)
                    {
                        base.Response.Write("<td width='1.5%' bgcolor='#ff69b4' align='center'>" + tempDataTable.Rows[i]["num"].ToString() + "</td>");
                    }
                    if (tempDataTable.Rows[i]["DName"].ToString().Length > 0)
                    {
                        base.Response.Write("<td width='1.5%' bgcolor='#ff69b4' align='center'>" + tempDataTable.Rows[i]["DName"].ToString() + "</td>");
                    }
                    else
                    {
                        base.Response.Write("<td width='1.5%' bgcolor='#ff69b4' align='center'>" + "" + "</td>");
                    }
                    if (tempDataTable.Rows[i]["WorkNo"].ToString().Length > 0)
                    {
                        base.Response.Write("<td width='1.5%' bgcolor='#ff69b4' align='center'>" + tempDataTable.Rows[i]["WorkNo"].ToString() + "</td>");
                    }
                    else 
                    {
                        base.Response.Write("<td width='1.5%' bgcolor='#ff69b4' align='center'>" + "" + "</td>");
                    }
                    if (tempDataTable.Rows[i]["LocalName"].ToString().Length > 0)
                    {
                        base.Response.Write("<td width='1.5%' bgcolor='#ff69b4' align='center'>" + tempDataTable.Rows[i]["LocalName"].ToString() + "</td>");
                    }
                    else
                    {
                        base.Response.Write("<td width='1.5%' bgcolor='#ff69b4' align='center'>" + ""+ "</td>");
                    }
                    if (tempDataTable.Rows[i]["Allowance"].ToString().Length > 0)
                    {
                        base.Response.Write("<td width='1.5%' bgcolor='#ff69b4' align='center'>" + tempDataTable.Rows[i]["Allowance"].ToString() + "</td>");
                    }
                    else
                    {
                        base.Response.Write("<td width='1.5%' bgcolor='#ff69b4' align='center'>" + ""+ "</td>");
                    }
                    SetDayData(tempDataTable.Rows[i]["WorkNo"].ToString());
                    base.Response.Write("</tr>");
                }
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            this.dateFrom = Request.QueryString["dateFrom"];
            this.dateTo = Request.QueryString["dateTo"];
            model.WorkNo = Request.QueryString["workNo"];
            string depCode = Request.QueryString["depCode"];
            model.LocalName = Request.QueryString["localName"];
            string SqlDep = "SELECT aa.depcode FROM gds_sc_persondept aa WHERE aa.personcode = '" + CurrentUserInfo.Personcode + "' AND aa.modulecode = '" + Request.QueryString["moduleCode"] + "' AND EXISTS (SELECT 1 FROM gds_sc_personcompany bb WHERE  bb.personcode='" + CurrentUserInfo.Personcode + "'AND aa.companyid = bb.companyid)";
            string status = "1";
            tempDataTable = bll.GetNightExcel(model, dateFrom, dateTo, deptCode, status, SqlDep);
            base.Response.ContentType = "application/vnd.ms-excel";
            base.Response.AppendHeader("content-disposition", "attachment; filename=Allowance.xls");
        }

        public void SetDayData(string workNo)
        {
            this.dateFrom = Request.QueryString["dateFrom"];
            this.dateTo = Request.QueryString["dateTo"];
            int days = this.getDays();
            bool bValue = false;
            DataTable tempDataTable = bll.GetNightExcelStatus0(dateFrom, dateTo, workNo);
            for (int i = 0; i < days; i++)
            {
                DateTime date = Convert.ToDateTime(this.dateFrom).AddDays((double)i);
                bValue = false;
                for (int j = 0; j < tempDataTable.Rows.Count; j++)
                {
                    if (date.CompareTo(Convert.ToDateTime(tempDataTable.Rows[j]["KQDate"])) == 0)
                    {
                        if (this.isHoliday(date))
                        {
                            base.Response.Write("<td width='1.5%' bgcolor='#ff69b4' align='center'>1</td>");
                        }
                        else
                        {
                            base.Response.Write("<td width='1.5%' align='center'>1</td>");
                        }
                        bValue = true;
                        break;
                    }
                }
                if (!bValue)
                {
                    if (this.isHoliday(date))
                    {
                        base.Response.Write("<td width='1.5%' bgcolor='#ff69b4' align='center'>0</td>");
                    }
                    else
                    {
                        base.Response.Write("<td width='1.5%' align='center'>0</td>");
                    }
                }
            }
        }

        public void SetDays()
        {
            int days = this.getDays();
            for (int i = 1; i <= days; i++)
            {
                DateTime date = Convert.ToDateTime(this.dateFrom).AddDays((double)(i - 1));
                if (this.isHoliday(date))
                {
                    base.Response.Write("<td width='1.5%' bgcolor='#ff69b4' align=center><font color=#f0f8ff><b>" + date.ToString("M/d").Split(new char[] { '/' })[1] + "</b></font></td>");
                }
                else
                {
                    base.Response.Write("<td width='1.5%' bgcolor='#000080' align=center><font color=#f0f8ff><b>" + date.ToString("M/d").Split(new char[] { '/' })[1] + "</b></font></td>");
                }
            }
        }

        public void SetWeek()
        {
            int days = this.getDays();
            for (int i = 0; i < days; i++)
            {
                DateTime date = Convert.ToDateTime(this.dateFrom).AddDays((double)i);
                if (this.isHoliday(date))
                {
                    base.Response.Write("<td width='1.5%' bgcolor='#ff69b4' align=center><font color=#f0f8ff><b>" + this.GetWeek(date) + "</b></font></td>");
                }
                else
                {
                    base.Response.Write("<td width='1.5%' bgcolor='#000080' align=center><font color=#f0f8ff><b>" + this.GetWeek(date) + "</b></font></td>");
                }
            }
        }
    }
}
