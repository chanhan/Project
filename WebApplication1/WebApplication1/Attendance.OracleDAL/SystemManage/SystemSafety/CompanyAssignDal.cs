/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： CompanyAssignDal.cs
 * 檔功能描述： 關聯公司設定數據操作類
 * 
 * 版本：1.0
 * 創建標識： 顧陳偉 2011.12.7
 * 
 */

using System;
using System.Data;
using System.Data.OracleClient;
using GDSBG.MiABU.Attendance.IDAL.SystemManage.SystemSafety;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;

namespace GDSBG.MiABU.Attendance.OracleDAL.SystemManage.SystemSafety
{
    public class CompanyAssignDal : DALBase<PersonModel>, ICompanyAssignDal
    {
        /// <summary>
        /// 獲得用戶所關聯的公司代碼
        /// </summary>
        /// <param name="personcode"></param>
        /// <returns></returns>
        public DataTable GetPersonCompany(string personcode)
        {
            //string str = "select a.personcode,a.companyid,b.companyname from gds_sc_person a,gds_sc_company b where b.deleted='N' and a.personcode='" + personcode + "'";
            string str = "select * from (select a.*,b.companyname from gds_sc_personcompany a,gds_sc_company b where a.companyid=b.companyid(+) and b.deleted='N' order by a.companyid) where personcode  =:personcode";
            return DalHelper.ExecuteQuery(str,new OracleParameter(":personcode",personcode));
        }

        /// <summary>
        /// 獲得所有用戶沒有關聯的公司代碼
        /// </summary>
        /// <param name="personcode"></param>
        /// <returns></returns>
        public DataTable GetOtherAllCompany(string personcode)
        {
            //string str = "select companyid,companyname from gds_sc_company where deleted='N' and companyid not in (select companyid from gds_sc_person where personcode='" + personcode + "') order by companyid";
            string str = "select * from gds_sc_company  where companyid not in (select companyid from gds_sc_personcompany where personcode  =:personcode) and deleted='N'";
            return DalHelper.ExecuteQuery(str, new OracleParameter(":personcode", personcode));
        }

        /// <summary>
        /// 保存用戶公司關聯信息
        /// </summary>
        /// <param name="personcode"></param>
        /// <param name="companyList"></param>
        /// <param name="p_user"></param>
        /// <returns></returns>
        public int SavePersonCompany(string personcode, string companyList, string p_user, SynclogModel logmodel)
        {
            if (string.IsNullOrEmpty(companyList))
            {
                string str = "delete from gds_sc_personcompany where personcode =:personcode";
                DalHelper.ExecuteNonQuery(str,logmodel, new OracleParameter(":personcode", personcode));
                return 1;
            }
            else
            {
                OracleParameter outPara = new OracleParameter("p_out", OracleType.Int32);
                outPara.Direction = ParameterDirection.Output;
                DalHelper.ExecuteNonQuery("gds_sc_savepersoncompany", CommandType.StoredProcedure,
                    new OracleParameter("p_personcode", personcode), new OracleParameter("p_companylist", companyList), new OracleParameter("p_user", p_user), outPara,
                    new OracleParameter("p_transactiontype", logmodel.TransactionType == null ? "" : logmodel.TransactionType.ToString()),
                new OracleParameter("p_levelno", logmodel.LevelNo == null ? "" : logmodel.LevelNo.ToString()),
                new OracleParameter("p_fromhost", logmodel.FromHost == null ? "" : logmodel.FromHost.ToString()),
                new OracleParameter("p_tohost", logmodel.ToHost == null ? "" : logmodel.ToHost.ToString()),
                new OracleParameter("p_docno", logmodel.DocNo == null ? "" : logmodel.DocNo.ToString()),
                new OracleParameter("p_processflag", logmodel.ProcessFlag == null ? "" : logmodel.ProcessFlag.ToString()),
                new OracleParameter("p_processowner", logmodel.ProcessOwner == null ? "" : logmodel.ProcessOwner.ToString()));
                return Convert.ToInt32(outPara.Value);
            }
        }

        /// <summary>
        /// 獲得用戶所有公司代碼
        /// </summary>
        /// <param name="personcode"></param>
        /// <returns></returns>
        public DataTable GetAllCompany(string personcode)
        {
            //string str = "select a.personcode,a.companyid,b.companyname from gds_sc_person a,gds_sc_company b where b.deleted='N' and a.personcode='" + personcode + "'";
            string str = "select * from (select a.*,b.companyname from gds_sc_personcompany a,gds_sc_company b where a.companyid=b.companyid(+) AND a.personcode ='" + personcode + "' order by a.companyid)";
            return DalHelper.ExecuteQuery(str);
        }
    }
}
