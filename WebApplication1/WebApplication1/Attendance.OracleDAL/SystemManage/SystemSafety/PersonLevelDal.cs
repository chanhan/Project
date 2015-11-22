/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： PersonLevelDal.cs
 * 檔功能描述： 組織層級設定數據操作類
 * 
 * 版本：1.0
 * 創建標識： 高子焱 2011.12.01
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
    public class PersonLevelDal : DALBase<PersonLevelModel>, IPersonLevelDal
    {

        /// <summary>
        /// 根据工號查询員工已擁有的組織層級的DataTable
        /// </summary>
        /// <param name="model"></param>
        /// <returns>DataTable</returns>
        public DataTable GetDeptDataTableByKey(string personCode)
        {
            OracleParameter outPara = new OracleParameter("dt", OracleType.Cursor);
            outPara.Direction = ParameterDirection.Output;
            DataTable dt = DalHelper.ExecuteQuery("gds_sc_pk.checkedlevel", CommandType.StoredProcedure
                , new OracleParameter("v_personcode", personCode), outPara);
            return dt;
        }
        /// <summary>
        /// 根据工號查询員工未選中的組織層級的DataTable
        /// </summary>
        /// <param name="model"></param>
        /// <returns>DataTable</returns>
        public DataTable GetDeptUncheckedByKey(string personCode)
        {
            OracleParameter outPara = new OracleParameter("dt", OracleType.Cursor);
            outPara.Direction = ParameterDirection.Output;
            DataTable dt = DalHelper.ExecuteQuery("gds_sc_pk.uncheckedlevel", CommandType.StoredProcedure
                , new OracleParameter("v_personcode", personCode), outPara);
            return dt;
        }

        /// <summary>
        /// 根據主鍵修改功能
        /// </summary>
        /// <param name="model">要修改的功能Model</param>
        /// <returns>是否成功</returns>
        public int UpdateDeptByKey(string personCode, Hashtable levels, SynclogModel logmodel)
        {
            int i = 0;
            int a;
            DalHelper.ExecuteNonQuery("DELETE FROM gds_sc_personlevel WHERE personcode ='" + personCode + "'", logmodel);
            for (int j = 1; j <= levels.Count; j++)
            {
                string levelCode = levels[j].ToString();
                OracleParameter outPara = new OracleParameter("v_out", OracleType.Int32);
                outPara.Direction = ParameterDirection.Output;
                a = DalHelper.ExecuteNonQuery("gds_sc_pk.updatechecklevel", CommandType.StoredProcedure
                    , new OracleParameter("v_personcode", personCode), new OracleParameter("v_levelcode", levelCode), outPara, 
                    new OracleParameter("p_transactiontype", logmodel.TransactionType), new OracleParameter("p_levelno", logmodel.LevelNo),
                    new OracleParameter("p_fromhost", logmodel.FromHost),new OracleParameter("p_tohost", logmodel.ToHost == null ? "" : logmodel.ToHost.ToString()), 
                    new OracleParameter("p_docno", logmodel.DocNo == null ? "" : logmodel.DocNo.ToString()), new OracleParameter("p_processflag", logmodel.ProcessFlag),
                    new OracleParameter("p_processowner", logmodel.ProcessOwner));
                i = Convert.ToInt32(outPara.Value);
            }
            return i;
        }
    }
}
