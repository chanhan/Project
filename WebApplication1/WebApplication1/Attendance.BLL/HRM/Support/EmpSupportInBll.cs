/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： EmpSupportInBll.cs
 * 檔功能描述： 內部支援業務邏輯類
 * 
 * 版本：1.0
 * 創建標識： 顧陳偉 2012.01.06
 * 
 */

using System.Collections.Generic;
using System.Data;
using GDSBG.MiABU.Attendance.IDAL.HRM.Support;
using GDSBG.MiABU.Attendance.Model.HRM.Support;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;

namespace GDSBG.MiABU.Attendance.BLL.HRM.Support
{
    public class EmpSupportInBll : BLLBase<IEmpSupportInDal>
    {
        /// <summary>
        /// 根據條件分頁查詢
        /// </summary>
        /// <param name="model"></param>
        /// <param name="LevelCondition"></param>
        /// <param name="ManagerCondition"></param>
        /// <param name="StartDateFrom"></param>
        /// <param name="StartDateTo"></param>
        /// <param name="PrepEndDateFrom"></param>
        /// <param name="PreEndDateTo"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public DataTable GetEmpSupportInPageInfo(EmpSupportInModel model, string SupportDept, string LevelCondition, string ManagerCondition, string StartDateFrom, string StartDateTo, string PrepEndDateFrom, string PrepEndDateTo, int pageIndex, int pageSize, out int totalCount)
        {
            return DAL.GetEmpSupportInPageInfo(model,SupportDept, LevelCondition, ManagerCondition, StartDateFrom, StartDateTo, PrepEndDateFrom, PrepEndDateTo, pageIndex, pageSize, out totalCount);
        }

        /// <summary>
        /// 導出查詢
        /// </summary>
        /// <param name="model"></param>
        /// <param name="sql"></param>
        /// <param name="SupportDept"></param>
        /// <param name="LevelCondition"></param>
        /// <param name="ManagerCondition"></param>
        /// <param name="StartDateFrom"></param>
        /// <param name="StartDateTo"></param>
        /// <param name="PrepEndDateFrom"></param>
        /// <param name="PrepEndDateTo"></param>
        /// <returns></returns>
        public DataTable GetEmpSupportInForExport(EmpSupportInModel model, string SupportDept, string LevelCondition, string ManagerCondition, string StartDateFrom, string StartDateTo, string PrepEndDateFrom, string PrepEndDateTo)
        {
            return DAL.GetEmpSupportInForExport(model, SupportDept, LevelCondition, ManagerCondition, StartDateFrom, StartDateTo, PrepEndDateFrom, PrepEndDateTo);
        }

        /// <summary>
        /// 將datatable轉換為List
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public List<EmpSupportInModel> GetList(DataTable dt)
        {
            return DAL.GetList(dt);
        }

        /// <summary>
        /// 刪除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int DeleteEmpSupportIn(EmpSupportInModel model, SynclogModel logmodel)
        {
            return DAL.DeleteEmpSupportIn(model,logmodel);
        }

        /// <summary>
        /// 根據主鍵查詢
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public EmpSupportInModel GetEmpSupportInInfo(EmpSupportInModel model)
        {
            return DAL.GetEmpSupportInInfo(model);
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddEmpSupportIn(EmpSupportInModel model, SynclogModel logmodel)
        {
            return DAL.AddEmpSupportIn(model,logmodel);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateEmpSupportIn(EmpSupportInModel model, SynclogModel logmodel)
        {
            return DAL.UpdateEmpSupportIn(model,logmodel);
        }

        /// <summary>
        /// 獲得員工資料
        /// </summary>
        /// <param name="EmployeeNo"></param>
        /// <returns></returns>
        public DataTable GetEmp(string EmployeeNo)
        {
            return DAL.GetEmp(EmployeeNo);
        }

        /// <summary>
        /// 獲得內部支援員工資料
        /// </summary>
        /// <param name="EmployeeNo"></param>
        /// <returns></returns>
        public DataTable GetEmpSupportIn(string EmployeeNo)
        {
            return DAL.GetEmpSupportIn(EmployeeNo);
        }

        /// <summary>
        /// 獲得員工在職狀態
        /// </summary>
        /// <param name="workno"></param>
        /// <returns></returns>
        public DataTable GetEmpStatus(string workno)
        {
            return DAL.GetEmpStatus(workno);
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

        /// <summary>
        /// 放大鏡部門查詢
        /// </summary>
        /// <param name="depname"></param>
        /// <param name="depcode"></param>
        /// <returns></returns>
        public DataTable GetDataByCondition(string modulecode, string sql, string depname, string depcode)
        {
            return DAL.GetDataByCondition(modulecode,sql,depname, depcode);
        }
    }
}
