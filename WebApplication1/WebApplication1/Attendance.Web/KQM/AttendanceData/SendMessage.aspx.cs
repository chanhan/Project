/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： NightAllowanceToExcel.cs
 * 檔功能描述： 發送短信息UI層
 * 
 * 版本：1.0
 * 創建標識： 高子焱 2012.01.09
 * 
 */
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using dotnetSMSInterface;
using GDSBG.MiABU.Attendance.BLL.KQM.AttendanceData;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;

namespace GDSBG.MiABU.Attendance.Web.KQM.AttendanceData
{
    public partial class SendMessage : BasePage
    {
        static SynclogModel logmodel = new SynclogModel();
        #region 發送信息按鈕
        protected void btnSendMsg_Click(object sender, EventArgs e)
        {
            PageHelper.ButtonControls(base.FuncList, pnlShowPanel.Controls, base.FuncListModule);
            SendMessageBll bll = new SendMessageBll();
            if (this.txtMSG.Text.Trim().Length < 5)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "show", "alert('短信內容必須大於5個字符')", true);
                return;
            }
            string resultcode = "";
            string sendresult = "";
            string alertText = "";

            string empbg = "";
            string content = "";
            string bg_phonenum = "";
            string name = "";
            string RemindContent = this.txtMSG.Text + "<br>   This is a message from SystmeAdmin:"+CurrentUserInfo.DepName;
            if (this.txtWorkNo.Text.Trim().Length > 0)
            {
                string workNo = this.txtWorkNo.Text.Trim();
                int i = bll.GetWorkNoData(workNo);
                if (i == 0)
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "show", "alert('工號不存在！')", true);
                }
                else
                {

                    int flag = bll.GetSendMsgByWork(workNo, RemindContent, logmodel);
                    if (flag == 1)
                    {
                        #region 發送短信
                        DataTable dt = bll.GetBasic(workNo);
                        if (dt.Rows.Count > 0)
                        {
                            empbg = dt.Rows[0]["dname"].ToString();
                            workNo = dt.Rows[0]["workNo"].ToString();
                            bg_phonenum = dt.Rows[0]["tel"].ToString();
                            name = dt.Rows[0]["localname"].ToString();
                            content =empbg+":"+workNo+name+dt.Rows[0]["remindcontent"].ToString();

                            resultcode = SenSmS_ToTransfer(bg_phonenum, name, content);
                            sendresult = SendResult(resultcode);
                            alertText = empbg + ":" + workNo + name + sendresult;
                        }
                        #endregion
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(GetType(), "show", "alert('信息保存出現錯誤！')", true);
                    }
                    Page.ClientScript.RegisterStartupScript(GetType(), "show", "alert('" + alertText + "')", true);

                }
                return;
            }
            string sendTo = this.ddlSendTo.SelectedValue;
            if (sendTo != "0")
            {

                int flag = bll.GetSendMsgByStyle(RemindContent, sendTo,logmodel);
                if (flag > 0)
                {

                    #region 發送短信
                    DataTable dt = bll.GetWorkDataByDll(sendTo);
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            empbg = dt.Rows[i]["dname"].ToString();
                            string workNo = dt.Rows[i]["workNo"].ToString();
                            bg_phonenum = dt.Rows[i]["tel"].ToString();
                            name = dt.Rows[i]["localname"].ToString();
                            content = empbg + ":" + workNo + name + dt.Rows[i]["remindcontent"].ToString();

                            resultcode = SenSmS_ToTransfer(bg_phonenum, name, content);
                            sendresult = SendResult(resultcode);
                            alertText = empbg + ":" + workNo + name + sendresult + "還有" + Convert.ToString(dt.Rows.Count - i) + "個人沒有發送短信";
                            if (alertText.Length > 0)
                            {
                                Page.ClientScript.RegisterStartupScript(GetType(), "show", "alert('" + alertText + "')", true);
                                return;
                            }
                        }
                    }
                    #endregion
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "show", "alert('信息保存失敗！')", true);
                }
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "show", "alert('選擇要發送的方式！')", true);
            }
        }
        #endregion

        #region  頁面加載
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                logmodel.ProcessOwner = CurrentUserInfo.Personcode;
                logmodel.TransactionType = Request.QueryString["ModuleCode"];
                logmodel.LevelNo = "2";
                logmodel.FromHost = Request.UserHostAddress;
                if ((Convert.ToString(CurrentUserInfo.RoleCode).IndexOf("ADMIN") >= 0) || Convert.ToString(this.Session["admin"]).Equals("Y"))
                {
                    this.ddlSendTo.Items.Insert(0, new ListItem("", "0"));
                    this.ddlSendTo.Items.Insert(1, new ListItem("管理員帳號(不含派駐、不含有管理職本土)", "A"));
                    this.ddlSendTo.Items.Insert(2, new ListItem("管理員帳號(不含派駐)", "B"));
                    this.ddlSendTo.Items.Insert(3, new ListItem("全部管理員帳號(含派駐)", "C"));
                    this.ddlSendTo.Items.Insert(4, new ListItem("當前在線管理員帳號(不含派駐)", "D"));
                    this.ddlSendTo.Items.Insert(5, new ListItem("當前在線所有員工(不含派駐)", "E"));
                }
                else
                {
                    base.Response.Write("<script type='text/javascript'>alert('無權群發短信!')</script>");
                }
            }
        }
        #endregion

        #region  發送短信 （發送條件不同的時候的datatable不同）
        public string SenSmS_ToTransfer(string phontnum, string empname, string reason)
        {
            string SendResult;//發送返回結果
            /*發送固定短信結果
                           0;	//已發送
                           1;	//已送簽核
                           2;   //返回2,發送失敗,原因:含有敏感詞彙
                           3;   //返回3,發送失敗,原因:超過月發送限額
                           4;   //返回4,發送失敗,原因:超過日發送限額
                           5;   //返回5,發送失敗,原因:接受號碼不在通信錄內
                           9;   //返回9, 發送失敗, 原因:未登陸
                           8;   //數據庫錯誤
                          10;    //不明錯誤
                          11;  //固定格式ID錯誤
                          20;   //此帳號未綁定該服務器IP地址
            */
            String UserName = "F3205566";//用戶名
            String Password = "12345678";//密碼心無旁騖 責無旁貸

            int FormatID = 1080;//固定格式短信的ID
            int SpaceNum = 7;//固定短信里空格的數量，如果短信內容沒有空格需要填充，則設置為0
            String PhotoNumber = phontnum;//手機號碼，多個手機號碼以逗號隔開
            //String Content = empname + "," + reason + "," + bg_ext;//發送的內容，以逗號進行分隔，如果有x個空格，就應該有x-1個逗號
            string content = empname + "," + reason + "," + "," + "," + "," + ",";

            SMSFormatClass send = new SMSFormatClass(UserName, Password);//發送固定短信
            if (!send.isLogin)
            {
                send.login();
            }
            SendResult = send.SendSMSFormat(PhotoNumber, FormatID, SpaceNum, content);
            return SendResult;
        }
        #endregion

        #region  短信發送情況
        private string SendResult(string code)
        {
            string result = "";
            if (code == "0")
            {
                result = "已發送 ";
            }
            else if (code == "1")
            {
                result = "已送簽核 ";
            }
            else if (code == "2")
            {
                result = "發送失敗,原因:含有敏感詞彙 ";
            }
            else if (code == "3")
            {
                result = "發送失敗,原因:超過月發送限額 ";
            }
            else if (code == "4")
            {
                result = "發送失敗,原因:超過日發送限額 ";
            }
            else if (code == "5")
            {
                result = "發送失敗,原因:接受號碼不在通信錄內 ";
            }
            else if (code == "9")
            {
                result = "發送失敗, 原因:未登陸 ";
            }
            else if (code == "8")
            {
                result = "發送失敗,原因:數據庫錯誤 ";
            }
            else if (code == "10")
            {
                result = "發送失敗,原因:不明錯誤 ";
            }
            else if (code == "11")
            {
                result = "發送失敗,原因:固定格式ID錯誤 ";
            }
            else if (code == "20")
            {
                result = "發送失敗,原因:此帳號未綁定該服務器IP地址 ";
            }
            return result;
        }
        #endregion

    }
}
