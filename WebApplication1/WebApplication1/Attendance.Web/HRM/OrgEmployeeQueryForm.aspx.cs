using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;
using GDSBG.MiABU.Attendance.BLL.HRM;
using GDSBG.MiABU.Attendance.BLL.KQM.BasicData;
using GDSBG.MiABU.Attendance.Model.HRM;
using Infragistics.WebUI.UltraWebNavigator;
using Resources;


/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： HrmManagerInfoForm.aspx.cs
 * 檔功能描述： 組織人員查詢
 * 
 * 版本：1.0
 * 創建標識： 昝望 2011.12.12
 * 
 */

namespace GDSBG.MiABU.Attendance.Web.HRM
{
    public partial class OrgEmployeeQueryForm : BasePage
    {
        OrgmanagerBll bll = new OrgmanagerBll();
        static DataTable dt_global = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            string strDepName;
            SetSelector(ImageDepCode, txtDepCode, txtDepName, "DepCode");
            try
            {
                if (!this.Page.IsPostBack)
                {
                    this.ModuleCode.Value = Request.QueryString["ModuleCode"].ToString();
                    this.UltraWebTreeData.Nodes.Clear();
                    SortedList allTreeNodes = new SortedList();
                    
                    RelationSelectorBll bll = new RelationSelectorBll();
                    DataTable tempTable = bll.GetTypeDataList(CurrentUserInfo.Personcode, "Foxconn", this.ModuleCode.Value);
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
                            node.Style.BorderColor = Color.DarkGray;
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
            catch (Exception ex)
            {
                //base.WriteMessage(2, (ex.InnerException == null) ? ex.Message : ex.InnerException.Message);
            }
        }

        #region 查詢
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            if (this.UltraWebTreeData.Nodes.Count > 0)
            {
                for (int i = 0; i < this.UltraWebTreeData.Nodes.Count; i++)
                {
                    this.ExpandTree(this.UltraWebTreeData.Nodes[i], this.txtDepCode.Text.Trim());
                }
            }
            DataTable dt = bll.OrgEmployeeQuery(CurrentUserInfo.Personcode, this.txtWorkNo.Text.Trim(), txtDepCode.Text.Trim());
            dt_global = dt;
            this.gridEmployee.DataSource = dt;
            this.gridEmployee.DataBind();
        }
        #endregion

        #region 導出報表
        protected void btnExport_Click(object sender, EventArgs e)
        {
            OrgmanagerModel model = new OrgmanagerModel();
            List<OrgmanagerModel> list = bll.GetList(dt_global);
            string[] header = { ControlText.gvHeadWorkNo, ControlText.gvHeadLocalName, ControlText.gvHeadParamsOrgDepName, ControlText.gvHeadLevelName, ControlText.gvHeadManagerName, ControlText.gvHeadIsManager, ControlText.gvHeadIsTW, ControlText.gvHeadTel, ControlText.gvHeadNotes};
            string[] properties = { "WorkNo", "LocalName", "DepName", "LevelName", "ManagerName", "IsDirectlyUnder", "IsTW", "Tel", "Notes"};
            string filePath = MapPath("~/ExportFileTemp/") + DateTime.Now.Ticks + ".xls";
            NPOIHelper.ExportExcel(list, header, properties, 5000, filePath);
            PageHelper.ReturnHTTPStream(filePath, true);

        }
        #endregion

        #region 點擊部門樹查詢
        public void UltraWebTreeDept_NodeClicked(object sender, WebTreeNodeEventArgs e)
        {
            string sDepCode = e.Node.Tag.ToString().Trim();
            DataTable dt = bll.OrgEmployeeQuery(CurrentUserInfo.Personcode, "", sDepCode);
            dt_global = dt;
            this.gridEmployee.DataSource = dt;
            this.gridEmployee.DataBind();
        }
        #endregion

        #region 設置Selector
        /// <summary>
        /// 設置Selector
        /// </summary>
        /// <param name="ctrlTrigger">控件ID--按鈕</param>
        /// <param name="ctrlCode">控件ID--文本框1</param>
        /// <param name="ctrlName">控件ID--文本框2</param>
        /// <param name="flag">Selector區分標誌</param>
        public static void SetSelector(WebControl ctrlTrigger, Control ctrlCode, Control ctrlName, string flag)
        {
            if (ctrlCode is TextBox) { (ctrlCode as TextBox).Attributes.Add("readonly", "readonly"); }
            if (ctrlName is TextBox) { (ctrlName as TextBox).Attributes.Add("readonly", "readonly"); }
            ctrlTrigger.Attributes.Add("onclick", string.Format("return setSelector('{0}','{1}','{2}')",
                ctrlCode.ClientID, ctrlName.ClientID, flag));
        }
        #endregion

        #region 選中相應樹節點
        /// <summary>
        /// 選中相應樹節點
        /// </summary>
        /// <param name="treeNode"></param>
        /// <param name="strDepCode"></param>
        private void ExpandTree(Node treeNode, string strDepCode)
        {
            if (treeNode.Tag.ToString() == strDepCode)
            {
                treeNode.Expand(false);
                this.UltraWebTreeData.SelectedNode = treeNode;
                this.UltraWebTreeData.SelectedNode.Style.ForeColor = Color.Red;
            }
            foreach (Node node in treeNode.Nodes)
            {
                this.ExpandTree(node, strDepCode);
            }
        }
        #endregion
    }
}

