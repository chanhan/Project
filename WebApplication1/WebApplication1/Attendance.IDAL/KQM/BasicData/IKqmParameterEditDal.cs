
/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： IKqmParameterEditDal.cs
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
using System.Data;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;

namespace GDSBG.MiABU.Attendance.IDAL.KQM.BasicData
{
    /// <summary>
    /// 考勤參數設定數據操作接口
    /// </summary>
    [RefClass("KQM.BasicData.KqmParameterEditDal")]
    public interface IKqmParameterEditDal
    {

        /// <summary>
        /// 查詢單位考勤參數信息
        /// </summary>
        /// <param name="model">要查詢的model</param>
        /// <returns>返回的datatable</returns>
        DataTable GetKQMParamsOrgData(AttKQParamsOrgModel model);



        /// <summary>
        /// 新增功能
        /// </summary>
        /// <param name="model">要新增的考勤參數Model</param>
        /// <returns>是否成功</returns>
        bool AddKQMParamsOrgData(AttKQParamsOrgModel model,SynclogModel logmodel);
       

        /// <summary>
        /// 根據主鍵修改功能
        /// </summary>
        /// <param name="model">要修改的考勤參數Model</param>
        /// <returns>是否成功</returns>
        bool UpdateKQMParamsOrgByKey(AttKQParamsOrgModel model,SynclogModel logmodel);
       
        /// <summary>
        /// 刪除考勤參數
        /// </summary>
        /// <param name="model"> 要刪除的model</param>
        /// <returns></returns>
        int DeleteKQMParamsOrgData(AttKQParamsOrgModel model,SynclogModel logmodel);

        /// <summary>
        /// 獲取ParaValue
        /// </summary>
        /// <returns></returns>
        string GetValue(string flag, AttKQParamsOrgModel model);


        /// <summary>
        /// 根據部門編號取得部門名稱
        /// </summary>
        /// <param name="depCode"></param>
        /// <returns></returns>
        string GetDepName(string depCode);
    }
}
