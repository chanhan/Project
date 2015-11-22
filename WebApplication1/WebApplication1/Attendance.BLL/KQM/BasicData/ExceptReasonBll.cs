/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： ExceptReasonBll.cs
 * 檔功能描述： 異常原因維護業務邏輯類
 * 
 * 版本：1.0
 * 創建標識：高子焱 2011.12.15
 * 
 */

using System.Collections.Generic;
using GDSBG.MiABU.Attendance.IDAL.KQM.BasicData;
using GDSBG.MiABU.Attendance.Model.KQM.BasicData;
using System.Data;
using System.Collections;
using System;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;

namespace GDSBG.MiABU.Attendance.BLL.KQM.BasicData
{
    public class ExceptReasonBll:BLLBase<IExceptReasonDal>
    {
        /// <summary>
        /// 根據主鍵查詢Model
        /// <param name="model">異常原因model</param>
        /// <param name="orderType">排序</param>
        /// <param name="pageIndex">當前頁</param>
        /// <param name="pageSize"></param></param>
        /// <param name="totalCount">總頁數</param>
        /// <returns></returns>
        public DataTable GetExceptList(ExceptReasonModel model, string orderType, int pageIndex, int pageSize, out int totalCount)
        {
            return DAL.GetExceptList(model, orderType, pageIndex, pageSize, out totalCount);
        }
        /// <summary>
        /// 刪除一條異常原因記錄
        /// </summary>
        /// <param name="functionId">要刪除的異常原因的model</param>
        /// <returns>是否執行成功</returns>
        public int DeleteExceptByKey(ExceptReasonModel model, SynclogModel logmodel)
        {
            return DAL.DeleteExceptByKey(model,logmodel);
        }
        /// <summary>
        /// 根據主鍵修改功能
        /// </summary>
        /// <param name="model">要修改的功能Model</param>
        /// <returns>是否成功</returns>
        public bool UpdateExceptByKey(ExceptReasonModel model , SynclogModel logmodel)
        {

            return DAL.UpdateExceptByKey(model, logmodel);
        }
        /// <summary>
        /// 根據主鍵查詢Model
        /// <param name="model">異常原因model</param>
        /// <returns></returns>
        public DataTable GetExceptByKey(ExceptReasonModel model)
        {
            return DAL.GetExceptByKey(model);
        }
        /// <summary>
        /// 插入一條異常原因記錄
        /// </summary>
        /// <param name="functionId">要插入的異常原因的model</param>
        /// <returns>插入是否成功</returns>
        public int InsertExceptByKey(ExceptReasonModel model, SynclogModel logmodel)
        {
            return DAL.InsertExceptByKey(model, logmodel);
        }
    }
}
