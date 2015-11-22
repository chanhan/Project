/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： IParameterDal.cs
 * 檔功能描述： 系統參數操作接口
 * 
 * 版本：1.0
 * 創建標識： 何西 2011.12.05
 * 
 */

using System;
using GDSBG.MiABU.Attendance.Common.Attributes;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemData;
using System.Data;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;


namespace GDSBG.MiABU.Attendance.IDAL.SystemManage.SystemData
{
    /// <summary>
    /// 系統參數操作接口
    /// </summary>
    [RefClass("SystemManage.SystemData.ParameterDal")]
    public interface IParameterDal
    {
        /// <summary>
        /// 根據主鍵查詢
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ParameterModel GetParameterByKey(ParameterModel model);

        /// <summary>
        /// 新增數據
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool AddNew(ParameterModel model,SynclogModel logmodel);

        /// <summary>
        /// 修改數據
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool UpdateByKey(ParameterModel model, bool ignoreNull, SynclogModel logmodel);

        /// <summary>
        /// 根據主鍵刪除
        /// </summary>
        /// <param name="model">參數Model</param>
        /// <returns>是否成功</returns>
        bool DeleteByKey(ParameterModel model, SynclogModel logmodel);
       
        /// <summary>
        /// 根據條件查詢數據
        /// </summary>
        /// <param name="model"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        DataTable GetParameter(ParameterModel model, int pageIndex, int pageSize, out int totalCount);
        
        /// <summary>
        /// 根據條件查詢數據
        /// </summary>
        /// <returns></returns>
        DataTable GetTypeDataDayWork();
    }
}
