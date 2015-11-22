
/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： Module.aspx.cs
 * 檔功能描述： 系統模組
 * 
 * 版本：1.0
 * 創建標識： 張明強 2011.11.28
 * 
 */

using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using GDSBG.MiABU.Attendance.BLL.SystemManage.SystemSafety;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;
using Resources;
using GDSBG.MiABU.Attendance.Common;
namespace GDSBG.MiABU.Attendance.Web.SystemManage.SystemSafety
{

    public partial class Module : BasePage
    {
        ModuleBll bll = new ModuleBll();
        static ModuleModel model = new ModuleModel();
        JavaScriptSerializer JsSerializer = new JavaScriptSerializer();
        Dictionary<string, string> ClientMessage = null;
        DataTable tempTable = new DataTable();
        static List<ModuleModel> ExportList = new List<ModuleModel>();
        static SynclogModel logmodel = new SynclogModel();

        #region 頁面加載
        /// <summary>
        /// 頁面加載
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            //頁面按鈕添加權限
            PageHelper.ButtonControls(base.FuncList, pnlShowPanel.Controls, base.FuncListModule);
            //頁面彈框顯示信息
            if (ClientMessage == null)
            {
                ClientMessage = new Dictionary<string, string>();
                ClientMessage.Add("AddFailed", Message.AddFailed);
                ClientMessage.Add("AddSuccess", Message.AddSuccess);
                ClientMessage.Add("DisableFailed", Message.DisableFailed);
                ClientMessage.Add("DisableSuccess", Message.DisableSuccess);
                ClientMessage.Add("EnableFailed", Message.EnableFailed);
                ClientMessage.Add("EnableSuccess", Message.EnableSuccess);
                ClientMessage.Add("UpdateSuccess", Message.UpdateSuccess);
                ClientMessage.Add("UpdateFailed", Message.UpdateFailed);
                ClientMessage.Add("DeleteFailed", Message.DeleteFailed);
                ClientMessage.Add("DeleteSuccess", Message.DeleteSuccess);
                ClientMessage.Add("TextBoxNotNull", Message.TextBoxNotNull);
                ClientMessage.Add("DeleteNotNull", Message.DeleteNotNull);
                ClientMessage.Add("ConditionNotNull", Message.ConditionNotNull);
                ClientMessage.Add("OrderIDNotNumber", Message.OrderIDNotNumber);
                ClientMessage.Add("DisableConfirm", Message.DisableConfirm);
                ClientMessage.Add("AtLastOneChoose", Message.AtLastOneChoose);
                ClientMessage.Add("SaveConfim", Message.SaveConfim);
                ClientMessage.Add("ModuleCodeIsExist", Message.ModuleCodeIsExist);
                ClientMessage.Add("DeleteConfirm", Message.DeleteConfirm);
                ClientMessage.Add("EnableConfirm", Message.EnableConfirm);
                ClientMessage.Add("ModuleCodeEquleParent", Message.ModuleCodeEquleParent);
            }
            string clientmsg = JsSerializer.Serialize(ClientMessage);
            Page.ClientScript.RegisterStartupScript(GetType(), "ClientMessage", "var Message=" + clientmsg + ";", true);



            //設置Selector----Module
            SetSelector(imgbtnParentmodulecode, txtParentModuleCode, txtParentModuleName, "module");

            //設置Selector----Privileged
            SetSelector(imgbtnPrivileged, txtPrivileged, txtPrivilegedName, "privileged");
            if (!IsPostBack)
            {
                logmodel.ProcessOwner = CurrentUserInfo.Personcode;
                logmodel.TransactionType = (Request.QueryString["ModuleCode"] == null) ? "" : Request.QueryString["ModuleCode"].ToString();
                logmodel.LevelNo = "2";
                logmodel.FromHost = Request.UserHostAddress;
                pager.CurrentPageIndex = 1;
                //綁定數據

                ModuleDataBind();

            }

        }
        #endregion

        #region 頁面顯示數據
        /// <summary>
        /// 綁定模組數據
        /// </summary>
        private void ModuleDataBind()
        {
            if (this.hidSelectFlag.Value.ToString() == "select")
            {
                model = PageHelper.GetModel<ModuleModel>(pnlContent.Controls);
            }
            int totalCount = 0;
            tempTable = bll.GetUserModuleList(model, pager.CurrentPageIndex, pager.PageSize, out totalCount);
            ExportList = bll.GetUserModuleList(model);
            //當datatable數據為空時,為其添加空行
            //if (tempTable.Rows.Count == 0)
            //{
            //    tempTable.Rows.Add(tempTable.NewRow());
            //}
            pager.RecordCount = totalCount;
            UltraWebGridModule.DataSource = tempTable.DefaultView;
            UltraWebGridModule.DataBind();
            pager.TextAfterPageIndexBox = "/" + pager.PageCount.ToString();

        }
        #endregion

