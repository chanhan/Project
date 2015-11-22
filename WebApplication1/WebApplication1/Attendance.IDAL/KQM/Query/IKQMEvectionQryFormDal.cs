/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： IHrmEmpOtherMoveDal.cs
 * 檔功能描述：出差明細查詢功能模組接口類
 * 
 * 版本：1.0
 * 創建標識： 陈函 2011.12.30
 * 
 */
using System.Collections.Generic;
using System.Data;
using GDSBG.MiABU.Attendance.Common.Attributes;
using GDSBG.MiABU.Attendance.Model.KQM.Query;

namespace GDSBG.MiABU.Attendance.IDAL.KQM.Query
{
    [RefClass("KQM.Query.KQMEvectionQryFormDal")]
   public interface IKQMEvectionQryFormDal
    {
        /// <summary>
        /// 查詢數據綁定DropDownList
        /// </summary>
        /// <param name="dataType">參數類型</param>
        /// <returns>查詢結果DataTable</returns>
       DataTable getData(string dataType);
       /// <summary>
       /// 查詢出差明細
       /// </summary>
       /// <param name="Privileged">是否有組織權限</param>
       /// <param name="depName">部門名稱</param>
       /// <param name="Billno">申請單號</param>
       /// <param name="Workno">工號</param>
       /// <param name="Localname">姓名</param>
       /// <param name="Evectiontype">出差類別</param>
       /// <param name="Status">表單狀態</param>
       /// <param name="StartDate">出差日期(起)</param>
       /// <param name="EndDate">出差日期(迄)</param>
       /// <param name="Evectionaddress">出差地址</param>
       /// <param name="PageIndex">分頁索引</param>
       /// <param name="PageSize">每頁顯示的記錄數</param>
       /// <param name="totalCount">總記錄數</param>
       /// <returns>查詢結果DataTable</returns>
       DataTable SelectEvectionData(bool Privileged, string sqlDep, string depName, string Billno, string Workno, string Localname, string Evectiontype, string Status, string StartDate, string EndDate, string Evectionaddress, int PageIndex, int PageSize, out int totalCount);
       /// <summary>
       /// 查詢出差明細，用於導出Excel
       /// </summary>
       /// <param name="Privileged">是否有組織權限</param>
       /// <param name="depName">部門名稱</param>
       /// <param name="Billno">申請單號</param>
       /// <param name="Workno">工號</param>
       /// <param name="Localname">姓名</param>
       /// <param name="Evectiontype">出差類別</param>
       /// <param name="Status">表單狀態</param>
       /// <param name="StartDate">出差日期(起)</param>
       /// <param name="EndDate">出差日期(迄)</param>
       /// <param name="Evectionaddress">出差地址</param>
       /// <returns>查詢結果List</returns>
       List<EvectionApplyVModel> SelectEvectionData(bool Privileged, string sqlDep, string depName, string Billno, string Workno, string Localname, string Evectiontype, string Status, string StartDate, string EndDate, string Evectionaddress);

      // DataTable SelectEvectionData(bool Privileged, string sqlDep, EvectionApplyVModel evectionApplyVModel, int currentPageIndex, int pageSize, out int totalCount);
    }
}
