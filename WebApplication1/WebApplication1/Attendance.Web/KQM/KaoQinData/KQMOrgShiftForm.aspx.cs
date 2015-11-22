using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using GDSBG.MiABU.Attendance.BLL.KQM.BasicData;
using Infragistics.WebUI.UltraWebNavigator;
using System.Drawing;

namespace GDSBG.MiABU.Attendance.Web.KQM.KaoQinData
{
    public partial class KQMOrgShiftForm : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string strDepName;
            try
            {
                if (!this.Page.IsPostBack)
                {
                    this.ModuleCode.Value = "KQMSYS306";
                    this.UltraWebTreeData.Nodes.Clear();
                    SortedList allTreeNodes = new SortedList();

                    RelationSelectorBll bll = new RelationSelectorBll();
                    DataTable tempTable = bll.GetTypeDataList(CurrentUserInfo.Personcode, "Foxconn", this.ModuleCode.Value, "N");
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
    }
}
