/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： KaoQinQryForm
 * 檔功能描述： 考勤結果查詢UI
 * 
 * 版本：1.0
 * 創建標識： 顧陳偉 2011.12.27
 * 
 */
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using GDSBG.MiABU.Attendance.BLL.KQM.AttendanceData;
using GDSBG.MiABU.Attendance.BLL.KQM.BasicData;
using GDSBG.MiABU.Attendance.BLL.KQM.Query;
using GDSBG.MiABU.Attendance.Model.KQM.Query;
using Resources;

namespace GDSBG.MiABU.Attendance.Web.KQM.Query
{
    public partial class KaoQinQryForm : BasePage
    {
        KaoQinDataQueryModel model = new KaoQinDataQueryModel();
        BellCardDataModel bcmodel = new BellCardDataModel();
        KaoQinQryBll kaoQinQryBll = new KaoQinQryBll();
        TypeDataBll typeDataBll = new TypeDataBll();
        KQMReGetKaoQinBll bllKaoQin = new KQMReGetKaoQinBll();
        BellCardQueryBll bllBellData = new BellCardQueryBll();
        static DataTable dt_global = new DataTable();
        DataTable dt = new DataTable();
        JavaScriptSerializer JsSerializer = new JavaScriptSerializer();
        Dictionary<string, string> ClientMessage = null;

        #region 頁面加載
        protected void Page_Load(object sender, EventArgs e)
        {
            PageHelper.ButtonControls(FuncList, pnlShowPanel.Controls, base.FuncListModule);
            SetCalendar(txtKQDateFrom, txtKQDateTo);
            if (!base.IsPostBack)
            {
                
                this.txtKQDateFrom.Text = DateTime.Now.AddDays(-1.0).ToString("yyyy/MM/dd");
                this.txtKQDateTo.Text = DateTime.Now.ToString("yyyy/MM/dd");
                this.ModuleCode.Value = base.Request.QueryString["ModuleCode"].ToString();
                this.ddlExceptionTypeDataBind();
                this.ddlShiftNoDataBind();
                this.ddlStatusNameDataBind();
                this.ddlIsSupporter.Items.Insert(0, new ListItem("", ""));
                this.ddlIsSupporter.Items.Insert(1, new ListItem("N", "N"));
                this.ddlIsSupporter.Items.Insert(2, new ListItem("Y", "Y"));
                this.ddlStatusDataBind();
                this.chkFlag.Attributes.Add("onclick", "onChangeFlag(this)");
                this.chkFlagB.Attributes.Add("onclick", "onChangeFlag(this)");
                this.imgBatchWorkNo.Attributes.Add("OnClick", "javascript:ShowBatchWorkNo()");
            }
            if (ClientMessage == null)
            {
                ClientMessage = new Dictionary<string, string>();
                ClientMessage.Add("WrongDate", Message.WrongDate);
                ClientMessage.Add("KQDateNotNull", Message.KQDateNotNull);
                ClientMessage.Add("WorkNo", Message.WorkNo);
                ClientMessage.Add("LocalName", Message.LocalName);
                ClientMessage.Add("CardNo", Message.CardNo);
                ClientMessage.Add("CardTime", Message.CardTime);
                ClientMessage.Add("BellNo", Message.BellNo);
                ClientMessage.Add("ReadTime", Message.ReadTime);
                ClientMessage.Add("QueryWithinTwoMonths", Message.QueryWithinTwoMonths);
            }
            string clientmsg = JsSerializer.Serialize(ClientMessage);
            Page.ClientScript.RegisterStartupScript(GetType(), "ClientMessage", "var Message=" + clientmsg + ";", true);
        }
        #endregion

