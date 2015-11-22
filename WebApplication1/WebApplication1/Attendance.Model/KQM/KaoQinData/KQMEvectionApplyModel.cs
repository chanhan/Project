/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： KQMEvectionApplyModel.cs
 * 檔功能描述： 外出申請實體類
 * 
 * 版本：1.0
 * 創建標識： 高子焱 2012.02.16
 * 
 */
using System;
using GDSBG.MiABU.Attendance.Common.Attributes;

namespace GDSBG.MiABU.Attendance.Model.KQM.KaoQinData
{
    /// <summary>
    /// 班別定義實體類
    /// </summary>
    [Serializable, TableName(" gds_att_applyout", SelectTable = "gds_att_applyout_v")]
    public class KQMEvectionApplyModel : ModelBase
    {
        private string id;
        private string workNo;
        private string localName;
        private string evectionType;
        private string evectionReason;
        private Nullable<DateTime> evectionTime;
        private string evectionTel;
        private string evectionAddress;
        private string evectionTask;
        private string evectionObject;
        private string evectionRoad;
        private Nullable<DateTime> returnTime;
        private string evectionBy;
        private string motorMan;
        private string remark;
        private string status;
        private string billNo;
        private string approver;
        private Nullable<DateTime> approveDate;
        private string auditer;
        private Nullable<DateTime> auditDate;
        private string auditIdea;
        private string createUser;
        private Nullable<DateTime> createDate;
        private string apRemark;
        private string dCode;
        private string depName;
        private string buName;
        private string statusName;
        private string evectionTypeName;
        private string comeYears;

