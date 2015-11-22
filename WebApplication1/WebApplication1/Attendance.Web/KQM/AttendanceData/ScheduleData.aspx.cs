using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web.UI.WebControls;
using GDSBG.MiABU.Attendance.BLL.KQM.AttendanceData;
using GDSBG.MiABU.Attendance.Model.KQM.AttendanceData;
using GDSBG.MiABU.Attendance.Model.KQM.BasicData;
using GDSBG.MiABU.Attendance.BLL.KQM.BasicData;
using Resources;

namespace GDSBG.MiABU.Attendance.Web.KQM.AttendanceData
{
    public partial class ScheduleData : BasePage
    {
        WorkShiftBll workBll = new WorkShiftBll();
        SchduleDataBll scheduleBll = new SchduleDataBll();
        protected void Page_Load(object sender, EventArgs e)
        {
            SetCalendar(txtSchduleDate);
            if (!IsPostBack)
            {

            }

        }

        /// <summary>
        /// 分頁
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            pager.CurrentPageIndex = 1;
            ShowDataBind();
        }

        /// <summary>
        /// 排班作業數據綁定
        /// </summary>
        private void ShowDataBind()
        {
            int totalCount = 1;
            string status = " ";
            string scheduleDate = this.txtSchduleDate.Text;
            ScheduleDataModel model = PageHelper.GetModel<ScheduleDataModel>(pnlContent.Controls);
            List<ScheduleDataModel> list = scheduleBll.GetPagerSchduleInfoList(scheduleDate, status, model, pager.CurrentPageIndex, pager.PageSize, out totalCount);
            pager.RecordCount = totalCount;
            this.UltraWebGridSchedule.DataSource = list;
            this.UltraWebGridSchedule.DataBind();
            pager.TextAfterPageIndexBox = "/" + pager.PageCount.ToString();
        }

        /// <summary>
        /// 導出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonExport_Click(object sender, EventArgs e)
        {
            ScheduleDataModel model = PageHelper.GetModel<ScheduleDataModel>(pnlContent.Controls);
            string scheduleDate = txtSchduleDate.Text;
            string status = null;
            List<ScheduleDataModel> list = scheduleBll.GetSchduleInfoList(scheduleDate, status, model);
            string[] header = { ControlText.gvHeadWorkNo, ControlText.gvHeadLocalName, ControlText.gvHeadDepName, ControlText.gvHeadShiftDesc, ControlText.gvHeadStartEndDate };
            string[] properties = { "WorkNo", "LocalName", "DepName", "ShiftDesc", "StartEndDate" };
            string filePath = MapPath("~/ExportFileTemp/") + DateTime.Now.Ticks + ".xls";
            NPOIHelper.ExportExcel(list, header, properties, 5000, filePath);
            PageHelper.ReturnHTTPStream(filePath, true);
        }

        /// <summary>
        /// 導入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonImport_Click(object sender, EventArgs e)
        {

        }



        #region 導入
        /// <summary>
        /// 導入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnImportSave_Click(object sender, EventArgs e)
        {
            ImportExcel();
        }

