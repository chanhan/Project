/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： HrmDepartmentForm
 * 檔功能描述： 組織擴建UI
 * 
 * 版本：1.0
 * 創建標識： 顧陳偉 2012.01.02
 * 
 */

using System;
using System.Data;
using System.Drawing;
using GDSBG.MiABU.Attendance.BLL.HRM;
using Infragistics.WebUI.UltraWebNavigator;

namespace GDSBG.MiABU.Attendance.Web.HRM
{
    public partial class HrmDepartmentForm : BasePage
    {
        OrgEmployeeBll orgEmployeeBll = new OrgEmployeeBll();
        HrmDepartmentBll hrmDepartmentBll = new HrmDepartmentBll();
        DataTable dt = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                string personCode = CurrentUserInfo.Personcode;
                string companyId = CurrentUserInfo.CompanyId;
                string module_Code = this.Request.QueryString["ModuleCode"].ToString();
                ModuleCode.Value = module_Code;
                dt = hrmDepartmentBll.getOrgTree(personCode, companyId, module_Code);
                //=================加載樹=================
                this.UltraWebTreeData.Nodes.Clear();
                System.Collections.SortedList allTreeNodes = new System.Collections.SortedList();
                Color foreColor;
                string strDeleted = "";
                foreach (System.Data.DataRow row in dt.Rows)
                {
                    switch (row["levelcode"].ToString())
                    {
                        case "0":
                            foreColor = Color.Blue;
                            break;
                        case "1":
                            foreColor = Color.Maroon;
                            break;
                        case "2":
                            foreColor = Color.SaddleBrown;
                            break;
                        case "3":
                            foreColor = Color.Red;
                            break;
                        default:
                            foreColor = Color.Black;
                            break;
                    }
                    if (row["Deleted"].ToString() == "Y")
                    {
                        foreColor = Color.DarkGray;
                        strDeleted = "[Disabled]";
                    }
                    else
                    {
                        strDeleted = "";
                    }
                    allTreeNodes.Add(System.Convert.ToString(row["depcode"]), base.CreateNode(System.Convert.ToString(row["depcode"]), System.Convert.ToString(row["depname"]) + "[" + System.Convert.ToString(row["depcode"]) + "]" + strDeleted, false,
                        System.Convert.ToDecimal(dt.Compute("count(depcode)", "parentdepcode='" + row["depcode"] + "'")) == 0, foreColor));

                    if (row["parentdepcode"].ToString().Trim().Length > 0)
                    {
                        if (allTreeNodes.IndexOfKey(row["parentdepcode"]) >= 0)
                        {
                            ((Infragistics.WebUI.UltraWebNavigator.Node)allTreeNodes.GetByIndex(allTreeNodes.IndexOfKey(row["parentdepcode"]))).Nodes.Add((Infragistics.WebUI.UltraWebNavigator.Node)allTreeNodes.GetByIndex(allTreeNodes.IndexOfKey(row["depcode"])));
                        }
                        else
                        {
                            this.UltraWebTreeData.Nodes.Add((Infragistics.WebUI.UltraWebNavigator.Node)allTreeNodes.GetByIndex(allTreeNodes.IndexOfKey(System.Convert.ToString(row["depcode"]))));
                        }
                    }
                    else
                    {
                        this.UltraWebTreeData.Nodes.Add((Infragistics.WebUI.UltraWebNavigator.Node)allTreeNodes.GetByIndex(allTreeNodes.IndexOfKey(System.Convert.ToString(row["depcode"]))));
                    }
                }
                //=================加載完成=================
                foreach (Node node in this.UltraWebTreeData.Nodes)
                {
                    node.Expand(false);
                }
            }


        }
    }
}
