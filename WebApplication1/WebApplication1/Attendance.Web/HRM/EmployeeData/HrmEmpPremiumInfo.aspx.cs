/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： HrmEmpPremiunInfo.cs
 * 檔功能描述：員工行政處分UI類
 * 
 * 版本：1.0
 * 創建標識： 劉炎 2012.03.13
 * 
 */
using System;
using System.Data;
using System.IO;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using GDSBG.MiABU.Attendance.BLL.HRM.EmployeeData;
using GDSBG.MiABU.Attendance.Model.HRM.EmployeeData;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;

namespace GDSBG.MiABU.Attendance.Web.HRM.EmployeeData
{
    /// <summary>
    /// 員工行政處分UI類
    /// </summary>
    public partial class HrmEmpPremiumInfo : BasePage
    {
        HrmEmpPremiumInfoBll PreBll = new HrmEmpPremiumInfoBll();

        #region 頁面加載
        /// <summary>
        /// 頁面加載
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.txtStartDate.Text = DateTime.Now.ToString("yyyy/MM/01");
                pager.CurrentPageIndex = 1;
                PageHelper.SetDepartmentSelector(imgbtnDeptSearch, hidDeptList, txtDeptList);
                ShowData();
                ShowDdlBind();
            }
            SetCalendar(txtStartDate, txtEndDate);
        }
        #endregion

        #region DDL數據綁定
        /// <summary>
        /// DDL數據綁定
        /// </summary>
        private void ShowDdlBind()
        {
            DataTable dt = PreBll.GetPremiumName();
            this.ddlPremiumName.DataSource = dt.DefaultView;
            this.ddlPremiumName.DataValueField = "PREMIUM_NAME";
            this.ddlPremiumName.DataTextField = "PREMIUM_NAME";
            this.ddlPremiumName.DataBind();
            this.ddlPremiumName.Items.Insert(0, new ListItem("請選擇", ""));
        }
        #endregion

        #region GridView數據綁定
        /// <summary>
        /// GridView數據綁定
        /// </summary>
        private void ShowData()
        {
            HrmEmpPremiumInfoModel model = new HrmEmpPremiumInfoModel();
            string empNoList = this.txtEmpNo.Text;
            model.EmpName = this.txtEmpName.Text;
            model.PremiumName = this.ddlPremiumName.SelectedValue;
            string deptList = this.hidDeptList.Value;
            string startDate = this.txtStartDate.Text;
            string endDate = this.txtEndDate.Text;
            string jobStatus = this.ddlJobStatus.SelectedValue;
            int totalCount = 0;
            List<HrmEmpPremiumInfoModel> list = PreBll.GetEmpPremiumInfo(empNoList, model, startDate, endDate, deptList, jobStatus, pager.CurrentPageIndex, pager.PageSize, out totalCount);
            pager.RecordCount = totalCount;
            this.UltraWebGridSupportIn.DataSource = list;
            this.UltraWebGridSupportIn.DataBind();
            pager.TextAfterPageIndexBox = "/" + pager.RecordCount.ToString();
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
            ShowData();
        }
        #endregion

        #region 導出
        /// <summary>
        /// 導出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnExport_Click(object sender, EventArgs e)
        {

        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sheet"></param>
        private void LoadPremiumInfo(Sheet sheet,Sheet contentSheet) 
        {   
            Row  titleOne = CreateCell(sheet,contentSheet,0,0);
            contentSheet.AddMergedRegion(new CellRangeAddress(0,0,0,9));
            
        }

        #region 查詢
        /// <summary>
        /// 查詢
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            pager.CurrentPageIndex = 1;
            ShowData();
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

        }
        #endregion

        #region 創建單元格
        /// <summary>
        /// 創建單元格
        /// </summary>
        /// <param name="cellSheet"></param>
        /// <param name="row"></param>
        private Row CreateCell(Sheet ySheet, Sheet cloneSheet, int row, int stRow)
        {
            Row cellRow = cloneSheet.CreateRow(row);
            cellRow.Height = ySheet.GetRow(stRow).Height;
            for (int c = 0; c <= 10; c++)
                cellRow.CreateCell(c).CellStyle = ySheet.GetRow(stRow).GetCell(c).CellStyle;
            return cellRow;
        }
        #endregion

        #region 輸出EXCEL文檔信息
        /// <summary>
        /// 輸出EXCEL文檔信息
        /// </summary>
        /// <param name="workBook"></param>
        /// <param name="filePath"></param>
        private void OutPut(HSSFWorkbook workBook, string filePath)
        {
            using (FileStream file = new FileStream(filePath, FileMode.Create))
            {
                workBook.Write(file);
                file.Flush();
                file.Close();
                workBook = null;
            }
        }
        #endregion

        #region 獲取模板
        /// <summary>
        /// 獲取模板
        /// </summary>
        /// <returns></returns>
        private HSSFWorkbook GetExcelTemplate()
        {
            string filePath = Server.MapPath("~/ExcelModel/HrmEmpPremiumExport.xls");
            HSSFWorkbook hssfworkbook;

            //獲取模板
            using (Stream fileStream = new FileStream(filePath, FileMode.Open))
            {
                hssfworkbook = new HSSFWorkbook(fileStream);
            }
            return hssfworkbook;
        }
        #endregion
    }
}
