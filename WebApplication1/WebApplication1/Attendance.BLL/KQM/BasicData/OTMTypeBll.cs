/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： OTMTypeBll.cs
 * 檔功能描述： 加班類別定義功能模組業務邏輯類
 * 
 * 版本：1.0
 * 創建標識： 陈函 2011.12.13
 * 
 */
using System;
using System.Data;
using GDSBG.MiABU.Attendance.IDAL.KQM.BasicData;
using GDSBG.MiABU.Attendance.Model.KQM.BasicData;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;

namespace GDSBG.MiABU.Attendance.BLL.KQM.BasicData
{
    public class OTMTypeBll : BLLBase<IOTMTypeDal>
    {

        /// <summary>
        /// 根據條件查詢
        /// </summary>
        /// <param name="attTypeModel">加班類別定義Model</param>
        /// <param name="deptName">查詢結果DataTable</param>
        public DataTable GetAttType(AttTypeModel attTypeModel, int currentPageIndex, int pageSize, out int totalCount, string sqlDep)
        {
            return DAL.GetAttType(attTypeModel, currentPageIndex, pageSize, out  totalCount, sqlDep);
        }
        /// <summary>
        /// 綁定加班類別DropDownList
        /// </summary>
        /// <returns>查詢結果DataTable</returns>
        public DataTable GetAttTypeData()
        {
            return DAL.GetAttTypeData();
        }
        /// <summary>
        /// 新增用戶
        /// </summary>
        /// <param name="attTypeModel">加班類別定義Model</param>
        /// <returns>新增是否成功</returns>
        public bool AddAttType(AttTypeModel attTypeModel, SynclogModel logmodel)
        {
            return DAL.AddAttType(attTypeModel, logmodel) > 0;
        }
        /// <summary>
        /// 是否為事業群用戶
        /// </summary>
        /// <param name="personCode">用戶代碼</param>
        /// <returns>查詢結果</returns>
        public bool IslevelUser(string personCode)
        {
            return Convert.ToInt32(DAL.IslevelUser(personCode).Rows[0][0]) > 0;
        }
        /// <summary>
        /// 更新加班類別定義
        /// </summary>
        /// <param name="attTypeModel">加班類別定義Model</param>
        /// <returns>是否更新成功</returns>
        public bool UpDateAttType(AttTypeModel attTypeModel, SynclogModel logmodel)
        {
            return DAL.UpDateAttType(attTypeModel, logmodel) > 0;
        }
        /// <summary>
        /// 根據Model刪除加班類別定義表的數據
        /// </summary>
        /// <param name="attTypeModel">加班類別定義Model</param>
        /// <returns>刪除是否成功</returns>
        public bool DeleteAttType(AttTypeModel attTypeModel, SynclogModel logmodel)
        {
            return DAL.DeleteAttType(attTypeModel,logmodel) > 0;
        }

        /// <summary>
        /// 生效加班類別定義
        /// </summary>
        /// <param name="attTypeModel">加班類別定義Model</param>
        /// <returns>生效是否成功</returns>
        public bool EnableAttType(AttTypeModel attTypeModel, SynclogModel logmodel)
        {
            return DAL.EnableAttType(attTypeModel,logmodel) > 0;
        }

        public bool isExistsOTM(AttTypeModel attTypeModel)
        {
            return DAL.isExistsOTM( attTypeModel).Rows.Count>0;
        }
    }
}
