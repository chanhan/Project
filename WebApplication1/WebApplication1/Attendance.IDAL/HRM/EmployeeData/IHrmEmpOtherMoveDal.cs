/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： IHrmEmpOtherMoveDal.cs
 * 檔功能描述：加班類別異動功能模組接口類
 * 
 * 版本：1.0
 * 創建標識： 陈函 2011.12.23
 * 
 */
using System.Data;
using GDSBG.MiABU.Attendance.Common.Attributes;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;

namespace GDSBG.MiABU.Attendance.IDAL.HRM.EmployeeData
{
    [RefClass("HRM.EmployeeData.HrmEmpOtherMoveDal")]
    public interface IHrmEmpOtherMoveDal
    {
       

        /// <summary>
        /// 判斷是否有組織權限
        /// </summary>
        /// <param name="moduleCode">模組代碼</param>
        /// <returns>查詢結果DataTable</returns>
        DataTable GetDataByCondition(string moduleCode);
        /// <summary>
        /// 查詢
        /// </summary>
        /// <param name="Privileged">是否有組織權限</param>
        /// <param name="depName">部門名稱</param>
        /// <param name="historyMove">歷史異動</param>
        /// <param name="workNo">工號</param>
        /// <param name="localName">姓名</param>
        /// <param name="applyMan">申請人</param>
        /// <param name="moveType">異動類別</param>
        /// <param name="beforeValueName">異動前</param>
        /// <param name="afterValueName">異動后</param>
        /// <param name="moveState">異動狀態</param>
        /// <param name="effectDateFrom">生效起始日期</param>
        /// <param name="effectDateTo">生效截止日期</param>
        /// <param name="moveReason">異動原因</param>
        /// <param name="pageIndex">分頁索引</param>
        /// <param name="pageSize">每頁顯示的記錄數</param>
        /// <param name="totalCount">總頁數</param>
        /// <returns>查詢結果DataTable</returns>
        DataTable SelectEmpMove(bool Privileged, string sqlDep,string depName, string historyMove, string workNo, string localName, string applyMan, string moveType, string beforeValueName, string afterValueName, string moveState, string effectDateFrom, string effectDateTo, string moveReason, int pageIndex, int pageSize, out int totalCount);
        /// <summary>
        /// 初始化異動類型
        /// </summary>
        /// 
        DataTable InitddlMoveType();
        /// <summary>
        /// 初始化狀態
        /// </summary>
        DataTable InitddlddlMoveState();
        /// <summary>
        /// 返回員工基本資料
        /// </summary>
        /// <param name="EmployeeNo">工號</param>
        /// <param name="Privileged">是否有組織權限</param>
        /// <returns>員工基本資料查詢結果</returns>
        DataTable GetEmp(string EmployeeNo, bool Privileged, string sqlDep);
        /// <summary>
        /// 根據異動類別點擊放大鏡，彈出窗口
        /// </summary>
        /// <param name="typeName">加班類別</param>
        /// <param name="typeCode">類別代碼</param>
        /// <param name="moveType">異動類型</param>
        /// <returns>查詢結果DataTable</returns>
        DataTable GetDataByCondition(string typeName, string typeCode, string moveType);
        /// <summary>
        /// 根據參數描述返回參數代碼
        /// </summary>
        /// <param name="moveValueName">參數描述</param>
        /// <param name="moveTypeValue">異動類型</param>
        /// <returns>參數代碼</returns>
        DataTable GetDataCode(string moveValueName, string moveTypeValue);
        /// <summary>
        /// 查詢某工號是否已存在相同的異動記錄
        /// </summary>
        /// <param name="processFlag">標誌</param>
        /// <param name="beforeDataCode">異動前</param>
        /// <param name="afterDataCode">異動后</param>
        /// <param name="employeeNo">工號</param>
        /// <param name="effectDate">生效日期</param>
        /// <param name="moveOrder"></param>
        /// <returns>是否已存在相同的異動記錄</returns>
        DataTable IsExistMoveType(string processFlag, string beforeDataCode, string afterDataCode, string employeeNo, string effectDate, string moveOrder);
        /// <summary>
        /// 判斷新增生效日是否小于當前生效日
        /// </summary>
        /// <param name="processFlag">標誌</param>
        /// <param name="employeeNo">工號</param>
        /// <param name="moveTypeValue">異動類別</param>
        /// <param name="effectDate">生效日期</param>
        /// <param name="moveOrder"></param>
        /// <returns>新增生效日是否小于當前生效日DataTable</returns>
        DataTable checkDate(string processFlag, string employeeNo, string moveTypeValue, string effectDate, string moveOrder);
        /// <summary>
        /// 新增修改異動類別
        /// </summary>
        /// <param name="processFlag">新增修改標誌</param>
        /// <param name="employeeNo">工號</param>
        /// <param name="moveTypeValue">異動值</param>
        /// <param name="beforeDataCode">異動前</param>
        /// <param name="afterDataCode">異動后</param>
        /// <param name="effectDate">生效日期</param>
        /// <param name="moveReason">異動原因</param>
        /// <param name="remark">備註</param>
        /// <param name="personCode">登陸工號</param>
        /// <param name="MoveOrder">異動序號</param>
        /// <returns>保存成功標誌</returns>
        int SaveData(string processFlag, string employeeNo, string moveTypeValue, string beforeDataCode, string afterDataCode, string effectDate, string moveReason, string remark, string personCode, string MoveOrder,SynclogModel logmodel);
        /// <summary>
        /// 生成組織樹
        /// </summary>
        /// <param name="personCode">登陸工號</param>
        /// <param name="companID">公司ID</param>
        /// <param name="moudelCode">模組代碼</param>
        /// <returns>查詢結果DataTable</returns>
        DataTable GetAuthorizedTreeDept(string personCode, string companID, string moudelCode);

