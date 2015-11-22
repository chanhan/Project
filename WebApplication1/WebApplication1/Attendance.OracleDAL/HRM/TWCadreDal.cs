/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： TWCadreDal.cs
 * 檔功能描述： 駐派幹部資料數據操作類
 * 
 * 版本：1.0
 * 創建標識： 顧陳偉 2011.12.16
 * 
 */

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using GDSBG.MiABU.Attendance.IDAL.HRM;
using GDSBG.MiABU.Attendance.Model.HRM;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;

namespace GDSBG.MiABU.Attendance.OracleDAL.HRM
{
    public class TWCadreDal : DALBase<TWCadreModel>, ITWCadreDal
    {
        /// <summary>
        /// 獲得所有資位
        /// </summary>
        /// <returns></returns>
        public DataTable GetLevel()
        {
            string str = "SELECT a.levelcode, a.levelname, a.remark, a.leveltype, a.effectflag,a.modifier, a.modifydate,(SELECT datavalue FROM gds_att_typedata b WHERE b.datatype = 'LevelType' AND b.datacode = a.leveltype) AS leveltypename FROM gds_att_level a WHERE EffectFlag='Y' ORDER BY OrderNo";
            return DalHelper.ExecuteQuery(str);
        }

        /// <summary>
        /// 獲得所有管理職
        /// </summary>
        /// <returns></returns>
        public DataTable GetManager()
        {
            string str = "SELECT * FROM GDS_ATT_manager WHERE EffectFlag='Y' ORDER BY OrderNo";
            return DalHelper.ExecuteQuery(str);
        }

        /// <summary>
        /// 獲得所有在職狀態
        /// </summary>
        /// <returns></returns>
        public DataTable GetEmpStatus()
        {
            string str = "select datacode statuscode,datavalue statusname from gds_att_typedata where datatype = 'EmpState' order by orderid";
            return DalHelper.ExecuteQuery(str);
        }

        /// <summary>
        /// 獲得性別資料
        /// </summary>
        /// <returns></returns>
        public DataTable GetSex()
        {
            string str = "select datacode sexcode,datavalue sexname from gds_att_typedata where datatype='Sex' order by orderid desc";
            return DalHelper.ExecuteQuery(str);
        }
        /// <summary>
        /// 根據條件分頁查詢數據
        /// </summary>
        /// <param name="model"></param>
        /// <param name="sql"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public DataTable GetTWCadrePageInfo(TWCadreModel model, string sql, string LevelCondition, string ManagerCondition, string StatusCondition, Nullable<DateTime> JoinDateFrom, Nullable<DateTime> JoinDateTo, Nullable<DateTime> LeaveDateFrom, Nullable<DateTime> LeaveDateTo, int pageIndex, int pageSize, out int totalCount)
        {
            string strCon = "";
            string depName = "";
            if (!string.IsNullOrEmpty(model.DepName))
            {
                depName = model.DepName.ToString();
            }
            model.DepName = "";
            model.DepCode = "";
            string condition = "";
            List<OracleParameter> listPara = DalHelper.CreateConditionParameters(model, true, "a", out strCon);
            string cmdText = @"select a.workno,a.localname,a.sex,a.identityno,a.byname,a.levelcode,a.managercode,a.depcode,a.extension,
                               a.notes,a.joindate,a.leavedate,a.status,a.passwd,a.teamcode,a.iskaoqin,a.cardno,a.flag,a.areacode,a.update_user,
                                a.update_date,a.create_date,a.create_user,a.levelname,a.managername,a.sexcode,a.sexname,a.depname,a.statuscode,a.statusname 
                                from gds_att_twcadre_v a where 1=1 ";
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
            if (JoinDateFrom != DateTime.MinValue)
            {
                cmdText = cmdText + " and a.joindate>= :JoinDateFrom";
                listPara.Add(new OracleParameter(":JoinDateFrom", JoinDateFrom));
            }
            if (JoinDateTo != DateTime.MaxValue)
            {
                cmdText = cmdText + " and a.joindate<= :JoinDateTo";
                listPara.Add(new OracleParameter(":JoinDateTo", JoinDateTo));
            }
            if (LeaveDateFrom != DateTime.MinValue)
            {
                cmdText = cmdText + " and a.joindate>= :LeaveDateFrom";
                listPara.Add(new OracleParameter(":LeaveDateFrom", LeaveDateFrom));
            }
            if (LeaveDateTo != DateTime.MaxValue)
            {
                cmdText = cmdText + " and a.joindate<= :LeaveDateTo";
                listPara.Add(new OracleParameter(":LeaveDateTo", LeaveDateTo));
            }
            if (!string.IsNullOrEmpty(LevelCondition))
            {
                cmdText = cmdText + " and a.levelcode in (" + LevelCondition + ")";
            }
            if (!string.IsNullOrEmpty(ManagerCondition))
            {
                cmdText = cmdText + " and a.managercode in (" + ManagerCondition + ")";
            }
            if (!string.IsNullOrEmpty(StatusCondition))
            {
                cmdText = cmdText + " and a.status in (" + StatusCondition + ")";
            }
            return DalHelper.ExecutePagerQuery(cmdText, pageIndex, pageSize, out totalCount, listPara.ToArray());
        }

