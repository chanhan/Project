using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using GDSBG.MiABU.Attendance.BLL.HRM;
using Infragistics.WebUI.UltraWebGrid;
using Resources;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;

namespace GDSBG.MiABU.Attendance.Web.HRM
{
    public partial class OrgEmployeeEditForm : BasePage
    {
        OrgEmployeeBll orgEmployeeBll = new OrgEmployeeBll();
        string OrgCode = "";
    //    string sqlDep = "";
        JavaScriptSerializer JsSerializer = new JavaScriptSerializer();
        Dictionary<string, string> ClientMessage = null;
        static SynclogModel logmodel = new SynclogModel();
        protected void Page_Load(object sender, EventArgs e)
        {
            
            OrgCode = this.Request.QueryString["OrgCode"] == null ? "" : this.Request.QueryString["OrgCode"].ToString();
            if (ClientMessage == null)
            {
                ClientMessage = new Dictionary<string, string>();
                ClientMessage.Add("SaveConfirm", Message.SaveConfirm);
            }
            string clientmsg = JsSerializer.Serialize(ClientMessage);
            Page.ClientScript.RegisterStartupScript(GetType(), "ClientMessage", "var Message=" + clientmsg + ";", true);
            //   OrgCode = orgEmployeeBll.GetSqlDep(CurrentUserInfo.Personcode, Request.QueryString["moduleCode"],CurrentUserInfo.CompanyId);
            if (!IsPostBack)
            {
                logmodel.ProcessOwner = CurrentUserInfo.Personcode;
                logmodel.TransactionType = Request.QueryString["ModuleCode"] == null ? "" : Request.QueryString["ModuleCode"].ToString();
                logmodel.LevelNo = "2";
                logmodel.FromHost = Request.UserHostAddress;

                if (OrgCode.Length != 0)
                {
                    DataTable dt = Query(OrgCode);
                    gridEmployee.DataSource = dt;
                    gridEmployee.DataBind();
                }
                else
                {
                    btnSave.Enabled = false;
              //      btnImport.Enabled = false;
                    btnEmpty.Enabled = false;
                }
            }
        }
        /// <summary>
        /// 根據組織代碼查詢
        /// </summary>
        /// <param name="OrgCode"></param>
        /// <returns></returns>
        private DataTable Query(string OrgCode)
        {
            string depCode = orgEmployeeBll.GetDepCode(OrgCode);
            ibtnPickDept.Attributes.Add("onclick", "javascript:GetDepByParentDep('txtOrgCode',\"WHERE companyid='" + CurrentUserInfo.CompanyId + "' \",'" + this.Request.QueryString["moduleCode"] + "','txtOrgName','" + depCode + "')");
            DataTable dt = orgEmployeeBll.GetDataByOrgCode(OrgCode);
            return dt;
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string alert;
            string orgcode = txtOrgCode.Text.Trim();
            if (orgcode.Length == 0)
            {
                alert = "alert('" + Message.SelectFirst + "')";
            }
            else
            {
                if (GridSelEmployee.Rows.Count == 0)
                {
                    alert = "alert('" + Message.SelectEmployIsNotNull + "')";
                }
                else
                {
                    int index = 0;
                    string worknos = "";
              //      string sqlDep = "";
                    Infragistics.WebUI.UltraWebGrid.TemplatedColumn tcol = (TemplatedColumn)GridSelEmployee.Bands[0].Columns[0];
                    for (int loop = 0; loop < GridSelEmployee.Rows.Count; loop++)
                    {
                        CellItem GridItem = (CellItem)tcol.CellItems[loop];
                        CheckBox chkIsHaveRight = (CheckBox)(GridItem.FindControl("CheckBoxCell"));
                        if (chkIsHaveRight.Checked == true)
                        {
                            index++;
                            worknos += GridSelEmployee.Rows[loop].Cells[1].Text + "§";
                        }
                    }

                    if (this.GridSelEmployee.Rows.Count > index)
                    {
                        alert = "alert('" + Message.SelectAll + "')";
                    }
                    else
                    {

                        if (!orgEmployeeBll.IsInDep(orgcode, SqlDep))//判斷課組編號是否屬於該部門
                        {
                            alert = "alert('" + Message.ClassGroupNotBelong + "')";
                        }
                        else
                        {
                              logmodel.ProcessFlag = "update";
                              bool flag = orgEmployeeBll.UpdateData(worknos, orgcode, CurrentUserInfo.Personcode, logmodel);
                            if (flag)
                            {
                                alert = "alert('" + index + Message.AssignInfo + txtOrgName.Text + "')";
                            }
                            else
                            {
                                alert = "alert('" + Message.SaveFailed + "')";
                            }
                        }
                    }
                }
            }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "SaveOrgEmployee", alert, true);
        }
        /// <summary>
        /// 右方向按鈕事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnRight_Click(object sender, EventArgs e)
        {
            int index = GridSelEmployee.Rows.Count;
            Infragistics.WebUI.UltraWebGrid.TemplatedColumn tcol = (TemplatedColumn)gridEmployee.Bands[0].Columns[0];

            for (int loop = 0; loop < gridEmployee.Rows.Count; loop++)
            {
                CellItem GridItem = (CellItem)tcol.CellItems[loop];
                CheckBox chkIsHaveRight = (CheckBox)(GridItem.FindControl("CheckBoxCell"));
                if (chkIsHaveRight.Checked == true)
                {
                    GridSelEmployee.Rows.Add(ControlText.gvHeadParamsOrgEmpWorkNo);
                    GridSelEmployee.Rows[index].Cells[1].Text = gridEmployee.Rows[loop].Cells[1].Text;
                    GridSelEmployee.Rows[index].Cells[2].Text = gridEmployee.Rows[loop].Cells[2].Text;
                    index++;
                }
            }
            for (int iLoop = gridEmployee.Rows.Count - 1; iLoop >= 0; iLoop--)
            {
                CellItem GridItem = (CellItem)tcol.CellItems[iLoop];
                CheckBox chkIsHaveRight = (CheckBox)(GridItem.FindControl("CheckBoxCell"));
                if (chkIsHaveRight.Checked == true)
                {
                    gridEmployee.Rows.RemoveAt(iLoop);
                }
            }
        }
        /// <summary>
        /// 左方向按鈕事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnLeft_Click(object sender, EventArgs e)
        {
            int index = gridEmployee.Rows.Count;
            Infragistics.WebUI.UltraWebGrid.TemplatedColumn tcol = (TemplatedColumn)GridSelEmployee.Bands[0].Columns[0];
            for (int loop = 0; loop < GridSelEmployee.Rows.Count; loop++)
            {
                CellItem GridItem = (CellItem)tcol.CellItems[loop];
                CheckBox chkIsHaveRight = (CheckBox)(GridItem.FindControl("CheckBoxCell"));
                if (chkIsHaveRight.Checked == true)
                {
                    gridEmployee.Rows.Add(ControlText.gvHeadParamsOrgEmpWorkNo);
                    gridEmployee.Rows[index].Cells[1].Text = GridSelEmployee.Rows[loop].Cells[1].Text;
                    gridEmployee.Rows[index].Cells[2].Text = GridSelEmployee.Rows[loop].Cells[2].Text;
                    index++;
                }
            }
            for (int iLoop = GridSelEmployee.Rows.Count - 1; iLoop >= 0; iLoop--)
            {
                CellItem GridItem = (CellItem)tcol.CellItems[iLoop];
                CheckBox chkIsHaveRight = (CheckBox)(GridItem.FindControl("CheckBoxCell"));
                if (chkIsHaveRight.Checked == true)
                {
                    GridSelEmployee.Rows.RemoveAt(iLoop);
                }
            }
        }

