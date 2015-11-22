/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： BasePage.cs
 * 檔功能描述： 所有需要驗證頁面基類
 * 
 * 版本：1.0
 * 創建標識： Lucky Lee 2011.09.19
 * 
 */

using System;
using System.Globalization;
using System.Resources;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GDSBG.MiABU.Attendance.Common;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;
using Infragistics.WebUI.UltraWebNavigator;
namespace GDSBG.MiABU.Attendance.Web
{
    using System.Text;
    using Infragistics.WebUI.WebSchedule;
    using Resources;
    using GDSBG.MiABU.Attendance.BLL.SystemManage.SystemSafety;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Text.RegularExpressions;
    /// <summary>
    /// 所有需要驗證頁面基類
    /// </summary>
    public class BasePage : Page
    {
        public static string sAppPath;
        /// <summary>
        /// 是否進行訪問權限管控
        /// </summary>
        protected virtual bool AccessPermissionRequired { get { return true; } }

        public string sqlDep = "";

        /// <summary>
        /// 當前登錄用戶組織權限
        /// </summary>
        public string SqlDep
        {
            get
            {
                //return HttpContext.Current.Session[GlobalData.UserInfoSessionKey] == sqlDep ? null : "SELECT aa.depcode FROM gds_sc_persondept aa WHERE aa.personcode = '" + CurrentUserInfo.Personcode + "' AND aa.modulecode = '" + Request.QueryString["ModuleCode"] + "' AND EXISTS (SELECT 1 FROM gds_sc_personcompany bb WHERE  bb.personcode='" + CurrentUserInfo.Personcode + "'AND aa.companyid = bb.companyid)";
                return HttpContext.Current.Session[GlobalData.UserInfoSessionKey] == sqlDep ? null : "SELECT char_list DEPCODE  FROM TABLE (gds_sc_getsqldep ('" + CurrentUserInfo.Personcode + "', '" + Request.QueryString["ModuleCode"] + "'))";
            }
        }

        /// <summary>
        /// 當前登錄用戶信息
        /// </summary>
        protected PersonModel CurrentUserInfo
        {
            get
            {
                return HttpContext.Current.Session[GlobalData.UserInfoSessionKey] == null ? null : HttpContext.Current.Session[GlobalData.UserInfoSessionKey] as PersonModel;
            }
        }
        /// <summary>
        /// 查找button權限管控列表
        /// </summary>
        public string FuncList
        {
            get
            {
                return new ModuleBll().GetFuncList(Request.QueryString["ModuleCode"].ToString(),CurrentUserInfo.Personcode);
            }
        }
        /// <summary>
        /// 查找button權限管控列表
        /// </summary>
        public string FuncListModule
        {
            get
            {
                return new ModuleBll().GetFuncList(Request.QueryString["ModuleCode"].ToString());
            }
        }


        /// <summary>
        /// 設置介面語言值
        /// </summary>
        /// <param name="containerControl">父容器</param>
        private void SetUIText(Control containerControl)
        {
            foreach (Control control in containerControl.Controls)
            {

                string text = null;
                switch (control.GetType().Name)
                {
                    case "Literal":
                        text = ControlText.ResourceManager.GetString(control.ID);
                        if (!string.IsNullOrEmpty(text))
                        {
                            (control as Literal).Text = text;
                        }
                        break;
                    case "Label":
                        text = ControlText.ResourceManager.GetString(control.ID);
                        if (!string.IsNullOrEmpty(text))
                        {
                            (control as Label).Text = text;
                        }
                        break;
                    case "CheckBox":
                        text = ControlText.ResourceManager.GetString(control.ID);
                        if (!string.IsNullOrEmpty(text))
                        {
                            (control as CheckBox).Text = text;
                        }
                        break;
                    case "Button":
                        text = ControlText.ResourceManager.GetString(control.ID);
                        if (!string.IsNullOrEmpty(text))
                        {
                            (control as Button).Text = text;
                        }
                        break;
                    case "GridView": break;
                    default:
                        SetUIText(control); break;
                }
            }
        }

     

        /// <summary>
        /// 設置日曆控件到TextBox
        /// </summary>
        /// <param name="textBoxes"></param>
        protected void SetCalendar(params TextBox[] textBoxes)
        {
            if (textBoxes != null)
            {
                //StringBuilder calScript = new StringBuilder("$(function(){$.datepicker.setDefaults($.datepicker.regional['")
                //    .Append("zh-CN").Append("']);$(\"");
                StringBuilder calScript = new StringBuilder("$(function(){$.datepicker.setDefaults($.datepicker.regional['")
                    .Append(CurrentUserInfo.Language).Append("']);$(\"");//2011.12.09 何西  
                foreach (TextBox textBox in textBoxes)
                {
                    calScript.AppendFormat("#{0},", textBox.ClientID);
                }
                calScript.Remove(calScript.Length - 1, 1).Append("\").datepicker();});");
                Page.ClientScript.RegisterStartupScript(this.GetType(), "calendar", calScript.ToString(), true);
            }
        }

