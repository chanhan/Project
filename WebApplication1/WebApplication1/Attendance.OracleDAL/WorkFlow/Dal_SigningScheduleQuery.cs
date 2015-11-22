using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using GDSBG.MiABU.Attendance.Model.KQM.BasicData;
//using GDSBG.MiABU.Attendance.IDAL.KQM.BasicData;
using GDSBG.MiABU.Attendance.IDAL.WorkFlow;
using System.Data.OracleClient;
using GDSBG.MiABU.Attendance.Model.WorkFlow;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;
using GDSBG.MiABU.Attendance.OracleDAL.KQM.KaoQinData;
using GDSBG.MiABU.Attendance.OracleDAL.KQM.OTM;
using GDSBG.MiABU.Attendance.OracleDAL.Hr.KQM.KaoQinData;

namespace GDSBG.MiABU.Attendance.OracleDAL.WorkFlow
{
    public class Dal_SigningScheduleQuery : DALBase<TypeDataModel>, IDal_SigningScheduleQuery
    {
        public DataTable GetBillType()
        {
            string SQL = "Select BillTypeName,BillTypeNo From GDS_WF_BILLTYPE Where AuditFlag ='Y' order by orderNo";
            //string SQL = " SELECT   datatype, datacode, datavalue, datatypedetail,(datacode || '?B' || datavalue) AS newdatavalue "
            //           + " FROM gds_att_typedata WHERE datatype = 'DocNoType' ORDER BY orderid ";
            return DalHelper.ExecuteQuery(SQL);
        }
        public DataTable GetSignState()
        {
            string SQL
                = "SELECT   datacode, datavalue "
                + "    FROM gds_att_typedata "
                + "   WHERE datatype = 'WFMBillStatus' AND datacode <> '3' "
                + "ORDER BY orderid ";
            return DalHelper.ExecuteQuery(SQL);
        }
        public DataTable GetSigningScheduleStatus(string DataType)
        {
            string SQL = "";
            SQL = " SELECT DataType,DataCode,DataValue,DataTypeDetail,(DataCode||'?B'||DataValue)as newDataValue FROM gds_att_typedata  WHERE DataType='" + DataType + "' and DataCode<>'3' ORDER BY OrderId";
            DataTable dt = DalHelper.ExecuteQuery(SQL);
            return dt;
        }
        public DataTable GetSigningScheduleInfo(Mod_SigningScheduleQuery model)
        {

            string condition = "";
            string ddlStr = "";
            string[] temVal = null;
            #region SelectSql
            condition = "SELECT * "
                         + "  FROM (SELECT a.*, (SELECT BILLTYPENAME "
                         + "                       FROM GDS_WF_BILLTYPE c "
                         + "                      WHERE c.BILLTYPECODE = a.billtypecode) billtypename, "
                         + "               (SELECT depname "
                         + "                  FROM gds_sc_department c "
                         + "                 WHERE c.depcode = a.orgcode) orgname, "
                         + "               (SELECT datavalue "
                         + "                  FROM gds_att_typedata c "
                         + "                 WHERE c.datatype = 'WFMBillStatus' "
                         + "                   AND c.datacode = a.status) statusname, "
                         + "               (SELECT localname "
                         + "                  FROM gds_att_employee c "
                         + "                 WHERE c.workno = a.applyman) applymanname "
                         + "          FROM gds_att_bill a "
                         + "         WHERE 1 = 1 "
                         ;

            #endregion
            if (model.DepName.Length > 0)
            {
                //if (base.bPrivileged)
                //{
                //    string CS$0$0001 = condition;
                //    condition = CS$0$0001 + " AND a.OrgCode IN ((" + base.sqlDep + ") INTERSECT SELECT DepCode FROM bfw_department START WITH depname = '" + this.textBoxDepName.Text.Trim() + "' CONNECT BY PRIOR depcode = parentdepcode) ";
                //}
                //else
                //{
                condition = condition + " AND a.OrgCode IN (SELECT DepCode FROM bfw_department START WITH depname = '" + model.DepName + "' CONNECT BY PRIOR depcode = parentdepcode) ";
                //}
            }
            //else if (base.bPrivileged && (this.Session["roleCode"].ToString().IndexOf("Admin") < 0))
            //{
            //    condition = condition + " AND a.OrgCode in (" + base.sqlDep + ")";
            //}
            ddlStr = "";
            if (model.BillTypeCode != "")
            {
                temVal = model.BillTypeCode.Split(new char[] { ',' });
                for (int iLoop = 0; iLoop < temVal.Length; iLoop++)
                {
                    ddlStr = ddlStr + " a.BillNo like '" + temVal[iLoop] + "%' or";
                }
                ddlStr = ddlStr.Substring(0, ddlStr.Length - 2);
                condition = condition + " and (" + ddlStr + ")";
            }
            if (model.BillNo != "")
            {
                condition = condition + " AND a.BillNo like '" + model.BillNo + "%'";
            }
            if (model.Status != "")
            {
                condition = condition + " AND a.Status = '" + model.Status + "'";
            }
            if (model.ApplyDateFrom != "")
            {
                condition = condition + " AND a.ApplyDate >= to_date('" + DateTime.Parse(model.ApplyDateFrom).ToString("yyyy/MM/dd") + " 00:00','yyyy/mm/dd hh24:mi') ";
            }
            if (model.ApplyDateTo != "")
            {
                condition = condition + " AND a.ApplyDate <= to_date('" + DateTime.Parse(model.ApplyDateTo).ToString("yyyy/MM/dd") + " 23:59','yyyy/mm/dd hh24:mi') ";
            }
            //if (model.AuditMan != "")
            //{
            //    condition = condition + " AND exists (select 1 from WFM_AuditStatus c,V_Employee e Where c.AuditMan=e.WorkNo(+) and c.BillNo=a.BillNo and e.LocalName like '" + model.AuditMan + "%')";
            //}
            if (model.ApplyMan != "")
            {
                condition = condition + " AND exists (select 1 from BFW_Person c Where c.PersonCode=a.ApplyMan and c.CName like '" + model.ApplyMan + "%')";
            }

            DataTable dt = DalHelper.ExecuteQuery(condition);

            return dt;
        }
        public DataTable GetSigningScheduleInfo(Mod_SigningScheduleQuery model, int pageIndex, int pageSize, out int totalCount)
        {

            string condition = "";
            string ddlStr = "";
            string[] temVal = null;
            #region SelectSql

            condition = "SELECT * "
                        + "  FROM (SELECT a.*, (SELECT BILLTYPENAME "
                         + "                       FROM GDS_WF_BILLTYPE c "
                         + "                      WHERE c.BILLTYPECODE = a.billtypecode) billtypename, "
                        + "               (SELECT depname "
                        + "                  FROM gds_sc_department c "
                        + "                 WHERE c.depcode = a.orgcode) orgname, "
                        + "               (SELECT datavalue "
                        + "                  FROM gds_att_typedata c "
                        + "                 WHERE c.datatype = 'WFMBillStatus' "
                        + "                   AND c.datacode = a.status) statusname, "
                        + "               (SELECT localname "
                        + "                  FROM gds_att_employee c "
                        + "                 WHERE c.workno = a.applyman) applymanname "
                        + "          FROM gds_att_bill a "
                        + "         WHERE 1 = 1 "
                        ;


            #endregion
            if (model.DepName.Length > 0)
            {
                //if (base.bPrivileged)
                //{
                //    string CS$0$0001 = condition;
                //    condition = CS$0$0001 + " AND a.OrgCode IN ((" + base.sqlDep + ") INTERSECT SELECT DepCode FROM bfw_department START WITH depname = '" + this.textBoxDepName.Text.Trim() + "' CONNECT BY PRIOR depcode = parentdepcode) ";
                //}
                //else
                //{
                condition = condition + " AND a.OrgCode IN (SELECT DepCode FROM gds_sc_department START WITH depname = '" + model.DepName + "' CONNECT BY PRIOR depcode = parentdepcode) ";
                //}
            }
            //else if (base.bPrivileged && (this.Session["roleCode"].ToString().IndexOf("Admin") < 0))
            //{
            //    condition = condition + " AND a.OrgCode in (" + base.sqlDep + ")";
            //}
            ddlStr = "";
            if (model.BillTypeCode != "")
            {
                temVal = model.BillTypeCode.Split(new char[] { ',' });
                for (int iLoop = 0; iLoop < temVal.Length; iLoop++)
                {
                    ddlStr = ddlStr + " a.BillNo like '" + temVal[iLoop] + "%' or";
                }
                ddlStr = ddlStr.Substring(0, ddlStr.Length - 2);
                condition = condition + " and (" + ddlStr + ")";
            }
            if (model.BillNo != "")
            {
                condition = condition + " AND a.BillNo like '" + model.BillNo + "%'";
            }
            if (model.Status != "")
            {
                condition = condition + " AND a.Status = '" + model.Status + "'";
            }
            if (model.ApplyDateFrom != "")
            {
                condition = condition + " AND a.ApplyDate >= to_date('" + DateTime.Parse(model.ApplyDateFrom).ToString("yyyy/MM/dd") + " 00:00','yyyy/mm/dd hh24:mi') ";
            }
            if (model.ApplyDateTo != "")
            {
                condition = condition + " AND a.ApplyDate <= to_date('" + DateTime.Parse(model.ApplyDateTo).ToString("yyyy/MM/dd") + " 23:59','yyyy/mm/dd hh24:mi') ";
            }
            //if (!string.IsNullOrEmpty(model.AuditMan))
            //{
            //    condition = condition + " AND exists (select 1 from GDS_ATT_AUDITSTATUS c,V_Employee e Where c.AuditMan=e.WorkNo(+) and c.BillNo=a.BillNo and e.LocalName like '" + model.AuditMan + "%')";
            //}
            if (model.ApplyMan != "")
            {
                condition = condition + " AND exists (select 1 from GDS_SC_PERSON c Where c.PersonCode=a.ApplyMan and c.CName like '" + model.ApplyMan + "%')";
            }
            condition = condition + ")";
            DataTable dt = DalHelper.ExecutePagerQuery(condition, pageIndex, pageSize, out totalCount);

            return dt;
        }
        public string GetAllAuditDept(string sDepCode)
        {
            string SQL = "";
            string result = "";
            SQL = " select * from (select (depnamelevel2||case when depnamelevel3 is not null then '/'||depnamelevel3 end  || case when depnamelevel4 is not null then '/'||depnamelevel4 end || case when depnamelevel5 is not null then '/'||depnamelevel5 end || case when depnamelevel6 is not null then '/'||depnamelevel6 end || case when depnamelevel7 is not null then '/'||depnamelevel7 end || case when depnamelevel8 is not null then '/'||depnamelevel8 end || case when depnamelevel9 is not null then '/'||depnamelevel9 end || case when depnamelevel10 is not null then '/'||depnamelevel10 end || case when depnamelevel11 is not null then '/'||depnamelevel11 end) depname from GDS_ATT_ORGTREE where depcode='" + sDepCode + "')";
            result = Convert.ToString(DalHelper.ExecuteScalar(SQL)); ;
            return result;
        }
        public string GetBillTypeCode(string BillTypeNo)
        {
            string SQL = "";
            string result = "";
            SQL = " SELECT datavalue "
                + "    FROM gds_att_typedata "
                + "    WHERE datatype = 'DocNoType' AND DATACODE = '" + BillTypeNo + "' "
                + "    ORDER BY orderid  "
                ;
            result = Convert.ToString(DalHelper.ExecuteScalar(SQL)); ;
            return result;
        }
        public bool SaveBatchDisAuditData(string BillNo, string AuditMan, string BillTypeCode, SynclogModel logmodel)
        {
            bool bResult = false;
            bool ConnectionOpenHere = false;
            OracleCommand command = new OracleCommand();
            try
            {

                command.Connection = DalHelper.Connection;
                if (command.Connection.State == ConnectionState.Closed)
                {
                    command.Connection.Open();
                    ConnectionOpenHere = true;
                }
                OracleTransaction trans = command.Connection.BeginTransaction();
                command.Transaction = trans;

                command.CommandText = "SELECT count(1) FROM GDS_ATT_BILL WHERE BillNo='" + BillNo + "' and Status='0'";
                if (Convert.ToDecimal(command.ExecuteScalar()) == 1M)
                {
                    string ApRemark = AuditMan + " Stop the WorkFlow!";

                    command.CommandText = " SELECT BILLTYPENAME  FROM GDS_WF_BILLTYPE  WHERE BILLTYPECODE = '" + BillTypeCode + "' ORDER BY ORDERNO  ";
                    string BillTypeName = Convert.ToString(command.ExecuteScalar());

                    command.CommandText = "SELECT nvl(MAX (ParaValue),'')  FROM GDS_SC_PARAMETER WHERE ParaName ='WFMDisRemindSubject'";
                    string Content = BillTypeName + Convert.ToString(command.ExecuteScalar());

                    command.CommandText = "update GDS_ATT_AUDITSTATUS set AuditStatus='2',AuditDate=sysdate, Remark='" + ApRemark + "' where BillNo='" + BillNo + "' and OrderNo=(select nvl(min(e.OrderNo),'-1') from GDS_ATT_AUDITSTATUS e where BillNo='" + BillNo + "' and e.Auditstatus='0') and AuditStatus='0'";
                    command.ExecuteNonQuery();
                    SaveLogData("U", BillNo, command.CommandText, command, logmodel);

                    command.CommandText = "update GDS_ATT_BILL set Status='2',Remark='" + ApRemark + "' where BillNo='" + BillNo + "' and Status='0'";
                    command.ExecuteNonQuery();
                    SaveLogData("U", BillNo, command.CommandText, command, logmodel);

                    switch (BillTypeCode)
                    {
                        // case "OTMAdvanceApply" :加班單

                        case "D001":
                            //command.CommandText = "insert into GDS_WF_NOTESREMIND(Content,WorkNo,ApplyMan,ApplyDate,BillNo,SendNotes,Auditstatus,Remark,RemindType,Subject) SELECT '" + BillTypeName + "', WorkNo, '" + AuditMan + "',sysdate,'" + BillNo + "','N','0','" + ApRemark + "','A','" + Content + "'  FROM GDS_ATT_ADVANCEAPPLY WHERE BillNo='" + BillNo + "' and Status='1'";
                            //command.ExecuteNonQuery();
                            //command.CommandText = "insert into GDS_WF_NOTESREMIND(Content,WorkNo,ApplyMan,ApplyDate,BillNo,SendNotes,Auditstatus,Remark,RemindType,Subject) SELECT '" + BillTypeName + "', ApplyMan, '" + AuditMan + "',sysdate,'" + BillNo + "','N','0','" + ApRemark + "','D','" + Content + "'  FROM GDS_ATT_BILL WHERE BillNo='" + BillNo + "'";
                            //command.ExecuteNonQuery();
                            command.CommandText = "update GDS_ATT_ADVANCEAPPLY set Status='3',Approver='" + AuditMan + "',ApproveDate=sysdate,ApRemark='" + ApRemark + "' where BillNo='" + BillNo + "' and Status='1'";
                            command.ExecuteNonQuery();
                            SaveLogData("U", BillNo, command.CommandText, command, logmodel);
                            break;
                        //case "KQMLeaveApply":請假單

                        case "D002":
                            //command.CommandText = "insert into GDS_WF_NOTESREMIND(Content,WorkNo,ApplyMan,ApplyDate,BillNo,SendNotes,Auditstatus,Remark,RemindType,Subject) SELECT '" + BillTypeName + "', WorkNo, '" + AuditMan + "',sysdate,'" + BillNo + "','N','0','" + ApRemark + "','A','" + Content + "'  FROM GDS_ATT_LEAVEAPPLY WHERE BillNo='" + BillNo + "' and Status='1'";
                            //command.ExecuteNonQuery();
                            //command.CommandText = "insert into GDS_WF_NOTESREMIND(Content,WorkNo,ApplyMan,ApplyDate,BillNo,SendNotes,Auditstatus,Remark,RemindType,Subject) SELECT '" + BillTypeName + "', ApplyMan, '" + AuditMan + "',sysdate,'" + BillNo + "','N','0','" + ApRemark + "','D','" + Content + "'  FROM GDS_ATT_BILL WHERE BillNo='" + BillNo + "'";
                            //command.ExecuteNonQuery();
                            command.CommandText = "update GDS_ATT_LEAVEAPPLY set ProxyStatus='0',Status='3',Approver='" + AuditMan + "',ApproveDate=sysdate,ApRemark='" + ApRemark + "' where BillNo='" + BillNo + "' and Status='1'";
                            command.ExecuteNonQuery();
                            SaveLogData("U", BillNo, command.CommandText, command, logmodel);
                            break;
                        case "KQMApplyOut":  //外出申請單

                            //command.CommandText = "insert into GDS_WF_NOTESREMIND(Content,WorkNo,ApplyMan,ApplyDate,BillNo,SendNotes,Auditstatus,Remark,RemindType,Subject) SELECT '" + BillTypeName + "', WorkNo, '" + AuditMan + "',sysdate,'" + BillNo + "','N','0','" + ApRemark + "','A','" + Content + "'  FROM GDS_ATT_KAOQINDATA WHERE BillNo='" + BillNo + "' and Status='4'";
                            //command.ExecuteNonQuery();
                            //command.CommandText = "insert into GDS_WF_NOTESREMIND(Content,WorkNo,ApplyMan,ApplyDate,BillNo,SendNotes,Auditstatus,Remark,RemindType,Subject) SELECT '" + BillTypeName + "', ApplyMan, '" + AuditMan + "',sysdate,'" + BillNo + "','N','0','" + ApRemark + "','D','" + Content + "'  FROM GDS_ATT_BILL WHERE BillNo='" + BillNo + "'";
                            //command.ExecuteNonQuery();
                            command.CommandText = "update GDS_ATT_ApplyOUT set Status='3',AUDITER='" + AuditMan + "',AUDITDATE=sysdate,AUDITIDEA='" + ApRemark + "' where BillNo='" + BillNo + "' and Status='4'";
                            command.ExecuteNonQuery();
                            SaveLogData("U", BillNo, command.CommandText, command, logmodel);
                            break;

                        case "KQMException"://異常處理單

                            //command.CommandText = "insert into GDS_WF_NOTESREMIND(Content,WorkNo,ApplyMan,ApplyDate,BillNo,SendNotes,Auditstatus,Remark,RemindType,Subject) SELECT '" + BillTypeName + "', WorkNo, '" + AuditMan + "',sysdate,'" + BillNo + "','N','0','" + ApRemark + "','A','" + Content + "'  FROM GDS_ATT_KAOQINDATA WHERE BillNo='" + BillNo + "' and Status='4'";
                            //command.ExecuteNonQuery();
                            //command.CommandText = "insert into GDS_WF_NOTESREMIND(Content,WorkNo,ApplyMan,ApplyDate,BillNo,SendNotes,Auditstatus,Remark,RemindType,Subject) SELECT '" + BillTypeName + "', ApplyMan, '" + AuditMan + "',sysdate,'" + BillNo + "','N','0','" + ApRemark + "','D','" + Content + "'  FROM GDS_ATT_BILL WHERE BillNo='" + BillNo + "'";
                            //command.ExecuteNonQuery();
                            command.CommandText = "update GDS_ATT_KAOQINDATA set Status='5',Approver='" + AuditMan + "',ApproveDate=sysdate,ApRemark='" + ApRemark + "' where BillNo='" + BillNo + "' and Status='4'";
                            command.ExecuteNonQuery();
                            SaveLogData("U", BillNo, command.CommandText, command, logmodel);
                            break;

                        case "KQMMakeup"://未刷補卡
                            //command.CommandText = "insert into GDS_WF_NOTESREMIND(Content,WorkNo,ApplyMan,ApplyDate,BillNo,SendNotes,Auditstatus,Remark,RemindType,Subject) SELECT '" + BillTypeName + "', WorkNo, '" + AuditMan + "',sysdate,'" + BillNo + "','N','0','" + ApRemark + "','A','" + Content + "'  FROM GDS_ATT_MAKEUP WHERE BillNo='" + BillNo + "' and Status='1'";
                            //command.ExecuteNonQuery();
                            //command.CommandText = "insert into GDS_WF_NOTESREMIND(Content,WorkNo,ApplyMan,ApplyDate,BillNo,SendNotes,Auditstatus,Remark,RemindType,Subject) SELECT '" + BillTypeName + "', ApplyMan, '" + AuditMan + "',sysdate,'" + BillNo + "','N','0','" + ApRemark + "','D','" + Content + "'  FROM GDS_ATT_BILL WHERE BillNo='" + BillNo + "'";
                            //command.ExecuteNonQuery();
                            command.CommandText = "update GDS_ATT_MAKEUP set Status='3',Approver='" + AuditMan + "',ApproveDate=sysdate,ApRemark='" + ApRemark + "' where BillNo='" + BillNo + "' and Status='1'";
                            command.ExecuteNonQuery();
                            SaveLogData("U", BillNo, command.CommandText, command, logmodel);
                            break;

                        case "KQMMonthTotal"://月加班匯總

                            //command.CommandText = "insert into GDS_WF_NOTESREMIND(Content,WorkNo,ApplyMan,ApplyDate,BillNo,SendNotes,Auditstatus,Remark,RemindType,Subject) SELECT '" + BillTypeName + "', WorkNo, '" + AuditMan + "',sysdate,'" + BillNo + "','N','0','" + ApRemark + "','A','" + Content + "'  FROM GDS_ATT_MAKEUP WHERE BillNo='" + BillNo + "' and Status='1'";
                            //command.ExecuteNonQuery();
                            //command.CommandText = "insert into GDS_WF_NOTESREMIND(Content,WorkNo,ApplyMan,ApplyDate,BillNo,SendNotes,Auditstatus,Remark,RemindType,Subject) SELECT '" + BillTypeName + "', ApplyMan, '" + AuditMan + "',sysdate,'" + BillNo + "','N','0','" + ApRemark + "','D','" + Content + "'  FROM GDS_ATT_BILL WHERE BillNo='" + BillNo + "'";
                            //command.ExecuteNonQuery();
                            command.CommandText = "update gds_att_monthtotal set opproveflag='3',Approver='" + AuditMan + "',ApproveDate=sysdate,ApRemark='" + ApRemark + "' where BillNo='" + BillNo + "' and opproveflag='1'";
                            command.ExecuteNonQuery();
                            SaveLogData("U", BillNo, command.CommandText, command, logmodel);
                            break;

                        case "KQMOTMA"://免卡人員加班
                            //command.CommandText = "insert into GDS_WF_NOTESREMIND(Content,WorkNo,ApplyMan,ApplyDate,BillNo,SendNotes,Auditstatus,Remark,RemindType,Subject) SELECT '" + BillTypeName + "', WorkNo, '" + AuditMan + "',sysdate,'" + BillNo + "','N','0','" + ApRemark + "','A','" + Content + "'  FROM GDS_ATT_MAKEUP WHERE BillNo='" + BillNo + "' and Status='1'";
                            //command.ExecuteNonQuery();
                            //command.CommandText = "insert into GDS_WF_NOTESREMIND(Content,WorkNo,ApplyMan,ApplyDate,BillNo,SendNotes,Auditstatus,Remark,RemindType,Subject) SELECT '" + BillTypeName + "', ApplyMan, '" + AuditMan + "',sysdate,'" + BillNo + "','N','0','" + ApRemark + "','D','" + Content + "'  FROM GDS_ATT_BILL WHERE BillNo='" + BillNo + "'";
                            //command.ExecuteNonQuery();
                            command.CommandText = "update GDS_ATT_ACTIVITY set Status='3',Approver='" + AuditMan + "',ApproveDate=sysdate,ApRemark='" + ApRemark + "' where BillNo='" + BillNo + "' and Status='1'";
                            command.ExecuteNonQuery();
                            SaveLogData("U", BillNo, command.CommandText, command, logmodel);
                            break;

                        case "OTMProjectApply"://專案加班預報
                            //command.CommandText = "insert into GDS_WF_NOTESREMIND(Content,WorkNo,ApplyMan,ApplyDate,BillNo,SendNotes,Auditstatus,Remark,RemindType,Subject) SELECT '" + BillTypeName + "', WorkNo, '" + AuditMan + "',sysdate,'" + BillNo + "','N','0','" + ApRemark + "','A','" + Content + "'  FROM GDS_ATT_MAKEUP WHERE BillNo='" + BillNo + "' and Status='1'";
                            //command.ExecuteNonQuery();
                            //command.CommandText = "insert into GDS_WF_NOTESREMIND(Content,WorkNo,ApplyMan,ApplyDate,BillNo,SendNotes,Auditstatus,Remark,RemindType,Subject) SELECT '" + BillTypeName + "', ApplyMan, '" + AuditMan + "',sysdate,'" + BillNo + "','N','0','" + ApRemark + "','D','" + Content + "'  FROM GDS_ATT_BILL WHERE BillNo='" + BillNo + "'";
                            //command.ExecuteNonQuery();
                            command.CommandText = "update GDS_ATT_ADVANCEAPPLY set Status='3',Approver='" + AuditMan + "',ApproveDate=sysdate,ApRemark='" + ApRemark + "' where BillNo='" + BillNo + "' and Status='1'";
                            command.ExecuteNonQuery();
                            SaveLogData("U", BillNo, command.CommandText, command, logmodel);
                            break;
                    }
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
        public bool SaveReSendAuditData(string BillNo, string BillTypeCode, string ApplyMan,string AuditOrgCode ,string Flow_LevelRemark,SynclogModel logmodel)
        {
            OracleCommand command = new OracleCommand();
            bool bValue = true;
            bool ConnectionOpenHere = false;
            string UpdateTable = "";
            string Status = "";
            string UpdateStatusField = "";
            string BillNoCondition = "";
            string ENDSTATUS = "";
            try
            {
                DataTable dt = DalHelper.ExecuteQuery("select b.UpdateTable,b.StatusCondition,b.ENDSTATUS,b.UpdateStatusField,b.BillNoCondition from GDS_WF_BILLTYPE b where b.BillTypeCode ='" + BillTypeCode + "'");
                if (dt != null && dt.Rows.Count > 0)
                {
                    UpdateTable = dt.Rows[0]["UpdateTable"].ToString();
                    Status = dt.Rows[0]["StatusCondition"].ToString();
                    UpdateStatusField = dt.Rows[0]["UpdateStatusField"].ToString();
                    BillNoCondition = dt.Rows[0]["BillNoCondition"].ToString();
                    ENDSTATUS = dt.Rows[0]["ENDSTATUS"].ToString();
                }
                command.Connection = DalHelper.Connection;
                if (command.Connection.State == ConnectionState.Closed)
                {
                    command.Connection.Open();
                    ConnectionOpenHere = true;
                }

                OracleTransaction trans = command.Connection.BeginTransaction();
                command.Transaction = trans;
                command.CommandText = "select Count(1) from " + UpdateTable + " where " + BillNoCondition + "='" + BillNo + "' and " + UpdateStatusField + "='" + ENDSTATUS + "'";
                if (Convert.ToDecimal(command.ExecuteScalar()) > 0M)
                {
                    command.CommandText = "update " + UpdateTable + " set " + UpdateStatusField + "='" + Status + "' where " + BillNoCondition + "='" + BillNo + "' and " + UpdateStatusField + "='" + ENDSTATUS + "'";
                    command.ExecuteNonQuery();
                    SaveLogData("U", BillNo, command.CommandText, command, logmodel);
                    command.CommandText = "UPDATE GDS_ATT_AUDITSTATUS SET SendNotes='N' WHERE BillNo='" + BillNo + "' and auditstatus='0'";
                    command.ExecuteNonQuery();
                    command.CommandText = "UPDATE GDS_ATT_AUDITSTATUS SET AuditDate='', auditstatus='0',SendNotes='N',Remark='' WHERE BillNo='" + BillNo + "' and auditstatus='2'";
                    command.ExecuteNonQuery();
                    SaveLogData("U", BillNo, command.CommandText, command, logmodel);
                    command.CommandText = "UPDATE GDS_ATT_BILL SET Status='0' WHERE BillNo='" + BillNo + "' and Status='2'";
                    command.ExecuteNonQuery();
                    SaveLogData("U", BillNo, command.CommandText, command, logmodel);
                }
                else
                {
                    bValue = false;
                }
                command.Transaction.Commit();
                command.Transaction = null;
            }
            catch (Exception ex)
            {
                if (command.Transaction != null)
                {
                    command.Transaction.Rollback();
                    command.Transaction = null;
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
            return bValue;

            #region 20120322
            //bool bValue = false;
            //try
            //{
            //    OracleConnection OracleString = new OracleConnection();
            //    switch (BillTypeCode)
            //    {
            //        case "KQMException"://異常處理單

            //            Dal_AbnormalAttendanceHandle dal_Abnormal=new Dal_AbnormalAttendanceHandle();             

            //            DataTable dt_Abnormal = DalHelper.ExecuteQuery(" SELECT WORKNO,KQDATE from GDS_ATT_KAOQINDATA where BILLNO ='" + BillNo + "'");
            //            OracleString = DalHelper.Connection;

            //            if (dt_Abnormal != null && dt_Abnormal.Rows.Count > 0)
            //            {
            //                foreach (DataRow dr in dt_Abnormal.Rows)//重新送簽批量被拒的單據時需要循環,並且按單張單據送簽處理,非組織送簽.
            //                {
            //                    bValue = dal_Abnormal.KQMSaveAuditData(dr["WORKNO"].ToString(), dr["KQDATE"].ToString(), "KQE", "KQMException", ApplyMan, AuditOrgCode, Flow_LevelRemark, OracleString, logmodel);
            //                }
            //            }
            //            break;
            //        case "KQMApplyOut"://外出申請單
            //            KQMEvectionApplyDal ApplyOutDal = new KQMEvectionApplyDal();
            //            DataTable dt_applyout = DalHelper.ExecuteQuery(" SELECT ID,WorkNo from GDS_ATT_ApplyOUT where BILLNO ='" + BillNo + "'");
            //            OracleString = DalHelper.Connection;

            //            if (dt_applyout != null && dt_applyout.Rows.Count > 0)
            //            {
            //                foreach (DataRow dr in dt_applyout.Rows)//重新送簽批量被拒的單據時需要循環,並且按單張單據送簽處理,非組織送簽.
            //                {
            //                    bValue = ApplyOutDal.SaveAuditData(dr["ID"].ToString(), dr["WorkNo"].ToString(), "KQT", "KQMApplyOut", ApplyMan,AuditOrgCode, Flow_LevelRemark, OracleString, logmodel);
            //                }
            //            }
            //            break;
            //        case "D001": //加班預報單                    
            //            OverTimeDal dal_overtime = new OverTimeDal();                        
            //            DataTable tb_overtime = DalHelper.ExecuteQuery(" SELECT ID,WorkNo,OTTYPE from GDS_ATT_ADVANCEAPPLY where BILLNO ='" + BillNo + "'");
            //            OracleString = DalHelper.Connection;
            //            if (tb_overtime != null && tb_overtime.Rows.Count > 0)
            //            {
            //                foreach (DataRow dr in tb_overtime.Rows)//重新送簽批量被拒的單據時需要循環,並且按單張單據送簽處理,非組織送簽.
            //                {
            //                    bValue = dal_overtime.SaveAuditData(dr["ID"].ToString(), "OTD", AuditOrgCode, BillTypeCode, dr["OTTYPE"].ToString(), ApplyMan, dr["WorkNo"].ToString(), OracleString, logmodel);                                
            //                }
            //            }
            //            break;
            //        case "D002"://請假單   
            //            KQMLeaveApplyForm_ZBLHDal leaveApply = new KQMLeaveApplyForm_ZBLHDal();
            //            DataTable dtLeaveApply = DalHelper.ExecuteQuery("select * from gds_att_leaveapply_v where billno='"+BillNo+"'");
            //            OracleString = DalHelper.Connection;
            //            if (dtLeaveApply!=null&&dtLeaveApply.Rows.Count>0)
            //            {
            //                foreach (DataRow dr in dtLeaveApply.Rows)//重新送簽批量被拒的單據時需要循環,並且按單張單據送簽處理,非組織送簽.
            //                {
            //                    string startDate = Convert.ToDateTime(dr["startdate"].ToString()).ToString("yyyy/MM/dd") + " " + Convert.ToDateTime(dr["starttime"].ToString()).ToString("HH:mm");
            //                    string endDate = Convert.ToDateTime(dr["enddate"].ToString()).ToString("yyyy/MM/dd") + " " + Convert.ToDateTime(dr["endtime"].ToString()).ToString("HH:mm");
            //                    bValue = leaveApply.SaveAuditData(OracleString,Flow_LevelRemark, dr["ID"].ToString(), "KQL", AuditOrgCode, BillTypeCode, ApplyMan, dr["reason"].ToString(), dr["workno"].ToString(), startDate, endDate, dr["lvtypecode"].ToString(), logmodel);
            //                }
            //            }
            //            break;
            //        case "OTMProjectApply": //專案加班
            //            WFProjectApplyDal dal_project = new WFProjectApplyDal();                        
            //            DataTable tb_project = DalHelper.ExecuteQuery(" SELECT ID,WorkNo,OTTYPE from GDS_ATT_ADVANCEAPPLY where BILLNO ='" + BillNo + "'");
            //            OracleString = DalHelper.Connection;
            //            if (tb_project != null && tb_project.Rows.Count > 0)
            //            {
            //                foreach (DataRow dr in tb_project.Rows)//重新送簽批量被拒的單據時需要循環,並且按單張單據送簽處理,非組織送簽.
            //                {
            //                    bValue = dal_project.SaveAuditData(dr["ID"].ToString(), "OTM", AuditOrgCode, BillTypeCode, ApplyMan, dr["OTTYPE"].ToString(), dr["WorkNo"].ToString(), OracleString, logmodel);                                
            //                }
            //            }
            //            break;
            //        case "KQMOTMA": //免卡人員加班                 
            //            OTMActivityApplyDal dal_KQMOTMA = new OTMActivityApplyDal();
            //            DataTable tb_KQMOTMA = DalHelper.ExecuteQuery(" SELECT ID,WorkNo,OTTYPE from GDS_ATT_ACTIVITY where BILLNO ='" + BillNo + "'");
            //            OracleString = DalHelper.Connection;
            //            if (tb_KQMOTMA != null && tb_KQMOTMA.Rows.Count > 0)
            //            {
            //                foreach (DataRow dr in tb_KQMOTMA.Rows)//重新送簽批量被拒的單據時需要循環,並且按單張單據送簽處理,非組織送簽.
            //                {
            //                    bValue = dal_KQMOTMA.SaveAuditData(dr["WORKNO"].ToString(), "TMA", "KQMOTMA", ApplyMan, AuditOrgCode, Flow_LevelRemark, OracleString, logmodel);
                                    
            //                }
            //            }
            //            break;
            //        case "KQMMakeup": //考勤異常                    
            //            WorkFlowCardMakeupDal Makeupdal = new WorkFlowCardMakeupDal();
            //            DataTable tb_Makeupdal = DalHelper.ExecuteQuery(" SELECT ID,WorkNo,MAKEUPTYPE OTTYPE from GDS_ATT_makeup where BILLNO ='" + BillNo + "'");
            //            OracleString = DalHelper.Connection;
            //            if (tb_Makeupdal != null && tb_Makeupdal.Rows.Count > 0)
            //            {
            //                foreach (DataRow dr in tb_Makeupdal.Rows)//重新送簽批量被拒的單據時需要循環,並且按單張單據送簽處理,非組織送簽.
            //                {
            //                    bValue = Makeupdal.SaveAuditData_new(dr["WORKNO"].ToString(), "KQU", "KQMMakeup", ApplyMan, AuditOrgCode, Flow_LevelRemark, OracleString, logmodel);
            //                }
            //            }
            //            break;
            //        default:
            //            bValue = false;
            //            break;
            //    }
               
            //}
            //catch (Exception ex)
            //{
            //    return false;
            //}
            //return bValue;
            #endregion
        }
       
        public bool SaveSendNotesData(string BillNo, SynclogModel logmodel)
        {
            bool ConnectionOpenHere = false;
            bool bResult = false;
            OracleCommand command = new OracleCommand();
            try
            {
                command.Connection = DalHelper.Connection;
                if (command.Connection.State == ConnectionState.Closed)
                {
                    command.Connection.Open();
                    ConnectionOpenHere = true;
                }
                OracleTransaction trans = command.Connection.BeginTransaction();
                command.Transaction = trans;

                command.CommandText = "SELECT count(1) FROM GDS_ATT_AUDITSTATUS WHERE BillNo='" + BillNo + "' and auditstatus='0' and sendnotes='Y'";
                if (Convert.ToDecimal(command.ExecuteScalar()) > 0M)
                {
                    command.CommandText = "UPDATE GDS_ATT_AUDITSTATUS SET SendNotes='N' WHERE BillNo='" + BillNo + "' and auditstatus='0' and sendnotes='Y'";
                    command.ExecuteNonQuery();
                    SaveLogData("U", BillNo, command.CommandText, command, logmodel);
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
