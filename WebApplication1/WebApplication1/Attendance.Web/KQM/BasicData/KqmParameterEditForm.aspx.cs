/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： KqmParameterEditForm.aspx.cs
 * 檔功能描述： 考勤參數設定(單位)
 * 
 * 版本：1.0
 * 創建標識： 張明強 2011.12.13
 * 
 */

using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Script.Serialization;
using System.Web.UI;
using GDSBG.MiABU.Attendance.BLL.KQM.BasicData;
using GDSBG.MiABU.Attendance.Model.KQM.BasicData;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;
using Resources;
namespace GDSBG.MiABU.Attendance.Web.KQM.BasicData
{
    public partial class KqmParameterEditForm : BasePage
    {
        JavaScriptSerializer JsSerializer = new JavaScriptSerializer();
        Dictionary<string, string> ClientMessage = null;
        BellCardBll BellCardBll = new BellCardBll();
        KqmParameterEditBll ParamsEditBll = new KqmParameterEditBll();
        AttKQParamsOrgModel model;
        DataTable ParamsOrgInfo;
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
                string orgCode = (base.Request.QueryString["OrgCode"] == null) ? "" : base.Request.QueryString["OrgCode"].ToString();
                if (orgCode.ToString().Length != 0)
                {
                    Query(orgCode);
                }
            }

        }
        #endregion

        #region 頁面按鈕事件
        /// <summary>
        /// 刪除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            logmodel.ProcessFlag = "delete";
            string alert = "";
            model = new AttKQParamsOrgModel();
            string orgCode = (base.Request.QueryString["OrgCode"] == null) ? "" : base.Request.QueryString["OrgCode"].ToString();
            //orgCode = "test004";
            model.OrgCode = orgCode;

            if (ParamsEditBll.DeleteKQMParamsOrgData(model,logmodel) > 0)
            {
                alert = "alert('" + Message.DeleteSuccess + "')";
            }
            else
            {
                alert = "alert('" + Message.DeleteFailed + "')";
            }

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "show", alert, true);
            model = new AttKQParamsOrgModel();
            this.Query(orgCode);
            this.hidOperate.Value = "";
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string actionFlag = this.hidOperate.Value.ToString().Trim();
            bool result = false;
            string alert = "";
            string orgCode = (base.Request.QueryString["OrgCode"] == null) ? "" : base.Request.QueryString["OrgCode"].ToString();
            // orgCode = "test004";
            model = new AttKQParamsOrgModel();
            model.OrgCode = orgCode;
            model.BellNo = this.ddlBellNo.SelectedValuesToString().Replace(", ", ",");
            model.IsNotKaoQin = this.ddlIsNotKaoQin.SelectedValue.ToString();
            

            if (ParamsEditBll.GetValue("IsAllowNotKaoQin",null).Equals("N") && this.ddlIsNotKaoQin.SelectedValue.Equals("Y"))
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "show", "alert('" + Message.NotAllowNotKaoQin + "')", true);
            }
            else
            {

                //新增
                if (actionFlag == "add")
                {
                    logmodel.ProcessFlag = "insert";
                    model.CreateUser = CurrentUserInfo.Personcode;
                    model.UpdateUser = CurrentUserInfo.Personcode;
                    model.UpdateDate = System.DateTime.Now;
                    model.CreateDate = System.DateTime.Now;
                    result = ParamsEditBll.AddKQMParamsOrgData(model,logmodel);
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
                    result = ParamsEditBll.UpdateKQMParamsOrgByKey(model,logmodel);
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
            }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "show", alert, true);
            model = new AttKQParamsOrgModel();
            this.Query(orgCode);
            this.hidOperate.Value = "";
        }

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            this.ddlBellNoDataBind();
            string orgCode = (base.Request.QueryString["OrgCode"] == null) ? "" : base.Request.QueryString["OrgCode"].ToString();
            if (orgCode.ToString().Length != 0)
            {
                Query(orgCode);
            }
            this.hidOperate.Value = "";
        }

        #endregion

        #region  頁面綁定數據
        /// <summary>
        /// 頁面UI數據綁定
        /// </summary>
        /// <param name="orgCode"></param>
        private void DataUIBind(string orgCode)
        {
            ParamsOrgInfo = new DataTable();
            if (orgCode != "")
            {
                model = new AttKQParamsOrgModel();
                model.OrgCode = orgCode;
                ParamsOrgInfo = ParamsEditBll.GetKQMParamsOrgData(model);
            }
            //WebGrid綁定數據
            this.UltraWebGrid.DataSource = ParamsOrgInfo.DefaultView;
            this.UltraWebGrid.DataBind();

            //下拉列表綁定數據
            if (ParamsOrgInfo.Rows.Count > 0)
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
            }
            else
            {
                this.ddlIsNotKaoQin.SelectedValue = "";
                this.ddlBellNo.ClearSelection();
            }
        }

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
        /// <param name="orgCode"></param>
        private void Query(string orgCode)
        {
            //this.txtOrgCode.Text = (base.Request.QueryString["OrgName"] == null) ? "" : base.Request.QueryString["OrgName"].ToString();
            string depName = ParamsEditBll.GetDepName(base.Request.QueryString["OrgCode"].Trim());
            this.txtOrgCode.Text = depName;
            DataUIBind(orgCode);
        }

        #endregion

        #region 返回頁面狀態至前臺
        /// <summary>
        /// 返回頁面狀態至前臺
        /// </summary>
        /// <returns></returns>
        public int GetTableNum()
        {
            int num = 0;
            if (this.UltraWebGrid.DisplayLayout.Rows.Count > 0)
            {
                num = 1;
            }
            return num;
        }
        #endregion
    }
}
