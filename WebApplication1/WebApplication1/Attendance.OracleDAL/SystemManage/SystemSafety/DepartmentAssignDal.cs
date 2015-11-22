/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： DepartmentAssignDal.cs
 * 檔功能描述： 關聯組織設定數據操作類
 * 
 * 版本：1.0
 * 創建標識： 顧陳偉 2011.12.8
 * 
 */

using System;
using System.Data;
using System.Data.OracleClient;
using GDSBG.MiABU.Attendance.IDAL.SystemManage.SystemSafety;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;

namespace GDSBG.MiABU.Attendance.OracleDAL.SystemManage.SystemSafety
{
    public class DepartmentAssignDal : DALBase<PersonModel>, IDepartmentAssignDal
    {
        /// <summary>
        /// 獲得用戶的所有功能模組
        /// </summary>
        /// <param name="personcode"></param>
        /// <returns></returns>
        public DataTable GetPersonModule(string rolescode)
        {
            string str = "select * from (select a.*,b.functionlist as allfunctionlist,b.description,b.language_key,b.functiondesc,b.formname,b.url,b.orderid,b.parentmodulecode,'Y' as authorized from (select modulecode,max(rolecode)rolecode from gds_sc_authority where rolecode in(select y.rolecode from gds_sc_roles x,gds_sc_rolesrole y where x.rolescode=y.rolescode and x.rolescode=:rolescode) group by modulecode) a,gds_sc_module b where a.modulecode=b.modulecode(+) and b.privileged='Y' and exists(select * from gds_sc_role where rolecode=a.rolecode and deleted='N') and b.deleted='N')  f where (formname is not null or exists (select * from gds_sc_module d where exists(select * from gds_sc_authority where rolecode in(select y.rolecode from gds_sc_roles x,gds_sc_rolesrole y where x.rolescode=y.rolescode and x.rolescode=:rolescode) and modulecode=d.modulecode)  and formname is not null start with (parentmodulecode =f.modulecode) and deleted='N' connect by prior modulecode=parentmodulecode))start with (parentmodulecode is null) connect by prior modulecode=parentmodulecode order siblings by orderid";
            return DalHelper.ExecuteQuery(str, new OracleParameter(":rolescode", rolescode));
        }

        /// <summary>
        /// 獲得用戶的公司代碼
        /// </summary>
        /// <param name="personcode"></param>
        /// <returns></returns>
        public DataTable GetPersonCompany(string personcode)
        {
            string str = "select * from (select a.*,b.companyname from gds_sc_personcompany a,gds_sc_company b where a.companyid=b.companyid(+) order by a.companyid) where personcode  =:personcode";
            return DalHelper.ExecuteQuery(str, new OracleParameter(":personcode", personcode));
        }

        /// <summary>
        /// 獲得用戶的組織層級
        /// </summary>
        /// <param name="personcode"></param>
        /// <returns></returns>
        public DataTable GetPersonDeplevel(string personcode)
        {
            string str = "select nvl(max(b.orderid),'0') from gds_sc_person a,gds_sc_deplevel b where a.deplevel=b.levelcode and a.personcode=:personcode";
            return DalHelper.ExecuteQuery(str, new OracleParameter(":personcode", personcode));
        }

        /// <summary>
        /// 獲得所有的組織層級
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllLevelCode()
        {
            string str="select * from gds_sc_deplevel order by orderid";
            return DalHelper.ExecuteQuery(str);
        }

        /// <summary>
        /// 根據公司和模組查詢權限範圍內的部門
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="personCode"></param>
        /// <param name="ModuleCode"></param>
        /// <param name="CompanyId"></param>
        /// <param name="DepLevel"></param>
        /// <returns></returns>
        public DataTable GetPersonDeptDataByModule(string Appuser,string modulecode, string personCode, string ModuleCode, string CompanyId, string DepLevel)
        {
            string str = @"SELECT     *
                         FROM (SELECT a.*, b.personcode,
                         DECODE (b.personcode, NULL, 'N', 'Y') AS authorized
                   FROM (SELECT x.*
                      FROM gds_sc_department x, gds_sc_deplevel y
                     WHERE x.levelcode = y.levelcode AND y.orderid <=
                                                                     :deplevel) a,
                   (SELECT *
                      FROM gds_sc_persondept
                     WHERE personcode = :personcode
                       AND modulecode = :modulecode
                       AND companyid = :companyid) b,
                   ( SELECT char_list depcode
                                   FROM TABLE (gds_sc_getsqldep (:Appuser,
                                                                 :appmodulecode
                                                                )
                                              )) c
             WHERE a.companyid = :companyid
               AND a.deleted = 'N'
               AND a.companyid = b.companyid(+)
               AND a.depcode = b.depcode(+)
               AND a.depcode = c.depcode)
             START WITH (parentdepcode IS NULL)
            CONNECT BY PRIOR depcode = parentdepcode";
            return DalHelper.ExecuteQuery(str, new OracleParameter(":Appuser", Appuser), new OracleParameter(":appmodulecode", modulecode), new OracleParameter(":personCode", personCode), new OracleParameter(":ModuleCode", ModuleCode), new OracleParameter(":CompanyId", CompanyId), new OracleParameter(":DepLevel", DepLevel));
        }

        /// <summary>
        /// 保存用戶組織關聯信息
        /// </summary>
        /// <param name="personcode"></param>
        /// <param name="moduleCode"></param>
        /// <param name="companyId"></param>
        /// <param name="depts"></param>
        /// <param name="p_user"></param>
        /// <returns></returns>
        public int SavePersonDeptData(string personcode, string rolecode, string modulecode, string companyId, string depts, string p_user, SynclogModel logmodel)
        {
            OracleParameter outPara = new OracleParameter("p_out", OracleType.Int32);
            outPara.Direction = ParameterDirection.Output;
            DalHelper.ExecuteNonQuery("gds_sc_savepersondept", CommandType.StoredProcedure,
                new OracleParameter("p_personcode", personcode), new OracleParameter("p_rolecode", rolecode), new OracleParameter("p_modulecode", modulecode), new OracleParameter("p_companyid", companyId),
                new OracleParameter("p_deptlist", depts), new OracleParameter("p_user", p_user), outPara, 
                new OracleParameter("p_transactiontype", logmodel.TransactionType == null ? "" : logmodel.TransactionType.ToString()),
                new OracleParameter("p_levelno", logmodel.LevelNo == null ? "" : logmodel.LevelNo.ToString()),
                new OracleParameter("p_fromhost", logmodel.FromHost == null ? "" : logmodel.FromHost.ToString()),
                new OracleParameter("p_tohost", logmodel.ToHost == null ? "" : logmodel.ToHost.ToString()),
                new OracleParameter("p_docno", logmodel.DocNo == null ? "" : logmodel.DocNo.ToString()),
                new OracleParameter("p_processflag", logmodel.ProcessFlag == null ? "" : logmodel.ProcessFlag.ToString()),
                new OracleParameter("p_processowner", logmodel.ProcessOwner == null ? "" : logmodel.ProcessOwner.ToString()));
            return Convert.ToInt32(outPara.Value);
        }

    }
}
