﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GDSBG.MiABU.Attendance.Model.WorkFlow;
using GDSBG.MiABU.Attendance.IDAL.WorkFlow;
using System.Data;
using GDSBG.MiABU.Attendance.OracleDAL;
using System.Data.OracleClient;
using System.Data.OleDb;
using System.Resources;
using System;
using System.Collections;
using System.Configuration;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;

namespace GDSBG.MiABU.Attendance.OracleDAL.WorkFlow
{

    public class WFProjectApplyDal : DALBase<WorkFlowApplyModel>, IWFProjectApplyDal
    {
        public DataTable GetWorkFlowApplyData(string condition, int pageIndex, int pageSize, out int totalCount)
        {
            string SqlText = @" SELECT * FROM (  SELECT a.*,to_char(a.OTDate,'DY') as Week,TRIM(INITCAP (TO_CHAR (a.OTDate,'day','NLS_DATE_LANGUAGE=American'))) EnWeek, 
                                      (case when a.OTType='G1' and instr(a.Status,'2')=0 then a.Hours else 0 end) G1Total,
                                      (case when a.OTType='G2' and instr(a.Status,'2')=0 then a.Hours else 0 end) G2Total,
                                      (case when a.OTType in('G3','G4') and instr(a.Status,'2')=0 then a.Hours else 0 end) G3Total, 
                                      b.LocalName,b.OverTimeType,b.DCode, b.LevelName,b.ManagerName, 
                                      (select datavalue from GDS_ATT_TYPEDATA c where c.DataType='OTMAdvanceApplyStatus' and c.DataCode=a.Status) StatusName,
                                      b.dname DepName, (SELECT depname FROM GDS_SC_DEPARTMENT s WHERE s.LevelCode='2'
                                      START WITH s.depcode=b.depcode CONNECT BY s.depcode = PRIOR s.parentdepcode ) buname, 
                                      (select datavalue from GDS_ATT_TYPEDATA where datatype='EMPtype' 
                                      and datacode=substr((select e.POSTCODE from gds_att_employees e where e.workno=b.workno),0,1)) persontype,
                                      (select LocalName From GDS_ATT_EMPLOYEE e Where e.WorkNo=a.update_user)modifyName, 
                                      (select LocalName From GDS_ATT_EMPLOYEE e Where e.WorkNo=upper(a.Approver)) ApproverName 
                                      from GDS_ATT_ADVANCEAPPLY a,GDS_ATT_EMPLOYEE b where b.WorkNo=a.WorkNo " + condition + " ) ORDER BY Week DESC";
            DataTable dt = DalHelper.ExecutePagerQuery(SqlText, pageIndex, pageSize, out totalCount, null);
            return dt;
        }

