/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： OTMAdvanceApplyModel.cs
 * 檔功能描述：加班明細查詢實體類
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
    /// 加班明細查詢實體類
    /// </summary>
    [Serializable, TableName("gds_att_advanceapply", SelectTable = "gds_att_otmadvanceapply_v")]
    public class OTMAdvanceApplyQryModel : ModelBase
    {
        private string iD;
        private string workNo;
        private string oTType;
        private Nullable<DateTime> oTDate;
        private Nullable<DateTime> beginTime;
        private Nullable<DateTime> endTime;
        private Nullable<Decimal> hours;
        private string workDesc;
        private string remark;
        private string oTMsgFlag;
        private string approver;
        private Nullable<DateTime> approveDate;
        private string apRemark;
        private string status;
        private string updateUser;
        private Nullable<DateTime> updateDate;
        private Nullable<DateTime> applyDate;
        private string billNo;
        private string isProject;
        private string importRemark;
        private string importFlag;
        private string oTShiftNo;
        private Nullable<DateTime> planAdjust;
        private string isPay;
        private string g2IsForrest;
        private string levelName;
        private string week;
        private string enWeek;
        private Nullable<Decimal> g1Total;
        private Nullable<Decimal> g2Total;
        private Nullable<Decimal> g3Total;
        private string localName;
        private string overTimeType;
        private string dcode;
        private string managerName;
        private string statusName;
        private string depName;
        private string buName;
        private string personType;
        private string modifyName;
        private string approverName;

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

        #region 開始時間
        /// <summary>
        /// 開始時間
        /// </summary>
        [Column("BeginTime")]
        public Nullable<DateTime> BeginTime
        {
            get { return beginTime; }
            set { beginTime = value; }
        }
        #endregion

        #region 結束時間
        /// <summary>
        /// 結束時間
        /// </summary>
        [Column("EndTime")]
        public Nullable<DateTime> EndTime
        {
            get { return endTime; }
            set { endTime = value; }
        }
        #endregion

        #region 時數
        /// <summary>
        /// 時數
        /// </summary>
        [Column("Hours")]
        public Nullable<Decimal> Hours
        {
            get { return hours; }
            set { hours = value; }
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

        #region  OTMsgFlag
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

        #region 更新人
        /// <summary>
        /// 更新人
        /// </summary>
        [Column("UpdateUser")]
        public string UpdateUser
        {
            get { return updateUser; }
            set { updateUser = value; }
        }

        #endregion

        #region 更新日期
        /// <summary>
        /// 更新日期
        /// </summary>
        [Column("UpdateDate")]
        public Nullable<DateTime> UpdateDate
        {
            get { return updateDate; }
            set { updateDate = value; }
        }

        #endregion

        #region 申請日期
        /// <summary>
        /// 申請日期
        /// </summary>
        [Column("ApplyDate",OnlySelect=true)]
        public Nullable<DateTime> ApplyDate
        {
            get { return applyDate; }
            set { applyDate = value; }
        }

        #endregion

        #region 簽核單號
        /// <summary>
        /// 簽核單號
        /// </summary>
        [Column("BillNo", OnlySelect = true)]
        public string BillNo
        {
            get { return billNo; }
            set { billNo = value; }
        }

        #endregion

        #region  IsProject
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

        #region 轉入有效
        /// <summary>
        /// 轉入有效
        /// </summary>
        [Column("ImportFlag", OnlySelect = true)]
        public string ImportFlag
        {
            get { return importFlag; }
            set { importFlag = value; }
        }

        #endregion

        #region  ImportRemark
        /// <summary>
        /// ImportRemark
        /// </summary>
        [Column("ImportRemark", OnlySelect = true)]
        public string ImportRemark
        {
            get { return importRemark; }
            set { importRemark = value; }
        }

        #endregion

        #region OTShiftNo
        /// <summary>
        /// OTShiftNo
        /// </summary>
        [Column("OTShiftNo", OnlySelect = true)]
        public string OTShiftNo
        {
            get { return oTShiftNo; }
            set { oTShiftNo = value; }
        }

        #endregion

        #region PlanAdjust
        /// <summary>
        /// PlanAdjust
        /// </summary>
        [Column("PlanAdjust", OnlySelect = true)]
        public Nullable<DateTime> PlanAdjust
        {
            get { return planAdjust; }
            set { planAdjust = value; }
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

        #region G2IsForrest
        /// <summary>
        ///  G2IsForrest
        /// </summary>
        [Column("G2IsForrest", OnlySelect = true)]
        public string G2IsForrest
        {
            get { return g2IsForrest; }
            set { g2IsForrest = value; }
        }

        #endregion

        #region Week
        /// <summary>
        ///  Week
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

        #region G1Total
        /// <summary>
        /// G1Total
        /// </summary>
        [Column("G1Total", OnlySelect = true)]
        public Nullable<Decimal> G1Total
        {
            get { return g1Total; }
            set { g1Total = value; }
        }

        #endregion

        #region G3Total
        /// <summary>
        /// G3Total
        /// </summary>
        [Column("G2Total", OnlySelect = true)]
        public Nullable<Decimal> G2Total
        {
            get { return g2Total; }
            set { g2Total = value; }
        }

        #endregion

        #region G3Total
        /// <summary>
        /// G3Total
        /// </summary>
        [Column("G3Total", OnlySelect = true)]
        public Nullable<Decimal> G3Total
        {
            get { return g3Total; }
            set { g3Total = value; }
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

        #region levelName
        /// <summary>
        /// levelName
        /// </summary>
        [Column("levelName", OnlySelect = true)]
        public string LevelName
        {
            get { return levelName; }
            set { levelName = value; }
        }

        #endregion

        #region 管理者名字
        /// <summary>
        /// 管理者名字
        /// </summary>
        [Column("ManagerName", OnlySelect = true)]
        public string ManagerName
        {
            get { return managerName; }
            set { managerName = value; }
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
        /// PersonType
        /// </summary>
        [Column("PersonType", OnlySelect = true)]
        public string PersonType
        {
            get { return personType; }
            set { personType = value; }
        }

        #endregion

        #region 申請人名字
        /// <summary>
        /// 申請人名字
        /// </summary>
        [Column("ModifyName", OnlySelect = true)]
        public string ModifyName
        {
            get { return modifyName; }
            set { modifyName = value; }
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
    }
}
