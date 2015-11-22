/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： WorkShiftModel.cs
 * 檔功能描述： 班別定義實體類
 * 
 * 版本：1.0
 * 創建標識： 高子焱 2011.12.06
 * 
 */
using System;
using GDSBG.MiABU.Attendance.Common.Attributes;


namespace GDSBG.MiABU.Attendance.Model.KQM.BasicData
{
    /// <summary>
    /// 班別定義實體類
    /// </summary>
    [Serializable, TableName(" gds_att_workshift", SelectTable = "gds_att_workshift_v")]
    public class WorkShiftModel : ModelBase
    {
        private string shiftNo;
        private string shiftNoType;
        private string shiftDesc;
        private Nullable<decimal> timeQty;
        private string onDutyTime;
        private string offDutyTime;
        private string amRestSTime;
        private string amRestETime;
        private string onDutyTime1;
        private string offDutyTime1;
        private string pmRestSTime;
        private string pmRestETime;
        private Nullable<decimal> pmRestQty;
        private string otOnDutyTime;
        private string otOffDutyTime;
        private string orgCode;
        private Nullable<DateTime> effectDate;
        private Nullable<DateTime> expireDate;
        private string reMark;
        private string createUser;
        private Nullable<DateTime> createDate;
        private Nullable<DateTime> updateDate;
        private string updateUser;
        private string shiftType;
        private string isLactation;
        private string shareDepcode;
        private string shiftDetail;
        private string shiftTypeName;
        private string orgName;
        private string shareOrgName;

        #region 班別編碼
        /// <summary>
        /// 班別編碼
        /// </summary>
        [Column("shiftno", IsPrimaryKey = true)]
        public string ShiftNo
        {
            get { return shiftNo; }
            set { shiftNo = value; }
        }
        #endregion
        #region 班別類型代碼
        /// <summary>
        /// 班別類型代碼
        /// </summary>
        [Column("ShiftNoType", OnlySelect=true)]
        public string ShiftNoType
        {
            get { return shiftNoType; }
            set { shiftNoType = value; }
        }
        #endregion

        #region 班別描述
        /// <summary>
        /// 班別描述
        /// </summary>
        [Column("shiftdesc")]
        public string ShiftDesc
        {
            get { return shiftDesc; }
            set { shiftDesc = value; }
        }
        #endregion
        #region 標準時數
        /// <summary>
        /// 標準時數
        /// </summary>
        [Column("timeqty")]
        public Nullable<decimal> TimeQty
        {
            get { return timeQty; }
            set { timeQty = value; }
        }
        #endregion
        #region 工作時段開始時間
        /// <summary>
        /// 工作時段開始時間
        /// </summary>
        [Column("ondutytime")]
        public string OnDutyTime
        {
            get { return onDutyTime; }
            set { onDutyTime = value; }
        }
        #endregion
        #region 工作時段結束時間
        /// <summary>
        /// 工作時段結束時間
        /// </summary>
        [Column("offdutytime")]
        public string OffDutyTime
        {
            get { return offDutyTime; }
            set { offDutyTime = value; }
        }
        #endregion
        #region 中間休息開始時間
        /// <summary>
        /// 中間休息開始時間
        /// </summary>
        [Column("amreststime")]
        public string AMRestSTime
        {
            get { return amRestSTime; }
            set { amRestSTime = value; }
        }
        #endregion

