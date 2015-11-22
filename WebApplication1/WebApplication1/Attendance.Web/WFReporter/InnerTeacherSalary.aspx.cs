/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： InnerTeacherSalary.aspx.cs
 * 檔功能描述： 內部講師費UI層
 * 
 * 版本：1.0
 * 創建標識： 張明強 2012.03.19
 * 
 */
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace GDSBG.MiABU.Attendance.Web.WFReporter
{
    public partial class InnerTeacherSalary : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnQuery_Click(object sender, EventArgs e)
        {

        }

        protected void pager_PageChanged(object sender, EventArgs e)
        {
            //DataBind();
        }

        #region WebGrid綁定
        protected void UltraWebGrid_DataBound(object sender, EventArgs e)
        {
            
        }
        #endregion
    }
}
