using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
using System.Configuration;
using GDSBG.MiABU.Attendance.BLL.WorkFlow;
using GDSBG.MiABU.Attendance.BLL.KQM.Query.OTM;
using Infragistics.WebUI.UltraWebGrid;
using System.Text;
using Infragistics.WebUI.UltraWebGrid.ExcelExport;
using Resources;
using System.Web.Script.Serialization;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;

namespace GDSBG.MiABU.Attendance.Web.WorkFlowForm
{


    public partial class PCMAdvanceApplyForm : BasePage
    {
        static DataTable ddlTable = new DataTable();
        Dictionary<string, string> ClientMessage = null;
        JavaScriptSerializer JsSerializer = new JavaScriptSerializer();
         protected DataSet tempDataSet;
         protected DataTable tempDataTable;
         protected DataSet dataSet;
         OTMDetailQryBll detailQryBll = new OTMDetailQryBll();
         OverTimeBll bll = new OverTimeBll();
         static SynclogModel logmodel = new SynclogModel();

        protected void Page_Load(object sender, EventArgs e)
        {

            try 
            {
                if (ClientMessage == null)
                {
                    ClientMessage = new Dictionary<string, string>();
                    ClientMessage.Add("checkapproveflag", Message.checkapproveflag);

                    ClientMessage.Add("choess_no", Message.choess_no);
                    ClientMessage.Add("DeleteApplyovertimeEnd", Message.DeleteApplyovertimeEnd);
                    ClientMessage.Add("common_message_data_select", Message.common_message_data_select);

                    ClientMessage.Add("ConfirmReturn", Message.ConfirmReturn);
                    ClientMessage.Add("common_sunday", Message.common_sunday);
                    ClientMessage.Add("common_monday", Message.common_monday);
                    ClientMessage.Add("common_tuesday", Message.common_tuesday);
                    ClientMessage.Add("common_wednesday", Message.common_wednesday);
                    ClientMessage.Add("common_thursday", Message.common_thursday);
                    ClientMessage.Add("common_friday", Message.common_friday);
                    ClientMessage.Add("common_saturday", Message.common_saturday);

                    string clientmsg = JsSerializer.Serialize(ClientMessage);
                    Page.ClientScript.RegisterStartupScript(GetType(), "ClientMessage", "var Message=" + clientmsg + ";", true);
                }
                //((ImageButton)this.PageNavigator.FindControl("ImageButtonPrevious")).Click += new ImageClickEventHandler(this.ButtonPrevious_Click);
                //((ImageButton)this.PageNavigator.FindControl("ImageButtonNext")).Click += new ImageClickEventHandler(this.ButtonNext_Click);
                //((ImageButton)this.PageNavigator.FindControl("ImageButtonGoto")).Click += new ImageClickEventHandler(this.ButtonGoto_Click);
                if (!base.IsPostBack)
                {

                    logmodel.ProcessOwner = CurrentUserInfo.Personcode;
                    logmodel.TransactionType = (Request.QueryString["ModuleCode"] == null) ? "" : Request.QueryString["ModuleCode"].ToString();
                    logmodel.LevelNo = "2";
                    logmodel.FromHost = Request.UserHostAddress;
                    //this.Internationalization();
                    this.ddlDataBind();
                    //this.textBoxOTDateFrom.Attributes.Add("onfocus", "calendar('" + base.Language + "','" + base.dateFormat + "');");
                    //this.textBoxOTDateFrom.Attributes.Add("formater", base.dateFormat);
                    this.textBoxOTDateFrom.Text = DateTime.Now.AddDays((double)(-DateTime.Now.Day + 1)).ToString("yyyy/MM/dd");
                    //this.textBoxOTDateTo.Attributes.Add("onfocus", "calendar('" + base.Language + "','" + base.dateFormat + "');");
                    //this.textBoxOTDateTo.Attributes.Add("formater", base.dateFormat);
                    this.ModuleCode.Value = Request.QueryString["ModuleCode"];
                    this.Query(true, "Goto");
                }
                SetCalendar(textBoxOTDateFrom, textBoxOTDateTo);
            }
            catch (Exception ex)
            {
                this.WriteMessage(2, (ex.InnerException == null) ? ex.Message : ex.InnerException.Message);
            }
            PageHelper.ButtonControlsWF(FuncList, pnlShowPanel.Controls, base.FuncListModule);
        }

        #region 分頁
        protected void pager_PageChanged(object sender, EventArgs e)
        {
            this.Query(true, "Goto");
        }
        #endregion

