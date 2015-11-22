/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： KQMSelfServiceDal.cs
 * 檔功能描述：員工自助查詢數據操作類
 * 
 * 版本：1.0
 * 創建標識： 陳函 2012.02.03
 * 
 */
using System;
using System.Data;
using System.Data.OracleClient;
using GDSBG.MiABU.Attendance.IDAL.KQM.Query;
using GDSBG.MiABU.Attendance.Model.KQM.Query;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;

namespace GDSBG.MiABU.Attendance.OracleDAL.KQM.Query
{
    public class KQMSelfServiceDal : DALBase<EvectionApplyVModel>, IKQMSelfServiceDal
    {
        /// <summary>
        /// 獲取人員信息
        /// </summary>
        /// <param name="workNo">工號</param>
        /// <param name="sqlDep"></param>
        /// <param name="privileged">是否有組織權限</param>
        /// <returns>人員信息</returns>
        public DataTable getEmpInfo(string workNo, string sqlDep, bool privileged)
        {
            string cmdText = "select * from gds_att_selfservice_v a where a.workno='" + workNo + "'";
            if (privileged)
            {
                cmdText += " AND exists (SELECT 1 FROM (" + sqlDep + ") e where e.DepCode=a.dCode)";
            }
            DataTable dt = new DataTable();
            dt = DalHelper.ExecuteQuery(cmdText);
            return dt;
        }
        /// <summary>
        /// 獲取鞋櫃位置
        /// </summary>
        /// <param name="workNo">工號</param>
        /// <returns>鞋櫃位置</returns>
        public DataTable getPlaceName(string workNo)
        {
            string cmdText = @"SELECT a.*, b.localname, c.sctype, gds_att_getdepname ('2', b.depcode) syc,
               b.depname, d.placename,
               (SELECT datavalue
                  FROM gds_att_typedata c
                 WHERE c.datatype = 'EmpState'
                   AND c.datacode = b.status) empstatusname,
               b.status empstatus,
               (SELECT datavalue
                  FROM gds_att_typedata e
                 WHERE e.datatype = 'SCMSCType'
                   AND e.datacode = a.status) statusname
          FROM gds_att_scm_shoecabinet a, gds_att_employee b, gds_att_scm_bigcabinet c, gds_att_scm_place d
         WHERE a.workno = b.workno(+)
           AND a.scbcode = c.scbcode
           AND c.placecode = d.placecode
           AND a.workno = '" + workNo + "'";
            return DalHelper.ExecuteQuery(cmdText);
        }
        /// <summary>
        /// 獲取有效卡鐘
        /// </summary>
        /// <param name="workNo">工號</param>
        /// <returns>有效卡鐘</returns>
        public DataTable getEffectBellNo(string workNo)
        {
            string cmdText = "SELECT * FROM gds_att_kqparamsemp WHERE ISNOTKAOQIN='N' and bellno >' ' and workno='" + workNo + "'";
            return DalHelper.ExecuteQuery(cmdText);
        }
        /// <summary>
        /// 獲取有效卡鐘
        /// </summary>
        /// <param name="workNo">工號</param>
        /// <returns>有效卡鐘</returns>
        public DataTable getEffectBellNo2(string workNo)
        {
            string cmdText = "SELECT * FROM (SELECT   a.* FROM gds_att_KQPARAMSORG a,(SELECT     LEVEL orderid, depcode FROM gds_sc_department START WITH depcode = (SELECT dcode FROM gds_att_employee WHERE workno = '" + workNo + "') CONNECT BY PRIOR parentdepcode = depcode ORDER BY LEVEL) b WHERE a.orgcode = b.depcode AND bellno > ' ' ORDER BY orderid) WHERE ROWNUM <= 1";
            return DalHelper.ExecuteQuery(cmdText);
        }
        /// <summary>
        /// 獲取當月考勤異常明細
        /// </summary>
        /// <param name="workNo">工號</param>
        /// <returns>當月考勤異常明細</returns>
        public DataTable getKaoQinData(string workNo)
        {
            string cmdText = @"SELECT a.workno, a.kqdate, a.shiftno, b.localname, a.workhours,
       b.dname depname, b.dcode,
       TO_CHAR (a.otondutytime, 'hh24:mi') otondutytime,
       TO_CHAR (a.otoffdutytime, 'hh24:mi') otoffdutytime,
       (SELECT shiftno || ':' || shiftdesc || '[' || shifttype
               || ']'
          FROM gds_att_workshift c
         WHERE c.shiftno = a.shiftno) shiftdesc,
       TO_CHAR (a.ondutytime, 'hh24:mi') AS ondutytime,
       TO_CHAR (a.offdutytime, 'hh24:mi') offdutytime,
       TRIM (TO_CHAR (CASE
                         WHEN a.absentqty = 0
                            THEN NULL
                         ELSE a.absentqty
                      END, '999')
            ) absentqty,
       a.status, a.exceptiontype, a.reasontype, a.reasonremark,
       (SELECT reasonname
          FROM gds_att_exceptreason b
         WHERE b.reasonno = a.reasontype) reasonname,
       (SELECT datavalue
          FROM gds_att_typedata c
         WHERE c.datatype = 'ExceptionType'
           AND c.datacode = a.exceptiontype) exceptiontypename,
       (SELECT datavalue
          FROM gds_att_typedata b
         WHERE b.datatype = 'KqmKaoQinStatus'
           AND b.datacode = a.status) statusname,
       (CASE
           WHEN (SELECT COUNT (1)
                   FROM gds_att_makeup c
                  WHERE c.workno = a.workno
                    AND c.kqdate = a.kqdate
                    AND c.status = '2') > 0
              THEN 'Y'
           ELSE 'N'
        END
       ) ismakeup,
       (CASE
           WHEN a.othours - TRUNC (a.othours) > 0.5
              THEN TRUNC (a.othours) + 0.5
           WHEN othours - TRUNC (a.othours) = 0.5
              THEN a.othours
           WHEN othours - TRUNC (a.othours) < 0.5
              THEN TRUNC (a.othours)
        END
       ) AS othours,
       ROUND ((a.offdutytime - a.ondutytime) * 24, 1) AS totalhours,
       '0' absenttotal
  FROM gds_att_kaoqindata a, gds_att_employee b
 WHERE b.workno = a.workno
   AND b.workno = :p_workno
   AND a.kqdate > LAST_DAY (ADD_MONTHS (TRUNC (SYSDATE), -1))
   AND a.status > '0'";
            return DalHelper.ExecuteQuery(cmdText, new OracleParameter(":p_workno", workNo));
        }

