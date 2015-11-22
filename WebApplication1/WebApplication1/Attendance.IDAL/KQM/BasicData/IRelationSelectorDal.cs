/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： IRelationSelectorDal.cs
 * 檔功能描述： 組織層級設定數據操作接口
 * 
 * 版本：1.0
 * 創建標識： 高子焱 2011.12.10
 * 
 */

using System;
using GDSBG.MiABU.Attendance.Common.Attributes;
using GDSBG.MiABU.Attendance.Model.KQM.BasicData;
using System.Data;
using System.Collections;

namespace GDSBG.MiABU.Attendance.IDAL.KQM.BasicData
{
    /// <summary>
    /// 功能管理數據操作接口
    /// </summary>
    [RefClass("KQM.BasicData.RelationSelectorDal")]
    public interface  IRelationSelectorDal
    {
        /// <summary>
        /// 查詢用戶的權限組織
        /// </summary>
        /// <param name="personCode">用戶代碼</param>
        /// <param name="companyId">公司代碼</param>
        /// <param name="moduleCode">模組代碼</param>
        /// <returns></returns>
        DataTable GetTypeDataList(string personCode, string companyId, string moduleCode);
        /// <summary>
        /// 查詢用戶的權限組織
        /// </summary>
        /// <param name="personCode">用戶工號</param>
        /// <param name="companyId">公司Id</param>
        /// <param name="moduleCode">模組代碼</param>
        /// <returns>返回該用戶的權限組織</returns>
        DataTable GetTypeDataList(string personCode, string companyId, string moduleCode, string delete);
    }
}
