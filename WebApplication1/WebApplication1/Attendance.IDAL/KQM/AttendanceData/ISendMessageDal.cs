/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： ConfigSendMessageDal.cs
 * 檔功能描述： 短信息提醒操作接口
 * 
 * 版本：1.0
 * 創建標識： 高子焱 2012.01.04
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
    ///  短信息提醒操作接口
    /// </summary>
    [RefClass("KQM.AttendanceData.SendMessageDal")]
    public interface ISendMessageDal
    {
        /// <summary>
        /// 按工號發送短信
        /// </summary>
        /// <param name="workNo">工號</param>
        /// <returns></returns>
        int GetWorkNoData(string workNo);
         /// <summary>
        /// 插入到表格中準備發送短信
        /// </summary>
        /// <param name="workNo">工號</param>
        /// <param name="RemindContent">短信內容</param>
        /// <returns></returns>
        int GetSendMsgByWork(string workNo, string RemindContent, SynclogModel logmodel);
        /// <summary>
        /// 按照選擇的方式發送短信
        /// </summary>
        /// <param name="RemindContent">短信內容</param>
        /// <param name="sendTo">發送方式</param>
        /// <returns></returns>
        int GetSendMsgByStyle(string RemindContent, string sendTo, SynclogModel logmodel);
        /// <summary>
        /// 根據工號查詢用戶信息
        /// </summary>
        /// <param name="workNo">工號</param>
        /// <returns></returns>
        DataTable GetBasic(string workNo);
        /// <summary>
        /// 按照選擇的方式查找用戶信息
        /// </summary>
        /// <param name="sendTo">選擇的代碼</param>
        /// <returns></returns>
        DataTable GetWorkDataByDll(string sendTo);
    }
}
