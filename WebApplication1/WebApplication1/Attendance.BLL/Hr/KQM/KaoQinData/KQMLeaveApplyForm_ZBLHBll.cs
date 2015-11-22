/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： KQMLeaveApplyForm_ZBLHBll.cs
 * 檔功能描述： 請假申請業務邏輯類
 * 
 * 版本：1.0
 * 創建標識： 陳函 2012.3.10
 * 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GDSBG.MiABU.Attendance.IDAL.Hr.KQM.KaoQinData;
using System.Data;
using System.Collections;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;
using GDSBG.MiABU.Attendance.Model.KQM.KaoQinData;

namespace GDSBG.MiABU.Attendance.BLL.Hr.KQM.KaoQinData
{
    public class KQMLeaveApplyForm_ZBLHBll : BLLBase<IKQMLeaveApplyForm_ZBLHDal>
    {
        /// <summary>
        /// 獲取請假類型
        /// </summary>
        /// <returns>請假類型</returns>
        public DataTable getLeaveType()
        {
            return DAL.getLeaveType();
        }
        /// <summary>
        /// 獲取表單狀態
        /// </summary>
        /// <returns>表單狀態</returns>
        public DataTable getStatus()
        {
            return DAL.getStatus();
        }
        /// <summary>
        /// 獲取申請類型
        /// </summary>
        /// <returns>申請類型</returns>
        public DataTable getApplyType()
        {
            return DAL.getApplyType();
        }
        /// <summary>
        /// 請假是否需要簽核
        /// </summary>
        /// <returns>請假是否需要簽核</returns>
        public string isLeaveNoAudit()
        {
            DataTable dt = DAL.isLeaveNoAudit();
            if (dt != null && dt.Rows.Count > 0)
            {
                return dt.Rows[0][0].ToString();
            }
            return "";
        }

        //public bool CheckDateMonths(string startDate, string endDate)
        //{
        //    return Convert.ToInt32(DAL.CheckDateMonths(startDate, endDate).Rows[0][0].ToString()) >= 3;
        //}
        /// <summary>
        /// 獲取請假信息用於導出Excel
        /// </summary>
        /// <param name="Privileged">是否有組織權限</param>
        /// <param name="sqlDep">組織權限管控</param>
        /// <param name="depName">部門名稱</param>
        /// <param name="billNo">單據編號</param>
        /// <param name="workNo">工號 </param>
        /// <param name="localName">姓名 </param>
        /// <param name="LVTypeCode">請假類別 </param>
        /// <param name="status">表單狀態 </param>
        /// <param name="testify">繳交證明 </param>
        /// <param name="startDate">開始日期</param>
        /// <param name="endDate">截止日期</param>
        /// <param name="applyStartDate">申請開始日期</param>
        /// <param name="applyEndDate">申請截止日期</param>
        /// <param name="applyType">申請類別</param>
        /// <param name="flag">是否查詢因班別變動導致請假時數有問題數據 </param>
        /// <param name="IsLastYear">是否補休</param>
        /// <param name="currentPageIndex">當前頁數 </param>
        /// <param name="pageSize">每頁顯示的最大記錄數</param>
        /// <param name="totalCount">總記錄數</param>
        /// <returns>請假信息</returns>
        public DataTable getApplyData(bool Privileged, string SqlDep, string depName, string billNo, string workNo, string localName, string LVTypeCode, string status, string testify, string startDate, string endDate, string applyStartDate, string applyEndDate, string applyType, bool flag, string IsLastYear, int currentPageIndex, int pageSize, out int totalCount)
        {
            return DAL.getApplyData(Privileged, SqlDep, depName, billNo, workNo, localName, LVTypeCode, status, testify, startDate, endDate, applyStartDate, applyEndDate, applyType, flag, IsLastYear, currentPageIndex, pageSize, out  totalCount);

        }

        //public string getThisLVTotal(string sID)
        //{
        //    return DAL.getThisLVTotal(sID).Rows[0][0].ToString();
        //}

