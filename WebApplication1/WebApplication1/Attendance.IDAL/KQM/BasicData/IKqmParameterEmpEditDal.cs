
/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： IKqmParameterEmpEditDal.cs
 * 檔功能描述： 考勤參數設定數據操作接口
 * 
 * 版本：1.0
 * 創建標識： 張明強 2011.12.13
 * 
 */

using System;
using System.Collections.Generic;
using GDSBG.MiABU.Attendance.Common.Attributes;
using GDSBG.MiABU.Attendance.Model.KQM.BasicData;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;
using System.Data;

namespace GDSBG.MiABU.Attendance.IDAL.KQM.BasicData
{
    /// <summary>
    /// 考勤參數設定數據操作接口
    /// </summary>
    [RefClass("KQM.BasicData.KqmParameterEmpEditDal")]
    public  interface IKqmParameterEmpEditDal
    {
        /// <summary>
        /// 查詢個人考勤參數信息
        /// </summary>
        /// <param name="model">要查詢的model</param>
        /// <returns>返回的datatable</returns>
        DataTable GetKQMParamsEmpData(AttKQParamsEmpEditModel model);

        /// <summary>
        /// 查詢員工信息
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        DataTable GetVDataByCondition(string employeeNo, string sqlDep);
 

        /// <summary>
        /// 新增功能
        /// </summary>
        /// <param name="model">要新增的考勤參數Model</param>
        /// <returns>是否成功</returns>
        bool AddKQMParamsEmpData(AttKQParamsEmpEditModel model,SynclogModel logmodel);


        /// <summary>
        /// 根據主鍵修改功能
        /// </summary>
        /// <param name="model">要修改的考勤參數Model</param>
        /// <returns>是否成功</returns>
        bool UpdateKQMParamsEmpByKey(AttKQParamsEmpEditModel model,SynclogModel logmodel);

        /// <summary>
        /// 刪除考勤參數
        /// </summary>
        /// <param name="model"> 要刪除的model</param>
        /// <returns></returns>
        int DeleteKQMParamsEmpData(AttKQParamsEmpEditModel model,SynclogModel logmodel);
    }
}
