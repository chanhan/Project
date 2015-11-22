/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： GlobalData.cs
 * 檔功能描述： 全局數據及信息
 * 
 * 版本：1.0
 * 創建標識： Lucky Lee 2011.09.21
 * 
 */

namespace GDSBG.MiABU.Attendance.Common
{
    /// <summary>
    /// 全局數據及信息
    /// </summary>
    public class GlobalData
    {
        /// <summary>
        /// 保存數據庫連接信息之Session Key
        /// </summary>
        public const string ConnectInfoSessionKey = "__DB_CONNECTION_INFO_STRING";
        /// <summary>
        /// 數據庫類型信息之Session Key
        /// </summary>
        public const string DbTypeSessionKey = "__DB_TYPE_INFO_KEY";
        /// <summary>
        /// 當前用戶信息Model之Session Key
        /// </summary>
        public const string UserInfoSessionKey = "__CURRENT_USER_INFO";
        /// <summary>
        /// 當前系統信息Model之Session Key
        /// </summary>
        public const string SystemInfoSessionKey = "__CURRENT_SYSTEM_INFO";
        /// <summary>
        /// 當前登錄用戶的功能清單之Session Key
        /// </summary>
        public const string UserFunctionsSessionKey = "__CURRENT_USER_FUNCTIONS";
        /// <summary>
        /// 驗證碼存儲之Session Key
        /// </summary>
        public const string ValidateCodeSessionKey = "__ESS_VALIDATECODE";
        /// <summary>
        /// 公共連接字串之配置文件中connectionStrings Key
        /// </summary>
        public const string CommonDbConfigKey = "CommonConnectionString";
        /// <summary>
        /// 公共連接數據庫類型之配置文件中appSettings Key
        /// </summary>
        public const string CommonDbTypeConfigKey = "CommonDataBaseType";
        /// <summary>
        /// 用戶登陸帳號
        /// </summary>
        public const string UserLoginId = "__CURRENT_USER_LOGINID";
    }
}