/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： OrgmanagerBll.cs
 * 檔功能描述： 組織管理者資料業務邏輯類
 * 
 * 版本：1.0
 * 創建標識： 昝望 2011.12.1
 * 
 */

using System.Collections.Generic;
using GDSBG.MiABU.Attendance.IDAL.SystemManage.SystemSafety;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;
using System.Data;
using GDSBG.MiABU.Attendance.IDAL.HRM;
using GDSBG.MiABU.Attendance.Model.HRM;

namespace GDSBG.MiABU.Attendance.BLL.HRM
{
     public class OrgmanagerBll : BLLBase<IOrgmanagerDal>
    {
        /// <summary>
        /// 查詢組織管理者資料
        /// </summary>
        /// <param name="model">給出主鍵值的Model</param>
        /// <returns>返回對應主鍵的Model</returns>
         public DataTable GetOrgmanager(OrgmanagerModel model, int pageIndex, int pageSize, out int totalCount, string sqlDep)
        {
            return DAL.GetOrgmanager(model, pageIndex, pageSize, out totalCount, sqlDep);
        }


              /// <summary>
        /// 查詢組織管理者資料(用於Ajax)
        /// </summary>
        /// <param name="model">給出主鍵值的Model</param>
        /// <returns>返回對應主鍵的Model</returns>
         public DataTable GetOrgmanager(OrgmanagerModel model)
         {
             return DAL.GetOrgmanager(model);
         }

         /// <summary>
         /// 新增組織管理者
         /// </summary>
         /// <param name="model">要新增的組織管理者Model</param>
         /// <returns>是否成功</returns>
         public int AddOrgmanager(OrgmanagerModel model, SynclogModel logmodel)
         {
             return DAL.AddOrgmanager(model,logmodel);
         }

         /// <summary>
         /// 根據主鍵修改組織管理者資料
         /// </summary>
         /// <param name="model">要修改的組織管理者Model</param>
         /// <returns>是否成功</returns>
         public int UpdateOrgmanagerByKey(OrgmanagerModel model,string olddepcode,string oldworkno, SynclogModel logmodel)
         {
             return DAL.UpdateOrgmanagerByKey(model,olddepcode,oldworkno,logmodel);
         }

         /// <summary>
         /// 刪除一個組織管理者
         /// </summary>
         /// <param name="functionId">要刪除的組織管理者Id</param>
         /// <returns>刪除組織管理者條數</returns>
         public int DeleteOrgmanagerByKey(string workno, string depCode, SynclogModel logmodel)
         {
             return DAL.DeleteOrgmanagerByKey(workno, depCode,logmodel);


         }

            /// <summary>
        /// 更新人員基本表
        /// </summary>
        /// <param name="WorkNo"></param>
        /// <param name="notes"></param>
        /// <param name="managercode"></param>
        /// <param name="managername"></param>
        /// <returns></returns>
         public bool UpdateEmployeeByKey(string WorkNo, string notes, string managercode, string managername, SynclogModel logmodel)
        {
            return DAL.UpdateEmployeeByKey(WorkNo, notes, managercode, managername,logmodel);
        }

        /// <summary>
        /// 獲得管理職信息
        /// </summary>
        /// <returns></returns>
        public DataTable GetManager()
        {
            return DAL.GetManager();
        }

        /// <summary>
        /// 查詢工號對應的姓名與管理職
        /// </summary>
        /// <returns></returns>
        public DataTable GetInfo(string workno)
        {
            return DAL.GetInfo(workno);
        }


        
       /// <summary>
       /// 導入
       /// </summary>
       /// <param name="personcode"></param>
       /// <param name="successnum"></param>
       /// <param name="errornum"></param>
       /// <returns></returns>
        public DataTable ImpoertExcel(string personcode, out int successnum, out int errornum, SynclogModel logmodel)
        {
            return DAL.ImpoertExcel(personcode, out successnum, out errornum,logmodel);
        }

          /// <summary>
        /// 將datatable轉換為List
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public List<OrgmanagerModel> GetList(DataTable dt)
        {
            return DAL.GetList(dt);
        }


         /// <summary>
        /// 組織人員查詢
        /// </summary>
        /// <param name="personcode"></param>
        /// <param name="workno"></param>
        /// <param name="depcode"></param>
        /// <returns></returns>
        public DataTable OrgEmployeeQuery(string personcode, string workno, string depcode)
        {
            return DAL.OrgEmployeeQuery(personcode, workno, depcode);
        }
    }
}
