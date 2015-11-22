/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： PowerChangeDal.cs
 * 檔功能描述： 權限交接數據操作類
 * 
 * 版本：1.0
 * 創建標識： 高子焱 2011.12.08
 * 
 */

using System;
using System.Data;
using System.Data.OracleClient;
using GDSBG.MiABU.Attendance.IDAL.SystemManage.SystemSafety;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;
using System.Collections;

namespace GDSBG.MiABU.Attendance.OracleDAL.SystemManage.SystemSafety
{
    public class PowerChangeDal : DALBase<PersonModel>, IPowerChangeDal
    {
        /// <summary>
        /// 根据工號查询員工已擁有的組織層級的DataTable
        /// </summary>
        /// <param name="model"></param>
        /// <returns>DataTable</returns>
        public DataTable GetPowerTableByKey(string personCode)
        {
            OracleParameter outPara = new OracleParameter("dt", OracleType.Cursor);
            outPara.Direction = ParameterDirection.Output;
            DataTable dt = DalHelper.ExecuteQuery("gds_sc_pk.powerchange", CommandType.StoredProcedure
                , new OracleParameter("v_personcode", personCode), outPara);
            return dt;
        }
        /// <summary>
        /// 根據傳入參數更新表信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int SaveData(string userNow, string fromPersoncode, string toPersonCode, string activeFlag, SynclogModel logmodel)
        {
            OracleParameter outPara = new OracleParameter("v_out", OracleType.Int32);
            outPara.Direction = ParameterDirection.Output;
            int i = DalHelper.ExecuteNonQuery("gds_sc_pk.powerchangeSave", CommandType.StoredProcedure
                , new OracleParameter("v_usernow", userNow), new OracleParameter("v_frompersoncode", fromPersoncode),
                new OracleParameter("v_topersoncode", toPersonCode), new OracleParameter("v_activeflag", activeFlag), outPara, new OracleParameter("p_transactiontype", logmodel.TransactionType), new OracleParameter("p_levelno", logmodel.LevelNo),
                    new OracleParameter("p_fromhost", logmodel.FromHost), new OracleParameter("p_tohost", logmodel.ToHost == null ? "" : logmodel.ToHost.ToString()),
                    new OracleParameter("p_docno", logmodel.DocNo == null ? "" : logmodel.DocNo.ToString()), new OracleParameter("p_processflag", logmodel.ProcessFlag),
                    new OracleParameter("p_processowner", logmodel.ProcessOwner));
            return i = Convert.ToInt32(outPara.Value);
        }
        /// <summary>
        /// 根據傳入參數更新表信息；交接功能
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int ChangData(string fromPersoncode, string toPersonCode, string activeFlag, SynclogModel logmodel)
        {
            OracleParameter outPara = new OracleParameter("v_out", OracleType.Int32);
            outPara.Direction = ParameterDirection.Output;
            int i = DalHelper.ExecuteNonQuery("gds_sc_pk.powerchangetranf", CommandType.StoredProcedure
                ,new OracleParameter("v_frompersoncode", fromPersoncode),new OracleParameter("v_topersoncode", toPersonCode),
                new OracleParameter("v_activeflag", activeFlag), outPara,new OracleParameter("p_transactiontype", logmodel.TransactionType), new OracleParameter("p_levelno", logmodel.LevelNo),
                    new OracleParameter("p_fromhost", logmodel.FromHost), new OracleParameter("p_tohost", logmodel.ToHost == null ? "" : logmodel.ToHost.ToString()),
                    new OracleParameter("p_docno", logmodel.DocNo == null ? "" : logmodel.DocNo.ToString()), new OracleParameter("p_processflag", logmodel.ProcessFlag),
                    new OracleParameter("p_processowner", logmodel.ProcessOwner));
            return i = Convert.ToInt32(outPara.Value);
        }
    }
}
