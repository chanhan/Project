/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： KqmRulesModel.cs
 * 檔功能描述： 缺勤規則實體類
 * 
 * 版本：1.0
 * 創建標識： 張明強 2011.12.8
 * 
 */



using System;
using GDSBG.MiABU.Attendance.Common.Attributes;

namespace GDSBG.MiABU.Attendance.Model.KQM.BasicData
{
    /// <summary>
    /// 缺勤規則實體類
    /// </summary>
    [Serializable, TableName("GDS_ATT_RULES", SelectTable = "GDS_ATT_RULES_V")]
    public class KqmRulesModel : ModelBase
    {
        private string id;
        private string absentType;
        private string absentTypeDesc;
        private Nullable<int> threshold0;
        private Nullable<int> threshold1;
        private string punishType;
        private Nullable<int> pNumber;
        private string emolument;
        private string formula;
        private Nullable<DateTime> expireDate;
        private Nullable<DateTime> effectDate;
        private string remark;
        private Nullable<DateTime> createDate;
        private string createUser;
        private Nullable<DateTime> updateDate;
        private string updateUser;
        private string punishTypeValue;
        private string absentTypeValue;


        #region ID
        /// <summary>
        /// ID
        /// </summary>
        [Column("ID", IsPrimaryKey = true)]
        public string ID
        {
            get { return id; }
            set { id = value; }
        }
        #endregion

        #region 缺勤類別
        /// <summary>
        /// 缺勤類別
        /// </summary>
        [Column("absenttype")]
        public string AbsentType
        {
            get { return absentType; }
            set { absentType = value; }
        }
        #endregion


        #region 缺勤時數描述
        /// <summary>
        /// 缺勤時數描述
        /// </summary>
        [Column("absenttypedesc")]
        public string AbsentTypeDesc
        {
            get { return absentTypeDesc; }
            set { absentTypeDesc = value; }
        }

        #endregion


        #region 上限時數（以分鐘為單位）
        /// <summary>
        /// 上限時數（以分鐘為單位）
        /// </summary>
        [Column("threshold0")]
        public Nullable<int> Threshold0
        {
            get { return threshold0; }
            set { threshold0 = value; }
        }

        #endregion


        #region 下限時數（以分鐘為單位）
        /// <summary>
        /// 下限時數（以分鐘為單位）
        /// </summary>
        [Column("threshold1")]
        public Nullable<int> Threshold1
        {
            get { return threshold1; }
            set { threshold1 = value; }
        }

        #endregion


        #region 懲罰類型
        /// <summary>
        /// 懲罰類型
        /// </summary>
        [Column("punishtype")]
        public string PunishType
        {
            get { return punishType; }
            set { punishType = value; }
        }

        #endregion


        #region 懲罰次數
        /// <summary>
        /// 懲罰次數
        /// </summary>
        [Column("pnumber")]
        public Nullable<int> PNumber
        {
            get { return pNumber; }
            set { pNumber = value; }
        }

        #endregion


        #region 薪資處理描述
        /// <summary>
        /// 薪資處理描述
        /// </summary>
        [Column("emolument")]
        public string Emolument
        {
            get { return emolument; }
            set { emolument = value; }
        }

        #endregion


        #region 扣薪公式
        /// <summary>
        /// 扣薪公式
        /// </summary>
        [Column("formula")]
        public string Formula
        {
            get { return formula; }
            set { formula = value; }
        }

        #endregion


        #region 生效日
        /// <summary>
        /// 生效日
        /// </summary>
        [Column("effectdate")]
        public Nullable<DateTime> EffectDate
        {
            get { return effectDate; }
            set { effectDate = value; }
        }

        #endregion


        #region 失效日
        /// <summary>
        /// 失效日
        /// </summary>
        [Column("expiredate")]
        public Nullable<DateTime> ExpireDate
        {
            get { return expireDate; }
            set { expireDate = value; }
        }

        #endregion


        #region 備註
        /// <summary>
        /// 備註
        /// </summary>
        [Column("remark")]
        public string Remark
        {
            get { return remark; }
            set { remark = value; }
        }
        #endregion

        #region 創建日期
        /// <summary>
        /// 創建日期
        /// </summary>
        [Column("create_date")]
        public Nullable<DateTime> CreateDate
        {
            get { return createDate; }
            set { createDate = value; }
        }
        #endregion

        #region 創建用戶
        /// <summary>
        /// 創建用戶
        /// </summary>
        [Column("create_user")]
        public string CreateUser
        {
            get { return createUser; }
            set { createUser = value; }
        }
        #endregion

        #region 更新日期
        /// <summary>
        /// 更新日期
        /// </summary>
        [Column("update_date")]
        public Nullable<DateTime> UpdateDate
        {
            get { return updateDate; }
            set { updateDate = value; }
        }
        #endregion

        #region 更新用戶
        /// <summary>
        /// 更新用戶
        /// </summary>
        [Column("update_user")]
        public string UpdateUser
        {
            get { return updateUser; }
            set { updateUser = value; }
        }
        #endregion

        #region 缺勤類別描述
        /// <summary>
        /// 缺勤類別描述
        /// </summary>
        [Column("absenttypevalue", OnlySelect = true)]
        public string AbsentTypeValue
        {
            get { return absentTypeValue; }
            set { absentTypeValue = value; }
        }
        #endregion


        #region 懲罰類型描述
        /// <summary>
        /// 懲罰類型描述
        /// </summary>
        [Column("punishtypevalue", OnlySelect = true)]
        public string PunishTypeValue
        {
            get { return punishTypeValue; }
            set { punishTypeValue = value; }
        }

        #endregion
    }
}
