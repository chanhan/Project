/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： HrmEmpOtherMoveForm.cs
 * 檔功能描述：加班類別異動功能模組UI類
 * 
 * 版本：1.0
 * 創建標識： 陈函 2011.12.23
 * 
 */
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using GDSBG.MiABU.Attendance.BLL.HRM.EmployeeData;
using GDSBG.MiABU.Attendance.Model.HRM.EmployeeData;
using Infragistics.WebUI.UltraWebGrid;
using Resources;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;
namespace GDSBG.MiABU.Attendance.Web.HRM.EmployeeData
{
    public partial class HrmEmpOtherMoveForm : BasePage
    {
        bool Privileged = true;//組織權限
        HrmEmpOtherMoveBll hrmEmpOtherMoveBll = new HrmEmpOtherMoveBll();
        string strModuleCode;
        JavaScriptSerializer JsSerializer = new JavaScriptSerializer();
        Dictionary<string, string> ClientMessage = null;
        static SynclogModel logmodel = new SynclogModel();
        static DataTable dtTemp= new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            strModuleCode = Request.QueryString["ModuleCode"] == null ? "" : Request.QueryString["ModuleCode"].ToString();
            PageHelper.ButtonControls(FuncList, pnlShowPanel.Controls, base.FuncListModule);

            if (ClientMessage == null)
            {
                ClientMessage = new Dictionary<string, string>();
                ClientMessage.Add("AtLastOneChoose", Message.AtLastOneChoose);
                ClientMessage.Add("ConfirmOnTime", Message.ConfirmOnTime);
                ClientMessage.Add("ConfirmConfirmData", Message.ConfirmConfirmData);
                ClientMessage.Add("DeleteConfirmed", Message.DeleteConfirmed);
                ClientMessage.Add("UnConfirmOnTime", Message.UnConfirmOnTime);
                ClientMessage.Add("ConfirmUnConfirmData", Message.ConfirmUnConfirmData);
                ClientMessage.Add("ConfirmBatchConfirm", Message.ConfirmBatchConfirm);
                ClientMessage.Add("ChooseOtherMoveTypeFirst", Message.ChooseOtherMoveTypeFirst);

            }
            string clientmsg = JsSerializer.Serialize(ClientMessage);
            Page.ClientScript.RegisterStartupScript(GetType(), "ClientMessage", "var Message=" + clientmsg + ";", true);
            SetCalendar(txtEffectDateFrom, txtEffectDateTo);
            IsHavePrivileged();

            if (!base.IsPostBack)
            {
                logmodel.ProcessOwner = CurrentUserInfo.Personcode;
                logmodel.TransactionType = Request.QueryString["ModuleCode"] == null ? "" : Request.QueryString["ModuleCode"].ToString();
                logmodel.LevelNo = "2";
                logmodel.FromHost = Request.UserHostAddress;

                HiddenLevelCode.Value = CurrentUserInfo.DepLevel;
                InitDropDownList();
                this.ModuleCode.Value = strModuleCode;
                pager.CurrentPageIndex = 1;
                DataBind();
                this.ImageDepCode.Attributes.Add("onclick", "javascript:GetTreeDataValue(\"txtDepCode\",'" + strModuleCode + "','txtDepName')");
                this.ImageBeforeValue.Attributes.Add("onclick", "onclickBeforeValue(ddlMoveType.selectedIndex)");
                this.ImageAfterValue.Attributes.Add("onclick", "onclickAfterValue(ddlMoveType.selectedIndex)");
            }
            if (this.PanelImport.Visible)
            {
                this.btnImport.Text = Resources.ControlText.btnReturn;
            }
        }
        #region 綁定DropDownList異動類別、是否歷史異動
        /// <summary>
        /// 綁定DropDownList異動類別、是否歷史異動
        /// </summary>
        private void InitDropDownList()
        {
            DataTable dt1 = hrmEmpOtherMoveBll.InitddlMoveType();
            ddlMoveType.DataSource = dt1;
            ddlMoveType.DataTextField = "DataValue";
            ddlMoveType.DataValueField = "DataCode";
            ddlMoveType.DataBind();
            ddlMoveType.Items.Insert(0, new ListItem("", ""));
            DataTable dt2 = hrmEmpOtherMoveBll.InitddlddlMoveState();
            ddlState.DataSource = dt2;
            ddlState.DataTextField = "DataValue";
            ddlState.DataValueField = "DataCode";
            ddlState.DataBind();
            ddlState.Items.Insert(0, new ListItem("", ""));
        }
        #endregion