        /// <summary>
        /// 初始化語言文化特性
        /// </summary>
        protected override void InitializeCulture()
        {
            if (CurrentUserInfo != null && !string.IsNullOrEmpty(CurrentUserInfo.Language))
            {
                base.InitializeCulture();
                Culture =CurrentUserInfo.Language;
                UICulture = CurrentUserInfo.Language;
            }
        }

        protected Node CreateNode(string tag, string text, bool check, bool endnode)
        {
            Node newNode = new Node();
            try
            {
                newNode.Tag = tag;
                newNode.Text = text;
                if (check)
                {
                    newNode.Checked = true;
                    newNode.TargetFrame = "1";
                }
                else
                {
                    newNode.Checked = false;
                    newNode.TargetFrame = "0";
                }
                if (endnode)
                {
                    newNode.ImageUrl = sAppPath + "../../CSS/Images/debug_break_disabled_marker.PNG";
                    newNode.SelectedImageUrl = sAppPath + "../../CSS/Images/debug_break_marker.PNG";
                    return newNode;
                }
                newNode.ImageUrl = sAppPath + "../../CSS/Images/ig_treeFolder.gif";
                newNode.SelectedImageUrl = sAppPath + "../../CSS/Images/ig_treeFolderOpen.gif";
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return newNode;
        }

        //update by 顧陳偉 2011-12-30  tree節點標記顏色
        protected Node CreateNode(string tag, string text, bool check, bool endnode, Color foreColor)
        {
            Node newNode = new Node();
            try
            {
                newNode.Tag = tag;
                newNode.Text = text;
                if (check)
                {
                    newNode.Checked = true;
                    newNode.TargetFrame = "1";
                }
                else
                {
                    newNode.Checked = false;
                    newNode.TargetFrame = "0";
                }
                newNode.Style.ForeColor = foreColor;
                if (endnode)
                {
                    newNode.ImageUrl = sAppPath + "../../CSS/Images/debug_break_disabled_marker.PNG";
                    newNode.SelectedImageUrl = sAppPath + "../../CSS/Images/debug_break_marker.PNG";
                    return newNode;
                }
                newNode.ImageUrl = sAppPath + "../../CSS/Images/ig_treeFolder.gif";
                newNode.SelectedImageUrl = sAppPath + "../../CSS/Images/ig_treeFolderOpen.gif";
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return newNode;
        }

        protected string GetResouseValue(string key)
        {
            try
            {
                string resourceValue = "";
                CultureInfo ci = CultureInfo.CurrentCulture;
                resourceValue = new ResourceManager(typeof(Resource)).GetString(key, ci);
                if (string.IsNullOrEmpty(resourceValue))
                {
                    resourceValue = "Undefine";
                }
                return resourceValue.Replace("'", "''");
            }
            catch (Exception)
            {
                return "Undefine";
            }
        }

        /// <summary>
        /// Ajax請求
        /// </summary>
        protected virtual void AjaxProcess() { }

        /// <summary>
        /// 登錄判斷及初始化
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreInit(EventArgs e)
        {
            AjaxProcess();
            if (CurrentUserInfo == null)
            {
                List<PersonModel> user = new List<PersonModel>();
                if (Request.QueryString["IsMail"] == "Y")
                {
                    user = new PersonBll().GetPersonUserId(Request.QueryString["UserId"], Request.QueryString["PassWord"], Request.QueryString["IsMail"]);
                }
                else
                {
                    user = new PersonBll().GetPersonLoginId(Request.QueryString["UserId"]);
                }
                if (user != null && user.Count >= 1)
                {
                    Session[GlobalData.UserInfoSessionKey] = user[0];
                }
                else
                    Response.Redirect("http://10.138.4.205:100");

            }

        }

        /// <summary>
        /// 控件初始化
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreLoad(EventArgs e)
        {
            base.OnPreLoad(e);
            SetUIText(Page);
        }
        /// <summary>
        /// 檢驗小數點后精確到5位；
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public bool IsDouble1(string s)
        {
            string pattern = @"^\d+(\.\d{1})?$";
            return Regex.IsMatch(s, pattern);
        }
    }
}
