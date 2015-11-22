/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： SynclogBll.cs
 * 檔功能描述： 組織層級設定業務邏輯類
 * 
 * 版本：1.0
 * 創建標識：高子焱 2011.12.03
 * 
 */

using System.Collections.Generic;
using GDSBG.MiABU.Attendance.IDAL.SystemManage.SystemSafety;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;
using System.Data;
using System.Collections;
using System;

namespace GDSBG.MiABU.Attendance.BLL.SystemManage.SystemSafety
{
    public class SynclogBll : BLLBase<ISynclogDal>
    {
        /// <summary>
        /// 根据存在的查詢條件查詢
        /// </summary>
        /// <param name="model"></param>
        /// <returns>DataTable</returns>
        public DataTable SelectByString(SynclogModel model, string condition, string FromDate, string ToDate, int pageIndex, int pageSize,out int  totalCount)
        {
            DateTime fromDate = DateTime.MinValue, toDate = DateTime.MaxValue;
            if (!string.IsNullOrEmpty(FromDate))
            {
                fromDate = Convert.ToDateTime(FromDate);
            }
            if (!string.IsNullOrEmpty(ToDate))
            {
                toDate = Convert.ToDateTime(ToDate);
            }
            return DAL.SelectByString(model, condition, fromDate, toDate,pageIndex,pageSize, out  totalCount);
        }
    }
}