        #region  是否有組織權限
        /// <summary>
        /// 是否有組織權限
        /// </summary>
        private void IsHavePrivileged()
        {

            if (CurrentUserInfo.Personcode.Equals("internal") || CurrentUserInfo.RoleCode.Equals("Person"))
            {
                Privileged = false;
            }
            else
            {
                DataTable dt = hrmEmpOtherMoveBll.GetDataByCondition(strModuleCode);
                if ((dt != null) && (dt.Rows.Count > 0))
                {
                    Privileged = false;
                }
            }
        }
        #endregion

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

        #region 數據綁定
        /// <summary>
        /// 數據綁定
        /// </summary>
        private void DataBind()
        {
            DataTable dt = new DataTable();
            int totalCount;
            dt = hrmEmpOtherMoveBll.SelectEmpMove(Privileged, SqlDep, txtDepName.Text, ddlHistoryMove.SelectedValue, txtWorkNo.Text, txtLocalName.Text, txtApplyMan.Text, ddlMoveType.SelectedValue, txtBeforeValueName.Text, txtAfterValueName.Text, ddlState.SelectedValue, txtEffectDateFrom.Text, txtEffectDateTo.Text, txtMoveReason.Text, pager.CurrentPageIndex, pager.PageSize, out totalCount);
            UltraWebGridEmpMove.DataSource = dt;
            pager.RecordCount = totalCount;
            UltraWebGridEmpMove.DataBind();
            pager.TextAfterPageIndexBox = "/" + pager.PageCount.ToString();
        }
        #endregion

        #region UltraWebGrid_DataBound
        /// <summary>
        /// UltraWebGrid_DataBound
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void UltraWebGridEmpMove_DataBound(object sender, EventArgs e)
        {
            Infragistics.WebUI.UltraWebGrid.TemplatedColumn tcol = (TemplatedColumn)UltraWebGridEmpMove.Bands[0].Columns[0];
            for (int i = 0; i < UltraWebGridEmpMove.Rows.Count; i++)
            {
                if (UltraWebGridEmpMove.Rows[i].Cells.FromKey("State").Text.Trim() == "0")
                {
                    UltraWebGridEmpMove.Rows[i].Style.ForeColor = System.Drawing.Color.Red;
                }
            }
        }
        #endregion

