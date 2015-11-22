/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： OrgmanagerBll.cs
 * 檔功能描述： 考勤查詢業務邏輯類
 * 
 * 版本：1.0
 * 創建標識： 昝望 2012.1.4
 * 
 */
using System;
using System.Collections.Generic;

using System.Text;
using GDSBG.MiABU.Attendance.IDAL.Hr.PCM;
using System.Data;
using GDSBG.MiABU.Attendance.Model.Hr.PCM;

namespace GDSBG.MiABU.Attendance.BLL.Hr.PCM
{
   public  class KaoQinDataBll : BLLBase<IKaoQinDataDal>
    {
               /// <summary>
        /// 結果下拉框
        /// </summary>
        /// <returns></returns>
       public DataTable GetExceptionType()
       {
           return DAL.GetExceptionType();
       }

            /// <summary>
        /// 狀態下拉框
        /// </summary>
        /// <returns></returns>
       public DataTable GetKqmKaoQinStatus()
       {
           return DAL.GetKqmKaoQinStatus();
       }

            /// <summary>
        /// 班別下拉框
        /// </summary>
        /// <returns></returns>
       public DataTable GetKqmWorkShiftType()
       {
           return DAL.GetKqmWorkShiftType();
       }

        /// <summary>
        /// 查詢
        /// </summary>
        /// <param name="model">給出主鍵值的Model</param>
        /// <returns>返回對應主鍵的Model</returns>
       public DataTable GetKaoQinData(string KQDateFrom, string KQDateTo, string ExceptionType, string Status, string ShiftNo, string personcode, int pageIndex, int pageSize, out int totalCount)
       {
           return DAL.GetKaoQinData(KQDateFrom,KQDateTo,ExceptionType,Status,ShiftNo,personcode,pageIndex,pageSize,out totalCount);
       }

        /// <summary>
        /// 導出查詢
        /// </summary>
        /// <param name="model">給出主鍵值的Model</param>
        /// <returns>返回對應主鍵的Model</returns>
       public DataTable GetKaoQinData(string KQDateFrom, string KQDateTo, string ExceptionType, string Status, string ShiftNo, string personcode)
       {
           return DAL.GetKaoQinData(KQDateFrom, KQDateTo, ExceptionType, Status, ShiftNo, personcode);
       }

        /// <summary>
        /// 將datatable轉換為List
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
       public List<KaoQinDataModel> GetList(DataTable dt)
       {
           return DAL.GetList(dt);
       }
    }
}
