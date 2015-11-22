using System;
using System.Collections.Generic;
using System.Text;
using GDSBG.MiABU.Attendance.Common.Attributes;
using GDSBG.MiABU.Attendance.Model.WorkFlow;
using System.Data;
using System.Resources;
using System.Collections;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;

namespace GDSBG.MiABU.Attendance.IDAL.WorkFlow
{
    [RefClass("WorkFlow.OverTimeDal")]
    public interface IOverTimeDal
    {
        bool SaveData(List<OverTimeModel> list, string states,string id);

        DataTable GetOverTimeInfo(string id);

        DataTable GetEmployees(string condition, string KQDate);

        DataTable GetShiftInfo(string depcode, string shiftdate, string BatchEmployeeNo, string overtimtype, string sqlDep);

        string GetEmpOrgShift(string OrgCode, string KQDate);

        string GetValue(string sql);

        DataSet GetDataByCondition(string condition);

        DataSet GetDataByCondition_1(string condition);

        DataSet GetDataByCondition_pager(string condition, int pageIndex, int pageSize, out int totalCount);


        string GetOTType(string sWorkNo, string sDate);

        string GetShiftNo(string sWorkNo, string sDate);

        bool DeleteData(DataTable dataTable, SynclogModel logmodel);

        double GetOtHours(string WorkNo, string OTDate, string StrBtime, string StrEtime, string OTType);

        string GetOTMSGFlag(string WorkNo, string OtDate, double OtHours, string OTType, string IsProject, string ModifyID, ResourceManager rm);

        string GetOTMSGFlag(string WorkNo, string OtDate, double OtHours, string OTType, string IsProject, string ModifyID);

        DataView ExceltoDataView(string strFilePath);

        int GetVWorkNoCount(string WorkNo, string sqlDep);

        SortedList ReturnOTTTime(string WorkNo, string OTDate, DateTime dtTempBeginTime, DateTime dtTempEndTime, string ShiftNo);

        DataSet GetDataSetBySQL(string sql);

        void SaveData(string processFlag, DataTable dataTable, SynclogModel logmodel);

        void Audit(DataTable dataTable, string workno, SynclogModel logmodel);


        void CancelAudit(DataTable dataTable, SynclogModel logmodel);

        string GetWorkFlowOrgCode(string OrgCode, string BillTypeCode, string reason1);

        DataSet GetMonthAllOverTime(string condition);

        DataSet GetVDataByCondition(string condition);

        DataTable GetEmpinfo(string empno);

        int SaveOrgAuditData(string processFlag, Dictionary<string, List<string>> diry, string BillNoType, string BillTypeCode, string Person, SynclogModel logmodel);

        string SaveAuditData(string processFlag, string ID, string BillNoType, string AuditOrgCode, string BillTypeCode, string OTType, string Person,string senduser, SynclogModel logmodel);

        DataSet GetDataByCondition_2(string condition);

        string GetEmpWorkFlag(string sWorkNo, string sDate);
    }
}
