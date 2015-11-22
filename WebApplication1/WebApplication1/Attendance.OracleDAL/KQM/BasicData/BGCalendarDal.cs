/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： BGCalendarDal.cs
 * 檔功能描述： BG行事歷數據操作類
 * 
 * 版本：1.0
 * 創建標識：昝望 2011.12.21
 * 
 */

using System;
using System.Data;
using System.Data.OracleClient;
using GDSBG.MiABU.Attendance.IDAL.KQM.BasicData;
using GDSBG.MiABU.Attendance.Model.KQM.BasicData;
using System.Collections;
using System.Collections.Generic;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;
namespace GDSBG.MiABU.Attendance.OracleDAL.KQM.BasicData
{
    public class BGCalendarDal : DALBase<BGCalendarModel>, IBGCalendarDal
    {
       /// <summary>
       /// 獲得組織信息
       /// </summary>
       /// <returns></returns>
       public DataTable GetDepartment()
       {
           string str = "select depcode,depname from gds_sc_department where levelcode='0'";
           return DalHelper.ExecuteQuery(str);
       }

        /// <summary>
        /// 獲得星期幾
        /// </summary>
        /// <param name="sDate"></param>
        /// <returns></returns>
       public DataTable GetValue(string sDate)
       {
           string str = "SELECT TO_NUMBER(TO_CHAR (to_date('" + sDate + "','yyyy/MM/dd'), 'iw')) workno FROM DUAL";
           return DalHelper.ExecuteQuery(str);
       }

        /// <summary>
        /// 查詢
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
       public DataTable GetBGCalendar(BGCalendarModel model)
        {
            return DalHelper.Select(model, null);
        }


       /// <summary>
       /// 根據主鍵修改BG行事歷
       /// </summary>
       /// <param name="model"></param>
       /// <returns></returns>
       public bool UpdateBGCalendarByKey(BGCalendarModel model, SynclogModel logmodel)
       {
           return DalHelper.UpdateByKey(model, true,logmodel) != -1;
       }

    }
}
