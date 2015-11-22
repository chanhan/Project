/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： IOTMDetailQryDal.cs
 * 檔功能描述： 加班實報查詢操作接口
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
using GDSBG.MiABU.Attendance.Model.KQM.Query;

namespace GDSBG.MiABU.Attendance.IDAL.KQM.Query.OTM
{
    /// <summary>
    ///  加班實報查詢操作接口
    /// </summary>
    [RefClass("KQM.Query.OTM.OTMRealQryDal")]
    public interface IOTMRealQryDal
    {
        /// <summary>
        /// 根據條件查詢
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        DataTable GetDataByCondition(string condition);


        /// <summary>
        /// 加班實報查詢
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
        DataTable GetOTMRealQryList(OTMRealApplyQryModel model,string sqlDep, string dCode, string batchEmployeeNo, string dateFrom, string dateTo, string hoursCondition, string hours, int pageIndex, int pageSize, out int totalCount);

        /// <summary>
        /// 獲取卡鐘信息
        /// </summary>
        /// <param name="WorkNo"></param>
        /// <param name="KQDate"></param>
        /// <param name="ShiftNo"></param>
        /// <returns></returns>
        DataTable GetBellCardData(string WorkNo, string KQDate, string ShiftNo);

        /// <summary>
        /// 根據SQL語句查詢
        /// </summary>
        /// <param name="workNo"></param>
        /// <param name="oTdate"></param>
        /// <returns></returns>
        DataTable GetDataTableBySQL(string workNo, string oTdate);



        /// <summary>
        /// 加班預報查詢(不分頁)
        /// </summary>
        /// <param name="model"></param>
        /// <param name="dCode"></param>
        /// <param name="batchEmployeeNo"></param>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        /// <param name="hoursCondition"></param>
        /// <param name="hours"></param>
        /// <returns></returns>
        DataTable GetOTMRealQryList(OTMRealApplyQryModel model,string sqlDep, string dCode, string batchEmployeeNo, string dateFrom, string dateTo, string hoursCondition, string hours);

        /// <summary>
        /// 將datatable轉換為List
        /// </summary>
        /// <param name="dt">徐轉化的Datatable</param>
        /// <returns></returns>
        List<OTMRealApplyQryModel> GetList(DataTable dt);

        /// <summary>
        /// 將datatable轉換為List
        /// </summary>
        /// <param name="dt">徐轉化的Datatable</param>
        /// <returns></returns>
        List<BellCardDataModel> GetBellCardList(DataTable dt);
        

      
    }
}
