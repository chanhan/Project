/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： IPowerChangeDal.cs
 * 檔功能描述： 權限交接數據操作接口
 * 
 * 版本：1.0
 * 創建標識： 高子焱 2011.12.08
 * 
 */

using System;
using GDSBG.MiABU.Attendance.Common.Attributes;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;
using System.Data;
using System.Collections;


namespace GDSBG.MiABU.Attendance.IDAL.SystemManage.SystemSafety
{
    /// <summary>
    /// 功能管理數據操作接口
    /// </summary>
    [RefClass("SystemManage.SystemSafety.PowerChangeDal")]
    public interface IPowerChangeDal
    {
        /// <summary>
        /// 根据工號查询員工已擁有的組織層級的DataTable
        /// </summary>
        /// <param name="model"></param>
        /// <returns>DataTable</returns>
        DataTable GetPowerTableByKey(string personCode);
        /// <summary>
        /// 根據傳入參數更新表信息;複製功能
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        int SaveData(string userNow, string fromPersoncode, string toPersonCode, string activeFlag, SynclogModel logmodel);
        /// <summary>
        /// 根據傳入參數更新表信息；交接功能
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        int ChangData(string fromPersoncode, string toPersonCode, string activeFlag, SynclogModel logmodel);
    }
       
}
