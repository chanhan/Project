/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： PowerChangeBll.cs
 * 檔功能描述： 權限交接業務邏輯類
 * 
 * 版本：1.0
 * 創建標識：高子焱 2011.12.08
 * 
 */

using System.Collections.Generic;
using GDSBG.MiABU.Attendance.IDAL.SystemManage.SystemSafety;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;
using System.Data;
using System.Collections;

namespace GDSBG.MiABU.Attendance.BLL.SystemManage.SystemSafety
{
    public class PowerChangeBll : BLLBase<IPowerChangeDal>
    {
        /// <summary>
        /// 根据工號查询員工已擁有的權限的DataTable
        /// </summary>
        /// <param name="model"></param>
        /// <returns>DataTable</returns>
        public DataTable GetPowerTableByKey(string personCode)
        {
            return DAL.GetPowerTableByKey(personCode);
        }
        /// <summary>
        /// 根據傳入參數更新表信息;複製功能
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int SaveData(string userNow, string fromPersoncode, string toPersonCode, string activeFlag, SynclogModel logmodel)
        {
            return DAL.SaveData(userNow, fromPersoncode, toPersonCode, activeFlag,logmodel);
        }
        /// <summary>
        /// 根據傳入參數更新表信息；交接功能
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int ChangData(string fromPersoncode, string toPersonCode, string activeFlag,SynclogModel logmodel)
        {
            return DAL.ChangData( fromPersoncode, toPersonCode, activeFlag,logmodel);
        }
    }
}
