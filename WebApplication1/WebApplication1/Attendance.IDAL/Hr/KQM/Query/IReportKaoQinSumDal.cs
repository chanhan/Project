/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： IKQMAbsentMonthDal.cs
 * 檔功能描述： 缺勤統計報表操作接口
 * 
 * 版本：1.0
 * 創建標識： 張明強 2011.12.30
 * 
 */

using System;
using GDSBG.MiABU.Attendance.Common.Attributes;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using GDSBG.MiABU.Attendance.Model.KQM.Query.OTM;

namespace GDSBG.MiABU.Attendance.IDAL.Hr.KQM.Query
{
    /// <summary>
    ///  缺勤統計報表操作接口
    /// </summary>
    [RefClass("Hr.KQM.Query.ReportKaoQinSumDal")]
    public interface IReportKaoQinSumDal
    {
        /// <summary>
        /// 查詢?
        /// </summary>
        /// <param name="toDate"></param>
        /// <returns></returns>
        DataTable GetDataBySqlText(string toDate);
   
    }
}
