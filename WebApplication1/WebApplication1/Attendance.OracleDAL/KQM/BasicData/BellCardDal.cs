/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： BellCardDal.cs
 * 檔功能描述： 卡鐘信息數據操作類
 * 
 * 版本：1.0
 * 創建標識： 張明強 2011.12.13
 * 修改標識： 昝望 2011.12.20
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
    public class BellCardDal : DALBase<BellCardModel>, IBellCardDal
    {
        /// <summary>
        /// 查詢卡鐘信息,用於填充頁面下拉列表
        /// </summary>
        /// <param name="model">要查詢的model</param>
        /// <returns>返回的datatable</returns>
        public DataTable GetKQMBellCard(BellCardModel model)
        {
            return DalHelper.Select(model, null);
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
            return DalHelper.Select(model, "bellNo", pageIndex, pageSize, out totalCount);
        }


        /// <summary>
        ///新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddBellCard(BellCardModel model, SynclogModel logmodel)
        {
            return DalHelper.Insert(model,logmodel) != -1;
        }


        /// <summary>
        /// 根據主鍵修改卡鐘資料
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateBellCardByKey(BellCardModel model,SynclogModel logmodel)
        {
            return DalHelper.UpdateByKey(model, true,logmodel) != -1;
        }


        /// <summary>
        /// 刪除一個卡鐘
        /// </summary>
        /// <param name="BellNo"></param>
        /// <returns></returns>
        public int DeleteBellCardByKey(string BellNo, SynclogModel logmodel)
        {
            string str = "delete from GDS_ATT_BELLCARD where BellNo='" + BellNo + "'";
            return DalHelper.ExecuteNonQuery(str,logmodel);
        }



        /// <summary>
        /// 獲得卡鐘類別
        /// </summary>
        /// <returns></returns>
        public DataTable GetBellType()
        {
            string str = "select datacode,datavalue from gds_att_TYPEDATA where datatype='BellType'";
            return DalHelper.ExecuteQuery(str);
        }


    }
}
