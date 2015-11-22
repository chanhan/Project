/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： IOrgmanagerDal.cs
 * 檔功能描述： 組織管理者資料數據操作接口
 * 
 * 版本：1.0
 * 創建標識： 昝望 2011.12.12
 * 
 */

using System;
using System.Collections.Generic;
using GDSBG.MiABU.Attendance.Common.Attributes;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;
using System.Data;
using GDSBG.MiABU.Attendance.Model.HRM;

namespace GDSBG.MiABU.Attendance.IDAL.HRM
{
    [RefClass("HRM.OrgmanagerDal")]
    public interface IOrgmanagerDal
    {
        /// <summary>
        /// 查詢組織管理者資料
        /// </summary>
        /// <param name="model">給出主鍵值的Model</param>
        /// <returns>返回對應主鍵的Model</returns>
        DataTable GetOrgmanager(OrgmanagerModel model, int pageIndex, int pageSize, out int totalCount, string sqlDep);


             /// <summary>
        /// 查詢組織管理者資料(用於Ajax)
        /// </summary>
        /// <param name="model">給出主鍵值的Model</param>
        /// <returns>返回對應主鍵的Model</returns>
        DataTable GetOrgmanager(OrgmanagerModel model);

        /// <summary>
        /// 新增組織管理者
        /// </summary>
        /// <param name="model">要新增的組織管理者Model</param>
        /// <returns>是否成功</returns>
        int AddOrgmanager(OrgmanagerModel model, SynclogModel logmodel);

        /// <summary>
        /// 根據主鍵修改組織管理者資料
        /// </summary>
        /// <param name="model">要修改的組織管理者Model</param>
        /// <returns>是否成功</returns>
        int UpdateOrgmanagerByKey(OrgmanagerModel model,string olddepcode,string oldworkno, SynclogModel logmodel);


        /// <summary>
        /// 刪除一個組織管理者
        /// </summary>
        /// <param name="functionId">要刪除的組織管理者Id</param>
        /// <returns>刪除組織管理者條數</returns>
        int DeleteOrgmanagerByKey(string workno, string depCode, SynclogModel logmodel);

        /// <summary>
        /// 更新人員基本表
        /// </summary>
        /// <param name="WorkNo"></param>
        /// <param name="notes"></param>
        /// <param name="managercode"></param>
        /// <param name="managername"></param>
        /// <returns></returns>
        bool UpdateEmployeeByKey(string WorkNo, string notes, string managercode, string managername, SynclogModel logmodel);

        /// <summary>
        /// 獲得管理職信息
        /// </summary>
        /// <returns></returns>
        DataTable GetManager();

        /// <summary>
        /// 查詢工號對應的姓名與管理職
        /// </summary>
        /// <returns></returns>
        DataTable GetInfo(string workno);
  
       /// <summary>
       /// 導入
       /// </summary>
       /// <param name="personcode"></param>
       /// <param name="successnum"></param>
       /// <param name="errornum"></param>
       /// <returns></returns>
        DataTable ImpoertExcel(string personcode, out int successnum, out int errornum, SynclogModel logmodel);

         /// <summary>
        /// 將datatable轉換為List
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
       List<OrgmanagerModel> GetList(DataTable dt);


        /// <summary>
        /// 組織人員查詢
        /// </summary>
        /// <param name="personcode"></param>
        /// <param name="workno"></param>
        /// <param name="depcode"></param>
        /// <returns></returns>
       DataTable OrgEmployeeQuery(string personcode, string workno, string depcode);
  
    }
}
