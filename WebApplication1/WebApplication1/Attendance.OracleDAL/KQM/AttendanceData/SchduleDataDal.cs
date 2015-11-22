/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： SchduleDataDal.cs
 * 檔功能描述： 排班作業數據操作類
 * 
 * 版本：1.0
 * 創建標識： 劉炎 2011.12.20
 * 
 */

using System;
using System.Data;
using System.Data.OracleClient;
using GDSBG.MiABU.Attendance.IDAL.KQM.AttendanceData;
using GDSBG.MiABU.Attendance.Model.KQM.AttendanceData;
using System.Collections;
using System.Collections.Generic;


namespace GDSBG.MiABU.Attendance.OracleDAL.KQM.AttendanceData
{
    /// <summary>
    /// 排班作業數據操作類
    /// </summary>
    public class SchduleDataDal : DALBase<ScheduleDataModel>, ISchduleDataDal
    {
        /// <summary>
        /// 根據排班時間，在職狀態獲取排班作業集
        /// </summary>
        /// <param name="condition">條件</param>
        /// <param name="schduleDate">排班時間</param>
        /// <param name="status">在職狀態</param>
        /// <returns>排班作業Model集</returns>
        public List<ScheduleDataModel> GetSchduleInfoList(Nullable<DateTime> schduleDate, string status, ScheduleDataModel model)
        {
            string conditionClause;
            List<OracleParameter> listPara = DalHelper.CreateConditionParameters(model, true, "A", out conditionClause);

            string sqlCondition = "";
            string flagClass = status;

            if (flagClass == null)
            {
                sqlCondition = " ";
            }
            if (!(flagClass == "0"))
            {
                if (flagClass == "1")
                {
                    sqlCondition = " where shiftno is not null ";
                }
                sqlCondition = " ";
            }
            string cmdTxt = @" SELECT WORKNO,LOCALNAME,DNAME,SHIFTDESC,STARTENDDATE FROM GDS_ATT_SCHDULE_V  WHERE  StartDate <= :schduleDate and  enddate>= :schduleDate  or LeaveDate>= :schduleDate";

            listPara.Add(new OracleParameter(":schduleDate", schduleDate));
            //listPara.Add(new OracleParameter(":sqlCondition", sqlCondition));
            cmdTxt = cmdTxt + conditionClause + sqlCondition;

            DataTable dtbl = DalHelper.ExecuteQuery(cmdTxt, listPara.ToArray());
            return OrmHelper.SetDataTableToList(dtbl);
        }

        /// <summary>
        /// 根據排班時間，在職狀態獲取排班作業分頁集
        /// </summary>
        /// <param name="schduleDate">排班時間</param>
        /// <param name="status">在職狀態</param>
        /// <param name="model">排班</param>
        /// <param name="pageIndex">頁索引</param>
        /// <param name="pageSize">頁面大小</param>
        /// <param name="totalCount">頁總數</param>
        /// <returns>排班作業Model集</returns>
        public List<ScheduleDataModel> GetPagerScheduleInfoList(Nullable<DateTime> schduleDate, string status, ScheduleDataModel model, int pageIndex, int pageSize, out int totalCount)
        {
            string conditionClause;
            List<OracleParameter> listPara = DalHelper.CreateConditionParameters(model, true, "A", out conditionClause);

            string sqlCondition = "";
            string flagClass = status;

            if (flagClass == null)
            {
                sqlCondition = " ";
            }
            if (!(flagClass == "0"))
            {
                if (flagClass == "1")
                {
                    sqlCondition = " where shiftno is not null ";
                }
                sqlCondition = " ";
            }
            string cmdTxt = @" SELECT WORKNO,LOCALNAME,DNAME,SHIFTDESC,STARTENDDATE FROM GDS_ATT_SCHDULE_V  WHERE  StartDate <= :schduleDate and  enddate>= :schduleDate  or LeaveDate>= :schduleDate";

            listPara.Add(new OracleParameter(":schduleDate", schduleDate));
            cmdTxt = cmdTxt + conditionClause + sqlCondition;
            DataTable dtbl = DalHelper.ExecutePagerQuery(cmdTxt, pageIndex, pageSize, out totalCount, listPara.ToArray());
            return OrmHelper.SetDataTableToList(dtbl);
        }

        /// <summary>
        /// 反饋導入信息
        /// </summary>
        /// <param name="workno">工號</param>
        /// <param name="successNum">成功數量</param>
        /// <param name="errorNum">錯誤數量</param>
        /// <returns>導入信息</returns>
        public DataTable GetImportCount(string workno, out int successNum, out int errorNum)
        {
            OracleParameter outCursor = new OracleParameter("p_coursor", OracleType.Cursor);
            OracleParameter outSuccess = new OracleParameter("p_success", OracleType.Int32);
            OracleParameter outError = new OracleParameter("p_error", OracleType.Int32);
            outCursor.Direction = ParameterDirection.Output;
            outSuccess.Direction = ParameterDirection.Output;
            outError.Direction = ParameterDirection.Output;
            DataTable dtbl = DalHelper.ExecuteQuery("gds_att_employeeshift_pro", CommandType.StoredProcedure,
               new OracleParameter("p_workno", workno), outCursor, outSuccess, outError);
            successNum = Convert.ToInt32(outSuccess.Value);
            errorNum = Convert.ToInt32(outError.Value);
            return dtbl;
        }

        /// <summary>
        /// 獲取導入人的所有信息
        /// </summary>
        /// <param name="createUser">導入人</param>
        /// <returns>排班集</returns>
        public List<ScheduleDataModel> GetImportData(string createUser)
        {
            string cmdTxt = @"SELECT WORKNO,ID,STARTDATE,ENDDATE,LOCALNAME ,DNAME,DCODE FROM gds_att_employeeshift_temp where create_user=:createUser";
            DataTable dtbl = DalHelper.ExecuteQuery(cmdTxt, new OracleParameter(":createUser", createUser));
            return OrmHelper.SetDataTableToList(dtbl);
        }

        /// <summary>
        /// 根據Model獲取排班集
        /// </summary>
        /// <param name="model">排班Model</param>
        /// <returns>排班集</returns>
        public List<ScheduleDataModel> GetShiftInfo(ScheduleDataModel model)
        {
            DataTable dtbl = DalHelper.Select(model);
            return OrmHelper.SetDataTableToList(dtbl);
        }

        /// <summary>
        /// 向數據庫中插入一條數據
        /// </summary>
        /// <param name="model"></param>
        /// <returns>是否成功</returns>
        public bool AddShiftInfo(ScheduleDataModel model)
        {
            return DalHelper.Insert(model) != -1;
        }

        /// <summary>
        /// 修改數據
        /// </summary>
        /// <param name="model"></param>
        /// <returns>是否成功</returns>
        public bool EditShiftInfo(ScheduleDataModel model)
        {
            return DalHelper.UpdateByKey(model) != -1;
        }
    }
}
