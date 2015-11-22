/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： OTMRemainModel.cs
 * 檔功能描述： 有效加班實體類
 * 
 * 版本：1.0
 * 創建標識： 昝望 2011.12.23
 * 
 */
using System;
using System.Collections.Generic;
using System.Text;
using GDSBG.MiABU.Attendance.Common.Attributes;

namespace GDSBG.MiABU.Attendance.Model.KQM.OTM
{
    [Serializable, TableName("gds_att_REALAPPLY", SelectTable = "gds_att_realapply_v")]
    public class RealapplyModel : ModelBase
    {
        private string id;


        #region ID
        [Column("Id", IsPrimaryKey = true)]
        public string Id
        {
            get { return id; }
            set { id = value; }
        }
        #endregion

        private string depName;


        #region 組織名稱
        [Column("DepName")]
        public string DepName
        {
            get { return depName; }
            set { depName = value; }
        }
        #endregion

        private string employeeNo;


        #region 工號
        [Column("WorkNo")]
        public string EmployeeNo
        {
            get { return employeeNo; }
            set { employeeNo = value; }
        }
        #endregion

        private string name;

        #region 姓名
        [Column("LocalName")]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        #endregion

        private string status;

        #region 
        [Column("Status")]
        public string Status
        {
            get { return status; }
            set { status = value; }
        }
        #endregion

        private string dCode;

        #region 
        [Column("DCode")]
        public string DCode
        {
            get { return dCode; }
            set { dCode = value; }
        }
        #endregion

        private Nullable<DateTime> oTDate;

        #region
        [Column("OTDate")]
        public Nullable<DateTime> OTDate
        {
            get { return oTDate; }
            set { oTDate = value; }
        }
        #endregion

        private string oTType;

        #region
        [Column("OTType")]
        public string OTType
        {
            get { return oTType; }
            set { oTType = value; }
        }
        #endregion

        private string week;

        #region
        [Column("Week")]
        public string Week
        {
            get { return week; }
            set { week = value; }
        }
        #endregion

        private string overTimeType;

        #region
        [Column("OverTimeType")]
        public string OverTimeType
        {
            get { return overTimeType; }
            set { overTimeType = value; }
        }
        #endregion

        private string shiftDesc;

        #region
        [Column("ShiftDesc")]
        public string ShiftDesc
        {
            get { return shiftDesc; }
            set { shiftDesc = value; }
        }
        #endregion

        private Nullable<DateTime> advanceTime;

        #region
        [Column("AdvanceTime")]
        public Nullable<DateTime> AdvanceTime
        {
            get { return advanceTime; }
            set { advanceTime = value; }
        }
        #endregion

        private Nullable<double> advanceHours;

        #region
        [Column("AdvanceHours")]
        public Nullable<double> AdvanceHours
        {
            get { return advanceHours; }
            set { advanceHours = value; }
        }
        #endregion

        private Nullable<DateTime> overTimeSpan;

        #region
        [Column("OverTimeSpan")]
        public Nullable<DateTime> OverTimeSpan
        {
            get { return overTimeSpan; }
            set { overTimeSpan = value; }
        }
        #endregion

        private Nullable<double> realHours;

        #region
        [Column("RealHours")]
        public Nullable<double> RealHours
        {
            get { return realHours; }
            set { realHours = value; }
        }
        #endregion

        private Nullable<double> confirmHours;

        #region
        [Column("ConfirmHours")]
        public Nullable<double> ConfirmHours
        {
            get { return confirmHours; }
            set { confirmHours = value; }
        }
        #endregion

        private string confirmRemark;

        #region
        [Column("ConfirmRemark")]
        public string ConfirmRemark
        {
            get { return confirmRemark; }
            set { confirmRemark = value; }
        }
        #endregion

        private string workDesc;

        #region
        [Column("WorkDesc")]
        public string WorkDesc
        {
            get { return workDesc; }
            set { workDesc = value; }
        }
        #endregion

        private string isProject;

        #region
        [Column("IsProject")]
        public string IsProject
        {
            get { return isProject; }
            set { isProject = value; }
        }
        #endregion

        private string iSPay;

        #region
        [Column("ISPay")]
        public string ISPay
        {
            get { return iSPay; }
            set { iSPay = value; }
        }
        #endregion

        private string statusName;

        #region
        [Column("StatusName")]
        public string StatusName
        {
            get { return statusName; }
            set { statusName = value; }
        }
        #endregion

        private string g1Total;

        #region
        [Column("G1Total")]
        public string G1Total
        {
            get { return g1Total; }
            set { g1Total = value; }
        }
        #endregion

        private string g2Total;

        #region
        [Column("g2Total")]
        public string G2Total
        {
            get { return g2Total; }
            set { g2Total = value; }
        }
        #endregion

        private string g3Total;

        #region
        [Column("g3Total")]
        public string G3Total
        {
            get { return g3Total; }
            set { g3Total = value; }
        }
        #endregion

        private string remark;

        #region
        [Column("remark")]
        public string Remark
        {
            get { return remark; }
            set { remark = value; }
        }
        #endregion

        private string billNo;

        #region
        [Column("BillNo")]
        public string BillNo
        {
            get { return billNo; }
            set { billNo = value; }
        }
        #endregion

        private string approverName;

        #region
        [Column("ApproverName")]
        public string ApproverName
        {
            get { return approverName; }
            set { approverName = value; }
        }
        #endregion

        private Nullable<DateTime> approveDate;

        #region
        [Column("ApproveDate")]
        public Nullable<DateTime> ApproveDate
        {
            get { return approveDate; }
            set { approveDate = value; }
        }
        #endregion

        private string apRemark;

        #region
        [Column("ApRemark")]
        public string ApRemark
        {
            get { return apRemark; }
            set { apRemark = value; }
        }
        #endregion

        private string updateUser;

        #region
        [Column("Update_User")]
        public string UpdateUser
        {
            get { return updateUser; }
            set { updateUser = value; }
        }
        #endregion

        private Nullable<DateTime> updateDate;

        #region
        [Column("Update_Date")]
        public Nullable<DateTime> UpdateDate
        {
            get { return updateDate; }
            set { updateDate = value; }
        }
        #endregion

        private string oTMSGFlag;

        #region
        [Column("OTMSGFlag")]
        public string OTMSGFlag
        {
            get { return oTMSGFlag; }
            set { oTMSGFlag = value; }
        }
        #endregion


        private Nullable<DateTime> beginTime;

        [Column("beginTime")]
        public Nullable<DateTime> BeginTime
        {
            get { return beginTime; }
            set { beginTime = value; }
        }

        private Nullable<DateTime> endTime;

        [Column("endTime")]
        public Nullable<DateTime> EndTime
        {
            get { return endTime; }
            set { endTime = value; }
        }

        private string importRemark;

        [Column("ImportRemark")]
        public string ImportRemark
        {
            get { return importRemark; }
            set { importRemark = value; }
        }

    }
}
