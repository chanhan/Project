/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： KqmRules.aspx.cs
 * 檔功能描述： 缺勤規則設定
 * 
 * 版本：1.0
 * 創建標識： 張明強 2011.12.9
 * 
 */
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using GDSBG.MiABU.Attendance.Model.KQM.BasicData;
using GDSBG.MiABU.Attendance.BLL.KQM.BasicData;
using System.Collections.Generic;
using Resources;
using System.Web.Script.Serialization;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;

namespace GDSBG.MiABU.Attendance.Web.KQM.BasicData
{
    public partial class KqmRules : BasePage
    {
        JavaScriptSerializer JsSerializer = new JavaScriptSerializer();
        Dictionary<string, string> ClientMessage = null;
        KqmRulesBll rulesBll = new KqmRulesBll();
        TypeDataBll typeDataBll = new TypeDataBll();
        static KqmRulesModel model;
        DataTable tempTable;
        static SynclogModel logmodel = new SynclogModel();


        #region 頁面載入
        protected void Page_Load(object sender, EventArgs e)
        {
            //頁面按鈕添加權限
            PageHelper.ButtonControls(base.FuncList, pnlShowPanel.Controls, base.FuncListModule);

            //日曆文本框
            SetCalendar(txtEffectDate, txtExpireDate);

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
                ClientMessage.Add("WrongDate", Message.WrongDate);
                ClientMessage.Add("ToLaterThanFrom", Message.ToLaterThanFrom);
                ClientMessage.Add("NecessayNotNull", Message.NecessayNotNull);
            }
            string clientmsg = JsSerializer.Serialize(ClientMessage);
            Page.ClientScript.RegisterStartupScript(GetType(), "ClientMessage", "var Message=" + clientmsg + ";", true);
            if (!IsPostBack)
            {
                logmodel.ProcessOwner = CurrentUserInfo.Personcode;
                logmodel.TransactionType = (Request.QueryString["ModuleCode"] == null) ? "" : Request.QueryString["ModuleCode"].ToString();
                logmodel.LevelNo = "2";
                logmodel.FromHost = Request.UserHostAddress;
                pager.CurrentPageIndex = 1;
                model = new KqmRulesModel();
                //綁定數據
                RulesDataBind();
                //綁定缺勤類型下拉列表
                AbsentTypeDataBind();
                //綁定懲罰類型下拉列表
                PunishTypeDataBind();
            }
        }

        #endregion

        #region WebGrid數據綁定事件
        /// <summary>
        /// WebGrid數據綁定事件,控制失效數據顯示紅色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void UltraWebGrid_DataBound(object sender, EventArgs e)
        {
            for (int i = 0; i < UltraWebGrid.Rows.Count; i++)
            {
                if (UltraWebGrid.Rows[i].Cells.FromKey("ExpireDate").Text != null)
                {
                    DateTime expiredate = Convert.ToDateTime(UltraWebGrid.Rows[i].Cells.FromKey("ExpireDate").Text.Trim());
                    if (expiredate.CompareTo(DateTime.Now) <= 0)
                    {
                        UltraWebGrid.Rows[i].Style.ForeColor = System.Drawing.Color.Red;
                    }
                }
               
            }
        }
        #endregion

        #region 綁定數據
        /// <summary>
        /// 綁定數據
        /// </summary>
        private void RulesDataBind()
        {
            int totalCount = 0;
            tempTable = rulesBll.GetKqmRulesList(model, pager.CurrentPageIndex, pager.PageSize, out totalCount);
            //if (tempTable.Rows.Count == 0)
            //{
            //    tempTable.Rows.Add(tempTable.NewRow());
            //}
            pager.RecordCount = totalCount;
            this.UltraWebGrid.DataSource = tempTable.DefaultView;
            this.UltraWebGrid.DataBind();
            pager.TextAfterPageIndexBox = "/" + pager.PageCount.ToString();

        }

        #endregion