        #region 導出
        protected void btnExport_Click(object sender, EventArgs e)
        {
            bool flagA = false;
            bool flagB = false;
            this.HiddenHisFlag.Value = "N";
            string depname = this.txtDepName.Text.Trim();
            string IsMakeup = this.ddlIsMakeup.SelectedValue;
            string IsSupporter = this.ddlIsSupporter.SelectedValue;
            string BatchEmployeeNo = "";
            string EmployeeNo = this.txtEmployeeNo.Text.Trim();
            string str = txtBatchEmployeeNo.Text.Trim();
            if (str != "")
            {
                for (int i = 0; i < str.Split('\n').Length; i++)
                {
                    BatchEmployeeNo = BatchEmployeeNo + str.Split('\n')[i].Trim() + "§";
                }
            }
            string LocalName = this.txtLocalName.Text.Trim();
            string fromDate = this.txtKQDateFrom.Text.Trim();
            string toDate = this.txtKQDateTo.Text.Trim();
            string StatusName = this.ddlStatusName.SelectedValue;
            string ExceptionType = "";
            if (!string.IsNullOrEmpty(this.ddlExceptionType.SelectedValue.ToString()))
            {
                string[] temVal = ddlExceptionType.SelectedValuesToString(",").Split(',');
                for (int iLoop = 0; iLoop < temVal.Length; iLoop++)
                {
                    ExceptionType += "'" + temVal[iLoop] + "',";
                }
                ExceptionType = ExceptionType.Substring(0, ExceptionType.Length - 1);
            }
            string ShiftNo = this.ddlShiftNo.SelectedValue;
            string Status = "";
            if (!string.IsNullOrEmpty(this.ddlStatus.SelectedValue.ToString()))
            {
                string[] temVal = ddlStatus.SelectedValuesToString(",").Split(',');
                for (int iLoop = 0; iLoop < temVal.Length; iLoop++)
                {
                    Status += "'" + temVal[iLoop] + "',";
                }
                Status = Status.Substring(0, Status.Length - 1);
            }

            if (this.chkFlag.Checked)
            {
                flagA = true;
            }
            if (this.chkFlagB.Checked)
            {
                flagB = true;
            }
            string sql = base.SqlDep;
            DataTable newdt = kaoQinQryBll.GetKaoQinDataForExport(sql, depname, IsMakeup, IsSupporter, EmployeeNo, BatchEmployeeNo, LocalName, fromDate, toDate, StatusName, ExceptionType, ShiftNo, Status, flagA, flagB);
            if (newdt.Rows.Count == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "nodataexport", "alert('" + Message.NoDataExport + "');", true);
            }
            else
            {
                List<KaoQinDataQueryModel> list = kaoQinQryBll.GetList(newdt);
                string[] header = { ControlText.gvDepName, ControlText.gvWorkNo, ControlText.gvLocalName, ControlText.gvKQDate, ControlText.gvStatusName, ControlText.gvKQMShiftDesc, ControlText.gvIsMakeUp, ControlText.gvOnDutyTime, ControlText.gvOffDutyTime, ControlText.gvOTOnDutyTime, ControlText.gvOTOffDutyTime, ControlText.gvAbsentQty, ControlText.gvExceptionTypeName, ControlText.gvKQMReasonName, ControlText.gvReasonRemark, ControlText.gvAbsentTotal };
                string[] properties = { "DepName", "WorkNo", "LocalName", "KQDate", "StatusName", "ShiftDesc", "IsMakeUp", "OnDutyTime", "OffDutyTime", "OTOnDutyTime", "OTOffDutyTime", "AbsentQty", "ExceptionTypeName", "ReasonName", "ReasonRemark", "AbsentTotal" };
                string filePath = MapPath("~/ExportFileTemp/") + DateTime.Now.Ticks + ".xls";
                NPOIHelper.ExportExcel(list, header, properties, 5000, filePath);
                PageHelper.ReturnHTTPStream(filePath, true);
            }
        }
        #endregion

