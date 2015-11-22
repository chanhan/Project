/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： IModuleDal.cs
 * 檔功能描述： 模組管理數據操作接口
 * 
 * 版本：1.0
 * 創建標識： 高子焱 2011.11.30
 * 
 */

using System;
using System.Collections.Generic;
using GDSBG.MiABU.Attendance.Common.Attributes;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;
using System.Data;

namespace GDSBG.MiABU.Attendance.IDAL.SystemManage.SystemSafety
{
    [RefClass("SystemManage.SystemSafety.ChangPwdDal")]
    public interface IChangPwdDal
    {
        /// <summary>
        /// 更新用戶信息
        /// </summary>
        /// <param name="model">用戶Model</param>
        /// <returns>是否成功</returns>
        int UpdateUserByKey(string userno, string oldpwd, string newpwd, SynclogModel logmodel);

        /// <summary>
        /// 更新用戶信息
        /// </summary>
        /// <param name="model">用戶Model</param>
        /// <returns>是否成功</returns>
        bool UpdateMailByKey(PersonModel model, SynclogModel logmodel);
        /// <summary>
        /// 根據登錄人的工號查詢登錄人的信息（郵件、分機和手機）
        /// </summary>
        /// <param name="personCode"></param>
        /// <returns></returns>
        DataTable GetPerInfo(string personCode);
    }
}
