/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： RelationSelectorDal.cs
 * 檔功能描述： 固定參數表數據操作類
 * 
 * 版本：1.0
 * 創建標識： 高子焱 2011.12.10
 * 
 */

using System;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using GDSBG.MiABU.Attendance.IDAL.KQM.BasicData;
using GDSBG.MiABU.Attendance.Model.KQM.BasicData;
using System.Collections;
namespace GDSBG.MiABU.Attendance.OracleDAL.KQM.BasicData
{
    public class RelationSelectorDal : DALBase<TypeDataModel>, IRelationSelectorDal
    {
        /// <summary>
        /// 查詢用戶的權限組織
        /// </summary>
        /// <param name="personCode">用戶工號</param>
        /// <param name="companyId">公司Id</param>
        /// <param name="moduleCode">模組代碼</param>
        /// <returns>返回該用戶的權限組織</returns>
        public DataTable GetTypeDataList(string personCode, string companyId, string moduleCode)
        {
            OracleParameter outPara = new OracleParameter("dt", OracleType.Cursor);
            outPara.Direction = ParameterDirection.Output;
            DataTable dt = DalHelper.ExecuteQuery("gds_sc_pk.relationselector", CommandType.StoredProcedure
                          , new OracleParameter("v_personcode", personCode), new OracleParameter("v_companyid", companyId),
                           new OracleParameter("v_mudulecode", moduleCode), outPara);
            return dt;
        }
        /// <summary>
        /// 查詢用戶的權限組織
        /// </summary>
        /// <param name="personCode">用戶工號</param>
        /// <param name="companyId">公司Id</param>
        /// <param name="moduleCode">模組代碼</param>
        /// <returns>返回該用戶的權限組織</returns>
        public DataTable GetTypeDataList(string personCode, string companyId, string moduleCode, string delete)
        {
            OracleParameter outPara = new OracleParameter("dt", OracleType.Cursor);
            outPara.Direction = ParameterDirection.Output;
            DataTable dt = DalHelper.ExecuteQuery("gds_sc_pk.relationselectorchecked", CommandType.StoredProcedure
                          , new OracleParameter("v_personcode", personCode), new OracleParameter("v_companyid", companyId),
                           new OracleParameter("v_mudulecode", moduleCode), new OracleParameter("v_delete", delete), outPara);
            return dt;
        }
    }
}
