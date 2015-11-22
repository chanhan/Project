using System;
using System.Data;
using GDSBG.MiABU.Attendance.BLL.SystemManage.SystemSafety;

/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： CompanySelector.aspx.cs
 * 檔功能描述： 公司放大鏡
 * 
 * 版本：1.0
 * 創建標識： 昝望 2011.12.21
 * 
 */

namespace GDSBG.MiABU.Attendance.Web.SystemManage.SystemSafety
{
    public partial class CompanySelector : BasePage
    {

        CompanyAssignBll companyAssignBll = new CompanyAssignBll();
        static string personcode;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                personcode=Request.QueryString["personcode"];
                BindData();
            }
        }


        private void BindData()
        {

            DataTable dt = companyAssignBll.GetAllCompany(personcode);
            this.UltraWebGridCompany.DataSource = dt;
            this.UltraWebGridCompany.DataBind();
        }
    }
}
