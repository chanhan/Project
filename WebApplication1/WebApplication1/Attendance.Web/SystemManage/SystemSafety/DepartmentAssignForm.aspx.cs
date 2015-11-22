/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： DepartmentAssignForm
 * 檔功能描述： 關聯組織設定UI
 * 
 * 版本：1.0
 * 創建標識： 顧陳偉 2011.12.7
 * 
 */

using System;
using System.Collections;
using System.Data;
using System.Web.UI;
using GDSBG.MiABU.Attendance.BLL.SystemManage.SystemSafety;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;
using Infragistics.WebUI.UltraWebNavigator;
using Resources;

namespace GDSBG.MiABU.Attendance.Web.SystemManage.SystemSafety
{
    public partial class DepartmentAssignForm : BasePage
    {
        DepartmentAssignBll departmentAssignBll = new DepartmentAssignBll();
        static SynclogModel logmodel = new SynclogModel();
        string personCode = "";
        string rolesCode = "";
        string createUser = "";
        string modulecode = "";
        #region 頁面加載
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                logmodel.ProcessOwner = CurrentUserInfo.Personcode;
                logmodel.TransactionType = Request.QueryString["ModuleCode"] == null ? "" : Request.QueryString["ModuleCode"].ToString();
                logmodel.LevelNo = "2";
                logmodel.FromHost = Request.UserHostAddress;

