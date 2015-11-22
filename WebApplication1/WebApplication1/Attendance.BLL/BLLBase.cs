/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： BLLBase.cs
 * 檔功能描述： 所有業務邏輯類基類
 * 
 * 版本：1.0
 * 創建標識： Lucky Lee 2011.09.19
 * 
 */

using System.Configuration;
using System.Web;
using GDSBG.MiABU.Attendance.Common;
using GDSBG.MiABU.Attendance.DALFactory;

namespace GDSBG.MiABU.Attendance.BLL
{
    /// <summary>
    /// 業務邏輯類基類
    /// </summary>
    /// <typeparam name="T">數據訪問層接口類型</typeparam>
    public abstract class BLLBase<T>
    {
        private T dal;
        private string m_DBType;

        /// <summary>
        /// 數據操作接口
        /// </summary>
        protected T DAL
        {
            get
            {

                HttpContext.Current.Session[GlobalData.ConnectInfoSessionKey] = ConfigurationManager.ConnectionStrings[GlobalData.CommonDbConfigKey].ConnectionString;
                HttpContext.Current.Session[GlobalData.DbTypeSessionKey] = ConfigurationManager.AppSettings[GlobalData.CommonDbTypeConfigKey];
                string m_dbConnectInfo = HttpContext.Current.Session[GlobalData.ConnectInfoSessionKey].ToString();
                string m_dbType = HttpContext.Current.Session[GlobalData.DbTypeSessionKey].ToString();

                if (dal == null || m_dbType!=m_DBType)
                {
                    dal = DAlFactory.CreateInstanse<T>(m_dbConnectInfo,m_dbType);
                    m_DBType = m_dbType;
                }
                else
                {
                    DAlFactory.SetConnecionString(dal, m_dbConnectInfo);
                }
                return dal;
            }
        }
    }
}
