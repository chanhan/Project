/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： OrgEmployeeBll.cs
 * 檔功能描述： 人員編組功能模組子頁面查詢所選組織下的子階組織或所屬組級、線級、段級的所有組織UI類
 * 
 * 版本：1.0
 * 創建標識： 陈函 2011.12.16
 * 
 */
using System;
using System.Data;
using GDSBG.MiABU.Attendance.BLL.HRM;

namespace GDSBG.MiABU.Attendance.Web.HRM
{
    public partial class SingleDataPickForm : BasePage
    {
        OrgEmployeeBll orgEmployeeBll = new OrgEmployeeBll();
        protected void Page_Load(object sender, EventArgs e)
        {
            string personCode = CurrentUserInfo.Personcode;
            string companyId = CurrentUserInfo.CompanyId;
            string parentDep = Request.QueryString["ParentDep"];
            string moduleCode = Request.QueryString["moduleCode"];
            DataTable dt = orgEmployeeBll.GetAuthorizedDept(personCode, companyId, moduleCode, parentDep);
            UltraWebGridOtherData.DataSource = dt;
           UltraWebGridOtherData.DataBind();
        }
    }
}
