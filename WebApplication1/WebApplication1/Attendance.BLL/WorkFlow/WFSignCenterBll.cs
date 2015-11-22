using System;
using System.Collections.Generic;
using System.Text;
using GDSBG.MiABU.Attendance.IDAL.WorkFlow;
using System.Data;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;

namespace GDSBG.MiABU.Attendance.BLL.WorkFlow
{
    public class WFSignCenterBll : BLLBase<IWFSignCenterDal>
    {
        public void SaveBatchAuditData(string BillNo, string AuditMan, string BillTypeCode, SynclogModel logmodel)
        {
            DAL.SaveBatchAuditData(BillNo, AuditMan, BillTypeCode,  logmodel);
        }

        public DataSet GetAuditCenterDataByCondition(string condition)
        {
            return DAL.GetAuditCenterDataByCondition(condition);
        }

        public string GetAllAuditDept(string sDepCode)
        {
            return DAL.GetAllAuditDept(sDepCode);
        }

        public DataSet ExcuteSQL(string sql)
        {
            return DAL.ExcuteSQL(sql);
        }

        public void SaveDisAuditData(string BillNo, string AuditMan, string BillTypeCode, string ApRemark, List<string> DisSignRamark, List<string> Id, SynclogModel logmodel)
        {
            DAL.SaveDisAuditData(BillNo, AuditMan, BillTypeCode, ApRemark, DisSignRamark, Id, logmodel);
        }
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
        public void SaveDisAuditData(string BillNo, string AuditMan, string BillTypeCode, string ApRemark, List<string> DisSignRamark, List<string> YearMonth, List<string> WorkNo, SynclogModel logmodel)
        {
            DAL.SaveDisAuditData(BillNo, AuditMan, BillTypeCode, ApRemark, DisSignRamark, YearMonth, WorkNo, logmodel);
        }

        public void SaveAuditData(string BillNo, string AuditMan, string BillTypeCode, string ApRemark, string condition, List<string> DisSignRamark, List<string> Id, SynclogModel logmodel)
        {
            DAL.SaveAuditData(BillNo, AuditMan, BillTypeCode, ApRemark, condition, DisSignRamark, Id, logmodel);
        }
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
        public void SaveAuditData(string BillNo, string AuditMan, string BillTypeCode, string ApRemark, string condition, List<string> DisSignRamark, List<string> WorkNo, List<string> YearMonth, SynclogModel logmodel)
        {
            DAL.SaveAuditData(BillNo, AuditMan, BillTypeCode, ApRemark, condition, DisSignRamark, WorkNo, YearMonth, logmodel);
        }

        public string GetValue(string sql)
        {
            return DAL.GetValue(sql);
        }

        public DataSet GetDataSetBySQL(string sql)
        {
            return DAL.GetDataSetBySQL(sql);
        }

        public DataSet GetAuditDataByCondition(string condition)
        {
            return DAL.GetAuditDataByCondition(condition);
        }

        public DataSet GetDataByCondition(string condition)
        {
            return DAL.GetDataByCondition(condition);
        }
        /// <summary>
        /// 獲取單據類型
        /// </summary>
        /// <param name="billType">單據類型簡寫代碼</param>
        /// <returns></returns>
        public DataTable GetBillTypeCode(string billType)
        {
            return DAL.GetBillTypeCode(billType);
        }

        public string GetAllDept(string sDepCode, bool bParent)
        {
            return DAL.GetAllDept(sDepCode, bParent);
        }


        public DataSet GetDataByCondition_Bill(string condition)
        {
            return DAL.GetDataByCondition_Bill(condition);
        }

        public DataSet GetApprovedDataByCondition(string condition, int pageindex, int pagesize, out int totalcount)
        {
            return DAL.GetApprovedDataByCondition(condition, pageindex, pagesize, out totalcount);
        }

        //public DataSet GetApprovedDataByCondition(string condition, int pageSize, ref int out_CurrentPage, ref int out_TotalPage, ref int out_TotalRecords)
        //{
        //    return DAL.GetApprovedDataByCondition(condition, pageSize, ref out_CurrentPage, ref out_TotalPage, ref out_TotalRecords);
        //}

        public DataSet GetDataByCondition_HRM(string condition)
        {
            return DAL.GetDataByCondition_HRM(condition);
        }
    }
}
