/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： KQMLeaveApplyForm_ZBLHDal.cs
 * 檔功能描述： 請假申請數據業務邏輯類
 * 
 * 版本：1.0
 * 創建標識： 陳函 2012.3.10
 * 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using GDSBG.MiABU.Attendance.IDAL.Hr.KQM.KaoQinData;
using GDSBG.MiABU.Attendance.Model.KQM.KaoQinData;
using System.Data.OracleClient;
using System.Collections;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;

namespace GDSBG.MiABU.Attendance.OracleDAL.Hr.KQM.KaoQinData
{
    public class KQMLeaveApplyForm_ZBLHDal : DALBase<LeaveApplyTempModel>, IKQMLeaveApplyForm_ZBLHDal
    {
        /// <summary>
        /// 獲取請假類型
        /// </summary>
        /// <returns>請假類型</returns>
        public DataTable getLeaveType()
        {
            string cmdText = "SELECT a.*,(Select DataValue From gds_att_TypeData b where b.DataType='LeaveSex' and b.DataCode=a.FitSex) FitSexName FROM gds_att_LEAVETYPE a  WHERE EffectFlag='Y' ORDER BY LVTypeCode";
            return DalHelper.ExecuteQuery(cmdText);
        }
        /// <summary>
        /// 獲取表單狀態
        /// </summary>
        /// <returns>表單狀態</returns>
        public DataTable getStatus()
        {
            string cmdText = "SELECT DataType,DataCode,DataValue,DataTypeDetail,(DataCode||'?B'||DataValue)as newDataValue FROM gds_att_TYPEDATA  WHERE DataType='BillAuditState' ORDER BY OrderId";
            return DalHelper.ExecuteQuery(cmdText);
        }
        /// <summary>
        /// 獲取申請類型
        /// </summary>
        /// <returns>申請類型</returns>
        public DataTable getApplyType()
        {
            string cmdText = "SELECT DataType,DataCode,DataValue,DataTypeDetail,(DataCode||'?B'||DataValue)as newDataValue FROM gds_att_TYPEDATA  WHERE DataType='ApplyType' ORDER BY OrderId";
            return DalHelper.ExecuteQuery(cmdText);
        }
        /// <summary>
        /// 請假是否需要簽核
        /// </summary>
        /// <returns>請假是否需要簽核</returns>
        public DataTable isLeaveNoAudit()
        {
            string cmdText = "SELECT paravalue FROM gds_sc_parameter WHERE paraname='LeaveNoAudit'";
            return DalHelper.ExecuteQuery(cmdText);
        }
        //public DataTable CheckDateMonths(string startDate, string endDate)
        //{
        //    string cmdText = "select floor(MONTHS_BETWEEN(to_date('" + Convert.ToDateTime(endDate).ToString("yyyy/MM/dd") + "','yyyy/MM/dd'),to_date('" + Convert.ToDateTime(startDate).ToString("yyyy/MM/dd") + "','yyyy/MM/dd'))) sDays from dual";
        //    return DalHelper.ExecuteQuery(cmdText);
        //}
        /// <summary>
        /// 獲取請假信息用於導出Excel
        /// </summary>
        /// <param name="Privileged">是否有組織權限</param>
        /// <param name="sqlDep">組織權限管控</param>
        /// <param name="depName">部門名稱</param>
        /// <param name="billNo">單據編號</param>
        /// <param name="workNo">工號 </param>
        /// <param name="localName">姓名 </param>
        /// <param name="LVTypeCode">請假類別 </param>
        /// <param name="status">表單狀態 </param>
        /// <param name="testify">繳交證明 </param>
        /// <param name="startDate">開始日期</param>
        /// <param name="endDate">截止日期</param>
        /// <param name="applyStartDate">申請開始日期</param>
        /// <param name="applyEndDate">申請截止日期</param>
        /// <param name="applyType">申請類別</param>
        /// <param name="flag">是否查詢因班別變動導致請假時數有問題數據 </param>
        /// <param name="IsLastYear">是否補休</param>
        /// <param name="currentPageIndex">當前頁數 </param>
        /// <param name="pageSize">每頁顯示的最大記錄數</param>
        /// <param name="totalCount">總記錄數</param>
        /// <returns>請假信息</returns>
        public DataTable getApplyData(bool Privileged, string sqlDep, string depName, string billNo, string workNo, string localName, string LVTypeCode, string status, string testify, string startDate, string endDate, string applyStartDate, string applyEndDate, string applyType, bool flag, string IsLastYear, int currentPageIndex, int pageSize, out int totalCount)
        {
            string cmdText = "select * from gds_att_leaveapply_v where 1=1 ";
            if (depName.Length > 0)
            {
                if (Privileged)
                {
                    cmdText += " AND dCode IN ((" + sqlDep + ") INTERSECT SELECT DepCode FROM gds_sc_department START WITH depname like '" + depName + "%' CONNECT BY PRIOR depcode = parentdepcode) ";
                }
                else
                {
                    cmdText += " AND dCode IN (SELECT DepCode FROM gds_sc_department START WITH depname like '" + depName + "%' CONNECT BY PRIOR depcode = parentdepcode) ";
                }
            }
            else
            {
                if (Privileged)
                {
                    cmdText += " AND dcode in (" + sqlDep + ")";
                }
            }
            if (billNo.Length > 0)
            {
                cmdText += " AND BillNo like '" + billNo + "%'";
            }
            if (workNo.Length > 0)
            {
                cmdText += " AND WorkNO like '" + workNo.ToUpper() + "%'";
            }
            if (localName.Length > 0)
            {
                cmdText += " AND LocalName like '" + localName + "%'";
            }
            if (LVTypeCode.Length > 0)
            {
                cmdText += " AND LVTypeCode = '" + LVTypeCode + "'";
            }
            if (status.Length > 0)
            {
                cmdText += " AND Status = '" + status + "'";
            }
            if (testify.Length > 0)
            {
                cmdText += " AND lvtypecode IN(SELECT lvtypecode FROM gds_att_leavetype WHERE istestify='Y')";
                if (testify.Equals("Y"))
                {
                    cmdText += " AND TestifyFile>' '";
                }
                else if (testify.Equals("N"))
                {
                    cmdText += " AND Testifyfile IS NULL";
                }
            }
            string StartDate = DateTime.Parse(startDate).ToString("yyyy/MM/dd");
            string EndDate = DateTime.Parse(endDate).AddDays(1).ToString("yyyy/MM/dd");
            cmdText += " AND ((to_date(to_char(StartDate,'yyyy/mm/dd')||StartTime,'yyyy/mm/dd hh24:mi') <= to_date('" + StartDate + " 07:00','yyyy/mm/dd hh24:mi') AND to_date(to_char(EndDate,'yyyy/mm/dd')||EndTime,'yyyy/mm/dd hh24:mi') >= to_date('" + StartDate + " 07:00','yyyy/mm/dd hh24:mi')) or " +
                             "(to_date(to_char(StartDate,'yyyy/mm/dd')||StartTime,'yyyy/mm/dd hh24:mi') <= to_date('" + EndDate + " 07:00','yyyy/mm/dd hh24:mi') AND to_date(to_char(EndDate,'yyyy/mm/dd')||EndTime,'yyyy/mm/dd hh24:mi') >= to_date('" + EndDate + " 07:00','yyyy/mm/dd hh24:mi')) or " +
                             "(to_date(to_char(StartDate,'yyyy/mm/dd')||StartTime,'yyyy/mm/dd hh24:mi') >= to_date('" + StartDate + " 07:00','yyyy/mm/dd hh24:mi') AND to_date(to_char(EndDate,'yyyy/mm/dd')||EndTime,'yyyy/mm/dd hh24:mi') <= to_date('" + EndDate + " 07:00','yyyy/mm/dd hh24:mi')))";
            if (applyStartDate.Length > 0)
            {
                cmdText += " AND TRUNC(UPDATE_DATE) >= to_date('" + applyStartDate + "','yyyy/mm/dd')";
            }
            if (applyEndDate.Length > 0)
            {
                cmdText += " AND TRUNC(UPDATE_DATE) <= to_date('" + applyEndDate + "','yyyy/mm/dd')";
            }
            if (applyType.Length > 0)
            {
                cmdText += " AND ApplyType = '" + applyType + "'";
            }
            if (flag)
            {
                cmdText += " AND lvtotal <> (select nvl(sum(lvtotal),0)from gds_att_leavedetail e where e.id=id)";
            }
            if (IsLastYear.Length > 0)
            {
                cmdText += " AND IsLastYear = '" + IsLastYear + "'";
            }
            return DalHelper.ExecutePagerQuery(cmdText, currentPageIndex, pageSize, out  totalCount);
        }

        //public DataTable getThisLVTotal(string sID)
        //{
        //    string cmdText = "select nvl(sum(lvtotal),0) from gds_att_leavedetail where id=:p_sID";
        //    return DalHelper.ExecuteQuery(cmdText, new OracleParameter(":p_sID", sID));
        //}

