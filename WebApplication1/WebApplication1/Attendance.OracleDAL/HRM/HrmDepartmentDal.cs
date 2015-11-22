/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： HrmDepartmentDal.cs
 * 檔功能描述： 組織擴建數據操作類
 * 
 * 版本：1.0
 * 創建標識： 顧陳偉 2012.01.02
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
    public class HrmDepartmentDal : DALBase<DepartmentModel>, IHrmDepartmentDal
    {
        /// <summary>
        /// 得到組織層級樹
        /// </summary>
        /// <param name="personCode">工號</param>
        /// <param name="companyId">公司ID</param>
        /// <param name="moduleCode">模組代碼</param>
        /// <returns>查詢結果DataTable</returns>
        public DataTable getOrgTree(string personCode, string companyId, string moduleCode)
        {
            string cmdText = "gds_att_hrmdepartment";
            OracleParameter dtresult = new OracleParameter("dt", OracleType.Cursor);
            dtresult.Direction = ParameterDirection.Output;
            DataTable dt = DalHelper.ExecuteQuery(cmdText, CommandType.StoredProcedure,
               new OracleParameter("v_personcode", personCode),
                new OracleParameter("v_companyid", companyId),
             new OracleParameter("v_mudulecode", moduleCode), dtresult);
            return dt;
        }
        /// <summary>
        /// 獲得層級標識
        /// </summary>
        /// <param name="orderid"></param>
        /// <returns></returns>
        public DataTable GetHead(string orderid)
        {
            string str = "SELECT Head,levelname,levelcode FROM GDS_SC_DEPLEVEL where OrderID=:orderid";
            return DalHelper.ExecuteQuery(str, new OracleParameter(":orderid", orderid));
        }

        /// <summary>
        /// 獲得組織資料
        /// </summary>
        /// <param name="companyid"></param>
        /// <param name="depcode"></param>
        /// <returns></returns>
        public DataTable GetDepartment(string companyid, string depcode)
        {
            string str = "select a.depname,a.orderid,a.levelcode,a.ParentDepCode,b.LevelName,b.Head,b.OrderID DepLevel  from gds_sc_department a,gds_sc_deplevel b where a.LevelCode=b.LevelCode and companyid=:companyid AND depcode=:depcode";
            return DalHelper.ExecuteQuery(str, new OracleParameter(":companyid", companyid), new OracleParameter(":depcode", depcode));
        }

        /// <summary>
        /// 獲得組織詳細資料
        /// </summary>
        /// <param name="conditon"></param>
        /// <param name="companyid"></param>
        /// <param name="depcode"></param>
        /// <returns></returns>
        public DataTable GetDepartmentDetailData(string conditon, string companyid, string depcode)
        {
            string cmdText = "SELECT * FROM(SELECT a.*,b.levelname,c.depname parentdepname ,d.simplename corporationname,e.singlename areaname FROM gds_sc_department a,gds_sc_deplevel b,gds_sc_department c,gds_att_corporation d,gds_att_plantarea e WHERE a.levelcode=b.levelcode(+)   AND a.parentdepcode=c.depcode(+) AND a.corporationid=d.corporationid(+) AND a.areacode=e.areacode(+)) ";
            if (string.IsNullOrEmpty(conditon))
            {
                cmdText += " where 1=2";
            }
            if (conditon == "condition")
            {
                cmdText += "WHERE companyid='" + companyid + "' AND depcode='" + depcode + "'";
            }
            return DalHelper.ExecuteQuery(cmdText);
        }

        /// <summary>
        ///獲得最大的depcode
        /// </summary>
        /// <param name="depcode"></param>
        /// <returns></returns>
        public DataTable GetMaxDepCode(string depcode)
        {
            string str = "select NVL(max(DepCode),'" + depcode + "'||'000') MaxDepCode from gds_sc_department where DepCode like '" + depcode + "%' and length(depcode)=9 ";
            return DalHelper.ExecuteQuery(str);
        }

        /// <summary>
        /// 獲得組織下員工
        /// </summary>
        /// <param name="flag"></param>
        /// <param name="depcode"></param>
        /// <returns></returns>
        public DataTable GetWorkNoByDepCode(int flag, string depcode)
        {
            string cmdText = "SELECT WorkNo FROM GDS_ATT_EMPLOYEE WHERE dcode in (SELECT DepCode FROM GDS_SC_department START WITH depcode=:depcode CONNECT BY PRIOR depcode = parentdepcode)";
            if (flag == 1)
            {
                cmdText += " and Status<'2'";
            }
            if (flag == 2)
            {
                cmdText += " and status='0'";
            }
            return DalHelper.ExecuteQuery(cmdText, new OracleParameter(":depcode", depcode));
        }

        /// <summary>
        /// 獲得組織及其子組織
        /// </summary>
        /// <param name="flag"></param>
        /// <param name="depcode"></param>
        /// <returns></returns>
        public DataTable GetDeptByParentdepcode(bool flag,string depcode)
        {
            string cmdText = "SELECT DepCode,DepName FROM gds_sc_department START WITH depcode=:depcode CONNECT BY PRIOR depcode = parentdepcode";
            if (flag == true)
            {
                cmdText += " and deleted='N'";
            }
            return DalHelper.ExecuteQuery(cmdText, new OracleParameter(":depcode", depcode));
        }

        /// <summary>
        /// 查詢組織
        /// </summary>
        /// <param name="newdepname"></param>
        /// <returns></returns>
        public DataTable GetDept(string newdepname,string sql)
        {
            string str = "SELECT DepCode FROM gds_sc_department a where LevelCode='3' and costcode >' ' and a.depcode=:newdepname";
            string condition = " and depcode in (" + sql + ")";
            str = str + condition;
            return DalHelper.ExecuteQuery(str, new OracleParameter(":newdepname", newdepname));
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateDepartment(DepartmentModel model, SynclogModel logmodel)
        {
            return DalHelper.UpdateByKey(model, true, logmodel) != -1;
        }

        /// <summary>
        /// 刪除
        /// </summary>
        /// <param name="companyid"></param>
        /// <param name="depcode"></param>
        /// <returns></returns>
        public int DeleteDepartment(string companyid, string depcode, SynclogModel logmodel)
        {
            OracleParameter Out = new OracleParameter("p_out", OracleType.Int32);
            Out.Direction = ParameterDirection.Output;
            DalHelper.ExecuteNonQuery("gds_sc_deletedepartment", CommandType.StoredProcedure,
                new OracleParameter("p_companyid", companyid), new OracleParameter("p_depcode", depcode), Out,
                new OracleParameter("p_transactiontype", logmodel.TransactionType == null ? "" : logmodel.TransactionType.ToString()),
                new OracleParameter("p_levelno", logmodel.LevelNo == null ? "" : logmodel.LevelNo.ToString()),
                new OracleParameter("p_fromhost", logmodel.FromHost == null ? "" : logmodel.FromHost.ToString()),
                new OracleParameter("p_tohost", logmodel.ToHost == null ? "" : logmodel.ToHost.ToString()),
                new OracleParameter("p_docno", logmodel.DocNo == null ? "" : logmodel.DocNo.ToString()),
                new OracleParameter("p_processflag", logmodel.ProcessFlag == null ? "" : logmodel.ProcessFlag.ToString()),
                new OracleParameter("p_processowner", logmodel.ProcessOwner == null ? "" : logmodel.ProcessOwner.ToString()));
            return Convert.ToInt32(Out.Value);
        }

        /// <summary>
        /// 失效/生效
        /// </summary>
        /// <param name="companyid"></param>
        /// <param name="depcode"></param>
        /// <param name="disableflag"></param>
        /// <returns></returns>
        public int DisableOrEnable(string companyid, string depcode, string disableflag, SynclogModel logmodel)
        {
            OracleParameter Out = new OracleParameter("p_out", OracleType.Int32);
            Out.Direction = ParameterDirection.Output;
            DalHelper.ExecuteNonQuery("gds_sc_disabledepartment", CommandType.StoredProcedure,
                new OracleParameter("p_companyid", companyid), new OracleParameter("p_depcode", depcode), new OracleParameter("p_disableflag", disableflag), Out,
                new OracleParameter("p_transactiontype", logmodel.TransactionType == null ? "" : logmodel.TransactionType.ToString()),
                new OracleParameter("p_levelno", logmodel.LevelNo == null ? "" : logmodel.LevelNo.ToString()),
                new OracleParameter("p_fromhost", logmodel.FromHost == null ? "" : logmodel.FromHost.ToString()),
                new OracleParameter("p_tohost", logmodel.ToHost == null ? "" : logmodel.ToHost.ToString()),
                new OracleParameter("p_docno", logmodel.DocNo == null ? "" : logmodel.DocNo.ToString()),
                new OracleParameter("p_processflag", logmodel.ProcessFlag == null ? "" : logmodel.ProcessFlag.ToString()),
                new OracleParameter("p_processowner", logmodel.ProcessOwner == null ? "" : logmodel.ProcessOwner.ToString()));
            return Convert.ToInt32(Out.Value);
        }

        /// <summary>
        /// 存儲
        /// </summary>
        /// <param name="processFlag"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public int SaveDepartment(string processFlag, DepartmentModel model, SynclogModel logmodel)
        {
            int num=0;
            if (processFlag == "Add")
            {
                num=DalHelper.Insert(model,logmodel);
            }
            if (processFlag == "Modify")
            {
                num = DalHelper.UpdateByKey(model,true,logmodel);
            }
            return num;
        }

        /// <summary>
        /// 組織異動
        /// </summary>
        /// <param name="newdepcode"></param>
        /// <param name="depcode"></param>
        /// <returns></returns>
        public int ChangeSave(string newdepcode, string depcode, SynclogModel logmodel)
        {
            string str = "update gds_sc_department set parentdepcode=:newdepcode where depcode =:depcode";
            return DalHelper.ExecuteNonQuery(str,logmodel, new OracleParameter(":newdepcode", newdepcode), new OracleParameter(":depcode", depcode));
        }
    }
}
