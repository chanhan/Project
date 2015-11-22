/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： DepartmentForm.cs
 * 檔功能描述：組織資料UI
 * 
 * 版本：1.0
 * 創建標識： 顧陳偉 2012.02.02
 * 
 */
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Web.Script.Serialization;
using System.Web.UI;
using GDSBG.MiABU.Attendance.BLL.HRM;
using GDSBG.MiABU.Attendance.BLL.HRM.EmployeeData;
using GDSBG.MiABU.Attendance.BLL.SystemManage.SystemSafety;
using GDSBG.MiABU.Attendance.Model.HRM;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;
using Resources;

namespace GDSBG.MiABU.Attendance.Web.HRM
{
    public partial class DepartmentForm : BasePage
    {
        DepartmentBll bllDepartment = new DepartmentBll();
        DepartmentAssignBll bllDepartmentAssign = new DepartmentAssignBll();
        HrmEmpOtherMoveBll bllHrmEmpOtherMove = new HrmEmpOtherMoveBll();
        DataTable dt = new DataTable();
        static DataTable dt_global = new DataTable();
        static DepartmentModel model;
        static SynclogModel logmodel = new SynclogModel();
        JavaScriptSerializer JsSerializer = new JavaScriptSerializer();
        Dictionary<string, string> ClientMessage = null;

        #region 頁面加載
        protected void Page_Load(object sender, EventArgs e)
        {
            PageHelper.ButtonControls(FuncList, pnlShowPanel.Controls, base.FuncListModule);
            if (!base.IsPostBack)
            {
                logmodel.ProcessOwner = CurrentUserInfo.Personcode;
                logmodel.TransactionType = Request.QueryString["ModuleCode"] == null ? "" : Request.QueryString["ModuleCode"].ToString();
                logmodel.LevelNo = "2";
                logmodel.FromHost = Request.UserHostAddress;

                this.HiddenModuleCode.Value = base.Request.QueryString["ModuleCode"];
                string DepLevel = bllDepartment.GetOrderId().Rows[0][0].ToString();
                dt = bllDepartmentAssign.GetAllLevelCode();
                this.ddlDepLevel.DataSource = dt.DefaultView;
                this.ddlDepLevel.DataTextField = "LevelName";
                this.ddlDepLevel.DataValueField = "orderid";
                this.ddlDepLevel.DataBind();
                this.ddlDepLevel.SelectedIndex = this.ddlDepLevel.Items.IndexOf(this.ddlDepLevel.Items.FindByValue(DepLevel));
                model = new DepartmentModel();
                //Query();
            }
            //頁面彈框顯示信息
            if (ClientMessage == null)
            {
                ClientMessage = new Dictionary<string, string>();
                ClientMessage.Add("DeleteConfirm", Message.DeleteConfirm);
                ClientMessage.Add("EnableConfirm", Message.EnableConfirm);
                ClientMessage.Add("DisableConfirm", Message.DisableConfirm);
                ClientMessage.Add("AtLastOneChoose", Message.AtLastOneChoose);
                ClientMessage.Add("TextBoxNotNull", Message.TextBoxNotNull);
                ClientMessage.Add("OrderIDNotNumber", Message.OrderIDNotNumber);
                ClientMessage.Add("SaveConfim", Message.SaveConfim);
                ClientMessage.Add("NoAuthority", Message.NoAuthority);
            }
            string clientmsg = JsSerializer.Serialize(ClientMessage);
            Page.ClientScript.RegisterStartupScript(GetType(), "ClientMessage", "var Message=" + clientmsg + ";", true);

        }
        #endregion

        #region 分頁
        protected void pager_PageChanged(object sender, EventArgs e)
        {
            Query();
        }
        #endregion

        #region 查詢方法
        private void Query()
        {
            int totalCount = 0;
            string sql = base.SqlDep;
            string levelcode = this.ddlDepLevel.SelectedValue;
            dt_global = bllDepartment.GetDeaprtmentPageInfo(model,levelcode, sql, pager.CurrentPageIndex, pager.PageSize, out totalCount);
            //當datatable數據為空時,為其添加空行
            if (dt_global.Rows.Count == 0)
            {
                dt_global.Rows.Add(dt_global.NewRow());
            }
            pager.RecordCount = totalCount;
            UltraWebGridDepartment.DataSource = dt_global.DefaultView;
            UltraWebGridDepartment.DataBind();
            pager.TextAfterPageIndexBox = "/" + pager.PageCount.ToString();
            DataUIBind();
        }
        #endregion

