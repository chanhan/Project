/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： UserModel.cs
 * 檔功能描述： 用戶實體類
 * 
 * 版本：1.0
 * 創建標識： Lucky Lee 2011.09.19
 * 
 */

using System;
using GDSBG.MiABU.Attendance.Common.Attributes;

namespace GDSBG.MiABU.Attendance.Model.SystemManage
{
    /// <summary>
    /// 用戶實體類
    /// </summary>
    [Serializable, TableName("gds_sc_person")]
    public class UserModel : ModelBase
    {
        private string password;
        private string empNo;
        private string phone;
        private string tel;
        private string mail;
        private string languageType;
        private string activeFlag;

        /// <summary>
        /// 密碼
        /// </summary>
        [Column("PASSWD")]
        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        /// <summary>
        /// 工號
        /// </summary>
        [Column("personcode", IsPrimaryKey = true)]
        public string EmpNo
        {
            get { return empNo; }
            set { empNo = value; }
        }

        /// <summary>
        /// 辦公電話
        /// </summary>
        [Column("tel")]
        public string Tel
        {
            get { return tel; }
            set { tel = value; }
        }

        /// <summary>
        /// 手機
        /// </summary>
        [Column("mobile")]
        public string Phone
        {
            get { return phone; }
            set { phone = value; }
        }

        /// <summary>
        /// 郵件
        /// </summary>
        [Column("MAIL")]
        public string Mail
        {
            get { return mail; }
            set { mail = value; }
        }

        /// <summary>
        /// 語言類型
        /// </summary>
        [Column("LANGUAGE")]
        public string LanguageType
        {
            get { return languageType; }
            set { languageType = value; }
        }


        /// <summary>
        /// 是否有效
        /// </summary>
        [Column("DELETED")]
        public string ActiveFlag
        {
            get { return activeFlag; }
            set { activeFlag = value; }
        }
    }
}
