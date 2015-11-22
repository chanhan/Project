

/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： KqmRulesBll.cs
 * 檔功能描述： 缺勤規則業務邏輯類
 * 
 * 版本：1.0
 * 創建標識： 張明強 2011.12.8
 * 
 */
using System.Collections.Generic;
using GDSBG.MiABU.Attendance.IDAL.KQM.BasicData;
using GDSBG.MiABU.Attendance.Model.KQM.BasicData;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;
using System.Data;

namespace GDSBG.MiABU.Attendance.BLL.KQM.BasicData
{
    public class KqmRulesBll : BLLBase<IKqmRulesDal>
    {
        /// <summary>
        /// 新增功能
        /// </summary>
        /// <param name="model">要新增的考勤規則Model</param>
        /// <returns>是否成功</returns>
        public bool AddKqmRules(KqmRulesModel model, SynclogModel logmodel)
        {
            return DAL.AddKqmRules(model,logmodel);
        }

        /// <summary>
        /// 根據model查詢（模糊查詢）
        /// </summary>
        /// <param name="model">要查詢的考勤規則Model</param>
        /// <returns>考勤規則清單datatable</returns>
        public DataTable GetKqmRules(KqmRulesModel model)
        {
            return DAL.GetKqmRules(model);
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
            return DAL.GetKqmRulesList(model, pageIndex, pageSize, out  totalCount);
        }


        /// <summary>
        /// 根據主鍵修改功能
        /// </summary>
        /// <param name="model">要修改的功能Model</param>
        /// <returns>是否成功</returns>
        public bool UpdateRulesByKey(KqmRulesModel model,SynclogModel logmodel)
        {
            return DAL.UpdateRulesByKey(model,logmodel);
        }


        /// <summary>
        /// 刪除Rules
        /// </summary>
        /// <param name="model"> 要刪除的model</param>
        /// <returns></returns>
        public int DeleteRules(KqmRulesModel model,SynclogModel logmodel)
        {
            return  DAL.DeleteRules(model,logmodel);
        }
    }
}
