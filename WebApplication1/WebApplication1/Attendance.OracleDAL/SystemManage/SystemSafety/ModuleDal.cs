/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： ModuleDal.cs
 * 檔功能描述： 模組管理數據操作類
 * 
 * 版本：1.0
 * 創建標識： 何西 2011.11.28
 * 
 */

using System;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using GDSBG.MiABU.Attendance.IDAL.SystemManage.SystemSafety;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;
using System.Collections.Generic;

namespace GDSBG.MiABU.Attendance.OracleDAL.SystemManage.SystemSafety
{
    public class ModuleDal : DALBase<ModuleModel>, IModuleDal
    {

        /// <summary>
        /// 根據主鍵獲得功能Model
        /// </summary>
        /// <param name="model">給出主鍵值的Model</param>
        /// <returns>返回對應主鍵的Model</returns>
        public ModuleModel GetModuleByKey(ModuleModel model)
        {
            return DalHelper.SelectByKey(model);
        }

        /// <summary>
        /// 新增功能
        /// </summary>
        /// <param name="model">要新增的功能Model</param>
        /// <returns>是否成功</returns>
        public bool AddModule(ModuleModel model, SynclogModel logmodel)
        {
            return DalHelper.Insert(model, logmodel) != -1;
        }

        /// <summary>
        /// 根據主鍵修改功能
        /// </summary>
        /// <param name="model">要修改的功能Model</param>
        /// <returns>是否成功</returns>
        public bool UpdateModuleByKey(ModuleModel model, SynclogModel logmodel)
        {

            return DalHelper.UpdateByKey(model,  logmodel) != -1;
        }

        /// <summary>
        /// 根據主鍵修改功能
        /// </summary>
        /// <param name="model">要修改的功能Model</param>
        /// <returns>是否成功</returns>
        public bool UpdateModuleByKey(ModuleModel model, bool ignoreNull, SynclogModel logmodel)
        {
            string moduleCode = model.ModuleCode.ToString().Trim();
            DataTable dt = DalHelper.ExecuteQuery(@"SELECT language_key FROM gds_sc_module a WHERE a.modulecode = :modulecode",
                new OracleParameter(":modulecode", moduleCode));
            model.LanguageKey = dt == null ? "" : dt.Rows[0][0].ToString().Trim();
            return DalHelper.UpdateByKey(model, ignoreNull, logmodel) != -1;
        }

        /// <summary>
        /// 刪除一個功能及子功能
        /// </summary>
        /// <param name="functionId">要刪除的功能Id</param>
        /// <returns>刪除功能條數</returns>
        public int DeleteModuleByKey(string moduleCode,SynclogModel logmodel)
        {
            OracleParameter outPara = new OracleParameter("p_out", OracleType.Int32);
            outPara.Direction = ParameterDirection.Output;
            int i = DalHelper.ExecuteNonQuery("delete_module_pro", CommandType.StoredProcedure,
                outPara,
                new OracleParameter("p_modulecode", moduleCode),
                new OracleParameter("p_transactiontype", logmodel.TransactionType == null ? "" : logmodel.TransactionType.ToString()),
                new OracleParameter("p_levelno", logmodel.LevelNo == null ? "" : logmodel.LevelNo.ToString()),
                new OracleParameter("p_fromhost", logmodel.FromHost == null ? "" : logmodel.FromHost.ToString()),
                new OracleParameter("p_tohost", logmodel.ToHost == null ? "" : logmodel.ToHost.ToString()),
                new OracleParameter("p_docno", logmodel.DocNo == null ? "" : logmodel.DocNo.ToString()),
                new OracleParameter("p_processflag", logmodel.ProcessFlag == null ? "" : logmodel.ProcessFlag.ToString()),
                new OracleParameter("p_processowner", logmodel.ProcessOwner == null ? "" : logmodel.ProcessOwner.ToString())
                );
            return i == -1 ? i : Convert.ToInt32(outPara.Value);
        }

        /// <summary>
        /// 獲得用戶功能清單
        /// </summary>
        /// <returns>用戶清單ModuleModel集</returns>
        public DataTable GetUserModuleList()
        {
            ModuleModel model = new ModuleModel();
            return DalHelper.Select(model, "orderid");
            //DataTable dt= DalHelper.Select(model);
            //List<ModuleModel> list = OrmHelper.SetDataTableToList(dt);            
        }


