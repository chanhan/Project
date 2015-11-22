/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： KQMSelfServiceBll.cs
 * 檔功能描述：員工自助查詢 功能模組業務邏輯類
 * 
 * 版本：1.0
 * 創建標識： 陈函 2012.02.03
 * 
 */
using System.Data;
using GDSBG.MiABU.Attendance.IDAL.KQM.Query;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;

namespace GDSBG.MiABU.Attendance.BLL.KQM.Query
{
    public class KQMSelfServiceBll : BLLBase<IKQMSelfServiceDal>
    {
        /// <summary>
        /// 獲取人員信息
        /// </summary>
        /// <param name="workNo">工號</param>
        /// <param name="sqlDep"></param>
        /// <param name="privileged">是否有組織權限</param>
        /// <returns>人員信息</returns>
        public DataTable getEmpInfo(string workNo, string sqlDep, bool privileged)
        {
            return DAL.getEmpInfo(workNo, sqlDep, privileged);
        }
        /// <summary>
        /// 獲取鞋櫃位置
        /// </summary>
        /// <param name="workNo">工號</param>
        /// <returns>鞋櫃位置</returns>
        public DataTable getPlaceName(string workNo)
        {
            return DAL.getPlaceName(workNo);
        }
        /// <summary>
        /// 獲取有效卡鐘
        /// </summary>
        /// <param name="workNo">工號</param>
        /// <returns>有效卡鐘</returns>
        public DataTable getEffectBellNo(string workNo)
        {
            return DAL.getEffectBellNo(workNo);
        }

        /// <summary>
        /// 獲取有效卡鐘
        /// </summary>
        /// <param name="workNo">工號</param>
        /// <returns>有效卡鐘</returns>
        public DataTable getEffectBellNo2(string workNo)
        {
            return DAL.getEffectBellNo2(workNo);
        }

        /// <summary>
        /// 獲取當月考勤異常明細
        /// </summary>
        /// <param name="workNo">工號</param>
        /// <returns>當月考勤異常明細</returns>
        public DataTable getKaoQinData(string workNo)
        {
            return DAL.getKaoQinData(workNo);
        }

        /// <summary>
        /// 獲取當月曠工累計數
        /// </summary>
        /// <param name="workNo">工號</param>
        /// <param name="kqDate">考勤日期</param>
        /// <returns>當月曠工累計數</returns>
        public DataTable getAbsentTotal(string workNo, string kqDate)
        {
            return DAL.getAbsentTotal(workNo, kqDate);
        }

        /// <summary>
        /// 獲取加扣分項查詢結果
        /// </summary>
        /// <param name="workNo">工號</param>
        /// <returns>加扣分項查詢結果</returns>
        public DataTable getScoreItemData(string workNo)
        {
            return DAL.getScoreItemData(workNo);
        }

        /// <summary>
        /// 獲取請假統計查詢結果
        /// </summary>
        /// <param name="workNo">工號</param>
        /// <returns>請假統計查詢結果</returns>
        public DataTable getLeaveReportData(string workNo)
        {
            return DAL.getLeaveReportData(workNo);
        }

        /// <summary>
        /// 計算請假統計
        /// </summary>
        /// <param name="workNo">工號</param>
        public void GetEmpLeaveReport(string workNo, SynclogModel logmodel)
        {
            DAL.GetEmpLeaveReport(workNo, logmodel);
        }

        /// <summary>
        /// 計算未來7天排班 
        /// </summary>
        /// <param name="workNo">工號</param>
        public void GetEmployeeShift(string workNo, SynclogModel logmodel)
        {
            DAL.GetEmployeeShift(workNo, logmodel);
        }

        /// <summary>
        /// 獲取未來7天排班查詢結果
        /// </summary>
        /// <param name="workNo">工號</param>
        /// <returns>未來7天排班查詢結果</returns>
        public DataTable getWorkShiftData(string workNo)
        {
            return DAL.getWorkShiftData( workNo);
        }

        /// <summary>
        /// 獲取當年請假明細查詢結果
        /// </summary>
        /// <param name="workNo">工號</param>
        /// <returns>當年請假明細查詢結果</returns>
        public DataTable getLeaveDetailData(string workNo)
        {
            return DAL.getLeaveDetailData(workNo);
        }

        /// <summary>
        /// 獲取加班匯總查詢結果
        /// </summary>
        /// <param name="workNo">工號</param>
        /// <returns>加班匯總查詢結果</returns>
        public DataTable getOTMMonthTotalData(string workNo)
        {
            return DAL.getOTMMonthTotalData(workNo);
        }

        /// <summary>
        /// 獲取加班明細查詢結果
        /// </summary>
        /// <param name="workNo">工號</param>
        /// <returns>加班明細查詢結果</returns>
        public DataTable getOTMonthDetailData(string workNo)
        {
            return DAL.getOTMonthDetailData(workNo);
        }

        public DataTable getKaoQinData(string workNo, string year)
        {
            return DAL.getKaoQinData(workNo, year);            
        }

        public DataTable getdataTableJiangChengData(string workNo, string year)
        {
            return DAL.getdataTableJiangChengData(workNo, year);            
        }

        public DataTable getdataTableIEData(string workNo, string year)
        {
            return DAL.getdataTableIEData(workNo, year);                      
        }

        public DataTable getdataTableStudyData(string workNo, string year)
        {
            return DAL.getdataTableStudyData(workNo, year);                      
        }
    }
}
