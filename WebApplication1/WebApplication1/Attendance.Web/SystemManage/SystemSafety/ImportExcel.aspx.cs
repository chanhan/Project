/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名：  ImportExcel.cs
 * 檔功能描述： EXCEL導入數據庫
 * 
 * 版本：1.0
 * 創建標識： 劉炎 2011.12.13
 * 
 */

using System;
using System.IO;


namespace GDSBG.MiABU.Attendance.Web.SystemManage.SystemSafety
{
    public partial class ImportExcel : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        #region 導入
        /// <summary>
        /// 導入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnImportExcel_Click(object sender, EventArgs e)
        {
            ImportModuleExcel();
        }
        #endregion

        #region 將文件中的數據導入到數據庫中
        /// <summary>
        /// 將文件中的數據導入到數據庫中
        /// </summary>
        private void ImportModuleExcel()
        {
            int flag;
            bool deFlag;
            string tableName = "gds_sc_temp";
            string[] columnProperties = { "description1", "orderid","jiang_you" };//需要導入數據庫的欄位
            string[] columnType = { "date", "varchar","number" };//需要導入數據庫中數據的類型（四種類型分別是"varchar","int","date","number"）
            //string createUser = CurrentUserInfo.EmpNo;
            string createUser = "測試打醬油(CurrentUserInfo.EmpNo)";//當前用戶，暫時附固定值，等頁面層中加入抓取SESSION[empNo]后再處理
            deFlag = NPOIHelper.DeleteExcelSql(tableName, createUser);//根據表名執行刪除命令
            string filePath = GetImpFileName();//獲取導入的文件路徑
            if (string.IsNullOrEmpty(filePath))
                return;

            flag = NPOIHelper.ImportExcel(columnProperties, columnType, filePath, tableName, createUser);//執行導入數據的命令
            if (flag == 1)
            {
                lblMessage.Text = "導入成功~";
            }
            else if (flag == 0)
            {
                lblMessage.Text = "數據保存失敗！";
            }
            else
            {
                lblMessage.Text = "Excel數據格式錯誤";
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
            if (fileAnnexFile.FileName.Trim() != "")
            {
                try
                {
                    filePath = MapPath("~/ExportFileTemp/") + DateTime.Now.ToString("yyyyMMddHHmmss") + Path.GetFileName(fileAnnexFile.FileName);
                    fileAnnexFile.SaveAs(filePath);

                }
                catch
                {
                    lblMessage.Text = "Exel上傳到服務器失敗!";
                }
            }
            else
            {
                lblMessage.Text = "導入路徑為空,請選擇要匯入的Excel文件！";
            }

            return filePath;
        }
        #endregion
    }
}
