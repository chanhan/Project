using System;
using GDSBG.MiABU.Attendance.BLL.SystemManage.SystemSafety;

/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： DepLevelSelector.aspx.cs
 * 檔功能描述： 層級放大鏡
 * 
 * 版本：1.0
 * 創建標識： 昝望 2011.12.21
 * 
 */

namespace GDSBG.MiABU.Attendance.Web.SystemManage.SystemSafety
{
    public partial class DepLevelSelector : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            PersonBll bll = new PersonBll();
            this.UltraWebGridDepLevel.DataSource = bll.selectDepLevel();
            UltraWebGridDepLevel.DataBind();
        }
    }
}
