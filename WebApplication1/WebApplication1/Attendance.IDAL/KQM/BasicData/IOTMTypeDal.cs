/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： IOTMTypeDal.cs
 * 檔功能描述： 加班類別定義功能模組接口類
 * 
 * 版本：1.0
 * 創建標識： 陈函 2011.12.13
 * 
 */
using System.Data;
using GDSBG.MiABU.Attendance.Common.Attributes;
using GDSBG.MiABU.Attendance.Model.KQM.BasicData;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;

namespace GDSBG.MiABU.Attendance.IDAL.KQM.BasicData
{
    [RefClass("KQM.BasicData.OTMTypeDal")]
    public interface IOTMTypeDal
    {
        /// <summary>
        /// 根據條件查詢
        /// </summary>
        /// <param name="attTypeModel">加班類別定義Model</param>
        /// <param name="deptName">查詢結果DataTable</param>
         DataTable GetAttType(AttTypeModel attTypeModel,int currentPageIndex, int pageSize, out int totalCount,string sqlDep);

         /// <summary>
         /// 綁定加班類別DropDownList
         /// </summary>
         /// <returns>查詢結果DataTable</returns>
         DataTable GetAttTypeData( );

        /// <summary>
         /// 新增加班類別定義
        /// </summary>
         /// <param name="attTypeModel">加班類別定義Model</param>
        /// <returns>新增的行數</returns>
         int AddAttType(AttTypeModel attTypeModel,SynclogModel logmodel);

         /// <summary>
         /// 是否為事業群用戶
         /// </summary>
         /// <param name="personCode">用戶代碼</param>
         /// <returns>查詢結果</returns>
         DataTable IslevelUser(string personCode);

         /// <summary>
         /// 更新加班類別定義
         /// </summary>
         /// <param name="attTypeModel">加班類別定義Model</param>
         /// <returns>更新的行數</returns>
         int UpDateAttType(AttTypeModel attTypeModel,SynclogModel logmodel);

         /// <summary>
         /// 根據Model刪除加班類別定義表的數據
         /// </summary>
         /// <param name="attTypeModel">加班類別定義Model</param>
         /// <returns>刪除的行數</returns>
         int DeleteAttType(AttTypeModel attTypeModel, SynclogModel logmodel);

         /// <summary>
         /// 生效加班類別定義
         /// </summary>
         /// <param name="attTypeModel">加班類別定義Model</param>
         /// <returns>生效是否成功</returns>
         int EnableAttType(AttTypeModel attTypeModel, SynclogModel logmodel);

         DataTable isExistsOTM(AttTypeModel attTypeModel);
    }
}
