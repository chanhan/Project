/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： KqmParameterEditDal.cs
 * 檔功能描述： 考勤參數設定(單位)數據操作類
 * 
 * 版本：1.0
 * 創建標識： 張明強 2011.12.13
 * 
 */

using System;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using GDSBG.MiABU.Attendance.IDAL.KQM.BasicData;
using GDSBG.MiABU.Attendance.Model.KQM.BasicData;
using System.Collections.Generic;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;
namespace GDSBG.MiABU.Attendance.OracleDAL.KQM.BasicData
{
    public class KqmParameterEditDal : DALBase<AttKQParamsOrgModel>, IKqmParameterEditDal
    {
        /// <summary>
        /// 查詢單位考勤參數信息
        /// </summary>
        /// <param name="model">要查詢的model</param>
        /// <returns>返回的datatable</returns>
        public DataTable GetKQMParamsOrgData(AttKQParamsOrgModel model)
        {
            return DalHelper.Select(model, null);
        }


        /// <summary>
        /// 新增功能
        /// </summary>
        /// <param name="model">要新增的考勤參數Model</param>
        /// <returns>是否成功</returns>
        public bool AddKQMParamsOrgData(AttKQParamsOrgModel model, SynclogModel logmodel)
        {
            return DalHelper.Insert(model, logmodel) != -1;
        }

        /// <summary>
        /// 根據主鍵修改功能
        /// </summary>
        /// <param name="model">要修改的考勤參數Model</param>
        /// <returns>是否成功</returns>
        public bool UpdateKQMParamsOrgByKey(AttKQParamsOrgModel model, SynclogModel logmodel)
        {
            return DalHelper.UpdateByKey(model, logmodel) != -1;
        }

        /// <summary>
        /// 刪除考勤參數
        /// </summary>
        /// <param name="model"> 要刪除的model</param>
        /// <returns></returns>
        public int DeleteKQMParamsOrgData(AttKQParamsOrgModel model, SynclogModel logmodel)
        {
            return DalHelper.Delete(model, logmodel);
        }

        /// <summary>
        /// 獲取ParaValue
        /// </summary>
        /// <returns></returns>
        public string GetValue(string flag, AttKQParamsOrgModel model)
        {
            DataTable dt = new DataTable();
            string value = "";
            if (flag == "IsAllowNotKaoQin")
            {
                dt = DalHelper.ExecuteQuery(@"SELECT NVL(max(paravalue),'N') FROM gds_sc_parameter WHERE paraname='IsAllowNotKaoQin'");
            }

            if (dt != null)
            {
                value = dt.Rows[0][0].ToString().Trim();
            }


            return value;
        }

        /// <summary>
        /// 根據部門編號取得部門名稱
        /// </summary>
        /// <param name="depCode"></param>
        /// <returns></returns>
        public string GetDepName(string depCode)
        {
            string depName="";
            DataTable dt = DalHelper.ExecuteQuery(@"select depname from gds_sc_department where depcode=:depcode", new OracleParameter(":depcode", depCode));
            if (dt != null)
            {
                depName = dt.Rows[0][0].ToString().Trim();
            }
            return depName;

        }
    }
}
