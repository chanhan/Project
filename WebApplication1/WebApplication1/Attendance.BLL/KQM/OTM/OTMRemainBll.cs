/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： OTMRemainBll.cs
 * 檔功能描述： 剩余加班導入業務邏輯類
 * 
 * 版本：1.0
 * 創建標識： 顧陳偉 2011.12.23
 * 
 */
using System.Collections.Generic;
using System.Data;
using GDSBG.MiABU.Attendance.IDAL.KQM.OTM;
using GDSBG.MiABU.Attendance.Model.KQM.OTM;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;

namespace GDSBG.MiABU.Attendance.BLL.KQM.OTM
{
    public class OTMRemainBll : BLLBase<IOTMRemainDal>
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
        public DataTable GetAllRemainInfo(OTMRemainModel model,string sql, int pageIndex, int pageSize, out int totalCount)
        {
            return DAL.GetAllRemainInfo(model,sql, pageIndex, pageSize, out totalCount);
        }

        /// <summary>
        /// 導出查詢
        /// </summary>
        /// <param name="model"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        public DataTable GetAllRemainForExport(OTMRemainModel model, string sql)
        {
            return DAL.GetAllRemainForExport(model, sql);
        }

        /// <summary>
        /// 將datatable轉換為List
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public List<OTMRemainModel> GetList(DataTable dt)
        {
            return DAL.GetList(dt);
        }

        /// <summary>
        /// 刪除
        /// </summary>
        /// <param name="workno">工號</param>
        /// <param name="yearmonth">年月</param>
        /// <returns></returns>
        public int DeleteRemain(string workno, string yearmonth, SynclogModel logmodel)
        {
            return DAL.DeleteRemain(workno, yearmonth,logmodel);
        }

        /// 導入
        /// </summary>
        /// <param name="personcode"></param>
        /// <param name="moduleCode"></param>
        /// <param name="successnum"></param>
        /// <param name="errornum"></param>
        /// <returns></returns>
        public DataTable ImportExcel(string personcode, string moduleCode, out int successnum, out int errornum, SynclogModel logmodel)
        {
            return DAL.ImportExcel(personcode,moduleCode, out successnum, out errornum,logmodel);
        }

        /// <summary>
        /// 獲得用戶組織權限
        /// </summary>
        /// <param name="appuser"></param>
        /// <param name="rolecode"></param>
        /// <param name="modulecode"></param>
        /// <returns></returns>
        public bool Privileged(string appuser, string rolecode, string modulecode)
        {
            bool privileged = true;
            if (appuser.Equals("internal") || rolecode.Equals("Person"))
            {
                privileged = false;
            }
            else
            {
                DataTable dt = DAL.GetModuleInfo(modulecode);
                if ((dt != null) && (dt.Rows.Count > 0))
                {
                    privileged = false;
                }
            }
            return privileged;
        }
    }
}
