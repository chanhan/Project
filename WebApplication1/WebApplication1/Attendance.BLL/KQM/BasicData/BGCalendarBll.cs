/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： BGCalendarBll.cs
 * 檔功能描述： BG行事歷業務邏輯類
 * 
 * 版本：1.0
 * 創建標識：昝望 2011.12.21
 * 
 */

using System.Collections.Generic;
using GDSBG.MiABU.Attendance.IDAL.KQM.BasicData;
using GDSBG.MiABU.Attendance.Model.KQM.BasicData;
using System.Data;
using System.Collections;
using System;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;


namespace GDSBG.MiABU.Attendance.BLL.KQM.BasicData
{
    public class BGCalendarBll : BLLBase<IBGCalendarDal>
    {
        /// <summary>
        /// 獲得組織信息
        /// </summary>
        /// <returns></returns>
        public DataTable GetDepartment()
        {
            return DAL.GetDepartment();
        }

              /// <summary>
        /// 獲得星期幾
        /// </summary>
        /// <param name="sDate"></param>
        /// <returns></returns>
        public DataTable GetValue(string sDate)
        {
            return DAL.GetValue(sDate);
        }

        /// <summary>
        /// 查詢
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public DataTable GetBGCalendar(BGCalendarModel model)
        {
            return DAL.GetBGCalendar(model);
        }

            /// <summary>
       /// 根據主鍵修改BG行事歷
       /// </summary>
       /// <param name="model"></param>
       /// <returns></returns>
        public bool UpdateBGCalendarByKey(BGCalendarModel model, SynclogModel logmodel)
        {
            return DAL.UpdateBGCalendarByKey(model,logmodel);
        }
    }
}
