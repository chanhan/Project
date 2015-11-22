/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： PowerChange.cs
 * 檔功能描述： 權限交接UI層
 * 
 * 版本：1.0
 * 創建標識： 高子焱 2012.01.09
 * 
 */
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Script.Serialization;
using System.Web.UI;
using GDSBG.MiABU.Attendance.BLL.SystemManage.SystemSafety;
using Resources;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;

namespace GDSBG.MiABU.Attendance.Web.SystemManage.SystemSafety
{
    public partial class PowerChange : BasePage
    {
        private string FromPersonCode = "";
        private string ToPersonCode = "";
        DataTable dt = new DataTable();
        PowerChangeBll bll = new PowerChangeBll();
        static SynclogModel logmodel = new SynclogModel();
        int flag;
        #region  權限複製
        protected void btnCopy_Click(object sender, EventArgs e)
        {
            try
            {
                flag = bll.SaveData(CurrentUserInfo.Personcode, this.txtFromPersoncode.Text, this.txtToPersoncode.Text, "copy", logmodel);
                this.QueryTo();
                if (flag > 0)
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "show", "alert('" + Message.CopySucssess + "')", true);
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "show", "alert('" + Message.CopyFailed + "')", true);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region  權限交接者資料查詢
        protected void btnFormQuery_Click(object sender, EventArgs e)
        {
            try
            {
                this.QueryFrom();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region 權限交接
        protected void btnChangeSave_Click(object sender, EventArgs e)
        {
            try
            {
                logmodel.ProcessFlag = "insert";
                flag = bll.ChangData(this.txtFromPersoncode.Text, this.txtToPersoncode.Text, "change", logmodel);
                this.QueryTo();
                if (flag > 0)
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "show", "alert('" + Message.ChangeSuccess + "')", true);
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "show", "alert('" + Message.ChangeFailed + "')", true);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region 現權限接收者資料查詢
        protected void btnToQuery_Click(object sender, EventArgs e)
        {
            try
            {
                if (ToPersonCode.Length > 0)
                {
                    this.QueryTo();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region 綁定數據
        private void DataUIBind()
        {
            this.UltraWebGridFromPerson.DataSource = dt.DefaultView;
            this.UltraWebGridFromPerson.DataBind();
        }
        #endregion
        #region 頁面加載
        protected void Page_Load(object sender, EventArgs e)
        {
            JavaScriptSerializer JsSerializer = new JavaScriptSerializer();
            Dictionary<string, string> ClientMessage = null;
            this.FromPersonCode = this.txtFromPersoncode.Text.ToString().Trim();
            this.ToPersonCode = this.txtToPersoncode.Text.ToString().Trim();
            //PageHelper.ButtonControls(base.FuncList, pnlShowPanel.Controls,base.FuncListModule);
            if (!IsPostBack)
            {
                logmodel.ProcessOwner = CurrentUserInfo.Personcode;
                logmodel.TransactionType = Request.QueryString["ModuleCode"];
                logmodel.LevelNo = "2";
                logmodel.FromHost = Request.UserHostAddress;
            }
            if (ClientMessage == null)
            {
                ClientMessage = new Dictionary<string, string>();
                ClientMessage.Add("CopyConfirm", Message.CopyConfirm);
                ClientMessage.Add("PleaseEnterOldPsn", Message.PleaseEnterOldPsn);
                ClientMessage.Add("OldPsnRoleNotExist", Message.OldPsnRoleNotExist);
                ClientMessage.Add("PleaseEnterNewPsn", Message.PleaseEnterNewPsn);
                ClientMessage.Add("NewPsnNotExist", Message.NewPsnNotExist);
                ClientMessage.Add("NewPsnRoleIsExist", Message.NewPsnRoleIsExist);

                ClientMessage.Add("NewMustNotOld", Message.NewMustNotOld);
                ClientMessage.Add("PersoncodeNotNull", Message.PersoncodeNotNull);
            }
            string clientmsg = JsSerializer.Serialize(ClientMessage);
            Page.ClientScript.RegisterStartupScript(GetType(), "ClientMessage", "var Message=" + clientmsg + ";", true);
        }
        #endregion
        #region  具體查詢權限交接者的資料
        private void QueryFrom()
        {
            dt = bll.GetPowerTableByKey(FromPersonCode);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    this.txtFromPersonName.Text = Convert.ToString(dt.Rows[0]["LOCALNAME"]);
                    this.txtFromRoleName.Text = Convert.ToString(dt.Rows[0]["RoleName"]);
                    this.txtFromDepName.Text = Convert.ToString(dt.Rows[0]["DEPNAME"]);
                    this.txtFromRole.Text = Convert.ToString(dt.Rows[0]["Role"]);
                    if (Convert.ToString(dt.Rows[0]["personcode"]) != "")
                    {
                        this.DataUIBind();
                    }
                    else
                    {
                        this.UltraWebGridFromPerson.Rows.Clear();
                    }
                }
                else
                {
                    this.UltraWebGridFromPerson.Rows.Clear();
                    this.txtFromPersonName.Text = "";
                    this.txtFromRoleName.Text = "";
                    this.txtFromDepName.Text = "";
                    this.txtFromRole.Text = "";
                }
            }
            dt.Clear();

        }

        private void QueryTo()
        {

            dt = bll.GetPowerTableByKey(ToPersonCode);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    this.txtToPersonName.Text = Convert.ToString(dt.Rows[0]["LOCALNAME"]);
                    this.txtToRoleName.Text = Convert.ToString(dt.Rows[0]["RoleName"]);
                    this.txtToDepName.Text = Convert.ToString(dt.Rows[0]["DEPNAME"]);
                    this.txtToRole.Text = Convert.ToString(dt.Rows[0]["Role"]);
                }
                else
                {
                    this.txtToPersonName.Text = "";
                    this.txtToRoleName.Text = "";
                    this.txtToDepName.Text = "";
                    this.txtToRole.Text = "";
                }
            }
            dt.Clear();
        }
        #endregion
    }
}
