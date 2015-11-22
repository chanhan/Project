﻿/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： ParameterBll.cs
 * 檔功能描述： 參數設置業務邏輯類
 * 
 * 版本：1.0
 * 創建標識： 何西 2011.12.05
 * 
 */

using System.Collections.Generic;
using GDSBG.MiABU.Attendance.IDAL.SystemManage.SystemData;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemData;
using System.Data;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;

namespace GDSBG.MiABU.Attendance.BLL.SystemManage.SystemData
{
    public class ParameterBll : BLLBase<IParameterDal>
    {
        /// <summary>
        /// 根據主鍵查詢
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ParameterModel GetParameterByKey(string parameterName)
        {
            ParameterModel model = new ParameterModel();
            model.ParaName = parameterName;
            return DAL.GetParameterByKey(model);
        }
        /// <summary>
        /// 新增數據
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddNew(ParameterModel model, SynclogModel logmodel)
        {
            return DAL.AddNew(model, logmodel);
        }
        /// <summary>
        /// 修改數據
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateByKey(ParameterModel model, bool ignoreNull, SynclogModel logmodel)
        {
            return DAL.UpdateByKey(model, ignoreNull,logmodel);
        }

        /// <summary>
        /// 根據主鍵刪除
        /// </summary>
        /// <param name="model">參數Model</param>
        /// <returns>是否成功</returns>
        public bool DeleteByKey(ParameterModel model, SynclogModel logmodel)
        {
            return DAL.DeleteByKey(model,logmodel);
        }
        /// <summary>
        /// 根據條件查詢數據
        /// </summary>
        /// <param name="model"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public DataTable GetParameter(ParameterModel model, int pageIndex, int pageSize, out int totalCount)
        {
            return DAL.GetParameter(model, pageIndex, pageSize, out totalCount);
        }
        /// <summary>
        /// 根據條件查詢數據
        /// </summary>
        /// <returns></returns>
        public DataTable GetTypeDataDayWork()
        {
            return DAL.GetTypeDataDayWork();
        }

    }
}