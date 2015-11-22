/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： DepartmentLayoutBForm.cs
 * 檔功能描述：組織圖B的UI
 * 
 * 版本：1.0
 * 創建標識： 顧陳偉 2012.02.06
 * 
 */
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Script.Serialization;
using System.Web.UI.WebControls;
using GDSBG.MiABU.Attendance.BLL.HRM;
using Resources;
using Whidsoft.WebControls;

namespace GDSBG.MiABU.Attendance.Web.HRM
{
    public partial class DepartmentLayoutBForm : BasePage
    {
        DepartmentBll bllDepartment = new DepartmentBll();
        DataTable dt = new DataTable();
        DataTable dtnew = new DataTable();
        DataTable newdt = new DataTable();
        JavaScriptSerializer JsSerializer = new JavaScriptSerializer();
        Dictionary<string, string> ClientMessage = null;

        #region 查詢
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            this.MyDataBind(this.RadioType.SelectedValue);
        }
        #endregion

        #region 加載綁定
        public void PageLoadBind()
        {
            DataTable table = new DataTable();
            table.Columns.Add("depcode", typeof(string));
            table.Columns.Add("depname", typeof(string));
            table.Columns.Add("parentdepcode", typeof(string));
            table.Rows.Add("A01", "總部周邊", "");
            table.Rows.Add("A02", "公司員工", "A01");
            table.Rows.Add("A03", "部門名稱", "A01");
            DataRow[] rows = table.Select(" depcode = 'A01'");
            foreach (DataRow dr in rows)
            {
                OrgNode tn = new OrgNode
                {
                    ID = Convert.ToString(dr["depcode"]),
                    Text = Convert.ToString(dr["depname"]),
                    Type = "",
                    NavigateUrl = "javascript:NoMessage()",
                    Width = 130
                };
                DataRow[] newrows = table.Select(" parentdepcode = 'A01'");
                foreach (DataRow newdr in newrows)
                {
                    OrgNode newtn = new OrgNode
                    {
                        ID = Convert.ToString(newdr["depcode"]),
                        Text = Convert.ToString(newdr["depname"]),
                        Type = "",
                        NavigateUrl = "javascript:NoMessage()",
                        Width = 130
                    };
                    tn.Nodes.Add(newtn);
                }
                this.OrgChart1.Node = tn;
            }
            this.OrgChart1.ChartStyle = ("1" == "1") ? Whidsoft.WebControls.Orientation.Vertical : Whidsoft.WebControls.Orientation.Horizontal;
        }
        #endregion

        #region 數據綁定
        public void MyDataBind(string style)
        {
            string PersonQty = Message.PersonQty;
            this.HiddenType.Value = style;
            dt = bllDepartment.GetDepartmentInfo(txtDepCode.Text);
            dtnew = dt.Copy();
            string TotalNum = "";
            string Manager = "";
            if (dtnew.Rows.Count > 0)
            {
                DataRow[] rows = dtnew.Select(" depcode = '" + this.txtDepCode.Text + "'");
                foreach (DataRow dr in rows)
                {
                    TotalNum = "";
                    dt.Clear();
                    dt = bllDepartment.GetDepEmpCount(txtDepCode.Text);
                    newdt.Clear();
                    newdt = dt.Copy();
                    if ((newdt.Rows.Count > 0) && !Convert.ToString(newdt.Rows[0]["TotalCount"]).Equals("0"))
                    {
                        TotalNum = "<br>" + PersonQty + ":" + Convert.ToString(newdt.Rows[0]["TotalCount"]) + "";
                    }
                    if (Convert.ToString(dr["Manager"]).Length > 0)
                    {
                        Manager = "<br><font color='red'>" + Convert.ToString(dr["Manager"]) + "</font>";
                    }
                    else
                    {
                        Manager = "";
                    }
                    OrgNode tn = new OrgNode
                    {
                        ID = Convert.ToString(dr["depcode"]),
                        Text = Convert.ToString(dr["depname"]) + Manager + TotalNum,
                        Type = "",
                        NavigateUrl = "javascript:NoMessage()",
                        Width = 130
                    };
                    int childcount = this.SetNodeChild(dtnew, tn);
                    this.OrgChart1.Node = tn;
                }
            }
            this.OrgChart1.ChartStyle = (style == "1") ? Whidsoft.WebControls.Orientation.Vertical : Whidsoft.WebControls.Orientation.Horizontal;
        }
        #endregion

        #region 頁面加載
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                PageLoadBind();
                this.HiddenModuleCode.Value = base.Request.QueryString["ModuleCode"];
                this.txtDepName.BorderStyle = BorderStyle.None;
            }
            //頁面彈框顯示信息
            if (ClientMessage == null)
            {
                ClientMessage = new Dictionary<string, string>();
                ClientMessage.Add("DepNameNotNull", Message.DepNameNotNull);
            }
            string clientmsg = JsSerializer.Serialize(ClientMessage);
            Page.ClientScript.RegisterStartupScript(GetType(), "ClientMessage", "var Message=" + clientmsg + ";", true);
        }
        #endregion

        #region 組織圖數據處理
        public int SetNodeChild(DataTable dt, OrgNode NodeParent)
        {
            string PersonQty = Message.PersonQty;
            string TotalNum = "";
            string Manager = "";
            if (dt.Rows.Count > 0)
            {
                DataRow[] rows = dt.Select(" parentdepcode = '" + NodeParent.ID.Trim() + "'");
                foreach (DataRow dr in rows)
                {
                    TotalNum = "";
                    DataTable newtable = bllDepartment.GetDepEmpCount(Convert.ToString(dr["depcode"]));
                    if ((newtable.Rows.Count > 0) && !Convert.ToString(newtable.Rows[0]["TotalCount"]).Equals("0"))
                    {
                        TotalNum = "<br>" + PersonQty + ":" + Convert.ToString(newtable.Rows[0]["TotalCount"]);
                    }
                    if (Convert.ToString(dr["Manager"]).Length > 0)
                    {
                        Manager = "<br><font color='red'>" + Convert.ToString(dr["Manager"]) + "</font>";
                    }
                    else
                    {
                        Manager = "";
                    }
                    OrgNode tn = new OrgNode
                    {
                        ID = Convert.ToString(dr["depcode"]),
                        Text = Convert.ToString(dr["depname"]) + Manager + TotalNum,
                        Type = "",
                        NavigateUrl = "javascript:NoMessage()",
                        Width = 130
                    };
                    this.SetNodeChild(dt, tn);
                    NodeParent.Nodes.Add(tn);
                }
                return rows.Length;
            }
            return 0;
        }
        #endregion
    }
}
