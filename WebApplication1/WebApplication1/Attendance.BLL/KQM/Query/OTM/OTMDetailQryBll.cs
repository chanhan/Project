/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： OTMDetailQryBll.cs
 * 檔功能描述： 加班預報查詢業務邏輯類
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
namespace GDSBG.MiABU.Attendance.BLL.KQM.Query.OTM
{
    public class OTMDetailQryBll : BLLBase<IOTMDetailQryDal>
    {
        /// <summary>
        /// 加班預報查詢
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
        public DataTable GetOTMAdvanceQryList(OTMAdvanceApplyQryModel model,string sqlDep, string dCode, string batchEmployeeNo, string dateFrom, string dateTo, string hoursCondition, string hours, int pageIndex, int pageSize, out int totalCount)
        {
            return DAL.GetOTMAdvanceQryList(model,sqlDep, dCode, batchEmployeeNo, dateFrom, dateTo, hoursCondition, hours, pageIndex, pageSize, out  totalCount);

        }



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
        /// 將datatable轉換為List
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public List<OTMAdvanceApplyQryModel> GetList(DataTable dt)
        {
            return DAL.GetList(dt);
        }


        /// <summary>
        /// 根據SQL語句查詢
        /// </summary>
        /// <param name="workNo"></param>
        /// <param name="oTdate"></param>
        /// <returns></returns>
        public DataTable GetDataTableBySQL(string workNo, string oTdate)
        {
            return DAL.GetDataTableBySQL(workNo, oTdate);
        }


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
        public DataTable GetOTMAdvanceQryList(OTMAdvanceApplyQryModel model, string sqlDep, string dCode, string batchEmployeeNo, string dateFrom, string dateTo, string hoursCondition, string hours)
        {

            return DAL.GetOTMAdvanceQryList(model,sqlDep, dCode, batchEmployeeNo, dateFrom, dateTo, hoursCondition, hours);
        }


    }
}
