/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： EmpSupportOutBll.cs
 * 檔功能描述： 外部支援業務邏輯類
 * 
 * 版本：1.0
 * 創建標識： 顧陳偉 2012.01.04
 * 
 */

using System.Collections.Generic;
using System.Data;
using GDSBG.MiABU.Attendance.IDAL.HRM.Support;
using GDSBG.MiABU.Attendance.Model.HRM.Support;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;

namespace GDSBG.MiABU.Attendance.BLL.HRM.Support
{
    public class EmpSupportOutBll : BLLBase<IEmpSupportOutDal>
    {
        /// <summary>
        /// 根據條件分頁查詢
        /// </summary>
        /// <param name="model"></param>
        /// <param name="LevelCondition"></param>
        /// <param name="ManagerCondition"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public DataTable GetEmpSupportOutPageInfo(EmpSupportOutModel model, string sql, string SupportDept, string LevelCondition, string ManagerCondition, int pageIndex, int pageSize, out int totalCount)
        {
            return DAL.GetEmpSupportOutPageInfo(model,sql, SupportDept, LevelCondition, ManagerCondition, pageIndex, pageSize, out totalCount);
        }

        /// <summary>
        /// 導出查詢
        /// </summary>
        /// <param name="model"></param>
        /// <param name="sql"></param>
        /// <param name="SupportDept"></param>
        /// <param name="LevelCondition"></param>
        /// <param name="ManagerCondition"></param>
        /// <returns></returns>
        public DataTable GetEmpSupportOutForExport(EmpSupportOutModel model, string sql, string SupportDept, string LevelCondition, string ManagerCondition)
        {
            return DAL.GetEmpSupportOutForExport(model, sql, SupportDept, LevelCondition, ManagerCondition);
        }

        /// <summary>
        /// 將datatable轉換為List
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public List<EmpSupportOutModel> GetList(DataTable dt)
        {
            return DAL.GetList(dt);
        }

        /// <summary>
        /// 刪除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int DeleteEmpSupportOut(EmpSupportOutModel model, SynclogModel logmodel)
        {
            return DAL.DeleteEmpSupportOut(model,logmodel);
        }

        /// <summary>
        /// 根據主鍵查詢
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public EmpSupportOutModel GetEmpSupportOutInfo(EmpSupportOutModel model)
        {
            return DAL.GetEmpSupportOutInfo(model);
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddEmpSupportOut(EmpSupportOutModel model, SynclogModel logmodel)
        {
            return DAL.AddEmpSupportOut(model,logmodel);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateEmpSupportOut(EmpSupportOutModel model, SynclogModel logmodel)
        {
            return DAL.UpdateEmpSupportOut(model,logmodel);
        }

        /// <summary>
        /// 獲得支援中員工
        /// </summary>
        /// <param name="workno"></param>
        /// <returns></returns>
        public DataTable GetEmpSupportOut(string workno)
        {
            return DAL.GetEmpSupportOut(workno);
        }

        /// <summary>
        /// 獲得員工的支援順序號
        /// </summary>
        /// <param name="workno"></param>
        /// <returns></returns>
        public DataTable GetEmpsupportOrder(string workno)
        {
            return DAL.GetEmpsupportOrder(workno);
        }

        /// <summary>
        /// 由工號和支援序號查詢工號
        /// </summary>
        /// <param name="workno"></param>
        /// <param name="supportorder"></param>
        /// <returns></returns>
        public DataTable GetEmpSupportByWorkNoAndOrder(string workno, string supportorder)
        {
            return DAL.GetEmpSupportByWorkNoAndOrder(workno, supportorder);
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