        protected void ButtonQuery_Click(object sender, EventArgs e)
        {
            try
            {
                this.Query(true, "Goto");
                this.ProcessFlag.Value = "";
            }
            catch (Exception ex)
            {
                this.WriteMessage(2, (ex.InnerException == null) ? ex.Message : ex.InnerException.Message);
            }
        }


        protected void ButtonDelete_Click(object sender, EventArgs e)
        {
            try
            {
                int i;
                CellItem GridItem;
                CheckBox chkIsHaveRight;
                int intDeleteOk = 0;
                int intDeleteError = 0;
                TemplatedColumn tcol = (TemplatedColumn)this.UltraWebGrid.Bands[0].Columns[0];
                for (i = 0; i < this.UltraWebGrid.Rows.Count; i++)
                {
                    GridItem = (CellItem)tcol.CellItems[i];
                    chkIsHaveRight = (CheckBox)GridItem.FindControl("CheckBoxCell");
                    if (chkIsHaveRight.Checked && ((this.UltraWebGrid.Rows[i].Cells.FromKey("Status").Text.Trim() != "0") && (this.UltraWebGrid.Rows[i].Cells.FromKey("Status").Text.Trim() != "3")))
                    {
                        this.WriteMessage(1, Message.bfw_kqm_PCMAdvanceApplyForm_noDelect);
                        return;
                    }
                }
                for (i = 0; i < this.UltraWebGrid.Rows.Count; i++)
                {
                    GridItem = (CellItem)tcol.CellItems[i];
                    chkIsHaveRight = (CheckBox)GridItem.FindControl("CheckBoxCell");
                    if (chkIsHaveRight.Checked)
                    {
                        this.tempDataTable = bll.GetDataByCondition_1("and a.ID='" + this.UltraWebGrid.DisplayLayout.Rows[i].Cells.FromKey("id").Value + "' and INSTR(a.ImportFlag,'N')>0 and (a.status='0' or a.status='3')").Tables["OTM_AdvanceApply"];
                        if (this.tempDataTable.Rows.Count > 0)
                        {
                            bll.DeleteData(this.tempDataTable,logmodel);
                            intDeleteOk++;
                        }
                        else
                        {
                            intDeleteError++;
                        }
                    }
                }
                if ((intDeleteOk + intDeleteError) > 0)
                {
                    this.WriteMessage(0, string.Concat(new object[] { Message.SuccCount, "：", intDeleteOk, ";", Message.FaileCount, "：", intDeleteError }));
                    this.Query(false, "Goto");
                }
                else
                {
                    this.WriteMessage(1, Message.common_message_data_select);
                    return;
                }
                this.ProcessFlag.Value = "";
            }
            catch (Exception ex)
            {
                this.WriteMessage(2, (ex.InnerException == null) ? ex.Message : ex.InnerException.Message);
            }
        }