        //public string getTLVWorkDays(string sID)
        //{
        //    return DAL.getTLVWorkDays(sID).Rows[0][0].ToString();
        //}
        /// <summary>
        /// 根據工號查詢員工基本信息
        /// </summary>
        /// <param name="IsPrivileged">是否有組織權限</param>
        /// <param name="sqlDep">權限管控</param>
        /// <param name="empNo">工號</param>
        /// <returns>員工基本信息</returns>
        public DataTable getEmpInfo(bool IsPrivileged, string sqlDep, string empNo)
        {
            return DAL.getEmpInfo(IsPrivileged, sqlDep, empNo);
        }
        /// <summary>
        /// 統計每天加班時數,每月加班時數
        /// </summary>
        /// <param name="empNo">工號</param>
        public void CountCanAdjlasthy(string empNo)
        {
            DAL.CountCanAdjlasthy(empNo);
        }
        /// <summary>
        /// 獲取員工性別
        /// </summary>
        /// <param name="empNo">工號</param>
        /// <returns>員工性別</returns>
        public string getSexCode(string empNo)
        {
            DataTable dt = DAL.getSexCode(empNo);
            return dt.Rows[0][0].ToString();
        }
        /// <summary>
        /// 獲取員工入職時數
        /// </summary>
        /// <param name="empNo">工號</param>
        /// <returns>員工入職時數</returns>
        public string getInWorkYears(string empNo)
        {
            DataTable dt = DAL.getInWorkYears(empNo);
            if (dt != null && dt.Rows.Count > 0)
            {
                return dt.Rows[0][0].ToString();
            }
            return "0";
        }
        /// <summary>
        /// 獲取非晚婚假限制天數
        /// </summary>
        /// <returns></returns>
        public string getLimitdays()
        {
            return DAL.getLimitdays().Rows[0][0].ToString();
        }
        /// <summary>
        /// 獲取員工入職時數
        /// </summary>
        /// <param name="empNo">工號</param>
        /// <returns>員工入職時數</returns>
        public string getAges(string empNo)
        {
            return DAL.getAges(empNo).Rows[0][0].ToString();
        }
        /// <summary>
        /// 獲取婚假限制天數
        /// </summary>
        /// <returns>婚假限制天數</returns>
        public string getJLimitdays()
        {
            DataTable dt = DAL.getJLimitdays();
            if (dt != null && dt.Rows.Count > 0)
            {
                return dt.Rows[0][0].ToString();
            }
            return "";
        }
        /// <summary>
        ///按工號獲取婚假限制天數
        /// </summary>
        /// <param name="empNo">工號</param>
        /// <returns></returns>
        public string getSpecLimitDays(string empNo)
        {
            DataTable dt = DAL.getSpecLimitDays(empNo);
            if (dt != null && dt.Rows.Count > 0)
            {
                return dt.Rows[0][0].ToString();
            }
            return "";
        }
        /// <summary>
        /// 獲取請假類別數
        /// </summary>
        /// <param name="sexCode">性別代碼</param>
        /// <returns>請假類別數據</returns>
        public DataTable getLeaveTypeCount(string sexCode)
        {
            return DAL.getLeaveTypeCount(sexCode);
        }
        /// <summary>
        /// 根據性別獲取請假類別
        /// </summary>
        /// <param name="sexCode">性別代碼</param>
        /// <returns>請假類別</returns>
        public DataTable getDataByCondition(string sexCode)
        {
            return DAL.getDataByCondition(sexCode);
        }
        /// <summary>
        /// 獲取請假總時數
        /// </summary>
        /// <param name="empNo">工號</param>
        /// <returns>請假總時數</returns>
        public DataTable getLVTotal(string empNo)
        {
            return DAL.getLVTotal(empNo);
        }
        /// <summary>
        /// 獲取限制天數
        /// </summary>
        /// <param name="empNo">工號</param>
        /// <param name="LVTypeCode">類別代碼</param>
        /// <returns></returns>
        public string getLimitDays(string empNo, string LVTypeCode)
        {
            DataTable dt = DAL.getLimitDays(empNo, LVTypeCode);
            if (dt != null && dt.Rows.Count > 0)
            {
                return dt.Rows[0][0].ToString();
            }
            return "";
        }
        /// <summary>
        /// 獲取年度依年資應修年假天數
        /// </summary>
        /// <param name="empNo">工號</param>
        /// <param name="reportYear">當前年份</param>
        /// <param name="applyDate">當前日期</param>
        /// <returns></returns>
        public string getYearLeaveDays(string empNo, string reportYear, string applyDate)
        {
            return DAL.getYearLeaveDays(empNo, reportYear, applyDate);
        }
        /// <summary>
        /// 獲取請假總時數
        /// </summary>
        /// <param name="LVTotal">已修總時數</param>
        /// <param name="empNo">工號</param>
        /// <param name="typeCode">請假類別</param>
        /// <param name="IsUTypeCode">是否為調休</param>
        /// <returns>請假總時數</returns>
        public string getTime(string empNo, double LVTotal)
        {
            DataTable dt = DAL.getTime(empNo, LVTotal);
            if (dt != null && dt.Rows.Count > 0)
            {
                return dt.Rows[0][0].ToString();
            }
            return "";
        }
        /// <summary>
        /// 獲取調休總時數
        /// </summary>
        /// <param name="LVTotal">已修總時數</param>
        /// <param name="empNo">工號</param>
        /// <param name="typeCode">請假類別</param>
        /// <param name="IsUTypeCode">是否為調休</param>
        /// <returns>調休總時數</returns>
        public string getSumlvTotal(double LVTotal, string empNo, string typeCode, bool IsUTypeCode)
        {
            return DAL.getSumlvTotal(LVTotal, empNo, typeCode, IsUTypeCode).Rows[0][0].ToString();
        }
        /// <summary>
        /// 獲取試用員工或預師工號
        /// </summary>
        /// <param name="employeeNo">工號</param>
        /// <returns>試用員工或預師工號</returns>
        public string getWorkNo(string employeeNo)
        {
            DataTable dt = DAL.getWorkNo(employeeNo);
            if (dt != null && dt.Rows.Count > 0)
            {
                return dt.Rows[0][0].ToString();
            }
            return "";
        }
        /// <summary>
        /// 獲取請假類別
        /// </summary>
        /// <param name="marryFlag">是否已婚</param>
        /// <param name="noYearHolidayFlag">是否為年休假</param>
        /// <returns>請假類別</returns>
        public DataTable getLVType(bool marryFlag, bool noYearHolidayFlag)
        {
            return DAL.getLVType(marryFlag, noYearHolidayFlag);
        }
        /// <summary>
        /// 是否為非年假
        /// </summary>
        /// <returns>是否為非年假</returns>
        public string getNoYearHoliday()
        {
            return DAL.getNoYearHoliday().Rows[0][0].ToString();
        }
        /// <summary>
        /// 是否結婚管控假別
        /// </summary>
        /// <returns>是否結婚管控假別</returns>
        public string getMarryFlag()
        {
            return DAL.getMarryFlag().Rows[0][0].ToString();
        }
        /// <summary>
        /// 獲取員工資位
        /// </summary>
        /// <returns>員工資位</returns>
        public DataTable getLevelCode()
        {
            return DAL.getLevelCode();
        }
        /// <summary>
        /// 查詢請假申請是否拒簽
        /// </summary>
        /// <param name="workNo">工號</param>
        /// <param name="typeCode">請假類別代碼</param>
        /// <returns>是否拒簽結果</returns>
        public string checkRefuseApply(string workNo, string typeCode)
        {
            DataTable dt = DAL.checkRefuseApply(workNo, typeCode);
            if (dt != null && dt.Rows.Count > 0)
            {
                return dt.Rows[0][0].ToString();
            }
            return "N";
        }
        /// <summary>
        /// 獲取請假總時數
        /// </summary>
        /// <param name="workNo">工號</param>
        /// <param name="startDate">開始日期</param>
        /// <param name="endDate">結束日期</param>
        /// <param name="typeCode">請假類別代碼</param>
        /// <returns>請假總時數</returns>
        public string getLVTotal(string workNo, string startDate, string endDate, string typeCode)
        {
            return DAL.getLVTotal(workNo, startDate, endDate, typeCode);
        }
        /// <summary>
        /// 獲取員工班別
        /// </summary>
        /// <param name="workNo">工號</param>
        /// <param name="startDate">開始日期</param>
        /// <returns>員工班別</returns>
        public string getLVTotal(string workNo, string startDate)
        {
            return DAL.getLVTotal(workNo, startDate);
        }
        /// <summary>
        /// 獲取允許申請的資位
        /// </summary>
        /// <param name="lvTypeCode">請假類別</param>
        /// <returns>允許申請的資位</returns>
        public string getAllowDepLevel(string lvTypeCode)
        {
            DataTable dt = DAL.getAllowDepLevel(lvTypeCode);
            if (dt != null && dt.Rows.Count > 0)
            {
                return dt.Rows[0][0].ToString();
            }
            return "";
        }
        /// <summary>
        /// 獲取離職日期
        /// </summary>
        /// <param name="empNo">工號</param>
        /// <returns>離職日期</returns>
        public string getLeaveDate(string empNo)
        {
            DataTable dt = DAL.getLeaveDate(empNo);
            if (dt != null && dt.Rows.Count > 0)
            {
                return dt.Rows[0][0].ToString();
            }
            return "";
        }
        /// <summary>
        /// 獲取授權功能列表
        /// </summary>
        /// <param name="roleCode">角色代碼</param>
        /// <param name="moduleCode">模組代碼</param>
        /// <returns>授權功能列表</returns>
        public SortedList getAuthorizedFunctionList(string roleCode, string moduleCode)
        {
            SortedList list = new SortedList();
            string strFunctionList = "";
            DataTable dt = DAL.getAuthorizedFunctionList(roleCode, moduleCode);
            if (dt != null && dt.Rows.Count > 0)
            {
                strFunctionList = dt.Rows[0][0].ToString();
                if (strFunctionList.Length > 0)
                {
                    string[] arr = strFunctionList.Split(new char[] { ',' });
                    for (int i = 0; i < arr.Length; i++)
                    {
                        list.Add(list.Count, arr[i]);
                    }
                }
            }
            return list;
        }
        /// <summary>
        /// 請假是否需要簽核
        /// </summary>
        /// <returns> 請假是否需要簽核</returns>
        public string getParaValue()
        {
            DataTable dt = DAL.getParaValue();
            if (dt != null && dt.Rows.Count > 0)
            {
                return dt.Rows[0][0].ToString();
            }
            return "";
        }

