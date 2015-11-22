/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： LeaveTypeBll.cs
 * 檔功能描述：請假類別定義業務邏輯類
 * 
 * 版本：1.0
 * 創建標識： 陈函 2011.12.13
 * 
 */
using System.Data;
using GDSBG.MiABU.Attendance.IDAL.KQM.BasicData;
using GDSBG.MiABU.Attendance.Model.KQM.BasicData;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;

namespace GDSBG.MiABU.Attendance.BLL.KQM.BasicData
{
    public class LeaveTypeBll : BLLBase<ILeaveTypeDal>
    {
        /// <summary>
        /// 查詢請假類別定義
        /// </summary>
        /// <param name="leaveTypeModel">請假類別model</param>
        /// <param name="currentPageIndex">當前頁索引</param>
        /// <param name="pageSize">一頁顯示的記錄數</param>
        /// <param name="totalCount">總記錄數</param>
        /// <returns>查詢請假類別定義結果</returns>
        public DataTable GetLeaveType(LeaveTypeModel leaveTypeModel, int currentPageIndex, int pageSize, out int totalCount)
        {
            return DAL.GetLeaveType(leaveTypeModel, currentPageIndex, pageSize, out  totalCount);
        }
        /// <summary>
        /// 驗證請假類別代碼是否存在
        /// </summary>
        /// <param name="LvTypeCode">請假類別代碼</param>
        /// <returns>是否存在</returns>
        public bool IsExist(string IsExist)
        {
            return DAL.IsExist(IsExist).Rows.Count>0;
        }
        /// <summary>
        /// 添加請假類別定義
        /// </summary>
        /// <param name="leaveTypeModel">請假類別model</param>
        /// <returns>添加是否成功</returns>
        public bool AddLeaveType(LeaveTypeModel leaveTypeModel, SynclogModel logmodel)
        {
            return DAL.AddLeaveType(leaveTypeModel,logmodel)== 1;
        }
        /// <summary>
        /// 修改請假類別定義
        /// </summary>
        /// <param name="leaveTypeModel">請假類別model</param>
        /// <returns>修改是否成功</returns>
        public bool UpDateLeaveType(LeaveTypeModel leaveTypeModel, SynclogModel logmodel)
        {
            return DAL.UpDateLeaveType(leaveTypeModel, logmodel) == 1;
        }
        /// <summary>
        /// 刪除請假類別定義
        /// </summary>
        /// <param name="LvTypeCode">請假類別model</param>
        /// <returns>刪除是否成功</returns>
        public bool DeleteLeaveType(string LvTypeCode, SynclogModel logmodel)
        {
            return DAL.DeleteLeaveType(LvTypeCode,logmodel) ==1;
        }
        /// <summary>
        /// 根據model生效失效請假類別定義表
        /// </summary>
        /// <param name="leaveTypeModel"></param>
        /// <returns>更新是否成功</returns>
        public bool EnableAndDisableLeaveType(LeaveTypeModel leaveTypeModel, SynclogModel logmodel)
        {
            return DAL.EnableAndDisableLeaveType(leaveTypeModel,logmodel) > 0;
        }
    }
}
