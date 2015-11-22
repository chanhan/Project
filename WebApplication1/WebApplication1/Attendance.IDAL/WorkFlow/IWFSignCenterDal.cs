using System;
using System.Collections.Generic;
using System.Text;
using GDSBG.MiABU.Attendance.Common.Attributes;
using System.Data;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;

namespace GDSBG.MiABU.Attendance.IDAL.WorkFlow
{
    [RefClass("WorkFlow.WFSignCenterDal")]
    public interface IWFSignCenterDal
    {

        void SaveBatchAuditData(string BillNo, string AuditMan, string BillTypeCode, SynclogModel logmodel);

        DataSet GetAuditCenterDataByCondition(string condition);

        string GetAllAuditDept(string sDepCode);

        DataSet ExcuteSQL(string sql);

        void SaveDisAuditData(string BillNo, string AuditMan, string BillTypeCode, string ApRemark, List<string> DisSignRamark, List<string> Id, SynclogModel logmodel);
        /// <summary>
        /// 重載拒簽動作的方法---made by ziyan 
        /// </summary>
        /// <param name="BillNo">單號</param>
        /// <param name="AuditMan">簽核人（登錄人）</param>
        /// <param name="BillTypeCode">單據類型 （月加班匯總）</param>
        /// <param name="ApRemark">簽核意見</param>
        /// <param name="DisSignRamark">拒簽意見</param>
        /// <param name="YearMonth">年月（主鍵一）</param>
        /// <param name="WorkNo">工號（主鍵二）</param>
        void SaveDisAuditData(string BillNo, string AuditMan, string BillTypeCode, string ApRemark, List<string> DisSignRamark, List<string> YearMonth, List<string> WorkNo, SynclogModel logmodel);

        void SaveAuditData(string BillNo, string AuditMan, string BillTypeCode, string ApRemark, string condition, List<string> DisSignRamark, List<string> Id, SynclogModel logmodel);
        /// <summary>
        /// 重載同意動作的方法---made by ziyan 
        /// </summary>
        /// <param name="BillNo">單號</param>
        /// <param name="AuditMan">簽核人（登錄人）</param>
        /// <param name="BillTypeCode">單據類型 （月加班匯總）</param>
        /// <param name="ApRemark">簽核意見</param>
        /// <param name="DisSignRamark">拒簽意見</param>
        /// <param name="YearMonth">年月（主鍵一）</param>
        /// <param name="WorkNo">工號（主鍵二）</param>
        void SaveAuditData(string BillNo, string AuditMan, string BillTypeCode, string ApRemark, string condition, List<string> DisSignRamark, List<string> WorkNo, List<string> YearMonth, SynclogModel logmodel);

        string GetValue(string sql);

        DataSet GetDataSetBySQL(string sql);

        DataSet GetAuditDataByCondition(string condition);

        DataSet GetDataByCondition(string condition);
        /// <summary>
        /// 獲取單據類型
        /// </summary>
        /// <param name="billType">單據類型簡寫代碼</param>
        /// <returns></returns>
        DataTable GetBillTypeCode(string billType);

        string GetAllDept(string sDepCode, bool bParent);

        DataSet GetDataByCondition_Bill(string condition);

        DataSet GetApprovedDataByCondition(string condition, int pageindex, int pagesize, out int totalcount);

        //DataSet GetApprovedDataByCondition(string condition, int pageSize, ref int out_CurrentPage, ref int out_TotalPage, ref int out_TotalRecords);

        DataSet GetDataByCondition_HRM(string condition);

    }
}
