using System;
using System.Collections.Generic;
using System.Text;
using GDSBG.MiABU.Attendance.IDAL.WorkFlow;
using GDSBG.MiABU.Attendance.Model.WorkFlow;
using System.Data.OracleClient;
using System.Data;
using System.Collections;

namespace GDSBG.MiABU.Attendance.OracleDAL.WorkFlow
{
    class WorkFlowBillChangeAuditFlowDal : DALBase<WorkFlowBillChangeAuditFlowModel>, IWorkFlowBillChangeAuditFlowDal
    {

        #region 保存變更簽核數據
        /// <summary>
        /// 保存變更簽核數據
        /// </summary>
        /// <param name="BillNo"></param>
        /// <param name="dataTable"></param>
        public void SaveChangeAuditData(string BillNo, DataTable dataTable)
        {
            //刪除未簽核的簽核人信息
            string CommandText = "Delete FROM GDS_ATT_AUDITSTATUS where BillNo='" + BillNo + "' and auditstatus='0' ";
            DalHelper.ExecuteNonQuery(CommandText);
            // base.SaveLogData("D", "", CommandText);
            //循環插入待簽核人
            foreach (DataRow newRow in dataTable.Rows)
            {
                string CommandText1 = string.Concat(new object[] { "SELECT count(1) FROM GDS_ATT_AuditStatus WHERE BillNo='", BillNo, "' and OrderNo='", newRow["OrderNo"], "'" });
                if (Convert.ToDecimal(DalHelper.ExecuteScalar(CommandText1)) == 0M)
                {
                    string CommandText2 = string.Concat(new object[] { "insert into GDS_ATT_AuditStatus(BillNo,AuditMan,OrderNo,AuditStatus,SendNotes,AuditType,AuditManType) values('", BillNo, "','", newRow["AuditMan"], "','", newRow["OrderNo"], "','0','", newRow["SendNotes"], "','", newRow["AuditType"], "','", newRow["AuditManType"], "')" });
                    DalHelper.ExecuteNonQuery(CommandText2);
                    //base.SaveLogData("I", "", CommandText2);
                }
            }

        }
        #endregion
 

