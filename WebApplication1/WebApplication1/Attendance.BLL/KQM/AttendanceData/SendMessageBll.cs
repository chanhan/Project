/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： SendMessageFormBll.cs
 * 檔功能描述： 短信提醒業務邏輯類
 * 
 * 版本：1.0
 * 創建標識：高子焱 2012.01.04
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
    public class SendMessageBll : BLLBase<ISendMessageDal>
    {
        /// <summary>
        /// 按工號發送短信
        /// </summary>
        /// <param name="workNo">工號</param>
        /// <returns></returns>
        public int GetWorkNoData(string workNo)
        {
            return DAL.GetWorkNoData(workNo);
        }
        /// <summary>
        /// 插入到表格中準備發送短信
        /// </summary>
        /// <param name="workNo">工號</param>
        /// <param name="RemindContent">短信內容</param>
        /// <returns></returns>
        public int GetSendMsgByWork(string workNo, string RemindContent, SynclogModel logmodel)
        {
            return DAL.GetSendMsgByWork(workNo, RemindContent,logmodel);
        }
        /// <summary>
        /// 按照選擇的方式發送短信
        /// </summary>
        /// <param name="RemindContent">短信內容</param>
        /// <param name="sendTo">發送方式</param>
        /// <returns></returns>
        public int GetSendMsgByStyle(string RemindContent, string sendTo, SynclogModel logmodel)
        {
            return DAL.GetSendMsgByStyle(RemindContent, sendTo,logmodel);
        }
        /// <summary>
        /// 根據工號查詢用戶信息
        /// </summary>
        /// <param name="workNo">工號</param>
        /// <returns></returns>
        public DataTable GetBasic(string workNo)
        {
            return DAL.GetBasic(workNo);
        }
        /// <summary>
        /// 按照選擇的方式查找用戶信息
        /// </summary>
        /// <param name="sendTo">選擇的代碼</param>
        /// <returns></returns>
        public DataTable GetWorkDataByDll(string sendTo)
        {
            return DAL.GetWorkDataByDll(sendTo);
        }
    }
}
