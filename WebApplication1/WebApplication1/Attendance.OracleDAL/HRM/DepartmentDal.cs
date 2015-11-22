/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： DepartmentDal.cs
 * 檔功能描述： 組織擴資料數據操作類
 * 
 * 版本：1.0
 * 創建標識： 顧陳偉 2012.02.03
 * 
 */

using System;
using System.Data;
using System.Data.OracleClient;
using System.Collections;
using GDSBG.MiABU.Attendance.IDAL.HRM;
using System.Collections.Generic;
using GDSBG.MiABU.Attendance.Model.HRM;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;

namespace GDSBG.MiABU.Attendance.OracleDAL.HRM
{
    public class DepartmentDal : DALBase<DepartmentModel>, IDepartmentDal
    {
        /// <summary>
        /// 獲得部門排序Id
        /// </summary>
        /// <returns></returns>
        public DataTable GetOrderId()
        {
            string str = "select orderid from gds_sc_deplevel where levelcode='3'";
            return DalHelper.ExecuteQuery(str);
        }

        /// <summary>
        /// 根據條件查詢
        /// </summary>
        /// <param name="model"></param>
        /// <param name="sql"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public DataTable GetDeaprtmentPageInfo(DepartmentModel model,string levelcode,string sql, int pageIndex, int pageSize, out int totalCount)
        {
            string strCon = "";
            List<OracleParameter> listPara = DalHelper.CreateConditionParameters(model, true, "a", out strCon);
            string cmdText = @"select a.depname1,a.companyid,a.depcode,a.depname,a.costcode,a.deleted,a.leadercode,
                               a.assistantcode,a.parentdepcode,a.levelcode,to_char(a.orderid) orderid,a.corporationid,a.dephead,
                               a.depart_no,a.areacode,a.accountentity,a.create_date,a.create_user,a.update_date,
                               a.update_user,a.levelname,a.parentdepname,a.corporationname,a.areaname,a.siteid,a.deptshortname,
                               a.factorycode,a.deptalias from gds_att_department_v a where 1=1 ";
            string condition = " and a.depcode in (" + sql + ")";
            cmdText = cmdText + strCon + condition;
            if (levelcode.Length > 0)
            {
                cmdText = cmdText + " and a.levelcode in(select levelcode from gds_sc_deplevel where orderid<='" + levelcode + "')";
            }
            return DalHelper.ExecutePagerQuery(cmdText, pageIndex, pageSize, out totalCount, listPara.ToArray());
        }

        /// <summary>
        /// 導出查詢
        /// </summary>
        /// <param name="model"></param>
        /// <param name="levelcode"></param>
        /// <param name="sql"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public DataTable GetDeaprtmentForExport(DepartmentModel model, string levelcode, string sql)
        {
            string strCon = "";
            List<OracleParameter> listPara = DalHelper.CreateConditionParameters(model, true, "a", out strCon);
            string cmdText = @"select a.depname1,a.companyid,a.depcode,a.depname,a.costcode,a.deleted,a.leadercode,
                               a.assistantcode,a.parentdepcode,a.levelcode,to_char(a.orderid) orderid,a.corporationid,a.dephead,
                               a.depart_no,a.areacode,a.accountentity,a.create_date,a.create_user,a.update_date,
                               a.update_user,a.levelname,a.parentdepname,a.corporationname,a.areaname,a.siteid,a.deptshortname,
                               a.factorycode,a.deptalias from gds_att_department_v a where 1=1 ";
            string condition = " and a.depcode in (" + sql + ")";
            cmdText = cmdText + strCon + condition;
            if (levelcode.Length > 0)
            {
                cmdText = cmdText + " and a.levelcode in(select levelcode from gds_sc_deplevel where orderid<='" + levelcode + "')";
            }
            return DalHelper.ExecuteQuery(cmdText, listPara.ToArray());
        }


        /// <summary>
        /// 將datatable轉換為List
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public List<DepartmentModel> GetList(DataTable dt)
        {
            return OrmHelper.SetDataTableToList(dt);
        }

