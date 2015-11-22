
/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： KQMAbsentMonthBll.cs
 * 檔功能描述：缺勤統計報表業務邏輯類
 * 
 * 版本：1.0
 * 創建標識： 張明強 2011.12.8
 * 
 */
using System.Collections.Generic;
using GDSBG.MiABU.Attendance.IDAL.Hr.KQM.Query;
using System.Data;
using System;
namespace GDSBG.MiABU.Attendance.BLL.Hr.KQM.Query
{
    public class ReportKaoQinSumBll : BLLBase<IReportKaoQinSumDal>
    {
        /// <summary>
        /// 查詢?
        /// </summary>
        /// <param name="toDate"></param>
        /// <returns></returns>
        public DataTable GetDataBySqlText(string toDate)
        {
            return DAL.GetDataBySqlText(toDate);
        }
    }
}
