using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Resources;
using Infragistics.WebUI.WebSchedule;
using System.Data;
using System.Collections;
using System.IO;
using NPOI.HSSF.UserModel;
using System.Text;
using NPOI.SS.UserModel;
using GDSBG.MiABU.Attendance.BLL.WorkFlow;
using GDSBG.MiABU.Attendance.BLL.SystemManage.SystemSafety;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;
using Resources;

namespace GDSBG.MiABU.Attendance.Web.WorkFlow
{
    public partial class WorkFlowImportForm : BasePage
    {
        protected DataSet dataSet;
        protected DataSet tempDataSet;
        protected DataTable tempDataTable;
        private WorkFlowSetBll wfset = new WorkFlowSetBll();
        private PersonBll person = new PersonBll();

        protected void Page_Load(object sender, EventArgs e)
        {

        }
        

        protected void ButtonExport_Click(object sender, EventArgs e)
        {
            try
            {
                this.UltraWebGridExcelExporter.DownloadName = "WFMBillAuditImport.xls";
                this.UltraWebGridExcelExporter.Export(this.UltraWebGridImport);
            }
            catch (Exception ex)
            {
                //WriteMessage(2, (ex.InnerException == null) ? ex.Message : ex.InnerException.Message);
            }
        }

        /// <summary>
        /// 獲取導入的文件名,并將文件保存到服務器
        /// </summary>
        /// <returns></returns>
        private string GetImpFileName()
        {
            string filePath = string.Empty;
            if (FileUpload.FileName.Trim() != string.Empty)
            {
                try
                {
                    filePath = MapPath("~/ExportFileTemp/") + DateTime.Now.ToString("yyyyMMddHHmmss") + Path.GetFileName(FileUpload.FileName);
                    string strFileExt = FileUpload.FileName.Substring(FileUpload.FileName.LastIndexOf(".") + 1);
                    if (strFileExt == "xls")
                    {
                        FileUpload.SaveAs(filePath);
                    }

                }
                catch
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('" + Message.FailToUpload + "')</script>");
                    //lblupload.Text = "Exel上傳到服務器失敗!";
                    filePath = string.Empty;
                }
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('" + Message.PathIsNull + "')</script>");
                //lblupload.Text = "導入路徑為空,請選擇要匯入的Excel文件！";
            }
            return filePath;
        }