        /// <summary>
        /// 查詢組織下的員工
        /// </summary>
        /// <param name="depcode"></param>
        /// <returns></returns>
        public DataTable GetWorkNoByDept(string depcode)
        {
            string str = @"SELECT workno
                          FROM gds_att_employee
                         WHERE status < '2' AND depcode IN (SELECT     depcode
                                                                FROM gds_sc_department
                                                          START WITH depcode = :depcode
                                                          CONNECT BY PRIOR depcode = parentdepcode)";
            return DalHelper.ExecuteQuery(str, new OracleParameter(":depcode", depcode));
        }

        /// <summary>
        ///查詢子組織
        /// </summary>
        /// <param name="companyid"></param>
        /// <param name="depcode"></param>
        /// <returns></returns>
        public DataTable GetDeptByParent(string companyid, string depcode)
        {
            string str = @"SELECT *
                          FROM (SELECT a.*, b.levelname, c.depname parentdepname,
                                       d.simplename corporationname, e.singlename areaname
                                  FROM gds_sc_department a,
                                       gds_sc_deplevel b,
                                       gds_sc_department c,
                                       gds_att_corporation d,
                                       gds_att_plantarea e
                                 WHERE a.levelcode = b.levelcode(+)
                                   AND a.parentdepcode = c.depcode(+)
                                   AND a.corporationid = d.corporationid(+)
                                   AND a.areacode = e.areacode(+))
                         WHERE companyid = :companyid
                           AND depcode IN (SELECT     depcode
                                                 FROM gds_sc_department
                                           START WITH depcode = :depcode
                                           CONNECT BY PRIOR depcode = parentdepcode)";
            return DalHelper.ExecuteQuery(str, new OracleParameter(":companyid", companyid), new OracleParameter(":depcode", depcode));
        }

        /// <summary>
        /// 查詢用戶層級信息
        /// </summary>
        /// <param name="personcode"></param>
        /// <param name="levelcode"></param>
        /// <returns></returns>
        public DataTable GetUserDepLevel(string personcode, string levelcode)
        {
            string str = "SELECT * FROM gds_sc_deplevel WHERE levelcode=:levelcode AND levelcode IN(SELECT levelcode FROM gds_sc_personlevel WHERE personcode=:personcode) ORDER BY orderid ";
            return DalHelper.ExecuteQuery(str, new OracleParameter(":personcode", personcode), new OracleParameter(":levelcode", levelcode));
        }

        /// <summary>
        /// 失效
        /// </summary>
        /// <param name="companyid"></param>
        /// <param name="depcode"></param>
        /// <returns></returns>
        public bool Disable(string companyid, string depcode, SynclogModel logmodel)
        {
            string str = @"UPDATE gds_sc_department
                           SET deleted = 'Y'
                         WHERE companyid = :companyid
                           AND depcode IN (
                                  SELECT depcode
                                    FROM (SELECT a.*, b.levelname, c.depname parentdepname,
                                                 d.simplename corporationname, e.singlename areaname
                                            FROM gds_sc_department a,
                                                 gds_sc_deplevel b,
                                                 gds_sc_department c,
                                                 gds_att_corporation d,
                                                 gds_att_plantarea e
                                           WHERE a.levelcode = b.levelcode(+)
                                             AND a.parentdepcode = c.depcode(+)
                                             AND a.corporationid = d.corporationid(+)
                                             AND a.areacode = e.areacode(+))
                                   WHERE companyid = :companyid
                                     AND depcode IN (SELECT     depcode
                                                           FROM gds_sc_department
                                                     START WITH depcode = :depcode
                                                     CONNECT BY PRIOR depcode = parentdepcode))";
            return DalHelper.ExecuteNonQuery(str,logmodel, new OracleParameter(":companyid", companyid), new OracleParameter(":depcode", depcode)) != -1;
        }

