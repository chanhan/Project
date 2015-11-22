/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： AreaSelector.cs
 * 檔功能描述： 區域放大鏡頁面
 * 
 * 版本：1.0
 * 創建標識： 顧陳偉 2012.02.04
 * 
 */
using System;
using System.Data;
using GDSBG.MiABU.Attendance.BLL.HRM;

namespace GDSBG.MiABU.Attendance.Web.HRM
{
    public partial class AreaSelector : BasePage
    {
        DepartmentBll bllDepartment = new DepartmentBll();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                DataTable dt = bllDepartment.GetAreaInfo();
                UltraWebGridOtherData.DataSource = dt;
                UltraWebGridOtherData.DataBind();
            }
        }
    }
}