        /// <summary>
        /// 獲取假別的最小時數和標準時數
        /// </summary>
        /// <param name="LVType">假別代碼</param>
        /// <returns>假別的最小時數和標準時數</returns>
        public DataTable getHours(string LVType)
        {
            return DAL.getHours(LVType);
        }
        /// <summary>
        /// 根據ID獲取請假信息
        /// </summary>
        /// <param name="ID">請假ID</param>
        /// <returns>請假信息</returns>
        public DataTable getDataByBillNo(string billNO)
        {
            return DAL.getDataByBillNo(billNO);
        }
        /// <summary>
        /// 是否允許調休
        /// </summary>
        /// <returns>是否允許調休</returns>
        public string getRestChangeHours()
        {
            DataTable dt = DAL.getRestChangeHours();
            if (dt != null && dt.Rows.Count > 0)
            {
                return dt.Rows[0][0].ToString();
            }
            return "";
        }
        /// <summary>
        /// 新增修改請假信息
        /// </summary>
        /// <param name="processFlag">新增修改標誌</param>
        /// <param name="tempDataTable">請假信息</param>
        /// <param name="logmodel">操作日誌</param>
        /// <returns>請假ID</returns>
        public string SaveData(string processFlag, DataTable tempDataTable, SynclogModel logmodel)
        {
            return DAL.SaveData(processFlag, tempDataTable, logmodel);
        }
        /// <summary>
        /// 判斷請假時數是否超過管控上限
        /// </summary>
        /// <param name="ProcessFlag">新增修改標誌</param>
        /// <param name="workNo">工號</param>
        /// <param name="lvType">假別</param>
        /// <param name="strDate">請假日期</param>
        /// <param name="sTotal">總時數</param>
        /// <param name="BillNo">ID</param>
        /// <param name="ImportType"></param>
        /// <returns>請假時數是否超過管控上限</returns>
        public bool CheckLvtotal(string ProcessFlag, string workNo, string lvType, string strDate, string sTotal, string BillNo, string ImportType)
        {
            return DAL.CheckLvtotal(ProcessFlag, workNo, lvType, strDate, sTotal, BillNo, ImportType);
        }
        /// <summary>
        /// 獲取當月可調時數
        /// </summary>
        /// <param name="ProcessFlag">新增修改標誌</param>
        /// <param name="workNo">工號</param>
        /// <param name="strDate">開始日期</param>
        /// <param name="ID">ID</param>
        /// <returns>獲取當月可調時數</returns>
        public double CheckLvtotal(string processFlag, string empNo, string startDate, string ID)
        {
            return DAL.CheckLvtotal(processFlag, empNo, startDate, ID);
        }
        /// <summary>
        /// 判斷請假日期是否有重複天數是否
        /// </summary>
        /// <param name="empNo">工號</param>
        /// <param name="startDate">開始日期</param>
        /// <param name="startTime">開始時間</param>
        /// <param name="endDate">結束日期</param>
        /// <param name="endTime">結束時間</param>
        /// <returns>請假日期是否有重複天數是否</returns>
        public bool CheckLeaveOverTime(string empNo, string startDate, string startTime, string endDate, string endTime)
        {
            return DAL.CheckLeaveOverTime(empNo, startDate, startTime, endDate, endTime);
        }
        /// <summary>
        ///計算請假總時長
        /// </summary>
        /// <param name="LeaveID">ID</param>
        public void getLeaveDetail(string LeaveID)
        {
            DAL.getLeaveDetail(LeaveID);
        }
        /// <summary>
        /// 修改時判斷請假日期是否有重複天數是否
        /// </summary>
        /// <param name="empNo">工號</param>
        /// <param name="startDate">開始日期</param>
        /// <param name="startTime">開始時間</param>
        /// <param name="endDate">結束日期</param>
        /// <param name="endTime">結束時間</param>
        /// <param name="ID">ID</param>
        /// <returns>請假日期是否有重複天數是否</returns>
        public bool CheckLeaveOverTime(string WorkNo, string StartDate, string StartTime, string EndDate, string EndTime, string ID)
        {
            return DAL.CheckLeaveOverTime(WorkNo, StartDate, StartTime, EndDate, EndTime, ID);
        }
        /// <summary>
        /// 獲取每月多少日之後不允許重新計算上月及以前考勤數據
        /// </summary>
        /// <returns>天數</returns>
        public string getKqoQinDays()
        {
            DataTable dt = DAL.getKqoQinDays();
            if (dt != null && dt.Rows.Count > 0)
            {
                return dt.Rows[0][0].ToString();
            }
            return "";
        }
        /// <summary>
        /// 獲取未核准或拒簽的請假信息
        /// </summary>
        /// <param name="ID">ID</param>
        /// <returns>請假信息</returns>
        public DataTable getLeaveApplyData(string ID)
        {
            return DAL.getLeaveApplyData(ID);
        }
        /// <summary>
        /// 刪除請假信息
        /// </summary>
        /// <param name="tempDataTable">請假信息</param>
        /// <param name="logmodel">操作日誌</param>
        public void DeleteData(DataTable tempDataTable, SynclogModel logmodel)
        {
            DAL.DeleteData(tempDataTable, logmodel);
        }
        /// <summary>
        /// 導入
        /// </summary>
        /// <param name="createUser">登錄人工號</param>
        /// <param name="moduleCode">模組代碼</param>
        /// <param name="successnum">成功筆數</param>
        /// <param name="errornum">失敗筆數</param>
        /// <param name="logmodel">操作日誌</param>
        /// <returns>錯誤記錄</returns>
        public DataTable ImpoertExcel(string createUser, string moduleCode, out int successnum, out int errornum, SynclogModel logmodel)
        {
            return DAL.ImpoertExcel(createUser, moduleCode, out  successnum, out  errornum, logmodel);
        }
        /// <summary>
        /// 將DataTable轉換為List用於導出
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <returns>List</returns>
        public List<LeaveApplyTempModel> changList(DataTable dt)
        {
            return DAL.changList(dt);
        }
        /// <summary>
        /// 核准表單
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="approver">核准人</param>
        /// <param name="approvedate">核准日期</param>
        /// <param name="status">表單狀態</param>
        /// <param name="logmodel">操作日誌</param>
        public void changeStatusByID(string id, string approver, string approvedate, string status, SynclogModel logmodel)
        {
            DAL.changeStatusByID(id, approver, approvedate, status, logmodel);
        }
        /// <summary>
        /// 取消核准
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="status">表單狀態</param>
        /// <param name="logmodel">操作日誌</param>
        public void changeStatusByID(string id, string status,string  proxyStatus,SynclogModel logmodel)
        {
            DAL.changeStatusByID(id, status, proxyStatus,logmodel);
        }

