using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Infragistics.WebUI.UltraWebGrid;
using System.Data;
using Resources;
using GDSBG.MiABU.Attendance.BLL.WorkFlow;
using GDSBG.MiABU.Attendance.BLL.KQM.KaoQinData;
using System.Web.Script.Serialization;
using System;
using System.Data.OleDb;

namespace GDSBG.MiABU.Attendance.Web.WorkFlow
{
    public partial class WorkFlowBillChangeAuditFlowForm : BasePage
    {

        JavaScriptSerializer JsSerializer = new JavaScriptSerializer();
        DataTable tmpImportDataTable = new DataTable();
        
        DataTable tempDataTable = new DataTable();
        DataTable importDataTable = new DataTable();
        WorkFlowCardMakeupBll cardMakeupBll = new WorkFlowCardMakeupBll();
        WorkFlowSetBll workflowset = new WorkFlowSetBll();
        KQMMoveShiftBll moveShitBll = new KQMMoveShiftBll();
        WorkFlowBillChangeAuditFlowBll billChangeAuditFlowBll = new WorkFlowBillChangeAuditFlowBll();
        
        protected void ddlAuditManType_OnInitializeDataSource(object sender, EventArgs e)
        {
            this.tempDataTable.Clear();
            this.tempDataTable = workflowset.GetDocNoTypeList("AuditTypeLeave");
            this.ddlAuditManType.DataSource = tempDataTable;
            this.ddlAuditManType.DataTextField = "DataValue";
            this.ddlAuditManType.DataValueField = "DataCode";
            this.ddlAuditManType.DataBind(); 
        }

        protected void ddlAuditType_OnInitializeDataSource(object sender, EventArgs e)
        { 
            this.tempDataTable.Clear();
            this.tempDataTable = workflowset.GetDocNoTypeList("AuditTypeMove");
            this.ddlAuditManType.DataSource = tempDataTable;
            this.ddlAuditManType.DataTextField = "DataValue";
            this.ddlAuditManType.DataValueField = "DataCode";
            this.ddlAuditManType.DataBind();
        }


        #region 頁面加載