        /// <summary>
        /// 查詢組織信息
        /// </summary>
        /// <param name="companyid"></param>
        /// <param name="depcode"></param>
        /// <returns></returns>
        public DataTable GetDept(string companyid, string depcode)
        {
            string str = @"SELECT *
                          FROM (SELECT a.*, b.levelname, c.depname parentdepname,
                                       d.simplename corporationname, e.singlename areaname
                                  FROM gds_sc_department a,
                                       gds_sc_deplevel b,
                                       gds_sc_department c,
                                       gds_att_corporation d,
                                       gds_att_plantarea e
                                 WHERE a.levelcode = b.levelcode(+)
                                   AND a.parentdepcode = c.depcode(+)
                                   AND a.corporationid = d.corporationid(+)
                                   AND a.areacode = e.areacode(+))
                         WHERE companyid = :companyid
                           AND depcode =:depcode";
            return DalHelper.ExecuteQuery(str, new OracleParameter(":companyid", companyid), new OracleParameter(":depcode", depcode));
        }

        /// <summary>
        /// 生效
        /// </summary>
        /// <param name="companyid"></param>
        /// <param name="depcode"></param>
        /// <returns></returns>
        public bool Enable(string companyid, string depcode, SynclogModel logmodel)
        {
            string str = @"UPDATE gds_sc_department
                           SET deleted = 'N'
                         WHERE companyid = :companyid
                           AND depcode IN (
                                  SELECT depcode
                                    FROM (SELECT a.*, b.levelname, c.depname parentdepname,
                                                 d.simplename corporationname, e.singlename areaname
                                            FROM gds_sc_department a,
                                                 gds_sc_deplevel b,
                                                 gds_sc_department c,
                                                 gds_att_corporation d,
                                                 gds_att_plantarea e
                                           WHERE a.levelcode = b.levelcode(+)
                                             AND a.parentdepcode = c.depcode(+)
                                             AND a.corporationid = d.corporationid(+)
                                             AND a.areacode = e.areacode(+))
                                   WHERE companyid = :companyid AND depcode = :depcode)";
            return DalHelper.ExecuteNonQuery(str,logmodel, new OracleParameter(":companyid", companyid), new OracleParameter(":depcode", depcode)) != -1;
        }

        /// <summary>
        /// 獲得組織及其子組織信息
        /// </summary>
        /// <param name="depcode"></param>
        /// <returns></returns>
        public DataTable GetDeptByParentDept(string depcode)
        {
            string str = "SELECT DepCode,DepName FROM gds_sc_department START WITH depcode=:depcode CONNECT BY PRIOR depcode = parentdepcode";
            return DalHelper.ExecuteQuery(str, new OracleParameter(":depcode", depcode));
        }

        /// <summary>
        /// 刪除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Delete(DepartmentModel model, SynclogModel logmodel)
        {
            return DalHelper.Delete(model,logmodel) != -1;
        }

        /// <summary>
        /// 檢查費用代碼
        /// </summary>
        /// <param name="CostCode"></param>
        /// <param name="AccountEntity"></param>
        /// <param name="DepCode"></param>
        /// <returns></returns>
        public DataTable GetCostCode(string CostCode, string AccountEntity, string DepCode)
        {
            string str = "SELECT 1 FROM gds_sc_department WHERE costcode='" + CostCode + "'";
            if (!string.IsNullOrEmpty(AccountEntity))
            {
                str += " AND accountentity='" + AccountEntity + "'";
            }
            if (!string.IsNullOrEmpty(DepCode))
            {
                str += " AND depcode<>'" + DepCode + "'";
            }
            return DalHelper.ExecuteQuery(str);
        }

        /// <summary>
        /// 查詢參數值
        /// </summary>
        /// <returns></returns>
        public DataTable GetParaValue()
        {
            string str = "select nvl(paravalue,'Y') from gds_sc_parameter where paraname='DepartmentLimitFlag'";
            return DalHelper.ExecuteQuery(str);
        }

