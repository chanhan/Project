/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： OTMActivityApplyDal.cs
 * 檔功能描述： 免卡人員加班導入數據操作類
 * 
 * 版本：1.0
 * 創建標識： 張明強 2011.12.23
 * 
 * @Modify by 劉小明 2012-02-24
 * @Description:增加【送簽】與【組織送簽功能】
 * 
 */

using System;
using System.Data;
using System.Data.OracleClient;
using GDSBG.MiABU.Attendance.IDAL.KQM.OTM;
using GDSBG.MiABU.Attendance.Model.KQM.OTM;
using System.Collections;
using System.Collections.Generic;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;

namespace GDSBG.MiABU.Attendance.OracleDAL.KQM.OTM
{
    public class OTMActivityApplyDal : DALBase<ActivityModel>, IOTMActivityApplyDal
    {
        /// <summary>
        /// 獲取審核轉檯(未審核,已審核)
        /// </summary>
        /// <returns></returns>
        public DataTable GetOTStatus()
        {
            //DataTable dt = DalHelper.ExecuteQuery(@"select * from GDS_ATT_TYPEDATA WHERE DATATYPE='OTMAdvanceApplyStatus' and datacode in ('0','2') ORDER BY OrderId");
            DataTable dt = DalHelper.ExecuteQuery(@"select * from GDS_ATT_TYPEDATA WHERE DATATYPE='OTMAdvanceApplyStatus'  ORDER BY OrderId");
            return dt;
        }

        /// <summary>
        /// 分頁查詢免卡人員加班導入資料
        /// </summary>
        /// <param name="model"></param>
        /// <param name="?"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        //public DataTable GetActivityApplyList(ActivityModel model, int pageIndex, int pageSize, out int totalCount)
        //{
        //    return DalHelper.Select(model, true, pageIndex, pageSize, out totalCount);
        //}
        public DataTable GetActivityApplyList(ActivityModel model,string sqlDep, string depCode, string workNoStrings, string dateFrom, string dateTo, int pageIndex, int pageSize, out int totalCount)
                                    
        {
            string strCon = "";
            string depName = model.DepName.ToString();
            model.DepName = "";
            
            List<OracleParameter> listPara = DalHelper.CreateConditionParameters(model, true, "a", out strCon);
           // string cmdText = @"select ID,workno,BillNo,ottype,otdate, confirmhours, workdesc,remark,status,yearmonth, create_date, create_user,
          //                  update_date,update_user,depname,DEPCODE,localname, statusname,starttime,endtime from GDS_ATT_ACTIVITY_V a where 1=1 ";
            string cmdText = @"select * from (SELECT a.ID, a.workno, a.billno, a.ottype, a.otdate, a.confirmhours,
       a.workdesc, a.remark, a.status, a.yearmonth, a.create_date,
       a.create_user, a.update_date, a.update_user, a.depname, a.depcode,
       a.localname, a.statusname, a.starttime, a.endtime,
       (SELECT localname
          FROM gds_att_employee e
         WHERE e.workno = UPPER (b.approver)) approvername, approvedate,
       apremark
  FROM gds_att_activity_v a, gds_att_activity b
 WHERE 1 = 1 AND a.workno = b.workno AND a.billno = b.billno AND a.ID = b.ID ";
            cmdText = cmdText + strCon;
            if (!string.IsNullOrEmpty(depName))
            {
                cmdText += "AND a.depcode IN ((" + sqlDep + ") INTERSECT SELECT DepCode FROM gds_sc_department START WITH depname = :depName CONNECT BY PRIOR depcode = parentdepcode) ";
                listPara.Add(new OracleParameter(":depName", depName));
            }
            else
            {
                cmdText += " AND a.depcode IN ("+sqlDep+") ";
            }
            

            if (!String.IsNullOrEmpty(workNoStrings))
            {
                cmdText = cmdText + " and a.workno in (SELECT char_list  FROM TABLE (gds_sc_chartotable ('" + workNoStrings + "','§')))";
            }
            if (!string.IsNullOrEmpty(dateFrom) && !string.IsNullOrEmpty(dateTo))
            {
                cmdText += " and (a.otdate between to_date(:dateFrom,'yyyy/mm/dd') and to_date(:dateTo,'yyyy/mm/dd')) ";
                listPara.Add(new OracleParameter(":dateFrom", dateFrom));
                listPara.Add(new OracleParameter(":dateTo", dateTo));
            }
            cmdText += " ) ";

            DataTable dt = DalHelper.ExecutePagerQuery(cmdText, pageIndex, pageSize, out totalCount, listPara.ToArray());
            return dt;

        }

