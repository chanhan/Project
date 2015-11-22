using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Data;
using GDSBG.MiABU.Attendance.Model.WorkFlow;
using System.Collections;
using GDSBG.MiABU.Attendance.Common.Attributes;
using System.Data.OleDb;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;


namespace GDSBG.MiABU.Attendance.IDAL.WorkFlow
{
     //<summary>
     //D:\Attendance_New\Attendance\Attendance.IDAL\WorkFlow\IWFProjectApplyDal.cs
     //</summary>
    [RefClass("WorkFlow.WFProjectApplyDal")]
    public interface IWFProjectApplyDal
    {
        #region
         ///<summary>
         ///專案加班預報
         ///</summary>
         ///<param name="adcon"></param>
         ///<param name="pageIndex"></param>
         ///<param name="pageSize"></param>
         ///<param name="totalCount"></param>
        /// <returns></returns>
        DataTable GetWorkFlowApplyData(string condition, int pageIndex, int pageSize, out int totalCount);
        #endregion

        DataSet GetApplyDataByCondition(string condition);

        DataSet GetDataByCondition(string condition);

        DataSet GetVDataByCondition(string condition);

        DataSet GetDataByCondition_1(string condition);

        //DataSet GetByCondition(string condition);

        bool DeleteData(DataTable dataTable,SynclogModel logmodel);

        string GetEmpWorkFlag(string sWorkNo, string sDate);

        string GetShiftNo(string sWorkNo, string sDate);

        string GetValue(string sql);

        string GetAddValueDataList(string condition);

        SortedList ReturnOTTTime(string WorkNo, string OTDate, DateTime dtTempBeginTime, DateTime dtTempEndTime, string ShiftNo);

        string GetOTMSGFlag(string WorkNo, string OtDate, double OtHours, string OTType, string IsProject, string ModifyID);

        void SaveData(string processFlag, DataTable dataTable, SynclogModel logmodel);

        DataSet GetMonthAllOverTime(string condition);

        DataSet GetDataSetBySQL(string sql);

        string GetOTType(string sWorkNo, string sDate);

        //SortedList RunProc(OleDbParameter[] param, string sProcName);

        void Audit(DataTable dataTable, string appUser, SynclogModel logmodel);

        void CancelAudit(DataTable dataTable, SynclogModel logmodel);

        string GetWorkFlowOrgCode(string OrgCode, string BillTypeCode, string OTType);

        string SaveAuditData(string processFlag, string ID, string BillNoType, string AuditOrgCode, string BillTypeCode, string ApplyMan, string otmtype, string senduser, SynclogModel logmodel);

        int SaveOrgAuditData(string processFlag, Dictionary<string, List<string>> diry, string BillNoType, string BillTypeCode, string ApplyMan, SynclogModel logmodel);

        DataSet GetRealDataByCondition(string condition);

        double GetOtHours(string WorkNo, string OTDate, string StrBtime, string StrEtime, string OTType);

        string GetOTMFlag(string WorkNo, string OtDate);

        DataSet GetFixedDataByCondition(string condition);

        DataView ExceltoDataView(string strFilePath);

        int GetVWorkNoCount(string WorkNo, string sqlDep);

        DataTable GetEmpinfo(string empno);

    }
}
