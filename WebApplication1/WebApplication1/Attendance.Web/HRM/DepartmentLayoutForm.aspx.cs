/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： DepartmentLayoutForm.cs
 * 檔功能描述：組織圖A的UI
 * 
 * 版本：1.0
 * 創建標識： 顧陳偉 2012.02.06
 * 
 */
using System;
using System.Data;
using System.Web.UI.WebControls;
using GDSBG.MiABU.Attendance.BLL.HRM;
using Whidsoft.WebControls;
using Resources;

namespace GDSBG.MiABU.Attendance.Web.HRM
{
    public partial class DepartmentLayoutForm : BasePage
    {
        DepartmentBll bllDepartment = new DepartmentBll();
        DataTable dt = new DataTable();
        DataTable dtnew = new DataTable();
        DataTable newdt = new DataTable();
        #region 查詢
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            if (bllDepartment.CheckDepCode(txtDepCode.Text).Rows.Count > 0)
            {
                base.Response.Write("<script type='text/javascript'>alert(\"" + Message.QueryError + "\");</script>");
            }
            else if (this.txtDepCode.Text.Length == 0)
            {
                base.Response.Write("<script type='text/javascript'>alert(\"" + Message.ChoiceDepCode + "\");</script>");
            }
            else
            {
                this.MyDataBind(this.RadioType.SelectedValue);
            }
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
        private void MyDataBind(string style)
        {
            this.HiddenType.Value = style;
            dt = bllDepartment.GetDepartmentAInfo(txtDepCode.Text);
            dtnew = dt.Copy();
            string TotalNum = "";
            string Manager = "";
            if (this.dtnew.Rows.Count > 0)
            {
                DataRow[] rows = this.dtnew.Select(" depcode = '" + this.txtDepCode.Text + "'");
                foreach (DataRow dr in rows)
                {
                    TotalNum = "";
                    dt = bllDepartment.GetEmpCountByLevelType(txtDepCode.Text);
                    newdt=dt.Copy();
                    if ((this.newdt.Rows.Count > 0) && !Convert.ToString(this.newdt.Rows[0]["TotalCount"]).Equals("0"))
                    {
                        if (!Convert.ToString(this.newdt.Rows[0]["CountA"]).Equals("0"))
                        {
                            TotalNum = "師級:" + Convert.ToString(this.newdt.Rows[0]["CountA"]);
                        }
                        if (!Convert.ToString(this.newdt.Rows[0]["CountB"]).Equals("0"))
                        {
                            if (TotalNum.Length > 0)
                            {
                                TotalNum = TotalNum + ";<br>";
                            }
                            TotalNum = TotalNum + "員級:" + Convert.ToString(this.newdt.Rows[0]["CountB"]);
                        }
                        if (!Convert.ToString(this.newdt.Rows[0]["CountC"]).Equals("0"))
                        {
                            if (TotalNum.Length > 0)
                            {
                                TotalNum = TotalNum + ";<br>";
                            }
                            TotalNum = TotalNum + "不銓敘:" + Convert.ToString(this.newdt.Rows[0]["CountC"]);
                        }
                        TotalNum = "<br>人數:" + Convert.ToString(this.newdt.Rows[0]["TotalCount"]) + "<br>(<font color='black'>" + TotalNum + "</font>)";
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
                    int childcount = this.SetNodeChild(this.dtnew, tn);
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
        }
        #endregion

        #region 組織圖數據處理
        private int SetNodeChild(DataTable dt, OrgNode NodeParent)
        {
            string TotalNum = "";
            string Manager = "";
            if (dt.Rows.Count > 0)
            {
                DataRow[] rows = dt.Select(" parentdepcode = '" + NodeParent.ID.Trim() + "'");
                foreach (DataRow dr in rows)
                {
                    TotalNum = "";
                    newdt = bllDepartment.GetEmpCountByLevelType(Convert.ToString(dr["depcode"]));
                    if ((this.newdt.Rows.Count > 0) && !Convert.ToString(this.newdt.Rows[0]["TotalCount"]).Equals("0"))
                    {
                        if (!Convert.ToString(this.newdt.Rows[0]["CountA"]).Equals("0"))
                        {
                            TotalNum = "師級:" + Convert.ToString(this.newdt.Rows[0]["CountA"]);
                        }
                        if (!Convert.ToString(this.newdt.Rows[0]["CountB"]).Equals("0"))
                        {
                            if (TotalNum.Length > 0)
                            {
                                TotalNum = TotalNum + ";<br>";
                            }
                            TotalNum = TotalNum + "員級:" + Convert.ToString(this.newdt.Rows[0]["CountB"]);
                        }
                        if (!Convert.ToString(this.newdt.Rows[0]["CountC"]).Equals("0"))
                        {
                            if (TotalNum.Length > 0)
                            {
                                TotalNum = TotalNum + ";<br>";
                            }
                            TotalNum = TotalNum + "不銓敘:" + Convert.ToString(this.newdt.Rows[0]["CountC"]);
                        }
                        TotalNum = "<br>人數:" + Convert.ToString(this.newdt.Rows[0]["TotalCount"]) + "<br>(<font color='black'>" + TotalNum + "</font>)";
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
