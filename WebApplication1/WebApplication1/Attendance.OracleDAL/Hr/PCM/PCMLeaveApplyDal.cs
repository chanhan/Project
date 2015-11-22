/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： PCMLeaveApplyDal.cs
 * 檔功能描述： 個人中心請假申請數據操作類
 * 
 * 版本：1.0
 * 創建標識： 陳函 2012.3.10
 * 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GDSBG.MiABU.Attendance.IDAL.Hr.PCM;
using GDSBG.MiABU.Attendance.Model.KQM.KaoQinData;
using System.Data;
using System.Data.OracleClient;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;

namespace GDSBG.MiABU.Attendance.OracleDAL.Hr.PCM
{
    public class PCMLeaveApplyDal : DALBase<LeaveApplyViewModel>, IPCMLeaveApplyDal
    {
        /// <summary>
        /// 獲取個人請假信息
        /// </summary>
        /// <param name="user">請假人工號</param>
        /// <param name="billNo">申請單號</param>
        /// <param name="LVTypeCode">請假類別</param>
        /// <param name="status">表單狀態</param>
        /// <param name="startDate">開始日期</param>
        /// <param name="endDate">結束日期</param>
        /// <param name="applyType">申請類別</param>
        /// <param name="currentPageIndex">當前頁數</param>
        /// <param name="pageSize">每頁顯示的最大記錄數</param>
        /// <param name="totalCount">總記錄數</param>
        /// <returns>個人請假信息</returns>
        public DataTable getApplyData(string user, string billNo, string LVTypeCode, string status, string startDate, string endDate, string applyType, int currentPageIndex, int pageSize, out int totalCount)
        {
            string cmdText = "select * from gds_att_leaveapply_v a where 1=1 ";

            cmdText += " and workno='" + user + "'";

            if (billNo.Length > 0)
            {
                cmdText += " AND BillNo like '" + billNo + "%'";
            }
            if (LVTypeCode.Length > 0)
            {
                cmdText += " AND LVTypeCode = '" + LVTypeCode + "'";
            }
            if (status.Length > 0)
            {
                cmdText += " AND Status = '" + status + "'";
            }
            string StartDate = "";
            string EndDate = "";
            if ((startDate.Length != 0) && (endDate.Length != 0))
            {
                StartDate = DateTime.Parse(startDate).ToString("yyyy/MM/dd");
                EndDate = DateTime.Parse(endDate).AddDays(1.0).ToString("yyyy/MM/dd");
                cmdText += " AND ((to_date(to_char(a.StartDate,'yyyy/mm/dd')||a.StartTime,'yyyy/mm/dd hh24:mi') <= to_date('" + StartDate + " 07:00','yyyy/mm/dd hh24:mi') AND to_date(to_char(a.EndDate,'yyyy/mm/dd')||a.EndTime,'yyyy/mm/dd hh24:mi') >= to_date('" + StartDate + " 07:00','yyyy/mm/dd hh24:mi')) or (to_date(to_char(a.StartDate,'yyyy/mm/dd')||a.StartTime,'yyyy/mm/dd hh24:mi') <= to_date('" + EndDate + " 07:00','yyyy/mm/dd hh24:mi') AND to_date(to_char(a.EndDate,'yyyy/mm/dd')||a.EndTime,'yyyy/mm/dd hh24:mi') >= to_date('" + EndDate + " 07:00','yyyy/mm/dd hh24:mi')) or (to_date(to_char(a.StartDate,'yyyy/mm/dd')||a.StartTime,'yyyy/mm/dd hh24:mi') >= to_date('" + StartDate + " 07:00','yyyy/mm/dd hh24:mi') AND to_date(to_char(a.EndDate,'yyyy/mm/dd')||a.EndTime,'yyyy/mm/dd hh24:mi') <= to_date('" + EndDate + " 07:00','yyyy/mm/dd hh24:mi')))";
            }

            if ((startDate.Length != 0) && (endDate.Length == 0))
            {
                StartDate = DateTime.Parse(startDate).ToString("yyyy/MM/dd");
                cmdText += " AND a.StartDate >= to_date('" + StartDate + "','yyyy/mm/dd')";
            }
            if ((startDate.Length == 0) && (endDate.Length != 0))
            {
                EndDate = DateTime.Parse(endDate).ToString("yyyy/MM/dd");
                cmdText += " AND a.StartDate >= to_date('" + EndDate + "','yyyy/mm/dd')";
            }
            //string StartDate = DateTime.Parse(startDate).ToString("yyyy/MM/dd");
            //string EndDate = DateTime.Parse(endDate).AddDays(1).ToString("yyyy/MM/dd");
            //cmdText += " AND ((to_date(to_char(StartDate,'yyyy/mm/dd')||StartTime,'yyyy/mm/dd hh24:mi') <= to_date('" + StartDate + " 07:00','yyyy/mm/dd hh24:mi') AND to_date(to_char(EndDate,'yyyy/mm/dd')||EndTime,'yyyy/mm/dd hh24:mi') >= to_date('" + StartDate + " 07:00','yyyy/mm/dd hh24:mi')) or " +
            //                 "(to_date(to_char(StartDate,'yyyy/mm/dd')||StartTime,'yyyy/mm/dd hh24:mi') <= to_date('" + EndDate + " 07:00','yyyy/mm/dd hh24:mi') AND to_date(to_char(EndDate,'yyyy/mm/dd')||EndTime,'yyyy/mm/dd hh24:mi') >= to_date('" + EndDate + " 07:00','yyyy/mm/dd hh24:mi')) or " +
            //                 "(to_date(to_char(StartDate,'yyyy/mm/dd')||StartTime,'yyyy/mm/dd hh24:mi') >= to_date('" + StartDate + " 07:00','yyyy/mm/dd hh24:mi') AND to_date(to_char(EndDate,'yyyy/mm/dd')||EndTime,'yyyy/mm/dd hh24:mi') <= to_date('" + EndDate + " 07:00','yyyy/mm/dd hh24:mi')))";
            //if (startDate.Length > 0)
            //{
            //    cmdText += " AND TRUNC(UPDATE_DATE) >= to_date('" + startDate + "','yyyy/mm/dd')";
            //}
            //if (endDate.Length > 0)
            //{
            //    cmdText += " AND TRUNC(UPDATE_DATE) <= to_date('" + endDate + "','yyyy/mm/dd')";
            //}
            if (applyType.Length > 0)
            {
                cmdText += " AND ApplyType = '" + applyType + "'";
            }
            return DalHelper.ExecutePagerQuery(cmdText, currentPageIndex, pageSize, out  totalCount);
        }
        /// <summary>
        /// 獲取員工基本信息
        /// </summary>
        /// <param name="workNo">員工工號</param>
        /// <returns>員工基本信息</returns>
        public DataTable getEmployeeDataByCondition(string workNo)
        {
            string cmdText = "SELECT a.WORKNO,a.LOCALNAME,a.Sex,a.marrystate,a.MANAGERCODE,a.Notes,a.DepCode,a.Flag,a.DName, (SELECT depname FROM gds_sc_department b WHERE b.depcode=a.dcode) depname, (select ManagerName from gds_att_Manager l where l.ManagerCode=a.ManagerCode ) as ManagerName  from gds_att_employee a  where  a.WorkNo='" + workNo + "'";
            return DalHelper.ExecuteQuery(cmdText);
        }

