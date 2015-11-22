/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： ModuleBll.cs
 * 檔功能描述： 用戶資料業務邏輯類
 * 
 * 版本：1.0
 * 創建標識： 昝望 2011.12.1
 * 
 */

using System.Collections.Generic;
using GDSBG.MiABU.Attendance.IDAL.SystemManage.SystemSafety;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;
using System.Data;
using GDSBG.MiABU.Attendance.Model.KQM.BasicData;

namespace GDSBG.MiABU.Attendance.BLL.SystemManage.SystemSafety
{
    /// <summary>
    /// 用戶資料業務邏輯類
    /// </summary>
    public class PersonBll : BLLBase<IPersonDal>
    {
        public DataTable GetPersonList()
        {
            return DAL.GetPersonList();
        }



        #region 根據Model條件查詢記錄數
        /// <summary>
        /// 根據Model條件查詢記錄數
        /// </summary>
        /// <param name="model">條件Model</param>
        /// <returns>記錄數</returns>
        public int GetCount(PersonModel model)
        {
            return DAL.GetCount(model);
        }
        #endregion


        /// <summary>
        /// 刪除一個用戶
        /// </summary>
        /// <param name="functionId">要刪除的用戶Id</param>
        /// <returns>刪除用戶條數</returns>
        public int DeletePersonByKey(string personCode, SynclogModel logmodel)
        {
            return DAL.DeletePersonByKey(personCode,logmodel);
        }

        /// <summary>
        /// 新增用戶
        /// </summary>
        /// <param name="model">要新增的用戶Model</param>
        /// <returns>是否成功</returns>
        public bool AddPerson(PersonModel model, SynclogModel logmodel)
        {
            return DAL.AddPerson(model,logmodel);
        }

        /// <summary>
        /// 根據主鍵修改用戶資料
        /// </summary>
        /// <param name="model">要修改的用戶Model</param>
        /// <returns>是否成功</returns>
        public bool UpdatePersonByKey(PersonModel model, SynclogModel logmodel)
        {
            return DAL.UpdatePersonByKey(model,logmodel);
        }


        /// <summary>
        /// 根據主鍵獲得用戶Model
        /// </summary>
        /// <param name="model">給出主鍵值的Model</param>
        /// <returns>返回對應主鍵的Model</returns>
        public DataTable GetPerson(PersonModel model, int pageIndex, int pageSize, out int totalCount, string sqlDep)
        {
            return DAL.GetPerson(model, pageIndex, pageSize, out totalCount,sqlDep);
        }

        /// <summary>
        /// 導出查詢
        /// </summary>
        /// <param name="model">給出主鍵值的Model</param>
        /// <returns>返回對應主鍵的Model</returns>
        public DataTable GetPerson(PersonModel model, string sqlDep)
        {
            return DAL.GetPerson(model,sqlDep);
        }

        public bool UpdatePWDByKey(string userno, string pwd, SynclogModel logmodel)
        {
            return DAL.UpdatePWDByKey(userno, pwd,logmodel) != 0;
        }

        /// <summary>
        /// 保存用戶組織關聯信息
        /// </summary>
        /// <param name="personcode"></param>
        /// <param name="moduleCode"></param>
        /// <param name="companyId"></param>
        /// <param name="depts"></param>
        /// <param name="p_user"></param>
        /// <returns></returns>
        public int SavePersonDeptData(string personcode, string roleCode, string companyId, string depts, string p_user)
        {
            return DAL.SavePersonDeptData(personcode, roleCode, companyId, depts, p_user);
        }


        /// <summary>
        /// 將datatable轉換為List
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public List<PersonModel> GetList(DataTable dt)
        {
            return DAL.GetList(dt);
        }

        /// <summary>
        /// 查找語言
        /// </summary>
        /// <returns></returns>
        public DataTable selectLanguage()
        {
            return DAL.selectLanguage();
        }

        /// <summary>
        /// 查找層級
        /// </summary>
        /// <returns></returns>
        public DataTable selectDepLevel()
        {
            return DAL.selectDepLevel();
        }

        /// <summary>
        /// 查找群組
        /// </summary>
        /// <returns></returns>
        public DataTable selectRoles()
        {
            return DAL.selectRoles();
        }

        /// <summary>
        /// 根據Model獲取人員信息
        /// </summary>
        /// <param name="model">人員信息Model</param>
        /// <returns>人員信息集</returns>
        public List<PersonModel> GetPersonUserId(string userId)
        {
            return DAL.GetPersonUserId(userId);
        }

        
        /// <summary>
        /// 按登陸用戶信息查詢數據庫(用戶登陸)
        /// </summary>
        /// <param name="userId">用戶名</param>
        /// <param name="password">密碼</param>
        /// <returns></returns>
        public List<PersonModel> GetPersonUserId(string userId, string password)
        {
            return DAL.GetPersonUserId(userId, password);
        }

        public List<PersonModel> GetPersonUserId(string userId, string password, string ismail)
        {
            return DAL.GetPersonUserId(userId, password, ismail);
        }

        /// <summary>
        /// 根據登陸用戶查詢該用戶是否登陸并返回登陸用戶資料
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<PersonModel> GetPersonLoginId(string userId)
        {
            return DAL.GetPersonLoginId(userId);
        }

        
        /// <summary>
        /// 根據用戶名查詢工號信息
        /// </summary>
        /// <param name="workno"></param>
        /// <returns></returns>
        public DataTable GetEmployeeInfo(string workno)
        {
            return DAL.GetEmployeeInfo(workno);
        }
        /// <summary>
        /// 根據用戶名查詢工號信息
        /// </summary>
        /// <param name="workno"></param>
        /// <returns></returns>
        public DataTable GetEmployeeInfoList(string workno)
        {
            return DAL.GetEmployeeInfoList(workno);
        }
        /// <summary>
        /// 根據用戶名和權限查詢用戶信息———外出申請修改功能使用
        /// </summary>
        /// <param name="workno"></param>
        /// <returns></returns>
        public DataTable GetEmployeeInfo(string workNo,string sqlDep)
        {
            return DAL.GetEmployeeInfo(workNo,sqlDep);
        }
    }
}