        #region 頁面按鈕事件
        /// <summary>
        /// 查詢按鈕事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            string actionFlag = this.hidOperate.Value.ToString().Trim();
            //未點擊"條件",查詢全部信息
            if (actionFlag == "")
            {
                model = new KqmRulesModel();
            }
            //點擊"條件",依據條件查詢
            else if (actionFlag == "condition")
            {
                model = PageHelper.GetModel<KqmRulesModel>(pnlContent.Controls);

            }
            RulesDataBind();
            pager.CurrentPageIndex = 1;
            this.hidOperate.Value = "";
            this.IDRules.Value = "";
        }

        /// <summary>
        /// 保存按鈕事件--實現新增和修改功能
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string actionFlag = this.hidOperate.Value.ToString().Trim();
            model = PageHelper.GetModel<KqmRulesModel>(pnlContent.Controls);
            bool result = false;
            string alert = "";
            //新增
            if (actionFlag == "add")
            {
                logmodel.ProcessFlag = "insert";
                model.CreateUser = CurrentUserInfo.Personcode;
                model.UpdateUser = CurrentUserInfo.Personcode;
                model.CreateDate = System.DateTime.Now;
                model.UpdateDate = System.DateTime.Now;
                result = rulesBll.AddKqmRules(model,logmodel);
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
                model.ID = this.IDRules.Value.ToString().Trim();
                result = rulesBll.UpdateRulesByKey(model,logmodel);
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
            PageHelper.CleanControlsValue(pnlContent.Controls);
            model = new KqmRulesModel();
            RulesDataBind();

            pager.CurrentPageIndex = 1;
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
            model = new KqmRulesModel();
            model.ID = this.IDRules.Value.ToString().Trim();
           
            if (rulesBll.DeleteRules(model,logmodel) > 0)
            {
                alert = "alert('" + Message.DeleteSuccess + "')";
            }
            else
            {
                alert = "alert('" + Message.DeleteFailed + "')";
            }


            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "show", alert, true);
            PageHelper.CleanControlsValue(pnlContent.Controls);
            model = new KqmRulesModel();
            RulesDataBind();
            pager.CurrentPageIndex = 1;
        }
        #endregion

        #region 分頁
        protected void pager_PageChanged(object sender, EventArgs e)
        {
            RulesDataBind();
        }
        #endregion

        #region  缺勤類型下拉列表綁定數據
        /// <summary>
        /// 缺勤下拉列表綁定數據
        /// </summary>
        protected void AbsentTypeDataBind()
        {
            DataTable absentTable = new DataTable();
            absentTable = typeDataBll.GetTypeDataList("AbsentType");
            this.ddlAbsentType.DataSource = absentTable;
            this.ddlAbsentType.DataValueField = "DATACODE";
            this.ddlAbsentType.DataTextField = "DATAVALUE";
            this.ddlAbsentType.DataBind();
            this.ddlAbsentType.Items.Insert(0, new ListItem("", ""));
            this.ddlAbsentType.SelectedIndex = this.ddlAbsentType.Items.IndexOf(this.ddlAbsentType.Items.FindByValue("0"));
        }
        #endregion

        #region 懲罰類型下拉列表綁定數據
        /// <summary>
        /// 懲罰類型下拉列表綁定數據
        /// </summary>
        protected void PunishTypeDataBind()
        {
            DataTable punishTable = new DataTable();
            punishTable = typeDataBll.GetTypeDataList("PunishType");
            this.ddlPunishType.DataSource = punishTable;
            this.ddlPunishType.DataValueField = "DATACODE";
            this.ddlPunishType.DataTextField = "DATAVALUE";
            this.ddlPunishType.DataBind();
            this.ddlPunishType.Items.Insert(0, new ListItem("", ""));
            this.ddlPunishType.SelectedIndex = this.ddlPunishType.Items.IndexOf(this.ddlPunishType.Items.FindByValue("0"));
        }
        #endregion

    }
}
