/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： OTMAdvanceApplyModel.cs
 * 檔功能描述：加班實報查詢實體類
 * 
 * 版本：1.0
 * 創建標識： 張明強 2011.12.30
 * 
 */
using System;
using System.Collections.Generic;
using System.Text;
using GDSBG.MiABU.Attendance.Common.Attributes;

namespace GDSBG.MiABU.Attendance.Model.KQM.Query.OTM
{
    /// <summary>
    /// 加班實報查詢實體類
    /// </summary>
    [Serializable, TableName("gds_att_kaoqindata_exception_v")]
    public class OTMExceptionApplyQryModel : ModelBase
    {
        private string oTType;
        private string diffReason;
        private string diffReasonName;
        private string dName;
        private string dCode;
        private string workNo;
        private string localName;
        private string overTimeType;
        private string kQTime;

        private string onDutyTime;
        private string offDutyTime;
        private string beginTime;
        private string endTime;
        private string realHours;
        private string confirmHours;
        private string shiftNo;
        private string workDesc;

        private string remarks;
        private string oTMsgFlag;
        private string status;

        private Nullable<DateTime> oTDate;
        private string week;
        private string kQShift;
        private string iD;
        private string remark;
        private string buName;
        private string statusName;
        private string modifier;
        private Nullable<DateTime> modifyDate;
        private string approver;
        private Nullable<DateTime> approveDate;
        private Nullable<Decimal> advanceHours;
        private string realType;
        private string billNo;


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

        #region  DiffReason
        /// <summary>
        /// DiffReason
        /// </summary>
        [Column("diffreason")]
        public string DiffReason
        {
            get { return diffReason; }
            set { diffReason = value; }
        }

        #endregion

        #region   DiffReasonName
        /// <summary>
        /// DiffReasonName
        /// </summary>
        [Column("diffreasonname")]
        public string DiffReasonName
        {
            get { return diffReasonName; }
            set { diffReasonName = value; }
        }

        #endregion

        #region 部門
        /// <summary>
        /// 部門
        /// </summary>
        [Column("DName", OnlySelect = true)]
        public string DName
        {
            get { return dName; }
            set { dName = value; }
        }

        #endregion

        #region dCode
        /// <summary>
        /// dcode
        /// </summary>
        [Column("DCode", OnlySelect = true)]
        public string DCode
        {
            get { return dCode; }
            set { dCode = value; }
        }

        #endregion

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

        #region 姓名
        /// <summary>
        /// 姓名
        /// </summary>
        [Column("LocalName", OnlySelect = true)]
        public string LocalName
        {
            get { return localName; }
            set { localName = value; }
        }


        #endregion

        #region 加班類型
        /// <summary>
        /// 加班類型
        /// </summary>
        [Column("OverTimeType", OnlySelect = true)]
        public string OverTimeType
        {
            get { return overTimeType; }
            set { overTimeType = value; }
        }

        #endregion

        #region KQTime
        /// <summary>
        /// KQTime
        /// </summary>
        [Column("KQTime")]
        public string KQTime
        {
            get { return kQTime; }
            set { kQTime = value; }
        }
        #endregion

        #region 實際開始時間
        /// <summary>
        /// 實際開始時間
        /// </summary>
        [Column("OnDutyTime")]
        public string OnDutyTime
        {
            get { return onDutyTime; }
            set { onDutyTime = value; }
        }
        #endregion

        #region 實際結束時間
        /// <summary>
        /// 實際結束時間
        /// </summary>
        [Column("offDutyTime")]
        public string OffDutyTime
        {
            get { return offDutyTime; }
            set { offDutyTime = value; }
        }
        #endregion

        #region 預報開始時間
        /// <summary>
        /// 預報開始時間
        /// </summary>
        [Column("BeginTime")]
        public string BeginTime
        {
            get { return beginTime; }
            set { beginTime = value; }
        }
        #endregion

        #region 預報結束時間
        /// <summary>
        /// 預報結束時間
        /// </summary>
        [Column("EndTime")]
        public string EndTime
        {
            get { return endTime; }
            set { endTime = value; }
        }
        #endregion

        #region RealHours
        /// <summary>
        /// RealHours
        /// </summary>
        [Column("RealHours")]
        public string RealHours
        {
            get { return realHours; }
            set { realHours = value; }
        }

        #endregion

        #region 確認時數
        /// <summary>
        /// 確認時數
        /// </summary>
        [Column("confirmhours")]
        public string ConfirmHours
        {
            get { return confirmHours; }
            set { confirmHours = value; }
        }

