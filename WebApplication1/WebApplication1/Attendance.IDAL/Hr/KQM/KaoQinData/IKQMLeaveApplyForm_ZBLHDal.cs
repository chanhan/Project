/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： IKQMLeaveApplyForm_ZBLHDal.cs
 * 檔功能描述： 請假申請數據接口類
 * 
 * 版本：1.0
 * 創建標識： 陳函 2012.3.10
 * 
 */
using System;
using System.Collections.Generic;
using System.Text;
using GDSBG.MiABU.Attendance.Common.Attributes;
using System.Data;
using System.Collections;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;
using GDSBG.MiABU.Attendance.Model.KQM.KaoQinData;

namespace GDSBG.MiABU.Attendance.IDAL.Hr.KQM.KaoQinData
{
    [RefClass("Hr.KQM.KaoQinData.KQMLeaveApplyForm_ZBLHDal")]
   public interface IKQMLeaveApplyForm_ZBLHDal
    {
        /// <summary>
        /// 獲取請假類型
        /// </summary>
        /// <returns>請假類型</returns>
        DataTable getLeaveType();
        /// <summary>
        /// 獲取表單狀態
        /// </summary>
        /// <returns>表單狀態</returns>
        DataTable getStatus();
        /// <summary>
        /// 獲取申請類型
        /// </summary>
        /// <returns>申請類型</returns>
        DataTable getApplyType();
        /// <summary>
        /// 請假是否需要簽核
        /// </summary>
        /// <returns>請假是否需要簽核</returns>
        DataTable isLeaveNoAudit();
        //DataTable CheckDateMonths(string startDate, string endDate);
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
        DataTable getApplyData(bool Privileged, string SqlDep, string depName, string billNo, string workNo, string localName, string LVTypeCode, string status, string testify, string startDate, string endDate, string applyStartDate, string applyEndDate, string applyType, bool flag, string IsLastYear, int currentPageIndex, int pageSize, out int totalCount);

        //DataTable getThisLVTotal(string sID);

        //DataTable getTLVWorkDays(string sID);
        /// <summary>
        /// 根據工號查詢員工基本信息
        /// </summary>
        /// <param name="IsPrivileged">是否有組織權限</param>
        /// <param name="sqlDep">權限管控</param>
        /// <param name="empNo">工號</param>
        /// <returns>員工基本信息</returns>
        DataTable getEmpInfo(bool IsPrivileged, string sqlDep, string empNo);
        /// <summary>
        /// 統計每天加班時數,每月加班時數
        /// </summary>
        /// <param name="empNo">工號</param>
        void CountCanAdjlasthy(string empNo);
        /// <summary>
        /// 獲取員工性別
        /// </summary>
        /// <param name="empNo">工號</param>
        /// <returns>員工性別</returns>
        DataTable getSexCode(string empNo);
        /// <summary>
        /// 獲取員工入職時數
        /// </summary>
        /// <param name="empNo">工號</param>
        /// <returns>員工入職時數</returns>
        DataTable getInWorkYears(string empNo);
        /// <summary>
        /// 獲取非晚婚假限制天數
        /// </summary>
        /// <returns></returns>
        DataTable getLimitdays();
        /// <summary>
        /// 獲取員工入職時數
        /// </summary>
        /// <param name="empNo">工號</param>
        /// <returns>員工入職時數</returns>
        DataTable getAges(string empNo);
        /// <summary>
        /// 獲取婚假限制天數
        /// </summary>
        /// <returns>婚假限制天數</returns>
        DataTable getJLimitdays();
        /// <summary>
        ///按工號獲取婚假限制天數
        /// </summary>
        /// <param name="empNo">工號</param>
        /// <returns></returns>
        DataTable getSpecLimitDays(string empNo);
        /// <summary>
        /// 獲取請假類別數
        /// </summary>
        /// <param name="sexCode">性別代碼</param>
        /// <returns>請假類別數據</returns>
        DataTable getLeaveTypeCount(string sexCode);

