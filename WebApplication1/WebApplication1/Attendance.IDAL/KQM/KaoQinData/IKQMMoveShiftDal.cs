
/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： IKQMMoveShiftDal.cs
 * 檔功能描述： 彈性調班數據操作接口
 * 
 * 版本：1.0
 * 創建標識： 張明強 2011.12.8
 * 
 */

using System;
using System.Collections.Generic;
using GDSBG.MiABU.Attendance.Common.Attributes;
using GDSBG.MiABU.Attendance.Model.KQM.KaoQinData;
using System.Data;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;

namespace GDSBG.MiABU.Attendance.IDAL.KQM.KaoQinData
{
    /// <summary>
    /// 缺勤規則數據操作接口
    /// </summary>
    [RefClass("KQM.KaoQinData.KQMMoveShiftDal")]
    public interface IKQMMoveShiftDal
    {
        /// <summary>
        /// 分頁查詢彈性調班資料
        /// </summary>
        /// <param name="model"></param>
        /// <param name="depCode"></param>
        /// <param name="workDate1"></param>
        /// <param name="workDate2"></param>
        /// <param name="noWorkDate1"></param>
        /// <param name="noWorkDate2"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        DataTable GetMoveShiftTable(MoveShiftModel model, string sqlDep, string depCode, string workDate1, string workDate2, string noWorkDate1, string noWorkDate2, int pageIndex, int pageSize, out int totalCount);


        /// <summary>
        /// 獲取ParaValue
        /// </summary>
        /// <returns></returns>
        string  GetValue(string flag, MoveShiftModel moveShiftModel);

        /// <summary>
        /// 根據model刪除記錄
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        int DeleteMoveShift(MoveShiftModel model,SynclogModel logmodel);


        /// <summary>
        /// 重新計算考勤記錄
        /// </summary>
        /// <param name="sWorkNo"></param>
        /// <param name="sOrgCode"></param>
        /// <param name="sFromKQDate"></param>
        /// <param name="sToKQDate"></param>
        void GetKaoQinData(string sWorkNo, string sOrgCode, string sFromKQDate, string sToKQDate);



        /// <summary>
        /// 獲取員工信息
        /// </summary>
        /// <param name="EmployeeNo"></param>
        /// <returns></returns>
        DataTable GetVData(string EmployeeNo,string sqlDep);

        /// <summary>
        /// 查詢員工具體調班信息
        /// </summary>
        /// <param name="EmployeeNo"></param>
        /// <param name="WorkDate"></param>
        /// <param name="NoWorkDate"></param>
        /// <returns></returns>
        DataTable GetData(MoveShiftModel moveShiftModel, string condition, string hidWorkDate);

        /// <summary>
        /// 獲取加班類型
        /// </summary>
        /// <param name="sWorkNo"></param>
        /// <param name="sDate"></param>
        /// <returns></returns>
        string GetOTType(string workNo, string date);


        /// <summary>
        /// 新增功能
        /// </summary>
        /// <param name="model">要新增的功能Model</param>
        /// <returns>是否成功</returns>
        bool AddMoveShift(MoveShiftModel model,SynclogModel logmodel);
       

        /// <summary>
        /// 根據主鍵修改功能
        /// </summary>
        /// <param name="model">要修改的功能Model</param>
        /// <returns>是否成功</returns>
        bool UpdateMoveShiftByKey(MoveShiftModel model,SynclogModel logmodel);

        /// <summary>
        /// 根據主鍵修改功能
        /// </summary>
        /// <param name="model">要修改的功能Model</param>
        /// <returns>是否成功</returns>
        bool UpdateMoveShift(MoveShiftModel model, string workDate, SynclogModel logmodel);

        /// <summary>
        /// 導入彈性調班信息,正確信息插入正式表,錯誤信息返回datatable
        /// </summary>
        /// <param name="createUser">創建人</param>
        /// <returns>返回的datatable</returns>
        DataTable GetTempTableErrorData(string createUser, out int successNum, out int errorNum,SynclogModel logmodel);
     


        /// <summary>
        /// 將datatable轉換為List
        /// </summary>
        /// <param name="dt">徐轉化的Datatable</param>
        /// <returns></returns>
        List<MoveShiftModel> GetList(DataTable dt);


        /// <summary>
        /// 查詢彈性調班資料
        /// </summary>
        /// <param name="model"></param>
        /// <param name="depCode"></param>
        /// <param name="workDate1"></param>
        /// <param name="workDate2"></param>
        /// <param name="noWorkDate1"></param>
        /// <param name="noWorkDate2"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        DataTable GetMoveShiftTable(MoveShiftModel model, string sqlDep, string depCode, string workDate1, string workDate2, string noWorkDate1, string noWorkDate2);


        /// <summary>
        /// 根據工號和調班日期獲取調班信息的LIST
        /// </summary>
        /// <param name="workNo"></param>
        /// <param name="workDate"></param>
        /// <returns></returns>
        List<MoveShiftModel> GetMoveShiftList(string workNo, string workDate);


        /// <summary>
        /// 根據員工工號和日期判斷獲取當天的加班信息
        /// </summary>
        /// <param name="workNo"></param>
        /// <param name="oTDate"></param>
        /// <returns></returns>
        DataTable GetOTInfo(string workNo, string oTDate);

        /// <summary>
        /// 判斷某一時間區間是否屬於另一時間區間
        /// </summary>
        /// <param name="begintime"></param>
        /// <param name="endtime"></param>
        /// <param name="begintimeflag"></param>
        /// <param name="endtimeflag"></param>
        /// <returns></returns>
        string GetTimeSpanFlag(string begintime, string endtime, string begintimeflag, string endtimeflag);
        
    }


}
