/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： EmpSupportInDal.cs
 * 檔功能描述： 內部支援數據操作類
 * 
 * 版本：1.0
 * 創建標識： 顧陳偉 2012.01.06
 * 
 */

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using GDSBG.MiABU.Attendance.IDAL.HRM.Support;
using GDSBG.MiABU.Attendance.Model.HRM.Support;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;
namespace GDSBG.MiABU.Attendance.OracleDAL.HRM.Support
{
    public class EmpSupportInDal : DALBase<EmpSupportInModel>, IEmpSupportInDal
    {
        /// <summary>
        /// 根據條件分頁查詢
        /// </summary>
        /// <param name="model"></param>
        /// <param name="LevelCondition"></param>
        /// <param name="ManagerCondition"></param>
        /// <param name="StartDateFrom"></param>
        /// <param name="StartDateTo"></param>
        /// <param name="PrepEndDateFrom"></param>
        /// <param name="PreEndDateTo"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public DataTable GetEmpSupportInPageInfo(EmpSupportInModel model,string SupportDept, string LevelCondition, string ManagerCondition, string StartDateFrom, string StartDateTo, string PrepEndDateFrom, string PrepEndDateTo, int pageIndex, int pageSize, out int totalCount)
        {
            string strCon = "";
            List<OracleParameter> listPara = DalHelper.CreateConditionParameters(model, true, "a", out strCon);
            string cmdText = @"select a.workno,to_char(a.supportorder) supportorder,a.supportdept,a.startdate,a.prependdate,a.enddate,a.remark,a.state,a.statename,a.sexname,a.identityno,
                              a.supportdeptname,a.levelcode,a.levelname,a.managercode,a.managername,a.technicalname,a.localname,a.sex from gds_att_empsupportin_v a where 1=1 ";
            cmdText = cmdText + strCon;
            if (!string.IsNullOrEmpty(SupportDept))
            {
                cmdText = cmdText + "and a.SUPPORTDEPT in (SELECT DepCode FROM gds_sc_department START WITH depname LIKE '" + SupportDept + "%' CONNECT BY PRIOR depcode = parentdepcode )";
            }
            if (!string.IsNullOrEmpty(LevelCondition))
            {
                cmdText = cmdText + " and a.levelcode in (" + LevelCondition + ")";
            }
            if (!string.IsNullOrEmpty(ManagerCondition))
            {
                cmdText = cmdText + " and a.managercode in (" + ManagerCondition + ")";
            }
            if (!string.IsNullOrEmpty(StartDateFrom))
            {
                cmdText += " AND a.startdate >= to_date('" + DateTime.Parse(StartDateFrom).ToString("yyyy/MM/dd") + "','yyyy/mm/dd') ";
            }
            if (!string.IsNullOrEmpty(StartDateTo))
            {
                cmdText += " AND a.startdate <= to_date('" + DateTime.Parse(StartDateTo).ToString("yyyy/MM/dd") + "','yyyy/mm/dd') ";
            }
            if (!string.IsNullOrEmpty(PrepEndDateFrom))
            {
                cmdText += " AND a.prependdate >= to_date('" + DateTime.Parse(PrepEndDateFrom).ToString("yyyy/MM/dd") + "','yyyy/mm/dd') ";
            }
            if (!string.IsNullOrEmpty(PrepEndDateTo))
            {
                cmdText += " AND a.prependdate <= to_date('" + DateTime.Parse(PrepEndDateTo).ToString("yyyy/MM/dd") + "','yyyy/mm/dd') ";
            }
            return DalHelper.ExecutePagerQuery(cmdText, pageIndex, pageSize, out totalCount, listPara.ToArray());
        }

