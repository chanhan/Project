/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： CompanyAssignForm
 * 檔功能描述： 關聯公司設定UI
 * 
 * 版本：1.0
 * 創建標識： 顧陳偉 2011.12.7
 * 
 */

using System;
using System.Data;
using System.Web.UI;
using GDSBG.MiABU.Attendance.BLL.SystemManage.SystemSafety;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;
using Infragistics.WebUI.UltraWebNavigator;
using Resources;

namespace GDSBG.MiABU.Attendance.Web.SystemManage.SystemSafety
{
    public partial class CompanyAssignForm : BasePage
    {
        CompanyAssignBll companyAssignBll = new CompanyAssignBll();
        static SynclogModel logmodel = new SynclogModel();
        string persconCode = "";
        string createUser = "";
        #region 頁面加載
        protected void Page_Load(object sender, EventArgs e)
        {
            persconCode = base.Request.QueryString["PersonCode"];
            createUser = base.CurrentUserInfo.Personcode;
            if (!base.IsPostBack)
            {
                logmodel.ProcessOwner = CurrentUserInfo.Personcode;
                logmodel.TransactionType = Request.QueryString["ModuleCode"] == null ? "" : Request.QueryString["ModuleCode"].ToString();
                logmodel.LevelNo = "2";
                logmodel.FromHost = Request.UserHostAddress;

                if (!string.IsNullOrEmpty(persconCode))
                {
                    QueryData();
                }
            }
        }
        #endregion

        #region 存儲
        protected void btnSave_Click(object sender, EventArgs e)
        {
            logmodel.ProcessFlag = "insert";
            string companyList = "";
            bool flag = false;
            string alert = "";
            foreach (Node node in this.UltraWebTreeCompany.CheckedNodes)
            {
                companyList = companyList + node.Tag.ToString() + "§";
            }
            flag = companyAssignBll.SavePersonCompany(persconCode, companyList, createUser,logmodel);
            if (flag == true)
            {
                alert = "alert('" + Message.SaveSuccess + "')";
            }
            else
            {
                alert = "alert('" + Message.SaveFailed + "')";
            }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "SavePersonCompany", alert, true);
            this.QueryData();

        }
        #endregion

        #region 關聯公司數據
        private void QueryData()
        {
            this.UltraWebTreeCompany.Nodes.Clear();          
            DataTable dtPc = companyAssignBll.GetPersonCompany(persconCode);
            foreach (DataRow row in dtPc.Rows)
            {
                this.UltraWebTreeCompany.Nodes.Add(base.CreateNode(Convert.ToString(row["companyid"]), row["companyname"].ToString(), true, true));
            }
            DataTable dtOc = companyAssignBll.GetOtherAllCompany(persconCode);
            foreach (DataRow row in dtOc.Rows)
            {
                this.UltraWebTreeCompany.Nodes.Add(base.CreateNode(Convert.ToString(row["companyid"]), row["companyname"].ToString(), false, true));
            }
            this.UltraWebTreeCompany.ExpandAll();
        }
        #endregion
    }
}