        public DataTable getActivityApplyList(string depName, string sqlDep, string depCode, string workNoStrings, string dateFrom, string dateTo, string condition, int pageIndex, int pageSize, out int totalCount)
        {

            string cmdText = @"select * from (SELECT a.ID, a.workno, a.billno, a.ottype, a.otdate, a.confirmhours,
       a.workdesc, a.remark, a.status, a.yearmonth, a.create_date,
       a.create_user, a.update_date, a.update_user, a.depname, a.depcode,
       a.localname, a.statusname, a.starttime, a.endtime,
       (SELECT localname
          FROM gds_att_employee e
         WHERE e.workno = UPPER (b.approver)) approvername, approvedate,
       apremark
  FROM gds_att_activity_v a, gds_att_activity b
 WHERE 1 = 1 AND a.workno = b.workno  AND a.ID = b.ID ";
           
            if (!string.IsNullOrEmpty(depName))
            {
                cmdText += "AND a.depcode IN ((" + sqlDep + ") INTERSECT SELECT DepCode FROM gds_sc_department START WITH depname = '" + depName + "' CONNECT BY PRIOR depcode = parentdepcode) ";
                
            }
            else
            {
                if (!string.IsNullOrEmpty(sqlDep))
                {
                    cmdText += " AND a.depcode IN (" + sqlDep + ") ";
                }
            }


            if (!String.IsNullOrEmpty(workNoStrings))
            {
                cmdText = cmdText + " and a.workno in (SELECT char_list  FROM TABLE (gds_sc_chartotable ('" + workNoStrings + "','§')))";
            }
            if (!string.IsNullOrEmpty(dateFrom) && !string.IsNullOrEmpty(dateTo))
            {
                cmdText += " and (a.otdate between to_date('" + dateFrom + "','yyyy/mm/dd') and to_date('" + dateTo + "','yyyy/mm/dd')) ";
               
            }
            cmdText += condition;
            cmdText += " ) ";
            // string cardSql = "SELECT B.LOCALNAME,B.SEX,B.DEPCODE,B.DEPNAME,A.* FROM GDS_ATT_MAKEUP A LEFT JOIN GDS_ATT_EMPLOYEE B ON A.WORKNO = B.WORKNO  where 1=1 " + condition;
           // string cardSql = " select *  FROM  ( SELECT a.DISSIGNRMARK,a.ID, a.workno, a.kqdate, a.cardtime, a.makeuptype, a.status,a.Reasontype,a.ReasonRemark, a.approver, a.apremark, a.approvedate, a.billno, (select LocalName From gds_att_Employee e Where e.WorkNo=a.modifier)modifier ,a.DecSalary, (select (select ShiftNo||':'||ShiftDesc||'['||shifttype||']' from gds_att_WorkShift c where c.ShiftNo=w.ShiftNo) from gds_att_KaoQinData w where w.workno=a.workno and w.KQdate=a.KQdate)  ShiftDesc,  (SELECT depname FROM gds_sc_department s WHERE s.LevelCode='2' START WITH s.depcode=b.depcode CONNECT BY s.depcode = PRIOR s.parentdepcode) buname, (Select ReasonName from gds_att_EXCEPTREASON b where b.REASONNO=a.Reasontype) ReasonName,  (Select SALARYFLAG from gds_att_EXCEPTREASON b where b.REASONNO=a.Reasontype) SalaryFlag,  (SELECT DATAVALUE FROM gds_att_TYPEDATA b WHERE b.DATATYPE='KQMMakeup' and b.DATACODE=a.makeuptype) MakeupTypeName,  (SELECT DATAVALUE FROM gds_att_TYPEDATA b WHERE b.DATATYPE='ApproveFlag' and b.DATACODE=a.Status) StatusName,  (select LocalName From gds_att_Employee e Where e.WorkNo=upper(a.Approver)) ApproverName, a.modifydate,b.localname,b.depname,b.dname,b.dcode,b.depcode FROM gds_att_Makeup a inner join  gds_att_employee b on  a.workno=b.workno(+) where 1=1 " + condition + "  )";
            //return DalHelper.ExecuteQuery(cardSql);
            return DalHelper.ExecutePagerQuery(cmdText, pageIndex, pageSize, out totalCount, null);

        }