        protected void ButtonExport_Click(object sender, EventArgs e)
        {
            if (this.ViewState["condition"] == null)
            {
                this.WriteMessage(1, Message.common_message_nodataexport);
            }
            else
            {
                try
                {
                    this.dataSet = bll.GetDataByCondition_1(this.ViewState["condition"].ToString());
                    string WorkNo = "";
                    string OTDate = "";
                    foreach (DataRow dr in this.dataSet.Tables["OTM_AdvanceApply"].Rows)
                    {
                        WorkNo = Convert.ToString(dr["WorkNo"]);
                        OTDate = Convert.ToDateTime(dr["OTDate"]).ToString("yyyy/MM/dd");
                        this.tempDataTable = bll.GetDataSetBySQL("select nvl(sum(Decode(OTType,'G1',ConfirmHours,0)),0)G1Total,nvl(sum(Decode(OTType,'G2',ConfirmHours,0)),0)G2Total,nvl(sum(Decode(OTType,'G3',ConfirmHours,0)),0)G3Total from GDS_ATT_REALAPPLY where WorkNo='" + WorkNo + "' and otdate>last_day(add_months(to_date('" + OTDate + "','yyyy/MM/dd'),-1)) and OTDate<=to_date('" + OTDate + "','yyyy/MM/dd') and status<'3'").Tables["TempTable"];
                        if (this.tempDataTable.Rows.Count > 0)
                        {
                            dr["G1Total"] = Convert.ToDouble(dr["G1Total"]) + Convert.ToDouble(this.tempDataTable.Rows[0]["G1Total"]);
                            dr["G2Total"] = Convert.ToDouble(dr["G2Total"]) + Convert.ToDouble(this.tempDataTable.Rows[0]["G2Total"]);
                            dr["G3Total"] = Convert.ToDouble(dr["G3Total"]) + Convert.ToDouble(this.tempDataTable.Rows[0]["G3Total"]);
                        }
                    }
                    this.tempDataTable = this.dataSet.Tables["OTM_AdvanceApply"];
                    HttpResponse res = this.Page.Response;
                    res.Clear();
                    res.Buffer = true;
                    res.AppendHeader("Content-Disposition", "attachment;filename=PCMAdvanceApply.xls");
                    res.Charset = "UTF-8";
                    res.ContentEncoding = Encoding.Default;
                    res.ContentType = "application/ms-excel";
                    this.EnableViewState = false;
                    string colHeaders = "";
                    string ls_item = "";
                    for (int iLoop = 1; iLoop < this.UltraWebGrid.Columns.Count; iLoop++)
                    {
                        if (!this.UltraWebGrid.Columns[iLoop].Hidden && this.UltraWebGrid.Columns[iLoop].Key != "")
                        {
                            colHeaders = colHeaders + this.UltraWebGrid.Columns[iLoop].Header.Caption + "\t";
                        }
                    }
                    res.Write(colHeaders);
                    for (int i = 0; i < this.tempDataTable.Rows.Count; i++)
                    {
                        ls_item = "\n";
                        for (int iLop = 1; iLop < this.UltraWebGrid.Columns.Count; iLop++)
                        {
                            if (!this.UltraWebGrid.Columns[iLop].Hidden && this.UltraWebGrid.Columns[iLop].Key != "")
                            {
                                if (this.UltraWebGrid.Columns[iLop].Key.ToLower() == "otdate")
                                {
                                    try
                                    {
                                        ls_item = ls_item + string.Format("{0:" + "yyyy/MM/dd" + "}", Convert.ToDateTime(this.tempDataTable.Rows[i][this.UltraWebGrid.Columns[iLop].Key])) + "\t";
                                    }
                                    catch
                                    {
                                        ls_item = ls_item + "\t";
                                    }
                                }
                                else if ((this.UltraWebGrid.Columns[iLop].Key.ToLower() == "begintime") || (this.UltraWebGrid.Columns[iLop].Key.ToLower() == "endtime"))
                                {
                                    try
                                    {
                                        ls_item = ls_item + string.Format("{0:HH:mm}", Convert.ToDateTime(this.tempDataTable.Rows[i][this.UltraWebGrid.Columns[iLop].Key])) + "\t";
                                    }
                                    catch
                                    {
                                        ls_item = ls_item + "\t";
                                    }
                                }
                                else if ((this.UltraWebGrid.Columns[iLop].Key.ToLower() == "approvedate") || (this.UltraWebGrid.Columns[iLop].Key.ToLower() == "modifydate"))
                                {
                                    try
                                    {
                                        ls_item = ls_item + string.Format("{0:" + Convert.ToString("yyyy/MM/dd") + "}", Convert.ToDateTime(this.tempDataTable.Rows[i][this.UltraWebGrid.Columns[iLop].Key])) + "\t";
                                    }
                                    catch
                                    {
                                        ls_item = ls_item + "\t";
                                    }
                                }
                                else
                                {
                                    ls_item = ls_item + this.tempDataTable.Rows[i][this.UltraWebGrid.Columns[iLop].Key].ToString().Replace("\n", "").Replace("\r", "") + "\t";
                                }
                            }
                        }
                        res.Write(ls_item);
                        ls_item = "";
                    }
                    res.End();
                }
                catch (Exception ex)
                {
                    this.WriteMessage(2, (ex.InnerException == null) ? ex.Message : ex.InnerException.Message);
                }
            }
        }

        /// <summary>
        /// 下拉列表綁定數據
        /// </summary>
        protected void ddlDataBind()
        {
            ddlTable = new DataTable();
            ddlTable = detailQryBll.GetDataByCondition("condition1");
            this.ddlOTStatus.DataSource = ddlTable.DefaultView;
            this.ddlOTStatus.DataTextField = "DataValue";
            this.ddlOTStatus.DataValueField = "DataCode";
            this.ddlOTStatus.DataBind();
            this.ddlOTStatus.Items.Insert(0, new ListItem("", ""));
            this.ddlOTStatus.SelectedValue = "";
            ddlTable.Clear();
            ddlTable = detailQryBll.GetDataByCondition("condition2");
            this.ddlPersonType.DataSource = ddlTable.DefaultView;
            this.ddlPersonType.DataTextField = "DataValue";
            this.ddlPersonType.DataValueField = "DataCode";
            this.ddlPersonType.DataBind();
            this.ddlPersonType.Items.Insert(0, new ListItem("", ""));

        }

       

