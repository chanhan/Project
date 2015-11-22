/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： OTReporterBll.cs
 * 檔功能描述： 加班統計表業務邏輯類
 * 
 * 版本：1.0
 * 創建標識：張明強 2012.3.13
 * 
 */

using System.Collections.Generic;
using System.Data;
using System.Collections;
using System;
using GDSBG.MiABU.Attendance.IDAL.WFReporter;
using GDSBG.MiABU.Attendance.Model.KQM.Query.OTM;



namespace GDSBG.MiABU.Attendance.BLL.WFReporter
{
    public class OTReporterBll:  BLLBase<IOTReporterDal>
    {
        /// <summary>
        /// 加班統計表查詢,依據月份
        /// </summary>
        /// <param name="yearMonth"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public DataTable GetOTMQryList(string SQLDep, string yearMonth, int pageIndex, int pageSize, out int totalCount)
        {
            return DAL.GetOTMQryList(SQLDep, yearMonth, pageIndex, pageSize, out  totalCount);
        }
    }
}
