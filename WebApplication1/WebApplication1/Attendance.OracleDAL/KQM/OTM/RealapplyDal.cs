/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： RealapplyDal.cs
 * 檔功能描述： 有效加班數據操作類
 * 
 * 版本：1.0
 * 創建標識： 昝望 2011.12.23
 * 
 */
using System;
using System.Collections.Generic;

using System.Text;
using GDSBG.MiABU.Attendance.Model.KQM.OTM;
using GDSBG.MiABU.Attendance.IDAL.KQM.OTM;
using System.Data;
using System.Data.OracleClient;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;

namespace GDSBG.MiABU.Attendance.OracleDAL.KQM.OTM
{
    public class RealapplyDal : DALBase<RealapplyModel>, IRealapplyDal
    {
        /// <summary>
        /// 查詢有效加班
        /// </summary>
        /// <param name="model">給出主鍵值的Model</param>
        /// <returns>返回對應主鍵的Model</returns>
        public DataTable GetRealapply(RealapplyModel model, int pageIndex, int pageSize, out int totalCount, string symbol, string Hours, string OTDateFrom, string OTDateTo, string BatchEmployeeNo, string sqlDep)
        {
            //return DalHelper.Select(model, pageIndex, pageSize, out totalCount);
            string strCon = "";
            List<OracleParameter> listPara = DalHelper.CreateConditionParameters(model, true, "", out strCon);
            string cmdText = @"SELECT *  FROM gds_att_realapply_v where RealType='A' and dCode IN (" + sqlDep + ")";
                cmdText=cmdText+strCon;
                if (!string.IsNullOrEmpty(Hours))
                {
                    cmdText = cmdText + "confirmHours" + symbol + Hours; 
                }
                if (OTDateFrom != "")
                {
                    cmdText = cmdText + " AND OTDate>=to_date('" + OTDateFrom + "','yyyy/MM/dd')";
                }
                if (OTDateTo != "")
                {
                    cmdText = cmdText + " AND OTDate<=to_date('" + OTDateTo + "','yyyy/MM/dd')";
                }
                if (BatchEmployeeNo != "")
                {
                    cmdText = cmdText + " and workno in (SELECT char_list    FROM TABLE (gds_sc_chartotable ('" + BatchEmployeeNo + "', '§')))";
                }
                DataTable dt = DalHelper.ExecutePagerQuery(cmdText, pageIndex, pageSize, out  totalCount, listPara.ToArray());
                return dt;
        }

        /// <summary>
        /// 將datatable轉換為List
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public List<RealapplyModel> GetList(DataTable dt)
        {
            return OrmHelper.SetDataTableToList(dt);
        }

        /// <summary>
        /// 查詢加班類型
        /// </summary>
        /// <returns></returns>
        public DataTable GetOverTimeType()
        {
            string str = "select * from gds_att_TYPEDATA where datatype= 'OverTimeType' order by datacode";
            return DalHelper.ExecuteQuery(str);
        }



        /// <summary>
        /// 查詢核准狀態
        /// </summary>
        /// <returns></returns>
        public DataTable GetOTMAdvanceApplyStatus()
        {
            string str = "select * from gds_att_TYPEDATA where datatype= 'OTMAdvanceApplyStatus' order by datacode";
            return DalHelper.ExecuteQuery(str);
        }



        /// <summary>
        /// 刪除
        /// </summary>
        /// <param name="functionId">/param>
        /// <returns></returns>
        public int DeleteRealapplyByKey(string ID, SynclogModel logmodel)
        {
            string str = "delete from gds_att_REALAPPLY where ID='" + ID + "'";
            return DalHelper.ExecuteNonQuery(str,logmodel);
        }


        /// <summary>
        /// 取消簽核
        /// </summary>
        /// <param name="model"></param>
        /// <returns>是否成功</returns>
        public int UpdateRealapplyByKey(RealapplyModel model, SynclogModel logmodel)
        {
            return DalHelper.UpdateByKey(model, true,logmodel);
        }

        /// <summary>
        /// 查詢未轉入有效的預報加班
        /// </summary>
        /// <param name="decode"></param>
        /// <param name="isproject"></param>
        /// <param name="employeeno"></param>
        /// <param name="name"></param>
        /// <param name="otdatefrom"></param>
        /// <param name="otdateto"></param>
        /// <returns></returns>
        public DataTable SelectAdvanceapply(string decode, string isproject, string employeeno, string name, string otdatefrom, string otdateto, string sqlDep)
        {
            string sqlText = "select * from gds_att_advanceapply_v where Status='2' and ImportFlag='N' and  rownum<=500 and dCode IN (" + sqlDep + ")";
            if (decode != "")
            {
                sqlText = sqlText + " and dcode in (SELECT DepCode FROM gds_sc_department START WITH depcode ='" + decode + "' CONNECT BY PRIOR depcode = parentdepcode)";
            }
            if (isproject != "")
            {
                sqlText = sqlText + " and IsProject='" + isproject + "'";
            }
            if (employeeno != "")
            {
                sqlText = sqlText + " and WorkNO='" + employeeno + "'";
            }
            if (name != "")
            {
                sqlText = sqlText + " and LocalName like '" + employeeno + "%'";
            }
            if (otdatefrom != "")
            {
                sqlText = sqlText + " and OTDate>=to_date('" + otdatefrom + "'," + "'yyyy/mm/dd')";
            }
            if (otdateto != "")
            {
                sqlText = sqlText + " and OTDate<=to_date('" + otdateto + "'," + "'yyyy/mm/dd')";
            }
            return DalHelper.ExecuteQuery(sqlText);
        }

        /// <summary>
        /// 轉入有效加班
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="updateuser"></param>
        /// <returns></returns>
        public int UpdateAdvanceapply(string ID, string updateuser, SynclogModel logmodel)
        {
            OracleParameter outPara = new OracleParameter("p_flag", OracleType.Int32);
            outPara.Direction = ParameterDirection.Output;
            int i = DalHelper.ExecuteNonQuery("gds_att_advanceapply_update", CommandType.StoredProcedure,
                new OracleParameter("p_id", ID), new OracleParameter("p_updateuser", updateuser), outPara, new OracleParameter("p_transactiontype", logmodel.TransactionType), new OracleParameter("p_levelno", logmodel.LevelNo), new OracleParameter("p_fromhost", logmodel.FromHost),
                new OracleParameter("p_tohost", logmodel.ToHost == null ? "" : logmodel.ToHost.ToString()), new OracleParameter("p_docno", logmodel.DocNo == null ? "" : logmodel.DocNo.ToString()), new OracleParameter("p_processflag", logmodel.ProcessFlag),
                new OracleParameter("p_processowner", logmodel.ProcessOwner));
            return Convert.ToInt32(outPara.Value);
        }
    }
}