        //public string getBillTypeCode(string LVTypeCode)
        //{
        //    DataTable dt = DAL.getBillTypeCode(LVTypeCode);
        //    if (dt != null && dt.Rows.Count > 0)
        //    {
        //        return dt.Rows[0][0].ToString();
        //    }
        //    return "";
        //}

        //public DataTable getAppTypeToBillConfigData(string LVTypeCode)
        //{
        //    return DAL.getAppTypeToBillConfigData(LVTypeCode);
        //}
        /// <summary>
        /// 獲取送簽組織
        /// </summary>
        /// <param name="OrgCode">部門代碼</param>
        /// <param name="BillTypeCode">表單類型代碼</param>
        /// <param name="lvType">請假類型代碼</param>
        /// <param name="startDate">開始日期</param>
        /// <param name="endDate">截止日期</param>
        /// <param name="empNo">工號</param>
        /// <returns>送簽組織</returns>
        public string getWorkFlowOrgCode(string OrgCode, string BillTypeCode, string lvType, string startDate, string endDate, string empNo, string lvTotalDays)
        {
            DataTable dt = DAL.getWorkFlowOrgCode(OrgCode, BillTypeCode, lvType, startDate, endDate, empNo, lvTotalDays);
            if (dt != null && dt.Rows.Count > 0)
            {
                return dt.Rows[0][0].ToString();
            }
            return "";
        }
        /// <summary>
        /// 送簽
        /// </summary>
        /// <param name="sFlow_LevelRemark">簽核角色</param>
        /// <param name="ID">ID</param>
        /// <param name="BillNoType">表單類型</param>
        /// <param name="AuditOrgCode">送簽組織</param>
        /// <param name="BillTypeCode">表單類型代碼</param>
        /// <param name="workNo">工號</param>
        /// <param name="reason">請假原因</param>
        /// <param name="empNo">工號</param>
        /// <param name="startDate">開始日期</param>
        /// <param name="endDate">結束日期</param>
        /// <param name="lvtypeCode">請假類別</param>
        /// <param name="model">操作日誌</param>
        /// <returns>送簽是否成功</returns>
        public bool SaveAuditData(string sFlow_LevelRemark, string ID, string BillNoType, string AuditOrgCode, string BillTypeCode, string workNo, string reason, string empNo, string startDate, string endDate, string lvtypeCode, SynclogModel logmodel)
        {
            return DAL.SaveAuditData(sFlow_LevelRemark, ID, BillNoType, AuditOrgCode, BillTypeCode, workNo, reason, empNo, startDate, endDate, lvtypeCode, logmodel);
        }