        #endregion

        #region 加班內容
        /// <summary>
        /// 加班內容
        /// </summary>
        [Column("WorkDesc")]
        public string WorkDesc
        {
            get { return workDesc; }
            set { workDesc = value; }
        }

        #endregion

        #region 班別代碼
        /// <summary>
        ///  班別代碼
        /// </summary>
        [Column("ShiftNo")]
        public string ShiftNo
        {
            get { return shiftNo; }
            set { shiftNo = value; }
        }

        #endregion

        #region remarks
        /// <summary>
        /// remarks
        /// </summary>
        [Column("Remarks")]
        public string Remarks
        {
            get { return remarks; }
            set { remarks = value; }
        }

        #endregion

        #region OTMsgFlag
        /// <summary>
        /// OTMsgFlag
        /// </summary>
        [Column("OTMsgFlag")]
        public string OTMsgFlag
        {
            get { return oTMsgFlag; }
            set { oTMsgFlag = value; }
        }

        #endregion

        #region 簽核狀態
        /// <summary>
        /// 簽核狀態
        /// </summary>
        [Column("status", OnlySelect = true)]
        public string Status
        {
            get { return status; }
            set { status = value; }
        }

        #endregion

        #region 加班日期
        /// <summary>
        /// 加班日期
        /// </summary>
        [Column("OTDATE")]
        public Nullable<DateTime> OTDate
        {
            get { return oTDate; }
            set { oTDate = value; }
        }
        #endregion

        #region Week
        /// <summary>
        /// Week
        /// </summary>
        [Column("Week", OnlySelect = true)]
        public string Week
        {
            get { return week; }
            set { week = value; }
        }

        #endregion

        #region KQShift
        /// <summary>
        /// KQShift
        /// </summary>
        [Column("KQShift", OnlySelect = true)]
        public string KQShift
        {
            get { return kQShift; }
            set { kQShift = value; }
        }

        #endregion

        #region ID
        /// <summary>
        /// ID
        /// </summary>
        [Column("ID", IsPrimaryKey = true)]
        public string ID
        {
            get { return iD; }
            set { iD = value; }
        }
        #endregion

        #region remark
        /// <summary>
        /// remark
        /// </summary>
        [Column("Remark")]
        public string Remark
        {
            get { return remark; }
            set { remark = value; }
        }

        #endregion

        #region 事業處
        /// <summary>
        /// 事業處
        /// </summary>
        [Column("BuName", OnlySelect = true)]
        public string BuName
        {
            get { return buName; }
            set { buName = value; }
        }

        #endregion

        #region 簽核狀態
        /// <summary>
        /// 簽核狀態
        /// </summary>
        [Column("StatusName", OnlySelect = true)]
        public string StatusName
        {
            get { return statusName; }
            set { statusName = value; }
        }

        #endregion

        #region 維護人
        /// <summary>
        /// 維護人
        /// </summary>
        [Column("modifier")]
        public string Modifier
        {
            get { return modifier; }
            set { modifier = value; }
        }

        #endregion

        #region 維護日期
        /// <summary>
        /// 維護日期
        /// </summary>
        [Column("modifydate")]
        public Nullable<DateTime> ModifyDate
        {
            get { return modifyDate; }
            set { modifyDate = value; }
        }

        #endregion

        #region 簽核人
        /// <summary>
        /// 簽核人
        /// </summary>
        [Column("Approver")]
        public string Approver
        {
            get { return approver; }
            set { approver = value; }
        }

        #endregion

        #region 簽核日期
        /// <summary>
        /// 簽核日期
        /// </summary>
        [Column("ApproveDate")]
        public Nullable<DateTime> ApproveDate
        {
            get { return approveDate; }
            set { approveDate = value; }
        }

        #endregion

        #region 預報時數
        /// <summary>
        /// 預報時數
        /// </summary>
        [Column("advancehours")]
        public Nullable<Decimal> AdvanceHours
        {
            get { return advanceHours; }
            set { advanceHours = value; }
        }

        #endregion

        #region realtype
        /// <summary>
        /// realtype
        /// </summary>
        [Column("realtype")]
        public string RealType
        {
            get { return realType; }
            set { realType = value; }
        }

        #endregion

        #region 簽核單號
        /// <summary>
        /// 簽核單號
        /// </summary>
        [Column("BillNo")]
        public string BillNo
        {
            get { return billNo; }
            set { billNo = value; }
        }

        #endregion

    }
}
