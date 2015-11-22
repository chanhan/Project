using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Serialization;
using Resources;
using System.Data;
using Infragistics.WebUI.UltraWebGrid;
using GDSBG.MiABU.Attendance.BLL.WorkFlow;
using Infragistics.WebUI.UltraWebNavigator;
using System.Data.OleDb;
using System.Text;
using GDSBG.MiABU.Attendance.BLL.KQM.KaoQinData;
using System.Collections;
using GDSBG.MiABU.Attendance.Model.WorkFlow;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;


namespace GDSBG.MiABU.Attendance.Web.WorkFlow
{
    public partial class WorkFlowCardMakeupList : BasePage
    {
        JavaScriptSerializer JsSerializer = new JavaScriptSerializer();
        DataTable tmpImportDataTable = new DataTable();
        DataRow row;
        DataTable tempDataTable = new DataTable();
        DataTable importDataTable = new DataTable();
        WorkFlowCardMakeupBll cardMakeupBll = new WorkFlowCardMakeupBll();
        WorkFlowSetBll workflowset = new WorkFlowSetBll();
        KQMMoveShiftBll moveShitBll = new KQMMoveShiftBll();
        Bll_AbnormalAttendanceHandle bll_abnormal = new Bll_AbnormalAttendanceHandle();
        static SynclogModel logmodel = new SynclogModel();
        string sFlow_LevelRemark = Message.flow_levelremark;
        
      //  OverTimeBll overTimeBill = new OverTimeBll();
        

        /// <summary>
        /// 頁面加載
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //2012/03/16
                logmodel.ProcessOwner = CurrentUserInfo.Personcode;
                logmodel.TransactionType = (Request.QueryString["ModuleCode"] == null) ? "" : Request.QueryString["ModuleCode"].ToString();
                logmodel.LevelNo = "2";
                logmodel.FromHost = Request.UserHostAddress;

                ddclDataBind("KQMMakeup", ddlReasonType);    //未刷補卡異常原因
                getddlStatus();     //狀態值 
                SetSelector(ImageDepCode, textBoxDepCode, textBoxDepName);    
                 
                this.textBoxKQDateFrom.Text = DateTime.Now.AddDays(-1).ToString("yyyy/MM/dd");
                this.textBoxKQDateTo.Text = DateTime.Now.ToString("yyyy/MM/dd");
            }
            //設置日歷控件到文本欄位

