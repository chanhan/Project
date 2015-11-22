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
using Resources;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;
using System.IO;
using GDSBG.MiABU.Attendance.Model.KQM.KaoQinData;
using System.Collections.Generic;

namespace GDSBG.MiABU.Attendance.Web.Hr.KQM.KaoQinData
{
    public partial class KQMLeaveApplyImportForm : BasePage
    {
        KQMLeaveApplyForm_ZBLHBll leaveApply = new KQMLeaveApplyForm_ZBLHBll();

        static SynclogModel logmodel = new SynclogModel();
        static DataTable dt = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                logmodel.ProcessOwner = CurrentUserInfo.Personcode;
                logmodel.TransactionType = Request.QueryString["ModuleCode"] == null ? "" : Request.QueryString["ModuleCode"].ToString();
                logmodel.LevelNo = "2";
                logmodel.FromHost = Request.UserHostAddress;
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
            string tableName = "gds_att_leaveapply_temp";
            string[] columnProperties = { "WORKNO", "LVTYPECODE", "STARTDATE", "STARTTIME", "ENDDATE", "ENDTIME", "LVTOTAL", "REASON", "PROXY", "REMARK", "APPLYTYPE" };
            string[] columnType = { "varchar", "varchar", "varchar", "varchar", "varchar", "varchar", "varchar", "varchar", "varchar", "varchar", "varchar" };
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
                //    List<LeaveApplyTempModel> list = new List<LeaveApplyTempModel>();
                DataTable newdt = leaveApply.ImpoertExcel(createUser, Request.QueryString["ModuleCode"].ToString(), out successnum, out errornum, logmodel);
                lbllblupload.Text = "上傳成功筆數：" + successnum + ",上傳失敗筆數：" + errornum;
                this.UltraWebGridImport.DataSource = changeError(newdt);
                this.UltraWebGridImport.DataBind();
                dt = newdt;
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
                            case "ErrWorkNoNull": errorInfo = errorInfo + Message.ErrWorkNotNull + Message.ErrorInformationSlipt;
                                break;
                            case "WorkNoIsWrong": errorInfo = errorInfo + Message.WorkNoIsWrong + Message.ErrorInformationSlipt;
                                break;
                            case "ErrLeaveTypeNull": errorInfo = errorInfo + Message.ErrLeaveTypeNull + Message.ErrorInformationSlipt;
                                break;
                            case "ErrStartDateNull": errorInfo = errorInfo + Message.ErrStartDateNull + Message.ErrorInformationSlipt;
                                break;
                            case "ErrStartDateNotRight": errorInfo = errorInfo + Message.ErrStartDateNotRight + Message.ErrorInformationSlipt;
                                break;
                            case "ErrStartTimeNull": errorInfo = errorInfo + Message.ErrStartTimeNull + Message.ErrorInformationSlipt;
                                break;
                            case "ErrStartTimeNotRight": errorInfo = errorInfo + Message.ErrStartTimeNotRight + Message.ErrorInformationSlipt;
                                break;
                            case "ErrEndDateNull": errorInfo = errorInfo + Message.ErrEndDateNull + Message.ErrorInformationSlipt;
                                break;
                            case "ErrEndDateNotRight": errorInfo = errorInfo + Message.ErrEndDateNotRight + Message.ErrorInformationSlipt;
                                break;
                            case "ErrEndTimeNull": errorInfo = errorInfo + Message.ErrEndTimeNull + Message.ErrorInformationSlipt;
                                break;
                            case "ErrEndTimeNotRight": errorInfo = errorInfo + Message.ErrEndTimeNotRight + Message.ErrorInformationSlipt;
                                break;
                            case "ErrReasonNull": errorInfo = errorInfo + Message.ErrReasonNull + Message.ErrorInformationSlipt;
                                break;
                            case "ErrApplyTypeNull": errorInfo = errorInfo + Message.ErrApplyTypeNull + Message.ErrorInformationSlipt;
                                break;
                            case "ErrLeaveTotal": errorInfo = errorInfo + Message.ErrLeaveTotal + Message.ErrorInformationSlipt;
                                break;
                            case "ErrLeaveTotalNumber": errorInfo = errorInfo + Message.ErrLeaveTotalNumber + Message.ErrorInformationSlipt;
                                break;
                            case "LeaveDateAndOutDate": errorInfo = errorInfo + Message.LeaveDateAndOutDate + Message.ErrorInformationSlipt;
                                break;
                            case "StartDateAndEndDate": errorInfo = errorInfo + Message.StartDateAndEndDate + Message.ErrorInformationSlipt;
                                break;
                            case "ShiftNoNotSame": errorInfo = errorInfo + Message.ShiftNoNotSame + Message.ErrorInformationSlipt;
                                break;
                            case "LeaveDaySame": errorInfo = errorInfo + Message.LeaveDaySame + Message.ErrorInformationSlipt;
                                break;
                            case "ErrorWorNoOrAccessWorkNo": errorInfo = errorInfo + Message.ErrorWorNoOrAccessWorkNo + Message.ErrorInformationSlipt;
                                break;
                            case "NoAuthorityForLeaveType": errorInfo = errorInfo + Message.NoAuthorityForLeaveType + Message.ErrorInformationSlipt;
                                break;
                            case "ErrorLeaveType": errorInfo = errorInfo + Message.ErrorLeaveType + Message.ErrorInformationSlipt;
                                break;
                            case "ErrorApplyType": errorInfo = errorInfo + Message.ErrorApplyType + Message.ErrorInformationSlipt;
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
        protected void btnExport_Click(object sender, EventArgs e)
        {
            if (dt.Rows.Count > 0)
            {
                List<LeaveApplyTempModel> list = new List<LeaveApplyTempModel>();
                list = leaveApply.changList(dt);
                string[] header = { ControlText.gvErrorMsg, ControlText.gvWorkNo, 
                                  ControlText.gvLVTypeName, ControlText.gvStartDate, 
                                  ControlText.gvStartTime, ControlText.gvgvEndDate, 
                                   ControlText.gvEndTime, ControlText.gvgvLVTotal,
                                   ControlText.gvReason, ControlText.gvProxy, 
                                   ControlText.gvRemark, ControlText.gvApplyTypeName};
                string[] properties = { "ErrorMsg","WorkNo", "LvTypeCode", "StartDate", "StartTime", "EndDate", "EndTime",
                                          "LvTotal", "Reason", "Proxy", "Remark","ApplyType"};
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
}
