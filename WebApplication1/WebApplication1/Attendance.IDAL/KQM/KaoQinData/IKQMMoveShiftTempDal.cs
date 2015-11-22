
/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： IKQMMoveShiftDal.cs
 * 檔功能描述： 彈性調班數據操作接口
 * 
 * 版本：1.0
 * 創建標識： 張明強 2011.12.8
 * 
 */

using System;
using System.Collections.Generic;
using GDSBG.MiABU.Attendance.Common.Attributes;
using GDSBG.MiABU.Attendance.Model.KQM.KaoQinData;
using System.Data;

namespace GDSBG.MiABU.Attendance.IDAL.KQM.KaoQinData
{
    /// <summary>
    /// 缺勤規則數據操作接口
    /// </summary>
    [RefClass("KQM.KaoQinData.KQMMoveShiftTempDal")]
    public interface IKQMMoveShiftTempDal
    {
        /// <summary>
        /// 將datatable轉換為List
        /// </summary>
        /// <param name="dt">徐轉化的Datatable</param>
        /// <returns></returns>
        List<MoveShiftTempModel> GetTempList(DataTable dt);
    }
}
