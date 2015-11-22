/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： BellCardModel.cs
 * 檔功能描述： 卡鐘信息實體類
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
    /// 卡鐘信息實體類
    /// </summary>
    [Serializable, TableName("GDS_ATT_BELLCARD",SelectTable="gds_att_bellcard_v")]
    public class BellCardModel : ModelBase
    {
        
        private string bellNo;
        private string portIP;
        private string address;        
        private string manufacturer;        
        private string bellSize;        
        private string produceID;                
        private string pickDataIP;        
        private string pickComputeuser;        
        private string pickComputePW;       
        private string contactMan;        
        private string contactTel;
        private string userDept;
        private string userYM;        
        private string remark;                
        private string effectFlag;                
        private string bellType;
        private string bellTypeName;
        private Nullable<DateTime> createDate;
        private string createUser;
        private string updateUser;
        private Nullable<DateTime> updateDate;

        #region 卡鐘編號
        /// <summary>
        /// 卡鐘編號
        /// </summary>
        [Column("BellNo",IsPrimaryKey=true)]
        public string BellNo
        {
            get { return bellNo; }
            set { bellNo = value; }
        }
        #endregion

        #region 端口IP
        /// <summary>
        /// 端口IP
        /// </summary>
        [Column("PORTIP")]
        public string PortIP
        {
            get { return portIP; }
            set { portIP = value; }
        }
        #endregion

        #region 安裝位置
        /// <summary>
        /// 安裝位置
        /// </summary>
        [Column("ADDRESS")]
        public string Address
        {
            get { return address; }
            set { address = value; }
        }
        #endregion

        #region 廠商
        /// <summary>
        /// 廠商
        /// </summary>
        [Column("MANUFACTURER")]
        public string Manufacturer
        {
            get { return manufacturer; }
            set { manufacturer = value; }
        }
        #endregion

        #region 型號規格
        /// <summary>
        /// 型號規格
        /// </summary>
        [Column("BELLSIZE")]
        public string BellSize
        {
            get { return bellSize; }
            set { bellSize = value; }
        }
        #endregion

        #region 廠商ID
        /// <summary>
        /// 廠商ID
        /// </summary>
        [Column("PRODUCEID")]
        public string ProduceID
        {
            get { return produceID; }
            set { produceID = value; }
        }
        #endregion

        #region 採集電腦IP
        /// <summary>
        /// 採集電腦IP
        /// </summary>
        [Column("PICKDATAIP")]
        public string PickDataIP
        {
            get { return pickDataIP; }
            set { pickDataIP = value; }
        }
        #endregion

        #region 採集電腦用戶
        /// <summary>
        /// 採集電腦用戶
        /// </summary>
        [Column("PICKCOMPUTEUSER")]
        public string PickComputeUser
        {
            get { return pickComputeuser; }
            set { pickComputeuser = value; }
        }
        #endregion

        #region 採集電腦密碼
        /// <summary>
        /// 採集電腦密碼
        /// </summary>
        [Column("PICKCOMPUTEPW")]
        public string PickComputePW
        {
            get { return pickComputePW; }
            set { pickComputePW = value; }
        }
        #endregion

        #region 連絡人
        /// <summary>
        /// 連絡人
        /// </summary>
        [Column("CONTACTMAN")]
        public string ContactMan
        {
            get { return contactMan; }
            set { contactMan = value; }
        }
        #endregion

        #region 聯繫電話
        /// <summary>
        /// 聯繫電話
        /// </summary>
        [Column("CONTACTTEL")]
        public string ContactTel
        {
            get { return contactTel; }
            set { contactTel = value; }
        }
        #endregion

        #region 用戶單位
        /// <summary>
        /// 用戶單位
        /// </summary>
        [Column("USEDEPT")]
        public string UseDept
        {
            get { return userDept; }
            set { userDept = value; }
        }
        #endregion

        #region 啟用年月
        /// <summary>
        /// 啟用年月
        /// </summary>
        [Column("USERYM")]
        public string UserYM
        {
            get { return userYM; }
            set { userYM = value; }
        }
        #endregion

        #region 備註
        /// <summary>
        /// 備註
        /// </summary>
        [Column("REMARK")]
        public string Remark
        {
            get { return remark; }
            set { remark = value; }
        }
        #endregion

        #region 是否有效
        /// <summary>
        /// 是否有效
        /// </summary>
        [Column("EFFECTFLAG")]
        public string EffectFlag
        {
            get { return effectFlag; }
            set { effectFlag = value; }
        }
        #endregion

        #region 卡鐘類別
        /// <summary>
        /// 卡鐘類別
        /// </summary>
        [Column("BELLTYPE")]
        public string BellType
        {
            get { return bellType; }
            set { bellType = value; }
        }
        #endregion

        #region 卡鐘類別名稱
        [Column("BellTypeName")]
        public string BellTypeName
        {
            get { return bellTypeName; }
            set { bellTypeName = value; }
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

    }
}
