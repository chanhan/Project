using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using GDSBG.MiABU.Attendance.BLL.Hr.KQM.KaoQinData;
using System.IO;
using Resources;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;

namespace GDSBG.MiABU.Attendance.Web.Hr.KQM.KaoQinData
{
    public partial class KQMLeaveTestifyForm : BasePage
    {
        KQMLeaveApplyForm_ZBLHBll leaveApply = new KQMLeaveApplyForm_ZBLHBll();
        static SynclogModel logmodel = new SynclogModel();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                logmodel.ProcessOwner = CurrentUserInfo.Personcode;
                logmodel.TransactionType = Request.QueryString["ModuleCode"] == null ? "" : Request.QueryString["ModuleCode"].ToString();
                logmodel.LevelNo = "2";
                logmodel.FromHost = Request.UserHostAddress;

                string id = (Request.QueryString["ID"] == null) ? "" : Request.QueryString["ID"].ToString();
                if (id.Length == 0)
                {
                    Response.Write("<script type='text/javascript'>alert('" + Message.AtLastOneChoose + "');window.close();</script>");
                }
                else
                {
                    Query(id);
                    hidID.Value = id;
                    chkFlag.Attributes.Add("onclick", "onChangeFlag(this)");
                }
            }

        }

        private void Query(string id)
        {
            DataTable tempDataTable = leaveApply.getLeaveApply(id);
            if (tempDataTable.Rows.Count > 0)
            {
                txtEmployeeNo.Text = tempDataTable.Rows[0]["WorkNo"].ToString();
                txtLocalName.Text = tempDataTable.Rows[0]["LocalName"].ToString();
                txtLeaveTypeName.Text = tempDataTable.Rows[0]["LVTypeName"].ToString();
            }
            else
            {
                Response.Write("<script type='text/javascript'>alert('" + Message.AtLastOneChoose + "');window.close();</script>");
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(Server.MapPath("~/Testify/")))
            {

                Directory.CreateDirectory(Server.MapPath("~/Testify/"));
            }
            string fileName = "";
            if (chkFlag.Checked)
            {
                fileName = FileUpload.FileName;
                string MaxSize = ConfigurationManager.AppSettings["MaxSize"];
                int intFileSize = FileUpload.PostedFile.ContentLength;
                if (string.IsNullOrEmpty(MaxSize))
                {
                    MaxSize = "1024";
                }
                //if (fileName.IndexOf(" ") >= 0)
                //{
                //    base.WriteMessage(1, base.GetResouseValue("common.Adjunct") + base.GetResouseValue("common.notallowblank"));
                //    return;
                //}
                //  if (!string.IsNullOrEmpty(FileUpload.PostedFile.FileName) && File.Exists(FileUpload.PostedFile.FileName))
                if (FileUpload.HasFile)
                {
                    //判斷上傳文件類型
                    //判斷上傳文件大小
                    //if (!this.Session["roleCode"].ToString().Equals("Admin") && (Convert.ToInt32(MaxSize) < (intFileSize / 0x400)))
                    //{
                    //    base.WriteMessage(2, base.GetResouseValue("bbs.upload.checksize") + MaxSize + "KB");
                    //    return;
                    //}
                    fileName = txtEmployeeNo.Text.Trim() + fileName;
                    FileUpload.PostedFile.SaveAs(Server.MapPath("~/Testify/") + fileName);
                    leaveApply.Testify(hidID.Value, fileName, CurrentUserInfo.Cname, logmodel);
                    Response.Write("<script type='text/javascript'>alert('" + Message.DataSaveSuccess + "');window.close();window.dialogArguments.document.all.btnQuery.click();</script>");
                }
                else
                {
                    Response.Write("<script type='text/javascript'>alert('" + Message.FileNotExist + "');window.close();</script>");
                }
            }
            else
            {
                leaveApply.Testify(hidID.Value, fileName, CurrentUserInfo.Cname, logmodel);
                Response.Write("<script type='text/javascript'>alert('" + Message.DataSaveSuccess + "');window.close();window.dialogArguments.document.all.btnQuery.click();</script>");
            }
        }


    }
}