        /// <summary>
        /// 獲取當月曠工累計數
        /// </summary>
        /// <param name="workNo">工號</param>
        /// <param name="kqDate">考勤日期</param>
        /// <returns>當月曠工累計數</returns>
        public DataTable getAbsentTotal(string workNo, string kqDate)
        {
            string cmdText = "SELECT ExcepTionQty FROM (SELECT SUM(DECODE(instr(ExceptionType,'C'),1,1,0))+SUM(DECODE(instr(ExceptionType,'D'),1,1,0)) ExcepTionQty FROM gds_att_kaoqindata WHERE workno = :p_workno and kqdate>last_day(add_months(to_date(:p_kqdate,'yyyy/MM/dd'),-1)) and kqdate<=last_day(to_date(:p_kqdate,'yyyy/MM/dd')))";
            return DalHelper.ExecuteQuery(cmdText, new OracleParameter(":p_workno", workNo), new OracleParameter(":p_kqdate", kqDate));
        }

        /// <summary>
        /// 獲取加扣分項查詢結果
        /// </summary>
        /// <param name="workNo">工號</param>
        /// <returns>加扣分項查詢結果</returns>
        public DataTable getScoreItemData(string workNo)
        {
            string cmdText = @"SELECT b.annualcode, a.dname, a.workno, a.localname,
       (SELECT e.qty
          FROM gds_att_pam_empkqapscore e
         WHERE e.workno = b.workno
           AND e.annualcode = b.annualcode
           AND e.itemcode = 'KQSickLeave') kqsickleave,
       (SELECT e.qty
          FROM gds_att_pam_empkqapscore e
         WHERE e.workno = b.workno
           AND e.annualcode = b.annualcode
           AND e.itemcode = 'KQAffairLeave') kqaffairleave,
       (SELECT e.qty
          FROM gds_att_pam_empkqapscore e
         WHERE e.workno = b.workno
           AND e.annualcode = b.annualcode
           AND e.itemcode = 'KQLate1') kqlate1,
       (SELECT e.qty
          FROM gds_att_pam_empkqapscore e
         WHERE e.workno = b.workno
           AND e.annualcode = b.annualcode
           AND e.itemcode = 'KQLate2') kqlate2,
       (SELECT e.qty
          FROM gds_att_pam_empkqapscore e
         WHERE e.workno = b.workno
           AND e.annualcode = b.annualcode
           AND e.itemcode = 'KQEarly1') kqearly1,
       (SELECT e.qty
          FROM gds_att_pam_empkqapscore e
         WHERE e.workno = b.workno
           AND e.annualcode = b.annualcode
           AND e.itemcode = 'KQEarly2') kqearly2,
       (SELECT e.qty
          FROM gds_att_pam_empkqapscore e
         WHERE e.workno = b.workno
           AND e.annualcode = b.annualcode
           AND e.itemcode = 'KQAbsent1') kqabsent1,
       (SELECT e.qty
          FROM gds_att_pam_empkqapscore e
         WHERE e.workno = b.workno
           AND e.annualcode = b.annualcode
           AND e.itemcode = 'KQAbsent2') kqabsent2,
       (SELECT e.qty
          FROM gds_att_pam_empkqapscore e
         WHERE e.workno = b.workno
           AND e.annualcode = b.annualcode
           AND e.itemcode = 'APAward1') apaward1,
       (SELECT e.qty
          FROM gds_att_pam_empkqapscore e
         WHERE e.workno = b.workno
           AND e.annualcode = b.annualcode
           AND e.itemcode = 'APAward2') apaward2,
       (SELECT e.qty
          FROM gds_att_pam_empkqapscore e
         WHERE e.workno = b.workno
           AND e.annualcode = b.annualcode
           AND e.itemcode = 'APAward3') apaward3,
       (SELECT e.qty
          FROM gds_att_pam_empkqapscore e
         WHERE e.workno = b.workno
           AND e.annualcode = b.annualcode
           AND e.itemcode = 'APPunish1') appunish1,
       (SELECT e.qty
          FROM gds_att_pam_empkqapscore e
         WHERE e.workno = b.workno
           AND e.annualcode = b.annualcode
           AND e.itemcode = 'APPunish2') appunish2,
       (SELECT e.qty
          FROM gds_att_pam_empkqapscore e
         WHERE e.workno = b.workno
           AND e.annualcode = b.annualcode
           AND e.itemcode = 'APPunish3') appunish3,
       c.improvelevel10, c.improvelevel89, c.improvelevel67, c.improvelevel15,
       c.improverealvalue, c.improvetargetvalue, d.traintargetvalue,
       d.trainrealvalue, '' activities, '' volunteer
  FROM gds_att_employee a,
       gds_att_pam_empassess b,
       gds_att_pam_empimprovescore c,
       gds_att_pam_emptrainscore d
 WHERE a.workno = b.workno
   AND b.workno = c.workno(+)
   AND b.workno = d.workno(+)
   AND b.annualcode = c.annualcode(+)
   AND b.annualcode = d.annualcode(+)
   AND a.workno = :p_workno
ORDER BY annualcode DESC";
            return DalHelper.ExecuteQuery(cmdText, new OracleParameter(":p_workno", workNo));
        }
        /// <summary>
        /// 獲取請假統計查詢結果
        /// </summary>
        /// <param name="workNo">工號</param>
        /// <returns>請假統計查詢結果</returns>
        public DataTable getLeaveReportData(string workNo)
        {
            string cmdText = "SELECT (select lvtypename from gds_att_leavetype b where a.lvtypecode = b.lvtypecode)lvtypename,decode(a.standarddays,'-',a.standarddays,trim(to_char(a.standarddays,'999,990.9999')))  standarddays, to_char(a.alreaddays,'999,990.9999') alreaddays, decode(a.leavedays,'-',a.leavedays,trim(to_char(a.leavedays,'999,990.9999'))) leavedays FROM gds_att_kqm_empleavereport a where a.workno=:p_workno";
          //  string cmdText = "SELECT (select lvtypename from gds_att_leavetype b where a.lvtypecode = b.lvtypecode)lvtypename,a.standarddays, a.alreaddays, a.leavedays FROM gds_att_kqm_empleavereport a where a.workno=:p_workno";
            return DalHelper.ExecuteQuery(cmdText, new OracleParameter(":p_workno", workNo));
        }
        /// <summary>
        /// 計算請假統計
        /// </summary>
        /// <param name="workNo">工號</param>
        public void GetEmpLeaveReport(string workNo, SynclogModel logmodel)
        {
            string cmdText = "gds_att_timerGetEmpLeaveReport";
            try
            {
             int i=   DalHelper.ExecuteNonQuery(cmdText, CommandType.StoredProcedure, new OracleParameter("v_worknoin", workNo), new OracleParameter("p_transactiontype", logmodel.TransactionType == null ? "" : logmodel.TransactionType.ToString()),
                   new OracleParameter("p_levelno", logmodel.LevelNo == null ? "" : logmodel.LevelNo.ToString()),
                   new OracleParameter("p_fromhost", logmodel.FromHost == null ? "" : logmodel.FromHost.ToString()),
                   new OracleParameter("p_tohost", logmodel.ToHost == null ? "" : logmodel.ToHost.ToString()),
                   new OracleParameter("p_docno", logmodel.DocNo == null ? "" : logmodel.DocNo.ToString()),
                   new OracleParameter("p_processflag", logmodel.ProcessFlag == null ? "" : logmodel.ProcessFlag.ToString()),
                   new OracleParameter("p_processowner", logmodel.ProcessOwner == null ? "" : logmodel.ProcessOwner.ToString()));
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// 計算未來7天排班 
        /// </summary>
        /// <param name="workNo">工號</param>
        public void GetEmployeeShift(string workNo, SynclogModel logmodel)
        {
            string cmdText = "gds_att_timerGetEmployeeShift";
            try
            {
                DalHelper.ExecuteNonQuery(cmdText, CommandType.StoredProcedure, new OracleParameter("v_worknoin", workNo), new OracleParameter("p_transactiontype", logmodel.TransactionType == null ? "" : logmodel.TransactionType.ToString()),
                   new OracleParameter("p_levelno", logmodel.LevelNo == null ? "" : logmodel.LevelNo.ToString()),
                   new OracleParameter("p_fromhost", logmodel.FromHost == null ? "" : logmodel.FromHost.ToString()),
                   new OracleParameter("p_tohost", logmodel.ToHost == null ? "" : logmodel.ToHost.ToString()),
                   new OracleParameter("p_docno", logmodel.DocNo == null ? "" : logmodel.DocNo.ToString()),
                   new OracleParameter("p_processflag", logmodel.ProcessFlag == null ? "" : logmodel.ProcessFlag.ToString()),
                   new OracleParameter("p_processowner", logmodel.ProcessOwner == null ? "" : logmodel.ProcessOwner.ToString()));
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// 獲取未來7天排班查詢結果
        /// </summary>
        /// <param name="workNo">工號</param>
        /// <returns>未來7天排班查詢結果</returns>
        public DataTable getWorkShiftData(string workNo)
        {
            string cmdText = "select * from gds_att_cess_workshift_v where workno='" + workNo + "'";
            return DalHelper.ExecuteQuery(cmdText);
        }
        /// <summary>
        /// 獲取當年請假明細查詢結果
        /// </summary>
        /// <param name="workNo">工號</param>
        /// <returns>當年請假明細查詢結果</returns>
        public DataTable getLeaveDetailData(string workNo)
        {
            string cmdText = @"SELECT a.ID, a.workno, a.lvtotal, a.lvtypecode, a.proxy, a.reason, a.approver,
       (TO_CHAR (a.startdate, 'yyyy/mm/dd') || '  ' || TO_CHAR (a.starttime)
       ) AS stime,
       (TO_CHAR (a.enddate, 'yyyy/mm/dd') || '  ' || TO_CHAR (a.endtime)
       ) AS etime,
       a.startdate, a.enddate,
       (SELECT lvtypename
          FROM gds_att_leavetype c
         WHERE c.lvtypecode = a.lvtypecode) AS leavetype, b.depcode,
       b.localname, b.dname depname,
       (SELECT datavalue
          FROM gds_att_typedata c
         WHERE c.datatype = 'ApplyState' AND a.status = c.datacode)
                                                                   statusname
  FROM gds_att_leaveapply a, gds_att_employee b
 WHERE a.workno = b.workno and b.workno='" + workNo + "' and a.StartDate>=to_date('" + DateTime.Now.Year.ToString() + "/01/01','yyyy/MM/dd') order by a.StartDate";
            return DalHelper.ExecuteQuery(cmdText);
        }
        /// <summary>
        /// 獲取加班匯總查詢結果
        /// </summary>
        /// <param name="workNo">工號</param>
        /// <returns>加班匯總查詢結果</returns>
        public DataTable getOTMMonthTotalData(string workNo)
        {
            string cmdText = @"SELECT a.*, c.localname, c.dname depname, b.monthtotal, b.day1, b.day2,
       b.day3, b.day4, b.day5, b.day6, b.day7, b.day8, b.day9, b.day10,
       b.day11, b.day12, b.day13, b.day14, b.day15, b.day16, b.day17, b.day18,
       b.day19, b.day20, b.day21, b.day22, b.day23, b.day24, b.day25, b.day26,
       b.day27, b.day28, b.day29, b.day30, b.day31, b.specday1, b.specday2,
       b.specday3, b.specday4, b.specday5, b.specday6, b.specday7, b.specday8,
       b.specday9, b.specday10, b.specday11, b.specday12, b.specday13,
       b.specday14, b.specday15, b.specday16, b.specday17, b.specday18,
       b.specday19, b.specday20, b.specday21, b.specday22, b.specday23,
       b.specday24, b.specday25, b.specday26, b.specday27, b.specday28,
       b.specday29, b.specday30, b.specday31, b.premonday28, b.premonday29,
       b.premonday30, b.premonday31, getdepname ('2', c.depcode) buname,
       (SELECT datavalue
          FROM gds_att_typedata e
         WHERE e.datatype = 'ApproveFlag'
           AND a.approveflag = e.datacode) approveflagname
  FROM gds_att_monthtotal a, gds_att_monthdetail b, gds_att_employee c
 WHERE a.workno = b.workno(+)
   AND a.yearmonth = b.yearmonth(+)
   AND c.workno = a.workno
   AND (c.flag = 'Local' OR c.flag = 'Supporter')  and b.workno='" + workNo + "' and a.YearMonth in('" + DateTime.Now.ToString("yyyyMM") + "','" + DateTime.Now.AddMonths(-1).ToString("yyyyMM") + "')";
            return DalHelper.ExecuteQuery(cmdText);
        }
        /// <summary>
        /// 獲取加班明細查詢結果
        /// </summary>
        /// <param name="workNo">工號</param>
        /// <returns>加班明細查詢結果</returns>
        public DataTable getOTMonthDetailData(string workNo)
        {
            string cmdText = @"SELECT a.*, TO_CHAR (otdate, 'DY') AS week,
       TRIM (INITCAP (TO_CHAR (a.otdate, 'day', 'NLS_DATE_LANGUAGE=American'))
            ) enweek,
       '' g1total, '' g2total, '' g3total,
          TO_CHAR (a.begintime, 'hh24:mi')
       || '-'
       || TO_CHAR (a.endtime, 'hh24:mi') AS advancetime,
          TO_CHAR (a.ondutytime, 'hh24:mi')
       || '-'
       || TO_CHAR (a.offdutytime, 'hh24:mi') AS overtimespan,
       b.localname, b.overtimetype, b.dcode,
       (SELECT levelname
          FROM gds_att_level j
         WHERE j.levelcode = b.levelcode) AS levelname,
       (SELECT datavalue
          FROM gds_att_typedata c
         WHERE c.datatype = 'OTMAdvanceApplyStatus'
           AND c.datacode = a.status) statusname,
       b.dname depname, (SELECT     depname
                               FROM gds_sc_department s
                              WHERE s.levelcode = '2'
                         START WITH s.depcode = b.depcode
                         CONNECT BY s.depcode = PRIOR s.parentdepcode) buname,
       (SELECT datavalue
          FROM gds_att_typedata
         WHERE datatype = 'EMPtype'
           AND datacode = SUBSTR ((SELECT e.postcode
                                     FROM gds_att_employees e
                                    WHERE e.workno = b.workno), 0, 1))
                                                                   persontype,
       (SELECT localname
          FROM gds_att_employee e
         WHERE e.workno = UPPER (a.approver)) approvername,
       (CASE
           WHEN diffreason = 'D'
              THEN 'Y'
           ELSE 'N'
        END) isproject, (SELECT shiftno || ':' || shiftdesc
                           FROM gds_att_workshift b
                          WHERE b.shiftno = a.shiftno) shiftdesc
  FROM gds_att_realapply a, gds_att_employee b
 WHERE b.workno = a.workno and b.workno='" + workNo + "' and a.otdate>last_day(add_months(trunc(sysdate),-1)) and a.otdate<=last_day(trunc(sysdate))";
            return DalHelper.ExecuteQuery(cmdText);
        }

        public DataTable getKaoQinData(string workNo, string year)
        {
            string cmdText = "select a.annualcode,a.workno,b.ITEMNAME, b.ITEMDESC,a.QTY,a.SCORE    from gds_att_PAM_EMPKQAPSCORE a ,gds_att_PAM_KQAPITEM b    where a.ANNUALCODE=b.annualcode(+) and a.itemcode=b.itemcode(+) and a.ANNUALCODE=:p_annualcode and a.workno=:p_workno and b.ITEMTYPE='KQ'";
            return DalHelper.ExecuteQuery(cmdText, new OracleParameter(":p_annualcode", year), new OracleParameter(":p_workno", workNo));
        }

        public DataTable getdataTableJiangChengData(string workNo, string year)
        {
            string cmdText = "select a.annualcode,a.workno,b.ITEMNAME, b.ITEMDESC,a.QTY,a.SCORE    from gds_att_PAM_EMPKQAPSCORE a ,gds_att_PAM_KQAPITEM b    where a.ANNUALCODE=b.annualcode(+) and a.itemcode=b.itemcode(+) and a.ANNUALCODE=:p_annualcode and a.workno=:p_workno and b.ITEMTYPE='AP'";
            return DalHelper.ExecuteQuery(cmdText, new OracleParameter(":p_annualcode", year), new OracleParameter(":p_workno", workNo));
        }
        public DataTable getdataTableIEData(string workNo, string year)
        {
            string cmdText = " SELECT  P.ANNUALCODE, P.WORKNO, P.IMPROVELEVEL10, P.IMPROVELEVEL89, P.IMPROVELEVEL67, P.IMPROVELEVEL15,     P.IMPROVEREALVALUE, P.IMPROVETARGETVALUE, P.SCORE,     P.REMARK, P.MODIFIER, P.MODIFYDATE,    (P.IMPROVEREALVALUE-P.IMPROVETARGETVALUE) improveValue,   round( ((P.IMPROVEREALVALUE-P.IMPROVETARGETVALUE)/decode( nvl(P.IMPROVETARGETVALUE,0),0,1,P.IMPROVETARGETVALUE))*5,1) TimproveValue   FROM gds_att_PAM_EMPIMPROVESCORE P    where P.ANNUALCODE=:p_annualcode and P.workno=:p_workno";
            return DalHelper.ExecuteQuery(cmdText, new OracleParameter(":p_annualcode", year), new OracleParameter(":p_workno", workNo));
        }

        public DataTable getdataTableStudyData(string workNo, string year)
        {
            string cmdText = " select TrainRealValue,TrainTargetValue,(TrainRealValue-TrainTargetValue) trainValue,Score  from   gds_att_PAM_EMPTRAINSCORE    where ANNUALCODE=:p_annualcode and workno=:p_workno";
            return DalHelper.ExecuteQuery(cmdText, new OracleParameter(":p_annualcode", year), new OracleParameter(":p_workno", workNo));
        }
    }



}