        protected void ButtonReset_Click(object sender, EventArgs e)
        {
            this.ddlPersonType.SelectedValue = "";
            this.ddlOTType.SelectedValue = "";
            this.ddlOTStatus.SelectedValue = "";
            this.textBoxOTDateFrom.Text = DateTime.Now.AddDays((double)(-DateTime.Now.Day + 1)).ToString("yyyy/MM/dd");
            this.textBoxOTDateTo.Text = "";
        }


        private void Query(bool WindowOpen, string forwarderType)
        {
            string condition = "";
            if (this.ProcessFlag.Value.ToLower().Equals("condition") || WindowOpen)
            {
                condition = condition + " AND a.WorkNO = '" + CurrentUserInfo.Personcode + "'";
                if (this.ddlPersonType.SelectedValue.Length != 0)
                {
                    condition = condition + " AND b.OverTimeType = '" + this.ddlPersonType.SelectedValue + "'";
                }
                if (this.ddlOTType.SelectedValue.Length != 0)
                {
                    condition = condition + " AND a.OTType = '" + this.ddlOTType.SelectedValue + "'";
                }
                if (this.ddlOTStatus.SelectedValue.Length != 0)
                {
                    condition = condition + " AND a.Status = '" + this.ddlOTStatus.SelectedValue + "'";
                }
                if (this.textBoxOTDateFrom.Text.Trim().Length > 0)
                {
                    condition = condition + " AND a.OTDate >= to_date('" + DateTime.Parse(this.textBoxOTDateFrom.Text.Trim()).ToString("yyyy/MM/dd") + "','yyyy/mm/dd') ";
                }
                if (this.textBoxOTDateTo.Text.Trim().Length > 0)
                {
                    condition = condition + " AND a.OTDate <= to_date('" + DateTime.Parse(this.textBoxOTDateTo.Text.Trim()).ToString("yyyy/MM/dd") + "','yyyy/mm/dd') ";
                }
                this.ViewState.Add("condition", condition);
            }
            else
            {
                condition = Convert.ToString(this.ViewState["condition"]);
            }
            int totalCount = 0;
            this.dataSet = bll.GetDataByCondition_pager(condition, pager.CurrentPageIndex, pager.PageSize, out  totalCount);
            //base.SetForwardPage(forwarderType, ((WebNumericEdit)this.PageNavigator.FindControl("WebNumericEditCurrentpage")).Value.ToString());
            //this.dataSet = ((ServiceLocator)this.Session["serviceLocator"]).GetOTMAdvanceApplyData().GetDataByCondition(condition, Convert.ToInt32(this.Session["PageSize"]), ref this.forwarderPage, ref this.totalPage, ref this.totalRecodrs);
            //this.SetPageInfor(base.forwarderPage, base.totalPage, base.totalRecodrs);
            pager.RecordCount = totalCount;
            pager.TextAfterPageIndexBox = "/" + pager.PageCount.ToString();

            this.DataUIBind();
            this.WriteMessage(0, Message.common_message_trans_complete);
        }

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

        private void DataUIBind()
        {
            this.UltraWebGrid.DataSource = this.dataSet.Tables["OTM_AdvanceApply"].DefaultView;
            this.UltraWebGrid.DataBind();
        }