        /// <summary>
        /// 根據性別獲取員工能請的假別
        /// </summary>
        /// <param name="marrystate">是否已婚</param>
        /// <param name="sexCode">性別代碼</param>
        /// <returns>員工能請的假別</returns>
        public DataTable getKQMLeaveTypeData(string marrystate, string sexCode)
        {
            string cmdText = "SELECT a.*,(Select DataValue From gds_att_TypeData b where b.DataType='LeaveSex' and b.DataCode=a.FitSex) FitSexName FROM gds_att_LEAVETYPE a   WHERE EffectFlag='Y' and LVTypeCode<>'Y' and FitSex<>'" + sexCode + "' and IsAllowPCM='Y' ";
            if (marrystate == "Y")
            {
                cmdText += " and lvtypecode <> 'J' ";
            }
            cmdText += "ORDER BY LVTypeCode";
            return DalHelper.ExecuteQuery(cmdText);
        }
        /// <summary>
        /// 獲取代理人的基本信息
        /// </summary>
        /// <param name="workNo">代理人工號</param>
        /// <param name="localName">代理人姓名</param>
        /// <returns>代理人的基本信息</returns>
        public DataTable getAuditData(string workNo, string localName)
        {
            string cmdText = "select dname DepName,workno AuditMan,localname,notes,ManagerName from gds_att_employee where Status='0' ";
            if (workNo.Length != 0)
            {
                cmdText = cmdText + " AND WorkNO like '" + workNo.ToUpper() + "%'";
            }
            if (localName.Length != 0)
            {
                cmdText = cmdText + " AND LocalName like '" + localName + "%'";
            }
            cmdText = cmdText + " and rownum<100";
            return DalHelper.ExecuteQuery(cmdText);
        }

