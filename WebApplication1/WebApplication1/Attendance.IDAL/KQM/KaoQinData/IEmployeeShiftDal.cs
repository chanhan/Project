/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： IRealapplyDal.cs
 * 檔功能描述： 排班作業數據操作接口
 * 
 * 版本：1.0
 * 創建標識： 昝望 2011.12.27
 * 
 */


using System;
using System.Collections.Generic;
using System.Text;
using GDSBG.MiABU.Attendance.Common.Attributes;
using System.Data;
using GDSBG.MiABU.Attendance.Model.KQM.KaoQinData;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;

namespace GDSBG.MiABU.Attendance.IDAL.KQM.KaoQinData
{
    [RefClass("KQM.KaoQinData.EmployeeShiftDal")]
    public interface IEmployeeShiftDal
    {
        /// <summary>
        /// 查詢排班作業
        /// </summary>
        /// <param name="model">給出主鍵值的Model</param>
        /// <returns>返回對應主鍵的Model</returns>
        DataTable GetShiftInfo(EmployeeShiftModel model, int pageIndex, int pageSize, out int totalCount, string depcode, string shiftdate, string BatchEmployeeNo, string shift, string sqlDep);

        /// <summary>
        /// 導出查詢排班作業
        /// </summary>
        /// <param name="model">給出主鍵值的Model</param>
        /// <returns>返回對應主鍵的Model</returns>
        DataTable GetShiftInfo(EmployeeShiftModel model, string depcode, string shiftdate, string BatchEmployeeNo, string shift, string sqlDep);

        /// <summary>
        /// 將datatable轉換為List
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        List<EmployeeShiftModel> GetList(DataTable dt);

        /// <summary>
        /// 獲得類別
        /// </summary>
        /// <returns></returns>
        DataTable GetShiftType();

        /// <summary>
        /// 獲得權限下的班別類型
        /// </summary>
        /// <param name="PersonDepCode"></param>
        /// <returns></returns>
        DataTable GetShift(string PersonDepCode);

        /// <summary>
        /// 導入
        /// </summary>
        /// <param name="personcode"></param>
        /// <param name="successnum"></param>
        /// <param name="errornum"></param>
        /// <returns></returns>
        DataTable ImpoertExcel(string personcode, out int successnum, out int errornum, SynclogModel logmodel);


        /// <summary>
        /// 獲得工號的對應信息
        /// </summary>
        /// <param name="workno"></param>
        /// <returns></returns>
        DataTable GetEmployeeInfo(string workno);



        /// <summary>
        /// 獲得對應工號的例外加班信息
        /// </summary>
        /// <param name="workno"></param>
        /// <returns></returns>
        DataTable GetEmployeeShifInfo(string workno);

        /// <summary>
        /// 修改例外排班
        /// </summary>
        /// <param name="model"></param>
        /// <returns>是否成功</returns>
        bool UpdateEmployeeShiftByKey(EmployeeShiftModel model, SynclogModel logmodel);


        /// <summary>
        /// 新增例外排班
        /// </summary>
        /// <param name="model"></param>
        /// <returns>是否成功</returns>
        bool AddEmployeeShift(EmployeeShiftModel model, SynclogModel logmodel);

        /// <summary>
        /// 刪除
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        bool DeleteEmployeeShiftByKey(string ID, SynclogModel logmodel);



        /// <summary>
        /// 查詢對應部門的組織排班
        /// </summary>
        /// <param name="depcode"></param>
        /// <returns></returns>
        DataTable SelectOrgShift(string depcode);


        /// <summary>
        /// 新增組織排班
        /// </summary>
        /// <param name="depcode"></param>
        /// <param name="shiftno"></param>
        /// <param name="startdate"></param>
        /// <param name="endate"></param>
        /// <param name="personcode"></param>
        /// <returns></returns>
        bool AddOrgShift(string depcode, string shiftno, string startdate, string endate, string personcode, SynclogModel logmodel);


        /// <summary>
        /// 更新組織排班
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="shiftno"></param>
        /// <param name="startdate"></param>
        /// <param name="endate"></param>
        /// <returns></returns>
        bool UpdateOrgShift(string ID, string shiftno, string startdate, string endate, string personcode, SynclogModel logmodel);

        /// <summary>
        /// 刪除組織排班
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        bool DeleteOrgShift(string ID, SynclogModel logmodel);
    }
}