        public DataSet GetApplyDataByCondition(string condition)
        {
            DataSet ds = new DataSet();
            try
            {
                string CommandText = @"SELECT * from (SELECT a.*,to_char(a.OTDate,'DY') as Week,TRIM(INITCAP (TO_CHAR (a.OTDate,'day','NLS_DATE_LANGUAGE=American'))) EnWeek, 
                                  (case when a.OTType='G1' and instr(a.Status,'2')=0 then a.Hours else 0 end) G1Total,(case when a.OTType='G2' and instr(a.Status,'2')=0 then a.Hours else 0 end) G2Total,
                                  (case when a.OTType in('G3','G4') and instr(a.Status,'2')=0 then a.Hours else 0 end) G3Total, b.LocalName,b.OverTimeType,b.DCode,b.LevelName,b.ManagerName, 
                                  (select datavalue from GDS_ATT_TYPEDATA c where c.DataType='OTMAdvanceApplyStatus' and c.DataCode=a.Status) StatusName, b.dname DepName,
                                  (SELECT depname FROM GDS_SC_DEPARTMENT s WHERE s.LevelCode='2' START WITH s.depcode=b.depcode CONNECT BY s.depcode = PRIOR s.parentdepcode ) buname, 
                                  (select datavalue from GDS_ATT_TYPEDATA where datatype='EMPtype' and datacode=substr((select e.POSTCODE from GDS_ATT_EMPLOYEES e where e.workno=b.workno),0,1)) persontype,
                                  (select LocalName From GDS_ATT_EMPLOYEES e Where e.WorkNo=a.update_user)modifyName, (select LocalName From GDS_ATT_EMPLOYEES e Where e.WorkNo=upper(a.Approver)) ApproverName 
                                  from GDS_ATT_ADVANCEAPPLY a,GDS_ATT_EMPLOYEE b where b.WorkNo=a.WorkNo " + condition + ")";
                DataTable dt = DalHelper.ExecuteQuery(CommandText);
                dt.TableName = "OTM_AdvanceApply";
                ds.Tables.Add(dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet GetRealDataByCondition(string condition)
        {
           DataSet ds = new DataSet();
           try
           {
               string strsql = @"SELECT * FROM (SELECT b.dname, a.workno, b.localname, b.overtimetype,  a.otdate,TO_CHAR(a.otdate, 'd') week, a.ottype, (select ShiftNo||':'||ShiftDesc from GDS_ATT_WORKSHIFT c where c.ShiftNo=a.ShiftNo) kqshift, (   TO_CHAR (a.ondutytime, 'HH24:mi') ||'-'|| TO_CHAR (a.offdutytime, 'HH24:mi') ) kqtime,  TO_CHAR (a.begintime, 'HH24:mi') begintime,TO_CHAR (a.endtime, 'HH24:mi') endtime,  a.realhours, a.workdesc, a.id,a.remark, a.remarks,b.dcode, (SELECT depname FROM GDS_SC_DEPARTMENT s WHERE s.LevelCode='2' START WITH s.depcode=b.depcode CONNECT BY s.depcode = PRIOR s.parentdepcode ) buname, (select datavalue from GDS_ATT_TYPEDATA c where c.DataType='DiffReason' and c.DataCode=a.diffreason) diffreasonName, (select datavalue from GDS_ATT_TYPEDATA c where c.DataType='ApproveFlag' and c.DataCode=a.Status) StatusName, a.Modifier,a.ModifyDate,a.Status,a.Approver,a.ApproveDate,a.diffreason,a.AdvanceHours, a.ConfirmHours,a.OTMSGFlag,a.RealType,a.ShiftNo,a.OnDutyTime,a.OffDutyTime,a.BillNo FROM GDS_ATT_REALAPPLY a,GDS_ATT_employee b where b.workno = a.workno " + condition + ")";
               DataTable dt = DalHelper.ExecuteQuery(strsql);
               dt.TableName = "OTM_RealApply";
               ds.Tables.Add(dt);
           }
           catch (Exception ex)
           {
               throw ex;
           }
           return ds;
    }

        public DataSet GetDataByCondition(string condition)
        {
            DataSet ds = new DataSet();
            try
            {
                string sql = "SELECT DataType,DataCode,DataValue,DataTypeDetail,(DataCode||'?B'||DataValue)as newDataValue FROM GDS_ATT_TYPEDATA " + condition;
                DataTable dt = DalHelper.ExecuteQuery(sql);
                dt.TableName = "OTM_AdvanceApply";
                ds.Tables.Add(dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet GetFixedDataByCondition(string condition)
        {
            DataSet ds = new DataSet();
            try
            {
                string sql = "SELECT DataType,DataCode,DataValue,DataTypeDetail,(DataCode||'?B'||DataValue)as newDataValue FROM GDS_ATT_TYPEDATA " + condition;
                DataTable dt = DalHelper.ExecuteQuery(sql);
                dt.TableName = "HRM_FixedlType";
                ds.Tables.Add(dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet GetDataByCondition_1(string condition)
        {
            DataSet ds = new DataSet();
            try
            {
                string sql = @"SELECT * from (SELECT a.*,to_char(a.OTDate,'DY') as Week,TRIM(INITCAP (TO_CHAR (a.OTDate,'day','NLS_DATE_LANGUAGE=American'))) EnWeek, 
                               (case when a.OTType='G1' and instr(a.Status,'2')=0 then a.Hours else 0 end) G1Total,
                               (case when a.OTType='G2' and instr(a.Status,'2')=0 then a.Hours else 0 end) G2Total,
                               (case when a.OTType in('G3','G4') and instr(a.Status,'2')=0 then a.Hours else 0 end) G3Total,
                               b.LocalName,b.OverTimeType,b.DCode,b.LevelName,b.ManagerName, (select datavalue from GDS_ATT_TYPEDATA c where c.DataType='OTMAdvanceApplyStatus' and c.DataCode=a.Status) StatusName, 
                               b.dname DepName, (SELECT depname FROM GDS_SC_DEPARTMENT s WHERE s.LevelCode='2' START WITH s.depcode=b.depcode CONNECT BY s.depcode = PRIOR s.parentdepcode ) buname, 
                               (select datavalue from GDS_ATT_TYPEDATA where datatype='EMPtype' and datacode=substr((select e.POSTCODE from GDS_ATT_EMPLOYEES e where e.workno=b.workno),0,1)) persontype,
                               (select LocalName From GDS_ATT_EMPLOYEE e Where e.WorkNo=a.UPDATE_USER)modifyName, (select LocalName From GDS_ATT_EMPLOYEE e Where e.WorkNo=upper(a.Approver)) ApproverName
                               from GDS_ATT_ADVANCEAPPLY a,GDS_ATT_EMPLOYEE b where b.WorkNo=a.WorkNo " + condition + ")";
                DataTable dt = DalHelper.ExecuteQuery(sql);
                dt.TableName = "OTM_AdvanceApply";
                ds.Tables.Add(dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
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



        public bool DeleteData(DataTable dataTable, SynclogModel logmodel)
        {
            bool result = false;
            OracleCommand command = new OracleCommand();
            command.Connection = DalHelper.Connection;
            command.Connection.Open();
            OracleTransaction trans = command.Connection.BeginTransaction();
            try
            {
                command.Transaction = trans;
                foreach (DataRow deletedRow in dataTable.Rows)
                {
                     //command.CommandText = "DELETE FROM GDS_ATT_ADVANCEAPPLY WHERE ID='" + deletedRow["ID"] + "' ";
                     //int i = DalHelper.ExecuteNonQuery(sql);
                     command.CommandText = "DELETE FROM GDS_ATT_ADVANCEAPPLY WHERE ID='" + deletedRow["ID"] + "' ";
                     command.ExecuteNonQuery();
                     SaveLogData("D", deletedRow["WorkNo"].ToString(), command.CommandText, command,logmodel);
                }
                trans.Commit();
                command.Connection.Close();
                result = true;
            }
            catch (Exception ex)
            {
                trans.Rollback();
                result = false;
                command.Connection.Close();
            }
            return result;
        }

        public DataSet GetVDataByCondition(string condition)
        {
            try
            {
                DataSet ds = new DataSet();
                string sql = "SELECT * FROM (SELECT a.WORKNO,a.LOCALNAME,a.marrystate,a.DName,a.LEVELCODE,a.MANAGERCODE,a.IDENTITYNO,a.Notes,a.Flag,(select DataValue from GDS_ATT_TYPEDATA c where c.DataType='Sex' and c.DataCode=a.Sex) as Sex,a.Sex SexCode,  a.technicalname, a.LevelName,a.ManagerName,  (SELECT TechnicalTypeName FROM GDS_ATT_TECHNICAL b,GDS_ATT_TECHNICALTYPE c WHERE c.TechnicalTypeCode=b.TechnicalType and b.technicalcode = a.technicalcode) as TechnicalType,  (SELECT Costcode FROM GDS_SC_DEPARTMENT b WHERE b.depcode=a.depcode) Costcode,  a.TECHNICALCODE,a.DEPCODE,a.Dcode, a.dname depname,a.DepName SYBName, GetDepName('2',a.depcode) Syc,GetDepName('1',a.depcode) BGName,GetDepName('0',a.depcode) CBGName, (select ProfessionalName from GDS_ATT_PROFESSIONAL n where n.ProfessionalCode=a.ProfessionalCode ) as ProfessionalName,  round((MONTHS_BETWEEN(sysdate,a.JoinDate)-nvl(a.DeductYears,0))/12,1) as ComeYears,  (select (select DataValue from GDS_ATT_TYPEDATA b where b.DataType='AssessLevel' and e.AssesLevel=b.DataCode)   from GDS_ATT_EMPASSESS e where e.WorkNo=a.WorkNo and e.AssesDate=(select Max(AssesDate)from GDS_ATT_EMPASSESS w where w.WorkNo=e.WorkNo) and ROWNUM<=1) as AssesLevel,    (select LevelType from GDS_ATT_LEVEL j where j.LevelCode=a.LevelCode ) as LevelType,  TO_CHAR(a.JoinDate,'yyyy/mm/dd') AS JoinDate,a.OverTimeType, (select DataValue from GDS_ATT_TYPEDATA t where t.DataType='OverTimeType' and t.DataCode=a.OverTimeType) as OverTimeTypeName from GDS_ATT_Employee a " + condition + ")";
                DataTable dt = DalHelper.ExecuteQuery(sql);

                if (dt != null)
                {
                    dt.TableName = "V_Employee";
                    ds.Tables.Add(dt);
                }
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string GetEmpWorkFlag(string sWorkNo, string sDate)
        {
            string sValue = "";
            try
            {
                OracleParameter result = new OracleParameter("workflag", OracleType.VarChar, 2);
                result.Direction = ParameterDirection.Output;
                OracleParameter[] paramList = new OracleParameter[] { new OracleParameter("workdate", sDate), new OracleParameter("v_workno", sWorkNo), result };
                //SortedList list = this.RunProc(paramList, "prog_getempottype");
                int a = DalHelper.ExecuteNonQuery("prog_getempworkflag", CommandType.StoredProcedure, paramList);
                if (a > 0)
                {
                    sValue = Convert.ToString(paramList[2].Value);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return sValue;
        }

        public string GetVlaue(string sql)
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

        public DataTable GetEmpinfo(string empno)
        {
            try
            {
                string sql = " select * from  GDS_ATT_EMPLOYEE where WORKNO='" + empno + "'";
                DataTable dt = DalHelper.ExecuteQuery(sql);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public string GetShiftNo(string sWorkNo, string sDate)
        {

            string value = "";
            try
            {
                OracleParameter sValue = new OracleParameter("v_shiftno", OracleType.VarChar, 100);
                sValue.Direction = ParameterDirection.Output;
                int a = DalHelper.ExecuteNonQuery("prog_getempshiftno", CommandType.StoredProcedure, new OracleParameter("v_workno", sWorkNo), new OracleParameter("v_date", sDate), sValue);
                if (a > 0)
                {
                    value = Convert.ToString(sValue.Value);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return value;
        }

        public string GetAddValueDataList(string condition)
        {
            string SQL = @"select nvl(max(workno),'Y') from GDS_ATT_employee where workno='" + condition + "' ";
            string varRevalue = this.GetValue(SQL);
            return varRevalue;
        }

        public SortedList ReturnOTTTime(string WorkNo, string OTDate, DateTime dtTempBeginTime, DateTime dtTempEndTime, string ShiftNo)
        {
            string condition = "";
            SortedList list = new SortedList();

            DateTime dtMidTime = DateTime.Parse(OTDate + " 12:00");
            DateTime dtMidTime2 = DateTime.Parse(OTDate + " 08:00");
            try
            {
                condition = "select to_date('" + dtTempEndTime.ToString("yyyy/MM/dd HH:mm") + "','yyyy/mm/dd hh24:mi:ss')-to_date('" + dtTempBeginTime.ToString("yyyy/MM/dd HH:mm") + "','yyyy/mm/dd hh24:mi:ss') from dual";
                if (Convert.ToDecimal(GetValue(condition)) < 0M)
                {
                    dtTempEndTime = DateTime.Parse(DateTime.Parse(OTDate).AddDays(1.0).ToString("yyyy/MM/dd") + " " + dtTempEndTime.ToString("HH:mm"));
                }
                else if (ShiftNo.StartsWith("C"))
                {
                    condition = "select to_date('" + dtTempBeginTime.ToString("yyyy/MM/dd HH:mm") + "','yyyy/mm/dd hh24:mi:ss')-to_date('" + dtMidTime.ToString("yyyy/MM/dd HH:mm") + "','yyyy/mm/dd hh24:mi:ss') from dual";
                    if (Convert.ToDecimal(GetValue(condition)) < 0M)
                    {
                        dtTempBeginTime = DateTime.Parse(DateTime.Parse(OTDate).AddDays(1.0).ToString("yyyy/MM/dd") + " " + dtTempBeginTime.ToString("HH:mm"));
                        dtTempEndTime = DateTime.Parse(DateTime.Parse(OTDate).AddDays(1.0).ToString("yyyy/MM/dd") + " " + dtTempEndTime.ToString("HH:mm"));
                    }
                    else
                    {
                        condition = "select to_date('" + dtTempEndTime.ToString("yyyy/MM/dd HH:mm") + "','yyyy/mm/dd hh24:mi:ss')-to_date('" + dtMidTime.ToString("yyyy/MM/dd HH:mm") + "','yyyy/mm/dd hh24:mi:ss') from dual";
                        if (Convert.ToDecimal(GetValue(condition)) < 0M)
                        {
                            dtTempEndTime = DateTime.Parse(DateTime.Parse(OTDate).AddDays(1.0).ToString("yyyy/MM/dd") + " " + dtTempEndTime.ToString("HH:mm"));
                        }
                    }
                }
                else if (ShiftNo.StartsWith("B"))
                {
                    condition = "select to_date('" + dtTempBeginTime.ToString("yyyy/MM/dd HH:mm") + "','yyyy/mm/dd hh24:mi:ss')-to_date('" + dtMidTime2.ToString("yyyy/MM/dd HH:mm") + "','yyyy/mm/dd hh24:mi:ss') from dual";
                    if (Convert.ToDecimal(GetValue(condition)) < 0M)
                    {
                        dtTempBeginTime = DateTime.Parse(DateTime.Parse(OTDate).AddDays(1.0).ToString("yyyy/MM/dd") + " " + dtTempBeginTime.ToString("HH:mm"));
                        dtTempEndTime = DateTime.Parse(DateTime.Parse(OTDate).AddDays(1.0).ToString("yyyy/MM/dd") + " " + dtTempEndTime.ToString("HH:mm"));
                    }
                }
                list.Add("A", dtTempBeginTime);
                list.Add("B", dtTempEndTime);
                return list;
            }
            catch (Exception)
            {
                list.Add("A", dtTempBeginTime);
                list.Add("B", dtTempEndTime);
                return list;
            }
        }

        #region

        public string GetOTMSGFlag(string WorkNo, string OtDate, double OtHours, string OTType, string IsProject, string ModifyID)
        {
            OracleParameter[] paramList;
            SortedList list;
            Exception ex;
            string condition = "";
            string IsAllowProjectByDayLimit = "";
            string nwFlag = "";
            bool IsSixWorkDays = false;
            double G1DLimit = -1.0;
            double G2DLimit = -1.0;
            double G3DLimit = -1.0;
            double G1MLimit = -1.0;
            double G2MLimit = -1.0;
            double G12MLimit = -1.0;
            double G13MLimit = -1.0;
            double G123MLimit = -1.0;
            string ISAllowProject = "";
            string OverTimeType = "";
            string DepCode = "";
            string V_ID = ModifyID;
            if (V_ID.Length == 0)
            {
                V_ID = "N";
            }
            string OTMAdvanceG1Flag = this.GetValue("select nvl(max(ParaValue),'N') from GDS_SC_PARAMETER where ParaName ='OTMAdvanceG1Flag'");
            string OTMAdvanceApplyG2LMT = this.GetValue("select nvl(max(ParaValue),'2') from GDS_SC_PARAMETER where ParaName ='OTMAdvanceApplyG2LMT'");
            condition = "Select OverTimeType,DCode From GDS_ATT_EMPLOYEE Where Workno='" + WorkNo + "'";
            DataTable tempDataTable = this.GetDataSetBySQL(condition).Tables["TempTable"];
            if (tempDataTable.Rows.Count > 0)
            {
                condition = "select overtimetype from GDS_ATT_MONTHTOTAL where workno='" + WorkNo + "' AND yearmonth='" + Convert.ToDateTime(OtDate).ToString("yyyyMM") + "'";
                OverTimeType = this.GetValue(condition);
                if (string.IsNullOrEmpty(OverTimeType))
                {
                    OverTimeType = tempDataTable.Rows[0]["OverTimeType"].ToString().Trim();
                }
                DepCode = tempDataTable.Rows[0]["DCode"].ToString().Trim();
            }
            if (OverTimeType.Length <= 0)
            {
                return ("A未定義加班類別");
            }
            if ((OverTimeType.Equals("F") && (this.GetValue("select nvl(MAX(paravalue),'N') from GDS_SC_PARAMETER where paraname='IsG1ForFFlag'") == "N")) && OTType.Equals("G1"))
            {
                return ("AF類人員不允許申報G1加班");
            }
            if (OverTimeType.Equals("H"))
            {
                return ("AH類(年薪制)及I類(三期)人員不允許申報加班");
            }
            if (OverTimeType.Equals("I"))
            {
                return ("AH類(年薪制)及I類(三期)人員不允許申報加班");
            }
            string ShiftNo = this.GetShiftNo(WorkNo, OtDate);
            switch (ShiftNo)
            {
                case null:
                case "":
                    return ("A" + "班別" + "不能為空.");
            }
            if (this.GetValue("select IsLactation from GDS_ATT_WORKSHIFT where shiftno ='" + ShiftNo + "'").Equals("Y"))
            {
                return ("A" + "哺乳期員工不允許加班");
            }
            condition = "select * from(select a.G1DLimit,a.G2DLimit,a.G3DLimit,a.G1MLimit,a.G2MLimit,a.G12Mlimit,a.ISAllowProject,a.G13Mlimit,a.G123Mlimit from GDS_ATT_TYPE a,GDS_SC_DEPARTMENT b,GDS_SC_DEPLEVEL c where a.OrgCode=b.DepCode(+) and b.LevelCode=c.LevelCode(+)  and a.OTTypeCode='" + OverTimeType + "' AND a.EffectFlag='Y' and b.depcode in (SELECT DepCode FROM GDS_SC_DEPARTMENT  e   START WITH e.depCode ='" + DepCode + "'  CONNECT BY e.depcode = PRIOR e.parentdepcode)  order by c.orderid desc) where rownum<=1 ";
            tempDataTable = this.GetDataSetBySQL(condition).Tables["TempTable"];
            if (tempDataTable.Rows.Count > 0)
            {
                if (Convert.ToString(tempDataTable.Rows[0]["G1DLimit"]).Length > 0)
                {
                    G1DLimit = double.Parse(tempDataTable.Rows[0]["G1DLimit"].ToString());
                }
                if (Convert.ToString(tempDataTable.Rows[0]["G2DLimit"]).Length > 0)
                {
                    G2DLimit = double.Parse(tempDataTable.Rows[0]["G2DLimit"].ToString());
                }
                if (Convert.ToString(tempDataTable.Rows[0]["G3DLimit"]).Length > 0)
                {
                    G3DLimit = double.Parse(tempDataTable.Rows[0]["G3DLimit"].ToString());
                }
                if (Convert.ToString(tempDataTable.Rows[0]["G1MLimit"]).Length > 0)
                {
                    G1MLimit = double.Parse(tempDataTable.Rows[0]["G1MLimit"].ToString());
                }
                if (Convert.ToString(tempDataTable.Rows[0]["G2MLimit"]).Length > 0)
                {
                    G2MLimit = double.Parse(tempDataTable.Rows[0]["G2MLimit"].ToString());
                }
                if (Convert.ToString(tempDataTable.Rows[0]["G12MLimit"]).Length > 0)
                {
                    G12MLimit = double.Parse(tempDataTable.Rows[0]["G12MLimit"].ToString());
                }
                ISAllowProject = Convert.ToString(tempDataTable.Rows[0]["ISAllowProject"]);
                if (Convert.ToString(tempDataTable.Rows[0]["G13MLimit"]).Length > 0)
                {
                    G13MLimit = double.Parse(tempDataTable.Rows[0]["G13MLimit"].ToString());
                }
                if (Convert.ToString(tempDataTable.Rows[0]["G123MLimit"]).Length > 0)
                {
                    G123MLimit = double.Parse(tempDataTable.Rows[0]["G123MLimit"].ToString());
                }
            }
            else
            {
                return ("A" + "沒有定義加班類別上限");
            }
            double G1LMT = -1.0;
            double G2LMT = -1.0;
            double MonthLMT = -1.0;
            condition = "select G1LMT,G2LMT,NoWorkFlag,MonthLMT from GDS_ATT_CONFIG";
            tempDataTable = this.GetDataSetBySQL(condition).Tables["TempTable"];
            if (tempDataTable.Rows.Count > 0)
            {
                if (Convert.ToString(tempDataTable.Rows[0]["G1LMT"]).Length > 0)
                {
                    G1LMT = double.Parse(tempDataTable.Rows[0]["G1LMT"].ToString());
                }
                if (Convert.ToString(tempDataTable.Rows[0]["G2LMT"]).Length > 0)
                {
                    G2LMT = double.Parse(tempDataTable.Rows[0]["G2LMT"].ToString());
                }
                if (Convert.ToString(tempDataTable.Rows[0]["MonthLMT"]).Length > 0)
                {
                    MonthLMT = double.Parse(tempDataTable.Rows[0]["MonthLMT"].ToString());
                }
                nwFlag = tempDataTable.Rows[0]["NoWorkFlag"].ToString();
            }
            string strMonday = CalculateLastDateOfWeek(Convert.ToDateTime(OtDate)).AddDays(-6.0).ToString("yyyy/MM/dd");
            string strSunday = CalculateLastDateOfWeek(Convert.ToDateTime(OtDate)).ToString("yyyy/MM/dd");
            string sValue = "";
            try
            {
                //paramList = new OracleParameter[] { new OracleParameter("v_otdate", OracleType.VarChar, 10, OtDate), new OracleParameter("v_workno", OracleType.VarChar, 10, WorkNo), new OracleParameter("o_result", OracleType.VarChar, 1, sValue) };
                //int a = DalHelper.ExecuteNonQuery("proc_isworksixday", CommandType.StoredProcedure, paramList);
                //sValue = paramList[2].Value.ToString();

                OracleParameter strValue = new OracleParameter("o_result", OracleType.VarChar, 100);
                strValue.Direction = ParameterDirection.Output;
                OracleParameter[] paramList1 = new OracleParameter[] { new OracleParameter("v_otdate", OtDate), new OracleParameter("v_workno", WorkNo), strValue };
                int a = DalHelper.ExecuteNonQuery("proc_isworksixday", CommandType.StoredProcedure, paramList1);
                sValue = paramList1[2].Value.ToString();
            }
            catch (Exception exception1)
            {
                ex = exception1;
                sValue = "N";
            }
            if (sValue.Equals("Y"))
            {
                IsSixWorkDays = true;
                if (nwFlag.Equals("Y"))
                {
                    return ("A" + "連續上班六天");
                }
            }
            double G1Total = 0.0;
            double G2Total = 0.0;
            double G3Total = 0.0;
            double G12Total = 0.0;
            double DayTotal = 0.0;
            double NotAuditCount = 0.0;
            double NotAuditCountProject = 0.0;
            double G1NoAudit = 0.0;
            double G2NoAudit = 0.0;
            double G2RealSalary = 0.0;
            double weekAdvanceTotal = 0.0;
            double weekRealTotal = 0.0;
            double DayRealTotal = 0.0;
            double OTAdvanceMonth = 0.0;
            double OTRealMonth = 0.0;
            double G3RealSalary = 0.0;
            double G13Total = 0.0;
            double G123Total = 0.0;
            double G3NoAudit = 0.0;
            string G123MParmLimit = "";
            string G13MParmLimit = "";
            OtDate = Convert.ToDateTime(OtDate).ToString("yyyy/MM/dd");
            string nOtDate = "to_date('" + OtDate + "','yyyy/MM/dd')";
            condition = "select nvl(sum(Hours),0) from GDS_ATT_ADVANCEAPPLY where WorkNo='" + WorkNo + "' and OTType in('G1','G2','G3','G4') and OTDate<>" + nOtDate + " and OTDate>last_day(add_months(" + nOtDate + ",-1)) and OTDate<=last_day(" + nOtDate + ") and (status<'2' or OTDate>=trunc(sysdate)) and importremark is null and instr(ImportFlag,'N')>0";
            OTAdvanceMonth = double.Parse(this.GetValue(condition));
            condition = "select nvl(sum(G1RelSalary),0)+nvl(sum(G2RelSalary),0)+nvl(sum(G3RelSalary),0)+nvl(sum(SpecG1Salary),0)+nvl(sum(SpecG2Salary),0)+nvl(sum(SpecG3Salary),0) from GDS_ATT_MONTHTOTAL where WorkNo='" + WorkNo + "' and yearMonth='" + Convert.ToDateTime(OtDate).ToString("yyyyMM") + "'";
            OTRealMonth = double.Parse(this.GetValue(condition));
            condition = "SELECT count(1) FROM GDS_ATT_MONTHTOTAL WHERE ApproveFlag='2' and workno='" + WorkNo + "' AND YearMonth='" + Convert.ToDateTime(OtDate).ToString("yyyyMM") + "'";
            if (this.GetValue(condition).Equals("1"))
            {
                return ("A" + "當月月加班匯總已核準");
            }
            condition = "select count(1) from GDS_ATT_EMPLEAVEDETAIL a,GDS_ATT_EMPLEAVEMASTER b where a.workno='" + WorkNo + "' and b.BillNo =a.BillNo and b.BillProcess in('A190','A21','B152','B20','C142','C20','D142','D20','2')";
            if (this.GetValue(condition).Equals("1"))
            {
                return ("A" + "非在職員工");
            }
            condition = "select nvl(sum(ConfirmHours),0) from GDS_ATT_REALAPPLY where WorkNo='" + WorkNo + "' and OTDate=" + nOtDate + " and (diffreason<>'D' or diffreason is null)";
            DayRealTotal = double.Parse(this.GetValue(condition));
            condition = "select nvl(sum(ConfirmHours),0) from GDS_ATT_REALAPPLY where WorkNo='" + WorkNo + "' and OTDate>last_day(add_months(" + nOtDate + ",-1)) and OTDate<=last_day(" + nOtDate + ") and instr(OTType,'G1')>0 and status<'3' and (diffreason<>'D' or diffreason is null)";
            G1Total = double.Parse(this.GetValue(condition));
            if (OTType.Equals("G1"))
            {
                G1Total -= DayRealTotal;
            }
            condition = "select nvl(sum(ConfirmHours),0) from GDS_ATT_REALAPPLY where WorkNo='" + WorkNo + "' and OTDate>last_day(add_months(" + nOtDate + ",-1)) and OTDate<=last_day(" + nOtDate + ") and instr(OTType,'G2')>0 and status<'3' and (diffreason<>'D' or diffreason is null)";
            G2Total = double.Parse(this.GetValue(condition));
            if (OTType.Equals("G2"))
            {
                G2Total -= DayRealTotal;
            }
            condition = "select nvl(sum(ConfirmHours),0) from GDS_ATT_REALAPPLY where WorkNo='" + WorkNo + "' and OTDate>last_day(add_months(" + nOtDate + ",-1)) and OTDate<=last_day(" + nOtDate + ") and instr(OTType,'G2')>0 and status<'2' and (diffreason<>'D' or diffreason is null)";
            G2NoAudit = double.Parse(this.GetValue(condition));
            if (OTType.Equals("G3"))
            {
                condition = "select nvl(sum(ConfirmHours),0) from GDS_ATT_REALAPPLY where WorkNo='" + WorkNo + "' and OTDate=" + nOtDate + " and instr(OTType,'G3')>0 and status<'3' and (diffreason<>'D' or diffreason is null)";
                G3Total = double.Parse(this.GetValue(condition));
            }
            condition = "select nvl(sum(Hours),0) from GDS_ATT_ADVANCEAPPLY where WorkNo='" + WorkNo + "' and OTDate<>" + nOtDate + " and OTDate>last_day(add_months(" + nOtDate + ",-1)) and OTDate<=last_day(" + nOtDate + ") and instr(OTType,'G1')>0 and (status<'2' or OTDate>=trunc(sysdate)) and importremark is null and instr(isProject,'N')>0 and instr(ImportFlag,'N')>0";
            G1NoAudit += double.Parse(this.GetValue(condition));
            condition = "select nvl(sum(Hours),0) from GDS_ATT_ADVANCEAPPLY where WorkNo='" + WorkNo + "' and OTDate<>" + nOtDate + " AND OTDate>last_day(add_months(" + nOtDate + ",-1)) and OTDate<=last_day(" + nOtDate + ") and instr(OTType,'G2')>0 and (status<'2' or OTDate>=trunc(sysdate)) and importremark is null and instr(isProject,'N')>0 and instr(ImportFlag,'N')>0";
            G2NoAudit += double.Parse(this.GetValue(condition));
            if (OTType.Equals("G3"))
            {
                if (ModifyID.Length > 0)
                {
                    condition = "select nvl(sum(Hours),0) from GDS_ATT_ADVANCEAPPLY where WorkNo='" + WorkNo + "' and OTDate=" + nOtDate + " and instr(OTType,'G3')>0 and id<>'" + ModifyID + "' and (status<'2' or OTDate>=trunc(sysdate)) and importremark is null and instr(isProject,'N')>0 and instr(ImportFlag,'N')>0";
                }
                else
                {
                    condition = "select nvl(sum(Hours),0) from GDS_ATT_ADVANCEAPPLY where WorkNo='" + WorkNo + "' and OTDate=" + nOtDate + " and instr(OTType,'G3')>0 and (status<'2' or OTDate>=trunc(sysdate)) and importremark is null and instr(isProject,'N')>0 and instr(ImportFlag,'N')>0";
                }
                G3Total += double.Parse(this.GetValue(condition));
            }
            condition = "select nvl(sum(G2RelSalary),0) from GDS_ATT_MONTHTOTAL where WorkNo='" + WorkNo + "' and yearMonth='" + Convert.ToDateTime(OtDate).ToString("yyyyMM") + "'";
            G2RealSalary = double.Parse(this.GetValue(condition));
            if (OTType.Equals("G2"))
            {
                G2RealSalary -= DayRealTotal;
            }
            try
            {
                G123MParmLimit = ConfigurationManager.AppSettings["G123MParmLimit"].ToString();
                G13MParmLimit = ConfigurationManager.AppSettings["G13MParmLimit"].ToString();
            }
            catch
            {
                G123MParmLimit = "";
                G13MParmLimit = "";
            }
            G12Total = ((G1Total + G1NoAudit) + G2NoAudit) + G2RealSalary;
            if (G13MParmLimit.Equals("Y"))
            {
                condition = "select nvl(sum(G3RelSalary),0) from GDS_ATT_MONTHTOTAL where WorkNo='" + WorkNo + "' and yearMonth='" + Convert.ToDateTime(OtDate).ToString("yyyyMM") + "'";
                G3RealSalary = double.Parse(this.GetValue(condition));
                if (OTType.Equals("G3") || OTType.Equals("G4"))
                {
                    G3RealSalary -= DayRealTotal;
                }
                condition = "select nvl(sum(Hours),0) from GDS_ATT_ADVANCEAPPLY where WorkNo='" + WorkNo + "' and OTDate<>" + nOtDate + " AND OTDate>last_day(add_months(" + nOtDate + ",-1)) and OTDate<=last_day(" + nOtDate + ") and OTType in('G3','G4') and (status<'2' or OTDate>=trunc(sysdate)) and importremark is null and instr(isProject,'N')>0 and instr(ImportFlag,'N')>0";
                G3NoAudit += double.Parse(this.GetValue(condition));
                G13Total = ((G1Total + G1NoAudit) + G3NoAudit) + G3RealSalary;
            }
            G123Total = ((((G1Total + G1NoAudit) + G2NoAudit) + G2RealSalary) + G3NoAudit) + G3RealSalary;
            if (ModifyID.Length > 0)
            {
                condition = "select nvl(sum(Hours),0) from GDS_ATT_ADVANCEAPPLY where WorkNo='" + WorkNo + "' and OTDate=to_date('" + OtDate + "','yyyy/MM/dd') and instr(OTType,'" + OTType + "')>0 and status<'3' and id<>'" + ModifyID + "' and instr(isProject,'N')>0";
            }
            else
            {
                condition = "select nvl(sum(Hours),0) from GDS_ATT_ADVANCEAPPLY where WorkNo='" + WorkNo + "' and OTDate=to_date('" + OtDate + "','yyyy/MM/dd') and instr(OTType,'" + OTType + "')>0 and status<'3' and instr(isProject,'N')>0";
            }
            DayTotal = double.Parse(this.GetValue(condition));
            if (ModifyID.Length > 0)
            {
                condition = "select count(1) from GDS_ATT_ADVANCEAPPLY where WorkNo='" + WorkNo + "' and instr(OTType,'" + OTType + "')>0 and status<'2' and id<>'" + ModifyID + "' and instr(isProject,'N')>0 and OTDate>sysdate-30";
            }
            else
            {
                condition = "select count(1) from GDS_ATT_ADVANCEAPPLY where WorkNo='" + WorkNo + "' and instr(OTType,'" + OTType + "')>0 and status<'2' and instr(isProject,'N')>0 and OTDate>sysdate-30";
            }
            NotAuditCount = double.Parse(this.GetValue(condition));
            if (ModifyID.Length > 0)
            {
                condition = "select count(1) from GDS_ATT_ADVANCEAPPLY where WorkNo='" + WorkNo + "' and instr(OTType,'" + OTType + "')>0 and status<'2' and id<>'" + ModifyID + "' and instr(isProject,'Y')>0 and OTDate>sysdate-30";
            }
            else
            {
                condition = "select count(1) from GDS_ATT_ADVANCEAPPLY where WorkNo='" + WorkNo + "' and instr(OTType,'" + OTType + "')>0 and status<'2' and instr(isProject,'Y')>0 and OTDate>sysdate-30";
            }
            NotAuditCountProject = double.Parse(this.GetValue(condition));
            if (this.GetValue("select nvl(max(paravalue),'Y') from GDS_SC_PARAMETER where paraname='OTMAdvanceNoAuditLimit'").Equals("Y"))
            {
                if ((NotAuditCount > 2.0) || (NotAuditCountProject > 2.0))
                {
                    return ("A" + "存在兩筆狀態為未核准或簽核中的加班預報記錄時,不允許申報第三筆加班");
                }
                if ((NotAuditCount > 15.0) || (NotAuditCountProject > 15.0))
                {
                    return ("A" + "存在兩筆狀態為未核准或簽核中的加班預報記錄時,不允許申報第三筆加班");
                }
            }
            IsAllowProjectByDayLimit = this.GetValue("select nvl(max(paravalue),'Y') from GDS_SC_PARAMETER where paraname='IsAllowProjectByDayLimit'");
            if (OTType == "G1")
            {
                if ((G1LMT >= 0.0) && ((OtHours + DayTotal) > G1LMT))
                {
                    return ("A" + "已超過當日加班管控上限" + G1LMT.ToString() + "H");
                }
            }
            else if ((G2LMT >= 0.0) && ((OtHours + DayTotal) > G2LMT))
            {
                return ("A" + "已超過當日加班管控上限" + G2LMT.ToString() + "H");
            }
            if ((MonthLMT >= 0.0) && (((OTAdvanceMonth + OTRealMonth) + (OtHours + DayTotal)) > MonthLMT))
            {
                return ("A" + "已超過當日加班管控上限" + Convert.ToString((double)((OTAdvanceMonth + OTRealMonth) + DayTotal)) + "H+預報時數" + OtHours.ToString() + "H>預報時數" + MonthLMT.ToString() + "H");
            }
            double WeekWorkHours = 0.0;
            if (IsProject.Equals("N"))
            {
                try
                {
                    //paramList = new OracleParameter[] { new OracleParameter("v_otdate", OracleType.VarChar, 10, OtDate), new OracleParameter("v_workno", OracleType.VarChar, 10, WorkNo), new OracleParameter("v_hours", OracleType.Double, 10, OtHours.ToString()), new OracleParameter("v_id", OracleType.VarChar, 0x24, V_ID), new OracleParameter("o_result", OracleType.VarChar, 1, sValue), new OracleParameter("o_workhours", OracleType.Double, 10, WeekWorkHours.ToString()) };
                    //int a = DalHelper.ExecuteNonQuery("proc_isoverworktime", CommandType.StoredProcedure, paramList);
                    //sValue = paramList[4].Value.ToString();
                    //WeekWorkHours = Convert.ToDouble(paramList[5].Value.ToString());

                    OracleParameter strResult = new OracleParameter("o_result", OracleType.VarChar, 100);
                    OracleParameter strWorkhours = new OracleParameter("o_workhours", OracleType.Double, 10);
                    strResult.Direction = ParameterDirection.Output;
                    strWorkhours.Direction = ParameterDirection.Output;
                    OracleParameter[] paramList2 = new OracleParameter[] { new OracleParameter("v_otdate", OtDate), new OracleParameter("v_workno", WorkNo), new OracleParameter("v_hours", OtHours.ToString()), new OracleParameter("v_id", V_ID), strResult, strWorkhours };
                    int a = DalHelper.ExecuteNonQuery("proc_isoverworktime", CommandType.StoredProcedure, paramList2);
                    sValue = paramList2[4].Value.ToString();
                    WeekWorkHours = Convert.ToDouble(paramList2[5].Value.ToString());
                }
                catch (Exception exception2)
                {
                    ex = exception2;
                    sValue = "N";
                    WeekWorkHours = 0.0;
                }
                if (sValue.Equals("Y"))
                {
                    return ("A" + "當周工作時數" + WeekWorkHours.ToString() + "H+預報時數" + Convert.ToString(OtHours) + "H" + "超過當周工作時間上限");
                }
                if (OTType == "G1")
                {
                    if ((G1DLimit >= 0.0) && ((OtHours + DayTotal) > G1DLimit))
                    {
                        return ("A" + "已超過當日加班管控上限" + G1DLimit.ToString() + "H");
                    }
                }
                else if (OTType == "G2")
                {
                    if ((G2DLimit >= 0.0) && ((OtHours + DayTotal) > G2DLimit))
                    {
                        return ("A" + "已超過當日加班管控上限" + G2DLimit.ToString() + "H");
                    }
                }
                else if ((OTType == "G3") && ((G3DLimit >= 0.0) && ((OtHours + DayTotal) > G3DLimit)))
                {
                    return ("A" + "已超過當日加班管控上限" + G3DLimit.ToString() + "H");
                }
                if ((G12MLimit >= 0.0) && (OTType != "G3"))
                {
                    if (OTType.Equals("G1"))
                    {
                        if ((((G1Total + G1NoAudit) + OtHours) + DayTotal) > G12MLimit)
                        {
                            return ("A" + "本月G1已加班" + Convert.ToString((double)(G1Total + G1NoAudit)) + "H+預報時數" + Convert.ToString((double)(OtHours + DayTotal)) + "H>管控上限" + G12MLimit.ToString() + "H");
                        }
                    }
                    else if ((G12Total + (OtHours + DayTotal)) > G12MLimit)
                    {
                        return ("A" + "本月已加班" + G12Total.ToString() + "H+預報時數" + Convert.ToString((double)(OtHours + DayTotal)) + "H>管控上限" + G12MLimit.ToString() + "H");
                    }
                }
                if (((G13MLimit >= 0.0) && (OTType != "G2")) && ((G13Total + (OtHours + DayTotal)) > G13MLimit))
                {
                    return ("A" + "本月已加班" + G13Total.ToString() + "H+預報時數" + Convert.ToString((double)(OtHours + DayTotal)) + "H>管控上限" + G13MLimit.ToString() + "H");
                }
                if ((G123MParmLimit.Equals("CDZBG123Limit") && (G123MLimit >= 0.0)) && ((G123Total + (OtHours + DayTotal)) > G123MLimit))
                {
                    return ("A" + "本月已加班" + G123Total.ToString() + "H+預報時數" + Convert.ToString((double)(OtHours + DayTotal)) + "H>管控上限" + G123MLimit.ToString() + "H");
                }
                if (OTType == "G1")
                {
                    if ((G1MLimit >= 0.0) && ((((G1Total + G1NoAudit) + OtHours) + DayTotal) > G1MLimit))
                    {
                        return ("A" + "本月G1已加班" + Convert.ToString((double)(G1Total + G1NoAudit)) + "H+預報時數" + Convert.ToString((double)(OtHours + DayTotal)) + "H>管控上限" + G1MLimit.ToString() + "H");
                    }
                    if ((G12MLimit > 0.0) && ((G12Total + (OtHours + DayTotal)) > G12MLimit))
                    {
                        if (OTMAdvanceG1Flag.Equals("Y"))
                        {
                            return ("3" + "本月已加班" + G12Total.ToString() + "H+預報時數" + OtHours.ToString() + "H>管控上限" + G12MLimit.ToString() + "H");
                        }
                        return ("A" + "本月已加班" + G12Total.ToString() + "H+預報時數" + OtHours.ToString() + "H>管控上限" + G12MLimit.ToString() + "H");
                    }
                }
                if (((OTType == "G2") && (G2MLimit >= 0.0)) && ((((G2NoAudit + G2RealSalary) + OtHours) + DayTotal) > G2MLimit))
                {
                    return ("A" + "本月G2已加班" + Convert.ToString((double)(G2NoAudit + G2RealSalary)) + "H+預報時數" + Convert.ToString((double)(OtHours + DayTotal)) + "H>管控上限" + G2MLimit.ToString() + "H");
                }
            }
            else
            {
                try
                {
                    //paramList = new OracleParameter[] { new OracleParameter("v_otdate", OracleType.VarChar, 10, OtDate), new OracleParameter("v_workno", OracleType.VarChar, 10, WorkNo), new OracleParameter("v_hours", OracleType.Double, 10, OtHours.ToString()), new OracleParameter("v_id", OracleType.VarChar, 0x24, V_ID), new OracleParameter("o_result", OracleType.VarChar, 1, sValue), new OracleParameter("o_workhours", OracleType.Double, 10, WeekWorkHours.ToString()) };
                    ////list = this.RunProc(paramList, "proc_isoverworktime");
                    //int a = DalHelper.ExecuteNonQuery("proc_isoverworktime", CommandType.StoredProcedure, paramList);
                    //sValue = paramList[4].Value.ToString();
                    //WeekWorkHours = Convert.ToDouble(paramList[5].Value.ToString());

                    OracleParameter strResult1 = new OracleParameter("o_result", OracleType.VarChar, 100);
                    OracleParameter strWorkhours1 = new OracleParameter("o_workhours", OracleType.Double, 10);
                    strResult1.Direction = ParameterDirection.Output;
                    strWorkhours1.Direction = ParameterDirection.Output;
                    OracleParameter[] paramList3 = new OracleParameter[] { new OracleParameter("v_otdate", OtDate), new OracleParameter("v_workno", WorkNo), new OracleParameter("v_hours", OtHours.ToString()), new OracleParameter("v_id", V_ID), strResult1, strWorkhours1 };
                    int a = DalHelper.ExecuteNonQuery("proc_isoverworktime", CommandType.StoredProcedure, paramList3);
                    sValue = paramList3[4].Value.ToString();
                    WeekWorkHours = Convert.ToDouble(paramList3[5].Value.ToString());
                }
                catch (Exception exception3)
                {
                    ex = exception3;
                    sValue = "N";
                    WeekWorkHours = 0.0;
                }
                if (ISAllowProject.Equals("N"))
                {
                    return ("A" + OverTimeType + "加班類別不允許申報專案加班");
                }
                if (!IsAllowProjectByDayLimit.Equals("N"))
                {
                    if (OTType == "G1")
                    {
                        if (G1DLimit == 0.0)
                        {
                            return ("A" + "加班類別不允許申報專案加班");
                        }
                        if ((G1DLimit >= 0.0) && (G12MLimit >= 0.0))
                        {
                            if ((G12Total + OtHours) > (G12MLimit - 0.4))
                            {
                                if (((G12Total + DayTotal) < (G12MLimit - 0.4)) && (DayTotal < G1DLimit))
                                {
                                    if (G1MLimit >= 0.0)
                                    {
                                        if (((((G1Total + G1NoAudit) + OtHours) + DayTotal) % 0.5) != 0.0)
                                        {
                                            if (((((G1Total + G1NoAudit) + OtHours) + DayTotal) <= (G1MLimit - 0.4)) && sValue.Equals("N"))
                                            {
                                                return ("A" + "月加班時數未超出當月上限時不允許申請專案加班。");
                                            }
                                        }
                                        else if (((((G1Total + G1NoAudit) + OtHours) + DayTotal) <= G1MLimit) && sValue.Equals("N"))
                                        {
                                            return ("A" + "月加班時數未超出當月上限時不允許申請專案加班。");
                                        }
                                    }
                                    else if (sValue.Equals("N"))
                                    {
                                        return ("A" + "月加班時數未超出當月上限時不允許申請專案加班。");
                                    }
                                }
                            }
                            else if ((DayTotal < G1DLimit) && ((G12Total + DayTotal) < (G12MLimit - 0.4)))
                            {
                                if (G1MLimit >= 0.0)
                                {
                                    if (((((G1Total + G1NoAudit) + OtHours) + DayTotal) % 0.5) != 0.0)
                                    {
                                        if (((((G1Total + G1NoAudit) + OtHours) + DayTotal) <= (G1MLimit - 0.4)) && sValue.Equals("N"))
                                        {
                                            return ("A" + "月加班時數未超出當月上限時不允許申請專案加班。");
                                        }
                                    }
                                    else if (((((G1Total + G1NoAudit) + OtHours) + DayTotal) <= G1MLimit) && sValue.Equals("N"))
                                    {
                                        return ("A" + "月加班時數未超出當月上限時不允許申請專案加班。");
                                    }
                                }
                                else if (sValue.Equals("N"))
                                {
                                    return ("A" + "月加班時數未超出當月上限時不允許申請專案加班。");
                                }
                            }
                        }
                        else if ((G12MLimit >= 0.0) && ((G12Total + DayTotal) < (G12MLimit - 0.4)))
                        {
                            if (G1MLimit >= 0.0)
                            {
                                if (((((G1Total + G1NoAudit) + OtHours) + DayTotal) % 0.5) != 0.0)
                                {
                                    if (((((G1Total + G1NoAudit) + OtHours) + DayTotal) <= (G1MLimit - 0.4)) && sValue.Equals("N"))
                                    {
                                        return ("A" + "月加班時數未超出當月上限時不允許申請專案加班。");
                                    }
                                }
                                else if (((((G1Total + G1NoAudit) + OtHours) + DayTotal) <= G1MLimit) && sValue.Equals("N"))
                                {
                                    return ("A" + "月加班時數未超出當月上限時不允許申請專案加班。");
                                }
                            }
                            else if (sValue.Equals("N"))
                            {
                                return ("A" + "月加班時數未超出當月上限時不允許申請專案加班。");
                            }
                        }
                    }
                    else if (OTType == "G2")
                    {
                        if ((G2DLimit >= 0.0) && (G12MLimit >= 0.0))
                        {
                            if ((G12Total + OtHours) > (G12MLimit - 0.4))
                            {
                                if ((G12Total + DayTotal) < (G12MLimit - 0.4))
                                {
                                    if (G2MLimit >= 0.0)
                                    {
                                        if (((((G2Total + G2NoAudit) + OtHours) + DayTotal) % 0.5) != 0.0)
                                        {
                                            if (((((G2Total + G2NoAudit) + OtHours) + DayTotal) <= (G2MLimit - 0.4)) && sValue.Equals("N"))
                                            {
                                                return ("A" + "月加班時數未超出當月上限時不允許申請專案加班。");
                                            }
                                        }
                                        else if (((((G2Total + G2NoAudit) + OtHours) + DayTotal) <= G2MLimit) && sValue.Equals("N"))
                                        {
                                            return ("A" + "月加班時數未超出當月上限時不允許申請專案加班。");
                                        }
                                    }
                                    else if (sValue.Equals("N"))
                                    {
                                        return ("A" + "月加班時數未超出當月上限時不允許申請專案加班。");
                                    }
                                }
                            }
                            else if ((DayTotal < G2DLimit) && ((G12Total + DayTotal) < (G12MLimit - 0.4)))
                            {
                                if (G2MLimit >= 0.0)
                                {
                                    if (((((G2Total + G2NoAudit) + OtHours) + DayTotal) % 0.5) != 0.0)
                                    {
                                        if (((((G2Total + G2NoAudit) + OtHours) + DayTotal) <= (G2MLimit - 0.4)) && sValue.Equals("N"))
                                        {
                                            return ("A" + "月加班時數未超出當月上限時不允許申請專案加班。");
                                        }
                                    }
                                    else if (((((G2Total + G2NoAudit) + OtHours) + DayTotal) <= G2MLimit) && sValue.Equals("N"))
                                    {
                                        return ("A" + "月加班時數未超出當月上限時不允許申請專案加班。");
                                    }
                                }
                                else if (sValue.Equals("N"))
                                {
                                    return ("A" + "月加班時數未超出當月上限時不允許申請專案加班。");
                                }
                            }
                        }
                        else if ((G12MLimit >= 0.0) && ((G12Total + DayTotal) < (G12MLimit - 0.4)))
                        {
                            if (G2MLimit >= 0.0)
                            {
                                if (((((G2Total + G2NoAudit) + OtHours) + DayTotal) % 0.5) != 0.0)
                                {
                                    if (((((G2Total + G2NoAudit) + OtHours) + DayTotal) <= (G2MLimit - 0.4)) && sValue.Equals("N"))
                                    {
                                        return ("A" + "月加班時數未超出當月上限時不允許申請專案加班。");
                                    }
                                }
                                else if (((((G2Total + G2NoAudit) + OtHours) + DayTotal) <= G2MLimit) && sValue.Equals("N"))
                                {
                                    return ("A" + "月加班時數未超出當月上限時不允許申請專案加班。");
                                }
                            }
                            else if (sValue.Equals("N"))
                            {
                                return ("A" + "月加班時數未超出當月上限時不允許申請專案加班。");
                            }
                        }
                        string sFlag = "N";
                        if (sValue.Equals("Y") && (WeekWorkHours < 59.6))
                        {
                            if (OTType == "G1")
                            {
                                if ((G1DLimit >= 0.0) && ((OtHours + DayTotal) > G1DLimit))
                                {
                                    sFlag = "Y";
                                }
                            }
                            else if (OTType == "G2")
                            {
                                if ((G2DLimit >= 0.0) && ((OtHours + DayTotal) > G2DLimit))
                                {
                                    sFlag = "Y";
                                }
                            }
                            else if ((OTType == "G3") && ((G3DLimit >= 0.0) && ((OtHours + DayTotal) > G3DLimit)))
                            {
                                sFlag = "Y";
                            }
                            if ((G12MLimit >= 0.0) && (OTType != "G3"))
                            {
                                if (OTType.Equals("G1"))
                                {
                                    if ((((G1Total + G1NoAudit) + OtHours) + DayTotal) > G12MLimit)
                                    {
                                        sFlag = "Y";
                                    }
                                }
                                else if ((G12Total + (OtHours + DayTotal)) > G12MLimit)
                                {
                                    sFlag = "Y";
                                }
                            }
                            if (OTType == "G1")
                            {
                                if (G1MLimit >= 0.0)
                                {
                                    if ((((G1Total + G1NoAudit) + OtHours) + DayTotal) > G1MLimit)
                                    {
                                        sFlag = "Y";
                                    }
                                    if ((G12Total + (OtHours + DayTotal)) > G12MLimit)
                                    {
                                        if (OTMAdvanceG1Flag.Equals("Y"))
                                        {
                                            sFlag = "Y";
                                        }
                                        else
                                        {
                                            sFlag = "Y";
                                        }
                                    }
                                }
                                if (((OTType == "G2") && (G2MLimit >= 0.0)) && ((((G2NoAudit + G2RealSalary) + OtHours) + DayTotal) > G2MLimit))
                                {
                                    sFlag = "Y";
                                }
                                if (sFlag.Equals("N"))
                                {
                                    return ("A" + "周工作時數未超出當周工作上限時不允許申請專案加班");
                                }
                            }
                        }
                    }
                    else if (OTType == "G3")
                    {
                        if (G3DLimit >= 0.0)
                        {
                            if (((DayTotal + OtHours) < G3Total) && sValue.Equals("N"))
                            {
                                return ("A" + "月加班時數未超出當月上限時不允許申請專案加班。");
                            }
                        }
                        else if (sValue.Equals("N"))
                        {
                            return ("A" + "月加班時數未超出當月上限時不允許申請專案加班。");
                        }
                    }
                }
            }
            if (G123MParmLimit.Equals("LHZBG123Limit") && (G123MLimit >= 0.0))
            {
                condition = "select nvl(sum(ConfirmHours),0) from GDS_ATT_REALAPPLY where WorkNo='" + WorkNo + "' and OTDate=" + nOtDate + "";
                DayRealTotal = double.Parse(this.GetValue(condition));
                condition = "select nvl(sum(ConfirmHours),0) from GDS_ATT_REALAPPLY where WorkNo='" + WorkNo + "' and OTDate>last_day(add_months(" + nOtDate + ",-1)) and OTDate<=last_day(" + nOtDate + ") and instr(OTType,'G1')>0 and status<'3'";
                G1Total = double.Parse(this.GetValue(condition));
                if (OTType.Equals("G1"))
                {
                    G1Total -= DayRealTotal;
                }
                condition = "select nvl(sum(Hours),0) from GDS_ATT_ADVANCEAPPLY where WorkNo='" + WorkNo + "' and OTDate<>" + nOtDate + " and OTDate>last_day(add_months(" + nOtDate + ",-1)) and OTDate<=last_day(" + nOtDate + ") and instr(OTType,'G1')>0 and (status<'2' or OTDate>=trunc(sysdate)) and importremark is null and instr(ImportFlag,'N')>0";
                G1NoAudit = double.Parse(this.GetValue(condition));
                condition = "select nvl(sum(Hours),0) from GDS_ATT_ADVANCEAPPLY where WorkNo='" + WorkNo + "' and OTDate<>" + nOtDate + " AND OTDate>last_day(add_months(" + nOtDate + ",-1)) and OTDate<=last_day(" + nOtDate + ") and instr(OTType,'G2')>0 and (status<'2' or OTDate>=trunc(sysdate)) and importremark is null and instr(ImportFlag,'N')>0";
                G2NoAudit = double.Parse(this.GetValue(condition));
                condition = "select nvl(sum(Hours),0) from GDS_ATT_ADVANCEAPPLY where WorkNo='" + WorkNo + "' and OTDate<>" + nOtDate + " AND OTDate>last_day(add_months(" + nOtDate + ",-1)) and OTDate<=last_day(" + nOtDate + ") and OTType in('G3','G4') and (status<'2' or OTDate>=trunc(sysdate)) and importremark is null and instr(isProject,'N')>0 and instr(ImportFlag,'N')>0";
                G3NoAudit = double.Parse(this.GetValue(condition));
                G123Total = ((((G1Total + G1NoAudit) + G2NoAudit) + G2RealSalary) + G3NoAudit) + G3RealSalary;
                if ((G123Total + (OtHours + DayTotal)) > G123MLimit)
                {
                    return ("A" + "本月已加班" + G123Total.ToString() + "H+預報時數" + Convert.ToString((double)(OtHours + DayTotal)) + "H>管控上限" + G123MLimit.ToString() + "H");
                }
            }
            condition = "select Sdays from(select to_date('" + OtDate + "','yyyy/MM/dd')-trunc(sysdate) Sdays from dual)";
            double AdvanceDays = double.Parse(this.GetValue(condition));
            if (!(OTType.Equals("G1") || ((Convert.ToInt32(DateTime.Parse(OtDate).DayOfWeek) != 0) && (Convert.ToInt32(DateTime.Parse(OtDate).DayOfWeek) != 6))))
            {
                if (AdvanceDays > Convert.ToInt32(OTMAdvanceApplyG2LMT))
                {
                    return ("A" + "雙休日加班只能提前預報的天數為" + OTMAdvanceApplyG2LMT);
                }
            }
            else if (AdvanceDays > 30.0)
            {
                return ("A" + "只允許預報30天以內的加班" + "30");
            }
            if (sValue.Equals("Y"))
            {
                return ("4" + "當周工作時數" + WeekWorkHours.ToString() + "H+預報時數" + Convert.ToString(OtHours) + "H" + "超過當周工作時間上限");
            }
            if (ModifyID.Length > 0)
            {
                condition = "select nvl(sum(Hours),0) from GDS_ATT_ADVANCEAPPLY where WorkNo='" + WorkNo + "' and OTDate>=to_date('" + strMonday + "','yyyy/MM/dd') and OTDate<=to_date('" + strSunday + "','yyyy/MM/dd') and OTType<>'G3' and id<>'" + ModifyID + "' and status<'3' and ImportFlag='N'";
            }
            else
            {
                condition = "select nvl(sum(Hours),0) from GDS_ATT_ADVANCEAPPLY where WorkNo='" + WorkNo + "' and OTDate>=to_date('" + strMonday + "','yyyy/MM/dd') and OTDate<=to_date('" + strSunday + "','yyyy/MM/dd') and OTType<>'G3' and status<'3' and ImportFlag='N'";
            }
            weekAdvanceTotal += double.Parse(this.GetValue(condition));
            condition = "select nvl(sum(ConfirmHours),0) from GDS_ATT_REALAPPLY where WorkNo='" + WorkNo + "' and OTDate>=to_date('" + strMonday + "','yyyy/MM/dd') and OTDate<=to_date('" + strSunday + "','yyyy/MM/dd') and OTType<>'G3' and status<'3'";
            weekRealTotal = double.Parse(this.GetValue(condition));
            if (((weekAdvanceTotal + weekRealTotal) + OtHours) >= 18.0)
            {
                return ("5" + "當周加班時數+本次加班時數已達" + Convert.ToString((double)((weekAdvanceTotal + weekRealTotal) + OtHours)) + "H");
            }
            if (IsSixWorkDays)
            {
                return ("6" + "連續上班六天");
            }
            return "";
        }
        #endregion

        public static DateTime CalculateLastDateOfWeek(DateTime someDate)
        {
            int i = (int)someDate.DayOfWeek;
            if (i != 0)
            {
                i = 7 - i;
            }
            TimeSpan ts = new TimeSpan(i, 0, 0, 0);
            return someDate.Add(ts);
        }

        #region
        public void SaveData(string processFlag, DataTable dataTable, SynclogModel logmodel)
        {
            OracleCommand command = new OracleCommand();
            command.Connection = DalHelper.Connection;
            command.Connection.Open();
            OracleTransaction trans = command.Connection.BeginTransaction();
            command.Transaction = trans;
            string isShowMoveLeaveFlag = "N";
            try
            {
                command.CommandText = "select nvl(max(paravalue),'N') from GDS_SC_PARAMETER where paraname='IsShowMoveLeaveFlag'";

                isShowMoveLeaveFlag = Convert.ToString(command.ExecuteScalar());
                //isShowMoveLeaveFlag=command.ExecuteNonQuery();
                foreach (DataRow newRow in dataTable.Rows)
                {
                    string PlanAdjust = "''";
                    string sql1 = string.Empty;
                    string sql2 = string.Empty;
                    if (!isShowMoveLeaveFlag.Equals("Y") && !newRow["PlanAdjust"].ToString().Equals(""))
                    {
                        PlanAdjust = "TO_DATE('" + DateTime.Parse(newRow["PlanAdjust"].ToString()).ToString("yyyy-MM-dd") + "','yyyy-mm-dd')";
                    }
                    if (processFlag.Equals("Add"))
                    {
                        if (isShowMoveLeaveFlag == "Y")
                        {
                            sql1 = string.Concat(new object[] { 
                                "INSERT INTO GDS_ATT_ADVANCEAPPLY(WorkNo,OTType,OTDate,BeginTime,EndTime,Hours,WorkDesc,Remark,OTMSGFlag,Status,IsProject,PlanAdjust,OTShiftNo,ApplyDate,update_user,update_date,G2ISFORREST) VALUES('", newRow["WorkNo"], "','", newRow["OTType"], "',to_date('", Convert.ToDateTime(newRow["OTDate"]).ToString("yyyy/MM/dd"), "','yyyy/mm/dd'),to_date('", Convert.ToDateTime(newRow["BeginTime"]).ToString("yyyy/MM/dd HH:mm"), "','yyyy/mm/dd hh24:mi:ss'),to_date('", Convert.ToDateTime(newRow["EndTime"]).ToString("yyyy/MM/dd HH:mm"), "','yyyy/mm/dd hh24:mi:ss'),'", newRow["Hours"], "','", newRow["WorkDesc"], "','", newRow["Remark"], 
                                "','", newRow["OTMSGFlag"], "','0','", newRow["IsProject"], "',", PlanAdjust, ",'", newRow["OTShiftNo"], "',to_date('", Convert.ToDateTime(newRow["ApplyDate"]).ToString("yyyy/MM/dd"), "','yyyy/mm/dd'),'", newRow["UPDATE_USER"], "',sysdate,'", newRow["G2ISFORREST"], "')"
                             });
                        }
                        else
                        {
                            sql1 = string.Concat(new object[] { 
                                "INSERT INTO GDS_ATT_ADVANCEAPPLY(WorkNo,OTType,OTDate,BeginTime,EndTime,Hours,WorkDesc,Remark,OTMSGFlag,Status,IsProject,PlanAdjust,OTShiftNo,ApplyDate,Modifier,ModifyDate) VALUES('", newRow["WorkNo"], "','", newRow["OTType"], "',to_date('", Convert.ToDateTime(newRow["OTDate"]).ToString("yyyy/MM/dd"), "','yyyy/mm/dd'),to_date('", Convert.ToDateTime(newRow["BeginTime"]).ToString("yyyy/MM/dd HH:mm"), "','yyyy/mm/dd hh24:mi:ss'),to_date('", Convert.ToDateTime(newRow["EndTime"]).ToString("yyyy/MM/dd HH:mm"), "','yyyy/mm/dd hh24:mi:ss'),'", newRow["Hours"], "','", newRow["WorkDesc"], "','", newRow["Remark"], 
                                "','", newRow["OTMSGFlag"], "','0','", newRow["IsProject"], "',", PlanAdjust, ",'", newRow["OTShiftNo"], "',to_date('", Convert.ToDateTime(newRow["ApplyDate"]).ToString("yyyy/MM/dd"), "','yyyy/mm/dd'),'",  newRow["UPDATE_USER"], "',sysdate)"
                             });
                        }
                        command.CommandText = sql1;
                        command.ExecuteNonQuery();
                        SaveLogData("I", newRow["WorkNo"].ToString(), command.CommandText, command,logmodel);
                    }
                    else if (processFlag.Equals("Modify"))
                    {
                        if (isShowMoveLeaveFlag == "Y")
                        {
                            sql2 = string.Concat(new object[] { 
                                "UPDATE GDS_ATT_ADVANCEAPPLY SET OTType='", newRow["OTType"], "',OTDate=to_date('", Convert.ToDateTime(newRow["OTDate"]).ToString("yyyy/MM/dd"), "','yyyy/mm/dd'),BeginTime=to_date('", Convert.ToDateTime(newRow["BeginTime"]).ToString("yyyy/MM/dd HH:mm"), "','yyyy/mm/dd hh24:mi:ss'),EndTime=to_date('", Convert.ToDateTime(newRow["EndTime"]).ToString("yyyy/MM/dd HH:mm"), "','yyyy/mm/dd hh24:mi:ss'),Hours='", newRow["Hours"], "',WorkDesc='", newRow["WorkDesc"], "',Remark='", newRow["Remark"], "',OTMSGFlag='", newRow["OTMSGFlag"], 
                                "',ApplyDate=to_date('", Convert.ToDateTime(newRow["ApplyDate"]).ToString("yyyy/MM/dd"), "','yyyy/mm/dd'),Status='", newRow["Status"], "',OTShiftNo='", newRow["OTShiftNo"], "',PlanAdjust=", PlanAdjust, ",UPDATE_USER='", newRow["UPDATE_USER"], "',update_date=sysdate,G2ISFORREST='", newRow["G2ISFORREST"], "'  WHERE ID='", newRow["ID"], "' "
                             });
                        }
                        else
                        {
                            sql2 = string.Concat(new object[] { 
                                "UPDATE GDS_ATT_ADVANCEAPPLY SET OTType='", newRow["OTType"], "',OTDate=to_date('", Convert.ToDateTime(newRow["OTDate"]).ToString("yyyy/MM/dd"), "','yyyy/mm/dd'),BeginTime=to_date('", Convert.ToDateTime(newRow["BeginTime"]).ToString("yyyy/MM/dd HH:mm"), "','yyyy/mm/dd hh24:mi:ss'),EndTime=to_date('", Convert.ToDateTime(newRow["EndTime"]).ToString("yyyy/MM/dd HH:mm"), "','yyyy/mm/dd hh24:mi:ss'),Hours='", newRow["Hours"], "',WorkDesc='", newRow["WorkDesc"], "',Remark='", newRow["Remark"], "',OTMSGFlag='", newRow["OTMSGFlag"], 
                                "',ApplyDate=to_date('", Convert.ToDateTime(newRow["ApplyDate"]).ToString("yyyy/MM/dd"), "','yyyy/mm/dd'),Status='", newRow["Status"], "',OTShiftNo='", newRow["OTShiftNo"], "',PlanAdjust=", PlanAdjust, ",UPDATE_USER='",  newRow["UPDATE_USER"], "',update_date=sysdate WHERE ID='", newRow["ID"], "' "
                             });
                        }
                        command.CommandText = sql2;
                        command.ExecuteNonQuery();
                        SaveLogData("U", newRow["WorkNo"].ToString(), command.CommandText, command,logmodel);
                    }
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
        }
        #endregion

     

        public DataSet GetMonthAllOverTime(string condition)
        {
            try
            {
                DataSet ds = new DataSet();
                string sql = "SELECT MonthTotal FROM GDS_ATT_MONTHDETAIL " + condition;
                DataTable dt = DalHelper.ExecuteQuery(sql);
                if (dt != null)
                {
                    dt.TableName = "OTM_MonthDetail";
                    ds.Tables.Add(dt);
                }
                return ds;
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

        public string GetOTType(string sWorkNo, string sDate)
        {
            string sValue_new = "";
            try
            {
                OracleParameter sValue = new OracleParameter("v_ottype", OracleType.VarChar, 100);
                sValue.Direction = ParameterDirection.Output;
                OracleParameter[] paramList = new OracleParameter[] { new OracleParameter("v_workno", sWorkNo), new OracleParameter("v_date", sDate), sValue };
               
                int a = DalHelper.ExecuteNonQuery("prog_getempottype", CommandType.StoredProcedure, paramList);
                if (a > 0)
                {
                    sValue_new = Convert.ToString(paramList[2].Value);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return sValue_new;
        }

       
        public void Audit(DataTable dataTable, string appUser, SynclogModel logmodel)
        {
            OracleCommand command = new OracleCommand();
            command.Connection = DalHelper.Connection;
            command.Connection.Open();
            OracleTransaction trans = command.Connection.BeginTransaction();
            command.Transaction = trans;
            try
            {
                foreach (DataRow AuditRow in dataTable.Rows)
                {
                    string sql = "UPDATE GDS_ATT_ADVANCEAPPLY Set Status='2',Approver='" + appUser + "',ApproveDate=sysdate WHERE ID='" + AuditRow["ID"] + "' ";
                    command.CommandText = sql;
                    command.ExecuteNonQuery();
                    SaveLogData("U", AuditRow["WorkNo"].ToString(), command.CommandText, command,logmodel);
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
        }

        public void CancelAudit(DataTable dataTable, SynclogModel logmodel)
        {
            OracleCommand command = new OracleCommand();
            command.Connection = DalHelper.Connection;
            command.Connection.Open();
            OracleTransaction trans = command.Connection.BeginTransaction();
            command.Transaction = trans;
            try
            {
                foreach (DataRow AuditRow in dataTable.Rows)
                {
                    string sql = "UPDATE GDS_ATT_ADVANCEAPPLY  Set Status='0' WHERE ID='" + AuditRow["ID"] + "' ";
                    command.CommandText = sql;
                    command.ExecuteNonQuery();
                    SaveLogData("U", AuditRow["WorkNo"].ToString(), command.CommandText,command,logmodel);
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
        }

        public string GetWorkFlowOrgCode(string OrgCode, string BillTypeCode, string OTType)
        {
            string ReturnOrgCode = "";
            try
            {
                if (OrgCode != "")
                {
                    string sql
                     = "SELECT depcode " +
                         "  FROM (SELECT   b.depcode, " +
                         "                 a.orderid,rownum rowindex " +
                         "            FROM gds_wf_flowset a, " +
                         "                 (SELECT     LEVEL orderid, " +
                         "                             depcode " +
                         "                        FROM gds_sc_department " +
                         "                  START WITH depcode = '" + OrgCode + "' " +
                         "                  CONNECT BY PRIOR parentdepcode = depcode " +
                         "                    ORDER BY LEVEL) b " +
                         "           WHERE a.deptcode = b.depcode " +
                         "             AND a.formtype = '" + BillTypeCode + "' " +
                         "             AND a.reason1 = '" + OTType + "' " +
                         "        ORDER BY orderid) " +
                         " WHERE rowindex <= 1 ";
                    DataTable dt = DalHelper.ExecuteQuery(sql);
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

        public string SaveAuditData(string processFlag, string ID, string BillNoType, string AuditOrgCode, string BillTypeCode, string ApplyMan, string otmtype,string senduser, SynclogModel logmodel)
        {
            string strMax = "";
            OracleCommand command = new OracleCommand();
            command.Connection = DalHelper.Connection;
            command.Connection.Open();
            OracleTransaction trans = command.Connection.BeginTransaction();
            command.Transaction = trans;
            try
            {
                #region
                if (processFlag.Equals("Add"))
                {
                    #region  
                    command.CommandText = "Select BillTypeNo From GDS_WF_BILLTYPE Where BillTypeCode='" + BillNoType + "'";
                    BillNoType = Convert.ToString(command.ExecuteScalar());

                    command.CommandText = "SELECT MAX (billno) strMax  FROM GDS_ATT_ADVANCEAPPLY WHERE billno LIKE '" + BillNoType + AuditOrgCode + "'|| TO_CHAR (SYSDATE, 'yymm')|| '%'";
                    strMax = Convert.ToString(command.ExecuteScalar());
                    if (strMax.Length == 0)
                    {
                        strMax = BillNoType + AuditOrgCode + DateTime.Now.ToString("yyMM") + "0001";
                    }
                    else
                    {
                        int i = Convert.ToInt32(strMax.Substring(BillNoType.Length + 4)) + 1;
                        strMax = i.ToString().PadLeft(4, '0');
                        strMax = BillNoType + AuditOrgCode + DateTime.Now.ToString("yyMM") + strMax;
                    }
                    #endregion

                    command.CommandText = "UPDATE GDS_ATT_ADVANCEAPPLY SET Status='1' , BillNo =  '" + strMax + "' Where ID='" + ID + "'";
                    command.ExecuteNonQuery();
                    SaveLogData("U", strMax, command.CommandText, command,logmodel);
                    command.CommandText = "SELECT count(1) FROM GDS_ATT_BILL WHERE BillNo='" + strMax + "'";

                    if (Convert.ToDecimal(command.ExecuteScalar()) == 0M)
                    {
                        command.CommandText = "insert into GDS_ATT_BILL(BillNo,OrgCode,ApplyMan,ApplyDate,Status,BillTypeCode) values('" + strMax + "','" + AuditOrgCode + "','" + ApplyMan + "',sysdate,'0','" + BillTypeCode + "')";
                        command.ExecuteNonQuery();
                        SaveLogData("I", strMax, command.CommandText, command,logmodel);
                    }
                    else
                    {
                        command.CommandText = "update GDS_ATT_BILL set OrgCode='" + AuditOrgCode + "',ApplyMan='" + ApplyMan + "',ApplyDate=sysdate,Status='0',BillTypeCode='" + BillTypeCode + "' where BillNo='" + strMax + "'";
                        command.ExecuteNonQuery();
                        SaveLogData("U", strMax, command.CommandText, command,logmodel);
                    }
                    command.CommandText = "SELECT count(1) FROM GDS_ATT_AUDITSTATUS WHERE BillNo='" + strMax + "'";

                    if (Convert.ToDecimal(command.ExecuteScalar()) > 0M)
                    {
                        command.CommandText = "delete FROM GDS_ATT_AUDITSTATUS WHERE BillNo='" + strMax + "' ";
                        command.ExecuteNonQuery();
                        SaveLogData("D", strMax, command.CommandText, command,logmodel);
                    }
                   // command.CommandText = "insert into GDS_ATT_AUDITSTATUS(BillNo,AuditMan,OrderNo,AuditStatus,SendNotes) SELECT '" + strMax + "', FLOW_EMPNO, ORDERID,'0','N'   FROM GDS_WF_FLOWSET WHERE FORMTYPE='" + BillTypeCode + "' and DEPTCODE='" + AuditOrgCode + "' and REASON1='" + otmtype + "'";
                    command.CommandText = "insert into GDS_ATT_AUDITSTATUS(BillNo,AuditMan,OrderNo,AuditStatus,SendNotes,OLDAUDITMAN)  " +
                                      "select '" + strMax + "', nvl(getagentempno(FLOW_EMPNO,'" + AuditOrgCode + "'),FLOW_EMPNO) as FLOW_EMPNO, ORDERID,'0','N',decode(NVL (getagentempno (flow_empno, '" + AuditOrgCode + "'), flow_empno),flow_empno,'',flow_empno) as oldauditman  from (  " +
                                              " select * from GDS_WF_FLOWSET WHERE FORMTYPE='" + BillTypeCode + "' and DEPTCODE='" + AuditOrgCode + "' and REASON1='" + otmtype + "') " +
                                              " where  FLOW_EMPNO!='" + senduser + "'or (FLOW_EMPNO='" + senduser + "' and FLOW_LEVEL not in ('課級主管','部級主管')) ";
                    command.ExecuteNonQuery();
                    SaveLogData("I", strMax, command.CommandText, command,logmodel);
                }
                else if (processFlag.Equals("Modify"))
                {
                    strMax = BillNoType;
                    command.CommandText = "UPDATE GDS_ATT_ADVANCEAPPLY SET Status='1' , BillNo =  '" + strMax + "' Where ID='" + ID + "'";
                    command.ExecuteNonQuery();
                    SaveLogData("U", strMax, command.CommandText, command,logmodel);
                }
                trans.Commit();
                command.Connection.Close();
             #endregion
            }
            catch (Exception ex)
            {
                trans.Rollback();
                command.Connection.Close();
                throw ex;
            }
            return strMax;
        }

        public bool SaveAuditData(string ID, string BillNoType, string AuditOrgCode, string BillTypeCode, string ApplyMan, string otmtype, string senduser, OracleConnection OracleString, SynclogModel logmodel)
        {
            
            string strMax = string.Empty;
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
            try
            {
                command.Transaction = trans;
                command.CommandText = "Select BillTypeNo From GDS_WF_BILLTYPE Where BillTypeCode='" + BillNoType + "'";
                BillNoType = Convert.ToString(command.ExecuteScalar());

                command.CommandText = "SELECT MAX (billno) strMax  FROM GDS_ATT_ADVANCEAPPLY WHERE billno LIKE '" + BillNoType + AuditOrgCode + "'|| TO_CHAR (SYSDATE, 'yymm')|| '%'";
                strMax = Convert.ToString(command.ExecuteScalar());
                if (strMax.Length == 0)
                {
                    strMax = BillNoType + AuditOrgCode + DateTime.Now.ToString("yyMM") + "0001";
                }
                else
                {
                    int i = Convert.ToInt32(strMax.Substring(BillNoType.Length + 4)) + 1;
                    strMax = i.ToString().PadLeft(4, '0');
                    strMax = BillNoType + AuditOrgCode + DateTime.Now.ToString("yyMM") + strMax;
                }
                command.CommandText = "UPDATE GDS_ATT_ADVANCEAPPLY SET Status='1' , BillNo =  '" + strMax + "' Where ID='" + ID + "'";
                command.ExecuteNonQuery();
                SaveLogData("U", strMax, command.CommandText, command, logmodel);
                command.CommandText = "SELECT count(1) FROM GDS_ATT_BILL WHERE BillNo='" + strMax + "'";

                if (Convert.ToDecimal(command.ExecuteScalar()) == 0M)
                {
                    command.CommandText = "insert into GDS_ATT_BILL(BillNo,OrgCode,ApplyMan,ApplyDate,Status,BillTypeCode) values('" + strMax + "','" + AuditOrgCode + "','" + ApplyMan + "',sysdate,'0','" + BillTypeCode + "')";
                    command.ExecuteNonQuery();
                    SaveLogData("I", strMax, command.CommandText, command, logmodel);
                }
                else
                {
                    command.CommandText = "update GDS_ATT_BILL set OrgCode='" + AuditOrgCode + "',ApplyMan='" + ApplyMan + "',ApplyDate=sysdate,Status='0',BillTypeCode='" + BillTypeCode + "' where BillNo='" + strMax + "'";
                    command.ExecuteNonQuery();
                    SaveLogData("U", strMax, command.CommandText, command, logmodel);
                }
                command.CommandText = "SELECT count(1) FROM GDS_ATT_AUDITSTATUS WHERE BillNo='" + strMax + "'";

                if (Convert.ToDecimal(command.ExecuteScalar()) > 0M)
                {
                    command.CommandText = "delete FROM GDS_ATT_AUDITSTATUS WHERE BillNo='" + strMax + "' ";
                    command.ExecuteNonQuery();
                    SaveLogData("D", strMax, command.CommandText, command, logmodel);
                }
                // command.CommandText = "insert into GDS_ATT_AUDITSTATUS(BillNo,AuditMan,OrderNo,AuditStatus,SendNotes) SELECT '" + strMax + "', FLOW_EMPNO, ORDERID,'0','N'   FROM GDS_WF_FLOWSET WHERE FORMTYPE='" + BillTypeCode + "' and DEPTCODE='" + AuditOrgCode + "' and REASON1='" + otmtype + "'";
                command.CommandText = "insert into GDS_ATT_AUDITSTATUS(BillNo,AuditMan,OrderNo,AuditStatus,SendNotes,OLDAUDITMAN)  " +
                                  "select '" + strMax + "', nvl(getagentempno(FLOW_EMPNO,'" + AuditOrgCode + "'),FLOW_EMPNO) as FLOW_EMPNO, ORDERID,'0','N',decode(NVL (getagentempno (flow_empno, '" + AuditOrgCode + "'), flow_empno),flow_empno,'',flow_empno) as oldauditman  from (  " +
                                          " select * from GDS_WF_FLOWSET WHERE FORMTYPE='" + BillTypeCode + "' and DEPTCODE='" + AuditOrgCode + "' and REASON1='" + otmtype + "') " +
                                          " where  FLOW_EMPNO!='" + senduser + "'or (FLOW_EMPNO='" + senduser + "' and FLOW_LEVEL not in ('課級主管','部級主管')) ";
                command.ExecuteNonQuery();
                SaveLogData("I", strMax, command.CommandText, command, logmodel);
                trans.Commit();
                command.Connection.Close();
                return true;
            }
            catch (Exception ex)
            {
                trans.Rollback();
                command.Connection.Close();
                return false;
            }

        }

        public int SaveOrgAuditData(string processFlag, Dictionary<string, List<string>> diry, string BillNoType, string BillTypeCode, string ApplyMan, SynclogModel logmodel)
        {
            string strMax = "";
            int k = 0;
            foreach (string key in diry.Keys)
            {
                OracleCommand command = new OracleCommand();
                command.Connection = DalHelper.Connection;
                command.Connection.Open();
                OracleTransaction trans = command.Connection.BeginTransaction();
                command.Transaction = trans;
                try
                {
                    #region

                    string[] info = key.Split('^');
                    string AuditOrgCode = info[0];
                    string OTType = info[1];
                    if (processFlag.Equals("Add"))
                    {
                        #region
                        command.CommandText = "Select BillTypeNo From GDS_WF_BILLTYPE Where BillTypeCode='" + BillNoType + "'";
                        BillNoType = Convert.ToString(command.ExecuteScalar());

                        command.CommandText = "SELECT MAX (billno) strMax  FROM GDS_ATT_ADVANCEAPPLY WHERE billno LIKE '" + BillNoType + AuditOrgCode + "'|| TO_CHAR (SYSDATE, 'yymm')|| '%'";
                        strMax = Convert.ToString(command.ExecuteScalar());
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
                        #endregion
                        foreach (string ID in diry[key])
                        {
                            command.CommandText = "UPDATE GDS_ATT_ADVANCEAPPLY SET Status='1' , BillNo =  '" + strMax + "' Where ID='" + ID + "'";
                            command.ExecuteNonQuery();
                        }
                        SaveLogData("U", strMax, command.CommandText, command,logmodel);

                        command.CommandText = "SELECT count(1) FROM GDS_ATT_BILL WHERE BillNo='" + strMax + "'";

                        if (Convert.ToDecimal(command.ExecuteScalar()) == 0M)
                        {
                            command.CommandText = "insert into GDS_ATT_BILL(BillNo,OrgCode,ApplyMan,ApplyDate,Status,BillTypeCode) values('" + strMax + "','" + AuditOrgCode + "','" + ApplyMan + "',sysdate,'0','" + BillTypeCode + "')";
                            command.ExecuteNonQuery();
                            SaveLogData("I", strMax, command.CommandText, command,logmodel);
                        }
                        else
                        {
                            command.CommandText = "update GDS_ATT_BILL set OrgCode='" + AuditOrgCode + "',ApplyMan='" + ApplyMan + "',ApplyDate=sysdate,Status='0',BillTypeCode='" + BillTypeCode + "' where BillNo='" + strMax + "'";
                            command.ExecuteNonQuery();
                            SaveLogData("U", strMax, command.CommandText, command,logmodel);
                        }
                        command.CommandText = "SELECT count(1) FROM GDS_ATT_AUDITSTATUS WHERE BillNo='" + strMax + "'";

                        if (Convert.ToDecimal(command.ExecuteScalar()) > 0M)
                        {
                            command.CommandText = "delete FROM GDS_ATT_AUDITSTATUS WHERE BillNo='" + strMax + "' ";
                            command.ExecuteNonQuery();
                            SaveLogData("D", strMax, command.CommandText, command,logmodel);
                        }
                        if (diry[key].Count == 1)
                        {
                            command.CommandText = "select WORKNO from  GDS_ATT_ADVANCEAPPLY where ID='" + diry[key][0].ToString() + "'";
                            string senduser = Convert.ToString(command.ExecuteScalar());
                            command.CommandText = "insert into GDS_ATT_AUDITSTATUS(BillNo,AuditMan,OrderNo,AuditStatus,SendNotes,OLDAUDITMAN)  " +
                               "select '" + strMax + "',  nvl(getagentempno(FLOW_EMPNO,'" + AuditOrgCode + "'),FLOW_EMPNO) as FLOW_EMPNO, ORDERID,'0','N',decode(NVL (getagentempno (flow_empno, '" + AuditOrgCode + "'), flow_empno),flow_empno,'',flow_empno) as oldauditman  from (  " +
                                       " select * from GDS_WF_FLOWSET WHERE FORMTYPE='" + BillTypeCode + "' and DEPTCODE='" + AuditOrgCode + "' and REASON1='" + OTType + "') " +
                                       " where  FLOW_EMPNO!='" + senduser + "'or (FLOW_EMPNO='" + senduser + "' and FLOW_LEVEL not in ('課級主管','部級主管')) ";
                        }
                        else
                        {
                            command.CommandText = "insert into GDS_ATT_AUDITSTATUS(BillNo,AuditMan,OrderNo,AuditStatus,SendNotes,OLDAUDITMAN) SELECT '" + strMax + "', nvl(getagentempno(FLOW_EMPNO,'" + AuditOrgCode + "'),FLOW_EMPNO) as FLOW_EMPNO, ORDERID,'0','N',decode(NVL (getagentempno (flow_empno, '" + AuditOrgCode + "'), flow_empno),flow_empno,'',flow_empno) as oldauditman   FROM GDS_WF_FLOWSET WHERE FORMTYPE='" + BillTypeCode + "' and DEPTCODE='" + AuditOrgCode + "' and REASON1='" + OTType + "'";
                        }

                       //command.CommandText = "insert into GDS_ATT_AUDITSTATUS(BillNo,AuditMan,OrderNo,AuditStatus,SendNotes)  " +
                       //                 "select '" + strMax + "', FLOW_EMPNO, ORDERID,'0','N'  from (  " +
                       //                         " select * from GDS_WF_FLOWSET WHERE FORMTYPE='" + BillTypeCode + "' and DEPTCODE='" + AuditOrgCode + "' and REASON1='" + OTType + "') " +
                       //                         " where  FLOW_EMPNO!='" + ApplyMan + "'or (FLOW_EMPNO='" + ApplyMan + "' and FLOW_LEVEL not in ('課級主管','部級主管')) ";

                        command.ExecuteNonQuery();
                        SaveLogData("I", strMax, command.CommandText, command,logmodel);
                    }
                    else if (processFlag.Equals("Modify"))
                    {
                        strMax = BillNoType;
                        foreach (string ID in diry[key])
                        {
                            command.CommandText = "UPDATE GDS_ATT_ADVANCEAPPLY SET Status='1' , BillNo =  '" + strMax + "' Where ID='" + ID + "'";
                            command.ExecuteNonQuery();
                        }
                        SaveLogData("U", strMax, command.CommandText, command,logmodel);
                    }

                    trans.Commit();
                    command.Connection.Close();
                    k++;
                    #endregion
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    command.Connection.Close();
                    throw ex;
                }
            }
            return k;
        }



        public double GetOtHours(string WorkNo, string OTDate, string StrBtime, string StrEtime, string OTType)
        {
            try
            {
                double OtHours = 0.0;
                double RestHours = 0.0;
                string condition = "";
                if (OTType.Length != 0)
                {
                    DateTime dtTempBeginTime;
                    DateTime dtTempEndTime;
                    string dtShiftOnTime;
                    string dtShiftOffTime;
                    string dtAMRestSTime;
                    string dtAMRestETime;
                    TimeSpan tsOTHours;
                    if (OTType.Equals("G4") && (TimeSpan.Parse(Convert.ToDateTime(StrBtime).ToString("HH:mm")) < TimeSpan.Parse("06:30")))
                    {
                        OTDate = Convert.ToDateTime(OTDate).AddDays(-1.0).ToString("yyyy/MM/dd");
                    }
                    //string strShiftNo = ((ServiceLocator)this.Session["serviceLocator"]).GetFunctionData().GetShiftNo(WorkNo.ToUpper(), OTDate);
                    string strShiftNo = this.GetShiftNo(WorkNo.ToUpper(), OTDate);
                    if (strShiftNo.Length == 0)
                    {
                        return OtHours;
                    }
                    if ((StrBtime.Length > 8) & (StrEtime.Length > 8))
                    {
                        dtTempBeginTime = DateTime.Parse(StrBtime);
                        dtTempEndTime = DateTime.Parse(StrEtime);
                    }
                    else
                    {
                        dtTempBeginTime = DateTime.Parse(OTDate + " " + StrBtime);
                        dtTempEndTime = DateTime.Parse(OTDate + " " + StrEtime);
                        SortedList list = new SortedList();
                        list = this.ReturnOTTTime(WorkNo, OTDate, dtTempBeginTime, dtTempEndTime, strShiftNo);
                        dtTempBeginTime = Convert.ToDateTime(list.GetByIndex(list.IndexOfKey("A")));
                        dtTempEndTime = Convert.ToDateTime(list.GetByIndex(list.IndexOfKey("B")));
                    }
                    if (OTType.Equals("G4") && !(dtTempBeginTime.ToString("yyyy/MM/dd").Equals(dtTempEndTime.ToString("yyyy/MM/dd")) || Convert.ToDateTime(StrBtime).ToString("HH:mm").Equals("00:00")))
                    {
                        return OtHours;
                    }
                    string dtBtime = dtTempBeginTime.ToString("yyyy/MM/dd HH:mm");
                    string dtEtime = dtTempEndTime.ToString("yyyy/MM/dd HH:mm");
                    condition = "select OnDutyTime,OffDutyTime,AMRestSTime,AMRestETime,PMRestSTime,PMRestETime,ShiftType from GDS_ATT_WORKSHIFT where ShiftNo='" + strShiftNo + "'";
                    //DataTable sdt = ((ServiceLocator)this.Session["serviceLocator"]).GetFunctionData().GetDataSetBySQL(condition).Tables["TempTable"];
                    DataTable sdt = this.GetDataSetBySQL(condition).Tables["TempTable"];
                    if (sdt.Rows.Count == 0)
                    {
                        return OtHours;
                    }
                    string ShiftOnTime = Convert.ToString(sdt.Rows[0]["OnDutyTime"]);
                    string ShiftOffTime = Convert.ToString(sdt.Rows[0]["OffDutyTime"]);
                    string AMRestSTime = Convert.ToString(sdt.Rows[0]["AMRestSTime"]);
                    string AMRestETime = Convert.ToString(sdt.Rows[0]["AMRestETime"]);
                    string PMRestSTime = Convert.ToString(sdt.Rows[0]["PMRestSTime"]);
                    string PMRestETime = Convert.ToString(sdt.Rows[0]["PMRestETime"]);
                    string ShiftType = Convert.ToString(sdt.Rows[0]["ShiftType"]);
                    string dtPMRestSTime = "";
                    string dtPMRestETime = "";
                    if (TimeSpan.Parse(ShiftOnTime) < TimeSpan.Parse(ShiftOffTime))
                    {
                        dtShiftOnTime = DateTime.Parse(OTDate + " " + ShiftOnTime).ToString("yyyy/MM/dd HH:mm");
                        dtShiftOffTime = DateTime.Parse(OTDate + " " + ShiftOffTime).ToString("yyyy/MM/dd HH:mm");
                        dtAMRestSTime = DateTime.Parse(OTDate + " " + AMRestSTime).ToString("yyyy/MM/dd HH:mm");
                        dtAMRestETime = DateTime.Parse(OTDate + " " + AMRestETime).ToString("yyyy/MM/dd HH:mm");
                        if ((PMRestSTime.Length > 0) && (PMRestETime.Length > 0))
                        {
                            if (TimeSpan.Parse(PMRestSTime) <= TimeSpan.Parse(PMRestETime))
                            {
                                dtPMRestSTime = DateTime.Parse(OTDate + " " + PMRestSTime).ToString("yyyy/MM/dd HH:mm");
                                dtPMRestETime = DateTime.Parse(OTDate + " " + PMRestETime).ToString("yyyy/MM/dd HH:mm");
                            }
                            else
                            {
                                dtPMRestSTime = DateTime.Parse(OTDate + " " + PMRestSTime).ToString("yyyy/MM/dd HH:mm");
                                dtPMRestETime = DateTime.Parse(OTDate + " " + PMRestETime).AddDays(1.0).ToString("yyyy/MM/dd HH:mm");
                            }
                        }
                    }
                    else
                    {
                        dtShiftOnTime = DateTime.Parse(OTDate + " " + ShiftOnTime).ToString("yyyy/MM/dd HH:mm");
                        dtShiftOffTime = DateTime.Parse(OTDate + " " + ShiftOffTime).AddDays(1.0).ToString("yyyy/MM/dd HH:mm");
                        if ((TimeSpan.Parse(AMRestSTime) <= TimeSpan.Parse(AMRestETime)) && (TimeSpan.Parse(ShiftOnTime) < TimeSpan.Parse(AMRestETime)))
                        {
                            dtAMRestSTime = DateTime.Parse(OTDate + " " + AMRestSTime).ToString("yyyy/MM/dd HH:mm");
                            dtAMRestETime = DateTime.Parse(OTDate + " " + AMRestETime).ToString("yyyy/MM/dd HH:mm");
                        }
                        else if ((TimeSpan.Parse(AMRestSTime) <= TimeSpan.Parse(AMRestETime)) && (TimeSpan.Parse(ShiftOnTime) > TimeSpan.Parse(AMRestETime)))
                        {
                            dtAMRestSTime = DateTime.Parse(OTDate + " " + AMRestSTime).AddDays(1.0).ToString("yyyy/MM/dd HH:mm");
                            dtAMRestETime = DateTime.Parse(OTDate + " " + AMRestETime).AddDays(1.0).ToString("yyyy/MM/dd HH:mm");
                        }
                        else
                        {
                            dtAMRestSTime = DateTime.Parse(OTDate + " " + AMRestSTime).ToString("yyyy/MM/dd HH:mm");
                            dtAMRestETime = DateTime.Parse(OTDate + " " + AMRestETime).AddDays(1.0).ToString("yyyy/MM/dd HH:mm");
                        }
                        if ((PMRestSTime.Length > 0) && (PMRestETime.Length > 0))
                        {
                            dtPMRestSTime = DateTime.Parse(OTDate + " " + PMRestSTime).AddDays(1.0).ToString("yyyy/MM/dd HH:mm");
                            dtPMRestETime = DateTime.Parse(OTDate + " " + PMRestETime).AddDays(1.0).ToString("yyyy/MM/dd HH:mm");
                        }
                    }
                    if (string.Compare(dtBtime, dtEtime) >= 0)
                    {
                        return OtHours;
                    }
                    if (OTType.Equals("G1"))
                    {
                        if (((string.Compare(dtBtime, dtShiftOnTime) >= 0) && (string.Compare(dtShiftOffTime, dtBtime) > 0)) || ((string.Compare(dtEtime, dtShiftOnTime) > 0) && (string.Compare(dtShiftOffTime, dtEtime) >= 0)))
                        {
                            return OtHours;
                        }
                        if (string.Compare(dtShiftOnTime, dtBtime) > 0)
                        {
                            if (string.Compare(dtEtime, dtShiftOnTime) > 0)
                            {
                                return OtHours;
                            }
                            tsOTHours = (TimeSpan)(dtTempEndTime - dtTempBeginTime);
                            OtHours = tsOTHours.TotalMinutes;
                        }
                        else if (string.Compare(dtBtime, dtShiftOffTime) >= 0)
                        {
                            if (dtPMRestSTime.Length > 0)
                            {
                                if ((string.Compare(dtBtime, dtPMRestSTime) >= 0) && (string.Compare(dtPMRestETime, dtBtime) > 0))
                                {
                                    if ((string.Compare(dtEtime, dtPMRestSTime) > 0) && (string.Compare(dtPMRestETime, dtEtime) >= 0))
                                    {
                                        return OtHours;
                                    }
                                    tsOTHours = (TimeSpan)(dtTempEndTime - dtTempBeginTime);
                                    OtHours = tsOTHours.TotalMinutes;
                                    tsOTHours = (TimeSpan)(Convert.ToDateTime(dtPMRestETime) - dtTempBeginTime);
                                    RestHours += tsOTHours.TotalMinutes;
                                }
                                else if (string.Compare(dtPMRestSTime, dtBtime) >= 0)
                                {
                                    if (string.Compare(dtPMRestSTime, dtEtime) >= 0)
                                    {
                                        tsOTHours = (TimeSpan)(dtTempEndTime - dtTempBeginTime);
                                        OtHours = tsOTHours.TotalMinutes;
                                    }
                                    else if (string.Compare(dtPMRestETime, dtEtime) >= 0)
                                    {
                                        tsOTHours = (TimeSpan)(Convert.ToDateTime(dtPMRestSTime) - dtTempBeginTime);
                                        OtHours = tsOTHours.TotalMinutes;
                                    }
                                    else
                                    {
                                        tsOTHours = (TimeSpan)(dtTempEndTime - dtTempBeginTime);
                                        OtHours = tsOTHours.TotalMinutes;
                                        tsOTHours = (TimeSpan)(Convert.ToDateTime(dtPMRestETime) - Convert.ToDateTime(dtPMRestSTime));
                                        RestHours = tsOTHours.TotalMinutes;
                                    }
                                }
                                else
                                {
                                    tsOTHours = (TimeSpan)(dtTempEndTime - dtTempBeginTime);
                                    OtHours = tsOTHours.TotalMinutes;
                                }
                            }
                            else
                            {
                                tsOTHours = (TimeSpan)(dtTempEndTime - dtTempBeginTime);
                                OtHours = tsOTHours.TotalMinutes;
                            }
                        }
                    }
                    else
                    {
                        if (string.Compare(dtShiftOnTime, dtBtime) > 0)
                        {
                            dtTempBeginTime = Convert.ToDateTime(dtShiftOnTime);
                            dtBtime = dtTempBeginTime.ToString("yyyy/MM/dd HH:mm");
                        }
                        if ((string.Compare(dtAMRestETime, dtBtime) >= 0) && (string.Compare(dtBtime, dtAMRestSTime) >= 0))
                        {
                            dtTempBeginTime = Convert.ToDateTime(dtAMRestETime);
                            dtBtime = dtTempBeginTime.ToString("yyyy/MM/dd HH:mm");
                        }
                        if ((string.Compare(dtAMRestETime, dtEtime) >= 0) && (string.Compare(dtEtime, dtAMRestSTime) >= 0))
                        {
                            dtTempEndTime = Convert.ToDateTime(dtAMRestSTime);
                            dtEtime = dtTempEndTime.ToString("yyyy/MM/dd HH:mm");
                        }
                        if ((string.Compare(dtPMRestETime, dtBtime) >= 0) && (string.Compare(dtBtime, dtPMRestSTime) >= 0))
                        {
                            dtTempBeginTime = Convert.ToDateTime(dtPMRestETime);
                            dtBtime = dtTempBeginTime.ToString("yyyy/MM/dd HH:mm");
                        }
                        if ((string.Compare(dtPMRestETime, dtEtime) >= 0) && (string.Compare(dtEtime, dtPMRestSTime) >= 0))
                        {
                            dtTempEndTime = Convert.ToDateTime(dtPMRestSTime);
                            dtEtime = dtTempEndTime.ToString("yyyy/MM/dd HH:mm");
                        }
                        if (string.Compare(dtBtime, dtEtime) >= 0)
                        {
                            return OtHours;
                        }
                        if ((string.Compare(dtAMRestSTime, dtBtime) >= 0) && (string.Compare(dtEtime, dtAMRestETime) >= 0))
                        {
                            tsOTHours = (TimeSpan)(Convert.ToDateTime(dtAMRestETime) - Convert.ToDateTime(dtAMRestSTime));
                            RestHours += tsOTHours.TotalMinutes;
                            if ((dtPMRestETime.Length > 0) && (string.Compare(dtEtime, dtPMRestETime) > 0))
                            {
                                tsOTHours = (TimeSpan)(Convert.ToDateTime(dtPMRestETime) - Convert.ToDateTime(dtPMRestSTime));
                                RestHours += tsOTHours.TotalMinutes;
                            }
                        }
                        else if ((dtPMRestETime.Length > 0) && ((string.Compare(dtPMRestSTime, dtBtime) > 0) && (string.Compare(dtEtime, dtPMRestETime) > 0)))
                        {
                            tsOTHours = (TimeSpan)(Convert.ToDateTime(dtPMRestETime) - Convert.ToDateTime(dtPMRestSTime));
                            RestHours += tsOTHours.TotalMinutes;
                        }
                        tsOTHours = (TimeSpan)(dtTempEndTime - dtTempBeginTime);
                        OtHours = tsOTHours.TotalMinutes;
                    }
                    //if (((ServiceLocator)this.Session["serviceLocator"]).GetFunctionData().GetValue("select nvl(max(paravalue),'Y') from bfw_parameter where paraname='IsOTMHours10M'").Equals("Y"))
                    if (this.GetValue("select nvl(max(paravalue),'Y') from GDS_SC_PARAMETER where paraname='IsOTMHours10M'").Equals("Y"))
                    {
                        OtHours = Math.Round((double)(Math.Floor((double)((OtHours - RestHours) / 10.0)) / 6.0), 1);
                    }
                    else
                    {
                        OtHours = Math.Round((double)(((OtHours - RestHours) / 60.0) * 100.0)) / 100.0;
                        if ((OtHours % 0.5) != 0.0)
                        {
                            double ihours = Math.Floor(OtHours);
                            if (OtHours > (ihours + 0.5))
                            {
                                OtHours = ihours + 0.5;
                            }
                            else
                            {
                                OtHours = ihours;
                            }
                        }
                    }
                    if ((OtHours >= 20.0) || (OtHours < 0.0))
                    {
                        OtHours = 0.0;
                    }
                }
                return OtHours;
            }
            catch (Exception)
            {
                return 0.0;
            }
        }

        public string GetOTMFlag(string WorkNo, string OtDate)
        {
            string nwFlag = "";
            string value = "";

            DataTable tempDataTable = DalHelper.ExecuteQuery(@"select NoWorkFlag,MonthLMT from GDS_ATT_CONFIG");
            if (tempDataTable != null)
            {
                if (tempDataTable.Rows.Count > 0)
                {
                    nwFlag = tempDataTable.Rows[0]["NoWorkFlag"].ToString().Trim();
                }

                if (nwFlag == "Y")//連續上班六天必須休息1天Flag
                {
                    OracleParameter sValue = new OracleParameter("o_result", OracleType.VarChar, 1);
                    sValue.Direction = ParameterDirection.Output;
                    int a = DalHelper.ExecuteNonQuery("proc_isworksixday", CommandType.StoredProcedure, new OracleParameter("v_odate", OtDate), new OracleParameter("v_workno", WorkNo), sValue);
                    if (a > 0)
                    {
                        value = Convert.ToString(sValue.Value);
                    }
                }
            }
            return value;
        }

        public DataView ExceltoDataView(string strFilePath)
        {
            DataView dv;
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
                DataSet ds = new DataSet();
                adp.Fill(ds, "Excel");
                dv = ds.Tables[0].DefaultView;
                conn.Close();
            }
            catch (Exception)
            {
                Exception strEx = new Exception("請確認是否使用模板上傳(上傳的Excel中第一個工作表名稱是否為Sheet1)");
                throw strEx;
            }
            return dv;
        }

        public int GetVWorkNoCount(string WorkNo, string sqlDep)
        {
            int iValue = 0;
            //if (sqlDep == "")
            //{
            //    sqlDep = " 'D00200004'";
            //}
            //string sqlDepStr = " and Dcode IN(" + sqlDep + ")";
            try
            {
                string CommandText = "select NVL(count(WorkNO),0) from GDS_ATT_EMPLOYEE where WorkNO='" + WorkNo + "' and status='0' " + sqlDep;
                //string CommandText = "select NVL(count(WorkNO),0) from gds_att_Employee where WorkNO='" + WorkNo + "' and status='0' " + sqlDepStr;
                iValue = Convert.ToInt32(DalHelper.ExecuteScalar(CommandText));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iValue;
        }

        
       


    }

}