/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： LeaveTypeDal.cs
 * 檔功能描述： 請假類別定義功能模組操作類
 * 
 * 版本：1.0
 * 創建標識： 陈函 2011.12.13
 * 
 */
using System;
using System.Data;
using System.Data.OracleClient;
using GDSBG.MiABU.Attendance.IDAL.KQM.BasicData;
using GDSBG.MiABU.Attendance.Model.KQM.BasicData;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;

namespace GDSBG.MiABU.Attendance.OracleDAL.KQM.BasicData
{
    public class LeaveTypeDal : DALBase<LeaveTypeModel>, ILeaveTypeDal
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
            return DalHelper.Select(leaveTypeModel, "lvtypecode", currentPageIndex, pageSize, out totalCount);
        }


        /// <summary>
        /// 驗證請假類別代碼是否存在
        /// </summary>
        /// <param name="LvTypeCode">請假類別代碼</param>
        /// <returns>查詢的結果</returns>
        public DataTable IsExist(string LvTypeCode)
        {
            LeaveTypeModel leaveTypeModel = new LeaveTypeModel();
            leaveTypeModel.LvTypeCode = LvTypeCode;
            return DalHelper.Select(leaveTypeModel);
        }

        /// <summary>
        /// 添加請假類別定義
        /// </summary>
        /// <param name="leaveTypeModel">請假類別model</param>
        /// <returns>添加的行數</returns>
        public int AddLeaveType(LeaveTypeModel leaveTypeModel, SynclogModel logmodel)
        {
            string cmdText = "gds_att_addleavetypepro";
            OracleParameter opstandardhours = new OracleParameter("P_STANDARDHOURS", Convert.ToDouble(leaveTypeModel.StandardHours));
            opstandardhours.OracleType = OracleType.Int32;
            OracleParameter opminhours = new OracleParameter("P_MINHOURS", Convert.ToDouble(leaveTypeModel.MinHours));
            opminhours.OracleType = OracleType.Int32;
            OracleParameter opOutResult = new OracleParameter();
            opOutResult.Direction = ParameterDirection.Output;
            opOutResult.ParameterName = "result";
            opOutResult.OracleType = OracleType.Int32;
            DalHelper.ExecuteNonQuery(cmdText, CommandType.StoredProcedure, opOutResult,
                new OracleParameter("P_LVTYPECODE", leaveTypeModel.LvTypeCode == null ? "" : leaveTypeModel.LvTypeCode),
                new OracleParameter("P_LVTYPENAME", leaveTypeModel.LvTypeName == null ? "" : leaveTypeModel.LvTypeName),
                new OracleParameter("P_USESTATE", leaveTypeModel.UseState == null ? "" : leaveTypeModel.UseState),
                new OracleParameter("P_LIMITDAYS", leaveTypeModel.LimitDays == null ? "" : leaveTypeModel.LimitDays),
                new OracleParameter("P_HASMONEY", leaveTypeModel.HasMoney == null ? "" : leaveTypeModel.HasMoney),
                new OracleParameter("P_PROVE", leaveTypeModel.Prove == null ? "" : leaveTypeModel.Prove),
                new OracleParameter("P_REMARK", leaveTypeModel.Remark == null ? "" : leaveTypeModel.Remark),
                new OracleParameter("P_EFFECTFLAG", leaveTypeModel.EffectFlag == null ? "" : leaveTypeModel.EffectFlag),
                new OracleParameter("P_MODIFIER", leaveTypeModel.Modifier == null ? "" : leaveTypeModel.Modifier),
                new OracleParameter("P_ISCLUDEHOLIDAY", leaveTypeModel.IscludeHoliday == null ? "" : leaveTypeModel.IscludeHoliday),
                new OracleParameter("P_ISTESTIFY", leaveTypeModel.Prove != null ? "Y" : "N"),
                new OracleParameter("P_ISALLOWPCM", leaveTypeModel.IsAllowPCM == null ? "" : leaveTypeModel.IsAllowPCM),
                opminhours,
                opstandardhours,
                new OracleParameter("P_FITSEX", leaveTypeModel.FitSex == null ? "" : leaveTypeModel.FitSex),
                 new OracleParameter("p_transactiontype", logmodel.TransactionType == null ? "" : logmodel.TransactionType.ToString()),
                   new OracleParameter("p_levelno", logmodel.LevelNo == null ? "" : logmodel.LevelNo.ToString()),
                   new OracleParameter("p_fromhost", logmodel.FromHost == null ? "" : logmodel.FromHost.ToString()),
                   new OracleParameter("p_tohost", logmodel.ToHost == null ? "" : logmodel.ToHost.ToString()),
                   new OracleParameter("p_docno", logmodel.DocNo == null ? "" : logmodel.DocNo.ToString()),
                   new OracleParameter("p_processflag", logmodel.ProcessFlag == null ? "" : logmodel.ProcessFlag.ToString()),
                   new OracleParameter("p_processowner", logmodel.ProcessOwner == null ? "" : logmodel.ProcessOwner.ToString())
                );
            return Convert.ToInt32(opOutResult.Value);
        }

        /// <summary>
        /// 修改請假類別定義
        /// </summary>
        /// <param name="leaveTypeModel">請假類別model</param>
        /// <returns>修改的行數</returns>
        public int UpDateLeaveType(LeaveTypeModel leaveTypeModel, SynclogModel logmodel)
        {
            string cmdText = "gds_att_updateleavetypepro";
            OracleParameter opstandardhours = new OracleParameter("P_STANDARDHOURS", Convert.ToDouble(leaveTypeModel.StandardHours));
            opstandardhours.OracleType = OracleType.Int32;
            OracleParameter opminhours = new OracleParameter("P_MINHOURS", Convert.ToDouble(leaveTypeModel.MinHours));
            opminhours.OracleType = OracleType.Int32;
            OracleParameter opOutResult = new OracleParameter();
            opOutResult.Direction = ParameterDirection.Output;
            opOutResult.ParameterName = "result";
            opOutResult.OracleType = OracleType.Int32;
            DalHelper.ExecuteNonQuery(cmdText, CommandType.StoredProcedure, opOutResult,
                new OracleParameter("P_LVTYPECODE", leaveTypeModel.LvTypeCode == null ? "" : leaveTypeModel.LvTypeCode),
                new OracleParameter("P_LVTYPENAME", leaveTypeModel.LvTypeName == null ? "" : leaveTypeModel.LvTypeName),
                new OracleParameter("P_USESTATE", leaveTypeModel.UseState == null ? "" : leaveTypeModel.UseState),
                new OracleParameter("P_LIMITDAYS", leaveTypeModel.LimitDays == null ? "" : leaveTypeModel.LimitDays),
                new OracleParameter("P_HASMONEY", leaveTypeModel.HasMoney == null ? "" : leaveTypeModel.HasMoney),
                new OracleParameter("P_PROVE", leaveTypeModel.Prove == null ? "" : leaveTypeModel.Prove),
                new OracleParameter("P_REMARK", leaveTypeModel.Remark == null ? "" : leaveTypeModel.Remark),
                new OracleParameter("P_MODIFIER", leaveTypeModel.Modifier == null ? "" : leaveTypeModel.Modifier),
                new OracleParameter("P_ISCLUDEHOLIDAY", leaveTypeModel.IscludeHoliday == null ? "" : leaveTypeModel.IscludeHoliday),
                new OracleParameter("P_ISTESTIFY", leaveTypeModel.Prove != null ? "Y" : "N"),
                new OracleParameter("P_ISALLOWPCM", leaveTypeModel.IsAllowPCM == null ? "" : leaveTypeModel.IsAllowPCM), opminhours, opstandardhours,
                new OracleParameter("P_FITSEX", leaveTypeModel.FitSex == null ? "" : leaveTypeModel.FitSex),
                   new OracleParameter("p_transactiontype", logmodel.TransactionType == null ? "" : logmodel.TransactionType.ToString()),
                   new OracleParameter("p_levelno", logmodel.LevelNo == null ? "" : logmodel.LevelNo.ToString()),
                   new OracleParameter("p_fromhost", logmodel.FromHost == null ? "" : logmodel.FromHost.ToString()),
                   new OracleParameter("p_tohost", logmodel.ToHost == null ? "" : logmodel.ToHost.ToString()),
                   new OracleParameter("p_docno", logmodel.DocNo == null ? "" : logmodel.DocNo.ToString()),
                   new OracleParameter("p_processflag", logmodel.ProcessFlag == null ? "" : logmodel.ProcessFlag.ToString()),
                   new OracleParameter("p_processowner", logmodel.ProcessOwner == null ? "" : logmodel.ProcessOwner.ToString())
                );
            return Convert.ToInt32(opOutResult.Value);
        }

        /// <summary>
        /// 刪除請假類別定義
        /// </summary>
        /// <param name="LvTypeCode">請假類別model</param>
        /// <returns>刪除的行數</returns>
        public int DeleteLeaveType(string LvTypeCode, SynclogModel logmodel)
        {
            string cmdText = "gds_att_deleteleavetypepro";
            OracleParameter oplvtypecode = new OracleParameter();
            oplvtypecode.Direction = ParameterDirection.Input;
            oplvtypecode.ParameterName = "p_lvtype";
            oplvtypecode.Value = LvTypeCode;

            OracleParameter opOutResult = new OracleParameter();
            opOutResult.Direction = ParameterDirection.Output;
            opOutResult.ParameterName = "p_result";
            opOutResult.DbType = DbType.Int32;
            DalHelper.ExecuteNonQuery(cmdText, CommandType.StoredProcedure,
            oplvtypecode, opOutResult,
             new OracleParameter("p_transactiontype", logmodel.TransactionType == null ? "" : logmodel.TransactionType.ToString()),
                   new OracleParameter("p_levelno", logmodel.LevelNo == null ? "" : logmodel.LevelNo.ToString()),
                   new OracleParameter("p_fromhost", logmodel.FromHost == null ? "" : logmodel.FromHost.ToString()),
                   new OracleParameter("p_tohost", logmodel.ToHost == null ? "" : logmodel.ToHost.ToString()),
                   new OracleParameter("p_docno", logmodel.DocNo == null ? "" : logmodel.DocNo.ToString()),
                   new OracleParameter("p_processflag", logmodel.ProcessFlag == null ? "" : logmodel.ProcessFlag.ToString()),
                   new OracleParameter("p_processowner", logmodel.ProcessOwner == null ? "" : logmodel.ProcessOwner.ToString())
            );
            return Convert.ToInt32(opOutResult.Value);
        }

        /// <summary>
        /// 根據model生效失效請假類別定義表
        /// </summary>
        /// <param name="leaveTypeModel"></param>
        /// <returns>更新的行數</returns>
        public int EnableAndDisableLeaveType(LeaveTypeModel leaveTypeModel, SynclogModel logmodel)
        {
            return DalHelper.UpdateByKey(leaveTypeModel, true, logmodel);
        }
    }
}
