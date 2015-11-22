﻿/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： OTMActivityApplyDal.cs
 * 檔功能描述： 免卡人員加班導入數據操作類
 * 
 * 版本：1.0
 * 創建標識： 張明強 2011.12.23
 * 
 */

using System;
using System.Data;
using System.Data.OracleClient;
using GDSBG.MiABU.Attendance.IDAL.KQM.OTM;
using GDSBG.MiABU.Attendance.Model.KQM.OTM;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;
using System.Collections;
using System.Collections.Generic;

namespace GDSBG.MiABU.Attendance.OracleDAL.KQM.OTM
{
    public class OTMActivityApplyTempDal : DALBase<ActivityTempModel>, IOTMActivityApplyTempDal
    {
        /// <summary>
        /// 導入信息,正確信息插入正式表,錯誤信息返回datatable
        /// </summary>
        /// <param name="createUser">創建人</param>
        /// <returns>返回的datatable</returns>
        public DataTable GetTempTableErrorData(string createUser, out int successNum, out int errorNum, SynclogModel logmodel)
        {
            OracleParameter outCursor = new OracleParameter("p_coursor", OracleType.Cursor);
            OracleParameter outSuccess = new OracleParameter("p_success", OracleType.Int32);
            OracleParameter outError = new OracleParameter("p_error", OracleType.Int32);
            outCursor.Direction = ParameterDirection.Output;
            outSuccess.Direction = ParameterDirection.Output;
            outError.Direction = ParameterDirection.Output;
            DataTable TempErrorTable = DalHelper.ExecuteQuery("gds_att_activity_temp_check", CommandType.StoredProcedure,
                new OracleParameter("p_personcode", createUser), outCursor, outSuccess, outError,
                new OracleParameter("p_transactiontype", logmodel.TransactionType == null ? "" : logmodel.TransactionType.ToString()),
                new OracleParameter("p_levelno", logmodel.LevelNo == null ? "" : logmodel.LevelNo.ToString()),
                new OracleParameter("p_fromhost", logmodel.FromHost == null ? "" : logmodel.FromHost.ToString()),
                new OracleParameter("p_tohost", logmodel.ToHost == null ? "" : logmodel.ToHost.ToString()),
                new OracleParameter("p_docno", logmodel.DocNo == null ? "" : logmodel.DocNo.ToString()),
                new OracleParameter("p_processflag", logmodel.ProcessFlag == null ? "" : logmodel.ProcessFlag.ToString()),
                new OracleParameter("p_processowner", logmodel.ProcessOwner == null ? "" : logmodel.ProcessOwner.ToString())
                );
            successNum = Convert.ToInt32(outSuccess.Value);
            errorNum = Convert.ToInt32(outError.Value);
            return TempErrorTable;
        }


        /// <summary>
        /// 將datatable轉換為List
        /// </summary>
        /// <param name="dt">徐轉化的Datatable</param>
        /// <returns></returns>
        public List<ActivityTempModel> GetList(DataTable dt)
        {
            return OrmHelper.SetDataTableToList(dt);
        }
    }
}
