/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： IBGcalendarDal.cs
 * 檔功能描述： BG行事歷操作接口
 * 
 * 版本：1.0
 * 創建標識： 昝望 2011.12.21
 * 
 */

using System;
using GDSBG.MiABU.Attendance.Common.Attributes;
using GDSBG.MiABU.Attendance.Model.KQM.BasicData;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;


namespace GDSBG.MiABU.Attendance.IDAL.KQM.BasicData
{
    [RefClass("KQM.BasicData.BGCalendarDal")]
    public interface IBGCalendarDal
    {

        /// <summary>
        /// 獲得組織信息
        /// </summary>
        /// <returns></returns>
        DataTable GetDepartment();

        /// <summary>
        /// 獲得星期幾
        /// </summary>
        /// <param name="sDate"></param>
        /// <returns></returns>
        DataTable GetValue(string sDate);

        /// <summary>
        /// 查詢
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        DataTable GetBGCalendar(BGCalendarModel model);

        /// <summary>
        /// 根據主鍵修改BG行事歷
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool UpdateBGCalendarByKey(BGCalendarModel model, SynclogModel logmodel);

    }
}
