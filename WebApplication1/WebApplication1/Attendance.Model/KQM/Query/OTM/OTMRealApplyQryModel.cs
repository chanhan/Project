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
    [Serializable, TableName("gds_att_realapply", SelectTable = "gds_att_otmrealapply_v")]
    public class OTMRealApplyQryModel : ModelBase
    {
        private string iD;
        private string workNo;
        private string oTType;
        private Nullable<DateTime> oTDate;
        private string advanceID;
        private Nullable<DateTime> beginTime;
        private Nullable<DateTime> endTime;
        private Nullable<Decimal> advanceHours;
        private string workDesc;
        private string diffReason;
        private string remark;
        private string approver;
        private Nullable<DateTime> approveDate;
        private string apRemark;
        private string realType;
        private string shiftNo;
        private Nullable<DateTime> onDutyTime;
        private Nullable<DateTime> offDutyTime;
        private Nullable<Decimal> realHours;
        private Nullable<Decimal> confirmHours;
        private string confirmRemark;
        private string remarks;
        private string oTMsgFlag;
        private string status;
        private Nullable<DateTime> applyDate;
        private string updateUser;
        private Nullable<DateTime> updateDate;
        private string billNo;
        private string isPay;
        private string g2IsForrest;
        private string week;
        private string enWeek;
        private Nullable<Decimal> g1Total;
        private Nullable<Decimal> g2Total;
        private Nullable<Decimal> g3Total;
        private string advanceTime;
        private string overTimeSpan;

        
        private string localName;
        private string overTimeType;
        private string dcode;
        private string managerName;

        
        private string statusName;
        private string depName;
        private string buName;
        private string personType;
        private string approverName;
        private string isProject;
        private string shiftDesc;

        #region OverTimeSpan
        /// <summary>
        /// OverTimeSpan
        /// </summary>
        [Column("OverTimeSpan")]
        public string OverTimeSpan
        {
            get { return overTimeSpan; }
            set { overTimeSpan = value; }
        }
        #endregion

        #region ManagerName
        /// <summary>
        /// ManagerName
        /// </summary>
        [Column("ManagerName")]
        public string ManagerName
        {
            get { return managerName; }
            set { managerName = value; }
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

        #region 對應預報加班的ID
        /// <summary>
        /// 對應預報加班的ID
        /// </summary>
        [Column("AdvanceID")]
        public string AdvanceID
        {
            get { return advanceID; }
            set { advanceID = value; }
        }
        #endregion


        #region 預報開始時間
        /// <summary>
        /// 預報開始時間
        /// </summary>
        [Column("BeginTime")]
        public Nullable<DateTime> BeginTime
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
        public Nullable<DateTime> EndTime
        {
            get { return endTime; }
            set { endTime = value; }
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


        #region  是否專案加班:如果是則值為D,否則為空
        /// <summary>
        /// 是否專案加班:如果是則值為D,否則為空
        /// </summary>
        [Column("diffreason")]
        public string DiffReason
        {
            get { return diffReason; }
            set { diffReason = value; }
        }

        #endregion

        #region 管控提示
        /// <summary>
        /// 管控提示
        /// </summary>
        [Column("Remark")]
        public string Remark
        {
            get { return remark; }
            set { remark = value; }
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

        #region 簽核意見
        /// <summary>
        /// 簽核意見
        /// </summary>
        [Column("apRemark")]
        public string ApRemark
        {
            get { return apRemark; }
            set { apRemark = value; }
        }

        #endregion

        #region 加班類別
        /// <summary>
        /// 加班類別
        /// </summary>
        [Column("RealType")]
        public string RealType
        {
            get { return realType; }
            set { realType = value; }
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

        #region 實際開始時間
        /// <summary>
        /// 實際開始時間
        /// </summary>
        [Column("OnDutyTime")]
        public Nullable<DateTime> OnDutyTime
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
        public Nullable<DateTime> OffDutyTime
        {
            get { return offDutyTime; }
            set { offDutyTime = value; }
        }
        #endregion

        #region 實際時數
        /// <summary>
        /// 實際時數
        /// </summary>
        [Column("realhours")]
        public Nullable<Decimal> RealHours
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
        public Nullable<Decimal> ConfirmHours
        {
            get { return confirmHours; }
            set { confirmHours = value; }
        }

        #endregion

        #region 確認備註
        /// <summary>
        /// 確認備註
        /// </summary>
        [Column("confirmRemark")]
        public string ConfirmRemark
        {
            get { return confirmRemark; }
            set { confirmRemark = value; }
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
        [Column("Status")]
        public string Status
        {
            get { return status; }
            set { status = value; }
        }

        #endregion

        #region 申請日期
        /// <summary>
        /// 申請日期
        /// </summary>
        [Column("ApplyDate")]
        public Nullable<DateTime> ApplyDate
        {
            get { return applyDate; }
            set { applyDate = value; }
        }
        #endregion

        #region 維護人
        /// <summary>
        /// 維護人
        /// </summary>
        [Column("UpdateUser")]
        public string UpdateUser
        {
            get { return updateUser; }
            set { updateUser = value; }
        }

        #endregion

        #region 維護日期
        /// <summary>
        /// 維護日期
        /// </summary>
        [Column("UpdateDate")]
        public Nullable<DateTime> UpdateDate
        {
            get { return updateDate; }
            set { updateDate = value; }
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

        #region IsPay
        /// <summary>
        /// IsPay
        /// </summary>
        [Column("IsPay", OnlySelect = true)]
        public string IsPay
        {
            get { return isPay; }
            set { isPay = value; }
        }

        #endregion

        #region 是否調休
        /// <summary>
        /// 是否調休
        /// </summary>
        [Column("G2IsForrest")]
        public string G2IsForrest
        {
            get { return g2IsForrest; }
            set { g2IsForrest = value; }
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

        #region EnWeek
        /// <summary>
        /// EnWeek
        /// </summary>
        [Column("EnWeek", OnlySelect = true)]
        public string EnWeek
        {
            get { return enWeek; }
            set { enWeek = value; }
        }

        #endregion

        #region G1合計
        /// <summary>
        /// G1合計
        /// </summary>
        [Column("G1Total", OnlySelect = true)]
        public Nullable<Decimal> G1Total
        {
            get { return g1Total; }
            set { g1Total = value; }
        }

        #endregion

        #region G2合計
        /// <summary>
        /// G2合計
        /// </summary>
        [Column("G2Total", OnlySelect = true)]
        public Nullable<Decimal> G2Total
        {
            get { return g2Total; }
            set { g2Total = value; }
        }

        #endregion

        #region G3合計
        /// <summary>
        /// G3合計
        /// </summary>
        [Column("G3Total", OnlySelect = true)]
        public Nullable<Decimal> G3Total
        {
            get { return g3Total; }
            set { g3Total = value; }
        }

        #endregion


        #region advanceTime
        /// <summary>
        ///  advanceTime
        /// </summary>
        [Column("advancetime", OnlySelect = true)]
        public string AdvanceTime
        {
            get { return advanceTime; }
            set { advanceTime = value; }
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

        #region dcode
        /// <summary>
        /// dcode
        /// </summary>
        [Column("Dcode", OnlySelect = true)]
        public string Dcode
        {
            get { return dcode; }
            set { dcode = value; }
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

        #region 部門
        /// <summary>
        /// 部門
        /// </summary>
        [Column("DepName", OnlySelect = true)]
        public string DepName
        {
            get { return depName; }
            set { depName = value; }
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

        #region PersonType
        /// <summary>
        ///  PersonType
        /// </summary>
        [Column("PersonType", OnlySelect = true)]
        public string PersonType
        {
            get { return personType; }
            set { personType = value; }
        }

        #endregion

        #region 簽核人名字
        /// <summary>
        /// 簽核人名字
        /// </summary>
        [Column("ApproverName", OnlySelect = true)]
        public string ApproverName
        {
            get { return approverName; }
            set { approverName = value; }
        }
        #endregion

        #region IsProject
        /// <summary>
        /// IsProject
        /// </summary>
        [Column("IsProject", OnlySelect = true)]
        public string IsProject
        {
            get { return isProject; }
            set { isProject = value; }
        }

        #endregion


        #region ShiftDesc
        /// <summary>
        /// ShiftDesc
        /// </summary>
        [Column("ShiftDesc", OnlySelect = true)]
        public string ShiftDesc
        {
            get { return shiftDesc; }
            set { shiftDesc = value; }
        }

        #endregion
       

    }
}