        /// <summary>
        /// 導出查詢
        /// </summary>
        /// <param name="model"></param>
        /// <param name="sql"></param>
        /// <param name="SupportDept"></param>
        /// <param name="LevelCondition"></param>
        /// <param name="ManagerCondition"></param>
        /// <param name="StartDateFrom"></param>
        /// <param name="StartDateTo"></param>
        /// <param name="PrepEndDateFrom"></param>
        /// <param name="PrepEndDateTo"></param>
        /// <returns></returns>
        public DataTable GetEmpSupportInForExport(EmpSupportInModel model, string SupportDept, string LevelCondition, string ManagerCondition, string StartDateFrom, string StartDateTo, string PrepEndDateFrom, string PrepEndDateTo)
        {
            string strCon = "";
            List<OracleParameter> listPara = DalHelper.CreateConditionParameters(model, true, "a", out strCon);
            string cmdText = @"select a.workno,to_char(a.supportorder) supportorder,a.supportdept,a.startdate,a.prependdate,a.enddate,a.remark,a.state,a.statename,a.sexname,a.identityno,
                              a.supportdeptname,a.levelcode,a.levelname,a.managercode,a.managername,a.technicalname,a.localname,a.sex from gds_att_empsupportin_v a where 1=1 ";
            cmdText = cmdText + strCon;
            if (!string.IsNullOrEmpty(SupportDept))
            {
                cmdText = cmdText + "and a.SUPPORTDEPT in (SELECT DepCode FROM gds_sc_department START WITH depname LIKE '" + SupportDept + "%' CONNECT BY PRIOR depcode = parentdepcode )";
            }
            if (!string.IsNullOrEmpty(LevelCondition))
            {
                cmdText = cmdText + " and a.levelcode in (" + LevelCondition + ")";
            }
            if (!string.IsNullOrEmpty(ManagerCondition))
            {
                cmdText = cmdText + " and a.managercode in (" + ManagerCondition + ")";
            }
            if (!string.IsNullOrEmpty(StartDateFrom))
            {
                cmdText += " AND a.startdate >= to_date('" + DateTime.Parse(StartDateFrom).ToString("yyyy/MM/dd") + "','yyyy/mm/dd') ";
            }
            if (!string.IsNullOrEmpty(StartDateTo))
            {
                cmdText += " AND a.startdate <= to_date('" + DateTime.Parse(StartDateTo).ToString("yyyy/MM/dd") + "','yyyy/mm/dd') ";
            }
            if (!string.IsNullOrEmpty(PrepEndDateFrom))
            {
                cmdText += " AND a.prependdate >= to_date('" + DateTime.Parse(PrepEndDateFrom).ToString("yyyy/MM/dd") + "','yyyy/mm/dd') ";
            }
            if (!string.IsNullOrEmpty(PrepEndDateTo))
            {
                cmdText += " AND a.prependdate <= to_date('" + DateTime.Parse(PrepEndDateTo).ToString("yyyy/MM/dd") + "','yyyy/mm/dd') ";
            }
            return DalHelper.ExecuteQuery(cmdText, listPara.ToArray());
        }

        /// <summary>
        /// 將datatable轉換為List
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public List<EmpSupportInModel> GetList(DataTable dt)
        {
            return OrmHelper.SetDataTableToList(dt);
        }

        /// <summary>
        /// 刪除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int DeleteEmpSupportIn(EmpSupportInModel model, SynclogModel logmodel)
        {
            return DalHelper.Delete(model,logmodel);
        }

