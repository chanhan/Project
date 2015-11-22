/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： HrmEmpPremiumInfoDal.cs
 * 檔功能描述：員工行政處分操作類
 * 
 * 版本：1.0
 * 創建標識： 劉炎 2012.03.12
 * 
 */
using System;
using System.Data;
using System.Collections.Generic;
using System.Data.OracleClient;
using GDSBG.MiABU.Attendance.IDAL.HRM.EmployeeData;
using GDSBG.MiABU.Attendance.Model.HRM.EmployeeData;

namespace GDSBG.MiABU.Attendance.OracleDAL.HRM.EmployeeData
{
    /// <summary>
    /// 員工行政處分操作類
    /// </summary>
    public class HrmEmpPremiumInfoDal : DALBase<HrmEmpPremiumInfoModel>, IHrmEmpPremiumInfoDal
    {
        /// <summary>
        /// 根據條件查詢員工檢查信息
        /// </summary>
        /// <param name="empNoList">所要查詢員工號</param>
        /// <param name="model">獎懲信息數據集</param>
        /// <param name="startDate">開始時間</param>
        /// <param name="endDate">結束時間</param>
        /// <param name="deptList">所要查詢部門</param>
        /// <param name="jobSituationCode">在職狀態</param>
        /// <param name="pageIndex">頁面索引</param>
        /// <param name="pageSize">頁面大小</param>
        /// <param name="totalCount">頁面總數</param>
        /// <returns>員工獎懲信息Model集</returns>
        public List<HrmEmpPremiumInfoModel> GetEmpPremiumInfo(string empNoList, HrmEmpPremiumInfoModel model, DateTime startDate, DateTime endDate, string deptList, string jobSituationCode, int pageIndex, int pageSize, out int totalCount)
        {
            string conditionClause = "";
            List<OracleParameter> listPara = DalHelper.CreateConditionParameters(model, true, "A", out conditionClause);
            string cmdText = " SELECT COL_BU_NAME,DEPT_NAME,EMP_NO,EMP_NAME,PREMIUM_NAME,PREMIUM_DATE,PREMIUM_NUM,PREMIUM_TYPE,PREMIUM_COMMENT FROM HRM_EMP_PREMIUMS_SV A   WHERE  PREMIUM_DATE>:startDate AND PREMIUM_DATE<:endDate AND JOB_SITUATION_CODE=:jobSituationCode ";
            listPara.Add(new OracleParameter(":startDate", startDate));
            listPara.Add(new OracleParameter(":endDate", endDate == DateTime.MaxValue ? endDate : endDate.AddDays(1)));
            listPara.Add(new OracleParameter(":jobSituationCode", jobSituationCode));
            cmdText = cmdText + conditionClause;
            if (!string.IsNullOrEmpty(empNoList))
            {
                cmdText = cmdText + " AND A.EMP_NO IN (SELECT CHAR_LIST FROM CHAR_TABLE(:empNoList,',')) ";
                listPara.Add(new OracleParameter(":empNoList", empNoList));
            }
            if (!string.IsNullOrEmpty(deptList))
            {
                cmdText = cmdText + " AND EXISTS (SELECT 1 FROM HRM_ORG_DETAIL_RELATIONS_SV B WHERE ((B.RELATION_ID IN (SELECT CHAR_LIST FROM CHAR_TABLE(:deptList,',') or B.RELATION_PARENT_ID IN (SELECT CHAR_LIST FROM CHAR_TABLE(:deptList,','))))  ";
                listPara.Add(new OracleParameter(":deptList", deptList));

            }
            cmdText = cmdText + " ORDER BY EMP_NO, PREMIUN_DATE DESC ";
            DataTable dt = DalHelper.ExecutePagerQuery(cmdText, pageIndex, pageSize, out totalCount, listPara.ToArray());
            return OrmHelper.SetDataTableToList(dt);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DataTable GetPremiumName()
        {
            string cmdText = " SELECT PREMIUM_NAME,PREMIUM_TYPE  FROM HRM_EMP_PREMIUMS_SV";
            return DalHelper.ExecuteQuery(cmdText);
        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <returns></returns>
        //public DataTable GetJobStatus() 
        //{
        //   //string cmdText =" SELECT  "
        //}
    }
}