        #region 中間休息結束時間
        /// <summary>
        /// 中間休息結束時間
        /// </summary>
        [Column("amrestetime")]
        public string AMRestETime
        {
            get { return amRestETime; }
            set { amRestETime = value; }
        }
        #endregion
        #region 加班前休息開始時間?
        /// <summary>
        /// 加班前休息開始時間?
        /// </summary>
        [Column("ondutytime1")]
        public string OnDutyTime1
        {
            get { return onDutyTime1; }
            set { onDutyTime1 = value; }
        }
        #endregion
        #region 加班前休息結束時間?
        /// <summary>
        /// 加班前休息結束時間?
        /// </summary>
        [Column("offdutytime1")]
        public string OffDutyTime1
        {
            get { return offDutyTime1; }
            set { offDutyTime1 = value; }
        }
        #endregion
        #region 加班前休息開始時間
        /// <summary>
        /// 加班前休息開始時間
        /// </summary>
        [Column("pmreststime")]
        public string PMRestSTime
        {
            get { return pmRestSTime; }
            set { pmRestSTime = value; }
        }
        #endregion
        #region 加班前休息結束時間
        /// <summary>
        /// 加班前休息結束時間
        /// </summary>
        [Column("pmrestetime")]
        public string PMRestETime
        {
            get { return pmRestETime; }
            set { pmRestETime = value; }
        }
        #endregion
        #region 加班前休息時長
        /// <summary>
        /// 加班前休息時長
        /// </summary>
        [Column("pmrestqty")]
        public Nullable<decimal> PMRestQty
        {
            get { return pmRestQty; }
            set { pmRestQty = value; }
        }
        #endregion

        #region otOnDutyTime
        /// <summary>
        /// otOnDutyTime
        /// </summary>
        [Column("otondutytime")]
        public string OtOnDutyTime
        {
            get { return otOnDutyTime; }
            set { otOnDutyTime = value; }
        }
        #endregion
        #region otOffDutyTime
        /// <summary>
        ///otOffDutyTime
        /// </summary>
        [Column("otoffdutytime")]
        public string OtOffDutyTime
        {
            get { return otOffDutyTime; }
            set { otOffDutyTime = value; }
        }
        #endregion
        #region 單位代碼
        /// <summary>
        /// 單位代碼
        /// </summary>
        [Column("orgcode")]
        public string OrgCode
        {
            get { return orgCode; }
            set { orgCode = value; }
        }
        #endregion
        #region 生效日期
        /// <summary>
        /// 生效日期
        /// </summary>
        [Column("effectdate")]
        public Nullable<DateTime> EffectDate
        {
            get { return effectDate; }
            set { effectDate = value; }
        }
        #endregion
        #region 失效日期
        /// <summary>
        /// 失效日期
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
            get { return reMark; }
            set { reMark = value; }
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
        #region 創建日期
        /// <summary>
        ///創建日期
        /// </summary>
        [Column("create_date")]
        public Nullable<DateTime> CreateDate
        {
            get { return createDate; }
            set { createDate = value; }
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
        #region 是否直落
        /// <summary>
        /// 是否直落
        /// </summary>
        [Column("shifttype")]
        public string ShiftType
        {
            get { return shiftType; }
            set { shiftType = value; }
        }
        #endregion
        #region 是否哺乳期
        /// <summary>
        /// 是否哺乳期
        /// </summary>
        [Column("islactation")]
        public string IsLactation
        {
            get { return isLactation; }
            set { isLactation = value; }
        }
        #endregion
        #region 共用單位
        /// <summary>
        /// 共用單位
        /// </summary>
        [Column("shareorgcode")]
        public string ShareDepcode
        {
            get { return shareDepcode; }
            set { shareDepcode = value; }
        }
        #endregion

        #region 班別細節
        /// <summary>
        /// 班別細節
        /// </summary>
        [Column("shiftdetail", OnlySelect = true)]
        public string ShiftDetail
        {
            get { return shiftDetail; }
            set { shiftDetail = value; }
        }
        #endregion
        #region 班別名稱
        /// <summary>
        /// 班別細節
        /// </summary>
        [Column("shifttypename", OnlySelect = true)]
        public string ShiftTypeName
        {
            get { return shiftTypeName; }
            set { shiftTypeName = value; }
        }
        #endregion
        #region 單位名稱
        /// <summary>
        /// 班別細節
        /// </summary>
        [Column("orgname", OnlySelect = true)]
        public string OrgName
        {
            get { return orgName; }
            set { orgName = value; }
        }
        #endregion
        #region 共用單位名稱
        /// <summary>
        /// 共用單位名稱
        /// </summary>
        [Column("shareorgname", OnlySelect = true)]
        public string ShareOrgName
        {
            get { return shareOrgName; }
            set { shareOrgName = value; }
        }
        #endregion
    }
}
