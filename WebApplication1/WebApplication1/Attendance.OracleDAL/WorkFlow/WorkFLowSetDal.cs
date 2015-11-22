using System;
using System.Collections.Generic;
using System.Text;
using GDSBG.MiABU.Attendance.Model.KQM.BasicData;
using GDSBG.MiABU.Attendance.IDAL.KQM.BasicData;
using System.Data;
using GDSBG.MiABU.Attendance.IDAL.WorkFlow;
using System.Data.OracleClient;
using GDSBG.MiABU.Attendance.Model.WorkFlow;
using GDSBG.MiABU.Attendance.Common;

namespace GDSBG.MiABU.Attendance.OracleDAL.WorkFlow
{
    public class WorkFLowSetDal : DALBase<TypeDataModel>, IWorkFlowSetDal
    {

        public DataTable GetDocNoTypeList(string type)
        {
            return DalHelper.ExecuteQuery(@"SELECT   datatype, datacode, datavalue, datatypedetail,
                                                 (datacode || '?B' || datavalue) AS newdatavalue
                                            FROM gds_att_typedata
                                           WHERE datatype = '" + type + @"'
                                        ORDER BY orderid");
        }

        public DataTable GetDocNoTypeList()
        {
            string sql = @"SELECT   a.*,
                                 (SELECT datavalue
                                    FROM gds_att_typedata b
                                   WHERE b.datatype = 'LeaveSex' AND b.datacode = a.fitsex)
                                                                                           fitsexname
                            FROM gds_att_leavetype a
                           WHERE effectflag = 'Y'
                        ORDER BY lvtypecode";
            return  DalHelper.ExecuteQuery(sql);
        }


        public DataTable GetDocNoTypeList_new(string type)
        {
            return DalHelper.ExecuteQuery(@"SELECT billtypecode AS datacode,
                                           billtypename AS datavalue,
                                           (billtypecode || '?B' || billtypename) AS newdatavalue
                                      FROM gds_wf_billtype
                                     WHERE WorkFlowFlag ='Y' order by ORDERNO");
        }

        public DataTable GetOverTimeType(string type)
        {
            return DalHelper.ExecuteQuery(@"select * from  GDS_WF_KEYVALUE where V_TYPE='OVERTIMETYPE'");

        }

        public DataTable GetSignPath(string deptcode, string formtype, List<string> resonlist)
        {
            string sql
            = "SELECT flow_empno, flow_empname, flow_notes, flow_manager, flow_type, " +
                "       flow_level " +
                "  FROM gds_wf_flowset " +
                " WHERE formtype = :formtype AND deptcode = :deptcode ";
            if (resonlist != null && resonlist.Count > 0)
            { 
                for(int i=0;i<resonlist.Count;i++)
                {

                    sql += " and reason" + (i + 1).ToString() + "= '" + resonlist[i] + "' ";
                }
            }
            sql += " order by ORDERID ";
            return DalHelper.ExecuteQuery(sql, new OracleParameter(":formtype", formtype), new OracleParameter(":deptcode", deptcode));
        }

        /// <summary>
        /// 流程設定保存
        /// </summary>
        /// <param name="deptid"></param>
        /// <param name="Formtype"></param>
        /// <param name="resonlist"></param>
        /// <param name="driy"></param>
        /// <returns></returns>
        public bool SaveData(string deptid, string Formtype, List<string> resonlist, Dictionary<int, List<string>> driy)
        {
            bool result = false;
            if (driy.Count > 0)
            {
               
                OracleCommand command = new OracleCommand();
                command.Connection = DalHelper.Connection;
                command.Connection.Open();
                OracleTransaction trans = command.Connection.BeginTransaction();
                command.Transaction = trans;
                try
                {
                    string sql = " delete from gds_wf_flowset where formtype = '" + Formtype + "' AND deptcode = '" + deptid + "' ";
                    if (resonlist != null && resonlist.Count > 0)
                    {
                        for (int i = 0; i < resonlist.Count; i++)
                        {                           
                            sql += " and reason" + (i + 1).ToString() + "= '" + resonlist[i] + "' ";                            
                        }
                    }
                    command.CommandText = sql;
                    command.ExecuteNonQuery();
                    foreach (int num in driy.Keys)
                    {

                        string sql1
                      = "INSERT INTO gds_wf_flowset " +
                          "            (formtype, reason1, reason2, reason3, reason4, flow_empno, " +
                          "             flow_empname, flow_notes, flow_manager, flow_type, flow_level, " +
                          "             orderid, deptcode " +
                          "            ) " +
                          "     VALUES ('" + Formtype + "', '" + (resonlist.Count >= 1 ? resonlist[0] : string.Empty) + "', '" + (resonlist.Count >= 2 ? resonlist[1] : string.Empty) + "', '" + (resonlist.Count >= 3 ? resonlist[2] : string.Empty) + "', '" + (resonlist.Count >= 4 ? resonlist[3] : string.Empty) + "' " +
                          "     ,'" + driy[num][0] + "', '" + driy[num][1] + "', '" + driy[num][2] + "', '" + driy[num][3] + "', '" + driy[num][5] + "', '" + driy[num][4] + "','" + num + "', '" + deptid + "' " +
                          "            ) ";
                        command.CommandText = sql1;
                        command.ExecuteNonQuery();

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
            }
            return result;
        }

        /// <summary>
        /// 判斷部門代碼是否存在
        /// </summary>
        /// <param name="deptcode"></param>
        /// <returns></returns>
        public int IsExistsDeptCode(string deptcode)
        {
            string sql
            = "SELECT * " +
                "  FROM gds_sc_department " +
                " WHERE depcode = :deptcode ";
            try
            {
                DataTable dt = DalHelper.ExecuteQuery(sql, new OracleParameter(":deptcode", deptcode));
                int i = 0;
                if (dt != null && dt.Rows.Count > 0)
                {
                    i = dt.Rows.Count;
                }
                return i;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }


        /// <summary>
        /// 獲取部門ID 下的所有簽核路徑
        /// </summary>
        /// <param name="deptid"></param>
        /// <returns></returns>
        public List<WorkFlowSetModel> GetExpAllSignPath(string deptid)
        {
            string sql
                = "SELECT   deptcode AS orgcode, " +
                    "         formtype AS billtypename, " +
                    "         reason1, " +
                    "         reason2, " +
                    "         reason3, " +
                    "         reason4, " +
                    "         MAX (CASE orderid " +
                    "                 WHEN 0 " +
                    "                    THEN flow_empno " +
                    "              END) AS \"auditman1\", " +
                    "         MAX (CASE orderid " +
                    "                 WHEN 0 " +
                    "                    THEN flow_empname " +
                    "              END) AS \"localname1\", " +
                    "         MAX (CASE orderid " +
                    "                 WHEN 0 " +
                    "                    THEN flow_type " +
                    "              END) AS \"audittypename1\", " +
                    "         MAX (CASE orderid " +
                    "                 WHEN 0 " +
                    "                    THEN flow_level " +
                    "              END) AS \"auditmantype1\", " +
                    "         MAX (CASE orderid " +
                    "                 WHEN 1 " +
                    "                    THEN flow_empno " +
                    "              END) AS \"auditman2\", " +
                    "         MAX (CASE orderid " +
                    "                 WHEN 1 " +
                    "                    THEN flow_empname " +
                    "              END) AS \"localname2\", " +
                    "         MAX (CASE orderid " +
                    "                 WHEN 1 " +
                    "                    THEN flow_type " +
                    "              END) AS \"audittypename2\", " +
                    "         MAX (CASE orderid " +
                    "                 WHEN 1 " +
                    "                    THEN flow_level " +
                    "              END) AS \"auditmantype2\", " +
                    "         MAX (CASE orderid " +
                    "                 WHEN 2 " +
                    "                    THEN flow_empno " +
                    "              END) AS \"auditman3\", " +
                    "         MAX (CASE orderid " +
                    "                 WHEN 2 " +
                    "                    THEN flow_empname " +
                    "              END) AS \"localname3\", " +
                    "         MAX (CASE orderid " +
                    "                 WHEN 2 " +
                    "                    THEN flow_type " +
                    "              END) AS \"audittypename3\", " +
                    "         MAX (CASE orderid " +
                    "                 WHEN 2 " +
                    "                    THEN flow_level " +
                    "              END) AS \"auditmantype3\", " +
                    "         MAX (CASE orderid " +
                    "                 WHEN 3 " +
                    "                    THEN flow_empno " +
                    "              END) AS \"auditman4\", " +
                    "         MAX (CASE orderid " +
                    "                 WHEN 3 " +
                    "                    THEN flow_empname " +
                    "              END) AS \"localname4\", " +
                    "         MAX (CASE orderid " +
                    "                 WHEN 3 " +
                    "                    THEN flow_type " +
                    "              END) AS \"audittypename4\", " +
                    "         MAX (CASE orderid " +
                    "                 WHEN 3 " +
                    "                    THEN flow_level " +
                    "              END) AS \"auditmantype4\", " +
                    "         MAX (CASE orderid " +
                    "                 WHEN 4 " +
                    "                    THEN flow_empno " +
                    "              END) AS \"auditman5\", " +
                    "         MAX (CASE orderid " +
                    "                 WHEN 4 " +
                    "                    THEN flow_empname " +
                    "              END) AS \"localname5\", " +
                    "         MAX (CASE orderid " +
                    "                 WHEN 4 " +
                    "                    THEN flow_type " +
                    "              END) AS \"audittypename5\", " +
                    "         MAX (CASE orderid " +
                    "                 WHEN 4 " +
                    "                    THEN flow_level " +
                    "              END) AS \"auditmantype5\", " +
                    "         MAX (CASE orderid " +
                    "                 WHEN 5 " +
                    "                    THEN flow_empno " +
                    "              END) AS \"auditman6\", " +
                    "         MAX (CASE orderid " +
                    "                 WHEN 5 " +
                    "                    THEN flow_empname " +
                    "              END) AS \"localname6\", " +
                    "         MAX (CASE orderid " +
                    "                 WHEN 5 " +
                    "                    THEN flow_type " +
                    "              END) AS \"audittypename6\", " +
                    "         MAX (CASE orderid " +
                    "                 WHEN 5 " +
                    "                    THEN flow_level " +
                    "              END) AS \"auditmantype6\", " +
                    "         MAX (CASE orderid " +
                    "                 WHEN 6 " +
                    "                    THEN flow_empno " +
                    "              END) AS \"auditman7\", " +
                    "         MAX (CASE orderid " +
                    "                 WHEN 6 " +
                    "                    THEN flow_empname " +
                    "              END) AS \"localname7\", " +
                    "         MAX (CASE orderid " +
                    "                 WHEN 6 " +
                    "                    THEN flow_type " +
                    "              END) AS \"audittypename7\", " +
                    "         MAX (CASE orderid " +
                    "                 WHEN 6 " +
                    "                    THEN flow_level " +
                    "              END) AS \"auditmantype7\", " +
                    "         MAX (CASE orderid " +
                    "                 WHEN 7 " +
                    "                    THEN flow_empno " +
                    "              END) AS \"auditman8\", " +
                    "         MAX (CASE orderid " +
                    "                 WHEN 7 " +
                    "                    THEN flow_empname " +
                    "              END) AS \"localname8\", " +
                    "         MAX (CASE orderid " +
                    "                 WHEN 7 " +
                    "                    THEN flow_type " +
                    "              END) AS \"audittypename8\", " +
                    "         MAX (CASE orderid " +
                    "                 WHEN 7 " +
                    "                    THEN flow_level " +
                    "              END) AS \"auditmantype8\", " +
                    "         MAX (CASE orderid " +
                    "                 WHEN 8 " +
                    "                    THEN flow_empno " +
                    "              END) AS \"auditman9\", " +
                    "         MAX (CASE orderid " +
                    "                 WHEN 8 " +
                    "                    THEN flow_empname " +
                    "              END) AS \"localname9\", " +
                    "         MAX (CASE orderid " +
                    "                 WHEN 8 " +
                    "                    THEN flow_type " +
                    "              END) AS \"audittypename9\", " +
                    "         MAX (CASE orderid " +
                    "                 WHEN 8 " +
                    "                    THEN flow_level " +
                    "              END) AS \"auditmantype9\", " +
                    "         MAX (CASE orderid " +
                    "                 WHEN 9 " +
                    "                    THEN flow_empno " +
                    "              END) AS \"auditman10\", " +
                    "         MAX (CASE orderid " +
                    "                 WHEN 9 " +
                    "                    THEN flow_empname " +
                    "              END) AS \"localname10\", " +
                    "         MAX (CASE orderid " +
                    "                 WHEN 9 " +
                    "                    THEN flow_type " +
                    "              END) AS \"audittypename10\", " +
                    "         MAX (CASE orderid " +
                    "                 WHEN 9 " +
                    "                    THEN flow_level " +
                    "              END) AS \"auditmantype10\", " +
                    "         MAX (CASE orderid " +
                    "                 WHEN 10 " +
                    "                    THEN flow_empno " +
                    "              END) AS \"auditman11\", " +
                    "         MAX (CASE orderid " +
                    "                 WHEN 10 " +
                    "                    THEN flow_empname " +
                    "              END) AS \"localname11\", " +
                    "         MAX (CASE orderid " +
                    "                 WHEN 10 " +
                    "                    THEN flow_type " +
                    "              END) AS \"audittypename11\", " +
                    "         MAX (CASE orderid " +
                    "                 WHEN 10 " +
                    "                    THEN flow_level " +
                    "              END) AS \"auditmantype11\", " +
                    "         MAX (CASE orderid " +
                    "                 WHEN 11 " +
                    "                    THEN flow_empno " +
                    "              END) AS \"auditman12\", " +
                    "         MAX (CASE orderid " +
                    "                 WHEN 11 " +
                    "                    THEN flow_empname " +
                    "              END) AS \"localname12\", " +
                    "         MAX (CASE orderid " +
                    "                 WHEN 11 " +
                    "                    THEN flow_type " +
                    "              END) AS \"audittypename12\", " +
                    "         MAX (CASE orderid " +
                    "                 WHEN 11 " +
                    "                    THEN flow_level " +
                    "              END) AS \"auditmantype12\", " +
                    "         MAX (CASE orderid " +
                    "                 WHEN 12 " +
                    "                    THEN flow_empno " +
                    "              END) AS \"auditman13\", " +
                    "         MAX (CASE orderid " +
                    "                 WHEN 12 " +
                    "                    THEN flow_empname " +
                    "              END) AS \"localname13\", " +
                    "         MAX (CASE orderid " +
                    "                 WHEN 12 " +
                    "                    THEN flow_type " +
                    "              END) AS \"audittypename13\", " +
                    "         MAX (CASE orderid " +
                    "                 WHEN 12 " +
                    "                    THEN flow_level " +
                    "              END) AS \"auditmantype13\", " +
                    "         MAX (CASE orderid " +
                    "                 WHEN 13 " +
                    "                    THEN flow_empno " +
                    "              END) AS \"auditman14\", " +
                    "         MAX (CASE orderid " +
                    "                 WHEN 13 " +
                    "                    THEN flow_empname " +
                    "              END) AS \"localname14\", " +
                    "         MAX (CASE orderid " +
                    "                 WHEN 13 " +
                    "                    THEN flow_type " +
                    "              END) AS \"audittypename14\", " +
                    "         MAX (CASE orderid " +
                    "                 WHEN 13 " +
                    "                    THEN flow_level " +
                    "              END) AS \"auditmantype14\", " +
                    "         MAX (CASE orderid " +
                    "                 WHEN 14 " +
                    "                    THEN flow_empno " +
                    "              END) AS \"auditman15\", " +
                    "         MAX (CASE orderid " +
                    "                 WHEN 14 " +
                    "                    THEN flow_empname " +
                    "              END) AS \"localname15\", " +
                    "         MAX (CASE orderid " +
                    "                 WHEN 14 " +
                    "                    THEN flow_type " +
                    "              END) AS \"audittypename15\", " +
                    "         MAX (CASE orderid " +
                    "                 WHEN 14 " +
                    "                    THEN flow_level " +
                    "              END) AS \"auditmantype15\", " +
                    "         MAX (CASE orderid " +
                    "                 WHEN 15 " +
                    "                    THEN flow_empno " +
                    "              END) AS \"auditman16\", " +
                    "         MAX (CASE orderid " +
                    "                 WHEN 15 " +
                    "                    THEN flow_empname " +
                    "              END) AS \"localname16\", " +
                    "         MAX (CASE orderid " +
                    "                 WHEN 15 " +
                    "                    THEN flow_type " +
                    "              END) AS \"audittypename16\", " +
                    "         MAX (CASE orderid " +
                    "                 WHEN 15 " +
                    "                    THEN flow_level " +
                    "              END) AS \"auditmantype16\", " +
                    "         MAX (CASE orderid " +
                    "                 WHEN 16 " +
                    "                    THEN flow_empno " +
                    "              END) AS \"auditman17\", " +
                    "         MAX (CASE orderid " +
                    "                 WHEN 16 " +
                    "                    THEN flow_empname " +
                    "              END) AS \"localname17\", " +
                    "         MAX (CASE orderid " +
                    "                 WHEN 16 " +
                    "                    THEN flow_type " +
                    "              END) AS \"audittypename17\", " +
                    "         MAX (CASE orderid " +
                    "                 WHEN 16 " +
                    "                    THEN flow_level " +
                    "              END) AS \"auditmantype17\", " +
                    "         MAX (CASE orderid " +
                    "                 WHEN 17 " +
                    "                    THEN flow_empno " +
                    "              END) AS \"auditman18\", " +
                    "         MAX (CASE orderid " +
                    "                 WHEN 17 " +
                    "                    THEN flow_empname " +
                    "              END) AS \"localname18\", " +
                    "         MAX (CASE orderid " +
                    "                 WHEN 17 " +
                    "                    THEN flow_type " +
                    "              END) AS \"audittypename18\", " +
                    "         MAX (CASE orderid " +
                    "                 WHEN 17 " +
                    "                    THEN flow_level " +
                    "              END) AS \"auditmantype18\", " +
                    "         MAX (CASE orderid " +
                    "                 WHEN 18 " +
                    "                    THEN flow_empno " +
                    "              END) AS \"auditman19\", " +
                    "         MAX (CASE orderid " +
                    "                 WHEN 18 " +
                    "                    THEN flow_empname " +
                    "              END) AS \"localname19\", " +
                    "         MAX (CASE orderid " +
                    "                 WHEN 18 " +
                    "                    THEN flow_type " +
                    "              END) AS \"audittypename19\", " +
                    "         MAX (CASE orderid " +
                    "                 WHEN 18 " +
                    "                    THEN flow_level " +
                    "              END) AS \"auditmantype19\", " +
                    "         MAX (CASE orderid " +
                    "                 WHEN 19 " +
                    "                    THEN flow_empno " +
                    "              END) AS \"auditman20\", " +
                    "         MAX (CASE orderid " +
                    "                 WHEN 19 " +
                    "                    THEN flow_empname " +
                    "              END) AS \"localname20\", " +
                    "         MAX (CASE orderid " +
                    "                 WHEN 19 " +
                    "                    THEN flow_type " +
                    "              END) AS \"audittypename20\", " +
                    "         MAX (CASE orderid " +
                    "                 WHEN 19 " +
                    "                    THEN flow_level " +
                    "              END) AS \"auditmantype20\" " +
                    "    FROM gds_wf_flowset " +
                    "   WHERE deptcode IN (SELECT     depcode " +
                    "                            FROM gds_sc_department " +
                    "                      START WITH depcode = :deptid " +
                    "                      CONNECT BY PRIOR depcode = parentdepcode) " +
                    "GROUP BY deptcode, formtype, reason1, reason2, reason3, reason4 ";
            try
            {
                DataTable dt = DalHelper.ExecuteQuery(sql, new OracleParameter(":deptid", deptid));
                ORMHelper<WorkFlowSetModel> dal = new ORMHelper<WorkFlowSetModel>();

                return dal.SetDataTableToList(dt);
               
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 根據工號獲取管理職
        /// </summary>
        /// <param name="empno"></param>
        /// <returns></returns>
        public string GetManager(string empno)
        {
            string sql
            = "SELECT managername " +
                "  FROM gds_att_employee " +
                " WHERE workno = :empno ";
            try
            {
                DataTable dt = DalHelper.ExecuteQuery(sql, new OracleParameter(":empno", empno));
                string result = string.Empty;
                if (dt != null && dt.Rows.Count > 0)
                {
                    result = dt.Rows[0]["managername"].ToString();
                }
                return result;
            }
            catch
            {
                return string.Empty;
            }

        }

        /// <summary>
        /// 刪除全部流程
        /// </summary>
        /// <param name="deptid"></param>
        /// <returns></returns>
        public bool DeleteAllWorkFlowData(string deptid)
        {
            string sql
            = "DELETE FROM gds_wf_flowset " +
                "      WHERE deptcode = :deptid ";
            try
            {
                int i = DalHelper.ExecuteNonQuery(sql, new OracleParameter(":deptid", deptid));
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


        /// <summary>
        /// 導出Excel
        /// </summary>
        /// <param name="deptid"></param>
        /// <returns></returns>
        public List<WorkFlowSignModel> GetSignExpData(string deptid)
        {
            string sql
	        = "SELECT   deptcode, " + 
                "         formtype, " +
                "         reason1, " +
                "         reason2, " +
                "         reason3, " +
                "         reason4, " +
                "         orderid, " +
                "         flow_empno, " +
                "         flow_empname, " +
                "         flow_notes, " +
                "         flow_manager, " +
                "         flow_type, " +
                "         flow_level " +
                "    FROM gds_wf_flowset " +
                "   WHERE deptcode = :deptid " +
                "ORDER BY deptcode, formtype, reason1, reason2, reason3, reason4, orderid " ;
            try
            {
                DataTable dt = DalHelper.ExecuteQuery(sql, new OracleParameter(":deptid", deptid));
                ORMHelper<WorkFlowSignModel> dal = new ORMHelper<WorkFlowSignModel>();
                return dal.SetDataTableToList(dt);
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        /// <summary>
        /// 批量替換指定工號
        /// </summary>
        /// <param name="info_FormType"></param>
        /// <param name="o_empno"></param>
        /// <param name="n_empno"></param>
        /// <param name="n_name"></param>
        /// <param name="notes"></param>
        /// <param name="n_manager"></param>
        /// <returns></returns>
        public bool ReplaceAllEmpno(string[] info_FormType, string o_empno, string n_empno, string n_name, string notes, string n_manager)
        {
            try
            {
                bool result = false;
                if (info_FormType != null && info_FormType.Length > 0)
                {
                    OracleCommand command = new OracleCommand();
                    command.Connection = DalHelper.Connection;
                    command.Connection.Open();
                    OracleTransaction trans = command.Connection.BeginTransaction();
                    command.Transaction = trans;
                    foreach (string formtype in info_FormType)
                    {
                        string sql
                        = "UPDATE gds_wf_flowset " +
                            "   SET flow_empno = '" + n_empno + "', " +
                            "       flow_empname = '" + n_name + "', " +
                            "       flow_notes = '" + notes + "', " +
                            "       flow_manager = '" + n_manager + "' " +
                            " WHERE formtype = '" + formtype + "' AND flow_empno = '" + o_empno + "' ";
                        command.CommandText = sql;
                        command.ExecuteNonQuery();
                    }
                    trans.Commit();
                    command.Connection.Close();
                    result = true;
                }
                return result;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// 查詢數據
        /// </summary>
        /// <param name="dept_list"></param>
        /// <param name="empno"></param>
        /// <param name="empname"></param>
        /// <returns></returns>
        public DataTable GetDeptPerson(List<string> dept_list, string empno, string empname)
        {
            try
            {
                string dept = string.Empty;
                if (dept_list != null && dept_list.Count > 0)
                {
                    foreach (string item in dept_list)
                    {
                        dept += "'" + item + "'" + ",";
                    }
                }
                if (dept != string.Empty)
                {
                    dept = dept.Substring(0, dept.Length - 1);
                }
                string sql
                 = "SELECT depname, " +
                     "       workno, " +
                     "       localname, " +
                     "       notes, " +
                     "       managername " +
                     "  FROM gds_att_employee b " +
                     " WHERE EXISTS (SELECT 1 " +
                     "                 FROM (SELECT     depcode " +
                     "                             FROM gds_sc_department " +
                     "                       START WITH depcode IN (" + dept + ") " +
                     "                       CONNECT BY PRIOR depcode = parentdepcode) a " +
                     "                WHERE a.depcode = b.depcode) " +
                     "   AND (workno LIKE '%' || :workno || '%' OR :workno IS NULL) " +
                     "   AND (localname LIKE '%' || :localname || '%' OR :localname IS NULL) ";
                DataTable dt = DalHelper.ExecuteQuery(sql, new OracleParameter(":workno", empno), new OracleParameter(":localname", empname));
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        /// <summary>
        /// 獲取天數類別
        /// </summary>
        /// <param name="deptid"></param>
        /// <returns></returns>
        public DataTable GetDaysType(string deptid,string day_type)
        {
            try
            {
                string sql
                = "SELECT day_code, " +
                    "       day_min, " +
                    "       day_max " +
                    "  FROM gds_wf_dayset " +
                    " WHERE (deptid = :deptid OR :deptid IS NULL) AND day_type = :day_type order by day_min";
                DataTable dt = DalHelper.ExecuteQuery(sql, new OracleParameter(":deptid", deptid), new OracleParameter(":day_type", day_type));
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 獲取天數類別
        /// </summary>
        /// <param name="deptid"></param>
        /// <returns></returns>
        public DataTable GetDaysType_1(string deptid, string day_type)
        {
            try
            {
                string sql
                = "SELECT   day_code, " +
                    "         CASE " +
                    "            WHEN day_type = 'ShiweiType' " +
                    "               THEN (SELECT oc_name " +
                    "                       FROM gds_wf_ziwei " +
                    "                      WHERE oc_code = day_min) " +
                    "            WHEN day_type = 'GlzType' " +
                    "               THEN (SELECT g_name " +
                    "                       FROM gds_wf_manager " +
                    "                      WHERE g_code = day_min) " +
                    "         END day_min, " +
                    "         CASE " +
                    "            WHEN day_type = 'ShiweiType' " +
                    "               THEN (SELECT oc_name " +
                    "                       FROM gds_wf_ziwei " +
                    "                      WHERE oc_code = day_max) " +
                    "            WHEN day_type = 'GlzType' " +
                    "               THEN (SELECT g_name " +
                    "                       FROM gds_wf_manager " +
                    "                      WHERE g_code = day_max) " +
                    "         END day_max " +
                    "    FROM gds_wf_dayset " +
                    "   WHERE (deptid = :deptid OR :deptid IS NULL) AND day_type = :day_type " +
                    "ORDER BY day_min ";


                DataTable dt = DalHelper.ExecuteQuery(sql, new OracleParameter(":deptid", deptid), new OracleParameter(":day_type", day_type));
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        /// <summary>
        /// 插入天數數據
        /// </summary>
        /// <param name="deptid"></param>
        /// <param name="dayscode"></param>
        /// <param name="daysname"></param>
        /// <param name="mindays"></param>
        /// <param name="maxdays"></param>
        /// <param name="day_type"></param>
        /// <returns></returns>
        public bool InsertDayType(string deptid, string dayscode, string daysname, int mindays, int maxdays, string day_type)
        {
            try
            {
                string sql
                    = "INSERT INTO gds_wf_dayset " +
                        "            (day_code, day_min, day_max, day_name, deptid, day_type " +
                        "            ) " +
                        "     VALUES ('" + dayscode + "', '" + mindays + "', '" + maxdays + "', '" + daysname + "', '" + deptid + "', '" + day_type + "' " +
                        "            ) ";
                int i = DalHelper.ExecuteNonQuery(sql);
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

        /// <summary>
        /// 刪除DAY數據
        /// </summary>
        /// <param name="deptid"></param>
        /// <param name="dayscode"></param>
        /// <param name="typecode"></param>
        /// <returns></returns>
        public bool DeleteDayType(string deptid, string dayscode, string typecode)
        {
            try
            {
                string sql
                = "DELETE FROM gds_wf_dayset " +
                    "      WHERE day_type = '" + typecode + "' AND deptid = '" + deptid + "' AND day_code = '" + dayscode + "' ";
                int i = DalHelper.ExecuteNonQuery(sql);
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


        /// <summary>
        /// 獲取師資位表
        /// </summary>
        /// <returns></returns>
        public DataTable GetShiwei()
        {
            try
            {
                string sql
                    = "select OC_CODE, OC_NAME from GDS_WF_ZIWEI order by OC_CODE ";
                return DalHelper.ExecuteQuery(sql);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 獲取管理職表
        /// </summary>
        /// <returns></returns>
        public DataTable GetGlz()
        {
            try
            {
                string sql
                    = "select G_CODE,G_NAME from  GDS_WF_MANAGER order by  G_CODE ";
                return DalHelper.ExecuteQuery(sql);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 獲取類別
        /// </summary>
        /// <param name="deptid"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public DataTable GetKeyValue(string deptid, string type)
        {
            try
            {
                string sql
                    = "SELECT   DAY_NAME,DAY_CODE " +
                        "    FROM gds_wf_dayset " +
                        "   WHERE deptid = '" + deptid + "' AND day_type = '" + type + "' " +
                        "ORDER BY day_min ";
                return DalHelper.ExecuteQuery(sql);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}
