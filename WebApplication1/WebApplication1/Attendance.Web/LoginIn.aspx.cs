/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： LoginIn.cs
 * 檔功能描述： 登陸信息
 * 
 * 版本：1.0
 * 創建標識： 劉炎 2011.12.16
 * 
 */

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Script.Serialization;
using System.Web.UI;
using GDSBG.MiABU.Attendance.BLL.SystemManage.SystemSafety;
using GDSBG.MiABU.Attendance.Common;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;
using Resources;

namespace GDSBG.MiABU.Attendance.Web
{
    public partial class LoginIn : Page
    {
        JavaScriptSerializer JsSerializer = new JavaScriptSerializer();
        Dictionary<string, string> ClientMessage;
        PersonBll pBll = new PersonBll();
        protected override void OnPreInit(EventArgs e)
        {
            //AjaxProcess();
            base.OnPreInit(e);
        }


        /// <summary>
        /// 登陸狀態，驗證當前用戶信息與數據庫數據是否匹配（暫時作廢）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session[GlobalData.ConnectInfoSessionKey] = ConfigurationManager.ConnectionStrings[GlobalData.CommonDbConfigKey].ConnectionString;
                Session[GlobalData.DbTypeSessionKey] = ConfigurationManager.AppSettings[GlobalData.CommonDbTypeConfigKey];
            }
            if (ClientMessage != null)
            {
                ClientMessage = new Dictionary<string, string>();
                ClientMessage.Add("SystemLogining", "登陸中，請稍候");
            }
        }


        /// <summary>
        /// 登陸驗證
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void imgbtnLogin_Click(object sender, EventArgs e)
        {
            PersonModel model = new PersonModel();
            if (!string.IsNullOrEmpty(txtUserId.Text) && !string.IsNullOrEmpty(txtPassword.Text))
            {
                string personcode = this.txtUserId.Text;
                string password = this.txtPassword.Text;
                List<PersonModel> user = pBll.GetPersonUserId(personcode, password);
                // List<PersonModel> pList = pBll.GetPersonPassword(password);
                if (user == null||user.Count<1)
                {
                    lblMessage.Text = Message.EmpNotExist;
                }
                else 
                {
                    model.Personcode = txtUserId.Text;
                    model.LoginTime = DateTime.Now;
                    model.Passwd = user[0].Passwd;
                    Session[GlobalData.UserLoginId] = model.Personcode;
                    Session[GlobalData.UserInfoSessionKey] = user[0];
                    Response.Redirect("MainForm.aspx?");
                }
            }
            else
            {
                lblMessage.Text = Message.TextBoxNotNull;
            }
        }
    }
}
