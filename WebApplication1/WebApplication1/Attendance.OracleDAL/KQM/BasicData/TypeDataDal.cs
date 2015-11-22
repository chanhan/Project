/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： TypeDataDal.cs
 * 檔功能描述： 固定參數表數據操作類
 * 
 * 版本：1.0
 * 創建標識： 高子焱 2011.12.06
 * 
 */

using System;
using System.Data;
using System.Data.OracleClient;
using GDSBG.MiABU.Attendance.IDAL.KQM.BasicData;
using GDSBG.MiABU.Attendance.Model.KQM.BasicData;
using System.Collections;

namespace GDSBG.MiABU.Attendance.OracleDAL.KQM.BasicData
{
    public class TypeDataDal : DALBase<TypeDataModel>, ITypeDataDal
    {
        /// <summary>
        /// 查詢全部Model清單
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Model</returns>
        public DataTable GetTypeDataList(string datatypevalue)
        {
            TypeDataModel model = new TypeDataModel();
            model.DataType = datatypevalue;
            DataTable dt = DalHelper.Select(model, "orderid");
            return dt;
        }
        /// <summary>
        /// 查詢全部Model清單
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Model</returns>
        public DataTable GetDataList(string exceptionCode)
        {
            TypeDataModel model = new TypeDataModel();
            model.DataCode = exceptionCode;
            model.DataType="ExceptionType";
            DataTable dt = DalHelper.Select(model, "orderid");
            return dt;
        } 
        /// <summary>
        /// 查詢班別（白班和晚班）
        /// </summary>
        /// <returns></returns>
        public DataTable GetDataList()
        {
            return DalHelper.ExecuteQuery(@"SELECT   datatype, datacode, datavalue, datatypedetail,
                  (datacode || '?B' || datavalue) AS newdatavalue FROM gds_att_typedata
                  WHERE datatype = 'KqmWorkShiftType' AND (datacode = 'A' OR datacode = 'C') ORDER BY orderid");
        }
        
        /// <summary>
        /// 查詢班別清單
        /// </summary>
        /// <returns></returns>
        public DataTable GetShiftNoList()
        {
            return DalHelper.ExecuteQuery(@"SELECT   datatype, datacode, datavalue, datatypedetail,
                          (datacode || '?B' || datavalue) AS newdatavalue FROM gds_att_typedata
                          WHERE DataType='KqmWorkShiftType' ORDER BY OrderId");
        }
        /// <summary>
        /// 查詢在職狀態清單
        /// </summary>
        /// <returns></returns>
        public DataTable GetStatusList()
        {
            return DalHelper.ExecuteQuery(@"SELECT   datatype, datacode, datavalue, datatypedetail,
                          (datacode || '?B' || datavalue) AS newdatavalue FROM gds_att_typedata
                          WHERE DataType='EmpState' ORDER BY OrderId");
        }
        /// <summary>
        /// 查詢異常類別清單
        /// </summary>
        /// <returns></returns>
        public DataTable GetExceptionTypeList()
        {
            return DalHelper.ExecuteQuery(@"SELECT   datatype, datacode, datavalue, datatypedetail,
                          (datacode || '?B' || datavalue) AS newdatavalue FROM gds_att_typedata
                          WHERE DataType='ExceptionType' ORDER BY OrderId");
        }
        /// <summary>
        /// 查詢考勤狀態（正常、異常）
        /// </summary>
        /// <returns></returns>
        public DataTable GetKqmKaoQinStatusList()
        {
            return DalHelper.ExecuteQuery(@"SELECT   datatype, datacode, datavalue, datatypedetail,
                          (datacode || '?B' || datavalue) AS newdatavalue FROM gds_att_typedata
                          WHERE DataType='KqmKaoQinStatus' AND (DataCode=0 OR DataCode=1) ORDER BY OrderId");
        }
        /// <summary>
        /// 根據傳入條件查詢數據列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetDataTypeList(string DataType)
        {
            return DalHelper.ExecuteQuery(@"SELECT  datatype, datacode, datavalue, datatypedetail FROM gds_att_typedata WHERE DataType=" + DataType + " ORDER BY OrderId ");
        }

        /// <summary>
        /// 獲得支援狀態列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetSupportStatusList()
        {
            return DalHelper.ExecuteQuery(@"SELECT   datatype, datacode, datavalue, datatypedetail,
                          (datacode || '?B' || datavalue) AS newdatavalue FROM gds_att_typedata
                          WHERE DataType='SupportType' ORDER BY OrderId");
        }

        /// <summary>
        /// 獲得下拉菜單數據列表
        /// </summary>
        /// <param name="datatypevalue"></param>
        /// <returns></returns>
        public DataTable GetdllDateTypeList(string datatypevalue)
        {
            return DalHelper.ExecuteQuery(@"SELECT   datatype, datacode, datavalue, datatypedetail,
                          (datacode || '?B' || datavalue) AS newdatavalue FROM gds_att_typedata
                          WHERE DataType='" + datatypevalue + "' ORDER BY OrderId");
        }
    }
}
