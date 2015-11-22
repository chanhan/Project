/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： KQMEmployeeShiftDal.cs
 * 檔功能描述： 考勤查詢數據操作類
 * 
 * 版本：1.0
 * 創建標識： 昝望 2012.1.4
 * 
 */
using System;
using System.Collections.Generic;

using System.Text;
using GDSBG.MiABU.Attendance.IDAL.Hr.PCM;
using System.Data;
using GDSBG.MiABU.Attendance.Model.Hr.PCM;

namespace GDSBG.MiABU.Attendance.OracleDAL.Hr.PCM
{
    public class KaoQinDataDal : DALBase<KaoQinDataModel>, IKaoQinDataDal
    {
        /// <summary>
        /// 結果下拉框
        /// </summary>
        /// <returns></returns>
        public DataTable GetExceptionType()
        {
            string str = "SELECT DataType,DataCode,DataValue,DataTypeDetail,(DataCode||'?B'||DataValue)as newDataValue FROM gds_att_TYPEDATA WHERE DataType='ExceptionType' ORDER BY OrderId";
            return DalHelper.ExecuteQuery(str);
        }

        /// <summary>
        /// 狀態下拉框
        /// </summary>
        /// <returns></returns>
        public DataTable GetKqmKaoQinStatus()
        {
            string str = "SELECT DataType,DataCode,DataValue,DataTypeDetail,(DataCode||'?B'||DataValue)as newDataValue FROM gds_att_TYPEDATA WHERE DataType='KqmKaoQinStatus' AND (DataCode=0 OR DataCode=1) ORDER BY OrderId";
            return DalHelper.ExecuteQuery(str);
        }


        /// <summary>
        /// 班別下拉框
        /// </summary>
        /// <returns></returns>
        public DataTable GetKqmWorkShiftType()
        {
            string str = "SELECT DataType,DataCode,DataValue,DataTypeDetail,(DataCode||'?B'||DataValue)as newDataValue FROM gds_att_TYPEDATA WHERE DataType='KqmWorkShiftType' AND (DataCode='A' OR DataCode='C') ORDER BY OrderId";
            return DalHelper.ExecuteQuery(str);
        }


        /// <summary>
        /// 查詢
        /// </summary>
        /// <param name="model">給出主鍵值的Model</param>
        /// <returns>返回對應主鍵的Model</returns>
        public DataTable GetKaoQinData(string KQDateFrom,string KQDateTo,string ExceptionType,string Status,string ShiftNo, string personcode, int pageIndex, int pageSize, out int totalCount)
        {
            string cmdText = "select  WORKNO, to_char(KQDATE,'YYYY/MM/DD') KQDATE,SHIFTNO,LOCALNAME,DEPNAME,DCODE,OTONDUTYTIME,OTOFFDUTYTIME,SHIFTDESC,ONDUTYTIME,OFFDUTYTIME,ABSENTQTY,STATUS,EXCEPTIONTYPE,EXCEPTIONNAME,REASONTYPE,REASONREMARK,REASONNAME,STATUSNAME,OTHOURS,TOTALHOURS from gds_att_kaoqindata_v where workno='" + personcode + "'";
            if (KQDateFrom != "")
            {
                cmdText = cmdText + " and kqdate >=to_date('" + KQDateFrom + "','YYYY/MM/DD')";
            }
            if (KQDateTo != "")
            {
                cmdText = cmdText + " and kqdate <=to_date('" + KQDateTo + "','YYYY/MM/DD')";
            }
            if (ExceptionType != "")
            {
                cmdText = cmdText + " and ExceptionType in (SELECT char_list FROM TABLE (gds_sc_chartotable ('" + ExceptionType + "', '§')))";
            }
            if (Status == "1")
            {
                cmdText = cmdText + " AND (Status='1' OR Status='2' OR Status='4' OR Status='5')";
            }
            if (Status == "0")
            {
                cmdText = cmdText + " AND (Status='0' OR Status='3')";
            }
            if (ShiftNo =="A")
            {
                cmdText = cmdText + " AND (ShiftNo LIKE 'A%' OR ShiftNo LIKE 'B%')";
            }
            if (ShiftNo == "C")
            {
                cmdText = cmdText + " AND ShiftNo LIKE 'C%'";
            }

            DataTable dt = DalHelper.ExecutePagerQuery(cmdText, pageIndex, pageSize, out  totalCount, null);
            return dt;
        }

        /// <summary>
        /// 導出查詢
        /// </summary>
        /// <param name="model">給出主鍵值的Model</param>
        /// <returns>返回對應主鍵的Model</returns>
        public DataTable GetKaoQinData(string KQDateFrom, string KQDateTo, string ExceptionType, string Status, string ShiftNo, string personcode)
        {
            string cmdText = "select * from gds_att_kaoqindata_v where workno='" + personcode + "'";
            if (KQDateFrom != "")
            {
                cmdText = cmdText + " and kqdate >=to_date('" + KQDateFrom + "','YYYY/MM/DD')";
            }
            if (KQDateTo != "")
            {
                cmdText = cmdText + " and kqdate <=to_date('" + KQDateTo + "','YYYY/MM/DD')";
            }
            if (ExceptionType != "")
            {
                cmdText = cmdText + " and ExceptionType in (SELECT char_list FROM TABLE (gds_sc_chartotable ('" + ExceptionType + "', '§')))";
            }
            if (Status == "1")
            {
                cmdText = cmdText + " AND (Status='1' OR Status='2' OR Status='4' OR Status='5')";
            }
            if (Status == "0")
            {
                cmdText = cmdText + " AND (Status='0' OR Status='3')";
            }
            if (ShiftNo == "A")
            {
                cmdText = cmdText + " AND (ShiftNo LIKE 'A%' OR ShiftNo LIKE 'B%')";
            }
            if (ShiftNo == "C")
            {
                cmdText = cmdText + " AND ShiftNo LIKE 'C%'";
            }

            DataTable dt = DalHelper.ExecuteQuery(cmdText, null);
            return dt;
        }

        /// <summary>
        /// 將datatable轉換為List
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public List<KaoQinDataModel> GetList(DataTable dt)
        {
            return OrmHelper.SetDataTableToList(dt);
        }
    }
}
