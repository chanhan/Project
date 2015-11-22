/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： AbsentReporterDal.cs
 * 檔功能描述：缺勤統計表數據操作類
 * 
 * 版本：1.0
 * 創建標識： 顧陳偉 2012.3.15
 * 
 * 
 */

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using GDSBG.MiABU.Attendance.IDAL.WFReporter;
using GDSBG.MiABU.Attendance.Model.WFReporter;

namespace GDSBG.MiABU.Attendance.OracleDAL.WFReporter
{
    public class AbsentReporterDal : DALBase<AbsentReporterModel>, IAbsentReportDal
    {
        /// <summary>
        /// 缺勤統計
        /// </summary>
        /// <param name="depcode">組織</param>
        /// <param name="workno">工號</param>
        /// <param name="localname">姓名</param>
        /// <param name="fromdate">起始時間</param>
        /// <param name="todate">截止時間</param>
        /// <param name="yearmonth">年月</param>
        /// <param name="absenttype">缺勤類型</param>
        /// <param name="status">狀態</param>
        /// <returns></returns>
        public DataTable GetAbsentInfo(string sql, string depcode, string workno, string localname, string fromdate, string todate, string yearmonth, string absenttype, string status, int pageIndex, int pageSize, out int totalCount)
        {
            string cmd = @"select a.*,b.remark from (
                          SELECT a.*, b.bgname, b.dname depname, b.dcode depcode, b.localname
                          FROM (SELECT NVL (a.workno, b.workno) workno, a.ab, a.c, a.i, a.t, a.j, a.s,
                                       a.k, a.v, a.y,a.r, a.x, a.z, b.numcount
                                  FROM (SELECT   workno,
                         SUM (DECODE (exceptiontype,
                                      'A', absentqty,
                                      'B', absentqty
                                     )
                             ) ab,                                  
                         ROUND (SUM (DECODE (exceptiontype,
                                             'C', absentqty / 480
                                            )
                                    ),
                                2
                               ) c,                                   
                         ROUND (SUM (DECODE (exceptiontype,
                                             'I', absentqty / 480
                                            )
                                    ),
                                2
                               ) i,                               
                         ROUND (SUM (DECODE (exceptiontype,
                                             'T', absentqty / 480
                                            )
                                    ),
                                2
                               ) t,                                     
                         ROUND (SUM (DECODE (exceptiontype,
                                             'J', absentqty / 480
                                            )
                                    ),
                                2
                               ) j,                                     
                         ROUND (SUM (DECODE (exceptiontype,
                                             'S', absentqty / 480,
                                             's', absentqty / 480,
                                             'h', absentqty / 480,
                                             'g', absentqty / 480,
                                             'f', absentqty / 480,
                                             'e', absentqty / 480,
                                             'd', absentqty / 480,
                                             'z', absentqty / 480
                                            )
                                    ),
                                2
                               ) s,                                     
                         ROUND (SUM (DECODE (exceptiontype,
                                             'K', absentqty / 480
                                            )
                                    ),
                                2
                               ) k,                                     
                         ROUND (SUM (DECODE (exceptiontype,
                                             'V', absentqty / 480
                                            )
                                    ),
                                2
                               ) v,                                  
                         ROUND (SUM (DECODE (exceptiontype,
                                             'Y', absentqty / 480
                                            )
                                    ),
                                2
                               ) y,
                         ROUND (SUM (DECODE (exceptiontype,
                                             'r', absentqty / 480,
                                             'W', absentqty / 480,
                                             'R', absentqty / 480
                                            )
                                    ),
                                2
                               ) r,                                    
                         ROUND (SUM (DECODE (exceptiontype,
                                             'x', absentqty / 480
                                            )
                                    ),
                                2
                               ) x,                    
                         ROUND (SUM (DECODE (exceptiontype,
                                             'Z', absentqty / 480
                                            )
                                    ),
                                2
                               ) z                                    
                    FROM gds_att_kaoqindata_v where 1=1";
            if (!string.IsNullOrEmpty(fromdate))
            {
                cmd += " and kqdate >= to_date('" + DateTime.Parse(fromdate).ToString("yyyy/MM/dd") + "','yyyy/mm/dd') ";
            }
            if (!string.IsNullOrEmpty(todate))
            {
                cmd += " and kqdate <= to_date('" + DateTime.Parse(todate).ToString("yyyy/MM/dd") + "','yyyy/mm/dd') ";
            }
            if (!string.IsNullOrEmpty(yearmonth))
            {
                cmd += " and to_char(kqdate,'yyyymm')='" + yearmonth + "'";
            }
            cmd += @"GROUP BY workno) a
                   FULL JOIN
                   (SELECT   workno, COUNT (*) numcount
                        FROM gds_att_makeup
                       WHERE reasontype IN ('A', 'd')";
            if (!string.IsNullOrEmpty(fromdate))
            {
                cmd += " and kqdate >= to_date('" + DateTime.Parse(fromdate).ToString("yyyy/MM/dd") + "','yyyy/mm/dd') ";
            }
            if (!string.IsNullOrEmpty(todate))
            {
                cmd += " and kqdate <= to_date('" + DateTime.Parse(todate).ToString("yyyy/MM/dd") + "','yyyy/mm/dd') ";
            }
            if (!string.IsNullOrEmpty(yearmonth))
            {
                cmd += " and to_char(kqdate,'yyyymm')='" + yearmonth + "'";
            }
            cmd += @"GROUP BY workno
                  HAVING COUNT (*) > 3) b ON a.workno = b.workno
                  ) a,
                       gds_att_employee_v b
                 WHERE a.workno = b.workno
                   AND    a.ab
                       || a.c
                       || a.i
                       || a.t
                       || a.j
                       || a.s
                       || a.k
                       || a.v
                       || a.y
                       || a.r
                       || a.x
                       || a.z
                       || a.numcount IS NOT NULL";
            if (!string.IsNullOrEmpty(depcode))
            {
                cmd += "AND b.depcode IN ((" + sql + ") INTERSECT SELECT DepCode FROM gds_sc_department START WITH depcode = '" + depcode + "' CONNECT BY PRIOR depcode = parentdepcode) ";
            }
            else
            {
                cmd += " AND b.depcode IN (" + sql + ") ";
            }
            if (!string.IsNullOrEmpty(workno))
            {
                cmd += " and a.workno='" + workno + "'";
            }
            if (!string.IsNullOrEmpty(localname))
            {
                cmd += " and b.localname='" + localname + "'";
            }
            if (!string.IsNullOrEmpty(absenttype))
            {
                cmd += " and a." + absenttype + ">0";
            }
            cmd += @") a,
                                       (SELECT   workno, gds_att_strcat (remark) remark
    FROM (SELECT   workno,
                      DECODE (startdate,
                              enddate, TO_CHAR (startdate, 'mm/dd'),
                                 TO_CHAR (startdate, 'mm/dd')
                              || '-'
                              || TO_CHAR (enddate, 'mm/dd')
                             )
                   || CASE
                         WHEN lvtypecode = 'I'
                            THEN '事假'
                         WHEN lvtypecode = 'T'
                            THEN '病假'
                         WHEN lvtypecode = 'J'
                            THEN '婚假'
                         WHEN lvtypecode IN
                                     ('S', 's', 'h', 'g', 'f', 'e', 'd', 'z')
                            THEN '產假'
                         WHEN lvtypecode = 'K'
                            THEN '喪假'
                         WHEN lvtypecode = 'V'
                            THEN '節育假'
                         WHEN lvtypecode = 'Y'
                            THEN '年休假'
                         WHEN lvtypecode IN ('R', 'W', 'r')
                            THEN '公休假'
                         WHEN lvtypecode = 'x'
                            THEN '因私事假'
                         WHEN lvtypecode = 'Z'
                            THEN '醫療期'
                      END
                   || (CASE
                          WHEN lvtotal < 8
                             THEN lvtotal || 'H'
                          ELSE ROUND(lvtotal / 8,2) || '天'
                       END
                      ) remark
              FROM gds_sc_leavequerydata_v
             WHERE lvtypecode IN
                      ('I', 'T', 'J', 'S', 's', 'h', 'g', 'f', 'e', 'd', 'z',
                       'K', 'V', 'Y', 'R', 'W', 'r', 'x', 'Z')";
            cmd+=" AND (   startdate BETWEEN TO_DATE (NVL ('"+fromdate+"','1000/01/01'),'yyyy/mm/dd')"
                 +"AND TO_DATE (NVL ('"+todate+"','9999/01/01'),'yyyy/mm/dd') OR enddate BETWEEN TO_DATE"
                 +"(NVL ('"+fromdate+"', '1000/01/01'),'yyyy/mm/dd') AND TO_DATE (NVL ('"+todate+"', '9999/01/01'),"
                 +"'yyyy/mm/dd'))";
            if (!string.IsNullOrEmpty(yearmonth))
            {
                cmd += @" AND ((   TO_CHAR (startdate, 'yyyymm') = '" + yearmonth + "' OR TO_CHAR (enddate, 'yyyymm') = '" + yearmonth + "'))";
            }
            cmd += "ORDER BY workno, startdate) GROUP BY workno) b where a.workno=b.workno(+) order by a.bgname,a.depcode,a.localname";
            return DalHelper.ExecutePagerQuery(cmd, pageIndex, pageSize, out totalCount);
        }

        /// <summary>
        /// 導出查詢
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="depcode"></param>
        /// <param name="workno"></param>
        /// <param name="localname"></param>
        /// <param name="fromdate"></param>
        /// <param name="todate"></param>
        /// <param name="yearmonth"></param>
        /// <param name="absenttype"></param>
        /// <param name="status"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public DataTable GetAbsentInfoForExport(string sql, string depcode, string workno, string localname, string fromdate, string todate, string yearmonth, string absenttype, string status)
        {
            string cmd = @"select a.*,b.remark from (
                          SELECT a.*, b.bgname, b.dname depname, b.dcode depcode, b.localname
                          FROM (SELECT NVL (a.workno, b.workno) workno, a.ab, a.c, a.i, a.t, a.j, a.s,
                                       a.k, a.v, a.y,a.r, a.x, a.z, b.numcount
                                  FROM (SELECT   workno,
                         SUM (DECODE (exceptiontype,
                                      'A', absentqty,
                                      'B', absentqty
                                     )
                             ) ab,                                  
                         ROUND (SUM (DECODE (exceptiontype,
                                             'C', absentqty / 480
                                            )
                                    ),
                                2
                               ) c,                                   
                         ROUND (SUM (DECODE (exceptiontype,
                                             'I', absentqty / 480
                                            )
                                    ),
                                2
                               ) i,                               
                         ROUND (SUM (DECODE (exceptiontype,
                                             'T', absentqty / 480
                                            )
                                    ),
                                2
                               ) t,                                     
                         ROUND (SUM (DECODE (exceptiontype,
                                             'J', absentqty / 480
                                            )
                                    ),
                                2
                               ) j,                                     
                         ROUND (SUM (DECODE (exceptiontype,
                                             'S', absentqty / 480,
                                             's', absentqty / 480,
                                             'h', absentqty / 480,
                                             'g', absentqty / 480,
                                             'f', absentqty / 480,
                                             'e', absentqty / 480,
                                             'd', absentqty / 480,
                                             'z', absentqty / 480
                                            )
                                    ),
                                2
                               ) s,                                     
                         ROUND (SUM (DECODE (exceptiontype,
                                             'K', absentqty / 480
                                            )
                                    ),
                                2
                               ) k,                                     
                         ROUND (SUM (DECODE (exceptiontype,
                                             'V', absentqty / 480
                                            )
                                    ),
                                2
                               ) v,                                  
                         ROUND (SUM (DECODE (exceptiontype,
                                             'Y', absentqty / 480
                                            )
                                    ),
                                2
                               ) y,
                         ROUND (SUM (DECODE (exceptiontype,
                                             'r', absentqty / 480,
                                             'W', absentqty / 480,
                                             'R', absentqty / 480
                                            )
                                    ),
                                2
                               ) r,                                    
                         ROUND (SUM (DECODE (exceptiontype,
                                             'x', absentqty / 480
                                            )
                                    ),
                                2
                               ) x,                    
                         ROUND (SUM (DECODE (exceptiontype,
                                             'Z', absentqty / 480
                                            )
                                    ),
                                2
                               ) z                                    
                    FROM gds_att_kaoqindata_v where 1=1";
            if (!string.IsNullOrEmpty(fromdate))
            {
                cmd += " and kqdate >= to_date('" + DateTime.Parse(fromdate).ToString("yyyy/MM/dd") + "','yyyy/mm/dd') ";
            }
            if (!string.IsNullOrEmpty(todate))
            {
                cmd += " and kqdate <= to_date('" + DateTime.Parse(todate).ToString("yyyy/MM/dd") + "','yyyy/mm/dd') ";
            }
            if (!string.IsNullOrEmpty(yearmonth))
            {
                cmd += " and to_char(kqdate,'yyyymm')='" + yearmonth + "'";
            }
            cmd += @"GROUP BY workno) a
                   FULL JOIN
                   (SELECT   workno, COUNT (*) numcount
                        FROM gds_att_makeup
                       WHERE reasontype IN ('A', 'd')";
            if (!string.IsNullOrEmpty(fromdate))
            {
                cmd += " and kqdate >= to_date('" + DateTime.Parse(fromdate).ToString("yyyy/MM/dd") + "','yyyy/mm/dd') ";
            }
            if (!string.IsNullOrEmpty(todate))
            {
                cmd += " and kqdate <= to_date('" + DateTime.Parse(todate).ToString("yyyy/MM/dd") + "','yyyy/mm/dd') ";
            }
            if (!string.IsNullOrEmpty(yearmonth))
            {
                cmd += " and to_char(kqdate,'yyyymm')='" + yearmonth + "'";
            }
            cmd += @"GROUP BY workno
                  HAVING COUNT (*) > 3) b ON a.workno = b.workno
                  ) a,
                       gds_att_employee_v b
                 WHERE a.workno = b.workno
                   AND    a.ab
                       || a.c
                       || a.i
                       || a.t
                       || a.j
                       || a.s
                       || a.k
                       || a.v
                       || a.y
                       || a.r
                       || a.x
                       || a.z
                       || a.numcount IS NOT NULL";
            if (!string.IsNullOrEmpty(depcode))
            {
                cmd += "AND b.depcode IN ((" + sql + ") INTERSECT SELECT DepCode FROM gds_sc_department START WITH depcode = '" + depcode + "' CONNECT BY PRIOR depcode = parentdepcode) ";
            }
            else
            {
                cmd += " AND b.depcode IN (" + sql + ") ";
            }
            if (!string.IsNullOrEmpty(workno))
            {
                cmd += " and a.workno='" + workno + "'";
            }
            if (!string.IsNullOrEmpty(localname))
            {
                cmd += " and b.localname='" + localname + "'";
            }
            if (!string.IsNullOrEmpty(absenttype))
            {
                cmd += " and upper(a." + absenttype + ")>0";
            }
            cmd += @") a,
                                       (SELECT   workno, gds_att_strcat (remark) remark
    FROM (SELECT   workno,
                      DECODE (startdate,
                              enddate, TO_CHAR (startdate, 'mm/dd'),
                                 TO_CHAR (startdate, 'mm/dd')
                              || '-'
                              || TO_CHAR (enddate, 'mm/dd')
                             )
                   || CASE
                         WHEN lvtypecode = 'I'
                            THEN '事假'
                         WHEN lvtypecode = 'T'
                            THEN '病假'
                         WHEN lvtypecode = 'J'
                            THEN '婚假'
                         WHEN lvtypecode IN
                                     ('S', 's', 'h', 'g', 'f', 'e', 'd', 'z')
                            THEN '產假'
                         WHEN lvtypecode = 'K'
                            THEN '喪假'
                         WHEN lvtypecode = 'V'
                            THEN '節育假'
                         WHEN lvtypecode = 'Y'
                            THEN '年休假'
                         WHEN lvtypecode IN ('R', 'W', 'r')
                            THEN '公休假'
                         WHEN lvtypecode = 'x'
                            THEN '因私事假'
                         WHEN lvtypecode = 'Z'
                            THEN '醫療期'
                      END
                   || (CASE
                          WHEN lvtotal < 8
                             THEN lvtotal || 'H'
                          ELSE ROUND(lvtotal / 8,2) || '天'
                       END
                      ) remark
              FROM gds_sc_leavequerydata_v
             WHERE lvtypecode IN
                      ('I', 'T', 'J', 'S', 's', 'h', 'g', 'f', 'e', 'd', 'z',
                       'K', 'V', 'Y', 'R', 'W', 'r', 'x', 'Z')
               AND (   startdate BETWEEN TO_DATE (NVL (:fromdate,
                                                       '1000/01/01'),
                                                  'yyyy/mm/dd'
                                                 )
                                     AND TO_DATE (NVL (:todate, '9999/01/01'),
                                                  'yyyy/mm/dd'
                                                 )
                    OR enddate BETWEEN TO_DATE (NVL (:fromdate, '1000/01/01'),
                                                'yyyy/mm/dd'
                                               )
                                   AND TO_DATE (NVL (:todate, '9999/01/01'),
                                                'yyyy/mm/dd'
                                               )
                   )";
            if (!string.IsNullOrEmpty(yearmonth))
            {
                cmd += @" AND ((   TO_CHAR (startdate, 'yyyymm') = '" + yearmonth + "' OR TO_CHAR (enddate, 'yyyymm') = '" + yearmonth + "'))";
            }
            cmd += "ORDER BY workno, startdate) GROUP BY workno) b where a.workno=b.workno(+) order by a.bgname,a.depcode,a.localname";
            return DalHelper.ExecuteQuery(cmd, new OracleParameter(":fromdate", fromdate), new OracleParameter(":todate", todate));
        }

        /// <summary>
        /// 將datatable轉換為List
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public List<AbsentReporterModel> GetList(DataTable dt)
        {
            return OrmHelper.SetDataTableToList(dt);
        }
    }
}