        protected void ButtonImportSave_Click(object sender, EventArgs e)
        {
            try
            {              
                string strFilePath = GetImpFileName();
                if (strFilePath == string.Empty)
                {
                    return;
                }
                this.dataSet = new DataSet();
                this.dataSet.Clear();
                this.dataSet.Tables.Add("WFM_Import");
                this.dataSet.Tables["WFM_Import"].Columns.Add(new DataColumn("ErrorMsg", typeof(string)));
                this.dataSet.Tables["WFM_Import"].Columns.Add(new DataColumn("orgcode", typeof(string)));
                this.dataSet.Tables["WFM_Import"].Columns.Add(new DataColumn("orgname", typeof(string)));
                this.dataSet.Tables["WFM_Import"].Columns.Add(new DataColumn("billtypename", typeof(string)));

                this.dataSet.Tables["WFM_Import"].Columns.Add(new DataColumn("Overtimetype", typeof(string)));

                this.dataSet.Tables["WFM_Import"].Columns.Add(new DataColumn("LeaveDays", typeof(string)));
                this.dataSet.Tables["WFM_Import"].Columns.Add(new DataColumn("Shiwei", typeof(string)));
                this.dataSet.Tables["WFM_Import"].Columns.Add(new DataColumn("Manger", typeof(string)));
                this.dataSet.Tables["WFM_Import"].Columns.Add(new DataColumn("leaveType", typeof(string)));

                this.dataSet.Tables["WFM_Import"].Columns.Add(new DataColumn("outtype", typeof(string)));
                this.dataSet.Tables["WFM_Import"].Columns.Add(new DataColumn("outtypeDays", typeof(string)));

                this.dataSet.Tables["WFM_Import"].Columns.Add(new DataColumn("auditman1", typeof(string)));
                this.dataSet.Tables["WFM_Import"].Columns.Add(new DataColumn("localname1", typeof(string)));
                this.dataSet.Tables["WFM_Import"].Columns.Add(new DataColumn("audittypename1", typeof(string)));
                this.dataSet.Tables["WFM_Import"].Columns.Add(new DataColumn("auditmantype1", typeof(string)));
                this.dataSet.Tables["WFM_Import"].Columns.Add(new DataColumn("auditman2", typeof(string)));
                this.dataSet.Tables["WFM_Import"].Columns.Add(new DataColumn("localname2", typeof(string)));
                this.dataSet.Tables["WFM_Import"].Columns.Add(new DataColumn("audittypename2", typeof(string)));
                this.dataSet.Tables["WFM_Import"].Columns.Add(new DataColumn("auditmantype2", typeof(string)));
                this.dataSet.Tables["WFM_Import"].Columns.Add(new DataColumn("auditman3", typeof(string)));
                this.dataSet.Tables["WFM_Import"].Columns.Add(new DataColumn("localname3", typeof(string)));
                this.dataSet.Tables["WFM_Import"].Columns.Add(new DataColumn("audittypename3", typeof(string)));
                this.dataSet.Tables["WFM_Import"].Columns.Add(new DataColumn("auditmantype3", typeof(string)));
                this.dataSet.Tables["WFM_Import"].Columns.Add(new DataColumn("auditman4", typeof(string)));
                this.dataSet.Tables["WFM_Import"].Columns.Add(new DataColumn("localname4", typeof(string)));
                this.dataSet.Tables["WFM_Import"].Columns.Add(new DataColumn("audittypename4", typeof(string)));
                this.dataSet.Tables["WFM_Import"].Columns.Add(new DataColumn("auditmantype4", typeof(string)));
                this.dataSet.Tables["WFM_Import"].Columns.Add(new DataColumn("auditman5", typeof(string)));
                this.dataSet.Tables["WFM_Import"].Columns.Add(new DataColumn("localname5", typeof(string)));
                this.dataSet.Tables["WFM_Import"].Columns.Add(new DataColumn("audittypename5", typeof(string)));
                this.dataSet.Tables["WFM_Import"].Columns.Add(new DataColumn("auditmantype5", typeof(string)));
                this.dataSet.Tables["WFM_Import"].Columns.Add(new DataColumn("auditman6", typeof(string)));
                this.dataSet.Tables["WFM_Import"].Columns.Add(new DataColumn("localname6", typeof(string)));
                this.dataSet.Tables["WFM_Import"].Columns.Add(new DataColumn("audittypename6", typeof(string)));
                this.dataSet.Tables["WFM_Import"].Columns.Add(new DataColumn("auditmantype6", typeof(string)));
                this.dataSet.Tables["WFM_Import"].Columns.Add(new DataColumn("auditman7", typeof(string)));
                this.dataSet.Tables["WFM_Import"].Columns.Add(new DataColumn("localname7", typeof(string)));
                this.dataSet.Tables["WFM_Import"].Columns.Add(new DataColumn("audittypename7", typeof(string)));
                this.dataSet.Tables["WFM_Import"].Columns.Add(new DataColumn("auditmantype7", typeof(string)));
                this.dataSet.Tables["WFM_Import"].Columns.Add(new DataColumn("auditman8", typeof(string)));
                this.dataSet.Tables["WFM_Import"].Columns.Add(new DataColumn("localname8", typeof(string)));
                this.dataSet.Tables["WFM_Import"].Columns.Add(new DataColumn("audittypename8", typeof(string)));
                this.dataSet.Tables["WFM_Import"].Columns.Add(new DataColumn("auditmantype8", typeof(string)));
                this.dataSet.Tables["WFM_Import"].Columns.Add(new DataColumn("auditman9", typeof(string)));
                this.dataSet.Tables["WFM_Import"].Columns.Add(new DataColumn("localname9", typeof(string)));
                this.dataSet.Tables["WFM_Import"].Columns.Add(new DataColumn("audittypename9", typeof(string)));
                this.dataSet.Tables["WFM_Import"].Columns.Add(new DataColumn("auditmantype9", typeof(string)));
                this.dataSet.Tables["WFM_Import"].Columns.Add(new DataColumn("auditman10", typeof(string)));
                this.dataSet.Tables["WFM_Import"].Columns.Add(new DataColumn("localname10", typeof(string)));
                this.dataSet.Tables["WFM_Import"].Columns.Add(new DataColumn("audittypename10", typeof(string)));
                this.dataSet.Tables["WFM_Import"].Columns.Add(new DataColumn("auditmantype10", typeof(string)));
                this.dataSet.Tables["WFM_Import"].Columns.Add(new DataColumn("auditman11", typeof(string)));
                this.dataSet.Tables["WFM_Import"].Columns.Add(new DataColumn("localname11", typeof(string)));
                this.dataSet.Tables["WFM_Import"].Columns.Add(new DataColumn("audittypename11", typeof(string)));
                this.dataSet.Tables["WFM_Import"].Columns.Add(new DataColumn("auditmantype11", typeof(string)));
                this.dataSet.Tables["WFM_Import"].Columns.Add(new DataColumn("auditman12", typeof(string)));
                this.dataSet.Tables["WFM_Import"].Columns.Add(new DataColumn("localname12", typeof(string)));
                this.dataSet.Tables["WFM_Import"].Columns.Add(new DataColumn("audittypename12", typeof(string)));
                this.dataSet.Tables["WFM_Import"].Columns.Add(new DataColumn("auditmantype12", typeof(string)));
                this.dataSet.Tables["WFM_Import"].Columns.Add(new DataColumn("auditman13", typeof(string)));
                this.dataSet.Tables["WFM_Import"].Columns.Add(new DataColumn("localname13", typeof(string)));
                this.dataSet.Tables["WFM_Import"].Columns.Add(new DataColumn("audittypename13", typeof(string)));
                this.dataSet.Tables["WFM_Import"].Columns.Add(new DataColumn("auditmantype13", typeof(string)));
                this.dataSet.Tables["WFM_Import"].Columns.Add(new DataColumn("auditman14", typeof(string)));
                this.dataSet.Tables["WFM_Import"].Columns.Add(new DataColumn("localname14", typeof(string)));
                this.dataSet.Tables["WFM_Import"].Columns.Add(new DataColumn("audittypename14", typeof(string)));
                this.dataSet.Tables["WFM_Import"].Columns.Add(new DataColumn("auditmantype14", typeof(string)));
                this.dataSet.Tables["WFM_Import"].Columns.Add(new DataColumn("auditman15", typeof(string)));
                this.dataSet.Tables["WFM_Import"].Columns.Add(new DataColumn("localname15", typeof(string)));
                this.dataSet.Tables["WFM_Import"].Columns.Add(new DataColumn("audittypename15", typeof(string)));
                this.dataSet.Tables["WFM_Import"].Columns.Add(new DataColumn("auditmantype15", typeof(string)));
                this.dataSet.Tables["WFM_Import"].Columns.Add(new DataColumn("auditman16", typeof(string)));
                this.dataSet.Tables["WFM_Import"].Columns.Add(new DataColumn("localname16", typeof(string)));
                this.dataSet.Tables["WFM_Import"].Columns.Add(new DataColumn("audittypename16", typeof(string)));
                this.dataSet.Tables["WFM_Import"].Columns.Add(new DataColumn("auditmantype16", typeof(string)));
                this.dataSet.Tables["WFM_Import"].Columns.Add(new DataColumn("auditman17", typeof(string)));
                this.dataSet.Tables["WFM_Import"].Columns.Add(new DataColumn("localname17", typeof(string)));
                this.dataSet.Tables["WFM_Import"].Columns.Add(new DataColumn("audittypename17", typeof(string)));
                this.dataSet.Tables["WFM_Import"].Columns.Add(new DataColumn("auditmantype17", typeof(string)));
                this.dataSet.Tables["WFM_Import"].Columns.Add(new DataColumn("auditman18", typeof(string)));
                this.dataSet.Tables["WFM_Import"].Columns.Add(new DataColumn("localname18", typeof(string)));
                this.dataSet.Tables["WFM_Import"].Columns.Add(new DataColumn("audittypename18", typeof(string)));
                this.dataSet.Tables["WFM_Import"].Columns.Add(new DataColumn("auditmantype18", typeof(string)));
                this.dataSet.Tables["WFM_Import"].Columns.Add(new DataColumn("auditman19", typeof(string)));
                this.dataSet.Tables["WFM_Import"].Columns.Add(new DataColumn("localname19", typeof(string)));
                this.dataSet.Tables["WFM_Import"].Columns.Add(new DataColumn("audittypename19", typeof(string)));
                this.dataSet.Tables["WFM_Import"].Columns.Add(new DataColumn("auditmantype19", typeof(string)));
                this.dataSet.Tables["WFM_Import"].Columns.Add(new DataColumn("auditman20", typeof(string)));
                this.dataSet.Tables["WFM_Import"].Columns.Add(new DataColumn("localname20", typeof(string)));
                this.dataSet.Tables["WFM_Import"].Columns.Add(new DataColumn("audittypename20", typeof(string)));
                this.dataSet.Tables["WFM_Import"].Columns.Add(new DataColumn("auditmantype20", typeof(string)));
                string OrgCode = string.Empty;
                string OrgName = string.Empty;
                string BillTypeName = string.Empty;
                string errorMsg = string.Empty;

                string Overtimetype = string.Empty;

                string LeaveDays = string.Empty;
                string Shiwei = string.Empty;
                string Manger = string.Empty;
                string leaveType = string.Empty;

                string outtype = string.Empty;
                string outtypeDays = string.Empty;

                string[,] AuditMan = new string[20, 4];
                string WorkNo = string.Empty;
                string AuditTypeName = string.Empty;
              
                string AuditManType = string.Empty;
                string AuditMan1 = string.Empty;
                string LocalName1 = string.Empty;
                string AuditTypeName1 = string.Empty;
                string AuditManType1 = string.Empty;
                string AuditMan2 = string.Empty;
                string LocalName2 = string.Empty;
                string AuditTypeName2 = string.Empty;
                string AuditManType2 = string.Empty;
                string AuditMan3 = string.Empty;
                string LocalName3 = string.Empty;
                string AuditTypeName3 = string.Empty;
                string AuditManType3 = string.Empty;
                string AuditMan4 = string.Empty;
                string LocalName4 = string.Empty;
                string AuditTypeName4 = string.Empty;
                string AuditManType4 = string.Empty;
                string AuditMan5 = string.Empty;
                string LocalName5 = string.Empty;
                string AuditTypeName5 = string.Empty;
                string AuditManType5 = string.Empty;
                string AuditMan6 = string.Empty;
                string LocalName6 = string.Empty;
                string AuditTypeName6 = string.Empty;
                string AuditManType6 = string.Empty;
                string AuditMan7 = string.Empty;
                string LocalName7 = string.Empty;
                string AuditTypeName7 = string.Empty;
                string AuditManType7 = string.Empty;
                string AuditMan8 = string.Empty;
                string LocalName8 = string.Empty;
                string AuditTypeName8 = string.Empty;
                string AuditManType8 = string.Empty;
                string AuditMan9 = string.Empty;
                string LocalName9 = string.Empty;
                string AuditTypeName9 = string.Empty;
                string AuditManType9 = string.Empty;
                string AuditMan10 = string.Empty;
                string LocalName10 = string.Empty;
                string AuditTypeName10 = string.Empty;
                string AuditManType10 = string.Empty;
                string AuditMan11 = string.Empty;
                string LocalName11 = string.Empty;
                string AuditTypeName11 = string.Empty;
                string AuditManType11 = string.Empty;
                string AuditMan12 = string.Empty;
                string LocalName12 = string.Empty;
                string AuditTypeName12 = string.Empty;
                string AuditManType12 = string.Empty;
                string AuditMan13 = string.Empty;
                string LocalName13 = string.Empty;
                string AuditTypeName13 = string.Empty;
                string AuditManType13 = string.Empty;
                string AuditMan14 = string.Empty;
                string LocalName14 = string.Empty;
                string AuditTypeName14 = string.Empty;
                string AuditManType14 = string.Empty;
                string AuditMan15 = string.Empty;
                string LocalName15 = string.Empty;
                string AuditTypeName15 = string.Empty;
                string AuditManType15 = string.Empty;
                string AuditMan16 = string.Empty;
                string LocalName16 = string.Empty;
                string AuditTypeName16 = string.Empty;
                string AuditManType16 = string.Empty;
                string AuditMan17 = string.Empty;
                string LocalName17 = string.Empty;
                string AuditTypeName17 = string.Empty;
                string AuditManType17 = string.Empty;
                string AuditMan18 = string.Empty;
                string LocalName18 = string.Empty;
                string AuditTypeName18 = string.Empty;
                string AuditManType18 = string.Empty;
                string AuditMan19 = string.Empty;
                string LocalName19 = string.Empty;
                string AuditTypeName19 = string.Empty;
                string AuditManType19 = string.Empty;
                string AuditMan20 = string.Empty;
                string LocalName20 = string.Empty;
                string AuditTypeName20 = string.Empty;
                string AuditManType20 = string.Empty;
                HSSFWorkbook hssfworkbook;
                StringBuilder sb = new StringBuilder(20000);
                using (Stream fileStream = new FileStream(strFilePath, FileMode.Open))
                {
                    hssfworkbook = new HSSFWorkbook(fileStream);
                }
                Sheet insertSheet = hssfworkbook.GetSheet("Sheet1");
                int nRows = 1;
                int index = 0;
                int errorCount = 0;
                while (insertSheet.GetRow(nRows) != null)
                {

                    try
                    {
                        Row dr = insertSheet.GetRow(nRows);

                        OrgCode = dr.GetCell(0) == null ? string.Empty : dr.GetCell(0).ToString().Trim();
                        OrgName = dr.GetCell(1) == null ? string.Empty : dr.GetCell(1).ToString().Trim();
                        BillTypeName = dr.GetCell(2) == null ? string.Empty : dr.GetCell(2).ToString().Trim();
                        Overtimetype = dr.GetCell(3) == null ? string.Empty : dr.GetCell(3).ToString().Trim();
                        LeaveDays = dr.GetCell(4) == null ? string.Empty : dr.GetCell(4).ToString().Trim();
                        if (LeaveDays != string.Empty)
                        {
                            LeaveDays = LeaveDays.Substring(0, LeaveDays.IndexOf("["));
                        }
                        Shiwei = dr.GetCell(5) == null ? string.Empty : dr.GetCell(5).ToString().Trim();
                        if (Shiwei != string.Empty)
                        {
                            Shiwei = Shiwei.Substring(0, Shiwei.IndexOf("["));
                        }
                        Manger = dr.GetCell(6) == null ? string.Empty : dr.GetCell(6).ToString().Trim();
                        if (Manger != string.Empty)
                        {
                            Manger = Manger.Substring(0, Manger.IndexOf("["));
                        }
                        leaveType = dr.GetCell(7) == null ? string.Empty : dr.GetCell(7).ToString().Trim();
                        if (leaveType != string.Empty)
                        {
                            leaveType = leaveType.Substring(0, leaveType.IndexOf("["));
                        }
                        outtype = dr.GetCell(8) == null ? string.Empty : dr.GetCell(8).ToString().Trim();
                        if (outtype != string.Empty)
                        {
                            outtype = outtype.Substring(0, outtype.IndexOf("["));
                        }
                        outtypeDays = dr.GetCell(9) == null ? string.Empty : dr.GetCell(9).ToString().Trim();
                        if (outtypeDays != string.Empty)
                        {
                            outtypeDays = outtypeDays.Substring(0, outtypeDays.IndexOf("["));
                        }
                        AuditMan1 = dr.GetCell(10) == null ? string.Empty : dr.GetCell(10).ToString().Trim();
                        AuditMan1 = dr.GetCell(11) == null ? string.Empty : dr.GetCell(11).ToString().Trim();
                        LocalName1 = dr.GetCell(12) == null ? string.Empty : dr.GetCell(12).ToString().Trim();
                        AuditTypeName1 = dr.GetCell(13) == null ? string.Empty : dr.GetCell(13).ToString().Trim();
                        AuditManType1 = dr.GetCell(14) == null ? string.Empty : dr.GetCell(14).ToString().Trim();
                        AuditMan2 = dr.GetCell(15) == null ? string.Empty : dr.GetCell(15).ToString().Trim();
                        LocalName2 = dr.GetCell(16) == null ? string.Empty : dr.GetCell(16).ToString().Trim();
                        AuditTypeName2 = dr.GetCell(17) == null ? string.Empty : dr.GetCell(17).ToString().Trim();
                        AuditManType2 = dr.GetCell(18) == null ? string.Empty : dr.GetCell(18).ToString().Trim();
                        AuditMan3 = dr.GetCell(19) == null ? string.Empty : dr.GetCell(19).ToString().Trim();
                        LocalName3 = dr.GetCell(20) == null ? string.Empty : dr.GetCell(20).ToString().Trim();
                        AuditTypeName3 = dr.GetCell(21) == null ? string.Empty : dr.GetCell(21).ToString().Trim();
                        AuditManType3 = dr.GetCell(22) == null ? string.Empty : dr.GetCell(22).ToString().Trim();
                        AuditMan4 = dr.GetCell(23) == null ? string.Empty : dr.GetCell(23).ToString().Trim();
                        LocalName4 = dr.GetCell(24) == null ? string.Empty : dr.GetCell(24).ToString().Trim();
                        AuditTypeName4 = dr.GetCell(25) == null ? string.Empty : dr.GetCell(25).ToString().Trim();
                        AuditManType4 = dr.GetCell(26) == null ? string.Empty : dr.GetCell(26).ToString().Trim();
                        AuditMan5 = dr.GetCell(27) == null ? string.Empty : dr.GetCell(27).ToString().Trim();
                        LocalName5 = dr.GetCell(28) == null ? string.Empty : dr.GetCell(28).ToString().Trim();
                        AuditTypeName5 = dr.GetCell(29) == null ? string.Empty : dr.GetCell(29).ToString().Trim();
                        AuditManType5 = dr.GetCell(30) == null ? string.Empty : dr.GetCell(30).ToString().Trim();
                        AuditMan6 = dr.GetCell(31) == null ? string.Empty : dr.GetCell(31).ToString().Trim();
                        LocalName6 = dr.GetCell(32) == null ? string.Empty : dr.GetCell(32).ToString().Trim();
                        AuditTypeName6 = dr.GetCell(33) == null ? string.Empty : dr.GetCell(33).ToString().Trim();
                        AuditManType6 = dr.GetCell(34) == null ? string.Empty : dr.GetCell(34).ToString().Trim();
                        AuditMan7 = dr.GetCell(35) == null ? string.Empty : dr.GetCell(35).ToString().Trim();
                        LocalName7 = dr.GetCell(36) == null ? string.Empty : dr.GetCell(36).ToString().Trim();
                        AuditTypeName7 = dr.GetCell(37) == null ? string.Empty : dr.GetCell(37).ToString().Trim();
                        AuditManType7 = dr.GetCell(38) == null ? string.Empty : dr.GetCell(38).ToString().Trim();
                        AuditMan8 = dr.GetCell(39) == null ? string.Empty : dr.GetCell(39).ToString().Trim();
                        LocalName8 = dr.GetCell(40) == null ? string.Empty : dr.GetCell(40).ToString().Trim();
                        AuditTypeName8 = dr.GetCell(41) == null ? string.Empty : dr.GetCell(41).ToString().Trim();
                        AuditManType8 = dr.GetCell(42) == null ? string.Empty : dr.GetCell(42).ToString().Trim();
                        AuditMan9 = dr.GetCell(43) == null ? string.Empty : dr.GetCell(43).ToString().Trim();
                        LocalName9 = dr.GetCell(44) == null ? string.Empty : dr.GetCell(44).ToString().Trim();
                        AuditTypeName9 = dr.GetCell(45) == null ? string.Empty : dr.GetCell(45).ToString().Trim();
                        AuditManType9 = dr.GetCell(46) == null ? string.Empty : dr.GetCell(46).ToString().Trim();
                        AuditMan10 = dr.GetCell(47) == null ? string.Empty : dr.GetCell(47).ToString().Trim();
                        LocalName10 = dr.GetCell(48) == null ? string.Empty : dr.GetCell(48).ToString().Trim();
                        AuditTypeName10 = dr.GetCell(49) == null ? string.Empty : dr.GetCell(49).ToString().Trim();
                        AuditManType10 = dr.GetCell(50) == null ? string.Empty : dr.GetCell(50).ToString().Trim();
                        AuditMan11 = dr.GetCell(51) == null ? string.Empty : dr.GetCell(51).ToString().Trim();
                        LocalName11 = dr.GetCell(52) == null ? string.Empty : dr.GetCell(52).ToString().Trim();
                        AuditTypeName11 = dr.GetCell(53) == null ? string.Empty : dr.GetCell(53).ToString().Trim();
                        AuditManType11 = dr.GetCell(54) == null ? string.Empty : dr.GetCell(54).ToString().Trim();
                        AuditMan12 = dr.GetCell(55) == null ? string.Empty : dr.GetCell(55).ToString().Trim();
                        LocalName12 = dr.GetCell(56) == null ? string.Empty : dr.GetCell(56).ToString().Trim();
                        AuditTypeName12 = dr.GetCell(57) == null ? string.Empty : dr.GetCell(57).ToString().Trim();
                        AuditManType12 = dr.GetCell(58) == null ? string.Empty : dr.GetCell(58).ToString().Trim();
                        AuditMan13 = dr.GetCell(59) == null ? string.Empty : dr.GetCell(59).ToString().Trim();
                        LocalName13 = dr.GetCell(60) == null ? string.Empty : dr.GetCell(60).ToString().Trim();
                        AuditTypeName13 = dr.GetCell(61) == null ? string.Empty : dr.GetCell(61).ToString().Trim();
                        AuditManType13 = dr.GetCell(62) == null ? string.Empty : dr.GetCell(62).ToString().Trim();
                        AuditMan14 = dr.GetCell(63) == null ? string.Empty : dr.GetCell(63).ToString().Trim();
                        LocalName14 = dr.GetCell(64) == null ? string.Empty : dr.GetCell(64).ToString().Trim();
                        AuditTypeName14 = dr.GetCell(65) == null ? string.Empty : dr.GetCell(65).ToString().Trim();
                        AuditManType14 = dr.GetCell(66) == null ? string.Empty : dr.GetCell(66).ToString().Trim();
                        AuditMan15 = dr.GetCell(67) == null ? string.Empty : dr.GetCell(67).ToString().Trim();
                        LocalName15 = dr.GetCell(68) == null ? string.Empty : dr.GetCell(68).ToString().Trim();
                        AuditTypeName15 = dr.GetCell(69) == null ? string.Empty : dr.GetCell(69).ToString().Trim();
                        AuditManType15 = dr.GetCell(70) == null ? string.Empty : dr.GetCell(70).ToString().Trim();
                        AuditMan16 = dr.GetCell(71) == null ? string.Empty : dr.GetCell(71).ToString().Trim();
                        LocalName16 = dr.GetCell(72) == null ? string.Empty : dr.GetCell(72).ToString().Trim();
                        AuditTypeName16 = dr.GetCell(73) == null ? string.Empty : dr.GetCell(73).ToString().Trim();
                        AuditManType16 = dr.GetCell(74) == null ? string.Empty : dr.GetCell(74).ToString().Trim();
                        AuditMan17 = dr.GetCell(75) == null ? string.Empty : dr.GetCell(75).ToString().Trim();
                        LocalName17 = dr.GetCell(76) == null ? string.Empty : dr.GetCell(76).ToString().Trim();
                        AuditTypeName17 = dr.GetCell(77) == null ? string.Empty : dr.GetCell(77).ToString().Trim();
                        AuditManType17 = dr.GetCell(78) == null ? string.Empty : dr.GetCell(78).ToString().Trim();
                        AuditMan18 = dr.GetCell(79) == null ? string.Empty : dr.GetCell(79).ToString().Trim();
                        LocalName18 = dr.GetCell(80) == null ? string.Empty : dr.GetCell(80).ToString().Trim();
                        AuditTypeName18 = dr.GetCell(81) == null ? string.Empty : dr.GetCell(81).ToString().Trim();
                        AuditManType18 = dr.GetCell(82) == null ? string.Empty : dr.GetCell(82).ToString().Trim();
                        AuditMan19 = dr.GetCell(83) == null ? string.Empty : dr.GetCell(83).ToString().Trim();
                        LocalName19 = dr.GetCell(84) == null ? string.Empty : dr.GetCell(84).ToString().Trim();
                        AuditTypeName19 = dr.GetCell(85) == null ? string.Empty : dr.GetCell(85).ToString().Trim();
                        AuditManType19 = dr.GetCell(86) == null ? string.Empty : dr.GetCell(86).ToString().Trim();
                        AuditMan20 = dr.GetCell(87) == null ? string.Empty : dr.GetCell(87).ToString().Trim();
                        LocalName20 = dr.GetCell(88) == null ? string.Empty : dr.GetCell(88).ToString().Trim();
                        AuditTypeName20 = dr.GetCell(89) == null ? string.Empty : dr.GetCell(89).ToString().Trim();
                        AuditManType20 = dr.GetCell(90) == null ? string.Empty : dr.GetCell(90).ToString().Trim();
                    }
                    catch (Exception ex)
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('" + Message.IsNotExcel + "');</script>");
                        return;
                    }

                    int t = 0;
                    int intCount = 10;
                    while (t < 20)
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            if (insertSheet.GetRow(nRows).GetCell(intCount) != null)
                            {
                                AuditMan[t, j] = insertSheet.GetRow(nRows).GetCell(intCount).ToString().Trim();
                            }
                            else
                            {
                                AuditMan[t, j] = string.Empty;
                            }
                            intCount++;
                        }
                        t++;
                    }

