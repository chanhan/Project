/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： KqmParameterEmpEditForm.aspx.cs
 * 檔功能描述： 考勤參數設定(單位)
 * 
 * 版本：1.0
 * 創建標識： 張明強 2011.12.13
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
using GDSBG.MiABU.Attendance.Model.KQM.BasicData;
using GDSBG.MiABU.Attendance.BLL.KQM.BasicData;
using System.Collections.Generic;
using Resources;
using System.Web.Script.Serialization;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;


namespace GDSBG.MiABU.Attendance.Web.KQM.BasicData
{
    public partial class KqmParameterEmpEditForm : BasePage
    {
        JavaScriptSerializer JsSerializer = new JavaScriptSerializer();
        Dictionary<string, string> ClientMessage = null;
        BellCardBll BellCardBll = new BellCardBll();
        KqmParameterEmpEditBll paramsEmpEditBll = new KqmParameterEmpEditBll();
        KqmParameterEmpBll paramsEmpBll = new KqmParameterEmpBll();
        AttKQParamsEmpEditModel model;
        AttKQParamsEmpModel empModel;
        static DataTable tempDataTable;
        static DataTable tempEmpTable;
        static SynclogModel logmodel = new SynclogModel();

        #region 頁面加載方法
        /// <summary>
        /// 頁面加載方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            //頁面按鈕添加權限
            PageHelper.ButtonControls(base.FuncList, pnlShowPanel.Controls, base.FuncListModule);

            //彈框內容
            if (ClientMessage == null)
            {
                ClientMessage = new Dictionary<string, string>();
                ClientMessage.Add("AddFailed", Message.AddFailed);
                ClientMessage.Add("AddSuccess", Message.AddSuccess);
                ClientMessage.Add("UpdateSuccess", Message.UpdateSuccess);
                ClientMessage.Add("UpdateFailed", Message.UpdateFailed);
                ClientMessage.Add("DeleteFailed", Message.DeleteFailed);
                ClientMessage.Add("DeleteSuccess", Message.DeleteSuccess);
                ClientMessage.Add("StringFormNotRight", Message.StringFormNotRight);
                ClientMessage.Add("AtLastOneChoose", Message.AtLastOneChoose);
                ClientMessage.Add("ConditionNotNull", Message.ConditionNotNull);
                ClientMessage.Add("SaveConfim", Message.SaveConfim);
                ClientMessage.Add("RulesDeleteConfirm", Message.RulesDeleteConfirm);
                ClientMessage.Add("RulesNotNumber", Message.RulesNotNumber);
                ClientMessage.Add("EmpBasicInfoNotExist", Message.EmpBasicInfoNotExist);
                ClientMessage.Add("WorkNoNotNull", Message.WorkNoNotNull);
                ClientMessage.Add("KQMIsNotKaoQin", Message.KQMIsNotKaoQin);
            }
            string clientmsg = JsSerializer.Serialize(ClientMessage);
            Page.ClientScript.RegisterStartupScript(GetType(), "ClientMessage", "var Message=" + clientmsg + ";", true);

            if (!this.Page.IsPostBack)
            {
                logmodel.ProcessOwner = CurrentUserInfo.Personcode;
                logmodel.TransactionType = (Request.QueryString["ModuleCode"] == null) ? "" : Request.QueryString["ModuleCode"].ToString();
                logmodel.LevelNo = "2";
                logmodel.FromHost = Request.UserHostAddress;
                this.ddlBellNoDataBind();
                string EmployeeNo = (base.Request.QueryString["EmployeeNo"] == null) ? "" : base.Request.QueryString["EmployeeNo"].ToString();
                this.HiddenEmployeeNo.Value = EmployeeNo;
                if (EmployeeNo.ToString().Length != 0)
                {
                    this.EmpQuery(EmployeeNo);
                }
                this.HiddenEmpNotExist.Value = "";
            }
        }

        #endregion