        /// <summary>
        /// 查詢異動
        /// </summary>
        /// <param name="MoveOrder">異動序號</param>
        /// <param name="EmployeeNo">工號</param>
        /// <param name="Privileged">是否有組織權限</param>
        /// <param name="moduleCode">模組代碼</param>
        /// <param name="personCode">登陸工號</param>
        /// <param name="companyID">公司ID</param>
        /// <returns>查詢結果DataTable</returns>
        DataTable SelectEmpMove(string MoveOrder, string EmployeeNo, bool Privileged, string sqlDep);

        /// <summary>
        /// 導入
        /// </summary>
        /// <param name="createUser">登陸工號</param>
        /// <param name="moduleCode">模組代碼</param>
        /// <param name="companyID">公司ID</param>
        /// <param name="successnum">導入成功記錄數</param>
        /// <param name="errornum">導入失敗記錄數</param>
        /// <returns>導入失敗數據DataTable</returns>
        DataTable ImpoertExcel(string createUser, string moduleCode, string companyID, out int successnum, out int errornum, SynclogModel logmodel);
        /// <summary>
        /// 刪除異動類別
        /// </summary>
        /// <param name="WorkNoMoveOrder">工號與異動序號字符串</param>
        /// <returns>刪除是否成功</returns>
        int DeleteEmpMove(string WorkNoMoveOrder,SynclogModel logmodel);
        /// <summary>
        /// 確認
        /// </summary>
        /// <param name="WorkNoMoveOrderAfterValue">工號與異動序號確認后字符串</param>
        /// <param name="personCode">登陸工號</param>
        /// <returns>確認是否成功</returns>
        int ConfirmData(string WorkNoMoveOrderAfterValue, string personCode, SynclogModel logmodel);
        /// <summary>
        /// 取消確認
        /// </summary>
        /// <param name="WorkNoMoveOrderBeforeValue">工號與異動序號確認前字符串</param>
        /// <param name="personCode">登陸工號</param>
        /// <returns>取消確認是否成功</returns>
        int UnConfirmData(string WorkNoMoveOrderBeforeValue, string personCode, SynclogModel logmodel);
        /// <summary>
        /// 查詢所有異動，用於批量確認
        /// </summary>
        /// <param name="Privileged">是否有組織權限</param>
        /// <param name="depName">部門名稱</param>
        /// <param name="historyMove">歷史異動</param>
        /// <param name="workNo">工號</param>
        /// <param name="localName">姓名</param>
        /// <param name="applyMan">申請人</param>
        /// <param name="moveType">異動類別</param>
        /// <param name="beforeValueName">異動前</param>
        /// <param name="afterValueName">異動后</param>
        /// <param name="moveState">異動狀態</param>
        /// <param name="effectDateFrom">生效起始日期</param>
        /// <param name="effectDateTo">生效截止日期</param>
        /// <param name="moveReason">異動原因</param>
        /// <returns>查詢結果DataTable</returns>
        DataTable SelectEmpMove(bool Privileged, string sqlDep, string depName, string historyMove, string workNo, string localName, string applyMan, string moveType, string beforeValueName, string afterValueName, string moveState, string effectDateFrom, string effectDateTo, string moveReason);
        /// <summary>
        /// 顯示失效的組織
        /// </summary>
        /// <param name="Personcode">工號</param>
        /// <param name="moudelCode">模組代碼</param>
        /// <param name="CompanyId">公司ID</param>
        /// <param name="levelcode">組織級別</param>
        /// <param name="abate">是否顯示失效組織</param>
        /// <param name="logmodel">日誌管理實體類</param>
        void GetDepCodeTable(string Personcode, string moudelCode, string CompanyId, string levelcode, string abate, SynclogModel logmodel);
    }
}
