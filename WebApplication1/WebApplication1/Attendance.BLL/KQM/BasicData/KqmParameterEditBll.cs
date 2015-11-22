
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
    public class KqmParameterEditBll : BLLBase<IKqmParameterEditDal>
    {
        /// <summary>
        /// 查詢單位考勤參數信息
        /// </summary>
        /// <param name="model">要查詢的model</param>
        /// <returns>返回的datatable</returns>
        public DataTable GetKQMParamsOrgData(AttKQParamsOrgModel model)
        {
            return DAL.GetKQMParamsOrgData(model);
        }


        /// <summary>
        /// 新增功能
        /// </summary>
        /// <param name="model">要新增的考勤參數Model</param>
        /// <returns>是否成功</returns>
        public bool AddKQMParamsOrgData(AttKQParamsOrgModel model,SynclogModel logmodel)
        {
            return DAL.AddKQMParamsOrgData(model,logmodel);
        }

        /// <summary>
        /// 根據主鍵修改功能
        /// </summary>
        /// <param name="model">要修改的考勤參數Model</param>
        /// <returns>是否成功</returns>
        public bool UpdateKQMParamsOrgByKey(AttKQParamsOrgModel model,SynclogModel logmodel)
        {
            return DAL.UpdateKQMParamsOrgByKey(model,logmodel);
        }

        /// <summary>
        /// 刪除考勤參數
        /// </summary>
        /// <param name="model"> 要刪除的model</param>
        /// <returns></returns>
        public int DeleteKQMParamsOrgData(AttKQParamsOrgModel model,SynclogModel logmodel)
        {
            return DAL.DeleteKQMParamsOrgData(model,logmodel);
        }


        /// <summary>
        /// 獲取ParaValue
        /// </summary>
        /// <returns></returns>
        public string GetValue(string flag, AttKQParamsOrgModel model)
        {
            return DAL.GetValue(flag, model);
        }

        /// <summary>
        /// 根據部門編號取得部門名稱
        /// </summary>
        /// <param name="depCode"></param>
        /// <returns></returns>
        public string GetDepName(string depCode)
        {
            return DAL.GetDepName(depCode);
        }

    }
}