        /// <summary>
        /// 導入數據
        /// </summary>
        private void ImportExcel()
        {
            int flag;
            bool deFlag;
            string tableName = "gds_att_employeeshift_temp";
            string[] columnProperties = { "workno", "localname", "startdate", "enddate" };
            string[] columnType = { "varchar", "varchar", "varchar", "varchar" };
            //string createUser = CurrentUserInfo.EmpNo;
            string createUser = CurrentUserInfo.Personcode;
            deFlag = NPOIHelper.DeleteExcelSql(tableName, createUser);
            string filePath = GetImpFileName();
            if (string.IsNullOrEmpty(filePath))
                return;

            flag = NPOIHelper.ImportExcel(columnProperties, columnType, filePath, tableName, createUser);
            if (flag == 1)
            {
                int successNum = 0;
                int errorNum = 0;
                DataTable newDtbl = scheduleBll.GetImportCount(createUser, out successNum, out errorNum);
                DataTable dtbl = new DataTable();
                lblUploadMsg.Text = "上傳成功筆數：" + successNum + ",上傳失敗筆數：" + errorNum;
                dtbl = changeError(newDtbl);
                this.UltraWebGridImport.DataSource = dtbl
;
                this.UltraWebGridImport.DataBind();
            }
            else if (flag == 0)
            {
                lblUploadMsg.Text = "數據保存失敗！";
            }
            else
            {
                lblUploadMsg.Text = "Excel數據格式錯誤";
            }
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
                    lblUploadMsg.Text = "Exel上傳到服務器失敗!";
                }
            }
            else
            {
                lblUploadMsg.Text = "導入路徑為空,請選擇要匯入的Excel文件！";
            }

            return filePath;
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
                            case "ErrWorkNo": errorInfo = errorInfo + Message.ErrWorkNo;
                                break;
                            case "ErrWorkNoRepeat": errorInfo = errorInfo + Message.ErrWorkNoRepeat;
                                break;
                            case "ErrDate(2012/01/01)": errorInfo = errorInfo + Message.ErrDate;
                                break;
                            default: break;
                        }
                    }
                    dt.Rows[i]["errormsg"] = errorInfo;
                }
            }
            return dt;
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        protected void ddlShiftBind()
        {
            this.ddlShift.DataTextField = "ShiftDetail";
            this.ddlShift.DataValueField = "ShiftNo";
            WorkShiftModel model = new WorkShiftModel();
            List<WorkShiftModel> list = workBll.GetShiftType(model);
            this.ddlShift.DataSource = list;
            this.ddlShift.DataBind();
        }

        #region 分頁
        protected void pager_PageChanged(object sender, EventArgs e)
        {
            ShowDataBind();
        }
        #endregion

        //#region 放大鏡綁定
        ///// <summary>
        ///// 設置Selector
        ///// </summary>
        ///// <param name="ctrlTrigger">控件ID--按鈕</param>
        ///// <param name="ctrlCode">控件ID--文本框1</param>
        ///// <param name="ctrlName">控件ID--文本框2</param>
        //public void SetSelector(WebControl ctrlTrigger, Control ctrlCode, Control ctrlName)
        //{
        //    if (ctrlCode is TextBox) { (ctrlCode as TextBox).Attributes.Add("readonly", "readonly"); }
        //    if (ctrlName is TextBox) { (ctrlName as TextBox).Attributes.Add("readonly", "readonly"); }
        //    ctrlTrigger.Attributes.Add("onclick", string.Format("return setSelector('{0}','{1}')",
        //        ctrlCode.ClientID, ctrlName.ClientID));
        //}

        //#endregion
        //protected void ButtonExport_Click(object sender, EventArgs e)
        //{
        //    Exception ex;
        //    if (!this.PanelImport.Visible)
        //    {
        //        if (this.ViewState["condition"] == null)
        //        {
        //            base.WriteMessage(1, base.GetResouseValue("common.message.nodataexport"));
        //        }
        //        else if (base.CheckData(this.textBoxShiftDate.Text, "bfw.kqm_employeeshift.date"))
        //        {
        //            try
        //            {
        //                this.dataSet = ((ServiceLocator)this.Session["serviceLocator"]).GetKQMShiftQuery().GetDataByCondition(this.ViewState["condition"].ToString(), Convert.ToDateTime(this.textBoxShiftDate.Text).ToString("yyyy/MM/dd"), this.ddlShiftType.SelectedValue);
        //                string OrgCode = "";
        //                string ShiftNo = "";
        //                string ShiftDesc = "";
        //                string StartDate = "";
        //                string EndDate = "";
        //                string strTemp = "";
        //                string[] temVal = null;
        //                SortedList SListOrgCodeShiftDesc = new SortedList();
        //                SortedList SListOrgCodeStartDate = new SortedList();
        //                SortedList SListOrgCodeEndDate = new SortedList();
        //                string ShiftDate = this.textBoxShiftDate.Text;
        //                try
        //                {
        //                    ShiftDate = Convert.ToDateTime(ShiftDate).ToString("yyyy/MM/dd");
        //                }
        //                catch (Exception)
        //                {
        //                    ShiftDate = DateTime.Now.ToString("yyyy/MM/dd");
        //                }
        //                foreach (DataRow dr in this.dataSet.Tables["KQM_ShiftQuery"].Rows)
        //                {
        //                    OrgCode = dr["dCode"].ToString();
        //                    if (Convert.ToString(dr["ShiftDesc"]).Length == 0)
        //                    {
        //                        if (SListOrgCodeShiftDesc.IndexOfKey(OrgCode) >= 0)
        //                        {
        //                            ShiftDesc = SListOrgCodeShiftDesc.GetByIndex(SListOrgCodeShiftDesc.IndexOfKey(OrgCode)).ToString();
        //                            if (ShiftDesc.Length > 0)
        //                            {
        //                                StartDate = SListOrgCodeStartDate.GetByIndex(SListOrgCodeStartDate.IndexOfKey(OrgCode)).ToString();
        //                                EndDate = SListOrgCodeEndDate.GetByIndex(SListOrgCodeEndDate.IndexOfKey(OrgCode)).ToString();
        //                                dr["ShiftDesc"] = ShiftDesc;
        //                                dr["StartDate"] = StartDate;
        //                                dr["EndDate"] = EndDate;
        //                                if ((StartDate.Length > 0) && (EndDate.Length > 0))
        //                                {
        //                                    try
        //                                    {
        //                                        dr["StartEndDate"] = Convert.ToDateTime(StartDate).ToString(base.dateFormat) + "~" + Convert.ToDateTime(EndDate).ToString(base.dateFormat);
        //                                    }
        //                                    catch (Exception)
        //                                    {
        //                                        dr["StartEndDate"] = "";
        //                                    }
        //                                }
        //                            }
        //                        }
        //                        else
        //                        {
        //                            strTemp = ((ServiceLocator)this.Session["serviceLocator"]).GetFunctionData().GetEmpOrgShift(OrgCode, ShiftDate);
        //                            if (strTemp.Length > 0)
        //                            {
        //                                temVal = strTemp.Split(new char[] { '|' });
        //                                ShiftNo = temVal[0].ToString();
        //                                if (ShiftNo.Length > 0)
        //                                {
        //                                    ShiftDesc = ((ServiceLocator)this.Session["serviceLocator"]).GetFunctionData().GetValue("select ShiftDesc from(select ShiftNo||':'||ShiftDesc||'['||(select dataValue from bfw_typedata b where b.datatype='ShiftType' and b.datacode=a.shifttype)||']' ShiftDesc from KQM_WorkShift a  where a.ShiftNo='" + ShiftNo + "')") + "(" + temVal[4].ToString() + ")";
        //                                    StartDate = temVal[1].ToString();
        //                                    EndDate = temVal[2].ToString();
        //                                    dr["ShiftDesc"] = ShiftDesc;
        //                                    dr["StartDate"] = StartDate;
        //                                    dr["EndDate"] = EndDate;
        //                                    if ((StartDate.Length > 0) && (EndDate.Length > 0))
        //                                    {
        //                                        try
        //                                        {
        //                                            dr["StartEndDate"] = Convert.ToDateTime(StartDate).ToString(base.dateFormat) + "~" + Convert.ToDateTime(EndDate).ToString(base.dateFormat);
        //                                        }
        //                                        catch (Exception)
        //                                        {
        //                                            dr["StartEndDate"] = "";
        //                                        }
        //                                    }
        //                                    SListOrgCodeShiftDesc.Add(OrgCode, ShiftDesc);
        //                                    SListOrgCodeStartDate.Add(OrgCode, StartDate);
        //                                    SListOrgCodeEndDate.Add(OrgCode, EndDate);
        //                                }
        //                                else
        //                                {
        //                                    SListOrgCodeShiftDesc.Add(OrgCode, "");
        //                                    SListOrgCodeStartDate.Add(OrgCode, "");
        //                                    SListOrgCodeEndDate.Add(OrgCode, "");
        //                                }
        //                            }
        //                            else
        //                            {
        //                                SListOrgCodeShiftDesc.Add(OrgCode, "");
        //                                SListOrgCodeStartDate.Add(OrgCode, "");
        //                                SListOrgCodeEndDate.Add(OrgCode, "");
        //                            }
        //                        }
        //                    }
        //                    else
        //                    {
        //                        try
        //                        {
        //                            dr["StartEndDate"] = Convert.ToDateTime(dr["StartDate"]).ToString(base.dateFormat) + "~" + Convert.ToDateTime(dr["EndDate"]).ToString(base.dateFormat);
        //                        }
        //                        catch (Exception)
        //                        {
        //                            dr["StartEndDate"] = "";
        //                        }
        //                    }
        //                }
        //                this.tempDataTable = this.dataSet.Tables["KQM_ShiftQuery"];
        //                HttpResponse res = this.Page.Response;
        //                res.Clear();
        //                res.Buffer = true;
        //                res.AppendHeader("Content-Disposition", "attachment;filename=KQMEmpShift.xls");
        //                res.Charset = "UTF-8";
        //                res.ContentEncoding = Encoding.Default;
        //                res.ContentType = "application/ms-excel";
        //                this.EnableViewState = false;
        //                string colHeaders = "";
        //                string ls_item = "";
        //                for (int iLoop = 0; iLoop < this.UltraWebGridShiftQuery.Columns.Count; iLoop++)
        //                {
        //                    if (!this.UltraWebGridShiftQuery.Columns[iLoop].Hidden)
        //                    {
        //                        colHeaders = colHeaders + this.UltraWebGridShiftQuery.Columns[iLoop].Header.Caption + "\t";
        //                    }
        //                }
        //                res.Write(colHeaders);
        //                for (int i = 0; i < this.tempDataTable.Rows.Count; i++)
        //                {
        //                    ls_item = "\n";
        //                    for (int iLop = 0; iLop < this.UltraWebGridShiftQuery.Columns.Count; iLop++)
        //                    {
        //                        if (!this.UltraWebGridShiftQuery.Columns[iLop].Hidden)
        //                        {
        //                            ls_item = ls_item + this.tempDataTable.Rows[i][this.UltraWebGridShiftQuery.Columns[iLop].Key].ToString() + "\t";
        //                        }
        //                    }
        //                    res.Write(ls_item);
        //                    ls_item = "";
        //                }
        //                res.End();
        //            }
        //            catch (Exception exception5)
        //            {
        //                ex = exception5;
        //                base.WriteMessage(2, (ex.InnerException == null) ? ex.Message : ex.InnerException.Message);
        //            }
        //        }
        //    }
        //    else
        //    {
        //        try
        //        {
        //            this.UltraWebGridExcelExporter.DownloadName = "EmployeeShifError.xls";
        //            this.UltraWebGridExcelExporter.Export(this.UltraWebGridImport);
        //        }
        //        catch (Exception exception6)
        //        {
        //            ex = exception6;
        //            base.WriteMessage(2, (ex.InnerException == null) ? ex.Message : ex.InnerException.Message);
        //        }
        //    }
        //}



    }
}
