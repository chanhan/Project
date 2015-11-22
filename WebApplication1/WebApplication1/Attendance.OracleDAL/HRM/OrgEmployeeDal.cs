/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： OTMTypeDal.cs
 * 檔功能描述： 人員層級功能模組操作類
 * 
 * 版本：1.0
 * 創建標識： 陈函 2011.12.13
 * 
 */
using System;
using System.Data;
using System.Data.OracleClient;
using GDSBG.MiABU.Attendance.IDAL.HRM;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;

namespace GDSBG.MiABU.Attendance.OracleDAL.HRM
{
    public class OrgEmployeeDal : DALBase<AuthorityModel>, IOrgEmployeeDal
    {
        /// <summary>
        /// 得到人員層級樹
        /// </summary>
        /// <param name="personCode">工號</param>
        /// <param name="companyId">公司ID</param>
        /// <param name="moduleCode">模組代碼</param>
        /// <returns>查詢結果DataTable</returns>
        public DataTable getOrgEmployeeTree(string personCode, string companyId, string moduleCode)
        {
            string cmdText = "gds_att_relationemployeepro";
            OracleParameter dtresult = new OracleParameter("dt", OracleType.Cursor);
            dtresult.Direction = ParameterDirection.Output;
            DataTable dt = DalHelper.ExecuteQuery(cmdText, CommandType.StoredProcedure,
               new OracleParameter("v_personcode", personCode),
                new OracleParameter("v_companyid", companyId),
             new OracleParameter("v_mudulecode", moduleCode), dtresult);
            return dt;
        }
        /// <summary>
        /// 根據組織代碼查詢
        /// </summary>
        /// <param name="OrgCode">組織代碼</param>
        /// <returns>查詢結果DataTable</returns>
        public DataTable GetDataByOrgCode(string OrgCode)
        {
            string cmdText = @"SELECT   workno, localname, depcode
                                FROM gds_att_employee a
                               WHERE NOT EXISTS (SELECT workno
                                                   FROM gds_att_employeemove b
                                                  WHERE a.workno = b.workno)
                                 AND a.status < '2'
                                 AND a.depcode IN (SELECT     depcode
                                                         FROM gds_sc_department
                                                   START WITH depcode = :Org_Code
                                                   CONNECT BY PRIOR depcode = parentdepcode)";
            return DalHelper.ExecuteQuery(cmdText, new OracleParameter(":Org_Code", OrgCode));
        }

        /// <summary>
        /// 獲取處級及以下組織代碼
        /// </summary>
        /// <param name="OrgCode">組織代碼</param>
        /// <returns>處級及以下組織代碼</returns>
        public DataTable GetDepCode(string OrgCode)
        {
            string cmdText = "SELECT depcode FROM gds_sc_department  WHERE (levelcode = '3' or levelcode='4') START WITH depcode = :Org_Code CONNECT BY depcode = PRIOR parentdepcode";
            return DalHelper.ExecuteQuery(cmdText, new OracleParameter(":Org_Code", OrgCode));
        }
        /// <summary>
        /// 查詢所選組織下的子階組織或所屬組級、線級、段級的所有組
        /// </summary>
        /// <param name="personCode">工號</param>
        /// <param name="companyId">公司ID</param>
        /// <param name="moduleCode">模組代碼</param>
        /// <param name="parentDep">父級組織代碼</param>
        /// <returns>所選組織下的子階組織或所屬組級、線級、段級的所有組</returns>
        public DataTable GetAuthorizedDept(string personCode, string companyId, string moduleCode, string parentDep)
        {
            //  string cmdText = "gds_att_singledatapro";
            /*   string cmdText = @"select * from  (SELECT depcode,depname
         FROM (SELECT a.*, b.levelname, c.depname parentdepname
                 FROM gds_sc_department a,
                      gds_sc_deplevel b,
                      gds_sc_department c,
                      (SELECT depcode tdcode
                         FROM gds_sc_persondept temp
                        WHERE temp.personcode = :v_personcode
                          AND temp.companyid = :v_companyid
                          AND temp.modulecode = :v_modulecode) e
                WHERE a.levelcode = b.levelcode(+)
                  AND a.parentdepcode = c.depcode(+)
                  AND a.depcode = e.tdcode) thisdept
   START WITH parentdepcode =:v_parentDep
   CONNECT BY PRIOR depcode = parentdepcode
     ORDER SIBLINGS BY orderid)signledata";*/
            string cmdText = @"select * from(SELECT depname, depcode
      FROM (SELECT a.*, b.levelname, c.depname parentdepname,
                   d.simplename corporationname
              FROM gds_sc_department a,
                   gds_sc_deplevel b,
                   gds_sc_department c,
                   gds_att_corporation d,
                   (SELECT depcode tdcode
                      FROM gds_sc_persondept temp
                     WHERE temp.personcode =:v_personcode
                       AND temp.companyid =:v_companyid
                       AND temp.modulecode = :v_modulecode) e
             WHERE a.levelcode = b.levelcode(+)
               AND a.parentdepcode = c.depcode(+)
               AND a.corporationid = d.corporationid(+)
               AND a.depcode = e.tdcode) dept
START WITH parentdepcode = :v_parentDep
CONNECT BY PRIOR depcode = parentdepcode
  ORDER SIBLINGS BY orderid) signledata";
            DataTable dt = DalHelper.ExecuteQuery(cmdText,
               new OracleParameter(":v_personcode", personCode),
                new OracleParameter(":v_companyid", companyId),
             new OracleParameter(":v_modulecode", moduleCode), new OracleParameter(":v_parentDep", parentDep));
            return dt;
        }
        /// <summary>
        /// 所選"課組"必須是所填員工目前所屬部門下的課或組
        /// </summary>
        /// <param name="moduleCode">模組代碼</param>
        /// <param name="OrgCode">組織代碼</param>
        /// <param name="personCode">登陸工號</param>
        /// <param name="companyId">公司ID</param>
        /// <returns>查詢結果DataTable</returns>
        public DataTable IsInDep(string OrgCode, string sqlDep)
        {
            //if (moduleCode.Length == 0)
            //{
            //    //  sqlDep = "SELECT 1 FROM dual";
            //    string cmdText = "select depcode from gds_sc_department where depcode in( SELECT 1 FROM dual ) and levelcode in(4,5,6,7) and DepCode=:Org_Code";
            //    return DalHelper.ExecuteQuery(cmdText, new OracleParameter(":Org_Code", OrgCode));
            //}
            //else
            //{
            //    //     sqlDep = "SELECT depcode FROM gds_sc_persondept where personcode=:p_personcode and modulecode=:p_modulecode and companyid=:p_companyid";
            //    string cmdText = "select depcode from gds_sc_department where depcode in( SELECT depcode FROM gds_sc_persondept where personcode=:p_personcode and modulecode=:p_modulecode and companyid=:p_companyid ) and levelcode in(4,5,6,7) and DepCode=:Org_Code";
            //    return DalHelper.ExecuteQuery(cmdText,
            //        new OracleParameter(":Org_Code", OrgCode),
            //        new OracleParameter(":p_personcode", personCode),
            //       new OracleParameter(":p_companyid", companyId),
            //    new OracleParameter(":p_modulecode", moduleCode));
            //}
            string cmdText = "select depcode from gds_sc_department where depcode in(" + sqlDep + ") and levelcode in(4,5,6,7) and DepCode='" + OrgCode + "'";
            return DalHelper.ExecuteQuery(cmdText);
        }

