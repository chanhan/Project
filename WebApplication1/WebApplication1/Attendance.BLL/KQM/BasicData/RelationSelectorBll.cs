/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： RelationSelectorBll.cs
 * 檔功能描述： 固定參數表業務邏輯類
 * 
 * 版本：1.0
 * 創建標識：高子焱 2011.12.10
 * 
 */

using System.Collections.Generic;
using GDSBG.MiABU.Attendance.IDAL.KQM.BasicData;
using GDSBG.MiABU.Attendance.Model.KQM.BasicData;
using System.Data;
using System.Collections;

namespace GDSBG.MiABU.Attendance.BLL.KQM.BasicData
{
    public class RelationSelectorBll : BLLBase<IRelationSelectorDal>
    {
        /// <summary>
        /// 查詢用戶的權限組織
        /// </summary>
        /// <param name="personCode">用戶工號</param>
        /// <param name="companyId">公司Id</param>
        /// <param name="moduleCode">模組代碼</param>
        /// <returns>返回該用戶的權限組織</returns>
        public DataTable GetTypeDataList(string personCode, string companyId, string moduleCode)
        {
            return DAL.GetTypeDataList(personCode, companyId,moduleCode);
        }
        /// <summary>
        /// 查詢用戶的權限組織
        /// </summary>
        /// <param name="personCode">用戶工號</param>
        /// <param name="companyId">公司Id</param>
        /// <param name="moduleCode">模組代碼</param>
        /// <returns>返回該用戶的權限組織</returns>
        public DataTable GetTypeDataList(string personCode, string companyId, string moduleCode,string delete)
        {
            return DAL.GetTypeDataList(personCode, companyId, moduleCode,delete);
        }
    }
}
