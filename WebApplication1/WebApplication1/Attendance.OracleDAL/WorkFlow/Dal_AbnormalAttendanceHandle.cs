using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using System.Data.OleDb;
using GDSBG.MiABU.Attendance.Model.WorkFlow;
using GDSBG.MiABU.Attendance.IDAL.WorkFlow;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;

namespace GDSBG.MiABU.Attendance.OracleDAL.WorkFlow
{
    public class Dal_AbnormalAttendanceHandle : DALBase<Mod_AbnormalAttendanceHandle>, IDal_AbnormalAttendanceHandle
    {
        public DataTable GetAbnormalAttendanceInfo(Mod_AbnormalAttendanceHandle model, int pageIndex, int pageSize, out int totalCount)
        {

            string condition = "";
            string ddlStr = "";
            string[] temVal = null;
            #region SelectSql
            condition = "select * from (SELECT a.id,a.workno, a.kqdate, a.shiftno, b.localname, a.billno, a.approver, "
                        + "       a.approvedate, a.apremark, b.dname depname, b.dcode, "
                        + "       TO_CHAR (a.otondutytime, 'hh24:mi') otondutytime, "
                        + "       TO_CHAR (a.otoffdutytime, 'hh24:mi') otoffdutytime, "
                        + "       (SELECT shiftno || ':' || shiftdesc || '[' || shifttype "
                        + "               || ']' "
                        + "          FROM GDS_ATT_WORKSHIFT c "
                        + "         WHERE c.shiftno = a.shiftno) shiftdesc, "
                        + "       TO_CHAR (a.ondutytime, 'hh24:mi') AS ondutytime, "
                        + "       TO_CHAR (a.offdutytime, 'hh24:mi') offdutytime, "
                        + "       TRIM (TO_CHAR (CASE "
                        + "                         WHEN a.absentqty = 0 "
                        + "                            THEN NULL "
                        + "                         ELSE a.absentqty "
                        + "                      END, '999') "
                        + "            ) absentqty, "
                        + "       a.status, a.exceptiontype, a.reasontype, a.reasonremark, "
                        + "       (SELECT reasonname "
                        + "          FROM GDS_ATT_EXCEPTREASON b "
                        + "         WHERE b.reasonno = a.reasontype) reasonname, "
                        + "       (SELECT datavalue "
                        + "          FROM GDS_ATT_TYPEDATA c "
                        + "         WHERE c.datatype = 'ExceptionType' "
                        + "           AND c.datacode = a.exceptiontype) exceptiontypename, "
                        + "       (SELECT datavalue "
                        + "          FROM GDS_ATT_TYPEDATA b "
                        + "         WHERE b.datatype = 'KqmKaoQinStatus' "
                        + "           AND b.datacode = a.status) statusname, "
                        + "       (CASE "
                        + "           WHEN (SELECT COUNT (1) "
                        + "                   FROM GDS_ATT_MAKEUP c "
                        + "                  WHERE c.workno = a.workno "
                        + "                    AND c.kqdate = a.kqdate "
                        + "                    AND c.status = '2') > 0 "
                        + "              THEN 'Y' "
                        + "           ELSE 'N' "
                        + "        END "
                        + "       ) ismakeup, "
                        + "       (CASE "
                        + "           WHEN a.othours - TRUNC (a.othours) > 0.5 "
                        + "              THEN TRUNC (a.othours) + 0.5 "
                        + "           WHEN othours - TRUNC (a.othours) = 0.5 "
                        + "              THEN a.othours "
                        + "           WHEN othours - TRUNC (a.othours) < 0.5 "
                        + "              THEN TRUNC (a.othours) "
                        + "        END "
                        + "       ) AS othours, "
                        + "       ROUND ((a.offdutytime - a.ondutytime) * 24, 1) AS totalhours, "
                        + "       '0' absenttotal "
                        + "  FROM GDS_ATT_KAOQINDATA a, GDS_ATT_EMPLOYEE b "
                        + " WHERE b.workno = a.workno "
                        ;
            #endregion
            //暫時取消權限管控
            if (model.DepName != "")
            {
                //if (base.bPrivileged)
                //{
                condition = condition + " AND b.dCode IN ((" + model.sqlDep + ") INTERSECT SELECT DepCode FROM GDS_SC_DEPARTMENT START WITH depname = '" + model.DepName + "' CONNECT BY PRIOR depcode = parentdepcode) ";
                //}
                //else
                //{
                //    condition = condition + " AND b.dCode IN (SELECT DepCode FROM bfw_department START WITH depname = '" + this.textBoxDepName.Text.Trim() + "' CONNECT BY PRIOR depcode = parentdepcode) ";
                //}
            }
            //if(model.DepCode!="")
            //{
            //    condition = condition + " AND b.dcode in (" + model.sqlDep + ")";
            //}

            if (!string.IsNullOrEmpty(model.EmployeeNo))
            {
                condition = condition + " AND b.WorkNo = '" + model.EmployeeNo.ToUpper() + "'";
            }
            if (!string.IsNullOrEmpty(model.EmpName))
            {
                condition = condition + " AND b.LocalName like '" + model.EmpName + "%'";
            }
            if (!string.IsNullOrEmpty(model.AttendHandleStatus))
            {
                condition = condition + " AND a.STATUS ='" + model.AttendHandleStatus + "'";
            }

            if (model.ExceptionType != "")
            {
                string ExceptionType = model.ExceptionType;
                //temVal = this.ddlExceptionType.SelectedValuesToString(",").Split(new char[] { ',' });
                temVal = ExceptionType.Split(new char[] { ',' });
                for (int iLoop = 0; iLoop < temVal.Length; iLoop++)
                {
                    ddlStr = ddlStr + " a.ExceptionType='" + temVal[iLoop] + "' or";
                }
                ddlStr = ddlStr.Substring(0, ddlStr.Length - 2);
                condition = condition + " and (" + ddlStr + ")";
            }

            if (model.ShiftNoCode != "")
            {
                condition = condition + " AND INSTR(a.ShiftNo,'" + model.ShiftNoCode + "')>0 ";
            }
            if (model.CheckBoxFlag == "1")
            {
                if (model.AttendanceDateFrom != "")
                {
                    condition = condition + " and (((instr(a.ShiftNo,'A')=1 OR instr(a.ShiftNo,'B')=1) AND a.KQDate = to_date('" + DateTime.Parse(model.AttendanceDateFrom).ToString("yyyy/MM/dd") + "','yyyy/mm/dd')) or (instr(a.ShiftNo,'C')=1 AND a.KQDate = to_date('" + DateTime.Parse(model.AttendanceDateFrom).AddDays(-1.0).ToString("yyyy/MM/dd") + "','yyyy/mm/dd')))";
                }
            }
            else
            {
                condition = condition + " AND a.KQDate >= to_date('" + DateTime.Parse(model.AttendanceDateFrom).ToString("yyyy/MM/dd") + "','yyyy/mm/dd') ";

                if (model.AttendanceDateTo != "")
                {
                    condition = condition + " AND a.KQDate <= to_date('" + DateTime.Parse(model.AttendanceDateTo).ToString("yyyy/MM/dd") + "','yyyy/mm/dd') ";
                }
            }
            if (model.IsSupporter != "")
            {
                if (model.IsSupporter.Equals("N"))
                {
                    condition = condition + " AND b.flag='Local' ";
                }
                else
                {
                    condition = condition + " AND b.flag='Supporter' ";
                }
            }
            if (model.IsFillCard.Equals("Y"))
            {
                condition = condition + " AND exists (select 1 from GDS_ATT_MAKEUP c where c.workno=a.workno and c.kqdate=a.kqdate and c.Status='2')";
            }
            condition = condition + ")";
            DataTable dt = DalHelper.ExecutePagerQuery(condition, pageIndex, pageSize, out totalCount);

            return dt;
        }
        public DataTable GetAbnormalAttendanceInfo(Mod_AbnormalAttendanceHandle model)
        {

            string condition = "";
            string ddlStr = "";
            string[] temVal = null;
            #region SelectSql
            condition = "select * from (SELECT a.id,a.workno, a.kqdate, a.shiftno, b.localname, a.billno, a.approver, "
                        + "       a.approvedate, a.apremark, b.dname depname, b.dcode, "
                        + "       TO_CHAR (a.otondutytime, 'hh24:mi') otondutytime, "
                        + "       TO_CHAR (a.otoffdutytime, 'hh24:mi') otoffdutytime, "
                        + "       (SELECT shiftno || ':' || shiftdesc || '[' || shifttype "
                        + "               || ']' "
                        + "          FROM GDS_ATT_WORKSHIFT c "
                        + "         WHERE c.shiftno = a.shiftno) shiftdesc, "
                        + "       TO_CHAR (a.ondutytime, 'hh24:mi') AS ondutytime, "
                        + "       TO_CHAR (a.offdutytime, 'hh24:mi') offdutytime, "
                        + "       TRIM (TO_CHAR (CASE "
                        + "                         WHEN a.absentqty = 0 "
                        + "                            THEN NULL "
                        + "                         ELSE a.absentqty "
                        + "                      END, '999') "
                        + "            ) absentqty, "
                        + "       a.status, a.exceptiontype, a.reasontype, a.reasonremark, "
                        + "       (SELECT reasonname "
                        + "          FROM GDS_ATT_EXCEPTREASON b "
                        + "         WHERE b.reasonno = a.reasontype) reasonname, "
                        + "       (SELECT datavalue "
                        + "          FROM GDS_ATT_TYPEDATA c "
                        + "         WHERE c.datatype = 'ExceptionType' "
                        + "           AND c.datacode = a.exceptiontype) exceptiontypename, "
                        + "       (SELECT datavalue "
                        + "          FROM GDS_ATT_TYPEDATA b "
                        + "         WHERE b.datatype = 'KqmKaoQinStatus' "
                        + "           AND b.datacode = a.status) statusname, "
                        + "       (CASE "
                        + "           WHEN (SELECT COUNT (1) "
                        + "                   FROM GDS_ATT_MAKEUP c "
                        + "                  WHERE c.workno = a.workno "
                        + "                    AND c.kqdate = a.kqdate "
                        + "                    AND c.status = '2') > 0 "
                        + "              THEN 'Y' "
                        + "           ELSE 'N' "
                        + "        END "
                        + "       ) ismakeup, "
                        + "       (CASE "
                        + "           WHEN a.othours - TRUNC (a.othours) > 0.5 "
                        + "              THEN TRUNC (a.othours) + 0.5 "
                        + "           WHEN othours - TRUNC (a.othours) = 0.5 "
                        + "              THEN a.othours "
                        + "           WHEN othours - TRUNC (a.othours) < 0.5 "
                        + "              THEN TRUNC (a.othours) "
                        + "        END "
                        + "       ) AS othours, "
                        + "       ROUND ((a.offdutytime - a.ondutytime) * 24, 1) AS totalhours, "
                        + "       '0' absenttotal "
                        + "  FROM GDS_ATT_KAOQINDATA a, GDS_ATT_EMPLOYEE b "
                        + " WHERE b.workno = a.workno "
                        ;
            #endregion
            //暫時取消權限管控
            if (model.DepName != "")
            {
                //if (base.bPrivileged)
                //{
                condition = condition + " AND b.dCode IN ((" + model.sqlDep + ") INTERSECT SELECT DepCode FROM GDS_SC_DEPARTMENT START WITH depname = '" + model.DepName + "' CONNECT BY PRIOR depcode = parentdepcode) ";
                //}
                //else
                //{
                //    condition = condition + " AND b.dCode IN (SELECT DepCode FROM bfw_department START WITH depname = '" + this.textBoxDepName.Text.Trim() + "' CONNECT BY PRIOR depcode = parentdepcode) ";
                //}
            }

            if (model.EmployeeNo != "")
            {
                condition = condition + " AND b.WorkNo = '" + model.EmployeeNo.ToUpper() + "'";
            }
            if (model.EmpName != "")
            {
                condition = condition + " AND b.LocalName like '" + model.EmpName + "%'";
            }
            if (model.AttendHandleStatus != "")
            {
                condition = condition + " AND a.STATUS ='" + model.AttendHandleStatus + "'";
            }

            if (model.ExceptionType != "")
            {
                string ExceptionType = model.ExceptionType;
                //temVal = this.ddlExceptionType.SelectedValuesToString(",").Split(new char[] { ',' });
                temVal = ExceptionType.Split(new char[] { ',' });
                for (int iLoop = 0; iLoop < temVal.Length; iLoop++)
                {
                    ddlStr = ddlStr + " a.ExceptionType='" + temVal[iLoop] + "' or";
                }
                ddlStr = ddlStr.Substring(0, ddlStr.Length - 2);
                condition = condition + " and (" + ddlStr + ")";
            }

            if (model.ShiftNoCode != "")
            {
                condition = condition + " AND INSTR(a.ShiftNo,'" + model.ShiftNoCode + "')>0 ";
            }
            if (model.CheckBoxFlag == "1")
            {
                if (model.AttendanceDateFrom != "")
                {
                    condition = condition + " and (((instr(a.ShiftNo,'A')=1 OR instr(a.ShiftNo,'B')=1) AND a.KQDate = to_date('" + DateTime.Parse(model.AttendanceDateFrom).ToString("yyyy/MM/dd") + "','yyyy/mm/dd')) or (instr(a.ShiftNo,'C')=1 AND a.KQDate = to_date('" + DateTime.Parse(model.AttendanceDateFrom).AddDays(-1.0).ToString("yyyy/MM/dd") + "','yyyy/mm/dd')))";
                }
            }
            else
            {
                condition = condition + " AND a.KQDate >= to_date('" + DateTime.Parse(model.AttendanceDateFrom).ToString("yyyy/MM/dd") + "','yyyy/mm/dd') ";

                if (model.AttendanceDateTo != "")
                {
                    condition = condition + " AND a.KQDate <= to_date('" + DateTime.Parse(model.AttendanceDateTo).ToString("yyyy/MM/dd") + "','yyyy/mm/dd') ";
                }
            }
            if (model.IsSupporter != "")
            {
                if (model.IsSupporter.Equals("N"))
                {
                    condition = condition + " AND b.flag='Local' ";
                }
                else
                {
                    condition = condition + " AND b.flag='Supporter' ";
                }
            }
            if (model.IsFillCard.Equals("Y"))
            {
                condition = condition + " AND exists (select 1 from kqm_makeup c where c.workno=a.workno and c.kqdate=a.kqdate and c.Status='2')";
            }
            condition = condition + ")";
            DataTable dt = DalHelper.ExecuteQuery(condition);

            return dt;
        }
        public DataTable GetAbnormalAttendanceInfo(string EmployeeNo, string KqDate)
        {
            string condition = "";

            #region SelectSql
            condition = "SELECT a.id,a.workno, a.kqdate, a.shiftno, b.localname, a.billno, a.approver, "
                        + "       a.approvedate, a.apremark, b.dname depname, b.dcode, "
                        + "       TO_CHAR (a.otondutytime, 'hh24:mi') otondutytime, "
                        + "       TO_CHAR (a.otoffdutytime, 'hh24:mi') otoffdutytime, "
                        + "       (SELECT shiftno || ':' || shiftdesc || '[' || shifttype "
                        + "               || ']' "
                        + "          FROM GDS_ATT_WORKSHIFT c "
                        + "         WHERE c.shiftno = a.shiftno) shiftdesc, "
                        + "       TO_CHAR (a.ondutytime, 'hh24:mi') AS ondutytime, "
                        + "       TO_CHAR (a.offdutytime, 'hh24:mi') offdutytime, "
                        + "       TRIM (TO_CHAR (CASE "
                        + "                         WHEN a.absentqty = 0 "
                        + "                            THEN NULL "
                        + "                         ELSE a.absentqty "
                        + "                      END, '999') "
                        + "            ) absentqty, "
                        + "       a.status, a.exceptiontype, a.reasontype, a.reasonremark, "
                        + "       (SELECT reasonname "
                        + "          FROM GDS_ATT_EXCEPTREASON b "
                        + "         WHERE b.reasonno = a.reasontype) reasonname, "
                        + "       (SELECT datavalue "
                        + "          FROM GDS_ATT_TYPEDATA c "
                        + "         WHERE c.datatype = 'ExceptionType' "
                        + "           AND c.datacode = a.exceptiontype) exceptiontypename, "
                        + "       (SELECT datavalue "
                        + "          FROM GDS_ATT_TYPEDATA b "
                        + "         WHERE b.datatype = 'KqmKaoQinStatus' "
                        + "           AND b.datacode = a.status) statusname, "
                        + "       (CASE "
                        + "           WHEN (SELECT COUNT (1) "
                        + "                   FROM GDS_ATT_MAKEUP c "
                        + "                  WHERE c.workno = a.workno "
                        + "                    AND c.kqdate = a.kqdate "
                        + "                    AND c.status = '2') > 0 "
                        + "              THEN 'Y' "
                        + "           ELSE 'N' "
                        + "        END "
                        + "       ) ismakeup, "
                        + "       (CASE "
                        + "           WHEN a.othours - TRUNC (a.othours) > 0.5 "
                        + "              THEN TRUNC (a.othours) + 0.5 "
                        + "           WHEN othours - TRUNC (a.othours) = 0.5 "
                        + "              THEN a.othours "
                        + "           WHEN othours - TRUNC (a.othours) < 0.5 "
                        + "              THEN TRUNC (a.othours) "
                        + "        END "
                        + "       ) AS othours, "
                        + "       ROUND ((a.offdutytime - a.ondutytime) * 24, 1) AS totalhours, "
                        + "       '0' absenttotal "
                        + "  FROM GDS_ATT_KAOQINDATA a, GDS_ATT_EMPLOYEE b "
                        + " WHERE b.workno = a.workno and b.WorkNO=:workno And a.KQDATE=to_date(:kqdate,'yyyy/mm/dd')"
                        ;
            #endregion
            DataTable dt = DalHelper.ExecuteQuery(condition, new OracleParameter(":workno", EmployeeNo), new OracleParameter(":kqdate", KqDate));
            return dt;
        }
        public DataTable GetAbnormalAttendanceInfo(string EmployeeNo, string KqDate, string sysKqoQinDays, string sysKaoQinDataAbsent)
        {

            string condition = "";
            #region SelectSql
            condition = "SELECT a.id,a.workno, a.kqdate, a.shiftno, b.localname, a.billno, a.approver, "
                     + "       a.approvedate, a.apremark, b.dname depname, b.dcode, "
                     + "       TO_CHAR (a.otondutytime, 'hh24:mi') otondutytime, "
                     + "       TO_CHAR (a.otoffdutytime, 'hh24:mi') otoffdutytime, "
                     + "       (SELECT shiftno || ':' || shiftdesc || '[' || shifttype "
                     + "               || ']' "
                     + "          FROM GDS_ATT_WORKSHIFT c "
                     + "         WHERE c.shiftno = a.shiftno) shiftdesc, "
                     + "       TO_CHAR (a.ondutytime, 'hh24:mi') AS ondutytime, "
                     + "       TO_CHAR (a.offdutytime, 'hh24:mi') offdutytime, "
                     + "       TRIM (TO_CHAR (CASE "
                     + "                         WHEN a.absentqty = 0 "
                     + "                            THEN NULL "
                     + "                         ELSE a.absentqty "
                     + "                      END, '999') "
                     + "            ) absentqty, "
                     + "       a.status, a.exceptiontype, a.reasontype, a.reasonremark, "
                     + "       (SELECT reasonname "
                     + "          FROM GDS_ATT_EXCEPTREASON b "
                     + "         WHERE b.reasonno = a.reasontype) reasonname, "
                     + "       (SELECT datavalue "
                     + "          FROM GDS_ATT_TYPEDATA c "
                     + "         WHERE c.datatype = 'ExceptionType' "
                     + "           AND c.datacode = a.exceptiontype) exceptiontypename, "
                     + "       (SELECT datavalue "
                     + "          FROM GDS_ATT_TYPEDATA b "
                     + "         WHERE b.datatype = 'KqmKaoQinStatus' "
                     + "           AND b.datacode = a.status) statusname, "
                     + "       (CASE "
                     + "           WHEN (SELECT COUNT (1) "
                     + "                   FROM GDS_ATT_MAKEUP c "
                     + "                  WHERE c.workno = a.workno "
                     + "                    AND c.kqdate = a.kqdate "
                     + "                    AND c.status = '2') > 0 "
                     + "              THEN 'Y' "
                     + "           ELSE 'N' "
                     + "        END "
                     + "       ) ismakeup, "
                     + "       (CASE "
                     + "           WHEN a.othours - TRUNC (a.othours) > 0.5 "
                     + "              THEN TRUNC (a.othours) + 0.5 "
                     + "           WHEN othours - TRUNC (a.othours) = 0.5 "
                     + "              THEN a.othours "
                     + "           WHEN othours - TRUNC (a.othours) < 0.5 "
                     + "              THEN TRUNC (a.othours) "
                     + "        END "
                     + "       ) AS othours, "
                     + "       ROUND ((a.offdutytime - a.ondutytime) * 24, 1) AS totalhours, "
                     + "       '0' absenttotal "
                     + "  FROM GDS_ATT_KAOQINDATA a, GDS_ATT_EMPLOYEE b "
                     + " WHERE b.workno = a.workno and b.WorkNO=:workno And a.KQDATE=to_date(:kqdate,'yyyy/mm/dd') and sysdate-kqdate<=:syskqoqindays "
                        ;
            #endregion

            if (sysKaoQinDataAbsent.Equals("N"))
            {
                condition = condition + " and a.ExceptionType in('A','B') ";
            }
            else
            {
                condition = condition + " and a.ExceptionType in('A','B','C','D') ";
            }

            DataTable dt = DalHelper.ExecuteQuery(condition, new OracleParameter(":workno", EmployeeNo), new OracleParameter(":kqdate", KqDate), new OracleParameter(":syskqoqindays", sysKqoQinDays));
            return dt;
        }
        public DataTable GetSignAbnormalAttendanceInfo(string BillNo, string Status)
        {

            string condition = "";
            #region SelectSql
            condition= "SELECT * "
                  + "  FROM (SELECT a.id,a.workno, a.kqdate, a.shiftno, b.localname, a.billno, "
                  + "               a.approver, a.approvedate, a.apremark, b.dname depname, "
                  + "               b.dcode, TO_CHAR (a.otondutytime, 'hh24:mi') otondutytime, "
                  + "               TO_CHAR (a.otoffdutytime, 'hh24:mi') otoffdutytime, "
                  + "               (SELECT    shiftno "
                  + "                       || ':' "
                  + "                       || shiftdesc "
                  + "                       || '[' "
                  + "                       || shifttype "
                  + "                       || ']' "
                  + "                  FROM gds_att_workshift c "
                  + "                 WHERE c.shiftno = a.shiftno) shiftdesc, "
                  + "               TO_CHAR (a.ondutytime, 'hh24:mi') AS ondutytime, "
                  + "               TO_CHAR (a.offdutytime, 'hh24:mi') offdutytime, "
                  + "               TRIM (TO_CHAR (CASE "
                  + "                                 WHEN a.absentqty = 0 "
                  + "                                    THEN NULL "
                  + "                                 ELSE a.absentqty "
                  + "                              END, "
                  + "                              '999' "
                  + "                             ) "
                  + "                    ) absentqty, "
                  + "               a.status, a.exceptiontype, a.reasontype, a.reasonremark, "
                  + "               (SELECT reasonname "
                  + "                  FROM gds_att_exceptreason b "
                  + "                 WHERE b.reasonno = a.reasontype) reasonname, "
                  + "               (SELECT datavalue "
                  + "                  FROM gds_att_typedata c "
                  + "                 WHERE c.datatype = 'ExceptionType' "
                  + "                   AND c.datacode = a.exceptiontype) exceptiontypename, "
                  + "               (SELECT datavalue "
                  + "                  FROM gds_att_typedata b "
                  + "                 WHERE b.datatype = 'KqmKaoQinStatus' "
                  + "                   AND b.datacode = a.status) statusname, "
                  + "               (CASE "
                  + "                   WHEN (SELECT COUNT (1) "
                  + "                           FROM gds_att_makeup c "
                  + "                          WHERE c.workno = a.workno "
                  + "                            AND c.kqdate = a.kqdate "
                  + "                            AND c.status = '2') > 0 "
                  + "                      THEN 'Y' "
                  + "                   ELSE 'N' "
                  + "                END "
                  + "               ) ismakeup, "
                  + "               (CASE "
                  + "                   WHEN a.othours - TRUNC (a.othours) > 0.5 "
                  + "                      THEN TRUNC (a.othours) + 0.5 "
                  + "                   WHEN othours - TRUNC (a.othours) = 0.5 "
                  + "                      THEN a.othours "
                  + "                   WHEN othours - TRUNC (a.othours) < 0.5 "
                  + "                      THEN TRUNC (a.othours) "
                  + "                END "
                  + "               ) AS othours, "
                  + "               ROUND ((a.offdutytime - a.ondutytime) * 24, 1) AS totalhours, "
                  + "               '0' absenttotal,a.dissignrmark  "
                  + "          FROM gds_att_kaoqindata a, gds_att_employee b "
                  + "         WHERE b.workno = a.workno  and a.BillNo ='" + BillNo + "' "
                  ;

            #endregion

            if (!string.IsNullOrEmpty(Status))
            {
                condition += " and a.Status='4' ";
            }
            condition += ")";

            DataTable dt = DalHelper.ExecuteQuery(condition);
            return dt;
        }
        public string GetExceptionqty(string EmployeeNo, string KqDate)
        {
            string SQL = "";
            string result = "";
            SQL = "SELECT exceptionqty "
                + "  FROM (SELECT   SUM (DECODE (INSTR (exceptiontype, 'C'), 1, 1, 0) "
                + "                     ) "
                + "               + SUM (DECODE (INSTR (exceptiontype, 'D'), 1, 1, 0)) "
                + "                                                                 exceptionqty "
                + "          FROM gds_att_kaoqindata "
                + "         WHERE workno = :workno "
                + "           AND kqdate > LAST_DAY (ADD_MONTHS (TO_DATE (:kqdate, 'yyyy/MM/dd'), -1)) "
                + "           AND kqdate <= LAST_DAY (TO_DATE (:kqdate, 'yyyy/MM/dd'))) "
                ;
            DataTable dt = DalHelper.ExecuteQuery(SQL, new OracleParameter(":workno", EmployeeNo), new OracleParameter(":kqdate", KqDate));
            if (dt.Rows.Count > 0)
            {
                result = dt.Rows[0]["EXCEPTIONQTY"].ToString();
            }
            return result;
        }
        public string GetSysKaoqinDataAbsent()
        {
            string SQL = "";
            string result = "";
            SQL = " select nvl(MAX(paravalue),'N') syskaoqindataabsent from GDS_SC_PARAMETER where paraname='KaoQinDataAbsent'";
            DataTable dt = DalHelper.ExecuteQuery(SQL);
            if (dt.Rows.Count > 0)
            {
                result = dt.Rows[0]["SYSKAOQINDATAABSENT"].ToString();
            }
            return result;
        }
        public DataTable GetAbnormalAttendanceHandleStatus(string DataType)
        {
            string SQL = "";
            SQL = " SELECT DataType,DataCode,DataValue,DataTypeDetail,(DataCode||'?B'||DataValue)as newDataValue FROM GDS_ATT_TYPEDATA WHERE DataType=:datatype ORDER BY OrderId";
            DataTable dt = DalHelper.ExecuteQuery(SQL, new OracleParameter(":datatype", DataType));
            return dt;
        }
        public int GetVWorkNoCount(string EmployeeNo, string DCode)
        {

            string condition = "";
            int result = 0;

            //condition = "select NVL(count(WorkNO),0) VWorkNoCount from GDS_ATT_EMPLOYEE where WorkNO=:employeeno and status='0' and Dcode IN(:dcode) ";
            condition = "select NVL(count(WorkNO),0) VWorkNoCount from GDS_ATT_EMPLOYEE where WorkNO=:employeeno and status='0'  ";
            DataTable dt = DalHelper.ExecuteQuery(condition, new OracleParameter(":employeeno", EmployeeNo));
            if (dt.Rows.Count > 0)
            {
                result = int.Parse(dt.Rows[0]["VWorkNoCount"].ToString());
            }
            return result;
        }
        public string GetWorkFlowOrgCode(string DepCode, string BillTypeCode, string reason1)
        {
            string ReturnOrgCode = "";
            try
            {
                if (DepCode != "")
                {
                    //從當前部門開始循環向上查找有簽核流程的部門編碼
                    //string SQL
                    //    = "SELECT depcode "
                    //    + "  FROM (SELECT   b.depcode, b.orderid "
                    //    + "            FROM gds_wf_flowset a, "
                    //    + "                 (SELECT     LEVEL orderid, depcode "
                    //    + "                        FROM gds_sc_department "
                    //    + "                  START WITH depcode = '" + DepCode + "' "
                    //    + "                  CONNECT BY PRIOR parentdepcode = depcode "
                    //    + "                    ORDER BY LEVEL) b "
                    //    + "           WHERE a.deptcode = b.depcode AND a.formtype = '" + BillTypeCode + "' "
                    //    + "        ORDER BY orderid) "
                    //    + " WHERE ROWNUM <= 1 "
                    //    ;
                    //只查找當前部門是否有簽核流程 
                    string SQL
                    = "SELECT a.deptcode "
                    + "  FROM gds_wf_flowset a "
                    + " WHERE a.deptcode = '" + DepCode + "' AND a.formtype = '" + BillTypeCode + "' AND ROWNUM <= 1 "
                    ;

                    DataTable dt = DalHelper.ExecuteQuery(SQL);

                    if (dt.Rows.Count > 0)
                    {
                        ReturnOrgCode = dt.Rows[0][0].ToString().Trim();
                    }
                }
                return ReturnOrgCode;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable GetBellCardData(string WorkNo, string KQDate, string ShiftNo)
        {
            string SQL = "";
            SQL = " SELECT b.workno, b.localname,TO_CHAR (a.cardtime, 'yyyy/mm/dd hh24:mi:ss') AS cardtime, a.bellno,TO_CHAR (a.readtime, 'yyyy/mm/dd hh24:mi:ss') AS readtime, a.cardno   FROM GDS_ATT_BELLCARDDATA a,GDS_ATT_EMPLOYEE b where (b.CardNo=a.CardNo or b.workno=a.workno) ";
            if ((ShiftNo.IndexOf("A") >= 0) || (ShiftNo.IndexOf("B") >= 0))
            {
                SQL = SQL + " and b.WorkNo='" + WorkNo + "' and a.cardtime>=to_date('" + KQDate + "','yyyy/mm/dd') and a.cardtime<to_date('" + Convert.ToDateTime(KQDate).AddDays(1.0).ToString("yyyy/MM/dd") + "','yyyy/mm/dd') ";
            }
            else
            {
                SQL = SQL + " and b.WorkNo='" + WorkNo + "' and a.cardtime>=to_date('" + KQDate + " 12:00:00','yyyy/mm/dd hh:mi:ss') and a.cardtime<=to_date('" + Convert.ToDateTime(KQDate).AddDays(1.0).ToString("yyyy/MM/dd") + " 12:00:00','yyyy/mm/dd hh:mi:ss')";
            }

            DataTable dt = DalHelper.ExecuteQuery(SQL);
            return dt;
        }
        public DataTable ExceltoDataView(string strFilePath)
        {
            DataTable dt = new DataTable();
            try
            {
                OleDbConnection conn = new OleDbConnection("Provider=Microsoft.Jet.Oledb.4.0;Data Source=" + strFilePath + ";Extended Properties='Excel 8.0;HDR=YES;IMEX=1'");
                conn.Open();
                object[] CSs0s0001 = new object[4];
                CSs0s0001[3] = "TABLE";
                DataTable tblSchema = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, CSs0s0001);
                string tableName = Convert.ToString(tblSchema.Rows[0]["TABLE_NAME"]);
                if (tblSchema.Rows.Count > 1)
                {
                    tableName = "sheet1$";
                }
                string sql_F = "SELECT * FROM [{0}]";
                OleDbDataAdapter adp = new OleDbDataAdapter(string.Format(sql_F, tableName), conn);
                adp.Fill(dt);
                conn.Close();
                return dt;
            }
            catch (Exception)
            {
                return null;
                //Exception strEx = new Exception("請確認是否使用模板上傳(上傳的Excel中第一個工作表名稱是否為Sheet1)");
                //throw strEx;
            }
        }
        public string GetsysKqoQinDays(string CompanyId, string RoleCode)
        {
            string sysKqoQinDays = "0";
            try
            {
                string SQL = "";
                SQL = "select nvl(MAX(paravalue),'3') sysKqoQinDays from GDS_SC_PARAMETER where paraname='KaoQinDataFLAG'";
                sysKqoQinDays = Convert.ToString(DalHelper.ExecuteScalar(SQL));

                int i = 0;
                int WorkDays = 0;
                string UserOTType = "";
                while (i < Convert.ToDouble(sysKqoQinDays))
                {
                    SQL = "SELECT workflag FROM GDS_ATT_BGCALENDAR WHERE workday = to_date('" + DateTime.Now.AddDays((double)((-1 - i) - WorkDays)).ToString("yyyy/MM/dd") + "','yyyy/MM/dd') AND bgcode IN (SELECT depcode FROM GDS_SC_DEPARTMENT WHERE levelcode = '0' and companyid='" + CompanyId + "')";
                    UserOTType = Convert.ToString(DalHelper.ExecuteScalar(SQL));
                    if (UserOTType.Length == 0)
                    {
                        break;
                    }
                    if (UserOTType.Equals("Y"))
                    {
                        i++;
                    }
                    else
                    {
                        WorkDays++;
                    }
                }
                sysKqoQinDays = Convert.ToString((int)(i + WorkDays));
            }
            catch (Exception)
            {
                sysKqoQinDays = "10";
            }
            if (RoleCode.Equals("admin"))
            {
                sysKqoQinDays = Convert.ToString((int)(Convert.ToInt32(sysKqoQinDays) + 30));
            }
            return sysKqoQinDays;
        }
        public bool KQMSaveAbnormalAttendanceInfo(string processFlag, DataTable dataTable, string appUser, SynclogModel logmodel)
        {
            bool result = false;
            string sSql = "";
            OracleCommand command = new OracleCommand();
            command.Connection = DalHelper.Connection;
            command.Connection.Open();
            OracleTransaction trans = command.Connection.BeginTransaction();
            command.Transaction = trans;
            try
            {
                foreach (DataRow newRow in dataTable.Rows)
                {
                    if (processFlag.Equals("Modify"))
                    {
                        sSql = string.Concat(new object[] { "UPDATE GDS_ATT_KAOQINDATA SET Status='", newRow["Status"], "', ReasonType='", newRow["ReasonType"], "',ReasonRemark='", newRow["ReasonRemark"], "' Where WorkNo='", newRow["WorkNo"], "' and KQDate=to_date('", DateTime.Parse(newRow["KQDate"].ToString()).ToString("yyyy/MM/dd"), "','yyyy/mm/dd')" });
                        command.CommandText = sSql;
                        command.ExecuteNonQuery();
                        SaveLogData("U", "", command.CommandText, command, logmodel);
                    }
                    else if (processFlag.Equals("Confirm"))
                    {
                        sSql = string.Concat(new object[] { "UPDATE GDS_ATT_KAOQINDATA SET Status='3',Approver='", appUser, "',ApproveDate=sysdate  Where WorkNo='", newRow["WorkNo"], "' and KQDate=to_date('", DateTime.Parse(newRow["KQDate"].ToString()).ToString("yyyy/MM/dd"), "','yyyy/mm/dd')" });
                        command.CommandText = sSql;
                        command.ExecuteNonQuery();
                        SaveLogData("U", "", command.CommandText, command, logmodel);
                    }
                    else if (processFlag.Equals("UnConfirm"))
                    {
                        sSql = string.Concat(new object[] { "UPDATE GDS_ATT_KAOQINDATA SET Status='2' ,Approver='',ApproveDate=null  Where WorkNo='", newRow["WorkNo"], "' and KQDate=to_date('", DateTime.Parse(newRow["KQDate"].ToString()).ToString("yyyy/MM/dd"), "','yyyy/mm/dd')" });
                        command.CommandText = sSql;
                        command.ExecuteNonQuery();
                        SaveLogData("U", "", command.CommandText, command, logmodel);
                    }
                }
                trans.Commit();
                command.Connection.Close();
                result = true;
                return result;
            }
            catch (Exception ex)
            {
                trans.Rollback();
                result = false;
                command.Connection.Close();
                return result;
            }
        }
        public bool KQMSaveAuditData(string WorkNo, string KQDate, string BillNoType,string BillTypeCode, string ApplyMan,string AuditOrgCode,string Flow_LevelRemark,SynclogModel logmodel)
        {
            string strMax = "";
            string sSql = "";
            bool bResult = false;
            bool ConnectionOpenHere = false;
            OracleCommand command = new OracleCommand();
            command.Connection = DalHelper.Connection;
            if (command.Connection.State == ConnectionState.Closed)
            {
                command.Connection.Open();
                ConnectionOpenHere = true;
            }
            OracleTransaction trans = command.Connection.BeginTransaction();
            command.Transaction = trans;
            try
            {

                BillNoType = BillNoType + AuditOrgCode;
                sSql = "SELECT MAX (billno) strMax  FROM GDS_ATT_KAOQINDATA WHERE billno LIKE '" + BillNoType + "'|| TO_CHAR (SYSDATE, 'yymm')|| '%'";
                command.CommandText = sSql;
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
                sSql = "UPDATE GDS_ATT_KAOQINDATA SET Status='4' , BillNo = '" + strMax + "'  Where WorkNo='" + WorkNo + "' and KQDate=to_date('" + DateTime.Parse(KQDate).ToString("yyyy/MM/dd") + "','yyyy/mm/dd')";
                command.CommandText = sSql;
                command.ExecuteNonQuery();
                SaveLogData("U", strMax, command.CommandText, command, logmodel);

                ///-------------------------------GDS_ATT_BILL---------------------------------
                string BillNo = strMax;

                sSql = "SELECT count(1) FROM GDS_ATT_BILL WHERE BillNo='" + BillNo + "'";
                command.CommandText = sSql;
                if (Convert.ToDecimal(command.ExecuteScalar()) == 0M)
                {
                    sSql = "insert into GDS_ATT_BILL(BillNo,OrgCode,ApplyMan,ApplyDate,Status,BillTypeCode) values('" + BillNo + "','" + AuditOrgCode + "','" + ApplyMan + "',sysdate,'0','" + BillTypeCode + "')";
                    command.CommandText = sSql;
                    command.ExecuteNonQuery();
                    SaveLogData("I", BillNo, command.CommandText, command, logmodel);
                }
                else
                {
                    sSql = "update GDS_ATT_BILL set OrgCode='" + AuditOrgCode + "',ApplyMan='" + ApplyMan + "',BillTypeCode='" + BillTypeCode+"',ApplyDate=sysdate,Status='0' where BillNo='" + BillNo + "'";
                    command.CommandText = sSql;
                    command.ExecuteNonQuery();
                    SaveLogData("U", BillNo, command.CommandText, command, logmodel);
                }
                ///-------------------------------GDS_ATT_AUDITSTATUS---------------------------------
                sSql = "SELECT count(1) FROM GDS_ATT_AUDITSTATUS WHERE BillNo='" + BillNo + "' ";
                command.CommandText = sSql;

                if (Convert.ToDecimal(command.ExecuteScalar()) > 0M)
                {
                    sSql = "delete FROM GDS_ATT_AUDITSTATUS WHERE BillNo='" + BillNo + "' ";
                    command.CommandText = sSql;
                    command.ExecuteNonQuery();
                    SaveLogData("D", BillNo, command.CommandText, command, logmodel);
                }
                sSql = "insert into GDS_ATT_AUDITSTATUS(BillNo,AuditMan,OrderNo,AuditStatus,SendNotes,OLDAUDITMAN)  " +
                                "select '" + BillNo + "', nvl(getagentempno(FLOW_EMPNO,'" + AuditOrgCode + "'),FLOW_EMPNO) as FLOW_EMPNO, ORDERID,'0','N',decode(NVL (getagentempno (flow_empno, '" + AuditOrgCode + "'), flow_empno),flow_empno,'',flow_empno) as oldauditman  from (  " +
                                        " select * from GDS_WF_FLOWSET WHERE FORMTYPE='" + BillTypeCode + "' and DEPTCODE='" + AuditOrgCode + "' ) " +
                                        " where  FLOW_EMPNO!='" + WorkNo + "'or (FLOW_EMPNO='" + WorkNo + "' and FLOW_LEVEL not in (" + Flow_LevelRemark + ")) ";
                command.CommandText = sSql;
                command.ExecuteNonQuery();
                SaveLogData("I", BillNo, command.CommandText, command, logmodel);

                trans.Commit();
                command.Connection.Close();
                bResult=true;
            }
            catch (Exception ex)
            {
                if (command.Transaction != null)
                {
                    command.Transaction.Rollback();
                    command.Transaction = null;
                }
                return false;
                //throw ex;
            }
            finally
            {
                if (ConnectionOpenHere)
                {
                    command.Connection.Close();
                }
            }
            return bResult;
        }
        public bool KQMSaveAuditData(string WorkNo, string KQDate, string BillNoType, string BillTypeCode, string ApplyMan, string AuditOrgCode, string Flow_LevelRemark, OracleConnection OracleString, SynclogModel logmodel)
        {
            string strMax = "";
            string sSql = "";
            bool bResult = false;
            bool ConnectionOpenHere = false;
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
                ConnectionOpenHere = true;
            }
            OracleTransaction trans = command.Connection.BeginTransaction();
            command.Transaction = trans;
            try
            {

                BillNoType = BillNoType + AuditOrgCode;
                sSql = "SELECT MAX (billno) strMax  FROM GDS_ATT_KAOQINDATA WHERE billno LIKE '" + BillNoType + "'|| TO_CHAR (SYSDATE, 'yymm')|| '%'";
                command.CommandText = sSql;
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
                sSql = "UPDATE GDS_ATT_KAOQINDATA SET Status='4' , BillNo = '" + strMax + "'  Where WorkNo='" + WorkNo + "' and KQDate=to_date('" + DateTime.Parse(KQDate).ToString("yyyy/MM/dd") + "','yyyy/mm/dd')";
                command.CommandText = sSql;
                command.ExecuteNonQuery();
                SaveLogData("U", strMax, command.CommandText, command, logmodel);

                ///-------------------------------GDS_ATT_BILL---------------------------------
                string BillNo = strMax;

                sSql = "SELECT count(1) FROM GDS_ATT_BILL WHERE BillNo='" + BillNo + "'";
                command.CommandText = sSql;
                if (Convert.ToDecimal(command.ExecuteScalar()) == 0M)
                {
                    sSql = "insert into GDS_ATT_BILL(BillNo,OrgCode,ApplyMan,ApplyDate,Status,BillTypeCode) values('" + BillNo + "','" + AuditOrgCode + "','" + ApplyMan + "',sysdate,'0','" + BillTypeCode + "')";
                    command.CommandText = sSql;
                    command.ExecuteNonQuery();
                    SaveLogData("I", BillNo, command.CommandText, command, logmodel);
                }
                else
                {
                    sSql = "update GDS_ATT_BILL set OrgCode='" + AuditOrgCode + "',ApplyMan='" + ApplyMan + "',BillTypeCode='" + BillTypeCode + "',ApplyDate=sysdate,Status='0' where BillNo='" + BillNo + "'";
                    command.CommandText = sSql;
                    command.ExecuteNonQuery();
                    SaveLogData("U", BillNo, command.CommandText, command, logmodel);
                }
                ///-------------------------------GDS_ATT_AUDITSTATUS---------------------------------
                sSql = "SELECT count(1) FROM GDS_ATT_AUDITSTATUS WHERE BillNo='" + BillNo + "' ";
                command.CommandText = sSql;

                if (Convert.ToDecimal(command.ExecuteScalar()) > 0M)
                {
                    sSql = "delete FROM GDS_ATT_AUDITSTATUS WHERE BillNo='" + BillNo + "' ";
                    command.CommandText = sSql;
                    command.ExecuteNonQuery();
                    SaveLogData("D", BillNo, command.CommandText, command, logmodel);
                }
                sSql = "insert into GDS_ATT_AUDITSTATUS(BillNo,AuditMan,OrderNo,AuditStatus,SendNotes,OLDAUDITMAN)  " +
                                "select '" + BillNo + "', nvl(getagentempno(FLOW_EMPNO,'" + AuditOrgCode + "'),FLOW_EMPNO) as FLOW_EMPNO, ORDERID,'0','N',decode(NVL (getagentempno (flow_empno, '" + AuditOrgCode + "'), flow_empno),flow_empno,'',flow_empno) as oldauditman  from (  " +
                                        " select * from GDS_WF_FLOWSET WHERE FORMTYPE='" + BillTypeCode + "' and DEPTCODE='" + AuditOrgCode + "' ) " +
                                        " where  FLOW_EMPNO!='" + WorkNo + "'or (FLOW_EMPNO='" + WorkNo + "' and FLOW_LEVEL not in (" + Flow_LevelRemark + ")) ";
                command.CommandText = sSql;
                command.ExecuteNonQuery();
                SaveLogData("I", BillNo, command.CommandText, command, logmodel);

                trans.Commit();
                command.Connection.Close();
                bResult = true;
            }
            catch (Exception ex)
            {
                if (command.Transaction != null)
                {
                    command.Transaction.Rollback();
                    command.Transaction = null;
                }
                return false;
                //throw ex;
            }
            finally
            {
                if (ConnectionOpenHere)
                {
                    command.Connection.Close();
                }
            }
            return bResult;
        }
        public bool WFMSaveData(string BillNo, string OrgCode, string ApplyMan, SynclogModel logmodel)
        {
            bool result = false;
            string sSql = "";
            OracleCommand command = new OracleCommand();
            command.Connection = DalHelper.Connection;
            command.Connection.Open();
            OracleTransaction trans = command.Connection.BeginTransaction();
            command.Transaction = trans;
            try
            {
                sSql = "SELECT count(1) FROM GDS_ATT_BILL WHERE BillNo='" + BillNo + "'";
                command.CommandText = sSql;
                if (Convert.ToDecimal(command.ExecuteScalar()) == 0M)
                {
                    sSql = "insert into GDS_ATT_BILL(BillNo,OrgCode,ApplyMan,ApplyDate,Status) values('" + BillNo + "','" + OrgCode + "','" + ApplyMan + "',sysdate,'0')";
                    command.CommandText = sSql;
                    command.ExecuteNonQuery();
                    SaveLogData("I", BillNo, command.CommandText, command, logmodel);
                }
                else
                {
                    sSql = "update GDS_ATT_BILL set OrgCode='" + OrgCode + "',ApplyMan='" + ApplyMan + "',ApplyDate=sysdate,Status='0' where BillNo='" + BillNo + "'";
                    command.CommandText = sSql;
                    command.ExecuteNonQuery();
                    SaveLogData("U", BillNo, command.CommandText, command, logmodel);
                }
                trans.Commit();
                command.Connection.Close();
                result = true;
                return result;
            }
            catch (Exception ex)
            {
                trans.Rollback();
                command.Connection.Close();
                return result;
            }
        }
        public bool WFMSaveAuditStatusData(string BillNo, string OrgCode, string BillTypeCode,string SendUser,string Flow_LevelRemark, SynclogModel logmodel)
        {
            bool result = false;
            string sSql = "";
            OracleCommand command = new OracleCommand();
            command.Connection = DalHelper.Connection;
            command.Connection.Open();
            OracleTransaction trans = command.Connection.BeginTransaction();
            command.Transaction = trans;
            try
            {
                sSql = "SELECT count(1) FROM GDS_ATT_AUDITSTATUS WHERE BillNo='" + BillNo + "' ";
                command.CommandText = sSql;

                if (Convert.ToDecimal(command.ExecuteScalar()) > 0M)
                {
                    sSql = "delete FROM GDS_ATT_AUDITSTATUS WHERE BillNo='" + BillNo + "' ";
                    command.CommandText = sSql;
                    command.ExecuteNonQuery();
                    SaveLogData("D", BillNo, command.CommandText, command, logmodel);
                }
                //sSql = "insert into GDS_ATT_AUDITSTATUS(BillNo,AuditMan,OrderNo,AuditStatus,SendNotes)SELECT '" + BillNo + "', FLOW_EMPNO, ORDERID,'0','N'   FROM GDS_WF_FLOWSET WHERE FORMTYPE='" + BillTypeCode + "' and DEPTCODE='" + OrgCode + "'";
                // command.CommandText = "insert into GDS_ATT_AUDITSTATUS(BillNo,AuditMan,OrderNo,AuditStatus,SendNotes) SELECT '" + strMax + "', FLOW_EMPNO, ORDERID,'0','N'   FROM GDS_WF_FLOWSET WHERE FORMTYPE='" + BillTypeCode + "' and DEPTCODE='" + AuditOrgCode + "' and REASON1='" + OTType + "' ";
                sSql = "insert into GDS_ATT_AUDITSTATUS(BillNo,AuditMan,OrderNo,AuditStatus,SendNotes,OLDAUDITMAN)  " +
                                "select '" + BillNo + "', nvl(getagentempno(FLOW_EMPNO,'" + OrgCode + "'),FLOW_EMPNO) as FLOW_EMPNO, ORDERID,'0','N',decode(NVL (getagentempno (flow_empno, '" + OrgCode + "'), flow_empno),flow_empno,'',flow_empno) as oldauditman  from (  " +
                                        " select * from GDS_WF_FLOWSET WHERE FORMTYPE='" + BillTypeCode + "' and DEPTCODE='" + OrgCode + "' ) " +
                                        " where  FLOW_EMPNO!='" + SendUser + "'or (FLOW_EMPNO='" + SendUser + "' and FLOW_LEVEL not in (" + Flow_LevelRemark + ")) ";

                
                command.CommandText = sSql;
                command.ExecuteNonQuery();
                SaveLogData("I", BillNo, command.CommandText, command, logmodel);

                sSql = "Update GDS_ATT_BILL set BillTypeCode='" + BillTypeCode + "' WHERE BillNo='" + BillNo + "' ";
                command.CommandText = sSql;
                command.ExecuteNonQuery();
                SaveLogData("U", BillNo, command.CommandText, command, logmodel);
                trans.Commit();
                command.Connection.Close();
                result = true;
                return result;
            }
            catch (Exception ex)
            {
                trans.Rollback();
                command.Connection.Close();
                return result;
            }
        }

