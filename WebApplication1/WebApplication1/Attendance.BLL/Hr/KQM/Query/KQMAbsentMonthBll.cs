
/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： KQMAbsentMonthBll.cs
 * 檔功能描述：缺勤統計報表業務邏輯類
 * 
 * 版本：1.0
 * 創建標識： 張明強 2011.12.8
 * 
 */
using System.Collections.Generic;
using GDSBG.MiABU.Attendance.IDAL.Hr.KQM.Query;
using System.Data;
using System;

namespace GDSBG.MiABU.Attendance.BLL.Hr.KQM.Query
{
    public class KQMAbsentMonthBll : BLLBase<IKQMAbsentMonthDal>
    {

        /// <summary>
        /// 異常原因類別
        /// </summary>
        /// <returns></returns>
        public DataTable GetExceptReason()
        {
            return DAL.GetExceptReason();
        }


          /// <summary>
        /// 獲得Paravalue
        /// </summary>
        /// <returns></returns>
        public DataTable GetParavalue()
        {
            return DAL.GetParavalue();
        }

             /// <summary>
        /// 廠區
        /// </summary>
        /// <returns></returns>
        public DataTable GetAreaCode()
        {
            return DAL.GetAreaCode();
        }

        /// <summary>
        /// 驗證日期
        /// </summary>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <returns></returns>
        public  bool CheckDateMonths(string StartDate, string EndDate)
        {
            DataTable dt = DAL.CheckDateMonths(StartDate,EndDate);

            try
            {
                if (Convert.ToInt32(dt.Rows[0][0].ToString()) >= 3)
                {
                   
                    return false;
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        /// <summary>
        /// 驗證出勤日報表是否有數據
        /// </summary>
        /// <param name="personcode"></param>
        /// <param name="successnum"></param>
        /// <param name="errornum"></param>
        /// <returns></returns>
        public int KaoQinDay_val(string kqdate, string personcode, string modulecode, string companyid, string depcode)
        {
            return DAL.KaoQinDay_val(kqdate,personcode,modulecode,companyid,depcode);
        }

        /// <summary>
        /// 驗證缺勤統計報表是否有數據
        /// </summary>
        /// <param name="personcode"></param>
        /// <param name="successnum"></param>
        /// <param name="errornum"></param>
        /// <returns></returns>
        public int AbsentMonth_val(string personcode, string modulecode, string companyid, string depcode, string startdate, string enddate, string workno)
        {
            return DAL.AbsentMonth_val(personcode,modulecode,companyid,depcode,startdate ,enddate ,workno );
        }
    }
}