        protected void Page_Load(object sender, EventArgs e)
        {
            
            try
            {
                if (!this.Page.IsPostBack)
                {
                    string BillNo = (Request.QueryString["BillNo"] == null) ? "" : Request.QueryString["BillNo"].ToString();
                    string ModuleCode = (Request.QueryString["ModuleCode"] == null) ? "" : Request.QueryString["ModuleCode"].ToString();
                    if (BillNo.ToString().Length > 0)
                    {
                        this.HiddenBillNo.Value = BillNo;
                        this.Btn_save.Attributes.Add("onclick", "return confirm('" + Message.SaveConfim + "')");
                        //this.ImageDepCode.Attributes.Add("onclick", string.Concat(new object[] { "javascript:GetTreeDataValue(\"textBoxDepCode\",\"Department\",\"\",\"WHERE companyid='", this.Session["companyID"], "' \",'", BasePage.sAppPath, "','", Request["moduleCode"], "','textBoxDepName')" }));
                      
                        //textBoxDepName.Text = CurrentUserInfo.DepName; 

                        SetSelector(ImageDepCode, textBoxDepCode, textBoxDepName);
                        textBoxDepName.Text = CurrentUserInfo.DepName;
                        // SetSelector(imgDepCode, txtOrgCode, tb_unit);
                       // tb_unit.Text = CurrentUserInfo.DepName;
                          Query(BillNo);
                    } 
                    else
                    {
                        Response.Write("<script type='text/javascript'>alert('" + Message.wfm_message_billdeleted + "');window.close();</script>");
                    }
                }
                return;
   
                 
            }
            catch (Exception ex)
            {
                WriteMessage(1, (ex.InnerException == null) ? ex.Message : ex.InnerException.Message);
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


        #region   新 增

        protected void ButtonAdd_Click(object sender, EventArgs e)
        {
            
            try
            {
                //獲取表單類型
                string BillTypeCode = cardMakeupBll.GetValue("Select BillTypeCode from gds_att_Bill where billno='" + this.HiddenBillNo.Value + "'");
                
                int index = this.UltraWebGridBill.Rows.Count;
                
                //查看未簽核狀態值
                string StatusName = cardMakeupBll.GetValue("select DataValue From gds_att_TypeData Where DataType='WFMBillStatus' and DataCode='0'");
                
                for (int i = 0; i < this.UltraWebGridAudit.Rows.Count; i++)
                {
                    if (!this.UltraWebGridAudit.Rows[i].Selected)
                    {
                        continue;
                    }
                    switch (BillTypeCode)
                    {
                        case "OrgMove":
                            break;

                        default:
                            for (int t = 0; t < this.UltraWebGridBill.Rows.Count; t++)
                            {
                                
                               // string s_s = this.UltraWebGridAudit.Rows[i].Cells.FromKey("AuditMan").Text.ToString();
                              

                                string s = this.UltraWebGridBill.Rows[t].Cells.FromKey("AuditMan").Text.ToString();

                                string s_s = "";
                                if (UltraWebGridAudit.Rows[i].Cells.FromKey("workno").Text != null)
                                {
                                    s_s = UltraWebGridAudit.Rows[i].Cells.FromKey("workno").Text.ToString(); 
                                }
                                
                                
                               //判斷人員是否重復
                                if (s == s_s)
                                {
                                    WriteMessage(1, Message.wfm_billauditflow_errorworkno);
                                    return;
                                }
                            }
                            break;
                    }
                    this.UltraWebGridBill.Rows.Add("AuditMan");
                    this.UltraWebGridBill.Rows[index].Cells.FromKey("AuditMan").Text = this.UltraWebGridAudit.Rows[i].Cells.FromKey("workno").Text;
                    this.UltraWebGridBill.Rows[index].Cells.FromKey("LocalName").Text = this.UltraWebGridAudit.Rows[i].Cells.FromKey("localname").Text;
                    this.UltraWebGridBill.Rows[index].Cells.FromKey("Notes").Text = this.UltraWebGridAudit.Rows[i].Cells.FromKey("notes").Text;
                    this.UltraWebGridBill.Rows[index].Cells.FromKey("ManagerName").Text = this.UltraWebGridAudit.Rows[i].Cells.FromKey("managername").Text;
                    this.UltraWebGridBill.Rows[index].Cells.FromKey("StatusName").Text = StatusName;
                    this.UltraWebGridBill.Rows[index].Cells.FromKey("SendNotes").Text = "N";
                    this.UltraWebGridBill.Rows[index].Cells.FromKey("AuditStatus").Text = "0";
                    this.UltraWebGridBill.Rows[index].Cells.FromKey("AuditType").Value = "";
                    this.UltraWebGridBill.Rows[index].Cells.FromKey("AuditManType").Value = "";
                    this.UltraWebGridAudit.Rows.RemoveAt(i);
                }
            }
            catch (Exception ex)
            {
                 WriteMessage(1, (ex.InnerException == null) ? ex.Message : ex.InnerException.Message);
            }
        }

        #endregion


        #region 查 詢
        /// <summary>
        /// 查詢
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonQuery_Click(object sender, EventArgs e)
        {
            string deptid = textBoxDepCode.Text;
            string empno = textBoxWorkNo.Text;
            string empname = textBoxLocalName.Text;
            if (deptid == string.Empty)
            {
                deptid = CurrentUserInfo.DepCode;
            }
            List<string> list = new List<string>();
            list.Add(deptid);
            DataTable dt = workflowset.GetDeptPerson(list, empno, empname);
            UltraWebGridAudit.DataSource = dt;
            UltraWebGridAudit.DataBind();
        }
         
        /// <summary>
        /// 查詢該表單單號所有簽核人
        /// </summary>
        /// <param name="BillNo">表單號</param>
        private void Query(string BillNo)
        {
            try
            {
                string condition = " and a.BillNo= '" + BillNo + "' ";
                /***權限管控
                // if (base.bPrivileged)
                // {
                //      condition = condition + " AND exists (SELECT 1 FROM (" + base.sqlDep + ") e where e.DepCode=c.orgcode)";
                // }
               ***/

               //this.dataSet = ((ServiceLocator)this.Session["serviceLocator"]).GetWFMBillAuditFlowData().GetAuditStatusDataByCondition(condition);

                this.tmpImportDataTable = billChangeAuditFlowBll.GetAuditStatusDataByCondition(condition);
                this.UltraWebGridBill.DataSource = tmpImportDataTable;
                this.UltraWebGridBill.DataBind();
            }
            catch (Exception ex)
            {
                WriteMessage(1, (ex.InnerException == null) ? ex.Message : ex.InnerException.Message);
            }
        }

        #endregion

        #region 刪 除
        /// <summary>
        /// 刪除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonDelete_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < this.UltraWebGridBill.Rows.Count; i++)
                {
                    if (this.UltraWebGridBill.Rows[i].Selected)
                    {
                        this.UltraWebGridBill.Rows[i].Delete();
                        break;
                    }
                }
                Page.ClientScript.RegisterStartupScript(this.GetType(), "show", "alert('" + Message.DeleteSuccess + "')", true); 
            }
            catch (Exception ex)
            {
                WriteMessage(1, (ex.InnerException == null) ? ex.Message : ex.InnerException.Message);
            }
        }
        #endregion 

        #region 存 儲

