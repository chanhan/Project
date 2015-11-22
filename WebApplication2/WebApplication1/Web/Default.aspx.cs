using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Maticsoft.Common;
using Maticsoft.BLL;
using System.Collections.Generic;
using System.Net;
using Maticsoft.Web.com.frisocrm.sms;
using Maticsoft.Common.DEncrypt;
using System.IO;
using System.Text;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Maticsoft.Web
{
    public partial class Default : System.Web.UI.Page
    {
        private Maticsoft.BLL.CodeData dal = new Maticsoft.BLL.CodeData();
        Maticsoft.BLL.ReciveMsgBLL recivemsgbll = new Maticsoft.BLL.ReciveMsgBLL();
        Regex regex = new Regex("^[\u4e00-\u9fa5]+$");
        ReturnStatus status = new ReturnStatus();

        string AllowedIP = ConfigurationManager.AppSettings["AllowedIP"].ToString();

        protected void Page_Load(object sender, EventArgs e)
        {
            string ip=   Request.UserHostAddress;
            if (ip!=AllowedIP && AllowedIP != "*")
            {
                return;
            }
            try
            {
                List<string> msglist = new List<string>();
                Hashtable HtCustomer = Application["App_Customer"] as Hashtable;
                Hashtable HtCode = Application["App_Code"] as Hashtable;
                if (string.IsNullOrEmpty(Request.QueryString["reqmobilephone"]) || string.IsNullOrEmpty(Request.QueryString["eCouponNo"]))
                {
                    Response.Write("-1");
                    return;
                }
                //保存原始接收信息
                recivemsgbll.SaveMsgData(Request.QueryString["reqmobilephone"], Request.QueryString["eCouponNo"], true);

                string phonenumber = StringPlus.ToDBC(WebUtility.HtmlDecode(Request.QueryString["reqmobilephone"])).Replace(" ", "").ToUpper();

                string codes = StringPlus.ToDBC(WebUtility.HtmlDecode(Request.QueryString["eCouponNo"])).Replace(" ", "");
                string str = "";
                if (DateTime.Now > DateTime.Parse("2014/01/31 23:59:59"))
                {

                    str = recivemsgbll.getMsg();
                }
                else
                {
                    if (phonenumber != "" && codes != "")
                    {
                        if (codes.ToUpper()=="Y")//兑换密净准
                        {
                            str = GetExchangeMsg(phonenumber, HtCustomer, HtCode);

                        }
                        else if (codes.Contains("/") && codes.Length > 2 && regex.Match(codes.Substring(0, 2)).Success == true)
                        {
                            
                            str = recivemsgbll.SaveAddress(phonenumber, codes,HtCustomer);
                        }
                        else//验证兑换码
                        {
                            msglist = ReciveMsg(phonenumber, codes, HtCustomer, HtCode);
                        }
                    }
                }
                if (str != "")
                {
                    int MsgID = recivemsgbll.SaveMsgData(phonenumber, str, false);
                    string password = MD5Encrypt("friso_compaign").ToLower();
                    status = new Web.com.frisocrm.sms.SendSMSService2().SendSMSSingle(80008, password, 91, phonenumber, str);
                    if (status.Status == "ERROR")
                    {
                        SaveErrorMsg(status, phonenumber, MsgID);
                    }
                    Response.Write("100");
                }
                if (msglist.Count > 0)
                {
                    try
                    {
                        foreach (string msgstr in msglist)
                        {
                            if (msgstr != "")
                            {
                                int MsgID = recivemsgbll.SaveMsgData(phonenumber, msgstr, false);
                                string password = MD5Encrypt("friso_compaign").ToLower();
                                status = new Web.com.frisocrm.sms.SendSMSService2().SendSMSSingle(80008, password, 91, phonenumber, msgstr);
                                if (status.Status == "ERROR")
                                {
                                    SaveErrorMsg(status, phonenumber, MsgID);
                                }
                            }
                        }
                    }
                    catch (Exception)
                    {

                        throw;
                    }


                    Response.Write("100");
                }
              
            }
            catch (Exception emsg)
            {
                string stacktrace = emsg.StackTrace;
                string exmsg = string.Format("手机号[{0}]---代码[{1}]---异常信息[{2}]---错误位置[{3}]---日期[{4}]\r\n", Request.QueryString["reqmobilephone"], Request.QueryString["eCouponNo"], emsg.Message, stacktrace, DateTime.Now.ToString()).Replace("\r\n", "");
                WriteLog(exmsg);
                Response.Write("-1");
            }

        }
        public void WriteLog(string text)
        {
            string path = Server.MapPath("~/") + "\\ErrorLog.log";
            FileStream fs = new FileStream(path, FileMode.Append);
            StreamWriter sw = new StreamWriter(fs, Encoding.Default);
            sw.Write(text);
            sw.Close();
            fs.Close();
        }
        public bool SaveErrorMsg(ReturnStatus status, string phonenumber, int MsgID)
        {
            Maticsoft.Model.ErrormsgData errormsg = new Model.ErrormsgData();
            errormsg.Errormsg = status.StatusMsg;
            errormsg.PhoneNumber = phonenumber;
            errormsg.ReturnedID = MsgID;
            errormsg.ReturnTime = DateTime.Now;
            return new Maticsoft.BLL.ErrormsgData().Add(errormsg);
        }
        public static string MD5Encrypt(string pwd)
        {

            string str = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(pwd, "MD5");
            return str;
        }

        private string GetExchangeMsg(string phonenumber, Hashtable HtCustomer, Hashtable HtCode)
        {
            Maticsoft.BLL.ReciveMsgBLL bll = new Maticsoft.BLL.ReciveMsgBLL();
            string msg = bll.GetExchangeMsg(phonenumber, HtCustomer, HtCode);
            return msg;
        }
        private List<string> ReciveMsg(string phonenumber, string codes, Hashtable HtCustomer, Hashtable HtCode)
        {
            Maticsoft.BLL.ReciveMsgBLL bll = new Maticsoft.BLL.ReciveMsgBLL();
            List<string> msg = bll.ReciveMsg(phonenumber, codes, HtCustomer, HtCode);
            return msg;
        }
    }
}