        #region 頁面按鈕功能實現

        /// <summary>
        /// 查詢操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            string actionFlag = this.hidOperate.Value.ToString().Trim();
            pager.CurrentPageIndex = 1;
            //不點擊"條件",查詢全部信息
            if (actionFlag != "condition")
            {
                PageHelper.CleanControlsValue(pnlContent.Controls);
                model = new ModuleModel();
            }
            else
            {
                if (this.hidSelectFlag.Value.ToString() == "select")
                {
                    model = PageHelper.GetModel<ModuleModel>(pnlContent.Controls);
                }
            }
            ModuleDataBind();
            this.hidOperate.Value = "";
        }



        /// <summary>
        /// 重載Ajax方法
        /// </summary>
        protected override void AjaxProcess()
        {

            bool blPro = false;
            bool result = false;
            int flag = 0;
            string actionFlag = Request.Form["actionFlag"];

            //新增之前判斷是否重複主鍵
            if (actionFlag == "add")
            {
                string moduleCode = Request.Form["moduleCode"];
                ModuleModel tempMoudule = new ModuleModel();
                tempMoudule = bll.GetModuleByKey(moduleCode);
                if (tempMoudule == null)
                {
                    result = true;
                }
                //this.hidOperate.Value = "";
                blPro = true;
            }
            if (blPro)
            {
                if (result)
                {
                    flag = 1;
                }
                Response.Clear();
                Response.Write(flag);
                Response.End();
            }
        }


        /// <summary>
        /// 失效
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDisable_Click(object sender, EventArgs e)
        {
            string alert = "";
            logmodel.ProcessFlag = "update";
            model.UpdateDate = System.DateTime.Now;
            model.UpdateUser = CurrentUserInfo.Personcode;
            model.ModuleCode = this.hidModuleCode.Value.Trim();
            model.Deleted = "Y";
            bool result = bll.UpdateModuleByKey(model, true, logmodel);
            if (result == true)
            {
                alert = "alert('" + Message.DisableSuccess + "')";
            }
            else
            {
                alert = "alert('" + Message.DisableFailed + "')";
            }
            this.hidOperate.Value = "";
            this.hidModuleCode.Value = "";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "show", alert, true);
            PageHelper.CleanControlsValue(pnlContent.Controls);
            model = new ModuleModel();
            ModuleDataBind();


        }

        /// <summary>
        /// 生效
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnEnable_Click(object sender, EventArgs e)
        {
            string alert = "";
            logmodel.ProcessFlag = "update";
            model.UpdateDate = System.DateTime.Now;
            model.UpdateUser = CurrentUserInfo.Personcode;
            model.ModuleCode = this.hidModuleCode.Value.Trim();
            model.Deleted = "N";
            bool result = bll.UpdateModuleByKey(model, true, logmodel);
            if (result == true)
            {
                alert = "alert('" + Message.EnableSuccess + "')";
            }
            else
            {
                alert = "alert('" + Message.EnableFailed + "')";
            }
            this.hidOperate.Value = "";
            this.hidModuleCode.Value = "";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "show", alert, true);
            PageHelper.CleanControlsValue(pnlContent.Controls);
            model = new ModuleModel();
            ModuleDataBind();
        }


        /// <summary>
        /// 新增/修改之後執行保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            bool result = false;
            string alert = "";
            string actionFlag = this.hidOperate.Value.ToString().Trim();
            //新增
            if (actionFlag == "add")
            {
                logmodel.ProcessFlag = "insert";
                model = PageHelper.GetModel<ModuleModel>(pnlContent.Controls);
                model.CreateDate = System.DateTime.Today.Date;
                model.CreateUser = CurrentUserInfo.Personcode;
                model.Deleted = "N";
                result = bll.AddModule(model, logmodel);
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
                model = PageHelper.GetModel<ModuleModel>(pnlContent.Controls);
                model.UpdateDate = System.DateTime.Now;
                model.UpdateUser = CurrentUserInfo.Personcode;
                model.Deleted = this.hidDeleted.Value.ToString();
                result = bll.UpdateModuleByKey(model, false, logmodel);
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
            model = new ModuleModel();
            ModuleDataBind();

        }


        /// <summary>
        /// 刪除按鈕事件--刪除模組
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            logmodel.ProcessFlag = "delete";
            string alert = "";
            string actionFlag = this.hidOperate.Value.ToString().Trim();
            //刪除
            if (actionFlag == "delete")
            {
                model.ModuleCode = this.txtModuleCode.Text.ToString().Trim();
                if (bll.DeleteModuleByKey(model.ModuleCode, logmodel) > 0)
                {
                    alert = "alert('" + Message.DeleteSuccess + "')";
                }
                else
                {
                    alert = "alert('" + Message.DeleteFailed + "')";
                }
                this.hidOperate.Value = "";
            }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "show", alert, true);
            PageHelper.CleanControlsValue(pnlContent.Controls);
            model = new ModuleModel();
            ModuleDataBind();
        }

        #endregion

        #region 設置放大鏡頁面,用於輔助選擇
        /// <summary>
        /// 設置Selector
        /// </summary>
        /// <param name="ctrlTrigger">控件ID--按鈕</param>
        /// <param name="ctrlCode">控件ID--文本框1</param>
        /// <param name="ctrlName">控件ID--文本框2</param>
        /// <param name="flag">Selector區分標誌</param>
        public void SetSelector(WebControl ctrlTrigger, Control ctrlCode, Control ctrlName, string flag)
        {
            if (ctrlCode is TextBox) { (ctrlCode as TextBox).Attributes.Add("readonly", "readonly"); }
            if (ctrlName is TextBox) { (ctrlName as TextBox).Attributes.Add("readonly", "readonly"); }
            ctrlTrigger.Attributes.Add("onclick", string.Format("return setSelector('{0}','{1}','{2}')",
                ctrlCode.ClientID, ctrlName.ClientID, flag));
        }

        #endregion

        #region 分頁
        protected void pager_PageChanged(object sender, EventArgs e)
        {
            ModuleDataBind();
        }

        #endregion

        #region 導出EXCEL文檔
        /// <summary>
        /// 導出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnExport_Click(object sender, EventArgs e)
        {
            if (this.UltraWebGridModule.Rows.Count == 0)
            {
                Page.ClientScript.RegisterClientScriptBlock(GetType(), "show", "alert('" + Message.NoDataExport + "')", true);
                return;
            }
            else
            {
                // ModuleModel model = PageHelper.GetModel<ModuleModel>(pnlContent.Controls);
                //List<ModuleModel> list = bll.GetUserModuleList(model);
                List<ModuleModel> list = ExportList;
                string[] header = { ControlText.gvHeadModuleDescription, ControlText.gvHeadOrderID, ControlText.gvHeadParentModuleCode, ControlText.gvHeadFormName, ControlText.gvHeadURL, ControlText.gvHeadPrivileged, ControlText.gvHeadFunctionList, ControlText.gvHeadFunctionDesc ,
                                  ControlText.lblFunctionMenuType,ControlText.lblFunctionComment,ControlText.lblFunctionVersion,ControlText.lblFunctionImage,ControlText.lblFunctionMouseImage,ControlText.lblListFlag,ControlText.lblIsKaoQin1,};
                string[] properties = { "Description1", "OrderId", "Parentmodulename", "FormName", "Url", "Privileged", "FunctionList", "FunctionDesc", "FunctionMenuType", "FunctionComment", "FunctionVersion", "FunctionImage", "FunctionMouseImage", "ListFlag", "IsKaoQin" };
                string filePath = MapPath("~/ExportFileTemp/") + DateTime.Now.Ticks + ".xls";
                NPOIHelper.ExportExcel(list, header, properties, 5000, filePath);
                PageHelper.ReturnHTTPStream(filePath, true);
            }
        }
        #endregion

        #region UltraWebGridModule的DataBound事件
        /// <summary>
        /// UltraWebGridModule的DataBound事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void UltraWebGridModule_DataBound(object sender, EventArgs e)
        {
            //失效數據顯示為紅色
            string deleted = "";
            for (int i = 0; i < UltraWebGridModule.Rows.Count; i++)
            {
                deleted = UltraWebGridModule.Rows[i].Cells.FromKey("DELETED").Text == null ? "" : UltraWebGridModule.Rows[i].Cells.FromKey("DELETED").Text.Trim();
                if (deleted == "Y")
                {
                    UltraWebGridModule.Rows[i].Style.ForeColor = System.Drawing.Color.Red;
                }
            }
        }
        #endregion
    }
}
