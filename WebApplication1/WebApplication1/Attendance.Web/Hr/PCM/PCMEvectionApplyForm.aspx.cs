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

namespace GDSBG.MiABU.Attendance.Web.Hr.PCM
{
    public partial class PCMEvectionApplyForm : BasePage
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

            HiddenModuleCode.Value = Request.QueryString["ModuleCode"].ToString();
            if (!IsPostBack)
            {
                this.txtDepName.Text = CurrentUserInfo.DepName;
                this.txtDepCode.Text = CurrentUserInfo.DepCode;
                this.txtWorkNo.Text = CurrentUserInfo.Personcode;
                this.txtLocalName.Text = CurrentUserInfo.Cname;

                logmodel.ProcessOwner = CurrentUserInfo.Personcode;
                logmodel.TransactionType = Request.QueryString["ModuleCode"];
                logmodel.LevelNo = "2";
                logmodel.FromHost = Request.UserHostAddress;
                this.DropDownListBind(this.ddlEvectionType, "EvectionTypeOut");
                this.DropDownListBind(this.ddlStatus, "BillAuditState");
                model = PageHelper.GetModel<KQMEvectionApplyModel>(pnlContent.Controls);
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
            }
            string clientmsg = JsSerializer.Serialize(ClientMessage);
            Page.ClientScript.RegisterStartupScript(GetType(), "ClientMessage", "var Message=" + clientmsg + ";", true);
            PageHelper.ButtonControlsWF(FuncList, pnlShowPanel.Controls, base.FuncListModule);
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
        #region 保存按鈕
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (this.UltraWebGridEvectionApply.Rows.Count != 0)
            {
                int i;
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
                    }
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
                    Page.ClientScript.RegisterStartupScript(GetType(), "show", "alert('" + Message.NoDataExport + "')", true);
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
                    Page.ClientScript.RegisterStartupScript(GetType(), "show", "alert('" + Message.NoDataExport + "')", true);
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
            //model = PageHelper.GetModel<KQMEvectionApplyModel>(pnlContent.Controls);
            DataTable dtSelectAll = bll.GetEvectionList(model,depCode,sqlDep,pager.CurrentPageIndex, pager.PageSize, out totalCount);
            dt = bll.GetEvectionList(model);
            pager.RecordCount = totalCount;
            UltraWebGridEvectionApply.DataSource = dtSelectAll;
            UltraWebGridEvectionApply.DataBind();
            pager.TextAfterPageIndexBox = "/" + pager.PageCount.ToString();
        }
        #endregion
    }
}
