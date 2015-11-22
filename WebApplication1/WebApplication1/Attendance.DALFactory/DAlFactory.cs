/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： DAlFactory.cs
 * 檔功能描述： 數據工廠類
 * 
 * 版本：1.0
 * 創建標識： Lucky Lee 2011.09.19
 * 
 */

using System;
using System.Configuration;
using System.Reflection;
using System.Web;
using GDSBG.MiABU.Attendance.Common;
using GDSBG.MiABU.Attendance.Common.Attributes;

namespace GDSBG.MiABU.Attendance.DALFactory
{
    /// <summary>
    /// 數據工廠類
    /// </summary>
    public sealed class DAlFactory
    {
        /// <summary>
        /// 根據連接字串及數據庫類型創建數據訪問層接口
        /// </summary>
        /// <typeparam name="T">要創建的數據訪問層接口類型</typeparam>
        /// <param name="dbConnectString">加密的連接字串</param>
        /// <param name="dbType">數據庫類型，據此獲得配置文件中數據訪問程式集信息</param>
        /// <returns>與數據操作接口對應的數據操作實體</returns>
        public static T CreateInstanse<T>(string dbConnectString, string dbType)
        {
            string assemblyInfo = EncryptClass.Instance.Decrypt(ConfigurationManager.AppSettings[dbType]);
            string[] assemblyInfos = assemblyInfo.Split('|');
            Type type = typeof(T);
            RefClassAttribute refClass = (RefClassAttribute)type.GetCustomAttributes(typeof(RefClassAttribute), false)[0];

            object obj = CreateObject(assemblyInfo, refClass.RefClassName);
            SetConnecionString(obj,dbConnectString);

            return (T)obj;
        }

        /// <summary>
        /// 設置數據訪問層接口的連接字串
        /// </summary>
        /// <typeparam name="T">數據訪問層接口類型</typeparam>
        /// <param name="instance">接口實例</param>
        /// <param name="connectionString">加密的連接字串</param>
        public static void SetConnecionString<T>(T instance, string connectionString)
        {
            instance.GetType().GetProperty("ConnectionString").SetValue(instance, EncryptClass.Instance.Decrypt(connectionString), null);
        }

        /// <summary>
        /// 創建數據訪問對象
        /// </summary>
        /// <param name="assemblyInfo">程式集信息</param>
        /// <param name="className">類名</param>
        /// <returns>返回創建的對象</returns>
        private static object CreateObject(string assemblyInfo, string className)
        {
            string[] assemblyInfos = assemblyInfo.Split('|');
            object objAssembly = HttpRuntime.Cache[assemblyInfo];
            if (objAssembly == null)
            {
                objAssembly = Assembly.Load(assemblyInfos[0]);
                HttpRuntime.Cache.Insert(assemblyInfo, objAssembly);
            }
            return ((Assembly)objAssembly).CreateInstance(assemblyInfos[1] + "." + className);
        }
    }
}