        protected void btnEmpty_Click(object sender, EventArgs e)
        {
            string alert;
            //    string orgcode = txtOrgCode.Text.Trim();
            if (GridSelEmployee.Rows.Count == 0)
            {
                alert = "alert('" + Message.SelectFirst + "')";
            }
            else
            {
                string worknos = "";
                Infragistics.WebUI.UltraWebGrid.TemplatedColumn tcol = (TemplatedColumn)GridSelEmployee.Bands[0].Columns[0];
                for (int loop = 0; loop < GridSelEmployee.Rows.Count; loop++)
                {
                    worknos += GridSelEmployee.Rows[loop].Cells[1].Text + "§";
                }
                logmodel.ProcessFlag = "update";
                bool flag = orgEmployeeBll.UpdateData(worknos, "", CurrentUserInfo.Personcode, logmodel);
                if (flag)
                {
                    alert = "alert('" +Message.EmptySuccess + "')";
                }
                else
                {
                    alert = "alert('" + Message.EmptyFailed + "')";
                }
            }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DeleteOrgEmployee", alert, true);
        }
        protected void btnImport_Click(object sender, EventArgs e)
        {

            if (!this.PanelImport.Visible)
            {
                this.PanelImport.Visible = true;
                this.panelData.Visible = false;
                this.ProcessFlag.Value = "Import";
            }
            else
            {
                this.PanelImport.Visible = false;
                this.panelData.Visible = true;
                this.ProcessFlag.Value = "";
            }
        }

