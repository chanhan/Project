/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： KQMMoveShiftBll.cs
 * 檔功能描述：彈性調班業務邏輯類
 * 
 * 版本：1.0
 * 創建標識： 張明強 2011.12.8
 * 
 */
using System.Collections.Generic;
using GDSBG.MiABU.Attendance.IDAL.KQM.KaoQinData;
using GDSBG.MiABU.Attendance.Model.KQM.KaoQinData;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;
using System.Data;

namespace GDSBG.MiABU.Attendance.BLL.KQM.KaoQinData
{
    public class KQMMoveShiftBll : BLLBase<IKQMMoveShiftDal>
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
        public DataTable GetMoveShiftTable(MoveShiftModel model, string sqlDep, string depCode, string workDate1, string workDate2, string noWorkDate1, string noWorkDate2, int pageIndex, int pageSize, out int totalCount)
        {

            return DAL.GetMoveShiftTable(model, sqlDep, depCode, workDate1, workDate2, noWorkDate1, noWorkDate2, pageIndex, pageSize, out totalCount);
        }

        /// <summary>
        /// 獲取ParaValue
        /// </summary>
        /// <returns></returns>
        public string GetValue(string flag, MoveShiftModel moveShiftModel)
        {

            return DAL.GetValue(flag, moveShiftModel);
        }

        /// <summary>
        /// 根據model刪除記錄
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int DeleteMoveShift(MoveShiftModel model,SynclogModel logmodel)
        {
            return DAL.DeleteMoveShift(model,logmodel);
        }

        /// <summary>
        /// 重新計算考勤記錄
        /// </summary>
        /// <param name="sWorkNo"></param>
        /// <param name="sOrgCode"></param>
        /// <param name="sFromKQDate"></param>
        /// <param name="sToKQDate"></param>
        public void GetKaoQinData(string sWorkNo, string sOrgCode, string sFromKQDate, string sToKQDate)
        {
            DAL.GetKaoQinData(sWorkNo, sOrgCode, sFromKQDate, sToKQDate);
        }


        /// <summary>
        /// 獲取員工信息
        /// </summary>
        /// <param name="EmployeeNo"></param>
        /// <returns></returns>
        public DataTable GetVData(string EmployeeNo,string sqlDep)
        {
            return DAL.GetVData(EmployeeNo, sqlDep);
        }


        /// <summary>
        /// 查詢員工具體調班信息
        /// </summary>
        /// <param name="EmployeeNo"></param>
        /// <param name="WorkDate"></param>
        /// <param name="NoWorkDate"></param>
        /// <returns></returns>
        public DataTable GetData(MoveShiftModel moveShiftModel, string condition, string hidWorkDate)
        {
            return DAL.GetData(moveShiftModel, condition, hidWorkDate);
        }

        /// <summary>
        /// 獲取加班類型
        /// </summary>
        /// <param name="sWorkNo"></param>
        /// <param name="sDate"></param>
        /// <returns></returns>
        public string GetOTType(string workNo, string date)
        {
            return DAL.GetOTType(workNo, date);
        }


        /// <summary>
        /// 新增功能
        /// </summary>
        /// <param name="model">要新增的功能Model</param>
        /// <returns>是否成功</returns>
        public bool AddMoveShift(MoveShiftModel model,SynclogModel logmodel)
        {
            return DAL.AddMoveShift(model,logmodel);
        }

        /// <summary>
        /// 根據主鍵修改功能
        /// </summary>
        /// <param name="model">要修改的功能Model</param>
        /// <returns>是否成功</returns>
        public bool UpdateMoveShiftByKey(MoveShiftModel model,SynclogModel logmodel)
        {

            return DAL.UpdateMoveShiftByKey(model,logmodel);
        }

        /// <summary>
        /// 根據主鍵修改功能
        /// </summary>
        /// <param name="model">要修改的功能Model</param>
        /// <returns>是否成功</returns>
        public bool UpdateMoveShift(MoveShiftModel model,string workDate ,SynclogModel logmodel)
        {

            return DAL.UpdateMoveShift(model,workDate, logmodel);
        }

        /// <summary>
        /// 導入彈性調班信息,正確信息插入正式表,錯誤信息返回datatable
        /// </summary>
        /// <param name="createUser">創建人</param>
        /// <returns>返回的datatable</returns>
        public DataTable GetTempTableErrorData(string createUser, out int successNum, out int errorNum,SynclogModel logmodel)
        {
            return DAL.GetTempTableErrorData(createUser, out successNum, out errorNum,logmodel);

        }


        /// <summary>
        /// 將datatable轉換為List
        /// </summary>
        /// <param name="dt">徐轉化的Datatable</param>
        /// <returns></returns>
        public List<MoveShiftModel> GetList(DataTable dt)
        {
            return DAL.GetList(dt);
        }

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
        public DataTable GetMoveShiftTable(MoveShiftModel model, string sqlDep, string depCode, string workDate1, string workDate2, string noWorkDate1, string noWorkDate2)
        {
            return DAL.GetMoveShiftTable(model, sqlDep, depCode, workDate1, workDate2, noWorkDate1, noWorkDate2);
        }
        /// <summary>
        /// 根據工號和調班日期獲取調班信息的LIST
        /// </summary>
        /// <param name="workNo"></param>
        /// <param name="workDate"></param>
        /// <returns></returns>
        public List<MoveShiftModel> GetMoveShiftList(string workNo, string workDate)
        {
            return DAL.GetMoveShiftList(workNo, workDate);
        }
        /// <summary>
        /// 根據員工工號和日期判斷獲取當天的加班信息
        /// </summary>
        /// <param name="workNo"></param>
        /// <param name="oTDate"></param>
        /// <returns></returns>
        public DataTable GetOTInfo(string workNo, string oTDate)
        {
            return DAL.GetOTInfo(workNo, oTDate);
        }

        /// <summary>
        /// 判斷某一時間區間是否屬於另一時間區間
        /// </summary>
        /// <param name="begintime"></param>
        /// <param name="endtime"></param>
        /// <param name="begintimeflag"></param>
        /// <param name="endtimeflag"></param>
        /// <returns></returns>
        public string GetTimeSpanFlag(string begintime, string endtime, string begintimeflag, string endtimeflag)
        {
            return DAL.GetTimeSpanFlag(begintime, endtime, begintimeflag, endtimeflag);
        }

    }
}
