using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Maticsoft.DAL;
using System.Data;
using System.Transactions;
using System.Text.RegularExpressions;
using System.Collections;
namespace Maticsoft.BLL
{
    public class ReciveMsgBLL
    {
        private Maticsoft.DAL.CodeData CodeDatadal = new Maticsoft.DAL.CodeData();
        private Maticsoft.DAL.CustomerData CustomerDatadal = new Maticsoft.DAL.CustomerData();
        private Maticsoft.DAL.MsgData MsgDatadal = new Maticsoft.DAL.MsgData();
        private Maticsoft.DAL.DeliveryData DeliveryDatadal = new Maticsoft.DAL.DeliveryData();

        private static Regex RegNumber = new Regex("^[0-9]{9}$");

        public string getMsg()
        {
            return " 亲爱的美妈，“宝护盖”收集活动已结束，感谢您对美素佳儿的支持！";
        }
        public List<string> getMsg(int BaoNum, int JingNum, int MiNum, int ZhunNum)
        {
            int GiftPacks = 0;//可兑换礼品数
            int ProductPacks = 0;//可兑换正装数
            List<string> listmsg = new List<string>();
            while (JingNum > 0 && MiNum > 0 && ZhunNum > 0)
            {
                if (BaoNum > 0)
                {
                    List<int> list = new List<int>();
                    list.Add(JingNum);
                    list.Add(MiNum);
                    list.Add(ZhunNum);
                    list.Add(BaoNum);
                    ProductPacks = list.Min();
                    BaoNum -= ProductPacks;
                    JingNum -= ProductPacks;
                    ZhunNum -= ProductPacks;
                    MiNum -= ProductPacks;
                }
                else
                {
                    List<int> list = new List<int>();
                    list.Add(JingNum);
                    list.Add(MiNum);
                    list.Add(ZhunNum);
                    GiftPacks = list.Min();
                    JingNum -= GiftPacks;
                    ZhunNum -= GiftPacks;
                    MiNum -= GiftPacks;
                }
            }
            if (ProductPacks > 0)
            {
                string msgProductPacks = string.Format("亲爱的美妈，恭喜您已集齐{0}组“密、净、准+宝护盖”。直接回复“Y”,兑换900g正装奶粉{1}罐。 ", ProductPacks, ProductPacks);
                listmsg.Add(msgProductPacks);

            }
            if (GiftPacks > 0)
            {
                string msgGiftPacks = string.Format("亲爱的美妈，恭喜您已集齐{0}组“密、净、准”。直接回复“Y”,兑换便携装礼包{1}份,您也可以再集{2}个宝护盖兑换900g正装奶粉{3}罐。 ", GiftPacks, GiftPacks, GiftPacks, GiftPacks);
                listmsg.Add(msgGiftPacks);
            }
            return listmsg;
        }
        public void checkCode(string codes, Hashtable HtCode, out List<string> RightCodes, out List<string> WrongFormatCode, out List<string> ExchangedCode, out List<string> NoexistCode, out List<string> ValidatedCode)
        {
            string[] code = codes.Split('/');
            RightCodes = new List<string>();//正确代码
            WrongFormatCode = new List<string>();//格式错误代码
            ExchangedCode = new List<string>();//已兑换代码
            NoexistCode = new List<string>();//不存在代码
            ValidatedCode = new List<string>();//已验证代码
            for (int i = 0; i < code.Length; i++)
            {

                if (!RegNumber.Match(code[i]).Success)
                {
                    WrongFormatCode.Add(code[i]);//格式错误
                }
                else
                {

                    if (HtCode.Contains(code[i]))
                    {
                        Maticsoft.Model.CodeData CodeDataModel = HtCode[code[i]] as Maticsoft.Model.CodeData;

                        if (CodeDataModel.IsExchanged == true)
                        {
                            ExchangedCode.Add(code[i]);//已兑换
                        }
                        else if (CodeDataModel.IsValidated == true)
                        {
                            ValidatedCode.Add(code[i]);//已验证
                        }
                        else
                        {
                            RightCodes.Add(code[i]);
                        }
                    }
                    else
                    {
                        NoexistCode.Add(code[i]);
                    }
                }

            }

        }
        public List<string> ReciveMsg(string phonenumber, string codes, Hashtable HtCustomer, Hashtable HtCode)
        {

            List<string> msg = new List<string>();

            ReciveMsgDAL dal = new ReciveMsgDAL();

            List<string> RightCodes;//正确代码
            List<string> WrongFormatCode;//格式错误代码
            List<string> ExchangedCode;//已兑换代码
            List<string> NoexistCode;//不存在代码
            List<string> ValidatedCode;//已验证代码
            checkCode(codes, HtCode, out  RightCodes, out  WrongFormatCode, out  ExchangedCode, out NoexistCode, out  ValidatedCode);
            using (System.Transactions.TransactionScope tsCope = new TransactionScope(TransactionScopeOption.RequiresNew, new TimeSpan(0, 30, 0)))
            {

                try
                {
                    if (RightCodes.Count > 0)//代码正确
                    {
                        string[] rightcode = RightCodes.ToArray();
                        for (int i = 0; i < rightcode.Length; i++)
                        {
                            //更新CodeData表
                            Maticsoft.Model.CodeData CodeDataModel = HtCode[rightcode[i]] as Maticsoft.Model.CodeData;
                            CodeDataModel.PhoneNumber = phonenumber;
                            CodeDataModel.IsValidated = true;
                            CodeDataModel.ValidatedTime = DateTime.Now;
                            CodeDatadal.Update(CodeDataModel);

                            //CustomerData存在兑奖者记录
                            if (HtCustomer.Contains(phonenumber))
                            {
                                Maticsoft.Model.CustomerData CustomerDataModel = HtCustomer[phonenumber] as Maticsoft.Model.CustomerData;
                                if (CodeDataModel.Remark == "密")
                                {
                                    CustomerDataModel.MiAvailable += 1;
                                }
                                else if (CodeDataModel.Remark == "净")
                                {
                                    CustomerDataModel.JingAvailable += 1;
                                }
                                else if (CodeDataModel.Remark == "准")
                                {
                                    CustomerDataModel.ZhunAvailable += 1;
                                }
                                else if (CodeDataModel.Remark == "宝护盖")
                                {
                                    CustomerDataModel.BaoAvaivable += 1;
                                }
                                CustomerDatadal.Update(CustomerDataModel);//更新可兑换信息
                            }
                            else //CustomerData不存在兑奖者记录
                            {
                                Maticsoft.Model.CustomerData CustomerDataModel = new Maticsoft.Model.CustomerData();
                                CustomerDataModel.PhoneNumber = phonenumber;
                                CustomerDataModel.BaoAvaivable = 0;
                                CustomerDataModel.ZhunAvailable = 0;
                                CustomerDataModel.JingAvailable = 0;
                                CustomerDataModel.MiAvailable = 0;
                                CustomerDataModel.ProductPacks = 0;
                                CustomerDataModel.GiftPacks = 0;
                                CustomerDataModel.Address = "";
                                CustomerDataModel.CustomerName = "";
                                if (CodeDataModel.Remark == "密")
                                {
                                    CustomerDataModel.MiAvailable = 1;
                                }
                                else if (CodeDataModel.Remark == "净")
                                {
                                    CustomerDataModel.JingAvailable = 1;
                                }
                                else if (CodeDataModel.Remark == "准")
                                {
                                    CustomerDataModel.ZhunAvailable = 1;
                                }
                                else if (CodeDataModel.Remark == "宝护盖")
                                {
                                    CustomerDataModel.BaoAvaivable = 1;
                                }
                                CustomerDatadal.Add(CustomerDataModel);//新增可兑换信息

                                HtCustomer.Add(phonenumber, CustomerDataModel);//新增后添加到hashtable
                            }

                        }
                    }
                    if (WrongFormatCode.Count > 0 || ExchangedCode.Count > 0 || NoexistCode.Count > 0 || ValidatedCode.Count > 0)//代码错误
                    {
                        string wrongformatcodestring = "";
                        foreach (string wrongformatcodestr in WrongFormatCode)
                        {
                            wrongformatcodestring += wrongformatcodestr + "、";
                        }

                        if (wrongformatcodestring != "")
                        {
                            wrongformatcodestring = wrongformatcodestring.TrimEnd('、');
                            string errormsg = string.Format("您提交的代码{0}格式有误，请按正确格式将9位代码上传，多个代码则用“/”分隔。如有疑问，请咨询400-820-2775。", wrongformatcodestring);
                            msg.Add(errormsg);
                        }
                        string exchangedcodestring = "";

                        foreach (string exchangedcodestr in ExchangedCode)
                        {
                            Maticsoft.Model.CodeData codedatamodel = HtCode[exchangedcodestr] as Maticsoft.Model.CodeData;
                            string str = string.Format("{0}已于{1}年{2}月{3}日兑换", exchangedcodestr, codedatamodel.ExchangeTime.Value.Year, codedatamodel.ExchangeTime.Value.Month, codedatamodel.ExchangeTime.Value.Day);
                            exchangedcodestring += str + "、";
                        }

                        if (exchangedcodestring != "")
                        {
                            exchangedcodestring = exchangedcodestring.TrimEnd('、');
                            string errormsg = string.Format("您上传的代码{0}。如有疑问，请咨询400-820-2775。", exchangedcodestring);
                            msg.Add(errormsg);
                        }
                        string noexistcodestring = "";
                        foreach (string noexistcodestr in NoexistCode)
                        {
                            noexistcodestring += noexistcodestr + "、";
                        }

                        if (noexistcodestring != "")
                        {
                            noexistcodestring = noexistcodestring.TrimEnd('、');
                            string errormsg = string.Format("您上传的代码{0}有误，请核对后重新上传。如有疑问，请咨询400-820-2775。", noexistcodestring);
                            msg.Add(errormsg);
                        }

                        string validatedtcodestring = "";
                        foreach (string validatedtcodestr in ValidatedCode)
                        {
                            Maticsoft.Model.CodeData codedatamodel = HtCode[validatedtcodestr] as Maticsoft.Model.CodeData;
                            string str = string.Format("{0}已于{1}年{2}月{3}上传", validatedtcodestr, codedatamodel.ValidatedTime.Value.Year, codedatamodel.ValidatedTime.Value.Month, codedatamodel.ValidatedTime.Value.Day);
                            validatedtcodestring += str + "、";
                        }
                        if (validatedtcodestring != "")
                        {

                            validatedtcodestring = validatedtcodestring.TrimEnd('、');
                            string errormsg = string.Format("您上传的代码{0}。如有疑问，请咨询400-820-2775。", validatedtcodestring);
                            msg.Add(errormsg);
                        }
                    }

                    //获取可兑换信息
                    if (HtCustomer.Contains(phonenumber))
                    {

                        Maticsoft.Model.CustomerData model = HtCustomer[phonenumber] as Maticsoft.Model.CustomerData;

                        int BaoNum = model.BaoAvaivable;
                        int JingNum = model.JingAvailable;
                        int MiNum = model.MiAvailable;
                        int ZhunNum = model.ZhunAvailable;
                        msg.AddRange(getMsg(BaoNum, JingNum, MiNum, ZhunNum));
                    }

                    tsCope.Complete();
                }
                catch (Exception)
                {
                    msg.Clear();
                    throw;
                }
                finally
                {
                    tsCope.Dispose();
                }
            }
            return msg;
        }

