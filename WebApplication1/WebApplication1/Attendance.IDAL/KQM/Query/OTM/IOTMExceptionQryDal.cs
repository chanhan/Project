/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： IOTMDetailQryDal.cs
 * 檔功能描述： 加班預報查詢操作接口
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

namespace GDSBG.MiABU.Attendance.IDAL.KQM.Query.OTM
{
    /// <summary>
    ///  加班預報查詢操作接口
    /// </summary>
    [RefClass("KQM.Query.OTM.OTMExceptionQryDal")]
    public interface IOTMExceptionQryDal
    {


        /// <summary>
        /// 根據條件查詢
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        DataTable GetDataByCondition(string condition);


        /// <summary>
        /// 加班異常查詢
        /// </summary>
        /// <param name="model"></param>
        /// <param name="dCode"></param>
        /// <param name="batchEmployeeNo"></param>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        /// <param name="hoursCondition"></param>
        /// <param name="hours"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        DataTable GetOTMExceptionQryList(OTMExceptionApplyQryModel model, string sqlDep, string dCode, string dateFrom, string dateTo, string hoursCondition, string hours, int pageIndex, int pageSize, out int totalCount);


        /// <summary>
        /// 加班異常查詢
        /// </summary>
        /// <param name="model"></param>
        /// <param name="dCode"></param>
        /// <param name="batchEmployeeNo"></param>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        /// <param name="hoursCondition"></param>
        /// <param name="hours"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        DataTable GetOTMExceptionQryList(OTMExceptionApplyQryModel model, string sqlDep, string dCode, string dateFrom, string dateTo, string hoursCondition, string hours);

        /// <summary>
        /// 獲取ParaValue
        /// </summary>
        /// <returns></returns>
        string GetValue(string flag, string strTempBeginTime, string strTempEndTime, string strMidTime, string strMidTime2);


        /// <summary>
        /// 獲取員工班別
        /// </summary>
        /// <param name="sWorkNo"></param>
        /// <param name="sDate"></param>
        /// <returns></returns>
        string GetShiftNo(string workNo, string date);

        /// <summary>
        /// 根據班別ShiftNo查詢信息
        /// </summary>
        /// <param name="strShiftNo"></param>
        /// <returns></returns>
        DataTable GetDataTableBySQL(string strShiftNo);

        /// <summary>
        /// 將datatable轉換為List
        /// </summary>
        /// <param name="dt">徐轉化的Datatable</param>
        /// <returns></returns>
        List<OTMExceptionApplyQryModel> GetList(DataTable dt);
       
       

    }
}