        /// <summary>
        /// 導出查詢
        /// </summary>
        /// <param name="model"></param>
        /// <param name="sql"></param>
        /// <param name="LevelCondition"></param>
        /// <param name="ManagerCondition"></param>
        /// <param name="StatusCondition"></param>
        /// <param name="JoinDateFrom"></param>
        /// <param name="JoinDateTo"></param>
        /// <param name="LeaveDateFrom"></param>
        /// <param name="LeaveDateTo"></param>
        /// <returns></returns>
        public DataTable GetTWCadreForExport(TWCadreModel model, string sql, string LevelCondition, string ManagerCondition, string StatusCondition, Nullable<DateTime> JoinDateFrom, Nullable<DateTime> JoinDateTo, Nullable<DateTime> LeaveDateFrom, Nullable<DateTime> LeaveDateTo)
        {
            string strCon = "";
            string depName = "";
            if (!string.IsNullOrEmpty(model.DepName))
            {
                depName = model.DepName.ToString();
            }
            model.DepName = "";
            model.DepCode = "";
            string condition = "";
            List<OracleParameter> listPara = DalHelper.CreateConditionParameters(model, true, "a", out strCon);
            string cmdText = @"select a.workno,a.localname,a.sex,a.identityno,a.byname,a.levelcode,a.managercode,a.depcode,a.extension,
                               a.notes,a.joindate,a.leavedate,a.status,a.passwd,a.teamcode,a.iskaoqin,a.cardno,a.flag,a.areacode,a.update_user,
                                a.update_date,a.create_date,a.create_user,a.levelname,a.managername,a.sexcode,a.sexname,a.depname,a.statuscode,a.statusname 
                                from gds_att_twcadre_v a where 1=1 ";
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
            if (JoinDateFrom != DateTime.MinValue)
            {
                cmdText = cmdText + " and a.joindate>= :JoinDateFrom";
                listPara.Add(new OracleParameter(":JoinDateFrom", JoinDateFrom));
            }
            if (JoinDateTo != DateTime.MaxValue)
            {
                cmdText = cmdText + " and a.joindate<= :JoinDateTo";
                listPara.Add(new OracleParameter(":JoinDateTo", JoinDateTo));
            }
            if (LeaveDateFrom != DateTime.MinValue)
            {
                cmdText = cmdText + " and a.joindate>= :LeaveDateFrom";
                listPara.Add(new OracleParameter(":LeaveDateFrom", LeaveDateFrom));
            }
            if (LeaveDateTo != DateTime.MaxValue)
            {
                cmdText = cmdText + " and a.joindate<= :LeaveDateTo";
                listPara.Add(new OracleParameter(":LeaveDateTo", LeaveDateTo));
            }
            if (!string.IsNullOrEmpty(LevelCondition))
            {
                cmdText = cmdText + " and a.levelcode in (" + LevelCondition + ")";
            }
            if (!string.IsNullOrEmpty(ManagerCondition))
            {
                cmdText = cmdText + " and a.managercode in (" + ManagerCondition + ")";
            }
            if (!string.IsNullOrEmpty(StatusCondition))
            {
                cmdText = cmdText + " and a.status in (" + StatusCondition + ")";
            }
            return DalHelper.ExecuteQuery(cmdText, listPara.ToArray());
        }

        /// <summary>
        /// 將datatable轉換為List
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public List<TWCadreModel> GetList(DataTable dt)
        {
            return OrmHelper.SetDataTableToList(dt);
        }

        /// <summary>
        /// 新增功能
        /// </summary>
        /// <param name="model">要新增的功能Model</param>
        /// <returns>是否成功</returns>
        public bool AddTWCdare(TWCadreModel model, SynclogModel logmodel)
        {
            return DalHelper.Insert(model,logmodel) != -1;
        }

        /// <summary>
        /// 根據主鍵修改功能
        /// </summary>
        /// <param name="model">要修改的功能Model</param>
        /// <returns>是否成功</returns>
        public bool UpdateTWCdareByKey(TWCadreModel model, SynclogModel logmodel)
        {
            return DalHelper.UpdateByKey(model,true,logmodel) != -1;
        }

        /// <summary>
        /// 刪除
        /// </summary>
        /// <param name="workno">工號</param>
        /// <returns></returns>
        public int DeleteTWCadre(string workno, SynclogModel logmodel)
        {
            string str = " DELETE FROM gds_att_twcadre WHERE WorkNo=:workno";
            return DalHelper.ExecuteNonQuery(str,logmodel, new OracleParameter(":workno", workno));
        }

        /// <summary>
        /// 查詢資料是否存在
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public DataTable GetTWCafreByKey(TWCadreModel model)
        {
            string str = "SELECT workno FROM gds_att_employee where workno=:workno";
            return DalHelper.ExecuteQuery(str, new OracleParameter(":workno", model.WorkNo));
        }

        /// <summary>
        /// 獲得派駐幹部資料
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public TWCadreModel GetTWCadreInfoByKey(TWCadreModel model)
        {
            return DalHelper.SelectByKey(model);
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
            DataTable dt = DalHelper.ExecuteQuery("gds_sc_twcadre_vaildata", CommandType.StoredProcedure,
                new OracleParameter("p_personcode", personcode), outCursor, outSuccess, outError,
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
    }
}