        public string GetExchangeMsg(string phonenumber, Hashtable HtCustomer, Hashtable HtCode)
        {
            string msg = "";
            if (!HtCustomer.Contains(phonenumber))
            {
                return "";
            }
            try
            {

                Maticsoft.Model.CustomerData model = HtCustomer[phonenumber] as Maticsoft.Model.CustomerData;
                int BaoNum = model.BaoAvaivable;
                int JingNum = model.JingAvailable;
                int MiNum = model.MiAvailable;
                int ZhunNum = model.ZhunAvailable;
                int Gifts = 0;
                int Products = 0;

                getProductGiftNumber(BaoNum, JingNum, MiNum, ZhunNum, out  Gifts, out  Products);

                if (Products >0)
                {
                    model.JingAvailable -= Products;
                    model.MiAvailable -= Products;
                    model.ZhunAvailable -= Products;
                    model.BaoAvaivable -= Products;
                    model.ProductPacks += Products;
                    bool b = CustomerDatadal.Update(model);

                    List<string> codelist = new List<string>();

                    if (b)
                    {
                        string strwhere = string.Format(" PhoneNumber='{0}' and IsValidated=1 and IsExchanged=0 and Remark='密'", phonenumber);
                        DataTable dt = CodeDatadal.GetList(Products, strwhere, "ValidatedTime").Tables[0];
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            codelist.Add(dt.Rows[i]["code"].ToString());
                        }
                        strwhere = string.Format(" PhoneNumber='{0}' and IsValidated=1 and IsExchanged=0 and Remark='净'", phonenumber);
                        dt = CodeDatadal.GetList(Products, strwhere, "ValidatedTime").Tables[0];
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            codelist.Add(dt.Rows[i]["code"].ToString());
                        }
                        strwhere = string.Format(" PhoneNumber='{0}' and IsValidated=1 and IsExchanged=0 and Remark='准'", phonenumber);
                        dt = CodeDatadal.GetList(Products, strwhere, "ValidatedTime").Tables[0];
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            codelist.Add(dt.Rows[i]["code"].ToString());
                        }

                        strwhere = string.Format(" PhoneNumber='{0}' and IsValidated=1 and IsExchanged=0 and Remark='宝护盖'", phonenumber);
                        dt = CodeDatadal.GetList(Products, strwhere, "ValidatedTime").Tables[0];
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            codelist.Add(dt.Rows[i]["code"].ToString());
                        }

