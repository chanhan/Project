/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： IAbsentReportDal.cs
 * 檔功能描述：缺勤統計表數據操作接口
 * 
 * 版本：1.0
 * 創建標識： 顧陳偉 2012.3.15
 * 
 * 
 */

using System.Collections.Generic;
using System.Data;
using GDSBG.MiABU.Attendance.Common.Attributes;
using GDSBG.MiABU.Attendance.Model.WFReporter;

namespace GDSBG.MiABU.Attendance.IDAL.WFReporter
{
    [RefClass("WFReporter.AbsentReporterDal")]
    public interface IAbsentReportDal
    {
        /// <summary>
        /// 缺勤統計
        /// </summary>
        /// <param name="depcode">組織</param>
        /// <param name="workno">工號</param>
        /// <param name="localname">姓名</param>
        /// <param name="fromdate">起始時間</param>
        /// <param name="todate">截止時間</param>
        /// <param name="yearmonth">年月</param>
        /// <param name="absenttype">缺勤類型</param>
        /// <param name="status">狀態</param>
        /// <returns></returns>
        DataTable GetAbsentInfo(string sql, string depcode, string workno, string localname, string fromdate, string todate, string yearmonth, string absenttype, string status, int pageIndex, int pageSize, out int totalCount);

        /// <summary>
        /// 導出查詢
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="depcode"></param>
        /// <param name="workno"></param>
        /// <param name="localname"></param>
        /// <param name="fromdate"></param>
        /// <param name="todate"></param>
        /// <param name="yearmonth"></param>
        /// <param name="absenttype"></param>
        /// <param name="status"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        DataTable GetAbsentInfoForExport(string sql, string depcode, string workno, string localname, string fromdate, string todate, string yearmonth, string absenttype, string status);

        /// <summary>
        /// 將datatable轉換為List
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        List<AbsentReporterModel> GetList(DataTable dt);
    }
}
