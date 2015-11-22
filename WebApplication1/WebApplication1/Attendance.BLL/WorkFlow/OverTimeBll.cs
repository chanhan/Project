using System;
using System.Collections.Generic;
using System.Text;
using GDSBG.MiABU.Attendance.IDAL.WorkFlow;
using GDSBG.MiABU.Attendance.Model.WorkFlow;
using System.Data;
using System.Resources;
using System.Collections;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;

namespace GDSBG.MiABU.Attendance.BLL.WorkFlow
{

    public class OverTimeBll : BLLBase<IOverTimeDal>
    {

        public bool SaveData(List<OverTimeModel> list, string states, string id)
        {
            return DAL.SaveData(list, states, id);
        }

        /// <summary>
        /// 獲取加班預提信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataTable GetOverTimeInfo(string id)
        {
            return DAL.GetOverTimeInfo(id);
        }

        public DataTable GetEmployees(string condition, string KQDate)
        {
            return DAL.GetEmployees(condition, KQDate);
        }

        /// <summary>
        /// 查詢排班信息
        /// </summary>
        /// <param name="depcode">部門代碼</param>
        /// <param name="shiftdate">排班日期</param>
        /// <param name="BatchEmployeeNo">批量查詢時傳入的工號</param>
        /// <param name="overtimtype">加班類別</param>
        /// <param name="sqlDep">管控代碼</param>
        /// <returns></returns>
        public DataTable GetShiftInfo(string depcode, string shiftdate, string BatchEmployeeNo, string overtimtype, string sqlDep)
        {
            return DAL.GetShiftInfo(depcode, shiftdate, BatchEmployeeNo, overtimtype, sqlDep);
        }

        public string GetEmpOrgShift(string OrgCode, string KQDate)
        {
            return DAL.GetEmpOrgShift(OrgCode, KQDate);
        }

        public string GetValue(string sql)
        {
            return DAL.GetValue(sql);
        }

        public DataSet GetDataByCondition(string condition)
        {
            return DAL.GetDataByCondition(condition);
        }

        public DataSet GetDataByCondition_1(string condition)
        {
            return DAL.GetDataByCondition_1(condition);
        }

        public DataSet GetDataByCondition_pager(string condition, int pageIndex, int pageSize, out int totalCount)
        { 
            return DAL.GetDataByCondition_pager( condition,  pageIndex,  pageSize, out  totalCount);
        }

        public string GetOTType(string sWorkNo, string sDate)
        {
            return DAL.GetOTType(sWorkNo, sDate);
        }

        public string GetShiftNo(string sWorkNo, string sDate)
        {
            return DAL.GetShiftNo(sWorkNo, sDate);
        }

        public bool DeleteData(DataTable dataTable, SynclogModel logmodel)
        {
            return DAL.DeleteData(dataTable, logmodel);
        }

        public double GetOtHours(string WorkNo, string OTDate, string StrBtime, string StrEtime, string OTType)
        {
            return DAL.GetOtHours(WorkNo, OTDate, StrBtime, StrEtime, OTType);
        }


        public string GetOTMSGFlag(string WorkNo, string OtDate, double OtHours, string OTType, string IsProject, string ModifyID, ResourceManager rm)
        {
            return DAL.GetOTMSGFlag(WorkNo, OtDate, OtHours, OTType, IsProject, ModifyID, rm);
        }

        public string GetOTMSGFlag(string WorkNo, string OtDate, double OtHours, string OTType, string IsProject, string ModifyID)
        {
            return DAL.GetOTMSGFlag(WorkNo, OtDate, OtHours, OTType, IsProject, ModifyID);
        }

        public DataView ExceltoDataView(string strFilePath)
        {
            return DAL.ExceltoDataView(strFilePath);
        }

        public int GetVWorkNoCount(string WorkNo, string sqlDep)
        {
            return DAL.GetVWorkNoCount(WorkNo, sqlDep);
        }

        public SortedList ReturnOTTTime(string WorkNo, string OTDate, DateTime dtTempBeginTime, DateTime dtTempEndTime, string ShiftNo)
        {
            return DAL.ReturnOTTTime(WorkNo, OTDate, dtTempBeginTime, dtTempEndTime, ShiftNo);
        }

        public DataSet GetDataSetBySQL(string sql)
        {
            return DAL.GetDataSetBySQL(sql);
        }

        public void SaveData(string processFlag, DataTable dataTable, SynclogModel logmodel)
        {
             DAL.SaveData(processFlag, dataTable,logmodel);
        }

        public void Audit(DataTable dataTable, string workno, SynclogModel logmodel)
        {
            DAL.Audit(dataTable, workno,logmodel);
        }

        public void CancelAudit(DataTable dataTable, SynclogModel logmodel)
        {
            DAL.CancelAudit(dataTable,logmodel);
        }

        public string GetWorkFlowOrgCode(string OrgCode, string BillTypeCode, string reason1)
        {
            return DAL.GetWorkFlowOrgCode(OrgCode, BillTypeCode, reason1);
        }

        public DataSet GetMonthAllOverTime(string condition)
        {
            return DAL.GetMonthAllOverTime(condition);
        }

        public DataTable GetEmpinfo(string empno)
        {
            return DAL.GetEmpinfo(empno);
        }

        public DataSet GetVDataByCondition(string condition)
        {
            return DAL.GetVDataByCondition(condition);
        }

        public int SaveOrgAuditData(string processFlag, Dictionary<string, List<string>> diry, string BillNoType, string BillTypeCode, string Person, SynclogModel logmodel)
        {
            return DAL.SaveOrgAuditData(processFlag, diry, BillNoType, BillTypeCode, Person,logmodel);
        }

        public string SaveAuditData(string processFlag, string ID, string BillNoType, string AuditOrgCode, string BillTypeCode, string OTType, string Person, string senduser, SynclogModel logmodel)
        {
            return DAL.SaveAuditData(processFlag, ID, BillNoType, AuditOrgCode, BillTypeCode, OTType, Person, senduser,logmodel);
        }

        public DataSet GetDataByCondition_2(string condition)
        {
            return DAL.GetDataByCondition_2(condition);
        }

        public string GetEmpWorkFlag(string sWorkNo, string sDate)
        {
            return DAL.GetEmpWorkFlag(sWorkNo, sDate);
        }
    }
}
