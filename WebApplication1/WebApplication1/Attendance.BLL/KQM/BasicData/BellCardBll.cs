
/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： KqmParameterEditBll.cs
 * 檔功能描述： 考勤參數設定業務邏輯類
 * 
 * 版本：1.0
 * 創建標識： 張明強 2011.12.13
 * 
 */
using System.Collections.Generic;
using GDSBG.MiABU.Attendance.IDAL.KQM.BasicData;
using GDSBG.MiABU.Attendance.Model.KQM.BasicData;
using System.Data;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;

namespace GDSBG.MiABU.Attendance.BLL.KQM.BasicData
{
    public class BellCardBll : BLLBase<IBellCardDal>
    {

        /// <summary>
        /// 查詢卡鐘信息,用於填充頁面下拉列表
        /// </summary>
        /// <param name="model">要查詢的model</param>
        /// <returns>返回的datatable</returns>
        public DataTable GetKQMBellCard(BellCardModel model)
        {
            return DAL.GetKQMBellCard(model);
        }

        /// <summary>
        /// 卡鐘資料維護——查詢
        /// </summary>
        /// <param name="model"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public DataTable GetBellCard(BellCardModel model, int pageIndex, int pageSize, out int totalCount)
        {
            return DAL.GetBellCard(model, pageIndex, pageSize,out totalCount);
        }



        /// <summary>
        ///新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddBellCard(BellCardModel model, SynclogModel logmodel)
        {
            return DAL.AddBellCard(model,logmodel);
        }


        /// <summary>
        /// 根據主鍵修改卡鐘資料
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateBellCardByKey(BellCardModel model, SynclogModel logmodel)
        {
            return DAL.UpdateBellCardByKey(model,logmodel);
        }


        /// <summary>
        /// 刪除一個卡鐘
        /// </summary>
        /// <param name="BellNo"></param>
        /// <returns></returns>
        public int DeleteBellCardByKey(string BellNo, SynclogModel logmodel)
        {
            return DAL.DeleteBellCardByKey(BellNo,logmodel);
        }

        /// <summary>
        /// 獲得卡鐘類別
        /// </summary>
        /// <returns></returns>
        public DataTable GetBellType()
        {
            return DAL.GetBellType();
        }

    }
}
