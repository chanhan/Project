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
    [RefClass("Hr.KQM.Query.KQMAbsentMonthDal")]
    public interface IKQMAbsentMonthDal
    {

        /// <summary>
        /// 異常原因類別
        /// </summary>
        /// <returns></returns>
        DataTable GetExceptReason();

        /// <summary>
        /// 廠區
        /// </summary>
        /// <returns></returns>
        DataTable GetAreaCode();

        /// <summary>
        /// 獲得Paravalue
        /// </summary>
        /// <returns></returns>
        DataTable GetParavalue();

            /// <summary>
        /// 驗證日期
        /// </summary>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <returns></returns>
        DataTable CheckDateMonths(string StartDate, string EndDate);


                /// <summary>
        /// 驗證出勤日報表是否有數據
        /// </summary>
        /// <param name="personcode"></param>
        /// <param name="successnum"></param>
        /// <param name="errornum"></param>
        /// <returns></returns>
        int KaoQinDay_val(string kqdate, string personcode, string modulecode, string companyid, string depcode);


          /// <summary>
        /// 驗證缺勤統計報表是否有數據
        /// </summary>
        /// <param name="personcode"></param>
        /// <param name="successnum"></param>
        /// <param name="errornum"></param>
        /// <returns></returns>
        int AbsentMonth_val(string personcode, string modulecode, string companyid, string depcode, string startdate, string enddate, string workno);
    }
}
