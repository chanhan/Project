/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： TWCadreBll.cs
 * 檔功能描述： 駐派幹部資料業務邏輯類
 * 
 * 版本：1.0
 * 創建標識： 顧陳偉 2011.12.16
 * 
 */

using System;
using System.Collections.Generic;
using System.Data;
using GDSBG.MiABU.Attendance.IDAL.HRM;
using GDSBG.MiABU.Attendance.Model.HRM;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;

namespace GDSBG.MiABU.Attendance.BLL.HRM
{
    public class TWCadreBll : BLLBase<ITWCadreDal>
    {
        /// <summary>
        /// 獲得所有資位
        /// </summary>
        /// <returns></returns>
        public DataTable GetLevel()
        {
            return DAL.GetLevel();
        }

        /// <summary>
        /// 獲得所有管理職
        /// </summary>
        /// <returns></returns>
        public DataTable GetManager()
        {
            return DAL.GetManager();
        }

        /// <summary>
        /// 獲得所有在職狀態
        /// </summary>
        /// <returns></returns>
        public DataTable GetEmpStatus()
        {
            return DAL.GetEmpStatus();
        }

        /// <summary>
        /// 獲得性別資料
        /// </summary>
        /// <returns></returns>
        public DataTable GetSex()
        {
            return DAL.GetSex();
        }

        /// <summary>
        /// 根據條件查詢數據
        /// </summary>
        /// <param name="model"></param>
        /// <param name="sql"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public DataTable GetTWCadrePageInfo(TWCadreModel model,string sql, string LevelCondition, string ManagerCondition, string StatusCondition, string StrJoinDateFrom, string StrJoinDateTo, string StrLeaveDateFrom, string StrLeaveDateTo, int pageIndex, int pageSize, out int totalCount)
        {
            DateTime JoinDateFrom = DateTime.MinValue, JoinDateTo = DateTime.MaxValue, LeaveDateFrom = DateTime.MinValue, LeaveDateTo = DateTime.MaxValue;
            if (!string.IsNullOrEmpty(StrJoinDateFrom))
            {
                JoinDateFrom = Convert.ToDateTime(StrJoinDateFrom);
            }
            if (!string.IsNullOrEmpty(StrJoinDateTo))
            {
                JoinDateTo = Convert.ToDateTime(StrJoinDateTo);
            }
            if (!string.IsNullOrEmpty(StrLeaveDateFrom))
            {
                LeaveDateFrom = Convert.ToDateTime(StrLeaveDateFrom);
            }
            if (!string.IsNullOrEmpty(StrLeaveDateTo))
            {
                LeaveDateTo = Convert.ToDateTime(StrLeaveDateTo);
            }
            return DAL.GetTWCadrePageInfo(model,sql, LevelCondition, ManagerCondition, StatusCondition, JoinDateFrom, JoinDateTo, LeaveDateFrom, LeaveDateTo, pageIndex, pageSize, out totalCount);
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
        public DataTable GetTWCadreForExport(TWCadreModel model, string sql, string LevelCondition, string ManagerCondition, string StatusCondition, string StrJoinDateFrom, string StrJoinDateTo, string StrLeaveDateFrom, string StrLeaveDateTo)
        {
            DateTime JoinDateFrom = DateTime.MinValue, JoinDateTo = DateTime.MaxValue, LeaveDateFrom = DateTime.MinValue, LeaveDateTo = DateTime.MaxValue;
            if (!string.IsNullOrEmpty(StrJoinDateFrom))
            {
                JoinDateFrom = Convert.ToDateTime(StrJoinDateFrom);
            }
            if (!string.IsNullOrEmpty(StrJoinDateTo))
            {
                JoinDateTo = Convert.ToDateTime(StrJoinDateTo);
            }
            if (!string.IsNullOrEmpty(StrLeaveDateFrom))
            {
                LeaveDateFrom = Convert.ToDateTime(StrLeaveDateFrom);
            }
            if (!string.IsNullOrEmpty(StrLeaveDateTo))
            {
                LeaveDateTo = Convert.ToDateTime(StrLeaveDateTo);
            }
            return DAL.GetTWCadreForExport(model, sql, LevelCondition, ManagerCondition, StatusCondition, JoinDateFrom, JoinDateTo, LeaveDateFrom, LeaveDateTo);
        }

        /// <summary>
        /// 將datatable轉換為List
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public List<TWCadreModel> GetList(DataTable dt)
        {
            return DAL.GetList(dt);
        }

        /// <summary>
        /// 新增功能
        /// </summary>
        /// <param name="model">要新增的功能Model</param>
        /// <returns>是否成功</returns>
        public bool AddTWCdare(TWCadreModel model, SynclogModel logmodel)
        {
            return DAL.AddTWCdare(model,logmodel);
        }

        /// <summary>
        /// 根據主鍵修改功能
        /// </summary>
        /// <param name="model">要修改的功能Model</param>
        /// <returns>是否成功</returns>
        public bool UpdateTWCdareByKey(TWCadreModel model, SynclogModel logmodel)
        {
            return DAL.UpdateTWCdareByKey(model,logmodel);
        }

        /// <summary>
        /// 刪除
        /// </summary>
        /// <param name="workno">工號</param>
        /// <returns></returns>
        public int DeleteTWCadre(string workno, SynclogModel logmodel)
        {
            return DAL.DeleteTWCadre(workno,logmodel);
        }

        /// <summary>
        /// 查詢資料是否存在
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public DataTable GetTWCafreByKey(TWCadreModel model)
        {
            return DAL.GetTWCafreByKey(model);
        }

        /// <summary>
        /// 獲得派駐幹部資料
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public TWCadreModel GetTWCadreInfoByKey(TWCadreModel model)
        {
            return DAL.GetTWCadreInfoByKey(model);
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
            return DAL.ImpoertExcel(personcode, out successnum, out errornum,logmodel);
        }
    }
}
