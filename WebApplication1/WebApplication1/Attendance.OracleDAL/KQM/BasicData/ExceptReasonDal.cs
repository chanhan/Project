/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： ExceptReasonDal.cs
 * 檔功能描述： 異常原因設定數據操作類
 * 
 * 版本：1.0
 * 創建標識： 高子焱 2011.12.15
 * 
 */

using System;
using System.Data;
using System.Data.OracleClient;
using GDSBG.MiABU.Attendance.IDAL.KQM.BasicData;
using GDSBG.MiABU.Attendance.Model.KQM.BasicData;
using System.Collections;
using System.Collections.Generic;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;

namespace GDSBG.MiABU.Attendance.OracleDAL.KQM.BasicData
{
    public class ExceptReasonDal : DALBase<ExceptReasonModel>, IExceptReasonDal
    {
        /// <summary>
        /// 根據主鍵查詢Model
        /// <param name="model">班別定義model</param>
        /// <param name="orderType">排序</param>
        /// <param name="pageIndex">當前頁</param>
        /// <param name="pageSize"></param></param>
        /// <param name="totalCount">總頁數</param>
        /// <returns></returns>
        public DataTable GetExceptList(ExceptReasonModel model, string orderType, int pageIndex, int pageSize, out int totalCount)
        {
            string strCon = "";
            List<OracleParameter> listPara = DalHelper.CreateConditionParameters(model, true, "a", out strCon);
            string cmdText = @"SELECT reasonno, reasonname, update_user, update_date, effectflag, reasontype,
                               salaryflag FROM gds_att_exceptreason a where 1=1 ";
            cmdText = cmdText + strCon;
            if (orderType == "1")
            {
                cmdText = cmdText + "  ORDER BY effectflag desc ,REASONNO";
            }
            else
            {
                cmdText = cmdText + "  order by update_date desc";
            }
            DataTable dt = DalHelper.ExecutePagerQuery(cmdText, pageIndex, pageSize, out totalCount, listPara.ToArray());
            return dt;
        }
        /// <summary>
        /// 刪除一條異常原因記錄
        /// </summary>
        /// <param name="functionId">要刪除的異常原因的model</param>
        /// <returns>是否執行成功</returns>
        public int DeleteExceptByKey(ExceptReasonModel model, SynclogModel logmodel)
        {
            return DalHelper.Delete(model, logmodel);
        }
        /// <summary>
        /// 根據主鍵修改功能
        /// </summary>
        /// <param name="model">要修改的功能Model</param>
        /// <returns>是否成功</returns>
        public bool UpdateExceptByKey(ExceptReasonModel model, SynclogModel logmodel)
        {

            return DalHelper.UpdateByKey(model, true, logmodel) != -1;
        }
        /// <summary>
        /// 根據主鍵查詢Model
        /// <param name="model">異常原因model</param>
        /// <returns></returns>
        public DataTable GetExceptByKey(ExceptReasonModel model)
        {
            return DalHelper.Select(model);
        }
        /// <summary>
        /// 插入一條異常原因記錄
        /// </summary>
        /// <param name="functionId">要插入的異常原因的model</param>
        /// <returns>插入是否成功</returns>
        public int InsertExceptByKey(ExceptReasonModel model, SynclogModel logmodel)
        {
            return DalHelper.Insert(model,logmodel);
        }
        ///// <summary>
        ///// 更新一條異常原因記錄
        ///// </summary>
        ///// <param name="functionId">要更新的異常原因的model</param>
        ///// <returns>更新是否成功</returns>
        //public int UpdateExceptByKey(ExceptReasonModel model)
        //{
        //    return DalHelper.UpdateByKey(model,true);
        //}
    }
}
