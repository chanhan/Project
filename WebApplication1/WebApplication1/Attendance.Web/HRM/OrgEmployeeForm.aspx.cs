/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： OrgEmployeeBll.cs
 * 檔功能描述： 人員編組功能模組UI類
 * 
 * 版本：1.0
 * 創建標識： 陈函 2011.12.16
 * 
 */
using System;
using System.Collections;
using System.Data;
using System.Drawing;
using GDSBG.MiABU.Attendance.BLL.HRM;
using Infragistics.WebUI.UltraWebNavigator;

namespace GDSBG.MiABU.Attendance.Web.HRM
{
    public partial class OrgEmployeeForm : BasePage
    {
        OrgEmployeeBll orgEmployeeBll = new OrgEmployeeBll();
        string strModuleCode;
        protected void Page_Load(object sender, EventArgs e)
        {
            strModuleCode = Request.QueryString["ModuleCode"] == null ? "" : Request.QueryString["ModuleCode"].ToString(); 
            if (!base.IsPostBack)
            {
                LoadMoudelTree();
            }
        }
        /// <summary>
        /// 初始化人員層級樹
        /// </summary>
        private void LoadMoudelTree()
        {
            string personCode = CurrentUserInfo.Personcode;
            string companyId = CurrentUserInfo.CompanyId;
           // string moduleCode = "WFMSYS01";
            ModuleCode.Value = strModuleCode;
            DataTable dt = orgEmployeeBll.getOrgEmployeeTree(personCode, companyId, strModuleCode);
            this.UltraWebTreeDept.Nodes.Clear();
            SortedList allTreeNodes = new SortedList();
            foreach (DataRow row in dt.Rows)
            {
                string levelCode = row["levelcode"].ToString();
                if (levelCode == null)
                {
                    goto Label_015C;
                }
                if (!(levelCode == "4"))
                {
                    if (levelCode == "5")
                    {
                        goto Label_0144;
                    }
                    if (levelCode == "6")
                    {
                        goto Label_0150;
                    }
                    goto Label_015C;
                }
                Color foreColor = Color.Blue;
                goto Label_0168;
            Label_0144:
                foreColor = Color.Maroon;
                goto Label_0168;
            Label_0150:
                foreColor = Color.SaddleBrown;
                goto Label_0168;
            Label_015C:
                foreColor = Color.Black;
            Label_0168:
                if (row["Deleted"].ToString() == "Y")
                {
                   foreColor = Color.DarkGray;
                }
                allTreeNodes.Add(Convert.ToString(row["depcode"]), base.CreateNode(Convert.ToString(row["depcode"]), Convert.ToString(row["depname"]) + "[" + Convert.ToString(row["depcode"]) + "]", false, Convert.ToDecimal(dt.Compute("count(depcode)", "parentdepcode='" + row["depcode"] + "'")) == 0M, foreColor));
                if (row["parentdepcode"].ToString().Trim().Length > 0)
                {
                    if (allTreeNodes.IndexOfKey(row["parentdepcode"]) >= 0)
                    {
                        ((Node)allTreeNodes.GetByIndex(allTreeNodes.IndexOfKey(row["parentdepcode"]))).Nodes.Add((Node)allTreeNodes.GetByIndex(allTreeNodes.IndexOfKey(row["depcode"])));
                    }
                    else
                    {
                        this.UltraWebTreeDept.Nodes.Add((Node)allTreeNodes.GetByIndex(allTreeNodes.IndexOfKey(Convert.ToString(row["depcode"]))));
                    }
                }
                else
                {
                    this.UltraWebTreeDept.Nodes.Add((Node)allTreeNodes.GetByIndex(allTreeNodes.IndexOfKey(Convert.ToString(row["depcode"]))));
                }
            }
            foreach (Node node in this.UltraWebTreeDept.Nodes)
            {
                //node.Expand(true);
            }
        }
    }
}
