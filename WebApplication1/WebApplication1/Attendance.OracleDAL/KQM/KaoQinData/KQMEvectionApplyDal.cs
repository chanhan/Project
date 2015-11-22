/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： KQMEvectionApplyDal.cs
 * 檔功能描述： 外出申請數據操作類
 * 
 * 版本：1.0
 * 創建標識： 高子焱 2012.02.16
 * 
 */

using GDSBG.MiABU.Attendance.Model.KQM.KaoQinData;
using GDSBG.MiABU.Attendance.IDAL.KQM.KaoQinData;
using System.Data.OracleClient;
using System.Data;
using System.Collections.Generic;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;
using System;

namespace GDSBG.MiABU.Attendance.OracleDAL.KQM.KaoQinData
{
    public class KQMEvectionApplyDal : DALBase<KQMEvectionApplyModel>, IKQMEvectionApplyDal
    {
        /// <summary>
        /// 根據主鍵查詢Model
        /// <param name="model">班別定義model</param>
        /// <param name="deptCode">部門代碼</param>
        /// <param name="orderType">排序</param>
        /// <param name="pageIndex">當前頁</param>
        /// <param name="pageSize"></param></param>
        /// <param name="totalCount">總頁數</param>
        /// <returns></returns>
        public DataTable GetEvectionList(KQMEvectionApplyModel model,string depCode,string sqlDep,int pageIndex, int pageSize, out int totalCount)
        {
            string strCon = "";
            List<OracleParameter> listPara = DalHelper.CreateConditionParameters(model, true, "a", out strCon);
            string cmdText = @"SELECT id,workno, localname,evectiontype,evectionbyout, evectionreason, evectiontime,evectionobject, evectiontel, 
                             evectionaddress, evectiontask, evectionroad,returntime, evectionby, motorman, remark, status,
                             billno,approver,approvedate, auditer, auditdate, auditidea, create_user, create_date,apremark,
                             dcode, depname, buname, statusname, evectiontypename,dissignrmark FROM gds_att_applyout_v 
                             a where 1=1 ";
            if (!string.IsNullOrEmpty(depCode))
            {
                cmdText = cmdText + " AND a.dCode IN ((" + sqlDep + ") INTERSECT SELECT DepCode FROM gds_sc_department START WITH depcode = '" + depCode + "' CONNECT BY PRIOR depcode = parentdepcode) ";
            }
            else
            {
                cmdText = cmdText + " AND a.dcode in (" + sqlDep + ")";
            }
            cmdText = cmdText + strCon;
            DataTable dt = DalHelper.ExecutePagerQuery(cmdText, pageIndex, pageSize, out totalCount, listPara.ToArray());
            return dt;

        }
        /// <summary>
        /// 根據主鍵查詢Model
        /// <param name="model">班別定義model</param>
        /// <param name="deptCode">部門代碼</param>
        /// <param name="orderType">排序</param>
        /// <returns></returns>
        public DataTable GetEvectionList(KQMEvectionApplyModel model,string depCode,string sqlDep)
        {
            string strCon = "";
            List<OracleParameter> listPara = DalHelper.CreateConditionParameters(model, true, "a", out strCon);
            string cmdText = @"SELECT id, workno, localname,evectiontype,evectionbyout, evectionreason, evectiontime,evectionobject, evectiontel, 
                             evectionaddress, evectiontask, evectionroad,returntime, evectionby, motorman, remark, status,
                             billno,approver,approvedate, auditer, auditdate, auditidea, create_user, create_date,apremark,
                             dcode, depname, buname, statusname, evectiontypename,dissignrmark  FROM gds_att_applyout_v 
                             a where 1=1 ";
            if (!string.IsNullOrEmpty(depCode))
            {
                cmdText = cmdText + " AND a.dCode IN ((" + sqlDep + ") INTERSECT SELECT DepCode FROM gds_sc_department START WITH depcode = '" + depCode + "' CONNECT BY PRIOR depcode = parentdepcode) ";
            }
            else
            {
                cmdText = cmdText + " AND a.dcode in (" + sqlDep + ")";
            }
            cmdText = cmdText + strCon;
            DataTable dt = DalHelper.ExecuteQuery(cmdText, listPara.ToArray());
            return dt;
        }
        /// <summary>
        /// 根據主鍵查詢Model
        /// <param name="model">班別定義model</param>
        /// <param name="deptCode">部門代碼</param>
        /// <param name="orderType">排序</param>
        /// <returns></returns>
        public DataTable GetEvectionList(KQMEvectionApplyModel model)
        {
            string strCon = "";
            List<OracleParameter> listPara = DalHelper.CreateConditionParameters(model, true, "a", out strCon);
            string cmdText = @"SELECT id, workno, localname,evectiontype,evectionbyout, evectionreason, evectiontime,evectionobject, evectiontel, 
                             evectionaddress, evectiontask, evectionroad,returntime, evectionby, motorman, remark, status,
                             billno,approver,approvedate, auditer, auditdate, auditidea, create_user, create_date,apremark,
                             dcode, depname, buname, statusname, evectiontypename,dissignrmark  FROM gds_att_applyout_v 
                             a where 1=1 ";
            cmdText = cmdText + strCon;
            DataTable dt = DalHelper.ExecuteQuery(cmdText, listPara.ToArray());
            return dt;
        }
        /// <summary>
        /// 插入一筆外出申請記錄
        /// </summary>
        /// <param name="functionId"></param>
        /// <returns>插入是否成功</returns>
        public int InsertEvectionByKey(KQMEvectionApplyModel model, SynclogModel logmodel)
        {
            DataTable dt = new DataTable();
            model.BillNo = null;
            return DalHelper.Insert(model, logmodel);
        }
        /// <summary>
        /// 更新一筆外出申請記錄(修改功能）
        /// </summary>
        /// <param name="functionId"></param>
        /// <returns>更新是否成功</returns>
        public int UpdateEvctionByKey(KQMEvectionApplyModel model, SynclogModel logmodel)
        {
            return DalHelper.UpdateByKey(model, false, logmodel);
        }
        /// <summary>
        /// 更新一筆外出申請記錄（核准功能）
        /// </summary>
        /// <param name="functionId"></param>
        /// <returns>更新是否成功</returns>
        public int UpdateEvction(string ID, string status, KQMEvectionApplyModel model, SynclogModel logmodel)
        {
            return DalHelper.ExecuteNonQuery(@"UPDATE gds_att_applyout SET status ='" + status + "',Approver='" + model.Approver + "',approvedate=to_date('" + Convert.ToDateTime(model.ApproveDate).ToString("yyyy/MM/dd") + "','yyyy/mm/dd')   WHERE 1 = 1 AND ID ='" + ID + "'");
        }
        /// <summary>
        /// 更新一筆外出申請記錄（取消核准功能）
        /// </summary>
        /// <param name="functionId"></param>
        /// <returns>更新是否成功</returns>
        public int UpdateEvction(string ID, string status, SynclogModel logmodel)
        {
            return DalHelper.ExecuteNonQuery(@"UPDATE gds_att_applyout SET status ='" + status + "',Approver='',approvedate='' WHERE 1 = 1 AND ID ='" + ID + "'");
        }
        /// <summary>
        /// 根據主鍵刪除選中的數據
        /// </summary>
        /// <param name="model"></param>
        /// <param name="logmodel"></param>
        /// <returns></returns>
        public int DeleteEevctionByKey(KQMEvectionApplyModel model, SynclogModel logmodel)
        {
            return DalHelper.Delete(model, logmodel);
        }
        /// <summary>
        /// 將datatable轉換為List
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public List<KQMEvectionApplyModel> GetList(DataTable dt)
        {
            return OrmHelper.SetDataTableToList(dt);
        }
        /// <summary>
        /// 導入
        /// </summary>
        /// <param name="personcode"></param>
        /// <param name="successnum"></param>
        /// <param name="errornum"></param>
        /// <returns></returns>
        public DataTable ImpoertExcel(string personcode, out int successnum, out int errornum, SynclogModel logmodel)
        {
            OracleParameter outCursor = new OracleParameter("p_coursor", OracleType.Cursor);
            OracleParameter outSuccess = new OracleParameter("p_success", OracleType.Int32);
            OracleParameter outError = new OracleParameter("p_error", OracleType.Int32);
            outCursor.Direction = ParameterDirection.Output;
            outSuccess.Direction = ParameterDirection.Output;
            outError.Direction = ParameterDirection.Output;
            DataTable dt = DalHelper.ExecuteQuery("gds_att_applyout_vaildata", CommandType.StoredProcedure,
                new OracleParameter("p_personcode", personcode), outCursor, outSuccess, outError, new OracleParameter("p_transactiontype", logmodel.TransactionType), new OracleParameter("p_levelno", logmodel.LevelNo), new OracleParameter("p_fromhost", logmodel.FromHost),
                new OracleParameter("p_tohost", logmodel.ToHost == null ? "" : logmodel.ToHost.ToString()), new OracleParameter("p_docno", logmodel.DocNo == null ? "" : logmodel.DocNo.ToString()), new OracleParameter("p_processflag", logmodel.ProcessFlag),
                new OracleParameter("p_processowner", logmodel.ProcessOwner));
            successnum = Convert.ToInt32(outSuccess.Value);
            errornum = Convert.ToInt32(outError.Value);
            return dt;
        }
        /// <summary>
        /// 根據單據類型和組織ID查詢可以簽核的最近的部門ID
        /// </summary>
        /// <param name="OrgCode">組織</param>
        /// <param name="billTypeCode"></param>
        /// <returns></returns>
        public string GetWorkFlowOrgCode(string OrgCode, string billTypeCode)
        {
            string ReturnOrgCode = "";
            DataTable dt = DalHelper.ExecuteQuery(@"SELECT depcode FROM (SELECT   b.depcode, a.orderid FROM gds_wf_flowset a, (SELECT 
                                                 LEVEL orderid, depcode FROM gds_sc_department  START WITH depcode =:OrgCode CONNECT BY 
                                                 PRIOR parentdepcode = depcode   ORDER BY LEVEL) b  WHERE a.deptcode = b.depcode  AND
                                                 a.formtype =:billTypeCode  ORDER BY orderid) WHERE ROWNUM <= 1",
                                                 new OracleParameter("OrgCode", OrgCode), new OracleParameter("billTypeCode", billTypeCode));
            if (dt.Rows.Count > 0)
            {
                ReturnOrgCode = dt.Rows[0][0].ToString();
            }
            return ReturnOrgCode;
        }
        /// <summary>
        /// 根據數據的ID查詢單號
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataTable GetDtByID(string id)
        {
            return DalHelper.ExecuteQuery(@"select workNo,BILLNO from GDS_ATT_APPLYOUT where Status in('0','3') AND id='" + id + "'");
        }
        /// <summary>
        /// 送簽的流程   發起表單
        /// </summary>
        /// <param name="processFlag">標識位（是新發起的表單還是簽核中）</param>
        /// <param name="ID">數據的ID</param>
        /// <param name="billNoType">單據類型</param>
        /// <param name="auditOrgCode">要簽核單位</param>
        /// <param name="billTypeCode">單據類型代碼</param>
        /// <param name="workNo">工號</param>
        /// <returns></returns>
        public string SaveAuditData(string ID, string WorkNo, string BillNoType, string BillTypeCode, string ApplyMan, string AuditOrgCode, string Flow_LevelRemark, SynclogModel logmodel)
        {
            string strMax = "";
            string sSql = "";
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
                sSql = "SELECT MAX (billno) strMax  FROM GDS_ATT_APPLYOUT WHERE billno LIKE '" + BillNoType + "'|| TO_CHAR (SYSDATE, 'yymm')|| '%'";
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
                sSql = "UPDATE GDS_ATT_APPLYOUT SET Status='1' , BillNo = '" + strMax + "'  Where ID='" + ID + "'";
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
                // bResult = true;
            }
            catch (Exception ex)
            {
                if (command.Transaction != null)
                {
                    command.Transaction.Rollback();
                    command.Transaction = null;
                }
                return strMax;
                //throw ex;
            }
            finally
            {
                if (ConnectionOpenHere)
                {
                    command.Connection.Close();
                }
            }
            return strMax;
        }
        public bool SaveAuditData(string ID, string WorkNo, string BillNoType, string BillTypeCode, string ApplyMan, string AuditOrgCode, string Flow_LevelRemark, OracleConnection OracleString, SynclogModel logmodel)
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
                sSql = "SELECT MAX (billno) strMax  FROM GDS_ATT_APPLYOUT WHERE billno LIKE '" + BillNoType + "'|| TO_CHAR (SYSDATE, 'yymm')|| '%'";
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
                sSql = "UPDATE GDS_ATT_APPLYOUT SET Status='1' , BillNo = '" + strMax + "'  Where ID='" + ID + "'";
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
        #region  組織送簽
        /// <summary>
        /// 組織送簽
        /// </summary>
        /// <param name="processFlag">標識位</param>
        /// <param name="diry"></param>
        /// <param name="BillNoType">單據類型</param>
        /// <param name="BillTypeCode">單據類型代碼</param>
        /// <param name="Person">簽核人</param>
        /// <returns></returns>
        public int SaveOrgAuditData(string processFlag, Dictionary<string, List<string>> diry, string BillNoType, string BillTypeCode, string sFlow_LevelRemark, string Person, SynclogModel logmodel)
        {
            string strMax = "";
            string num = "0";
            string num1 = "0";
            int k = 0;
            foreach (string key in diry.Keys)
            {
                string AuditOrgCode = key;
                if (processFlag.Equals("Add"))
                {
                    try
                    {
                        BillNoType = BillNoType + AuditOrgCode;
                        string sql = "SELECT nvl(MAX (billno),'0') strMax  FROM GDS_ATT_APPLYOUT WHERE billno LIKE '" + BillNoType + "'|| TO_CHAR (SYSDATE, 'yymm')|| '%'";
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
                            strMax = BillNoType + DateTime.Now.ToString("yyMM") + "0001";
                        }
                        else
                        {
                            int i = Convert.ToInt32(strMax.Substring(BillNoType.Length + 4)) + 1;
                            strMax = i.ToString().PadLeft(4, '0');
                            strMax = BillNoType + DateTime.Now.ToString("yyMM") + strMax;
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
                            command.CommandText = "UPDATE GDS_ATT_APPLYOUT SET Status='1' , BillNo =  '" + strMax + "' Where ID='" + ID + "'";
                            command.ExecuteNonQuery();
                        }
                        SaveLogData("U", strMax, command.CommandText, command, logmodel);
                        if (num == "0")
                        {
                            command.CommandText = "insert into GDS_ATT_BILL(BillNo,OrgCode,ApplyMan,ApplyDate,Status,BillTypeCode) values('" + strMax + "','" + AuditOrgCode + "','" + Person + "',sysdate,'0','" + BillTypeCode + "')";
                            command.ExecuteNonQuery();
                            SaveLogData("I", strMax, command.CommandText, command, logmodel);
                        }
                        else
                        {
                            command.CommandText = "update GDS_ATT_BILL set OrgCode='" + AuditOrgCode + "',ApplyMan='" + Person + "',ApplyDate=sysdate,Status='0',BillTypeCode='" + BillTypeCode + "'  where BillNo='" + strMax + "'";
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
                            command.CommandText = "select WORKNO from  GDS_ATT_APPLYOUT where ID='" + diry[key][0].ToString() + "'";
                            string senduser = Convert.ToString(command.ExecuteScalar());
                            command.CommandText = @"insert into GDS_ATT_AUDITSTATUS(BillNo,AuditMan,OrderNo,AuditStatus,SendNotes,OLDAUDITMAN)  " +
                                        "select '" + strMax + "',  nvl(getagentempno(FLOW_EMPNO,'" + AuditOrgCode + "'),FLOW_EMPNO) as FLOW_EMPNO, ORDERID,'0','N',decode(NVL (getagentempno (flow_empno, '" + AuditOrgCode + "'), flow_empno),flow_empno,'',flow_empno) as oldauditman  from (  " +
                                       " select * from GDS_WF_FLOWSET WHERE FORMTYPE='" + BillTypeCode + "' and DEPTCODE='" + AuditOrgCode + "') " +
                                       " where  FLOW_EMPNO!='" + senduser + "'or (FLOW_EMPNO='" + senduser + "' and FLOW_LEVEL not in ("+sFlow_LevelRemark+")) ";
                        }
                        else
                        {
                            command.CommandText = "insert into GDS_ATT_AUDITSTATUS(BillNo,AuditMan,OrderNo,AuditStatus,SendNotes) SELECT '" + strMax + "', FLOW_EMPNO, ORDERID,'0','N'   FROM GDS_WF_FLOWSET WHERE FORMTYPE='" + BillTypeCode + "' and DEPTCODE='" + AuditOrgCode + "'";
                        }
                        command.ExecuteNonQuery();
                        SaveLogData("I", strMax, command.CommandText, command, logmodel);
                    }
                    else if (processFlag.Equals("Modify"))
                    {
                        foreach (string ID in diry[key])
                        {
                            strMax = BillNoType;
                            command.CommandText = "UPDATE GDS_ATT_APPLYOUT SET Status='1' , BillNo =  '" + strMax + "' Where ID='" + ID + "'";
                            command.ExecuteNonQuery();
                        }
                        SaveLogData("U", strMax, command.CommandText, command, logmodel);
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
        #endregion
    }
}
