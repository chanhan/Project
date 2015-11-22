
/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： CommonFrame.aspx.cs
 * 檔功能描述： 發佈使用的公共框架
 * 
 * 版本：1.0
 * 創建標識： 張明強 2011.12.20
 * 
 */
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using GDSBG.MiABU.Attendance.BLL.SystemManage.SystemSafety;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;
using System.Collections.Generic;
using GDSBG.MiABU.Attendance.Common;

namespace GDSBG.MiABU.Attendance.Web
{
    public partial class CommonFrame : System.Web.UI.Page
    {
        ModuleBll bll = new ModuleBll();
        ModuleModel model = new ModuleModel();
        string url = "";
        string moduleCode = "";

        #region PageLoad方法-根據參數FunId獲取所需跳轉的頁面地址
        /// <summary>
        /// 根據參數FunId獲取所需跳轉的頁面地址
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            //model.FunctionId = (base.Request.QueryString["FunId"] == null) ? "" : base.Request.QueryString["FunId"].ToString().Trim();
            ////model.FunctionId = "1341";
            //DataTable tempTable = new DataTable();
            //tempTable = bll.GetModuleByFunId(model);
            //if (tempTable != null)
            //{
            //    url = tempTable.Rows[0]["URL"].ToString().Trim();
            //    moduleCode = tempTable.Rows[0]["MODULECODE"].ToString().Trim();
            //}

            model.ModuleCode = (base.Request.QueryString["ModuleCode"] == null) ? "" : base.Request.QueryString["ModuleCode"].ToString().Trim();
            //model.FunctionId = "1341";
            DataTable tempTable = new DataTable();
            tempTable = bll.GetModuleByFunId(model);
            if (tempTable != null)
            {
                if (tempTable.Rows.Count > 0)
                {
                    url = tempTable.Rows[0]["URL"].ToString().Trim();
                }
            }
        }
        #endregion

        #region 返回頁面地址給iframe的src
        //返回頁面地址給iframe的src
        public string Address()
        {
            if (url.Length > 1)
            {
                string userId = (base.Request.QueryString["UserId"] == null) ? "" : base.Request.QueryString["UserId"].ToString().Trim();
                string moduleCode = (base.Request.QueryString["ModuleCode"] == null) ? "" : base.Request.QueryString["ModuleCode"].ToString().Trim();

                //從人事端口登錄時，取得其傳出的UserId，填充Session，防止在部份電腦中出現頁面跳出的問題。
                if (!string.IsNullOrEmpty(userId))
                {
                    PersonBll personBll = new PersonBll();
                    List<PersonModel> user = personBll.GetPersonUserId(userId);
                    if (user == null || user.Count < 1)
                    {
                        Response.Redirect("http://10.138.4.205:100");
                    }
                    else
                    {
                        Session[GlobalData.UserInfoSessionKey] = user[0];//填充用戶Session
                    }
                }
                //url = url.Substring(1) + "?ModuleCode="+moduleCode;
                url = url.Substring(1) + "?UserId=" + userId + "&ModuleCode=" + moduleCode;
                //url = url + "?UserId=ADMIN";
            }
            return url;
        }
        #endregion
    }
}