                personCode = base.Request.QueryString["PersonCode"];
                rolesCode = base.Request.QueryString["RoleCode"];
                createUser = base.CurrentUserInfo.Personcode;
                modulecode = "SYS03";//base.Request.QueryString["ModuleCode"]; --無法獲取，直接給定
                DataTable dt = new DataTable();
                dt = departmentAssignBll.GetPersonDeplevel(personCode);
                string DepLevel = dt.Rows[0][0].ToString();
                dt = departmentAssignBll.GetAllLevelCode();
                this.ddlDepLevel.DataSource = dt.DefaultView;
                this.ddlDepLevel.DataTextField = "LevelName";
                this.ddlDepLevel.DataValueField = "orderid";
                this.ddlDepLevel.DataBind();
                this.ddlDepLevel.SelectedIndex = this.ddlDepLevel.Items.IndexOf(this.ddlDepLevel.Items.FindByValue(DepLevel));
                this.CreateTreeModule();
                this.CreateTreeCompany();
                this.btnSave.Attributes.Add("onclick", "if(igtree_getTreeById('UltraWebTreeDepartment').getNodes().length<=0){alert('" + Message.NonDepartment + "');return false;} return confirm('" + Message.SaveConfim + "')");
            }
        }
        #endregion

        #region 保存
        protected void btnSave_Click(object sender, EventArgs e)
        {
            logmodel.ProcessFlag = "insert";
            string Deplist = "";
            string alert = "";
            personCode = base.Request.QueryString["PersonCode"];
            createUser = base.CurrentUserInfo.Personcode;
            rolesCode = base.Request.QueryString["RoleCode"];
            string ModuleCode = Convert.ToString(this.UltraWebTreeModule.SelectedNode.Tag);
            string CompanyId = Convert.ToString(this.UltraWebTreeCompany.SelectedNode.Tag);
            foreach (Node node in UltraWebTreeDepartment.Nodes)
            {
                ParaentNodeCheck(node);
            }

            foreach (Node node in this.UltraWebTreeDepartment.CheckedNodes)
            {
                Deplist += node.Tag.ToString() + "§";
            }
            if (departmentAssignBll.SavePersonDeptData(personCode, rolesCode, ModuleCode, CompanyId, Deplist, createUser, logmodel))
            {
                alert = "alert('" + Message.SaveSuccess + "')";
            }
            else
            {
                alert = "alert('" + Message.SaveFailed + "')";
            }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "SavePersonCompany", alert, true);
            this.CreateTreeDep(this.ddlDepLevel.SelectedValue);


        }
        #endregion

        #region 判斷該父節點是否應被選中
        /// <summary>
        /// 判斷該父節點是否應被選中 2012.02.17  F3228823
        /// </summary>
        /// <param name="node"></param>
        protected void ParaentNodeCheck(Node node)
        {
            if (node.FirstNode != null)
            {
                if (node.FirstNode.FirstNode != null)
                {
                    foreach (Node nodeSub in node.Nodes)
                    {
                        ParaentNodeCheck(nodeSub);
                    }
                }
                Node nodeFlag = node.FirstNode;
                if (nodeFlag.Checked == true)
                {
                    node.Checked = true;
                }
                else
                {
                    int checkNum = 0;
                    while (nodeFlag.NextNode != null)
                    {
                        if (nodeFlag.NextNode.Checked == true)
                        {
                            checkNum++;
                        }
                        nodeFlag = nodeFlag.NextNode;
                    }
                    node.Checked = checkNum >= 1 ? true : false;
                }
                while (node.Parent != null)
                {
                    node.Parent.Checked = node.Checked == true ? true : false;
                    node = node.Parent;
                }
            }
        }
        #endregion

        #region 組織層級下拉菜單change
        protected void ddlDepLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.CreateTreeDep(this.ddlDepLevel.SelectedValue);
        }
        #endregion

        #region 公司樹綁定
        private void CreateTreeCompany()
        {
            this.UltraWebTreeCompany.Nodes.Clear();
            DataTable dt = departmentAssignBll.GetPersonCompany(personCode);
            foreach (DataRow row in dt.Rows)
            {
                this.UltraWebTreeCompany.Nodes.Add(base.CreateNode(Convert.ToString(row["companyid"]), row["companyname"].ToString(), false, true));
            }
            foreach (Node node in this.UltraWebTreeCompany.Nodes)
            {
                node.Expand(false);
            }
        }
        #endregion

        #region 模組樹綁定
        private void CreateTreeModule()
        {
            DataTable dt = departmentAssignBll.GetPersonModule(rolesCode);
            this.UltraWebTreeModule.Nodes.Clear();
            SortedList allTreeNodes = new SortedList();
            foreach (DataRow row in dt.Rows)
            {
                allTreeNodes.Add(Convert.ToString(row["modulecode"]), base.CreateNode(Convert.ToString(row["modulecode"]), FunctionText.ResourceManager.GetString(row["LANGUAGE_KEY"].ToString()), false, Convert.ToDecimal(dt.Compute("count(modulecode)", "parentmodulecode='" + row["modulecode"] + "'")) == 0M));
                if ((row["parentmodulecode"].ToString().Trim().Length > 0) && (allTreeNodes.IndexOfKey(row["parentmodulecode"]) >= 0))
                {
                    ((Node)allTreeNodes.GetByIndex(allTreeNodes.IndexOfKey(row["modulecode"]))).Style.ForeColor = ((Node)allTreeNodes.GetByIndex(allTreeNodes.IndexOfKey(row["parentmodulecode"]))).Style.ForeColor;
                    ((Node)allTreeNodes.GetByIndex(allTreeNodes.IndexOfKey(row["parentmodulecode"]))).Nodes.Add((Node)allTreeNodes.GetByIndex(allTreeNodes.IndexOfKey(row["modulecode"])));
                }
            }
            foreach (DataRow row in dt.Rows)
            {
                if (row["parentmodulecode"].ToString().Trim().Length == 0)
                {
                    this.UltraWebTreeModule.Nodes.Add((Node)allTreeNodes.GetByIndex(allTreeNodes.IndexOfKey(Convert.ToString(row["modulecode"]))));
                }
            }
        }
        #endregion

        #region 部門能樹綁定
        private void CreateTreeDep(string DelLevel)
        {
            this.UltraWebTreeDepartment.Nodes.Clear();
            if ((this.UltraWebTreeModule.SelectedNode != null) && (this.UltraWebTreeCompany.SelectedNode != null))
            {
                string ModuleCode = Convert.ToString(this.UltraWebTreeModule.SelectedNode.Tag);
                string CompanyId = Convert.ToString(this.UltraWebTreeCompany.SelectedNode.Tag);
                DataTable dt = new DataTable();
                string Appuser = base.CurrentUserInfo.Personcode;
                modulecode = "SYS03";
                personCode = base.Request.QueryString["PersonCode"];
                dt = departmentAssignBll.GetPersonDeptDataByModule(Appuser, modulecode, personCode, ModuleCode, CompanyId, DelLevel);
                SortedList allTreeNodes = new SortedList();
                foreach (DataRow row in dt.Rows)
                {
                    allTreeNodes.Add(Convert.ToString(row["depcode"]), base.CreateNode(Convert.ToString(row["depcode"]), row["depname"].ToString(), Convert.ToString(row["authorized"]).Equals("Y"), Convert.ToDecimal(dt.Compute("count(depcode)", "parentdepcode='" + row["depcode"] + "'")) == 0M));
                    if ((row["parentdepcode"].ToString().Trim().Length > 0) && (allTreeNodes.IndexOfKey(row["parentdepcode"]) >= 0))
                    {
                        if (!(!((Node)allTreeNodes.GetByIndex(allTreeNodes.IndexOfKey(row["parentdepcode"]))).Checked && ((Node)allTreeNodes.GetByIndex(allTreeNodes.IndexOfKey(row["parentdepcode"]))).Enabled))
                        {
                            //((Node)allTreeNodes.GetByIndex(allTreeNodes.IndexOfKey(row["depcode"]))).Enabled = false;
                            //((Node)allTreeNodes.GetByIndex(allTreeNodes.IndexOfKey(row["depcode"]))).Checked = false;
                        }
                        ((Node)allTreeNodes.GetByIndex(allTreeNodes.IndexOfKey(row["parentdepcode"]))).Nodes.Add((Node)allTreeNodes.GetByIndex(allTreeNodes.IndexOfKey(row["depcode"])));
                    }
                }
                if (dt.Rows.Count > 0)
                {
                    this.UltraWebTreeDepartment.Nodes.Add((Node)allTreeNodes.GetByIndex(allTreeNodes.IndexOfKey(Convert.ToString(dt.Rows[0]["depcode"]))));
                }
            }
            foreach (Node node in this.UltraWebTreeDepartment.Nodes)
            {
                node.Expand(true);
            }
        }
        #endregion

        #region 模組節點選中事件
        protected void UltraWebTreeModule_NodeClicked(object sender, WebTreeNodeEventArgs e)
        {
            this.CreateTreeDep(this.ddlDepLevel.SelectedValue);
        }
        #endregion

        #region 公司節點選中事件
        protected void UltraWebTreeCompany_NodeClicked(object sender, WebTreeNodeEventArgs e)
        {
            this.CreateTreeDep(this.ddlDepLevel.SelectedValue);
        }
        #endregion
    }
}
