/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： SingleDataPickForm
 * 檔功能描述： 支援人力部門放大鏡
 * 
 * 版本：1.0
 * 創建標識： 顧陳偉 2012.01.30
 * 
 */
using System;
using System.Data;
using GDSBG.MiABU.Attendance.BLL.HRM.Support;

namespace GDSBG.MiABU.Attendance.Web.HRM.Support
{
    public partial class SingleDataPickForm : BasePage
    {
        EmpSupportInBll empSupportInBll = new EmpSupportInBll();
        protected void Page_Load(object sender, EventArgs e)
        {
            base.Response.Expires = -1;
            if (!base.IsPostBack)
            {
                this.Query();
            }
        }
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            this.Query();
        }
        private void Query()
        {
            DataTable dt = new DataTable();
            string modulecode = base.Request["ModuleCode"];
            string sql = base.SqlDep;
            dt = empSupportInBll.GetDataByCondition(modulecode, sql, txtDeptName.Text.Trim(),txtCostCode.Text.Trim());
            UltraWebGridOtherData.DataSource = dt;
            UltraWebGridOtherData.DataBind();
        }
    }
}