        protected void ButtonSave_Click(object sender, EventArgs e)
        {
             
            try
            {
                int t;
                int i;
                //獲取流程單據號
                string BillTypeCode = cardMakeupBll.GetValue("Select BillTypeCode from gds_att_bill where billno='" + this.HiddenBillNo.Value + "'");
                string AuditMan = "";
                string AuditType = "";
                string AuditManType = string.Empty;
                int intValueA = 0;
                switch (BillTypeCode)
                {
                    case "OrgMove":
                        t = 0;
                        goto Label_028C;

                    case "LeaveTypeAE":
                    case "LeaveTypeAF":
                    case "LeaveTypeAG":
                    case "LeaveTypeAH":
                    case "LeaveTypeAI":
                    case "LeaveTypeAJ":
                    case "LeaveTypeAK":
                    case "LeaveTypeM":
                    case "LeaveTypeN":
                        t = 0;
                        goto Label_035A;

                    default:
                        goto Label_037C;
                }
            Label_013D:
                //
                AuditType = (this.UltraWebGridBill.Rows[t].Cells.FromKey("AuditType").Value == null) ? "" : this.UltraWebGridBill.Rows[t].Cells.FromKey("AuditType").Value.ToString();
                if (string.IsNullOrEmpty(AuditType))
                {
                    WriteMessage(1, ControlText.auditmantype1 + Message.common_message_required);
                    return;
                }
                AuditType = this.UltraWebGridBill.Rows[t].Cells.FromKey("AuditType").Value.ToString();
                if (intValueA == 0)
                {
                    if (!AuditType.Equals("A"))
                    {
                        WriteMessage(1, Message.common_message_checkorgmovesenga);
                        return;
                    }
                    intValueA++;
                }
                else if (AuditType.Equals("A"))
                {
                    WriteMessage(1, Message.common_message_checkorgmovesengb);
                    return;
                }
                t++;
            Label_028C:
                if (t < this.UltraWebGridBill.Rows.Count)
                {
                    goto Label_013D;
                }
                goto Label_0448;
            Label_02B6:
                AuditManType = (this.UltraWebGridBill.Rows[t].Cells.FromKey("AuditManType").Value == null) ? "" : this.UltraWebGridBill.Rows[t].Cells.FromKey("AuditManType").Value.ToString();
                if (string.IsNullOrEmpty(AuditManType))
                {
                    WriteMessage(1, ControlText.auditmantype1 + Message.common_message_required);
                    return;
                }
                t++;
            Label_035A:
                if (t < this.UltraWebGridBill.Rows.Count)
                {
                    goto Label_02B6;
                }
                goto Label_0448;
            Label_037C:
                if (this.UltraWebGridBill.Rows.Count > 0)
                {
                    for (i = 0; i < this.UltraWebGridBill.Rows.Count; i++)
                    {
                        AuditMan = AuditMan + this.UltraWebGridBill.Rows[i].Cells.FromKey("AuditMan").Text.ToString() + ",";
                    }
                    AuditMan = AuditMan.TrimEnd(new char[] { ',' });
                }
             /**   if ((BillTypeCode.Length > 0) && !base.CheckWFMAuditLimit(AuditMan, BillTypeCode))
                {
                    return;
                }
              * **/
            Label_0448:
                this.tempDataTable = billChangeAuditFlowBll.GetAuditStatusDataByCondition(" and 1=2");
                if (this.UltraWebGridBill.Rows.Count > 0)
                {
                    for (i = 0; i < this.UltraWebGridBill.Rows.Count; i++)
                    {
                        if (this.UltraWebGridBill.Rows[i].Cells.FromKey("AuditStatus").Text.Equals("0"))
                        {
                            DataRow row = this.tempDataTable.NewRow();
                            row.BeginEdit();
                            row["BillNo"] = this.HiddenBillNo.Value;
                            row["AuditMan"] = this.UltraWebGridBill.Rows[i].Cells.FromKey("AuditMan").Text;
                            row["OrderNo"] = i + 1;
                            row["SendNotes"] = this.UltraWebGridBill.Rows[i].Cells.FromKey("SendNotes").Text;
                            row["AuditType"] = (this.UltraWebGridBill.Rows[i].Cells.FromKey("AuditType").Value == null) ? "" : this.UltraWebGridBill.Rows[i].Cells.FromKey("AuditType").Value.ToString();
                            row["AuditManType"] = (this.UltraWebGridBill.Rows[i].Cells.FromKey("AuditManType").Value == null) ? "" : this.UltraWebGridBill.Rows[i].Cells.FromKey("AuditManType").Value.ToString();
                            row.EndEdit();
                            this.tempDataTable.Rows.Add(row);
                            this.tempDataTable.AcceptChanges();
                        }
                    }
                    if (this.tempDataTable.Rows.Count > 0)
                    {
                        billChangeAuditFlowBll.SaveChangeAuditData(this.HiddenBillNo.Value, this.tempDataTable);
                        base.Response.Write("<script type='text/javascript'>alert('" + Message.savecomplete + "');window.opener.document.all.ButtonQuery.click();window.close();</script>");
                    }
                    else
                    {
                        WriteMessage(1, Message.common_message_checksavechangeauditflow);
                    }
                }
            }
            catch (Exception ex)
            {
                WriteMessage(1, (ex.InnerException == null) ? ex.Message : ex.InnerException.Message);
            }
              
        }

