using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.BLL;
using Maticsoft.Common;
using System.Collections;
using System.Data;

namespace Maticsoft.Web
{
    public partial class CallCenter : System.Web.UI.Page
    {
        ReciveMsgBLL recivemsgbll = new ReciveMsgBLL();
        Hashtable HtCustomer = new Hashtable();
        Hashtable HtCode = new Hashtable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User"]==null)
            {
                  Response.Redirect("Login.aspx");
            }
            HtCustomer = Application["App_Customer"] as Hashtable;
            HtCode = Application["App_Code"] as Hashtable;
            tabexchang.Visible = false;
            gvDelivery.Visible = false;
            Gift.Visible = false;
            Product.Visible = false;
            Button3.Text = "兑换";
            Button3.Enabled = false;
        }
        private void BindData(string phone)
        {
            DataTable dt = recivemsgbll.getDelivery(phone);
            gvDelivery.DataSource = dt;
            gvDelivery.DataBind();
        }
        private void Show()
        {
            string phone = StringPlus.ToDBC(phonenumber.Text.Trim()).ToUpper();
            Maticsoft.Model.CustomerData model = HtCustomer[phone] as Maticsoft.Model.CustomerData;
            List<Maticsoft.Model.DeliveryData> deliverymodel = recivemsgbll.getDeliveryData(phone);
            if (model != null)
            {
                labphone.Text = "";
                tabexchang.Visible = true;
                gvDelivery.Visible = true;
                Gift.Visible = true;
                Product.Visible = true;
                name.Text = model.CustomerName;
                address.Text = model.Address;
                AvailableBao.Text = model.BaoAvaivable.ToString();
                AvailableJing.Text = model.JingAvailable.ToString();
                AvailableMi.Text = model.MiAvailable.ToString();
                AvailableZhun.Text = model.ZhunAvailable.ToString();
                SumBao.Text = (model.BaoAvaivable + model.ProductPacks).ToString();
                SumMi.Text = (model.MiAvailable + model.GiftPacks + model.ProductPacks).ToString();
                SumJing.Text = (model.JingAvailable + model.GiftPacks + model.ProductPacks).ToString();
                SumZhun.Text = (model.ZhunAvailable + model.GiftPacks + model.ProductPacks).ToString();
                int gift = 0;
                int product = 0;
                recivemsgbll.getProductGiftNumber(model.BaoAvaivable, model.JingAvailable, model.MiAvailable, model.ZhunAvailable, out gift, out product);
                //Button3.Enabled = gift > 0;
                //Button4.Enabled = product > 0;
                int deliverygifts = 0;
                int deliveryproduct = 0;
                if (deliverymodel.Count > 0)
                {
                    foreach (Maticsoft.Model.DeliveryData delivery in deliverymodel)
                    {
                        deliverygifts += (int)delivery.GiftPacks;
                        deliveryproduct += (int)delivery.ProductPacks;
                    }
                }
                if (product > 0)
                {
                    Button3.Text = "兑换正装奶粉" + product + "罐";
                    Button3.Enabled = true;
                }
                else if (gift > 0)
                {
                    Button3.Text = "兑换便携装礼包" + gift + "份";
                    Button3.Enabled = true;
                }
               
                Gift.Text = string.Format("已兑换便携装礼包{0}份，其中{1}份已快递", model.GiftPacks.ToString(), deliverygifts);
                Product.Text = string.Format("已兑换正装奶粉{0}罐，其中{1}罐已快递", model.ProductPacks.ToString(), deliveryproduct);
            }
            else
            {
                labphone.Text = "手机号不存在";
                labcode.Text = "";
                tabexchang.Visible = false;
                gvDelivery.Visible = false;
                Gift.Visible = false;
                Product.Visible = false;
                //MessageBox.Show(this, "客户信息不存在");
                name.Text = "";
                address.Text = "";
                code.Text = "";
                Gift.Text = "";
                Product.Text = "";
                AvailableBao.Text = "";
                AvailableJing.Text = "";
                AvailableMi.Text = "";
                AvailableZhun.Text = "";
                SumBao.Text = "";
                SumMi.Text = "";
                SumJing.Text = "";
                SumZhun.Text = "";
                Button3.Enabled = false;
                //Button4.Enabled = false;
            }
            BindData(phone);
        }
        protected void search_Click(object sender, EventArgs e)
        {
            Show();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Maticsoft.BLL.CustomerData bll = new Maticsoft.BLL.CustomerData();
            string phone = StringPlus.ToDBC(phonenumber.Text.Trim()).ToUpper();
            try
            {
                Maticsoft.Model.CustomerData model = HtCustomer[phone] as Maticsoft.Model.CustomerData;
                if (model != null)
                {
                    if (name.Text.Trim() != "")
                    {
                        if (address.Text.Trim() != "")
                        {
                            model.CustomerName = StringPlus.ToDBC(name.Text.Trim());
                            model.Address = StringPlus.ToDBC(address.Text.Trim());

                            bool b = bll.Update(model);
                            if (b)
                            {
                                //HtCustomer[phone] = model;
                                MessageBox.Show(this, "姓名地址填写成功");
                            }
                            else
                            {
                                MessageBox.Show(this, "姓名地址填写失败");

                            }
                        }
                        else
                        {
                            MessageBox.Show(this, "请填写地址");
                        }
                    }
                    else
                    {
                        MessageBox.Show(this, "请填写姓名");
                    }
                    tabexchang.Visible = true;
                    gvDelivery.Visible = true;
                    Gift.Visible = true;
                    Product.Visible = true;
                }
                else
                {
                    tabexchang.Visible = false;
                    gvDelivery.Visible = false;
                    Gift.Visible = false;
                    Product.Visible = false;
                    //MessageBox.Show(this, "客户信息不存在");
                }

            }
            catch (Exception)
            {

                throw;
            }

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string phone = StringPlus.ToDBC(phonenumber.Text.Trim());
            string codestr = StringPlus.ToDBC(code.Text.Trim());
            List<string> RightCodes;//正确代码
            List<string> WrongFormatCode;//格式错误代码
            List<string> ExchangedCode;//已兑换代码
            List<string> NoexistCode;//不存在代码
            List<string> ValidatedCode;//已验证代码
            if (phone != "")
            {
                if (codestr != "")
                {
                  
                    List<string> listmsg = recivemsgbll.ReciveMsg(phone, codestr, HtCustomer, HtCode);
                    Show();
                    new ReciveMsgBLL().checkCode(codestr, HtCode, out  RightCodes, out  WrongFormatCode, out  ExchangedCode, out NoexistCode, out  ValidatedCode);
                    ShowCodeMsg(WrongFormatCode, ExchangedCode, NoexistCode, ValidatedCode);
                }
                else
                {
                    MessageBox.Show(this, "请填写兑换码");
                }
            }
            else
            {
                MessageBox.Show(this, "请填写手机号");
            }
        }
        private void ShowCodeMsg(  List<string> WrongFormatCode,List<string> ExchangedCode,List<string> NoexistCode,List<string> ValidatedCode)
        {
            string WrongMsg = "";
            if (WrongFormatCode.Count>0)
            {
                WrongMsg += string.Format("代码{0}格式错误。<br><br>", string.Join("、", WrongFormatCode.ToArray()));                
            }
            if (ExchangedCode.Count>0)
            {
                WrongMsg += string.Format("代码{0}已兑换。<br><br>", string.Join("、", ExchangedCode.ToArray()));                
                
            }
            if (ValidatedCode.Count > 0)
            {
                WrongMsg += string.Format("代码{0}已验证。<br><br>", string.Join("、", ValidatedCode.ToArray()));                
                
            }
            if (NoexistCode.Count > 0)
            {
                WrongMsg += string.Format("代码{0}不存在。<br><br>", string.Join("、", NoexistCode.ToArray()));
            }
            labcode.Text = WrongMsg;
        }
        protected void Button3_Click(object sender, EventArgs e)
        {
            string phone = StringPlus.ToDBC(phonenumber.Text.Trim()).ToUpper();
            Maticsoft.BLL.ReciveMsgBLL bll = new Maticsoft.BLL.ReciveMsgBLL();
            string msg = bll.GetExchangeMsg(phone,  HtCustomer, HtCode);
            Show();
        }
    }
}