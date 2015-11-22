/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： AuthorityForm.cs
 * 檔功能描述： 角色功能模組子頁面編輯角色權限UI類
 * 
 * 版本：1.0
 * 創建標識： 陈函 2011.12.7
 * 
 */
using System;
using System.Data;
using GDSBG.MiABU.Attendance.BLL.SystemManage.SystemSafety;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;

namespace GDSBG.MiABU.Attendance.Web.SystemManage.SystemSafety
{
    public partial class AuthorityDetailForm : BasePage
    {
        ModuleBll moduleBll = new ModuleBll();
        AuthorityBll authorityBll = new AuthorityBll();
        private string roleCode
        {
            get
            {
                if (ViewState["roleCode"] == null)
                {
                    return "";
                }
                else
                {
                    return (string)ViewState["roleCode"];
                }
            }
            set
            {
                ViewState["roleCode"] = value;
            }
        }

        private bool roleCheck
        {
            get
            {
                if (ViewState["roleCheck"] == null)
                {
                    return false;
                }
                else
                {
                    return (bool)ViewState["roleCheck"];
                }
            }
            set
            {
                ViewState["roleCheck"] = value;
            }
        }
        string moduleCode = "";
        string functionList = "";
        protected void Page_Load(object sender, EventArgs e)
        {
                if (Request.QueryString["RoleCode"] != null && Request.QueryString["ModuleCode"] != null && Request.QueryString["RoleCheck"] != null && Request.QueryString["functionList"] != null)
                {
                    roleCode = Request.QueryString["RoleCode"].ToString();
                    moduleCode = Request.QueryString["ModuleCode"].ToString();
                    roleCheck = Request.QueryString["RoleCheck"].ToString() == "true";
                    functionList = Request.QueryString["functionList"].ToString();
                    txtModuleCode.Text = moduleCode;
                    AuthorityModel authorityModel = new AuthorityModel();
                    authorityModel.ModuleCode = moduleCode;
                    authorityModel.RoleCode = roleCode;
                    DataTable dtAuthority = authorityBll.GetAuthority(authorityModel);
                    ModuleModel moduleModel = new ModuleModel();
                    moduleModel.ModuleCode = moduleCode;
                    DataTable dtModule = moduleBll.GetModule(moduleModel);
                    txtAllFunctionList.Text = functionList;

                    txtFunctionDesc.Text = dtModule.Rows.Count != 0 ? dtModule.Rows[0]["DESCRIPTION"].ToString() : "";

                    txtFunctionList.Text = dtAuthority.Rows.Count != 0 ? dtAuthority.Rows[0]["FUNCTIONLIST"].ToString() : "";

                }
        }
    }
}
