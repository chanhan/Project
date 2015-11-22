/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： ReportKaoQinSumDal.cs
 * 檔功能描述： 總部周邊日報表數據訪問層
 * 
 * 版本：1.0
 * 創建標識： 張明強 2011.12.30
 * 
 */

using System;
using GDSBG.MiABU.Attendance.Common.Attributes;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using GDSBG.MiABU.Attendance.IDAL.Hr.KQM.Query;
using GDSBG.MiABU.Attendance.Model.Hr.PCM;
using System.Data.OracleClient;

namespace GDSBG.MiABU.Attendance.OracleDAL.Hr.KQM.Query
{
    public class ReportKaoQinSumDal : DALBase<KaoQinDataModel>, IReportKaoQinSumDal
    {
        /// <summary>
        /// 查詢?
        /// </summary>
        /// <param name="toDate"></param>
        /// <returns></returns>
        public DataTable GetDataBySqlText(string toDate)
        {
            DataTable dt = DalHelper.ExecuteQuery(@"   select sum(aa) sumall,sum(bb) facton,sum(cc) absentqty from (
                                                select count(0) aa ,0 bb ,0 cc from gds_att_EMPLOYEES a 
                                                inner join gds_sc_department b 
                                                on a.DEPCODE = b.DEPCODE
                                                and  a.STATUS = '0' and b.DELETED = 'N'
                                                union all 
                                                select 0,
                                                SUM (CASE WHEN (a.ExceptionType in('A','B','E','O','X','M') or a.ExceptionType is null or (a.status ='3' and a.ExceptionType in('C','D')))  THEN 1 ELSE 0 END) FactOn,
                                                SUM (CASE WHEN (a.status <>'3' and a.ExceptionType in('C','D')or a.ExceptionType in('I','N','P','T','R','U','G','H','S','s','J','K','Y','V','L','W','Z','r','t','d','e','f','g','h','x','k','z')) THEN 1 ELSE 0 END) AbsentQty 
                                                from gds_att_kaoqindata  a inner join  gds_att_employee b
                                                on  
                                                 a.WORKNO = b.WORKNO and b.STATUS = '0' and 
                                                  a.kqdate < to_date(:toDate,'yyyy-MM-dd') + 1 and a.kqdate >= to_date(:toDate,'yyyy-MM-dd')
                                                and exists
                                                (select 0 from gds_sc_department c where c.DEPCODE = b.DEPCODE and c.DELETED = 'N'))", new OracleParameter(":toDate", toDate));
            return dt;
        }
    }
}
