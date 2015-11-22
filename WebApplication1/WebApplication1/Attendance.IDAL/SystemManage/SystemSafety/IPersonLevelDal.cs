/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： IPersonLevelDal.cs
 * 檔功能描述： 組織層級設定數據操作接口
 * 
 * 版本：1.0
 * 創建標識： 高子焱 2011.11.30
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
    [RefClass("SystemManage.SystemSafety.PersonLevelDal")]
    public interface IPersonLevelDal
    {
        /// <summary>
        /// 根据工號查询員工已擁有的組織層級的DataTable
        /// </summary>
        /// <param name="model"></param>
        /// <returns>DataTable</returns>
        DataTable GetDeptDataTableByKey(string personCode);
        /// <summary>
        /// 根据工號查询員工未選中的組織層級的DataTable
        /// </summary>
        /// <param name="model"></param>
        /// <returns>DataTable</returns>
        DataTable GetDeptUncheckedByKey(string personCode);

        /// <summary>
        /// 根據主鍵修改功能
        /// </summary>
        /// <param name="model">要修改的功能Model</param>
        /// <returns>是否成功</returns>
        int UpdateDeptByKey(string personCode, Hashtable levels, SynclogModel logmodel);
    }
}