        public int SaveOrgAuditData(string processFlag, Dictionary<string, List<string>> diry, string BillNoType, string BillTypeCode, string Person,string Flow_LevelRemark, SynclogModel logmodel)
        {

            string strMax = "";
            string num = "0";
            string num1 = "0";
            int k = 0;
            string sSql = "";

            OracleCommand command = new OracleCommand();
            command.Connection = DalHelper.Connection;
            command.Connection.Open();
            OracleTransaction trans = command.Connection.BeginTransaction();
            command.Transaction = trans;

            try
            {
                foreach (string key in diry.Keys)
                {
                    string AuditOrgCode = key;

                    if (processFlag.Equals("Add"))
                    {
                        BillNoType = BillNoType + AuditOrgCode;
                        sSql = "SELECT MAX (billno) strMax  FROM GDS_ATT_KAOQINDATA WHERE billno LIKE '" + BillNoType + "'|| TO_CHAR (SYSDATE, 'yymm')|| '%'";
                        command.CommandText = sSql;
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
                        string sql2 = "SELECT count(1) num FROM GDS_ATT_BILL WHERE BillNo='" + strMax + "'";
                        command.CommandText = sql2;
                        num =Convert.ToString(command.ExecuteScalar());
                        string sql4 = "SELECT count(1) num FROM GDS_ATT_AUDITSTATUS WHERE BillNo='" + strMax + "'";
                        command.CommandText = sql4;
                        num1 = num = Convert.ToString(command.ExecuteScalar());

                        foreach (string ID in diry[key])
                        {
                            sSql = "UPDATE GDS_ATT_KAOQINDATA SET Status='4' , BillNo =  '" + strMax + "' Where ID='" + ID + "'";
                            command.CommandText = sSql;
                            command.ExecuteNonQuery();
                            SaveLogData("U", strMax, command.CommandText, command, logmodel);
                        }
                        if (num == "0")
                        {
                            command.CommandText = "insert into GDS_ATT_BILL(BillNo,OrgCode,ApplyMan,ApplyDate,Status,BillTypeCode) values('" + strMax + "','" + AuditOrgCode + "','" + Person + "',sysdate,'0','" + BillTypeCode + "')";
                            command.ExecuteNonQuery();
                            SaveLogData("I", strMax, command.CommandText, command, logmodel);
                        }
                        else
                        {
                            command.CommandText = "update GDS_ATT_BILL set OrgCode='" + AuditOrgCode + "',ApplyMan='" + Person + "',ApplyDate=sysdate,Status='0',BillTypeCode='" + BillTypeCode + "' where BillNo='" + strMax + "'";
                            command.ExecuteNonQuery();
                            SaveLogData("U", strMax, command.CommandText, command, logmodel);
                        }

                        if (num1 != "0")
                        {
                            command.CommandText = "delete FROM GDS_ATT_AUDITSTATUS WHERE BillNo='" + strMax + "' ";
                            command.ExecuteNonQuery();
                            SaveLogData("D", strMax, command.CommandText, command, logmodel);
                        }
                        if (diry[key].Count == 1)
                        {
                            command.CommandText = "select WORKNO from  GDS_ATT_KAOQINDATA where ID='" + diry[key][0].ToString() + "'";
                            string senduser = Convert.ToString(command.ExecuteScalar());
                            command.CommandText = "insert into GDS_ATT_AUDITSTATUS(BillNo,AuditMan,OrderNo,AuditStatus,SendNotes,OLDAUDITMAN)  " +
                                            "select '" + strMax + "', nvl(getagentempno(FLOW_EMPNO,'" + AuditOrgCode + "'),FLOW_EMPNO) as FLOW_EMPNO, ORDERID,'0','N',decode(NVL (getagentempno (flow_empno, '" + AuditOrgCode + "'), flow_empno),flow_empno,'',flow_empno) as oldauditman  from (  " +
                                                    " select * from GDS_WF_FLOWSET WHERE FORMTYPE='" + BillTypeCode + "' and DEPTCODE='" + AuditOrgCode + "' ) " +
                                                    " where  FLOW_EMPNO!='" + senduser + "'or (FLOW_EMPNO='" + senduser + "' and FLOW_LEVEL not in (" + Flow_LevelRemark + ")) ";
                        }
                        else
                        {
                            command.CommandText = "insert into GDS_ATT_AUDITSTATUS(BillNo,AuditMan,OrderNo,AuditStatus,SendNotes,OLDAUDITMAN) SELECT '" + strMax + "', nvl(getagentempno(FLOW_EMPNO,'" + AuditOrgCode + "'),FLOW_EMPNO) as FLOW_EMPNO, ORDERID,'0','N',decode(NVL (getagentempno (flow_empno, '" + AuditOrgCode + "'), flow_empno),flow_empno,'',flow_empno) as oldauditman   FROM GDS_WF_FLOWSET WHERE FORMTYPE='" + BillTypeCode + "' and DEPTCODE='" + AuditOrgCode + "' ";
                        }
                           

                          //command.CommandText = "insert into GDS_ATT_AUDITSTATUS(BillNo,AuditMan,OrderNo,AuditStatus,SendNotes,OLDAUDITMAN) SELECT '" + strMax + "', nvl(getagentempno(FLOW_EMPNO,'" + AuditOrgCode + "'),FLOW_EMPNO) as FLOW_EMPNO, ORDERID,'0','N',decode(NVL (getagentempno (flow_empno, '" + AuditOrgCode + "'), flow_empno),flow_empno,'',flow_empno) as oldauditman   FROM GDS_WF_FLOWSET WHERE FORMTYPE='" + BillTypeCode + "' and DEPTCODE='" + AuditOrgCode + "' ";
                        command.ExecuteNonQuery();
                        SaveLogData("I", strMax, command.CommandText, command, logmodel);
                    }
                    //else if (processFlag.Equals("Modify"))
                    //{
                    //    foreach (string ID in diry[key])
                    //    {
                    //        strMax = BillNoType;
                    //        sSql = "UPDATE GDS_ATT_KAOQINDATA SET Status='4' , BillNo =  '" + strMax + "' Where ID='" + ID + "'";
                    //        command.ExecuteNonQuery();
                    //        SaveLogData("U", strMax, command.CommandText, command, logmodel);
                    //    }
                    //}
                    trans.Commit();
                    command.Connection.Close();

                    k++;
                }
            }
            catch (Exception ex)
            {
                trans.Rollback();
                command.Connection.Close();
                throw ex;

            }
            return k;
        }
        public void SaveLogData(string strFlag, string DocNo, string LogText, OracleCommand command, SynclogModel logmodel)
        {

            try
            {
                string ProcessFlag = "";

                if (strFlag == "I")
                {
                    ProcessFlag = "INSERT";
                }
                else if (strFlag == "U")
                {
                    ProcessFlag = "UPDATE";
                }
                else if (strFlag == "D")
                {
                    ProcessFlag = "DELETE";
                }

                command.CommandText = "Insert into GDS_SC_SYNCLOG "+
                "   (transactiontype,levelno,FromHost,ToHost,docno,text,logtime,processflag,processowner) "+
                " values('" + logmodel.TransactionType + "','2','" + logmodel .FromHost+ "','" + logmodel.ToHost + "','" + DocNo + "','" + LogText.Replace("'", "''") + "',sysdate,'" + ProcessFlag + "','" + logmodel.ProcessOwner + "')";
                command.ExecuteNonQuery();
            }
            catch (Exception)
            {
            }

        }
    }
}
