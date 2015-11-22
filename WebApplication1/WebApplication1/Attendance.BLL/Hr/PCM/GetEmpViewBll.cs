/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： GetEmpViewBll.cs
 * 檔功能描述： 請假申請獲取代理人基本信息業務邏輯類
 * 
 * 版本：1.0
 * 創建標識： 陳函 2012.3.10
 * 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GDSBG.MiABU.Attendance.IDAL.Hr.PCM;
using GDSBG.MiABU.Attendance.Model.Hr.PCM;

namespace GDSBG.MiABU.Attendance.BLL.Hr.PCM
{
    public class GetEmpViewBll : BLLBase<IGetEmpViewDal>
    {
        /// <summary>
        /// 獲取代理人的基本信息
        /// </summary>
        /// <param name="proxyWorkNo">代理人工號</param>
        /// <returns>代理人基本信息</returns>
        public List<GetEmpViewModel> GetEmpList(string proxyWorkNo)
        {
          return DAL.GetEmpList( proxyWorkNo);
        }
    }
}