                        foreach (string codestr in codelist)
                        {
                            Maticsoft.Model.CodeData Codemodel = HtCode[codestr] as Maticsoft.Model.CodeData;
                            Codemodel.ExchangeTime = DateTime.Now;
                            Codemodel.IsExchanged = true;
                            bool a = CodeDatadal.Update(Codemodel);

                        }
                        msg = string.Format("亲爱的美妈，您已兑换900g正装奶粉{0}罐。请直接回复“收件人姓名/收货地址”，如张三/上海市徐汇区挹翠苑188弄8号602室。", Products);

                    }
                }
                else if (Gifts>0)
                {
                    model.JingAvailable -= Gifts;
                    model.MiAvailable -= Gifts;
                    model.ZhunAvailable -= Gifts;
                    model.GiftPacks += Gifts;
                    bool b = CustomerDatadal.Update(model);
                    List<string> codelist = new List<string>();
                    if (b)
                    {
                        string strwhere = string.Format(" PhoneNumber='{0}' and IsValidated=1 and IsExchanged=0 and Remark='密'", phonenumber);
                        DataTable dt = CodeDatadal.GetList(Gifts, strwhere, "ValidatedTime").Tables[0];
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            codelist.Add(dt.Rows[i]["code"].ToString());
                        }
                        strwhere = string.Format(" PhoneNumber='{0}' and IsValidated=1 and IsExchanged=0 and Remark='净'", phonenumber);
                        dt = CodeDatadal.GetList(Gifts, strwhere, "ValidatedTime").Tables[0];
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            codelist.Add(dt.Rows[i]["code"].ToString());
                        }
                        strwhere = string.Format(" PhoneNumber='{0}' and IsValidated=1 and IsExchanged=0 and Remark='准'", phonenumber);
                        dt = CodeDatadal.GetList(Gifts, strwhere, "ValidatedTime").Tables[0];
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            codelist.Add(dt.Rows[i]["code"].ToString());
                        }

                        foreach (string codestr in codelist)
                        {
                            Maticsoft.Model.CodeData Codemodel = HtCode[codestr] as Maticsoft.Model.CodeData;
                            Codemodel.ExchangeTime = DateTime.Now;
                            Codemodel.IsExchanged = true;
                            bool a = CodeDatadal.Update(Codemodel);
                        }
                        msg = string.Format("亲爱的美妈，您已兑换便携装礼包{0}份。请直接回复“收件人姓名/收货地址”，如张三/上海市徐汇区挹翠苑188弄8号602室。", Gifts);

                    }

                }
            }
            catch { msg = ""; throw; }


            return msg;
        }
        public void getProductGiftNumber(int BaoNum, int JingNum, int MiNum, int ZhunNum, out int GiftsNum, out int ProductsNum)
        {
            int GiftPacks = 0;//可兑换礼品数
            int ProductPacks = 0;//可兑换正装数
            while (JingNum > 0 && MiNum > 0 && ZhunNum > 0)
            {
                if (BaoNum > 0)
                {
                    List<int> list = new List<int>();
                    list.Add(JingNum);
                    list.Add(MiNum);
                    list.Add(ZhunNum);
                    list.Add(BaoNum);
                    ProductPacks = list.Min();
                    BaoNum -= ProductPacks;
                    JingNum -= ProductPacks;
                    ZhunNum -= ProductPacks;
                    MiNum -= ProductPacks;
                }
                else
                {
                    List<int> list = new List<int>();
                    list.Add(JingNum);
                    list.Add(MiNum);
                    list.Add(ZhunNum);
                    GiftPacks = list.Min();
                    JingNum -= GiftPacks;
                    ZhunNum -= GiftPacks;
                    MiNum -= GiftPacks;
                }
            }

            ProductsNum = ProductPacks;
            GiftsNum = GiftPacks;

        }
        public int SaveMsgData(string phonenumber, string codes, bool RecivedMsg)
        {
            int i = 0;

            try
            {
                Maticsoft.Model.MsgData MsgDataModel = new Maticsoft.Model.MsgData();
                MsgDataModel.IsRecivedMsg = RecivedMsg;
                MsgDataModel.Msg = codes;
                MsgDataModel.MsgTime = DateTime.Now;
                MsgDataModel.PhoneNumber = phonenumber;
                MsgDataModel.ReciveMsgStatus = "";
                MsgDataModel.ReturnedID = "";
                Maticsoft.DAL.MsgData dal = new Maticsoft.DAL.MsgData();
                i = dal.Add(MsgDataModel);
            }
            catch (Exception)
            {
                i = -1;
                throw;
            }
            return i;
        }

        public string SaveAddress(string phonenumber, string codes, Hashtable HtCustomer)
        {
            if (!HtCustomer.Contains(phonenumber))
            {
                return "";
            }
            string msg = "";

            string name = codes.Split('/')[0];
            int index = codes.IndexOf('/');
            string address = codes.Substring(index + 1);

            Maticsoft.Model.CustomerData model = HtCustomer[phonenumber] as Maticsoft.Model.CustomerData;
            model.CustomerName = name;
            model.Address = address;
            if (CustomerDatadal.Update(model))
            {
                msg = "感谢您的支持！我们将尽快安排工作人员与您确认信息，请妈妈保持手机通畅哦！";
            }
            return msg;

        }


        public Model.CustomerData DataRowToCustomerModel(DataRow dr)
        {
            return CustomerDatadal.DataRowToModel(dr);
        }

        public Model.CodeData DataRowToCodeModel(DataRow dr)
        {
            return CodeDatadal.DataRowToModel(dr);
        }

        public List<Model.DeliveryData> getDeliveryData(string phone)
        {
            return DeliveryDatadal.Query(phone);
        }
        public DataTable getDelivery(string phone)
        {
            return DeliveryDatadal.QueryDataTable(phone);
        }

        public DataTable getCodeExchangeByCode(string code)
        {
            return CodeDatadal.QueryDataTableByCode(code);
        }

        public DataTable getCodeExchangeByPhoneNumber(string phonenumber)
        {
            return CodeDatadal.QueryDataTableByPhonenumber(phonenumber);

        }
    }
}