                    if (OrgCode.Length > 0)
                    {

                        int i = wfset.IsExistsDeptCode(OrgCode);
                        if (i <= 0)
                        {
                            if (errorMsg.Length > 0)
                            {
                                errorMsg = errorMsg + ",";
                            }
                            errorMsg = errorMsg + Message.deptcodenotexit;
                        }

                    }
                    else
                    {
                        if (errorMsg.Length > 0)
                        {
                            errorMsg = errorMsg + ",";
                        }
                        errorMsg = errorMsg + Message.deptcodenotisnull;
                    }
                    if (BillTypeName.Length > 0)
                    {
                        switch (BillTypeName)
                        {
                            case "D001":
                            case "OTMProjectApply":
                                if (Overtimetype == string.Empty)
                                {
                                    if (errorMsg.Length > 0)
                                    {
                                        errorMsg = errorMsg + ",";
                                    }
                                    errorMsg = errorMsg + Message.overtimenotisnull;
                                }
                                break;
                            case "D002":
                                if (LeaveDays == string.Empty || Shiwei == string.Empty || Manger == string.Empty || leaveType == string.Empty)
                                {
                                    if (errorMsg.Length > 0)
                                    {
                                        errorMsg = errorMsg + ",";
                                    }
                                    errorMsg = errorMsg + Message.leaveinfonotisnull;
                                }
                                break;
                            case "D003":
                                if (outtype == string.Empty || outtypeDays == string.Empty)
                                {
                                    if (errorMsg.Length > 0)
                                    {
                                        errorMsg = errorMsg + ",";
                                    }
                                    errorMsg = errorMsg + Message.outawayisnotnull;
                                }
                                break;
                            default:
                                if (errorMsg.Length > 0)
                                {
                                    errorMsg = errorMsg + ",";
                                }
                                errorMsg = errorMsg + Message.doctypenotexit;
                                break;
                        }
                    }
                    else
                    {
                        if (errorMsg.Length > 0)
                        {
                            errorMsg = errorMsg + ",";
                        }
                        errorMsg = errorMsg + Message.doctypenotidnull;
                    }
                    Dictionary<int, List<string>> driy = new Dictionary<int, List<string>>();
                    List<string> exit = new List<string>();
                    for (t = 0; t < 20; t++)
                    {
                        WorkNo = AuditMan[t, 0].ToString();
                        AuditTypeName = AuditMan[t, 2].ToString();
                        AuditManType = AuditMan[t, 3].ToString();
                        if (WorkNo.Length <= 0)
                        {
                            break;
                        }
                        List<PersonModel> listperson = person.GetPersonUserId(WorkNo);
                        if (listperson != null && listperson.Count > 0)
                        {

                            List<string> personstr = new List<string>();
                            personstr.Add(WorkNo);
                            exit.Add(WorkNo);
                            personstr.Add(listperson[0].Cname);
                            personstr.Add(listperson[0].Mail);
                            string manger= wfset.GetManager(WorkNo);
                            //這里管理職有點問題
                            personstr.Add(manger);
                            personstr.Add(AuditManType);
                            personstr.Add(AuditTypeName);
                            driy.Add(t, personstr);
                        }

                        else
                        {
                            if (errorMsg.Length > 0)
                            {
                                errorMsg = errorMsg + ",";
                            }
                            errorMsg = errorMsg + Message.supvisor + (t + 1).ToString() + Message.ErrWorkNoNotEXIST;
                        }

                    }
                     List<string> reasonList1 = new List<string>();
                     switch (BillTypeName)
                     {
                         case "D001":
                         case "OTMProjectApply":
                             reasonList1.Add(Overtimetype);
                             break;
                         case "D002":
                             reasonList1.Add(LeaveDays);
                             reasonList1.Add(Shiwei);
                             reasonList1.Add(Manger);
                             reasonList1.Add(leaveType);
                             break;
                         case "D003":
                             reasonList1.Add(outtype);
                             reasonList1.Add(outtypeDays);
                             break;
                     }
                    WorkFlowLimitBll worklimit = new WorkFlowLimitBll();
                    DataTable dt = worklimit.GetSignLimitInfo(OrgCode, BillTypeName, reasonList1);
                    List<string> sup = new List<string>();
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        for (int l = 0; l < dt.Rows.Count; l++)
                        {
                            string temp = dt.Rows[l]["flow_empno"].ToString();
                            if (!exit.Contains(temp))
                            {
                                sup.Add(temp);
                            }
                        }
                    }
                    if (sup.Count > 0)
                    {
                        if (errorMsg.Length > 0)
                        {
                            errorMsg = errorMsg + ",";
                        }
                        string temp1 = string.Empty;
                        foreach (string item in sup)
                        {
                            temp1 += item + ",";
                        }
                        if (temp1 != string.Empty)
                        {
                            temp1 = temp1.Substring(0, temp1.Length - 1);
                        }
                        errorMsg = errorMsg + Message.supvisor + temp1 + Message.inthiswayexit;
                    }

                    if (errorMsg.Length == 0)
                    {
                        if (driy.Count > 0)
                        {
                            List<string> reasonList = new List<string>();
                            switch (BillTypeName)
                            {
                                case "D001":
                                case "OTMProjectApply":
                                    reasonList.Add(Overtimetype);
                                    break;
                                case "D002":
                                    reasonList.Add(LeaveDays);
                                    reasonList.Add(Shiwei);
                                    reasonList.Add(Manger);
                                    reasonList.Add(leaveType);                                   
                                    break;
                                case "D003":
                                    reasonList.Add(outtype);
                                    reasonList.Add(outtypeDays);
                                    break;
                            }
                            wfset.SaveData(OrgCode, BillTypeName, reasonList, driy);
                        }
                        index++;
                    }
                    else  if (errorMsg.Length > 0)
                    {                      
                        errorCount++;                        
                        this.dataSet.Tables["WFM_Import"].Rows.Add(new string[] { 
                                errorMsg, OrgCode, OrgName, BillTypeName,Overtimetype,LeaveDays,Shiwei,Manger,leaveType,outtype,outtypeDays, AuditMan1, LocalName1, AuditTypeName1, AuditManType1, AuditMan2, LocalName2, AuditTypeName2, AuditManType2, AuditMan3, LocalName3, AuditTypeName3, AuditManType3, 
                                AuditMan4, LocalName4, AuditTypeName4, AuditManType4, AuditMan5, LocalName5, AuditTypeName5, AuditManType5, AuditMan6, LocalName6, AuditTypeName6, AuditManType6, AuditMan7, LocalName7, AuditTypeName7, AuditManType7, 
                                AuditMan8, LocalName8, AuditTypeName8, AuditManType8, AuditMan9, LocalName9, AuditTypeName9, AuditManType9, AuditMan10, LocalName10, AuditTypeName10, AuditManType10,AuditMan11, LocalName11, AuditTypeName11, AuditManType11,AuditMan12, LocalName12, AuditTypeName12, AuditManType12,
                                AuditMan13, LocalName13, AuditTypeName13, AuditManType13,AuditMan14, LocalName14, AuditTypeName14, AuditManType14
                             });
                    }
                    errorMsg = string.Empty;

                   
                    if (nRows % 250 == 0 || (insertSheet.GetRow(nRows + 1) == null))
                    {
                        break;
                    }
                    nRows++;
                }
                this.labeluploadMsg.Text = string.Concat(Message.NumberOfSuccessed, index, "  ;", Message.NumberOfFailed, errorCount, " .");
                this.UltraWebGridImport.DataSource = this.dataSet.Tables["WFM_Import"].DefaultView;
                this.UltraWebGridImport.DataBind();
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('" + ex.Message + "');</script>");
            }
        }
    }
}