        /// <summary>
        /// 保存清除編組
        /// </summary>
        /// <param name="worknos">所有工號</param>
        /// <param name="OrgCode">組織代碼</param>
        /// <param name="personcode">登陸帳號</param>
        /// <returns>返回結果</returns>
        public int UpdateData(string worknos, string OrgCode, string personCode, SynclogModel logmodel)
        {
            string cmdText = "gds_att_orgemployeepro";
            OracleParameter dtresult = new OracleParameter("RESULT", OracleType.Int32);
            dtresult.Direction = ParameterDirection.Output;
            DalHelper.ExecuteQuery(cmdText, CommandType.StoredProcedure,
              new OracleParameter("p_worknos", worknos),
               new OracleParameter("p_orgcode", OrgCode),
            new OracleParameter("p_appuser", personCode), dtresult,
            new OracleParameter("p_transactiontype", logmodel.TransactionType == null ? "" : logmodel.TransactionType.ToString()),
                   new OracleParameter("p_levelno", logmodel.LevelNo == null ? "" : logmodel.LevelNo.ToString()),
                   new OracleParameter("p_fromhost", logmodel.FromHost == null ? "" : logmodel.FromHost.ToString()),
                   new OracleParameter("p_tohost", logmodel.ToHost == null ? "" : logmodel.ToHost.ToString()),
                   new OracleParameter("p_docno", logmodel.DocNo == null ? "" : logmodel.DocNo.ToString()),
                   new OracleParameter("p_processflag", logmodel.ProcessFlag == null ? "" : logmodel.ProcessFlag.ToString()),
                   new OracleParameter("p_processowner", logmodel.ProcessOwner == null ? "" : logmodel.ProcessOwner.ToString()));
            return Convert.ToInt32(dtresult.Value);
        }


        /// <summary>
        /// 導入Excel
        /// </summary>
        /// <param name="createUser">創建者</param>
        /// <param name="successnum">導入成功記錄數</param>
        /// <param name="errornum">導入失敗記錄數</param>
        /// <returns>導入的信息DataTable</returns>
        public DataTable ImpoertExcel(string createUser, string moduleCode, string companyID, out int successnum, out int errornum,SynclogModel logmodel)
        {
            OracleParameter outCursor = new OracleParameter("p_coursor", OracleType.Cursor);
            OracleParameter outSuccess = new OracleParameter("p_success", OracleType.Int32);
            OracleParameter outError = new OracleParameter("p_error", OracleType.Int32);
            outCursor.Direction = ParameterDirection.Output;
            outSuccess.Direction = ParameterDirection.Output;
            outError.Direction = ParameterDirection.Output;
            DataTable dt = DalHelper.ExecuteQuery("gds_sc_orgemp_vaildata", CommandType.StoredProcedure,
                new OracleParameter("p_personcode", createUser),
                new OracleParameter("p_modulecode", moduleCode),
                new OracleParameter("p_companyid", companyID),
                outCursor, outSuccess, outError,
                   new OracleParameter("p_transactiontype", logmodel.TransactionType == null ? "" : logmodel.TransactionType.ToString()),
                   new OracleParameter("p_levelno", logmodel.LevelNo == null ? "" : logmodel.LevelNo.ToString()),
                   new OracleParameter("p_fromhost", logmodel.FromHost == null ? "" : logmodel.FromHost.ToString()),
                   new OracleParameter("p_tohost", logmodel.ToHost == null ? "" : logmodel.ToHost.ToString()),
                   new OracleParameter("p_docno", logmodel.DocNo == null ? "" : logmodel.DocNo.ToString()),
                   new OracleParameter("p_processflag", logmodel.ProcessFlag == null ? "" : logmodel.ProcessFlag.ToString()),
                   new OracleParameter("p_processowner", logmodel.ProcessOwner == null ? "" : logmodel.ProcessOwner.ToString())
                );
            successnum = Convert.ToInt32(outSuccess.Value);
            errornum = Convert.ToInt32(outError.Value);
            return dt;
        }
    }
}