        /// <summary>
        /// 查詢組織的排序Id
        /// </summary>
        /// <param name="depcode"></param>
        /// <returns></returns>
        public DataTable GetOrderIdByDepCode(string depcode)
        {
            string str = "SELECT b.orderid,b.row_id FROM gds_sc_department a,(select rownum-1 row_id,levelcode,orderid from (select levelcode,orderid from gds_sc_deplevel order by orderid)) b WHERE a.levelcode=b.levelcode AND a.depcode=:depcode";
            return DalHelper.ExecuteQuery(str, new OracleParameter(":depcode", depcode));
        }

        /// <summary>
        /// 查詢層級代碼的排序Id
        /// </summary>
        /// <param name="levelcode"></param>
        /// <returns></returns>
        public DataTable GetOrderIdBylevelcode(string levelcode)
        {
            string str = "SELECT orderid,row_id FROM (select rownum-1 row_id,levelcode,orderid from (select levelcode,orderid from gds_sc_deplevel order by orderid)) WHERE levelcode=:levelcode";
            return DalHelper.ExecuteQuery(str, new OracleParameter(":levelcode", levelcode));
        }

        /// <summary>
        /// 根據部門代碼和層級代碼檢驗信息是否存在
        /// </summary>
        /// <param name="depcode"></param>
        /// <param name="levelcode"></param>
        /// <returns></returns>
        public DataTable CheckDepCodeAndLevelCode(string depcode, string levelcode)
        {
            string str = @"SELECT 1
                          FROM gds_att_employees a, gds_sc_department b, gds_sc_deplevel c
                         WHERE a.depcode = b.depcode
                           AND b.levelcode = c.levelcode
                           AND a.depcode = :depcode
                           AND (SELECT orderid
                                  FROM gds_sc_deplevel
                                 WHERE levelcode = :levelcode) < '4'";
            return DalHelper.ExecuteQuery(str, new OracleParameter(":depcode", depcode), new OracleParameter(":levelcode", levelcode));
        }