        /// <summary>
        /// 新增功能
        /// </summary>
        /// <param name="model">要新增的免卡人員加班導入Model</param>
        /// <returns>是否成功</returns>
        public bool AddActivityApply(ActivityModel model,SynclogModel logmodel)
        {
            return DalHelper.Insert(model,logmodel) != -1;
        }

        /// <summary>
        /// 獲取員工資料
        /// </summary>
        /// <returns></returns>
        public DataTable GetEmp(string empNo, string sqlDep)
        {
            DataTable dt = DalHelper.ExecuteQuery("select * from gds_att_employee_v a WHERE a.WorkNO=:empNo and a.status='0' AND exists (SELECT 1 FROM (" + sqlDep + ") e where e.DepCode=a.DCode ) ", new OracleParameter(":empNo", empNo));
            return dt;
        }


        /// <summary>
        /// 將datatable轉換為List
        /// </summary>
        /// <param name="dt">徐轉化的Datatable</param>
        /// <returns></returns>
        public List<ActivityModel> GetList(DataTable dt)
        {
            return OrmHelper.SetDataTableToList(dt);
        }


        /// <summary>
        /// 刪除免卡人員加班信息
        /// </summary>
        /// <param name="model"> 要刪除的model</param>
        /// <returns></returns>
        public int DeleteActivity(ActivityModel model,SynclogModel logmodel)
        {
            return DalHelper.Delete(model,logmodel);
        }


        /// <summary>
        /// 根據主鍵修改功能
        /// </summary>
        /// <param name="model">要修改的功能Model</param>
        /// <returns>是否成功</returns>
        public bool UpdateActivityByKey(ActivityModel model,SynclogModel logmodel)
        {
            return DalHelper.UpdateByKey(model,logmodel) != -1;
        }

        /// <summary>
        /// 獲取員工某月加班時數
        /// </summary>
        /// <returns></returns>
        public DataTable GetMonthAllOverTime(string empNo, string yearMonth)
        {
            DataTable dt = DalHelper.ExecuteQuery("SELECT MonthTotal FROM GDS_ATT_MonthDetail WHERE WorkNO=:empNo AND YearMonth=:yearMonth", new OracleParameter(":empNo", empNo), new OracleParameter(":yearMonth", yearMonth));
            return dt;
        }

        /// <summary>
        /// 獲取加班類型
        /// </summary>
        /// <param name="sWorkNo"></param>
        /// <param name="sDate"></param>
        /// <returns></returns>
        public string GetOTType(string workNo, string date)
        {
            string value = "";
            OracleParameter ottype = new OracleParameter("v_ottype", OracleType.VarChar, 20);
            ottype.Direction = ParameterDirection.Output;
            int a = DalHelper.ExecuteNonQuery("GDS_ATT_getempottype_pro", CommandType.StoredProcedure,
                new OracleParameter("v_workno", workNo), new OracleParameter("v_date", date), ottype);
            if (a > 0)
            {
                value = Convert.ToString(ottype.Value);
            }
            return value;
        }

