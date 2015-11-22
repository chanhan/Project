/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： ITypeDataDal.cs
 * 檔功能描述： 組織層級設定數據操作接口
 * 
 * 版本：1.0
 * 創建標識： 高子焱 2011.12.06
 * 
 */

using System;
using GDSBG.MiABU.Attendance.Common.Attributes;
using GDSBG.MiABU.Attendance.Model.KQM.BasicData;
using System.Data;
using System.Collections;
namespace GDSBG.MiABU.Attendance.IDAL.KQM.BasicData
{
    /// <summary>
    /// 功能管理數據操作接口
    /// </summary>
    [RefClass("KQM.BasicData.TypeDataDal")]
    public interface ITypeDataDal
    {
        /// <summary>
        /// 根據主鍵查詢Model
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Model</returns>
        DataTable GetTypeDataList(string datatypevalue);
        /// <summary>
        /// 查詢全部Model清單
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Model</returns>
        DataTable GetDataList(string exceptionCode);
        /// <summary>
        /// 查詢班別（白班和晚班）
        /// </summary>
        /// <returns></returns>
        /// <summary>
        DataTable GetDataList();
        /// 查詢班別清單
        /// </summary>
        /// <returns></returns>
        DataTable GetShiftNoList();
        /// <summary>
        /// 查詢在職狀態清單
        /// </summary>
        /// <returns></returns>
        DataTable GetStatusList();
        /// <summary>
        /// 查詢異常類別清單
        /// </summary>
        /// <returns></returns>
        DataTable GetExceptionTypeList();
        /// <summary>
        /// 查詢考勤狀態（正常、異常）
        /// </summary>
        /// <returns></returns>
        DataTable GetKqmKaoQinStatusList();
        /// <summary>
        /// 根據傳入條件，查詢數據列表
        /// </summary>
        /// <returns></returns>
        DataTable GetDataTypeList(string DataType);
        /// <summary>
        /// 獲得支援狀態列表
        /// </summary>
        /// <returns></returns>
        DataTable GetSupportStatusList();
        /// <summary>
        /// 獲得下拉菜單數據列表
        /// </summary>
        /// <param name="datatypevalue"></param>
        /// <returns></returns>
        DataTable GetdllDateTypeList(string datatypevalue);
    }
}
