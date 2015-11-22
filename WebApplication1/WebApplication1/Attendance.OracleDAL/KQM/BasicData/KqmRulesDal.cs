/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： KqmRulesDal.cs
 * 檔功能描述： 缺勤規則數據操作類
 * 
 * 版本：1.0
 * 創建標識： 張明強 2011.12.8
 * 
 */

using System;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using GDSBG.MiABU.Attendance.IDAL.KQM.BasicData;
using GDSBG.MiABU.Attendance.Model.KQM.BasicData;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;
using System.Collections.Generic;

namespace GDSBG.MiABU.Attendance.OracleDAL.KQM.BasicData
{
    public class KqmRulesDal : DALBase<KqmRulesModel>, IKqmRulesDal
    {
        /// <summary>
        /// 新增功能
        /// </summary>
        /// <param name="model">要新增的缺勤規則Model</param>
        /// <returns>是否成功</returns>
        public bool AddKqmRules(KqmRulesModel model, SynclogModel logmodel)
        {
            return DalHelper.Insert(model,logmodel) != -1;
        }

        /// <summary>
        /// 根據model查詢（模糊查詢）
        /// </summary>
        /// <param name="model">要查詢的缺勤規則Model</param>
        /// <returns>考勤規則清單datatable</returns>
        public DataTable GetKqmRules(KqmRulesModel model)
        {
            return DalHelper.Select(model, true, null);
        }


        /// <summary>
        /// 分頁查詢缺勤規則資料
        /// </summary>
        /// <param name="model"></param>
        /// <param name="?"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public DataTable GetKqmRulesList(KqmRulesModel model, int pageIndex, int pageSize, out int totalCount)
        {
            return DalHelper.Select(model, true, pageIndex, pageSize, out totalCount);
        }


        /// <summary>
        /// 根據主鍵修改功能
        /// </summary>
        /// <param name="model">要修改的功能Model</param>
        /// <returns>是否成功</returns>
        public bool UpdateRulesByKey(KqmRulesModel model, SynclogModel logmodel)
        {
            return DalHelper.UpdateByKey(model,logmodel) != -1;
        }

        /// <summary>
        /// 刪除Rules
        /// </summary>
        /// <param name="model"> 要刪除的model</param>
        /// <returns></returns>
        public int DeleteRules(KqmRulesModel model, SynclogModel logmodel)
        {
            return DalHelper.Delete(model,logmodel);
        }

    }
}
