using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Serialization;
using System.Data;
using GDSBG.MiABU.Attendance.BLL.WorkFlow;
using GDSBG.MiABU.Attendance.BLL.KQM.KaoQinData;
using Resources;
using System.Data.OleDb;
using Infragistics.WebUI.UltraWebGrid;

namespace GDSBG.MiABU.Attendance.Web.WorkFlowForm
{
    public partial class PCMWorkFlowCardMakeupList :  BasePage
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
                //加載預信息
                HiddenWorkNo.Value = CurrentUserInfo.Personcode;
                ModuleCode.Value = Request.QueryString["ModuleCode"].ToString();
               // this.textBoxEmployeeNo.Text = CurrentUserInfo.Personcode;
                ddclDataBind("KQMMakeup", ddlReasonType);    //未刷補卡異常原因
                getddlStatus();     //狀態值 
               // SetSelector(ImageDepCode, textBoxDepCode, textBoxDepName);
                this.textBoxKQDateFrom.Text = DateTime.Now.AddMonths(-1).ToString("yyyy/MM/dd");  //格式化 
                this.textBoxKQDateTo.Text = DateTime.Now.AddDays(-1).ToString("yyyy/MM/dd");    //格式化

            }
            //設置日歷控件到文本欄位

            SetCalendar(textBoxKQDateFrom, textBoxKQDateTo);
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
            tempDataTable.Clear();
            if (flag != 0)
            {
                //查詢條件
                condition += " AND a.WorkNo = '" + CurrentUserInfo.Personcode + "'";
                
                /**    
                if (this.textBoxEmployeeNo.Text.Trim().Length != 0)
                {
                    condition += " AND a.WorkNo = '" + this.textBoxEmployeeNo.Text.ToUpper().Trim() + "'";
                }
                if (this.textBoxName.Text.Trim().Length != 0)
                {
                    condition += " AND b.LocalName like '" + this.textBoxName.Text.Trim() + "%'";
                }
                **/

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
                        condition += tmpStr.ToString();
                }
                ddlStr = "";
                /**
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
                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "show", "alert('" + Message.common_message_datenotnull + "')", true);

                        return;
                    }

                }
                **/
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
            /**
            this.textBoxDepName.Text = "";
            this.textBoxDepCode.Text = "";
            this.textBoxEmployeeNo.Text = "";
            this.textBoxName.Text = "";
            **/
            this.textBoxBillNo.Text = "";
            this.textBoxKQDateFrom.Text = "";
            this.textBoxKQDateTo.Text = "";
          //  this.textBoxKQDateFrom.Text = DateTime.Now.AddMonths(-1).ToString("yyyy/mm/dd");  //格式化

           // this.textBoxKQDateTo.Text = DateTime.Now.AddDays(-1.0).ToString("yyyy/mm/dd");    //格式化
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

                        this.tempDataTable = cardMakeupBll.GetDataByCondition("and (a.status='0' or a.status='3') and a.workno='"+CurrentUserInfo.Personcode+"' and a.id='" + this.UltraWebGridMakeup.DisplayLayout.Rows[i].Cells.FromKey("id").Value + "'");
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

     
    

        #region 公共方法
        protected void WriteMessage(int messageType, string message)
        {
            switch (messageType)
            {
                case 0:
                    this.Page.RegisterClientScriptBlock("jump", "<script language='JavaScript'>window.status='" + message.Replace("\"", "'").Replace("\r", "").Replace("\n", "") + "';</script>");
                    break;

                case 1:
                    this.Page.RegisterClientScriptBlock("jump", "<script language='JavaScript'>alert(\"" + message.Replace("\"", "'").Replace("\r", "").Replace("\n", "") + "\");</script>");
                    break;

                case 2:
                    this.Page.RegisterClientScriptBlock("jump", "<script language='JavaScript'>alert(\"" + message.Replace("\"", "'").Replace("\r", "").Replace("\n", "") + "\");</script>");
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
