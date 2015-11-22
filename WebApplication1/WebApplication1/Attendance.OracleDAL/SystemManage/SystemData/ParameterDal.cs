/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： ParameterDal.cs
 * 檔功能描述： 參數設置數據操作類
 * 
 * 版本：1.0
 * 創建標識： 何西 2011.12.05
 * 
 */

using System;
using System.Data;
using System.Data.OracleClient;
using GDSBG.MiABU.Attendance.IDAL.SystemManage.SystemData;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemData;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;

namespace GDSBG.MiABU.Attendance.OracleDAL.SystemManage.SystemData
{
    public class ParameterDal : DALBase<ParameterModel>, IParameterDal
    {
        /// <summary>
        /// 根據主鍵查詢
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ParameterModel GetParameterByKey(ParameterModel model)
        {
            return DalHelper.SelectByKey(model);
        }

        /// <summary>
        /// 新增數據
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddNew(ParameterModel model,SynclogModel logmodel)
        {
            return DalHelper.Insert(model, logmodel) == 1;
        }
        /// <summary>
        /// 修改數據
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateByKey(ParameterModel model, bool ignoreNull, SynclogModel logmodel)
        {
            return DalHelper.UpdateByKey(model, ignoreNull,logmodel) != -1;
        }

        /// <summary>
        /// 根據主鍵刪除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool DeleteByKey(ParameterModel model, SynclogModel logmodel)
        {
            return DalHelper.Delete(model,logmodel) == 1;
        }


        /// <summary>
        /// 根據條件查詢數據
        /// </summary>
        /// <param name="model"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public DataTable GetParameter(ParameterModel model, int pageIndex, int pageSize, out int totalCount)
        {
            return DalHelper.Select(model, pageIndex, pageSize, out totalCount);
        }
        /// <summary>
        /// 根據條件查詢數據
        /// </summary>
        /// <returns></returns>
        public DataTable GetTypeDataDayWork()
        {
            DataTable dt = DalHelper.ExecuteQuery(@"SELECT gds_sc_dayworkhours_fun ('DayWorkHours') dayworkhours,
                           gds_sc_dayworkhours_fun ('WorkOTRestHours') workotresthours,
                           gds_sc_dayworkhours_fun ('DayWorkUpperLimitHoursLactation') upperlimithourslactation,
                           gds_sc_dayworkhours_fun ('DayWorkLowerLimitHoursLactation') lowerlimithourslactation FROM DUAL");
            return dt;
        }
    }
}