        //public DataTable getTLVWorkDays(string sID)
        //{
        //    string cmdText = "select nvl(sum(workhours),0) from gds_att_leavedetail where id=:p_sID";
        //    return DalHelper.ExecuteQuery(cmdText, new OracleParameter(":p_sID", sID));
        //}
        /// <summary>
        /// 根據工號查詢員工基本信息
        /// </summary>
        /// <param name="IsPrivileged">是否有組織權限</param>
        /// <param name="sqlDep">權限管控</param>
        /// <param name="empNo">工號</param>
        /// <returns>員工基本信息</returns>
        public DataTable getEmpInfo(bool IsPrivileged, string sqlDep, string empNo)
        {
            string cmdText = "SELECT a.WORKNO,a.LOCALNAME,a.marrystate,a.DName,a.LEVELCODE,a.MANAGERCODE,a.IDENTITYNO,a.Notes,a.Flag,(select DataValue from gds_att_TypeData c where c.DataType='Sex' and c.DataCode=a.Sex) as Sex,a.Sex SexCode,  a.technicalname, a.LevelName,a.ManagerName,  (SELECT TechnicalTypeName FROM gds_att_technical b,gds_att_TechnicalType c WHERE c.TechnicalTypeCode=b.TechnicalType and b.technicalcode = a.technicalcode) as TechnicalType,  (SELECT Costcode FROM gds_sc_department b WHERE b.depcode=a.depcode) Costcode,  a.TECHNICALCODE,a.DEPCODE,a.Dcode, a.dname depname,a.DepName SYBName, gds_att_GetDepName('2',a.depcode) Syc,gds_att_GetDepName('1',a.depcode) BGName,gds_att_GetDepName('0',a.depcode) CBGName, (select ProfessionalName from gds_att_Professional n where n.ProfessionalCode=a.ProfessionalCode ) as ProfessionalName,  round((MONTHS_BETWEEN(sysdate,a.JoinDate)-nvl(a.DeductYears,0))/12,1) as ComeYears,  (select (select DataValue from gds_att_TypeData b where b.DataType='AssessLevel' and e.AssesLevel=b.DataCode)   from gds_att_EmpAssess e where e.WorkNo=a.WorkNo and e.AssesDate=(select Max(AssesDate)from gds_att_EmpAssess w where w.WorkNo=e.WorkNo) and ROWNUM<=1) as AssesLevel,    (select LevelType from gds_att_Level j where j.LevelCode=a.LevelCode ) as LevelType,  TO_CHAR(a.JoinDate,'yyyy/mm/dd') AS JoinDate,a.OverTimeType, (select DataValue from gds_att_TypeData t where t.DataType='OverTimeType' and t.DataCode=a.OverTimeType) as OverTimeTypeName from gds_att_Employee a  ";
            cmdText += " WHERE a.WorkNO='" + empNo + "' and (a.status='0' or a.LeaveDate>sysdate-35)";
            if (IsPrivileged)
            {
                cmdText += " AND a.DCode in(" + sqlDep + ")";
            }
            return DalHelper.ExecuteQuery(cmdText);
        }
        /// <summary>
        /// 統計每天加班時數,每月加班時數
        /// </summary>
        /// <param name="empNo">工號</param>
        public void CountCanAdjlasthy(string empNo)
        {
            string cmdText = "gds_att_otmonthtotaldataPro";
            DateTime CountDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month));
            // string CountDateStr = CountDate.ToString("yyyy/MM/dd");
            OracleParameter opCountDate = new OracleParameter("p_date", OracleType.DateTime);
            opCountDate.Value = CountDate;
            //  int i = DalHelper.ExecuteNonQuery(cmdText,CommandType.StoredProcedure,opCountDate);
            int i = DalHelper.ExecuteNonQuery(cmdText, CommandType.StoredProcedure, opCountDate, new OracleParameter("v_adddays", "0"), new OracleParameter("v_dcode", ""), new OracleParameter("v_empno", empNo));
        }
        /// <summary>
        /// 獲取員工性別
        /// </summary>
        /// <param name="empNo">工號</param>
        /// <returns>員工性別</returns>
        public DataTable getSexCode(string empNo)
        {
            string cmdText = "SELECT Sex FROM gds_att_employee WHERE WorkNo=:p_empNo";
            return DalHelper.ExecuteQuery(cmdText, new OracleParameter(":p_empNo", empNo));
        }
        /// <summary>
        /// 獲取員工入職時數
        /// </summary>
        /// <param name="empNo">工號</param>
        /// <returns>員工入職時數</returns>
        public DataTable getInWorkYears(string empNo)
        {
            string cmdText = "select InWorkYears from(SELECT (case when status in ('0','1') then round((MONTHS_BETWEEN(TO_DATE(TO_CHAR(SYSDATE,'yyyy')||'1231','yyyymmdd'),JoinDate)-nvl(DeductYears,0))/12,1)      else round((MONTHS_BETWEEN(LeaveDate,JoinDate)-nvl(DeductYears,0))/12,1) end)as InWorkYears FROM gds_att_employees WHERE workno=:p_empNo)";
            return DalHelper.ExecuteQuery(cmdText, new OracleParameter(":p_empNo", empNo));
        }
        /// <summary>
        /// 獲取非晚婚假限制天數
        /// </summary>
        /// <returns></returns>
        public DataTable getLimitdays()
        {
            string cmdText = "SELECT nvl(max(paravalue),'5') FROM gds_sc_parameter WHERE paraname='KQMLeaveJLimitdays'";
            return DalHelper.ExecuteQuery(cmdText);
        }
        /// <summary>
        /// 獲取員工入職時數
        /// </summary>
        /// <param name="empNo">工號</param>
        /// <returns>員工入職時數</returns>
        public DataTable getAges(string empNo)
        {
            string cmdText = "select InWorkYears from(SELECT (case when status in ('0','1') then round((MONTHS_BETWEEN(TO_DATE(TO_CHAR(SYSDATE,'yyyy')||'1231','yyyymmdd'),JoinDate)-nvl(DeductYears,0))/12,1)      else round((MONTHS_BETWEEN(LeaveDate,JoinDate)-nvl(DeductYears,0))/12,1) end)as InWorkYears FROM gds_att_employees WHERE workno=:p_empNo)";
            return DalHelper.ExecuteQuery(cmdText, new OracleParameter(":p_empNo", empNo));
        }
        /// <summary>
        /// 獲取婚假限制天數
        /// </summary>
        /// <returns>婚假限制天數</returns>
        public DataTable getJLimitdays()
        {
            string cmdText = "SELECT NVL(limitdays,13)limitdays FROM gds_att_leavetype WHERE lvtypecode='J'";
            return DalHelper.ExecuteQuery(cmdText);
        }
        /// <summary>
        ///按工號獲取婚假限制天數
        /// </summary>
        /// <param name="empNo">工號</param>
        /// <returns></returns>
        public DataTable getSpecLimitDays(string empNo)
        {
            string cmdText = "select LimitDays from gds_att_employee a,gds_sc_department b,gds_att_SpecLeaveType c where a.depcode=b.depcode and b.AreaCode=c.Areacode and a.workno=:p_empNo and c.lvtypecode='J'";
            return DalHelper.ExecuteQuery(cmdText, new OracleParameter(":p_empNo", empNo));
        }
        /// <summary>
        /// 獲取請假類別數
        /// </summary>
        /// <param name="sexCode">性別代碼</param>
        /// <returns>請假類別數據</returns>
        public DataTable getLeaveTypeCount(string sexCode)
        {
            string cmdText = "SELECT a.*,(Select DataValue From gds_att_TypeData b where b.DataType='LeaveSex' and b.DataCode=a.FitSex) FitSexName FROM gds_att_LEAVETYPE a   WHERE EffectFlag='Y' and FitSex<>:p_sexcode ORDER BY LVTypeCode ";
            return DalHelper.ExecuteQuery(cmdText, new OracleParameter(":p_sexcode", sexCode));
        }
        /// <summary>
        /// 根據性別獲取請假類別
        /// </summary>
        /// <param name="sexCode">性別代碼</param>
        /// <returns>請假類別</returns>
        public DataTable getDataByCondition(string sexCode)
        {
            string cmdText = " SELECT a.*,(Select DataValue From gds_att_TypeData b where b.DataType='LeaveSex' and b.DataCode=a.FitSex) FitSexName FROM gds_att_LEAVETYPE a  WHERE EffectFlag='Y' AND LVTypeCode!='Y' and FitSex<>:p_sexcode ORDER BY LVTypeCode ";
            return DalHelper.ExecuteQuery(cmdText, new OracleParameter(":p_sexcode", sexCode));
        }
        /// <summary>
        /// 獲取請假總時數
        /// </summary>
        /// <param name="empNo">工號</param>
        /// <returns>請假總時數</returns>
        public DataTable getLVTotal(string empNo)
        {
            string cmdText = "  SELECT NVL (SUM (lvtotal), 0) FROM gds_att_leavedetail a WHERE workno = :p_workno AND lvtypecode = 'U' AND lvdate >= last_day(trunc(sysdate))+1 and  exists(select 1 from gds_att_leaveapply b where a.id=b.id and b.status in('0','1','2','4'))";
            return DalHelper.ExecuteQuery(cmdText, new OracleParameter(":p_workno", empNo));
        }
        /// <summary>
        /// 獲取限制天數
        /// </summary>
        /// <param name="empNo">工號</param>
        /// <param name="LVTypeCode">類別代碼</param>
        /// <returns></returns>
        public DataTable getLimitDays(string empNo, string LVTypeCode)
        {
            string cmdText = "select LimitDays from gds_att_employee a,gds_sc_department b,gds_att_SpecLeaveType c where a.depcode=b.depcode and b.AreaCode=c.Areacode and a.workno=:p_workno and c.lvtypecode=:p_lvtypecode;";
            return DalHelper.ExecuteQuery(cmdText, new OracleParameter(":p_workno", empNo), new OracleParameter(":p_lvtypecode", LVTypeCode));
        }

        /// <summary>
        /// 獲取年度依年資應修年假天數
        /// </summary>
        /// <param name="empNo">工號</param>
        /// <param name="reportYear">當前年份</param>
        /// <param name="applyDate">當前日期</param>
        /// <returns></returns>
        public string getYearLeaveDays(string empNo, string reportYear, string applyDate)
        {
            string cmdText = "gds_att_getyearleavedayspro";
            OracleParameter standardDays = new OracleParameter("cur_info", OracleType.Cursor);
            standardDays.Direction = ParameterDirection.Output;

            //OracleParameter yearLeaveDays = new OracleParameter("v_yearleavedays", OracleType.Float);
            //yearLeaveDays.Direction = ParameterDirection.Output;

            //OracleParameter alreadDays = new OracleParameter("v_alreaddays", OracleType.Float);
            //alreadDays.Direction = ParameterDirection.Output;

            //OracleParameter reachLeaveDays = new OracleParameter("v_reachleavedays", OracleType.Float);
            //reachLeaveDays.Direction = ParameterDirection.Output;

            //OracleParameter thisJoinYear = new OracleParameter("v_thisjoinyear", OracleType.VarChar);
            //thisJoinYear.Direction = ParameterDirection.Output;

            // DalHelper.ExecuteQuery(cmdText, CommandType.StoredProcedure, new OracleParameter("v_empno", empNo), new OracleParameter("v_reportyear", reportYear), new OracleParameter("v_applydate", applyDate), standardDays, yearLeaveDays, alreadDays, reachLeaveDays, thisJoinYear);
            DataTable dt = DalHelper.ExecuteQuery(cmdText, CommandType.StoredProcedure, new OracleParameter("v_empno", empNo), new OracleParameter("v_reportyear", reportYear), new OracleParameter("v_applydate", applyDate), standardDays);

            //string StandardDays = standardDays == null ? "" : standardDays.Value.ToString();
            //string LeaveDays = yearLeaveDays == null ? "" : yearLeaveDays.Value.ToString();
            //string AlreadyDays = alreadDays == null ? "" : alreadDays.Value.ToString();
            //string ReachLeaveDays = reachLeaveDays == null ? "" : reachLeaveDays.Value.ToString();
            //string ThisJoinYear = thisJoinYear == null ? "" : thisJoinYear.Value.ToString();
            //     standardDays.Value.ToString();
            return dt.Rows[0][0].ToString();
            //   return (StandardDays + "|" + LeaveDays + "|" + AlreadyDays + "|" + ReachLeaveDays + "|" + ThisJoinYear);
        }
        /// <summary>
        /// 獲取當月可調時數
        /// </summary>
        /// <param name="empNo">工號</param>
        /// <param name="LVTotal">請假類別</param>
        /// <returns>當月可調時數</returns>
        public DataTable getTime(string empNo, double LVTotal)
        {
            string cmdText = @"SELECT NVL (mreladjust, 0)
  FROM (SELECT CASE
                  WHEN approveflag = '2'
                     THEN mreladjust - :p_lvtotal
                  ELSE   mreladjust
                       + g2relsalary
                       + specg2salary
                       - :p_lvtotal
               END mreladjust
          FROM gds_att_monthtotal
         WHERE workno = :p_empno AND yearmonth = TO_CHAR (SYSDATE, 'yyyymm'))";
            return DalHelper.ExecuteQuery(cmdText, new OracleParameter(":p_empno", empNo), new OracleParameter(":p_lvtotal", LVTotal));
        }
        /// <summary>
        /// 獲取請假總時數
        /// </summary>
        /// <param name="LVTotal">已修總時數</param>
        /// <param name="empNo">工號</param>
        /// <param name="typeCode">請假類別</param>
        /// <param name="IsUTypeCode">是否為調休</param>
        /// <returns>請假總時數</returns>
        public DataTable getSumlvTotal(double LVTotal, string empNo, string typeCode, bool IsUTypeCode)
        {
            string cmdText = "";
            if (IsUTypeCode)
            {
                cmdText = "SELECT NVL (SUM (lvtotal), 0)+:p_lvtotal FROM gds_att_leavedetail a WHERE workno = :p_empno AND lvtypecode = 'U' AND lvdate <= last_day(trunc(sysdate)) AND lvdate > last_day(add_months(trunc(sysdate),-1)) and  exists(select 1 from gds_att_leaveapply b where a.id=b.id and b.status in('0','1','2','4'))";
                return DalHelper.ExecuteQuery(cmdText, new OracleParameter(":p_empno", empNo), new OracleParameter(":p_lvtotal", LVTotal));
            }
            else
            {
                cmdText = " SELECT  round(NVL (SUM (lvtotal), 0),4) FROM gds_att_leavedetail a WHERE workno =:p_empno  AND lvtypecode = :p_lvtypecode AND TO_CHAR(lvdate,'yyyy')=TO_CHAR(SYSDATE,'yyyy') and  exists(select 1 from gds_att_leaveapply b where a.id=b.id and b.status in('2','4'))";
                return DalHelper.ExecuteQuery(cmdText, new OracleParameter(":p_empno", empNo), new OracleParameter(":p_lvtypecode", typeCode));
            }
        }
        /// <summary>
        /// 是否結婚管控假別
        /// </summary>
        /// <returns>是否結婚管控假別</returns>
        public DataTable getMarryFlag()
        {
            string cmdText = "select nvl(MAX(paravalue),'N') from gds_sc_parameter where paraname='MarryStateFlagForLeaveApply'";
            return DalHelper.ExecuteQuery(cmdText);
        }
        /// <summary>
        /// 是否為非年假
        /// </summary>
        /// <returns>是否為非年假</returns>
        public DataTable getNoYearHoliday()
        {
            string cmdText = "select nvl(MAX(paravalue),'N') from gds_sc_parameter where paraname='IsNoYearHolidayForTempFlag'";
            return DalHelper.ExecuteQuery(cmdText);
        }
        /// <summary>
        /// 獲取試用員工或預師工號
        /// </summary>
        /// <param name="employeeNo">工號</param>
        /// <returns>試用員工或預師工號</returns>
        public DataTable getWorkNo(string employeeNo)
        {
            string cmdText = "select workno from gds_att_employee where workno = :p_workno and  levelcode in (select levelcode from gds_att_level where levelname like '試用員%' or levelname like '預師%' )";
            return DalHelper.ExecuteQuery(cmdText, new OracleParameter(":p_workno", employeeNo));
        }
        /// <summary>
        /// 獲取請假類別
        /// </summary>
        /// <param name="marryFlag">是否已婚</param>
        /// <param name="noYearHolidayFlag">是否為年休假</param>
        /// <returns>請假類別</returns>
        public DataTable getLVType(bool marryFlag, bool noYearHolidayFlag)
        {
            string cmdText = "SELECT a.*,(Select DataValue From gds_att_TypeData b where b.DataType='LeaveSex' and b.DataCode=a.FitSex) FitSexName FROM gds_att_LEAVETYPE a where 1=1 ";
            if (marryFlag)
            {
                cmdText += " and lvtypecode <> 'J' ";
            }
            if (noYearHolidayFlag)
            {
                cmdText += " and lvtypecode <> 'Y' ";
            }
            return DalHelper.ExecuteQuery(cmdText);
        }
        /// <summary>
        /// 獲取員工資位
        /// </summary>
        /// <returns>員工資位</returns>
        public DataTable getLevelCode()
        {
            string cmdText = "SELECT a.LEVELCODE,a.LEVELNAME,a.REMARK,a.LEVELTYPE,a.EFFECTFLAG,a.MODIFIER,a.MODIFYDATE,(select DataValue from gds_att_TypeData b where b.DataType='LevelType' and b.DataCode=a.LevelType) as LEVELTYPENAME FROM gds_att_level a WHERE EffectFlag='Y' ORDER BY OrderNo";
            return DalHelper.ExecuteQuery(cmdText);
        }
        /// <summary>
        /// 查詢請假申請是否拒簽
        /// </summary>
        /// <param name="workNo">工號</param>
        /// <param name="typeCode">請假類別代碼</param>
        /// <returns>是否拒簽結果</returns>
        public DataTable checkRefuseApply(string workNo, string typeCode)
        {
            string cmdText = "SELECT 'Y' FROM gds_att_leaveapply WHERE Workno=:p_workno AND lvtypecode=:p_lvtypecode AND Status='3'";
            return DalHelper.ExecuteQuery(cmdText, new OracleParameter(":p_workno", workNo), new OracleParameter(":p_lvtypecode", typeCode));
        }
        /// <summary>
        /// 獲取請假總時數
        /// </summary>
        /// <param name="workNo">工號</param>
        /// <param name="startDate">開始日期</param>
        /// <param name="endDate">結束日期</param>
        /// <param name="typeCode">請假類別代碼</param>
        /// <returns>請假總時數</returns>
        public string getLVTotal(string workNo, string startDate, string endDate, string typeCode)
        {
            string cmdText = "gds_att_leavetotalpro";
            OracleParameter lvTotal = new OracleParameter("lvtotal", OracleType.Int32);
            lvTotal.Direction = ParameterDirection.Output;
            int flag = DalHelper.ExecuteNonQuery(cmdText, CommandType.StoredProcedure, new OracleParameter("bdate", startDate), new OracleParameter("edate", endDate), new OracleParameter("v_lvtype", typeCode), new OracleParameter("v_workno", workNo), lvTotal);
            return lvTotal.Value.ToString();
        }
        /// <summary>
        /// 獲取員工班別
        /// </summary>
        /// <param name="workNo">工號</param>
        /// <param name="startDate">開始日期</param>
        /// <returns>員工班別</returns>
        public string getLVTotal(string workNo, string startDate)
        {
            //string cmdText = "gds_att_getempshiftno";
            OracleParameter shiftNo = new OracleParameter("v_shiftno", OracleType.VarChar, 4);
            shiftNo.Direction = ParameterDirection.Output;
            int shift = DalHelper.ExecuteNonQuery("gds_att_getempshiftno", CommandType.StoredProcedure, new OracleParameter("v_workno", workNo), new OracleParameter("v_date", startDate), shiftNo);
            return shiftNo.Value.ToString();
        }
        /// <summary>
        /// 獲取允許申請的資位
        /// </summary>
        /// <param name="lvTypeCode">請假類別</param>
        /// <returns>允許申請的資位</returns>
        public DataTable getAllowDepLevel(string lvTypeCode)
        {
            string cmdText = "SELECT AllowDepLevel FROM gds_att_LeaveType WHERE LVTypeCode=:p_LVTypeCode";
            return DalHelper.ExecuteQuery(cmdText, new OracleParameter(":p_LVTypeCode", lvTypeCode));
        }
        /// <summary>
        /// 獲取離職日期
        /// </summary>
        /// <param name="empNo">工號</param>
        /// <returns>離職日期</returns>
        public DataTable getLeaveDate(string empNo)
        {
            string cmdText = "Select to_char(LeaveDate,'yyyy-MM-dd') from  gds_att_employee where workno=:p_workno and status>'1'";
            return DalHelper.ExecuteQuery(cmdText, new OracleParameter(":p_workno", empNo));
        }
        /// <summary>
        /// 獲取授權功能列表
        /// </summary>
        /// <param name="roleCode">角色代碼</param>
        /// <param name="moduleCode">模組代碼</param>
        /// <returns>授權功能列表</returns>
        public DataTable getAuthorizedFunctionList(string roleCode, string moduleCode)
        {
            string cmdText = "SELECT max(functionlist) FROM gds_sc_authority WHERE modulecode=:p_modulecode AND rolecode in(select y.RoleCode from gds_sc_roles x,gds_sc_rolesrole y where x.rolesCode=y.RolesCode and x.RolesCode=:p_RolesCode)";
            return DalHelper.ExecuteQuery(cmdText, new OracleParameter(":p_modulecode", moduleCode), new OracleParameter(":p_RolesCode", roleCode));
        }
        /// <summary>
        /// 請假是否需要簽核
        /// </summary>
        /// <returns> 請假是否需要簽核</returns>
        public DataTable getParaValue()
        {
            string cmdText = "SELECT paravalue FROM gds_sc_parameter WHERE paraname='LeaveNoAudit'";
            return DalHelper.ExecuteQuery(cmdText);
        }
        /// <summary>
        /// 獲取假別的最小時數和標準時數
        /// </summary>
        /// <param name="LVType">假別代碼</param>
        /// <returns>假別的最小時數和標準時數</returns>
        public DataTable getHours(string LVType)
        {
            string cmdText = "SELECT nvl(MinHours,0.5) MinHours,nvl(StandardHours,0.5) StandardHours from gds_att_leavetype  where lvtypecode=:p_lvtypecode";
            return DalHelper.ExecuteQuery(cmdText, new OracleParameter(":p_lvtypecode", LVType));
        }
        /// <summary>
        /// 根據ID獲取請假信息
        /// </summary>
        /// <param name="ID">請假ID</param>
        /// <returns>請假信息</returns>
        public DataTable getDataByBillNo(string ID)
        {
            string cmdText = @"SELECT a.*, b.localname, b.dcode, b.levelname,
               (SELECT datavalue
                  FROM gds_att_typedata c
                 WHERE c.datatype = 'Sex' AND c.datacode = b.sex) sexname,
               b.sex, b.dname depname, b.levelcode, b.managercode,
               (SELECT     depname
                      FROM gds_sc_department s
                     WHERE s.levelcode = '2'
                START WITH s.depcode = b.depcode
                CONNECT BY s.depcode = PRIOR s.parentdepcode) buname,
               (SELECT lvtypename
                  FROM gds_att_leavetype c
                 WHERE c.lvtypecode = a.lvtypecode) lvtypename,
               (SELECT datavalue
                  FROM gds_att_typedata c   
                 WHERE c.datatype = 'ApplyType'
                   AND c.datacode = a.applytype) applytypename,
               (SELECT datavalue
                  FROM gds_att_typedata c
                 WHERE c.datatype = 'ApplyState'
                   AND c.datacode = a.status) statusname,
               (SELECT notes
                  FROM gds_att_employee e
                 WHERE e.workno = a.proxyworkno) proxynotes,
               (SELECT flag
                  FROM gds_att_employee e
                 WHERE e.workno = a.proxyworkno) proxyflag,
               (SELECT localname
                  FROM gds_att_employee e
                 WHERE e.workno = a.UPDATE_USER) modifyname,
               (SELECT datavalue
                  FROM gds_att_typedata c
                 WHERE c.datatype = 'ProxyStatus'
                   AND c.datacode = a.proxystatus) proxystatusname,
               (SELECT istestify
                  FROM gds_att_leavetype c
                 WHERE c.lvtypecode = a.lvtypecode) istestify,
               ROUND (  (  MONTHS_BETWEEN (SYSDATE, b.joindate)
                         - NVL (b.deductyears, 0)
                        )
                      / 12,
                      1
                     ) comeyears
          FROM gds_att_leaveapply a, gds_att_employee b
         WHERE a.workno = b.workno and a.ID=:p_id";
            return DalHelper.ExecuteQuery(cmdText, new OracleParameter(":p_id", ID));
        }
        /// <summary>
        /// 是否允許調休
        /// </summary>
        /// <returns>是否允許調休</returns>
        public DataTable getRestChangeHours()
        {
            string cmdText = "select nvl(MAX(paravalue),'N') from gds_sc_parameter where paraname='MaxRestChangeHours'";
            return DalHelper.ExecuteQuery(cmdText);
        }
        /// <summary>
        /// 新增修改請假信息
        /// </summary>
        /// <param name="processFlag">新增修改標誌</param>
        /// <param name="tempDataTable">請假信息</param>
        /// <param name="logmodel">操作日誌</param>
        /// <returns>請假ID</returns>
        public string SaveData(string processFlag, DataTable tempDataTable, SynclogModel logmodel)
        {
            string cmdText = "";
            string sql = "";
            string strMax = "";
            foreach (DataRow newRow in tempDataTable.Rows)
            {
                object cmd4;
                if (newRow["ProxyFlag"].ToString() == "Local")
                {
                    cmdText = "select nvl(Notes,'') from gds_att_Employees Where WorkNo=:p_workno";
                    DataTable dt = DalHelper.ExecuteQuery(cmdText, new OracleParameter(":p_workno", newRow["ProxyWorkNo"].ToString()));
                    if (dt.Rows[0][0].ToString().Length == 0)
                    {
                        cmdText = "Update gds_att_Employees Set Notes=:p_Notes Where WorkNo=:p_workno";
                        logmodel.ProcessFlag = "Update";
                        DalHelper.ExecuteNonQuery(cmdText, logmodel, new OracleParameter(":p_Notes", newRow["ProxyNotes"].ToString()), new OracleParameter(":p_workno", newRow["ProxyWorkNo"].ToString()));
                    }
                }
                else if (newRow["ProxyFlag"].ToString() == "NoLocal")
                {
                    cmdText = "select nvl(Notes,'') from gds_att_TWCadre Where WorkNo=:p_workno";
                    DataTable dt = DalHelper.ExecuteQuery(cmdText, new OracleParameter(":p_workno", newRow["ProxyWorkNo"].ToString()));
                    if (dt.Rows[0][0].ToString().Length == 0)
                    {
                        cmdText = "Update gds_att_TWCadre Set Notes=:p_Notes Where WorkNo=:p_workno";
                        logmodel.ProcessFlag = "Update";
                        DalHelper.ExecuteNonQuery(cmdText, logmodel, new OracleParameter(":p_Notes", newRow["ProxyNotes"].ToString()), new OracleParameter(":p_workno", newRow["ProxyWorkNo"].ToString()));
                    }
                }
                else if (newRow["ProxyFlag"].ToString() == "Supporter")
                {
                    cmdText = "select nvl(Notes,'') from gds_att_EmpSupportOut Where WorkNo=:p_workno";
                    DataTable dt = DalHelper.ExecuteQuery(cmdText, new OracleParameter(":p_workno", newRow["ProxyWorkNo"].ToString()));
                    if (dt.Rows[0][0].ToString().Length == 0)
                    {
                        cmdText = "Update gds_att_EmpSupportOut Set Notes=:p_Notes Where WorkNo=:p_workno";
                        logmodel.ProcessFlag = "Update";
                        DalHelper.ExecuteNonQuery(cmdText, logmodel, new OracleParameter(":p_Notes", newRow["ProxyNotes"].ToString()), new OracleParameter(":p_workno", newRow["ProxyWorkNo"].ToString()));
                    }
                }
                if (processFlag.Equals("Add"))
                {
                    sql = "INSERT INTO gds_att_leaveapply(BillNo,WorkNo,LVTypeCode,StartDate,StartTime,EndDate,EndTime,LVTotal,Reason,Proxy,ProxyWorkNo,ProxyStatus,Remark,ApplyType,Status,IsLastYear,UPDATE_USER,UPDATE_DATE,StartShiftNo,EndShiftNo,EMERGENCYCONTACTPERSON,EMERGENCYTELEPHONE";
                    if (newRow["LVTypeCode"].ToString().Equals("J"))
                    {
                        sql = sql + ",WifeIsFoxconn,WifeWorkNo,WifeName,WifeLevelName,WifeBG";
                    }
                    if ((newRow["approver"] != null) && (newRow["approver"].ToString().Trim().Length > 0))
                    {
                        sql = sql + ",approver";
                    }
                    if ((newRow["ApproveDate"] != null) && (newRow["approvedate"].ToString().Trim().Length > 0))
                    {
                        sql = sql + ",ApproveDate";
                    }
                    cmd4 = sql;
                    sql = string.Concat(new object[] { 
                            cmd4, ") VALUES('','", newRow["WorkNo"], "','", newRow["LVTypeCode"], "',to_date('", DateTime.Parse(newRow["StartDate"].ToString()).ToString("yyyy/MM/dd"), "','yyyy/mm/dd'),'", newRow["StartTime"], "',to_date('", DateTime.Parse(newRow["EndDate"].ToString()).ToString("yyyy/MM/dd"), "','yyyy/mm/dd'),'", newRow["EndTime"], "',", newRow["LVTotal"], ",'", 
                            newRow["Reason"], "','", newRow["Proxy"], "','", newRow["ProxyWorkNo"], "','",newRow["ProxyStatus"],"','", newRow["Remark"], "','", newRow["ApplyType"], "','", newRow["Status"], "','", newRow["IsLastYear"], "','", newRow["UPDATE_USER"], "',sysdate,'", 
                            newRow["StartShiftNo"], "','", newRow["EndShiftNo"], "','",newRow["EMERGENCYCONTACTPERSON"],"','",newRow["EMERGENCYTELEPHONE"],"'"
                         });
                    if (newRow["LVTypeCode"].ToString().Equals("J"))
                    {
                        cmd4 = sql;
                        sql = string.Concat(new object[] { cmd4, ",'", newRow["WifeIsFoxconn"], "','", newRow["wifeWorkNo"], "','", newRow["WifeName"], "','", newRow["WifeLevelName"], "','", newRow["WifeBG"], "'" });
                    }
                    if ((newRow["approver"] != null) && (newRow["approver"].ToString().Trim().Length > 0))
                    {
                        cmd4 = sql;
                        sql = string.Concat(new object[] { cmd4, ",'", newRow["approver"], "'" });
                    }
                    if ((newRow["ApproveDate"] != null) && (newRow["ApproveDate"].ToString().Trim().Length > 0))
                    {
                        sql = sql + ",TO_DATE('" + DateTime.Parse(newRow["ApproveDate"].ToString()).ToString("yyyy/MM/dd") + "','yyyy/mm/dd')";
                    }
                    sql = sql + ")";
                    cmdText = sql;
                    logmodel.ProcessFlag = "Insert";
                    DalHelper.ExecuteNonQuery(cmdText, logmodel);
                    cmdText = string.Concat(new object[] { "Select ID From gds_att_leaveapply Where WorkNo='", newRow["WorkNo"], "' and LVTypeCode='", newRow["LVTypeCode"], "' and StartDate=to_date('", DateTime.Parse(newRow["StartDate"].ToString()).ToString("yyyy/MM/dd"), "','yyyy/mm/dd') and StartTime='", newRow["StartTime"], "' and EndDate=to_date('", DateTime.Parse(newRow["EndDate"].ToString()).ToString("yyyy/MM/dd"), "','yyyy/mm/dd') and EndTime='", newRow["EndTime"], "'" });
                    DataTable dtId = DalHelper.ExecuteQuery(cmdText);
                    strMax = dtId.Rows[0][0].ToString();
                }
                else if (processFlag.Equals("Modify"))
                {
                    strMax = Convert.ToString(newRow["id"]);
                    sql = string.Concat(new object[] { 
                            "UPDATE gds_att_leaveapply SET LVTypeCode='", newRow["LVTypeCode"], "',StartDate=to_date('", DateTime.Parse(newRow["StartDate"].ToString()).ToString("yyyy/MM/dd"), "','yyyy/mm/dd'),StartTime='", newRow["StartTime"], "',EndDate=to_date('", DateTime.Parse(newRow["EndDate"].ToString()).ToString("yyyy/MM/dd"), "','yyyy/mm/dd'),EndTime='", newRow["EndTime"], "',LVTotal=", newRow["LVTotal"], ",Reason='", newRow["Reason"], "',ProxyWorkNo='", newRow["ProxyWorkNo"], 
                            "',Proxy='", newRow["Proxy"], "',Remark='", newRow["Remark"], "',ApplyType='", newRow["ApplyType"], "',IsLastYear='", newRow["IsLastYear"], "',UPDATE_USER='", newRow["UPDATE_USER"], "',Status='", newRow["Status"], "',ProxyStatus='", newRow["ProxyStatus"], "',StartShiftNo='", newRow["StartShiftNo"], 
                            "',EndShiftNo='", newRow["EndShiftNo"], "',UPDATE_DATE=sysdate ",",EMERGENCYCONTACTPERSON='",newRow["EMERGENCYCONTACTPERSON"],"',EMERGENCYTELEPHONE='",newRow["EMERGENCYCONTACTPERSON"],"'"
                         });
                    if (newRow["LVTypeCode"].ToString().Equals("J"))
                    {
                        cmd4 = sql;
                        sql = string.Concat(new object[] { cmd4, ",WifeIsFoxconn='", newRow["WifeIsFoxconn"], "',WifeWorkNo='", newRow["WifeWorkNo"], "',WifeName='", newRow["WifeName"], "',WifeBG='", newRow["WifeBG"], "',WifeLevelName='", newRow["WifeLevelName"], "'" });
                    }
                    if ((newRow["approver"] != null) && (newRow["approver"].ToString().Trim().Length > 0))
                    {
                        cmd4 = sql;
                        sql = string.Concat(new object[] { cmd4, ",approver='", newRow["approver"], "'" });
                    }
                    if ((newRow["ApproveDate"] != null) && (newRow["ApproveDate"].ToString().Trim().Length > 0))
                    {
                        sql = sql + ",ApproveDate=TO_DATE('" + DateTime.Parse(newRow["ApproveDate"].ToString()).ToString("yyyy/MM/dd") + "','yyyy/mm/dd')";
                    }
                    cmd4 = sql;
                    sql = string.Concat(new object[] { cmd4, " WHERE ID='", newRow["ID"], "' " });
                    cmdText = sql;
                    logmodel.ProcessFlag = "Update";
                    int i = DalHelper.ExecuteNonQuery(cmdText, logmodel);
                    string sqlstr = "";
                }
                else if (processFlag.Equals("Confirm"))
                {
                    cmdText = string.Concat(new object[] { 
                            "UPDATE gds_att_leaveapply SET  StartDate=to_date('", DateTime.Parse(newRow["StartDate"].ToString()).ToString("yyyy/MM/dd"), "','yyyy/mm/dd'),StartTime='", newRow["StartTime"], "',EndDate=to_date('", DateTime.Parse(newRow["EndDate"].ToString()).ToString("yyyy/MM/dd"), "','yyyy/mm/dd'),EndTime='", newRow["EndTime"], "',LVTotal='", newRow["LVTotal"], "',LVTypeCode='", newRow["LVTypeCode"], "',Status='", newRow["Status"], "',REMARK='"+newRow["REMARK"]+"' WHERE ID='", newRow["ID"], 
                            "' "
                         });
                    logmodel.ProcessFlag = "Update";
                    DalHelper.ExecuteNonQuery(cmdText, logmodel);

                    strMax = Convert.ToString(newRow["ID"]);
                }
            }
            return strMax;
        }
        /// <summary>
        /// 判斷請假時數是否超過管控上限
        /// </summary>
        /// <param name="ProcessFlag">新增修改標誌</param>
        /// <param name="workNo">工號</param>
        /// <param name="lvType">假別</param>
        /// <param name="strDate">請假日期</param>
        /// <param name="sTotal">總時數</param>
        /// <param name="BillNo">ID</param>
        /// <param name="ImportType"></param>
        /// <returns>請假時數是否超過管控上限</returns>
        public bool CheckLvtotal(string ProcessFlag, string workNo, string lvType, string strDate, string sTotal, string BillNo, string ImportType)
        {
            string cmdText = "";
            bool bValue = false;
            string sql = "";
            string strYear = Convert.ToDateTime(strDate).ToString("yyyy");
            string strMon = Convert.ToDateTime(strDate).ToString("MM");
            int iLeaveYearMonth = int.Parse(strYear + strMon);//請假日期
            int iNowYearMonth = int.Parse(DateTime.Now.ToString("yyyyMM"));//當前時間
            if (Convert.ToInt32(Convert.ToDateTime(strDate).ToString("yyyy")) < Convert.ToInt32(DateTime.Now.ToString("yyyy")))
            {
                iNowYearMonth -= 0x58;
            }
            cmdText = "select LimitDays from gds_att_employee a,gds_sc_department b,gds_att_SpecLeaveType c where a.depcode=b.depcode and b.AreaCode=c.Areacode and a.workno=:p_empNo and c.lvtypecode=:p_lvtypecode";
            DataTable dt = DalHelper.ExecuteQuery(cmdText, new OracleParameter(":p_empNo", workNo), new OracleParameter(":p_lvtypecode", lvType));
            string SpecLimitDays = "";//是否為特殊假別
            if (dt != null && dt.Rows.Count > 0)
            {
                SpecLimitDays = dt.Rows[0][0].ToString();
            }
            string strOldLVType = "";
            cmdText = "SELECT Nvl(max(Status),'0') from gds_att_LeaveApply where ID=:p_ID";
            dt = DalHelper.ExecuteQuery(cmdText, new OracleParameter(":p_ID", BillNo));
            string strStatus = "";
            if (dt != null && dt.Rows.Count > 0)
            {
                strStatus = dt.Rows[0][0].ToString();
            }
            if (lvType.Equals("U"))
            {
                cmdText = "SELECT Nvl(max(lvtypecode),'') from gds_att_LeaveApply where Status<'5' and ID=:p_ID";
                dt = DalHelper.ExecuteQuery(cmdText, new OracleParameter(":p_ID", BillNo));
                if (dt != null && dt.Rows.Count > 0)
                {
                    strOldLVType = dt.Rows[0][0].ToString();
                }
                string KQMLeaveUCheck = "";
                //    cmdText = "SELECT nvl(max(paravalue),'Y') FROM gds_sc_parameter WHERE paraname='KQMLeaveUCheck'";
                //  dt = DalHelper.ExecuteQuery(cmdText);

                //     string KQMLeaveUCheck = ((ServiceLocator)this.Session["serviceLocator"]).GetFunctionData().GetValue("SELECT nvl(max(paravalue),'Y') FROM bfw_parameter WHERE paraname='KQMLeaveUCheck'");
                if (ImportType.Equals("U"))
                {
                    KQMLeaveUCheck = "Y";
                }
                else
                {
                    KQMLeaveUCheck = "N";
                }
                cmdText = "  SELECT nvl(max(paravalue),'2') FROM gds_sc_parameter WHERE paraname='KQMLeaveUCheckMonths'";
                dt = DalHelper.ExecuteQuery(cmdText);
                string KQMLeaveUCheckMonths = DalHelper.ExecuteQuery(cmdText).Rows[0][0].ToString();
                if ((iNowYearMonth - iLeaveYearMonth) > Convert.ToInt32(KQMLeaveUCheckMonths))
                {
                    return true;
                }
                if (((iNowYearMonth - iLeaveYearMonth) == Convert.ToInt32(KQMLeaveUCheckMonths)) && (DateTime.Today.Day > 12))
                {
                    return true;
                }
                double ModifyLvTotal = 0.0;
                if ((iLeaveYearMonth - iNowYearMonth) > 0)
                {
                    if (ProcessFlag.Equals("Modify") && strOldLVType.Equals("U"))
                    {
                        cmdText = "SELECT Nvl(sum(LvTotal),0) from gds_att_LeaveApply where workno=:p_workno AND Status<'5' and ID<>:p_id and lvtypecode=:p_lvtypecode and StartDate>=last_day(trunc(sysdate))+1";
                        ModifyLvTotal = Convert.ToDouble(DalHelper.ExecuteQuery(cmdText, new OracleParameter(":p_workno", workNo), new OracleParameter(":p_id", BillNo), new OracleParameter(":p_lvtypecode", lvType)).Rows[0][0].ToString());
                    }
                    else
                    {
                        cmdText = "SELECT Nvl(sum(LvTotal),0) from gds_att_LeaveApply where workno=:p_workno AND Status<'5' and lvtypecode=:p_lvtypecode and StartDate>=last_day(trunc(sysdate))+1";
                        ModifyLvTotal = Convert.ToDouble(DalHelper.ExecuteQuery(cmdText, new OracleParameter(":p_workno", workNo), new OracleParameter(":p_lvtypecode", lvType)).Rows[0][0].ToString());
                    }
                    if (KQMLeaveUCheck.Equals("N"))
                    {
                        sql = string.Concat(new object[] { "SELECT 'Y' FROM dual WHERE NVL((SELECT CASE WHEN ApproveFlag='2' THEN MRelAdjust-", ModifyLvTotal, " ELSE MRelAdjust+G2RelSalary+SpecG2Salary-", ModifyLvTotal, " END MRelAdjust FROM gds_att_monthtotal WHERE workno='", workNo, "' AND YearMonth=TO_CHAR(SYSDATE,'yyyymm')),0)<", sTotal });
                    }
                    else
                    {
                        sql = "SELECT 'Y' FROM gds_att_monthtotal WHERE ApproveFlag='2' and workno='" + workNo + "' AND YearMonth=TO_CHAR(SYSDATE,'yyyymm')";
                    }
                }
                else
                {
                    if (ProcessFlag.Equals("Modify") && strOldLVType.Equals("U"))
                    {
                        cmdText = "SELECT Nvl(sum(LvTotal),0) from gds_att_LeaveApply where id=:p_id";
                        ModifyLvTotal = Convert.ToDouble(DalHelper.ExecuteQuery(cmdText, new OracleParameter(":p_id", BillNo)).Rows[0][0].ToString());
                    }
                    else
                    {
                        ModifyLvTotal = 0.0;
                    }
                    if (KQMLeaveUCheck.Equals("N"))
                    {
                        sql = string.Concat(new object[] { "SELECT 'Y' FROM dual WHERE NVL((SELECT CASE WHEN ApproveFlag='2' THEN MRelAdjust+", ModifyLvTotal, " ELSE MRelAdjust+G2RelSalary+SpecG2Salary+", ModifyLvTotal, " END MRelAdjust FROM gds_att_monthtotal WHERE workno='", workNo, "' AND YearMonth='", strYear, strMon, "'),0)<", sTotal });
                    }
                    else
                    {
                        sql = "SELECT 'Y' FROM gds_att_monthtotal WHERE ApproveFlag='2' and workno='" + workNo + "' AND YearMonth='" + strYear + strMon + "'";
                    }
                }
            }
            else
            {
                double dYears = 1.0;
                if (lvType.Equals("I") || lvType.Equals("T"))
                {
                    cmdText = "select InWorkYears from(SELECT (case when status in ('0','1') then round((MONTHS_BETWEEN(TO_DATE(:p_date||'1231','yyyymmdd'),JoinDate)-nvl(DeductYears,0))/12,1)      else round((MONTHS_BETWEEN(LeaveDate,JoinDate)-nvl(DeductYears,0))/12,1) end) as InWorkYears FROM gds_att_employees WHERE workno=:p_workno)";
                    dt = DalHelper.ExecuteQuery(cmdText, new OracleParameter(":p_date", strYear), new OracleParameter(":p_workno", workNo));
                    string InWorkYears = "";
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        InWorkYears = dt.Rows[0][0].ToString();
                    }
                    if (!string.IsNullOrEmpty(InWorkYears))
                    {
                        dYears = Convert.ToDouble(InWorkYears);
                        if (dYears > 1.0)
                        {
                            dYears = 1.0;
                        }
                    }
                }
                double dJLimitdays = Convert.ToDouble(DalHelper.ExecuteQuery("SELECT nvl(max(paravalue),'5') FROM gds_sc_parameter WHERE paraname='KQMLeaveJLimitdays'").Rows[0][0].ToString());
                if (lvType.Equals("J"))
                {
                    cmdText = "select Ages from(SELECT round(MONTHS_BETWEEN(to_date(:p_date,'yyyy/MM/dd'),BORNDATE)/12,1)Ages FROM gds_att_EMPLOYEE WHERE WORKNO=:p_workno)";
                    string strAges = "";
                    dt = DalHelper.ExecuteQuery(cmdText, new OracleParameter(":p_date", strDate), new OracleParameter(":p_workno", workNo));
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        strAges = dt.Rows[0][0].ToString();
                    }
                    string strSex = "";
                    cmdText = "select Sex FROM gds_att_EMPLOYEE WHERE WORKNO=:p_workno";
                    dt = DalHelper.ExecuteQuery(cmdText, new OracleParameter(":p_workno", workNo));
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        strSex = dt.Rows[0][0].ToString();
                    }
                    string strJLimitdays = "";
                    cmdText = "SELECT NVL(limitdays,13)limitdays FROM gds_att_leavetype WHERE lvtypecode='J'";
                    dt = DalHelper.ExecuteQuery(cmdText);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        strJLimitdays = dt.Rows[0][0].ToString();
                    }
                    if (!string.IsNullOrEmpty(SpecLimitDays))
                    {
                        strJLimitdays = SpecLimitDays;
                    }
                    if (!string.IsNullOrEmpty(strAges))
                    {
                        if (strSex.Equals("1") && (Convert.ToDouble(strAges) >= 25.0))
                        {
                            dJLimitdays = Convert.ToDouble(strJLimitdays);
                        }
                        else if (strSex.Equals("0") && (Convert.ToDouble(strAges) >= 23.0))
                        {
                            dJLimitdays = Convert.ToDouble(strJLimitdays);
                        }
                    }
                }
                if (ProcessFlag.Equals("Modify"))
                {
                    if (lvType.Equals("J"))
                    {
                        sql = string.Concat(new object[] { "SELECT 'Y' FROM dual WHERE (SELECT NVL (SUM (lvtotal), 0) FROM gds_att_leavedetail a WHERE workno = '", workNo, "' AND lvtypecode = '", lvType, "' AND TO_CHAR(lvdate,'yyyy')='", strYear, "' and  exists(select 1 from gds_att_leaveapply b where a.id=b.id and b.status in('0','1','2','4') and b.id<>'", BillNo, "'))+", sTotal, ">", dJLimitdays * 8.0 });
                    }
                    else if (!string.IsNullOrEmpty(SpecLimitDays))
                    {
                        sql = string.Concat(new object[] { 
                        "SELECT 'Y' FROM dual WHERE (SELECT NVL (SUM (lvtotal), 0) FROM gds_att_leavedetail a WHERE workno = '", workNo, "' AND lvtypecode = '", lvType, "' AND TO_CHAR(lvdate,'yyyy')='", strYear, "' and  exists(select 1 from gds_att_leaveapply b where a.id=b.id and b.status in('0','1','2','4') and b.id<>'", BillNo, "'))+", sTotal, ">(SELECT ", SpecLimitDays, "*8*", dYears, " FROM gds_att_leavetype WHERE lvtypecode='", lvType, 
                        "')"
                     });
                    }
                    else
                    {
                        sql = string.Concat(new object[] { "SELECT 'Y' FROM dual WHERE (SELECT NVL (SUM (lvtotal), 0) FROM gds_att_leavedetail a WHERE workno = '", workNo, "' AND lvtypecode = '", lvType, "' AND TO_CHAR(lvdate,'yyyy')='", strYear, "' and  exists(select 1 from gds_att_leaveapply b where a.id=b.id and b.status in('0','1','2','4') and b.id<>'", BillNo, "'))+", sTotal, ">(SELECT NVL(limitdays,365)*8*", dYears, " FROM gds_att_leavetype WHERE lvtypecode='", lvType, "')" });
                    }
                }
                else if (lvType.Equals("J"))
                {
                    sql = string.Concat(new object[] { "SELECT 'Y' FROM dual WHERE (SELECT NVL (SUM (lvtotal), 0) FROM gds_att_leavedetail a WHERE workno = '", workNo, "' AND lvtypecode = '", lvType, "' AND TO_CHAR(lvdate,'yyyy')='", strYear, "' and  exists(select 1 from gds_att_leaveapply b where a.id=b.id and b.status in('0','1','2','4')) )+", sTotal, ">", dJLimitdays * 8.0 });
                }
                else if (!string.IsNullOrEmpty(SpecLimitDays))
                {
                    sql = string.Concat(new object[] { "SELECT 'Y' FROM dual WHERE (SELECT NVL (SUM (lvtotal), 0) FROM gds_att_leavedetail a WHERE workno = '", workNo, "' AND lvtypecode = '", lvType, "' AND TO_CHAR(lvdate,'yyyy')='", strYear, "' and  exists(select 1 from gds_att_leaveapply b where a.id=b.id and b.status in('0','1','2','4')) )+", sTotal, ">(SELECT ", SpecLimitDays, "*8*", dYears, " FROM gds_att_leavetype WHERE lvtypecode='", lvType, "')" });
                }
                else
                {
                    sql = string.Concat(new object[] { "SELECT 'Y' FROM dual WHERE (SELECT NVL (SUM (lvtotal), 0) FROM gds_att_leavedetail a WHERE workno = '", workNo, "' AND lvtypecode = '", lvType, "' AND TO_CHAR(lvdate,'yyyy')='", strYear, "' and  exists(select 1 from gds_att_leaveapply b where a.id=b.id and b.status in('0','1','2','4')) )+", sTotal, ">(SELECT NVL(limitdays,365)*8*", dYears, " FROM gds_att_leavetype WHERE lvtypecode='", lvType, "')" });
                }
            }
            dt = DalHelper.ExecuteQuery(sql);
            string tempStr = "";
            if (dt != null && dt.Rows.Count > 0)
            {
                tempStr = dt.Rows[0][0].ToString();
            }
            if (tempStr.Length > 0)
            {
                bValue = true;
            }
            if ((Convert.ToDouble(sTotal) < 160.0) && (iNowYearMonth >= iLeaveYearMonth))
            {
                sql = "SELECT 'Y' FROM gds_att_monthtotal WHERE ApproveFlag='2' and workno='" + workNo + "' AND YearMonth='" + strYear + strMon + "'";
                dt = DalHelper.ExecuteQuery(sql);
                tempStr = "";
                if (dt != null && dt.Rows.Count > 0)
                {
                    tempStr = dt.Rows[0][0].ToString();
                }
                if (!((tempStr.Length <= 0) || strStatus.Equals("2")))
                {
                    bValue = true;
                }
            }
            if (lvType.Equals("U") && !bValue)
            {
                if (!string.IsNullOrEmpty(SpecLimitDays))
                {
                    sql = "SELECT 'Y' FROM dual WHERE (SELECT " + SpecLimitDays + "*8 FROM gds_att_leavetype WHERE lvtypecode='" + lvType + "')<" + sTotal;
                }
                else
                {
                    sql = "SELECT 'Y' FROM dual WHERE (SELECT NVL(limitdays,365)*8 FROM gds_att_leavetype WHERE lvtypecode='" + lvType + "')<" + sTotal;
                }
                tempStr = "";
                dt = DalHelper.ExecuteQuery(sql);
                if (dt != null && dt.Rows.Count > 0)
                {
                    tempStr = dt.Rows[0][0].ToString();
                }
                if (tempStr.Length > 0)
                {
                    bValue = true;
                }
            }
            return bValue;
        }

        /// <summary>
        /// 獲取當月可調時數
        /// </summary>
        /// <param name="ProcessFlag">新增修改標誌</param>
        /// <param name="workNo">工號</param>
        /// <param name="strDate">開始日期</param>
        /// <param name="ID">ID</param>
        /// <returns>獲取當月可調時數</returns>
        public double CheckLvtotal(string ProcessFlag, string workNo, string strDate, string ID)
        {
            string cmdText = "";
            string sql = "";
            string strYearMonth = Convert.ToDateTime(strDate).ToString("yyyyMM");
            string strOldLVType = "";
            cmdText = "SELECT Nvl(max(lvtypecode),'') from gds_att_leaveapply where Status<'5' and ID=:p_id";
            DataTable dt = DalHelper.ExecuteQuery(cmdText, new OracleParameter(":p_id", ID));
            if (dt != null && dt.Rows.Count > 0)
            {
                strOldLVType = dt.Rows[0][0].ToString();
            }
            int iLeaveYearMonth = int.Parse(strYearMonth);
            int iNowYearMonth = int.Parse(DateTime.Now.ToString("yyyyMM"));
            double ModifyLvTotal = 0.0;
            if ((iLeaveYearMonth - iNowYearMonth) > 0)
            {
                if (ProcessFlag.Equals("Modify") && strOldLVType.Equals("U"))
                {
                    cmdText = "SELECT Nvl(sum(LvTotal),0) from gds_att_leaveapply where workno=:p_workno AND Status<'5' and ID<>:p_id and lvtypecode='U' and StartDate>=last_day(trunc(sysdate))+1";
                    dt = DalHelper.ExecuteQuery(cmdText, new OracleParameter(":p_id", ID), new OracleParameter(":p_workno", workNo));
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        ModifyLvTotal = Convert.ToDouble(dt.Rows[0][0].ToString());
                    }
                }
                else
                {
                    cmdText = "SELECT Nvl(sum(LvTotal),0) from gds_att_leaveapply where workno=:p_workno AND Status<'5' and lvtypecode='U' and StartDate>=last_day(trunc(sysdate))+1";
                    dt = DalHelper.ExecuteQuery(cmdText, new OracleParameter(":p_workno", workNo));
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        ModifyLvTotal = Convert.ToDouble(dt.Rows[0][0].ToString());
                    }
                }
                sql = string.Concat(new object[] { "select nvl(max(MRelAdjust),0) from(SELECT CASE WHEN ApproveFlag='2' THEN MRelAdjust-", ModifyLvTotal, " ELSE MRelAdjust+G2RelSalary+SpecG2Salary-", ModifyLvTotal, " END MRelAdjust  FROM gds_att_monthtotal WHERE workno='", workNo, "'  AND YearMonth=TO_CHAR(SYSDATE,'yyyymm'))" });
            }
            else
            {
                if (ProcessFlag.Equals("Modify") && strOldLVType.Equals("U"))
                {
                    cmdText = "SELECT Nvl(sum(LvTotal),0) from gds_att_leaveapply where id=:p_id";
                    dt = DalHelper.ExecuteQuery(cmdText, new OracleParameter(":p_id", ID));
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        ModifyLvTotal = Convert.ToDouble(dt.Rows[0][0].ToString());
                    }
                }
                else
                {
                    ModifyLvTotal = 0.0;
                }
                sql = string.Concat(new object[] { "select nvl(max(MRelAdjust),0) from(SELECT CASE WHEN ApproveFlag='2' THEN MRelAdjust+", ModifyLvTotal, " ELSE MRelAdjust+G2RelSalary+SpecG2Salary+", ModifyLvTotal, " END MRelAdjust  FROM gds_att_monthtotal WHERE workno='", workNo, "'  AND YearMonth='", strYearMonth, "')" });
            }
            return Convert.ToDouble(DalHelper.ExecuteQuery(sql).Rows[0][0].ToString());
        }
        /// <summary>
        /// 判斷請假日期是否有重複天數是否
        /// </summary>
        /// <param name="empNo">工號</param>
        /// <param name="startDate">開始日期</param>
        /// <param name="startTime">開始時間</param>
        /// <param name="endDate">結束日期</param>
        /// <param name="endTime">結束時間</param>
        /// <returns>請假日期是否有重複天數是否</returns>
        public bool CheckLeaveOverTime(string empNo, string startDate, string startTime, string endDate, string endTime)
        {
            string start_Time = Convert.ToDateTime(startDate + " " + startTime).ToString("yyyy/MM/dd HH:mm");
            string end_Time = Convert.ToDateTime(endDate + " " + endTime).ToString("yyyy/MM/dd HH:mm");
            string cmdText = "";
            cmdText = "SELECT WorkNo from gds_att_leaveapply a where a.WorkNo='" + empNo + "'  and a.EndDate>=to_date('" + startDate + "','yyyy/mm/dd')  AND ((to_date(to_char(a.StartDate,'yyyy/mm/dd')||a.StartTime,'yyyy/mm/dd hh24:mi') <= to_date('" + start_Time + "','yyyy/mm/dd hh24:mi') AND to_date(to_char(a.EndDate,'yyyy/mm/dd')||a.EndTime,'yyyy/mm/dd hh24:mi') > to_date('" + start_Time + "','yyyy/mm/dd hh24:mi')) or (to_date(to_char(a.StartDate,'yyyy/mm/dd')||a.StartTime,'yyyy/mm/dd hh24:mi') < to_date('" + end_Time + "','yyyy/mm/dd hh24:mi') AND to_date(to_char(a.EndDate,'yyyy/mm/dd')||a.EndTime,'yyyy/mm/dd hh24:mi') >= to_date('" + end_Time + "','yyyy/mm/dd hh24:mi')) or (to_date(to_char(a.StartDate,'yyyy/mm/dd')||a.StartTime,'yyyy/mm/dd hh24:mi') >= to_date('" + start_Time + "','yyyy/mm/dd hh24:mi') AND to_date(to_char(a.EndDate,'yyyy/mm/dd')||a.EndTime,'yyyy/mm/dd hh24:mi') <= to_date('" + end_Time + "','yyyy/mm/dd hh24:mi')))";
            DataTable dt = DalHelper.ExecuteQuery(cmdText);
            if (dt != null && dt.Rows.Count > 0)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        ///計算請假總時長
        /// </summary>
        /// <param name="LeaveID">ID</param>
        public void getLeaveDetail(string LeaveID)
        {
            string cmdText = "gds_att_getleavedetailpro";
            int i = DalHelper.ExecuteNonQuery(cmdText, CommandType.StoredProcedure, new OracleParameter("leaveid", LeaveID));
        }
        /// <summary>
        /// 修改時判斷請假日期是否有重複天數是否
        /// </summary>
        /// <param name="empNo">工號</param>
        /// <param name="startDate">開始日期</param>
        /// <param name="startTime">開始時間</param>
        /// <param name="endDate">結束日期</param>
        /// <param name="endTime">結束時間</param>
        /// <param name="ID">ID</param>
        /// <returns>請假日期是否有重複天數是否</returns>
        public bool CheckLeaveOverTime(string WorkNo, string StartDate, string StartTime, string EndDate, string EndTime, string ID)
        {
            string startTime = Convert.ToDateTime(StartDate + " " + StartTime).ToString("yyyy/MM/dd HH:mm");
            string endTime = Convert.ToDateTime(EndDate + " " + EndTime).ToString("yyyy/MM/dd HH:mm");
            string cmdText = "";
            cmdText = "SELECT WorkNo from gds_att_leaveapply a where a.WorkNo='" + WorkNo + "' and a.ID<>'" + ID + "' and a.EndDate>=to_date('" + StartDate + "','yyyy/mm/dd')  AND ((to_date(to_char(a.StartDate,'yyyy/mm/dd')||a.StartTime,'yyyy/mm/dd hh24:mi') <= to_date('" + startTime + "','yyyy/mm/dd hh24:mi') AND to_date(to_char(a.EndDate,'yyyy/mm/dd')||a.EndTime,'yyyy/mm/dd hh24:mi') > to_date('" + startTime + "','yyyy/mm/dd hh24:mi')) or (to_date(to_char(a.StartDate,'yyyy/mm/dd')||a.StartTime,'yyyy/mm/dd hh24:mi') < to_date('" + endTime + "','yyyy/mm/dd hh24:mi') AND to_date(to_char(a.EndDate,'yyyy/mm/dd')||a.EndTime,'yyyy/mm/dd hh24:mi') >= to_date('" + endTime + "','yyyy/mm/dd hh24:mi')) or (to_date(to_char(a.StartDate,'yyyy/mm/dd')||a.StartTime,'yyyy/mm/dd hh24:mi') >= to_date('" + startTime + "','yyyy/mm/dd hh24:mi') AND to_date(to_char(a.EndDate,'yyyy/mm/dd')||a.EndTime,'yyyy/mm/dd hh24:mi') <= to_date('" + endTime + "','yyyy/mm/dd hh24:mi')))";
            DataTable dt = DalHelper.ExecuteQuery(cmdText);
            if (dt.Rows.Count > 0)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 獲取每月多少日之後不允許重新計算上月及以前考勤數據
        /// </summary>
        /// <returns>天數</returns>
        public DataTable getKqoQinDays()
        {
            string cmdText = "select nvl(MAX(paravalue),'5') from gds_sc_parameter where paraname='KQMReGetKaoQin'";
            return DalHelper.ExecuteQuery(cmdText);
        }
        /// <summary>
        /// 獲取未核准或拒簽的請假信息
        /// </summary>
        /// <param name="ID">ID</param>
        /// <returns>請假信息</returns>
        public DataTable getLeaveApplyData(string ID)
        {
            string cmdText = @"SELECT a.*, b.localname, b.dcode, b.levelname,
       (SELECT datavalue
          FROM gds_att_typedata c
         WHERE c.datatype = 'Sex' AND c.datacode = b.sex) sexname, b.sex,
       b.dname depname, b.levelcode, b.managercode,
       (SELECT     depname
              FROM gds_sc_department s
             WHERE s.levelcode = '2'
        START WITH s.depcode = b.depcode
        CONNECT BY s.depcode = PRIOR s.parentdepcode) buname,
       (SELECT lvtypename
          FROM gds_att_leavetype c
         WHERE c.lvtypecode = a.lvtypecode) lvtypename,
       (SELECT datavalue
          FROM gds_att_typedata c
         WHERE c.datatype = 'ApplyType'
           AND c.datacode = a.applytype) applytypename,
       (SELECT datavalue
          FROM gds_att_typedata c
         WHERE c.datatype = 'ApplyState' AND c.datacode = a.status)
                                                                   statusname,
       (SELECT notes
          FROM gds_att_employee e
         WHERE e.workno = a.proxyworkno) proxynotes,
       (SELECT flag
          FROM gds_att_employee e
         WHERE e.workno = a.proxyworkno) proxyflag,
       (SELECT localname
          FROM gds_att_employee e
         WHERE e.workno = a.UPDATE_USER) modifyname,
       (SELECT datavalue
          FROM gds_att_typedata c
         WHERE c.datatype = 'ProxyStatus'
           AND c.datacode = a.proxystatus) proxystatusname,
       (SELECT istestify
          FROM gds_att_leavetype c
         WHERE c.lvtypecode = a.lvtypecode) istestify,
       ROUND (  (MONTHS_BETWEEN (SYSDATE, b.joindate) - NVL (b.deductyears, 0)
                )
              / 12,
              1
             ) comeyears
  FROM gds_att_leaveapply a, gds_att_employee b
 WHERE a.workno = b.workno and (a.status='0' or a.status='3') and a.ID=:p_id";
            return DalHelper.ExecuteQuery(cmdText, new OracleParameter(":p_id", ID));
        }
        /// <summary>
        /// 刪除請假信息
        /// </summary>
        /// <param name="tempDataTable">請假信息</param>
        /// <param name="logmodel">操作日誌</param>
        public void DeleteData(DataTable tempDataTable, SynclogModel logmodel)
        {
            string cmdText = "";
            logmodel.ProcessFlag = "Delete";
            foreach (DataRow deletedRow in tempDataTable.Rows)
            {
                cmdText = "DELETE FROM gds_att_leaveapply WHERE ID='" + deletedRow["ID"] + "' ";
                DalHelper.ExecuteNonQuery(cmdText, logmodel);
                cmdText = "DELETE gds_att_leavedetail WHERE ID='" + deletedRow["ID"] + "' ";
                DalHelper.ExecuteNonQuery(cmdText, logmodel);
                if (Convert.ToString(deletedRow["BillNo"]).Length > 0)
                {
                    cmdText = "DELETE gds_att_bill a where not exists( select billno from gds_att_leaveapply b where b.billno=:p_billno and b.billno=a.billno )and billno =:p_billno";
                    DalHelper.ExecuteNonQuery(cmdText, logmodel, new OracleParameter(":p_billno", deletedRow["BillNo"]));
                }
            }
        }
        /// <summary>
        /// 導入
        /// </summary>
        /// <param name="createUser">登錄人工號</param>
        /// <param name="moduleCode">模組代碼</param>
        /// <param name="successnum">成功筆數</param>
        /// <param name="errornum">失敗筆數</param>
        /// <param name="logmodel">操作日誌</param>
        /// <returns>錯誤記錄</returns>
        public DataTable ImpoertExcel(string createUser, string moduleCode, out int successnum, out int errornum, SynclogModel logmodel)
        {
            OracleParameter outCursor = new OracleParameter("p_coursor", OracleType.Cursor);
            OracleParameter outSuccess = new OracleParameter("p_success", OracleType.Int32);
            OracleParameter outError = new OracleParameter("p_error", OracleType.Int32);
            outCursor.Direction = ParameterDirection.Output;
            outSuccess.Direction = ParameterDirection.Output;
            outError.Direction = ParameterDirection.Output;
            DataTable dt = DalHelper.ExecuteQuery("gds_sc_leaveapply_vaildata", CommandType.StoredProcedure,
                new OracleParameter("p_personcode", createUser), new OracleParameter("p_modulecode", moduleCode),
                outCursor, outSuccess, outError,
                new OracleParameter("p_transactiontype", logmodel.TransactionType == null ? "" : logmodel.TransactionType.ToString()),
                   new OracleParameter("p_levelno", logmodel.LevelNo == null ? "" : logmodel.LevelNo.ToString()),
                   new OracleParameter("p_fromhost", logmodel.FromHost == null ? "" : logmodel.FromHost.ToString()),
                   new OracleParameter("p_tohost", logmodel.ToHost == null ? "" : logmodel.ToHost.ToString()),
                   new OracleParameter("p_docno", logmodel.DocNo == null ? "" : logmodel.DocNo.ToString()),
                   new OracleParameter("p_processflag", logmodel.ProcessFlag == null ? "" : logmodel.ProcessFlag.ToString()),
                   new OracleParameter("p_processowner", logmodel.ProcessOwner == null ? "" : logmodel.ProcessOwner.ToString())
                );
            successnum = Convert.ToInt32(outSuccess.Value);
            errornum = Convert.ToInt32(outError.Value);

            return dt;
        }
        /// <summary>
        /// 將DataTable轉換為List用於導出
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <returns>List</returns>
        public List<LeaveApplyTempModel> changList(DataTable dt)
        {
            return OrmHelper.SetDataTableToList(dt);
        }

        /// <summary>
        /// 核准表單
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="approver">核准人</param>
        /// <param name="approvedate">核准日期</param>
        /// <param name="status">表單狀態</param>
        /// <param name="logmodel">操作日誌</param>
        public void changeStatusByID(string id, string approver, string approvedate, string status, SynclogModel logmodel)
        {
            string cmdText = "UPDATE gds_att_leaveapply SET Status='" + status + "'";

            if ((approver != null) && (approver.ToString().Trim().Length > 0))
            {
                cmdText = cmdText + ",approver='" + approver.ToString().Trim() + "'";
            }
            if ((approvedate != null) && (approvedate.ToString().Trim().Length > 0))
            {
                cmdText = cmdText + ",approvedate=to_date('" + Convert.ToDateTime(approvedate).ToString("yyyy/MM/dd") + "','yyyy/mm/dd') ";
            }
            object cmd = cmdText;
            cmdText = string.Concat(new object[] { cmd, " WHERE ID='", id, "' " });
            logmodel.ProcessFlag = "Update";
            int i = DalHelper.ExecuteNonQuery(cmdText, logmodel);
        }
        /// <summary>
        /// 取消核准
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="status">表單狀態</param>
        /// <param name="logmodel">操作日誌</param>
        public void changeStatusByID(string id, string status, string proxyStatus ,SynclogModel logmodel)
        {
            string cmdText = "UPDATE gds_att_leaveapply set ProxyStatus='" + proxyStatus + "',Status='" + status + "'";
            object cmd = cmdText;
            cmdText = string.Concat(new object[] { cmd, " WHERE ID='", id, "' " });
            logmodel.ProcessFlag = "Update";
            int i = DalHelper.ExecuteNonQuery(cmdText, logmodel);
        }

        //public DataTable getBillTypeCode(string LVTypeCode)
        //{
        //    string cmdText = "select billtypecode from gds_att_LEAVETYPE where lvtypecode=:p_lvtypecode";
        //    return DalHelper.ExecuteQuery(cmdText, new OracleParameter(":p_lvtypecode", LVTypeCode));
        //}
        //public DataTable getAppTypeToBillConfigData(string LVTypeCode)
        //{
        //    string cmdText = "select * from gds_WF_APPTYPETOBILLCONFIG where applytype='kqmleave' and appvalue=:p_appvalue order by cmptype asc";
        //    return DalHelper.ExecuteQuery(cmdText, new OracleParameter(":p_appvalue", LVTypeCode));
        //}
        /// <summary>
        /// 獲取送簽組織
        /// </summary>
        /// <param name="OrgCode">部門代碼</param>
        /// <param name="BillTypeCode">表單類型代碼</param>
        /// <param name="lvType">請假類型代碼</param>
        /// <param name="startDate">開始日期</param>
        /// <param name="endDate">截止日期</param>
        /// <param name="empNo">工號</param>
        /// <returns>送簽組織</returns>
        public DataTable getWorkFlowOrgCode(string OrgCode, string BillTypeCode, string lvType, string startDate, string endDate, string empNo, string lvTotalDays)
        {
            string[] type = getAuditType(startDate, endDate, empNo, lvTotalDays).Split('^');
            string cmdText = @"SELECT depcode FROM (SELECT   b.depcode, a.orderid FROM gds_wf_flowset a, (SELECT 
                                                 LEVEL orderid, depcode FROM gds_sc_department  START WITH depcode =:p_DepCode CONNECT BY 
                                                 PRIOR parentdepcode = depcode   ORDER BY LEVEL) b  WHERE a.deptcode = b.depcode  AND
                                                 a.formtype =:p_FORMTYPE and a.REASON1='" + type[0] + "' and a.REASON2='" + type[1] + "' and a.REASON3='" + type[2] + "' and a.REASON4='" + lvType + "' ORDER BY orderid) WHERE ROWNUM <= 1";
            return DalHelper.ExecuteQuery(cmdText, new OracleParameter(":p_DepCode", OrgCode), new OracleParameter(":p_FORMTYPE", BillTypeCode));
        }
        /// <summary>
        /// 送簽
        /// </summary>
        /// <param name="sFlow_LevelRemark">簽核角色</param>
        /// <param name="ID">ID</param>
        /// <param name="BillNoType">表單類型</param>
        /// <param name="AuditOrgCode">送簽組織</param>
        /// <param name="BillTypeCode">表單類型代碼</param>
        /// <param name="workNo">工號</param>
        /// <param name="reason">請假原因</param>
        /// <param name="empNo">工號</param>
        /// <param name="startDate">開始日期</param>
        /// <param name="endDate">結束日期</param>
        /// <param name="lvtypeCode">請假類別</param>
        /// <param name="model">操作日誌</param>
        /// <returns>送簽是否成功</returns>
        public bool SaveAuditData(string sFlow_LevelRemark, string ID, string BillNoType, string AuditOrgCode, string BillTypeCode, string workNo, string reason, string empNo, string startDate, string endDate, string lvtypeCode, SynclogModel model)
        {
            string strMax = "";
            bool bResult = false;
            OracleCommand command = new OracleCommand();
            command.Connection = DalHelper.Connection;
            command.Connection.Open();
            OracleTransaction trans = command.Connection.BeginTransaction();
            command.Transaction = trans;
            try
            {
                //  if (processFlag.Equals("Add"))
                // {
                command.CommandText = "Select BillTypeNo From gds_wf_BillType Where BillTypeCode='" + BillTypeCode + "'";
                BillNoType = Convert.ToString(command.ExecuteScalar()) + AuditOrgCode;
                command.CommandText = "SELECT MAX (billno) strMax  FROM gds_att_leaveapply WHERE billno LIKE '" + BillNoType + "'|| TO_CHAR (SYSDATE, 'yymm')|| '%'";
                strMax = Convert.ToString(command.ExecuteScalar());
                if (strMax.Length == 0)
                {
                    strMax = BillNoType + DateTime.Now.ToString("yyMM") + "0001";
                }
                else
                {
                    int i = Convert.ToInt32(strMax.Substring(BillNoType.Length + 4)) + 1;
                    strMax = i.ToString().PadLeft(4, '0');
                    strMax = BillNoType + DateTime.Now.ToString("yyMM") + strMax;
                }
                command.CommandText = "UPDATE gds_att_leaveapply SET Status='1' , BillNo =  '" + strMax + "' Where ID='" + ID + "'";
                command.ExecuteNonQuery();
                model.ProcessFlag = "Update";
                InsertLog(command, model);
                command.CommandText = "SELECT count(1) FROM gds_att_Bill WHERE BillNo='" + strMax + "'";
                if (Convert.ToDecimal(command.ExecuteScalar()) == 0M)
                {
                    command.CommandText = "insert into gds_att_Bill(BillNo,OrgCode,ApplyMan,ApplyDate,Status,BillTypeCode,REASON1) values('" + strMax + "','" + AuditOrgCode + "','" + workNo + "',sysdate,'0','" + BillTypeCode + "','" + reason + "')";
                    command.ExecuteNonQuery();
                    model.ProcessFlag = "Insert";
                    InsertLog(command, model);
                }
                else
                {
                    command.CommandText = "update gds_att_Bill set OrgCode='" + AuditOrgCode + "',ApplyMan='" + workNo + "',ApplyDate=sysdate,Status='0',BillTypeCode='" + BillTypeCode + "' where BillNo='" + strMax + "'";
                    command.ExecuteNonQuery();
                    model.ProcessFlag = "Update";
                    InsertLog(command, model);
                }
                command.CommandText = "SELECT count(1) FROM gds_att_AuditStatus WHERE BillNo='" + strMax + "'";
                if (Convert.ToDecimal(command.ExecuteScalar()) > 0M)
                {
                    command.CommandText = "delete FROM gds_att_AuditStatus WHERE BillNo='" + strMax + "' ";
                    command.ExecuteNonQuery();
                    model.ProcessFlag = "Delete";
                    InsertLog(command, model);
                }
                command.CommandText = "select lvtotaldays from gds_att_leaveapply_v where id='" + ID + "'";
                string lvTotalDays = Convert.ToString(command.ExecuteScalar());
                // command.CommandText = "insert into gds_att_AuditStatus(BillNo,AuditMan,OrderNo,AuditStatus,SendNotes) SELECT '" + strMax + "', FLOW_EMPNO, Orderid,'0','N'   FROM gds_wf_flowset WHERE FORMTYPE='" + BillTypeCode + "' and DEPTCODE='" + AuditOrgCode + "' ";
                //command.CommandText = "insert into GDS_ATT_AUDITSTATUS(BillNo,AuditMan,OrderNo,AuditStatus,SendNotes)  " +
                //                "select '" + strMax + "', FLOW_EMPNO, ORDERID,'0','N'  from (  " +
                //                       " select * from GDS_WF_FLOWSET WHERE FORMTYPE='" + BillTypeCode + "' and DEPTCODE='" + AuditOrgCode + "') " +
                //                       " where  FLOW_EMPNO!='" + empNo + "'or (FLOW_EMPNO='" + workNo + "' and FLOW_LEVEL not in ('課級主管','部級主管'))";
                command.CommandText = "insert into GDS_ATT_AUDITSTATUS(BillNo,AuditMan,OrderNo,AuditStatus,SendNotes,OLDAUDITMAN)  " +
                                        "select '" + strMax + "', nvl(getagentempno(FLOW_EMPNO,'" + AuditOrgCode + "'),FLOW_EMPNO) as FLOW_EMPNO, ORDERID,'0','N' ,decode(NVL (getagentempno (flow_empno, '" + AuditOrgCode + "'), flow_empno),flow_empno,'',flow_empno) as oldauditman from (  " +
                                               " select * from GDS_WF_FLOWSET WHERE FORMTYPE='" + BillTypeCode + "' and DEPTCODE='" + AuditOrgCode + "' and REASON1=gds_att_getleavedaytypefun('" + empNo + "','" + startDate + "','" + endDate + "','" + lvTotalDays + "') and reason2=gds_att_getlevelcodefun('" + empNo + "') and REASON3=gds_att_getmanagerfun('" + empNo + "') and REASON4='" + lvtypeCode + "') " +
                                               " where  FLOW_EMPNO!='" + empNo + "'or (FLOW_EMPNO='" + empNo + "' and FLOW_LEVEL not in (" + sFlow_LevelRemark + "))";
                int count = command.ExecuteNonQuery();
                model.ProcessFlag = "Insert";
                InsertLog(command, model);
                //    }
                //else if (processFlag.Equals("Modify"))
                //{
                //    strMax = BillNoType;
                //    command.CommandText = "UPDATE gds_att_leaveapply SET Status='1' , BillNo =  '" + strMax + "' Where ID='" + ID + "'";
                //    command.ExecuteNonQuery();
                //    model.ProcessFlag = "Update";
                //    InsertLog(command, model);
                //}
                trans.Commit();
                bResult = true;
            }
            catch (Exception)
            {
                trans.Rollback();
                bResult = false;
            }
            finally
            {
                command.Connection.Close();
            }
            return bResult;
        }
        //public DataTable getBillTypeCodeFromLeaveApply(string LVTypeCode)
        //{
        //    string cmdText = "select nvl(max(BillTypeCode),'KQMLeaveApplyH') from gds_att_LEAVETYPE where LVTYPECODE=:p_LVTYPECODE";
        //    return DalHelper.ExecuteQuery(cmdText, new OracleParameter(":p_LVTYPECODE", LVTypeCode));
        //}
        /// <summary>
        /// 送簽代理人
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="workNo">工號</param>
        /// <param name="logmodel">操作日誌</param>
        public void changeProxyStatus(string id, string workNo, SynclogModel logmodel)
        {
            string cmdText = "Select BillTypeName From gds_wf_BillType Where BillTypeCode='D002'";
            string BillNoType = DalHelper.ExecuteQuery(cmdText).Rows[0][0].ToString();
            cmdText = "UPDATE gds_att_leaveapply SET ProxyStatus='1' WHERE ID='" + id + "' and status='0'";
            logmodel.ProcessFlag = "Update";
            int i = DalHelper.ExecuteNonQuery(cmdText, logmodel);
            cmdText = "SELECT nvl(MAX (ParaValue),'')  FROM gds_sc_Parameter WHERE ParaName ='KQMLeaveProxyLinkAddress'";
            string LinkAddress = DalHelper.ExecuteQuery(cmdText).Rows[0][0].ToString();
            cmdText = "SELECT nvl(MAX (ParaValue),'')  FROM gds_sc_Parameter WHERE ParaName ='KQMLeaveProxyContent'";
            string Content = DalHelper.ExecuteQuery(cmdText).Rows[0][0].ToString();
            cmdText = string.Concat(new object[] { "insert into gds_wf_NotesRemind(Content,WorkNo,ApplyMan,ApplyDate,BillNo,SendNotes,Auditstatus,RemindType,Subject,LinkAddress) SELECT '", BillNoType, "', ProxyWorkNo, '", workNo, "',sysdate,ID,'N','0','B','", Content, "' ,'", LinkAddress, "'  FROM gds_att_leaveapply WHERE ID='", id, "' " });
            logmodel.ProcessFlag = "Insert";
            i = DalHelper.ExecuteNonQuery(cmdText, logmodel);
        }
        /// <summary>
        /// 根據ID查詢請假信息
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="privileged">組織權限</param>
        /// <param name="sqlDep">權限管控</param>
        /// <returns>請假信息</returns>
        public DataTable getLeaveApplyData(string id, bool privileged, string sqlDep)
        {
            string cmdText = "select * from gds_att_leaveapply_v a  where a.ID='" + id + "'";
            if (privileged)
            {
                cmdText += " AND exists (SELECT 1 FROM (" + sqlDep + ") e where DepCode=a.DCode)";
            }
            return DalHelper.ExecuteQuery(cmdText);
        }
        /// <summary>
        /// 根據性別獲取請假類型
        /// </summary>
        /// <param name="sexCode">性別代碼</param>
        /// <returns>請假類型</returns>
        public DataTable getLVTypeBySexCode(string sexCode)
        {
            string cmdText = "SELECT a.*,(Select DataValue From gds_att_TypeData b where b.DataType='LeaveSex' and b.DataCode=a.FitSex) FitSexName FROM gds_att_LEAVETYPE a    WHERE EffectFlag='Y'  and FitSex<>'" + sexCode + "' ORDER BY LVTypeCode";
            return DalHelper.ExecuteQuery(cmdText);
        }
        /// <summary>
        /// 獲取請假類別信息
        /// </summary>
        /// <param name="lVTypeCode">請假類別代碼</param>
        /// <returns>請假類別信息</returns>
        public DataTable getLVTypeByLVTypeCode(string lVTypeCode)
        {
            string cmdText = "SELECT a.*,(Select DataValue From gds_att_TypeData b where b.DataType='LeaveSex' and b.DataCode=a.FitSex) FitSexName FROM gds_att_LEAVETYPE a WHERE LVTypeCode='" + lVTypeCode + "'";
            return DalHelper.ExecuteQuery(cmdText);
        }
        /// <summary>
        /// 獲取請假信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>請假信息</returns>
        public DataTable getLeaveApply(string id)
        {
            string cmdText = "select * from gds_att_leaveapply_v  where id='" + id + "'";
            return DalHelper.ExecuteQuery(cmdText);
        }
        /// <summary>
        /// 重新計算考勤結果
        /// </summary>
        /// <param name="WorkNo">工號</param>
        /// <param name="orgCode">部門代碼</param>
        /// <param name="StartDate">開始日期</param>
        /// <param name="EndDate">結束日期</param>
        public void getKaoQinData(string WorkNo, string orgCode, string StartDate, string EndDate)
        {
            string cmdText = "gds_att_reget_kaoqindata";
            int i = DalHelper.ExecuteNonQuery(cmdText, CommandType.StoredProcedure, new OracleParameter("p_workno", WorkNo), new OracleParameter("p_frmkqdate", StartDate), new OracleParameter("p_orgcode", orgCode), new OracleParameter("p_tokqdate", EndDate));
        }
        /// <summary>
        /// 上傳證明文件
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="fileName">文件名</param>
        /// <param name="updateUser">上傳人</param>
        /// <param name="logmodel">操作日誌</param>
        public void Testify(string id, string fileName, string updateUser, SynclogModel logmodel)
        {
            string cmdText = "UPDATE gds_att_leaveapply SET TestifyFile='" + fileName + "',UPDATE_USER='" + updateUser + "',UPDATE_DATE=SYSDATE WHERE ID='" + id + "'";
            logmodel.ProcessFlag = "Update";
            int i = DalHelper.ExecuteNonQuery(cmdText, logmodel);
        }
        /// <summary>
        /// 上傳附件
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="fileName">文件名</param>
        /// <param name="updateUser">上傳人</param>
        /// <param name="logmodel">操作日誌</param>
        public void leaveUploadFile(string id, string fileName, string updateUser, SynclogModel logmodel)
        {
            string cmdText = "UPDATE gds_att_leaveapply SET UploadFile='" + fileName + "',UPDATE_USER='" + updateUser + "',UPDATE_DATE=SYSDATE WHERE ID='" + id + "'";
            logmodel.ProcessFlag = "Update";
            int i = DalHelper.ExecuteNonQuery(cmdText, logmodel);
        }
        /// <summary>
        /// 組織送簽
        /// </summary>
        /// <param name="processFlag">新增修改標誌</param>
        /// <param name="diry">送簽信息</param>
        /// <param name="BillNoType">表單類型</param>
        /// <param name="BillTypeCode">表單類型代碼</param>
        /// <param name="person">送簽人</param>
        /// <returns>送簽成功筆數</returns>
        public int SaveOrgAuditData(string Flow_LevelRemark, string processFlag, Dictionary<string, List<string>> diry, string BillNoType, string BillTypeCode, string person)
        {
            string strMax = "";
            string num = "0";
            string num1 = "0";
            int k = 0;
            foreach (string key in diry.Keys)
            {
                string[] info = key.Split('^');
                string AuditOrgCode = info[0];
                //  string OTType = info[1];
                if (processFlag.Equals("Add"))
                {
                    try
                    {
                        string sql = "SELECT nvl(MAX (billno),'0') strMax  FROM  gds_att_leaveapply  WHERE billno LIKE '" + BillNoType + AuditOrgCode + "'|| TO_CHAR (SYSDATE, 'yymm')|| '%'";
                        DataTable dt_str = DalHelper.ExecuteQuery(sql);
                        if (dt_str != null && dt_str.Rows.Count > 0)
                        {
                            if (dt_str.Rows[0]["strMax"].ToString() == "0")
                            {
                                strMax = string.Empty;
                            }
                            else
                            {
                                strMax = dt_str.Rows[0]["strMax"].ToString();
                            }
                        }
                        if (strMax.Length == 0)
                        {
                            strMax = BillNoType + AuditOrgCode + DateTime.Now.ToString("yyMM") + "0001";
                        }
                        else
                        {
                            int i = Convert.ToInt32(strMax.Substring(BillNoType.Length + AuditOrgCode.Length + 4)) + 1;
                            strMax = i.ToString().PadLeft(4, '0');
                            strMax = BillNoType + AuditOrgCode + DateTime.Now.ToString("yyMM") + strMax;
                        }
                        string sql2 = "SELECT count(1) num FROM GDS_ATT_BILL WHERE BillNo='" + strMax + "'";
                        DataTable dt_1 = DalHelper.ExecuteQuery(sql2);

                        if (dt_1 != null && dt_1.Rows.Count > 0)
                        {
                            num = dt_1.Rows[0]["num"].ToString();
                        }
                        string sql4 = "SELECT count(1) num FROM GDS_ATT_AUDITSTATUS WHERE BillNo='" + strMax + "'";
                        DataTable dt_2 = DalHelper.ExecuteQuery(sql4);

                        if (dt_2 != null && dt_2.Rows.Count > 0)
                        {
                            num1 = dt_1.Rows[0]["num"].ToString();
                        }
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                }


                OracleCommand command = new OracleCommand();
                command.Connection = DalHelper.Connection;
                command.Connection.Open();
                OracleTransaction trans = command.Connection.BeginTransaction();
                command.Transaction = trans;

                try
                {
                    if (processFlag.Equals("Add"))
                    {
                        foreach (string ID in diry[key])
                        {
                            command.CommandText = "UPDATE GDS_ATT_LEAVEAPPLY SET Status='1' , BillNo =  '" + strMax + "' Where ID='" + ID + "'";
                            command.ExecuteNonQuery();
                        }
                        SaveLogData("U", strMax, command.CommandText, command);
                        if (num == "0")
                        {
                            command.CommandText = "insert into GDS_ATT_BILL(BillNo,OrgCode,ApplyMan,ApplyDate,Status,BillTypeCode) values('" + strMax + "','" + AuditOrgCode + "','" + person + "',sysdate,'0','" + BillTypeCode + "')";
                            command.ExecuteNonQuery();
                            SaveLogData("I", strMax, command.CommandText, command);
                        }
                        else
                        {
                            command.CommandText = "update GDS_ATT_BILL set OrgCode='" + AuditOrgCode + "',ApplyMan='" + person + "',ApplyDate=sysdate,Status='0',BillTypeCode='" + BillTypeCode + "'  where BillNo='" + strMax + "'";
                            command.ExecuteNonQuery();
                            SaveLogData("U", strMax, command.CommandText, command);
                        }

                        if (num1 != "0")
                        {
                            command.CommandText = "delete FROM GDS_ATT_AUDITSTATUS WHERE BillNo='" + strMax + "' ";
                            command.ExecuteNonQuery();
                            SaveLogData("D", strMax, command.CommandText, command);
                        }
                        if (diry[key].Count>1)
                        {
                            command.CommandText = "insert into GDS_ATT_AUDITSTATUS(BillNo,AuditMan,OrderNo,AuditStatus,SendNotes) SELECT '" + strMax + "', FLOW_EMPNO, ORDERID,'0','N'   FROM GDS_WF_FLOWSET WHERE FORMTYPE='" + BillTypeCode + "' and DEPTCODE='" + AuditOrgCode + "' and REASON1= '" + info[2] + "' and REASON2='" + info[3] + "' and REASON3='" + info[4] + "' and REASON4='" + info[1] + "'";
                        }
                        else
                        {
                            command.CommandText = "select WORKNO from  GDS_ATT_LEAVEAPPLY where ID='" + diry[key][0].ToString() + "'";
                            string empNo = Convert.ToString(command.ExecuteScalar());
                            command.CommandText = "insert into GDS_ATT_AUDITSTATUS(BillNo,AuditMan,OrderNo,AuditStatus,SendNotes,OLDAUDITMAN)  " +
                                     "select '" + strMax + "', nvl(getagentempno(FLOW_EMPNO,'" + AuditOrgCode + "'),FLOW_EMPNO) as FLOW_EMPNO, ORDERID,'0','N' ,decode(NVL (getagentempno (flow_empno, '" + AuditOrgCode + "'), flow_empno),flow_empno,'',flow_empno) as oldauditman from (  " +
                                            " select * from GDS_WF_FLOWSET WHERE FORMTYPE='" + BillTypeCode + "' and DEPTCODE='" + AuditOrgCode + "' and REASON1='"+info[2] +"' and reason2= '" + info[3] + "' and REASON3='"+ info[4] +"' and REASON4='" + info[1] + "') " +
                                            " where  FLOW_EMPNO!='" + empNo + "'or (FLOW_EMPNO='" + empNo + "' and FLOW_LEVEL not in (" + Flow_LevelRemark + "))";
                        }
                        int count = command.ExecuteNonQuery();
                        SaveLogData("I", strMax, command.CommandText, command);
                    }
                    else if (processFlag.Equals("Modify"))
                    {
                        foreach (string ID in diry[key])
                        {
                            strMax = BillNoType;
                            command.CommandText = "UPDATE GDS_ATT_LEAVEAPPLY SET Status='1' , BillNo =  '" + strMax + "' Where ID='" + ID + "'";
                            command.ExecuteNonQuery();
                        }
                        SaveLogData("U", strMax, command.CommandText, command);
                    }
                    trans.Commit();
                    command.Connection.Close();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    command.Connection.Close();
                    throw ex;

                }
                k++;
            }
            return k;

        }
        /// <summary>
        /// 代理人工號是否存在
        /// </summary>
        /// <param name="proxy">代理人工號</param>
        /// <returns>代理人工號是否存在</returns>
        public DataTable IsProxyExists(string proxy)
        {
            string cmdText = "select * from gds_att_employee where workno='" + proxy + "'";
            return DalHelper.ExecuteQuery(cmdText);
        }
        /// <summary>
        /// 獲取流程定義中的請假天數、資位、管理職信息
        /// </summary>
        /// <param name="startDate">開始日期</param>
        /// <param name="endDate">結束日期</param>
        /// <param name="empNo">工號</param>
        /// <returns>流程定義中的請假天數、資位、管理職信息</returns>
        public string getAuditType(string startDate, string endDate, string empNo, string lvTotalDays)
        {
            string cmdText = "gds_sc_getAuditType";
            OracleParameter outCursor = new OracleParameter("p_coursor", OracleType.Cursor);
            outCursor.Direction = ParameterDirection.Output;
            DataTable dt = DalHelper.ExecuteQuery(cmdText, CommandType.StoredProcedure, new OracleParameter("p_startdate", startDate), new OracleParameter("p_enddate", endDate), new OracleParameter("p_empno", empNo), new OracleParameter("p_lvtotaldays", lvTotalDays), outCursor);
            return dt.Rows[0][0].ToString();
        }
        #region 插入系統日誌

        public void InsertLog(OracleCommand cmd, SynclogModel model)
        {
            string str = cmd.CommandText;
            for (int i = 0; i < cmd.Parameters.Count; i++)
            {
                str = str.Replace(cmd.Parameters[i].ParameterName.ToString(), "'" + cmd.Parameters[i].Value.ToString() + "'");
            }
            str = str.Replace("'", "''");
            string sqlText = "insert into gds_sc_synclog values('','" + model.TransactionType + "','" + model.LevelNo + "','" + model.FromHost + "','" + model.ToHost + "','" + model.DocNo + "','" + str + "',sysdate,'" + model.ProcessFlag + "','" + model.ProcessOwner + "')";
            cmd.CommandText = sqlText;
            int flag = cmd.ExecuteNonQuery();
        }

        #endregion
        /// <summary>
        /// 系統日誌
        /// </summary>
        /// <param name="ProcessFlag"></param>
        /// <param name="DocNo"></param>
        /// <param name="LogText"></param>
        /// <param name="command"></param>
        public void SaveLogData(string ProcessFlag, string DocNo, string LogText, OracleCommand command)
        {

            try
            {
                string strFlag = ProcessFlag;
                if (strFlag != null)
                {
                    if (!(strFlag == "I"))
                    {
                        if (strFlag == "U")
                        {
                            goto Label_003B;
                        }
                        if (strFlag == "D")
                        {
                            goto Label_0044;
                        }
                    }
                    else
                    {
                        ProcessFlag = "Insert";
                    }
                }
                goto Label_004D;
            Label_003B:
                ProcessFlag = "Update";
                goto Label_004D;
            Label_0044:
                ProcessFlag = "Delete";
            Label_004D:
                command.CommandText = "Insert into GDS_SC_SYNCLOG(transactiontype,levelno,FromHost,ToHost,docno,text,logtime,processflag,processowner) values('','2','','','" + DocNo + "','" + LogText.Replace("'", "''") + "',sysdate,'" + ProcessFlag + "','')";
                command.ExecuteNonQuery();
            }
            catch (Exception)
            {
            }

        }
        /// <summary>
        /// 送簽
        /// </summary>
        /// <param name="OracleString">數據庫連接</param>
        /// <param name="sFlow_LevelRemark">簽核角色</param>
        /// <param name="ID">ID</param>
        /// <param name="BillNoType">表單類型</param>
        /// <param name="AuditOrgCode">送簽組織</param>
        /// <param name="BillTypeCode">表單類型代碼</param>
        /// <param name="workNo">工號</param>
        /// <param name="reason">請假原因</param>
        /// <param name="empNo">工號</param>
        /// <param name="startDate">開始日期</param>
        /// <param name="endDate">結束日期</param>
        /// <param name="lvtypeCode">請假類別</param>
        /// <param name="model">操作日誌</param>
        /// <returns>送簽是否成功</returns>
        public bool SaveAuditData(OracleConnection OracleString, string Flow_LevelRemark, string ID, string BillNoType, string AuditOrgCode, string BillTypeCode, string workNo, string reason, string empNo, string startDate, string endDate, string lvtypeCode, SynclogModel model)
        {
            string strMax = "";
            bool bResult = false;
            OracleCommand command = new OracleCommand();
            if (OracleString == null)
            {
                command.Connection = DalHelper.Connection;
            }
            else
            {
                command.Connection = OracleString;
            }
            if (command.Connection.State == ConnectionState.Closed)
            {
                command.Connection.Open();
            }
            OracleTransaction trans = command.Connection.BeginTransaction();
            command.Transaction = trans;
            try
            {
                command.CommandText = "Select BillTypeNo From gds_wf_BillType Where BillTypeCode='" + BillTypeCode + "'";
                BillNoType = Convert.ToString(command.ExecuteScalar()) + AuditOrgCode;
                command.CommandText = "SELECT MAX (billno) strMax  FROM gds_att_leaveapply WHERE billno LIKE '" + BillNoType + "'|| TO_CHAR (SYSDATE, 'yymm')|| '%'";
                strMax = Convert.ToString(command.ExecuteScalar());
                if (strMax.Length == 0)
                {
                    strMax = BillNoType + DateTime.Now.ToString("yyMM") + "0001";
                }
                else
                {
                    int i = Convert.ToInt32(strMax.Substring(BillNoType.Length + 4)) + 1;
                    strMax = i.ToString().PadLeft(4, '0');
                    strMax = BillNoType + DateTime.Now.ToString("yyMM") + strMax;
                }
                command.CommandText = "UPDATE gds_att_leaveapply SET Status='1' , BillNo =  '" + strMax + "' Where ID='" + ID + "'";
                command.ExecuteNonQuery();
                model.ProcessFlag = "Update";
                InsertLog(command, model);
                command.CommandText = "SELECT count(1) FROM gds_att_Bill WHERE BillNo='" + strMax + "'";
                if (Convert.ToDecimal(command.ExecuteScalar()) == 0M)
                {
                    command.CommandText = "insert into gds_att_Bill(BillNo,OrgCode,ApplyMan,ApplyDate,Status,BillTypeCode,REASON1) values('" + strMax + "','" + AuditOrgCode + "','" + workNo + "',sysdate,'0','" + BillTypeCode + "','" + reason + "')";
                    command.ExecuteNonQuery();
                    model.ProcessFlag = "Insert";
                    InsertLog(command, model);
                }
                else
                {
                    command.CommandText = "update gds_att_Bill set OrgCode='" + AuditOrgCode + "',ApplyMan='" + workNo + "',ApplyDate=sysdate,Status='0',BillTypeCode='" + BillTypeCode + "' where BillNo='" + strMax + "'";
                    command.ExecuteNonQuery();
                    model.ProcessFlag = "Update";
                    InsertLog(command, model);
                }
                command.CommandText = "SELECT count(1) FROM gds_att_AuditStatus WHERE BillNo='" + strMax + "'";
                if (Convert.ToDecimal(command.ExecuteScalar()) > 0M)
                {
                    command.CommandText = "delete FROM gds_att_AuditStatus WHERE BillNo='" + strMax + "' ";
                    command.ExecuteNonQuery();
                    model.ProcessFlag = "Delete";
                    InsertLog(command, model);
                }
                command.CommandText = "select lvtotaldays from gds_att_leaveapply_v where id='"+ID+"'";
                string lvTotalDays = Convert.ToString(command.ExecuteScalar());
                command.CommandText = "insert into GDS_ATT_AUDITSTATUS(BillNo,AuditMan,OrderNo,AuditStatus,SendNotes,OLDAUDITMAN)  " +
                                        "select '" + strMax + "', nvl(getagentempno(FLOW_EMPNO,'" + AuditOrgCode + "'),FLOW_EMPNO) as FLOW_EMPNO, ORDERID,'0','N' ,decode(NVL (getagentempno (flow_empno, '" + AuditOrgCode + "'), flow_empno),flow_empno,'',flow_empno) as oldauditman from (  " +
                                               " select * from GDS_WF_FLOWSET WHERE FORMTYPE='" + BillTypeCode + "' and DEPTCODE='" + AuditOrgCode + "' and REASON1=gds_att_getleavedaytypefun('" + empNo + "','" + startDate + "','" + endDate + "','"+lvTotalDays+"') and reason2=gds_att_getlevelcodefun('" + empNo + "') and REASON3=gds_att_getmanagerfun('" + empNo + "') and REASON4='" + lvtypeCode + "') " +
                                               " where  FLOW_EMPNO!='" + empNo + "'or (FLOW_EMPNO='" + empNo + "' and FLOW_LEVEL not in (" + Flow_LevelRemark + "))";
                int count = command.ExecuteNonQuery();
                model.ProcessFlag = "Insert";
                InsertLog(command, model);
                trans.Commit();
                bResult = true;
            }
            catch (Exception)
            {
                trans.Rollback();
                bResult = false;
            }
            finally
            {
                command.Connection.Close();
            }
            return bResult;
        }
    }
}