        #region 獲取數據
        /// <summary>
        /// 獲取所有的數據
        /// </summary>
        /// <param name="condition">查詢條件</param>
        /// <returns>數據集</returns>
        public DataTable GetAllDataByCondition(string condition)
        {

            string CommandText = "SELECT * FROM (select a.WorkNo AuditMan,b.LocalName,b.Notes,b.ManagerName, (select depname from GDS_SC_DEPARTMENT c where c.DepCode=a.DepCode) depname ";
            CommandText += " from GDS_ATT_ORGMANAGER a,GDS_ATT_EMPLOYEE b,GDS_SC_DEPARTMENT c,gds_sc_deplevel d ";
            CommandText += " where a.WorkNo=b.WorkNo and a.DepCode=c.DepCode(+) ";
            CommandText += " and c.LevelCode=d.LevelCode(+) and b.Status='0' " + condition + " order by d.Orderid desc)"; 

            return DalHelper.ExecuteQuery(CommandText);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public DataTable GetAuditStatusDataByCondition(string condition)
        {
            string CommandText = "";
            //  CommandText = "SELECT * FROM (select a.*,nvl(b.LocalName,(select LocalName From WFM_EmployeesOut e Where e.WorkNo=a.AuditMan)) LocalName,";
           // CommandText += " b.ManagerName,nvl(b.Notes,(select Notes From WFM_EmployeesOut e Where e.WorkNo=a.AuditMan)) Notes, ";
          //  CommandText += " (select DataValue From GDS_ATT_TYPEDATA c Where c.DataType='WFMBillStatus' and c.DataCode=a.AuditStatus)StatusName ";
           // CommandText += " from WFM_AuditStatus a,GDS_ATT_EMPLOYEE b,WFM_Bill c where a.AuditMan=b.WorkNo(+) and a.BillNo=c.BillNo " + condition + " order by a.OrderNo)";
           CommandText = " SELECT * FROM (select a.*,nvl(b.LocalName,(select LocalName From gds_att_employees e Where e.WorkNo=a.AuditMan)) LocalName,";
        CommandText += "  b.ManagerName,nvl(b.Notes,(select Notes From gds_att_employees e Where e.WorkNo=a.AuditMan)) Notes, ";
         CommandText += " (select DataValue From GDS_ATT_TYPEDATA c Where c.DataType='WFMBillStatus' and c.DataCode=a.AuditStatus)StatusName ";
        CommandText += " from gds_att_AuditStatus a,GDS_ATT_EMPLOYEE b,gds_att_Bill c where a.AuditMan=b.WorkNo(+) and a.BillNo=c.BillNo  " + condition + "  order by a.OrderNo)";
            return DalHelper.ExecuteQuery(CommandText);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public DataTable GetDataByCondition(string condition)
        {

            string CommandText = "SELECT * FROM (select a.*,b.LocalName,b.ManagerName,b.Notes,b.dname DepName,";
            CommandText += " (select DataValue From GDS_ATT_TYPEDATA c where c.DataType='AuditType' and c.DataCode=a.AuditType) AuditTypeName, ";
            CommandText += " (select DataValue From GDS_ATT_TYPEDATA c where c.DataType='AuditTypeLeave' and c.DataCode=a.AuditManType) AuditManTypeName";
            CommandText += " from WFM_BillAuditFlow a,gds_att_employee b where a.AuditMan=b.WorkNo(+) " + condition + " order by a.OrderNo)";
            return DalHelper.ExecuteQuery(CommandText);
        }

        #endregion

        #region 獲取部門信息
        /// <summary>
        /// 
        /// </summary>
        /// <param name="personCode"></param>
        /// <param name="companyId"></param>
        /// <param name="moduleCode"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public DataTable GetAuthorizedDept(string personCode, string companyId, string moduleCode, string condition)
        {

            string CommandText = " SELECT a.DepCode,a.DepName,a.ParentDepCode,a.levelcode,a.deleted   FROM  GDS_SC_DEPARTMENT a  WHERE a.deleted='N' and  a.depcode in   (SELECT depcode FROM temp_depcode b WHERE b.personcode='" + personCode + "' AND b.companyid='" + companyId + "' AND b.modulecode='" + moduleCode + "') " + condition + " START WITH a.parentdepcode IS NULL CONNECT BY PRIOR depcode = parentdepcode ORDER SIBLINGS BY orderid ";
            return DalHelper.ExecuteQuery(CommandText);

        }
        #endregion
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Deptcode"></param>
        /// <returns></returns>
        public DataTable GetDataByOrgCode(string Deptcode)
        {

            string CommandText = "SELECT * FROM (select  (select DepName From GDS_SC_DEPARTMENT c where c.depcode=a.Deptcode)ORGName,";
            CommandText += " (Select BillTypeName From bfw_BillType c where c.BillTypeCode=a.BillTypeCode)BillTypeName, a.OrDerNo,a.AuditMan, b.LocalName,b.ManagerName,b.Notes, ";
            CommandText += " (select DataValue From GDS_ATT_TYPEDATA c where c.DataType='AuditType' and c.DataCode=a.AuditType) AuditTypeName,";
            CommandText += " (select DataValue From gds_att_TYPEDATA c where c.DataType='AuditTypeLeave' and c.DataCode=a.AuditManType) AuditManTypeName, a.Modifier,a.ModifyDate ";
            CommandText += "  from gds_wf_flowset a,GDS_ATT_EMPLOYEE b  where a.AuditMan=b.WorkNo(+)  and a.Deptcode='" + Deptcode + "'  order by a.BillTypeCode,a.OrderNo) ";
            return DalHelper.ExecuteQuery(CommandText);

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public DataTable GetDataBySqlDep(string condition)
        {

            string CommandText = "SELECT * FROM (select  (select (SELECT depname FROM gds_sc_department WHERE levelcode = '1'  START WITH depcode = c.parentdepcode CONNECT BY depcode = PRIOR parentdepcode)||'-'||DepName From GDS_SC_DEPARTMENT c where c.depcode=a.Deptcode)ORGName, (Select BillTypeName From gds_sc_BillType c where c.BillTypeCode=a.BillTypeCode)BillTypeName, a.OrDerNo,a.AuditMan, b.LocalName,b.ManagerName,b.Notes,  (select DataValue From GDS_ATT_TYPEDATA c where c.DataType='AuditType' and c.DataCode=a.AuditType) AuditTypeName, (select DataValue From GDS_ATT_TYPEDATA c where c.DataType='AuditTypeLeave' and c.DataCode=a.AuditManType) AuditManTypeName, a.Modifier,a.ModifyDate from WFM_BillAuditFlow a,GDS_ATT_EMPLOYEE b  where a.AuditMan=b.WorkNo(+) and a.BillTypeCode in(Select BillTypeCode From BFW_BillType Where WorkFlowFlag ='Y') " + condition + " order by a.Deptcode,a.BillTypeCode,a.OrderNo) ";
            return DalHelper.ExecuteQuery(CommandText);

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public DataTable GetDataBySqlDep2(string condition)
        {

            string CommandText = " SELECT T1.*,T2.AuditMan11,T2.LocalName11,T2.AuditTypeName11,T2.AuditManType11,T2.AuditMan12,T2.LocalName12,T2.AuditTypeName12,T2.AuditManType12,\r\n ";
            CommandText = " T2.AuditMan13,T2.LocalName13,T2.AuditTypeName13,T2.AuditManType13,T2.AuditMan14,T2.LocalName14,T2.AuditTypeName14,T2.AuditManType14,   \r\n    ";
            CommandText = " T2.AuditMan15,T2.LocalName15,T2.AuditTypeName15,T2.AuditManType15, \r\n   ";
            CommandText = " T2.AuditMan16,T2.LocalName16,T2.AuditTypeName16,T2.AuditManType16,\r\n   ";
            CommandText = " T2.AuditMan17,T2.LocalName17,T2.AuditTypeName17,T2.AuditManType17,\r\n   ";
            CommandText = " T2.AuditMan18,T2.LocalName18,T2.AuditTypeName18,T2.AuditManType18,\r\n   ";
            CommandText = "T2.AuditMan19,T2.LocalName19,T2.AuditTypeName19,T2.AuditManType19,\r\n   ";
            CommandText = " T2.AuditMan20,T2.LocalName20,T2.AuditTypeName20,T2.AuditManType20 FROM\r\n          ";
            CommandText = " (SELECT * FROM (select a.Deptcode,(select (depnamelevel2||case when depnamelevel3 is not null then '/'||depnamelevel3 end \r\n ";
            CommandText = " || case when depnamelevel4 is not null then '/'||depnamelevel4 end  \r\n  ";
            CommandText = " || case when depnamelevel5 is not null then '/'||depnamelevel5 end  \r\n   ";
            CommandText = " || case when depnamelevel6 is not null then '/'||depnamelevel6 end  \r\n   ";
            CommandText = " || case when depnamelevel7 is not null then '/'||depnamelevel7 end  \r\n   ";
            CommandText = " || case when depnamelevel8 is not null then '/'||depnamelevel8 end  \r\n  ";
            CommandText = " || case when depnamelevel9 is not null then '/'||depnamelevel9 end  \r\n   ";
            CommandText = " || case when depnamelevel10 is not null then '/'||depnamelevel10 end  \r\n   ";
            CommandText = " || case when depnamelevel11 is not null then '/'||depnamelevel11 end) from hrm_orgtree s where s.depcode=a.Deptcode)ORGName, \r\n ";
            CommandText = " (Select BillTypeName From bfw_BillType c where c.BillTypeCode=a.BillTypeCode)BillTypeName, \r\n   ";
            CommandText = " max(decode(OrderNo,1,a.auditman,'')) AuditMan1,max(decode(OrderNo,1,LocalName,'')) LocalName1, \r\n  ";
            CommandText = " max(decode(OrderNo,1,AuditTypeName,'')) AuditTypeName1,max(decode(OrderNo,1,AuditManType,'')) AuditManType1, \r\n ";
            CommandText = " max(decode(OrderNo,2,a.auditman,'')) AuditMan2,max(decode(OrderNo,2,LocalName,'')) LocalName2, \r\n   ";
            CommandText = " max(decode(OrderNo,2,AuditTypeName,'')) AuditTypeName2,max(decode(OrderNo,2,AuditManType,'')) AuditManType2,\r\n ";
            CommandText = " max(decode(OrderNo,3,a.auditman,'')) AuditMan3,max(decode(OrderNo,3,LocalName,'')) LocalName3, \r\n  ";
            CommandText = " max(decode(OrderNo,3,AuditTypeName,'')) AuditTypeName3,max(decode(OrderNo,3,AuditManType,'')) AuditManType3,\r\n  ";
            CommandText = " max(decode(OrderNo,4,a.auditman,'')) AuditMan4,max(decode(OrderNo,4,LocalName,'')) LocalName4, \r\n   ";
            CommandText = " max(decode(OrderNo,4,AuditTypeName,'')) AuditTypeName4,max(decode(OrderNo,4,AuditManType,'')) AuditManType4,\r\n  ";
            CommandText = " max(decode(OrderNo,5,a.auditman,'')) AuditMan5,max(decode(OrderNo,5,LocalName,'')) LocalName5,\r\n  ";
            CommandText = " max(decode(OrderNo,5,AuditTypeName,'')) AuditTypeName5,max(decode(OrderNo,5,AuditManType,'')) AuditManType5,\r\n  ";
            CommandText = " max(decode(OrderNo,6,a.auditman,'')) AuditMan6,max(decode(OrderNo,6,LocalName,'')) LocalName6, \r\n   ";
            CommandText = " max(decode(OrderNo,6,AuditTypeName,'')) AuditTypeName6,max(decode(OrderNo,6,AuditManType,'')) AuditManType6, \r\n  ";
            CommandText = " max(decode(OrderNo,7,a.auditman,'')) AuditMan7,max(decode(OrderNo,7,LocalName,'')) LocalName7,\r\n    ";
            CommandText = " max(decode(OrderNo,7,AuditTypeName,'')) AuditTypeName7,max(decode(OrderNo,7,AuditManType,'')) AuditManType7,\r\n  ";
            CommandText = " max(decode(OrderNo,8,a.auditman,'')) AuditMan8,max(decode(OrderNo,8,LocalName,'')) LocalName8,\r\n   ";
            CommandText = " max(decode(OrderNo,8,AuditTypeName,'')) AuditTypeName8,max(decode(OrderNo,8,AuditManType,'')) AuditManType8,\r\n   ";
            CommandText = " max(decode(OrderNo,9,a.auditman,'')) AuditMan9,max(decode(OrderNo,9,LocalName,'')) LocalName9,\r\n  ";
            CommandText = " max(decode(OrderNo,9,AuditTypeName,'')) AuditTypeName9,max(decode(OrderNo,9,AuditManType,'')) AuditManType9,\r\n  ";
            CommandText += " max(decode(OrderNo,10,a.auditman,'')) AuditMan10,max(decode(OrderNo,10,LocalName,'')) LocalName10,\r\n ";
            CommandText += " max(decode(OrderNo,10,AuditTypeName,'')) ";
           CommandText += "  AuditTypeName10,max(decode(OrderNo,10,AuditManType,'')) AuditManType10\r\n  ";
           CommandText += "  from (SELECT f.auditman,f.OrderNo,f.orgcode,f.billtypecode,(select localname from GDS_ATT_EMPLOYEE b where f.auditman=b.workno) LocalName,\r\n  ";
           CommandText += " (select DataValue From GDS_ATT_TYPEDATA c where c.DataType='AuditType' and c.DataCode=f.AuditType) AuditTypeName,\r\n   ";
           CommandText += " (select DataValue From GDS_ATT_TYPEDATA c where c.DataType='AuditTypeLeave' and c.DataCode=f.AuditManType) AuditManType\r\n  ";
           CommandText += " FROM wfm_billauditflow f) a  where a.BillTypeCode in(Select BillTypeCode From BFW_BillType Where WorkFlowFlag ='Y') " + condition + " GROUP BY a.orgcode,a.BillTypeCode )) T1  ,(SELECT * FROM (select a.orgcode,(select (depnamelevel2||case when depnamelevel3 is not null then '/'||depnamelevel3 end   \r\n   ";
           CommandText = "|| case when depnamelevel4 is not null then '/'||depnamelevel4 end  \r\n  ";
           CommandText = " || case when depnamelevel5 is not null then '/'||depnamelevel5 end  \r\n   ";
           CommandText = " || case when depnamelevel6 is not null then '/'||depnamelevel6 end  \r\n ";
           CommandText = " || case when depnamelevel7 is not null then '/'||depnamelevel7 end  \r\n ";
           CommandText = " || case when depnamelevel8 is not null then '/'||depnamelevel8 end  \r\n  ";
           CommandText = " || case when depnamelevel9 is not null then '/'||depnamelevel9 end  \r\n  ";
           CommandText = " || case when depnamelevel10 is not null then '/'||depnamelevel10 end  \r\n  ";
           CommandText = " || case when depnamelevel11 is not null then '/'||depnamelevel11 end) from hrm_orgtree s where s.depcode=a.orgcode)ORGName,   \r\n   ";
           CommandText = " (Select BillTypeName From bfw_BillType c where c.BillTypeCode=a.BillTypeCode)BillTypeName,\r\n  ";
           CommandText = " max(decode(OrderNo,11,a.auditman,'')) AuditMan11,max(decode(OrderNo,11,LocalName,'')) LocalName11,\r\n  ";
           CommandText = " max(decode(OrderNo,11,AuditTypeName,'')) AuditTypeName11,max(decode(OrderNo,11,AuditManType,'')) AuditManType11,\r\n  ";
           CommandText = " max(decode(OrderNo,12,a.auditman,'')) AuditMan12,max(decode(OrderNo,12,LocalName,'')) LocalName12,\r\n  ";
           CommandText = " max(decode(OrderNo,12,AuditTypeName,'')) AuditTypeName12,max(decode(OrderNo,12,AuditManType,'')) AuditManType12,\r\n   ";
           CommandText = " max(decode(OrderNo,13,a.auditman,'')) AuditMan13,max(decode(OrderNo,13,LocalName,'')) LocalName13,\r\n  ";
           CommandText = " max(decode(OrderNo,13,AuditTypeName,'')) AuditTypeName13,\r\n   ";
           CommandText = " max(decode(OrderNo,13,AuditManType,'')) AuditManType13,max(decode(OrderNo,14,a.auditman,'')) AuditMan14, \r\n  ";
           CommandText = " max(decode(OrderNo,14,LocalName,'')) LocalName14,max(decode(OrderNo,14,AuditTypeName,'')) AuditTypeName14,\r\n ";
           CommandText = " max(decode(OrderNo,14,AuditManType,'')) AuditManType14,   max(decode(OrderNo,15,a.auditman,'')) AuditMan15,max(decode(OrderNo,15,LocalName,'')) LocalName15, \r\n   ";
           CommandText = " max(decode(OrderNo,15,AuditTypeName,'')) AuditTypeName15,max(decode(OrderNo,15,AuditManType,'')) AuditManType15,\r\n   ";
           CommandText = " max(decode(OrderNo,16,a.auditman,'')) AuditMan16,max(decode(OrderNo,16,LocalName,'')) LocalName16, \r\n   ";
           CommandText = " max(decode(OrderNo,16,AuditTypeName,'')) AuditTypeName16,max(decode(OrderNo,16,AuditManType,'')) AuditManType16,\r\n   ";
           CommandText = " max(decode(OrderNo,17,a.auditman,'')) AuditMan17,max(decode(OrderNo,17,LocalName,'')) LocalName17, \r\n    ";
           CommandText = " max(decode(OrderNo,17,AuditTypeName,'')) AuditTypeName17,max(decode(OrderNo,17,AuditManType,'')) AuditManType17,\r\n  ";
           CommandText = " max(decode(OrderNo,18,a.auditman,'')) AuditMan18,max(decode(OrderNo,18,LocalName,'')) LocalName18, \r\n    ";
           CommandText = " max(decode(OrderNo,18,AuditTypeName,'')) AuditTypeName18,max(decode(OrderNo,18,AuditManType,'')) AuditManType18,\r\n   ";
           CommandText = " max(decode(OrderNo,19,a.auditman,'')) AuditMan19,max(decode(OrderNo,19,LocalName,'')) LocalName19, \r\n ";
           CommandText = " max(decode(OrderNo,19,AuditTypeName,'')) AuditTypeName19,max(decode(OrderNo,19,AuditManType,'')) AuditManType19,\r\n ";
           CommandText = " max(decode(OrderNo,20,a.auditman,'')) AuditMan20,max(decode(OrderNo,20,LocalName,'')) LocalName20, \r\n ";
           CommandText = " max(decode(OrderNo,20,AuditTypeName,'')) AuditTypeName20,max(decode(OrderNo,20,AuditManType,'')) AuditManType20\r\n               ";
           CommandText = " from (SELECT f.auditman,f.OrderNo,f.orgcode,f.billtypecode,(select localname from GDS_ATT_EMPLOYEE b where f.auditman=b.workno) LocalName,\r\n   ";
           CommandText = " (select DataValue From GDS_ATT_TYPEDATA c where c.DataType='AuditType' and c.DataCode=f.AuditType) AuditTypeName,\r\n ";
           CommandText = " (select DataValue From GDS_ATT_TYPEDATA c where c.DataType='AuditTypeLeave' and c.DataCode=f.AuditManType) AuditManType\r\n    ";
           CommandText += " FROM wfm_billauditflow f) a  where a.BillTypeCode in(Select BillTypeCode From BFW_BillType Where WorkFlowFlag ='Y') " + condition + " GROUP BY a.orgcode,a.BillTypeCode  )) T2  where T1.ORGCODE=T2.orgcode and T1.BillTypeName=T2.BillTypeName AND T1.ORGName=T2.ORGName order by T1.orgcode,T1.BillTypeName";
            return DalHelper.ExecuteQuery(CommandText);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public DataTable GetManagerDataByCondition(string condition)
        {
            string CommandText = "SELECT * FROM (select a.WorkNo AuditMan,b.LocalName,b.Notes,b.ManagerName, (select depname from GDS_SC_DEPARTMENT c where c.DepCode=a.DepCode) depname from GDS_ATT_ORGMANAGER a,GDS_ATT_EMPLOYEE b,GDS_SC_DEPARTMENT c,gds_sc_deplevel d where a.WorkNo=b.WorkNo and a.DepCode=c.DepCode(+) and c.LevelCode=d.LevelCode(+)  and b.Status='0'  " + condition + " order by d.Orderid)";
            return DalHelper.ExecuteQuery(CommandText);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="BillNo"></param>
        /// <param name="BillTypeCode"></param>
        /// <param name="AuditMan"></param>
        /// <returns></returns>
        public string GetWorkFlowAuditManType(string BillNo, string BillTypeCode, string AuditMan)
        {
            string ReturnAuditManType = "";

            if (BillNo != "")
            {
                string CommandText = "SELECT AuditManType FROM wfm_auditstatus a,WFM_BILL b WHERE auditman='" + AuditMan + "' AND b.BILLNO='" + BillNo + "' AND AUDITSTATUS='0'AND a.billno=b.billno and b.BILLTYPECODE='" + BillTypeCode + "' ORDER BY ORDERNO ";
                DataTable dt = DalHelper.ExecuteQuery(CommandText);
                if (dt.Rows.Count > 0)
                {
                    ReturnAuditManType = dt.Rows[0][0].ToString().Trim();
                }
            }
            return ReturnAuditManType;


        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="OrgCode"></param>
        /// <param name="BillTypeCode"></param>
        /// <returns></returns>
        public string GetWorkFlowOrgCode(string OrgCode, string BillTypeCode)
        {
            string ReturnOrgCode = "";

            if (OrgCode != "")
            {
                string CommandText = "select DepCode from( select b.DepCode,OrderID from   wfm_billauditflow a, (SELECT  Level OrderID,DepCode FROM GDS_SC_DEPARTMENT START WITH DepCode='" + OrgCode + "' CONNECT BY PRIOR ParentDepCode = DepCode ORDER BY Level) b where a.OrgCode=b.DepCode and a.BillTypeCode='" + BillTypeCode + "' order by orderid)   where rownum<=1 ";

                DataTable dt = DalHelper.ExecuteQuery(CommandText);
                if (dt.Rows.Count > 0)
                {
                    ReturnOrgCode = dt.Rows[0][0].ToString().Trim();
                }
            }
            return ReturnOrgCode;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataTable"></param>
        public void SaveData(DataTable dataTable)
        {
            bool ConnectionOpenHere = false;
            foreach (DataRow newRow in dataTable.Rows)
            {
                string CommandText = (string.Concat(new object[] { "insert into WFM_BillAuditFlow(BillTypeCode,OrgCode,AuditMan,OrderNo,AuditType,AuditManType,Modifier,ModifyDate) values('", newRow["BillTypeCode"], "','", newRow["OrgCode"], "','", newRow["AuditMan"], "','", newRow["OrderNo"], "','", newRow["AuditType"], "','", newRow["AuditManType"], "','", newRow["user"], "',sysdate)" }));
                DalHelper.ExecuteNonQuery(CommandText);
                // base.SaveLogData("I", "", base.command.get_CommandText());
            }


        }

        #region 郵件定制組織范圍設置
        /// <summary>
        /// 郵件定制組織范圍設置
        /// </summary>
        /// <param name="personcode">工號</param>
        /// <param name="sModuleCode">模組代碼</param>
        /// <param name="sCompanyID">公司ID</param>
        /// <param name="DepCodes">部門代碼</param>
        public void SaveSRMData(string personcode, string sModuleCode, string sCompanyID, Hashtable DepCodes)
        {

            string CommandText = ("DELETE FROM GDS_SC_DEPCODE  WHERE personcode='" + personcode + "' and ModuleCode='" + sModuleCode + "' and CompanyID='" + sCompanyID + "'");
            DalHelper.ExecuteNonQuery(CommandText);
            for (int i = 1; i <= DepCodes.Count; i++)
            {
                string CommandText2 = (string.Concat(new object[] { "INSERT INTO GDS_SC_DEPCODE(personcode,ModuleCode,CompanyID,DepCode) VALUES ('", personcode, "','", sModuleCode, "','", sCompanyID, "','", DepCodes[i], "')" }));
                DalHelper.ExecuteNonQuery(CommandText2);
            }
        }
        #endregion

    }
}
