/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： IConfigSendMessageDal.cs
 * 檔功能描述： 郵件提醒操作接口
 * 
 * 版本：1.0
 * 創建標識： 高子焱 2012.01.03
 * 
 */

using System;
using GDSBG.MiABU.Attendance.Common.Attributes;
using GDSBG.MiABU.Attendance.Model.KQM.AttendanceData;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;

namespace GDSBG.MiABU.Attendance.IDAL.KQM.AttendanceData
{
    /// <summary>
    ///  郵件提醒操作接口
    /// </summary>
    [RefClass("KQM.AttendanceData.ConfigSendMessageDal")]
    public interface IConfigSendMessageDal
    {
        /// <summary>
        /// 查詢用戶有權限的模塊代碼
        /// </summary>
        /// <param name="personCode">工號</param>
        /// <param name="roleCode">角色代碼</param>
        /// <returns></returns>
        DataTable GetSendMsgDataByKey(string personCode, string roleCode);
        /// <summary>
        /// 查詢用戶沒有權限的模塊代碼
        /// </summary>
        /// <param name="personCode">工號</param>
        /// <param name="roleCode">角色代碼</param>
        /// <returns></returns>
        DataTable GetSendMsgDataNotByKey(string personCode, string roleCode);
        /// <summary>
        /// 保存樹的數據
        /// </summary>
        /// <param name="personCode">工號</param>
        /// <param name="levels">樹層級</param>
        /// <returns></returns>
        int UpdateSendMsgByKey(string personCode, Hashtable levels, SynclogModel logmodel);
        /// <summary>
        /// 查詢用戶的權限組織
        /// </summary>
        /// <param name="personcode">工號</param>
        /// <param name="sModuleCode">模組代碼</param>
        /// <param name="sCompanyID">公司ID</param>
        /// <returns></returns>
        DataTable GetPersonDeptDataByModule(string personcode, string sModuleCode, string sCompanyID);
        /// <summary>
        /// 保存樹信息
        /// </summary>
        /// <param name="personCode">工號</param>
        /// <param name="moduleCode">模塊代碼</param>
        /// <param name="componyID">公司代碼</param>
        /// <param name="depts">組織名稱</param>
        /// <returns></returns>
        int SaveSRMData(string personCode, string moduleCode, string componyID, Hashtable depts, SynclogModel logmodel);
    }
}
