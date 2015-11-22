/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： ActivityTempModel.cs
 * 檔功能描述： 免卡人員加班導入臨時表實體類
 * 
 * 版本：1.0
 * 創建標識： 張明強 2011.12.8
 * 
 */
using System;
using GDSBG.MiABU.Attendance.Common.Attributes;

namespace GDSBG.MiABU.Attendance.Model.KQM.OTM
{
    /// <summary>
    /// 免卡人員加班導入臨時表實體類
    /// </summary>
    [Serializable, TableName("GDS_ATT_ACTIVITY_TEMP")]
    public class ActivityTempModel: ModelBase
    {

        private string workNo;
        private string oTType;
        //private Nullable<DateTime> oTDate;
        //private Nullable<decimal> confirmHours;
        private string oTDate;
        private string confirmHours;
        private string workDesc;
        private Nullable<DateTime> createDate;
        private string createUser;
        private string localName;
        private string errorMsg;
        private string startTime;
        private string endTime;


        #region 工號
        /// <summary>
        /// 工號
        /// </summary>
        [Column("WORKNO")]
        public string WorkNo
        {
            get { return workNo; }
            set { workNo = value; }
        }
        #endregion

        #region 加班類型
        /// <summary>
        /// 加班類型
        /// </summary>
        [Column("OTTYPE")]
        public string OTType
        {
            get { return oTType; }
            set { oTType = value; }
        }
        #endregion

        //#region 加班日期
        ///// <summary>
        ///// 加班日期
        ///// </summary>
        //[Column("OTDATE")]
        //public Nullable<DateTime> OTDate
        //{
        //    get { return oTDate; }
        //    set { oTDate = value; }
        //}
        //#endregion

        //#region 確認時數
        ///// <summary>
        ///// 確認時數
        ///// </summary>
        //[Column("CONFIRMHOURS")]
        //public Nullable<decimal> ConfirmHours
        //{
        //    get { return confirmHours; }
        //    set { confirmHours = value; }
        //}
        //#endregion

        #region 加班日期
        /// <summary>
        /// 加班日期
        /// </summary>
        [Column("OTDATE")]
        public string OTDate
        {
            get { return oTDate; }
            set { oTDate = value; }
        }
        #endregion

        #region 確認時數
        /// <summary>
        /// 確認時數
        /// </summary>
        [Column("CONFIRMHOURS")]
        public string ConfirmHours
        {
            get { return confirmHours; }
            set { confirmHours = value; }
        }
        #endregion

        #region 加班內容描述
        /// <summary>
        /// 加班內容描述
        /// </summary>
        [Column("WORKDESC")]
        public string WorkDesc
        {
            get { return workDesc; }
            set { workDesc = value; }
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



        #region 姓名
        /// <summary>
        /// 姓名
        /// </summary>
        [Column("LOCALNAME", OnlySelect = true)]
        public string LocalName
        {
            get { return localName; }
            set { localName = value; }
        }
        #endregion


        #region 錯誤描述
        /// <summary>
        /// 錯誤描述
        /// </summary>
        [Column("ERRORMSG")]
        public string ErrorMsg
        {
            get { return errorMsg; }
            set { errorMsg = value; }
        }
        #endregion

        #region 開始時間
        /// <summary>
        /// 開始時間
        /// </summary>
        [Column("STARTTIME")]
        public string StartTime
        {
            get { return startTime; }
            set { startTime = value; }
        }
        #endregion

        #region 結束時間
        /// <summary>
        /// 結束時間
        /// </summary>
        [Column("ENDTIME")]
        public string EndTime
        {
            get { return endTime; }
            set { endTime = value; }
        }
        #endregion

    }
}
