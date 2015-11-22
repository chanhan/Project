/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： RelationSelector.cs
 * 檔功能描述： 用戶可訪問的單位信息
 * 
 * 版本：1.0
 * 創建標識： 高子焱 2011.12.10
 * 
 */

using System;
using System.Collections;
using System.Data;
using System.Drawing;
using GDSBG.MiABU.Attendance.BLL.KQM.BasicData;
using Infragistics.WebUI.UltraWebNavigator;

namespace GDSBG.MiABU.Attendance.Web.KQM.BasicData
{
    public partial class RelationSelector : BasePage
    {
        private string moduleCode = "";
        RelationSelectorBll bll = new RelationSelectorBll();
        //綁定所有的組織代碼 （包含已失效的）
        protected void chkAbate_CheckedChanged(object sender, EventArgs e)
        {
            string strDepName;
            string personCode = CurrentUserInfo.Personcode;
            string companyId = CurrentUserInfo.CompanyId;
            this.UltraWebTreeData.Nodes.Clear();
            SortedList allTreeNodes = new SortedList();
            DataTable dt = bll.GetTypeDataList(personCode, companyId, moduleCode);
            foreach (DataRow row in dt.Rows)
            {
                strDepName = Convert.ToString(row["depname"]) + "[" + Convert.ToString(row["depcode"]) + "]";
                if (Convert.ToString(row["costcode"]).Trim().Length > 0)
                {
                    strDepName = strDepName + "-" + Convert.ToString(row["costcode"]);
                }
                Node node = base.CreateNode(Convert.ToString(row["depcode"]), strDepName, false, Convert.ToDecimal(dt.Compute("count(depcode)", "parentdepcode='" + row["depcode"] + "'")) == 0M);
                if (Convert.ToString(row["deleted"]).Equals("Y") || Convert.ToString(row["deleted"]).Equals(""))
                {
                    node.Style.ForeColor = Color.Red;
                }
                allTreeNodes.Add(Convert.ToString(row["depcode"]), node);
                if (row["parentdepcode"].ToString().Trim().Length > 0)
                {
                    if (allTreeNodes.IndexOfKey(row["parentdepcode"]) >= 0)
                    {
                        ((Node)allTreeNodes.GetByIndex(allTreeNodes.IndexOfKey(row["parentdepcode"]))).Nodes.Add((Node)allTreeNodes.GetByIndex(allTreeNodes.IndexOfKey(row["depcode"])));
                    }
                    else
                    {
                        this.UltraWebTreeData.Nodes.Add((Node)allTreeNodes.GetByIndex(allTreeNodes.IndexOfKey(Convert.ToString(row["depcode"]))));
                    }
                }
                else
                {
                    this.UltraWebTreeData.Nodes.Add((Node)allTreeNodes.GetByIndex(allTreeNodes.IndexOfKey(Convert.ToString(row["depcode"]))));
                }
            }
            foreach (Node node in this.UltraWebTreeData.Nodes)
            {
                node.Expand(false);
            }
        }
        //綁定所有有效的組織代碼
        private void DeptDataBind()
        {
            string strDepName;
            string personCode = CurrentUserInfo.Personcode;
            string companyId = CurrentUserInfo.CompanyId;
            SortedList allTreeNodes = new SortedList();
            DataTable dt = bll.GetTypeDataList(personCode, companyId, moduleCode, "N");
            foreach (DataRow row in dt.Rows)
            {
                strDepName = row["depname"].ToString() + "[" + row["depcode"].ToString() + "]";
                if (row["costcode"].ToString().Trim().Length > 0)
                {
                    strDepName = strDepName + "-" + row["costcode"].ToString();
                }
                Node node = base.CreateNode(row["depcode"].ToString(), strDepName, false, Convert.ToDecimal(dt.Compute("count(depcode)", "parentdepcode='" + row["depcode"].ToString() + "'")) == 0M);
                if (row["deleted"].ToString().Equals("Y"))
                {
                    node.Style.BackColor = Color.Red;
                }
                allTreeNodes.Add(row["depcode"].ToString(), node);
                if (row["parentdepcode"].ToString().Trim().Length > 0)
                {
                    if (allTreeNodes.IndexOfKey(row["parentdepcode"]) >= 0)
                    {
                        ((Node)allTreeNodes.GetByIndex(allTreeNodes.IndexOfKey(row["parentdepcode"]))).Nodes.Add((Node)allTreeNodes.GetByIndex(allTreeNodes.IndexOfKey(row["depcode"])));
                    }
                    else
                    {
                        this.UltraWebTreeData.Nodes.Add((Node)allTreeNodes.GetByIndex(allTreeNodes.IndexOfKey(row["depcode"].ToString())));
                    }
                }
                else
                {
                    this.UltraWebTreeData.Nodes.Add((Node)allTreeNodes.GetByIndex(allTreeNodes.IndexOfKey(row["depcode"].ToString())));
                }
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            moduleCode = Request.QueryString["moduleCode"];
            this.UltraWebTreeData.Nodes.Clear();
            SortedList allTreeNodes = new SortedList();
            DeptDataBind();
            foreach (Node node in this.UltraWebTreeData.Nodes)
            {
                node.Expand(false);
            }

        }
    }
}
