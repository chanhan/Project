/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： RealapplyBll.cs
 * 檔功能描述： 有效加班業務邏輯類
 * 
 * 版本：1.0
 * 創建標識： 昝望 2011.12.23
 * 
 */

using System;
using System.Collections.Generic;
using System.Text;
using GDSBG.MiABU.Attendance.IDAL.KQM.OTM;
using GDSBG.MiABU.Attendance.Model.KQM.OTM;
using System.Data;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;

namespace GDSBG.MiABU.Attendance.BLL.KQM.OTM
{
    public class RealapplyBll : BLLBase<IRealapplyDal>
    {
        /// <summary>
        /// 查詢有效加班
        /// </summary>
        /// <param name="model">給出主鍵值的Model</param>
        /// <returns>返回對應主鍵的Model</returns>
        public DataTable GetRealapply(RealapplyModel model, int pageIndex, int pageSize, out int totalCount, string symbol, string Hours, string OTDateFrom, string OTDateTo, string BatchEmployeeNo, string sqlDep)
        {
            return DAL.GetRealapply(model, pageIndex, pageSize, out totalCount, symbol, Hours, OTDateFrom, OTDateTo, BatchEmployeeNo,sqlDep);
        }

        /// <summary>
        /// 將datatable轉換為List
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public List<RealapplyModel> GetList(DataTable dt)
        {
            return DAL.GetList(dt); ;
        }


        /// <summary>
        /// 查詢加班類型
        /// </summary>
        /// <returns></returns>
        public DataTable GetOverTimeType()
        {
            return DAL.GetOverTimeType();
        }


        /// <summary>
        /// 查詢核准狀態
        /// </summary>
        /// <returns></returns>
        public DataTable GetOTMAdvanceApplyStatus()
        {
            return DAL.GetOTMAdvanceApplyStatus();
        }

        /// <summary>
        /// 刪除
        /// </summary>
        /// <param name="functionId">/param>
        /// <returns></returns>
        public int DeleteRealapplyByKey(string ID, SynclogModel logmodel)
        {
            return DAL.DeleteRealapplyByKey(ID,logmodel);
        }

        /// <summary>
        /// 取消簽核
        /// </summary>
        /// <param name="model"></param>
        /// <returns>是否成功</returns>
        public int UpdateRealapplyByKey(RealapplyModel model, SynclogModel logmodel)
        {
            return DAL.UpdateRealapplyByKey(model,logmodel);
        }


        /// <summary>
        /// 查詢未轉入有效的預報加班
        /// </summary>
        /// <param name="decode"></param>
        /// <param name="isproject"></param>
        /// <param name="employeeno"></param>
        /// <param name="name"></param>
        /// <param name="otdatefrom"></param>
        /// <param name="otdateto"></param>
        /// <returns></returns>
        public DataTable SelectAdvanceapply(string decode, string isproject, string employeeno, string name, string otdatefrom, string otdateto, string sqlDep)
        {
            return DAL.SelectAdvanceapply(decode, isproject, employeeno, name, otdatefrom, otdateto, sqlDep);
        }

        /// <summary>
        /// 轉入有效加班
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="updateuser"></param>
        /// <returns></returns>
        public int UpdateAdvanceapply(string ID, string updateuser, SynclogModel logmodel)
        {
            return DAL.UpdateAdvanceapply(ID, updateuser,logmodel);
        }
    }
}
