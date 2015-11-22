/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： KQMEvectionApplyForm.cs
 * 檔功能描述： 外出申請UI層
 * 
 * 版本：1.0
 * 創建標識： 高子焱 2012.02.16
 * 
 */
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Serialization;
using System.Collections.Generic;
using Resources;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;
using GDSBG.MiABU.Attendance.BLL.KQM.BasicData;
using System.Drawing;
using GDSBG.MiABU.Attendance.Model.KQM.KaoQinData;
using GDSBG.MiABU.Attendance.BLL.KQM.KaoQinData;
using Infragistics.WebUI.UltraWebGrid;
using System.IO;

namespace GDSBG.MiABU.Attendance.Web.Hr.KQM.KaoQinData
{
    public partial class KQMEvectionApplyForm : BasePage
    {
        static DataTable dt = new DataTable();
        static SynclogModel logmodel = new SynclogModel();
        TypeDataBll bllTypeData = new TypeDataBll();
        DataTable tempDataTable = new DataTable();
        KQMEvectionApplyModel model = new KQMEvectionApplyModel();
        KQMEvectionApplyBll bll = new KQMEvectionApplyBll();
        int totalCount;
        #region 頁面加載
        protected void Page_Load(object sender, EventArgs e)
        {
            JavaScriptSerializer JsSerializer = new JavaScriptSerializer();
            Dictionary<string, string> ClientMessage = null;
            SetCalendar(txtEvectionTime);
            txtApproveDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
            PageHelper.ButtonControls(FuncList, this.pnlShowPanel.Controls, base.FuncListModule);
            HiddenModuleCode.Value = Request.QueryString["ModuleCode"].ToString();
            if (!this.PanelImport.Visible)
            {
                this.btnImport.Text = Message.btnImport;
            }
            else
            {
                this.btnImport.Text = Message.btnBack;
            }
            if (!IsPostBack)
            {
                logmodel.ProcessOwner = CurrentUserInfo.Personcode;
                logmodel.TransactionType = Request.QueryString["ModuleCode"];
                logmodel.LevelNo = "2";
                logmodel.FromHost = Request.UserHostAddress;
                SetSelector(imgDepCode, txtDCode, txtDepName, Request.QueryString["ModuleCode"].ToString());
                this.DropDownListBind(this.ddlEvectionType, "EvectionTypeOut");
                this.DropDownListBind(this.ddlStatus, "BillAuditState");
                DataUIBind();
            }
            if (ClientMessage == null)
            {
                ClientMessage = new Dictionary<string, string>();
                ClientMessage.Add("AtLastOneChoose", Message.AtLastOneChoose);
                ClientMessage.Add("DeleteFailed", Message.DeleteFailed);
                ClientMessage.Add("DeleteSuccess", Message.DeleteSuccess);
                ClientMessage.Add("TextBoxNotNull", Message.TextBoxNotNull);
                ClientMessage.Add("DeleteConfirm", Message.DeleteConfirm);
                ClientMessage.Add("btnImport", Message.btnImport);
                ClientMessage.Add("btnBack", Message.btnBack);
                ClientMessage.Add("UnAudit", Message.UnAudit);
                ClientMessage.Add("UnSendAudit", Message.UnSendAudit);
                ClientMessage.Add("NoSelect", Message.NoSelect);
                ClientMessage.Add("ConfirmReturn", Message.ConfirmReturn);
                ClientMessage.Add("UnCancelAudit", Message.UnCancelAudit);
                ClientMessage.Add("OnlyNoAudit", Message.OnlyNoAudit);
                ClientMessage.Add("OnlyNoCanModify", Message.OnlyNoCanModify);
                ClientMessage.Add("ErrReturnTimeWrong", Message.ErrReturnTimeWrong);
                ClientMessage.Add("WrongDate", Message.WrongDate);
                ClientMessage.Add("uploading", Message.uploading);
            }
            string clientmsg = JsSerializer.Serialize(ClientMessage);
            Page.ClientScript.RegisterStartupScript(GetType(), "ClientMessage", "var Message=" + clientmsg + ";", true);
        }
        #endregion
        #region 綁定下拉框的值
        protected void DropDownListBind(DropDownList List, string DataTypeValue)
        {
            DataTable dt = new DataTable();
            dt = bllTypeData.GetTypeDataList(DataTypeValue);
            List.DataSource = dt;
            List.DataTextField = "DataValue";
            List.DataValueField = "DataCode";
            List.DataBind();
            List.Items.Insert(0, new ListItem("", ""));
        }
        #endregion
        #region 放大鏡綁定
        /// <summary>
        /// 設置Selector
        /// </summary>
        /// <param name="ctrlTrigger">控件ID--按鈕</param>
        /// <param name="ctrlCode">控件ID--文本框1</param>
        /// <param name="ctrlName">控件ID--文本框2</param>
        public void SetSelector(WebControl ctrlTrigger, Control ctrlCode, Control ctrlName, string moduleCode)
        {
            if (ctrlCode is TextBox) { (ctrlCode as TextBox).Attributes.Add("readonly", "readonly"); }
            if (ctrlName is TextBox) { (ctrlName as TextBox).Attributes.Add("readonly", "readonly"); }
            ctrlTrigger.Attributes.Add("onclick", string.Format("return setSelector('{0}','{1}','{2}')",
                ctrlCode.ClientID, ctrlName.ClientID, moduleCode));
        }
        #endregion
        # region webgrid 行數據綁定
        protected void UltraWebGridEvectionApply_DataBound(object sender, EventArgs e)
        {
            for (int i = 0; i < this.UltraWebGridEvectionApply.Rows.Count; i++)
            {
                if (!string.IsNullOrEmpty(this.UltraWebGridEvectionApply.Rows[i].Cells.FromKey("Status").Text))
                {
                    string status = this.UltraWebGridEvectionApply.Rows[i].Cells.FromKey("Status").Text.Trim();
                    if (!(status == "0"))
                    {
                        if (status == "1")
                        {
                            goto Label_00BC;
                        }
                        if (status == "3")
                        {
                            goto Label_00F1;
                        }
                        if (status == "4")
                        {
                            goto Label_0126;
                        }
                        goto Label_015B;
                    }
                }
                else
                {

                    goto Label_015B;

                }
                this.UltraWebGridEvectionApply.Rows[i].Cells.FromKey("StatusName").Style.ForeColor = Color.Green;
                continue;
            Label_00BC:
                this.UltraWebGridEvectionApply.Rows[i].Cells.FromKey("StatusName").Style.ForeColor = Color.Blue;
                continue;
            Label_00F1:
                this.UltraWebGridEvectionApply.Rows[i].Cells.FromKey("StatusName").Style.ForeColor = Color.Maroon;
                continue;
            Label_0126:
                this.UltraWebGridEvectionApply.Rows[i].Cells.FromKey("StatusName").Style.ForeColor = Color.Red;
                continue;
            Label_015B:
                this.UltraWebGridEvectionApply.Rows[i].Cells.FromKey("StatusName").Style.ForeColor = Color.Black;
            }
        }
        #endregion
        #region 核准的保存按鈕
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (this.UltraWebGridEvectionApply.Rows.Count != 0)
            {
                int i;
                int intDeleteOk = 0;
                int intDeleteError = 0;
                TemplatedColumn tcol = (TemplatedColumn)this.UltraWebGridEvectionApply.Bands[0].Columns[0];
                for (i = 0; i < this.UltraWebGridEvectionApply.Rows.Count; i++)
                {
                    if (((tcol.CellItems[i] as CellItem).FindControl("CheckBoxCell") as CheckBox).Checked)
                    {
                        string ID = this.UltraWebGridEvectionApply.Rows[i].Cells.FromKey("ID").Text.Trim();
                        string Status = "2";
                        model.ApproveDate = Convert.ToDateTime(txtApproveDate.Text.ToString());
                        model.Approver = txtApprover.Text.ToString();
                        int flag = bll.UpdateEvction(ID, Status, model, logmodel);
                        if (flag == 1)
                        {
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
                    string alertText = Message.AuditSucccess + "：" + intDeleteOk + "," + Message.AuditSucccess + "：" + intDeleteError;
                    Page.ClientScript.RegisterStartupScript(GetType(), "show", "alert('" + alertText + "')", true);
                }
                model = new KQMEvectionApplyModel();
                DataUIBind();
                this.ProcessFlag.Value = "";
            }
        }
        #endregion

        #region 刪除
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            int i;
            int intDeleteOk = 0;
            int intDeleteError = 0;
            TemplatedColumn tcol = (TemplatedColumn)this.UltraWebGridEvectionApply.Bands[0].Columns[0];
            for (i = 0; i < this.UltraWebGridEvectionApply.Rows.Count; i++)
            {
                CellItem GridItem = (CellItem)tcol.CellItems[i];
                CheckBox chkIsHaveRight = (CheckBox)GridItem.FindControl("CheckBoxCell");
                if (chkIsHaveRight.Checked)
                {
                    logmodel.ProcessFlag = "delete";
                    model.ID = this.UltraWebGridEvectionApply.Rows[i].Cells.FromKey("ID").Text.Trim();
                    int flag = bll.DeleteEevctionByKey(model, logmodel);
                    if (flag == 1)
                    {
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
                string alertText = Message.DeleteSuccess + "：" + intDeleteOk + "," + Message.DeleteFailed + "：" + intDeleteError;
                Page.ClientScript.RegisterStartupScript(GetType(), "show", "alert('" + alertText + "')", true);
            }
            pager.CurrentPageIndex = 1;
            model = new KQMEvectionApplyModel();
            DataUIBind();
        }
        #endregion
        #region  導入按鈕 設置panel間的轉換
        protected void btnImport_Click(object sender, EventArgs e)
        {
            this.UltraWebGridImport.Rows.Clear();
            if (!this.PanelImport.Visible)
            {
                this.PanelImport.Visible = true;
                this.PanelData.Visible = false;
                this.btnQuery.Enabled = false;
                this.btnReset.Enabled = false;
                this.btnDelete.Enabled = false;
                this.btnAdd.Enabled = false;
                this.btnModify.Enabled = false;
                this.btnAudit.Enabled = false;
                this.btnSendAudit.Enabled = false;
                //this.btnOrgAudit.Enabled = false;
                this.btnCancelAudit.Enabled = false;
                this.btnImport.Text = Message.btnBack;
                this.lbluploadMsg.Text = "";
                ImportFlag.Value = "2";
            }
            else
            {
                this.PanelImport.Visible = false;
                this.PanelData.Visible = true;
                this.btnQuery.Enabled = true;
                this.btnReset.Enabled = true;
                this.btnDelete.Enabled = true;
                this.btnAdd.Enabled = true;
                this.btnModify.Enabled = true;
                this.btnAudit.Enabled = true;
                this.btnSendAudit.Enabled = true;
                // this.btnOrgAudit.Enabled = true;
                this.btnCancelAudit.Enabled = true;
                this.btnImport.Text = Message.btnImport;
                this.lbluploadMsg.Text = "";
                ImportFlag.Value = "1";
            }
        }
        #endregion
        #region  上傳
        protected void btnImportSave_Click(object sender, EventArgs e)
        {
            int succesnum = 0;
            int errornum = 0;
            int flag = ImportModuleExcel();
            if (flag == 1)
            {
                logmodel.ProcessFlag = "insert";
                dt = bll.ImpoertExcel(CurrentUserInfo.Personcode, out succesnum, out errornum, logmodel);
                lblupload.Text = Message.NumberOfSuccessed + succesnum + Message.NumberOfFailed + errornum;
                dt = changeError(dt);
                this.UltraWebGridImport.DataSource = dt;
                this.UltraWebGridImport.DataBind();
            }
            else if (flag == 0)
            {
                lblupload.Text = Message.FailToSaveData;
            }
            else if (flag == -1)
            {
                lblupload.Text = Message.DataFormatError;
            }
            else if (flag == -3)
            {
                lblupload.Text = Message.IsNotExcel;
            }
            btnImport.Text = Message.btnBack;
            ImportFlag.Value = "2";
        }
        #endregion
        #region 導出報表
        protected void btnExport_Click(object sender, EventArgs e)
        {
            if (ImportFlag.Value == "2")
            {
                if (dt.Rows.Count != 0)
                {
                    model = PageHelper.GetModel<KQMEvectionApplyModel>(pnlContent.Controls);
                    List<KQMEvectionApplyModel> list = bll.GetList(dt);
                    string[] header = { ControlText.gvErrorMsg, ControlText.gvWorkNo,ControlText.gvEvectionTypeName, ControlText.gvEvectionReason, ControlText.gvEvectionTime,
                                       ControlText.gvEvectionTel, ControlText.gvEvectionAddress, ControlText.gvEvectionTask,ControlText.gvEvectionRoad,
                                       ControlText.gvReturnTime, ControlText.gvEvectionBy,ControlText.gvRemark, };
                    string[] properties = { "ErrorMsg","WorkNo","EvectionTypeName", "EvectionReason", "EvectionTime", 
                                             "EvectionTel", "EvectionAddress", "EvectionTask", "EvectionRoad", "ReturnTime", "EvectionBy","Remark"};
                    string filePath = MapPath("~/ExportFileTemp/") + DateTime.Now.Ticks + ".xls";
                    NPOIHelper.ExportExcel(list, header, properties, 5000, filePath, true);
                    PageHelper.ReturnHTTPStream(filePath, true);
                }
                else
                {
                    lblupload.Text = Message.NoDataExport;
                }
            }
            else
            {
                if (dt.Rows.Count != 0)
                {
                    model = PageHelper.GetModel<KQMEvectionApplyModel>(pnlContent.Controls);
                    dt = bll.GetEvectionList(model);
                    List<KQMEvectionApplyModel> list = bll.GetList(dt);
                    string[] header = {ControlText.gvHeadOrderID, ControlText.gvBillNo, ControlText.gvWorkNo, ControlText.gvLocalName, 
                                       ControlText.gvDepName,ControlText.gvEvectionTypeName, ControlText.gvEvectionReason, ControlText.gvEvectionTime,
                                       ControlText.gvEvectionTel, ControlText.gvEvectionAddress, ControlText.gvEvectionTask,ControlText.gvEvectionRoad,
                                       ControlText.gvReturnTime, ControlText.gvEvectionBy, ControlText.gvMotorMan, ControlText.gvRemark,ControlText.gvStatusName,
                                       ControlText.gvApprover, ControlText.gvApproverDate,ControlText.gvAuditer,ControlText.gvAuditDate,ControlText.gvAuditIdea,
                                       ControlText.gvCreateUser,ControlText.gvCreateDate};
                    string[] properties = { "ID", "BillNo", "WorkNo", "LocalName", "DepName", "EvectionTypeName", "EvectionReason", "EvectionTime", 
                                             "EvectionTel", "EvectionAddress", "EvectionTask", "EvectionRoad", "ReturnTime", "EvectionBy", "MotorMan",
                                             "Remark", "StatusName", "Approver", "ApproverDate", "Auditer", "AuditDate", "AuditIdea","CreateUser", "CreateDate"};
                    string filePath = MapPath("~/ExportFileTemp/") + DateTime.Now.Ticks + ".xls";
                    NPOIHelper.ExportExcel(list, header, properties, 5000, filePath, true);
                    PageHelper.ReturnHTTPStream(filePath, true);
                }
                else
                {
                    lblupload.Text = Message.NoDataExport;
                }
            }
        }
        #endregion
        #region 分頁
        protected void pager_PageChanged(object sender, EventArgs e)
        {
            model = PageHelper.GetModel<KQMEvectionApplyModel>(pnlContent.Controls);
            DataUIBind();
        }
        #endregion
        #region   取消核准
        protected void btnCancelAudit_Click(object sender, EventArgs e)
        {
            if (this.UltraWebGridEvectionApply.Rows.Count != 0)
            {
                int i;
                int intDeleteOk = 0;
                int intDeleteError = 0;
                CellItem GridItem;
                CheckBox chkIsHaveRight;
                TemplatedColumn tcol = (TemplatedColumn)this.UltraWebGridEvectionApply.Bands[0].Columns[0];
                for (i = 0; i < this.UltraWebGridEvectionApply.Rows.Count; i++)
                {
                    GridItem = (CellItem)tcol.CellItems[i];
                    chkIsHaveRight = (CheckBox)GridItem.FindControl("CheckBoxCell");
                    if (chkIsHaveRight.Checked)
                    {
                        string ID = this.UltraWebGridEvectionApply.Rows[i].Cells.FromKey("ID").Text.Trim();
                        string Status = "0";
                        int flag = bll.UpdateEvction(ID, Status, logmodel);
                        if (flag == 1)
                        {
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
                    string alertText = Message.CancelAuditSucccess + "：" + intDeleteOk + "," + Message.CancelAuditFaile + "：" + intDeleteError;
                    Page.ClientScript.RegisterStartupScript(GetType(), "show", "alert('" + alertText + "')", true);
                }
                model = new KQMEvectionApplyModel();
                DataUIBind();
                this.ProcessFlag.Value = "";
            }
        }
        #endregion

        #region 送簽
        protected void btnSendAudit_Click(object sender, EventArgs e)
        {
            //int intSendOK = 0;
            //int intSendBillNo = 0;
            //int intSendError = 0;
            //string OTDate = "";
            //string OrgCode = "", BillNo = "", AuditOrgCode = "", BillNoType = "KQT";
            //string alertText;
            //string sFlow_LevelRemark = Message.flow_levelremark;
            //string BillTypeCode = "KQMApplyOut";
            //TemplatedColumn tcol = (TemplatedColumn)this.UltraWebGridEvectionApply.Bands[0].Columns[0];
            //for (int i = 0; i < UltraWebGridEvectionApply.Rows.Count; i++)
            //{
            //    CellItem GridItem = (CellItem)tcol.CellItems[i];
            //    CheckBox chkIsHaveRight = (CheckBox)(GridItem.FindControl("CheckBoxCell"));
            //    if (chkIsHaveRight.Checked)
            //    {
            //        BillNo = "";
            //        OrgCode = String.IsNullOrEmpty(this.HiddenOrgCode.Value) ? UltraWebGridEvectionApply.Rows[i].Cells.FromKey("dCode").Text.Trim() : this.HiddenOrgCode.Value.Trim();

            //        AuditOrgCode = bll.GetWorkFlowOrgCode(OrgCode, BillTypeCode);

            //        if (AuditOrgCode.Length > 0)
            //        {
            //            string workNo = UltraWebGridEvectionApply.Rows[i].Cells.FromKey("WorkNo").Text.Trim();
            //            string id = UltraWebGridEvectionApply.Rows[i].Cells.FromKey("ID").Text.Trim();
            //            dt = bll.GetDtByID(id);
            //            if (dt.Rows.Count > 0)
            //            {
            //                BillNo = dt.Rows[0]["BILLNO"].ToString();
            //                //if (!string.IsNullOrEmpty(BillNo))
            //                //{
            //                //    //更新OTM_AdvanceApply表
            //                //    bll.SaveAuditData("Modify", id, workNo,BillNo, AuditOrgCode, BillTypeCode, CurrentUserInfo.Personcode,logmodel);
            //                //    intSendOK += 1;
            //                //}
            //                //else
            //                //{
            //                //生產單號，更新OTM_AdvanceApply表
            //                BillNo = bll.SaveAuditData(id, workNo, BillNoType, BillTypeCode, CurrentUserInfo.Personcode, AuditOrgCode, sFlow_LevelRemark, logmodel);
            //                intSendBillNo += 1;
            //                intSendOK += 1;
            //                //}
            //            }
            //        }
            //        else
            //        {
            //            //沒有定義簽核流程
            //            intSendError += 1;
            //        }
            //    }
            //}
            //if (intSendOK + intSendError > 0)
            //{
            //    if (intSendError > 0)
            //    {
            //        alertText = Message.SuccCount + intSendOK + ";" + Message.common_message_billcount + "：" + intSendBillNo + ";" + Message.FaileCount + intSendError + "(沒有定義簽核流程)";
            //        Page.ClientScript.RegisterStartupScript(GetType(), "show", "alert('" + alertText + "')", true);
            //    }
            //    else
            //    {
            //        alertText = Message.SuccCount + intSendOK + ";" + Message.common_message_billcount + "：" + intSendBillNo;
            //        Page.ClientScript.RegisterStartupScript(GetType(), "show", "alert('" + alertText + "')", true);
            //    }
            //}
            //else
            //{
            //    Page.ClientScript.RegisterStartupScript(GetType(), "show", "alert('" + Message.AtLastOneChoose + "')", true);
            //    return;
            //}
            //DataUIBind();
            //this.ProcessFlag.Value = "";
            //this.HiddenOrgCode.Value = "";
            try
            {
                int intSendOK = 0;
                int intSendBillNo = 0;
                int intSendError = 0;
                string OrgCode = "", alertText, AuditOrgCode = "", BillNoType = "KQT";
                string sFlow_LevelRemark = Message.flow_levelremark;
                TemplatedColumn tcol = (TemplatedColumn)UltraWebGridEvectionApply.Bands[0].Columns[0];
                string BillTypeCode = "KQMApplyOut";
                Dictionary<string, List<string>> dicy = new Dictionary<string, List<string>>();
                int countRow = UltraWebGridEvectionApply.Rows.Count;
                for (int i = 0; i < countRow; i++)
                {
                    CellItem GridItem = (CellItem)tcol.CellItems[i];
                    CheckBox chkIsHaveRight = (CheckBox)(GridItem.FindControl("CheckBoxCell"));
                    if (chkIsHaveRight.Checked)
                    {
                        OrgCode = UltraWebGridEvectionApply.Rows[i].Cells.FromKey("dcode").Text.Trim();
                        AuditOrgCode = bll.GetWorkFlowOrgCode(OrgCode, BillTypeCode);
                        string key = AuditOrgCode;
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
                for (int i = 0; i < countRow; i++)
                {
                    CellItem GridItem = (CellItem)tcol.CellItems[i];
                    CheckBox chkIsHaveRight = (CheckBox)(GridItem.FindControl("CheckBoxCell"));
                    if (chkIsHaveRight.Checked)
                    {
                        OrgCode = UltraWebGridEvectionApply.Rows[i].Cells.FromKey("dcode").Text.Trim();
                        AuditOrgCode = bll.GetWorkFlowOrgCode(OrgCode, BillTypeCode);
                        string key = AuditOrgCode;
                        if (!string.IsNullOrEmpty(key))
                        {
                            if (dicy[key] != null)
                            {
                                dicy[key].Add(UltraWebGridEvectionApply.Rows[i].Cells.FromKey("ID").Text.Trim());
                            }
                        }
                    }
                }
                //生產單號，更新OTM_AdvanceApply表(申請人）
                int count = bll.SaveOrgAuditData("Add", dicy, BillNoType, BillTypeCode, sFlow_LevelRemark, CurrentUserInfo.Personcode, logmodel);
                intSendBillNo = count;
                if (count > 0)
                {
                    intSendOK += 1;
                }
                if (intSendOK + intSendError > 0)
                {
                    if (intSendError > 0)
                    {
                        alertText = Message.SuccCount + intSendOK + ";" + Message.common_message_billcount + "：" + intSendBillNo + ";" + Message.FaileCount + intSendError + "(沒有定義簽核流程)";
                        Page.ClientScript.RegisterStartupScript(GetType(), "show", "alert('" + alertText + "')", true);
                    }
                    else
                    {
                        alertText = Message.SuccCount + intSendOK + ";" + Message.common_message_billcount + "：" + intSendBillNo;
                        Page.ClientScript.RegisterStartupScript(GetType(), "show", "alert('" + alertText + "')", true);
                    }
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "show", "alert('" + Message.AtLastOneChoose + "')", true);
                    return;
                }
                DataUIBind();
                this.ProcessFlag.Value = "";
                this.HiddenOrgCode.Value = "";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region 查詢按鈕
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            model = PageHelper.GetModel<KQMEvectionApplyModel>(pnlContent.Controls);
            pager.CurrentPageIndex = 1;
            DataUIBind();
            hidOperate.Value = "";
        }
        #endregion
        #region 數據綁定
        private void DataUIBind()
        {
            string sqlDep = base.SqlDep;
            string depCode = model.DCode;
            model.DCode = null;
            model.DepName = null;
            DataTable dtSelectAll = bll.GetEvectionList(model, depCode, sqlDep, pager.CurrentPageIndex, pager.PageSize, out totalCount);
            dt = bll.GetEvectionList(model, depCode, sqlDep);
            pager.RecordCount = totalCount;
            UltraWebGridEvectionApply.DataSource = dtSelectAll;
            UltraWebGridEvectionApply.DataBind();
            pager.TextAfterPageIndexBox = "/" + pager.PageCount.ToString();
        }
        #endregion


        #region 將文件中的數據導入到數據庫中
        /// <summary>
        /// 將文件中的數據導入到數據庫中
        /// </summary>
        private int ImportModuleExcel()
        {
            int flag;
            bool deFlag;
            string tableName = "gds_att_applyout_temp";
            string[] columnProperties = { "WorkNo", "EvectionType", "EvectionReason","EvectionTime","EvectionTel", "EvectionAddress",
                                          "EvectionObject","EvectionTask", "EvectionRoad", "ReturnTime", "EvectionBy","Remark"};
            string[] columnType = { "varchar", "varchar", "varchar", "varchar", "varchar", "varchar", "varchar", "varchar", "varchar", "varchar", "varchar", "varchar" };
            string createUser = CurrentUserInfo.Personcode;
            deFlag = NPOIHelper.DeleteExcelSql(tableName, createUser);
            string filePath = GetImpFileName();
            if (string.IsNullOrEmpty(filePath))
                return -2;

            flag = NPOIHelper.ImportExcel(columnProperties, columnType, filePath, tableName, createUser);
            return flag;
        }
        #endregion

        #region 轉換錯誤信息
        /// <summary>
        /// 轉換錯誤信息
        /// </summary>
        private DataTable changeError(DataTable dt)
        {
            if (dt != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string errorInfo = "";
                    for (int j = 0; j < dt.Rows[i]["ErrorMsg"].ToString().Split('§').Length; j++)
                    {
                        switch (dt.Rows[i]["ErrorMsg"].ToString().Split('§')[j].ToString().Trim())
                        {
                            case "ErrWorkNoNull": errorInfo = errorInfo + Message.ErrorWorkNoNull + ";";
                                break;
                            case "WorkNoIsWrong": errorInfo = errorInfo + Message.WorkNoIsWrong + ";";
                                break;
                            case "ErrEvectionTypeNull": errorInfo = errorInfo + Message.ErrEvectionTypeNull + ";";
                                break;
                            case "ErrEvectionReasonNull": errorInfo = errorInfo + Message.ErrEvectionReasonNull + ";";
                                break;
                            case "ErrEvectionTimeNull": errorInfo = errorInfo + Message.ErrEvectionTimeNull + ";";
                                break;
                            case "ErrReturnTimeNull": errorInfo = errorInfo + Message.ErrReturnTimeNull + ";";
                                break;
                            case "ErrEvectionType": errorInfo = errorInfo + Message.ErrEvectionType + ";";
                                break;
                            case "ErrEvectionBy": errorInfo = errorInfo + Message.ErrEvectionBy + ";";
                                break;
                            case "ErrEvectionTime": errorInfo = errorInfo + Message.ErrEvectionTime + ";";
                                break;
                            case "ErrReturnTime": errorInfo = errorInfo + Message.ErrReturnTime + ";";
                                break;
                            case "ErrEvectionByIn": errorInfo = errorInfo + Message.OnlySelectWalk + ";";
                                break;
                            case "ErrEvectionByOut": errorInfo = errorInfo + Message.OnlySelectByCar + ";";
                                break;
                            case "ErrReturnTimeWrong": errorInfo = errorInfo + Message.ErrReturnTimeWrong + ";";
                                break;
                            default: break;
                        }
                    }
                    dt.Rows[i]["ErrorMsg"] = errorInfo;
                }
            }
            return dt;
        }
        #endregion

        #region 獲取上傳文件名,并將文件保存到服務器
        /// <summary>
        /// 獲取導入的文件名,并將文件保存到服務器
        /// </summary>
        /// <returns></returns>
        private string GetImpFileName()
        {
            string filePath = "";
            if (FileUpload.FileName.Trim() != "")
            {
                try
                {
                    filePath = MapPath("~/ExportFileTemp/") + DateTime.Now.ToString("yyyyMMddHHmmss") + Path.GetFileName(FileUpload.FileName);
                    FileUpload.SaveAs(filePath);

                }
                catch
                {
                    lblupload.Text = Message.FailToUpload;
                }
            }
            else
            {
                lblupload.Text = Message.PathIsNull;
            }

            return filePath;
        }
        #endregion

        //#region  組織送簽
        //protected void btnOrgAudit_Click(object sender, EventArgs e)
        //{
        //    //try
        //    //{
        //    //    int intSendOK = 0;
        //    //    int intSendBillNo = 0;
        //    //    int intSendError = 0;
        //    //    string OrgCode = "", alertText, AuditOrgCode = "", BillNoType = "KQT";
        //    //    string sFlow_LevelRemark = Message.flow_levelremark;
        //    //    TemplatedColumn tcol = (TemplatedColumn)UltraWebGridEvectionApply.Bands[0].Columns[0];
        //    //    string BillTypeCode = "KQMApplyOut";
        //    //    Dictionary<string, List<string>> dicy = new Dictionary<string, List<string>>();
        //    //    int countRow = UltraWebGridEvectionApply.Rows.Count;
        //    //    for (int i = 0; i < countRow; i++)
        //    //    {
        //    //        CellItem GridItem = (CellItem)tcol.CellItems[i];
        //    //        CheckBox chkIsHaveRight = (CheckBox)(GridItem.FindControl("CheckBoxCell"));
        //    //        if (chkIsHaveRight.Checked)
        //    //        {
        //    //            OrgCode = UltraWebGridEvectionApply.Rows[i].Cells.FromKey("dcode").Text.Trim();
        //    //            AuditOrgCode = bll.GetWorkFlowOrgCode(OrgCode, BillTypeCode);
        //    //            string key = AuditOrgCode;
        //    //            List<string> list = new List<string>();
        //    //            if (!dicy.ContainsKey(key) && AuditOrgCode.Length > 0)
        //    //            {
        //    //                dicy.Add(key, list);
        //    //            }
        //    //            else if (AuditOrgCode.Length == 0)
        //    //            {
        //    //                intSendError += 1;

        //    //            }
        //    //            AuditOrgCode = "";
        //    //        }
        //    //    }
        //    //    for (int i = 0; i < countRow; i++)
        //    //    {
        //    //        CellItem GridItem = (CellItem)tcol.CellItems[i];
        //    //        CheckBox chkIsHaveRight = (CheckBox)(GridItem.FindControl("CheckBoxCell"));
        //    //        if (chkIsHaveRight.Checked)
        //    //        {
        //    //            OrgCode = UltraWebGridEvectionApply.Rows[i].Cells.FromKey("dcode").Text.Trim();
        //    //            AuditOrgCode = bll.GetWorkFlowOrgCode(OrgCode, BillTypeCode);
        //    //            string key = AuditOrgCode;
        //    //            if (dicy[key] != null)
        //    //            {
        //    //                dicy[key].Add(UltraWebGridEvectionApply.Rows[i].Cells.FromKey("ID").Text.Trim());
        //    //            }
        //    //        }
        //    //    }
        //    //    //生產單號，更新OTM_AdvanceApply表(申請人）
        //    //    int count = bll.SaveOrgAuditData("Add", dicy, BillNoType, BillTypeCode, CurrentUserInfo.Personcode, logmodel);
        //    //    intSendBillNo = count;
        //    //    intSendOK += 1;
        //    //    if (intSendOK + intSendError > 0)
        //    //    {
        //    //        if (intSendError > 0)
        //    //        {
        //    //            alertText = Message.SuccCount + intSendOK + ";" + Message.common_message_billcount + "：" + intSendBillNo + ";" + Message.FaileCount + intSendError + "(沒有定義簽核流程)";
        //    //            Page.ClientScript.RegisterStartupScript(GetType(), "show", "alert('" + alertText + "')", true);
        //    //        }
        //    //        else
        //    //        {
        //    //            alertText = Message.SuccCount + intSendOK + ";" + Message.common_message_billcount + "：" + intSendBillNo;
        //    //            Page.ClientScript.RegisterStartupScript(GetType(), "show", "alert('" + alertText + "')", true);
        //    //        }
        //    //    }
        //    //    else
        //    //    {
        //    //        Page.ClientScript.RegisterStartupScript(GetType(), "show", "alert('" + Message.AtLastOneChoose + "')", true);
        //    //        return;
        //    //    }
        //    //    DataUIBind();
        //    //    this.ProcessFlag.Value = "";
        //    //    this.HiddenOrgCode.Value = "";
        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    throw ex;
        //    //}
        //}
        //#endregion
    }
}
