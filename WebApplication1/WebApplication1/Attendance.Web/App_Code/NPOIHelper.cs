/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： NPOIHelper.cs
 * 檔功能描述： NPOI控件Excel匯出幫助類
 * 
 * 版本：1.0
 * 創建標識： Lucky Lee 2011.11.1
 * 
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using GDSBG.MiABU.Attendance.BLL.ImportExcel;
using ICSharpCode.SharpZipLib.Tools;
using ICSharpCode.SharpZipLib.Zip.Compression;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using Resources;

namespace GDSBG.MiABU.Attendance.Web
{
    /// <summary>
    /// NPOI控件Excel匯出幫助類
    /// </summary>
    public static class NPOIHelper
    {
        /// <summary>
        /// 創建Sheet第一行創建Excel表頭
        /// </summary>
        /// <param name="sheet">要創建表頭的Sheet</param>
        /// <param name="header">表頭字段集</param>
        private static void CreateHeader(Sheet sheet, string[] header)
        {
            Row row = sheet.CreateRow(0);
            for (int i = 0; i < header.Length; i++)
            {
                row.CreateCell(i, CellType.STRING).SetCellValue(header[i]);
            }
        }

        /// <summary>
        /// 按columnProperties中Model屬性順序，在Excel的指定Sheet中寫入行
        /// </summary>
        /// <typeparam name="T">Model類型</typeparam>
        /// <param name="sheet">要寫入數據的Sheet</param>
        /// <param name="rowIndex">要寫入的行索引</param>
        /// <param name="model">要寫入的Model</param>
        /// <param name="columnProperties">要寫入的Model的值對應的屬性集</param>
        private static void CreateRow<T>(Sheet sheet, int rowIndex, T model, string[] columnProperties)
        {
            Row row = sheet.CreateRow(rowIndex);
            for (int i = 0; i < columnProperties.Length; i++)
            {
                try
                {
                    string[] propertyInfo = columnProperties[i].Split(new char[] { '|' }, 2);
                    PropertyInfo property = typeof(T).GetProperty(propertyInfo[0]);
                    string dateTimeFormat = propertyInfo.Length == 2 ? propertyInfo[1] : "yyyy/MM/dd";
                    object val = null;
                    if (property != null)
                    {
                        val = property.GetValue(model, null);
                        if (val != null && ((property.PropertyType.IsGenericType && property.PropertyType.IsValueType &&
                            property.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) &&
                            property.PropertyType.GetGenericArguments()[0] == typeof(DateTime)) || property.PropertyType == typeof(DateTime)))
                        {
                            row.CreateCell(i, CellType.STRING).SetCellValue(Convert.ToDateTime(val).ToString(dateTimeFormat));
                        }
                        else if (val != null)
                        {
                            row.CreateCell(i, CellType.STRING).SetCellValue(val.ToString());
                        }
                    }
                }
                catch
                {
                }
            }
        }