        #endregion

        private void Internationalization()
        {
            /**
            this.labelDepName.Text = ControlText.gvDepName;
            this.labelWorkNo.Text = ControlText.WorkNo;
            this.labelLocalName.Text = ControlText.gvgvLOCALNAME;
           // this.ButtonAdd.Text = ControlText.Btn_Add;
          // /
          //  this.ButtonDelete.Text = ControlText.Btn_delete;
           // this.Btn_save.Text = ControlText.ButtonSave;
           // this.ButtonQuery.Text = ControlText.Btn_select;
            string BillTypeCode = cardMakeupBll.GetValue("Select BillTypeCode from gds_att_bill where billno='" + this.HiddenBillNo.Value + "'");
            this.UltraWebGridBill.Bands[0].Columns.FromKey("AuditMan").Header.Caption = ControlText.gvgvWorkNo;
            this.UltraWebGridBill.Bands[0].Columns.FromKey("LocalName").Header.Caption = ControlText.gvHeadApproverName;
            this.UltraWebGridBill.Bands[0].Columns.FromKey("Notes").Header.Caption = "Notes";
            this.UltraWebGridBill.Bands[0].Columns.FromKey("ManagerName").Header.Caption = ControlText.gvHeadmanager;
            this.UltraWebGridBill.Bands[0].Columns.FromKey("StatusName").Header.Caption =ControlText.gvHeadStatus;
            this.UltraWebGridBill.Bands[0].Columns.FromKey("SendNotes").Header.Caption = ControlText.ButtonSendNotes;
            this.UltraWebGridBill.Bands[0].Columns.FromKey("AuditType").Header.Caption = ControlText.auditmantype1;
            this.UltraWebGridBill.Bands[0].Columns.FromKey("AuditManType").Header.Caption = ControlText.auditmantype1;  //--簽核人類型

           
            this.UltraWebGridAudit.Bands[0].Columns.FromKey("DepName").Header.Caption = ControlText.gvDName;
            this.UltraWebGridAudit.Bands[0].Columns.FromKey("AuditMan").Header.Caption = ControlText.gvgvWorkNo;
            this.UltraWebGridAudit.Bands[0].Columns.FromKey("LocalName").Header.Caption = ControlText.gvHeadApproverName;
            this.UltraWebGridAudit.Bands[0].Columns.FromKey("Notes").Header.Caption = "Notes";
            this.UltraWebGridAudit.Bands[0].Columns.FromKey("ManagerName").Header.Caption = ControlText.gvHeadmanager;

            switch (BillTypeCode)
            {
                case "OrgMove":
                    this.UltraWebGridBill.Bands[0].Columns.FromKey("AuditManType").Hidden = true;
                    this.UltraWebGridBill.DisplayLayout.AllowUpdateDefault = AllowUpdate.Yes;
                    this.UltraWebGridBill.DisplayLayout.CellClickActionDefault = CellClickAction.Edit;
                    break;

                case "LeaveTypeAE":
                case "LeaveTypeAF":
                case "LeaveTypeAG":
                case "LeaveTypeAH":
                case "LeaveTypeAI":
                case "LeaveTypeAJ":
                case "LeaveTypeAK":
                case "LeaveTypeM":
                case "LeaveTypeN":
                    this.UltraWebGridBill.Bands[0].Columns.FromKey("AuditType").Hidden = true;
                    this.UltraWebGridBill.DisplayLayout.AllowUpdateDefault = AllowUpdate.Yes;
                    this.UltraWebGridBill.DisplayLayout.CellClickActionDefault = CellClickAction.Edit;
                    break;

                default:
                    this.UltraWebGridBill.Bands[0].Columns.FromKey("AuditType").Hidden = true;
                    this.UltraWebGridBill.Bands[0].Columns.FromKey("AuditManType").Hidden = true;
                    this.UltraWebGridBill.Bands[0].Columns.FromKey("Notes").Width = Unit.Percentage(35.0);
                    this.UltraWebGridBill.Bands[0].Columns.FromKey("ManagerName").Width = Unit.Percentage(25.0);

                    this.UltraWebGridBill.DisplayLayout.AllowUpdateDefault = AllowUpdate.Yes;
                    this.UltraWebGridBill.DisplayLayout.CellClickActionDefault = CellClickAction.Edit;
                    break;
             
            }* **/
        }
 
    }

}
