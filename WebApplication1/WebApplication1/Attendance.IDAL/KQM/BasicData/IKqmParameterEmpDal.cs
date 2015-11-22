
/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： IKqmParameterEmpDal.cs
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
    [RefClass("KQM.BasicData.KqmParameterEmpDal")]
    public interface IKqmParameterEmpDal
    {
        /// <summary>
        /// 查詢人員考勤參數信息
        /// </summary>
        /// <param name="model">要查詢的model</param>
        /// <returns>返回的datatable</returns>
        DataTable GetKQMParamsEmpData(AttKQParamsEmpModel model, string sqlDep);

        /// <summary>
        /// 刪除考勤參數
        /// </summary>
        /// <param name="model"> 要刪除的model</param>
        /// <returns></returns>
        int DeleteKQMParamsEmpData(AttKQParamsEmpModel model,SynclogModel logmodel);



        /// <summary>
        /// 根據model數據
        /// </summary>
        /// <param name="model">要查詢的功能Model</param>
        /// <returns>功能模組清單集</returns>
        List<AttKQParamsEmpModel> GetParamsEmpList(AttKQParamsEmpModel model, string sqlDep);

        /// <summary>
        /// 分頁查詢
        /// </summary>
        /// <param name="model"></param>
        /// <param name="?"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        DataTable GetParamsEmpList(AttKQParamsEmpModel model,string sqlDep, int pageIndex, int pageSize, out int totalCount);
        
    }

}