        protected void UltraWebGrid_DataBound(object sender, EventArgs e)
        {
            string OTDate = "";
            double AdvanceTotalG1 = 0.0;
            double AdvanceTotalG2 = 0.0;
            double AdvanceTotalG3 = 0.0;
            double RealTotalG1 = 0.0;
            double RealTotalG2 = 0.0;
            double RealTotalG3 = 0.0;
            string LHZBIsDisplayG2G3 = "";
            if (bll.GetValue("select nvl(max(paravalue),'N') from GDS_SC_PARAMETER where paraname='IsShowMoveLeaveFlag'").Equals("Y"))
            {
                this.UltraWebGrid.Bands[0].Columns.FromKey("G2IsForRest").Hidden = false;
            }
            try
            {
                LHZBIsDisplayG2G3 = ConfigurationManager.AppSettings["LHZBIsDisplayG2G3"];
            }
            catch
            {
                LHZBIsDisplayG2G3 = "N";
            }
            for (int i = 0; i < this.UltraWebGrid.Rows.Count; i++)
            {
                if (LHZBIsDisplayG2G3 == "Y")
                {
                    if (this.UltraWebGrid.Rows[i].Cells.FromKey("OTType").Text.Trim() == "G2")
                    {
                        this.UltraWebGrid.Rows[i].Cells.FromKey("OTType").Value = string.Concat(new object[] { this.UltraWebGrid.Rows[i].Cells.FromKey("OTType").Value, "(", base.GetResouseValue("otm.g2.remark"), ")" });
                    }
                    else if (this.UltraWebGrid.Rows[i].Cells.FromKey("OTType").Text.Trim() == "G3")
                    {
                        this.UltraWebGrid.Rows[i].Cells.FromKey("OTType").Value = string.Concat(new object[] { this.UltraWebGrid.Rows[i].Cells.FromKey("OTType").Value, "(", base.GetResouseValue("otm.g3.remark"), ")" });
                    }
                }
                if (this.UltraWebGrid.Rows[i].Cells.FromKey("Status").Text.Trim() == "0")
                {
                    this.UltraWebGrid.Rows[i].Cells.FromKey("StatusName").Style.ForeColor = Color.Green;
                }
                if (this.UltraWebGrid.Rows[i].Cells.FromKey("Status").Text.Trim() == "1")
                {
                    this.UltraWebGrid.Rows[i].Cells.FromKey("StatusName").Style.ForeColor = Color.Blue;
                }
                if (this.UltraWebGrid.Rows[i].Cells.FromKey("Status").Text.Trim() == "2")
                {
                    this.UltraWebGrid.Rows[i].Cells.FromKey("StatusName").Style.ForeColor = Color.Black;
                }
                if (this.UltraWebGrid.Rows[i].Cells.FromKey("Status").Text.Trim() == "3")
                {
                    this.UltraWebGrid.Rows[i].Cells.FromKey("StatusName").Style.ForeColor = Color.Maroon;
                }
                if (this.UltraWebGrid.Rows[i].Cells.FromKey("IsProject").Text.Trim() == "Y")
                {
                    this.UltraWebGrid.Rows[i].Style.ForeColor = Color.Red;
                }
                if ((this.UltraWebGrid.Rows[i].Cells.FromKey("OTMSGFlag").Value != null) && (this.UltraWebGrid.Rows[i].Cells.FromKey("WorkDesc").Value != null))
                {
                    string CS40002 = this.UltraWebGrid.Rows[i].Cells.FromKey("OTMSGFlag").Text;
                    if (CS40002 != null)
                    {
                        if (!(CS40002 == "1"))
                        {
                            if (CS40002 == "2")
                            {
                                goto Label_05E6;
                            }
                            if (CS40002 == "3")
                            {
                                goto Label_060D;
                            }
                            if (CS40002 == "4")
                            {
                                goto Label_0634;
                            }
                            if (CS40002 == "5")
                            {
                                goto Label_065B;
                            }
                            if (CS40002 == "6")
                            {
                                goto Label_0682;
                            }
                        }
                        else
                        {
                            this.UltraWebGrid.Rows[i].Style.ForeColor = Color.Green;
                        }
                    }
                }
                goto Label_06AA;
            Label_05E6:
                this.UltraWebGrid.Rows[i].Style.ForeColor = Color.Blue;
                goto Label_06AA;
            Label_060D:
                this.UltraWebGrid.Rows[i].Style.ForeColor = Color.SaddleBrown;
                goto Label_06AA;
            Label_0634:
                this.UltraWebGrid.Rows[i].Style.ForeColor = Color.Maroon;
                goto Label_06AA;
            Label_065B:
                this.UltraWebGrid.Rows[i].Style.ForeColor = Color.Tomato;
                goto Label_06AA;
            Label_0682:
                this.UltraWebGrid.Rows[i].Style.ForeColor = Color.Red;
            Label_06AA:
                OTDate = Convert.ToDateTime(this.UltraWebGrid.Rows[i].Cells.FromKey("OTDate").Text).ToString("yyyy/MM/dd");
                AdvanceTotalG1 = 0.0;
                AdvanceTotalG2 = 0.0;
                AdvanceTotalG3 = 0.0;
                RealTotalG1 = 0.0;
                RealTotalG2 = 0.0;
                RealTotalG3 = 0.0;
                this.tempDataTable = bll.GetDataSetBySQL("select nvl(sum(Decode(OTType,'G1',Hours,0)),0)G1Total,nvl(sum(Decode(OTType,'G2',Hours,0)),0)G2Total,nvl(sum(Decode(OTType,'G3',Hours,0)),0)G3Total,nvl(sum(Decode(OTType,'G4',Hours,0)),0)G4Total from GDS_ATT_ADVANCEAPPLY where WorkNo='" + this.UltraWebGrid.Rows[i].Cells.FromKey("WorkNo").Text + "' and otdate>last_day(add_months(to_date('" + OTDate + "','yyyy/MM/dd'),-1)) and OTDate<=to_date('" + OTDate + "','yyyy/MM/dd') and status<'3' and importflag='N'").Tables["TempTable"];
                if (this.tempDataTable.Rows.Count > 0)
                {
                    AdvanceTotalG1 = Convert.ToDouble(this.tempDataTable.Rows[0]["G1Total"]);
                    AdvanceTotalG2 = Convert.ToDouble(this.tempDataTable.Rows[0]["G2Total"]);
                    AdvanceTotalG3 = Convert.ToDouble(this.tempDataTable.Rows[0]["G3Total"]) + Convert.ToDouble(this.tempDataTable.Rows[0]["G4Total"]);
                }
                this.tempDataTable = bll.GetDataSetBySQL("select nvl(sum(Decode(OTType,'G1',ConfirmHours,0)),0)G1Total,nvl(sum(Decode(OTType,'G2',ConfirmHours,0)),0)G2Total,nvl(sum(Decode(OTType,'G3',ConfirmHours,0)),0)G3Total,nvl(sum(Decode(OTType,'G4',ConfirmHours,0)),0)G4Total from GDS_ATT_REALAPPLY where WorkNo='" + this.UltraWebGrid.Rows[i].Cells.FromKey("WorkNo").Text + "' and otdate>last_day(add_months(to_date('" + OTDate + "','yyyy/MM/dd'),-1)) and OTDate<=to_date('" + OTDate + "','yyyy/MM/dd') and status='2'").Tables["TempTable"];
                if (this.tempDataTable.Rows.Count > 0)
                {
                    RealTotalG1 = Convert.ToDouble(this.tempDataTable.Rows[0]["G1Total"]);
                    RealTotalG2 = Convert.ToDouble(this.tempDataTable.Rows[0]["G2Total"]);
                    RealTotalG3 = Convert.ToDouble(this.tempDataTable.Rows[0]["G3Total"]) + Convert.ToDouble(this.tempDataTable.Rows[0]["G4Total"]);
                }
                this.UltraWebGrid.Rows[i].Cells.FromKey("G1Total").Value = AdvanceTotalG1 + RealTotalG1;
                this.UltraWebGrid.Rows[i].Cells.FromKey("G2Total").Value = AdvanceTotalG2 + RealTotalG2;
                this.UltraWebGrid.Rows[i].Cells.FromKey("G3Total").Value = AdvanceTotalG3 + RealTotalG3;
            }
        }

        protected void UltraWebGridExcelExporter_CellExported(object sender, CellExportedEventArgs e)
        {
            int iRdex = e.CurrentRowIndex;
            int iCdex = e.CurrentColumnIndex;
            if (iRdex != 0)
            {
                if ((e.GridColumn.Key.ToLower() == "otdate") && (e.CurrentWorksheet.Rows[iRdex].Cells[iCdex].Value != null))
                {
                    e.CurrentWorksheet.Rows[iRdex].Cells[iCdex].Value = string.Format("{0:" + "yyyy/MM/dd" + "}", Convert.ToDateTime(e.CurrentWorksheet.Rows[iRdex].Cells[iCdex].Value));
                }
                e.CurrentWorksheet.Rows[iRdex].Height = 350;
            }
        }

        protected void UltraWebGridExcelExporter_HeaderCellExported(object sender, HeaderCellExportedEventArgs e)
        {
            int iRdex = e.CurrentRowIndex;
            int iCdex = e.CurrentColumnIndex;
            if (iRdex == 0)
            {
                e.CurrentWorksheet.Columns[iCdex].Width = 0xbb8;
                e.CurrentWorksheet.Rows[iRdex].Height = 500;
            }
        }


    }
}
