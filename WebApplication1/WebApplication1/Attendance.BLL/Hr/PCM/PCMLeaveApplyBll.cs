/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： PCMLeaveApplyBll.cs
 * 檔功能描述： 個人中心請假申請業務邏輯類
 * 
 * 版本：1.0
 * 創建標識： 陳函 2012.3.10
 * 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GDSBG.MiABU.Attendance.IDAL.Hr.PCM;
using System.Data;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;

namespace GDSBG.MiABU.Attendance.BLL.Hr.PCM
{
    public class PCMLeaveApplyBll : BLLBase<IPCMLeaveApplyDal>
    {
        /// <summary>
        /// 獲取個人請假信息
        /// </summary>
        /// <param name="user">請假人工號</param>
        /// <param name="billNo">申請單號</param>
        /// <param name="LVTypeCode">請假類別</param>
        /// <param name="status">表單狀態</param>
        /// <param name="startDate">開始日期</param>
        /// <param name="endDate">結束日期</param>
        /// <param name="applyType">申請類別</param>
        /// <param name="currentPageIndex">當前頁數</param>
        /// <param name="pageSize">每頁顯示的最大記錄數</param>
        /// <param name="totalCount">總記錄數</param>
        /// <returns>個人請假信息</returns>
        public DataTable getApplyData(string user, string billNo, string LVTypeCode, string status, string startDate, string endDate, string applyType, int currentPageIndex, int pageSize, out int totalCount)
        {
            return DAL.getApplyData(user, billNo, LVTypeCode, status, startDate, endDate, applyType, currentPageIndex, pageSize, out  totalCount);
        }
        /// <summary>
        /// 獲取員工基本信息
        /// </summary>
        /// <param name="workNo">員工工號</param>
        /// <returns>員工基本信息</returns>
        public DataTable getEmployeeDataByCondition(string workNo)
        {
            return DAL. getEmployeeDataByCondition( workNo);
        }
        /// <summary>
        /// 根據性別獲取員工能請的假別
        /// </summary>
        /// <param name="marrystate">是否已婚</param>
        /// <param name="sexCode">性別代碼</param>
        /// <returns>員工能請的假別</returns>
        public DataTable getKQMLeaveTypeData(string marrystate, string sexCode)
        {
           return DAL.getKQMLeaveTypeData( marrystate,  sexCode);
        }
        /// <summary>
        /// 獲取代理人的基本信息
        /// </summary>
        /// <param name="workNo">代理人工號</param>
        /// <param name="localName">代理人姓名</param>
        /// <returns>代理人的基本信息</returns>
        public DataTable getAuditData(string workNo, string localName)
        {
            return DAL.getAuditData(workNo, localName);
        }

        /// <summary>
        /// 獲取提示窗口信息
        /// </summary>
        /// <param name="workNo">登陸人工號</param>
        /// <returns>登錄人員工基本信息</returns>
        public DataTable GetDataSetBySQL(string workNo)
        {
          return DAL.GetDataSetBySQL( workNo);
        }
        /// <summary>
        /// 獲取員工基本信息
        /// </summary>
        /// <param name="employeeNo">員工工號</param>
        /// <returns>員工基本信息</returns>
        public DataTable getVDataByCondition(string employeeNo)
        {
            return DAL.getVDataByCondition(employeeNo);
        }
        /// <summary>
        /// 是否允許個人申請此假別
        /// </summary>
        /// <param name="lvTypeCode">請假類別代碼</param>
        /// <returns>是否允許申請此假別</returns>
        public string iSAllowPCM(string lvTypeCode)
        {
            DataTable dt = DAL.iSAllowPCM(lvTypeCode);
            if (dt!=null&&dt.Rows.Count>0)
            {
                return dt.Rows[0][0].ToString();
            }
            return "";
        }
        /// <summary>
        /// 根據ID獲取請假信息
        /// </summary>
        /// <param name="id">請假信息ID</param>
        /// <returns>請假信息</returns>
        public DataTable getDataById(string id)
        {
            return DAL.getDataById(id);
        }
        /// <summary>
        /// 新增短信通知記錄
        /// </summary>
        /// <param name="window">窗口類型</param>
        /// <param name="remindContent">短信類容</param>
        /// <param name="logmodel">操作日誌</param>
        public void ExcuteSQL(string window, string remindContent, SynclogModel logmodel)
        {
           DAL.ExcuteSQL( window,  remindContent,logmodel);
        }
        /// <summary>
        /// 根據工號獲取請假信息
        /// </summary>
        /// <param name="id">請假人工號</param>
        /// <returns>請假信息</returns>
        public DataTable getVDataByWorkNo(string workNo)
        {
            return DAL.getVDataByWorkNo(workNo);
        }

        //public DataTable getApptypetoBillConfig(string LVTypeCode)
        //{
        //    return DAL.getApptypetoBillConfig(LVTypeCode);
        //}

        //public string getBillTypeCode(string LVTypeCode)
        //{
        //   DataTable dt=DAL.getBillTypeCode( LVTypeCode);
        //   if (dt != null && dt.Rows.Count > 0)
        //   {
        //       return dt.Rows[0][0].ToString();
        //   }
        //   return "";
        //}
    }
}
