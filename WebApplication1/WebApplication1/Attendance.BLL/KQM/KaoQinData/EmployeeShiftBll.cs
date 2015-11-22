/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： OrgmanagerBll.cs
 * 檔功能描述： 排班作業業務邏輯類
 * 
 * 版本：1.0
 * 創建標識： 昝望 2011.12.27
 * 
 */


using System;
using System.Collections.Generic;
using System.Text;
using GDSBG.MiABU.Attendance.IDAL.KQM.KaoQinData;
using GDSBG.MiABU.Attendance.Model.KQM.KaoQinData;
using System.Data;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;

namespace GDSBG.MiABU.Attendance.BLL.KQM.KaoQinData
{
    public class EmployeeShiftBll : BLLBase<IEmployeeShiftDal>
    {
        /// <summary>
        /// 查詢排班作業
        /// </summary>
        /// <param name="model">給出主鍵值的Model</param>
        /// <returns>返回對應主鍵的Model</returns>
        public DataTable GetShiftInfo(EmployeeShiftModel model, int pageIndex, int pageSize, out int totalCount, string depcode, string shiftdate, string BatchEmployeeNo, string shift, string sqlDep)
        {
            return DAL.GetShiftInfo(model, pageIndex, pageSize, out totalCount, depcode, shiftdate, BatchEmployeeNo, shift,sqlDep);
        }

        /// <summary>
        /// 導出查詢排班作業
        /// </summary>
        /// <param name="model">給出主鍵值的Model</param>
        /// <returns>返回對應主鍵的Model</returns>
        public DataTable GetShiftInfo(EmployeeShiftModel model, string depcode, string shiftdate, string BatchEmployeeNo, string shift, string sqlDep)
        {
            return DAL.GetShiftInfo(model,depcode,shiftdate,BatchEmployeeNo,shift,sqlDep);
        }

        /// <summary>
        /// 獲得類別
        /// </summary>
        /// <returns></returns>
        public DataTable GetShiftType()
        {
            return DAL.GetShiftType();
        }

        /// <summary>
        /// 將datatable轉換為List
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public List<EmployeeShiftModel> GetList(DataTable dt)
        {
            return DAL.GetList(dt);
        }

        /// <summary>
        /// 獲得權限下的班別類型
        /// </summary>
        /// <param name="PersonDepCode"></param>
        /// <returns></returns>
        public DataTable GetShift(string PersonDepCode)
        {
            return DAL.GetShift(PersonDepCode);
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
        /// 獲得工號的對應信息
        /// </summary>
        /// <param name="workno"></param>
        /// <returns></returns>
        public DataTable GetEmployeeInfo(string workno)
        {

            return DAL.GetEmployeeInfo(workno);
        }


        /// <summary>
        /// 獲得對應工號的例外加班信息
        /// </summary>
        /// <param name="workno"></param>
        /// <returns></returns>
        public DataTable GetEmployeeShifInfo(string workno)
        {
            return DAL.GetEmployeeShifInfo(workno);
        }

        /// <summary>
        /// 修改例外排班
        /// </summary>
        /// <param name="model"></param>
        /// <returns>是否成功</returns>
        public bool UpdateEmployeeShiftByKey(EmployeeShiftModel model, SynclogModel logmodel)
        {
            return DAL.UpdateEmployeeShiftByKey(model,logmodel);
        }

        /// <summary>
        /// 新增例外排班
        /// </summary>
        /// <param name="model"></param>
        /// <returns>是否成功</returns>
        public bool AddEmployeeShift(EmployeeShiftModel model, SynclogModel logmodel)
        {
            return DAL.AddEmployeeShift(model,logmodel);
        }

        /// <summary>
        /// 刪除
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public bool DeleteEmployeeShiftByKey(string ID, SynclogModel logmodel)
        {
            return DAL.DeleteEmployeeShiftByKey(ID,logmodel);
        }



        /// <summary>
        /// 查詢對應部門的組織排班
        /// </summary>
        /// <param name="depcode"></param>
        /// <returns></returns>
        public DataTable SelectOrgShift(string depcode)
        {
            return DAL.SelectOrgShift(depcode);
        }

        /// <summary>
        /// 新增組織排班
        /// </summary>
        /// <param name="depcode"></param>
        /// <param name="shiftno"></param>
        /// <param name="startdate"></param>
        /// <param name="endate"></param>
        /// <param name="personcode"></param>
        /// <returns></returns>
        public bool AddOrgShift(string depcode, string shiftno, string startdate, string endate, string personcode, SynclogModel logmodel)
        {
            return DAL.AddOrgShift(depcode, shiftno, startdate, endate, personcode,logmodel);
        }

        /// <summary>
        /// 更新組織排班
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="shiftno"></param>
        /// <param name="startdate"></param>
        /// <param name="endate"></param>
        /// <returns></returns>
        public bool UpdateOrgShift(string ID, string shiftno, string startdate, string endate, string personcode, SynclogModel logmodel)
        {
            return DAL.UpdateOrgShift(ID, shiftno, startdate, endate, personcode,logmodel);
        }

        /// <summary>
        /// 刪除組織排班
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public bool DeleteOrgShift(string ID, SynclogModel logmodel)
        {
            return DAL.DeleteOrgShift(ID,logmodel);
        }
    }
}
