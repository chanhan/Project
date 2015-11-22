/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： ConfigSendMessageFormBll.cs
 * 檔功能描述： 郵件提醒業務邏輯類
 * 
 * 版本：1.0
 * 創建標識：高子焱 2012.01.03
 * 
 */

using System.Collections.Generic;
using GDSBG.MiABU.Attendance.IDAL.KQM.AttendanceData;
using GDSBG.MiABU.Attendance.Model.KQM.AttendanceData;
using System.Data;
using System.Collections;
using System;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;

namespace GDSBG.MiABU.Attendance.BLL.KQM.AttendanceData
{
    public class ConfigSendMessageFormBll:BLLBase<IConfigSendMessageDal>
    {
        /// <summary>
        /// 查詢用戶有權限的模塊代碼
        /// </summary>
        /// <param name="personCode">工號</param>
        /// <param name="roleCode">角色代碼</param>
        /// <returns></returns>
        public DataTable GetSendMsgDataByKey(string personCode, string roleCode)
        {
            return DAL.GetSendMsgDataByKey(personCode, roleCode);
        }
        /// <summary>
        /// 查詢用戶沒有權限的模塊代碼
        /// </summary>
        /// <param name="personCode">工號</param>
        /// <param name="roleCode">角色代碼</param>
        /// <returns></returns>
        public DataTable GetSendMsgDataNotByKey(string personCode, string roleCode)
        {
            return DAL.GetSendMsgDataNotByKey(personCode, roleCode);
        }
        /// <summary>
        /// 保存樹的數據
        /// </summary>
        /// <param name="personCode">工號</param>
        /// <param name="levels">樹層級</param>
        /// <returns></returns>
        public int UpdateSendMsgByKey(string personCode, Hashtable levels, SynclogModel logmodel)
        {
            return DAL.UpdateSendMsgByKey(personCode, levels,logmodel);
        }

        ///// <summary>
        ///// 保存樹的數據
        ///// </summary>
        ///// <param name="personCode">工號</param>
        ///// <param name="levels">樹層級</param>
        ///// <returns></returns>
        //public int UpdateSendMsgEditByKey(string personcode, string sModuleCode, string sCompanyID, Hashtable DepCodes)
        //{
        //    return DAL.UpdateSendMsgEditByKey(personCode,sModuleCode,sCompanyID,DepCodes);
        //}
        /// <summary>
        /// 查詢用戶的權限組織
        /// </summary>
        /// <param name="personcode">工號</param>
        /// <param name="sModuleCode">模組代碼</param>
        /// <param name="sCompanyID">公司ID</param>
        /// <returns></returns>
        public DataTable GetPersonDeptDataByModule(string personcode, string sModuleCode, string sCompanyID)
        {
            return DAL.GetPersonDeptDataByModule(personcode,sModuleCode,sCompanyID);
        }
        /// <summary>
        /// 保存樹信息
        /// </summary>
        /// <param name="personCode">工號</param>
        /// <param name="moduleCode">模塊代碼</param>
        /// <param name="componyID">公司代碼</param>
        /// <param name="depts">組織名稱</param>
        /// <returns></returns>
        public int SaveSRMData(string personCode, string moduleCode, string componyID, Hashtable depts, SynclogModel logmodel)
        {
            return DAL.SaveSRMData(personCode,moduleCode,componyID, depts,logmodel);
        }
        
    }
}
