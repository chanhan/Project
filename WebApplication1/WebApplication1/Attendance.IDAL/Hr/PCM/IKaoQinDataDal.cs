/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： IRealapplyDal.cs
 * 檔功能描述： 考勤查詢數據操作接口
 * 
 * 版本：1.0
 * 創建標識： 昝望 2012.1.4
 * 
 */


using System;
using System.Collections.Generic;
using System.Text;
using GDSBG.MiABU.Attendance.Common.Attributes;
using System.Data;
using GDSBG.MiABU.Attendance.Model.Hr.PCM;

namespace GDSBG.MiABU.Attendance.IDAL.Hr.PCM
{
    [RefClass("Hr.PCM.KaoQinDataDal")]
    public interface IKaoQinDataDal
    {
        /// <summary>
        /// 結果下拉框
        /// </summary>
        /// <returns></returns>
        DataTable GetExceptionType();

        /// <summary>
        /// 狀態下拉框
        /// </summary>
        /// <returns></returns>
        DataTable GetKqmKaoQinStatus();

        /// <summary>
        /// 班別下拉框
        /// </summary>
        /// <returns></returns>
        DataTable GetKqmWorkShiftType();

        /// <summary>
        /// 查詢
        /// </summary>
        /// <param name="model">給出主鍵值的Model</param>
        /// <returns>返回對應主鍵的Model</returns>
        DataTable GetKaoQinData(string KQDateFrom, string KQDateTo, string ExceptionType, string Status, string ShiftNo, string personcode, int pageIndex, int pageSize, out int totalCount);

        /// <summary>
        /// 導出查詢
        /// </summary>
        /// <param name="model">給出主鍵值的Model</param>
        /// <returns>返回對應主鍵的Model</returns>
        DataTable GetKaoQinData(string KQDateFrom, string KQDateTo, string ExceptionType, string Status, string ShiftNo, string personcode);

        /// <summary>
        /// 將datatable轉換為List
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        List<KaoQinDataModel> GetList(DataTable dt);

    }
}
