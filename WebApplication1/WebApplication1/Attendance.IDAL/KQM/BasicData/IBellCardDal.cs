/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： IBellCardDal.cs
 * 檔功能描述： 卡鐘信息數據操作接口
 * 
 * 版本：1.0
 * 創建標識： 張明強 2011.12.13
 * 
 */

using System;
using System.Collections.Generic;
using GDSBG.MiABU.Attendance.Common.Attributes;
using GDSBG.MiABU.Attendance.Model.KQM.BasicData;
using System.Data;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;
namespace GDSBG.MiABU.Attendance.IDAL.KQM.BasicData
{
    /// <summary>
    /// 卡鐘信息數據操作接口
    /// </summary>
    [RefClass("KQM.BasicData.BellCardDal")]
    public interface IBellCardDal
    {

        /// <summary>
        /// 查詢卡鐘信息,用於填充頁面下拉列表
        /// </summary>
        /// <param name="model">要查詢的model</param>
        /// <returns>返回的datatable</returns>
        DataTable GetKQMBellCard(BellCardModel model);

        /// <summary>
        /// 卡鐘資料維護——查詢
        /// </summary>
        /// <param name="model"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        DataTable GetBellCard(BellCardModel model, int pageIndex, int pageSize, out int totalCount);


        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool AddBellCard(BellCardModel model, SynclogModel logmodel);

        /// <summary>
        /// 根據主鍵修改卡鐘資料
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool UpdateBellCardByKey(BellCardModel model, SynclogModel logmodel);

        /// <summary>
        /// 刪除一個卡鐘
        /// </summary>
        /// <param name="BellNo"></param>
        /// <returns></returns>
        int DeleteBellCardByKey(string BellNo, SynclogModel logmodel);

        /// <summary>
        /// 獲得卡鐘類別
        /// </summary>
        /// <returns></returns>
        DataTable GetBellType();
    }


}
