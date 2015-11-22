/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： LoginInModel.cs
 * 檔功能描述： 用戶登陸實體類
 * 
 * 版本：1.0
 * 創建標識： 劉炎 2011.12.16
 * 
 */

using System;
using GDSBG.MiABU.Attendance.Common.Attributes;


namespace GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety
{
    /// <summary>
    ///  用戶登陸實體類
    /// </summary>
    [Serializable, TableName("GDS_SC_PERSON_LOGIN")]
    public class LoginInModel : ModelBase
    {
        private string personcode;
        private string password;
        private Nullable<DateTime> loginTime;

        /// <summary>
        /// 工號
        /// </summary>
        [Column("PERSONCODE", IsPrimaryKey = true)]
        public string Personcode
        {
            get { return personcode; }
            set { personcode = value; }
        }

        /// <summary>
        /// 密碼
        /// </summary>
        [Column("PASSWORD")]
        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        /// <summary>
        /// 登陸時間
        /// </summary>
        [Column("LOGINTIME")]
        public Nullable<DateTime> LoginTime
        {
            get { return loginTime; }
            set { loginTime = value; }
        }
    }
}
