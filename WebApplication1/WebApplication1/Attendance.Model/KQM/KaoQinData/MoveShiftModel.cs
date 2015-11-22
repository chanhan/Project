/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： MoveShiftModel.cs
 * 檔功能描述： 彈性調班實體類
 * 
 * 版本：1.0
 * 創建標識： 張明強 2011.12.8
 * 
 */
using System;
using GDSBG.MiABU.Attendance.Common.Attributes;

namespace GDSBG.MiABU.Attendance.Model.KQM.KaoQinData
{
    /// <summary>
    /// 免卡人員加班導入實體類
    /// </summary>
    [Serializable, TableName("GDS_ATT_MOVESHIFT", SelectTable = "gds_att_MoveShift_v")]
    public class MoveShiftModel:ModelBase
    {
        private string workNo;
        private Nullable<DateTime> workDate;
        private string workSTime;
        private string workETime;
        private Nullable<DateTime> noWorkDate;
        private string noWorkSTime;
        private string noWorkETime;
        private Nullable<decimal> timeQty;
        private string remark;
        private Nullable<DateTime> createDate;
        private string createUser;
        private Nullable<DateTime> updateDate;
        private string updateUser;
        private string localName;
        private string modifyName;
        private string depCode;
        private string depName;
        private string errorMsg;

        #region 工號
        /// <summary>
        /// 工號
        /// </summary>
        [Column("WORKNO",IsPrimaryKey=true)]
        public string WorkNo
        {
            get { return workNo; }
            set { workNo = value; }
        }
        #endregion

        #region 調班日期
        /// <summary>
        /// 調班日期
        /// </summary>
        [Column("WORKDATE",IsPrimaryKey=true)]
        public Nullable<DateTime> WorkDate
        {
            get { return workDate; }
            set { workDate = value; }
        }
        #endregion

        #region 調班開始時間
        /// <summary>
        /// 調班開始時間
        /// </summary>
        [Column("WORKSTIME")]
        public string WorkSTime
        {
            get { return workSTime; }
            set { workSTime = value; }
        }
        #endregion

        #region 調班結束時間
        /// <summary>
        /// 調班結束時間
        /// </summary>
        [Column("WORKETIME")]
        public string WorkETime
        {
            get { return workETime; }
            set { workETime = value; }
        }
        #endregion

        #region 上班日期
        /// <summary>
        /// 上班日期
        /// </summary>
        [Column("NOWORKDATE")]
        public Nullable<DateTime> NoWorkDate
        {
            get { return noWorkDate; }
            set { noWorkDate = value; }
        }
        #endregion

        #region 上班開始時間
        /// <summary>
        /// 上班開始時間
        /// </summary>
        [Column("NOWORKSTIME")]
        public string NoWorkSTime
        {
            get { return noWorkSTime; }
            set { noWorkSTime = value; }
        }
        #endregion

        #region 上班結束時間
        /// <summary>
        /// 上班結束時間
        /// </summary>
        [Column("NOWORKETIME")]
        public string NoWorkETime
        {
            get { return noWorkETime; }
            set { noWorkETime = value; }
        }
        #endregion

        #region 時數
        /// <summary>
        /// 時數
        /// </summary>
        [Column("TIMEQTY")]
        public Nullable<decimal> TimeQty
        {
            get { return timeQty; }
            set { timeQty = value; }
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

        #region 姓名
        /// <summary>
        /// 姓名
        /// </summary>
        [Column("localname",OnlySelect=true)]
        public string LocalName
        {
            get { return localName; }
            set { localName = value; }
        }
        #endregion

        #region 修改者姓名
        /// <summary>
        /// 修改者姓名
        /// </summary>
        [Column("modifyname", OnlySelect = true)]
        public string ModifyName
        {
            get { return modifyName; }
            set { modifyName = value; }
        }
        #endregion

        #region 部門
        /// <summary>
        /// 部門
        /// </summary>
        [Column("depcode", OnlySelect = true)]
        public string DepCode
        {
            get { return depCode; }
            set { depCode = value; }
        }
        #endregion

        #region 部門名稱
        /// <summary>
        /// 部門名稱
        /// </summary>
        [Column("depname", OnlySelect = true)]
        public string DepName
        {
            get { return depName; }
            set { depName = value; }
        }
        #endregion

    }
}
