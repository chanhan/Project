/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： IHrmEmpOtherMoveImportDal.cs
 * 檔功能描述：加班類別異動功能模組接口類
 * 
 * 版本：1.0
 * 創建標識： 陈函 2011.12.23
 * 
 */
using System.Collections.Generic;
using GDSBG.MiABU.Attendance.Common.Attributes;
using GDSBG.MiABU.Attendance.Model.HRM.EmployeeData;

namespace GDSBG.MiABU.Attendance.IDAL.HRM.EmployeeData
{
    [RefClass("HRM.EmployeeData.HrmEmpOtherMoveImportDal")]
    public interface IHrmEmpOtherMoveImportDal
    {
        /// <summary>
        /// 查詢所有異動，用於導出Excel
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
        /// <returns>查詢結果List</returns>
        List<HrmEmpOtherMoveModel> SelectEmpMove(bool Privileged, string sqlDep, string depName, string historyMove, string workNo, string localName, string applyMan, string moveType, string beforeValueName, string afterValueName, string moveState, string effectDateFrom, string effectDateTo, string moveReason);
    }
}
