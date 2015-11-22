/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： IWorkShiftDal.cs
 * 檔功能描述： 辨別定義數據操作接口
 * 
 * 版本：1.0
 * 創建標識： 高子焱 2011.11.30
 * 
 */

using System;
using GDSBG.MiABU.Attendance.Common.Attributes;
using GDSBG.MiABU.Attendance.Model.KQM.BasicData;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;

namespace GDSBG.MiABU.Attendance.IDAL.KQM.BasicData
{
    /// <summary>
    /// 辨別定義數據操作接口
    /// </summary>
    [RefClass("KQM.BasicData.WorkShiftDal")]
    public interface IWorkShiftDal
    {
        /// <summary>
        /// 根據主鍵查詢Model
        /// <param name="model">班別定義model</param>
        /// <param name="deptCode">部門代碼</param>
        /// <param name="orderType">排序</param>
        /// <returns></returns>
        DataTable GetWorkShiftList(WorkShiftModel model, string deptCode, string SQLDep, string effctDate, string expireDate);
        /// <summary>
        /// 根據主鍵查詢Model
        /// <param name="model">班別定義model</param>
        /// <param name="deptCode">部門代碼</param>
        /// <param name="orderType">排序</param>
        /// <param name="pageIndex">當前頁</param>
        /// <param name="pageSize"></param></param>
        /// <param name="totalCount">總頁數</param>
        /// <returns></returns>
        DataTable GetWorkShiftList(WorkShiftModel model, string deptCode, string orderType, string SQLDep,string effctDate, string expireDate, int pageIndex, int pageSize, out int totalCount);
        /// <summary>
        ///查詢全部的記錄
        /// </summary>
        /// <param name="model">給出主鍵值的Model</param>
        /// <returns>返回對應主鍵的Model</returns>
        DataTable GetWorkShiftListAll();
        /// <summary>
        ///查詢失效日期進行管控
        /// </summary>
        /// <param name="shiftNo">班別編號</param>
        /// <returns>班別的有效日期數</returns>
        DataTable GetTypeDay(string shiftNo);
        /// <summary>
        /// 刪除一條班別定義記錄
        /// </summary>
        /// <param name="functionId">要刪除的班別定義組成的model</param>
        /// <returns>刪除是否成功</returns>
        int DeleteShiftByKey(WorkShiftModel model, SynclogModel logmodel);

        /// <summary>
        ///查詢班別是不是已在使用中
        /// </summary>
        /// <param name="shiftNo">班別編號</param>
        /// <returns>辨別是不是在使用中</returns>
        DataTable GetTypeShift(string shiftNo);
        /// <summary>
        ///查詢部門層級進行管控
        /// </summary>
        /// <param name="model">部門層級代碼</param>
        /// <returns>部門層級數</returns>
        DataTable GetDepLevel(string personCode);
        /// <summary>
        /// 插入一條新的班別定義記錄
        /// </summary>
        /// <param name="functionId">要插入的班別定義組成的model</param>
        /// <returns>插入是否成功</returns>
        int InsertShiftByKey(WorkShiftModel model, SynclogModel logmodel);
        /// <summary>
        /// 查詢表中最大的班別
        /// </summary>
        /// <param name="functionId">辨別類型</param>
        /// <returns></returns>
        string SelectMaxShiftNo(string shiftType);
        /// <summary>
        /// 更新一條班別定義記錄
        /// </summary>
        /// <param name="functionId">要更新別定義組成的model</param>
        /// <returns>更新是否成功</returns>
        int UpdateShiftByKey(WorkShiftModel model, SynclogModel logmodel);
        /// <summary>
        /// 將datatable轉換成list
        /// </summary>
        /// <param name="dt">需要轉換的DataTable</param>
        /// <returns>modelList</returns>
        List<WorkShiftModel> GetList(DataTable dt);

        /// <summary>
        /// 獲取派別集
        /// </summary>
        /// <param name="model">排班Model</param>
        /// <returns>班別Model集</returns>
        List<WorkShiftModel> GetShiftType(WorkShiftModel model);
    }
}
