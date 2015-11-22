/*
 * Copyright (C) 2012 GDSBG MIABU 版權所有。
 * 
 * 檔案名： WorkFLowLimitModel.cs
 * 檔功能描述： 未刷補卡
 * 
 * 版本：1.0
 * 創建標識： 劉小明 2012.02.02
 * 
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OracleClient;

using GDSBG.MiABU.Attendance.Model.WorkFlow;
using GDSBG.MiABU.Attendance.IDAL.WorkFlow;
using GDSBG.MiABU.Attendance.Common;
using System.Data;
using System.Data.OleDb;
using System.Collections;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;


namespace GDSBG.MiABU.Attendance.OracleDAL.WorkFlow
{
    public class WorkFlowCardMakeupDal : DALBase<WorkFlowCardMakeupModel>, IWorkFlowCardMakeupDal
    {
        /// <summary>
        /// 未刷補卡列表
        /// </summary>
        /// <param name="condition">查詢條件</param>
        /// <param name="pageIndex">頁面索引值</param>
        /// <param name="pageSize">頁面指定數目條數</param>
        /// <param name="totalCount">總記錄數</param>
        /// <param name="parameters">條件</param>
        /// <returns>未刷補卡統計數據</returns>
        public DataTable CardMakeupList(string condition, int pageIndex, int pageSize, out int totalCount)
        {

            // string cardSql = "SELECT B.LOCALNAME,B.SEX,B.DEPCODE,B.DEPNAME,A.* FROM GDS_ATT_MAKEUP A LEFT JOIN GDS_ATT_EMPLOYEE B ON A.WORKNO = B.WORKNO  where 1=1 " + condition;
            string cardSql = " select *  FROM  ( SELECT a.DISSIGNRMARK,a.ID, a.workno,to_char(a.kqdate,'yyyy/mm/dd') kqdate,to_char( a.cardtime,'HH24:MI:SS') cardtime,  a.makeuptype, a.status,a.Reasontype,a.ReasonRemark, a.approver, a.apremark, a.approvedate, a.billno, (select LocalName From gds_att_Employee e Where e.WorkNo=a.modifier)modifier ,a.DecSalary, (select (select ShiftNo||':'||ShiftDesc||'['||shifttype||']' from gds_att_WorkShift c where c.ShiftNo=w.ShiftNo) from gds_att_KaoQinData w where w.workno=a.workno and w.KQdate=a.KQdate)  ShiftDesc,  (SELECT depname FROM gds_sc_department s WHERE s.LevelCode='2' START WITH s.depcode=b.depcode CONNECT BY s.depcode = PRIOR s.parentdepcode) buname, (Select ReasonName from gds_att_EXCEPTREASON b where b.REASONNO=a.Reasontype) ReasonName,  (Select SALARYFLAG from gds_att_EXCEPTREASON b where b.REASONNO=a.Reasontype) SalaryFlag,  (SELECT DATAVALUE FROM gds_att_TYPEDATA b WHERE b.DATATYPE='KQMMakeup' and b.DATACODE=a.makeuptype) MakeupTypeName,  (SELECT DATAVALUE FROM gds_att_TYPEDATA b WHERE b.DATATYPE='ApproveFlag' and b.DATACODE=a.Status) StatusName,  (select LocalName From gds_att_Employee e Where e.WorkNo=upper(a.Approver)) ApproverName, a.modifydate,b.localname,b.depname,b.dname,b.dcode,b.depcode FROM gds_att_Makeup a inner join  gds_att_employee b on  a.workno=b.workno(+) where 1=1 " + condition + "  )";
            //return DalHelper.ExecuteQuery(cardSql);
            return DalHelper.ExecutePagerQuery(cardSql, pageIndex, pageSize, out totalCount, null);

        }


        public DataTable CardMakeupListNoPage(string condition)
        {

            // string cardSql = "SELECT B.LOCALNAME,B.SEX,B.DEPCODE,B.DEPNAME,A.* FROM GDS_ATT_MAKEUP A LEFT JOIN GDS_ATT_EMPLOYEE B ON A.WORKNO = B.WORKNO  where 1=1 " + condition;
            string cardSql = " select Id, workno, to_char(kqdate,'yyyy/mm/dd') kqdate, to_char(cardtime,'HH24:MI:SS')cardtime, makeuptype, status, approver, apremark,approvedate, billno, modifier, modifydate, reasontype, reasonremark,decsalary, dissignrmark, shiftdesc, buname, reasonname, salaryflag,makeuptypename, statusname, approvername, localname, depname, dname  from gds_att_makeup_v a where 1=1 " + condition + "   ";
            //return DalHelper.ExecuteQuery(cardSql);
            return DalHelper.ExecuteQuery(cardSql);


        }

        /// <summary>
        /// 獲取員工信息（暫時不增加權限管控）
        /// </summary>
        /// <param name="EmployeeNo"></param>
        /// <param name="sqlDep">權限</param>
        /// <returns></returns>
        public DataTable GetVData(string EmployeeNo, string sqlDep)
        {
            DataTable dt = DalHelper.ExecuteQuery("select * from gds_att_employee a where a.workno=:empNo ", new OracleParameter(":empNo", EmployeeNo));
            return dt;
        }

        /// <summary>
        /// 數據更新操作方法
        /// </summary>
        /// <param name="flag">標志位，“Add” 表示新增；“Modify” 表示修改</param>
        /// <param name="workno">工號</param>
        /// <param name="kqdate">考勤日</param>
        /// <param name="cardtime">刷卡時間</param>
        /// <param name="makeuptype">補卡類別 ： 0 補上班卡；1 補下班卡；2 補加班上班卡；3 補加班下班卡</param>
        /// <param name="status">狀態：0未核準；1 簽核中；2 已核準；3 拒簽</param>
        /// <param name="approver">審核者</param>
        /// <param name="apremark">審核說明</param>
        /// <param name="approvedate">審核日期</param>
        /// <param name="billno">單號</param>
        /// <param name="modifier">維護者</param>
        /// <param name="modifydate">維護日期</param>
        /// <param name="reasontype">異常類別</param>
        /// <param name="reasonremark">異常原因</param>
        /// <param name="decsalary">是否記錄扣薪：Y/N</param>
        /// <returns></returns>
        public bool modifyCardMakeupInfo(string flag, string workno, string kqdate, string cardtime, string makeuptype, string status, string approver, string apremark, string approvedate, string billno, string modifier, string modifydate, string reasontype, string reasonremark, string decsalary)
        {
            //string str = "INSERT INTO GDS_ATT_ORGSHIFT ( ORGCODE, shiftno, startdate, enddate,update_user, update_date ) VALUES ('" + depcode + "','" + shiftno + "',to_date('" + startdate + "','yyyy/mm/dd'),to_date('" + endate + "','yyyy/mm/dd'),'" + personcode + "',sysdate)";
            //return DalHelper.ExecuteNonQuery(str) != -1;
            return false;
        }


        public void SaveData(string processFlag, DataTable dataTable)
        {
            foreach (DataRow newRow in dataTable.Rows)
            {
                if (processFlag.Equals("Add"))
                {
                    string sql = string.Concat(new object[] { 
                            "INSERT INTO gds_att_Makeup(workno, kqdate, cardtime, makeuptype,Reasontype,ReasonRemark, status,DecSalary, modifier,modifydate) VALUES('", newRow["WorkNo"], "',to_date('", Convert.ToDateTime(newRow["kqdate"]).ToString("yyyy/MM/dd"), "','yyyy/mm/dd'),to_date('", Convert.ToDateTime(newRow["cardtime"]).ToString("yyyy/MM/dd HH:mm"), "','yyyy/mm/dd hh24:mi:ss'),'", newRow["makeuptype"], "','", newRow["Reasontype"], "','", newRow["ReasonRemark"], "','0','", newRow["DecSalary"], "','", newRow["Modifier"], "',sysdate)"
                         });
                    DalHelper.ExecuteNonQuery(sql);
                    
                }
                else if (processFlag.Equals("Modify"))
                {
                    string sql = string.Concat(new object[] { 
                            "UPDATE gds_att_Makeup SET kqdate=to_date('", Convert.ToDateTime(newRow["kqdate"]).ToString("yyyy/MM/dd"), "','yyyy/mm/dd'),cardtime=to_date('", Convert.ToDateTime(newRow["cardtime"]).ToString("yyyy/MM/dd HH:mm"), "','yyyy/mm/dd hh24:mi:ss'),makeuptype='", newRow["makeuptype"], "',Reasontype='", newRow["Reasontype"], "',ReasonRemark='", newRow["ReasonRemark"], "',status='", newRow["status"], "',DecSalary='", newRow["DecSalary"], "',Modifier='", newRow["Modifier"], "',ModifyDate=sysdate WHERE ID='", newRow["ID"], "' "
                         });
                    DalHelper.ExecuteNonQuery(sql);
                    
                }
            }
        }

        protected string logText = "";

        public void Audit(DataTable dataTable)
        {

            foreach (DataRow AuditRow in dataTable.Rows)
            {
                string CommandText1 = string.Concat(new object[] { "UPDATE gds_att_Makeup Set Status='2',Approver='", "", "',ApproveDate=sysdate WHERE ID='", AuditRow["ID"], "' " });
                DalHelper.ExecuteNonQuery(CommandText1);
                // base.SaveLogData("U", AuditRow["WorkNo"].ToString(), base.command.CommandText);
                string CommandText2 = string.Concat(new object[] { "insert into gds_att_BellCardData(cardno, cardtime, bellno, readtime, processflag, workno) SELECT (select nvl(cardno,'", AuditRow["WorkNo"], "') from gds_att_employee where workno='", AuditRow["WorkNo"], "'), CardTime, 'SIGNMAKEUP',sysdate,'N','", AuditRow["WorkNo"], "' FROM gds_att_Makeup WHERE ID='", AuditRow["ID"], "' " });
                DalHelper.ExecuteNonQuery(CommandText2);
                //base.SaveLogData("U", AuditRow["WorkNo"].ToString(), base.command.CommandText);
            }
            // base.command.Transaction.Commit();
            // base.command.Transaction = null;

        }

        public void CancelAudit(DataTable dataTable)
        {
            foreach (DataRow AuditRow in dataTable.Rows)
            {
                string CommandText1 = "UPDATE gds_att_Makeup Set Status='0' WHERE ID='" + AuditRow["ID"] + "' ";
                DalHelper.ExecuteNonQuery(CommandText1);
                // base.SaveLogData("U", AuditRow["WorkNo"].ToString(), base.command.CommandText);
                string CommandText2 = string.Concat(new object[] { "DELETE FROM gds_att_BellCardData WHERE WorkNo='", AuditRow["WorkNo"], "' and CardTime=to_date('", Convert.ToDateTime(AuditRow["CardTime"]).ToString("yyyy/MM/dd HH:mm"), "','yyyy/mm/dd hh24:mi:ss') and bellno='SIGNMAKEUP'" });
                DalHelper.ExecuteNonQuery(CommandText2);
                //   base.SaveLogData("D", AuditRow["WorkNo"].ToString(), base.command.CommandText);
            }


        }

        public void DeleteData(DataTable dataTable)
        {
            foreach (DataRow deletedRow in dataTable.Rows)
            {
                string CommandText = "DELETE FROM gds_att_Makeup WHERE ID='" + deletedRow["ID"] + "' ";
                // base.command.ExecuteNonQuery();
                DalHelper.ExecuteNonQuery(CommandText);
                // base.SaveLogData("U", deletedRow["WorkNo"].ToString(), base.command.CommandText);
            }
        }

        public DataTable GetDataByCondition(string condition)
        {
            string sql = "SELECT * from (SELECT a.ID, a.workno, a.kqdate, a.cardtime, a.makeuptype, a.status,a.Reasontype,a.ReasonRemark, a.approver, a.apremark, a.approvedate, a.billno, (select LocalName From gds_att_employee e Where e.WorkNo=a.modifier)modifier ,a.DecSalary, (select (select ShiftNo||':'||ShiftDesc||'['||shifttype||']' from gds_att_WorkShift c where c.ShiftNo=w.ShiftNo) from gds_att_KaoQinData w where w.workno=a.workno and w.KQdate=a.KQdate)  ShiftDesc,  (SELECT depname FROM gds_sc_department s WHERE s.LevelCode='2' START WITH s.depcode=b.depcode CONNECT BY s.depcode = PRIOR s.parentdepcode) buname, (Select ReasonName from gds_att_EXCEPTREASON b where b.REASONNO=a.Reasontype) ReasonName,  (Select SALARYFLAG from gds_att_EXCEPTREASON b where b.REASONNO=a.Reasontype) SalaryFlag,  (SELECT DATAVALUE FROM gds_att_TYPEDATA b WHERE b.DATATYPE='KQMMakeup' and b.DATACODE=a.makeuptype) MakeupTypeName,  (SELECT DATAVALUE FROM gds_att_TYPEDATA b WHERE b.DATATYPE='ApproveFlag' and b.DATACODE=a.Status) StatusName,  (select LocalName From gds_att_Employee e Where e.WorkNo=upper(a.Approver)) ApproverName, a.modifydate,b.localname,b.depname,b.dname,b.dcode,b.depcode FROM gds_att_Makeup a,gds_att_Employee b  where a.workno=b.workno(+) " + condition + ")";

            return DalHelper.ExecuteQuery(sql);
        }



        public DataTable GetDataByCondition(string condition, int startRow, int endRow)
        {

            string sql = "SELECT * FROM(SELECT rownum rn,a.* FROM(SELECT a.ID, a.workno, a.kqdate, a.cardtime, a.makeuptype, a.status,a.Reasontype,a.ReasonRemark, a.approver, a.apremark, a.approvedate, a.billno,(select LocalName From gds_att_employee e Where e.WorkNo=a.modifier)modifier,a.DecSalary, (select (select ShiftNo||':'||ShiftDesc||'['||shifttype||']' from gds_att_WorkShift c where c.ShiftNo=w.ShiftNo) from gds_att_KaoQinData w where w.workno=a.workno and w.KQdate=a.KQdate)  ShiftDesc,  (SELECT depname FROM gds_sc_department s WHERE s.LevelCode='2' START WITH s.depcode=b.depcode CONNECT BY s.depcode = PRIOR s.parentdepcode) buname, (Select ReasonName from gds_att_EXCEPTREASON b where b.REASONNO=a.Reasontype) ReasonName,  (Select SALARYFLAG from gds_att_EXCEPTREASON b where b.REASONNO=a.Reasontype) SalaryFlag,  (SELECT DATAVALUE FROM gds_att_TYPEDATA b WHERE b.DATATYPE='KQMMakeup' and b.DATACODE=a.makeuptype) MakeupTypeName,  (SELECT DATAVALUE FROM gds_att_TYPEDATA b WHERE b.DATATYPE='ApproveFlag' and b.DATACODE=a.Status) StatusName,  (select LocalName From gds_att_employee e Where e.WorkNo=upper(a.Approver)) ApproverName, a.modifydate,b.localname,b.depname,b.dname,b.dcode,b.depcode FROM gds_att_Makeup a,gds_att_employee b where a.workno=b.workno(+) " + condition + " AND rownum<" + endRow.ToString() + ")a) WHERE rn > " + startRow.ToString();
            return DalHelper.ExecuteQuery(sql);

        }

        public DataTable GetMXDataByCondition(string condition)
        {
            string CommandText = "SELECT * from (SELECT a.ID, a.workno, a.kqdate, a.cardtime, a.makeuptype, a.status,a.Reasontype,a.ReasonRemark, a.approver, a.apremark, a.approvedate, a.billno, (select LocalName From gds_att_employee e Where e.WorkNo=a.modifier)modifier ,a.DecSalary, (select (select ShiftNo||':'||ShiftDesc||'['||TimeQty||']' from gds_att_WorkShift c where c.ShiftNo=w.ShiftNo) from gds_att_KaoQinData w where w.workno=a.workno and w.KQdate=a.KQdate)  ShiftDesc,  (SELECT depname FROM gds-att_department s WHERE s.LevelCode='2' START WITH s.depcode=b.depcode CONNECT BY s.depcode = PRIOR s.parentdepcode) buname, (Select ReasonName from gds_att_EXCEPTREASON b where b.REASONNO=a.Reasontype) ReasonName,  (Select SALARYFLAG from gds_att_EXCEPTREASON b where b.REASONNO=a.Reasontype) SalaryFlag,  (SELECT DATAVALUE FROM gds-att_TYPEDATA b WHERE b.DATATYPE='KQMMakeup' and b.DATACODE=a.makeuptype) MakeupTypeName,  (SELECT DATAVALUE FROM gds-att_TYPEDATA b WHERE b.DATATYPE='ApproveFlag' and b.DATACODE=a.Status) StatusName,  (select LocalName From gds_att_employee e Where e.WorkNo=upper(a.Approver)) ApproverName, a.modifydate,b.localname,b.depname,b.dname,b.dcode,b.depcode FROM gds_att_Makeup a,gds_att_employee b where a.workno=b.workno(+) " + condition + ")";
            return DalHelper.ExecuteQuery(CommandText);
        }

        public DataTable GetMXDataByCondition(string condition, int pageSize, int out_CurrentPage, int out_TotalPage, out int out_TotalRecords)
        {
            string SqlText = "SELECT a.ID, a.workno, a.kqdate, a.cardtime, a.makeuptype, a.status,a.Reasontype,a.ReasonRemark, a.approver, a.apremark, a.approvedate, a.billno, (select LocalName From gds_att_employee e Where e.WorkNo=a.modifier)modifier ,a.DecSalary, (select (select ShiftNo||':'||ShiftDesc||'['||TimeQty||']' from gds_att_WorkShift c where c.ShiftNo=w.ShiftNo) from gds_att_KaoQinData w where w.workno=a.workno and w.KQdate=a.KQdate)  ShiftDesc,  (SELECT depname FROM gds-att_department s WHERE s.LevelCode='2' START WITH s.depcode=b.depcode CONNECT BY s.depcode = PRIOR s.parentdepcode) buname, (Select ReasonName from gds_att_EXCEPTREASON b where b.REASONNO=a.Reasontype) ReasonName,  (Select SALARYFLAG from gds_att_EXCEPTREASON b where b.REASONNO=a.Reasontype) SalaryFlag,  (SELECT DATAVALUE FROM gds-att_TYPEDATA b WHERE b.DATATYPE='KQMMakeup' and b.DATACODE=a.makeuptype) MakeupTypeName,  (SELECT DATAVALUE FROM gds-att_TYPEDATA b WHERE b.DATATYPE='ApproveFlag' and b.DATACODE=a.Status) StatusName,  (select LocalName From gds_att_employee e Where e.WorkNo=upper(a.Approver)) ApproverName, a.modifydate,b.localname,b.depname,b.dname,b.dcode,b.depcode FROM gds_att_Makeup a,gds_att_employee b where a.workno=b.workno(+) " + condition;
            return DalHelper.ExecutePagerQuery(SqlText, out_CurrentPage, pageSize, out out_TotalRecords, null);
        }

        public string SaveAuditData(string processFlag, string ID, string BillNoType, string AuditOrgCode)
        {
            string strMax = ""; 
             
            string sSql = "";
            OracleCommand command = new OracleCommand();
            command.Connection = DalHelper.Connection;
            command.Connection.Open();
            OracleTransaction trans = command.Connection.BeginTransaction();
            command.Transaction = trans;
            try
            {
                if (processFlag.Equals("Add"))
                {
                    //sSql = "Select BillTypeNo From BFW_BillType Where BillTypeCode=:billnotype";
                    //BillNoType = Convert.ToString(DalHelper.ExecuteScalar(sSql, new OracleParameter(":billnotype", BillNoType))) + AuditOrgCode;

                    BillNoType = BillNoType + AuditOrgCode;
                    sSql = "SELECT MAX (billno) strMax  FROM gds_Att_Makeup WHERE billno LIKE '" + BillNoType + "'|| TO_CHAR (SYSDATE, 'yymm')|| '%'";
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
                    sSql = "UPDATE gds_Att_Makeup SET Status='1' , BillNo =  '" + strMax + "' Where ID='" + ID + "'";
                    command.CommandText = sSql;
                    command.ExecuteNonQuery();
                }
                else if (processFlag.Equals("Modify"))
                {
                    strMax = BillNoType;
                    //sSql = "UPDATE GDS_ATT_KAOQINDATA SET Status='4' , BillNo = '" + strMax + "'  Where WorkNo='" + WorkNo + "' and KQDate=to_date('" + DateTime.Parse(KQDate).ToString("yyyy/MM/dd") + "','yyyy/mm/dd')";
                    sSql = "UPDATE gds_Att_Makeup SET Status='1' , BillNo =  '" + strMax + "' Where ID='" + ID + "'";
                    command.CommandText = sSql;
                    command.ExecuteNonQuery();
                }
                trans.Commit();
                command.Connection.Close();
                return strMax;
            }
            catch (Exception ex)
            {
                trans.Rollback();
                command.Connection.Close();
                return strMax;
            }  
        }

        /// <summary>
        /// 獲取當前指定值
        /// </summary>
        /// <param name="SQL"></param>
        /// <returns></returns>
        public string GetValue(string SQL)
        {
            string sValue = "";
            DataTable dt = new DataTable();

            dt = DalHelper.ExecuteQuery(SQL);

            if (dt.Rows.Count > 0)
            {
                sValue = dt.Rows[0][0].ToString().Trim();
            }

            return sValue;
        }


        /// <summary>
        /// 查看指定班別的上下班時間
        /// </summary>
        /// <param name="WorkNo">工號</param>
        /// <param name="KQDate">考勤日</param>
        /// <param name="dCardTime">刷卡時間</param>
        /// <param name="ShiftNo">班別</param>
        /// <param name="MakeType">補卡類型</param>
        /// <returns>返回刷卡時間</returns>
        public string ReturnCardTime(string WorkNo, string KQDate, string dCardTime, string ShiftNo, string MakeType)
        {
            string condition = "";
            condition = "select OnDutyTime,OffDutyTime from gds_att_WorkShift where ShiftNo='" + ShiftNo + "'";
            DataTable sdt = DalHelper.ExecuteQuery(condition);
            if (sdt.Rows.Count != 0)
            {
                string dtShiftOnTime;
                string dtShiftOffTime;
                string CSs4s0003;
                string ShiftOnTime = Convert.ToString(sdt.Rows[0]["OnDutyTime"]);
                string ShiftOffTime = Convert.ToString(sdt.Rows[0]["OffDutyTime"]);
                if (TimeSpan.Parse(ShiftOnTime) < TimeSpan.Parse(ShiftOffTime))
                {
                    dtShiftOnTime = DateTime.Parse(KQDate + " " + ShiftOnTime).ToString("yyyy/MM/dd HH:mm");
                    dtShiftOffTime = DateTime.Parse(KQDate + " " + ShiftOffTime).ToString("yyyy/MM/dd HH:mm");
                }
                else
                {
                    dtShiftOnTime = DateTime.Parse(KQDate + " " + ShiftOnTime).ToString("yyyy/MM/dd HH:mm");
                    dtShiftOffTime = DateTime.Parse(KQDate + " " + ShiftOffTime).AddDays(1.0).ToString("yyyy/MM/dd HH:mm");
                }
                if (TimeSpan.Parse(ShiftOnTime) >= TimeSpan.Parse(ShiftOffTime))
                {
                    CSs4s0003 = MakeType;
                    if (CSs4s0003 != null)
                    {
                        if (!(CSs4s0003 == "0"))
                        {
                            if (((CSs4s0003 == "1") || (CSs4s0003 == "2")) || (CSs4s0003 == "3"))
                            {
                                if (string.Compare(Convert.ToDateTime(dtShiftOnTime).AddHours(-3.0).ToString("yyyy/MM/dd HH:mm"), dCardTime) > 0)
                                {
                                    dCardTime = Convert.ToDateTime(dCardTime).AddDays(1.0).ToString("yyyy/MM/dd HH:mm");
                                }
                                return dCardTime;
                            }
                        }
                        else if (string.Compare(dCardTime, Convert.ToDateTime(dtShiftOffTime).AddDays(-1.0).ToString("yyyy/MM/dd HH:mm")) < 0)
                        {
                            dCardTime = Convert.ToDateTime(dCardTime).AddDays(1.0).ToString("yyyy/MM/dd HH:mm");
                        }
                    }
                    return dCardTime;
                }
                CSs4s0003 = MakeType;
                if (CSs4s0003 != null)
                {
                    if (!(CSs4s0003 == "0"))
                    {
                        if (((CSs4s0003 == "1") || (CSs4s0003 == "2")) || (CSs4s0003 == "3"))
                        {
                            if (string.Compare(Convert.ToDateTime(dtShiftOnTime).AddHours(-3.0).ToString("yyyy/MM/dd HH:mm"), dCardTime) > 0)
                            {
                                dCardTime = Convert.ToDateTime(dCardTime).AddDays(1.0).ToString("yyyy/MM/dd HH:mm");
                            }
                            return dCardTime;
                        }
                    }
                    else if (string.Compare(dtShiftOnTime, Convert.ToDateTime(KQDate + " 00:00").ToString("yyyy/MM/dd HH:mm")) == 0)
                    {
                        dCardTime = Convert.ToDateTime(dCardTime).AddDays(-1.0).ToString("yyyy/MM/dd HH:mm");
                    }
                }
            }
            return dCardTime;
        }


        public DataTable KQMExceptionList(string Condition)
        {
            string sql = "select * from GDS_ATT_EXCEPTREASON where 1=1 " + Condition;
            return DalHelper.ExecuteQuery(sql);
        }

        /// <summary>
        /// 查看考勤記錄
        /// </summary>
        /// <param name="condition">查詢條件</param>
        /// <returns></returns>
        public DataTable GetKaoQinDataByCondition(string condition)
        {

            string commandText = "SELECT * FROM (SELECT a.WORKNO,a.KQDATE,a.ShiftNo,b.LocalName,a.BillNo,a.Approver,a.ApproveDate,a.ApRemark,  b.dname depname,b.dcode,TO_CHAR(a.OTOnDutyTime,'hh24:mi') OTOnDutyTime,TO_CHAR(a.OTOffDutyTime,'hh24:mi') OTOffDutyTime,  (select ShiftNo||':'||ShiftDesc||'['||shifttype||']' from gds_att_WorkShift c where c.ShiftNo=a.ShiftNo) ShiftDesc,  TO_CHAR(a.ONDUTYTIME,'hh24:mi') AS ONDUTYTIME,TO_CHAR(a.OFFDUTYTIME,'hh24:mi') OFFDUTYTIME,  TRIM(TO_CHAR(CASE WHEN a.ABSENTQTY=0 THEN NULL ELSE a.ABSENTQTY END,'999')) ABSENTQTY,  a.STATUS,a.EXCEPTIONTYPE,a.REASONTYPE,a.REASONREMARK,  (Select ReasonName from gds_att_EXCEPTREASON b where b.REASONNO=a.REASONTYPE) ReasonName,  (Select DATAVALUE FROM gds_att_TYPEDATA c WHERE c.DataType='ExceptionType' and c.DataCode=a.ExceptionType) ExceptionTypeName,  (SELECT DATAVALUE FROM gds_att_TYPEDATA b WHERE b.DATATYPE='KqmKaoQinStatus' and b.DATACODE=a.STATUS) StatusName,  (CASE when (select count(1) from gds_att_makeup c where c.workno=a.workno and c.kqdate=a.kqdate and c.Status='2')>0 THEN 'Y' ELSE 'N' END) ISMakeUp,(CASE WHEN a.OTHOURS-TRUNC(a.OTHOURS)>0.5 THEN TRUNC(a.OTHOURS)+0.5       WHEN OTHOURS-TRUNC(a.OTHOURS)=0.5 THEN a.OTHOURS       WHEN OTHOURS-TRUNC(a.OTHOURS)<0.5 THEN TRUNC(a.OTHOURS) END ) as OTHOURS,  round((a.OFFDUTYTIME-a.ONDUTYTIME)*24,1) AS TOTALHOURS,'0' AbsentTotal FROM gds_att_KAOQINDATA a, gds_att_employee b where b.WorkNo=a.WorkNo " + condition + ")";

            return DalHelper.ExecuteQuery(commandText);

        }

        /// <summary>
        /// 將datatable轉換為List
        /// </summary>
        /// <param name="dt">徐轉化的Datatable</param>
        /// <returns></returns>
        public List<WorkFlowCardMakeupModel> GetList(DataTable dt)
        {
            return OrmHelper.SetDataTableToList(dt);
        }

        /// <summary>
        /// 查詢SQL語句
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public DataTable getTBDataByCondition(string condition)
        {
            return DalHelper.ExecuteQuery(condition);
        }

        //判斷數據是否有效
        public int GetVWorkNoCount(string WorkNo, string sqlDep)
        {
            int iValue = 0;
            string sqlDepStr = "";
            if (sqlDep != "")  //sqlDep = " 'D00200004'";

             sqlDepStr = " and Dcode IN(" + sqlDep + ")";

            try
            {
                string CommandText = "select NVL(count(WorkNO),0) from gds_att_Employee where WorkNO='" + WorkNo + "' and status='0' " + sqlDepStr;

                iValue = Convert.ToInt32(DalHelper.ExecuteScalar(CommandText));
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iValue;
        }

        public string GetWorkFlowOrgCode(string OrgCode, string BillTypeCode)
        {
            string ReturnOrgCode = "";
            if (OrgCode != "")
            {
                string commandText = "SELECT depcode FROM (SELECT   b.depcode, a.orderid FROM gds_wf_flowset a, (SELECT LEVEL orderid, depcode FROM gds_sc_department  START WITH depcode = '" + OrgCode + "' CONNECT BY PRIOR parentdepcode = depcode  ORDER BY LEVEL) b  WHERE a.deptcode = b.depcode  AND a.FORMTYPE = '" + BillTypeCode + "'  ORDER BY a.orderid) WHERE ROWNUM <= 1 ";

                DataTable dt = DalHelper.ExecuteQuery(commandText);
                if (dt.Rows.Count > 0)
                {
                    ReturnOrgCode = dt.Rows[0][0].ToString().Trim();
                }
            }

            return ReturnOrgCode;
        }

        public void SaveData(string BillNo, string OrgCode, string ApplyMan)
        {
            bool result = false;
            string sSql = "";
            OracleCommand command = new OracleCommand();
            command.Connection = DalHelper.Connection;
            command.Connection.Open();
            OracleTransaction trans = command.Connection.BeginTransaction();
            command.Transaction = trans;

            string CommandText = "SELECT count(1) FROM GDS_ATT_BILL WHERE BillNo='" + BillNo + "'";
            if (Convert.ToDecimal(DalHelper.ExecuteScalar(CommandText)) == 0M)
            {
                string commandText = "insert into GDS_ATT_BILL(BillNo,OrgCode,ApplyMan,ApplyDate,Status) values('" + BillNo + "','" + OrgCode + "','" + ApplyMan + "',sysdate,'0')";
                DalHelper.ExecuteNonQuery(commandText);
               // SaveLogData("I", BillNo, command.CommandText, command, logmodel);
            }
            else
            {
                string CommandText2 = "update GDS_ATT_BILL set OrgCode='" + OrgCode + "',ApplyMan='" + ApplyMan + "',ApplyDate=sysdate,Status='0' where BillNo='" + BillNo + "'";
                DalHelper.ExecuteNonQuery(CommandText2);
               // SaveLogData("U", BillNo, command.CommandText, command, logmodel);
            }
            trans.Commit();
            command.Connection.Close();
            result = true;

        }

        /// <summary>
        /// 保存簽核狀態
        /// </summary>
        /// <param name="BillNo"></param>
        /// <param name="OrgCode"></param>
        /// <param name="BillTypeCode"></param>
        public void SaveAuditStatusData(string BillNo, string OrgCode, string BillTypeCode)
        {
            string CommandText3 = "SELECT count(1) FROM GDS_ATT_AUDITSTATUS WHERE BillNo='" + BillNo + "'";
            if (Convert.ToDecimal(DalHelper.ExecuteScalar(CommandText3)) > 0M)
            {
                string CommandText2 = "delete FROM GDS_ATT_AUDITSTATUS WHERE BillNo='" + BillNo + "' ";
                DalHelper.ExecuteNonQuery(CommandText2);
                // base.SaveLogData("D", "", base.command.CommandText);
            }
          //string CommandText1 = "insert into GDS_ATT_AUDITSTATUS(BillNo,AuditMan,OrderNo,AuditStatus,SendNotes,AuditManType) SELECT '" + BillNo + "', AuditMan, OrderNo,'0','N',AuditManType   FROM WFM_BillAuditFlow WHERE BillTypeCode='" + BillTypeCode + "' and OrgCode='" + OrgCode + "' ";
            string CommandText1 = "insert into GDS_ATT_AUDITSTATUS(BillNo,AuditMan,OrderNo,AuditStatus,SendNotes) SELECT '" + BillNo + "', FLOW_EMPNO, ORDERID,'0','N'   FROM GDS_WF_FLOWSET WHERE FORMTYPE='" + BillTypeCode + "' and DEPTCODE='" + OrgCode + "'";
              
            DalHelper.ExecuteNonQuery(CommandText1);
            //base.SaveLogData("I", "", base.command.CommandText);
            string CommandText = "Update GDS_ATT_BILL set BillTypeCode='" + BillTypeCode + "' WHERE BillNo='" + BillNo + "' ";
            DalHelper.ExecuteNonQuery(CommandText);


        }
        public bool  SaveAuditData_new(string WorkNo, string BillNoType, string BillTypeCode, string ApplyMan, string AuditOrgCode, string Flow_LevelRemark, OracleConnection OracleString, SynclogModel logmodel)
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
                sSql = "SELECT MAX (billno) strMax  FROM GDS_ATT_MAKEUP WHERE billno LIKE '" + BillNoType + "'|| TO_CHAR (SYSDATE, 'yymm')|| '%'";
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
                sSql = "UPDATE GDS_ATT_MAKEUP SET Status='1' , BillNo = '" + strMax + "'  Where WorkNo='" + WorkNo + "' ";
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

        public bool SaveAuditData_new(string WorkNo, string BillNoType, string BillTypeCode, string ApplyMan, string AuditOrgCode, string Flow_LevelRemark, SynclogModel logmodel)
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
                sSql = "SELECT MAX (billno) strMax  FROM GDS_ATT_MAKEUP WHERE billno LIKE '" + BillNoType + "'|| TO_CHAR (SYSDATE, 'yymm')|| '%'";
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
                sSql = "UPDATE GDS_ATT_MAKEUP SET Status='1' , BillNo = '" + strMax + "'  Where WorkNo='" + WorkNo + "'";
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

        /// <summary>
        /// 組織送簽
        /// </summary>
        /// <param name="processFlag"></param>
        /// <param name="diry"></param>
        /// <param name="BillNoType"></param>
        /// <param name="BillTypeCode"></param>
        /// <param name="Person"></param>
        /// <param name="logmodel"></param>
        /// <returns></returns>
        public int SaveOrgAuditData(string processFlag, Dictionary<string, List<string>> diry, string BillNoType, string BillTypeCode, string Person, SynclogModel logmodel)
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
                        sSql = "SELECT nvl(MAX (billno),'0') strMax  FROM GDS_ATT_MAKEUP WHERE billno LIKE '" + BillNoType + "'|| TO_CHAR (SYSDATE, 'yymm')|| '%'";
                        //     sSql = "SELECT MAX (billno) strMax  FROM GDS_ATT_KAOQINDATA WHERE billno LIKE '" + BillNoType + "'|| TO_CHAR (SYSDATE, 'yymm')|| '%'";
                        command.CommandText = sSql;
                        strMax = Convert.ToString(command.ExecuteScalar());
                        if (strMax == "0")
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
                        num = Convert.ToString(command.ExecuteScalar());
                        string sql4 = "SELECT count(1) num FROM GDS_ATT_AUDITSTATUS WHERE BillNo='" + strMax + "'";
                        command.CommandText = sql4;
                        num1 = num = Convert.ToString(command.ExecuteScalar());

                        foreach (string ID in diry[key])
                        {
                            sSql = "UPDATE  GDS_ATT_MAKEUP SET Status='1' , BillNo =  '" + strMax + "' Where ID='" + ID + "'";
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
                        command.CommandText = "insert into GDS_ATT_AUDITSTATUS(BillNo,AuditMan,OrderNo,AuditStatus,SendNotes,OLDAUDITMAN) SELECT '" + strMax + "', nvl(getagentempno(FLOW_EMPNO,'" + AuditOrgCode + "'),FLOW_EMPNO) as FLOW_EMPNO, ORDERID,'0','N',decode(NVL (getagentempno (flow_empno, '" + AuditOrgCode + "'), flow_empno),flow_empno,'',flow_empno) as oldauditman   FROM GDS_WF_FLOWSET WHERE FORMTYPE='" + BillTypeCode + "' and DEPTCODE='" + AuditOrgCode + "' ";
                        command.ExecuteNonQuery();
                        SaveLogData("I", strMax, command.CommandText, command, logmodel);
                    }
                    else if (processFlag.Equals("Modify"))
                    {
                        foreach (string ID in diry[key])
                        {
                            strMax = BillNoType;
                            sSql = "UPDATE GDS_ATT_MAKEUP SET Status='1' , BillNo =  '" + strMax + "' Where ID='" + ID + "'";
                            command.ExecuteNonQuery();
                            SaveLogData("U", strMax, command.CommandText, command, logmodel);
                        }
                    }
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

          /// <summary>
        /// 保存操作日志
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
                //int i = DalHelper.ExecuteNonQuery(sql);

            }
            catch (Exception)
            {
            }

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
