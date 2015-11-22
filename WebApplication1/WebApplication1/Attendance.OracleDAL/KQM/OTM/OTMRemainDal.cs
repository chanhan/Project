/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： OTMRemainDal.cs
 * 檔功能描述： 剩余加班導入操作類
 * 
 * 版本：1.0
 * 創建標識： 顧陳偉 2011.12.23
 * 
 */
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using GDSBG.MiABU.Attendance.IDAL.KQM.OTM;
using GDSBG.MiABU.Attendance.Model.KQM.OTM;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;

namespace GDSBG.MiABU.Attendance.OracleDAL.KQM.OTM
{
    public class OTMRemainDal : DALBase<OTMRemainModel>, IOTMRemainDal
    {
        /// <summary>
        /// 獲得所有剩餘加班資料
        /// </summary>
        /// <param name="model"></param>
        /// <param name="sql"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public DataTable GetAllRemainInfo(OTMRemainModel model,string sql, int pageIndex, int pageSize, out int totalCount)
        {
            //return DalHelper.Select(model, true, pageIndex, pageSize, out totalCount);
            string strCon = "";
            string depName = "";
            if (!string.IsNullOrEmpty(model.DepName))
            {
                depName = model.DepName.ToString();
            }
            model.DepName = "";
            string condition = "";
            List<OracleParameter> listPara = DalHelper.CreateConditionParameters(model, true, "a", out strCon);
            string cmdText = @"select a.workno, a.yearmonth, a.g1remain, a.g23remain, a.update_user,
                              a.update_date, a.create_date, a.create_user, a.iscurrent,
                              a.remark, a.localname, a.depname, a.overtimetype from gds_att_remain_v a where 1=1 ";
            if (!string.IsNullOrEmpty(depName))
            {
                condition = "AND a.depcode IN ((" + sql + ") INTERSECT SELECT DepCode FROM gds_sc_department START WITH depname = :depName CONNECT BY PRIOR depcode = parentdepcode) ";
                listPara.Add(new OracleParameter(":depName", depName));
            }
            else
            {
                condition = " AND a.depcode IN (" + sql + ") ";
            }
            cmdText = cmdText + strCon + condition;
            return DalHelper.ExecutePagerQuery(cmdText, pageIndex, pageSize, out totalCount, listPara.ToArray());
        }

        /// <summary>
        /// 導出查詢
        /// </summary>
        /// <param name="model"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        public DataTable GetAllRemainForExport(OTMRemainModel model, string sql)
        {
            string strCon = "";
            string depName = model.DepName.ToString();
            model.DepName = "";
            string condition = "";
            List<OracleParameter> listPara = DalHelper.CreateConditionParameters(model, true, "a", out strCon);
            string cmdText = @"select a.workno, a.yearmonth, a.g1remain, a.g23remain, a.update_user,
                              a.update_date, a.create_date, a.create_user, a.iscurrent,
                              a.remark, a.localname, a.depname, a.overtimetype from gds_att_remain_v a where 1=1 ";
            if (!string.IsNullOrEmpty(depName))
            {
                condition = "AND a.depcode IN ((" + sql + ") INTERSECT SELECT DepCode FROM gds_sc_department START WITH depname = :depName CONNECT BY PRIOR depcode = parentdepcode) ";
                listPara.Add(new OracleParameter(":depName", depName));
            }
            else
            {
                condition = " AND a.depcode IN (" + sql + ") ";
            }
            cmdText = cmdText + strCon + condition;
            return DalHelper.ExecuteQuery(cmdText, listPara.ToArray());
        }

        /// <summary>
        /// 將datatable轉換為List
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public List<OTMRemainModel> GetList(DataTable dt)
        {
            return OrmHelper.SetDataTableToList(dt);
        }

        /// <summary>
        /// 刪除
        /// </summary>
        /// <param name="workno">工號</param>
        /// <param name="yearmonth">年月</param>
        /// <returns></returns>
        public int DeleteRemain(string workno, string yearmonth, SynclogModel logmodel)
        {
            string str = " DELETE FROM gds_att_remain WHERE WorkNo=:workno and YearMonth =:yearmonth";
            return DalHelper.ExecuteNonQuery(str,logmodel, new OracleParameter(":workno", workno), new OracleParameter(":yearmonth", yearmonth));
        }

        /// 導入
        /// </summary>
        /// <param name="personcode"></param>
        /// <param name="moduleCode"></param>
        /// <param name="successnum"></param>
        /// <param name="errornum"></param>
        /// <returns></returns>
        public DataTable ImportExcel(string personcode, string moduleCode, out int successnum, out int errornum, SynclogModel logmodel)
        {
            OracleParameter outCursor = new OracleParameter("p_coursor", OracleType.Cursor);
            OracleParameter outSuccess = new OracleParameter("p_success", OracleType.Int32);
            OracleParameter outError = new OracleParameter("p_error", OracleType.Int32);
            outCursor.Direction = ParameterDirection.Output;
            outSuccess.Direction = ParameterDirection.Output;
            outError.Direction = ParameterDirection.Output;
            DataTable dt = DalHelper.ExecuteQuery("gds_sc_remain_vaildata", CommandType.StoredProcedure,
                new OracleParameter("p_personcode", personcode), new OracleParameter("p_modulecode", moduleCode), outCursor, outSuccess, outError,
                new OracleParameter("p_transactiontype", logmodel.TransactionType == null ? "" : logmodel.TransactionType.ToString()),
                new OracleParameter("p_levelno", logmodel.LevelNo == null ? "" : logmodel.LevelNo.ToString()),
                new OracleParameter("p_fromhost", logmodel.FromHost == null ? "" : logmodel.FromHost.ToString()),
                new OracleParameter("p_tohost", logmodel.ToHost == null ? "" : logmodel.ToHost.ToString()),
                new OracleParameter("p_docno", logmodel.DocNo == null ? "" : logmodel.DocNo.ToString()),
                new OracleParameter("p_processflag", logmodel.ProcessFlag == null ? "" : logmodel.ProcessFlag.ToString()),
                new OracleParameter("p_processowner", logmodel.ProcessOwner == null ? "" : logmodel.ProcessOwner.ToString()));
            successnum = Convert.ToInt32(outSuccess.Value);
            errornum = Convert.ToInt32(outError.Value);

            return dt;
        }

        /// <summary>
        /// 獲得登錄用戶模組權限
        /// </summary>
        /// <param name="modulecode"></param>
        /// <returns></returns>
        public DataTable GetModuleInfo(string modulecode)
        {
            // SELECT depcode FROM temp_depcode where personcode='" + this.appUser + "' and modulecode='" + this.moduleCode + "' and companyid='" + this.companyID + "'"
            string str = "SELECT * FROM gds_sc_module WHERE Privileged='N' AND modulecode=:modulecode' ORDER BY orderid";
            return DalHelper.ExecuteQuery(str, new OracleParameter(":modulecode", modulecode));

        }
    }
}
