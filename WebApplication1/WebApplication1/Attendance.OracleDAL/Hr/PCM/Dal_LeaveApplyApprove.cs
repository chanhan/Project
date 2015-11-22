using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using GDSBG.MiABU.Attendance.IDAL.WorkFlow;
using GDSBG.MiABU.Attendance.IDAL.Hr.PCM;
using GDSBG.MiABU.Attendance.Model.Hr.PCM;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;
using GDSBG.MiABU.Attendance.OracleDAL.Hr.KQM.KaoQinData;

namespace GDSBG.MiABU.Attendance.OracleDAL.Hr.PCM
{
    public class Dal_LeaveApplyApprove : DALBase<KaoQinDataModel>, IDal_LeaveApplyApprove
    {
        public DataTable GetLeaveApplyApproveInfo(string condition, int pageIndex, int pageSize, out int totalCount)
        {
            #region SelectSql
            string SQL
                = "select * from (SELECT a.*, b.localname, b.dcode, b.levelname, "
                + "       (SELECT datavalue "
                + "          FROM gds_att_typedata c "
                + "         WHERE c.datatype = 'Sex' AND c.datacode = b.sex) sexname, b.sex, "
                + "       b.dname depname, b.levelcode, b.managercode, "
                + "       (SELECT     depname "
                + "              FROM gds_sc_department s "
                + "             WHERE s.levelcode = '2' "
                + "        START WITH s.depcode = b.depcode "
                + "        CONNECT BY s.depcode = PRIOR s.parentdepcode) buname, "
                + "       (SELECT lvtypename "
                + "          FROM gds_att_leavetype c "
                + "         WHERE c.lvtypecode = a.lvtypecode) lvtypename, "
                + "       (SELECT datavalue "
                + "          FROM gds_att_typedata c "
                + "         WHERE c.datatype = 'ApplyType' "
                + "           AND c.datacode = a.applytype) applytypename, "
                + "       (SELECT datavalue "
                + "          FROM gds_att_typedata c "
                + "         WHERE c.datatype = 'ApplyState' AND c.datacode = a.status) "
                + "                                                                   statusname, "
                + "       (SELECT notes "
                + "          FROM gds_att_employee e "
                + "         WHERE e.workno = a.proxyworkno) proxynotes, "
                + "       (SELECT flag "
                + "          FROM gds_att_employee e "
                + "         WHERE e.workno = a.proxyworkno) proxyflag, "
                + "       (SELECT localname "
                + "          FROM gds_att_employee e "
                + "         WHERE e.workno = a.update_user) modifyname, "
                + "       (SELECT datavalue "
                + "          FROM gds_att_typedata c "
                + "         WHERE c.datatype = 'ProxyStatus' "
                + "           AND c.datacode = a.proxystatus) proxystatusname, "
                + "       (SELECT istestify "
                + "          FROM gds_att_leavetype c "
                + "         WHERE c.lvtypecode = a.lvtypecode) istestify, "
                + "       ROUND (  (MONTHS_BETWEEN (SYSDATE, b.joindate) - NVL (b.deductyears, 0) "
                + "                ) "
                + "              / 12, "
                + "              1 "
                + "             ) comeyears "
                + "  FROM GDS_ATT_LEAVEAPPLY a, gds_att_employee b "
                + " WHERE a.workno = b.workno " + condition + ")"
                ;
    

            #endregion

            DataTable dt = DalHelper.ExecutePagerQuery(SQL, pageIndex, pageSize, out totalCount);

            return dt;
        }
        public DataTable GetLeaveApplyApproveInfo(string condition)
        {
            #region SelectSql
            string SQL
                = "SELECT a.*, b.localname, b.dcode, b.levelname, "
                + "       (SELECT datavalue "
                + "          FROM gds_att_typedata c "
                + "         WHERE c.datatype = 'Sex' AND c.datacode = b.sex) sexname, b.sex, "
                + "       b.dname depname, b.levelcode, b.managercode, "
                + "       (SELECT     depname "
                + "              FROM gds_sc_department s "
                + "             WHERE s.levelcode = '2' "
                + "        START WITH s.depcode = b.depcode "
                + "        CONNECT BY s.depcode = PRIOR s.parentdepcode) buname, "
                + "       (SELECT lvtypename "
                + "          FROM gds_att_leavetype c "
                + "         WHERE c.lvtypecode = a.lvtypecode) lvtypename, "
                + "       (SELECT datavalue "
                + "          FROM gds_att_typedata c "
                + "         WHERE c.datatype = 'ApplyType' "
                + "           AND c.datacode = a.applytype) applytypename, "
                + "       (SELECT datavalue "
                + "          FROM gds_att_typedata c "
                + "         WHERE c.datatype = 'ApplyState' AND c.datacode = a.status) "
                + "                                                                   statusname, "
                + "       (SELECT notes "
                + "          FROM gds_att_employee e "
                + "         WHERE e.workno = a.proxyworkno) proxynotes, "
                + "       (SELECT flag "
                + "          FROM gds_att_employee e "
                + "         WHERE e.workno = a.proxyworkno) proxyflag, "
                + "       (SELECT localname "
                + "          FROM gds_att_employee e "
                + "         WHERE e.workno = a.update_user) modifyname, "
                + "       (SELECT datavalue "
                + "          FROM gds_att_typedata c "
                + "         WHERE c.datatype = 'ProxyStatus' "
                + "           AND c.datacode = a.proxystatus) proxystatusname, "
                + "       (SELECT istestify "
                + "          FROM gds_att_leavetype c "
                + "         WHERE c.lvtypecode = a.lvtypecode) istestify, "
                + "       ROUND (  (MONTHS_BETWEEN (SYSDATE, b.joindate) - NVL (b.deductyears, 0) "
                + "                ) "
                + "              / 12, "
                + "              1 "
                + "             ) comeyears "
                + "  FROM GDS_ATT_LEAVEAPPLY a, gds_att_employee b "
                + " WHERE a.workno = b.workno " + condition;
            ;

            #endregion

            DataTable dt = DalHelper.ExecuteQuery(SQL);
            return dt;
        }
        public DataTable GetVDataByCondition(string condition)
        {
            #region SelectSql
            string SQL
                = "SELECT a.workno, a.localname, a.marrystate, a.dname, a.levelcode, "
                + "               a.managercode, a.identityno, a.notes, a.flag, "
                + "               (SELECT datavalue "
                + "                  FROM GDS_ATT_TYPEDATA c "
                + "                 WHERE c.datatype = 'Sex' AND c.datacode = a.sex) AS sex, "
                + "               a.sex sexcode, a.technicalname, a.levelname, a.managername, "
                + "               (SELECT technicaltypename "
                + "                  FROM GDS_ATT_TECHNICAL b, GDS_ATT_TECHNICALTYPE c "
                + "                 WHERE c.technicaltypecode = b.technicaltype "
                + "                   AND b.technicalcode = a.technicalcode) AS technicaltype, "
                + "               (SELECT costcode "
                + "                  FROM GDS_SC_DEPARTMENT b "
                + "                 WHERE b.depcode = a.depcode) costcode, a.technicalcode, "
                + "               a.depcode, a.dcode, a.dname depname, a.depname sybname, "
                + "               getdepname ('2', a.depcode) syc, "
                + "               getdepname ('1', a.depcode) bgname, "
                + "               getdepname ('0', a.depcode) cbgname, "
                + "               (SELECT professionalname "
                + "                  FROM GDS_ATT_PROFESSIONAL n "
                + "                 WHERE n.professionalcode = "
                + "                                       a.professionalcode) "
                + "                                                          AS professionalname, "
                + "               ROUND (  (  MONTHS_BETWEEN (SYSDATE, a.joindate) "
                + "                         - NVL (a.deductyears, 0) "
                + "                        ) "
                + "                      / 12, "
                + "                      1 "
                + "                     ) AS comeyears, "
                + "               (SELECT (SELECT datavalue "
                + "                          FROM GDS_ATT_TYPEDATA b "
                + "                         WHERE b.datatype = 'AssessLevel' "
                + "                           AND e.asseslevel = b.datacode) "
                + "                  FROM GDS_ATT_EMPASSESS e "
                + "                 WHERE e.workno = a.workno "
                + "                   AND e.assesdate = (SELECT MAX (assesdate) "
                + "                                        FROM GDS_ATT_EMPASSESS w "
                + "                                       WHERE w.workno = e.workno) "
                + "                   AND ROWNUM <= 1) AS asseslevel, "
                + "               (SELECT leveltype "
                + "                  FROM GDS_ATT_LEVEL j "
                + "                 WHERE j.levelcode = a.levelcode) AS leveltype, "
                + "               TO_CHAR (a.joindate, 'yyyy/mm/dd') AS joindate, a.overtimetype, "
                + "               (SELECT datavalue "
                + "                  FROM GDS_ATT_TYPEDATA t "
                + "                 WHERE t.datatype = 'OverTimeType' "
                + "                   AND t.datacode = a.overtimetype) AS overtimetypename "
                + "          FROM gds_att_employee a " + condition;
            ;
            #endregion
            DataTable dt = DalHelper.ExecuteQuery(SQL);
            return dt;
        }
        public DataTable GetDataByCondition(string condition)
        {
            string SQL
                = "SELECT * "
                + "  FROM (SELECT a.*, "
                + "               (SELECT datavalue "
                + "                  FROM GDS_ATT_TYPEDATA b "
                + "                 WHERE b.datatype = 'LeaveSex' "
                + "                   AND b.datacode = a.fitsex) fitsexname "
                + "          FROM GDS_ATT_LEAVETYPE a " + condition + ") "
                ;
            DataTable dt = DalHelper.ExecuteQuery(SQL);
            return dt;
        }
        public string GetYearLeaveDays(string EmployeeNo, string ReportYear, string ApplyDate)
        {
            try
            {
                string StandardDays = "";
                string LeaveDays = "";
                string AlreadyDays = "";
                string ReachLeaveDays = "";
                string ThisJoinYear = "";

                OracleParameter[] param = new OracleParameter[] { 
                new OracleParameter("v_empno", OracleType.VarChar, 10, ParameterDirection.Input, false, 1, 0xff, null, DataRowVersion.Current, EmployeeNo), 
                new OracleParameter("v_reportyear", OracleType.VarChar, 4, ParameterDirection.Input, false, 1, 0xff, null, DataRowVersion.Current, ReportYear), 

                new OracleParameter("v_applydate", OracleType.VarChar, 10, ParameterDirection.Input, false, 1, 0xff, null, DataRowVersion.Current, ApplyDate), 
                new OracleParameter("v_standarddays", OracleType.Double, 15, ParameterDirection.Output, false, 1, 0xff, null, DataRowVersion.Current, 0), 

                new OracleParameter("v_yearleavedays", OracleType.Double, 15, ParameterDirection.Output, false, 1, 0xff, null, DataRowVersion.Current, 0), 
                new OracleParameter("v_alreaddays", OracleType.Double, 15, ParameterDirection.Output, false, 1, 0xff, null, DataRowVersion.Current, 0), 

                new OracleParameter("v_reachleavedays", OracleType.Double, 15, ParameterDirection.Output, false, 1, 0xff, null, DataRowVersion.Current, 0),
                new OracleParameter("v_thisjoinyear", OracleType.VarChar, 10, ParameterDirection.Output, false, 1, 0xff, null, DataRowVersion.Current, 0) };

                DataTable list = DalHelper.ExecuteQuery("PROG_GETYEARLEAVEDAYS", CommandType.StoredProcedure, param);

                StandardDays = list.Rows[3][0].ToString();
                LeaveDays = list.Rows[4][0].ToString();
                AlreadyDays = list.Rows[5][0].ToString();
                ReachLeaveDays = list.Rows[6][0].ToString();
                ThisJoinYear = list.Rows[7][0].ToString();

                return (StandardDays + "|" + LeaveDays + "|" + AlreadyDays + "|" + ReachLeaveDays + "|" + ThisJoinYear);
            }
            catch (Exception)
            {
                return "0|0|0|0|0";
            }
        }
        public string GetValue(string sql)
        {
            try
            {
                string sValue = "";
                DataTable dt = DalHelper.ExecuteQuery(sql);
                if (dt.Rows.Count > 0)
                {
                    sValue = dt.Rows[0][0].ToString().Trim();
                }
                return sValue;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string GetSex(string sEmpNo)
        {
            string result = "";
            string SQL = "SELECT Sex FROM gds_att_employee WHERE WorkNo='" + sEmpNo + "'";
            result = Convert.ToString(DalHelper.ExecuteScalar(SQL));
            return result;
        }

        public bool SaveDisLeaveAuditData(string BillNo, string AuditMan, string ApRemark, SynclogModel logmodel)
        {
            bool bResult = false;
            OracleCommand command = new OracleCommand();
            command.Connection = DalHelper.Connection;
            bool ConnectionOpenHere = false;
            try
            {

                if (command.Connection.State == ConnectionState.Closed)
                {
                    command.Connection.Open();
                    ConnectionOpenHere = true;
                }
                command.Transaction = command.Connection.BeginTransaction();
                command.CommandText = "select count(1)  from  GDS_ATT_LEAVEAPPLY  where id='" + BillNo + "' and status='0' and ProxyStatus='1'";
                if (Convert.ToDecimal(command.ExecuteScalar()) > 0M)
                {
                    command.CommandText = "UPDATE GDS_ATT_LEAVEAPPLY SET Status='3',ProxyStatus='3',Approver='" + AuditMan + "',ApproveDate=sysdate,ApRemark='" + ApRemark + "' WHERE ID='" + BillNo + "'";
                    command.ExecuteNonQuery();
                    SaveLogData("U", BillNo, command.CommandText, command, logmodel);
                    command.CommandText = "UPDATE GDS_WF_NOTESREMIND SET AuditStatus='2',Remark='" + ApRemark + "' WHERE BillNo='" + BillNo + "' and WorkNo='" + AuditMan + "'";
                    command.ExecuteNonQuery();
                    SaveLogData("U", BillNo, command.CommandText, command, logmodel);
                    command.CommandText = "Select BillTypeName From GDS_WF_BILLTYPE Where BillTypeCode='KQMLeaveApply'";
                    string BillTypeName = Convert.ToString(command.ExecuteScalar());
                    command.CommandText = "SELECT nvl(MAX (ParaValue),'')  FROM GDS_SC_PARAMETER WHERE ParaName ='WFMDisRemindSubject'";
                    string Content = BillTypeName + Convert.ToString(command.ExecuteScalar());
                    command.CommandText = "insert into GDS_WF_NOTESREMIND(Content,WorkNo,ApplyMan,ApplyDate,BillNo,SendNotes,Auditstatus,Remark,RemindType,Subject) SELECT '" + BillTypeName + "', WorkNo, '" + AuditMan + "',sysdate,'" + BillNo + "','N','0','" + ApRemark + "','A','" + Content + "'  FROM GDS_ATT_LEAVEAPPLY WHERE id='" + BillNo + "' and Status='1'";
                    command.ExecuteNonQuery();
                }
                command.Transaction.Commit();
                command.Transaction = null;
                bResult = true;
            }
            catch (Exception ex)
            {
                if (command.Transaction != null)
                {
                    command.Transaction.Rollback();
                    command.Transaction = null;
                    return false;
                }
                throw ex;
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
        public bool SaveZBLHLeaveAuditData(string BillNo, string AuditMan, string ApRemark,  string Flow_LevelRemark, SynclogModel logmodel)
        {
            bool sValue = true;
          
            string SQL = "";
            DataTable dt = new DataTable();
            try
            {
                string OrgCode = "";
                string AuditOrgCode = "";
                string BillTypeCode = "D002";
                string LVTYPECODE="";
                string STARTDATE="";
                string ENDDATE="";
                string WorkNo = "";
                string REASON="";
                string LVTOTALDAYS = "";
                //SQL = "select a.WorkNo,a.LVTotal,a.LVTypeCode,a.REASON,b.LevelCode,b.ManagerCode,a.LVTYPECODE,a.STARTDATE,a.STARTTIME,a.ENDDATE,a.ENDTIME,b.DCode  from  GDS_ATT_LEAVEAPPLY a,GDS_ATT_EMPLOYEE b  where a.WorkNo=b.WorkNo and a.id='" + BillNo + "' and a.status='0' and a.ProxyStatus='1'";
                SQL = "SELECT WORKNO, LVTOTAL, LVTYPECODE, REASON, LEVELCODE, "
                    + "       MANAGERCODE,STARTDATE, STARTTIME, ENDDATE, "
                    + "       ENDTIME,DCODE,LVTOTALDAYS "
                    + "  FROM gds_att_leaveapply_v "
                    + " WHERE  ID = '" + BillNo + "' "
                    + "   AND status = '0' "
                    + "   AND proxystatus = '1' "
                    ;
                
                dt = DalHelper.ExecuteQuery(SQL);

                if (dt.Rows.Count > 0)
                {
                    WorkNo = Convert.ToString(dt.Rows[0]["WORKNO"]);
                    OrgCode = Convert.ToString(dt.Rows[0]["DCODE"]);
                    LVTOTALDAYS = Convert.ToString(dt.Rows[0]["LVTOTALDAYS"]);

                    LVTYPECODE =dt.Rows[0]["LVTYPECODE"].ToString();
                    STARTDATE =Convert.ToDateTime(dt.Rows[0]["STARTDATE"]).ToString("yyyy/MM/dd")+" "+Convert.ToDateTime(dt.Rows[0]["STARTTIME"]).ToString("HH:mm");
                    ENDDATE = Convert.ToDateTime(dt.Rows[0]["ENDDATE"]).ToString("yyyy/MM/dd") + " " + Convert.ToDateTime(dt.Rows[0]["ENDTIME"]).ToString("HH:mm");
                    REASON=dt.Rows[0]["REASON"].ToString() ;
                    
                    #region 原始code
                    //SQL = "select billtypecode from GDS_ATT_LEAVETYPE where lvtypecode='" + LVTypeCode + "'";
                    //if (Convert.ToString(DalHelper.ExecuteScalar(SQL)).Trim() == "")
                    //{
                    //    SQL = "select * from GDS_WF_APPTYPETOBILLCONFIG where applytype='kqmleave' and appvalue='" + LVTypeCode + "' order by cmptype asc";
                    //    dt = DalHelper.ExecuteQuery(SQL);
                    //    if (dt.Rows.Count > 0)
                    //    {
                    //        foreach (DataRow newRow in dt.Rows)
                    //        {
                    //            appValue = newRow["AppValue"].ToString();
                    //            cmpType = newRow["CmpType"].ToString();
                    //            capValue = newRow["CapValue"].ToString();
                    //            minDays = Convert.ToDouble(newRow["MinDays"].ToString().Trim());
                    //            maxDays = Convert.ToDouble(newRow["MaxDays"].ToString().Trim());
                    //            if (cmpType.Trim() == "hrmmgr")
                    //            {
                    //                if (((LVTotal > (minDays * 8.0)) && (LVTotal <= (maxDays * 8.0))) && (capValue.Trim() != ""))
                    //                {
                    //                    temVal = capValue.Trim().Replace("，", ",").Split(new char[] { ',' });
                    //                    Array.Sort<string>(temVal);
                    //                    if (Array.BinarySearch<string>(temVal, ManagerCode) > 0)
                    //                    {
                    //                        BillTypeCode = newRow["billType"].ToString().Trim();
                    //                        isFlag = true;
                    //                    }
                    //                }
                    //            }
                    //            else if (cmpType.Trim() == "hrmlevel")
                    //            {
                    //                if (((LVTotal > (minDays * 8.0)) && (LVTotal <= (maxDays * 8.0))) && (capValue.Trim() != ""))
                    //                {
                    //                    temVal = capValue.Trim().Replace("，", ",").Split(new char[] { ',' });
                    //                    Array.Sort<string>(temVal);
                    //                    if (Array.BinarySearch<string>(temVal, LevelCode) > 0)
                    //                    {
                    //                        BillTypeCode = newRow["billType"].ToString().Trim();
                    //                        isFlag = true;
                    //                    }
                    //                }
                    //            }
                    //            else if ((cmpType.Trim() == "") && ((LVTotal > (minDays * 8.0)) && (LVTotal <= (maxDays * 8.0))))
                    //            {
                    //                BillTypeCode = newRow["billType"].ToString().Trim();
                    //                isFlag = true;
                    //            }
                    //            if (isFlag)
                    //            {
                    //                break;
                    //            }
                    //        }
                    //    }
                    //}
                    #endregion
                    string cmdText = "gds_sc_getAuditType";
                    OracleParameter outCursor = new OracleParameter("p_coursor", OracleType.Cursor);
                    outCursor.Direction = ParameterDirection.Output;
                    DataTable dt_getAuditType = DalHelper.ExecuteQuery(cmdText, CommandType.StoredProcedure, new OracleParameter("p_startdate", STARTDATE), new OracleParameter("p_enddate", ENDDATE), new OracleParameter("p_empno", WorkNo), new OracleParameter("p_lvtotaldays", LVTOTALDAYS), outCursor);
                    if (dt_getAuditType != null && dt_getAuditType.Rows.Count > 0)
                    {
                        string[] type = dt_getAuditType.Rows[0][0].ToString().Split('^');
                        cmdText = @"SELECT depcode FROM (SELECT   b.depcode, a.orderid FROM gds_wf_flowset a, (SELECT 
                                             LEVEL orderid, depcode FROM gds_sc_department  START WITH depcode =:p_DepCode CONNECT BY 
                                             PRIOR parentdepcode = depcode   ORDER BY LEVEL) b  WHERE a.deptcode = b.depcode  AND
                                             a.formtype =:p_FORMTYPE and a.REASON1='" + type[0] + "' and a.REASON2='" + type[1] + "' and a.REASON3='" + type[2] + "' and a.REASON4='" + LVTYPECODE + "' ORDER BY orderid) WHERE ROWNUM <= 1";
                        DataTable dt_ZBLHDal = DalHelper.ExecuteQuery(cmdText, new OracleParameter(":p_DepCode", OrgCode), new OracleParameter(":p_FORMTYPE", BillTypeCode));
                        if (dt_ZBLHDal != null && dt_ZBLHDal.Rows.Count > 0)
                        {
                            AuditOrgCode = dt_ZBLHDal.Rows[0]["depcode"].ToString();
                        }
                    }
                    OracleConnection OracleString= DalHelper.Connection;

                    if (AuditOrgCode.Length > 0)
                    {
                        KQMLeaveApplyForm_ZBLHDal leaveApply = new KQMLeaveApplyForm_ZBLHDal();
                        //sValue = leaveApply.SaveAuditData(OracleString, Flow_LevelRemark, BillNo, "KQL", AuditOrgCode, BillTypeCode, AuditMan, REASON, WorkNo, STARTDATE, ENDDATE, LVTYPECODE, logmodel);
                        //郵于是代理人簽核后才發起，所以該處應傳單據所屬人為申請人，而不是登錄人。
                        sValue = leaveApply.SaveAuditData(OracleString, Flow_LevelRemark, BillNo, "KQL", AuditOrgCode, BillTypeCode, WorkNo, REASON, WorkNo, STARTDATE, ENDDATE, LVTYPECODE, logmodel);
                        if (sValue)
                        {
                            int iResult = DalHelper.ExecuteNonQuery("UPDATE GDS_ATT_LEAVEAPPLY SET ProxyStatus='2',Status='1',Approver='" + AuditMan + "',ApproveDate=sysdate,ApRemark='" + ApRemark + "' WHERE id='" + BillNo + "'", logmodel);
                            if (iResult > 0)
                            {
                                sValue = true;
                            }
                        }

                        //SaveLogData("I", BillNo, command.CommandText, command, logmodel);
                    }
                    else
                    {
                        sValue = false;
                    }

                }

            }
            catch (Exception ex)
            {

                throw ex;
            }

            return sValue;
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

                command.CommandText = "Insert into GDS_SC_SYNCLOG " +
                "   (transactiontype,levelno,FromHost,ToHost,docno,text,logtime,processflag,processowner) " +
                " values('" + logmodel.TransactionType + "','2','" + logmodel.FromHost + "','" + logmodel.ToHost + "','" + DocNo + "','" + LogText.Replace("'", "''") + "',sysdate,'" + ProcessFlag + "','" + logmodel.ProcessOwner + "')";
                command.ExecuteNonQuery();
            }
            catch (Exception)
            {
            }

        }
    }
}