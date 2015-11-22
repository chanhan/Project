/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： SynclogDal.cs
 * 檔功能描述： 組織層級設定數據操作類
 * 
 * 版本：1.0
 * 創建標識： 高子焱 2011.12.03
 * 
 */

using System;
using System.Data;
using System.Data.OracleClient;
using GDSBG.MiABU.Attendance.IDAL.SystemManage.SystemSafety;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;
using System.Collections;
using System.Collections.Generic;

namespace GDSBG.MiABU.Attendance.OracleDAL.SystemManage.SystemSafety
{
    public class SynclogDal : DALBase<SynclogModel>, ISynclogDal
    {
        /// <summary>
        /// 根据存在的查詢條件查詢
        /// </summary>
        /// <param name="model"></param>
        /// <returns>DataTable</returns>
        public DataTable SelectByString(SynclogModel model, string condition, Nullable<DateTime> FromDate, Nullable<DateTime> ToDate,int  pageIndex,int pageSize,out int totalCount)
        {
            string strCon = "";
            List<OracleParameter> listPara = DalHelper.CreateConditionParameters(model, true, "a", out strCon);
            string cmdText = @"SELECT docno, transactiontype, text,levelno, fromhost, tohost,logtime,processflag, processowner
                    FROM gds_sc_synclog a where nvl(logtime,to_date('1900/01/01','yyyy/mm/dd')) BETWEEN :FromDate AND :ToDate "; 
            cmdText = cmdText + strCon ;
            if (!string.IsNullOrEmpty(condition))
            {
                cmdText = cmdText + " and levelno in ("+condition+")";
            }
            listPara.Add(new OracleParameter(":FromDate", FromDate));
            listPara.Add(new OracleParameter(":ToDate", ToDate));
            DataTable dt = DalHelper.ExecutePagerQuery(cmdText, pageIndex, pageSize,out  totalCount,listPara.ToArray());
            return dt;
        }
    }
}