        #region 頁面按鈕事件

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            this.ddlBellNoDataBind();
            string EmployeeNo = (base.Request.QueryString["EmployeeNo"] == null) ? "" : base.Request.QueryString["EmployeeNo"].ToString();
            this.HiddenEmployeeNo.Value = EmployeeNo;
            if (EmployeeNo.ToString().Length != 0)
            {
                this.EmpQuery(EmployeeNo);
            }
            this.HiddenEmpNotExist.Value = "";
            this.hidOperate.Value = "";
        }



        /// <summary>
        /// 刪除按鈕事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            logmodel.ProcessFlag = "delete";
            string alert = "";
            model = new AttKQParamsEmpEditModel();
            string employeeNo = this.txtEmployeeNo.Text.ToString().Trim();
            model.WorkNo = employeeNo;

            if (paramsEmpEditBll.DeleteKQMParamsEmpData(model,logmodel) > 0)
            {
                alert = "alert('" + Message.DeleteSuccess + "')";
            }
            else
            {
                alert = "alert('" + Message.DeleteFailed + "')";
            }


            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "show", alert, true);
            model = new AttKQParamsEmpEditModel();
            this.EmpQuery(employeeNo);
            this.hidOperate.Value = "";
        }

        /// <summary>
        /// 查詢按鈕事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            this.HiddenEmployeeNo.Value = this.txtEmployeeNo.Text.ToUpper().Trim();
            this.EmpQuery(this.txtEmployeeNo.Text.ToUpper());
            this.ProcessFlag.Value = "";
        }

        /// <summary>
        /// 保存按鈕事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string actionFlag = this.hidOperate.Value.ToString().Trim();
            bool result = false;
            string alert = "";
            string employeeNo = this.txtEmployeeNo.Text.ToString().Trim();

            model = new AttKQParamsEmpEditModel();
            model.WorkNo = employeeNo;
            model.BellNo = this.ddlBellNo.SelectedValuesToString().Replace(", ", ",");
            model.IsNotKaoQin = this.ddlIsNotKaoQin.SelectedValue.ToString();

            //新增
            if (actionFlag == "add")
            {
                logmodel.ProcessFlag = "insert";
                model.CreateUser = CurrentUserInfo.Personcode;
                model.UpdateUser = CurrentUserInfo.Personcode;
                model.UpdateDate = System.DateTime.Now;
                model.CreateDate = System.DateTime.Now;
                result = paramsEmpEditBll.AddKQMParamsEmpData(model,logmodel);
                if (result == true)
                {
                    alert = "alert('" + Message.AddSuccess + "')";
                }
                else
                {
                    alert = "alert('" + Message.AddFailed + "')";
                }
                this.hidOperate.Value = "";

            }
            //修改
            if (actionFlag == "modify")
            {
                logmodel.ProcessFlag = "update";
                model.UpdateUser = CurrentUserInfo.Personcode;
                model.UpdateDate = System.DateTime.Now;
                result = paramsEmpEditBll.UpdateKQMParamsEmpByKey(model,logmodel);
                if (result == true)
                {
                    alert = "alert('" + Message.UpdateSuccess + "')";
                }
                else
                {
                    alert = "alert('" + Message.UpdateFailed + "')";
                }

                this.hidOperate.Value = "";

            }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "show", alert, true);
            model = new AttKQParamsEmpEditModel();
            this.EmpQuery(employeeNo);
            this.hidOperate.Value = "";
        }

        #endregion

        #region 卡鐘下拉列表綁定數據
        /// <summary>
        /// 卡鐘下拉列表綁定數據
        /// </summary>
        protected void ddlBellNoDataBind()
        {
            DataTable bellCardInfo = new DataTable();
            BellCardModel bellCardModel = new BellCardModel();
            bellCardModel.BellType = "KQM";
            bellCardModel.EffectFlag = "Y";
            bellCardInfo = BellCardBll.GetKQMBellCard(bellCardModel);
            this.ddlBellNo.DataSource = bellCardInfo.DefaultView;
            this.ddlBellNo.DataTextField = "BELLNO";
            this.ddlBellNo.DataValueField = "BELLNO";
            this.ddlBellNo.DataBind();
        }
        #endregion

        #region 查詢
        /// <summary>
        /// 查詢
        /// </summary>
        /// <param name="EmployeeNo"></param>
        private void EmpQuery(string employeeNo)
        {
            string sqlDep = base.SqlDep;

            tempEmpTable = paramsEmpEditBll.GetVDataByCondition(employeeNo, sqlDep);
            if (tempEmpTable != null)
            {
                if (tempEmpTable.Rows.Count > 0)
                {
                    foreach (DataRow newRow in tempEmpTable.Rows)
                    {
                        this.txtEmployeeNo.Text = newRow["WORKNO"].ToString().ToUpper();
                        this.txtChineseName.Text = newRow["LOCALNAME"].ToString();
                        this.txtSYC.Text = newRow["Syc"].ToString();
                        this.txtSYB.Text = newRow["DName"].ToString();
                        this.HiddenOrgCode.Value = newRow["DepCode"].ToString();
                    }
                    this.Query(employeeNo);
                }
                else
                {
                    this.HiddenEmpNotExist.Value = "no";
                    string alert = "alert('" + Message.EmpBasicInfoNotExist + "')";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "show", alert, true);
                }
            }
            else
            {
                this.HiddenEmpNotExist.Value = "no";
                string alert = "alert('" + Message.EmpBasicInfoNotExist + "')";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "show", alert, true);
            }

        }


        /// <summary>
        /// 查詢
        /// </summary>
        /// <param name="EmployeeNo"></param>
        private void Query(string EmployeeNo)
        {
            empModel = new AttKQParamsEmpModel();
            empModel.WorkNo = this.HiddenEmployeeNo.Value.ToString().Trim();
            empModel.OrgCode = this.HiddenOrgCode.Value;
            tempDataTable = paramsEmpBll.GetKQMParamsEmpData(empModel, base.SqlDep.ToString());
            this.UltraWebGrid.DataSource = tempDataTable.DefaultView;
            this.UltraWebGrid.DataBind();

            if (tempDataTable.Rows.Count > 0)
            {
                if (this.UltraWebGrid.DisplayLayout.SelectedRows.Count == 0)
                {
                    this.UltraWebGrid.Rows[0].Selected = true;
                }
                int intSelect = this.UltraWebGrid.DisplayLayout.SelectedRows[0].Index;
                this.ddlIsNotKaoQin.SelectedValue = (this.UltraWebGrid.Rows[intSelect].Cells[2].Text == null) ? "" : this.UltraWebGrid.Rows[intSelect].Cells[2].Text.Trim();
                string[] temVal = null;
                temVal = (this.UltraWebGrid.Rows[intSelect].Cells[1].Text == null) ? null : this.UltraWebGrid.Rows[intSelect].Cells[1].Text.Split(new char[] { ',' });
                if (temVal != null)
                {
                    for (int iLoop = 0; iLoop < temVal.Length; iLoop++)
                    {
                        for (int j = 0; j < this.ddlBellNo.Items.Count; j++)
                        {
                            if (object.Equals(this.ddlBellNo.Items[j].Value.Trim(), temVal[iLoop].Trim()))
                            {
                                this.ddlBellNo.Items[j].Selected = true;
                            }
                        }
                    }
                }
                this.HiddenIsNotKaoQin.Value = (this.UltraWebGrid.Rows[intSelect].Cells[2].Text == null) ? "" : this.UltraWebGrid.Rows[intSelect].Cells[2].Text.Trim();
                this.HiddenBellNo.Value = (this.UltraWebGrid.Rows[intSelect].Cells[1].Text == null) ? null : this.UltraWebGrid.Rows[intSelect].Cells[1].Text;
                if (this.ddlIsNotKaoQin.SelectedValue == "")
                {
                    this.UltraWebGrid.DataSource = null;
                    this.UltraWebGrid.DataBind();
                }

                this.HiddenEmpNotExist.Value = "";
            }
            else
            {
                this.ddlIsNotKaoQin.SelectedValue = "";
                this.ddlBellNo.ClearSelection();
                this.HiddenEmpNotExist.Value = "no";

            }

        }
        #endregion

        #region 返回頁面狀態
        /// <summary>
        /// 返回頁面狀態
        /// </summary>
        /// <returns></returns>
        public int GetTableNum()
        {
            int num = 0;
            if (this.UltraWebGrid.DisplayLayout.Rows.Count > 0)
            {
                num = 1;
            }
            else if (txtEmployeeNo.Text.Length == 0)
            {
                num = -1;
            }
            return num;
        }
        #endregion
    }
}
