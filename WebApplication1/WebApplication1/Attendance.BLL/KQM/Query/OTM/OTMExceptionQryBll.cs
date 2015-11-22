/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： OTMDetailQryBll.cs
 * 檔功能描述： 加班實報查詢業務邏輯類
 * 
 * 版本：1.0
 * 創建標識：張明強 2011.12.30
 * 
 */

using System.Collections.Generic;
using System.Data;
using System.Collections;
using System;
using GDSBG.MiABU.Attendance.IDAL.KQM.Query.OTM;
using GDSBG.MiABU.Attendance.Model.KQM.Query.OTM;
using GDSBG.MiABU.Attendance.Model.KQM.Query;

namespace GDSBG.MiABU.Attendance.BLL.KQM.Query.OTM
{
    public class OTMExceptionQryBll : BLLBase<IOTMExceptionQryDal>
    {


        /// <summary>
        /// 根據條件查詢
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public DataTable GetDataByCondition(string condition)
        {
            return DAL.GetDataByCondition(condition);
        }

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
        public DataTable GetOTMExceptionQryList(OTMExceptionApplyQryModel model, string sqlDep, string dCode, string dateFrom, string dateTo, string hoursCondition, string hours, int pageIndex, int pageSize, out int totalCount)
        {
            return DAL.GetOTMExceptionQryList(model, sqlDep, dCode, dateFrom, dateTo, hoursCondition, hours, pageIndex, pageSize, out  totalCount);

        }

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
        public DataTable GetOTMExceptionQryList(OTMExceptionApplyQryModel model, string sqlDep, string dCode, string dateFrom, string dateTo, string hoursCondition, string hours)
        {
            return DAL.GetOTMExceptionQryList(model,sqlDep, dCode, dateFrom, dateTo, hoursCondition, hours);

        }

        /// <summary>
        /// 獲取ParaValue
        /// </summary>
        /// <returns></returns>
        public string GetValue(string flag, string strTempBeginTime, string strTempEndTime, string strMidTime, string strMidTime2)
        {
            return DAL.GetValue(flag, strTempBeginTime, strTempEndTime, strMidTime, strMidTime2);
        }


        /// <summary>
        /// 獲取員工班別
        /// </summary>
        /// <param name="sWorkNo"></param>
        /// <param name="sDate"></param>
        /// <returns></returns>
        public string GetShiftNo(string workNo, string date)
        {
            return DAL.GetShiftNo(workNo, date);
        }

        /// <summary>
        /// 根據班別ShiftNo查詢信息
        /// </summary>
        /// <param name="strShiftNo"></param>
        /// <returns></returns>
        public DataTable GetDataTableBySQL(string strShiftNo)
        {
            return DAL.GetDataTableBySQL(strShiftNo);
        }

        /// <summary>
        /// 將datatable轉換為List
        /// </summary>
        /// <param name="dt">徐轉化的Datatable</param>
        /// <returns></returns>
        public List<OTMExceptionApplyQryModel> GetList(DataTable dt)
        {
            return DAL.GetList(dt);
        }
    }
}
