/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： IGetEmpViewDal.cs
 * 檔功能描述： 請假申請獲取代理人基本信息接口類
 * 
 * 版本：1.0
 * 創建標識： 陳函 2012.3.10
 * 
 */
using System;
using System.Collections.Generic;
using System.Text;
using GDSBG.MiABU.Attendance.Common.Attributes;
using GDSBG.MiABU.Attendance.Model.Hr.PCM;

namespace GDSBG.MiABU.Attendance.IDAL.Hr.PCM
{
    [RefClass("Hr.PCM.GetEmpViewDal")]
    public interface IGetEmpViewDal
    {
        /// <summary>
        /// 獲取代理人的基本信息
        /// </summary>
        /// <param name="proxyWorkNo">代理人工號</param>
        /// <returns>代理人基本信息</returns>
        List<GetEmpViewModel> GetEmpList(string proxyWorkNo);
    }
}