            SetCalendar(textBoxKQDateFrom, textBoxKQDateTo);
            //給按鈕加權限
            PageHelper.ButtonControlsWF(FuncList, pnlShowPanel.Controls,base.FuncListModule);
        }

        #region 相關數據初始化綁定


        /// <summary>
        /// 數據綁定
        /// <param name="flag">是否需要帶查詢條件0不需要，1 需要</param>
        /// </summary>
        protected void bindData(int flag)
        {
            int totalCount;
            //查詢條件
            string condition = "";
            string ddlStr = "";
            string[] temVal = null;
            string depName = "";
            tempDataTable.Clear();
            if (flag != 0)
            {
                //查詢條件
                condition = "";
                depName = textBoxDepName.Text.ToString().Trim(); ;
                if (this.textBoxEmployeeNo.Text.Trim().Length != 0)
                {
                    condition += " AND a.WorkNo = '" + this.textBoxEmployeeNo.Text.ToUpper().Trim() + "'";
                }
                if (this.textBoxName.Text.Trim().Length != 0)
                {
                    condition += " AND b.LocalName like '" + this.textBoxName.Text.Trim() + "%'";
                }
                if (this.ddlStatus.SelectedValue != "")
                {
                    condition += " AND a.STATUS ='" + this.ddlStatus.SelectedValue + "'";
                }
                ddlStr = "";
                if (this.ddlReasonType.SelectedValue != "")
                {
                    temVal = this.ddlReasonType.SelectedValuesToString(",").Split(new char[] { ',' });

                    System.Text.StringBuilder tmpStr = new System.Text.StringBuilder();

                    for (int iLoop = 0; iLoop < temVal.Length; iLoop++)
                    {
                        //判斷不為最后一行


                        if (iLoop != temVal.Length - 1)
                        {
                            tmpStr.Append("'" + temVal[iLoop] + "',");
                        }
                        else
                        {
                            tmpStr.Append("'" + temVal[iLoop] + "'");
                        }
                        //ddlStr = ddlStr + " a.ReasonType='" + temVal[iLoop] + "' or"; 

                    }
                    //ddlStr = ddlStr.Substring(0, ddlStr.Length - 2);
                    // condition = condition + " and (" + ddlStr + ")";
                    if (tmpStr.ToString() != "")

                        condition += " and a.MAKEUPTYPE in (" + tmpStr.ToString() + ")";

                        //condition += tmpStr.ToString();
                }
                if (!string.IsNullOrEmpty(depName))
                {
                   // condition += "AND a.depcode IN ((" + sqlDep + ") INTERSECT SELECT DepCode FROM gds_sc_department START WITH depname = '" + depName + "' CONNECT BY PRIOR depcode = parentdepcode) ";
                    condition += "AND b.depcode IN ( SELECT DepCode FROM gds_sc_department START WITH depname = '" + depName + "' CONNECT BY PRIOR depcode = parentdepcode) ";
                }
                //else
               // {
               //     condition += " AND a.depcode IN (" + sqlDep + ") ";
               // }

                ddlStr = "";
                if (this.textBoxBatchEmployeeNo.Text.Trim().Length != 0)
                {
                    string[] workNoList = this.textBoxBatchEmployeeNo.Text.Trim().Split(new char[] { Convert.ToChar('\r') });
                    for (int i = 0; i < workNoList.Length; i++)
                    {
                        if (workNoList[i].ToString().Length > 0)
                        {
                            ddlStr = ddlStr + "'" + workNoList[i].ToString().Trim().ToUpper().Replace("\n", "") + "',";
                        }
                    }
                    ddlStr = ddlStr.Substring(0, ddlStr.Length - 1);
                    condition = condition + " and a.WorkNo in (" + ddlStr + ")";
                }
                if ((this.textBoxEmployeeNo.Text.Trim().Length == 0) && (this.textBoxName.Text.Trim().Length == 0))
                {
                    if ((this.textBoxKQDateFrom.Text.Trim().Length == 0) || (this.textBoxKQDateTo.Text.Trim().Length == 0))
                    {
                        //  WriteMessage(1, base.GetResouseValue("common.message.datenotnull"));
                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "show", "alert('" +Message.common_message_datenotnull+ "')", true);

                        return;
                    }

                }
                if (this.textBoxKQDateFrom.Text.Trim().Length != 0)
                {
                    condition = condition + " AND a.KQDate >= to_date('" + DateTime.Parse(this.textBoxKQDateFrom.Text.Trim()).ToString("yyyy/MM/dd") + "','yyyy/mm/dd') ";
                }
                if (this.textBoxKQDateTo.Text.Trim().Length != 0)
                {
                    condition = condition + " AND a.KQDate <= to_date('" + DateTime.Parse(this.textBoxKQDateTo.Text.Trim()).ToString("yyyy/MM/dd") + "','yyyy/mm/dd') ";
                }
                if (this.textBoxBillNo.Text.Trim().Length != 0)
                {
                    condition = condition + " AND a.billno = '" + this.textBoxBillNo.Text.Trim() + "'";
                }
                if (this.ddlDecSalary.SelectedValue != "")
                {
                    condition = condition + " AND a.DecSalary ='" + this.ddlDecSalary.SelectedValue + "'";
                }
            }
            this.ViewState.Add("condition", condition); 

            //
            //數據綁定

            tempDataTable = cardMakeupBll.getCardMakeupList(condition, pager.CurrentPageIndex, pager.PageSize, out totalCount);
            this.UltraWebGridMakeup.DataSource = tempDataTable;
            this.UltraWebGridMakeup.DataBind();
            pager.RecordCount = totalCount;
            pager.TextAfterPageIndexBox = "/" + pager.PageCount.ToString();

        }

        /// <summary>
        /// 下拉框值綁定

        /// </summary>
        /// <param name="type">綁定的數據類型</param>
        /// <param name="ddcl">菜單名</param>
        private void ddclDataBind(string type, UNLV.IAP.WebControls.DropDownCheckList ddcl)
        {
            this.tempDataTable.Clear();
            this.tempDataTable = workflowset.GetDocNoTypeList(type);
            ddcl.DataSource = tempDataTable;
            ddcl.DataTextField = "DataValue";
            ddcl.DataValueField = "DataCode";
            ddcl.DataBind();
        }

        /// <summary>
        /// 綁定狀態值 
        /// </summary>
        private void getddlStatus()
        {
            this.tempDataTable.Clear();
            this.tempDataTable = workflowset.GetDocNoTypeList("OTMAdvanceApplyStatus");
            ddlStatus.DataSource = tempDataTable;
            ddlStatus.DataTextField = "DataValue";
            ddlStatus.DataValueField = "DataCode";
            ddlStatus.DataBind();
            this.ddlStatus.Items.Insert(0, new ListItem("", ""));   //增加默認空行
        }

        /// <summary>
        /// 分頁
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void pager_PageChanged(object sender, EventArgs e)
        {
            bindData(1);
        }

        /// <summary>
        /// 數據航綁定 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void UltraWebGridMakeup_DataBound(object sender, EventArgs e)
        {
            string WorkNo = "";
            string KqDate = "";
            for (int i = 0; i < this.UltraWebGridMakeup.Rows.Count; i++)
            {
                WorkNo = (this.UltraWebGridMakeup.Rows[i].Cells.FromKey("WorkNo").Value == null) ? "" : this.UltraWebGridMakeup.Rows[i].Cells.FromKey("WorkNo").Text;
                KqDate = (this.UltraWebGridMakeup.Rows[i].Cells.FromKey("KQDate").Value == null) ? "" : Convert.ToDateTime(this.UltraWebGridMakeup.Rows[i].Cells.FromKey("KQDate").Text).ToString("yyyy/MM/dd");
                //this.UltraWebGridMakeup.Rows[i].Cells.FromKey("DeTimes").Value = cardMakeupBll.GetValue("SELECT DeTimes FROM (SELECT count(1) DeTimes FROM gds_att_makeup WHERE workno = '" + WorkNo + "' and kqdate>last_day(add_months(to_date('" + KqDate + "','yyyy/MM/dd'),-1)) and kqdate<=last_day(to_date('" + KqDate + "','yyyy/MM/dd')) and Status<>'3')");
            }
        }


        #endregion

        #region 放大鏡綁定


        /// <summary>
        /// 設置Selector
        /// </summary>
        /// <param name="ctrlTrigger">控件ID--按鈕</param>
        /// <param name="ctrlCode">控件ID--文本框1</param>
        /// <param name="ctrlName">控件ID--文本框2</param>
        public void SetSelector(WebControl ctrlTrigger, Control ctrlCode, Control ctrlName)
        {
            if (ctrlCode is TextBox) { (ctrlCode as TextBox).Attributes.Add("readonly", "readonly"); }
            if (ctrlName is TextBox) { (ctrlName as TextBox).Attributes.Add("readonly", "readonly"); }
            ctrlTrigger.Attributes.Add("onclick", string.Format("return setSelector('{0}','{1}','{2}')",
                ctrlCode.ClientID, ctrlName.ClientID, Request.QueryString["modulecode"]));
        }
        #endregion

        #region 常用功能
        /// <summary>
        /// 查詢功能
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonQuery_Click(object sender, EventArgs e)
        {
            bindData(1);
        }

        /// <summary>
        /// 重置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonReset_Click(object sender, EventArgs e)
        { 
            this.textBoxDepName.Text = "";
            this.textBoxDepCode.Text = "";
            this.textBoxEmployeeNo.Text = "";
            this.textBoxName.Text = "";
            this.textBoxBillNo.Text = "";
            this.textBoxKQDateFrom.Text = "";
            this.textBoxKQDateTo.Text = "";
            
            this.textBoxKQDateFrom.Text = DateTime.Now.AddDays(-1).ToString("yyyy/MM/dd");
            this.textBoxKQDateTo.Text = DateTime.Now.ToString("yyyy/MM/dd");

            this.ddlReasonType.ClearSelection();
            this.ddlStatus.ClearSelection();
        }

        /// <summary>
        /// 刪除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonDelete_Click(object sender, EventArgs e)
        {
            try
            {
                int i;
                CellItem GridItem;
                CheckBox chkIsHaveRight;
                int intDeleteOk = 0;
                int intDeleteError = 0;
                TemplatedColumn tcol = (TemplatedColumn)this.UltraWebGridMakeup.Bands[0].Columns[0];
                for (i = 0; i < this.UltraWebGridMakeup.Rows.Count; i++)
                {
                    GridItem = (CellItem)tcol.CellItems[i];
                    chkIsHaveRight = (CheckBox)GridItem.FindControl("CheckBoxCell");
                    if (chkIsHaveRight.Checked && ((this.UltraWebGridMakeup.Rows[i].Cells.FromKey("Status").Text.Trim() != "0") && (this.UltraWebGridMakeup.Rows[i].Cells.FromKey("Status").Text.Trim() != "3")))
                    {

                        WriteMessage(1, Message.DeleteApplyovertimeEnd);
                       // this.Page.ClientScript.RegisterStartupScript(this.GetType(), "show", "alert('" + Message.DeleteApplyovertimeEnd + "')", true);

                        return;
                    }
                }
                for (i = 0; i < this.UltraWebGridMakeup.Rows.Count; i++)
                {
                    GridItem = (CellItem)tcol.CellItems[i];
                    chkIsHaveRight = (CheckBox)GridItem.FindControl("CheckBoxCell");
                    if (chkIsHaveRight.Checked)
                    {
                         
                        this.tempDataTable = cardMakeupBll.GetDataByCondition("and (a.status='0' or a.status='3') and a.id='" + this.UltraWebGridMakeup.DisplayLayout.Rows[i].Cells.FromKey("id").Value + "'");
                        if (this.tempDataTable.Rows.Count > 0)
                        {
                            cardMakeupBll.DeleteData(this.tempDataTable);
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
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "show", "alert('" + string.Concat(new object[] { Message.common_message_successcount, "：", intDeleteOk, ";", Message.common_message_errorcount, "：", intDeleteError }) + "')", true);

                }
                else
                {
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "show", "alert('" + Message.common_message_data_select + "')", true);
                    return;
                }
                //this.Query(false, "Goto");
                bindData(1);
                this.ProcessFlag.Value = "";

            }
            catch (Exception ex)
            {
                string msg = (ex.InnerException == null) ? ex.Message : ex.InnerException.Message;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "show", "alert('" + msg + "')", true);

            }
        }

