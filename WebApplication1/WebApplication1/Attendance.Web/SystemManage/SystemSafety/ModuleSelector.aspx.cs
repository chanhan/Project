
/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： ModuleSelector.aspx.cs
 * 檔功能描述： 系統模組選擇頁面
 * 
 * 版本：1.0
 * 創建標識： 張明強 2011.11.28
 * 
 */
using System;
using System.Collections;
using System.Data;
using GDSBG.MiABU.Attendance.BLL.SystemManage.SystemSafety;
using Infragistics.WebUI.UltraWebNavigator;

namespace GDSBG.MiABU.Attendance.Web.SystemManage.SystemSafety
{

    /// <summary>
    /// 模組選擇器
    /// </summary>
    public partial class ModuleSelector : BasePage
    {
        ModuleBll moduleBll = new ModuleBll();
        string RequestModuleList;
        DataTable tempTable = new DataTable(); 
        protected override bool AccessPermissionRequired { get { return false; } }


        #region 頁面加載方法
        /// <summary>
        /// 頁面加載方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                RequestModuleList = "," + Request.QueryString["ModuleCodeList"] + ",";
                //BindModuleTree();
                WebTreeBind();
            }
        }
        #endregion


        #region  WebTree控件綁定數據
        /// <summary>
        /// WebTree控件綁定數據
        /// </summary>
        protected void WebTreeBind()
        {
            this.UltraWebTreeData.Nodes.Clear();
            SortedList allTreeNodes = new SortedList();
            DataTable tempTable = moduleBll.GetUserModuleTable();
            foreach (DataRow row in tempTable.Rows)
            {
                allTreeNodes.Add(Convert.ToString(row["ModuleCode"]), base.CreateNode(Convert.ToString(row["ModuleCode"]), Convert.ToString(row["description"]), false, Convert.ToDecimal(tempTable.Compute("count(ModuleCode)", "parentmodulecode='" + row["ModuleCode"] + "'")) == 0M));
                if (row["parentmodulecode"].ToString().Trim().Length > 0)
                {
                    if (allTreeNodes.IndexOfKey(row["parentmodulecode"]) >= 0)
                    {
                        ((Node)allTreeNodes.GetByIndex(allTreeNodes.IndexOfKey(row["parentmodulecode"]))).Nodes.Add((Node)allTreeNodes.GetByIndex(allTreeNodes.IndexOfKey(row["ModuleCode"])));
                    }
                }
                else
                {
                    this.UltraWebTreeData.Nodes.Add((Node)allTreeNodes.GetByIndex(allTreeNodes.IndexOfKey(Convert.ToString(row["ModuleCode"]))));
                }

            }
        }
        #endregion

        #region 原始代碼(TreeView控件使用)
        /*

        /// <summary>
        /// 選擇模組
        /// </summary>
        List<ModuleModel> ModuleList
        {
            get
            {
                if (ViewState["__MODULE_LIST"] == null)
                {
                    ViewState["__MODULE_LIST"] = TableToList();
                }
                return ViewState["__MODULE_LIST"] as List<ModuleModel>;
            }
            set { ViewState["__MODULE_LIST"] = value; }
        }

        /// <summary>
        /// 綁定模組
        /// </summary>
        private void BindModuleTree()
        {
            TreeNode rootNode = new TreeNode();
            rootNode.Expanded = true;
            rootNode.Value = null;
            rootNode.Text = "Foxconn";
            rootNode.NavigateUrl = "javascript:SelectModuleNode('ModuleRoot')";
            BindTree("", rootNode);
            treeModules.Nodes.Clear();
            treeModules.Nodes.Add(rootNode);
        }

        /// <summary>
        /// 遞迴綁定模組
        /// </summary>
        /// <param name="parentId">父節點ID</param>
        /// <param name="parentNode">父節點</param>
        private void BindTree(string parentId, TreeNode parentNode)
        {
            List<ModuleModel> modules = ModuleList.FindAll(model => { return model.ParentModuleCode == parentId; });
            foreach (ModuleModel module in modules)
            {
                TreeNode node = new TreeNode();
                node.Expanded = true;
                node.ShowCheckBox = true;
                node.Value = module.ModuleCode;
                node.Checked = RequestModuleList.Contains("," + node.Value + ",");
                node.Text = module.Description;
                node.NavigateUrl = string.Format("javascript:SelectModuleNode('{0}')", node.Value);
                parentNode.ChildNodes.Add(node);
                BindTree(module.ModuleCode, node);
            }
        }


        protected List<ModuleModel> TableToList()
        {
            tempTable = moduleBll.GetUserModuleList();
            List<ModuleModel> list = new List<ModuleModel>();
            ModuleModel model;
            for (int i = 0; i <= tempTable.Rows.Count - 1; i++)
            {
                model = new ModuleModel();
                model.ModuleCode = tempTable.Rows[i]["modulecode"].ToString().Trim();
                model.ParentModuleCode = tempTable.Rows[i]["parentmodulecode"].ToString().Trim();
                model.Description = tempTable.Rows[i]["description"].ToString().Trim();
                list.Add(model);
            }
            return list;
        }


        */
        #endregion

       


    }

}
