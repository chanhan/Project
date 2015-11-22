/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： ModuleBll.cs
 * 檔功能描述： 模組管理業務邏輯類
 * 
 * 版本：1.0
 * 創建標識： 何西 2011.11.28
 * 
 */

using System.Collections.Generic;
using GDSBG.MiABU.Attendance.IDAL.SystemManage.SystemSafety;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;
using System.Data;
namespace GDSBG.MiABU.Attendance.BLL.SystemManage.SystemSafety
{
    /// <summary>
    /// 模組管理業務邏輯類
    /// </summary>
    public class ModuleBll : BLLBase<IModuleDal>
    {
        /// <summary>
        /// 根據主鍵獲得功能Model
        /// </summary>
        /// <param name="model">給出主鍵值的Model</param>
        /// <returns>返回對應主鍵的Model</returns>
        public ModuleModel GetModuleByKey(string moduleCode)
        {
            ModuleModel model = new ModuleModel();
            model.ModuleCode = moduleCode;
            return DAL.GetModuleByKey(model);
        }

        /// <summary>
        /// 新增功能
        /// </summary>
        /// <param name="model">要新增的功能Model</param>
        /// <returns>是否成功</returns>
        public bool AddModule(ModuleModel model, SynclogModel logmodel)
        {
            return DAL.AddModule(model, logmodel);
        }

        /// <summary>
        /// 根據主鍵修改功能
        /// </summary>
        /// <param name="model">要修改的功能Model</param>
        /// <returns>是否成功</returns>
        public bool UpdateModuleByKey(ModuleModel model, bool ignoreNull, SynclogModel logmodel)
        {
            return DAL.UpdateModuleByKey(model,ignoreNull,  logmodel);
        }

        /// <summary>
        /// 刪除一個功能及子功能
        /// </summary>
        /// <param name="functionId">要刪除的功能Id</param>
        /// <returns>刪除功能條數</returns>
        public int DeleteModuleByKey(string moduleCode, SynclogModel logmodel)
        {
            return DAL.DeleteModuleByKey(moduleCode, logmodel);
        }

        /// <summary>
        /// 獲得用戶功能清單
        /// </summary>
        /// <returns>用戶清單ModuleModel集</returns>
        public DataTable GetUserModuleList()
        {
            return DAL.GetUserModuleList();
        }
        /// <summary>
        /// 獲得用戶功能DataTable
        /// </summary>
        /// <returns>用戶清單DataTable</returns>
        public DataTable GetUserModuleTable()
        {
            return DAL.GetUserModuleTable();
        }


        /// <summary>
        /// 獲得功能模組清單
        /// </summary>
        /// <param name="model">要查詢的功能Model</param>
        /// <returns>返回功能模組datatable</returns>
        public DataTable GetModule(ModuleModel model)
        {
            return DAL.GetModule(model);
        }

        /// <summary>
        /// 根據model數據
        /// </summary>
        /// <param name="model">要查詢的功能Model</param>
        /// <returns>功能模組清單集</returns>
        public List<ModuleModel> GetModuleList(ModuleModel model)
        {
            return DAL.GetModuleList(model);
        }

        /// <summary>
        /// 分頁查詢系統模組資料
        /// </summary>
        /// <param name="model"></param>
        /// <param name="?"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public DataTable GetUserModuleList(ModuleModel model, int pageIndex, int pageSize, out int totalCount)
        {
            return DAL.GetUserModuleList(model, pageIndex, pageSize, out  totalCount);
        }

        /// <summary>
        /// 查詢系統模組資料
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<ModuleModel> GetUserModuleList(ModuleModel model)
        {
            return DAL.GetUserModuleList(model);
        }

        /// <summary>
        /// 根據model查詢
        /// </summary>
        /// <param name="model">要查詢的功能Model</param>
        /// <returns>功能模組清單datatable</returns>
        public DataTable GetModuleByFunId(ModuleModel model)
        {
            return DAL.GetModuleByFunId(model);
        }
        /// <summary>
        /// 查找登錄用戶的button權限列表
        /// </summary>
        /// <param name="moduleCode">當前頁面的模組代碼</param>
        /// <param name="personCode">當前登錄用戶</param>
        /// <returns></returns>
        public string GetFuncList(string moduleCode,string personCode)
        {
            return DAL.GetFuncList(moduleCode, personCode);
        }
        /// <summary>
        /// 查找頁面的受管控的所有btn列表
        /// </summary>
        /// <param name="moduleCode">當前頁面的模組代碼</param>
        /// <returns></returns>
        public string GetFuncList(string moduleCode)
        {
            return DAL.GetFuncList(moduleCode);
        }

        /// <summary>
        /// 將datatable轉換為List
        /// </summary>
        /// <param name="dt">徐轉化的Datatable</param>
        /// <returns></returns>
        public List<ModuleModel> GetList(DataTable dt)
        {
            return DAL.GetList(dt);
        }
    }
}