        /// <summary>
        /// 查詢最大部門代碼
        /// </summary>
        /// <param name="levelcode"></param>
        /// <param name="parentcode"></param>
        /// <param name="outhead"></param>
        /// <returns></returns>
        public string GetMaxCode(string levelcode, string parentcode, out string outhead)
        {
            OracleParameter outdepcode = new OracleParameter("p_depcode", OracleType.VarChar,100);
            OracleParameter head = new OracleParameter("p_head", OracleType.VarChar,100);
            outdepcode.Direction = ParameterDirection.Output;
            head.Direction = ParameterDirection.Output;
            DalHelper.ExecuteQuery("gds_att_getmaxcode", CommandType.StoredProcedure,
                new OracleParameter("p_levelcode", levelcode), new OracleParameter("p_parentdepcode", parentcode), outdepcode, head);
            outhead = Convert.ToString(head.Value);
            return Convert.ToString(outdepcode.Value);
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddDepartment(DepartmentModel model, SynclogModel logmodel)
        {
            return DalHelper.Insert(model,logmodel) != -1;
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateDepartment(DepartmentModel model, SynclogModel logmodel)
        {
            return DalHelper.UpdateByKey(model, true,logmodel) != -1;
        }

        /// <summary>
        /// 查詢用戶權限部門資料
        /// </summary>
        /// <param name="personcode"></param>
        /// <param name="companyid"></param>
        /// <param name="modulecode"></param>
        /// <param name="depcode"></param>
        /// <returns></returns>
        public DataTable GetDepartmentByUser(string personcode,string companyid,string modulecode,string depcode)
        {
            string str = @"SELECT        LPAD ('-' || depcode, LENGTH (depcode) + LEVEL * 5)
                                   || '/'
                                   || depname AS depname1,
                                   dept.*
                              FROM (SELECT a.*, b.levelname, c.depname parentdepname,
                                           d.simplename corporationname
                                      FROM gds_sc_department a,
                                           gds_sc_deplevel b,
                                           gds_sc_department c,
                                           gds_att_corporation d,
                                           (SELECT depcode tdcode
                                              FROM gds_sc_persondept temp
                                             WHERE temp.personcode = :personcode
                                               AND temp.companyid = :companyid
                                               AND temp.modulecode = :modulecode) e
                                     WHERE a.levelcode = b.levelcode(+)
                                       AND a.parentdepcode = c.depcode(+)
                                       AND a.corporationid = d.corporationid(+)
                                       AND a.depcode = e.tdcode) dept
                             WHERE companyid = :companyid AND depcode = :depcode AND deleted = 'N'
                        START WITH levelcode = '0'
                        CONNECT BY PRIOR depcode = parentdepcode
                          ORDER SIBLINGS BY orderid";
            return DalHelper.ExecuteQuery(str, new OracleParameter(":personcode", personcode), new OracleParameter(":companyid", companyid), new OracleParameter(":modulecode", modulecode), new OracleParameter(":depcode", depcode));
        }

        /// <summary>
        /// 查詢已所要添加部門作為父組織的組織數
        /// </summary>
        /// <param name="companyid"></param>
        /// <param name="depcode"></param>
        /// <param name="parentdepcode"></param>
        /// <returns></returns>
        public DataTable GetCount(string companyid, string depcode, string parentdepcode)
        {
            string str = "  SELECT count(*) FROM(SELECT * FROM gds_sc_department START WITH (companyid=:companyid AND depcode=:depcode)CONNECT BY PRIOR depcode=parentdepcode) WHERE depcode= :parentdepcode";
            return DalHelper.ExecuteQuery(str, new OracleParameter(":companyid", companyid), new OracleParameter(":depcode", depcode), new OracleParameter(":parentdepcode", parentdepcode));
        }

        /// <summary>
        /// 查詢法人資料
        /// </summary>
        /// <returns></returns>
        public DataTable GetCorporationInfo()
        {
            string str = "SELECT * FROM gds_att_corporation WHERE deleted='N'";
            return DalHelper.ExecuteQuery(str);
        }

        /// <summary>
        /// 查詢區域資料
        /// </summary>
        /// <returns></returns>
        public DataTable GetAreaInfo()
        {
            string str = "SELECT * FROM gds_att_plantarea WHERE deleted='N'";
            return DalHelper.ExecuteQuery(str);
        }

        /// <summary>
        /// 查詢組織層級資料
        /// </summary>
        /// <param name="personcode"></param>
        /// <returns></returns>
        public DataTable GetDepLevelInfo(string personcode)
        {
            string str = "SELECT * FROM gds_sc_deplevel WHERE levelcode IN(SELECT levelcode FROM gds_sc_personlevel WHERE personcode=:personcode) AND NOT Head='-'";
            return DalHelper.ExecuteQuery(str, new OracleParameter(":personcode", personcode));
        }

        /// <summary>
        /// 查詢組織信息(用於組織圖B)
        /// </summary>
        /// <param name="depcode"></param>
        /// <returns></returns>
        public DataTable GetDepartmentInfo(string depcode)
        {
            string str = @"SELECT *
                          FROM (SELECT   depcode, depname,
                                         (SELECT NVL ((SELECT    '['
                                                              || localname
                                                              || ':'
                                                              || managername
                                                              || ']'
                                                         FROM gds_att_employee c
                                                        WHERE c.workno = b.workno),
                                                      (SELECT (SELECT    '['
                                                                      || localname
                                                                      || ':'
                                                                      || managername
                                                                      || ']'
                                                                 FROM gds_att_employee c
                                                                WHERE c.workno = b.workno)
                                                         FROM gds_att_orgmanager b
                                                        WHERE b.depcode = a.depcode
                                                          AND isdirectlyunder = 'N'
                                                          AND ROWNUM <= 1)
                                                     )
                                            FROM gds_att_orgmanager b
                                           WHERE b.depcode = a.depcode
                                             AND isdirectlyunder = 'Y'
                                             AND ROWNUM <= 1) manager,
                                         parentdepcode
                                    FROM gds_sc_department a
                                   WHERE deleted = 'N'
                                     AND depcode IN (SELECT     depcode
                                                           FROM gds_sc_department
                                                     START WITH depcode = :depcode
                                                     CONNECT BY PRIOR depcode = parentdepcode)
                                ORDER BY a.levelcode, a.orderid)";
            return DalHelper.ExecuteQuery(str, new OracleParameter(":depcode", depcode));
        }

        /// <summary>
        /// 查詢組織在職員工人數
        /// </summary>
        /// <param name="depcode"></param>
        /// <returns></returns>
        public DataTable GetDepEmpCount(string depcode)
        {
            string str = @"SELECT COUNT (1) totalcount
                          FROM gds_att_employee a
                         WHERE a.flag = 'Local'
                           AND status = '0'
                           AND a.dcode IN (SELECT     depcode
                                                 FROM gds_sc_department
                                           START WITH depcode = :depcode
                                           CONNECT BY PRIOR depcode = parentdepcode)";
            return DalHelper.ExecuteQuery(str, new OracleParameter(":depcode", depcode));
        }

        /// <summary>
        /// 查詢是否是事業群
        /// </summary>
        /// <param name="depcode"></param>
        /// <returns></returns>
        public DataTable CheckDepCode(string depcode)
        {
            string str = "select 1 from gds_sc_department where DepCode=:depcode AND levelcode ='0'";
            return DalHelper.ExecuteQuery(str, new OracleParameter(":depcode", depcode));
        }


        /// <summary>
        /// 查詢組織信息(用於組織圖A)
        /// </summary>
        /// <param name="depcode"></param>
        /// <returns></returns>
        public DataTable GetDepartmentAInfo(string depcode)
        {
            string str = @"SELECT *
                          FROM (SELECT   depcode, depname,
                                         (SELECT NVL ((SELECT    '['
                                                              || localname
                                                              || ':'
                                                              || levelname
                                                              || ':'
                                                              || managername
                                                              || ']'
                                                         FROM gds_att_employee c
                                                        WHERE c.workno = b.workno),
                                                      (SELECT (SELECT    '['
                                                                      || localname
                                                                      || ':'
                                                                      || levelname
                                                                      || ':'
                                                                      || managername
                                                                      || ']'
                                                                 FROM gds_att_employee c
                                                                WHERE c.workno = b.workno)
                                                         FROM gds_att_orgmanager b
                                                        WHERE b.depcode = a.depcode
                                                          AND isdirectlyunder = 'N'
                                                          AND ROWNUM <= 1)
                                                     )
                                            FROM gds_att_orgmanager b
                                           WHERE b.depcode = a.depcode
                                             AND isdirectlyunder = 'Y'
                                             AND ROWNUM <= 1) manager,
                                         parentdepcode
                                    FROM gds_sc_department a
                                   WHERE deleted = 'N'
                                     AND depcode IN (SELECT     depcode
                                                           FROM gds_sc_department
                                                     START WITH depcode = :depcode
                                                     CONNECT BY PRIOR depcode = parentdepcode)
                                ORDER BY a.levelcode, a.orderid)";
            return DalHelper.ExecuteQuery(str, new OracleParameter(":depcode", depcode));
        }

        /// <summary>
        /// 根據levletype查詢組織員工數
        /// </summary>
        /// <param name="depcode"></param>
        /// <returns></returns>
        public DataTable GetEmpCountByLevelType(string depcode)
        {
            string str = @"SELECT SUM (DECODE (b.leveltype, 'A', 1, 0)) counta,
                               SUM (DECODE (b.leveltype, 'B', 1, 0)) countb,
                               SUM (DECODE (b.leveltype, 'C', 1, 0)) countc, COUNT (1) totalcount
                          FROM gds_att_employee a, gds_att_level b
                         WHERE a.levelcode = b.levelcode
                           AND a.flag = 'Local'
                           AND status = '0'
                           AND a.dcode IN (SELECT     depcode
                                                 FROM gds_sc_department
                                           START WITH depcode = :depcode
                                           CONNECT BY PRIOR depcode = parentdepcode)";
            return DalHelper.ExecuteQuery(str, new OracleParameter(":depcode", depcode));
        }
    }
}
