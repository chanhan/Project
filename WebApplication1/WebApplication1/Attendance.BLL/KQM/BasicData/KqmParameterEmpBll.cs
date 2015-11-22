
/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： KqmParameterEditBll.cs
 * 檔功能描述： 考勤參數設定業務邏輯類
 * 
 * 版本：1.0
 * 創建標識： 張明強 2011.12.13
 * 
 */
using System.Collections.Generic;
using GDSBG.MiABU.Attendance.IDAL.KQM.BasicData;
using GDSBG.MiABU.Attendance.Model.KQM.BasicData;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;
using System.Data;
namespace GDSBG.MiABU.Attendance.BLL.KQM.BasicData
{
    public class KqmParameterEmpBll : BLLBase<IKqmParameterEmpDal>
    {
        /// <summary>
        /// 查詢人員考勤參數信息
        /// </summary>
        /// <param name="model">要查詢的model</param>
        /// <returns>返回的datatable</returns>
        public DataTable GetKQMParamsEmpData(AttKQParamsEmpModel model, string sqlDep)
        {
            return DAL.GetKQMParamsEmpData(model, sqlDep);
        }

        /// <summary>
        /// 刪除考勤參數
        /// </summary>
        /// <param name="model"> 要刪除的model</param>
        /// <returns></returns>
        public int DeleteKQMParamsEmpData(AttKQParamsEmpModel model,SynclogModel logmodel)
        {
            return DAL.DeleteKQMParamsEmpData(model,logmodel);
        }


        

        /// <summary>
        /// 根據model數據
        /// </summary>
        /// <param name="model">要查詢的功能Model</param>
        /// <returns>功能模組清單集</returns>
        public List<AttKQParamsEmpModel> GetParamsEmpList(AttKQParamsEmpModel model, string sqlDep)
        {
            return DAL.GetParamsEmpList(model,  sqlDep);
        }


        /// <summary>
        /// 分頁查詢
        /// </summary>
        /// <param name="model"></param>
        /// <param name="?"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public DataTable GetParamsEmpList(AttKQParamsEmpModel model,string sqlDep, int pageIndex, int pageSize, out int totalCount)
        {
            return DAL.GetParamsEmpList(model, sqlDep, pageIndex, pageSize, out totalCount);
        }

       
    }
}
