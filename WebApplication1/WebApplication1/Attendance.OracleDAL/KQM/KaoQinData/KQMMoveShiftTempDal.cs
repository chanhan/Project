/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： KQMMoveShiftDal.cs
 * 檔功能描述： 免卡人員加班導入數據操作類
 * 
 * 版本：1.0
 * 創建標識： 張明強 2011.12.23
 * 
 */

using System;
using System.Data;
using System.Data.OracleClient;
using GDSBG.MiABU.Attendance.IDAL.KQM.KaoQinData;
using GDSBG.MiABU.Attendance.Model.KQM.KaoQinData;
using System.Collections;
using System.Collections.Generic;

namespace GDSBG.MiABU.Attendance.OracleDAL.KQM.KaoQinData
{
    public class KQMMoveShiftTempDal : DALBase<MoveShiftTempModel>, IKQMMoveShiftTempDal
    {
        /// <summary>
        /// 將datatable轉換為List
        /// </summary>
        /// <param name="dt">徐轉化的Datatable</param>
        /// <returns></returns>
        public List<MoveShiftTempModel> GetTempList(DataTable dt)
        {
            return OrmHelper.SetDataTableToList(dt);
        }
    }
}