        /// <summary>
        /// 根據性別獲取請假類別
        /// </summary>
        /// <param name="sexCode">性別代碼</param>
        /// <returns>請假類別</returns>
        DataTable getDataByCondition(string sexCode);
        /// <summary>
        /// 獲取請假總時數
        /// </summary>
        /// <param name="empNo">工號</param>
        /// <returns>請假總時數</returns>
        DataTable getLVTotal(string empNo);
        /// <summary>
        /// 獲取限制天數
        /// </summary>
        /// <param name="empNo">工號</param>
        /// <param name="LVTypeCode">類別代碼</param>
        /// <returns></returns>
        DataTable getLimitDays(string empNo, string LVTypeCode);
        /// <summary>
        /// 獲取年度依年資應修年假天數
        /// </summary>
        /// <param name="empNo">工號</param>
        /// <param name="reportYear">當前年份</param>
        /// <param name="applyDate">當前日期</param>
        /// <returns></returns>
        string getYearLeaveDays(string empNo, string reportYear, string applyDate);
        /// <summary>
        /// 獲取當月可調時數
        /// </summary>
        /// <param name="empNo">工號</param>
        /// <param name="LVTotal">請假類別</param>
        /// <returns>當月可調時數</returns>
        DataTable getTime(string empNo, double LVTotal);
        /// <summary>
        /// 獲取請假總時數
        /// </summary>
        /// <param name="LVTotal">已修總時數</param>
        /// <param name="empNo">工號</param>
        /// <param name="typeCode">請假類別</param>
        /// <param name="IsUTypeCode">是否為調休</param>
        /// <returns>請假總時數</returns>
        DataTable getSumlvTotal(double LVTotal, string empNo, string typeCode, bool IsUTypeCode);
        /// <summary>
        /// 是否結婚管控假別
        /// </summary>
        /// <returns>是否結婚管控假別</returns>
        DataTable getMarryFlag();
        /// <summary>
        /// 是否為非年假
        /// </summary>
        /// <returns>是否為非年假</returns>
        DataTable getNoYearHoliday();
        /// <summary>
        /// 獲取試用員工或預師工號
        /// </summary>
        /// <param name="employeeNo">工號</param>
        /// <returns>試用員工或預師工號</returns>
        DataTable getWorkNo(string employeeNo);
        /// <summary>
        /// 獲取請假類別
        /// </summary>
        /// <param name="marryFlag">是否已婚</param>
        /// <param name="noYearHolidayFlag">是否為年休假</param>
        /// <returns>請假類別</returns>
        DataTable getLVType(bool marryFlag, bool noYearHolidayFlag);
        /// <summary>
        /// 獲取員工資位
        /// </summary>
        /// <returns>員工資位</returns>
        DataTable getLevelCode();
        /// <summary>
        /// 查詢請假申請是否拒簽
        /// </summary>
        /// <param name="workNo">工號</param>
        /// <param name="typeCode">請假類別代碼</param>
        /// <returns>是否拒簽結果</returns>
        DataTable checkRefuseApply(string workNo, string typeCode);
        /// <summary>
        /// 獲取請假總時數
        /// </summary>
        /// <param name="workNo">工號</param>
        /// <param name="startDate">開始日期</param>
        /// <param name="endDate">結束日期</param>
        /// <param name="typeCode">請假類別代碼</param>
        /// <returns>請假總時數</returns>
        string getLVTotal(string workNo, string startDate, string endDate, string typeCode);
        /// <summary>
        /// 獲取員工班別
        /// </summary>
        /// <param name="workNo">工號</param>
        /// <param name="startDate">開始日期</param>
        /// <returns>員工班別</returns>
        string getLVTotal(string workNo, string startDate);
        /// <summary>
        /// 獲取允許申請的資位
        /// </summary>
        /// <param name="lvTypeCode">請假類別</param>
        /// <returns>允許申請的資位</returns>
        DataTable getAllowDepLevel(string lvTypeCode);
        /// <summary>
        /// 獲取離職日期
        /// </summary>
        /// <param name="empNo">工號</param>
        /// <returns>離職日期</returns>
        DataTable getLeaveDate(string empNo);
        /// <summary>
        /// 獲取授權功能列表
        /// </summary>
        /// <param name="roleCode">角色代碼</param>
        /// <param name="moduleCode">模組代碼</param>
        /// <returns>授權功能列表</returns>
        DataTable getAuthorizedFunctionList(string roleCode, string moduleCode);
        /// <summary>
        /// 請假是否需要簽核
        /// </summary>
        /// <returns> 請假是否需要簽核</returns>
        DataTable getParaValue();
        /// <summary>
        /// 獲取假別的最小時數和標準時數
        /// </summary>
        /// <param name="LVType">假別代碼</param>
        /// <returns>假別的最小時數和標準時數</returns>
        DataTable getHours(string LVType);
        /// <summary>
        /// 根據ID獲取請假信息
        /// </summary>
        /// <param name="ID">請假ID</param>
        /// <returns>請假信息</returns>
        DataTable getDataByBillNo(string billNO);
        /// <summary>
        /// 是否允許調休
        /// </summary>
        /// <returns>是否允許調休</returns>
        DataTable getRestChangeHours();
        /// <summary>
        /// 新增修改請假信息
        /// </summary>
        /// <param name="processFlag">新增修改標誌</param>
        /// <param name="tempDataTable">請假信息</param>
        /// <param name="logmodel">操作日誌</param>
        /// <returns>請假ID</returns>
        string SaveData(string processFlag, DataTable tempDataTable, SynclogModel logmodel);
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
        bool CheckLvtotal(string ProcessFlag, string workNo, string lvType, string strDate, string sTotal, string BillNo, string ImportType);
        /// <summary>
        /// 獲取當月可調時數
        /// </summary>
        /// <param name="ProcessFlag">新增修改標誌</param>
        /// <param name="workNo">工號</param>
        /// <param name="strDate">開始日期</param>
        /// <param name="ID">ID</param>
        /// <returns>獲取當月可調時數</returns>
        double CheckLvtotal(string processFlag, string empNo, string startDate, string ID);
        /// <summary>
        /// 判斷請假日期是否有重複天數是否
        /// </summary>
        /// <param name="empNo">工號</param>
        /// <param name="startDate">開始日期</param>
        /// <param name="startTime">開始時間</param>
        /// <param name="endDate">結束日期</param>
        /// <param name="endTime">結束時間</param>
        /// <returns>請假日期是否有重複天數是否</returns>
        bool CheckLeaveOverTime(string empNo, string startDate, string startTime, string endDate, string endTime);
        /// <summary>
        ///計算請假總時長
        /// </summary>
        /// <param name="LeaveID">ID</param>
        void getLeaveDetail(string LeaveID);
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
        bool CheckLeaveOverTime(string WorkNo, string StartDate, string StartTime, string EndDate, string EndTime, string BillNo);
        /// <summary>
        /// 獲取每月多少日之後不允許重新計算上月及以前考勤數據
        /// </summary>
        /// <returns>天數</returns>
        DataTable getKqoQinDays();
        /// <summary>
        /// 獲取未核准或拒簽的請假信息
        /// </summary>
        /// <param name="ID">ID</param>
        /// <returns>請假信息</returns>
        DataTable getLeaveApplyData(string ID);
        /// <summary>
        /// 刪除請假信息
        /// </summary>
        /// <param name="tempDataTable">請假信息</param>
        /// <param name="logmodel">操作日誌</param>
        void DeleteData(DataTable tempDataTable, SynclogModel logmodel);
        /// <summary>
        /// 導入
        /// </summary>
        /// <param name="createUser">登錄人工號</param>
        /// <param name="moduleCode">模組代碼</param>
        /// <param name="successnum">成功筆數</param>
        /// <param name="errornum">失敗筆數</param>
        /// <param name="logmodel">操作日誌</param>
        /// <returns>錯誤記錄</returns>
        DataTable ImpoertExcel(string createUser, string moduleCode,out int successnum, out int errornum, SynclogModel logmodel);
        /// <summary>
        /// 將DataTable轉換為List用於導出
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <returns>List</returns>
        List<LeaveApplyTempModel> changList(DataTable dt);
        /// <summary>
        /// 核准表單
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="approver">核准人</param>
        /// <param name="approvedate">核准日期</param>
        /// <param name="status">表單狀態</param>
        /// <param name="logmodel">操作日誌</param>
        void changeStatusByID(string id, string approver, string approvedate, string status, SynclogModel logmodel);
        /// <summary>
        /// 取消核准
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="status">表單狀態</param>
        /// <param name="logmodel">操作日誌</param>
        void changeStatusByID(string id, string status, string proxyStatus,SynclogModel logmodel);

