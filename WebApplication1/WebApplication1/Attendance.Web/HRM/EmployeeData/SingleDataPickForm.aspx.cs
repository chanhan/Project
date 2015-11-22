/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： SingleDataPickForm.cs
 * 檔功能描述：加班類別異動功能模組子頁面加班類別查詢UI類
 * 
 * 版本：1.0
 * 創建標識： 陈函 2011.12.23
 * 
 */
using System;
using System.Data;
using GDSBG.MiABU.Attendance.BLL.HRM.EmployeeData;

namespace GDSBG.MiABU.Attendance.Web.HRM.EmployeeData
{
    public partial class SingleDataPickForm : BasePage
    {
        HrmEmpOtherMoveBll hrmEmpOtherMoveBll = new HrmEmpOtherMoveBll();
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
            dt = hrmEmpOtherMoveBll.GetDataByCondition(txtCode.Text.Trim(), txtName.Text.Trim(), HiddenDataType.Value);
            UltraWebGridOtherData.DataSource = dt;
            UltraWebGridOtherData.DataBind();
        }
    }
}
