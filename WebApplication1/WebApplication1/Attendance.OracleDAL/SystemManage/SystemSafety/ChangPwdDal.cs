/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： ChangPwdOracleDAL.cs
 * 檔功能描述： 密碼修改數據操作類
 * 
 * 版本：1.0
 * 創建標識： 高子焱 2011.11.30
 * 
 */

using System;
using System.Data;
using System.Data.OracleClient;
using GDSBG.MiABU.Attendance.IDAL.SystemManage.SystemSafety;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;

namespace GDSBG.MiABU.Attendance.OracleDAL.SystemManage.SystemSafety
{
    public class ChangPwdDal : DALBase<PersonModel>, IChangPwdDal
    {
        /// <summary>
        /// 更新密碼和驗證
        /// </summary>
        /// <param name="userno">用戶帳號</param>
        /// <param name="oldpwd">舊密碼</param>
        /// <param name="newpwd">新密碼</param>
        /// <returns>返回值的不同來標識更新和驗證的狀態i=0 更新失敗 i=1 更新成功 i=2 舊密碼錯誤</returns>
        public int UpdateUserByKey(string userno, string oldpwd, string newpwd, SynclogModel logmodel)
        {

            OracleParameter outPara = new OracleParameter("a_out", OracleType.Int32);
            outPara.Direction = ParameterDirection.Output;
            int i = DalHelper.ExecuteNonQuery("sc_pwd_chang.change_pwd", CommandType.StoredProcedure,
                new OracleParameter("v_user", userno), new OracleParameter("v_pwd", oldpwd), new OracleParameter("v_newpwd", newpwd), outPara,
                new OracleParameter("p_transactiontype", logmodel.TransactionType), new OracleParameter("p_levelno", logmodel.LevelNo), new OracleParameter("p_fromhost", logmodel.FromHost),
                new OracleParameter("p_tohost", logmodel.ToHost == null ? "" : logmodel.ToHost.ToString()), new OracleParameter("p_docno", logmodel.DocNo == null ? "" : logmodel.DocNo.ToString()), new OracleParameter("p_processflag", logmodel.ProcessFlag),
                new OracleParameter("p_processowner", logmodel.ProcessOwner));
            return i = Convert.ToInt32(outPara.Value);
        }

        /// <summary>
        /// 根據主鍵修改Mail等
        /// </summary>
        /// <param name="model">要修改的Model</param>
        /// <returns>是否成功</returns>
        public bool UpdateMailByKey(PersonModel model, SynclogModel logmodel)
        {
            //int i = DalHelper.UpdateByKey(model, false);
            //return i != -1;
            int i = DalHelper.ExecuteNonQuery("update gds_sc_person set mail='" + model.Mail + "', tel='" + model.Tel + "', mobile='" + model.Mobile + "'  where personcode='"+model.Personcode+"'",logmodel);
            return i != -1;
        }
        /// <summary>
        /// 根據登錄人的工號查詢登錄人的信息（郵件、分機和手機）
        /// </summary>
        /// <param name="personCode"></param>
        /// <returns></returns>
        public DataTable GetPerInfo(string personCode)
        {
            return DalHelper.ExecuteQuery("select mail,tel,mobile from gds_sc_person  where personcode=:personCode", new OracleParameter("personCode", personCode));
        }

    }
}
