/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： IKQMSelfServiceDal.cs
 * 檔功能描述：員工自助查詢功能模組接口類
 * 
 * 版本：1.0
 * 創建標識： 陈函 2012.02.03
 * 
 */
using System.Data;
using GDSBG.MiABU.Attendance.Common.Attributes;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;

namespace GDSBG.MiABU.Attendance.IDAL.KQM.Query
{
    [RefClass("KQM.Query.KQMSelfServiceDal")]
    public interface   IKQMSelfServiceDal
    {
        /// <summary>
        /// 獲取人員信息
        /// </summary>
        /// <param name="workNo">工號</param>
        /// <param name="sqlDep"></param>
        /// <param name="privileged">是否有組織權限</param>
        /// <returns>人員信息</returns>
        DataTable getEmpInfo(string workNo, string sqlDep, bool privileged);
        /// <summary>
        /// 獲取鞋櫃位置
        /// </summary>
        /// <param name="workNo">工號</param>
        /// <returns>鞋櫃位置</returns>
        DataTable getPlaceName(string workNo);
        /// <summary>
        /// 獲取有效卡鐘
        /// </summary>
        /// <param name="workNo">工號</param>
        /// <returns>有效卡鐘</returns>
        DataTable getEffectBellNo(string workNo);
        /// <summary>
        /// 獲取有效卡鐘
        /// </summary>
        /// <param name="workNo">工號</param>
        /// <returns>有效卡鐘</returns>
        DataTable getEffectBellNo2(string workNo);
        /// <summary>
        /// 獲取當月考勤異常明細
        /// </summary>
        /// <param name="workNo">工號</param>
        /// <returns>當月考勤異常明細</returns>
        DataTable getKaoQinData(string workNo);
        /// <summary>
        /// 獲取當月曠工累計數
        /// </summary>
        /// <param name="workNo">工號</param>
        /// <param name="kqDate">考勤日期</param>
        /// <returns>當月曠工累計數</returns>
        DataTable getAbsentTotal(string workNo, string kqDate);
        /// <summary>
        /// 獲取加扣分項查詢結果
        /// </summary>
        /// <param name="workNo">工號</param>
        /// <returns>加扣分項查詢結果</returns>
        DataTable getScoreItemData(string workNo);
        /// <summary>
        /// 獲取請假統計查詢結果
        /// </summary>
        /// <param name="workNo">工號</param>
        /// <returns>請假統計查詢結果</returns>
        DataTable getLeaveReportData(string workNo);
        /// <summary>
        /// 計算請假統計
        /// </summary>
        /// <param name="workNo">工號</param>
        void GetEmpLeaveReport(string workNo,SynclogModel logmodel);
        /// <summary>
        /// 計算未來7天排班 
        /// </summary>
        /// <param name="workNo">工號</param>
        void GetEmployeeShift(string workNo, SynclogModel logmodel);
        /// <summary>
        /// 獲取未來7天排班查詢結果
        /// </summary>
        /// <param name="workNo">工號</param>
        /// <returns>未來7天排班查詢結果</returns>
        DataTable getWorkShiftData(string workNo);
        /// <summary>
        /// 獲取當年請假明細查詢結果
        /// </summary>
        /// <param name="workNo">工號</param>
        /// <returns>當年請假明細查詢結果</returns>
        DataTable getLeaveDetailData(string workNo);
        /// <summary>
        /// 獲取加班匯總查詢結果
        /// </summary>
        /// <param name="workNo">工號</param>
        /// <returns>加班匯總查詢結果</returns>
        DataTable getOTMMonthTotalData(string workNo);
        /// <summary>
        /// 獲取加班明細查詢結果
        /// </summary>
        /// <param name="workNo">工號</param>
        /// <returns>加班明細查詢結果</returns>
        DataTable getOTMonthDetailData(string workNo);

        DataTable getKaoQinData(string workNo, string year);

        DataTable getdataTableJiangChengData(string workNo, string year);

        DataTable getdataTableIEData(string workNo, string year);

        DataTable getdataTableStudyData(string workNo, string year);
    }
}
