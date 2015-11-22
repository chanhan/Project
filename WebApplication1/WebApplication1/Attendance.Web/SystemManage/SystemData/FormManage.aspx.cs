using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using GDSBG.MiABU.Attendance.BLL.SystemManage.SystemData;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemData;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;
using Infragistics.WebUI.UltraWebGrid;
using Resources;

namespace GDSBG.MiABU.Attendance.Web.SystemManage.SystemData
{
    public partial class FormManage : BasePage
    {
        FormManageBll formManageBll = new FormManageBll();
        DataTable dt = new DataTable();
        JavaScriptSerializer JsSerializer = new JavaScriptSerializer();
        Dictionary<string, string> ClientMessage = null;
        static SynclogModel logmodel = new SynclogModel();
        protected void Page_Load(object sender, EventArgs e)
        {
           PageHelper.ButtonControls(FuncList, pnlShowPanel.Controls, base.FuncListModule);
            if (ClientMessage == null)
            {
                ClientMessage = new Dictionary<string, string>();
                ClientMessage.Add("AtLastOneChoose", Message.AtLastOneChoose);
                ClientMessage.Add("DeleteFormConfirm", Message.DeleteFormConfirm);
                ClientMessage.Add("ConfirmNewFormOverride", Message.ConfirmNewFormOverride);
                ClientMessage.Add("UploadDateMustBeToday", Message.UploadDateMustBeToday);
                
                
            }
            string clientmsg = JsSerializer.Serialize(ClientMessage);
            Page.ClientScript.RegisterStartupScript(GetType(), "ClientMessage", "var Message=" + clientmsg + ";", true);
            SetCalendar(txtUploadDate);
            if (!base.IsPostBack)
            {
                logmodel.ProcessOwner = CurrentUserInfo.Personcode;
                logmodel.TransactionType = Request.QueryString["ModuleCode"] == null ? "" : Request.QueryString["ModuleCode"].ToString();
                logmodel.LevelNo = "2";
                logmodel.FromHost = Request.UserHostAddress;

                FormTypeBind();
                pager.CurrentPageIndex = 1;
                DataBind();
            }
        }

        /// <summary>
        /// 編輯時的附件下載
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkbtnAnnexFile_Click(object sender, EventArgs e)
        {
            string filePath = Server.MapPath("~/FormFiles/") + hidFormPath.Value;
            PageHelper.ReturnHTTPStream(filePath, false);
        }

        #region 保存
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string fileName = GetImpFileName();
            FormModel model = PageHelper.GetModel<FormModel>(pnlFormInfo.Controls);
            string alert = "";
            if (hidOperate.Value == "Add")
            {
                logmodel.ProcessFlag = "insert";

                model.FormPath = fileName;
                model.CreateDate = DateTime.Now;
                model.CreateUser = CurrentUserInfo.Personcode;
                alert = formManageBll.AddForm(model, logmodel) ? "alert('" + Message.SaveSuccess + "')" : "alert('" + Message.SaveFailed + "')";
            }
            else if (hidOperate.Value == "Modify")
            {
                logmodel.ProcessFlag = "update";

                if (!string.IsNullOrEmpty(fileName)) { model.FormPath = fileName; }
                alert = formManageBll.UpdateFormByKey(model, logmodel) ? "alert('" + Message.UpdateSuccess + "')" : "alert('" + Message.UpdateFailed + "')";
     //           lblMessage.Text = formManageBll.UpdateFormByKey(model, logmodel) ? Message.UpdateSuccess : Message.UpdateFailed;
            }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "addOrUpDateForm", alert, true);
            PageHelper.CleanControlsValue(pnlFormInfo.Controls);
        //    hidOperate.Value = "Add";
            chkActiveFlag.Checked = true;
           DataBind();
        }
        #endregion

        #region 獲取上傳文件名,并將文件保存到服務器
        /// <summary>
        /// 獲取上傳文件名,并將文件保存到服務器
        /// </summary>
        /// <returns></returns>
        private string GetImpFileName()
        {
            string fileName = "";
            if (fileAnnexFile.FileName.Trim() != "")
            {
                fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + Path.GetFileName(fileAnnexFile.FileName);
                fileAnnexFile.SaveAs(MapPath("~/FormFiles/") + fileName);
            }
            return fileName;
        }
        #endregion

        /// <summary>
        /// 綁定數據
        /// </summary>
        private void DataBind()
        {
            int totalCount;
            FormModel model = PageHelper.GetModel<FormModel>(pnlFormInfo.Controls, hidFormSeq);
            model.FormPath = null;
            dt = formManageBll.GetOrderForm(model, pager.CurrentPageIndex, pager.PageSize, out totalCount);
            pager.RecordCount = totalCount;
            UltraWebGridInfoForm.DataSource = dt;
            UltraWebGridInfoForm.DataBind();
        }

        #region 分頁
        /// <summary>
        /// 分頁
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void pager_PageChanged(object sender, EventArgs e)
        {
            DataBind();
        }
        #endregion

        #region 表單類型綁定
        /// <summary>
        /// 表單類型綁定
        /// </summary>
        private void FormTypeBind()
        {
            ddlTypeId.DataTextField = "TYPE_NAME";
            ddlTypeId.DataValueField = "TYPE_ID";

            DataTable dataTable = formManageBll.GetFormType();

            ddlTypeId.DataSource = dataTable;
            ddlTypeId.DataBind();
            ddlTypeId.Items.Insert(0, new ListItem("", ""));
        }
        #endregion

        #region 查詢
        /// <summary>
        /// 查詢
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            pager.CurrentPageIndex = 1;
            DataBind();
            PageHelper.CleanControlsValue(pnlFormInfo.Controls);
            chkActiveFlag.Checked = true;
            ProcessFlag.Value = "N";
        }
        #endregion


        /// <summary>
        /// Ajax請求獲得功能Model Json
        /// </summary>
        protected override void AjaxProcess()
        {

            if (!string.IsNullOrEmpty(Request.Form["formId"]))
            {
                FormModel model = formManageBll.GetFormByKey(Request.Form["formId"]);
                string formJson = null;
                if (model != null)
                {
                    formJson = JsSerializer.Serialize(model);
                }
                Response.Clear();
                Response.Write(formJson);
                Response.End();
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            logmodel.ProcessFlag = "delete";
            FormModel model = new FormModel();
            model.FormSeq = Convert.ToInt32(hidFormSeq.Value);
            lblMessage.Text = formManageBll.DeleteNotice(model, logmodel) ? Message.DeleteSuccess : Message.DeleteFailed;
            PageHelper.CleanControlsValue(pnlFormInfo.Controls);
            chkActiveFlag.Checked = true;
            DataBind();
          ProcessFlag.Value = "N";
        }


        #region UltraWebGrid_DataBound
        /// <summary>
        /// UltraWebGrid_DataBound
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void UltraWebGridInfoForm_DataBound(object sender, EventArgs e)
        {
            Infragistics.WebUI.UltraWebGrid.TemplatedColumn tcol = (TemplatedColumn)UltraWebGridInfoForm.Bands[0].Columns[2];
            for (int loop = 0; loop < UltraWebGridInfoForm.Rows.Count; loop++)
            {
                CellItem GridItem = (CellItem)tcol.CellItems[loop];
                Image image = (Image)(GridItem.FindControl("imgActiveFlag"));
                if (dt.Rows[loop]["Active_Flag"].ToString() == "Y")
                {
                    image.ImageUrl = "../../CSS/Images_new/gou.gif";
                }
                else
                {
                    image.ImageUrl = "../../CSS/Images_new/cha.gif";
                }
            }
        }
        #endregion
    }
}
