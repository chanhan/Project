/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： IOTMActivityApplyTempDal.cs
 * 檔功能描述：免卡人員加班導入功能數據操作接口
 * 
 * 版本：1.0
 * 創建標識： 張明強 2011.12.8
 * 
 */

using System;
using System.Collections.Generic;
using GDSBG.MiABU.Attendance.Common.Attributes;
using GDSBG.MiABU.Attendance.Model.KQM.OTM;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;
using System.Data;

namespace GDSBG.MiABU.Attendance.IDAL.KQM.OTM
{
    /// <summary>
    /// 缺勤規則數據操作接口
    /// </summary>
    [RefClass("KQM.OTM.OTMActivityApplyTempDal")]
    public interface IOTMActivityApplyTempDal
    {
        /// <summary>
        /// 導入信息,正確信息插入正式表,錯誤信息返回datatable
        /// </summary>
        /// <param name="createUser">創建人</param>
        /// <returns>返回的datatable</returns>
        DataTable GetTempTableErrorData(string createUser, out int successNum, out int errorNum,SynclogModel logmodel);



        /// <summary>
        /// 將datatable轉換為List
        /// </summary>
        /// <param name="dt">徐轉化的Datatable</param>
        /// <returns></returns>
        List<ActivityTempModel> GetList(DataTable dt);
    }
}