#endregion

        #region 數據導入導出
        /// <summary>
        /// 數據上傳
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonImportSave_Click(object sender, EventArgs e)
        {
            try
            {
                //上傳數據
                string strFileName = this.FileUpload.FileName;      
                string strFileSize = this.FileUpload.PostedFile.ContentLength.ToString();
                string strFileExt = strFileName.Substring(strFileName.LastIndexOf(".") + 1);
                string strTime = string.Format("{0:yyyyMMddHHmmssffff}", DateTime.Now);
                string strFilePath = base.Server.MapPath(@"..\") + @"ExportFileTemp\" + strTime + ".xls";
                if (strFileExt == "xls")
                {
                    this.FileUpload.SaveAs(strFilePath);
                }
                else
                {
                    WriteMessage(2, Message.common_message_uploadxls);
                    return;
                }


                string WorkNo = "";
                string KQDate = "";
                string CardTime = "";
                string MakeupTypeName = "";
                string MakeupType = "";
                string ReasonName = "";
                string ReasonType = "";
                string ReasonRemark = "";
                string SALARYFLAG = "N";
                string errorMsg = "";
                string sKQDate = "";
                string ShiftNo = "";
                string nCardTime = "";
                int index = 0;
                int errorCount = 0;

                tmpImportDataTable.Clear();

                tmpImportDataTable.Columns.Add(new DataColumn("ErrorMsg", typeof(string)));
                tmpImportDataTable.Columns.Add(new DataColumn("WorkNo", typeof(string)));
                tmpImportDataTable.Columns.Add(new DataColumn("KQDate", typeof(string)));
                tmpImportDataTable.Columns.Add(new DataColumn("CardTime", typeof(string)));
                tmpImportDataTable.Columns.Add(new DataColumn("MakeupTypeName", typeof(string)));
                tmpImportDataTable.Columns.Add(new DataColumn("ReasonName", typeof(string)));
                tmpImportDataTable.Columns.Add(new DataColumn("ReasonRemark", typeof(string)));


                DataView dv = ExceltoDataView(strFilePath);     //數據格式驗證
                int inttotal = dv.Table.Rows.Count;
                string sysKqoQinDays = GetsysKqoQinDays();
                string condition = "";
                //  Function CommonFun = new Function();
                //日期格式強制轉換
                DateTime DToDay = Convert.ToDateTime(DateTime.Now.ToString("yyyy/MM/dd"));
                for (int i = 0; i < inttotal; i++)
                {
                    string CS40004;
                    WorkNo = dv.Table.Rows[i][0].ToString().Trim().ToUpper();   //工號
                    KQDate = dv.Table.Rows[i][1].ToString().Trim();     //      考勤日期
                    CardTime = dv.Table.Rows[i][2].ToString().Trim();       //刷卡時間
                    MakeupTypeName = dv.Table.Rows[i][3].ToString().Trim();     //補卡類別
                    ReasonName = dv.Table.Rows[i][4].ToString().Trim();     //異常原因
                    ReasonRemark = dv.Table.Rows[i][5].ToString().Trim();   //備注

                    if (WorkNo.IndexOf("注意") > 0) break;
                    //用戶判斷
                    if (cardMakeupBll.GetVWorkNoCount(WorkNo, sqlDep) == 0)  //判斷用戶是否有效
                    {
                        if (errorMsg.Length > 0)
                        {
                            errorMsg = errorMsg + ",";
                        }
                        errorMsg = errorMsg + Message.bfw_hrm_errorworkno;
                    }


                    //考勤日 
                    try
                    {
                        sKQDate = Convert.ToDateTime(KQDate).ToString("yyyy/MM/dd");    
                    }
                    catch (Exception)
                    {
                        sKQDate = KQDate;
                        if (errorMsg.Length > 0)
                        {
                            errorMsg = errorMsg + ",";
                        }
                        errorMsg = errorMsg + Message.common_message_data_errordate;
                    }

                    //刷卡時間
                    if (CardTime.Length > 0)
                    {
                        try
                        {
                            CardTime = DateTime.Parse(CardTime).ToString("HH:mm");
                        }
                        catch (Exception)
                        {
                            CardTime = dv.Table.Rows[i][2].ToString().Trim();
                            if (errorMsg.Length > 0)
                            {
                                errorMsg = errorMsg + ",";
                            }
                            errorMsg = errorMsg + ControlText.CardTime + Message.error;
                        }
                    }
                    else
                    {
                        if (errorMsg.Length > 0)
                        {
                            errorMsg = errorMsg + ",";
                        }
                        errorMsg = errorMsg + ControlText.CardTime + Message.common_message_required;
                    }

                    //補卡類別
                    if (MakeupTypeName.Length > 0)
                    {
                        string HrmFixedTypeDql = "SELECT DataType,DataCode,DataValue,DataTypeDetail,(DataCode||'?B'||DataValue)as newDataValue FROM GDS_ATT_TYPEDATA  WHERE DataType='KQMMakeup' and dataValue='" + MakeupTypeName + "'";
                        this.importDataTable = cardMakeupBll.getTBDataByCondition(HrmFixedTypeDql);
                        if (this.importDataTable.Rows.Count == 0)
                        {
                            if (errorMsg.Length > 0)
                            {
                                errorMsg = errorMsg + ",";
                            }
                            errorMsg = errorMsg + ControlText.MakeupTypeName + Message.NotExist;
                        }
                        else
                        {
                            MakeupType = this.importDataTable.Rows[0]["dataCode"].ToString().Trim();
                        }
                    }
                    else
                    {
                        if (errorMsg.Length > 0)
                        {
                            errorMsg = errorMsg + ",";
                        }
                        errorMsg = errorMsg + ControlText.MakeupTypeName + Message.common_message_required;
                    }


                    //考勤異常類型
                    /**
                    if (ReasonName.Length > 0)
                    {
                        //this.importDataTable = ((ServiceLocator) this.Session["serviceLocator"]).GetKqmExceptReasonData().GetDataByCondition("WHERE ReasonName='" + ReasonName + "' and EffectFlag='Y' ").Tables["kqm_exceptreason"];
                        string KqmExceptionReasonSQL = "SELECT * FROM GDS_ATT_EXCEPTREASON WHERE ReasonName='" + ReasonName + "' and EffectFlag='Y' ";
                        this.importDataTable = cardMakeupBll.getTBDataByCondition(KqmExceptionReasonSQL);
                        if (this.importDataTable.Rows.Count == 0)
                        {
                            if (errorMsg.Length > 0)
                            {
                                errorMsg = errorMsg + ",";
                            }
                            errorMsg = errorMsg + Message.kqm_kaoqindata_errorreasontype;
                        }
                        else
                        {
                            ReasonType = this.importDataTable.Rows[0]["ReasonNo"].ToString();
                            SALARYFLAG = this.importDataTable.Rows[0]["SALARYFLAG"].ToString();
                        }
                    }
                    else
                    {
                        if (errorMsg.Length > 0)
                        {
                            errorMsg = errorMsg + ",";
                        }
                        errorMsg = errorMsg + ControlText.gvHeadReasonName + Message.common_message_required;
                    }

                    **/

                    if (errorMsg.Length != 0)
                    {
                        goto Label_136C;
                    }

                    
                    //參數是否有效
                    if (!cardMakeupBll.GetValue("SELECT NVL(max(paravalue),'N') FROM gds_sc_parameter WHERE paraname='IsAllowMakeUpByIsNotKaoQin'").Equals("Y"))
                    {
                        goto Label_0B11;
                    }

                    //是否為出差人員

                    string condIsEvction = "SELECT NVL(count(0),0) FROM gds_att_employee a WHERE a.status = '0' AND NVL (a.joindate, TO_Date('" + sKQDate + "','YYYY/MM/DD') - 1) < TO_Date('" + sKQDate + "','YYYY/MM/DD') AND EXISTS ( SELECT 1 FROM gds_att_evectionapply e WHERE e.workno = a.workno AND (e.status = '2' OR e.status = '4') AND e.startdate <= TO_Date('" + sKQDate + "','YYYY/MM/DD') AND TO_Date('" + sKQDate + "','YYYY/MM/DD')  <= e.enddate AND e.isnotkaoqin = 'Y') AND a.workno='" + WorkNo + "'";
                    if (cardMakeupBll.GetValue(condIsEvction).Equals("1"))
                    {
                        condition = " and a.WorkNo='" + WorkNo + "' And a.KQDATE=to_date('" + sKQDate + "','yyyy/mm/dd') and a.status<>'2'  and trunc(sysdate)-a.kqdate<=" + sysKqoQinDays;
                    }
                    else
                    {
                        CS40004 = MakeupType;
                        if (CS40004 != null)
                        {
                            if (!(CS40004 == "0"))
                            {
                                if (CS40004 == "1")
                                {
                                    goto Label_0A41;
                                }
                                if (CS40004 == "2")
                                {
                                    goto Label_0A84;
                                }
                                if (CS40004 == "3")
                                {
                                    goto Label_0AC7;
                                }
                            }
                            else
                            {
                                condition = " and a.WorkNo='" + WorkNo + "' And a.KQDATE=to_date('" + sKQDate + "','yyyy/mm/dd') and a.ExceptionType in('D','C','O','F') and a.status<>'2' and a.OnDutyTime is null and trunc(sysdate)-a.kqdate<=" + sysKqoQinDays;
                            }
                        }
                    }
                    goto Label_0C73;
                Label_0A41: ;
                    condition = " and a.WorkNo='" + WorkNo + "' And a.KQDATE=to_date('" + sKQDate + "','yyyy/mm/dd') and a.ExceptionType in('D','C','O','F') and a.status<>'2' and a.OffDutyTime is null and trunc(sysdate)-a.kqdate<=" + sysKqoQinDays;
                    goto Label_0C73;
                Label_0A84: ;
                    condition = " and a.WorkNo='" + WorkNo + "' And a.KQDATE=to_date('" + sKQDate + "','yyyy/mm/dd') and (a.ExceptionType not in('O','F') or a.ExcepTionType is null) and a.OTOnDutyTime is null and trunc(sysdate)-a.kqdate<=" + sysKqoQinDays;
                    goto Label_0C73;
                Label_0AC7: ;
                    condition = " and a.WorkNo='" + WorkNo + "' And a.KQDATE=to_date('" + sKQDate + "','yyyy/mm/dd') and (a.ExceptionType not in('O','F') or a.ExcepTionType is null) and a.OTOffDutyTime is null and trunc(sysdate)-a.kqdate<=" + sysKqoQinDays;
                    goto Label_0C73;
                Label_0B11:
                    CS40004 = MakeupType;
                    if (CS40004 != null)
                    {
                        if (!(CS40004 == "0"))
                        {
                            if (CS40004 == "1")
                            {
                                goto Label_0BA9;
                            }
                            if (CS40004 == "2")
                            {
                                goto Label_0BEC;
                            }
                            if (CS40004 == "3")
                            {
                                goto Label_0C2F;
                            }
                        }
                        else
                        {
                            condition = " and a.WorkNo='" + WorkNo + "' And a.KQDATE=to_date('" + sKQDate + "','yyyy/mm/dd') and a.ExceptionType in('D','C','O','F') and a.status<>'2' and a.OnDutyTime is null and trunc(sysdate)-a.kqdate<=" + sysKqoQinDays;
                        }
                    }
                    goto Label_0C73;
                Label_0BA9: ;
                    condition = " and a.WorkNo='" + WorkNo + "' And a.KQDATE=to_date('" + sKQDate + "','yyyy/mm/dd') and a.ExceptionType in('D','C','O','F') and a.status<>'2' and a.OffDutyTime is null and trunc(sysdate)-a.kqdate<=" + sysKqoQinDays;
                    goto Label_0C73;
                Label_0BEC: ;
                    condition = " and a.WorkNo='" + WorkNo + "' And a.KQDATE=to_date('" + sKQDate + "','yyyy/mm/dd') and (a.ExceptionType not in('O','F') or a.ExcepTionType is null) and a.OTOnDutyTime is null and trunc(sysdate)-a.kqdate<=" + sysKqoQinDays;
                    goto Label_0C73;
                Label_0C2F: ;
                    condition = " and a.WorkNo='" + WorkNo + "' And a.KQDATE=to_date('" + sKQDate + "','yyyy/mm/dd') and (a.ExceptionType not in('O','F') or a.ExcepTionType is null) and a.OTOffDutyTime is null and trunc(sysdate)-a.kqdate<=" + sysKqoQinDays;
                Label_0C73:

                    //查看考勤記錄
                    this.importDataTable = cardMakeupBll.GetKaoQinDataByCondition(condition);
                    if (this.importDataTable.Rows.Count > 0)
                    {
                        if (cardMakeupBll.GetValue("select nvl(MAX(paravalue),'N') from gds_sc_parameter where paraname='IsOpenCardDataNumber'") != "N")
                        {
                            string startDate = DateTime.Now.AddDays((double)(-DateTime.Now.Day + 1)).ToString("yyyy/MM/dd");
                            string endDate = DateTime.Now.ToString("yyyy/MM/dd");
                            string sql = " select count(1) from gds_att_Makeup where workno = '" + WorkNo.ToUpper() + "' and kqdate >= to_date('" + startDate + "','yyyy-MM-dd') and kqdate <= to_date('" + endDate + "','yyyy-MM-dd') and reasontype in (select reasonno from gds_att_EXCEPTREASON where effectflag = 'Y' and salaryflag = 'Y')";
                            string count = cardMakeupBll.GetValue(sql);
                            string number = cardMakeupBll.GetValue("select nvl(MAX(paravalue),'0') from gds_sc_parameter where paraname='AddCardDataNumber'");
                            if ((number != "0") && (count == number))
                            {
                                if (errorMsg.Length > 0)
                                {
                                    errorMsg = errorMsg + ",";
                                }
                                errorMsg = Message.kaoqindata_forgetcardnumber + number + Message.common_times;
                            }
                        }
                        ShiftNo = this.importDataTable.Rows[0]["ShiftNo"].ToString();
                        nCardTime = string.Format(sKQDate + " " + CardTime, "yyyy/MM/dd HH:mm");
                        nCardTime = cardMakeupBll.ReturnCardTime(WorkNo, sKQDate, nCardTime, ShiftNo, MakeupType);
                        if (((DToDay.CompareTo(Convert.ToDateTime(sKQDate)) == 0) && ShiftNo.StartsWith("C")) && (TimeSpan.Parse(DateTime.Now.ToString("HH:mm")) < TimeSpan.Parse("19:00")))
                        {
                            if (errorMsg.Length > 0)
                            {
                                errorMsg = errorMsg + ",";
                            }
                            errorMsg = Message.kqm_kaoqindata_checknightkqdata;
                        }
                        if (string.Compare(nCardTime, DateTime.Now.ToString("yyyy/MM/dd HH:mm")) > 0)
                        {
                            if (errorMsg.Length > 0)
                            {
                                errorMsg = errorMsg + ",";
                            }
                            errorMsg = Message.CardTime + Message.error;
                        }
                        string KqmBellCardQuerySQL = "SELECT * FROM (SELECT a.CardTime, To_Char(a.CardTime,'hh24:mi:ss') as CardTimeMM,a.CARDNO,(select b.BellNo from gds_att_BellCard b where a.BellNo=b.BellNo or a.BellNo=b.ProduceID) BellNo,  b.WorkNo,b.LocalName, b.dname depname,   b.dcode ,(select AddRess from gds_att_BellCard z where z.BellNo=a.BellNo or z.ProduceID=a.BellNo) AddRess  FROM gds_att_BELLCARDDATA a,gds_att_Employee b where (b.CardNo=a.CardNo or b.workno=a.workno)  and b.WorkNo='" + WorkNo + "' and a.CardTime = to_date('" + DateTime.Parse(nCardTime).ToString("yyyy/MM/dd HH:mm") + "','yyyy/mm/dd hh24:mi:ss') )";
                        //this.tempDataTable = ((ServiceLocator) this.Session["serviceLocator"]).GetKQMBellCardQuery().GetDataByCondition("KQM_BELLCARDDATA", "and b.WorkNo='" + WorkNo + "' and a.CardTime = to_date('" + DateTime.Parse(nCardTime).ToString("yyyy/MM/dd HH:mm") + "','yyyy/mm/dd hh24:mi:ss')").Tables["KQM_BellCardQuery"];
                        this.tempDataTable = cardMakeupBll.getTBDataByCondition(KqmBellCardQuerySQL);
                        if (this.tempDataTable.Rows.Count > 0)
                        {
                            if (errorMsg.Length > 0)
                            {
                                errorMsg = errorMsg + ",";
                            }
                            errorMsg = Message.makeup_checkrepeat;
                        }
                        if (errorMsg.Length == 0)
                        {
                            this.tempDataTable = cardMakeupBll.GetDataByCondition("and a.WorkNo='" + WorkNo + "' and ((a.KQDate = to_date('" + DateTime.Parse(sKQDate).ToString("yyyy/MM/dd") + "','yyyy/mm/dd') and a.MakeupType='" + MakeupType + "') or a.CardTime = to_date('" + DateTime.Parse(nCardTime).ToString("yyyy/MM/dd HH:mm") + "','yyyy/mm/dd hh24:mi:ss')) ");
                            if (this.tempDataTable.Rows.Count > 0)
                            {
                                if (errorMsg.Length > 0)
                                {
                                    errorMsg = errorMsg + ",";
                                }
                                errorMsg = errorMsg + Message.kqm_kaoqindata_checknightkqdata;
                            }
                            else
                            {

                                this.row = this.tempDataTable.NewRow();
                                this.row.BeginEdit();
                                this.row["WorkNo"] = WorkNo;
                                this.row["KQDate"] = sKQDate;
                                this.row["CardTime"] = nCardTime;
                                this.row["MakeupType"] = MakeupType;
                                this.row["ReasonType"] = ReasonType;
                                this.row["ReasonRemark"] = ReasonRemark;
                                this.row["DecSalary"] = SALARYFLAG;
                                this.row["Modifier"] = CurrentUserInfo.Personcode.ToString();
                                this.row.EndEdit();
                                this.tempDataTable.Rows.Add(this.row);
                                this.tempDataTable.AcceptChanges();
                                cardMakeupBll.SaveData("Add", this.tempDataTable);
                                index++;
                            }
                        }
                    }
                    else
                    {
                        CS40004 = MakeupType;
                        if (CS40004 != null)
                        {
                            if (!(CS40004 == "0"))
                            {
                                {
                                    goto Label_131F;
                                }
                                if (CS40004 == "2")
                                {
                                    goto Label_1338;
                                }
                                if (CS40004 == "3")
                                {
                                    goto Label_1351;
                                }
                            }
                            else
                            {
                                errorMsg = Message.kqm_kaoqindata_errorrkaoqindata + sysKqoQinDays;
                            }
                        }
                    }
                    goto Label_136C;
                Label_131F:
                    errorMsg = Message.kqm_kaoqindata_errorrkaoqindata + sysKqoQinDays;
                    goto Label_136C;
                Label_1338:
                    errorMsg = Message.kqm_kaoqindata_errorrkaoqindata + sysKqoQinDays;
                    goto Label_136C;
                Label_1351:
                    errorMsg = Message.kqm_kaoqindata_errorrkaoqindata + sysKqoQinDays;
                Label_136C:
                    if (errorMsg.Length > 0)
                    {
                        errorCount++;
                        this.tmpImportDataTable.Rows.Add(new string[] { errorMsg, WorkNo, sKQDate, CardTime, MakeupTypeName, ReasonName, ReasonRemark });
                    }
                    errorMsg = "";
                }
                this.labeluploadMsg.Text = string.Concat(new object[] { Message.bfw_hrm_upsuccesscount, "：", index, "  ;", Message.bfw_hrm_upsfailcount, "：", errorCount, " ." });
                this.UltraWebGridImport.DataSource = this.tmpImportDataTable.DefaultView;
                this.UltraWebGridImport.DataBind();
                this.ProcessFlag.Value = "";
            }
            catch (Exception ex)
            {
                WriteMessage(2, (ex.InnerException == null) ? ex.Message : ex.InnerException.Message);
            }
        }

        /// <summary>
        /// 導入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonImport_Click(object sender, EventArgs e)
        {
            this.labeluploadMsg.Text = "";
            this.UltraWebGridImport.Rows.Clear();
            if (!this.PanelImport.Visible)
            {
                this.PanelImport.Visible = true;
                this.PanelData.Visible = false;
                this.btnFormQuery.Enabled = false;
                this.btnReset.Enabled = false;
                this.Btn_delete.Enabled = false;
                this.btn_check.Enabled = false;
                this.btncancelcheck.Enabled = false;
                this.btnsendsign.Enabled = false;
               // this.BtnViewSchedule.Enabled = false;
                this.btnAdd.Enabled = false;
                this.btnModify.Enabled = false;
                this.btnImport.Text = ControlText.btnReturn;
                this.labeluploadMsg.Text = "";
            }
            else
            {
                this.PanelImport.Visible = false;
                this.PanelData.Visible = true;
                this.btnFormQuery.Enabled = true;
                this.btnReset.Enabled = true;
                this.Btn_delete.Enabled = true;
                this.btn_check.Enabled = true;
                this.btncancelcheck.Enabled = true;
                this.btnsendsign.Enabled = true;
               // this.BtnViewSchedule.Enabled = true;
                this.btnAdd.Enabled = true;
                this.btnModify.Enabled = true;
                this.btnImport.Text = ControlText.btnImport;

            }
        }

        /// <summary>
        /// 導出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonExport_Click(object sender, EventArgs e)
        { 
            if (!this.PanelImport.Visible)
            {
                if (this.UltraWebGridMakeup.Rows.Count == 0)
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "show", "alert('" + Message.NoDataExport + "')", true);
                }
                else
                {
                  //  GetPageValue();
                    //DataTable tempExport = cardMakeupBll.GetActivityApplyList(model, base.SqlDep.ToString(), depCode, workNoStrings, dateFrom, dateTo);
                    
            //查詢條件
            string condition = "";
            string ddlStr = "";
            string[] temVal = null;
            tempDataTable.Clear();
           // if (flag != 0)
           // {
                //查詢條件
                condition = "";
                if (this.textBoxEmployeeNo.Text.Trim().Length != 0)
                {
                    condition += " AND a.WorkNo = '" + this.textBoxEmployeeNo.Text.ToUpper().Trim() + "'";
                }
                if (this.textBoxName.Text.Trim().Length != 0)
                {
                    condition += " AND a.LocalName like '" + this.textBoxName.Text.Trim() + "%'";
                }
                if (this.ddlStatus.SelectedValue != "")
                {
                    condition += " AND a.STATUS ='" + this.ddlStatus.SelectedValue + "'";
                }
                ddlStr = "";
                if (this.ddlReasonType.SelectedValue != "")
                {
                    temVal = this.ddlReasonType.SelectedValuesToString(",").Split(new char[] { ',' });

                    System.Text.StringBuilder tmpStr = new System.Text.StringBuilder();

                    for (int iLoop = 0; iLoop < temVal.Length; iLoop++)
                    {
                        //判斷不為最后一行


                        if (iLoop != temVal.Length - 1)
                        {
                            tmpStr.Append("'" + temVal[iLoop] + "',");
                        }
                        else
                        {
                            tmpStr.Append("'" + temVal[iLoop] + "'");
                        }
                        //ddlStr = ddlStr + " a.ReasonType='" + temVal[iLoop] + "' or"; 

                    }
                    //ddlStr = ddlStr.Substring(0, ddlStr.Length - 2);
                    // condition = condition + " and (" + ddlStr + ")";
                    if (tmpStr.ToString() != "")

                        condition += " and a.MAKEUPTYPE in (" + tmpStr.ToString() + ")";

                        //condition += tmpStr.ToString();
                }
                ddlStr = "";
                if (this.textBoxBatchEmployeeNo.Text.Trim().Length != 0)
                {
                    string[] workNoList = this.textBoxBatchEmployeeNo.Text.Trim().Split(new char[] { Convert.ToChar('\r') });
                    for (int i = 0; i < workNoList.Length; i++)
                    {
                        if (workNoList[i].ToString().Length > 0)
                        {
                            ddlStr = ddlStr + "'" + workNoList[i].ToString().Trim().ToUpper().Replace("\n", "") + "',";
                        }
                    }
                    ddlStr = ddlStr.Substring(0, ddlStr.Length - 1);
                    condition = condition + " and a.WorkNo in (" + ddlStr + ")";
                }
                if ((this.textBoxEmployeeNo.Text.Trim().Length == 0) && (this.textBoxName.Text.Trim().Length == 0))
                {
                    if ((this.textBoxKQDateFrom.Text.Trim().Length == 0) || (this.textBoxKQDateTo.Text.Trim().Length == 0))
                    {
                        //  WriteMessage(1, base.GetResouseValue("common.message.datenotnull"));
                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "show", "alert('" +Message.common_message_datenotnull+ "')", true);

                        return;
                    }

                }
                if (this.textBoxKQDateFrom.Text.Trim().Length != 0)
                {
                    condition = condition + " AND a.KQDate >= to_date('" + DateTime.Parse(this.textBoxKQDateFrom.Text.Trim()).ToString("yyyy/MM/dd") + "','yyyy/mm/dd') ";
                }
                if (this.textBoxKQDateTo.Text.Trim().Length != 0)
                {
                    condition = condition + " AND a.KQDate <= to_date('" + DateTime.Parse(this.textBoxKQDateTo.Text.Trim()).ToString("yyyy/MM/dd") + "','yyyy/mm/dd') ";
                }
                if (this.textBoxBillNo.Text.Trim().Length != 0)
                {
                    condition = condition + " AND a.billno = '" + this.textBoxBillNo.Text.Trim() + "'";
                }
                if (this.ddlDecSalary.SelectedValue != "")
                {
                    condition = condition + " AND a.DecSalary ='" + this.ddlDecSalary.SelectedValue + "'";
                }
           // }
            this.ViewState.Add("condition", condition); 

            //
            //數據綁定

            DataTable tempExport = cardMakeupBll.CardMakeupListNoPage(condition);
           
                    if (tempExport.Rows.Count != 0)
                    {
                        // List<ActivityModel> list = activityApplyBll.GetList(tempExport);

                       // List<Object> list = OrmHelper.SetDataTableToList(tempExport);

                         
                       

                        List<WorkFlowCardMakeupModel> list = cardMakeupBll.GetList(tempExport);

                        string[] header = {ControlText.gvDepName,
                                           ControlText.gvHeadActivityWorkNo,
                                           ControlText.gvHeaderLocalName, 
                                           ControlText.gvHeadShiftDesc,  
                                           ControlText.gvKQDate,
                                           ControlText.gvOTMRealCardTime,
                                           ControlText.cardMakeupType,
                                           ControlText.workFlowDecSalary,
                                           ControlText.gvApproveFlagName,
                                           ControlText.gvHeadReasonName,
                                           ControlText.gvHeadReasonRemark,
                                           ControlText.workFlowDeTimes,
                                           ControlText.gvHeadBillNo,
                                           ControlText.gvheadsignname,
                                           ControlText.gvOTMAdvanceApproveDate,
                                           ControlText.gvOTMAdvanceApRemark,
                                           ControlText.gvOTMAdvanceModifier,
                                           ControlText.gvOTMAdvanceModifyDate   
                                          
                                          };
                        //properties中的元素必須和Model中的名稱大小寫一致，否則無法進行相關的數據獲取

                        string[] properties = {"Dname", 
                                               "Workno", 
                                               "Localname", 
                                               "Shiftdesc", 
                                               "Kqdate", 
                                               "Cardtime", 
                                               "Makeuptypename", 
                                               "Decsalary", 
                                               "Statusname", 
                                               "Reasonname", 
                                               "Reasonremark",
                                               "Detimes",
                                               "Billno",
                                               "Approvername",
                                               "Approvedate",
                                               "Apremark",
                                               "Modifier",
                                               "Modifydate"
                                              };
                        string filePath = MapPath("~/ExportFileTemp/") + DateTime.Now.Ticks + ".xls";
                        NPOIHelper.ExportExcel(list, header, properties, 5000, filePath);
                         
                        PageHelper.ReturnHTTPStream(filePath, true);
                    }
                }
            }
            else
            {
              /**
                if (this.UltraWebGridImport.Rows.Count == 0)
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "show", "alert('" + Message.NoDataExport + "')", true);
                }
                else
                {
                    List<ActivityTempModel> list = activityApplyTempBll.GetList(dt_global);
                    string[] header = { ControlText.gvHeadActivityErrorMsg, ControlText.gvHeadActivityWorkNo, ControlText.gvHeadActivityLocalName, 
                                          ControlText.gvHeadActivityOTDate, ControlText.gvHeadActivityOTType,ControlText.gvHeadActivityConfirmHours, 
                                      ControlText.gvHeadActivityWorkDesc,ControlText.lblStartTime,ControlText.lblEndTime};
                    string[] properties = { "ErrorMsg", "WorkNo", "LocalName", "OTDate", "OTType", "ConfirmHours", "WorkDesc", "StartTime", "EndTime" };
                    string filePath = MapPath("~/ExportFileTemp/") + DateTime.Now.Ticks + ".xls";
                    NPOIHelper.ExportExcel(list, header, properties, 5000, filePath);
                    PageHelper.ReturnHTTPStream(filePath, true);

                }

                **/
            }
        }
               

        #endregion


        #region 簽核相關功能
        /// <summary>
        /// 核準
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonAudit_Click(object sender, EventArgs e)
        {
            try
            {
                int i;
                CellItem GridItem;
                CheckBox chkIsHaveRight;
                int intDeleteOk = 0;
                int intDeleteError = 0;
                TemplatedColumn tcol = (TemplatedColumn)this.UltraWebGridMakeup.Bands[0].Columns[0];
                for (i = 0; i < this.UltraWebGridMakeup.Rows.Count; i++)
                {
                    GridItem = (CellItem)tcol.CellItems[i];
                    chkIsHaveRight = (CheckBox)GridItem.FindControl("CheckBoxCell");
                    if (chkIsHaveRight.Checked && (this.UltraWebGridMakeup.Rows[i].Cells.FromKey("Status").Text.Trim() != "0"))
                    {
                        WriteMessage(1, Message.AuditUnaudit);
                        return;
                    }
                }
                for (i = 0; i < this.UltraWebGridMakeup.Rows.Count; i++)
                {
                    GridItem = (CellItem)tcol.CellItems[i];
                    chkIsHaveRight = (CheckBox)GridItem.FindControl("CheckBoxCell");
                    if (chkIsHaveRight.Checked)
                    {
                        this.tempDataTable = cardMakeupBll.GetDataByCondition("and a.id='" + this.UltraWebGridMakeup.DisplayLayout.Rows[i].Cells.FromKey("id").Value + "'");
                        if (this.tempDataTable.Rows.Count > 0)
                        {
                            cardMakeupBll.Audit(this.tempDataTable);
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
                    WriteMessage(0, string.Concat(new object[] { Message.bfw_hrm_upsuccesscount, "：", intDeleteOk, ";", Message.common_message_errorcount, "：", intDeleteError }));
                }
                else
                {
                    WriteMessage(1,Message.common_message_data_select);
                    return;
                }
              //  this.Query(false, "Goto");
                bindData(1);
                this.ProcessFlag.Value = "";
            }
            catch (Exception ex)
            {
                WriteMessage(2, (ex.InnerException == null) ? ex.Message : ex.InnerException.Message);
            }
        }
        /// <summary>
        /// 取消核準
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonCancelAudit_Click(object sender, EventArgs e)
        {
            try
            {
                int i;
                CellItem GridItem;
                CheckBox chkIsHaveRight;
                string sysKqoQinDays = GetsysKqoQinDays();
                string condition = "";
                int intDeleteOk = 0;
                int intDeleteError = 0;
                string WorkNo = "";
                string KQDate = "";
                TemplatedColumn tcol = (TemplatedColumn)this.UltraWebGridMakeup.Bands[0].Columns[0];
                for (i = 0; i < this.UltraWebGridMakeup.Rows.Count; i++)
                {
                    GridItem = (CellItem)tcol.CellItems[i];
                    chkIsHaveRight = (CheckBox)GridItem.FindControl("CheckBoxCell");
                    if (chkIsHaveRight.Checked)
                    {
                        if (this.UltraWebGridMakeup.Rows[i].Cells.FromKey("Status").Text.Trim() != "2")
                        {
                            WriteMessage(1, Message.AuditUncancelaudit);
                            return;
                        }
                        condition = "select trunc(sysdate)-to_date('" + Convert.ToDateTime(this.UltraWebGridMakeup.Rows[i].Cells.FromKey("KQDate").Text).ToString("yyyy/MM/dd") + "','yyyy/mm/dd') from dual";
                        if (Convert.ToDouble(cardMakeupBll.GetValue(condition)) > Convert.ToDouble(sysKqoQinDays))
                        {
                            WriteMessage(1, Message.kaoqindata_checkreget);
                            return;
                        }
                    }
                }
                for (i = 0; i < this.UltraWebGridMakeup.Rows.Count; i++)
                {
                    GridItem = (CellItem)tcol.CellItems[i];
                    chkIsHaveRight = (CheckBox)GridItem.FindControl("CheckBoxCell");
                    if (chkIsHaveRight.Checked)
                    {
                        this.tempDataTable = cardMakeupBll.GetDataByCondition("and a.id='" + this.UltraWebGridMakeup.DisplayLayout.Rows[i].Cells.FromKey("id").Value + "'");
                        if (this.tempDataTable.Rows.Count > 0)
                        {
                            cardMakeupBll.CancelAudit(this.tempDataTable);
                            WorkNo = this.UltraWebGridMakeup.DisplayLayout.Rows[i].Cells.FromKey("WorkNo").Text;
                            KQDate = Convert.ToDateTime(this.UltraWebGridMakeup.DisplayLayout.Rows[i].Cells.FromKey("KQDate").Text).ToString("yyyy/MM/dd");
                            //cardMakeupBll.GetKaoQinDataByCondition
                            moveShitBll.GetKaoQinData(WorkNo, "null", KQDate, KQDate);
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
                    WriteMessage(0, string.Concat(new object[] {Message.common_message_successcount, "：", intDeleteOk, ";", Message.common_message_errorcount, "：", intDeleteError }));
                }
                else
                {
                    WriteMessage(1, Message.common_message_data_select);
                    return;
                }
                //this.Query(false, "Goto");
                bindData(1);
                this.ProcessFlag.Value = "";
            }
            catch (Exception ex)
            {
                WriteMessage(2, (ex.InnerException == null) ? ex.Message : ex.InnerException.Message);
            }
        }

        /// <summary>
        /// 送簽
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonSendAudit_Click(object sender, EventArgs e)
        {
            SendOrgAudit();
            /**
            try
            {
                int i;
                CellItem GridItem;
                CheckBox chkIsHaveRight;
                int intSendOK = 0;
                int intSendBillNo = 0;
                int intSendError = 0;
                string OrgCode = "";
                string BillNo = "";
                string AuditOrgCode = "";
                string BillTypeCode = "KQMMakeup";
                string BillTypeNo = "KQU";
                SortedList BillNoOrgCode = new SortedList();
                TemplatedColumn tcol = (TemplatedColumn)this.UltraWebGridMakeup.Bands[0].Columns[0];
                bool bResult = false;

                #region 判斷是否已經核准
                for (i = 0; i < this.UltraWebGridMakeup.Rows.Count; i++)
                {
                    GridItem = (CellItem)tcol.CellItems[i];
                    chkIsHaveRight = (CheckBox)GridItem.FindControl("CheckBoxCell");
                    if (chkIsHaveRight.Checked && ((this.UltraWebGridMakeup.Rows[i].Cells.FromKey("Status").Text != "0") && (this.UltraWebGridMakeup.Rows[i].Cells.FromKey("Status").Text != "3")))
                    {
                        WriteMessage(1,Message.common_message_audit_unsendaudit);
                        return;
                    }
                }
                #endregion

                #region 送簽

                for (i = 0; i < this.UltraWebGridMakeup.Rows.Count; i++)
                {
                    GridItem = (CellItem)tcol.CellItems[i];
                    chkIsHaveRight = (CheckBox)GridItem.FindControl("CheckBoxCell");
                   
                    if (chkIsHaveRight.Checked)
                     {
                        BillNo = "";
                        AuditOrgCode = "";  //獲取部門代碼
                     
                        OrgCode = string.IsNullOrEmpty(this.HiddenOrgCode.Value) ? this.UltraWebGridMakeup.Rows[i].Cells.FromKey("DCode").Text.Trim() : this.HiddenOrgCode.Value.Trim();
                        
                        string senduser = this.UltraWebGridMakeup.Rows[i].Cells.FromKey("WorkNo").Text.Trim();

                           AuditOrgCode = cardMakeupBll.GetWorkFlowOrgCode(OrgCode, BillTypeCode);
                           if (AuditOrgCode.Length > 0)
                            {
                             //   if (BillNoOrgCode.IndexOfKey(AuditOrgCode) >= 0)
                              //  {
                              //      BillNo = BillNoOrgCode.GetByIndex(BillNoOrgCode.IndexOfKey(AuditOrgCode)).ToString();
                             //       cardMakeupBll.SaveAuditData("Modify", this.UltraWebGridMakeup.Rows[i].Cells.FromKey("ID").Text.Trim(), BillNo, AuditOrgCode);
                              //      intSendOK++;
                              //  }
                              //  else
                              //  {
                              //      BillNo = cardMakeupBll.SaveAuditData("Add", this.UltraWebGridMakeup.Rows[i].Cells.FromKey("ID").Text.Trim(), BillTypeNo, AuditOrgCode);
                              //     //cardMakeupBll.SaveData(BillNo, AuditOrgCode, CurrentUserInfo.Personcode.ToString());
                              //     // cardMakeupBll.SaveAuditStatusData(BillNo, AuditOrgCode, BillTypeCode);
                               //       bll_abnormal.WFMSaveData(BillNo, AuditOrgCode, CurrentUserInfo.Personcode,logmodel);
                               //       bll_abnormal.WFMSaveAuditStatusData(BillNo, AuditOrgCode, BillTypeCode, senduser, sFlow_LevelRemark,logmodel);
                               //     intSendBillNo++;
                               //     intSendOK++;
                              //      BillNoOrgCode.Add(AuditOrgCode, BillNo);
                               // }
                               
                                string WorkNo = this.UltraWebGridMakeup.Rows[i].Cells.FromKey("WorkNo").Text.Trim();
                                
                                bResult = cardMakeupBll.SaveAuditData_new(WorkNo, BillTypeNo,BillTypeCode, CurrentUserInfo.Personcode, AuditOrgCode, sFlow_LevelRemark, logmodel);
                                if (bResult)
                                {
                                    intSendBillNo++;
                                    intSendOK++;
                                }
                            }
                            else
                            {
                                intSendError++;
                            } 
                    }

                }
                #endregion

                if ((intSendOK + intSendError) > 0)
                {
                    if (intSendError > 0)
                    {
                        WriteMessage(1, string.Concat(new object[] { Message.common_message_successcount, "：", intSendOK, ";", Message.common_message_billcount, "：", intSendBillNo, ";",Message.common_message_errorcount, "：", intSendError, "(",Message.common_message_noworkflow, ")" }));
                    }
                    else
                    {
                        WriteMessage(1, string.Concat(new object[] { Message.common_message_successcount, "：", intSendOK, ";", Message.common_message_billcount, "：", intSendBillNo }));
                    }
                }
                else
                {
                    WriteMessage(1,Message.common_message_data_select);
                    return;
                }
                //this.Query(false, "Goto");
                bindData(1);
                this.ProcessFlag.Value = "";
            }
            catch (Exception ex)
            {
                WriteMessage(2, (ex.InnerException == null) ? ex.Message : ex.InnerException.Message);
            }

            **/
        }


         

        /// <summary>
        /// 組織送簽
        /// </summary>
        private void SendOrgAudit()
        {
            try
            {
                int intSendOK = 0;
                int intSendBillNo = 0;
                int intSendError = 0;
                //string OTDate = "";
                string OrgCode = "";
                string AuditOrgCode = "";
                string BillTypeCode = "KQMMakeup";
                string BillNoType = "KQU";
                string OTType = "";
                TemplatedColumn tcol = (TemplatedColumn)UltraWebGridMakeup.Bands[0].Columns[0];

                for (int i = 0; i < UltraWebGridMakeup.Rows.Count; i++)
                {
                    CellItem GridItem = (CellItem)tcol.CellItems[i];
                    CheckBox chkIsHaveRight = (CheckBox)(GridItem.FindControl("CheckBoxCell"));
                    if (chkIsHaveRight.Checked)
                    {
                        if (UltraWebGridMakeup.Rows[i].Cells.FromKey("Status").Text != "0" &&
                            UltraWebGridMakeup.Rows[i].Cells.FromKey("Status").Text != "3")
                        {
                            WriteMessage(1, Message.common_message_audit_unsendaudit);
                            return;
                        }
                         
                    }
                }
                
                Dictionary<string, List<string>> dicy = new Dictionary<string, List<string>>();

                for (int i = 0; i < UltraWebGridMakeup.Rows.Count; i++)
                {
                    CellItem GridItem = (CellItem)tcol.CellItems[i];
                    CheckBox chkIsHaveRight = (CheckBox)(GridItem.FindControl("CheckBoxCell"));
                    if (chkIsHaveRight.Checked)
                    { 
                        AuditOrgCode = "";  //獲取部門代碼
                        OrgCode = UltraWebGridMakeup.Rows[i].Cells.FromKey("DCode").Text.Trim();
                        //OrgCode = string.IsNullOrEmpty(this.HiddenOrgCode.Value) ? this.UltraWebGridMakeup.Rows[i].Cells.FromKey("DCode").Text.Trim() : this.HiddenOrgCode.Value.Trim();
                        AuditOrgCode = cardMakeupBll.GetWorkFlowOrgCode(OrgCode, BillTypeCode);

                        string key = AuditOrgCode;// +"^" + OTType;
                        List<string> list = new List<string>();
                        if (!dicy.ContainsKey(key) && AuditOrgCode.Length > 0)
                        {
                            dicy.Add(key, list);
                        }
                        else if (AuditOrgCode.Length == 0)
                        {
                            intSendError += 1;

                        }
                        AuditOrgCode = "";
                    }
                }

                for (int i = 0; i < UltraWebGridMakeup.Rows.Count; i++)
                {
                    CellItem GridItem = (CellItem)tcol.CellItems[i];
                    CheckBox chkIsHaveRight = (CheckBox)(GridItem.FindControl("CheckBoxCell"));
                    if (chkIsHaveRight.Checked)
                    { 
                        AuditOrgCode = "";  //獲取部門代碼
                        OrgCode = UltraWebGridMakeup.Rows[i].Cells.FromKey("DCode").Text.Trim();
                        AuditOrgCode = cardMakeupBll.GetWorkFlowOrgCode(OrgCode, BillTypeCode);
                        string key = AuditOrgCode;// +"^" + OTType;
                        
                        if (dicy[key] != null)
                        {
                            dicy[key].Add(UltraWebGridMakeup.Rows[i].Cells.FromKey("ID").Text.Trim());
                        }
                    }
                }

                
                int count = cardMakeupBll.SaveOrgAuditData("Add", dicy, BillNoType, BillTypeCode, CurrentUserInfo.Personcode,logmodel);
                intSendBillNo = count;
                intSendOK += 1;
                
                if (intSendOK + intSendError > 0)
                {
                    if (intSendError > 0)
                    {
                        this.WriteMessage(1, Message.SuccCount + intSendOK + ";" + Message.common_message_billcount + "：" + intSendBillNo + ";" + Message.FaileCount + intSendError + "(" + Message.common_message_noworkflow + ")");
                    }
                    else
                    {
                        this.WriteMessage(1, Message.SuccCount + intSendOK + ";" + Message.common_message_billcount + "：" + intSendBillNo);
                    }
                }
                else
                {
                    this.WriteMessage(1, Message.AtLastOneChoose);
                    return;
                }
                //this.Query(false, "Goto");
                bindData(1);
                this.ProcessFlag.Value = "";
                this.HiddenOrgCode.Value = "";
            }
            catch (Exception ex)
            {
                this.WriteMessage(2, ex.InnerException == null ? ex.Message : ex.InnerException.Message);
            }
        }

        #endregion  
        
        #region 公共方法
        protected void WriteMessage(int messageType, string message)
        {
            switch (messageType)
            {
                case 0:
                  //  this.Page.RegisterClientScriptBlock("jump", "<script language='JavaScript'>window.status='" + message.Replace("\"", "'").Replace("\r", "").Replace("\n", "") + "';</script>");
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "show", "alert(\"" + message.Replace("\"", "'").Replace("\r", "").Replace("\n", "") + "\")", true);
                    break;

                case 1:
                    //this.Page.RegisterClientScriptBlock("jump", "<script language='JavaScript'>alert(\"" + message.Replace("\"", "'").Replace("\r", "").Replace("\n", "") + "\");</script>");
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "show", "alert(\"" + message.Replace("\"", "'").Replace("\r", "").Replace("\n", "") + "\")", true);
                    break;

                case 2:
                    //this.Page.RegisterClientScriptBlock("jump", "<script language='JavaScript'>alert(\"" + message.Replace("\"", "'").Replace("\r", "").Replace("\n", "") + "\");</script>");
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "show", "alert(\"" + message.Replace("\"", "'").Replace("\r", "").Replace("\n", "") + "\")", true);
                    break;
            }
        }

        public string GetsysKqoQinDays()
        {
            string sysKqoQinDays = "";
            string condition = "";
            try
            {
                sysKqoQinDays = cardMakeupBll.GetValue("select nvl(MAX(paravalue),'3') from gds_sc_parameter where paraname='KaoQinDataFLAG'");
                int i = 0;
                int WorkDays = 0;
                string UserOTType = "";
                while (i < Convert.ToDouble(sysKqoQinDays))
                {
                    condition = "SELECT workflag FROM gds_att_bgcalendar WHERE workday = to_date('" + DateTime.Now.AddDays((double)((-1 - i) - WorkDays)).ToString("yyyy/MM/dd") + "','yyyy/MM/dd') AND bgcode IN (SELECT depcode FROM gds_sc_department WHERE levelcode = '0')";
                    UserOTType = cardMakeupBll.GetValue(condition);
                    if (UserOTType.Length == 0)
                    {
                        break;
                    }
                    if (UserOTType.Equals("Y"))
                    {
                        i++;
                    }
                    else
                    {
                        WorkDays++;
                    }
                }
                sysKqoQinDays = Convert.ToString((int)(i + WorkDays));
            }
            catch (Exception)
            {
                sysKqoQinDays = "10";
            }
            // if (this.Session["roleCode"].ToString().Equals("Admin"))
            // {
            sysKqoQinDays = Convert.ToString((int)(Convert.ToInt32(sysKqoQinDays) + 30));
            //  }
            return sysKqoQinDays;
        }
        public DataView ExceltoDataView(string strFilePath)
        {
            DataView dv;
            try
            {
                OleDbConnection conn = new OleDbConnection("Provider=Microsoft.Jet.Oledb.4.0;Data Source=" + strFilePath + ";Extended Properties='Excel 8.0;HDR=YES;IMEX=1'");
                conn.Open();
                object[] CSs0s0001 = new object[4];
                CSs0s0001[3] = "TABLE";
                DataTable tblSchema = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, CSs0s0001);
                string tableName = Convert.ToString(tblSchema.Rows[0]["TABLE_NAME"]);
                if (tblSchema.Rows.Count > 1)
                {
                    tableName = "sheet1$";
                }
                string sql_F = "SELECT * FROM [{0}]";
                OleDbDataAdapter adp = new OleDbDataAdapter(string.Format(sql_F, tableName), conn);
                DataSet ds = new DataSet();
                adp.Fill(ds, "Excel");
                dv = ds.Tables[0].DefaultView;
                conn.Close();
            }
            catch (Exception)
            {
                Exception strEx = new Exception("請確認是否使用模板上傳(上傳的Excel中第一個工作表名稱是否為Sheet1)");
                throw strEx;
            }
            return dv;
        }


        #endregion 
    }
}
