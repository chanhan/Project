/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： KqmParameterEmpDal.cs
 * 檔功能描述： 考勤參數設定(單位)數據操作類
 * 
 * 版本：1.0
 * 創建標識： 張明強 2011.12.13
 * 
 */

using System;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using GDSBG.MiABU.Attendance.IDAL.KQM.BasicData;
using GDSBG.MiABU.Attendance.Model.KQM.BasicData;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;
using System.Collections.Generic;

namespace GDSBG.MiABU.Attendance.OracleDAL.KQM.BasicData
{
    public class KqmParameterEmpDal : DALBase<AttKQParamsEmpModel>, IKqmParameterEmpDal
    {
        /// <summary>
        /// 查詢人員考勤參數信息
        /// </summary>
        /// <param name="model">要查詢的model</param>
        /// <returns>返回的datatable</returns>
        public DataTable GetKQMParamsEmpData(AttKQParamsEmpModel model,string sqlDep)
        {
            string strCon = "";
            string depCode = model.OrgCode.ToString();
            model.OrgCode = "";
            List<OracleParameter> listPara = DalHelper.CreateConditionParameters(model, true, "b", out strCon);
            string cmdText = @"select * from GDS_ATT_KQPARAMSEMP_V b where 1=1 ";
            cmdText = cmdText + strCon;
            cmdText += " AND b.orgcode in (" + sqlDep + ")";
            if (string.IsNullOrEmpty(model.WorkNo.ToString()) && string.IsNullOrEmpty(model.LocalName.ToString()))
            {
                if (!string.IsNullOrEmpty(depCode))
                {
                    cmdText += " AND b.orgcode  IN (SELECT DepCode FROM gds_sc_department START WITH depcode = :depCode CONNECT BY PARENTDepCODE=PRIOR depcode) ";
                    listPara.Add(new OracleParameter(":depCode", depCode));
                }
            }

            DataTable dt = DalHelper.ExecuteQuery(cmdText, listPara.ToArray());
            return dt;
        }



        /// <summary>
        /// 刪除考勤參數
        /// </summary>
        /// <param name="model"> 要刪除的model</param>
        /// <returns></returns>
        public int DeleteKQMParamsEmpData(AttKQParamsEmpModel model,SynclogModel logmodel)
        {
            return DalHelper.Delete(model,logmodel);
        }

        /// <summary>
        /// 根據主鍵獲得功能Model
        /// </summary>
        /// <param name="model">給出主鍵值的Model</param>
        /// <returns>返回對應主鍵的Model</returns>
        public AttKQParamsEmpModel GetParamsEmpByKey(AttKQParamsEmpModel model)
        {
            return DalHelper.SelectByKey(model);
        }

        /// <summary>
        /// 根據model數據
        /// </summary>
        /// <param name="model">要查詢的功能Model</param>
        /// <returns>功能模組清單集</returns>
        public List<AttKQParamsEmpModel> GetParamsEmpList(AttKQParamsEmpModel model, string sqlDep)
        {
            string strCon = "";
            string depCode = model.OrgCode.ToString();
            model.OrgCode = "";
            List<OracleParameter> listPara = DalHelper.CreateConditionParameters(model, true, "b", out strCon);
            string cmdText = @"select * from GDS_ATT_KQPARAMSEMP_V b where 1=1 ";
            cmdText = cmdText + strCon;
            cmdText += " AND b.orgcode in (" + sqlDep + ")";
            if (string.IsNullOrEmpty(model.WorkNo.ToString()) && string.IsNullOrEmpty(model.LocalName.ToString()))
            {
                if (!string.IsNullOrEmpty(depCode))
                {
                    cmdText += " AND b.orgcode  IN (SELECT DepCode FROM gds_sc_department START WITH depcode = :depCode CONNECT BY PARENTDepCODE=PRIOR depcode) ";
                    listPara.Add(new OracleParameter(":depCode", depCode));
                }
            }

            DataTable dt = DalHelper.ExecuteQuery(cmdText, listPara.ToArray());
            return OrmHelper.SetDataTableToList(dt);
        }

        /// <summary>
        /// 分頁查詢
        /// </summary>
        /// <param name="model"></param>
        /// <param name="?"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public DataTable GetParamsEmpList(AttKQParamsEmpModel model,string sqlDep, int pageIndex, int pageSize, out int totalCount)
        {
            string strCon = "";
            string depCode = model.OrgCode.ToString();
            model.OrgCode = "";
            List<OracleParameter> listPara = DalHelper.CreateConditionParameters(model, true, "b", out strCon);
            string cmdText = @"select * from GDS_ATT_KQPARAMSEMP_V b where 1=1 ";
            cmdText = cmdText + strCon;
            cmdText += " AND b.orgcode in (" + sqlDep + ")";
            if (string.IsNullOrEmpty(model.WorkNo.ToString()) && string.IsNullOrEmpty(model.LocalName.ToString()))
            {
                if (!string.IsNullOrEmpty(depCode))
                {
                    cmdText += " AND b.orgcode  IN (SELECT DepCode FROM gds_sc_department START WITH depcode = :depCode CONNECT BY PARENTDepCODE=PRIOR depcode) ";
                    listPara.Add(new OracleParameter(":depCode", depCode));
                }
            }

            DataTable dt = DalHelper.ExecutePagerQuery(cmdText, pageIndex, pageSize, out totalCount, listPara.ToArray());
            return dt;
        }

        
    }
}