        //public string getBillTypeCodeFromLeaveApply(string LVTypeCode)
        //{
        //    return DAL.getBillTypeCodeFromLeaveApply(LVTypeCode).Rows[0][0].ToString();
        //}
        /// <summary>
        /// 根據ID查詢請假信息
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="privileged">組織權限</param>
        /// <param name="sqlDep">權限管控</param>
        /// <returns>請假信息</returns>
        public DataTable getLeaveApplyData(string billNo, bool privileged, string sqlDep)
        {
            return DAL.getLeaveApplyData(billNo, privileged, sqlDep);
        }
        /// <summary>
        /// 根據性別獲取請假類型
        /// </summary>
        /// <param name="sexCode">性別代碼</param>
        /// <returns>請假類型</returns>
        public DataTable getLVTypeBySexCode(string sexCode)
        {
            return DAL.getLVTypeBySexCode(sexCode);
        }
        /// <summary>
        /// 獲取請假類別信息
        /// </summary>
        /// <param name="lVTypeCode">請假類別代碼</param>
        /// <returns>請假類別信息</returns>
        public DataTable getLVTypeByLVTypeCode(string LVTypeCode)
        {
            return DAL.getLVTypeByLVTypeCode(LVTypeCode);
        }
        /// <summary>
        /// 獲取請假信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>請假信息</returns>
        public DataTable getLeaveApply(string id)
        {
            return DAL.getLeaveApply(id);
        }
        /// <summary>
        /// 重新計算考勤結果
        /// </summary>
        /// <param name="WorkNo">工號</param>
        /// <param name="orgCode">部門代碼</param>
        /// <param name="StartDate">開始日期</param>
        /// <param name="EndDate">結束日期</param>
        public void getKaoQinData(string WorkNo, string orgCode, string StartDate, string EndDate)
        {
            DAL.getKaoQinData(WorkNo, orgCode, StartDate, EndDate);
        }
        /// <summary>
        /// 上傳證明文件
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="fileName">文件名</param>
        /// <param name="updateUser">上傳人</param>
        /// <param name="logmodel">操作日誌</param>
        public void Testify(string id, string fileName, string updateUser, SynclogModel logmodel)
        {
            DAL.Testify(id, fileName, updateUser, logmodel);
        }
        /// <summary>
        /// 上傳附件
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="fileName">文件名</param>
        /// <param name="updateUser">上傳人</param>
        /// <param name="logmodel">操作日誌</param>
        public void leaveUploadFile(string id, string fileName, string updateUser, SynclogModel logmodel)
        {
            DAL.leaveUploadFile(id, fileName, updateUser, logmodel);
        }
        /// <summary>
        /// 組織送簽
        /// </summary>
        /// <param name="processFlag">新增修改標誌</param>
        /// <param name="diry">送簽信息</param>
        /// <param name="BillNoType">表單類型</param>
        /// <param name="BillTypeCode">表單類型代碼</param>
        /// <param name="person">送簽人</param>
        /// <returns>送簽成功筆數</returns>
        public int SaveOrgAuditData(string Flow_LevelRemark,string processFlag, Dictionary<string, List<string>> dicy, string BillNoType, string BillTypeCode, string person)
        {
            return DAL.SaveOrgAuditData(Flow_LevelRemark,processFlag, dicy, BillNoType, BillTypeCode, person);
        }
        /// <summary>
        /// 送簽代理人
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="workNo">工號</param>
        /// <param name="logmodel">操作日誌</param>
        public void changeProxyStatus(string strID, string workNo, SynclogModel logmodel)
        {
            DAL.changeProxyStatus(strID, workNo, logmodel);
        }
        /// <summary>
        /// 代理人工號是否存在
        /// </summary>
        /// <param name="proxy">代理人工號</param>
        /// <returns>代理人工號是否存在</returns>
        public bool IsProxyExists(string proxy)
        {
            return DAL.IsProxyExists(proxy).Rows.Count > 0;
        }
        /// <summary>
        /// 獲取流程定義中的請假天數、資位、管理職信息
        /// </summary>
        /// <param name="startDate">開始日期</param>
        /// <param name="endDate">結束日期</param>
        /// <param name="empNo">工號</param>
        /// <returns>流程定義中的請假天數、資位、管理職信息</returns>
        public string getAuditType(string startDate, string endDate, string empNo, string lvTotalDays)
        {
            return DAL.getAuditType(startDate, endDate, empNo, lvTotalDays);
        }
    }
}
