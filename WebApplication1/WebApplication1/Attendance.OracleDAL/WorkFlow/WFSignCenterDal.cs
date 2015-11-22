using System;
using System.Collections.Generic;
using System.Text;
using GDSBG.MiABU.Attendance.IDAL.WorkFlow;
using GDSBG.MiABU.Attendance.Model.WorkFlow;
using System.Data.OracleClient;
using System.Data;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;
using GDSBG.MiABU.Attendance.OracleDAL.SystemManage.SystemSafety;

namespace GDSBG.MiABU.Attendance.OracleDAL.WorkFlow
{
    public class WFSignCenterDal : DALBase<OverTimeModel>, IWFSignCenterDal
    {

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
                //int i = DalHelper.ExecuteNonQuery(sql);

            }
            catch (Exception)
            {
            }

        }

        public void SaveBatchAuditData(string BillNo, string AuditMan, string BillTypeCode, SynclogModel logmodel)
        {


            OracleCommand command = new OracleCommand();
            command.Connection = DalHelper.Connection;
            command.Connection.Open();
            OracleTransaction trans = command.Connection.BeginTransaction();
            command.Transaction = trans;
            try
            {

                command.CommandText = "update GDS_ATT_AUDITSTATUS a set AuditStatus='1',AuditDate=sysdate where BillNo='" + BillNo + "' and AuditMan='" + AuditMan + "' and AuditStatus='0' and a.OrderNo=(select nvl(min(e.OrderNo),'-1') from GDS_ATT_AUDITSTATUS e where e.BillNo=a.BillNo and e.Auditstatus='0') ";
                command.ExecuteNonQuery();
                SaveLogData("U", "", command.CommandText, command);
                // command.CommandText = "SELECT count(1) FROM GDS_ATT_AUDITSTATUS WHERE BillNo='" + BillNo + "' and AuditStatus='0'";

                command.CommandText = "update GDS_ATT_BILL set Status='1' where BillNo='" + BillNo + "' and Status='0'";
                command.ExecuteNonQuery();
                SaveLogData("U", "", command.CommandText, command,logmodel);
                switch (BillTypeCode)
                {
                    case "OTMAdvanceApply":
                        command.CommandText = "update GDS_ATT_ADVANCEAPPLY set Status='2',Approver='" + AuditMan + "',ApproveDate=sysdate,ApRemark='' where BillNo='" + BillNo + "' and Status='1'";
                        command.ExecuteNonQuery();
                        SaveLogData("U", "", command.CommandText, command);
                        command.CommandText = "update GDS_ATT_ADVANCEAPPLY set Approver='" + AuditMan + "',ApproveDate=sysdate,ApRemark='' where BillNo='" + BillNo + "' and Status='1'";
                        command.ExecuteNonQuery();
                        SaveLogData("U", "", command.CommandText, command,logmodel);
                        break;
                }
                //switch (BillTypeCode)
                //{
                //    case "OTMAdvanceApply":

                //        break;
                //}

                trans.Commit();
                command.Connection.Close();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                command.Connection.Close();
                throw ex;
            }

        }

        public DataSet GetAuditCenterDataByCondition(string condition)
        {
            try
            {
                DataSet ds = new DataSet();
                //    string sql
                //= "SELECT * " +
                //    "  FROM (SELECT a.*, " +
                //    "               decode(substr(a.BILLNO,0,2),'OT','加班單','') billtypename, " +
                //    "               (SELECT depname " +
                //    "                  FROM gds_sc_department c " +
                //    "                 WHERE c.depcode = a.orgcode) orgname, " +
                //    "               (SELECT datavalue " +
                //    "                  FROM gds_att_typedata c " +
                //    "                 WHERE c.datatype = 'WFMBillStatus' AND c.datacode = a.status) " +
                //    "                                                                   statusname, " +
                //    "               (SELECT datavalue " +
                //    "                  FROM gds_att_typedata c " +
                //    "                 WHERE c.datatype = 'AuditTypeMove' " +
                //    "                   AND c.datacode = b.audittype) audittypename, " +
                //    "               NVL ((SELECT e.DEPCODE " +
                //    "                       FROM gds_att_employees e " +
                //    "                      WHERE e.workno = b.auditman AND e.status = '0'), " +
                //    "                    (SELECT DEPCODE " +
                //    "                       FROM gds_att_employees e " +
                //    "                      WHERE e.workno = b.auditman)) depname, " +
                //    "               (SELECT localname " +
                //    "                  FROM gds_att_employees e " +
                //    "                 WHERE e.workno = a.applyman) applymanname " +
                //    "          FROM gds_att_bill a, gds_att_auditstatus b " +
                //    "         WHERE a.billno = b.billno " + condition + ") ";
                //    DataTable dt = DalHelper.ExecuteQuery(sql);

                DataTable dt = DalHelper.ExecuteQuery(@"SELECT * FROM (SELECT a.*, (CASE WHEN (SELECT billtypecode  FROM gds_wf_billtype 
                                            c WHERE c.billtypeno = SUBSTR (a.billno, 0, 3)) ='KQMLeaveApply' THEN (SELECT billtypename
                                            FROM gds_wf_billtype c WHERE c.billtypeno = SUBSTR (a.billno, 0, 3))|| '-'||(SELECT lvtypename
                                            FROM gds_att_leavetype c WHERE c.lvtypecode =(SELECT lvtypecode FROM gds_att_leaveapply d WHERE 
                                            d.billno = a.billno AND ROWNUM = 1))ELSE (SELECT billtypename FROM gds_wf_billtype c  WHERE 
                                            c.billtypecode = a.billtypecode) END) billtypename,(SELECT depname FROM gds_sc_department c WHERE
                                            c.depcode = a.orgcode) orgname,(SELECT datavalue FROM gds_att_typedata c WHERE c.datatype = 'WFMBillStatus'
                                            AND c.datacode = a.status) statusname,(SELECT datavalue
                                            FROM gds_att_typedata c WHERE c.datatype = 'AuditTypeMove' AND c.datacode = b.audittype) audittypename,
                                            NVL ((SELECT e.depname FROM gds_att_employee e WHERE e.workno = b.auditman AND e.status = '0'),
                                            (SELECT depcode FROM gds_att_employees e WHERE e.workno = b.auditman)) depname, (SELECT localname
                                            FROM gds_att_employee e WHERE e.workno = a.applyman) applymanname  FROM gds_att_bill a, gds_att_auditstatus b
                                            WHERE a.billno = b.billno " + condition + ")");
                if (dt != null)
                {
                    dt.TableName = "WFM_Bill";
                }
                ds.Tables.Add(dt);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public string GetAllAuditDept(string sDepCode)
        {
            string sRValue = "";
            string sql = "";
            // bool ConnectionOpenHere = false;
            try
            {
                // sql = "select * from (select (depnamelevel2||case when depnamelevel3 is not null then '/'||depnamelevel3 end  || case when depnamelevel4 is not null then '/'||depnamelevel4 end || case when depnamelevel5 is not null then '/'||depnamelevel5 end || case when depnamelevel6 is not null then '/'||depnamelevel6 end || case when depnamelevel7 is not null then '/'||depnamelevel7 end || case when depnamelevel8 is not null then '/'||depnamelevel8 end || case when depnamelevel9 is not null then '/'||depnamelevel9 end || case when depnamelevel10 is not null then '/'||depnamelevel10 end || case when depnamelevel11 is not null then '/'||depnamelevel11 end) depname from hrm_orgtree where depcode='" + sDepCode + "')";
                sql
                = "SELECT * " +
                    "  FROM (SELECT (   depnamelevel2 " +
                    "                || CASE " +
                    "                      WHEN depnamelevel3 IS NOT NULL " +
                    "                         THEN '/' || depnamelevel3 " +
                    "                   END " +
                    "                || CASE " +
                    "                      WHEN depnamelevel4 IS NOT NULL " +
                    "                         THEN '/' || depnamelevel4 " +
                    "                   END " +
                    "                || CASE " +
                    "                      WHEN depnamelevel5 IS NOT NULL " +
                    "                         THEN '/' || depnamelevel5 " +
                    "                   END " +
                    "                || CASE " +
                    "                      WHEN depnamelevel6 IS NOT NULL " +
                    "                         THEN '/' || depnamelevel6 " +
                    "                   END " +
                    "                || CASE " +
                    "                      WHEN depnamelevel7 IS NOT NULL " +
                    "                         THEN '/' || depnamelevel7 " +
                    "                   END " +
                    "                || CASE " +
                    "                      WHEN depnamelevel8 IS NOT NULL " +
                    "                         THEN '/' || depnamelevel8 " +
                    "                   END " +
                    "                || CASE " +
                    "                      WHEN depnamelevel9 IS NOT NULL " +
                    "                         THEN '/' || depnamelevel9 " +
                    "                   END " +
                    "                || CASE " +
                    "                      WHEN depnamelevel10 IS NOT NULL " +
                    "                         THEN '/' || depnamelevel10 " +
                    "                   END " +
                    "                || CASE " +
                    "                      WHEN depnamelevel11 IS NOT NULL " +
                    "                         THEN '/' || depnamelevel11 " +
                    "                   END " +
                    "               ) depname " +
                    "          FROM GDS_ATT_ORGTREE " +
                    "         WHERE depcode = '" + sDepCode + "') ";

                sRValue = this.GetValue(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return sRValue;
        }

        public DataSet ExcuteSQL(string sql)
        {
            try
            {
                DataSet ds = new DataSet();
                DataTable dt = DalHelper.ExecuteQuery(sql);
                if (dt != null)
                {
                    ds.Tables.Add(dt);
                }

                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        public void SaveDisAuditData(string BillNo, string AuditMan, string BillTypeCode, string ApRemark, List<string> DisSignRamark, List<string> Id, SynclogModel logmodel)
        {
            OracleCommand command = new OracleCommand();
            command.Connection = DalHelper.Connection;
            command.Connection.Open();
            OracleTransaction trans = command.Connection.BeginTransaction();
            command.Transaction = trans;
            try
            {

                string BillTypeName = "";
                command.CommandText = "SELECT nvl(MAX (ParaValue),'')  FROM GDS_SC_PARAMETER WHERE ParaName ='WFMDisRemindSubject'";
                string Content = BillTypeName + command.ExecuteScalar().ToString();
                command.CommandText = "update GDS_ATT_AUDITSTATUS a set AuditStatus='2',AuditDate=sysdate, Remark='" + ApRemark + "' where BillNo='" + BillNo + "' and AuditMan='" + AuditMan + "' and AuditStatus='0' and a.OrderNo=(select nvl(min(e.OrderNo),'-1') from GDS_ATT_AUDITSTATUS e where e.BillNo=a.BillNo and e.Auditstatus='0')";
                command.ExecuteNonQuery();
                SaveLogData("U", "", command.CommandText, command,logmodel);
                command.CommandText = "update GDS_ATT_Bill set Status='2',Remark='" + ApRemark + "' where BillNo='" + BillNo + "' and Status='0'";
                command.ExecuteNonQuery();
                SaveLogData("U", "", command.CommandText, command,logmodel);
                ApRemark = ApRemark.Replace("|", "");
                switch (BillTypeCode)
                {
                    case "D001":
                    case "OTMProjectApply":
                        command.CommandText = "insert into GDS_WF_NOTESREMIND(Content,WorkNo,ApplyMan,ApplyDate,BillNo,SendNotes,Auditstatus,Remark,RemindType,Subject) SELECT '" + BillTypeName + "', WorkNo, '" + AuditMan + "',sysdate,'" + BillNo + "','N','0','" + ApRemark + "','A','" + Content + "'  FROM GDS_ATT_ADVANCEAPPLY WHERE BillNo='" + BillNo + "' and Status='1'";
                        command.ExecuteNonQuery();
                        command.CommandText = "insert into GDS_WF_NOTESREMIND(Content,WorkNo,ApplyMan,ApplyDate,BillNo,SendNotes,Auditstatus,Remark,RemindType,Subject) SELECT '" + BillTypeName + "', ApplyMan, '" + AuditMan + "',sysdate,'" + BillNo + "','N','0','" + ApRemark + "','D','" + Content + "'  FROM GDS_ATT_BILL WHERE BillNo='" + BillNo + "'";
                        command.ExecuteNonQuery();
                        command.CommandText = "update GDS_ATT_ADVANCEAPPLY set Status='3',Approver='" + AuditMan + "',ApproveDate=sysdate,ApRemark='" + ApRemark + "' where BillNo='" + BillNo + "' and Status='1'";
                        command.ExecuteNonQuery();

                        for (int i = 0; i < DisSignRamark.Count; i++)
                        {
                            command.CommandText = "UPDATE gds_att_advanceapply  SET dissignrmark = '" + DisSignRamark[i] + "' WHERE ID = '" + Id[i] + "'";
                            command.ExecuteNonQuery();

                            //發送郵件
                            command.CommandText = "select  workno from gds_att_advanceapply where ID = '" + Id[i] + "'";
                            string aplempno = command.ExecuteOracleScalar().ToString();
                            SendMail(aplempno, AuditMan, BillTypeCode == "D001" ? "加班單" : "專案加班單", BillNo, DisSignRamark[i], command);
                        }
                        SaveLogData("U", "", command.CommandText, command,logmodel);
                        break;
                    case "KQMApplyOut":
                        command.CommandText = "insert into GDS_WF_NOTESREMIND(Content,WorkNo,ApplyMan,ApplyDate,BillNo,SendNotes,Auditstatus,Remark,RemindType,Subject) SELECT '" + BillTypeName + "', WorkNo, '" + AuditMan + "',sysdate,'" + BillNo + "','N','0','" + ApRemark + "','A','" + Content + "'  FROM GDS_ATT_APPLYOUT WHERE BillNo='" + BillNo + "' and Status='1'";
                        command.ExecuteNonQuery();
                        command.CommandText = "insert into GDS_WF_NOTESREMIND(Content,WorkNo,ApplyMan,ApplyDate,BillNo,SendNotes,Auditstatus,Remark,RemindType,Subject) SELECT '" + BillTypeName + "', ApplyMan, '" + AuditMan + "',sysdate,'" + BillNo + "','N','0','" + ApRemark + "','D','" + Content + "'  FROM GDS_ATT_BILL WHERE BillNo='" + BillNo + "'";
                        command.ExecuteNonQuery();
                        command.CommandText = "update GDS_ATT_APPLYOUT set Status='3',AUDITER='" + AuditMan + "',AUDITDATE=sysdate,AUDITIDEA='" + ApRemark + "' where BillNo='" + BillNo + "' and Status='1'";
                        command.ExecuteNonQuery();
                        for (int i = 0; i < DisSignRamark.Count; i++)
                        {
                            command.CommandText = "UPDATE GDS_ATT_APPLYOUT  SET dissignrmark = '" + DisSignRamark[i] + "' WHERE ID = '" + Id[i] + "'";
                            command.ExecuteNonQuery();
                            //發送郵件
                            command.CommandText = "select  workno from GDS_ATT_APPLYOUT where ID = '" + Id[i] + "'";
                            string aplempno = command.ExecuteOracleScalar().ToString();
                            SendMail(aplempno, AuditMan, "外出申請單", BillNo, DisSignRamark[i], command);
                        }
                        SaveLogData("U", "", command.CommandText, command,logmodel);
                        break;
                    case "D002":
                        command.CommandText = "insert into GDS_WF_NOTESREMIND(Content,WorkNo,ApplyMan,ApplyDate,BillNo,SendNotes,Auditstatus,Remark,RemindType,Subject) SELECT '" + BillTypeName + "', WorkNo, '" + AuditMan + "',sysdate,'" + BillNo + "','N','0','" + ApRemark + "','A','" + Content + "'  FROM GDS_ATT_LEAVEAPPLY  WHERE BillNo='" + BillNo + "' and Status='1'";
                        command.ExecuteNonQuery();
                        command.CommandText = "insert into GDS_WF_NOTESREMIND(Content,WorkNo,ApplyMan,ApplyDate,BillNo,SendNotes,Auditstatus,Remark,RemindType,Subject) SELECT '" + BillTypeName + "', ApplyMan, '" + AuditMan + "',sysdate,'" + BillNo + "','N','0','" + ApRemark + "','D','" + Content + "'  FROM GDS_ATT_BILL WHERE BillNo='" + BillNo + "'";
                        command.ExecuteNonQuery();
                        command.CommandText = "update GDS_ATT_LEAVEAPPLY set Status='3',APPROVER='" + AuditMan + "',APPROVEDATE=sysdate,APREMARK='" + ApRemark + "' where BillNo='" + BillNo + "' and Status='1'";
                        command.ExecuteNonQuery();
                        for (int i = 0; i < DisSignRamark.Count; i++)
                        {
                            command.CommandText = "UPDATE GDS_ATT_LEAVEAPPLY  SET dissignrmark = '" + DisSignRamark[i] + "' WHERE ID = '" + Id[i] + "'";
                            command.ExecuteNonQuery();

                            //發送郵件
                            command.CommandText = "select  workno from gds_att_leaveapply where ID = '" + Id[i] + "'";
                            string aplempno = command.ExecuteOracleScalar().ToString();
                            SendMail(aplempno, AuditMan, "請假申請單", BillNo, DisSignRamark[i], command);
                        }
                        SaveLogData("U", "", command.CommandText, command,logmodel);
                        break;
                    case "KQMException"://異常處理單
                        command.CommandText = "insert into GDS_WF_NOTESREMIND(Content,WorkNo,ApplyMan,ApplyDate,BillNo,SendNotes,Auditstatus,Remark,RemindType,Subject) SELECT '" + BillTypeName + "', WorkNo, '" + AuditMan + "',sysdate,'" + BillNo + "','N','0','" + ApRemark + "','A','" + Content + "'  FROM GDS_ATT_KAOQINDATA  WHERE BillNo='" + BillNo + "' and Status='1'";
                        command.ExecuteNonQuery();
                        command.CommandText = "insert into GDS_WF_NOTESREMIND(Content,WorkNo,ApplyMan,ApplyDate,BillNo,SendNotes,Auditstatus,Remark,RemindType,Subject) SELECT '" + BillTypeName + "', ApplyMan, '" + AuditMan + "',sysdate,'" + BillNo + "','N','0','" + ApRemark + "','D','" + Content + "'  FROM GDS_ATT_BILL WHERE BillNo='" + BillNo + "'";
                        command.ExecuteNonQuery();
                        command.CommandText = "update GDS_ATT_KAOQINDATA set Status='3',APPROVER='" + AuditMan + "',APPROVEDATE=sysdate,APREMARK='" + ApRemark + "' where BillNo='" + BillNo + "' and Status='1'";
                        command.ExecuteNonQuery();
                        for (int i = 0; i < DisSignRamark.Count; i++)
                        {
                            command.CommandText = "UPDATE GDS_ATT_KAOQINDATA  SET dissignrmark = '" + DisSignRamark[i] + "' WHERE ID = '" + Id[i] + "'";
                            command.ExecuteNonQuery();
                            //發送郵件
                            command.CommandText = "select  workno from GDS_ATT_KAOQINDATA where ID = '" + Id[i] + "'";
                            string aplempno = command.ExecuteOracleScalar().ToString();
                            SendMail(aplempno, AuditMan, "異常處理單", BillNo, DisSignRamark[i], command);
                        }
                        SaveLogData("U", "", command.CommandText, command,logmodel);
                        break;
                    case "KQMMakeup"://未刷補卡
                        command.CommandText = "insert into GDS_WF_NOTESREMIND(Content,WorkNo,ApplyMan,ApplyDate,BillNo,SendNotes,Auditstatus,Remark,RemindType,Subject) SELECT '" + BillTypeName + "', WorkNo, '" + AuditMan + "',sysdate,'" + BillNo + "','N','0','" + ApRemark + "','A','" + Content + "'  FROM gds_att_makeup  WHERE BillNo='" + BillNo + "' and Status='1'";
                        command.ExecuteNonQuery();
                        command.CommandText = "insert into GDS_WF_NOTESREMIND(Content,WorkNo,ApplyMan,ApplyDate,BillNo,SendNotes,Auditstatus,Remark,RemindType,Subject) SELECT '" + BillTypeName + "', ApplyMan, '" + AuditMan + "',sysdate,'" + BillNo + "','N','0','" + ApRemark + "','D','" + Content + "'  FROM GDS_ATT_BILL WHERE BillNo='" + BillNo + "'";
                        command.ExecuteNonQuery();
                        command.CommandText = "update gds_att_makeup set Status='3',APPROVER='" + AuditMan + "',APPROVEDATE=sysdate,APREMARK='" + ApRemark + "' where BillNo='" + BillNo + "' and Status='1'";  //0-未核准;1-簽核中;2-已核准;3-拒簽
                        command.ExecuteNonQuery();
                        for (int i = 0; i < DisSignRamark.Count; i++)
                        {
                            command.CommandText = "UPDATE gds_att_makeup  SET dissignrmark = '" + DisSignRamark[i] + "' WHERE ID = '" + Id[i] + "'";
                            command.ExecuteNonQuery();
                            //發送郵件
                            command.CommandText = "select  workno from gds_att_makeup where ID = '" + Id[i] + "'";
                            string aplempno = command.ExecuteOracleScalar().ToString();
                            SendMail(aplempno, AuditMan, "未刷補卡", BillNo, DisSignRamark[i], command);
                        }
                        SaveLogData("U", "", command.CommandText, command,logmodel);
                        break;

                    case "KQMOTMA"://免卡人員加班
                        command.CommandText = "insert into GDS_WF_NOTESREMIND(Content,WorkNo,ApplyMan,ApplyDate,BillNo,SendNotes,Auditstatus,Remark,RemindType,Subject) SELECT '" + BillTypeName + "', WorkNo, '" + AuditMan + "',sysdate,'" + BillNo + "','N','0','" + ApRemark + "','A','" + Content + "'  FROM gds_att_Activity  WHERE BillNo='" + BillNo + "' and Status='1'";
                        command.ExecuteNonQuery();
                        command.CommandText = "insert into GDS_WF_NOTESREMIND(Content,WorkNo,ApplyMan,ApplyDate,BillNo,SendNotes,Auditstatus,Remark,RemindType,Subject) SELECT '" + BillTypeName + "', ApplyMan, '" + AuditMan + "',sysdate,'" + BillNo + "','N','0','" + ApRemark + "','D','" + Content + "'  FROM GDS_ATT_BILL WHERE BillNo='" + BillNo + "'";
                        command.ExecuteNonQuery();
                        command.CommandText = "update gds_att_Activity set Status='3',APPROVER='" + AuditMan + "',APPROVEDATE=sysdate,APREMARK='" + ApRemark + "' where BillNo='" + BillNo + "' and Status='1'";    //0-未核准;1-簽核中;2-已核准;3-拒簽
                        command.ExecuteNonQuery();
                        for (int i = 0; i < DisSignRamark.Count; i++)
                        {
                            command.CommandText = "UPDATE gds_att_Activity  SET DISSIGNRMARK = '" + DisSignRamark[i] + "' WHERE ID = '" + Id[i] + "'";
                            command.ExecuteNonQuery();
                            //發送郵件
                            command.CommandText = "select  workno from gds_att_Activity where ID = '" + Id[i] + "'";
                            string aplempno = command.ExecuteOracleScalar().ToString();
                            SendMail(aplempno, AuditMan, "免卡人員加班", BillNo, DisSignRamark[i], command);
                        }
                        SaveLogData("U", "", command.CommandText, command,logmodel);
                        break;
                    case "KQMMonthTotal":
                        command.CommandText = "insert into GDS_WF_NOTESREMIND(Content,WorkNo,ApplyMan,ApplyDate,BillNo,SendNotes,Auditstatus,Remark,RemindType,Subject) SELECT '" + BillTypeName + "', WorkNo, '" + AuditMan + "',sysdate,'" + BillNo + "','N','0','" + ApRemark + "','A','" + Content + "'  FROM GDS_ATT_APPLYOUT WHERE BillNo='" + BillNo + "' and Status='1'";
                        command.ExecuteNonQuery();
                        command.CommandText = "insert into GDS_WF_NOTESREMIND(Content,WorkNo,ApplyMan,ApplyDate,BillNo,SendNotes,Auditstatus,Remark,RemindType,Subject) SELECT '" + BillTypeName + "', ApplyMan, '" + AuditMan + "',sysdate,'" + BillNo + "','N','0','" + ApRemark + "','D','" + Content + "'  FROM GDS_ATT_BILL WHERE BillNo='" + BillNo + "'";
                        command.ExecuteNonQuery();
                        command.CommandText = "update GDS_ATT_monthtotal set opproveflag='3',APPROVER='" + AuditMan + "',APPROVEDATE=sysdate,APREMARK='" + ApRemark + "' where BillNo='" + BillNo + "' and opproveflag='1'";
                        command.ExecuteNonQuery();
                        for (int i = 0; i < DisSignRamark.Count; i++)
                        {
                            command.CommandText = "UPDATE GDS_ATT_monthtotal  SET dissignrmark = '" + DisSignRamark[i] + "' WHERE ID = '" + Id[i] + "'";
                            command.ExecuteNonQuery();
                        }
                        SaveLogData("U", "", command.CommandText, command,logmodel);
                        break;
                        
                }
                command.Transaction.Commit();
                command.Transaction = null;
            }
            catch (Exception ex)
            {
                command.Transaction.Rollback();
                command.Transaction = null;
                throw ex;
            }

        }
        #region 重載拒簽動作的方法---made by ziyan
        /// <summary>
        /// 重載拒簽動作的方法---made by ziyan 
        /// </summary>
        /// <param name="BillNo">單號</param>
        /// <param name="AuditMan">簽核人（登錄人）</param>
        /// <param name="BillTypeCode">單據類型 （月加班匯總）</param>
        /// <param name="ApRemark">簽核意見</param>
        /// <param name="DisSignRamark">拒簽意見</param>
        /// <param name="YearMonth">年月（主鍵一）</param>
        /// <param name="WorkNo">工號（主鍵二）</param>
        public void SaveDisAuditData(string BillNo, string AuditMan, string BillTypeCode, string ApRemark, List<string> DisSignRamark, List<string> WorkNo, List<string> YearMonth, SynclogModel logmodel)
        {
            OracleCommand command = new OracleCommand();
            command.Connection = DalHelper.Connection;
            command.Connection.Open();
            OracleTransaction trans = command.Connection.BeginTransaction();
            command.Transaction = trans;
            try
            {

                string BillTypeName = "";
                command.CommandText = "SELECT nvl(MAX (ParaValue),'')  FROM GDS_SC_PARAMETER WHERE ParaName ='WFMDisRemindSubject'";
                string Content = BillTypeName + command.ExecuteScalar().ToString();
                command.CommandText = "update GDS_ATT_AUDITSTATUS a set AuditStatus='2',AuditDate=sysdate, Remark='" + ApRemark + "' where BillNo='" + BillNo + "' and AuditMan='" + AuditMan + "' and AuditStatus='0' and a.OrderNo=(select nvl(min(e.OrderNo),'-1') from GDS_ATT_AUDITSTATUS e where e.BillNo=a.BillNo and e.Auditstatus='0')";
                command.ExecuteNonQuery();
                SaveLogData("U", "", command.CommandText, command);
                command.CommandText = "update GDS_ATT_Bill set Status='2',Remark='" + ApRemark + "' where BillNo='" + BillNo + "' and Status='0'";
                command.ExecuteNonQuery();
                SaveLogData("U", "", command.CommandText, command,logmodel);
                ApRemark = ApRemark.Replace("|", "");
                switch (BillTypeCode)
                {
                    case "KQMMonthTotal":
                        command.CommandText = "insert into GDS_WF_NOTESREMIND(Content,WorkNo,ApplyMan,ApplyDate,BillNo,SendNotes,Auditstatus,Remark,RemindType,Subject) SELECT '" + BillTypeName + "', WorkNo, '" + AuditMan + "',sysdate,'" + BillNo + "','N','0','" + ApRemark + "','A','" + Content + "'  FROM GDS_ATT_monthtotal WHERE BillNo='" + BillNo + "' and APPROVEFLAG='1'";
                        command.ExecuteNonQuery();
                        command.CommandText = "insert into GDS_WF_NOTESREMIND(Content,WorkNo,ApplyMan,ApplyDate,BillNo,SendNotes,Auditstatus,Remark,RemindType,Subject) SELECT '" + BillTypeName + "', ApplyMan, '" + AuditMan + "',sysdate,'" + BillNo + "','N','0','" + ApRemark + "','D','" + Content + "'  FROM GDS_ATT_BILL WHERE BillNo='" + BillNo + "'";
                        command.ExecuteNonQuery();
                        command.CommandText = "update GDS_ATT_monthtotal set APPROVEFLAG='3',APPROVER='" + AuditMan + "',APPROVEDATE=sysdate,APREMARK='" + ApRemark + "' where BillNo='" + BillNo + "' and APPROVEFLAG='1'";
                        command.ExecuteNonQuery();
                        for (int i = 0; i < DisSignRamark.Count; i++)
                        {
                            command.CommandText = "UPDATE GDS_ATT_monthtotal  SET dissignrmark = '" + DisSignRamark[i] + "' WHERE WORKNO = '" + WorkNo[i] + "'and YEARMONTH='"+YearMonth[i]+"'";
                            command.ExecuteNonQuery();
                        }
                        SaveLogData("U", "", command.CommandText, command,logmodel);
                        break;

                }
                command.Transaction.Commit();
                command.Transaction = null;
            }
            catch (Exception ex)
            {
                command.Transaction.Rollback();
                command.Transaction = null;
                throw ex;
            }

        }
        #endregion

        public void SaveAuditData(string BillNo, string AuditMan, string BillTypeCode, string ApRemark, string condition, List<string> DisSignRamark, List<string> Id, SynclogModel logmodel)
        {
            OracleCommand command = new OracleCommand();
            command.Connection = DalHelper.Connection;
            command.Connection.Open();
            OracleTransaction trans = command.Connection.BeginTransaction();
            command.Transaction = trans;
            try
            {
                string BillTypeName = string.Empty;
                command.CommandText = "SELECT nvl(MAX (ParaValue),'')  FROM GDS_SC_PARAMETER WHERE ParaName ='WFMDisRemindSubject'";
                string Content = BillTypeName + Convert.ToString(command.ExecuteScalar());
                string ApprovedApRemark = "";
                if (ApRemark.IndexOf("|") >= 0)
                {
                    ApprovedApRemark = ApRemark.Split(new char[] { '|' })[0].ToString();
                }
                ApRemark = ApRemark.Replace("|", "");
                if (ApRemark.Length > 500)
                {
                    ApRemark = ApRemark.Substring(0, 500) + "...";
                }
                command.CommandText = "update GDS_ATT_AUDITSTATUS a set AuditStatus='1',AuditDate=sysdate, Remark='" + ApRemark + "' where BillNo='" + BillNo + "' and AuditMan='" + AuditMan + "' and AuditStatus='0' and a.OrderNo=(select nvl(min(e.OrderNo),'-1') from GDS_ATT_AUDITSTATUS e where e.BillNo=a.BillNo and e.Auditstatus='0') ";
                command.ExecuteNonQuery();
                SaveLogData("U", "", command.CommandText, command,logmodel);
                command.CommandText = "SELECT count(1) FROM GDS_ATT_AUDITSTATUS WHERE BillNo='" + BillNo + "' and AuditStatus='0'";
                if (Convert.ToDecimal(command.ExecuteScalar()) == 0M)
                {
                    command.CommandText = "update GDS_ATT_BILL set Status='1', Remark='" + ApRemark + "' where BillNo='" + BillNo + "' and Status='0'";
                    command.ExecuteNonQuery();
                    SaveLogData("U", "", command.CommandText, command,logmodel);
                    switch (BillTypeCode)
                    {
                        case "D001":
                        case "OTMProjectApply":
                            if (condition.Length > 0)
                            {
                                command.CommandText = "insert into GDS_WF_NOTESREMIND(Content,WorkNo,ApplyMan,ApplyDate,BillNo,SendNotes,Auditstatus,Remark,RemindType,Subject) SELECT '" + BillTypeName + "', WorkNo, '" + AuditMan + "',sysdate,'" + BillNo + "','N','0','" + ApRemark + "','A','" + Content + "'  FROM GDS_ATT_ADVANCEAPPLY WHERE BillNo='" + BillNo + "' and Status='1' " + condition;
                                command.ExecuteNonQuery();
                                command.CommandText = "update GDS_ATT_ADVANCEAPPLY set Status='3',Approver='" + AuditMan + "',ApproveDate=sysdate,ApRemark='" + ApRemark + "' where Status='1' " + condition;
                                command.ExecuteNonQuery();
                                for (int i = 0; i < DisSignRamark.Count; i++)
                                {
                                    command.CommandText = "UPDATE gds_att_advanceapply  SET dissignrmark = '" + DisSignRamark[i] + "' WHERE ID = '" + Id[i] + "'";
                                    command.ExecuteNonQuery();

                                    //發送郵件
                                    command.CommandText = "select  workno from gds_att_advanceapply where ID = '" + Id[i] + "'";
                                    string aplempno = command.ExecuteOracleScalar().ToString();
                                    SendMail(aplempno, AuditMan, BillTypeCode == "D001" ? "加班單" : "專案加班單", BillNo, DisSignRamark[i], command);
                                }
                                SaveLogData("U", "", command.CommandText, command,logmodel);
                            }
                            command.CommandText = "update GDS_ATT_ADVANCEAPPLY set Status='2',Approver='" + AuditMan + "',ApproveDate=sysdate,ApRemark='" + ApprovedApRemark + "' where BillNo='" + BillNo + "' and Status='1'";
                            command.ExecuteNonQuery();
                            SaveLogData("U", "", command.CommandText, command,logmodel);
                            goto Label_2FB2;
                        case "KQMApplyOut":
                            if (condition.Length > 0)
                            {
                                command.CommandText = "insert into GDS_WF_NOTESREMIND(Content,WorkNo,ApplyMan,ApplyDate,BillNo,SendNotes,Auditstatus,Remark,RemindType,Subject) SELECT '" + BillTypeName + "', WorkNo, '" + AuditMan + "',sysdate,'" + BillNo + "','N','0','" + ApRemark + "','A','" + Content + "'  FROM GDS_ATT_ADVANCEAPPLY WHERE BillNo='" + BillNo + "' and Status='1' " + condition;
                                command.ExecuteNonQuery();
                                command.CommandText = "update GDS_ATT_APPLYOUT set Status='3',AUDITER='" + AuditMan + "',AUDITDATE=sysdate,AUDITIDEA='" + ApRemark + "' where Status='1' " + condition;
                                command.ExecuteNonQuery();
                                for (int i = 0; i < DisSignRamark.Count; i++)
                                {
                                    command.CommandText = "UPDATE GDS_ATT_APPLYOUT  SET dissignrmark = '" + DisSignRamark[i] + "' WHERE ID = '" + Id[i] + "'";
                                    command.ExecuteNonQuery();
                                    //發送郵件
                                    command.CommandText = "select  workno from GDS_ATT_APPLYOUT where ID = '" + Id[i] + "'";
                                    string aplempno = command.ExecuteOracleScalar().ToString();
                                    SendMail(aplempno, AuditMan, "外出申請單", BillNo, DisSignRamark[i], command);
                                }
                                SaveLogData("U", "", command.CommandText, command,logmodel);
                            }
                            command.CommandText = "update GDS_ATT_APPLYOUT set Status='2',AUDITER='" + AuditMan + "',AUDITDATE=sysdate,AUDITIDEA='" + ApprovedApRemark + "' where BillNo='" + BillNo + "' and Status='1'";
                            command.ExecuteNonQuery();
                            SaveLogData("U", "", command.CommandText, command,logmodel);
                            goto Label_2FB2;
                        case "D002":
                            if (condition.Length > 0)
                            {
                                command.CommandText = "insert into GDS_WF_NOTESREMIND(Content,WorkNo,ApplyMan,ApplyDate,BillNo,SendNotes,Auditstatus,Remark,RemindType,Subject) SELECT '" + BillTypeName + "', WorkNo, '" + AuditMan + "',sysdate,'" + BillNo + "','N','0','" + ApRemark + "','A','" + Content + "'  FROM GDS_ATT_LEAVEAPPLY WHERE BillNo='" + BillNo + "' and Status='1' " + condition;
                                command.ExecuteNonQuery();
                                command.CommandText = "update GDS_ATT_LEAVEAPPLY set Status='3',APPROVER='" + AuditMan + "',APPROVEDATE=sysdate,APREMARK='" + ApRemark + "' where Status='1' " + condition;
                                command.ExecuteNonQuery();
                                for (int i = 0; i < DisSignRamark.Count; i++)
                                {
                                    command.CommandText = "UPDATE GDS_ATT_LEAVEAPPLY  SET dissignrmark = '" + DisSignRamark[i] + "' WHERE ID = '" + Id[i] + "'";
                                    command.ExecuteNonQuery();

                                    //發送郵件
                                    command.CommandText = "select  workno from gds_att_leaveapply where ID = '" + Id[i] + "'";
                                    string aplempno = command.ExecuteOracleScalar().ToString();
                                    SendMail(aplempno, AuditMan, "請假申請單", BillNo, DisSignRamark[i], command);
                                }
                                SaveLogData("U", "", command.CommandText, command,logmodel);
                            }
                            command.CommandText = "update GDS_ATT_LEAVEAPPLY set Status='2',APPROVER='" + AuditMan + "',APPROVEDATE=sysdate,APREMARK='" + ApprovedApRemark + "' where BillNo='" + BillNo + "' and Status='1'";
                            command.ExecuteNonQuery();
                            SaveLogData("U", "", command.CommandText, command,logmodel);
                            goto Label_2FB2;
                        case "KQMException":
                            if (condition.Length > 0)
                            {
                                command.CommandText = "insert into GDS_WF_NOTESREMIND(Content,WorkNo,ApplyMan,ApplyDate,BillNo,SendNotes,Auditstatus,Remark,RemindType,Subject) SELECT '" + BillTypeName + "', WorkNo, '" + AuditMan + "',sysdate,'" + BillNo + "','N','0','" + ApRemark + "','A','" + Content + "'  FROM GDS_ATT_KAOQINDATA WHERE BillNo='" + BillNo + "' and Status='1' " + condition;
                                command.ExecuteNonQuery();
                                command.CommandText = "update GDS_ATT_KAOQINDATA set Status='3',APPROVER='" + AuditMan + "',AUDITDATE=sysdate,APREMARK='" + ApRemark + "' where Status='1' " + condition;
                                command.ExecuteNonQuery();
                                for (int i = 0; i < DisSignRamark.Count; i++)
                                {
                                    command.CommandText = "UPDATE GDS_ATT_KAOQINDATA  SET dissignrmark = '" + DisSignRamark[i] + "' WHERE ID = '" + Id[i] + "'";
                                    command.ExecuteNonQuery();
                                }
                                SaveLogData("U", "", command.CommandText, command,logmodel);
                            }
                            command.CommandText = "update GDS_ATT_KAOQINDATA set Status='2',APPROVER='" + AuditMan + "',APPROVEDATE=sysdate,APREMARK='" + ApprovedApRemark + "' where BillNo='" + BillNo + "' and Status='1'";
                            command.ExecuteNonQuery();
                            SaveLogData("U", "", command.CommandText, command,logmodel);
                            goto Label_2FB2;
                      
                        #region 未刷補卡

                        case "KQMMakeup":      //未刷補卡
                            if (condition.Length > 0)
                            {
                                command.CommandText = "insert into GDS_WF_NOTESREMIND(Content,WorkNo,ApplyMan,ApplyDate,BillNo,SendNotes,Auditstatus,Remark,RemindType,Subject) SELECT '" + BillTypeName + "', WorkNo, '" + AuditMan + "',sysdate,'" + BillNo + "','N','0','" + ApRemark + "','A','" + Content + "'  FROM GDS_ATT_makeup WHERE BillNo='" + BillNo + "' and Status='1' " + condition;
                                command.ExecuteNonQuery();
                                command.CommandText = "update GDS_ATT_makeup set Status='2',APPROVER='" + AuditMan + "',AUDITDATE=sysdate,APREMARK='" + ApRemark + "' where Status='1' " + condition;   //0-未核准;1-簽核中;2-已核准;3-拒簽
                                command.ExecuteNonQuery();
                                for (int i = 0; i < DisSignRamark.Count; i++)
                                {
                                    command.CommandText = "UPDATE GDS_ATT_makeup  SET dissignrmark = '" + DisSignRamark[i] + "' WHERE ID = '" + Id[i] + "'";
                                    command.ExecuteNonQuery();
                                }
                                SaveLogData("U", "", command.CommandText, command,logmodel);
                            }
                            command.CommandText = "update GDS_ATT_makeup set Status='2',APPROVER='" + AuditMan + "',APPROVEDATE=sysdate,APREMARK='" + ApprovedApRemark + "' where BillNo='" + BillNo + "' and Status='1'";  //0-未核准;1-簽核中;2-已核准;3-拒簽
                            command.ExecuteNonQuery();
                            SaveLogData("U", "", command.CommandText, command,logmodel);
                            goto Label_2FB2;

                        #endregion

                        #region 免卡人員加班

                        case "KQMOTMA":     //免卡人員加班
                            if (condition.Length > 0)
                            {
                                command.CommandText = "insert into GDS_WF_NOTESREMIND(Content,WorkNo,ApplyMan,ApplyDate,BillNo,SendNotes,Auditstatus,Remark,RemindType,Subject) SELECT '" + BillTypeName + "', WorkNo, '" + AuditMan + "',sysdate,'" + BillNo + "','N','0','" + ApRemark + "','A','" + Content + "'  FROM gds_att_Activity WHERE BillNo='" + BillNo + "' and Status='1' " + condition;
                                command.ExecuteNonQuery();
                                command.CommandText = "update gds_att_Activity set Status='2',APPROVER='" + AuditMan + "',AUDITDATE=sysdate,APREMARK='" + ApRemark + "' where Status='1' " + condition; //0-未核准;1-簽核中;2-已核准;3-拒簽
                                command.ExecuteNonQuery();
                                for (int i = 0; i < DisSignRamark.Count; i++)
                                {
                                    command.CommandText = "UPDATE gds_att_Activity  SET dissignrmark = '" + DisSignRamark[i] + "' WHERE ID = '" + Id[i] + "'";
                                    command.ExecuteNonQuery();
                                }
                                SaveLogData("U", "", command.CommandText, command,logmodel);
                            }
                            command.CommandText = "update gds_att_Activity set Status='2',APPROVER='" + AuditMan + "',APPROVEDATE=sysdate,APREMARK='" + ApprovedApRemark + "' where BillNo='" + BillNo + "' and Status='1'"; //0-未核准;1-簽核中;2-已核准;3-拒簽
                            command.ExecuteNonQuery();
                            SaveLogData("U", "", command.CommandText, command,logmodel);
                            goto Label_2FB2;

                        #endregion

                        #region  月加班匯總 KQM
                        case "KQMMonthTotal":
                            if (condition.Length > 0)
                            {
                                command.CommandText = "insert into GDS_WF_NOTESREMIND(Content,WorkNo,ApplyMan,ApplyDate,BillNo,SendNotes,Auditstatus,Remark,RemindType,Subject) SELECT '" + BillTypeName + "', WorkNo, '" + AuditMan + "',sysdate,'" + BillNo + "','N','0','" + ApRemark + "','A','" + Content + "'  FROM GDS_ATT_APPLYOUT WHERE BillNo='" + BillNo + "' and appoveflag='1' " + condition;
                                command.ExecuteNonQuery();
                                command.CommandText = "update GDS_ATT_MonthTotal set appoveflag='3',APPROVER='" + AuditMan + "',APPROVEDATE=sysdate,APREMARK='" + ApRemark + "' where appoveflag='1' " + condition;
                                command.ExecuteNonQuery();
                                for (int i = 0; i < DisSignRamark.Count; i++)
                                {
                                    command.CommandText = "UPDATE GDS_ATT_MonthTotal  SET dissignrmark = '" + DisSignRamark[i] + "' WHERE ID = '" + Id[i] + "'";
                                    command.ExecuteNonQuery();
                                }
                                SaveLogData("U", "", command.CommandText, command,logmodel);
                            }
                            command.CommandText = "update GDS_ATT_MonthTotal set APPROVER='" + AuditMan + "',APPROVEDATE=sysdate,APREMARK='" + ApRemark + "' where BillNo='" + BillNo + "' and appoveflag='1'";
                            command.ExecuteNonQuery();
                            SaveLogData("U", "", command.CommandText, command,logmodel);
                            goto Label_2FB2;
                        #endregion 
                    }
                }
                else
                {
                    switch (BillTypeCode)
                    {
                        case "D001":
                        case "OTMProjectApply":
                            if (condition.Length > 0)
                            {
                                command.CommandText = "insert into GDS_WF_NOTESREMIND(Content,WorkNo,ApplyMan,ApplyDate,BillNo,SendNotes,Auditstatus,Remark,RemindType,Subject) SELECT '" + BillTypeName + "', WorkNo, '" + AuditMan + "',sysdate,'" + BillNo + "','N','0','" + ApRemark + "','A','" + Content + "'  FROM GDS_ATT_APPLYOUT WHERE BillNo='" + BillNo + "' and Status='1' " + condition;
                                command.ExecuteNonQuery();
                                command.CommandText = "update GDS_ATT_ADVANCEAPPLY set Status='3',Approver='" + AuditMan + "',ApproveDate=sysdate,ApRemark='" + ApRemark + "' where Status='1' " + condition;
                                command.ExecuteNonQuery();
                                for (int i = 0; i < DisSignRamark.Count; i++)
                                {
                                    command.CommandText = "UPDATE gds_att_advanceapply  SET dissignrmark = '" + DisSignRamark[i] + "' WHERE ID = '" + Id[i] + "'";
                                    command.ExecuteNonQuery();
                                }
                                SaveLogData("U", "", command.CommandText, command,logmodel);
                            }
                            command.CommandText = "update GDS_ATT_ADVANCEAPPLY set Approver='" + AuditMan + "',ApproveDate=sysdate,ApRemark='" + ApprovedApRemark + "' where BillNo='" + BillNo + "' and Status='1'";
                            command.ExecuteNonQuery();
                            SaveLogData("U", "", command.CommandText, command,logmodel);
                            goto Label_2FB2;
                        case "KQMApplyOut":
                            if (condition.Length > 0)
                            {
                                command.CommandText = "insert into GDS_WF_NOTESREMIND(Content,WorkNo,ApplyMan,ApplyDate,BillNo,SendNotes,Auditstatus,Remark,RemindType,Subject) SELECT '" + BillTypeName + "', WorkNo, '" + AuditMan + "',sysdate,'" + BillNo + "','N','0','" + ApRemark + "','A','" + Content + "'  FROM GDS_ATT_APPLYOUT WHERE BillNo='" + BillNo + "' and Status='1' " + condition;
                                command.ExecuteNonQuery();
                                command.CommandText = "update GDS_ATT_APPLYOUT set Status='3',AUDITER='" + AuditMan + "',AUDITDATE=sysdate,AUDITIDEA='" + ApRemark + "' where Status='1' " + condition;
                                command.ExecuteNonQuery();
                                for (int i = 0; i < DisSignRamark.Count; i++)
                                {
                                    command.CommandText = "UPDATE GDS_ATT_APPLYOUT  SET dissignrmark = '" + DisSignRamark[i] + "' WHERE ID = '" + Id[i] + "'";
                                    command.ExecuteNonQuery();
                                }
                                SaveLogData("U", "", command.CommandText, command,logmodel);
                            }
                            command.CommandText = "update GDS_ATT_APPLYOUT set AUDITER='" + AuditMan + "',AUDITDATE=sysdate,AUDITIDEA='" +ApRemark + "' where BillNo='" + BillNo + "' and Status='1'";
                            command.ExecuteNonQuery();
                            SaveLogData("U", "", command.CommandText, command,logmodel);
                            goto Label_2FB2;

                        case "D002":
                            if (condition.Length > 0)
                            {
                                command.CommandText = "insert into GDS_WF_NOTESREMIND(Content,WorkNo,ApplyMan,ApplyDate,BillNo,SendNotes,Auditstatus,Remark,RemindType,Subject) SELECT '" + BillTypeName + "', WorkNo, '" + AuditMan + "',sysdate,'" + BillNo + "','N','0','" + ApRemark + "','A','" + Content + "'  FROM GDS_ATT_APPLYOUT WHERE BillNo='" + BillNo + "' and Status='1' " + condition;
                                command.ExecuteNonQuery();
                                command.CommandText = "update GDS_ATT_LEAVEAPPLY set Status='3',APPROVER='" + AuditMan + "',APPROVEDATE=sysdate,APREMARK='" + ApRemark + "' where Status='1' " + condition;
                                command.ExecuteNonQuery();
                                for (int i = 0; i < DisSignRamark.Count; i++)
                                {
                                    command.CommandText = "UPDATE GDS_ATT_LEAVEAPPLY  SET dissignrmark = '" + DisSignRamark[i] + "' WHERE ID = '" + Id[i] + "'";
                                    command.ExecuteNonQuery();
                                }
                                SaveLogData("U", "", command.CommandText, command,logmodel);
                            }
                            command.CommandText = "update GDS_ATT_LEAVEAPPLY set APPROVER='" + AuditMan + "',APPROVEDATE=sysdate,APREMARK='" + ApprovedApRemark + "' where BillNo='" + BillNo + "' and Status='1'";
                            command.ExecuteNonQuery();
                            SaveLogData("U", "", command.CommandText, command,logmodel);
                            goto Label_2FB2;
                        case "KQMException":
                            if (condition.Length > 0)
                            {
                                command.CommandText = "insert into GDS_WF_NOTESREMIND(Content,WorkNo,ApplyMan,ApplyDate,BillNo,SendNotes,Auditstatus,Remark,RemindType,Subject) SELECT '" + BillTypeName + "', WorkNo, '" + AuditMan + "',sysdate,'" + BillNo + "','N','0','" + ApRemark + "','A','" + Content + "'  FROM GDS_ATT_KAOQINDATA WHERE BillNo='" + BillNo + "' and Status='1' " + condition;
                                command.ExecuteNonQuery();
                                command.CommandText = "update GDS_ATT_KAOQINDATA set Status='3',APPROVER='" + AuditMan + "',APPROVEDATE=sysdate,APREMARK='" + ApRemark + "' where Status='1' " + condition;
                                command.ExecuteNonQuery();
                                for (int i = 0; i < DisSignRamark.Count; i++)
                                {
                                    command.CommandText = "UPDATE GDS_ATT_KAOQINDATA  SET dissignrmark = '" + DisSignRamark[i] + "' WHERE ID = '" + Id[i] + "'";
                                    command.ExecuteNonQuery();
                                }
                                SaveLogData("U", "", command.CommandText, command,logmodel);
                            }
                            command.CommandText = "update GDS_ATT_KAOQINDATA set APPROVER='" + AuditMan + "',APPROVEDATE=sysdate,APREMARK='" + ApprovedApRemark + "' where BillNo='" + BillNo + "' and Status='1'";
                            command.ExecuteNonQuery();
                            SaveLogData("U", "", command.CommandText, command,logmodel);
                            goto Label_2FB2;
                       
                        #region 未刷補卡

                        case "KQMMakeup":      //未刷補卡
                            if (condition.Length > 0)
                            {
                                command.CommandText = "insert into GDS_WF_NOTESREMIND(Content,WorkNo,ApplyMan,ApplyDate,BillNo,SendNotes,Auditstatus,Remark,RemindType,Subject) SELECT '" + BillTypeName + "', WorkNo, '" + AuditMan + "',sysdate,'" + BillNo + "','N','0','" + ApRemark + "','A','" + Content + "'  FROM GDS_ATT_makeup WHERE BillNo='" + BillNo + "' and Status='1' " + condition;
                                command.ExecuteNonQuery();
                                command.CommandText = "update GDS_ATT_makeup set Status='2',APPROVER='" + AuditMan + "',APPROVEDATE=sysdate,APREMARK='" + ApRemark + "' where Status='1' " + condition;
                                command.ExecuteNonQuery();
                                for (int i = 0; i < DisSignRamark.Count; i++)
                                {
                                    command.CommandText = "UPDATE GDS_ATT_makeup  SET dissignrmark = '" + DisSignRamark[i] + "' WHERE ID = '" + Id[i] + "'";
                                    command.ExecuteNonQuery();
                                }
                                SaveLogData("U", "", command.CommandText, command,logmodel);
                            }
                            command.CommandText = "update GDS_ATT_makeup set APPROVER='" + AuditMan + "',APPROVEDATE=sysdate,APREMARK='" + ApprovedApRemark + "' where BillNo='" + BillNo + "' and Status='1'";
                            command.ExecuteNonQuery();
                            SaveLogData("U", "", command.CommandText, command,logmodel);
                            goto Label_2FB2;

                        #endregion

                        #region 免卡人員加班

                        case "KQMOTMA":     //免卡人員加班
                            if (condition.Length > 0)
                            {
                                command.CommandText = "insert into GDS_WF_NOTESREMIND(Content,WorkNo,ApplyMan,ApplyDate,BillNo,SendNotes,Auditstatus,Remark,RemindType,Subject) SELECT '" + BillTypeName + "', WorkNo, '" + AuditMan + "',sysdate,'" + BillNo + "','N','0','" + ApRemark + "','A','" + Content + "'  FROM gds_att_Activity WHERE BillNo='" + BillNo + "' and Status='1' " + condition;
                                command.ExecuteNonQuery();
                                command.CommandText = "update gds_att_Activity set Status='2',APPROVER='" + AuditMan + "',APPROVEDATE=sysdate,APREMARK='" + ApRemark + "' where Status='1' " + condition;
                                command.ExecuteNonQuery();
                                for (int i = 0; i < DisSignRamark.Count; i++)
                                {
                                    command.CommandText = "UPDATE gds_att_Activity  SET dissignrmark = '" + DisSignRamark[i] + "' WHERE ID = '" + Id[i] + "'";
                                    command.ExecuteNonQuery();
                                }
                                SaveLogData("U", "", command.CommandText, command,logmodel);
                            }
                            command.CommandText = "update gds_att_Activity set APPROVER='" + AuditMan + "',APPROVEDATE=sysdate,APREMARK='" + ApprovedApRemark + "' where BillNo='" + BillNo + "' and Status='1'";
                            command.ExecuteNonQuery();
                            SaveLogData("U", "", command.CommandText, command,logmodel);
                            goto Label_2FB2;

                        #endregion
                        #region  月加班匯總 KQM
                        case "KQMMonthTotal":
                            if (condition.Length > 0)
                            {
                                command.CommandText = "insert into GDS_WF_NOTESREMIND(Content,WorkNo,ApplyMan,ApplyDate,BillNo,SendNotes,Auditstatus,Remark,RemindType,Subject) SELECT '" + BillTypeName + "', WorkNo, '" + AuditMan + "',sysdate,'" + BillNo + "','N','0','" + ApRemark + "','A','" + Content + "'  FROM GDS_ATT_APPLYOUT WHERE BillNo='" + BillNo + "' and appoveflag='1' " + condition;
                                command.ExecuteNonQuery();
                                command.CommandText = "update GDS_ATT_MonthTotal set appoveflag='3',APPROVER='" + AuditMan + "',APPROVEDATE=sysdate,APREMARK='" + ApRemark + "' where appoveflag='1' " + condition;
                                command.ExecuteNonQuery();
                                for (int i = 0; i < DisSignRamark.Count; i++)
                                {
                                    command.CommandText = "UPDATE GDS_ATT_MonthTotal  SET dissignrmark = '" + DisSignRamark[i] + "' WHERE ID = '" + Id[i] + "'";
                                    command.ExecuteNonQuery();
                                }
                                SaveLogData("U", "", command.CommandText, command,logmodel);
                            }
                            command.CommandText = "update GDS_ATT_MonthTotal set APPROVER='" + AuditMan + "',APPROVEDATE=sysdate,APREMARK='" + ApRemark + "' where BillNo='" + BillNo + "' and appoveflag='1'";
                            command.ExecuteNonQuery();
                            SaveLogData("U", "", command.CommandText, command,logmodel);
                            goto Label_2FB2;
                        #endregion 
                    }
                }
            Label_2FB2:
                command.Transaction.Commit();
                command.Transaction = null;
            }
            catch (Exception ex)
            {
                command.Transaction.Rollback();
                command.Transaction = null;
                throw ex;
            }

        }

        #region 重載同意動作的方法---made by ziyan
        /// <summary>
        /// 重載同意動作的方法---made by ziyan 
        /// </summary>
        /// <param name="BillNo">單號</param>
        /// <param name="AuditMan">簽核人（登錄人）</param>
        /// <param name="BillTypeCode">單據類型 （月加班匯總）</param>
        /// <param name="ApRemark">簽核意見</param>
        /// <param name="DisSignRamark">拒簽意見</param>
        /// <param name="YearMonth">年月（主鍵一）</param>
        /// <param name="WorkNo">工號（主鍵二）</param>
        public void SaveAuditData(string BillNo, string AuditMan, string BillTypeCode, string ApRemark, string condition, List<string> DisSignRamark, List<string> WorkNo, List<string> YearMonth, SynclogModel logmodel)
        {
            OracleCommand command = new OracleCommand();
            command.Connection = DalHelper.Connection;
            command.Connection.Open();
            OracleTransaction trans = command.Connection.BeginTransaction();
            command.Transaction = trans;
            try
            {
                string BillTypeName = string.Empty;
                command.CommandText = "SELECT nvl(MAX (ParaValue),'')  FROM GDS_SC_PARAMETER WHERE ParaName ='WFMDisRemindSubject'";
                string Content = BillTypeName + Convert.ToString(command.ExecuteScalar());
                string ApprovedApRemark = "";
                if (ApRemark.IndexOf("|") >= 0)
                {
                    ApprovedApRemark = ApRemark.Split(new char[] { '|' })[0].ToString();
                }
                ApRemark = ApRemark.Replace("|", "");
                if (ApRemark.Length > 500)
                {
                    ApRemark = ApRemark.Substring(0, 500) + "...";
                }
                command.CommandText = "update GDS_ATT_AUDITSTATUS a set AuditStatus='1',AuditDate=sysdate, Remark='" + ApRemark + "' where BillNo='" + BillNo + "' and AuditMan='" + AuditMan + "' and AuditStatus='0' and a.OrderNo=(select nvl(min(e.OrderNo),'-1') from GDS_ATT_AUDITSTATUS e where e.BillNo=a.BillNo and e.Auditstatus='0') ";
                command.ExecuteNonQuery();
                SaveLogData("U", "", command.CommandText, command);
                command.CommandText = "SELECT count(1) FROM GDS_ATT_AUDITSTATUS WHERE BillNo='" + BillNo + "' and AuditStatus='0'";
                if (Convert.ToDecimal(command.ExecuteScalar()) == 0M)
                {
                    command.CommandText = "update GDS_ATT_BILL set Status='1', Remark='" + ApRemark + "' where BillNo='" + BillNo + "' and Status='0'";
                    command.ExecuteNonQuery();
                    SaveLogData("U", "", command.CommandText, command,logmodel);
                    switch (BillTypeCode)
                    {
                        #region  月加班匯總 KQM
                        case "KQMMonthTotal":
                            if (condition.Length > 0)
                            {
                                command.CommandText = "insert into GDS_WF_NOTESREMIND(Content,WorkNo,ApplyMan,ApplyDate,BillNo,SendNotes,Auditstatus,Remark,RemindType,Subject) SELECT '" + BillTypeName + "', WorkNo, '" + AuditMan + "',sysdate,'" + BillNo + "','N','0','" + ApRemark + "','A','" + Content + "'  FROM GDS_ATT_monthtotal WHERE BillNo='" + BillNo + "' and APPROVEFLAG='1' " + condition;
                                command.ExecuteNonQuery();
                                command.CommandText = "update GDS_ATT_MonthTotal set appoveflag='3',APPROVER='" + AuditMan + "',APPROVEDATE=sysdate,APREMARK='" + ApRemark + "' where APPROVEFLAG='1' " + condition;
                                command.ExecuteNonQuery();
                                for (int i = 0; i < DisSignRamark.Count; i++)
                                {
                                    command.CommandText = "UPDATE GDS_ATT_MonthTotal  SET dissignrmark = '" + DisSignRamark[i] + "'WHERE WORKNO = '" + WorkNo[i] + "' and YEARMONTH='" + YearMonth[i] + "'";
                                    command.ExecuteNonQuery();
                                }
                                SaveLogData("U", "", command.CommandText, command,logmodel);
                            }
                            command.CommandText = "update GDS_ATT_MonthTotal set APPROVER='" + AuditMan + "',APPROVEDATE=sysdate,APREMARK='" + ApRemark + "' where BillNo='" + BillNo + "' and APPROVEFLAG='1'";
                            command.ExecuteNonQuery();
                            SaveLogData("U", "", command.CommandText, command,logmodel);
                            goto Label_2FB2;
                        #endregion
                    }
                }
                else
                {
                    switch (BillTypeCode)
                    {
                        #region  月加班匯總 KQM
                        case "KQMMonthTotal":
                            if (condition.Length > 0)
                            {
                                command.CommandText = "insert into GDS_WF_NOTESREMIND(Content,WorkNo,ApplyMan,ApplyDate,BillNo,SendNotes,Auditstatus,Remark,RemindType,Subject) SELECT '" + BillTypeName + "', WorkNo, '" + AuditMan + "',sysdate,'" + BillNo + "','N','0','" + ApRemark + "','A','" + Content + "'  FROM GDS_ATT_monthtotal WHERE BillNo='" + BillNo + "' and APPROVEFLAG='1' " + condition;
                                command.ExecuteNonQuery();
                                command.CommandText = "update GDS_ATT_MonthTotal set appoveflag='3',APPROVER='" + AuditMan + "',APPROVEDATE=sysdate,APREMARK='" + ApRemark + "' where APPROVEFLAG='1' " + condition;
                                command.ExecuteNonQuery();
                                for (int i = 0; i < DisSignRamark.Count; i++)
                                {
                                    command.CommandText = "UPDATE GDS_ATT_MonthTotal  SET dissignrmark = '" + DisSignRamark[i] + "' WHERE WORKNO = '" + WorkNo[i] + "' and YEARMONTH='"+YearMonth[i]+"'";
                                    command.ExecuteNonQuery();
                                }
                                SaveLogData("U", "", command.CommandText, command,logmodel);
                            }
                            command.CommandText = "update GDS_ATT_MonthTotal set APPROVER='" + AuditMan + "',APPROVEDATE=sysdate,APREMARK='" + ApRemark + "' where BillNo='" + BillNo + "' and  APPROVEFLAG='1'";
                            command.ExecuteNonQuery();
                            SaveLogData("U", "", command.CommandText, command,logmodel);
                            goto Label_2FB2;
                        #endregion
                    }
                }
            Label_2FB2:
                command.Transaction.Commit();
                command.Transaction = null;
            }
            catch (Exception ex)
            {
                command.Transaction.Rollback();
                command.Transaction = null;
                throw ex;
            }

        }
        #endregion
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


        public DataSet GetDataSetBySQL(string sql)
        {
            try
            {
                DataSet ds = new DataSet();
                DataTable dt = DalHelper.ExecuteQuery(sql);
                dt.TableName = "TempTable";
                ds.Tables.Add(dt);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public DataSet GetAuditDataByCondition(string condition)
        {

            try
            {
                DataSet ds = new DataSet();
                string sql = "SELECT * FROM (select a.*, (case when OldAuditMan >' ' then nvl((select LocalName From GDS_ATT_EMPLOYEE e Where e.WorkNo=a.AuditMan),(select LocalName From GDS_ATT_EMPLOYEE e Where e.WorkNo=a.AuditMan))|| '('||(select PARAVALUE from GDS_SC_PARAMETER where PARANAME='WFMAuditProxy')|| nvl((select LocalName From GDS_ATT_EMPLOYEE e Where e.WorkNo=a.OldAuditMan),(select LocalName From GDS_ATT_EMPLOYEE e Where e.WorkNo=a.OldAuditMan))||')' else nvl((select LocalName From GDS_ATT_EMPLOYEE e Where e.WorkNo=a.AuditMan),(select LocalName From GDS_ATT_EMPLOYEE e Where e.WorkNo=a.AuditMan)) end)AuditManName,round((nvl(a.Auditdate,sysdate)-nvl(a.Sendtime,nvl(a.AuditDate,sysdate)))*24,1) StayHours,  nvl((select e.depname From GDS_ATT_EMPLOYEE e Where e.WorkNo=a.AuditMan and e.status='0'),(select depname From GDS_ATT_EMPLOYEE e Where e.WorkNo=a.AuditMan)) Depname, (select DataValue From GDS_ATT_TYPEDATA c Where c.DataType='AuditTypeMove' and c.DataCode=a.AuditType)AuditTypeName, (select DataValue From GDS_ATT_TYPEDATA c Where c.DataType='WFMBillStatus' and c.DataCode=a.AuditStatus)StatusName, (select DataValue From GDS_ATT_TYPEDATA c Where c.DataType='AuditTypeLeave' and c.DataCode=a.AuditManType)AuditTypeLeaveName from GDS_ATT_AUDITSTATUS a Where 1=1 " + condition + " order by a.BillNo,a.orderno )";
                DataTable dt = DalHelper.ExecuteQuery(sql);
                if (dt != null)
                {
                    dt.TableName = "WFM_AuditStatus";
                }
                ds.Tables.Add(dt);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public DataSet GetDataByCondition(string condition)
        {

            try
            {
                DataSet ds = new DataSet();
                string sql = "SELECT * from (SELECT a.*,to_char(a.OTDate,'DY') as Week,TRIM(INITCAP (TO_CHAR (a.OTDate,'day','NLS_DATE_LANGUAGE=American'))) EnWeek, (case when a.OTType='G1' and instr(a.Status,'2')=0 then a.Hours else 0 end) G1Total,(case when a.OTType='G2' and instr(a.Status,'2')=0 then a.Hours else 0 end) G2Total,(case when a.OTType in('G3','G4') and instr(a.Status,'2')=0 then a.Hours else 0 end) G3Total, b.LocalName,b.OverTimeType,b.DCode,b.LevelName,b.ManagerName, (select datavalue from GDS_ATT_TYPEDATA c where c.DataType='OTMAdvanceApplyStatus' and c.DataCode=a.Status) StatusName, b.dname DepName, (SELECT depname FROM GDS_SC_DEPARTMENT s WHERE s.LevelCode='2' START WITH s.depcode=b.depcode CONNECT BY s.depcode = PRIOR s.parentdepcode ) buname, (select datavalue from GDS_ATT_TYPEDATA where datatype='EMPtype' and datacode=substr((select e.POSTCODE from GDS_ATT_EMPLOYEES e where e.workno=b.workno),0,1)) persontype,(select LocalName From GDS_ATT_EMPLOYEE e Where e.WorkNo=a.UPDATE_USER)modifyName, (select LocalName From GDS_ATT_EMPLOYEE e Where e.WorkNo=upper(a.Approver)) ApproverName from GDS_ATT_ADVANCEAPPLY a,GDS_ATT_EMPLOYEE b where b.WorkNo=a.WorkNo " + condition + ")";
                DataTable dt = DalHelper.ExecuteQuery(sql);
                if (dt != null)
                {
                    dt.TableName = "OTM_AdvanceApply";
                }
                ds.Tables.Add(dt);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public DataSet GetAuditDataByCondition(string condition)
        //{           
        //    try
        //    {
        //        DataSet ds = new DataSet();
        //        string sql = "SELECT * FROM (select a.*, (case when OldAuditMan >' ' then nvl((select LocalName From GDS_ATT_EMPLOYEE e Where e.WorkNo=a.AuditMan),(select LocalName From GDS_ATT_EMPLOYEE e Where e.WorkNo=a.AuditMan))|| '('||(select PARAVALUE from GDS_SC_PARAMETER where PARANAME='WFMAuditProxy')|| nvl((select LocalName From GDS_ATT_EMPLOYEE e Where e.WorkNo=a.OldAuditMan),(select LocalName From GDS_ATT_EMPLOYEE e Where e.WorkNo=a.OldAuditMan))||')' else nvl((select LocalName From GDS_ATT_EMPLOYEE e Where e.WorkNo=a.AuditMan),(select LocalName From GDS_ATT_EMPLOYEE e Where e.WorkNo=a.AuditMan)) end)AuditManName,round((nvl(a.Auditdate,sysdate)-nvl(a.Sendtime,nvl(a.AuditDate,sysdate)))*24,1) StayHours,  nvl((select e.depname From GDS_ATT_EMPLOYEE e Where e.WorkNo=a.AuditMan and e.status='0'),(select depname From GDS_ATT_EMPLOYEE e Where e.WorkNo=a.AuditMan)) Depname, (select DataValue From GDS_ATT_TYPEDATA c Where c.DataType='AuditTypeMove' and c.DataCode=a.AuditType)AuditTypeName, (select DataValue From GDS_ATT_TYPEDATA c Where c.DataType='WFMBillStatus' and c.DataCode=a.AuditStatus)StatusName, (select DataValue From GDS_ATT_TYPEDATA c Where c.DataType='AuditTypeLeave' and c.DataCode=a.AuditManType)AuditTypeLeaveName from GDS_ATT_AUDITSTATUS a Where 1=1 " + condition + " order by a.BillNo,a.orderno )";
        //        DataTable dt = DalHelper.ExecuteQuery(sql);
        //        if (dt != null)
        //        {
        //            dt.TableName = "WFM_AuditStatus";                    
        //        }
        //        return ds;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }           

        //}

        //public DataSet GetAuditCenterDataByCondition(string condition)
        //{            
        //    try
        //    {
        //        DataSet ds = new DataSet();
        //        string sql = "SELECT * FROM (select a.*,  (case when (select BillTypeCode From BFW_BillType c Where c.BillTypeNo=substr(a.BillNo,0,3))='KQMLeaveApply' then (select BillTypeName From BFW_BillType c Where c.BillTypeNo=substr(a.BillNo,0,3))||'-'|| (select LVTypeName from KQM_LeaveType c where c.LVTypeCode= (select LVTypeCode from kqm_leaveapply d where d.BillNo=a.BillNo and rownum=1)) else (select BillTypeName From BFW_BillType c Where c.BillTypeCode=a.BillTypeCode) end) BillTypeName, (select DepName From BFW_Department c Where c.DepCode=a.OrgCode) OrgName,  (select DataValue From BFW_TypeData c Where c.DataType='WFMBillStatus' and c.DataCode=a.Status) StatusName,  (select DataValue From BFW_TypeData c Where c.DataType='AuditTypeMove' and c.DataCode=b.AuditType)AuditTypeName, nvl((select e.depname From V_Employee e Where e.WorkNo=b.AuditMan and e.status='0'),(select depname From WFM_EmployeesOut e Where e.WorkNo=b.AuditMan)) Depname, (select LocalName From V_Employee e Where e.WorkNo=a.ApplyMan) ApplyManName  from WFM_Bill a ,WFM_AuditStatus b  Where a.BillNo=b.BillNo " + condition + ")";
        //        DataTable dt = DalHelper.ExecuteQuery(sql);
        //        if (dt != null)
        //        {
        //            dt.TableName = "WFM_Bill";
        //        }
        //        ds.Tables.Add(dt);
        //        return ds;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }           
        //}



        public string GetAllDept(string sDepCode, bool bParent)
        {
            string sRValue = "";
            string sql = "";

            try
            {
                if (bParent)
                {
                    sql = "select * from (select (depnamelevel2||case when depnamelevel3 is not null then '/'||depnamelevel3 end  || case when depnamelevel4 is not null then '/'||depnamelevel4 end || case when depnamelevel5 is not null then '/'||depnamelevel5 end || case when depnamelevel6 is not null then '/'||depnamelevel6 end || case when depnamelevel7 is not null then '/'||depnamelevel7 end || case when depnamelevel8 is not null then '/'||depnamelevel8 end || case when depnamelevel9 is not null then '/'||depnamelevel9 end || case when depnamelevel10 is not null then '/'||depnamelevel10 end || case when depnamelevel11 is not null then '/'||depnamelevel11 end) depname from GDS_ATT_ORGTREE where depcode='" + sDepCode + "')";
                    return this.GetValue(sql);
                }
                sql = "SELECT depname FROM GDS_SC_DEPARTMENT START WITH depcode='" + sDepCode + "' CONNECT BY ";
                if (bParent)
                {
                    sql = sql + " depcode=PRIOR parentdepcode ";
                }
                else
                {
                    sql = sql + " PRIOR depcode = parentdepcode ";
                }
                sql = sql + " ORDER BY levelcode ";
                DataTable dt = DalHelper.ExecuteQuery(sql);
                DataSet ds = new DataSet();
                if (dt != null && dt.Rows.Count > 0)
                {
                    dt.TableName = "bfw_department";
                }
                ds.Tables.Add(dt);
                //base.dataAdapter.Fill(base.dataSet, "bfw_department");
                if ((ds.Tables["bfw_department"] == null) || (ds.Tables["bfw_department"].Rows.Count <= 0))
                {
                    return sRValue;
                }
                foreach (DataRow dr in ds.Tables["bfw_department"].Rows)
                {
                    sRValue = sRValue + dr["depname"].ToString() + "/";
                }
                return sRValue.TrimEnd(new char[] { '/' });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet GetDataByCondition_Bill(string condition)
        {
            try
            {
                DataSet ds = new DataSet();
                string sql = "SELECT * FROM (select a.*, (case when (select BillTypeCode From GDS_WF_BILLTYPE c Where c.BillTypeNo=substr(a.BillNo,0,3))='KQMLeaveApply' then (select BillTypeName From GDS_WF_BILLTYPE c Where c.BillTypeNo=substr(a.BillNo,0,3))||'-'|| (select LVTypeName from GDS_ATT_LEAVETYPE c where c.LVTypeCode= (select LVTypeCode from GDS_ATT_LEAVEAPPLY d where d.BillNo=a.BillNo and rownum=1)) else (select BillTypeName From GDS_WF_BILLTYPE c Where c.BillTypeCode=a.BillTypeCode) end) BillTypeName, (select DepName From GDS_SC_DEPARTMENT c Where c.DepCode=a.OrgCode) OrgName, (select DataValue From GDS_ATT_TYPEDATA c Where c.DataType='WFMBillStatus' and c.DataCode=a.Status) StatusName, (select LocalName from GDS_ATT_EMPLOYEE c Where c.WorkNo=a.ApplyMan) ApplyManName, (select nvl((select LocalName From GDS_ATT_EMPLOYEE e Where e.WorkNo=c.AuditMan),(select LocalName From GDS_ATT_EMPLOYEE e Where e.WorkNo=c.AuditMan)) from GDS_ATT_AUDITSTATUS c Where c.BillNo=a.BillNo and c.OrderNo='1')AuditMan1, (select (select DataValue From GDS_ATT_TYPEDATA c Where c.DataType='WFMBillStatus' and c.DataCode=c.AuditStatus) from GDS_ATT_AUDITSTATUS c Where c.BillNo=a.BillNo and c.OrderNo='1')StatusName1, (select SendNotes from GDS_ATT_AUDITSTATUS c Where c.BillNo=a.BillNo and c.OrderNo='1')SendNotes1, (select nvl((select LocalName From GDS_ATT_EMPLOYEE e Where e.WorkNo=c.AuditMan),(select LocalName From GDS_ATT_EMPLOYEE e Where e.WorkNo=c.AuditMan)) from GDS_ATT_AUDITSTATUS c Where c.BillNo=a.BillNo and c.OrderNo='2')AuditMan2, (select (select DataValue From GDS_ATT_TYPEDATA c Where c.DataType='WFMBillStatus' and c.DataCode=c.AuditStatus) from GDS_ATT_AUDITSTATUS c Where c.BillNo=a.BillNo and c.OrderNo='2')StatusName2, (select SendNotes from GDS_ATT_AUDITSTATUS c Where c.BillNo=a.BillNo and c.OrderNo='2')SendNotes2, (select nvl((select LocalName From GDS_ATT_EMPLOYEE e Where e.WorkNo=c.AuditMan),(select LocalName From GDS_ATT_EMPLOYEE e Where e.WorkNo=c.AuditMan)) from GDS_ATT_AUDITSTATUS c Where c.BillNo=a.BillNo and c.OrderNo='3')AuditMan3, (select (select DataValue From GDS_ATT_TYPEDATA c Where c.DataType='WFMBillStatus' and c.DataCode=c.AuditStatus) from GDS_ATT_AUDITSTATUS c Where c.BillNo=a.BillNo and c.OrderNo='3')StatusName3, (select SendNotes from GDS_ATT_AUDITSTATUS c Where c.BillNo=a.BillNo and c.OrderNo='3')SendNotes3, (select nvl((select LocalName From GDS_ATT_EMPLOYEE e Where e.WorkNo=c.AuditMan),(select LocalName From GDS_ATT_EMPLOYEE e Where e.WorkNo=c.AuditMan)) from GDS_ATT_AUDITSTATUS c Where c.BillNo=a.BillNo and c.OrderNo='4')AuditMan4, (select (select DataValue From GDS_ATT_TYPEDATA c Where c.DataType='WFMBillStatus' and c.DataCode=c.AuditStatus) from GDS_ATT_AUDITSTATUS c Where c.BillNo=a.BillNo and c.OrderNo='4')StatusName4, (select SendNotes from GDS_ATT_AUDITSTATUS c Where c.BillNo=a.BillNo and c.OrderNo='4')SendNotes4 from GDS_ATT_bill a Where 1=1 " + condition + ")";
                DataTable dt = DalHelper.ExecuteQuery(sql);
                if (dt != null)
                {
                    dt.TableName = "WFM_Bill";
                }
                ds.Tables.Add(dt);
                return ds;
                //base.dataAdapter.Fill(base.dataSet, "WFM_Bill");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 獲取單據類型
        /// </summary>
        /// <param name="billType">單據類型簡寫代碼</param>
        /// <returns></returns>
        public DataTable GetBillTypeCode(string billType)
        {
            DataTable dt = DalHelper.ExecuteQuery(@"Select BillTypeCode,billtypename From GDS_WF_BillType Where BillTypeNo='" + billType + "'");
            return dt;
        }


        public DataSet GetApprovedDataByCondition(string condition, int pageindex, int pagesize, out int totalcount)
        {
            try
            {
                DataSet ds = new DataSet();
                string sql = "SELECT * FROM  (SELECT rownum rn,a.* FROM(select a.*, (case when (select BillTypeCode From GDS_WF_BILLTYPE c Where c.BillTypeNo=substr(a.BillNo,0,3))='KQMLeaveApply' then (select BillTypeName From GDS_WF_BILLTYPE c Where c.BillTypeNo=substr(a.BillNo,0,3))||'-'|| (select LVTypeName from GDS_ATT_LEAVETYPE c where c.LVTypeCode= (select LVTypeCode from GDS_ATT_LEAVEAPPLY d where d.BillNo=a.BillNo and rownum=1)) else (select BillTypeName From GDS_WF_BILLTYPE c Where c.BillTypeCode=a.BillTypeCode) end) BillTypeName,  (select DepName From GDS_SC_DEPARTMENT c Where c.DepCode=a.OrgCode) OrgName, (select DataValue From GDS_ATT_TYPEDATA c Where c.DataType='WFMBillStatus' and c.DataCode=a.Status) StatusName, (select LocalName from GDS_ATT_EMPLOYEE c Where c.WorkNo=a.ApplyMan) ApplyManName from GDS_ATT_Bill a Where 1=1 " + condition + ")a) ORDER BY applydate DESC";
                DataTable dt = DalHelper.ExecutePagerQuery(sql, pageindex, pagesize, out totalcount);
                if (dt != null)
                {
                    dt.TableName = "WFM_Bill";
                }
                ds.Tables.Add(dt);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public DataSet GetApprovedDataByCondition(string condition, int pageSize, ref int out_CurrentPage, ref int out_TotalPage, ref int out_TotalRecords)
        //{
        //    int out_StartRow = 1;
        //    int out_EndRow = 1;
        //    try
        //    {
        //        OracleCommand command = new OracleCommand();
        //        command.Connection = DalHelper.Connection;
        //        command.Connection.Open();
        //        SetPage("select a.BillNo from GDS_ATT_Bill a Where 1=1 " + condition, pageSize, ref out_CurrentPage, ref out_TotalPage, ref out_TotalRecords, ref out_StartRow, ref out_EndRow);
        //        return this.GetApprovedDataByCondition(condition, out_StartRow, out_EndRow, out out_EndRow);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        private int totalPages = 0;
        private int totalRecords = 0;
        public void SetPage(string sql, int pageSize, ref int out_CurrentPage, ref int out_TotalPage, ref int out_TotalRecords, ref int out_StartRow, ref int out_EndRow)
        {
            if ((pageSize > 0) && !sql.Equals(""))
            {

                try
                {
                    OracleCommand command = new OracleCommand();
                    command.Connection = DalHelper.Connection;
                    command.Connection.Open();
                    command.CommandText = "SELECT NVL(COUNT(1),0) FROM (" + sql + ")";
                    this.totalRecords = Convert.ToInt32(command.ExecuteScalar());
                    this.totalPages = ((this.totalRecords % pageSize) == 0) ? (this.totalRecords / pageSize) : ((this.totalRecords / pageSize) + 1);
                    out_TotalPage = this.totalPages;
                    out_TotalRecords = this.totalRecords;
                    if (out_TotalPage <= 0)
                    {
                        out_TotalPage = 1;
                    }
                    if (out_CurrentPage > out_TotalPage)
                    {
                        out_CurrentPage = out_TotalPage;
                    }
                    out_StartRow = (out_CurrentPage - 1) * pageSize;
                    out_EndRow = (pageSize + out_StartRow) + 1;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public DataSet GetDataByCondition_HRM(string condition)
        {

            try
            {
                DataSet ds = new DataSet();
                string sql = "SELECT DataType,DataCode,DataValue,DataTypeDetail,(DataCode||'?B'||DataValue)as newDataValue FROM GDS_ATT_TYPEDATA " + condition;

                DataTable dt = DalHelper.ExecuteQuery(sql);
                if (dt != null)
                {
                    dt.TableName = "HRM_FixedlType";
                }
                ds.Tables.Add(dt);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// 發送郵件
        /// </summary>
        /// <param name="apl_empno">申請人工號</param>
        /// <param name="sup_empno">當前簽核主管</param>
        /// <param name="formname">表單名</param>
        /// <param name="formno">案件編號</param>
        /// <param name="dis_reason">拒簽原因</param>
        /// <returns></returns>
        private bool SendMail(string apl_empno, string sup_empno, string formname, string formno, string dis_reason,OracleCommand cmd)
        {
            try
            {
                PersonDal dal = new PersonDal();
                cmd.CommandText = " select CNAME from  gds_sc_person where PERSONCODE='" + apl_empno + "'";
                string aplname = cmd.ExecuteOracleScalar().ToString();       
                cmd.CommandText = " select CNAME from  gds_sc_person where PERSONCODE='" + sup_empno + "'";
                string supname = cmd.ExecuteOracleScalar().ToString();
                cmd.CommandText = " select MAIL from  gds_sc_person where PERSONCODE='" + apl_empno + "'";
                string mailto = cmd.ExecuteOracleScalar().ToString();
                string url = "http://10.138.4.205:100/login.aspx";
                string title = formname + "(案件編號：" + formno + ",申請人工號：" + apl_empno + ",姓名：" + aplname + ")已被主管" + supname + "主管退件通知";
                string context = string.Format(@"<html>
                                 <head>
                                  <meta http-equiv=Content-Type content=text/html; charset=iso-8859-1>  
                                 </head>
                                 <body>尊敬的
                                   {0}
                                    主管/同仁:<br />
                                      您好!<br />                              
                                         {1}（案件編號：{2}，申請人工號：{3}，姓名：{4}）已被主管（{5}）退件，退件原因：{6}；請悉知！<br />
                                      <a href={7}>點擊進入HR電子簽核系統</a> <br /><br /><br />
                                      ========================================================<br /><br />
                                      這是HR電子簽核平台自動發送的郵件,<br />
                                      若您有疑問或遇到系統使用任何問題,請及時聯系我們<br />
                                      HR客服電話:560-106<br /><br />                                      
                                      如非必要,請不要將此郵件轉呈他人,如需轉呈,請去除超鏈接                                      
                                 </body>
                                </html>", aplname, formname, formno, apl_empno, aplname, supname, dis_reason, url);
                cmd.CommandText = string.Format(@"INSERT INTO gds_wf_sendnotes
                              (mi_to,
                               mi_title,
                               mi_content,
                               active_flag, mi_cycle_type, send_null_flag, mi_name
                              )  
                             VALUES
                              ('{0}','{1}','{2}','Y','Day','N', 'Hr電子簽核系統'
                                )", mailto, title, context);
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }



    }
}