        /// <summary>
        /// 獲取提示窗口信息
        /// </summary>
        /// <param name="workNo">登陸人工號</param>
        /// <returns>登錄人員工基本信息</returns>
        public DataTable GetDataSetBySQL(string workNo)
        {
            string cmdText = @"select a.personcode,a.personcode||'['||a.cname||']'||'-'||c.depname localname from   
                        gds_sc_person a,gds_sc_authority b,gds_sc_department c,gds_att_employee d 
                        where a.rolecode=b.rolecode and a.depcode=c.depcode and a.personcode=d.workno and d.flag='Local' and d.status='0' and d.ManagerCode='0003' 
                       and b.modulecode='KQMSYS91' 
                        and a.depcode in(SELECT DepCode FROM gds_sc_department  e   
                        START WITH e.depCode =(select dcode from gds_att_employee where workno=:p_workno)  
                        CONNECT BY e.depcode = PRIOR e.parentdepcode) and a.deleted='N' 
                        and exists(select 1 from gds_sc_role e where e.rolecode=a.rolecode and  e.acceptmsg='Y') 
                        order by a.depcode desc";
            return DalHelper.ExecuteQuery(cmdText, new OracleParameter("p_workno", workNo));
        }

        /// <summary>
        /// 獲取員工基本信息
        /// </summary>
        /// <param name="employeeNo">員工工號</param>
        /// <returns>員工基本信息</returns>
        public DataTable getVDataByCondition(string employeeNo)
        {
            string cmdText = @"SELECT a.workno, a.localname, a.marrystate, a.dname, a.levelcode,
       a.managercode, a.identityno, a.notes, a.flag,
       (SELECT datavalue
          FROM gds_att_typedata c
         WHERE c.datatype = 'Sex' AND c.datacode = a.sex) AS sex,
       a.sex sexcode, a.technicalname, a.levelname, a.managername,
       (SELECT technicaltypename
          FROM gds_att_technical b, gds_att_technicaltype c
         WHERE c.technicaltypecode = b.technicaltype
           AND b.technicalcode = a.technicalcode) AS technicaltype,
       (SELECT costcode
          FROM gds_sc_department b
         WHERE b.depcode = a.depcode) costcode, a.technicalcode, a.depcode,
       a.dcode, a.dname depname, a.depname sybname,
       gds_att_getdepname ('2', a.depcode) syc, gds_att_getdepname ('1', a.depcode) bgname,
       getdepname ('0', a.depcode) cbgname,
       (SELECT professionalname
          FROM gds_att_professional n
         WHERE n.professionalcode = a.professionalcode) AS professionalname,
       ROUND (  (MONTHS_BETWEEN (SYSDATE, a.joindate) - NVL (a.deductyears, 0)
                )
              / 12,
              1
             ) AS comeyears,
       (SELECT (SELECT datavalue
                  FROM gds_att_typedata b
                 WHERE b.datatype = 'AssessLevel'
                   AND e.asseslevel = b.datacode)
          FROM gds_att_empassess e
         WHERE e.workno = a.workno
           AND e.assesdate = (SELECT MAX (assesdate)
                                FROM gds_att_empassess w
                               WHERE w.workno = e.workno)
           AND ROWNUM <= 1) AS asseslevel,
       (SELECT leveltype
          FROM gds_att_level j
         WHERE j.levelcode = a.levelcode) AS leveltype,
       TO_CHAR (a.joindate, 'yyyy/mm/dd') AS joindate, a.overtimetype,
       (SELECT datavalue
          FROM gds_att_typedata t
         WHERE t.datatype = 'OverTimeType'
           AND t.datacode = a.overtimetype) AS overtimetypename
  FROM gds_att_employee a  where workno=:p_workno ";
            return DalHelper.ExecuteQuery(cmdText, new OracleParameter("p_workno", employeeNo));
        }
        /// <summary>
        /// 根據ID獲取請假信息
        /// </summary>
        /// <param name="id">請假信息ID</param>
        /// <returns>請假信息</returns>
        public DataTable getDataById(string id)
        {
            string cmdText = "select * from gds_att_leaveapply_v where ID='" + id + "'";
            return DalHelper.ExecuteQuery(cmdText);
        }
        /// <summary>
        /// 新增短信通知記錄
        /// </summary>
        /// <param name="window">窗口類型</param>
        /// <param name="remindContent">短信類容</param>
        /// <param name="logmodel">操作日誌</param>
        public void ExcuteSQL(string window, string remindContent, SynclogModel logmodel)
        {
            string cmdText = "INSERT INTO gds_att_REMIND (WORKNO, REMINDCONTENT, REMINDDATE,FLAG)VALUES('" + window + "','" + remindContent + "',sysdate,'N')";
            int i = DalHelper.ExecuteNonQuery(cmdText, logmodel);
        }
        /// <summary>
        /// 根據工號獲取請假信息
        /// </summary>
        /// <param name="id">請假人工號</param>
        /// <returns>請假信息</returns>
        public DataTable getVDataByWorkNo(string workNo)
        {
            string cmdText = "select * from gds_att_leaveapply_v where workno='" + workNo + "'";
            return DalHelper.ExecuteQuery(cmdText);
        }
        //public DataTable getApptypetoBillConfig(string LVTypeCode)
        //{
        //    string cmdText = "select * from gds_wf_APPTYPETOBILLCONFIG where applytype='kqmleave' and appvalue='" + LVTypeCode + "' order by cmptype asc";
        //    return DalHelper.ExecuteQuery(cmdText);         
        //}
        //public DataTable getBillTypeCode(string LVTypeCode)
        //{
        //    string cmdText = "select nvl(max(BillTypeCode),'KQMLeaveApplyH') from gds_att_LEAVETYPE where LVTYPECODE='" + LVTypeCode + "'";
        //    return DalHelper.ExecuteQuery(cmdText);         
        //}
        /// <summary>
        /// 是否允許個人申請此假別
        /// </summary>
        /// <param name="lvTypeCode">請假類別代碼</param>
        /// <returns>是否允許申請此假別</returns>
        public DataTable iSAllowPCM(string lvTypeCode)
        {
            string cmdText = "select ISAllowPCM from gds_att_leavetype where lvtypecode='" + lvTypeCode + "'";
            return DalHelper.ExecuteQuery(cmdText);         
        }

    }
}
