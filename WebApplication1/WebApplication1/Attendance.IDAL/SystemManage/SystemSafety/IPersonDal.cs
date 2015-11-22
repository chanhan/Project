/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： IModuleDal.cs
 * 檔功能描述： 用戶資料數據操作接口
 * 
 * 版本：1.0
 * 創建標識： 昝望 2011.12.1
 * 
 */

using System;
using System.Collections.Generic;
using GDSBG.MiABU.Attendance.Common.Attributes;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;
using System.Data;
using GDSBG.MiABU.Attendance.Model.KQM.BasicData;
namespace GDSBG.MiABU.Attendance.IDAL.SystemManage.SystemSafety
{
    /// <summary>
    /// 用戶資料數據操作接口
    /// </summary>
    [RefClass("SystemManage.SystemSafety.PersonDal")]
    public interface IPersonDal
    {
        /// <summary>
        /// 根據主鍵獲得用戶Model
        /// </summary>
        /// <param name="model">給出主鍵值的Model</param>
        /// <returns>返回對應主鍵的Model</returns>
        DataTable GetPerson(PersonModel model, int pageIndex, int pageSize, out int totalCount, string sqlDep);

        /// <summary>
        /// 導出查詢
        /// </summary>
        /// <param name="model">給出主鍵值的Model</param>
        /// <returns>返回對應主鍵的Model</returns>
        DataTable GetPerson(PersonModel model, string sqlDep);

        /// <summary>
        /// 新增用戶
        /// </summary>
        /// <param name="model">要新增的用戶Model</param>
        /// <returns>是否成功</returns>
        bool AddPerson(PersonModel model, SynclogModel logmodel);

        /// <summary>
        /// 根據主鍵修改用戶
        /// </summary>
        /// <param name="model">要修改的用戶Model</param>
        /// <returns>是否成功</returns>
        bool UpdatePersonByKey(PersonModel model, SynclogModel logmodel);

        /// <summary>
        /// 刪除一個功能及子功能
        /// </summary>
        /// <param name="functionId">要刪除的功能Id</param>
        /// <returns>刪除功能條數</returns>
        int DeletePersonByKey(string personCode,SynclogModel logmodel);

        /// <summary>
        /// 獲得用戶功能清單
        /// </summary>
        /// <returns>用戶清單ModuleModel集</returns>
        DataTable GetPersonList();


        /// <summary>
        /// 根據Model條件查詢記錄數
        /// </summary>
        /// <param name="model">條件Model</param>
        /// <param name="isFuzzy">是否模糊匹配，為true時不區分大小寫并以Like方式查詢</param>
        /// <returns>記錄數</returns>
        int GetCount(PersonModel model);

        /// <summary>
        /// 重置密碼
        /// </summary>
        /// <param name="userno"></param>
        /// <param name="pwd"></param>
        /// <param name="logmodel"></param>
        /// <returns></returns>
        int UpdatePWDByKey(string userno, string pwd, SynclogModel logmodel);

        /// <summary>
        /// 保存用戶組織關聯信息
        /// </summary>
        /// <param name="personcode"></param>
        /// <param name="moduleCode"></param>
        /// <param name="companyId"></param>
        /// <param name="depts"></param>
        /// <param name="p_user"></param>
        /// <returns></returns>
        int SavePersonDeptData(string personcode, string rolecode, string companyId, string depts, string p_user);


        /// <summary>
        /// 將datatable轉換為List
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        List<PersonModel> GetList(DataTable dt);

        DataTable selectLanguage();

        DataTable selectDepLevel();
        DataTable selectRoles();

        /// <summary>
        /// 根據Model獲取人員信息
        /// </summary>
        /// <param name="model">人員信息Model</param>
        /// <returns>人員信息集</returns>
        List<PersonModel> GetPersonUserId(string userId);

        /// <summary>
        /// 按登陸用戶信息查詢數據庫(用戶登陸)
        /// </summary>
        /// <param name="userId">用戶名</param>
        /// <param name="password">密碼</param>
        /// <returns></returns>
        List<PersonModel> GetPersonUserId(string userId, string password);

        List<PersonModel> GetPersonUserId(string userId, string password, string ismail);

        /// <summary>
        /// 根據登陸用戶查詢該用戶是否登陸并返回登陸用戶資料
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        List<PersonModel> GetPersonLoginId(string userId);


        
        /// <summary>
        /// 根據用戶名查詢工號信息
        /// </summary>
        /// <param name="workno"></param>
        /// <returns></returns>
        DataTable GetEmployeeInfo(string workno);

        /// <summary>
        /// 根據用戶名查詢工號信息
        /// </summary>
        /// <param name="workno"></param>
        /// <returns></returns>
        DataTable GetEmployeeInfoList(string workno);
        /// <summary>
        /// 根據用戶名和權限查詢用戶信息———外出申請修改功能使用
        /// </summary>
        /// <param name="workno"></param>
        /// <returns></returns>
        DataTable GetEmployeeInfo(string workNo, string sqlDep);
    }
}
