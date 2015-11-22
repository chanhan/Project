/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： ChangPwd.cs
 * 檔功能描述： 修改密碼和郵箱UI層
 * 
 * 版本：1.0
 * 創建標識： 高子焱 2012.01.09
 * 
 */
using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using System.Web.UI;
using GDSBG.MiABU.Attendance.BLL;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;
using Resources;
using System.Data;
namespace GDSBG.MiABU.Attendance.Web.SystemManage.SystemSafety
{
    public partial class ChangPwd : BasePage
    {
        static SynclogModel logmodel = new SynclogModel();
        JavaScriptSerializer JsSerializer = new JavaScriptSerializer();
        Dictionary<string, string> ClientMessage = null;
        ChangPwdBLL bll = new ChangPwdBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            PageHelper.ButtonControls(base.FuncList, pnlShowPanel.Controls, base.FuncListModule);
            if (!IsPostBack)
            {
                DataTable dt = bll.GetPerInfo(CurrentUserInfo.Personcode);
                if (dt != null)
                {
                    txtMail.Text = dt.Rows[0]["mail"].ToString();
                    txtTel.Text = dt.Rows[0]["tel"].ToString();
                    txtPhone.Text = dt.Rows[0]["mobile"].ToString();
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "show", "alert('"+Message.NotExist+"')", true);
                }
                logmodel.ProcessOwner = CurrentUserInfo.Personcode;
                logmodel.TransactionType = Request.QueryString["ModuleCode"];
                logmodel.LevelNo = "2";
                logmodel.FromHost = Request.UserHostAddress;
            }
            if (ClientMessage == null)
            {
                ClientMessage = new Dictionary<string, string>();
                ClientMessage.Add("OldPwdNotNull", Message.OldPwdNotNull);
                ClientMessage.Add("NewPwdNotNull", Message.NewPwdNotNull);
                ClientMessage.Add("NewPwdAgainNotNull", Message.NewPwdAgainNotNull);
                ClientMessage.Add("NewPasswordNotValid", Message.NewPasswordNotValid);
                ClientMessage.Add("NewPasswordEqualsOld", Message.NewPasswordEqualsOld);
                ClientMessage.Add("TwoNewPasswordNotEqual", Message.TwoNewPasswordNotEqual);
                ClientMessage.Add("OldPwdNotTrue", Message.OldPwdNotTrue);
                ClientMessage.Add("UpdateSuccess", Message.UpdateSuccess);
                ClientMessage.Add("UpdateFailed", Message.UpdateFailed);
                ClientMessage.Add("MailNotNull", Message.MailNotNull);
                ClientMessage.Add("TelFmtNotTrue", Message.TelFmtNotTrue);
                ClientMessage.Add("PhoneFmtNotTrue", Message.PhoneFmtNotTrue);
            }
            string clientmsg = JsSerializer.Serialize(ClientMessage);
            Page.ClientScript.RegisterStartupScript(GetType(), "ClientMessage", "var Message=" + clientmsg + ";", true);
        }

        /// <summary>
        /// 修改密碼
        /// </summary>
        protected override void AjaxProcess()
        {
            bool blPro = false;
            int result = 0;
            if (!string.IsNullOrEmpty(Request.Form["newPassword"]))
            {
                logmodel.ProcessFlag = "update";
                string newpwd = Request.Form["newPassword"];
                string oldpwd = Request.Form["Password"];
                string userno = CurrentUserInfo.Personcode;
                result = bll.UpdateUserByKey(userno, oldpwd, newpwd, logmodel);
                blPro = true;
            }
            if (!string.IsNullOrEmpty(Request.Form["Mail"]))
            {
                logmodel.ProcessFlag = "update";
                PersonModel model = new PersonModel();
                model.Mail = Request.Form["Mail"];
                model.Tel = Request.Form["Tel"];
                model.Mobile = Request.Form["Phone"];
                model.Personcode = CurrentUserInfo.Personcode;
                if (bll.UpdateMailByKey(model, logmodel))
                {
                    result = 1;
                }
                blPro = true;
            }
            if (blPro)
            {
                Response.Clear();
                Response.Write(result.ToString());
                Response.End();
            }
        }
    }
}