        protected void btnImportSave_Click(object sender, EventArgs e)
        {
            ImportExcel();
        }


        #region 將文件中的數據導入到數據庫中
        /// <summary>
        /// 將文件中的數據導入到數據庫中
        /// </summary>
        private void ImportExcel()
        {
            int flag;
            bool deFlag;
            string tableName = "GDS_ATT_TEMP_ORGEmplyee";
            string[] columnProperties = { "WORKNO", "LOCALNAME", "ORGCODE" };
            string[] columnType = { "varchar", "varchar", "varchar" };
            string createUser = CurrentUserInfo.Personcode;
            deFlag = NPOIHelper.DeleteExcelSql(tableName, createUser);
            string filePath = GetImpFileName();
            if (string.IsNullOrEmpty(filePath))
            {
                return;
            }
            flag = NPOIHelper.ImportExcel(columnProperties, columnType, filePath, tableName, createUser);
            if (flag == 1)
            {
                int successnum = 0;
                int errornum = 0;
                DataTable newdt = orgEmployeeBll.ImpoertExcel(createUser, Request.QueryString["moduleCode"], CurrentUserInfo.CompanyId, out successnum, out errornum,logmodel);
                lblupload.Text = "上傳成功筆數：" + successnum + ",上傳失敗筆數：" + errornum;
                this.UltraWebGridImport.DataSource = changeError(newdt);
                this.UltraWebGridImport.DataBind();
            }
            else if (flag == 0)
            {
                lblupload.Text = "數據保存失敗！";
            }
            else
            {
                lblupload.Text = "Excel數據格式錯誤";
            }
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
                    for (int j = 0; j < dt.Rows[i]["errormsg"].ToString().Split('§').Length; j++)
                    {

                        switch (dt.Rows[i]["errormsg"].ToString().Split('§')[j].ToString().Trim())
                        {
                            case "ErrWorkNoNull": errorInfo = errorInfo + Message.ErrorWorkNoNull + Message.ErrorInformationSlipt;
                                break;
                            case "ErrWorkNameNull": errorInfo = errorInfo + Message.ErrWorkNameNull+ Message.ErrorInformationSlipt;
                                break;
                            case "ErrOrgCodeNull": errorInfo = errorInfo + Message.ErrOrgCodeNull+ Message.ErrorInformationSlipt;
                                break;
                            case "ErrWorkNoNotEXIST": errorInfo = errorInfo + Message.ErrWorkNoNotEXIST + Message.ErrorInformationSlipt;
                                break;
                            case "ErrWorkNoAndWorkNameNotConsistency": errorInfo = errorInfo + Message.ErrWorkNoAndWorkNameNotConsistency + Message.ErrorInformationSlipt;
                                break;
                            case "ErrTeamCodeOut": errorInfo = errorInfo + Message.ErrTeamCodeOut + Message.ErrorInformationSlipt;
                                break;
                            default: break;
                        }
                    }
                    dt.Rows[i]["errormsg"] = errorInfo.TrimEnd(Message.ErrorInformationSlipt.ToCharArray());
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
                    lblupload.Text = "Exel上傳到服務器失敗!";
                }
            }
            else
            {
                lblupload.Text = "導入路徑為空,請選擇要匯入的Excel文件！";
            }

            return filePath;
        }
        #endregion

    }
}