        #region 導入
        /// <summary>
        /// 導入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                this.btnConfirm.Enabled = false;
                this.btnUnConfirm.Enabled = false;
                this.btnBatchConfirm.Enabled = false;
                this.btnAdd.Enabled = false;
                this.btnModify.Enabled = false;
                this.btnImport.Text = Resources.ControlText.btnReturn;
                //    this.lbluploadMsg.Text = "";
            }
            else
            {
                this.PanelImport.Visible = false;
                this.PanelData.Visible = true;
                this.btnQuery.Enabled = true;
                this.btnReset.Enabled = true;
                this.btnDelete.Enabled = true;
                this.btnConfirm.Enabled = true;
                this.btnUnConfirm.Enabled = true;
                this.btnBatchConfirm.Enabled = true;
                this.btnAdd.Enabled = true;
                this.btnModify.Enabled = true;
                this.btnImport.Text = Resources.ControlText.btnImport;
            }
        }
        #endregion

        #region 刪除
        /// <summary>
        /// 刪除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            logmodel.ProcessFlag = "delete";
            string WorkNoMoveOrder = "";
            string alert = "";
            TemplatedColumn col = (TemplatedColumn)UltraWebGridEmpMove.Bands[0].Columns[0];

            for (int i = 0; i < UltraWebGridEmpMove.Rows.Count; i++)
            {

                CellItem GridItem = (CellItem)col.CellItems[i];
                CheckBox chkIsHaveRight = (CheckBox)(GridItem.FindControl("CheckBoxCell"));
                if (chkIsHaveRight.Checked)
                {
                    WorkNoMoveOrder += UltraWebGridEmpMove.Rows[i].Cells.FromKey("WorkNo").Text.Trim() + "|" + UltraWebGridEmpMove.Rows[i].Cells.FromKey("MoveOrder").Text.Trim() + "§";

                }
            }
            if (hrmEmpOtherMoveBll.DeleteEmpMove(WorkNoMoveOrder, logmodel))
            {
                alert = "alert('" + Message.DeleteSuccess + "')";
            }
            else
            {
                alert = "alert('" + Message.DeleteFailed + "')";
            }

            DataBind();
            this.ProcessFlag.Value = "";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "SaveMoveType", alert, true);
        }
        #endregion
        #region 確認
        /// <summary>
        /// 確認
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            logmodel.ProcessFlag = "update";
            string WorkNoMoveOrderAfterValue = "";
            string alert = "";
            TemplatedColumn col = (TemplatedColumn)UltraWebGridEmpMove.Bands[0].Columns[0];

            for (int i = 0; i < UltraWebGridEmpMove.Rows.Count; i++)
            {

                CellItem GridItem = (CellItem)col.CellItems[i];
                CheckBox chkIsHaveRight = (CheckBox)(GridItem.FindControl("CheckBoxCell"));
                if (chkIsHaveRight.Checked)
                {
                    string aftervalue = UltraWebGridEmpMove.Rows[i].Cells.FromKey("aftervalue").Text == null ? "" : UltraWebGridEmpMove.Rows[i].Cells.FromKey("aftervalue").Text.Trim();
                    string WorkNo = UltraWebGridEmpMove.Rows[i].Cells.FromKey("WorkNo").Text == null ? "" : UltraWebGridEmpMove.Rows[i].Cells.FromKey("WorkNo").Text.Trim();
                    string MoveOrder = UltraWebGridEmpMove.Rows[i].Cells.FromKey("MoveOrder").Text == null ? "" : UltraWebGridEmpMove.Rows[i].Cells.FromKey("MoveOrder").Text.Trim();
                    WorkNoMoveOrderAfterValue += WorkNo + "|" + MoveOrder + "*" + aftervalue + "§";
                }
            }
            if (hrmEmpOtherMoveBll.ConfirmData(WorkNoMoveOrderAfterValue, CurrentUserInfo.Personcode, logmodel))
            {
                alert = "alert('" + Message.ConfirmSuccess + "')";
            }
            else
            {
                alert = "alert('" + Message.ConfirmFailed + "')";
            }
            DataBind();
            this.ProcessFlag.Value = "";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ConfirmMoveType", alert, true);
        }
        #endregion

        #region 取消確認
        /// <summary>
        /// 取消確認
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUnConfirm_Click(object sender, EventArgs e)
        {
            logmodel.ProcessFlag = "update";
            string WorkNoMoveOrderBeforeValue = "";
            string alert = "";
            TemplatedColumn col = (TemplatedColumn)UltraWebGridEmpMove.Bands[0].Columns[0];

            for (int i = 0; i < UltraWebGridEmpMove.Rows.Count; i++)
            {
                CellItem GridItem = (CellItem)col.CellItems[i];
                CheckBox chkIsHaveRight = (CheckBox)(GridItem.FindControl("CheckBoxCell"));
                if (chkIsHaveRight.Checked)
                {
                    string beforevalue = UltraWebGridEmpMove.Rows[i].Cells.FromKey("beforevalue").Text == null ? "" : UltraWebGridEmpMove.Rows[i].Cells.FromKey("beforevalue").Text.Trim();
                    string WorkNo = UltraWebGridEmpMove.Rows[i].Cells.FromKey("WorkNo").Text == null ? "" : UltraWebGridEmpMove.Rows[i].Cells.FromKey("WorkNo").Text.Trim();
                    string MoveOrder = UltraWebGridEmpMove.Rows[i].Cells.FromKey("MoveOrder").Text == null ? "" : UltraWebGridEmpMove.Rows[i].Cells.FromKey("MoveOrder").Text.Trim();
                    WorkNoMoveOrderBeforeValue += WorkNo + "|" + MoveOrder + "*" + beforevalue + "§";
                }
            }
            if (hrmEmpOtherMoveBll.UnConfirmData(WorkNoMoveOrderBeforeValue, CurrentUserInfo.Personcode, logmodel))
            {
                alert = "alert('" + Message.UnConfirmSuccess + "')";
            }
            else
            {
                alert = "alert('" + Message.UnConfirmFailed + "')";
            }
            DataBind();
            this.ProcessFlag.Value = "";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "UnConfirmMoveType", alert, true);
        }
        #endregion

        #region 批量確認
        /// <summary>
        /// 批量確認
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnBatchConfirm_Click(object sender, EventArgs e)
        {
            DataTable dt = hrmEmpOtherMoveBll.SelectEmpMove(Privileged, SqlDep, txtDepName.Text, ddlHistoryMove.SelectedValue, txtWorkNo.Text, txtLocalName.Text, txtApplyMan.Text, ddlMoveType.SelectedValue, txtBeforeValueName.Text, txtAfterValueName.Text, ddlState.SelectedValue, txtEffectDateFrom.Text, txtEffectDateTo.Text, txtMoveReason.Text);
            string WorkNoMoveOrderAfterValue = "";
            string alert = "";
            int count = 0;
            logmodel.ProcessFlag = "update";
            foreach (DataRow dtRow in dt.Rows)
            {
                if (dtRow["state"].ToString() == "0")
                {
                    WorkNoMoveOrderAfterValue += dtRow["WorkNo"].ToString() + "|" + dtRow["MoveOrder"].ToString() + "*" + dtRow["aftervalue"].ToString() + "§";
                    count++;
                }
            }
            if (WorkNoMoveOrderAfterValue.Length != 0)
            {
                if (hrmEmpOtherMoveBll.ConfirmData(WorkNoMoveOrderAfterValue, CurrentUserInfo.Personcode, logmodel))
                {
                    alert = "alert('" + Message.SuccCount + count + ";" + Message.FaileCount + "0')";
                }
                else
                {
                    alert = "alert('" + Message.SuccCount + "0;" + Message.FaileCount + count + "')";
                }
            }
            else
            {
                alert = "alert('" + Message.SuccCount + "0;" + Message.FaileCount + "0')";
            }
            DataBind();
            this.ProcessFlag.Value = "";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "BatchConfirmMoveType", alert, true);
        }
        #endregion

        #region 上傳
        /// <summary>
        /// 上傳
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnImportSave_Click(object sender, EventArgs e)
        {
            ImportExcel();
        }
        #endregion


        #region 將文件中的數據導入到數據庫中
        /// <summary>
        /// 將文件中的數據導入到數據庫中
        /// </summary>
        private void ImportExcel()
        {
            int flag;
            bool deFlag;
            string tableName = "GDS_ATT_EmpMove_TEMP";
            string[] columnProperties = { "WORKNO", "LOCALNAME", "AfterValueName", "EffectDate", "MoveReason", "Remark" };
            string[] columnType = { "varchar", "varchar", "varchar", "varchar", "varchar", "varchar" };
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
                DataTable newdt = hrmEmpOtherMoveBll.ImpoertExcel(createUser, Request.QueryString["moduleCode"], CurrentUserInfo.CompanyId, out successnum, out errornum, logmodel);
                if (newdt==null)
                {
                    lbllblupload.Text = "數據導入失敗，請與管理員聯繫";
                }
                else
                {
                    lbllblupload.Text = "上傳成功筆數：" + successnum + ",上傳失敗筆數：" + errornum;
                    this.UltraWebGridImport.DataSource = changeError(newdt);
                    this.UltraWebGridImport.DataBind();
                    dtTemp = newdt;
                }
            }
            else if (flag == 0)
            {
                lbllblupload.Text = "數據保存失敗！";
            }
            else
            {
                lbllblupload.Text = "Excel數據格式錯誤";
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
                            case "ErrWorkNameNull": errorInfo = errorInfo + Message.ErrWorkNameNull + Message.ErrorInformationSlipt;
                                break;
                            case "ErrAfterValueNull": errorInfo = errorInfo + Message.ErrAfterValueNull + Message.ErrorInformationSlipt;
                                break;
                            case "ErrEffDateNull": errorInfo = errorInfo + Message.ErrEffDateNull + Message.ErrorInformationSlipt;
                                break;
                            case "ErrWorkNoNotEXIST": errorInfo = errorInfo + Message.ErrWorkNoNotEXIST + Message.ErrorInformationSlipt;
                                break;
                            case "ErrWorkNoAndWorkNameNotConsistency": errorInfo = errorInfo + Message.ErrWorkNoAndWorkNameNotConsistency + Message.ErrorInformationSlipt;
                                break;
                            case "ErrMoveAfterValueCode": errorInfo = errorInfo + Message.ErrMoveAfterValueCode + Message.ErrorInformationSlipt;
                                break;
                            case "ErrMoveAfterValueCodeNotEqualMoveBeforeValueCode": errorInfo = errorInfo + Message.ErrMoveAfterValueCodeNotEqualMoveBeforeValueCode + Message.ErrorInformationSlipt;
                                break;
                            case "ErrEffDateNotRight": errorInfo = errorInfo + Message.ErrEffDateNotRight + Message.ErrorInformationSlipt;
                                break;
                            case "Duplicationofdata": errorInfo = errorInfo + Message.Duplicationofdata + Message.ErrorInformationSlipt;
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
                    lbllblupload.Text = "Exel上傳到服務器失敗!";
                }
            }
            else
            {
                lbllblupload.Text = "導入路徑為空,請選擇要匯入的Excel文件！";
            }

            return filePath;
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
            DataBind();
        }
        #endregion
        #region 重置
        /// <summary>
        /// 重置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnReset_Click(object sender, EventArgs e)
        {
            this.txtDepCode.Text = "";
            this.txtDepName.Text = "";
            this.txtWorkNo.Text = "";
            this.txtLocalName.Text = "";
            this.ddlState.SelectedIndex = -1;
            this.ddlMoveType.SelectedIndex = -1;
            this.HiddenBeforeValue.Value = "";
            this.txtBeforeValueName.Text = "";
            this.HiddenAfterValue.Value = "";
            this.txtAfterValueName.Text = "";
            this.txtEffectDateFrom.Text = "";
            this.txtEffectDateTo.Text = "";
            this.ddlHistoryMove.SelectedIndex = -1;
            this.txtApplyMan.Text = "";
            txtMoveReason.Text = "";
        }
        #endregion
        #region 導出
        protected void btnExport_Click(object sender, EventArgs e)
        {
            if (!PanelImport.Visible)
            {
                HrmEmpOtherMoveImportBll hrmEmpOtherMoveImportBll = new HrmEmpOtherMoveImportBll();
                List<HrmEmpOtherMoveModel> list = hrmEmpOtherMoveImportBll.SelectEmpMove(Privileged, SqlDep, txtDepName.Text, ddlHistoryMove.SelectedValue, txtWorkNo.Text, txtLocalName.Text, txtApplyMan.Text, ddlMoveType.SelectedValue, txtBeforeValueName.Text, txtAfterValueName.Text, ddlState.SelectedValue, txtEffectDateFrom.Text, txtEffectDateTo.Text, txtMoveReason.Text);
                string[] header = { ControlText.gvHeaderBUName, ControlText.gvHeaderDepName, 
                                  ControlText.gvHeaderWorkNo, ControlText.gvHeaderLocalName, 
                                  ControlText.gvHeaderMoveTypeName, ControlText.gvHeaderBeforeValueName, 
                                   ControlText.gvHeaderAfterValueName, ControlText.gvHeaderEffectDate,
                                   ControlText.gvHeaderMoveReason, ControlText.gvHeaderRemark, 
                                   ControlText.gvHeaderApplyMan, ControlText.gvHeaderApplyDate, 
                                   ControlText.gvHeaderConfirmMan, ControlText.gvHeaderConfirmDate, 
                                   ControlText.gvHeaderStateName};
                string[] properties = { "BuName", "DepName", "WorkNo", "LocalName", "MoveType", "BeforeValueName",
                                      "AfterValueName", "EffectDateStr", "MoveReason", "Remark","ApplyMan","ApplyDateStr","ConfirmMan","ConfirmDateStr","StateName"};
                string filePath = MapPath("~/ExportFileTemp/") + DateTime.Now.Ticks + ".xls";
                NPOIHelper.ExportExcel(list, header, properties, 5000, filePath);
                PageHelper.ReturnHTTPStream(filePath, true);
            }
            else
            {
                if (dtTemp.Rows.Count>0)
                {
                    
             
                HrmEmpMoveTempBll hrmEmpMoveTempBll = new HrmEmpMoveTempBll();
                List<HrmEmpMoveTempModel> list = hrmEmpMoveTempBll.SelectEmpTempMove(dtTemp);
                string[] header = { ControlText.gvHeaderErrorMsg, ControlText.gvHeaderWorkNo, 
                                  ControlText.gvHeaderLocalName,
                                  ControlText.gvHeaderAfterValueName, ControlText.gvHeaderEffectDate, 
                                   ControlText.gvHeaderMoveReason,ControlText.gvHeaderRemark};
                string[] properties = { "Errormsg", "Workno", "Localname", "Aftervaluename", "Effectdate", "Movereason", "Remark" };
                string filePath = MapPath("~/ExportFileTemp/") + DateTime.Now.Ticks + ".xls";
                NPOIHelper.ExportExcel(list, header, properties, 5000, filePath);
                PageHelper.ReturnHTTPStream(filePath, true);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "StartDateEndDateNotNull", "alert('" + Message.NoErrorData + "')", true);
                }
            }

        }
        #endregion

    }
}
