/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： KqmParameterForm.aspx.cs
 * 檔功能描述： 考勤參數設定
 * 
 * 版本：1.0
 * 創建標識： 張明強 2011.12.9
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
using System.Drawing;
using GDSBG.MiABU.Attendance.BLL.KQM.BasicData;
using Infragistics.WebUI.UltraWebNavigator;


namespace GDSBG.MiABU.Attendance.Web.KQM.BasicData
{
    public partial class KqmParameterForm : BasePage
    {
        #region 組織樹
        /// <summary>
        /// 頁面加載--組織樹
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            string strDepName;

            if (!this.Page.IsPostBack)
            {
                this.ModuleCode.Value = Request.QueryString["ModuleCode"].ToString();
                string personCode = CurrentUserInfo.Personcode;
                string companyId = CurrentUserInfo.CompanyId;
                this.UltraWebTreeData.Nodes.Clear();
                SortedList allTreeNodes = new SortedList();
                RelationSelectorBll bll = new RelationSelectorBll();
                DataTable tempTable = bll.GetTypeDataList(personCode, companyId, this.ModuleCode.Value, "N");
                foreach (DataRow row in tempTable.Rows)
                {

                    strDepName = row["depname"].ToString() + "[" + row["depcode"].ToString() + "]";
                    if (row["costcode"].ToString().Trim().Length > 0)
                    {
                        strDepName = strDepName + "-" + row["costcode"].ToString();
                    }
                    Node node = base.CreateNode(row["depcode"].ToString(), strDepName, false, Convert.ToDecimal(tempTable.Compute("count(depcode)", "parentdepcode='" + row["depcode"].ToString() + "'")) == 0M);
                    if (row["deleted"].ToString().Equals("Y"))
                    {
                        node.Style.ForeColor = Color.DarkGray;
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

        }
        #endregion 
    }
}
