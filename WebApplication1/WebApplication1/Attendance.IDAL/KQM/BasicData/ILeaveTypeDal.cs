/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： ILeaveTypeDal.cs
 * 檔功能描述： 請假類別定義功能模組接口類
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
    [RefClass("KQM.BasicData.LeaveTypeDal")]
    public interface ILeaveTypeDal
    {
        /// <summary>
        /// 查詢請假類別定義
        /// </summary>
        /// <param name="leaveTypeModel">請假類別model</param>
        /// <param name="currentPageIndex">當前頁索引</param>
        /// <param name="pageSize">一頁顯示的記錄數</param>
        /// <param name="totalCount">總記錄數</param>
        /// <returns>查詢請假類別定義結果</returns>
        DataTable GetLeaveType(LeaveTypeModel leaveTypeModel, int currentPageIndex, int pageSize, out int totalCount);

        /// <summary>
        /// 驗證請假類別代碼是否存在
        /// </summary>
        /// <param name="LvTypeCode">請假類別代碼</param>
        /// <returns>查詢的結果</returns>
        DataTable IsExist(string IsExist);

        /// <summary>
        /// 添加請假類別定義
        /// </summary>
        /// <param name="leaveTypeModel">請假類別model</param>
        /// <returns>添加的行數</returns>
        int AddLeaveType(LeaveTypeModel leaveTypeModel, SynclogModel logmodel);
        /// <summary>
        /// 修改請假類別定義
        /// </summary>
        /// <param name="leaveTypeModel">請假類別model</param>
        /// <returns>修改的行數</returns>
        int UpDateLeaveType(LeaveTypeModel leaveTypeModel, SynclogModel logmodel);
        /// <summary>
        /// 刪除請假類別定義
        /// </summary>
        /// <param name="LvTypeCode">請假類別model</param>
        /// <returns>刪除的行數</returns>
        int DeleteLeaveType(string LvTypeCode, SynclogModel logmodel);
        /// <summary>
        /// 根據model生效失效請假類別定義表
        /// </summary>
        /// <param name="leaveTypeModel"></param>
        /// <returns>更新的行數</returns>
        int EnableAndDisableLeaveType(LeaveTypeModel leaveTypeModel, SynclogModel logmodel);
    }
}
