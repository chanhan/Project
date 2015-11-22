/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： TypeDataBll.cs
 * 檔功能描述： 固定參數表業務邏輯類
 * 
 * 版本：1.0
 * 創建標識：高子焱 2011.12.06
 * 
 */

using System.Collections.Generic;
using GDSBG.MiABU.Attendance.IDAL.KQM.BasicData;
using GDSBG.MiABU.Attendance.Model.KQM.BasicData;
using System.Data;
using System.Collections;

namespace GDSBG.MiABU.Attendance.BLL.KQM.BasicData
{
    public class TypeDataBll : BLLBase<ITypeDataDal>
    {
        /// <summary>
        /// 根據主鍵獲得功能Model
        /// </summary>
        /// <param name="model">給出主鍵值的Model</param>
        /// <returns>返回對應主鍵的Model</returns>
        public DataTable GetTypeDataList(string datatypevalue)
        {
            return DAL.GetTypeDataList(datatypevalue);
        }
        /// <summary>
        /// 查詢全部Model清單
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Model</returns>
        public DataTable GetDataList(string exceptionCode)
        {
            return DAL.GetDataList(exceptionCode);
        }
        /// <summary>
        /// 查詢班別
        /// </summary>
        /// <returns></returns>
        public DataTable GetDataList()
        {
            return DAL.GetDataList();
        }
        /// 查詢班別清單
        /// </summary>
        /// <returns></returns>
        public DataTable GetShiftNoList()
        {
            return DAL.GetShiftNoList();
        }
        /// <summary>
        /// 查詢在職狀態清單
        /// </summary>
        /// <returns></returns>
        public DataTable GetStatusList()
        {
            return DAL.GetStatusList();
        }
        /// <summary>
        /// 查詢異常清單
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Model</returns>
        public DataTable GetExceptionTypeList()
        {
            return DAL.GetExceptionTypeList();
        }
        /// <summary>
        /// 查詢狀態
        /// </summary>
        /// <returns></returns>
        public DataTable GetKqmKaoQinStatusList()
        {
            return DAL.GetKqmKaoQinStatusList();
        }
        /// <summary>
        /// 查詢數據列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetDataTypeList(string DataType)
        {
            return DAL.GetDataTypeList(DataType);
        }
        /// <summary>
        /// 獲得支援狀態列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetSupportStatusList()
        {
            return DAL.GetSupportStatusList();
        }
        /// <summary>
        /// 獲得下拉菜單數據列表
        /// </summary>
        /// <param name="datatypevalue"></param>
        /// <returns></returns>
        public DataTable GetdllDateTypeList(string datatypevalue)
        {
            return DAL.GetdllDateTypeList(datatypevalue);
        }
    }
}