        private string errorMsg;
        #region 編號
        /// <summary>
        /// 編號
        /// </summary>
        [Column("id", IsPrimaryKey = true)]
        public string ID
        {
            get { return id; }
            set { id = value; }
        }
        #endregion
        #region 工號
        /// <summary>
        /// 工號
        /// </summary>
        [Column("workNo")]
        public string WorkNo
        {
            get { return workNo; }
            set { workNo = value; }
        }
        #endregion
        #region 姓名
        /// <summary>
        /// 姓名
        /// </summary>
        [Column("localName")]
        public string LocalName
        {
            get { return localName; }
            set { localName = value; }
        }
        #endregion
        #region 外出範圍
        /// <summary>
        /// 外出範圍
        /// </summary>
        [Column("evectionType")]
        public string EvectionType
        {
            get { return evectionType; }
            set { evectionType = value; }
        }
        #endregion
        #region 外出原因
        /// <summary>
        /// 外出原因
        /// </summary>
        [Column("evectionReason")]
        public string EvectionReason
        {
            get { return evectionReason; }
            set { evectionReason = value; }
        }
        #endregion
        #region 洽公對象
        /// <summary>
        /// 洽公對象
        /// </summary>
        [Column("evectionObject")]
        public string EvectionObject
        {
            get { return evectionObject; }
            set { evectionObject = value; }
        }
        #endregion
        #region 外出時間
        /// <summary>
        /// 外出時間
        /// </summary>
        [Column("evectionTime")]
        public Nullable<DateTime> EvectionTime
        {
            get { return evectionTime; }
            set { evectionTime = value; }
        }
        #endregion
        #region 外出聯繫電話
        /// <summary>
        /// 外出聯繫電話
        /// </summary>
        [Column("evectionTel")]
        public string EvectionTel
        {
            get { return evectionTel; }
            set { evectionTel = value; }
        }
        #endregion
        #region 洽公單位
        /// <summary>
        /// 洽公單位
        /// </summary>
        [Column("evectionAddress")]
        public string EvectionAddress
        {
            get { return evectionAddress; }
            set { evectionAddress = value; }
        }
        #endregion
        #region 洽公事由
        /// <summary>
        /// 洽公事由
        /// </summary>
        [Column("evectionTask")]
        public string EvectionTask
        {
            get { return evectionTask; }
            set { evectionTask = value; }
        }
        #endregion
        #region 洽公路線
        /// <summary>
        /// 洽公路線
        /// </summary>
        [Column("evectionroad")]
        public string EvectionRoad
        {
            get { return evectionRoad; }
            set { evectionRoad = value; }
        }
        #endregion
        #region 預計返回時間
        /// <summary>
        /// 預計返回時間
        /// </summary>
        [Column("returnTime")]
        public Nullable<DateTime> ReturnTime
        {
            get { return returnTime; }
            set { returnTime = value; }
        }
        #endregion
        #region 外出方式
        /// <summary>
        /// 外出方式
        /// </summary>
        [Column("evectionby")]
        public string EvectionBy
        {
            get { return evectionBy; }
            set { evectionBy = value; }
        }
        #endregion
        #region 司機
        /// <summary>
        /// 司機
        /// </summary>
        [Column("motorMan")]
        public string MotorMan
        {
            get { return motorMan; }
            set { motorMan = value; }
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
        #region 簽核狀態
        /// <summary>
        ///簽核狀態
        /// </summary>
        [Column("status")]
        public string Status
        {
            get { return status; }
            set { status = value; }
        }
        #endregion
        #region 簽核單號
        /// <summary>
        /// 簽核單號
        /// </summary>
        [Column("billNo")]
        public string BillNo
        {
            get { return billNo; }
            set { billNo = value; }
        }
        #endregion
        #region 申請人
        /// <summary>
        /// 申請人
        /// </summary>
        [Column("approver")]
        public string Approver
        {
            get { return approver; }
            set { approver = value; }
        }
        #endregion
        #region 申請日期
        /// <summary>
        /// 申請日期
        /// </summary>
        [Column("approveDate")]
        public Nullable<DateTime> ApproveDate
        {
            get { return approveDate; }
            set { approveDate = value; }
        }
        #endregion
        #region 當前簽核人的上一步簽核人
        /// <summary>
        /// 當前簽核人的上一步簽核人
        /// </summary>
        [Column("auditer")]
        public string Auditer
        {
            get { return auditer; }
            set { auditer = value; }
        }
        #endregion
        #region 簽核日期
        /// <summary>
        /// 簽核日期
        /// </summary>
        [Column("auditDate")]
        public Nullable<DateTime> AuditDate
        {
            get { return auditDate; }
            set { auditDate = value; }
        }
        #endregion
        #region 簽核意見
        /// <summary>
        /// 簽核意見
        /// </summary>
        [Column("auditIdea")]
        public string AuditIdea
        {
            get { return auditIdea; }
            set { auditIdea = value; }
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
        #region 暫不知道
        /// <summary>
        /// 暫不知道
        /// </summary>
        [Column("apRemark")]
        public string ApRemark
        {
            get { return apRemark; }
            set { apRemark = value; }
        }
        #endregion

        #region 部門代碼
        /// <summary>
        /// 部門代碼
        /// </summary>
        [Column("dcode", OnlySelect = true)]
        public string DCode
        {
            get { return dCode; }
            set { dCode = value; }
        }
        #endregion
        #region 部門名稱
        /// <summary>
        /// 部門名稱
        /// </summary>
        [Column("depName", OnlySelect = true)]
        public string DepName
        {
            get { return depName; }
            set { depName = value; }
        }
        #endregion

        #region 事業處名稱
        /// <summary>
        /// 事業處名稱
        /// </summary>
        [Column("buName", OnlySelect = true)]
        public string BUName
        {
            get { return buName; }
            set { buName = value; }
        }
        #endregion
        #region 簽核狀態名稱
        /// <summary>
        /// 簽核狀態名稱
        /// </summary>
        [Column("statusName", OnlySelect = true)]
        public string StatusName
        {
            get { return statusName; }
            set { statusName = value; }
        }
        #endregion
        #region 外出範圍名稱
        /// <summary>
        /// 外出範圍名稱
        /// </summary>
        [Column("evectiontypeName", OnlySelect = true)]
        public string EvectionTypeName
        {
            get { return evectionTypeName; }
            set { evectionTypeName = value; }
        }
        #endregion
        #region 在職時間
        /// <summary>
        /// 在職時間
        /// </summary>
        [Column("comeYears", OnlySelect = true)]
        public string ComeYears
        {
            get { return comeYears; }
            set { comeYears = value; }
        }
        #endregion

        #region 錯誤原因
        /// <summary>
        /// 錯誤原因
        /// </summary>
        [Column("ErrorMsg", OnlySelect = true)]
        public string ErrorMsg
        {
            get { return errorMsg; }
            set { errorMsg = value; }
        }
        #endregion

    }
}