        /// <summary>
        /// 獲取OTMFlag
        /// </summary>
        /// <param name="WorkNo"></param>
        /// <param name="OtDate"></param>
        /// <returns></returns>
        public string GetOTMFlag(string WorkNo, string OtDate)
        {
            string nwFlag = "";
            string value = "";

            DataTable tempDataTable = DalHelper.ExecuteQuery(@"select NoWorkFlag,MonthLMT from OTM_Config");
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
                    int a = DalHelper.ExecuteNonQuery("proc_isworksixday", CommandType.StoredProcedure,
                      new OracleParameter("v_odate", OtDate), new OracleParameter("v_workno", WorkNo), sValue);
                    if (a > 0)
                    {
                        value = Convert.ToString(sValue.Value);
                    }
                }

            }
            return value;

        }

        /// <summary>
        /// 獲取員工信息
        /// </summary>
        /// <param name="EmployeeNo"></param>
        /// <returns></returns>
        public DataTable GetVData(string EmployeeNo,string sqlDep)
        {

            DataTable dt = DalHelper.ExecuteQuery("select * from gds_att_employee_v a where a.workno=:empNo  AND exists (SELECT 1 FROM (" + sqlDep + ") e where e.DepCode=a.DCode)", new OracleParameter(":empNo", EmployeeNo));
            return dt;
        }

        /// <summary>
        /// 獲取 LHZBData
        /// </summary>
        /// <param name="EmployeeNo"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        public DataTable GetLHZBData(string EmployeeNo, string ID, string otDate, string flag)
        {
            DataTable dt = new DataTable();
            string cmdText = @"SELECT * FROM (SELECT a.*, TO_CHAR (a.otdate, 'DY') AS week,TRIM (INITCAP (TO_CHAR (a.otdate,'day','NLS_DATE_LANGUAGE=American' ) ) ) enweek,
                 b.localname, b.overtimetype, b.dcode, b.levelname,b.managername,(SELECT datavalue  FROM gds_att_typedata c WHERE c.datatype = 'OTMAdvanceApplyStatus'
                 AND c.datacode = a.status) statusname, b.dname depname, (SELECT  depname FROM gds_sc_department s WHERE s.levelcode = '2'
                 START WITH s.depcode = b.depcode CONNECT BY s.depcode = PRIOR s.parentdepcode) buname, (SELECT localname FROM gds_att_employee e
                 WHERE e.workno = a.update_user) modifyname  FROM gds_att_activity a, gds_att_employee b WHERE b.workno = a.workno ";
            EmployeeNo = EmployeeNo.ToUpper();
            if (flag == "condition1")
            {
                cmdText += " and a.status='0' and  a.ID=:ID )";
                dt = DalHelper.ExecuteQuery(cmdText, new OracleParameter(":ID", ID));
            }
            if (flag == "condition2")
            {
                string sqlDep = otDate;
                cmdText += " and a.WorkNO=:empNo AND a.ID=:ID AND exists (SELECT 1 FROM (" + sqlDep + ") e where e.DepCode=b.DCode) )";
                dt = DalHelper.ExecuteQuery(cmdText, new OracleParameter(":empNo", EmployeeNo), new OracleParameter(":ID", ID));
            }
            if (flag == "condition3")
            {
                cmdText += " and 1=2 )";
                dt = DalHelper.ExecuteQuery(cmdText);
            }
            if (flag == "condition4")
            {
                cmdText += "and a.ID!=:ID and a.workno=:empNo and a.OtDate=to_date(:otDate,'yyyy/MM/dd') )";
                dt = DalHelper.ExecuteQuery(cmdText, new OracleParameter(":empNo", EmployeeNo), new OracleParameter(":ID", ID), new OracleParameter(":otDate", otDate));
            }
            if (flag == "condition5")
            {
                cmdText += " and  a.ID=:ID )";
                dt = DalHelper.ExecuteQuery(cmdText, new OracleParameter(":ID", ID));
            }
            return dt;
        }

        /// <summary>
        /// 刪除數據
        /// </summary>
        /// <param name="dataTable"></param>
        public void LHZBDeleteData(DataTable dataTable,SynclogModel logmodel)
        {

            foreach (DataRow deletedRow in dataTable.Rows)
            {
                ActivityModel model = new ActivityModel();
                model.ID = deletedRow["ID"].ToString().Trim();
                int del = DeleteActivity(model,logmodel);
            }
        }

        /// <summary>
        /// 根據主鍵修改功能
        /// </summary>
        /// <param name="model">要修改的功能Model</param>
        /// <returns>是否成功</returns>
        public bool UpdateActivityByKey(ActivityModel model, bool ignoreNull,SynclogModel logmodel)
        {

            return DalHelper.UpdateByKey(model, ignoreNull,logmodel) != -1;
        }

        /// <summary>
        /// 核准
        /// </summary>
        /// <param name="dataTable"></param>
        public void LHZBAudit(DataTable dataTable,SynclogModel logmodel)
        {

            foreach (DataRow AuditRow in dataTable.Rows)
            {
                ActivityModel model = new ActivityModel();
                model.ID = AuditRow["ID"].ToString().Trim();
                model.Status = "2";
                bool update = UpdateActivityByKey(model, true,logmodel);
            }

        }


        /// <summary>
        /// 獲取ParaValue
        /// </summary>
        /// <returns></returns>
        public string GetValue(string flag, ActivityModel model)
        {
            DataTable dt = new DataTable();
            string value = "";
            if (flag == "KQMReGetKaoQin")
            {
                dt = DalHelper.ExecuteQuery(@"select nvl(MAX(paravalue),'5') from gds_sc_parameter where paraname='KQMReGetKaoQin'");
            }
            if (flag == "DayWorkHours")
            {
                dt = DalHelper.ExecuteQuery(@"Select nvl(Max(ParaValue),8) from gds_sc_parameter where ParaName='DayWorkHours'");
            }
            if (flag == "OTMAdvanceG1Flag")
            {
                dt = DalHelper.ExecuteQuery(@"select nvl(max(ParaValue),'N') from gds_sc_parameter where ParaName ='OTMAdvanceG1Flag'");
            }
            if (flag == "OTMAdvanceApplyG2LMT")
            {
                dt = DalHelper.ExecuteQuery(@"select nvl(max(ParaValue),'2') from gds_sc_parameter where ParaName ='OTMAdvanceApplyG2LMT'");
            }
            if (flag == "condition1")
            {
                dt = DalHelper.ExecuteQuery(@"select overtimetype from gds_att_monthtotal where workno=:WorkNo AND yearmonth=:YearMonth ", new OracleParameter(":WorkNo", model.WorkNo.Trim()), new OracleParameter(":YearMonth", model.YearMonth.Trim()));
            }
            if (flag == "IsG1ForFFlag")
            {
                dt = DalHelper.ExecuteQuery(@"select nvl(MAX(paravalue),'N') from gds_sc_parameter where paraname='IsG1ForFFlag' ");
            }
            if (flag == "condition2")
            {
                dt = DalHelper.ExecuteQuery(@"select IsLactation from gds_att_workshift where shiftno =:ShiftNo ", new OracleParameter(":ShiftNo", model.Remark.Trim()));
            }
            if (flag == "condition3")
            {
                string OtDate = model.Remark;
                dt = DalHelper.ExecuteQuery(@"select nvl(sum(Hours),0) from gds_att_advanceApply where WorkNo=:WorkNo and OTType in('G1','G2','G3','G4') and OTDate<>to_date(:OtDate,'yyyy/MM/dd') and OTDate>last_day(add_months(to_date(:OtDate,'yyyy/MM/dd') ,-1)) and OTDate<=last_day(to_date(:OtDate,'yyyy/MM/dd')) and (status<'2' or OTDate>=trunc(sysdate)) and importremark is null and instr(ImportFlag,'N')>0"
                , new OracleParameter(":WorkNo", model.WorkNo.Trim()), new OracleParameter(":OtDate", OtDate));
            }
            if (flag == "condition4")
            {
                dt = DalHelper.ExecuteQuery(@"select nvl(sum(G1RelSalary),0)+nvl(sum(G2RelSalary),0)+nvl(sum(G3RelSalary),0)+nvl(sum(SpecG1Salary),0)+nvl(sum(SpecG2Salary),0)+nvl(sum(SpecG3Salary),0) from gds_att_MonthTotal where WorkNo=:WorkNo and yearMonth=: YearMonth ", new OracleParameter(":WorkNo", model.WorkNo.Trim()), new OracleParameter(":YearMonth", model.YearMonth.Trim()));
            }
            if (flag == "condition5")
            {
                dt = DalHelper.ExecuteQuery(@"SELECT count(1) FROM gds_att_monthtotal WHERE ApproveFlag='2' and workno=:WorkNo  AND YearMonth=:YearMonth ", new OracleParameter(":WorkNo", model.WorkNo.Trim()), new OracleParameter(":YearMonth", model.YearMonth.Trim()));
            }


            if (flag == "condition15")
            {
                string OtDate = model.Remark;
                dt = DalHelper.ExecuteQuery(@"select count(*)  from gds_att_ACTIVITY where workno=:WorkNo and OtDate=to_date(:OtDate,'yyyy/MM/dd')"
                , new OracleParameter(":WorkNo", model.WorkNo.Trim()), new OracleParameter(":OtDate", OtDate));

            }



            if (dt != null)
            {
                value = dt.Rows[0][0].ToString().Trim();
            }


            return value;
        }

        /// <summary>
        /// 取消核准
        /// </summary>
        /// <param name="dataTable"></param>
        public void LHZBCancelAudit(DataTable dataTable,SynclogModel logmodel)
        {

            foreach (DataRow AuditRow in dataTable.Rows)
            {
                ActivityModel model = new ActivityModel();
                model.ID = AuditRow["ID"].ToString().Trim();
                model.Status = "0";
                bool update = UpdateActivityByKey(model, true,logmodel);
            }


        }

        /// <summary>
        /// 根據SQL查詢信息
        /// </summary>
        /// <param name="strShiftNo"></param>
        /// <returns></returns>
        public DataTable GetDataTableBySQL(string flag, ActivityModel model)
        {
            DataTable dt = new DataTable();
            if (flag == "condition1")
            {
                dt = DalHelper.ExecuteQuery(@"Select OverTimeType,DCode From gds_att_Employee Where Workno=:WorkNo  ", new OracleParameter(":WorkNo", model.WorkNo.Trim()));
            }
            if (flag == "condition2")
            {
                dt = DalHelper.ExecuteQuery(@"
            SELECT * 
FROM (SELECT   a.g1dlimit, a.g2dlimit, a.g3dlimit, a.g1mlimit, a.g2mlimit,
                 a.g12mlimit, a.isallowproject, a.g13mlimit, a.g123mlimit
            FROM gds_att_type a, gds_sc_department b, gds_sc_deplevel c
           WHERE a.orgcode = b.depcode(+)
             AND b.levelcode = c.levelcode(+)
             AND a.ottypecode = :OverTimeType
             AND a.effectflag = 'Y'
             AND b.depcode IN (SELECT     depcode
                                     FROM gds_sc_department e
                               START WITH e.depcode = :DepCode
                               CONNECT BY e.depcode = PRIOR e.parentdepcode)
        ORDER BY c.orderid DESC)
 WHERE ROWNUM <= 1", new OracleParameter(":OverTimeType", model.OverTimeType.Trim()), new OracleParameter(":DepCode", model.DepName.Trim()));

            }

            if (flag == "condition3")
            {
                dt = DalHelper.ExecuteQuery(@"select G1LMT,G2LMT,NoWorkFlag,MonthLMT from GDS_ATT_Config  ");
            }
            return dt;
        }

        /// <summary>
        /// 獲取ShiftNo
        /// </summary>
        /// <param name="sWorkNo"></param>
        /// <param name="sDate"></param>
        /// <returns></returns>
        public string GetShiftNo(string sWorkNo, string sDate)
        {

            string value = "";
            try
            {
                OracleParameter sValue = new OracleParameter("v_shiftno", OracleType.VarChar, 100);
                sValue.Direction = ParameterDirection.Output;
                int a = DalHelper.ExecuteNonQuery("prog_getempshiftno", CommandType.StoredProcedure,
                  new OracleParameter("v_odate", sDate), new OracleParameter("v_workno", sWorkNo), sValue);
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

        /// <summary>
        /// 查詢
        /// </summary>
        /// <param name="OtDate"></param>
        /// <param name="WorkNo"></param>
        /// <returns></returns>
        public string GetByIndex(string OtDate, string WorkNo)
        {

            string value = "";
            try
            {
                OracleParameter sValue = new OracleParameter("o_result", OracleType.VarChar, 100);
                sValue.Direction = ParameterDirection.Output;
                int a = DalHelper.ExecuteNonQuery("proc_isworksixday", CommandType.StoredProcedure,
                  new OracleParameter("v_odate", OtDate), new OracleParameter("v_workno", WorkNo), sValue);
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


        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="processFlag"></param>
        /// <param name="dataTable"></param>
        public void LHZBSaveData(string processFlag, DataTable dataTable, string loginID,SynclogModel logmodel)
        {
            string str = "";

            foreach (DataRow newRow in dataTable.Rows)
            {
                if (processFlag.Equals("Add"))
                {
                    str = string.Concat(new object[] { 
                            "INSERT INTO gds_att_Activity(WorkNo,OTType,OTDate,ConfirmHours,WorkDesc,Remark,Status,YearMonth,UPDATE_USER,UPDATE_DATE,STARTTIME,ENDTIME) VALUES('", newRow["WorkNo"], "','", newRow["OTType"], "',to_date('", Convert.ToDateTime(newRow["OTDate"]).ToString("yyyy/MM/dd"), "','yyyy/mm/dd'),'", newRow["ConfirmHours"], "','", newRow["WorkDesc"], "','','0','", newRow["YearMonth"], "','", newRow["UPDATE_USER"], "',to_date('", Convert.ToDateTime(newRow["UPDATE_Date"]).ToString("yyyy/MM/dd"), 
                            "','yyyy/mm/dd'),'", newRow["StartTime"], "','", newRow["EndTime"], "')"
                         });
                    int m = DalHelper.ExecuteNonQuery(str,logmodel);

                }
                else if (processFlag.Equals("Modify"))
                {

                    str = string.Concat(new object[] { "UPDATE gds_att_Activity SET OTType='", newRow["OTType"], "',OTDate=to_date('", Convert.ToDateTime(newRow["OTDate"]).ToString("yyyy/MM/dd"), "','yyyy/mm/dd'),ConfirmHours='", newRow["ConfirmHours"], "',WorkDesc='", newRow["WorkDesc"], "',UPDATE_USER='", loginID, "',UPDATE_DATE=sysdate, STARTTIME='", newRow["StartTime"], "',EndTime='", newRow["EndTime"], "' WHERE ID='", newRow["ID"], "' " });
                    int n = DalHelper.ExecuteNonQuery(str,logmodel);
                }
            }


        }


        /// <summary>
        /// 查詢免卡人員加班導入資料(不分頁)
        /// </summary>
        /// <param name="model"></param>
        /// <param name="?"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public DataTable GetActivityApplyList(ActivityModel model, string sqlDep, string depCode, string workNoStrings, string dateFrom, string dateTo)
        {
            string strCon = "";
            string depName = model.DepName.ToString();
            model.DepName = "";

            List<OracleParameter> listPara = DalHelper.CreateConditionParameters(model, true, "a", out strCon);
            string cmdText = @"select ID,workno,ottype,otdate,BillNo, confirmhours, workdesc,remark,status,yearmonth, create_date, create_user,
                            update_date,update_user,depname,localname, statusname,starttime,endtime from GDS_ATT_ACTIVITY_V a where 1=1 ";
            cmdText = cmdText + strCon;
            if (!string.IsNullOrEmpty(depName))
            {
                cmdText += "AND a.depcode IN ((" + sqlDep + ") INTERSECT SELECT DepCode FROM gds_sc_department START WITH depname = :depName CONNECT BY PRIOR depcode = parentdepcode) ";
                listPara.Add(new OracleParameter(":depName", depName));
            }
            else
            {
                cmdText += " AND a.depcode IN (" + sqlDep + ") ";
            }


            if (!String.IsNullOrEmpty(workNoStrings))
            {
                cmdText = cmdText + " and a.workno in (SELECT char_list  FROM TABLE (gds_sc_chartotable ('" + workNoStrings + "','§')))";
            }
            if (!string.IsNullOrEmpty(dateFrom) && !string.IsNullOrEmpty(dateTo))
            {
                cmdText += " and (otdate between to_date(:dateFrom,'yyyy/mm/dd') and to_date(:dateTo,'yyyy/mm/dd')) ";
                listPara.Add(new OracleParameter(":dateFrom", dateFrom));
                listPara.Add(new OracleParameter(":dateTo", dateTo));
            }


            DataTable dt = DalHelper.ExecuteQuery(cmdText,listPara.ToArray());
            return dt;

        }

        #region 簽核

        /// <summary>
        /// 免卡人員加班送簽功能實現
        /// </summary>
        /// <param name="processFlag">標志位：Add 新增； Modify：修改</param>
        /// <param name="ID">ID欄位值</param>
        /// <param name="BillNoType">單據類型</param>
        /// <param name="AuditOrgCode">部門ID</param>
        /// <returns></returns>
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
                    sSql = "SELECT MAX (billno) strMax  FROM GDS_ATT_ACTIVITY WHERE billno LIKE '" + BillNoType + "'|| TO_CHAR (SYSDATE, 'yymm')|| '%'";
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
                    sSql = "UPDATE GDS_ATT_ACTIVITY SET Status='1' , BillNo =  '" + strMax + "' Where ID='" + ID + "'";
                    command.CommandText = sSql;
                    command.ExecuteNonQuery();
                }
                else if (processFlag.Equals("Modify"))
                {
                    strMax = BillNoType;
                    sSql = "UPDATE GDS_ATT_ACTIVITY SET Status='1' , BillNo =  '" + strMax + "' Where ID='" + ID + "'";
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

        public bool  SaveAuditData(string WorkNo, string BillNoType, string BillTypeCode, string ApplyMan, string AuditOrgCode, string Flow_LevelRemark, SynclogModel logmodel)
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
                sSql = "SELECT MAX (billno) strMax  FROM GDS_ATT_ACTIVITY WHERE billno LIKE '" + BillNoType + "'|| TO_CHAR (SYSDATE, 'yymm')|| '%'";
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
                sSql = "UPDATE GDS_ATT_ACTIVITY SET Status='1' , BillNo = '" + strMax + "'  Where WorkNo='" + WorkNo + "'";
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

        public bool SaveAuditData(string WorkNo, string BillNoType, string BillTypeCode, string ApplyMan, string AuditOrgCode, string Flow_LevelRemark,OracleConnection OracleString, SynclogModel logmodel)
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
                sSql = "SELECT MAX (billno) strMax  FROM GDS_ATT_ACTIVITY WHERE billno LIKE '" + BillNoType + "'|| TO_CHAR (SYSDATE, 'yymm')|| '%'";
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
                sSql = "UPDATE GDS_ATT_ACTIVITY SET Status='1' , BillNo = '" + strMax + "'  Where WorkNo='" + WorkNo + "'  ";
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




        public DataTable getAuditBillInfoByBillNo(string condition, string status)
        {
            //string sql = " select * from gds_att_activity_v a where 1=1  " + condition;
            string sql = "select  dissignrmark, depname, workno, localname, otdate, starttime, endtime, ottype, confirmhours, statusname, workdesc, ID, remark, status, depcode  from GDS_ATT_ACTIVITY_V a where 1=1 " + condition;
            if (!string.IsNullOrEmpty(status))
            {
                sql += " and status='" + status + "'";
            } 
                

            return DalHelper.ExecuteQuery(sql);
 
        }
        #endregion

        #region 組織送簽

        /// <summary>
        /// 組織送簽
        /// </summary>
        /// <param name="processFlag"></param>
        /// <param name="diry"></param>
        /// <param name="BillNoType"></param>
        /// <param name="BillTypeCode"></param>
        /// <param name="Person"></param>
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
                        sSql = "SELECT nvl(MAX (billno),'0') strMax  FROM GDS_ATT_ACTIVITY WHERE billno LIKE '" + BillNoType + "'|| TO_CHAR (SYSDATE, 'yymm')|| '%'";
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
                            sSql = "UPDATE  GDS_ATT_ACTIVITY SET Status='1' , BillNo =  '" + strMax + "' Where ID='" + ID + "'";
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
                            sSql = "UPDATE GDS_ATT_ACTIVITY SET Status='1' , BillNo =  '" + strMax + "' Where ID='" + ID + "'";
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
#endregion

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
