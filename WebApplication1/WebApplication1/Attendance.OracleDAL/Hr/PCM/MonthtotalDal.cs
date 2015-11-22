/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： KQMEmployeeShiftDal.cs
 * 檔功能描述： 月加班匯總查詢數據操作類
 * 
 * 版本：1.0
 * 創建標識： 昝望 2012.1.6
 * 
 */

using System;
using System.Collections.Generic;
using System.Text;
using GDSBG.MiABU.Attendance.Model.KQM.Query.OTM;
using GDSBG.MiABU.Attendance.IDAL.Hr.PCM;
using System.Data;

namespace GDSBG.MiABU.Attendance.OracleDAL.Hr.PCM
{
    public class MonthtotalDal : DALBase<OTMTotalQryModel>, IMonthtotalDal
    {
        /// <summary>
        /// 查詢
        /// </summary>
        /// <param name="personcode"></param>
        /// <param name="yearmonth"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public DataTable GetMonthtotal(string personcode, string yearmonth, int pageIndex, int pageSize, out int totalCount)
        {
            string cmdText = "select * from  gds_att_monthtotal_v where workno='" + personcode + "'";
            if (yearmonth != "")
            {
                cmdText = cmdText + " and YEARMONTH='" + yearmonth + "'";
            }
            DataTable dt = DalHelper.ExecutePagerQuery(cmdText, pageIndex, pageSize, out  totalCount, null);
            return dt;
        }

        /// <summary>
        /// 導出查詢
        /// </summary>
        /// <param name="personcode"></param>
        /// <param name="yearmonth"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public DataTable GetMonthtotal(string personcode, string yearmonth)
        {
            string cmdText = "select * from  gds_att_monthtotal_v where workno='" + personcode + "'";
            if (yearmonth != "")
            {
                cmdText = cmdText + " and YEARMONTH='" + yearmonth + "'";
            }
            DataTable dt = DalHelper.ExecuteQuery(cmdText, null);
            return dt;
        }
    }
}