        #region 查詢
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            string actionFlag = this.ProcessFlag.Value.ToString().Trim();
            //點擊"條件",依據所選條件查詢
            if (actionFlag == "condition")
            {
                model = PageHelper.GetModel<DepartmentModel>(pnlContent.Controls);
                pager.CurrentPageIndex = 1;
                Query();
                this.hidOperate.Value = "";
            }
            //不點擊"條件",查詢全部信息
            else
            {
                model = new DepartmentModel();
                pager.CurrentPageIndex = 1;
                Query();

            }
            this.hidOperate.Value = "";
            this.ProcessFlag.Value = "";
        }
        #endregion

        #region 刪除
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            logmodel.ProcessFlag = "delete";
            string alert = "";
            if (this.UltraWebGridDepartment.DisplayLayout.ActiveRow != null)
            {
                dt.Clear();
                string depcode = this.txtDepCode.Text;
                dt = bllDepartment.GetDeptByParentDept(depcode);
                if (dt.Rows.Count > 1)
                {
                    alert = "alert('" + Message.operatedepartmentdelete + "')";
                }
                else
                {
                    dt = bllDepartment.GetWorkNoByDept(depcode);
                    if (dt.Rows.Count > 0)
                    {
                        alert = "alert('" + Message.extdepartmentdelete + "'+'" + dt.Rows[0]["WorkNo"].ToString() + "')";
                    }
                    else
                    {
                        string personcode = base.CurrentUserInfo.Personcode;
                        string levelcode = this.txtLevelCode.Text;
                        if (bllDepartment.GetUserDepLevel(personcode, levelcode).Rows.Count == 0)
                        {
                            alert = "alert('" + Message.NoPrivileged + "'+'" + levelcode + "')";
                        }
                        else
                        {
                            string companyid = base.CurrentUserInfo.CompanyId;
                            dt = bllDepartment.GetDeptByParent(companyid, depcode);
                            if (dt.Rows.Count > 0)
                            {
                                DepartmentModel newmodel = new DepartmentModel();
                                newmodel.CompanyId = companyid;
                                newmodel.DepCode = depcode;
                                bool flag = bllDepartment.Delete(newmodel,logmodel);
                                if (flag == true)
                                {
                                    alert = "alert('" + Message.DeleteSuccess + "')";
                                    //Query();
                                }
                                else
                                {
                                    alert = "alert('" + Message.DeleteFailed + "')";
                                }
                                goto Label_03F6;
                            }
                            else
                            {
                                alert = "alert('" + Message.NoItemSelected + "')";
                            }
                        }
                    }
                }
            }
            else
            {
                alert = "alert('" + Message.NoItemSelected + "')";
            }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "show", alert, true);
            return;
        Label_03F6:
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "show", alert, true);
            this.ProcessFlag.Value = "";
            Query();
        }
        #endregion

        #region 失效
        protected void btnDisable_Click(object sender, EventArgs e)
        {
            logmodel.ProcessFlag = "update";
            string alert = "";
            string depcode = this.txtDepCode.Text;
            dt = bllDepartment.GetWorkNoByDept(depcode);
            if (dt.Rows.Count > 0)
            {
                alert = "alert('" + Message.extdepartmentdelete + "'+'" + dt.Rows[0]["WorkNo"].ToString() + "')";
            }
            else
            {
                string companyid = base.CurrentUserInfo.CompanyId;
                dt.Clear();
                dt = bllDepartment.GetDeptByParent(companyid, depcode);
                if (dt.Rows.Count > 0)
                {
                    string personcode = base.CurrentUserInfo.Personcode;
                    string levelcode = this.txtLevelCode.Text;
                    if (bllDepartment.GetUserDepLevel(personcode, levelcode).Rows.Count == 0)
                    {
                        alert = "alert('" + Message.NoPrivileged + "'+'" + levelcode + "')";
                    }
                    else
                    {
                        bool flag = bllDepartment.Disable(companyid, depcode,logmodel);
                        if (flag == true)
                        {
                            alert = "alert('" + Message.DisableSuccess + "')";
                            Query();
                        }
                        else
                        {
                            alert = "alert('" + Message.DisableFailed + "')";
                        }
                    }
                }
            }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "show", alert, true);
            Query();
        }
        #endregion

        #region 生效
        protected void btnEnable_Click(object sender, EventArgs e)
        {
            logmodel.ProcessFlag = "update";
            string alert = "";
            string companyid = base.CurrentUserInfo.CompanyId;
            string depcdoe = this.txtDepCode.Text;
            dt.Clear();
            dt = bllDepartment.GetDept(companyid, depcdoe);
            if (dt.Rows.Count > 0)
            {
                string personcode = base.CurrentUserInfo.Personcode;
                string levelcode = this.txtLevelCode.Text;
                if (bllDepartment.GetUserDepLevel(personcode, levelcode).Rows.Count == 0)
                {
                    alert = "alert('" + Message.NoPrivileged + "'+'" + levelcode + "')";
                }
                else
                {
                    bool flag = bllDepartment.Enable(companyid, depcdoe,logmodel);
                    if (flag == true)
                    {
                        alert = "alert('" + Message.EnableSuccess + "')";
                        Query();
                    }
                    else
                    {
                        alert = "alert('" + Message.EnableFailed + "')";
                    }
                }
            }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "show", alert, true);
            Query();

        }
        #endregion

        #region 導出
        protected void btnExport_Click(object sender, EventArgs e)
        {
            string sql = base.SqlDep;
            string levelcode = this.ddlDepLevel.SelectedValue;
            dt = bllDepartment.GetDeaprtmentForExport(model, levelcode, sql);
            if (dt.Rows.Count == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "nodataexport", "alert('" + Message.NoDataExport + "');", true);
            }
            else
            {
                List<DepartmentModel> list = bllDepartment.GetList(dt);
                string[] header = { ControlText.gvHeadDepName, ControlText.gvCorporationName, ControlText.gvParentDepName, ControlText.gvHeadLevelName, ControlText.gvCostCode, ControlText.gvHeadOrderID, ControlText.gvAreaName, ControlText.gvAccountEntity, "SITEID", ControlText.gvDeptShortName, ControlText.gvDeptAlias, ControlText.gvFactoryCode };
                string[] properties = { "DepName1", "CorporationName", "ParentDepName", "LevelName", "CostCode", "OrderId", "AreaName", "AccountEntity", "SiteId", "DeptShortName", "DeptAlias", "FactoryCode" };
                string filePath = MapPath("~/ExportFileTemp/") + DateTime.Now.Ticks + ".xls";
                NPOIHelper.ExportExcel(list, header, properties, 5000, filePath);
                PageHelper.ReturnHTTPStream(filePath, true);
            }
        }
        #endregion

        #region ajax 驗證修改權限
        protected override void AjaxProcess()
        {
            if (!string.IsNullOrEmpty(Request.Form["LevelCode"]))
            {
                int flag = 0;
                string levelcode = Request.Form["LevelCode"];
                string personcode = base.CurrentUserInfo.Personcode;
                if (bllDepartment.GetUserDepLevel(personcode, levelcode).Rows.Count == 0)
                {
                    flag = 1;
                }
                Response.Clear();
                Response.Write(flag.ToString());
                Response.End();
            }
        }
        #endregion

        #region 保存
        protected void btnSave_Click(object sender, EventArgs e)
        {
            bool flag = false;
            if (this.CheckFeeNo())
            {
                if (!(this.txtAccountEntity.Text.Trim().Equals("N") || this.txtAccountEntity.Text.Trim().Equals("Y")))
                {
                    base.Response.Write("<script type='text/javascript'>alert(\"" + Message.AccountEntityError + "\");</script>");
                }
                else if (!bllDepartment.GetParaValue().Rows[0][0].ToString().Equals("Y") || this.CheckDepLevel())
                {
                    if (this.txtParentDepCode.Text.Length > 0)
                    {
                        dt.Clear();
                        dt = bllDepartment.GetDept(base.CurrentUserInfo.CompanyId, this.txtParentDepCode.Text);
                        if ((dt.Rows.Count > 0) && dt.Rows[0]["levelcode"].ToString().Equals(this.txtLevelCode.Text.Trim()))
                        {
                            base.Response.Write("<script type='text/javascript'>alert(\"" + Message.DepLevelError + "\");</script>");
                            goto Label_03F6;
                        }
                    }
                    if ((this.DepartmentValidated()) && this.LevelValidated())
                    {
                        if (this.txtCostCode.Text.Trim().Length > 0)
                        {
                            if (this.txtAreaCode.Text.Trim().Length == 0)
                            {
                                base.Response.Write("<script type='text/javascript'>alert(\"" + Message.CheckAreaCode + "\");</script>");
                                goto Label_03F6;
                            }
                            if (this.ProcessFlag.Value.Equals("Modify") && !((this.HiddenAreaCode.Value.Length <= 0) || this.HiddenAreaCode.Value.Equals(this.txtAreaCode.Text.Trim())))
                            {
                                base.Response.Write("<script type='text/javascript'>alert(\"" + Message.CheckAreaCodeChange + "\");</script>");
                                goto Label_03F6;
                            }
                        }
                        dt.Clear();
                        dt = bllDepartment.GetDept(base.CurrentUserInfo.CompanyId, txtDepCode.Text);
                        if (this.ProcessFlag.Value.Equals("Add"))
                        {
                            if (dt.Rows.Count > 0)
                            {
                                base.Response.Write("<script type='text/javascript'>alert(\"" + Message.DataIsExist + "\");</script>");
                                goto Label_03F6;
                            }
                            DepartmentModel newmodel = new DepartmentModel();
                            newmodel = PageHelper.GetModel<DepartmentModel>(pnlContent.Controls);
                            newmodel.DepCode = bllDepartment.GetMaxCode(newmodel.LevelCode, newmodel.ParentDepCode);
                            newmodel.CompanyId = base.CurrentUserInfo.CompanyId;
                            newmodel.Deleted = "N";
                            newmodel.CreateDate = System.DateTime.Now;
                            newmodel.CreateUser = base.CurrentUserInfo.Personcode;
                            logmodel.ProcessFlag = "insert";
                            flag = bllDepartment.AddDepartment(newmodel,logmodel);

                        }
                        else if (this.ProcessFlag.Value.Equals("Modify"))
                        {
                            if (dt.Rows.Count == 0)
                            {
                                base.Response.Write("<script type='text/javascript'>alert(\"" + Message.NoItemSelected + "\");</script>");
                                goto Label_03F6;
                            }
                            DepartmentModel newmodel = new DepartmentModel();
                            newmodel = PageHelper.GetModel<DepartmentModel>(pnlContent.Controls);
                            newmodel.DepCode = this.txtDepCode.Text;
                            newmodel.CompanyId = base.CurrentUserInfo.CompanyId;
                            newmodel.UpdateDate = System.DateTime.Now;
                            newmodel.UpdateUser = base.CurrentUserInfo.Personcode;
                            logmodel.ProcessFlag = "update";
                            flag = bllDepartment.UpdateDepartment(newmodel,logmodel);
                        }
                        if (flag == true)
                        {
                            string companyid = CurrentUserInfo.Personcode;
                            string module_code = Request.QueryString["ModuleCode"];
                            bllHrmEmpOtherMove.GetDepCodeTable(base.CurrentUserInfo.Personcode, module_code, companyid, txtLevelCode.Text, "Y",logmodel);
                            model = new DepartmentModel();
                            Query();
                            this.ProcessFlag.Value = "";
                            base.Response.Write("<script type='text/javascript'>alert(\"" + Message.DataSaveSuccess + "\");</script>");
                        }
                        else
                        {
                            base.Response.Write("<script type='text/javascript'>alert(\"" + Message.DataSaveFailed + "\");</script>");
                            goto Label_03F6;
                        }
                    }
                }
            }
            Label_03F6:
            if (flag == false)
            {
                this.hidOperate.Value = "Save";
            }
        }
        #endregion

        #region 檢查層級代碼
        private bool CheckDepLevel()
        {
            if ((this.txtParentDepCode.Text.Trim().Length == 0) && (this.txtLevelCode.Text.Trim() != "0"))
            {
                base.Response.Write("<script type='text/javascript'>alert(\"" + Message.DepLevelError + "\");</script>");
                return false;
            }
            if (this.txtParentDepCode.Text.Trim().Length > 0)
            {
                string depcode = txtParentDepCode.Text;
                string levelcode = txtLevelCode.Text;
                string strTemp = bllDepartment.GetOrderIdByDepCode(depcode).Rows[0]["row_id"].ToString();
                string strOrderID = bllDepartment.GetOrderIdBylevelcode(levelcode).Rows[0]["row_id"].ToString();
                try
                {
                    if (levelcode.Equals("3"))
                    {
                        if (!(strTemp.Equals("0") || (((Convert.ToInt32(strOrderID) - Convert.ToInt32(strTemp)) <= 6) && ((Convert.ToInt32(strOrderID) - Convert.ToInt32(strTemp)) >= -1))))
                        {
                            base.Response.Write("<script type='text/javascript'>alert(\"" + Message.DepLevelError + "\");</script>");
                            return false;
                        }
                    }
                    else if ((Convert.ToInt32(strOrderID) - Convert.ToInt32(strTemp)) != 1)
                    {
                        base.Response.Write("<script type='text/javascript'>alert(\"" + Message.DepLevelError + "\");</script>");
                        return false;
                    }
                }
                catch
                {
                }
                if (bllDepartment.CheckDepCodeAndLevelCode(depcode, levelcode).Rows.Count > 0)
                {
                    base.Response.Write("<script type='text/javascript'>alert(\"" + Message.DepLevelError + "\");</script>");
                    return false;
                }
            }
            return true;
        }
        #endregion

        #region 檢查會計實體，費用代碼
        private bool CheckFeeNo()
        {
            if (this.txtAccountEntity.Text.Trim().Equals("N"))
            {
                return true;
            }
            bool bValue = true;
            string flag = "";
            dt = bllDepartment.GetCostCode(txtCostCode.Text, txtAccountEntity.Text.Trim(), txtDepCode.Text);
            if (dt.Rows.Count > 0)
            {
                flag = dt.Rows[0][0].ToString();
            }
            bValue = string.IsNullOrEmpty(flag);
            if (!bValue)
            {
                //base.WriteMessage(2, base.GetResouseValue("bfw.department.errorcostcode"));
                base.Response.Write("<script type='text/javascript'>alert(\"" + Message.CostCodeError + "\");</script>");
            }
            return bValue;
        }
        #endregion

        #region GridView行處理
        private void DataUIBind()
        {
            this.UltraWebGridDepartment.DataSource = dt_global.DefaultView;
            this.UltraWebGridDepartment.DataBind();
            for (int i = 0; i < this.UltraWebGridDepartment.Rows.Count; i++)
            {
                if ((this.UltraWebGridDepartment.Rows[i].Cells.FromKey("DELETED").Value != null) && Convert.ToString(this.UltraWebGridDepartment.Rows[i].Cells.FromKey("DELETED").Value).Equals("Y"))
                {
                    this.UltraWebGridDepartment.Rows[i].Style.ForeColor = Color.Red;
                }
            }
        }
        #endregion

        #region 檢查組織
        private bool DepartmentValidated()
        {
            if ((this.ProcessFlag.Value.Equals("Add") || this.ProcessFlag.Value.Equals("Modify")) && (this.txtParentDepCode.Text.Trim().Length > 0))
            {
                string personcode = base.CurrentUserInfo.Personcode;
                string companyid = base.CurrentUserInfo.CompanyId;
                string modulecode = base.Request.QueryString["ModuleCode"];
                string depcode = txtDepCode.Text;
                string parentdepcode = txtParentDepCode.Text;
                if (bllDepartment.GetDepartmentByUser(personcode, companyid, modulecode, parentdepcode).Rows.Count == 0)
                {
                    base.Response.Write("<script type='text/javascript'>alert(\"" + Message.ParentDepCode + "\"+\"(" + this.txtParentDepCode.Text + ")\"+\"" + Message.NotExist + "\");</script>");
                    this.txtParentDepCode.Focus();
                    this.txtParentDepCode.ForeColor = Color.Red;
                    return false;
                }
                if (Convert.ToInt32(bllDepartment.GetCount(companyid, depcode, parentdepcode).Rows[0][0].ToString()) > 0)
                {
                    base.Response.Write("<script type='text/javascript'>alert(\"" + Message.ParentDeptError + "\");</script>");
                    this.txtParentDepCode.Focus();
                    this.txtParentDepCode.ForeColor = Color.Red;
                    return false;
                }
            }
            return true;
        }
        #endregion

        #region 檢查層級代碼是否存在
        private bool LevelValidated()
        {
            string personcode = base.CurrentUserInfo.Personcode;
            string levelcode = txtLevelCode.Text;
            if (((this.ProcessFlag.Value.Equals("Add") || this.ProcessFlag.Value.Equals("Modify")) && (bllDepartment.GetUserDepLevel(personcode, levelcode).Rows.Count == 0)))
            {
                base.Response.Write("<script type='text/javascript'>alert(\"" + Message.DepLevel + "\"+\"(" + this.txtLevelCode.Text + ")\"+\"" + Message.NotExist + "\");</script>");
                this.txtLevelCode.Focus();
                this.txtLevelCode.ForeColor = Color.Red;
                return false;
            }
            return true;
        }
        #endregion


    }
}
