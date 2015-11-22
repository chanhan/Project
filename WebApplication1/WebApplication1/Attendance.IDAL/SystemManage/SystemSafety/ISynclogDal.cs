/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： ISynclogDal.cs
 * 檔功能描述： 組織層級設定數據操作接口
 * 
 * 版本：1.0
 * 創建標識： 高子焱 2011.12.03
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
    [RefClass("SystemManage.SystemSafety.SynclogDal")]
    public interface ISynclogDal
    {
        /// <summary>
        /// 根据存在的查詢條件查詢
        /// </summary>
        /// <param name="model"></param>
        /// <returns>DataTable</returns>
        DataTable SelectByString(SynclogModel model, string condition, Nullable<DateTime> FromDate, Nullable<DateTime> ToDate, int pageIndex, int pageSize,  out int totalCount);
    }
}
