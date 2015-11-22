using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using GDSBG.MiABU.Attendance.IDAL.WorkFlow;
using GDSBG.MiABU.Attendance.Model.WorkFlow;
using System.Data.OleDb;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;


namespace GDSBG.MiABU.Attendance.BLL.WorkFlow
{
    public class WFProjectApplyBll : BLLBase<IWFProjectApplyDal>
    {
        public DataTable GetWFProjectApplyList(string condition, int pageIndex, int pageSize, out int totalCount)
        {
            return DAL.GetWorkFlowApplyData(condition, pageIndex, pageSize, out  totalCount);
        }

        public DataSet GetApplyDataByCondition(string condition)
        {
            return DAL.GetApplyDataByCondition(condition);
        }

        public DataSet GetDataByCondition(string condition)
        {
            return DAL.GetDataByCondition(condition);
        }

        public DataSet GetVDataByCondition(string condition)
        {
            return DAL.GetVDataByCondition(condition);
        }

        public DataSet GetDataByCondition_1(string condition)
        {
            return DAL.GetDataByCondition_1(condition);
        }

        public bool DeleteData(DataTable dataTable,SynclogModel logmodel)
        {
            return DAL.DeleteData(dataTable, logmodel);
        }

        public string GetEmpWorkFlag(string sWorkNo, string sDate)
        {
            return DAL.GetEmpWorkFlag(sWorkNo, sDate);
        }

        public string GetValue(string sql)
        {
            return DAL.GetValue(sql);
        }

        public string GetShiftNo(string sWorkNo, string sDate)
        {
            return DAL.GetShiftNo(sWorkNo, sDate);
        }

        public string GetAddCondValue(string condition)
        {
            return DAL.GetAddValueDataList(condition);
        }

        public SortedList ReturnOTTTime(string WorkNo, string OTDate, DateTime dtTempBeginTime, DateTime dtTempEndTime, string ShiftNo)
        {
            return DAL.ReturnOTTTime(WorkNo, OTDate, dtTempBeginTime, dtTempEndTime, ShiftNo);
        }

        public string GetOTMSGFlag(string WorkNo, string OtDate, double OtHours, string OTType, string IsProject, string ModifyID)
        {
            return DAL.GetOTMSGFlag(WorkNo, OtDate, OtHours, OTType, IsProject, ModifyID);
        }

        public void SaveData(string processFlag, DataTable dataTable, SynclogModel logmodel)
        {
            DAL.SaveData(processFlag, dataTable,logmodel);
        }

        public DataSet GetMonthAllOverTime(string condition)
        {
            return DAL.GetMonthAllOverTime(condition);
        }

        public DataSet GetDataSetBySQL(string sql)
        {
            return DAL.GetDataSetBySQL(sql);
        }

        public string GetOTType(string sWorkNo, string sDate)
        {
            return DAL.GetOTType(sWorkNo, sDate);
        }

        //public SortedList RunProc(OleDbParameter[] param, string sProcName)
        //{
        //    return DAL.RunProc(param, sProcName);
        //}

        public void Audit(DataTable dataTable, string appUser, SynclogModel logmodel)
        {
            DAL.Audit(dataTable,appUser,logmodel);
        }

        public void CancelAudit(DataTable dataTable, SynclogModel logmodel)
        {
            DAL.CancelAudit(dataTable,logmodel);
        }

        public string GetWorkFlowOrgCode(string OrgCode, string BillTypeCode, string OTType)
        {
            return DAL.GetWorkFlowOrgCode(OrgCode, BillTypeCode,  OTType);
        }

        public string SaveAuditData(string processFlag, string ID, string BillNoType, string AuditOrgCode, string BillTypeCode, string ApplyMan, string otmtype, string senduser, SynclogModel logmodel)
        {
            return DAL.SaveAuditData(processFlag, ID, BillNoType, AuditOrgCode, BillTypeCode, ApplyMan, otmtype, senduser,logmodel);
        }

        public int SaveOrgAuditData(string processFlag, Dictionary<string, List<string>> diry, string BillNoType, string BillTypeCode, string ApplyMan, SynclogModel logmodel)
        {
            return DAL.SaveOrgAuditData(processFlag, diry, BillNoType, BillTypeCode, ApplyMan,logmodel);
        }

        public DataSet GetRealDataByCondition(string condition)
        {
            return DAL.GetRealDataByCondition(condition);
        }

        public double GetOtHours(string WorkNo, string OTDate, string StrBtime, string StrEtime, string OTType)
        { 
          return DAL.GetOtHours( WorkNo,OTDate,StrBtime,StrEtime,OTType);
        }

        public string GetOTMFlag(string WorkNo, string OtDate)
        {
            return DAL.GetOTMFlag(WorkNo, OtDate);
        }

        public DataSet GetFixedDataByCondition(string condition)
        {
           return DAL.GetFixedDataByCondition(condition);
        }

        public DataView ExceltoDataView(string strFilePath)
        {
            return DAL.ExceltoDataView(strFilePath);
        }

        public int GetVWorkNoCount(string WorkNo, string sqlDep)
        {
            return DAL.GetVWorkNoCount(WorkNo, sqlDep);
        }

        public DataTable GetEmpinfo(string empno)
        {
            return DAL.GetEmpinfo(empno);
        }
    }
}
