/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： KQMMoveShiftBll.cs
 * 檔功能描述：彈性調班業務邏輯類
 * 
 * 版本：1.0
 * 創建標識： 張明強 2011.12.8
 * 
 */
using System.Collections.Generic;
using GDSBG.MiABU.Attendance.IDAL.KQM.KaoQinData;
using GDSBG.MiABU.Attendance.Model.KQM.KaoQinData;
using System.Data;


namespace GDSBG.MiABU.Attendance.BLL.KQM.KaoQinData
{
    public  class KQMMoveShiftTempBll : BLLBase<IKQMMoveShiftTempDal>
    {
        /// <summary>
        /// 將datatable轉換為List
        /// </summary>
        /// <param name="dt">徐轉化的Datatable</param>
        /// <returns></returns>
        public List<MoveShiftTempModel> GetTempList(DataTable dt)
        {
            return DAL.GetTempList(dt);
        }
    }
}
