
/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： KqmParameterEmpEditBll.cs
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
    public class KqmParameterEmpEditBll: BLLBase<IKqmParameterEmpEditDal>
    {
        /// <summary>
        /// 查詢人員考勤參數信息
        /// </summary>
        /// <param name="model">要查詢的model</param>
        /// <returns>返回的datatable</returns>
        public DataTable GetKQMParamsEmpData(AttKQParamsEmpEditModel model)
        {
            return DAL.GetKQMParamsEmpData(model);
        }

        /// <summary>
        /// 查詢員工信息
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public DataTable GetVDataByCondition(string employeeNo, string sqlDep)
        {
            return DAL.GetVDataByCondition(employeeNo, sqlDep);
        }

        /// <summary>
        /// 新增功能
        /// </summary>
        /// <param name="model">要新增的考勤參數Model</param>
        /// <returns>是否成功</returns>
        public bool AddKQMParamsEmpData(AttKQParamsEmpEditModel model,SynclogModel logmodel)
        {
            return DAL.AddKQMParamsEmpData(model,logmodel);
        }

        /// <summary>
        /// 根據主鍵修改功能
        /// </summary>
        /// <param name="model">要修改的考勤參數Model</param>
        /// <returns>是否成功</returns>
        public bool UpdateKQMParamsEmpByKey(AttKQParamsEmpEditModel model,SynclogModel logmodel)
        {
            return DAL.UpdateKQMParamsEmpByKey(model,logmodel);
        }

        /// <summary>
        /// 刪除考勤參數
        /// </summary>
        /// <param name="model"> 要刪除的model</param>
        /// <returns></returns>
        public int DeleteKQMParamsEmpData(AttKQParamsEmpEditModel model,SynclogModel logmodel)
        {
            return DAL.DeleteKQMParamsEmpData(model,logmodel);
        }
    }
}