        #region 查詢
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            pager.CurrentPageIndex = 1;
            Query();
        }
        #endregion

        #region 分頁
        protected void pager_PageChanged(object sender, EventArgs e)
        {
            Query();
        }
        #endregion

        #region 重置
        protected void btnReset_Click(object sender, EventArgs e)
        {
            this.txtDepCode.Text = "";
            this.txtDepName.Text = "";
            this.txtEmployeeNo.Text = "";
            this.txtKQDateFrom.Text = "";
            this.txtKQDateTo.Text = "";
            this.txtLocalName.Text = "";
            this.ddlStatus.ClearSelection();
            this.ddlShiftNo.ClearSelection();
            this.ddlExceptionType.ClearSelection();
            this.ddlIsSupporter.ClearSelection();
        }
        #endregion

        #region GridView綁定
        private void DataUIBind()
        {
            this.UltraWebGridKaoQinQuery.DataSource = dt_global.DefaultView;
            this.UltraWebGridKaoQinQuery.DataBind();
            for (int i = 0; i < this.UltraWebGridKaoQinQuery.Rows.Count; i++)
            {
                if ((this.UltraWebGridKaoQinQuery.Rows[i].Cells.FromKey("Status").Value != null) && this.UltraWebGridKaoQinQuery.Rows[i].Cells.FromKey("Status").Text.Equals("1"))
                {
                    this.UltraWebGridKaoQinQuery.Rows[i].Cells.FromKey("StatusName").Style.ForeColor = Color.Red;
                }
            }
        }
        #endregion

        #region 數據處理
        protected void UltraWebGridKaoQinQuery_DataBound(object sender, EventArgs e)
        {
            string WorkNo = "";
            string KqDate = "";
            string AbsentTotal = "";
            for (int i = 0; i < this.UltraWebGridKaoQinQuery.Rows.Count; i++)
            {
                WorkNo = (this.UltraWebGridKaoQinQuery.Rows[i].Cells.FromKey("WorkNo").Value == null) ? "" : this.UltraWebGridKaoQinQuery.Rows[i].Cells.FromKey("WorkNo").Text;
                KqDate = (this.UltraWebGridKaoQinQuery.Rows[i].Cells.FromKey("KQDate").Value == null) ? "" : Convert.ToDateTime(this.UltraWebGridKaoQinQuery.Rows[i].Cells.FromKey("KQDate").Text).ToString("yyyy/MM/dd");
                dt.Clear();
                dt = kaoQinQryBll.GetAbsentTotal(WorkNo, KqDate);
                AbsentTotal = dt.Rows[0][0].ToString();
                DataRow row = dt_global.Rows[i];
                row.BeginEdit();
                row["AbsentTotal"] = AbsentTotal;
                row.EndEdit();
                this.UltraWebGridKaoQinQuery.Rows[i].Cells.FromKey("KQDate").TargetURL = "javascript:ShowDetail('" + this.UltraWebGridKaoQinQuery.Rows[i].Cells.FromKey("WorkNo").Text + "','" + Convert.ToDateTime(this.UltraWebGridKaoQinQuery.Rows[i].Cells.FromKey("KQDate").Text).ToString("yyyy/MM/dd") + "','" + this.UltraWebGridKaoQinQuery.Rows[i].Cells.FromKey("ShiftNo").Text + "')";
                this.UltraWebGridKaoQinQuery.Rows[i].Cells.FromKey("AbsentTotal").Value = AbsentTotal;
            }
        }
        #endregion

        #region 結果下拉菜單綁定
        private void ddlExceptionTypeDataBind()
        {
            dt.Clear();
            dt = typeDataBll.GetExceptionTypeList();
            this.ddlExceptionType.DataSource = dt.DefaultView;
            this.ddlExceptionType.DataTextField = "DataValue";
            this.ddlExceptionType.DataValueField = "DataCode";
            this.ddlExceptionType.DataBind();
        }
        #endregion

        #region 班別下拉菜單綁定
        private void ddlShiftNoDataBind()
        {
            dt.Clear();
            dt = typeDataBll.GetShiftNoList();
            this.ddlShiftNo.DataSource = dt.DefaultView;
            this.ddlShiftNo.DataTextField = "DataValue";
            this.ddlShiftNo.DataValueField = "DataCode";
            this.ddlShiftNo.DataBind();
            this.ddlShiftNo.Items.Insert(0, new ListItem("", ""));
        }
        #endregion

        #region 狀態下拉菜單綁定
        private void ddlStatusNameDataBind()
        {
            dt.Clear();
            dt = typeDataBll.GetKqmKaoQinStatusList();
            this.ddlStatusName.DataSource = dt.DefaultView;
            this.ddlStatusName.DataTextField = "DataValue";
            this.ddlStatusName.DataValueField = "DataCode";
            this.ddlStatusName.DataBind();
            this.ddlStatusName.Items.Insert(0, new ListItem("", ""));
        }
        #endregion

        #region 在職狀態下拉菜單綁定
        private void ddlStatusDataBind()
        {
            dt.Clear();
            dt = typeDataBll.GetStatusList();
            this.ddlStatus.DataSource = dt.DefaultView;
            this.ddlStatus.DataTextField = "DataValue";
            this.ddlStatus.DataValueField = "DataCode";
            this.ddlStatus.DataBind();
        }
        #endregion

        #region 查詢方法
        private void Query()
        {
            int totalCount = 0;
            bool flagA=false;
            bool flagB=false;
            this.HiddenHisFlag.Value = "N";
            string depname=this.txtDepName.Text.Trim();
            string IsMakeup=this.ddlIsMakeup.SelectedValue;
            string IsSupporter=this.ddlIsSupporter.SelectedValue;
            string BatchEmployeeNo = ""; 
            string EmployeeNo=this.txtEmployeeNo.Text.Trim();
            string str = txtBatchEmployeeNo.Text.Trim();
            if (str != "")
            {
                for (int i = 0; i < str.Split('\n').Length; i++)
                {
                    BatchEmployeeNo = BatchEmployeeNo + str.Split('\n')[i].Trim() + "§";
                }
            }
            string LocalName=this.txtLocalName.Text.Trim();    
            string fromDate = this.txtKQDateFrom.Text.Trim();
            string toDate = this.txtKQDateTo.Text.Trim();
            string StatusName=this.ddlStatusName.SelectedValue;
            string  ExceptionType = "";
            if (!string.IsNullOrEmpty(this.ddlExceptionType.SelectedValue.ToString()))
            {
                string[] temVal = ddlExceptionType.SelectedValuesToString(",").Split(',');
                for (int iLoop = 0; iLoop < temVal.Length; iLoop++)
                {
                    ExceptionType += "'" + temVal[iLoop] + "',";
                }
                ExceptionType = ExceptionType.Substring(0, ExceptionType.Length - 1);
            }
            string ShiftNo=this.ddlShiftNo.SelectedValue;
            string Status="";
            if(!string.IsNullOrEmpty(this.ddlStatus.SelectedValue.ToString()))
            {
                string[] temVal = ddlStatus.SelectedValuesToString(",").Split(',');
                for (int iLoop = 0; iLoop < temVal.Length; iLoop++)
                {
                    Status += "'" + temVal[iLoop] + "',";
                }
                Status = Status.Substring(0, Status.Length - 1);
            }

            if(this.chkFlag.Checked)
            {
                flagA=true;
            }
            if(this.chkFlagB.Checked)
            {
                flagB=true;
            }
            string sql = base.SqlDep;
            dt_global = kaoQinQryBll.GetKaoQinDataList(sql, depname, IsMakeup, IsSupporter, EmployeeNo, BatchEmployeeNo, LocalName, fromDate, toDate, StatusName, ExceptionType, ShiftNo, Status, flagA, flagB, pager.CurrentPageIndex, pager.PageSize, out totalCount);
            this.UltraWebGridKaoQinQuery.DataSource = dt_global;
            this.UltraWebGridKaoQinQuery.DataBind();
            pager.RecordCount = totalCount;
            pager.TextAfterPageIndexBox = "/" + pager.PageCount.ToString();
            for (int i = 0; i < this.UltraWebGridKaoQinQuery.Rows.Count; i++)
            {
                if ((this.UltraWebGridKaoQinQuery.Rows[i].Cells.FromKey("Status").Value != null) && this.UltraWebGridKaoQinQuery.Rows[i].Cells.FromKey("Status").Text.Equals("1"))
                {
                    this.UltraWebGridKaoQinQuery.Rows[i].Cells.FromKey("StatusName").Style.ForeColor = Color.Red;
                }
            }
        }
        #endregion

        #region ajax
        protected override void AjaxProcess()
        {
            int result = 0;
            string noticeJson = "";
            if (!string.IsNullOrEmpty(Request.Form["FromDate"]))
            {
                    string FromDate = Request.Form["FromDate"];
                    string ToDate = Request.Form["ToDate"];
                    DataTable flag = bllKaoQin.GetFromDate(FromDate, ToDate);
                    if (Convert.ToInt32(flag.Rows[0]["sDays"].ToString()) >= 3)
                    {
                        result = 1;
                    }
                Response.Clear();
                Response.Write(result.ToString());
                Response.End();
            }
            if (!string.IsNullOrEmpty(Request.Form["WorkNo"]))
            {
                bcmodel.WorkNo = Request.Form["WorkNo"].ToString();
                string KQDate = Request.Form["KQDate"].ToString();
                string ShiftNo = Request.Form["ShiftNo"].ToString();
                List<BellCardDataModel> list = bllBellData.GetBellDataReList(bcmodel, KQDate, ShiftNo);
                if (list != null)
                {
                    noticeJson = JsSerializer.Serialize(list);
                }
                Response.Clear();
                Response.Write(noticeJson);
                Response.End();
            }
        }
        #endregion

        #region His
        //[AjaxMethod]
        //public DataSet ShowDetail(string WorkNo, string KQDate, string ShiftNo, string HisFlag)
        //{
        //    //if ((ShiftNo.IndexOf("A") >= 0) || (ShiftNo.IndexOf("B") >= 0))
        //    //{
        //    //    if (HisFlag.Equals("Y"))
        //    //    {
        //    //        this.tempDataSet = ((ServiceLocator)this.Session["serviceLocator"]).GetKQMKaoQinDataData().GetBellCardHISData("and b.WorkNo='" + WorkNo + "' and a.cardtime>=to_date('" + KQDate + "','yyyy/mm/dd') and a.cardtime<to_date('" + Convert.ToDateTime(KQDate).AddDays(1.0).ToString("yyyy/MM/dd") + "','yyyy/mm/dd') ");
        //    //    }
        //    //    else
        //    //    {
        //    //        this.tempDataSet = ((ServiceLocator)this.Session["serviceLocator"]).GetKQMKaoQinDataData().GetBellCardData("and b.WorkNo='" + WorkNo + "' and a.cardtime>=to_date('" + KQDate + "','yyyy/mm/dd') and a.cardtime<to_date('" + Convert.ToDateTime(KQDate).AddDays(1.0).ToString("yyyy/MM/dd") + "','yyyy/mm/dd') ");
        //    //    }
        //    //}
        //    //else if (HisFlag.Equals("Y"))
        //    //{
        //    //    this.tempDataSet = ((ServiceLocator)this.Session["serviceLocator"]).GetKQMKaoQinDataData().GetBellCardHISData("and b.WorkNo='" + WorkNo + "' and a.cardtime>=to_date('" + KQDate + " 12:00:00','yyyy/mm/dd hh:mi:ss') and a.cardtime<=to_date('" + Convert.ToDateTime(KQDate).AddDays(1.0).ToString("yyyy/MM/dd") + " 12:00:00','yyyy/mm/dd hh:mi:ss') ");
        //    //}
        //    //else
        //    //{
        //    //    this.tempDataSet = ((ServiceLocator)this.Session["serviceLocator"]).GetKQMKaoQinDataData().GetBellCardData("and b.WorkNo='" + WorkNo + "' and a.cardtime>=to_date('" + KQDate + " 12:00:00','yyyy/mm/dd hh:mi:ss') and a.cardtime<=to_date('" + Convert.ToDateTime(KQDate).AddDays(1.0).ToString("yyyy/MM/dd") + " 12:00:00','yyyy/mm/dd hh:mi:ss') ");
        //    //}
        //    //return this.tempDataSet;
        //}

        //protected void UltraWebGridExcelExporter_CellExported(object sender, CellExportedEventArgs e)
        //{
        //    //int iRdex = e.CurrentRowIndex;
        //    //int iCdex = e.CurrentColumnIndex;
        //    //if (iRdex != 0)
        //    //{
        //    //    if ((e.GridColumn.Key.ToLower() == "kqdate") && (e.CurrentWorksheet.Rows[iRdex].Cells[iCdex].Value != null))
        //    //    {
        //    //        e.CurrentWorksheet.Rows[iRdex].Cells[iCdex].Value = string.Format("{0:" + base.dateFormat + "}", Convert.ToDateTime(e.CurrentWorksheet.Rows[iRdex].Cells[iCdex].Value));
        //    //    }
        //    //    e.CurrentWorksheet.Rows[iRdex].Height = 350;
        //    //}
        //    //else if (iRdex != 0)
        //    //{
        //    //    e.CurrentWorksheet.Rows[iRdex].Height = 350;
        //    //}
        //}
        #endregion

    }
}
