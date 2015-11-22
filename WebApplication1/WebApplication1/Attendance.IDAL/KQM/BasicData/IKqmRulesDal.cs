
/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： IKqmRulesDal.cs
 * 檔功能描述： 缺勤規則數據操作接口
 * 
 * 版本：1.0
 * 創建標識： 張明強 2011.12.8
 * 
 */

using System;
using System.Collections.Generic;
using GDSBG.MiABU.Attendance.Common.Attributes;
using GDSBG.MiABU.Attendance.Model.KQM.BasicData;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;
using System.Data;

namespace GDSBG.MiABU.Attendance.IDAL.KQM.BasicData
{
    /// <summary>
    /// 缺勤規則數據操作接口
    /// </summary>
    [RefClass("KQM.BasicData.KqmRulesDal")]
    public interface IKqmRulesDal
    {
        /// <summary>
        /// 新增功能
        /// </summary>
        /// <param name="model">要新增的考勤規則Model</param>
        /// <returns>是否成功</returns>
        bool AddKqmRules(KqmRulesModel model, SynclogModel logmodel);

        /// <summary>
        /// 根據model查詢（模糊查詢）
        /// </summary>
        /// <param name="model">要查詢的考勤規則Model</param>
        /// <returns>考勤規則清單datatable</returns>
        DataTable GetKqmRules(KqmRulesModel model);


        /// <summary>
        /// 分頁查詢缺勤規則資料
        /// </summary>
        /// <param name="model"></param>
        /// <param name="?"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        DataTable GetKqmRulesList(KqmRulesModel model, int pageIndex, int pageSize, out int totalCount);

        /// <summary>
        /// 根據主鍵修改功能
        /// </summary>
        /// <param name="model">要修改的功能Model</param>
        /// <returns>是否成功</returns>
        bool UpdateRulesByKey(KqmRulesModel model, SynclogModel logmodel);


        /// <summary>
        /// 刪除Rules
        /// </summary>
        /// <param name="model"> 要刪除的model</param>
        /// <returns></returns>
        int DeleteRules(KqmRulesModel model, SynclogModel logmodel);
 
 
    }
}
