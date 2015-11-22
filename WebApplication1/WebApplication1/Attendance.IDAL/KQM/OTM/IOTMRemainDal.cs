/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： IOTMRemainDal.cs
 * 檔功能描述： 剩余加班導入接口類
 * 
 * 版本：1.0
 * 創建標識： 顧陳偉 2011.12.23
 * 
 */
using System.Collections.Generic;
using System.Data;
using GDSBG.MiABU.Attendance.Common.Attributes;
using GDSBG.MiABU.Attendance.Model.KQM.OTM;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;

namespace GDSBG.MiABU.Attendance.IDAL.KQM.OTM
{
    [RefClass("KQM.OTM.OTMRemainDal")]
    public interface IOTMRemainDal
    {
        /// <summary>
        /// 獲得所有剩餘加班資料
        /// </summary>
        /// <param name="model"></param>
        /// <param name="sql"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        DataTable GetAllRemainInfo(OTMRemainModel model, string sql, int pageIndex, int pageSize, out int totalCount);

        /// <summary>
        /// 導出查詢
        /// </summary>
        /// <param name="model"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        DataTable GetAllRemainForExport(OTMRemainModel model, string sql);

        /// <summary>
        /// 將datatable轉換為List
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        List<OTMRemainModel> GetList(DataTable dt);

        /// <summary>
        /// 刪除
        /// </summary>
        /// <param name="workno">工號</param>
        /// <param name="yearmonth">年月</param>
        /// <returns></returns>
        int DeleteRemain(string workno, string yearmonth, SynclogModel logmodel);

        /// 導入
        /// </summary>
        /// <param name="personcode"></param>
        /// <param name="moduleCode"></param>
        /// <param name="successnum"></param>
        /// <param name="errornum"></param>
        /// <returns></returns>
        DataTable ImportExcel(string personcode, string moduleCode, out int successnum, out int errornum, SynclogModel logmodel);

        /// <summary>
        /// 獲得登錄用戶模組權限
        /// </summary>
        /// <param name="modulecode"></param>
        /// <returns></returns>
        DataTable GetModuleInfo(string modulecode);
        
    }
}