        /// <summary>
        /// 獲得用戶功能DataTable
        /// </summary>
        /// <returns>用戶清單DataTable</returns>
        public DataTable GetUserModuleTable()
        {
            ModuleModel model = new ModuleModel();
            return DalHelper.Select(model);      
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

            string strCon = "";
            string parentModuleCode = model.ParentModuleCode;
            model.ParentModuleCode = "";
            List<OracleParameter> listPara = DalHelper.CreateConditionParameters(model, true, "a", out strCon);
            string cmdText = @"select * from gds_sc_module_v a where 1=1 ";
            cmdText = cmdText + strCon;
            if (!String.IsNullOrEmpty(parentModuleCode))
            {
                cmdText = cmdText + " start with a.modulecode=:parentModuleCode CONNECT BY PRIOR MODULECODE=PARENTMODULECODE ";
                listPara.Add(new OracleParameter(":parentModuleCode", parentModuleCode));
            }
            
            DataTable dt = DalHelper.ExecutePagerQuery(cmdText, pageIndex, pageSize, out totalCount, listPara.ToArray());
            model.ParentModuleCode = parentModuleCode;
            return dt;

            
        }

        /// <summary>
        /// 查詢系統模組資料
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<ModuleModel> GetUserModuleList(ModuleModel model)
        {
            //DataTable dt= DalHelper.Select(model, true);
            //return OrmHelper.SetDataTableToList(dt);
            string strCon = "";
            string parentModuleCode = model.ParentModuleCode;
            model.ParentModuleCode = "";
            List<OracleParameter> listPara = DalHelper.CreateConditionParameters(model, true, "a", out strCon);
            string cmdText = @"select * from gds_sc_module_v a where 1=1 ";
            cmdText = cmdText + strCon;
            if (!String.IsNullOrEmpty(parentModuleCode))
            {
                cmdText = cmdText + " start with a.modulecode=:parentModuleCode CONNECT BY PRIOR MODULECODE=PARENTMODULECODE ";
                listPara.Add(new OracleParameter(":parentModuleCode", parentModuleCode));
            }
            DataTable dt = DalHelper.ExecuteQuery(cmdText, listPara.ToArray());
            model.ParentModuleCode = parentModuleCode;
            return OrmHelper.SetDataTableToList(dt);
        }


        /// <summary>
        /// 根據model查詢（模糊查詢）
        /// </summary>
        /// <param name="model">要查詢的功能Model</param>
        /// <returns>功能模組清單datatable</returns>
        public DataTable GetModule(ModuleModel model)
        {
            return DalHelper.Select(model, true, null);
        }


        /// <summary>
        /// 根據model查詢
        /// </summary>
        /// <param name="model">要查詢的功能Model</param>
        /// <returns>功能模組清單datatable</returns>
        public DataTable GetModuleByFunId(ModuleModel model)
        {
            return DalHelper.Select(model, false, null);
        }

        /// <summary>
        /// 根據model數據
        /// </summary>
        /// <param name="model">要查詢的功能Model</param>
        /// <returns>功能模組清單集</returns>
        public List<ModuleModel> GetModuleList(ModuleModel model)
        {
            DataTable dt = DalHelper.Select(model, true, "orderid");
            return OrmHelper.SetDataTableToList(dt);
        }

        public void ExcuteTrans(List<ModuleModel> list)
        {
            OracleTransaction trans = DalHelper.Connection.BeginTransaction();

            foreach (ModuleModel model in list)
            {
                DalHelper.Insert(model);
            }
            trans.Commit();
        }
        /// <summary>
        /// 查找登錄用戶的button權限列表
        /// </summary>
        /// <param name="moduleCode">當前頁面的模組代碼</param>
        /// <param name="personCode">當前登錄用戶</param>
        /// <returns></returns>
        public string GetFuncList(string moduleCode, string personCode)
        {
            DataTable dt = DalHelper.ExecuteQuery(@" SELECT a.functionlist funclist FROM gds_sc_authority a, gds_sc_rolesrole b, gds_sc_person c
                                                  WHERE a.rolecode = b.rolecode AND a.modulecode = :modulecode AND b.rolescode = c.rolecode
                                                  AND c.personcode = :personcode  AND c.deleted <> 'Y'", new OracleParameter(":modulecode", moduleCode),
                                                  new OracleParameter(":personcode", personCode));
            if (dt != null && dt.Rows.Count== 1)
            {
                return Convert.ToString(dt.Rows[0]["funclist"].ToString());
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 查找頁面的受管控的所有btn列表
        /// </summary>
        /// <param name="moduleCode">當前頁面的模組代碼</param>
        /// <returns></returns>
        public string GetFuncList(string moduleCode)
        {
            DataTable dt = DalHelper.ExecuteQuery(@"SELECT functionlist funclist FROM gds_sc_module a WHERE a.modulecode = :modulecode", 
                new OracleParameter(":modulecode", moduleCode));
            if (dt != null && dt.Rows.Count == 1)
            {
                return Convert.ToString(dt.Rows[0]["funclist"].ToString());
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 將datatable轉換為List
        /// </summary>
        /// <param name="dt">徐轉化的Datatable</param>
        /// <returns></returns>
        public List<ModuleModel> GetList(DataTable dt)
        {
            return OrmHelper.SetDataTableToList(dt);
        }
    }
}
