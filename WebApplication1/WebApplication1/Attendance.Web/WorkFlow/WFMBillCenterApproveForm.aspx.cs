using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Infragistics.WebUI.UltraWebGrid;
using GDSBG.MiABU.Attendance.BLL.WorkFlow;
using System.Data;
using GDSBG.MiABU.Attendance.BLL.KQM.KaoQinData;
using GDSBG.MiABU.Attendance.Model.KQM.KaoQinData;
using Resources;
using GDSBG.MiABU.Attendance.BLL.Hr.KQM.KaoQinData;
using GDSBG.MiABU.Attendance.BLL.KQM.OTM;
using GDSBG.MiABU.Attendance.Model.KQM.Query.OTM;
using GDSBG.MiABU.Attendance.BLL.KQM.Query.OTM;
using System.Drawing;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;
using System.Web.Script.Serialization;

namespace GDSBG.MiABU.Attendance.Web.WorkFlow
{
    public partial class WFMBillCenterApproveForm : BasePage
    {

        private WFSignCenterBll bll = new WFSignCenterBll();
        protected System.Data.DataSet dataSet, tempDataSet;
        protected System.Data.DataTable tempDataTable;
        static SynclogModel logmodel = new SynclogModel();

        Dictionary<string, string> ClientMessage = null;
        JavaScriptSerializer JsSerializer = new JavaScriptSerializer();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(Request.QueryString["fileName"]))
                {
                    PageHelper.ReturnHTTPStream(MapPath("~/Testify/" + Request.QueryString["fileName"]), false);
                }
                if (ClientMessage == null)
                {
                    ClientMessage = new Dictionary<string, string>();
                    ClientMessage.Add("AtLastOneChoose", Message.AtLastOneChoose);

                    ClientMessage.Add("yousurecannotthisdate", Message.yousurecannotthisdate);
                    ClientMessage.Add("youcanclickagreenotsignsave", Message.youcanclickagreenotsignsave);
                    ClientMessage.Add("wfm_message_data_disapprove", Message.wfm_message_data_disapprove);
                    ClientMessage.Add("wfm_message_disaudit", Message.wfm_message_disaudit);
                    ClientMessage.Add("ConfirmBatchConfirm", Message.ConfirmBatchConfirm);
                    

                    string clientmsg = JsSerializer.Serialize(ClientMessage);
                    Page.ClientScript.RegisterStartupScript(GetType(), "ClientMessage", "var Message=" + clientmsg + ";", true);
                }
                if (!IsPostBack)
                {

                    logmodel.ProcessOwner = CurrentUserInfo.Personcode;
                    logmodel.TransactionType = (Request.QueryString["ModuleCode"] == null) ? "" : Request.QueryString["ModuleCode"].ToString();
                    logmodel.LevelNo = "2";
                    logmodel.FromHost = Request.UserHostAddress;

                    string BillNo = this.Request.QueryString["BillNo"] == null ? "" : this.Request.QueryString["BillNo"].ToString();
                    string SFlag = this.Request.QueryString["SFlag"] == null ? "" : this.Request.QueryString["SFlag"].ToString();

                    if (BillNo.Length == 0)//add
                    {
                        Response.Write("<script type='text/javascript'>alert('" + Message.wfm_message_billdeleted + "');window.close();</script>");
                        return;
                    }
                    else
                    {
                        this.HiddenSFlag.Value = SFlag;
                        this.HiddenBillTypeNo.Value = BillNo.Substring(0, 3);
                        Hiddentrid.Value = BillNo.Substring(0, 3);

                        DataTable dt = bll.GetBillTypeCode(this.HiddenBillTypeNo.Value);
                        if (dt.Rows.Count > 0)
                        {
                            this.HiddenBillTypeNo.Value = dt.Rows[0]["BILLTYPECODE"].ToString();
                            labelBillTypeName.Text = dt.Rows[0]["BILLTYPENAME"].ToString();
                        }

                        //if (BillNo.Substring(0, 3) == "OTM")
                        //{
                        //    labelBillTypeName.Text = "加班單";
                        //}
                        this.textBoxBillNo.Text = BillNo;
                        this.HiddenBillNo.Value = BillNo;
                        // this.Internationalization();

                        Query(BillNo);

                        if (SFlag.Equals("Y"))
                        {
                            labelApRemark.Visible = false;
                            textBoxApRemark.Visible = false;
                            this.ButtonApproveAgree.Visible = false;
                            this.ButtonDisApprove.Visible = false;
                            //this.ButtonCountersigning.Visible = false;
                            //this.ButtonSave.Visible = false;
                            this.labelAuditRemark.Visible = false;

                        }
                        else
                        {
                            string condition = "";
                            condition += " and a.Status in ('0','3')";
                            condition += " and b.OrderNo=(select nvl(min(e.OrderNo),'-1') from GDS_ATT_AUDITSTATUS e where e.BillNo=a.BillNo and e.Auditstatus='0')";
                            condition += " and b.AuditMan='" + CurrentUserInfo.Personcode + "' and b.BillNo='" + BillNo + "'";
                            this.tempDataTable = bll.GetAuditCenterDataByCondition(condition).Tables["WFM_Bill"];
                            if (tempDataTable.Rows.Count == 0)
                            {
                                this.ButtonApproveAgree.Enabled = false;
                                this.ButtonDisApprove.Enabled = false;
                                //this.ButtonCountersigning.Enabled = false;
                                //this.ButtonSave.Enabled = false;
                            }
                            // this.textBoxSignLocalName.Attributes.Add("onpropertychange", "GetEmp();");
                        }
                    }
                }
                // this.textBoxApplyDate.Attributes.Add("formater", System.Convert.ToString(Session["datetimeFormat"]));

            }
            catch (Exception ex)
            {
                this.WriteMessage(2, (ex.InnerException == null ? ex.Message : ex.InnerException.Message));
            }
            // AjaxPro.Utility.RegisterTypeForAjax(typeof(WFM_WFMBillCenterApproveForm));
        }

        //拒簽動作
        protected void ButtonDisApprove_Click(object sender, EventArgs e)
        {
            try
            {
                string ApRemark = this.textBoxApRemark.Text.Trim().Replace("'", "''");
                string BillNo = "", BillTypeNo = "";
                BillNo = this.textBoxBillNo.Text;
                BillTypeNo = this.HiddenBillTypeNo.Value;
                switch (BillTypeNo)
                {
                    #region  加班單   D001
                    case "D001":
                    case "OTMProjectApply":
                        string OTAid = "";
                        string DisSignRmark = string.Empty;
                        List<string> list_remark = new List<string>();
                        List<string> list_id = new List<string>();
                        for (int i = 0; i < UltraWebGridOTA.Rows.Count; i++)
                        {
                            OTAid = this.UltraWebGridOTA.DisplayLayout.Rows[i].Cells.FromKey("ID").Text;
                            DisSignRmark = this.UltraWebGridOTA.DisplayLayout.Rows[i].Cells.FromKey("DISSIGNRMARK").Text;
                            if (!string.IsNullOrEmpty(DisSignRmark))
                            {
                                list_remark.Add(DisSignRmark);
                            }
                            else
                            {
                                WriteMessage(1, Message.wfm_disapprove_remark);
                                return;
                            }
                            list_id.Add(OTAid);
                        }
                        //----拒簽的方法，根據不同的記錄 多表單時候判斷

                        bll.SaveDisAuditData(BillNo, CurrentUserInfo.Personcode, BillTypeNo, ApRemark, list_remark, list_id,logmodel);
                        //--------
                        break;
                    #endregion 
                    #region  外出申請 KQMApplyOut
                    case "KQMApplyOut":
                        list_remark = new List<string>();
                        list_id = new List<string>();
                        for (int i = 0; i < UltraWebGridKQT.Rows.Count; i++)
                        {
                            OTAid = this.UltraWebGridKQT.DisplayLayout.Rows[i].Cells.FromKey("ID").Text;
                            DisSignRmark = this.UltraWebGridKQT.DisplayLayout.Rows[i].Cells.FromKey("DISSIGNRMARK").Text;
                            if (!string.IsNullOrEmpty(DisSignRmark))
                            {
                                list_remark.Add(DisSignRmark);
                            }
                            else
                            {
                                WriteMessage(1, Message.wfm_disapprove_remark);
                                return;
                            }
                            list_id.Add(OTAid);
                        }
                        //----拒簽的方法，根據不同的記錄 多表單時候判斷

                        bll.SaveDisAuditData(BillNo, CurrentUserInfo.Personcode, BillTypeNo, ApRemark, list_remark, list_id,logmodel);
                        //--------
                        break;
                    #endregion 
                    #region D002
                    case "D002":
                        list_remark = new List<string>();
                        list_id = new List<string>();
                        for (int i = 0; i < UltraWebGridLeaveApply.Rows.Count; i++)
                        {
                            OTAid = this.UltraWebGridLeaveApply.DisplayLayout.Rows[i].Cells.FromKey("ID").Text;
                            DisSignRmark = this.UltraWebGridLeaveApply.DisplayLayout.Rows[i].Cells.FromKey("DISSIGNRMARK").Text;
                            if (!string.IsNullOrEmpty(DisSignRmark))
                            {
                                list_remark.Add(DisSignRmark);
                            }
                            else
                            {
                                WriteMessage(1, Message.wfm_disapprove_remark);
                                return;
                            }
                            list_id.Add(OTAid);
                        }
                        //----拒簽的方法，根據不同的記錄 多表單時候判斷

                        bll.SaveDisAuditData(BillNo, CurrentUserInfo.Personcode, BillTypeNo, ApRemark, list_remark, list_id,logmodel);
                        //--------
                        break;
                    #endregion
                    #region  異常刷卡簽單  KQMException
                    case "KQMException":
                        list_remark = new List<string>();
                        list_id = new List<string>();
                        for (int i = 0; i < UltraWebGridKQT.Rows.Count; i++)
                        {
                            OTAid = this.UltraWebGridKQE.DisplayLayout.Rows[i].Cells.FromKey("ID").Text;
                            DisSignRmark = this.UltraWebGridKQE.DisplayLayout.Rows[i].Cells.FromKey("DISSIGNRMARK").Text;
                            if (!string.IsNullOrEmpty(DisSignRmark))
                            {
                                list_remark.Add(DisSignRmark);
                            }
                            else
                            {
                                WriteMessage(1, Message.wfm_disapprove_remark);
                                return;
                            }
                            list_id.Add(OTAid);
                        }
                        //----拒簽的方法，根據不同的記錄 多表單時候判斷

                        bll.SaveDisAuditData(BillNo, CurrentUserInfo.Personcode, BillTypeNo, ApRemark, list_remark, list_id,logmodel);
                        //--------
                        break;
                    #endregion
                    #region 未刷補卡 & 免卡人員加班

                    case "KQMMakeup":      //未刷補卡
                    
                        list_remark = new List<string>();
                        list_id = new List<string>();
                        for (int i = 0; i < UltraWebGridKQU.Rows.Count; i++)
                        {
                            OTAid = this.UltraWebGridKQU.DisplayLayout.Rows[i].Cells.FromKey("ID").Text;
                            DisSignRmark = this.UltraWebGridKQU.DisplayLayout.Rows[i].Cells.FromKey("DISSIGNRMARK").Text;
                            if (!string.IsNullOrEmpty(DisSignRmark))
                            {
                                list_remark.Add(DisSignRmark);
                            }
                            else
                            {
                                WriteMessage(1, Message.wfm_disapprove_remark);
                                return;
                            }
                            list_id.Add(OTAid);
                        }
                        //----拒簽的方法，根據不同的記錄 多表單時候判斷

                       // bll.SaveDisAuditData(BillNo, CurrentUserInfo.Personcode, BillTypeNo, ApRemark, list_remark, list_id,logmodel);
                        bll.SaveDisAuditData(BillNo, CurrentUserInfo.Personcode, BillTypeNo, ApRemark, list_remark, list_id,logmodel);
                        //--------
                        break;

                    #endregion
                        
                    #region 免卡人員加班
 
                    case "KQMOTMA":     //免卡人員加班
                        list_remark = new List<string>();
                        list_id = new List<string>();
                        for (int i = 0; i < UltraWebGridTMA.Rows.Count; i++)
                        {
                            OTAid = this.UltraWebGridTMA.DisplayLayout.Rows[i].Cells.FromKey("ID").Text;
                            DisSignRmark = this.UltraWebGridTMA.DisplayLayout.Rows[i].Cells.FromKey("DISSIGNRMARK").Text;
                            if (!string.IsNullOrEmpty(DisSignRmark))
                            {
                                list_remark.Add(DisSignRmark);
                            }
                            else
                            {
                                WriteMessage(1, Message.wfm_disapprove_remark);
                                return;
                            }
                            list_id.Add(OTAid);
                        }
                        //----拒簽的方法，根據不同的記錄 多表單時候判斷

                        bll.SaveDisAuditData(BillNo, CurrentUserInfo.Personcode, BillTypeNo, ApRemark, list_remark, list_id,logmodel);
                        //--------
                        break;
  
                    #endregion
                       
                    #region 月加班匯總查詢KQM
                    case "KQMMonthTotal":
                        list_remark = new List<string>();
                        List<string> list_work = new List<string>();
                        List<string> list_year = new List<string>();
                        for (int i = 0; i < UltraWebGridKQM.Rows.Count; i++)
                        {
                            string WorkNo = this.UltraWebGridKQM.DisplayLayout.Rows[i].Cells.FromKey("WorkNo").Text;//工號
                            string YearMonth = this.UltraWebGridKQM.DisplayLayout.Rows[i].Cells.FromKey("YearMonth").Text;//年月
                            //OTAid = this.UltraWebGridLeaveApply.DisplayLayout.Rows[i].Cells.FromKey("ID").Text;//序號
                            DisSignRmark = this.UltraWebGridKQM.DisplayLayout.Rows[i].Cells.FromKey("DISSIGNRMARK").Text;
                            if (!string.IsNullOrEmpty(DisSignRmark))
                            {
                                list_remark.Add(DisSignRmark);
                            }
                            else
                            {
                                WriteMessage(1, Message.wfm_disapprove_remark);
                                return;
                            }
                            //list_id.Add(OTAid);
                            list_work.Add(WorkNo);
                            list_year.Add(YearMonth);

                        }
                        //----拒簽的方法，根據不同的記錄 多表單時候判斷

                        bll.SaveDisAuditData(BillNo, CurrentUserInfo.Personcode, BillTypeNo, ApRemark, list_remark, list_work, list_year,logmodel);
                        //--------
                        break;
                    #endregion
                    default:
                        break;
                }

                Response.Write("<script type='text/javascript'>alert('" + Message.wfm_message_disapprovecomplete + "!');window.opener.document.all.ButtonReset.click();window.close();</script>");
                this.ButtonApproveAgree.Enabled = false;
                this.ButtonDisApprove.Enabled = false;
            }
            catch (System.Exception ex)
            {
                this.WriteMessage(2, (ex.InnerException == null ? ex.Message : ex.InnerException.Message));
            }
        }

        //同意按鈕
        protected void ButtonApprove_Click(object sender, EventArgs e)
        {
            try
            {
                string ApRemark = this.textBoxApRemark.Text.Trim().Replace("'", "''");
                string BillNo = this.textBoxBillNo.Text;
                string condition = "", DisApproveEmp = "" + Message.wfm_disapprove + "：";
                int intDisApprove = 0;
                string BillTypeNo = this.HiddenBillTypeNo.Value;
                switch (BillTypeNo)
                {
                    #region 加班預報簽核單OTA
                    case "D001"://加班預報簽核單OTA
                    case "OTMProjectApply":
                        string ISPay = "";
                        string OTType = "";
                        string OTAid = "";
                        string DisSignRmark = string.Empty;
                        List<string> list_remark = new List<string>();
                        List<string> list_id = new List<string>();
                        TemplatedColumn tcolOTAISPay = (TemplatedColumn)UltraWebGridOTA.Bands[0].Columns[UltraWebGridOTA.Bands[0].Columns.FromKey("CheckBoxISPay").Index];
                        TemplatedColumn tcolOTA = (TemplatedColumn)UltraWebGridOTA.Bands[0].Columns[0];
                        for (int i = 0; i < UltraWebGridOTA.Rows.Count; i++)
                        {
                            OTAid = this.UltraWebGridOTA.DisplayLayout.Rows[i].Cells.FromKey("ID").Text;
                            DisSignRmark = this.UltraWebGridOTA.DisplayLayout.Rows[i].Cells.FromKey("DISSIGNRMARK").Text;

                            if (this.HiddenisProject.Value.Equals("Y"))//專案G2加班是否發薪
                            {
                                OTType = UltraWebGridOTA.Rows[i].Cells.FromKey("OTType").Text;
                                if (UltraWebGridOTA.Rows[i].Cells.FromKey("ISPay").Value != null)
                                {
                                    ISPay = UltraWebGridOTA.Rows[i].Cells.FromKey("ISPay").Text;
                                }
                                else
                                {
                                    ISPay = "N";
                                }
                                CellItem GridItemISPay = (CellItem)tcolOTAISPay.CellItems[i];
                                CheckBox chkIsHaveRightISPay = (CheckBox)(GridItemISPay.FindControl("CheckBoxCellISPay"));
                                //龍華周邊G2修改了名字 所以這裡要修改 判斷是否存在G2
                                //if (OTType.Equals("G2"))
                                if (OTType.IndexOf("G2") != -1)  //update by xukai 20111108
                                {
                                    if (chkIsHaveRightISPay.Checked && ISPay.Equals("N"))
                                    {
                                        bll.ExcuteSQL("UPDATE GDS_ATT_ADVANCEAPPLY SET ISPay='Y' where  ID='" + OTAid + "'");
                                    }
                                    if (!chkIsHaveRightISPay.Checked && ISPay.Equals("Y"))
                                    {
                                        bll.ExcuteSQL("UPDATE GDS_ATT_ADVANCEAPPLY SET ISPay='N' where  ID='" + OTAid + "'");
                                    }
                                }
                            }
                            CellItem GridItem = (CellItem)tcolOTA.CellItems[i];
                            CheckBox chkIsHaveRight = (CheckBox)(GridItem.FindControl("CheckBoxCell"));
                            if (chkIsHaveRight.Checked)
                            {
                                if (!string.IsNullOrEmpty(DisSignRmark))
                                {
                                    list_remark.Add(DisSignRmark);
                                }
                                else
                                {
                                    WriteMessage(1, Message.wfm_disapprove_remark);
                                    return;
                                }
                                list_id.Add(OTAid);
                                condition += " id = '" + OTAid + "' or";
                                DisApproveEmp += this.UltraWebGridOTA.DisplayLayout.Rows[i].Cells.FromKey("WorkNo").Text +
                                    "/" + this.UltraWebGridOTA.DisplayLayout.Rows[i].Cells.FromKey("LocalName").Text + "、";
                                intDisApprove += 1;
                            }
                        }
                        if (intDisApprove > 0)
                        {
                            condition = condition.Substring(0, condition.Length - 2);
                            condition = " and (" + condition + ")";
                            DisApproveEmp = "|" + DisApproveEmp.Substring(0, DisApproveEmp.Length - 1);
                        }
                        else
                        {
                            condition = "";
                            DisApproveEmp = "";
                        }
                        ApRemark = ApRemark + DisApproveEmp;
                        if (intDisApprove == UltraWebGridOTA.Rows.Count)
                        {
                            bll.SaveDisAuditData(BillNo, CurrentUserInfo.Personcode, BillTypeNo, ApRemark, list_remark, list_id,logmodel);
                        }
                        else
                        {
                            bll.SaveAuditData(BillNo, CurrentUserInfo.Personcode, BillTypeNo, ApRemark, condition, list_remark, list_id,logmodel);
                        }
                        break;
                    #endregion 
                    #region 外出申請簽核單KQT
                    case "KQMApplyOut"://外出申請簽核單KQT
                        list_remark = new List<string>();
                        list_id = new List<string>();
                        tcolOTA = (TemplatedColumn)UltraWebGridKQT.Bands[0].Columns[0];
                        for (int i = 0; i < UltraWebGridKQT.Rows.Count; i++)
                        {
                            OTAid = this.UltraWebGridKQT.DisplayLayout.Rows[i].Cells.FromKey("ID").Text;//序號
                            DisSignRmark = this.UltraWebGridKQT.DisplayLayout.Rows[i].Cells.FromKey("DISSIGNRMARK").Text;//拒簽欄位
                            CellItem GridItem = (CellItem)tcolOTA.CellItems[i];
                            CheckBox chkIsHaveRight = (CheckBox)(GridItem.FindControl("CheckBoxCell"));
                            if (chkIsHaveRight.Checked)
                            {
                                if (!string.IsNullOrEmpty(DisSignRmark))
                                {
                                    list_remark.Add(DisSignRmark);
                                }
                                else
                                {
                                    Page.ClientScript.RegisterStartupScript(GetType(), "show", "alert('" + Message.wfm_disapprove_remark + "')", true);
                                    return;
                                }
                                list_id.Add(OTAid);
                                condition += " id = '" + OTAid + "' or";
                                DisApproveEmp += this.UltraWebGridKQT.DisplayLayout.Rows[i].Cells.FromKey("WorkNo").Text +
                                    "/" + this.UltraWebGridKQT.DisplayLayout.Rows[i].Cells.FromKey("LocalName").Text + "、";
                                intDisApprove += 1;
                            }
                        }
                        if (intDisApprove > 0)
                        {
                            condition = condition.Substring(0, condition.Length - 2);
                            condition = " and (" + condition + ")";
                            DisApproveEmp = "|" + DisApproveEmp.Substring(0, DisApproveEmp.Length - 1);
                        }
                        else
                        {
                            condition = "";
                            DisApproveEmp = "";
                        }
                        ApRemark = ApRemark + DisApproveEmp;
                        if (intDisApprove == UltraWebGridKQT.Rows.Count)
                        {
                            bll.SaveDisAuditData(BillNo, CurrentUserInfo.Personcode, BillTypeNo, ApRemark, list_remark, list_id,logmodel);
                        }
                        else
                        {
                            bll.SaveAuditData(BillNo, CurrentUserInfo.Personcode, BillTypeNo, ApRemark, condition, list_remark, list_id,logmodel);
                        }
                        break;
                    #endregion 
                    #region  D002
                    case "D002":
                        list_remark = new List<string>();
                        list_id = new List<string>();
                        tcolOTA = (TemplatedColumn)UltraWebGridLeaveApply.Bands[0].Columns[0];
                        for (int i = 0; i < UltraWebGridLeaveApply.Rows.Count; i++)
                        {
                      
                          OTAid = this.UltraWebGridLeaveApply.Rows[i].Cells.FromKey("ID").Text;//序號
                            DisSignRmark = this.UltraWebGridLeaveApply.DisplayLayout.Rows[i].Cells.FromKey("DISSIGNRMARK").Text;//拒簽欄位
                            CellItem GridItem = (CellItem)tcolOTA.CellItems[i];
                            CheckBox chkIsHaveRight = (CheckBox)(GridItem.FindControl("CheckBoxCell"));
                            if (chkIsHaveRight.Checked)
                            {
                                if (!string.IsNullOrEmpty(DisSignRmark))
                                {
                                    list_remark.Add(DisSignRmark);
                                }
                                else
                                {
                                    Page.ClientScript.RegisterStartupScript(GetType(), "show", "alert('" + Message.wfm_disapprove_remark + "')", true);
                                    return;
                                }
                                list_id.Add(OTAid);
                                condition += " id = '" + OTAid + "' or";
                                DisApproveEmp += this.UltraWebGridLeaveApply.DisplayLayout.Rows[i].Cells.FromKey("WorkNo").Text +
                                    "/" + this.UltraWebGridLeaveApply.DisplayLayout.Rows[i].Cells.FromKey("LocalName").Text + "、";
                                intDisApprove += 1;//拒簽
                            }
                        }
                        if (intDisApprove > 0)
                        {
                            condition = condition.Substring(0, condition.Length - 2);
                            condition = " and (" + condition + ")";
                            DisApproveEmp = "|" + DisApproveEmp.Substring(0, DisApproveEmp.Length - 1);
                        }
                        else
                        {
                            condition = "";
                            DisApproveEmp = "";
                        }
                        ApRemark = ApRemark + DisApproveEmp;
                        if (intDisApprove == UltraWebGridLeaveApply.Rows.Count)
                        {
                            bll.SaveDisAuditData(BillNo, CurrentUserInfo.Personcode, BillTypeNo, ApRemark, list_remark, list_id,logmodel);
                        }
                        else
                        {
                            bll.SaveAuditData(BillNo, CurrentUserInfo.Personcode, BillTypeNo, ApRemark, condition, list_remark, list_id,logmodel);
                        }
                        break;
                    #endregion 
                    #region 考勤異常簽核單KQE
                    case "KQMException"://考勤異常簽核單KQE
                        list_remark = new List<string>();
                        list_id = new List<string>();

                        TemplatedColumn tcolKQE = (TemplatedColumn)UltraWebGridKQE.Bands[0].Columns[0];
                        for (int i = 0; i < UltraWebGridKQE.Rows.Count; i++)
                        {
                            OTAid = this.UltraWebGridKQE.DisplayLayout.Rows[i].Cells.FromKey("ID").Text;//序號
                            DisSignRmark = this.UltraWebGridKQE.DisplayLayout.Rows[i].Cells.FromKey("DISSIGNRMARK").Text;//拒簽欄位

                            CellItem GridItem = (CellItem)tcolKQE.CellItems[i];
                            CheckBox chkIsHaveRight = (CheckBox)(GridItem.FindControl("CheckBoxCell"));
                            if (chkIsHaveRight.Checked)
                            {
                                if (!string.IsNullOrEmpty(DisSignRmark))
                                {
                                    list_remark.Add(DisSignRmark);
                                }
                                else
                                {
                                    Page.ClientScript.RegisterStartupScript(GetType(), "show", "alert('" + Message.wfm_disapprove_remark + "')", true);
                                    return;
                                }
                                list_id.Add(OTAid);
                                condition += " id = '" + OTAid + "' or";
                                DisApproveEmp += this.UltraWebGridKQE.DisplayLayout.Rows[i].Cells.FromKey("WorkNo").Text +
                                    "/" + this.UltraWebGridKQE.DisplayLayout.Rows[i].Cells.FromKey("LocalName").Text + "、";
                                intDisApprove += 1;//拒簽

                                //condition += " (WorkNo = '" + this.UltraWebGridKQE.DisplayLayout.Rows[i].Cells.FromKey("WorkNo").Text +
                                //    "' and KQDate = to_date('" + DateTime.Parse(this.UltraWebGridKQE.DisplayLayout.Rows[i].Cells.FromKey("KQDate").Text).ToString("yyyy/MM/dd") + "','yyyy/mm/dd')) or";
                                //DisApproveEmp += this.UltraWebGridKQE.DisplayLayout.Rows[i].Cells.FromKey("WorkNo").Text +
                                //    "/" + this.UltraWebGridKQE.DisplayLayout.Rows[i].Cells.FromKey("LocalName").Text + "、";
                                //intDisApprove += 1;
                            }
                        }
                        if (intDisApprove > 0)
                        {
                            condition = condition.Substring(0, condition.Length - 2);
                            condition = " and (" + condition + ")";
                            DisApproveEmp = "|" + DisApproveEmp.Substring(0, DisApproveEmp.Length - 1);
                        }
                        else
                        {
                            condition = "";
                            DisApproveEmp = "";
                        }
                        ApRemark = ApRemark + DisApproveEmp;
                        if (intDisApprove == UltraWebGridKQE.Rows.Count)
                        {
                            bll.SaveDisAuditData(BillNo, CurrentUserInfo.Personcode, BillTypeNo, ApRemark, list_remark, list_id,logmodel);
                        }
                        else
                        {
                            bll.SaveAuditData(BillNo, CurrentUserInfo.Personcode, BillTypeNo, ApRemark, condition, list_remark, list_id,logmodel);
                        }
                        break;

                    #endregion 
                    #region 未刷補卡

                    case "KQMMakeup":      //未刷補卡
                        list_remark = new List<string>();
                        list_id = new List<string>();

                        TemplatedColumn tcolKQU = (TemplatedColumn)UltraWebGridKQU.Bands[0].Columns[0];
                        for (int i = 0; i < UltraWebGridKQU.Rows.Count; i++)
                        {
                            OTAid = this.UltraWebGridKQU.DisplayLayout.Rows[i].Cells.FromKey("ID").Text;//序號
                            DisSignRmark = this.UltraWebGridKQU.DisplayLayout.Rows[i].Cells.FromKey("DISSIGNRMARK").Text;//簽核意見欄位

                            CellItem GridItem = (CellItem)tcolKQU.CellItems[i];
                            CheckBox chkIsHaveRight = (CheckBox)(GridItem.FindControl("CheckBoxCell"));
                            if (chkIsHaveRight.Checked)
                            {
                                if (!string.IsNullOrEmpty(DisSignRmark))
                                {
                                    list_remark.Add(DisSignRmark);
                                }
                                else
                                {
                                    Page.ClientScript.RegisterStartupScript(GetType(), "show", "alert('" + Message.wfm_disapprove_remark + "')", true);
                                    return;
                                }
                                list_id.Add(OTAid);
                                condition += " id = '" + OTAid + "' or";
                                DisApproveEmp += this.UltraWebGridKQU.DisplayLayout.Rows[i].Cells.FromKey("WorkNo").Text +
                                    "/" + this.UltraWebGridKQU.DisplayLayout.Rows[i].Cells.FromKey("LocalName").Text + "、";
                                intDisApprove += 1;//拒簽


                            }
                        }
                        if (intDisApprove > 0)
                        {
                            condition = condition.Substring(0, condition.Length - 2);
                            condition = " and (" + condition + ")";
                            DisApproveEmp = "|" + DisApproveEmp.Substring(0, DisApproveEmp.Length - 1);
                        }
                        else
                        {
                            condition = "";
                            DisApproveEmp = "";
                        }
                        ApRemark = ApRemark + DisApproveEmp;
                        if (intDisApprove == UltraWebGridKQU.Rows.Count)
                        {
                            bll.SaveDisAuditData(BillNo, CurrentUserInfo.Personcode, BillTypeNo, ApRemark, list_remark, list_id,logmodel);
                        }
                        else
                        {
                            bll.SaveAuditData(BillNo, CurrentUserInfo.Personcode, BillTypeNo, ApRemark, condition, list_remark, list_id,logmodel);
                        }
                        break;

                    #endregion

                    #region 免卡人員加班

                    case "KQMOTMA":     //免卡人員加班
                        list_remark = new List<string>();
                        list_id = new List<string>();

                        TemplatedColumn tcolTMA = (TemplatedColumn)UltraWebGridTMA.Bands[0].Columns[0];
                        for (int i = 0; i < UltraWebGridTMA.Rows.Count; i++)
                        {
                            OTAid = this.UltraWebGridTMA.DisplayLayout.Rows[i].Cells.FromKey("ID").Text;//序號
                            DisSignRmark = this.UltraWebGridTMA.DisplayLayout.Rows[i].Cells.FromKey("DISSIGNRMARK").Text;//拒簽欄位

                            CellItem GridItem = (CellItem)tcolTMA.CellItems[i];
                            CheckBox chkIsHaveRight = (CheckBox)(GridItem.FindControl("CheckBoxCell"));
                            if (chkIsHaveRight.Checked)
                            {
                                if (!string.IsNullOrEmpty(DisSignRmark))
                                {
                                    list_remark.Add(DisSignRmark);
                                }
                                else
                                {
                                    Page.ClientScript.RegisterStartupScript(GetType(), "show", "alert('" + Message.wfm_disapprove_remark + "')", true);
                                    return;
                                }
                                list_id.Add(OTAid);
                                condition += " id = '" + OTAid + "' or";
                                DisApproveEmp += this.UltraWebGridTMA.DisplayLayout.Rows[i].Cells.FromKey("WorkNo").Text +
                                    "/" + this.UltraWebGridTMA.DisplayLayout.Rows[i].Cells.FromKey("LocalName").Text + "、";
                                intDisApprove += 1;//拒簽


                            }
                        }
                        if (intDisApprove > 0)
                        {
                            condition = condition.Substring(0, condition.Length - 2);
                            condition = " and (" + condition + ")";
                            DisApproveEmp = "|" + DisApproveEmp.Substring(0, DisApproveEmp.Length - 1);
                        }
                        else
                        {
                            condition = "";
                            DisApproveEmp = "";
                        }
                        ApRemark = ApRemark + DisApproveEmp;
                        if (intDisApprove == UltraWebGridTMA.Rows.Count)
                        {
                            bll.SaveDisAuditData(BillNo, CurrentUserInfo.Personcode, BillTypeNo, ApRemark, list_remark, list_id,logmodel);
                        }
                        else
                        {
                            bll.SaveAuditData(BillNo, CurrentUserInfo.Personcode, BillTypeNo, ApRemark, condition, list_remark, list_id,logmodel);
                        }
                        break;

                    #endregion
                    #region 月加班匯總簽核單KQM
                    case "KQMMonthTotal"://月加班匯總簽核單KQM
                        list_remark = new List<string>();
                        List<string> list_work = new List<string>();
                        List<string> list_year = new List<string>();
                        tcolOTA = (TemplatedColumn)UltraWebGridKQM.Bands[0].Columns[0];
                        for (int i = 0; i < UltraWebGridKQM.Rows.Count; i++)
                        {
                            string WorkNo = this.UltraWebGridKQM.DisplayLayout.Rows[i].Cells.FromKey("WorkNo").Text;//序號
                            string YearMonth = this.UltraWebGridKQM.DisplayLayout.Rows[i].Cells.FromKey("YearMonth").Text;//序號
                           // OTAid = this.UltraWebGridLeaveApply.DisplayLayout.Rows[i].Cells.FromKey("ID").Text;//序號
                            DisSignRmark = this.UltraWebGridKQM.DisplayLayout.Rows[i].Cells.FromKey("DISSIGNRMARK").Text;//拒簽欄位
                            CellItem GridItem = (CellItem)tcolOTA.CellItems[i];
                            CheckBox chkIsHaveRight = (CheckBox)(GridItem.FindControl("CheckBoxCell"));
                            if (chkIsHaveRight.Checked)
                            {
                                if (!string.IsNullOrEmpty(DisSignRmark))
                                {
                                    list_remark.Add(DisSignRmark);
                                }
                                else
                                {
                                    Page.ClientScript.RegisterStartupScript(GetType(), "show", "alert('" + Message.wfm_disapprove_remark + "')", true);
                                    return;
                                }
                                list_work.Add(WorkNo);
                                list_year.Add(YearMonth);
                                //list_id.Add(OTAid);
                                //condition += " id = '" + OTAid + "' or";
                                condition += " WorkNo = '" + WorkNo + "' and YearMonth = '" + YearMonth + "'  or";
                                DisApproveEmp += this.UltraWebGridKQM.DisplayLayout.Rows[i].Cells.FromKey("WorkNo").Text +
                                    "/" + this.UltraWebGridKQM.DisplayLayout.Rows[i].Cells.FromKey("LocalName").Text + "、";
                                intDisApprove += 1;
                            }
                        }
                        if (intDisApprove > 0)
                        {
                            condition = condition.Substring(0, condition.Length - 2);
                            condition = " and (" + condition + ")";
                            DisApproveEmp = "|" + DisApproveEmp.Substring(0, DisApproveEmp.Length - 1);
                        }
                        else
                        {
                            condition = "";
                            DisApproveEmp = "";
                        }
                        ApRemark = ApRemark + DisApproveEmp;
                        if (intDisApprove == UltraWebGridKQM.Rows.Count)
                        {
                            bll.SaveDisAuditData(BillNo, CurrentUserInfo.Personcode, BillTypeNo, ApRemark, list_remark, list_work, list_year,logmodel);
                        }
                        else
                        {
                            bll.SaveAuditData(BillNo, CurrentUserInfo.Personcode, BillTypeNo, ApRemark, condition, list_remark,list_work, list_year,logmodel);
                        }
                        break;
                    #endregion
                    default:
                        break;
                }
                Response.Write("<script type='text/javascript'>alert('" + Message.wfm_message_approvecomplete + "');window.opener.document.all.ButtonReset.click();window.close();</script>");
                this.ButtonApproveAgree.Enabled = false;
                this.ButtonDisApprove.Enabled = false;
            }
            catch (System.Exception ex)
            {
                this.WriteMessage(2, (ex.InnerException == null ? ex.Message : ex.InnerException.Message));
            }
        }

        #region

        protected void apAuditData(UltraWebGrid ultraWebGrid, string condition, List<string> list_remark, List<string> list_id, string OTAid, string DisSignRmark, string DisApproveEmp, int intDisApprove, string ApRemark, string BillNo, string BillTypeNo)
        {
            list_remark = new List<string>();
            list_id = new List<string>();

            TemplatedColumn tcolTMA = (TemplatedColumn)UltraWebGridKQU.Bands[0].Columns[0];
            for (int i = 0; i < UltraWebGridKQU.Rows.Count; i++)
            {
                OTAid = ultraWebGrid.DisplayLayout.Rows[i].Cells.FromKey("ID").Text;//序號
                DisSignRmark = ultraWebGrid.DisplayLayout.Rows[i].Cells.FromKey("DISSIGNRMARK").Text;//拒簽欄位

                CellItem GridItem = (CellItem)tcolTMA.CellItems[i];
                CheckBox chkIsHaveRight = (CheckBox)(GridItem.FindControl("CheckBoxCell"));
                if (chkIsHaveRight.Checked)
                {
                    if (!string.IsNullOrEmpty(DisSignRmark))
                    {
                        list_remark.Add(DisSignRmark);
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(GetType(), "show", "alert('" + Message.wfm_disapprove_remark + "')", true);
                        return;
                    }
                    list_id.Add(OTAid);
                    condition += " id = '" + OTAid + "' or";
                    DisApproveEmp += ultraWebGrid.DisplayLayout.Rows[i].Cells.FromKey("WorkNo").Text +
                        "/" + ultraWebGrid.DisplayLayout.Rows[i].Cells.FromKey("LocalName").Text + "、";
                    intDisApprove += 1;//拒簽


                }
            }
            if (intDisApprove > 0)
            {
                condition = condition.Substring(0, condition.Length - 2);
                condition = " and (" + condition + ")";
                DisApproveEmp = "|" + DisApproveEmp.Substring(0, DisApproveEmp.Length - 1);
            }
            else
            {
                condition = "";
                DisApproveEmp = "";
            }
            ApRemark = ApRemark + DisApproveEmp;
            if (intDisApprove == UltraWebGridKQE.Rows.Count)
            {
                bll.SaveDisAuditData(BillNo, CurrentUserInfo.Personcode, BillTypeNo, ApRemark, list_remark, list_id,logmodel);
            }
            else
            {
                bll.SaveAuditData(BillNo, CurrentUserInfo.Personcode, BillTypeNo, ApRemark, condition, list_remark, list_id,logmodel);
            }
        }

        #endregion
        //彈出信息
        protected void WriteMessage(int messageType, string message)
        {
            switch (messageType)
            {
                case 0:
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "jump", "<script language='JavaScript'>window.status='" + message.Replace("\"", "'").Replace("\r", "").Replace("\n", "") + "';</script>");
                    break;

                case 1:
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "jump", "<script language='JavaScript'>alert(\"" + message.Replace("\"", "'").Replace("\r", "").Replace("\n", "") + "\");</script>");
                    break;

                case 2:
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "jump", "<script language='JavaScript'>alert(\"" + message.Replace("\"", "'").Replace("\r", "").Replace("\n", "") + "\");</script>");
                    break;
            }
        }

        //加班預報簽核單OTA 數據綁定
        protected void UltraWebGridOTA_DataBound(object sender, EventArgs e)
        {
            string OTMWeekStartFromMonday = bll.GetValue("select nvl(max(paravalue),'Y') from GDS_SC_PARAMETER where paraname='OTMWeekStartFromMonday'");
            TemplatedColumn tcolOTA = (TemplatedColumn)UltraWebGridOTA.Bands[0].Columns[0];
            string OTDate = "";
            string Sunday = "";
            string condition = "";
            double AdvanceTotal = 0;
            double RealTotal = 0;
            double AdvanceTotalG1 = 0;
            double AdvanceTotalG2 = 0;
            double AdvanceTotalG3 = 0;
            double RealTotalG1 = 0;
            double RealTotalG2 = 0;
            double RealTotalG3 = 0;
            string ISPay = "";
            string OTType = "";
            #region 龍華總部周邊HR。G2，G3名稱更改為G2(休息日上班)，G3(法定假日上班) add by xukai 20111010
            string LHZBIsDisplayG2G3 = "";
            try
            {
                LHZBIsDisplayG2G3 = System.Configuration.ConfigurationManager.AppSettings["LHZBIsDisplayG2G3"].ToString();
            }
            catch
            {
                LHZBIsDisplayG2G3 = "N";
            }
            #endregion
            TemplatedColumn tcolOTAISPay = (TemplatedColumn)UltraWebGridOTA.Bands[0].Columns[UltraWebGridOTA.Bands[0].Columns.FromKey("CheckBoxISPay").Index];
            for (int i = 0; i < UltraWebGridOTA.Rows.Count; i++)
            {
                OTType = UltraWebGridOTA.Rows[i].Cells.FromKey("OTType").Text;
                #region add by xukai 20111010 G2，G3名稱更改為G2(休息日上班)，G3(法定假日上班)
                if (LHZBIsDisplayG2G3 == "Y")
                {
                    if (OTType == "G2")
                    {
                        UltraWebGridOTA.Rows[i].Cells.FromKey("OTType").Value = OTType + "(" + this.GetResouseValue("otm.g2.remark") + ")";
                    }
                    else if (OTType == "G3")
                    {
                        UltraWebGridOTA.Rows[i].Cells.FromKey("OTType").Value = OTType + "(" + this.GetResouseValue("otm.g3.remark") + ")";
                    }
                }

                #endregion

                if (this.HiddenisProject.Value.Equals("Y"))
                {
                    // OTType = UltraWebGridOTA.Rows[i].Cells.FromKey("OTType").Text;
                    if (UltraWebGridOTA.Rows[i].Cells.FromKey("ISPay").Value != null)
                    {
                        ISPay = UltraWebGridOTA.Rows[i].Cells.FromKey("ISPay").Text;
                    }
                    else
                    {
                        ISPay = "N";
                    }

                    CellItem GridItemISPay = (CellItem)tcolOTAISPay.CellItems[i];
                    CheckBox chkIsHaveRightISPay = (CheckBox)(GridItemISPay.FindControl("CheckBoxCellISPay"));
                    if (OTType.Equals("G2"))
                    {
                        if (ISPay.Equals("Y"))
                        {
                            chkIsHaveRightISPay.Checked = true;
                        }
                    }
                    else
                    {
                        chkIsHaveRightISPay.Checked = true;
                        chkIsHaveRightISPay.Enabled = false;
                    }
                    #region 專案發薪
                    //update by xukai 20110801
                    //chkIsHaveRightISPay.Enabled = false;
                    #endregion

                }
                CheckBox chkIsHaveRight = (CheckBox)(((CellItem)(tcolOTA.CellItems[i])).FindControl("CheckBoxCell"));
                chkIsHaveRight.Attributes.Add("onclick", "CheckOTA(" + i + ",'UltraWebGridOTA')");
                if (UltraWebGridOTA.Rows[i].Cells.FromKey("OTMSGFlag").Value != null)
                {
                    switch (UltraWebGridOTA.Rows[i].Cells.FromKey("OTMSGFlag").Text)
                    {
                        case "1":
                            UltraWebGridOTA.Rows[i].Style.ForeColor = System.Drawing.Color.Green;
                            //UltraWebGridOTA.Rows[i].Cells.FromKey("WorkDesc").Style.ForeColor = System.Drawing.Color.Green;
                            break;
                        case "2":
                            UltraWebGridOTA.Rows[i].Style.ForeColor = System.Drawing.Color.Blue;
                            break;
                        case "3":
                            UltraWebGridOTA.Rows[i].Style.ForeColor = System.Drawing.Color.SaddleBrown;
                            break;
                        case "4":
                            UltraWebGridOTA.Rows[i].Style.ForeColor = System.Drawing.Color.Maroon;
                            break;
                        case "5":
                            UltraWebGridOTA.Rows[i].Style.ForeColor = System.Drawing.Color.Tomato;
                            break;
                        case "6":
                            UltraWebGridOTA.Rows[i].Style.ForeColor = System.Drawing.Color.Red;
                            break;
                    }
                }
                OTDate = Convert.ToDateTime(UltraWebGridOTA.Rows[i].Cells.FromKey("OTDate").Text).ToString("yyyy/MM/dd");
                AdvanceTotalG1 = 0;
                AdvanceTotalG2 = 0;
                AdvanceTotalG3 = 0;
                RealTotalG1 = 0;
                RealTotalG2 = 0;
                RealTotalG3 = 0;
                this.tempDataTable = bll.GetDataSetBySQL("select nvl(sum(Decode(OTType,'G1',Hours,0)),0)G1Total,nvl(sum(Decode(OTType,'G2',Hours,0)),0)G2Total,nvl(sum(Decode(OTType,'G3',Hours,0)),0)G3Total from GDS_ATT_ADVANCEAPPLY where WorkNo='" + UltraWebGridOTA.Rows[i].Cells.FromKey("WorkNo").Text + "' and otdate>last_day(add_months(to_date('" + OTDate + "','yyyy/MM/dd'),-1)) and OTDate<=to_date('" + OTDate + "','yyyy/MM/dd') and status<'3' and importflag='N' and ImportRemark is null").Tables["TempTable"];
                if (this.tempDataTable.Rows.Count > 0)
                {
                    AdvanceTotalG1 = Convert.ToDouble(this.tempDataTable.Rows[0]["G1Total"]);
                    AdvanceTotalG2 = Convert.ToDouble(this.tempDataTable.Rows[0]["G2Total"]);
                    AdvanceTotalG3 = Convert.ToDouble(this.tempDataTable.Rows[0]["G3Total"]);
                }
                this.tempDataTable = bll.GetDataSetBySQL("select nvl(sum(Decode(OTType,'G1',ConfirmHours,0)),0)G1Total,nvl(sum(Decode(OTType,'G2',ConfirmHours,0)),0)G2Total,nvl(sum(Decode(OTType,'G3',ConfirmHours,0)),0)G3Total from GDS_ATT_REALAPPLY where WorkNo='" + UltraWebGridOTA.Rows[i].Cells.FromKey("WorkNo").Text + "' and otdate>last_day(add_months(to_date('" + OTDate + "','yyyy/MM/dd'),-1)) and OTDate<=to_date('" + OTDate + "','yyyy/MM/dd') and status<>'3'").Tables["TempTable"];
                if (this.tempDataTable.Rows.Count > 0)
                {
                    RealTotalG1 = Convert.ToDouble(this.tempDataTable.Rows[0]["G1Total"]);
                    RealTotalG2 = Convert.ToDouble(this.tempDataTable.Rows[0]["G2Total"]);
                    RealTotalG3 = Convert.ToDouble(this.tempDataTable.Rows[0]["G3Total"]);
                }
                UltraWebGridOTA.Rows[i].Cells.FromKey("G1Total").Value = AdvanceTotalG1 + RealTotalG1;
                UltraWebGridOTA.Rows[i].Cells.FromKey("G2Total").Value = AdvanceTotalG2 + RealTotalG2;
                UltraWebGridOTA.Rows[i].Cells.FromKey("G3Total").Value = AdvanceTotalG3 + RealTotalG3;
                //this.tempDataTable = ((eBFW.Sys.ServiceLocator)Session["serviceLocator"]).GetFunctionData().GetDataSetBySQL("select nvl(sum(Decode(OTType,'G1',ConfirmHours,0)),0)G1Total,nvl(sum(Decode(OTType,'G2',ConfirmHours,0)),0)G2Total,nvl(sum(Decode(OTType,'G3',ConfirmHours,0)),0)G3Total from otm_realApply where WorkNo='" + UltraWebGridOTA.Rows[i].Cells.FromKey("WorkNo").Text + "' and otdate>last_day(add_months(to_date('" + OTDate + "','yyyy/MM/dd'),-1)) and OTDate<=to_date('" + OTDate + "','yyyy/MM/dd') and status='2'").Tables["TempTable"];
                //if (this.tempDataTable.Rows.Count > 0)
                //{
                //    UltraWebGridOTA.Rows[i].Cells.FromKey("G1Total").Value = Convert.ToDouble(UltraWebGridOTA.Rows[i].Cells.FromKey("G1Total").Value) + Convert.ToDouble(this.tempDataTable.Rows[0]["G1Total"]);
                //    UltraWebGridOTA.Rows[i].Cells.FromKey("G2Total").Value = Convert.ToDouble(UltraWebGridOTA.Rows[i].Cells.FromKey("G2Total").Value) + Convert.ToDouble(this.tempDataTable.Rows[0]["G2Total"]);
                //    UltraWebGridOTA.Rows[i].Cells.FromKey("G3Total").Value = Convert.ToDouble(UltraWebGridOTA.Rows[i].Cells.FromKey("G3Total").Value) + Convert.ToDouble(this.tempDataTable.Rows[0]["G3Total"]);
                //}
                if (OTMWeekStartFromMonday.Equals("Y"))
                {
                    Sunday = CalculateLastDateOfWeek(Convert.ToDateTime(OTDate)).AddDays(-6).ToString("yyyy/MM/dd");
                }
                else
                {
                    Sunday = CalculateLastDateOfWeek(Convert.ToDateTime(OTDate)).ToString("yyyy/MM/dd");
                    if (!Sunday.Equals(OTDate))
                    {
                        Sunday = CalculateLastDateOfWeek(Convert.ToDateTime(OTDate)).AddDays(-7).ToString("yyyy/MM/dd");
                    }
                }
                //update by xukai 20111014 重新計算加班月統計。

                //AdvanceTotal = Convert.ToDouble(((eBFW.Sys.ServiceLocator)Session["serviceLocator"]).GetFunctionData().GetValue("select nvl(sum(Hours),0)WeekTotal from otm_advanceApply where WorkNo='" + UltraWebGridOTA.Rows[i].Cells.FromKey("WorkNo").Text + "' and otdate>=to_date('" + Sunday + "','yyyy/MM/dd') and otdate<=to_date('" + Sunday + "','yyyy/MM/dd')+6 and status<>'3' and importflag='N'"));
                AdvanceTotal = Convert.ToDouble(bll.GetValue("select nvl(sum(Hours),0)WeekTotal from GDS_ATT_ADVANCEAPPLY where WorkNo='" + UltraWebGridOTA.Rows[i].Cells.FromKey("WorkNo").Text + "' and otdate>=to_date('" + Sunday + "','yyyy/MM/dd') and otdate<=to_date('" + Sunday + "','yyyy/MM/dd')+6 and status<>'3' and importflag='N' and ImportRemark is null"));
                RealTotal = Convert.ToDouble(bll.GetValue("select nvl(sum(ConfirmHours),0)WeekTotal from GDS_ATT_REALAPPLY where WorkNo='" + UltraWebGridOTA.Rows[i].Cells.FromKey("WorkNo").Text + "' and otdate>=to_date('" + Sunday + "','yyyy/MM/dd') and otdate<=to_date('" + Sunday + "','yyyy/MM/dd')+6 and status<>'3'"));
                UltraWebGridOTA.Rows[i].Cells.FromKey("WeekTotal").Value = AdvanceTotal + RealTotal;

                condition = "SELECT nvl(sum(nvl(work_hours,8))/8,0) FROM GDS_ATT_DAYREPORT b where b.workno='" +
                    UltraWebGridOTA.Rows[i].Cells.FromKey("WorkNo").Text + "'  and b.OTDate >= to_date('" + Sunday + "','yyyy/mm/dd') AND  b.OTDate <= to_date('" + Sunday + "','yyyy/MM/dd')+6";
                UltraWebGridOTA.Rows[i].Cells.FromKey("WeekWorkDays").Value = bll.GetValue(condition);

                condition = "select nvl(sum(Hours),0)TodayTotal from GDS_ATT_ADVANCEAPPLY where WorkNo='" + UltraWebGridOTA.Rows[i].Cells.FromKey("WorkNo").Text + "' and otdate=to_date('" + OTDate + "','yyyy/MM/dd') and status<'3'";
                UltraWebGridOTA.Rows[i].Cells.FromKey("TodayTotal").Value = bll.GetValue(condition);

                condition = "select nvl(max(madjust1),0) from GDS_ATT_MONTHTOTAL where workno='" + UltraWebGridOTA.Rows[i].Cells.FromKey("WorkNo").Text + "' AND yearmonth>=TO_char(to_date('" + OTDate + "','yyyy/MM/dd'),'yyyyMM')";
                UltraWebGridOTA.Rows[i].Cells.FromKey("MAdjust1").Value = bll.GetValue(condition);
            }
        }

        #region 計算本周結束日期
        ///   <summary>   
        ///   計算本周結束日期（禮拜日的日期）   
        ///   </summary>   
        ///   <param   name="someDate">該周中任意一天</param>   
        ///   <returns>返回禮拜日日期，後面的具體時、分、秒和傳入值相等</returns>   
        public static DateTime CalculateLastDateOfWeek(DateTime someDate)
        {
            int i = someDate.DayOfWeek - DayOfWeek.Sunday;
            if (i != 0) i = 7 - i;//   因為枚舉原因，Sunday排在最前，相減間隔要被7減。   
            TimeSpan ts = new TimeSpan(i, 0, 0, 0);
            return someDate.Add(ts);
        }
        #endregion

        //考勤異常簽核單KQE
        protected void UltraWebGridKQE_DataBound(object sender, EventArgs e)
        {
            string WorkNo = "", KqDate = "";
            Bll_AbnormalAttendanceHandle bll_getExcep = new Bll_AbnormalAttendanceHandle();
            for (int i = 0; i < UltraWebGridKQE.Rows.Count; i++)
            {
                WorkNo = UltraWebGridKQE.Rows[i].Cells.FromKey("WorkNo").Value == null ? "" : UltraWebGridKQE.Rows[i].Cells.FromKey("WorkNo").Text;
                KqDate = UltraWebGridKQE.Rows[i].Cells.FromKey("KQDate").Value == null ? "" : Convert.ToDateTime(UltraWebGridKQE.Rows[i].Cells.FromKey("KQDate").Text).ToString("yyyy/MM/dd");
                UltraWebGridKQE.Rows[i].Cells.FromKey("ExcepTionQty").Value = bll_getExcep.GetExceptionqty(WorkNo, KqDate);
            }
        }

        //免卡人員加班
        protected void UltraWebGridTMA_DataBound(object sender, EventArgs e)
        {
            string WorkNo = "", KqDate = "";
            for (int i = 0; i < UltraWebGridKQE.Rows.Count; i++)
            {
                WorkNo = UltraWebGridKQE.Rows[i].Cells.FromKey("WorkNo").Value == null ? "" : UltraWebGridKQE.Rows[i].Cells.FromKey("WorkNo").Text;
                KqDate = UltraWebGridKQE.Rows[i].Cells.FromKey("KQDate").Value == null ? "" : Convert.ToDateTime(UltraWebGridKQE.Rows[i].Cells.FromKey("KQDate").Text).ToString("yyyy/MM/dd");
                // UltraWebGridKQE.Rows[i].Cells.FromKey("ExcepTionQty").Value = bll_getExcep.GetExceptionqty(WorkNo, KqDate);
            }
            /**
            string WorkNo = "", KqDate = "";
            Bll_AbnormalAttendanceHandle bll_getExcep = new Bll_AbnormalAttendanceHandle();
            for (int i = 0; i < UltraWebGridKQE.Rows.Count; i++)
            {
                WorkNo = UltraWebGridKQE.Rows[i].Cells.FromKey("WorkNo").Value == null ? "" : UltraWebGridKQE.Rows[i].Cells.FromKey("WorkNo").Text;
                KqDate = UltraWebGridKQE.Rows[i].Cells.FromKey("KQDate").Value == null ? "" : Convert.ToDateTime(UltraWebGridKQE.Rows[i].Cells.FromKey("KQDate").Text).ToString("yyyy/MM/dd");
                UltraWebGridKQE.Rows[i].Cells.FromKey("ExcepTionQty").Value = bll_getExcep.GetExceptionqty(WorkNo, KqDate);
            }**/
        }

        //未刷補卡
        //TODO CODE 
        protected void UltraWebGridKQU_DataBound(object sender, EventArgs e)
        {
            string WorkNo = "", KqDate = "";
            for (int i = 0; i < UltraWebGridKQE.Rows.Count; i++)
            {
                WorkNo = UltraWebGridKQE.Rows[i].Cells.FromKey("WorkNo").Value == null ? "" : UltraWebGridKQE.Rows[i].Cells.FromKey("WorkNo").Text;
                KqDate = UltraWebGridKQE.Rows[i].Cells.FromKey("KQDate").Value == null ? "" : Convert.ToDateTime(UltraWebGridKQE.Rows[i].Cells.FromKey("KQDate").Text).ToString("yyyy/MM/dd");
                // UltraWebGridKQE.Rows[i].Cells.FromKey("ExcepTionQty").Value = bll_getExcep.GetExceptionqty(WorkNo, KqDate);
            }
        }

        private void Query(string BillNo)
        {
            string condition = "";
            //抓取簽核流程
            condition = "and a.BillNo ='" + BillNo + "'";
            this.dataSet = bll.GetAuditDataByCondition(condition);
            UltraWebGridStatus.DataSource = this.dataSet.Tables["WFM_AuditStatus"].DefaultView;
            UltraWebGridStatus.DataBind();
            //獲取表頭基本信息
            switch (this.HiddenBillTypeNo.Value)
            {
                //case "KQMLeaveApply"://請假申請簽核單OTE
                case "HolidayApplyC"://Holiday (<=5 day) HAC
                case "HolidayApplyD"://Holiday (>5 day) HAD
                case "HolidayApplyH"://Other leave HAH
                case "ExEvection"://國外出差申請簽核單OTE
                case "ExEvectionCancel"://國外銷差申請簽核單ERC
                case "CEMVacReportRequest"://述職/休假申請簽核單VRN
                case "InEvection"://國內出差申請簽核單

                case "InEvectionCancel"://國內銷差申請簽核單

                case "ETMCurriculaPlan"://課程規劃簽核單ECP
                    //condition = "SELECT * FROM (select (case when (select BillTypeCode From BFW_BillType c Where c.BillTypeNo=substr(a.BillNo,0,3))='KQMLeaveApply' " +
                    //        "then (select BillTypeName From BFW_BillType c Where c.BillTypeNo=substr(a.BillNo,0,3))||'-'|| " +
                    //        "(select LVTypeName from KQM_LeaveType c where c.LVTypeCode= " +
                    //        "(select LVTypeCode from kqm_leaveapply d where d.BillNo=a.BillNo and rownum=1)) " +
                    //        "else (select BillTypeName From BFW_BillType c Where c.BillTypeCode=a.BillTypeCode) end) BillTypeName " +
                    //        "from WFM_Bill a " +
                    //        "Where a.BillNo ='" + BillNo + "')";
                    //this.labelBillTypeName.Text = ((eBFW.Sys.ServiceLocator)Session["serviceLocator"]).GetFunctionData().GetValue(condition);
                    break;
                default:
                    condition = "and a.BillNo ='" + BillNo + "'";
                    this.tempDataTable = bll.GetDataByCondition_Bill(condition).Tables["WFM_Bill"];
                    if (this.tempDataTable.Rows.Count > 0)
                    {
                        this.textBoxOrgName.Text = bll.GetAllDept(tempDataTable.Rows[0]["OrgCode"].ToString(), true);
                        this.HiddenOrgCode.Value = tempDataTable.Rows[0]["OrgCode"].ToString();
                        this.textBoxApplyMan.Text = tempDataTable.Rows[0]["ApplyManName"].ToString();
                        if (tempDataTable.Rows[0]["ApplyDate"].ToString().Length > 0)
                        {
                            this.textBoxApplyDate.Text = string.Format("{0:" + System.Convert.ToString("yyyy/MM/dd") + "}", Convert.ToDateTime(tempDataTable.Rows[0]["ApplyDate"].ToString()));
                        }
                        this.labelBillTypeName.Text = tempDataTable.Rows[0]["BillTypeName"].ToString();
                    }
                    else
                    {
                        Response.Write("<script type='text/javascript'>alert('" + Message.wfm_message_billdeleted + "');window.close();</script>");
                        return;
                    }
                    break;
            }


            string inWorkNum = "0", AdvanceNum = "0", LeaveNum = "0", Percent = "0";

            switch (this.HiddenBillTypeNo.Value)
            {
                case "LeaveTypeA"://辭職申請簽核單LVA
                case "LeaveTypeB"://自離申請簽核單LVB
                case "ExEvection"://國外出差申請簽核單OTE
                case "ExEvectionCancel"://國外銷差申請簽核單ERC
                case "CEMVacReportRequest"://述職/休假申請簽核單VRN
                case "InEvection"://國內出差申請簽核單

                case "InEvectionCancel"://國內銷差申請簽核單

                case "ETMCurriculaPlan"://課程規劃簽核單ECP
                case "SCMRequire"://鞋櫃申請單SCM
                case "OrgMove"://調動申請MVB
                    break;
                default:
                    //今日在職人數
                    condition = "select count(1) from GDS_ATT_EMPLOYEE a where a.flag='Local' and a.Status='0' " +
                                "and a.DCode in (SELECT DepCode FROM GDS_SC_DEPARTMENT START WITH  " +
                                "depcode='" + this.HiddenOrgCode.Value + "' CONNECT BY PRIOR depcode = parentdepcode) ";
                    inWorkNum = bll.GetValue(condition);
                    //今日加班預報人數
                    condition = "select count(1) from GDS_ATT_ADVANCEAPPLY a, GDS_ATT_EMPLOYEE b " +
                                "where a.WorkNo=b.WorkNo and a.OTDate=trunc(sysdate) and b.flag='Local' and b.Status='0' " +
                                "AND b.DCode in (SELECT DepCode FROM GDS_SC_DEPARTMENT START WITH  " +
                                "depcode='" + this.HiddenOrgCode.Value + "' CONNECT BY PRIOR depcode = parentdepcode) ";
                    AdvanceNum = bll.GetValue(condition);
                    //今日請假人數
                    condition = "select count(1) from GDS_ATT_LEAVEAPPLY a, GDS_ATT_EMPLOYEE b " +
                                "where a.WorkNo=b.WorkNo and a.StartDate>=trunc(sysdate) and a.EndDate<=trunc(sysdate) " +
                                "and b.flag='Local' and b.Status='0' " +
                                "AND b.DCode in (SELECT DepCode FROM GDS_SC_DEPARTMENT START WITH  " +
                                "depcode='" + this.HiddenOrgCode.Value + "' CONNECT BY PRIOR depcode = parentdepcode) ";
                    LeaveNum = bll.GetValue(condition);
                    break;
            }
            switch (this.HiddenBillTypeNo.Value)
            {

                case "D001"://加班預報簽核單OTA 需要做的操作
                case "OTMProjectApply":
                    string isProject = bll.GetValue("Select nvl(Max(isProject),'N') from GDS_ATT_ADVANCEAPPLY where billno='" + BillNo + "' and rownum<=1");
                    if (isProject.Equals("Y"))
                    {
                        //this.labelBillTypeName.Text = ((eBFW.Sys.ServiceLocator)Session["serviceLocator"]).GetFunctionData().GetValue("Select BillTypeName From Bfw_BillType Where BillTypeCode='OTMAdvanceApplyG3'");
                        this.HiddenisProject.Value = isProject;
                    }
                    else
                    {
                        this.UltraWebGridOTA.Bands[0].Columns.FromKey("CheckBoxISPay").Hidden = true;
                    }
                    condition = "";
                    condition += "and a.BillNo ='" + BillNo + "'";
                    if (!this.HiddenSFlag.Value.Equals("Y"))
                    {
                        condition += " and a.Status='1'";
                    }
                    this.dataSet = bll.GetDataByCondition(condition);
                    UltraWebGridOTA.Visible = true;
                    UltraWebGridOTA.DataSource = this.dataSet.Tables["OTM_AdvanceApply"].DefaultView;
                    UltraWebGridOTA.DataBind();

                    try
                    {
                        Percent = Convert.ToString(System.Math.Round(Convert.ToDecimal(AdvanceNum) / Convert.ToDecimal(inWorkNum) * 100, 0));
                    }
                    catch (System.Exception)
                    { }
                    this.labelMessage.Text = Message.wfm_inworknum + inWorkNum + "；" +
                                            Message.wfm_advancenum + AdvanceNum + "；" +
                                            Message.wfm_leavenum + LeaveNum + "；" +
                                            Message.wfm_advancepercent + Percent + "%；" +
                                            Message.wfm_billauditcount + this.dataSet.Tables["OTM_AdvanceApply"].Rows.Count.ToString();
                    break;
                case "KQMApplyOut"://外出申請簽核單KQT 需要做的操作
                    KQMEvectionApplyBll bllKQTOut = new KQMEvectionApplyBll();
                    KQMEvectionApplyModel modelKQTOut = new KQMEvectionApplyModel();
                    modelKQTOut.BillNo = BillNo;
                    //condition += "and a.BillNo ='" + BillNo + "'";
                    DataTable dt = bllKQTOut.GetEvectionList(modelKQTOut);
                    UltraWebGridKQT.Visible = true;
                    UltraWebGridKQT.DataSource = dt.DefaultView;
                    UltraWebGridKQT.DataBind();
                    this.labelMessage.Text = Message.wfm_inworknum + inWorkNum + "；" +
                                            Message.wfm_advancenum + AdvanceNum + "；" +
                                            Message.wfm_leavenum + LeaveNum + "；" +
                                            Message.wfm_advancepercent + Percent + "%；" +
                                            Message.wfm_billauditcount + dt.Rows.Count.ToString();
                    break;
                case "D002":
                    KQMLeaveApplyExportBll kqmLeaveApply = new KQMLeaveApplyExportBll();
                    LeaveApplyViewModel leaveApplyViewModel = new LeaveApplyViewModel();
                    leaveApplyViewModel.BillNo = BillNo;
                    DataTable dtLeaveApply = kqmLeaveApply.getLeaveApply(leaveApplyViewModel);
                    UltraWebGridLeaveApply.Visible = true;
                    UltraWebGridLeaveApply.DataSource = dtLeaveApply;
                    UltraWebGridLeaveApply.DataBind();
                    this.labelMessage.Text = Message.wfm_inworknum + inWorkNum + "；" +
                                      Message.wfm_advancenum + AdvanceNum + "；" +
                                      Message.wfm_leavenum + LeaveNum + "；" +
                                      Message.wfm_advancepercent + Percent + "%；" +
                                      Message.wfm_billauditcount + dtLeaveApply.Rows.Count.ToString();
                    break;

                case "KQMException"://考勤異常簽核單KQE
                    string Status = "";
                    condition = "";
                    condition += "and a.BillNo ='" + BillNo + "'";
                    if (!this.HiddenSFlag.Value.Equals("Y"))
                    {
                        Status = "4";
                    }
                    Bll_AbnormalAttendanceHandle bll_signabnormal = new Bll_AbnormalAttendanceHandle();
                    DataTable dt_signabnormal = bll_signabnormal.GetSignAbnormalAttendanceInfo(BillNo, Status);
                    UltraWebGridKQE.DataSource = dt_signabnormal;
                    UltraWebGridKQE.DataBind();
                    UltraWebGridKQE.Visible = true;

                    this.labelMessage.Text = Message.wfm_inworknum + inWorkNum + "；" + Message.wfm_leavenum + LeaveNum + "；" + Message.wfm_billauditcount + dt_signabnormal.Rows.Count.ToString();
                    break;
                case "KQMMonthTotal"://外出申請簽核單KQT 需要做的操作
                    OTMTotalQryModel modelMonthTotal = new OTMTotalQryModel();
                    OTMTotalQryBll bllOTMQry = new OTMTotalQryBll();
                    modelMonthTotal.BillNo = BillNo;
                    DataTable dt_monthtotal = bllOTMQry.GetOTMQryList(modelMonthTotal);
                    UltraWebGridKQM.Visible = true;
                    UltraWebGridKQM.DataSource = dt_monthtotal.DefaultView;
                    UltraWebGridKQM.DataBind();
                    this.labelMessage.Text = Message.wfm_inworknum + inWorkNum + "；" +
                                            Message.wfm_advancenum + AdvanceNum + "；" +
                                            Message.wfm_leavenum + LeaveNum + "；" +
                                            Message.wfm_advancepercent + Percent + "%；" +
                                            Message.wfm_billauditcount + dt_monthtotal.Rows.Count.ToString();
                    break;

                #region 免卡人員加班

                case "KQMOTMA":      //免卡人員加班TMA 

                    Status = "";
                    condition = "";
                    condition += "and a.BillNo ='" + BillNo + "'";
                    if (!this.HiddenSFlag.Value.Equals("Y"))
                    {
                        Status = "1";
                    }
                    OTMActivityApplyBll activityApplyBll = new OTMActivityApplyBll();
                    DataTable tmaTB = activityApplyBll.getAuditBillInfoByBillNo(condition, Status);// bll_signabnormal.GetSignAbnormalAttendanceInfo(BillNo, Status);
                    UltraWebGridTMA.DataSource = tmaTB;
                    UltraWebGridTMA.DataBind();
                    UltraWebGridTMA.Visible = true;


                    this.labelMessage.Text = Message.wfm_inworknum + inWorkNum + "；" + Message.wfm_leavenum + LeaveNum + "；" + Message.wfm_billauditcount + tmaTB.Rows.Count.ToString();

                    break;

                #endregion

                #region 未刷補卡

                case "KQMMakeup":   //未刷補卡
                    Status = "";
                    condition = "";
                    condition += "and a.BillNo ='" + BillNo + "'";
                    if (!this.HiddenSFlag.Value.Equals("Y"))
                    {
                        condition += " and a.Status='1' ";
                    }
                    //  Bll_AbnormalAttendanceHandle bll_signabnormal = new Bll_AbnormalAttendanceHandle();
                    // OTMActivityApplyBll KQMMakeupBll = new OTMActivityApplyBll();
                    WorkFlowCardMakeupBll cardMakeupBll = new WorkFlowCardMakeupBll();
                    int totalCount = 0;
                    DataTable tmaKQMMakeupTB = cardMakeupBll.getCardMakeupList(condition, 1, Int32.MaxValue, out totalCount);
                    // DataTable tmaKQMMakeupTB = new DataTable();// bll_signabnormal.GetSignAbnormalAttendanceInfo(BillNo, Status);
                    UltraWebGridKQU.DataSource = tmaKQMMakeupTB;
                    UltraWebGridKQU.DataBind();
                    UltraWebGridKQU.Visible = true;


                    this.labelMessage.Text = Message.wfm_inworknum + inWorkNum + "；" + Message.wfm_leavenum + LeaveNum + "；" + Message.wfm_billauditcount + tmaKQMMakeupTB.Rows.Count.ToString();
                    break;
                #endregion

                default:
                    break;
            }
        }
        #region  月加班匯總查詢
        #region  WebGrid綁定初始化
        protected void UltraWebGridKQM_InitializeLayout(object sender, LayoutEventArgs e)
        {
            int i;
            string str = "";
            string time = "";
            OTMTotalQryModel modelMonthTotal = new OTMTotalQryModel();
            OTMTotalQryBll bllOTMQry = new OTMTotalQryBll();
            modelMonthTotal.BillNo = this.HiddenBillNo.Value;
            DataTable dt_monthtotal = bllOTMQry.GetOTMQryList(modelMonthTotal);
            if (dt_monthtotal.Rows.Count > 0)
            {
                str = dt_monthtotal.Rows[0]["YearMonth"].ToString();
                time = (str.Substring(0, 4) + "/" + str.Substring(4, 2)).ToString();
            }
            DateTime YearMonth = Convert.ToDateTime(time + "/01");
            if (this.HiddenYearMonth.Value.Length == 0)
            {
                this.HiddenYearMonth.Value = YearMonth.ToString();
                foreach (UltraGridColumn c in e.Layout.Bands[0].Columns)
                {
                    c.Header.RowLayoutColumnInfo.OriginY = 1;
                }
                ColumnHeader ch = new ColumnHeader(true)
                {
                    Caption = Resources.ControlText.gvApplyHours
                };
                ch.RowLayoutColumnInfo.OriginY = 0;
                ch.RowLayoutColumnInfo.OriginX = 8;
                ch.RowLayoutColumnInfo.SpanX = 3;
                ch.Style.HorizontalAlign = HorizontalAlign.Center;
                e.Layout.Bands[0].HeaderLayout.Add(ch);
                ColumnHeader ch2 = new ColumnHeader(true)
                {
                    Caption = Resources.ControlText.gvRelSalaryHours
                };
                ch2.RowLayoutColumnInfo.OriginY = 0;
                ch2.RowLayoutColumnInfo.OriginX = 11;
                ch2.RowLayoutColumnInfo.SpanX = 3;
                ch2.Style.HorizontalAlign = HorizontalAlign.Center;
                e.Layout.Bands[0].HeaderLayout.Add(ch2);
                ColumnHeader ch3 = new ColumnHeader(true)
                {
                    Caption = Resources.ControlText.gvSpecApplyHours
                };
                ch3.RowLayoutColumnInfo.OriginY = 0;
                ch3.RowLayoutColumnInfo.OriginX = 14;
                ch3.RowLayoutColumnInfo.SpanX = 3;
                ch3.Style.HorizontalAlign = HorizontalAlign.Center;
                e.Layout.Bands[0].HeaderLayout.Add(ch3);
                ColumnHeader ch4 = new ColumnHeader(true)
                {
                    Caption = Resources.ControlText.gvSpecrelSalary
                };
                ch4.RowLayoutColumnInfo.OriginY = 0;
                ch4.RowLayoutColumnInfo.OriginX = 17;
                ch4.RowLayoutColumnInfo.SpanX = 3;
                ch4.Style.HorizontalAlign = HorizontalAlign.Center;
                e.Layout.Bands[0].HeaderLayout.Add(ch4);
                for (i = 1; i < 0x20; i++)
                {
                    ColumnHeader chr = new ColumnHeader(true)
                    {
                        Caption = i.ToString()
                    };
                    chr.RowLayoutColumnInfo.OriginY = 0;
                    chr.RowLayoutColumnInfo.OriginX = i + 0x19;
                    chr.RowLayoutColumnInfo.SpanX = 1;
                    chr.Style.HorizontalAlign = HorizontalAlign.Center;
                    e.Layout.Bands[0].HeaderLayout.Add(chr);
                }
                ch = e.Layout.Bands[0].Columns.FromKey("CheckBoxAll").Header;
                ch.RowLayoutColumnInfo.SpanY = 2;
                ch.RowLayoutColumnInfo.OriginY = 0;
                ch = e.Layout.Bands[0].Columns.FromKey("DISSIGNRMARK").Header;
                ch.RowLayoutColumnInfo.SpanY = 2;
                ch.RowLayoutColumnInfo.OriginY = 0;
                ch = e.Layout.Bands[0].Columns.FromKey("WorkNo").Header;
                ch.RowLayoutColumnInfo.SpanY = 2;
                ch.RowLayoutColumnInfo.OriginY = 0;
                ch = e.Layout.Bands[0].Columns.FromKey("LocalName").Header;
                ch.RowLayoutColumnInfo.SpanY = 2;
                ch.RowLayoutColumnInfo.OriginY = 0;
                ch = e.Layout.Bands[0].Columns.FromKey("BuName").Header;
                ch.RowLayoutColumnInfo.SpanY = 2;
                ch.RowLayoutColumnInfo.OriginY = 0;
                ch = e.Layout.Bands[0].Columns.FromKey("DName").Header;
                ch.RowLayoutColumnInfo.SpanY = 2;
                ch.RowLayoutColumnInfo.OriginY = 0;
                ch = e.Layout.Bands[0].Columns.FromKey("OverTimeType").Header;
                ch.RowLayoutColumnInfo.SpanY = 2;
                ch.RowLayoutColumnInfo.OriginY = 0;
                ch = e.Layout.Bands[0].Columns.FromKey("G2Remain").Header;
                ch.RowLayoutColumnInfo.SpanY = 2;
                ch.RowLayoutColumnInfo.OriginY = 0;
                ch = e.Layout.Bands[0].Columns.FromKey("MAdjust1").Header;
                ch.RowLayoutColumnInfo.SpanY = 2;
                ch.RowLayoutColumnInfo.OriginY = 0;
                ch = e.Layout.Bands[0].Columns.FromKey("MRelAdjust").Header;
                ch.RowLayoutColumnInfo.SpanY = 2;
                ch.RowLayoutColumnInfo.OriginY = 0;
                ch = e.Layout.Bands[0].Columns.FromKey("AdvanceAdjust").Header;
                ch.RowLayoutColumnInfo.SpanY = 2;
                ch.RowLayoutColumnInfo.OriginY = 0;
                ch = e.Layout.Bands[0].Columns.FromKey("RestAdjust").Header;
                ch.RowLayoutColumnInfo.SpanY = 2;
                ch.RowLayoutColumnInfo.OriginY = 0;
            }
            else
            {
                HiddenYearMonth.Value = dt_monthtotal.Rows[0]["YearMonth"].ToString().Replace("/", "");
                //this.HiddenYearMonth.Value = this.txtYearMonth.Text.Replace("/", "");
            }
            YearMonth = Convert.ToDateTime(HiddenYearMonth.Value);
            string FromKeyName = "";
            for (i = 1; i < 0x20; i++)
            {
                FromKeyName = "Day" + i.ToString();
                e.Layout.Bands[0].Columns.FromKey(FromKeyName).Header.Caption = this.GetWeek(YearMonth.AddDays((double)(i - 1)));
                if (this.isHoliday(i, YearMonth))
                {
                    e.Layout.Rows.Band.Columns[e.Layout.Bands[0].Columns.FromKey(FromKeyName).Index].CellStyle.BackColor = Color.DarkKhaki;
                }
                else
                {
                    e.Layout.Rows.Band.Columns[e.Layout.Bands[0].Columns.FromKey(FromKeyName).Index].CellStyle.BackColor = Color.White;
                }
            }
        }
        #endregion
        #region  判斷是否節假日
        public bool isHoliday(int day, DateTime date)
        {
            bool bValue = false;
            switch (Convert.ToInt32(date.AddDays((double)(day - 1)).DayOfWeek))
            {
                case 0:
                case 6:
                    bValue = true;
                    break;
            }
            return bValue;
        }
        #endregion
        #region 判斷星期幾
        private string GetWeek(DateTime SelectYearMonth)
        {
            string getWeek = SelectYearMonth.DayOfWeek.ToString();
            switch (getWeek)
            {
                case "Sunday":
                    return "日";

                case "Monday":
                    return "一";

                case "Tuesday":
                    return "二";

                case "Wednesday":
                    return "三";

                case "Thursday":
                    return "四";

                case "Friday":
                    return "五";

                case "Saturday":
                    return "六";
            }
            return "";
        }
        #endregion
        #region WebGrid綁定
        protected void UltraWebGridKQM_DataBound(object sender, EventArgs e)
        {
            decimal Normal = 0M;
            decimal Spec = 0M;
            for (int i = 0; i < this.UltraWebGridKQM.Rows.Count; i++)
            {
                int k;
                if (HiddenBillNo.Value.Substring(3, 1) == "S")
                {
                    k = 1;
                    while (k < 0x20)
                    {
                        Spec = Convert.ToDecimal((Convert.ToString(this.UltraWebGridKQM.Rows[i].Cells.FromKey("SpecDay" + k.ToString()).Text) == "") ? "0" : Convert.ToString(this.UltraWebGridKQM.Rows[i].Cells.FromKey("SpecDay" + k.ToString()).Text));
                        this.UltraWebGridKQM.Rows[i].Cells.FromKey("Day" + k.ToString()).Text = Convert.ToString((decimal)(Spec));
                        k++;
                    }
                }
                if (HiddenBillNo.Value.Substring(3, 1) == "A")
                {
                    for (k = 1; k < 0x20; k++)
                    {
                        Normal = Convert.ToDecimal((Convert.ToString(this.UltraWebGridKQM.Rows[i].Cells.FromKey("Day" + k.ToString()).Text) == "") ? "0" : Convert.ToString(this.UltraWebGridKQM.Rows[i].Cells.FromKey("Day" + k.ToString()).Text));
                        Spec = Convert.ToDecimal((Convert.ToString(this.UltraWebGridKQM.Rows[i].Cells.FromKey("SpecDay" + k.ToString()).Text) == "") ? "0" : Convert.ToString(this.UltraWebGridKQM.Rows[i].Cells.FromKey("SpecDay" + k.ToString()).Text));
                        this.UltraWebGridKQM.Rows[i].Cells.FromKey("Day" + k.ToString()).Text = Convert.ToString((decimal)(Normal + Spec));
                    }
                }
                else
                {
                    for (k = 1; k < 0x20; k++)
                    {
                        Normal = Convert.ToDecimal((Convert.ToString(this.UltraWebGridKQM.Rows[i].Cells.FromKey("Day" + k.ToString()).Text) == "") ? "0" : Convert.ToString(this.UltraWebGridKQM.Rows[i].Cells.FromKey("Day" + k.ToString()).Text));
                        this.UltraWebGridKQM.Rows[i].Cells.FromKey("Day" + k.ToString()).Text = Convert.ToString((decimal)(Normal));
                    }
                }
            }
        }
        #endregion
        #endregion


        protected void UltraWebGridLeaveApply_DataBound(object sender, EventArgs e)
        {
            string sStatus = "";
            string sID = "";
            string ThisLVTotal = "";
            string TestifyFile = "";
            string UploadFile = "";
            string LVWorkDays = "";
            for (int i = 0; i < UltraWebGridLeaveApply.Rows.Count; i++)
            {
                UltraWebGridLeaveApply.Rows[i].Cells[30].Value = "<a  href=javascript:openProgress('" + UltraWebGridLeaveApply.Rows[i].Cells[30].Value + "')>" + Message.WatchProgress + "</a>";
                sStatus = UltraWebGridLeaveApply.Rows[i].Cells.FromKey("Status").Text.Trim();
                switch (sStatus)
                {
                    case "0":
                        UltraWebGridLeaveApply.Rows[i].Cells.FromKey("StatusName").Style.ForeColor = Color.Green;
                        break;
                    case "1":
                        UltraWebGridLeaveApply.Rows[i].Cells.FromKey("StatusName").Style.ForeColor = Color.Blue;
                        break;
                    case "3":
                        UltraWebGridLeaveApply.Rows[i].Cells.FromKey("StatusName").Style.ForeColor = Color.Maroon;
                        break;
                    case "4":
                        UltraWebGridLeaveApply.Rows[i].Cells.FromKey("StatusName").Style.ForeColor = Color.Red;
                        break;
                    default:
                        UltraWebGridLeaveApply.Rows[i].Cells.FromKey("StatusName").Style.ForeColor = Color.Black;
                        break;
                }
           

                if (this.UltraWebGridLeaveApply.Rows[i].Cells.FromKey("TestifyFile").Value != null)
                {
                    TestifyFile = UltraWebGridLeaveApply.Rows[i].Cells.FromKey("TestifyFile").Value.ToString();
                    if (!TestifyFile.Equals("Y"))
                    {
                        UltraWebGridLeaveApply.Rows[i].Cells.FromKey("TestifyFile").Value = "<a  href=javascript:down('" + TestifyFile + "') >" + TestifyFile + "</a>";
                    }
                }
                if (this.UltraWebGridLeaveApply.Rows[i].Cells.FromKey("UploadFile").Value != null)
                {
                    UploadFile = UltraWebGridLeaveApply.Rows[i].Cells.FromKey("UploadFile").Value.ToString();
                    UltraWebGridLeaveApply.Rows[i].Cells.FromKey("UploadFile").Value = "<a  href=javascript:down('" + UploadFile + "') >" + UploadFile + "</a>";
                }
            }
        }
        public void UltraWebGrid(UltraWebGrid tmpUltraWebGrid, string condition, string[] captionNames, string[] tableColumns)
        {
            //tmpUltraWebGrid.Clear();     //清空 
            tmpUltraWebGrid.Bands[0].Columns.Clear();   //清空數據列
            UltraGridBand band = new UltraGridBand();
            band = tmpUltraWebGrid.Bands[0];

            //Head行
            UltraGridColumn tmpHeadColumn = new UltraGridColumn();
            UltraGridCell tmpCell = new UltraGridCell();


            band.Columns.Add(tmpHeadColumn);


            //數據行
            UltraGridColumn tmpColumn = new UltraGridColumn();

            // tmpColumn.Band.

            band.Columns.Add(tmpColumn);    //動態增加列






        }



    }
}
