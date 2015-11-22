/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： IRealapplyDal.cs
 * 檔功能描述： 有效加班數據操作接口
 * 
 * 版本：1.0
 * 創建標識： 昝望 2011.12.23
 * 
 */

using System;
using System.Collections.Generic;
using GDSBG.MiABU.Attendance.Common.Attributes;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;
using System.Data;
using GDSBG.MiABU.Attendance.Model.HRM;
using GDSBG.MiABU.Attendance.Model.KQM.OTM;
namespace GDSBG.MiABU.Attendance.IDAL.KQM.OTM
{
    [RefClass("KQM.OTM.RealapplyDal")]
    public interface IRealapplyDal
    {
        /// <summary>
        /// 查詢有效加班
        /// </summary>
        /// <param name="model">給出主鍵值的Model</param>
        /// <returns>返回對應主鍵的Model</returns>
        DataTable GetRealapply(RealapplyModel model, int pageIndex, int pageSize, out int totalCount, string symbol, string Hours, string OTDateFrom, string OTDateTo, string BatchEmployeeNo,string sqlDep);


        /// <summary>
        /// 將datatable轉換為List
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        List<RealapplyModel> GetList(DataTable dt);

        /// <summary>
        /// 查詢加班類型
        /// </summary>
        /// <returns></returns>
        DataTable GetOverTimeType();

        /// <summary>
        /// 查詢核准狀態
        /// </summary>
        /// <returns></returns>
        DataTable GetOTMAdvanceApplyStatus();


        /// <summary>
        /// 刪除
        /// </summary>
        /// <param name="functionId">/param>
        /// <returns></returns>
        int DeleteRealapplyByKey(string ID, SynclogModel logmodel);

        /// <summary>
        /// 取消簽核
        /// </summary>
        /// <param name="model"></param>
        /// <returns>是否成功</returns>
        int UpdateRealapplyByKey(RealapplyModel model, SynclogModel logmodel);

        
        /// <summary>
        /// 查詢未轉入有效的預報加班
        /// </summary>
        /// <param name="decode"></param>
        /// <param name="isproject"></param>
        /// <param name="employeeno"></param>
        /// <param name="name"></param>
        /// <param name="otdatefrom"></param>
        /// <param name="otdateto"></param>
        /// <returns></returns>
        DataTable SelectAdvanceapply(string decode, string isproject, string employeeno, string name, string otdatefrom, string otdateto, string sqlDep);

          /// <summary>
        /// 轉入有效加班
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="updateuser"></param>
        /// <returns></returns>
        int UpdateAdvanceapply(string ID, string updateuser, SynclogModel logmodel);
    }
}