        /// <summary>
        /// 根據主鍵查詢
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public EmpSupportInModel GetEmpSupportInInfo(EmpSupportInModel model)
        {
            return DalHelper.SelectByKey(model);
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddEmpSupportIn(EmpSupportInModel model, SynclogModel logmodel)
        {
            return DalHelper.Insert(model,logmodel) != -1;
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateEmpSupportIn(EmpSupportInModel model, SynclogModel logmodel)
        {
            return DalHelper.UpdateByKey(model, true,logmodel) != -1;
        }

        /// <summary>
        /// 獲得員工資料
        /// </summary>
        /// <param name="EmployeeNo"></param>
        /// <returns></returns>
        public DataTable GetEmp(string EmployeeNo)
        {
            string condition = @"SELECT *
  FROM gds_att_employees_v a WHERE a.WorkNO=:p_EmployeeNo and a.status='0'  ";
            return DalHelper.ExecuteQuery(condition, new OracleParameter(":p_EmployeeNo", EmployeeNo));
        }

        /// <summary>
        /// 獲得內部支援員工資料
        /// </summary>
        /// <param name="EmployeeNo"></param>
        /// <returns></returns>
        public DataTable GetEmpSupportIn(string EmployeeNo)
        {
            string condition = @"SELECT *
  FROM gds_att_empsupportin a WHERE a.WorkNO=:p_EmployeeNo and a.state='0'  ";
            return DalHelper.ExecuteQuery(condition, new OracleParameter(":p_EmployeeNo", EmployeeNo));
        }

        /// <summary>
        /// 獲得員工在職狀態
        /// </summary>
        /// <param name="workno"></param>
        /// <returns></returns>
        public DataTable GetEmpStatus(string workno)
        {
            string str="select nvl(max(status),'') from gds_att_employees where workno=:workno";
            return DalHelper.ExecuteQuery(str, new OracleParameter(":workno", workno));
        }

        /// <summary>
        /// 獲得員工的支援順序號
        /// </summary>
        /// <param name="workno"></param>
        /// <returns></returns>
        public DataTable GetEmpsupportOrder(string workno)
        {
            string str = "select NVL(max(SUPPORTORDER),0)+1 from gds_att_EmpSupportIn where WORKNO=:WORKNO";
            return DalHelper.ExecuteQuery(str, new OracleParameter(":workno", workno));
        }

        /// <summary>
        /// 由工號和支援序號查詢工號
        /// </summary>
        /// <param name="workno"></param>
        /// <param name="supportorder"></param>
        /// <returns></returns>
        public DataTable GetEmpSupportByWorkNoAndOrder(string workno, string supportorder)
        {
            string str = "select workno from gds_att_EmpSupportIn where workno=:workno and State='0' AND SUPPORTORDER ! = :SUPPORTORDER";
            return DalHelper.ExecuteQuery(str, new OracleParameter(":workno", workno), new OracleParameter(":SUPPORTORDER", supportorder));
        }

        /// <summary>
        /// 導入
        /// </summary>
        /// <param name="personcode"></param>
        /// <param name="successnum"></param>
        /// <param name="errornum"></param>
        /// <returns></returns>
        public DataTable ImpoertExcel(string personcode, out int successnum, out int errornum, SynclogModel logmodel)
        {
            OracleParameter outCursor = new OracleParameter("p_coursor", OracleType.Cursor);
            OracleParameter outSuccess = new OracleParameter("p_success", OracleType.Int32);
            OracleParameter outError = new OracleParameter("p_error", OracleType.Int32);
            outCursor.Direction = ParameterDirection.Output;
            outSuccess.Direction = ParameterDirection.Output;
            outError.Direction = ParameterDirection.Output;
            DataTable dt = DalHelper.ExecuteQuery("gds_att_empsupportin_vaildata", CommandType.StoredProcedure,
                new OracleParameter("p_personcode", personcode), outCursor, outSuccess, outError,
                 new OracleParameter("p_transactiontype", logmodel.TransactionType == null ? "" : logmodel.TransactionType.ToString()),
                new OracleParameter("p_levelno", logmodel.LevelNo == null ? "" : logmodel.LevelNo.ToString()),
                new OracleParameter("p_fromhost", logmodel.FromHost == null ? "" : logmodel.FromHost.ToString()),
                new OracleParameter("p_tohost", logmodel.ToHost == null ? "" : logmodel.ToHost.ToString()),
                new OracleParameter("p_docno", logmodel.DocNo == null ? "" : logmodel.DocNo.ToString()),
                new OracleParameter("p_processflag", logmodel.ProcessFlag == null ? "" : logmodel.ProcessFlag.ToString()),
                new OracleParameter("p_processowner", logmodel.ProcessOwner == null ? "" : logmodel.ProcessOwner.ToString()));
            successnum = Convert.ToInt32(outSuccess.Value);
            errornum = Convert.ToInt32(outError.Value);
            return dt;
        }

        /// <summary>
        /// 放大鏡部門查詢
        /// </summary>
        /// <param name="depname"></param>
        /// <param name="depcode"></param>
        /// <returns></returns>
        public DataTable GetDataByCondition(string modulecode,string sql,string depname, string depcode)
        {
            string condition = "";
            condition = "select depcode,depname,costcode from gds_sc_department where deleted='N' and costcode>' ' ";
            if (modulecode.Length > 0)
            {
                condition = condition + " and depCode IN(" + sql + ") ";
            }
            if (depcode.Length != 0)
            {
                condition = condition + " and CostCode like '" + depcode + "%'";
            }
            if (depname.Length != 0)
            {
                condition = condition + " and DepName IN (SELECT DepName FROM gds_sc_department START WITH DepName LIKE '" + depname + "%' CONNECT BY PRIOR depcode = parentdepcode)";
            }
            condition = condition + " order by DepName";
            DataTable dt = DalHelper.ExecuteQuery(condition);
            return dt;
        }
    }
}
