/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： IOTMTotalQryDal.cs
 * 檔功能描述： 加班統計表數據操作接口
 * 
 * 版本：1.0
 * 創建標識： 張明強 2012.3.13
 * 
 */

using System;
using GDSBG.MiABU.Attendance.Common.Attributes;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using GDSBG.MiABU.Attendance.Model.KQM.Query.OTM;

namespace GDSBG.MiABU.Attendance.IDAL.WFReporter
{
    /// <summary>
    ///  加班匯總操作接口
    /// </summary>
    [RefClass("WFReporter.OTReporterDal")]
    public interface IOTReporterDal
    {

        /// <summary>
        /// 加班統計表查詢,依據月份
        /// </summary>
        /// <param name="yearMonth"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        DataTable GetOTMQryList(string SQLDep, string yearMonth, int pageIndex, int pageSize, out int totalCount);
        
    }
}