        //DataTable getBillTypeCode(string LVTypeCode);

        //DataTable getAppTypeToBillConfigData(string LVTypeCode);
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
        DataTable getWorkFlowOrgCode(string OrgCode, string BillTypeCode, string lvType, string startDate, string endDate, string empNo,string lvTotalDays);
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
        bool SaveAuditData(string sFlow_LevelRemark, string ID, string BillNoType, string AuditOrgCode, string BillTypeCode, string workNo, string reason, string empNo, string startDate, string endDate, string lvtypeCode, SynclogModel logmodel);

        //DataTable getBillTypeCodeFromLeaveApply(string LVTypeCode);
        /// <summary>
        /// 根據ID查詢請假信息
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="privileged">組織權限</param>
        /// <param name="sqlDep">權限管控</param>
        /// <returns>請假信息</returns>
        DataTable getLeaveApplyData(string billNo, bool privileged, string sqlDep);
        /// <summary>
        /// 根據性別獲取請假類型
        /// </summary>
        /// <param name="sexCode">性別代碼</param>
        /// <returns>請假類型</returns>
        DataTable getLVTypeBySexCode(string sexCode);
        /// <summary>
        /// 獲取請假類別信息
        /// </summary>
        /// <param name="lVTypeCode">請假類別代碼</param>
        /// <returns>請假類別信息</returns>
        DataTable getLVTypeByLVTypeCode(string LVTypeCode);
        /// <summary>
        /// 獲取請假信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>請假信息</returns>
        DataTable getLeaveApply(string id);
        /// <summary>
        /// 重新計算考勤結果
        /// </summary>
        /// <param name="WorkNo">工號</param>
        /// <param name="orgCode">部門代碼</param>
        /// <param name="StartDate">開始日期</param>
        /// <param name="EndDate">結束日期</param>
        void getKaoQinData(string WorkNo, string orgCode, string StartDate, string EndDate);
        /// <summary>
        /// 上傳證明文件
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="fileName">文件名</param>
        /// <param name="updateUser">上傳人</param>
        /// <param name="logmodel">操作日誌</param>
        void Testify(string id, string fileName,string updateUser,SynclogModel logmodel);
        /// <summary>
        /// 上傳附件
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="fileName">文件名</param>
        /// <param name="updateUser">上傳人</param>
        /// <param name="logmodel">操作日誌</param>
        void leaveUploadFile(string id, string fileName, string updateUser,SynclogModel logmodel);
        /// <summary>
        /// 組織送簽
        /// </summary>
        /// <param name="processFlag">新增修改標誌</param>
        /// <param name="diry">送簽信息</param>
        /// <param name="BillNoType">表單類型</param>
        /// <param name="BillTypeCode">表單類型代碼</param>
        /// <param name="person">送簽人</param>
        /// <returns>送簽成功筆數</returns>
        int SaveOrgAuditData(string Flow_LevelRemark,string processFlag, Dictionary<string, List<string>> dicy, string BillNoType, string BillTypeCode, string person);

        /// <summary>
        /// 送簽代理人
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="workNo">工號</param>
        /// <param name="logmodel">操作日誌</param>
        void changeProxyStatus(string strID, string workNo,SynclogModel logmodel);
        /// <summary>
        /// 代理人工號是否存在
        /// </summary>
        /// <param name="proxy">代理人工號</param>
        /// <returns>代理人工號是否存在</returns>
        DataTable IsProxyExists(string proxy);
        /// <summary>
        /// 獲取流程定義中的請假天數、資位、管理職信息
        /// </summary>
        /// <param name="startDate">開始日期</param>
        /// <param name="endDate">結束日期</param>
        /// <param name="empNo">工號</param>
        /// <returns>流程定義中的請假天數、資位、管理職信息</returns>
        string getAuditType(string startDate, string endDate, string empNo, string lvTotalDays);
    }
}
