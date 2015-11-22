/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： GetEmpViewDal.cs
 * 檔功能描述： 請假申請獲取代理人基本信息數據操作類
 * 
 * 版本：1.0
 * 創建標識： 陳函 2012.3.10
 * 
 */
using System;
using System.Collections.Generic;
using System.Text;
using GDSBG.MiABU.Attendance.IDAL.Hr.PCM;
using GDSBG.MiABU.Attendance.Model.Hr.PCM;
using System.Data;

namespace GDSBG.MiABU.Attendance.OracleDAL.Hr.PCM
{
    public class GetEmpViewDal : DALBase<GetEmpViewModel>, IGetEmpViewDal
    {
        /// <summary>
        /// 獲取代理人的基本信息
        /// </summary>
        /// <param name="proxyWorkNo">代理人工號</param>
        /// <returns>代理人基本信息</returns>
        public List<GetEmpViewModel> GetEmpList(string proxyWorkNo)
        {
            string cmdText = "select workno,localname,notes,flag from gds_att_getEmp_v where workno='" + proxyWorkNo.ToUpper() + "'";
            DataTable dt = DalHelper.ExecuteQuery(cmdText);
            return OrmHelper.SetDataTableToList(dt);
        }
    }
}
