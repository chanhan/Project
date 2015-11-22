/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： IModuleDal.cs
 * 檔功能描述： 模組管理數據操作接口
 * 
 * 版本：1.0
 * 創建標識： 何西 2011.09.28
 * 
 */

using System;
using System.Collections.Generic;
using GDSBG.MiABU.Attendance.Common.Attributes;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;
using System.Data;

namespace GDSBG.MiABU.Attendance.IDAL.SystemManage.SystemSafety
{
    /// <summary>
    /// 功能管理數據操作接口
    /// </summary>
    [RefClass("SystemManage.SystemSafety.ModuleDal")]
    public interface IModuleDal
    {
        /// <summary>
        /// 根據主鍵獲得功能Model
        /// </summary>
        /// <param name="model">給出主鍵值的Model</param>
        /// <returns>返回對應主鍵的Model</returns>
        ModuleModel GetModuleByKey(ModuleModel model);

        /// <summary>
        /// 新增功能
        /// </summary>
        /// <param name="model">要新增的功能Model</param>
        /// <returns>是否成功</returns>
        bool AddModule(ModuleModel model, SynclogModel logmodel);

        /// <summary>
        /// 根據主鍵修改功能
        /// </summary>
        /// <param name="model">要修改的功能Model</param>
        /// <returns>是否成功</returns>
        bool UpdateModuleByKey(ModuleModel model, bool ignoreNull, SynclogModel logmodel);

        /// <summary>
        /// 刪除一個功能及子功能
        /// </summary>
        /// <param name="functionId">要刪除的功能Id</param>
        /// <returns>刪除功能條數</returns>
        int DeleteModuleByKey(string moduleCode, SynclogModel logmodel);

        /// <summary>
        /// 獲得用戶功能清單
        /// </summary>
        /// <returns>用戶清單ModuleModel集</returns>
        DataTable GetUserModuleList();

        /// <summary>
        /// 獲得用戶功能DataTable
        /// </summary>
        /// <returns>用戶清單DataTable</returns>
        DataTable GetUserModuleTable();
   


        /// <summary>
        /// 獲得功能模組清單
        /// </summary>
        /// <returns>功能模組清單datatable</returns>
        DataTable GetModule(ModuleModel model);

        /// <summary>
        /// 根據model查詢
        /// </summary>
        /// <param name="model">要查詢的功能Model</param>
        /// <returns>功能模組清單datatable</returns>
        DataTable GetModuleByFunId(ModuleModel model);
        

        /// <summary>
        /// 根據model數據
        /// </summary>
        /// <param name="model">要查詢的功能Model</param>
        /// <returns>功能模組清單集</returns>
        List<ModuleModel> GetModuleList(ModuleModel model);

        /// <summary>
        /// 分頁查詢系統模組資料
        /// </summary>
        /// <param name="model"></param>
        /// <param name="?"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        DataTable GetUserModuleList(ModuleModel model, int pageIndex, int pageSize, out int totalCount);

        /// <summary>
        /// 查詢系統模組資料
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        List<ModuleModel> GetUserModuleList(ModuleModel model);
       

        /// <summary>
        /// 查找登錄用戶的button權限列表
        /// </summary>
        /// <param name="moduleCode">當前頁面的模組代碼</param>
        /// <param name="personCode">當前登錄用戶</param>
        /// <returns></returns>
        string GetFuncList(string moduleCode, string personCode);
        /// <summary>
        /// 查找頁面的受管控的所有btn列表
        /// </summary>
        /// <param name="moduleCode">當前頁面的模組代碼</param>
        /// <returns></returns>
        string GetFuncList(string moduleCode);

        /// <summary>
        /// 將datatable轉換為List
        /// </summary>
        /// <param name="dt">徐轉化的Datatable</param>
        /// <returns></returns>
        List<ModuleModel> GetList(DataTable dt);
      
    }
}
