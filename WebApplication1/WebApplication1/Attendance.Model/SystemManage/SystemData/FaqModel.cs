/*
 * Copyright (C) 2011 GDSBG MiABU 版權所有。
 * 
 * 檔案名： FaqTypeModel.cs
 * 檔功能描述： FAQ類型實體類
 * 
 * 版本：1.0
 * 創建標識： 陳函 2012.01.07
 * 
 */
using System;
using System.Collections.Generic;
using System.Text;
using GDSBG.MiABU.Attendance.Common.Attributes;
using GDSBG.MiABU.Attendance.Model;
namespace GDSBG.MiABU.ESS.Model.SystemManage.Interaction
{
    /// <summary>
    /// FAQ類型實體類
    /// </summary>
    [Serializable, TableName("gds_att_info_faqs", SelectTable = "gds_att_info_faqs_v")]
    public class FaqModel : ModelBase
    {
        private Nullable<int> faqSeq;
        private string empNo;
        private string empName;
        private string empPhone;
        private string empEmail;
        private string faqTitle;
        private Nullable<DateTime> faqDate;
        private string faqContent;
        private string answerName;
        private string answerEmail;
        private Nullable<DateTime> answerDate;
        private string answerContent;
        private string answerFlag;
        private string isFamiliar;
        private Nullable<int> faqTypeId;
        private string faqTypeName;
        private string createUser;
        private Nullable<DateTime> createDate;
        private string updateUser;
        private Nullable<DateTime> updateDate;

        /// <summary>
        /// 序號
        /// </summary>
        [Column("FAQ_SEQ",IsPrimaryKey=true)]
        public Nullable<int> FaqSeq
        {
            get { return faqSeq; }
            set { faqSeq = value; }
        }

        /// <summary>
        /// 提問人工號
        /// </summary>
        [Column("EMP_NO")]
        public string EmpNo
        {
            get { return empNo; }
            set { empNo = value; }
        }

        /// <summary>
        /// 提問人姓名
        /// </summary>
        [Column("EMP_NAME")]
        public string EmpName
        {
            get { return empName; }
            set { empName = value; }
        }

        /// <summary>
        /// 提問人電話
        /// </summary>
        [Column("EMP_PHONE")]
        public string EmpPhone
        {
            get { return empPhone; }
            set { empPhone = value; }
        }

        /// <summary>
        /// 提問人mail
        /// </summary>
        [Column("EMP_EMAIL")]
        public string EmpEmail
        {
            get { return empEmail; }
            set { empEmail = value; }
        }

        /// <summary>
        /// 提問標題
        /// </summary>
        [Column("FAQ_TITLE")]
        public string FaqTitle
        {
            get { return faqTitle; }
            set { faqTitle = value; }
        }

        /// <summary>
        /// 提問日期
        /// </summary>
        [Column("FAQ_DATE")]
        public Nullable<DateTime> FaqDate
        {
            get { return faqDate; }
            set { faqDate = value; }
        }

        /// <summary>
        /// 提問內容
        /// </summary>
        [Column("FAQ_CONTENT")]
        public string FaqContent
        {
            get { return faqContent; }
            set { faqContent = value; }
        }

        /// <summary>
        /// 回覆人
        /// </summary>
        [Column("ANSWER_NAME")]
        public string AnswerName
        {
            get { return answerName; }
            set { answerName = value; }
        }

        /// <summary>
        /// 回覆人mail
        /// </summary>
        [Column("ANSWER_EMAIL")]
        public string AnswerEmail
        {
            get { return answerEmail; }
            set { answerEmail = value; }
        }

        /// <summary>
        /// 回覆日期
        /// </summary>
        [Column("ANSWER_DATE")]
        public Nullable<DateTime> AnswerDate
        {
            get { return answerDate; }
            set { answerDate = value; }
        }

        /// <summary>
        /// 回覆內容
        /// </summary>
        [Column("ANSWER_CONTENT")]
        public string AnswerContent
        {
            get { return answerContent; }
            set { answerContent = value; }
        }

        /// <summary>
        /// 是否已經回覆標識
        /// </summary>
        [Column("ANSWER_FLAG")]
        public string AnswerFlag
        {
            get { return answerFlag; }
            set { answerFlag = value; }
        }

        /// <summary>
        /// 是否為常見問題標識
        /// </summary>
        [Column("IS_FAMILIAR")]
        public string IsFamiliar
        {
            get { return isFamiliar; }
            set { isFamiliar = value; }
        }

        /// <summary>
        /// 問題類別Id
        /// </summary>
        [Column("FAQ_TYPE_ID")]
        public Nullable<int> FaqTypeId
        {
            get { return faqTypeId; }
            set { faqTypeId = value; }
        }

        /// <summary>
        /// 問題類別名稱
        /// </summary>
        [Column("FAQ_TYPE_NAME",OnlySelect=true)]
        public string FaqTypeName
        {
            get { return faqTypeName; }
            set { faqTypeName = value; }
        }

        /// <summary>
        /// 創建用戶
        /// </summary>
        [Column("CREATE_USER")]
        public string CreateUser
        {
            get { return createUser; }
            set { createUser = value; }
        }

        /// <summary>
        /// 創建日期
        /// </summary>
        [Column("CREATE_DATE")]
        public Nullable<DateTime> CreateDate
        {
            get { return createDate; }
            set { createDate = value; }
        }

        /// <summary>
        /// 更新用戶
        /// </summary>
        [Column("UPDATE_USER")]
        public string UpdateUser
        {
            get { return updateUser; }
            set { updateUser = value; }
        }

        /// <summary>
        /// 更新日期
        /// </summary>
        [Column("UPDATE_DATE")]
        public Nullable<DateTime> UpdateDate
        {
            get { return updateDate; }
            set { updateDate = value; }
        }
    }
}
