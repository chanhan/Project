using System;
using System.Collections;
using System.Data;
using System.Drawing;
using GDSBG.MiABU.Attendance.BLL.HRM.EmployeeData;
using Infragistics.WebUI.UltraWebNavigator;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;

namespace GDSBG.MiABU.Attendance.Web.HRM.EmployeeData
{
    public partial class TreeDataPickForm : BasePage
    {
        HrmEmpOtherMoveBll hrmEmpOtherMoveBll = new HrmEmpOtherMoveBll();
        string strDepName;
        DataTable dt;
        string moudelCode = "";
        static SynclogModel logmodel = new SynclogModel();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (base.IsPostBack)
            {
                return;
            }
            logmodel.ProcessOwner = CurrentUserInfo.Personcode;
            logmodel.TransactionType = Request.QueryString["modulecode"] == null ? "" : Request.QueryString["modulecode"].ToString();
            logmodel.LevelNo = "2";
            logmodel.FromHost = Request.UserHostAddress;

             moudelCode = Request.QueryString["modulecode"].ToString();
             dt = hrmEmpOtherMoveBll.GetAuthorizedTreeDept(CurrentUserInfo.Personcode, CurrentUserInfo.CompanyId, moudelCode);
            this.UltraWebTreeData.Nodes.Clear();
            SortedList allTreeNodes = new SortedList();
            foreach (DataRow row in dt.Rows)
            {
                strDepName = Convert.ToString(row["depname"]) + "[" + Convert.ToString(row["depcode"]) + "]";
                if (Convert.ToString(row["costcode"]).Trim().Length > 0)
                {
                    strDepName = strDepName + "-" + Convert.ToString(row["costcode"]);
                }
                Node node = base.CreateNode(Convert.ToString(row["depcode"]), strDepName, false, Convert.ToDecimal(dt.Compute("count(depcode)", "parentdepcode='" + row["depcode"] + "'")) == 0M);
                if (Convert.ToString(row["deleted"]).Equals("Y"))
                {
                    node.Style.BorderColor = Color.DarkGray;
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
        protected void CheckBoxAbate_CheckedChanged(object sender, EventArgs e)
        {
             moudelCode = Request.QueryString["modulecode"].ToString();
            this.UltraWebTreeData.Nodes.Clear();
            SortedList allTreeNodes = new SortedList();

            hrmEmpOtherMoveBll.GetDepCodeTable(CurrentUserInfo.Personcode, moudelCode, CurrentUserInfo.CompanyId, "", chkAbate.Checked ? "Y" : "N",logmodel);
              dt = hrmEmpOtherMoveBll.GetAuthorizedTreeDept(CurrentUserInfo.Personcode, CurrentUserInfo.CompanyId, moudelCode);
            string strDepName = "";
            foreach (DataRow row in dt.Rows)
            {
                strDepName = Convert.ToString(row["depname"]) + "[" + Convert.ToString(row["depcode"]) + "]";
                if (Convert.ToString(row["costcode"]).Trim().Length > 0)
                {
                    strDepName = strDepName + "-" + Convert.ToString(row["costcode"]);
                }
                Node node = base.CreateNode(Convert.ToString(row["depcode"]), strDepName, false, Convert.ToDecimal(dt.Compute("count(depcode)", "parentdepcode='" + row["depcode"] + "'")) == 0M);
                if (Convert.ToString(row["deleted"]).Equals("Y"))
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
    }
}