        /// <summary>
        /// 按columnProperties中Model屬性順序，在Excel的指定Sheet中寫入行
        /// </summary>
        /// <typeparam name="T">Model類型</typeparam>
        /// <param name="sheet">要寫入數據的Sheet</param>
        /// <param name="rowIndex">要寫入的行索引</param>
        /// <param name="model">要寫入的Model</param>
        /// <param name="columnProperties">要寫入的Model的值對應的屬性集</param>
        private static void CreateRow<T>(Sheet sheet, int rowIndex, T model, string[] columnProperties, bool isAllTime)
        {
            Row row = sheet.CreateRow(rowIndex);
            for (int i = 0; i < columnProperties.Length; i++)
            {
                try
                {
                    string dateTimeFormat;
                    string[] propertyInfo = columnProperties[i].Split(new char[] { '|' }, 2);
                    PropertyInfo property = typeof(T).GetProperty(propertyInfo[0]);
                    if (isAllTime)
                    {
                         dateTimeFormat = propertyInfo.Length == 2 ? propertyInfo[1] : "yyyy/MM/dd HH:mm";
                    }
                    else
                    {
                         dateTimeFormat = propertyInfo.Length == 2 ? propertyInfo[1] : "yyyy/MM/dd";
                    }
                    object val = null;
                    if (property != null)
                    {
                        val = property.GetValue(model, null);
                        if (val != null && ((property.PropertyType.IsGenericType && property.PropertyType.IsValueType &&
                            property.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) &&
                            property.PropertyType.GetGenericArguments()[0] == typeof(DateTime)) || property.PropertyType == typeof(DateTime)))
                        {
                            row.CreateCell(i, CellType.STRING).SetCellValue(Convert.ToDateTime(val).ToString(dateTimeFormat));
                        }
                        else if (val != null)
                        {
                            row.CreateCell(i, CellType.STRING).SetCellValue(val.ToString());
                        }
                    }
                }
                catch
                {
                }
            }
        }

        /// <summary>
        /// 保存Excel檔
        /// </summary>
        /// <param name="workBook">要保存的Excel</param>
        /// <param name="filePath">文件保存物理路徑</param>
        private static void SaveExcelFile(Workbook workBook, string filePath)
        {
            using (FileStream file = new FileStream(filePath, FileMode.Create))
            {
                workBook.Write(file);
                file.Flush();
                file.Close();
                workBook = null;
            }
        }

        /// <summary>
        /// 按SheetSize大小分文檔生成Excel，多個文件時壓縮
        /// </summary>
        /// <typeparam name="T">Model類型</typeparam>
        /// <param name="modelList">要匯出的Model集</param>
        /// <param name="header">表頭字段集</param>
        /// <param name="columnProperties">要匯出的Model值對應的屬性名稱集</param>
        /// <param name="sheetSize">頁大小</param>
        /// <param name="directoryPath">生成文件的物理路徑</param>
        /// <param name="fileName">文件名(無擴展名)</param>
        /// <returns>生成的文件名</returns>
        public static string ExportExcelFiles<T>(List<T> modelList, string[] header, string[] columnProperties, int sheetSize, string directoryPath, string fileName)
        {
            int rowIndex = 0;
            int fileIndex = 0;
            Workbook workbook = null;
            Sheet sheet = null;
            string dirpath = directoryPath.Trim();
            if (!Directory.Exists(dirpath))
            {
                Directory.CreateDirectory(dirpath);
            }
            if (!(dirpath[dirpath.Length - 1] == '\\'))
            {
                dirpath += "\\";
            }
            foreach (T model in modelList)
            {
                if (rowIndex == 0 || rowIndex == sheetSize + 1)
                {
                    if (rowIndex == sheetSize + 1)
                    {
                        SaveExcelFile(workbook, dirpath + fileName + "(" + fileIndex + ").xls");
                    }
                    workbook = new HSSFWorkbook();
                    sheet = workbook.CreateSheet("Sheet1");
                    CreateHeader(sheet, header);
                    rowIndex = 1;
                    fileIndex++;
                }
                CreateRow(sheet, rowIndex, model, columnProperties);
                rowIndex++;
            }
            string filename = fileIndex == 1 ? fileName + ".xls" : fileName + "(" + fileIndex + ").xls";
            SaveExcelFile(workbook, dirpath + filename);
            if (fileIndex > 1)//單個文檔時不壓縮
            {
                int zipLevel = Deflater.BEST_SPEED;//壓縮級別
                ZipClass zip = new ZipClass();
                string zippath = dirpath.TrimEnd('\\');
                string zipOutPath = zippath.Substring(0, zippath.LastIndexOf('\\') + 1);
                filename = fileName + ".zip";
                zip.ZipDir(zippath, zipOutPath + filename, zipLevel);
                File.Move(zipOutPath + filename, dirpath + filename);//移動至參數給出的路徑
            }
            return filename;
        }

        /// <summary>
        /// 按SheetSize大小分頁生成Excel
        /// </summary>
        /// <typeparam name="T">model類型</typeparam>
        /// <param name="modelList">要匯出的Model集</param>
        /// <param name="header">表頭字段集</param>
        /// <param name="columnProperties">要匯出的Model值對應的屬性名稱集</param>
        /// <param name="sheetSize">頁大小</param>
        /// <param name="filePath">生成Excel物理路徑</param>
        public static void ExportExcel<T>(List<T> modelList, string[] header, string[] columnProperties, int sheetSize, string filePath)
        {
            int rowIndex = 0;
            int sheetIndex = 1;
            Workbook workbook = new HSSFWorkbook();
            Sheet sheet = null;
            foreach (T model in modelList)
            {
                if (rowIndex == 0 || rowIndex == sheetSize + 1)
                {
                    sheet = workbook.CreateSheet("Sheet" + sheetIndex);
                    CreateHeader(sheet, header);
                    rowIndex = 1;
                    sheetIndex++;
                }
                CreateRow(sheet, rowIndex, model, columnProperties);
                rowIndex++;
            }
            SaveExcelFile(workbook, filePath);
        }

        /// <summary>
        /// 按SheetSize大小分頁生成Excel
        /// </summary>
        /// <typeparam name="T">model類型</typeparam>
        /// <param name="modelList">要匯出的Model集</param>
        /// <param name="header">表頭字段集</param>
        /// <param name="columnProperties">要匯出的Model值對應的屬性名稱集</param>
        /// <param name="sheetSize">頁大小</param>
        /// <param name="filePath">生成Excel物理路徑</param>
        public static void ExportExcel<T>(List<T> modelList, string[] header, string[] columnProperties, int sheetSize, string filePath,bool isAllTime)
        {
            int rowIndex = 0;
            int sheetIndex = 1;
            Workbook workbook = new HSSFWorkbook();
            Sheet sheet = null;
            foreach (T model in modelList)
            {
                if (rowIndex == 0 || rowIndex == sheetSize + 1)
                {
                    sheet = workbook.CreateSheet("Sheet" + sheetIndex);
                    CreateHeader(sheet, header);
                    rowIndex = 1;
                    sheetIndex++;
                }
                CreateRow(sheet, rowIndex, model, columnProperties, isAllTime);
                rowIndex++;
            }
            SaveExcelFile(workbook, filePath);
        }

        /// <summary>
        /// 按SheetSize大小分頁生成Excel(不生成表頭)
        /// </summary>
        /// <typeparam name="T">model類型</typeparam>
        /// <param name="modelList">要匯出的Model集</param>
        /// <param name="columnProperties">要匯出的Model值對應的屬性名稱集</param>
        /// <param name="sheetSize">頁大小</param>
        /// <param name="startRow">開始行</param>
        /// <param name="mbPath">模板路徑（全路徑）</param>
        /// <param name="filePath">生成Excel物理路徑</param>
        public static void ExportExcel<T>(List<T> modelList, string[] columnProperties, int sheetSize, int startRow, string mbPath, string filePath)
        {
            HSSFWorkbook hssfworkbook;
            using (Stream fileStream = new FileStream(mbPath, FileMode.Open))
            {
                hssfworkbook = new HSSFWorkbook(fileStream);
            }
            Sheet insertSheet = hssfworkbook.GetSheet("Sheet1");
            int rowIndex = 0;
            int sheetIndex = startRow;
            Workbook workbook = new HSSFWorkbook();
            Sheet sheet = null;
            foreach (T model in modelList)
            {
                if (rowIndex == 0 || rowIndex == sheetSize + 1)
                {
                    sheet = workbook.CreateSheet("Sheet" + sheetIndex);
                    rowIndex = 1;
                    sheetIndex++;
                }
                CreateRow(sheet, rowIndex, model, columnProperties);
                rowIndex++;
            }
            SaveExcelFile(workbook, filePath);
        }

        /// <summary>
        /// 按SheetSize大小分頁生成Excel(修改部門表頭內容，加班匯總查詢)
        /// </summary>
        /// <typeparam name="T">model類型</typeparam>
        /// <param name="modelList">要匯出的Model集</param>
        /// <param name="columnProperties">要匯出的Model值對應的屬性名稱集</param>
        /// <param name="sheetSize">頁大小</param>
        /// <param name="startRow">開始行</param>
        /// <param name="mbPath">模板路徑（全路徑）</param>
        /// <param name="filePath">生成Excel物理路徑</param>
        public static void ExportExcel<T>(List<T> modelList, string[] columnProperties, int sheetSize, int startRow, string mbPath, string filePath, string yearMonth)
        {
            HSSFWorkbook hssfworkbook;
            using (Stream fileStream = new FileStream(mbPath, FileMode.Open))
            {
                hssfworkbook = new HSSFWorkbook(fileStream);
            }
            Sheet insertSheet = hssfworkbook.GetSheet("Sheet1");
            insertSheet = LoadHead(insertSheet, 21, yearMonth);
            int rowIndex = startRow - 1;
            int sheetIndex = 1;
            Sheet sheet = null;
            foreach (T model in modelList)
            {
                if (sheetIndex == 1)
                {
                    CreateRow(insertSheet, rowIndex, model, columnProperties);
                }
                else if (rowIndex % sheetSize == 1)
                {
                    sheetIndex++;
                    sheet = hssfworkbook.CreateSheet("Sheet" + sheetIndex);
                    rowIndex = 0;
                    CreateRow(sheet, rowIndex, model, columnProperties);
                }
                rowIndex++;
            }
            SaveExcelFile(hssfworkbook, filePath);
        }

        private static Sheet LoadHead(Sheet tmpSheet, int col, string yearMonth)
        {
            DateTime date = Convert.ToDateTime(yearMonth + "/01");
            DateTime weekDate = Convert.ToDateTime(yearMonth + "/01");
            int days = System.Threading.Thread.CurrentThread.CurrentUICulture.Calendar.GetDaysInMonth(date.Year, date.Month);
            for (int d = 1; d <= days; d++)
            {
                tmpSheet.GetRow(0).CreateCell(col + d - 1).SetCellValue(d.ToString());
                weekDate = Convert.ToDateTime(yearMonth + "/" + d.ToString());
                string weekDay = GetWeek(yearMonth + "/" + d.ToString());
                tmpSheet.GetRow(1).CreateCell(col + d - 1).SetCellValue(weekDay.ToString());
            }
            return tmpSheet;
        }
        /// <summary>
        /// 獲取星期幾
        /// </summary>
        /// <param name="SelectYearMonth"></param>
        /// <returns></returns>
        private static string GetWeek(string SelectYearMonth)
        {
            DateTime yearMonth = Convert.ToDateTime(SelectYearMonth);
            string getWeek = yearMonth.DayOfWeek.ToString();
            //string getWeek = SelectYearMonth.DayOfWeek.ToString();
            switch (getWeek)
            {
                case "Sunday":
                    return "日";

                case "Monday":
                    return "一";

                case "Tuesday":
                    return "二";

                case "Wednesday":
                    return "三";

                case "Thursday":
                    return "四";

                case "Friday":
                    return "五";

                case "Saturday":
                    return "六";
            }
            return "";
        }

        /// <summary>
        /// 根據EXCEL存放的路徑獲取數據
        /// </summary>
        /// <param name="columnProperties">表頭字段集</param>
        /// <param name="filePath">生成Excel的物理路徑</param>
        /// <param name="tableName">表名</param>
        /// <param name="createUser">導入數據人</param>
        /// <param name="createDate">導入時間</param>
        /// <returns>是否獲取成功</returns>
        public static int ImportExcel(string[] columnProperties, string[] columnType, string filePath, string tableName, string createUser)
        {
            bool blIns = false;
            int nIns = 0;
            string insSql = "";
            HSSFWorkbook hssfworkbook;
            StringBuilder sb = new StringBuilder(20000);
            if (Path.GetExtension(filePath) != ".xls")//若文檔非EXCEL文檔則提示非正確文本
            {
                nIns = -3;
                return nIns;
            }
            else
            {
                try
                {

                    using (Stream fileStream = new FileStream(filePath, FileMode.Open))
                    {
                        hssfworkbook = new HSSFWorkbook(fileStream);
                    }
                    Sheet insertSheet = hssfworkbook.GetSheet("Sheet1");
                    int nRows = 1;

                    while (insertSheet.GetRow(nRows) != null && insertSheet.GetRow(nRows).GetCell(0) != null)
                    {
                        try
                        {
                            insSql = GetInsertSql(tableName, columnProperties, columnType, insertSheet.GetRow(nRows), createUser);
                        }
                        catch
                        {
                            nIns = -1;
                            return nIns;
                        }
                        sb.AppendFormat(insSql);
                        nRows++;
                        if (nRows % 250 == 0 || (insertSheet.GetRow(nRows + 1) == null))
                        {
                            blIns = ImportData(sb);
                            if (!blIns)
                            {
                                break;
                            }
                            sb.Remove(0, sb.Length);
                        }
                    }
                    if (blIns)
                    {
                        nIns = 1;
                    }
                    else
                        nIns = 0;
                }
                catch
                {
                    nIns = -1;
                    return nIns;
                }
            }

            return nIns;
        }

        /// <summary>
        /// 刪除導入表中當前用戶的所有數據
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="CreateUser">當前用戶</param>
        /// <returns>是否刪除成功</returns>
        public static bool DeleteExcelSql(string tableName, string CreateUser)
        {
            bool deFlag = false;
            string sbSql = "";
            StringBuilder sb = new StringBuilder(20000);
            sbSql = DeleteSql(tableName, CreateUser);
            sb.AppendFormat(sbSql);
            deFlag = DeleteData(sb);
            return deFlag;
        }
        /// <summary>
        /// 向對應的表中插入從EXCEL表獲取的數據（組織SQL語句）
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="columnProperties">欄位名</param>
        /// <param name="row">數據行</param>
        /// <param name="createUser">當前用戶</param>
        /// <param name="createDate">導入時間</param>
        /// <returns>OracelCommand對象</returns>
        private static string GetInsertSql(string tableName, string[] columnProperties, string[] columnType, Row row, string createUser)
        {
            StringBuilder insSql = new StringBuilder();
            insSql.Append(" insert into ").Append(tableName);
            int nC = columnProperties.Length;
            string col = "(";
            string val = "(";
            for (int c = 0; c < nC; c++)
            {
                if (c == nC - 1)
                {
                    col = col + columnProperties[c] + "," + "create_User" + "," + "create_Date";
                    val = GetInsTypeValue(columnType[c], row, c, val) + "'" + createUser + "'," + "sysdate,";
                }
                else
                {
                    col = col + columnProperties[c] + ",";
                    val = GetInsTypeValue(columnType[c], row, c, val);
                }
            }
            col = col.TrimEnd(',') + ")";
            val = val.TrimEnd(',') + ");";
            insSql.Append(col).Append(" values ").Append(val);
            return insSql.ToString();
        }

        /// <summary>
        /// 根據表名和當前用戶刪除所有數據（組織SQL語句）
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="createUser">當前用戶</param>
        /// <returns>OracelCommand對象</returns>
        public static string DeleteSql(string tableName, string createUser)
        {
            StringBuilder deleteSql = new StringBuilder();
            deleteSql.Append("delete from ").Append(tableName).Append(" where create_User = ").Append("'").Append(createUser).Append("'");
            return deleteSql.ToString();
        }

        /// <summary>
        /// 根據Excel中的數據類型進行導出數據
        /// </summary>
        /// <param name="columnType">字段類別</param>
        /// <param name="row">數據行</param>
        /// <param name="nCell">Excel中的單元格</param>
        /// <param name="values">需要導入數據庫的數據</param>
        /// <returns>需要導入數據庫的數據（即Values(）中的數據）</returns>
        private static string GetInsTypeValue(string columnType, Row row, int nCell, string values)
        {
            if (row.GetCell(nCell) == null || row.GetCell(nCell).CellType == CellType.BLANK)
            {
                values = values + "null" + ",";
            }
            else
            {
                switch (columnType)
                {
                    case "varchar":
                        if (row.GetCell(nCell).CellType == CellType.NUMERIC)
                        {
                            values = values + row.GetCell(nCell).NumericCellValue + ",";
                        }
                        else if (row.GetCell(nCell).CellType == CellType.STRING)
                        {
                            values = values + "'" + row.GetCell(nCell).StringCellValue + "',";
                        }
                        else
                        {
                            values = values + "'" + row.GetCell(nCell).DateCellValue + "',";
                        }
                        break;
                    case "int": values = values + "" + row.GetCell(nCell).NumericCellValue + ",";
                        break;
                    case "date": values = values + "to_date('" + row.GetCell(nCell).DateCellValue.ToString("yyyy/MM/dd") + "','yyyy/mm/dd'),";
                        break;
                    case "number": values = values + row.GetCell(nCell).NumericCellValue + ",";
                        break;
                }
            }
            return values;
        }

        /// <summary>
        /// 執行向數據庫插入數據的命令
        /// </summary>
        /// <param name="sbSql">OracleCommand對象（要執行的插入命令）</param>
        /// <returns>是否插入成功</returns>
        public static bool ImportData(StringBuilder sbSql)
        {
            bool blIns = false;
            if (sbSql.Length > 0)
            {
                StringBuilder sb = new StringBuilder(20000);
                sb.Append(" begin ");
                sb.Append(sbSql.ToString());
                sb.Append(" end;");
                blIns = new ImportExcelBll().InsertExcel(sb.ToString());
            }
            return blIns;
        }

        /// <summary>
        /// 執行根據導入人刪除數據的命令
        /// </summary>
        /// <param name="sbSql">OracleCommand對象（要執行的刪除命令）</param>
        /// <returns>是否刪除成功</returns>
        public static bool DeleteData(StringBuilder sbSql)
        {
            bool deleteIns = false;
            if (sbSql.Length > 0)
            {
                StringBuilder sb = new StringBuilder(20000);
                sb.Append(sbSql.ToString());
                deleteIns = new ImportExcelBll().DeleteData(sb.ToString());
            }
            return deleteIns;
        }
    }
}