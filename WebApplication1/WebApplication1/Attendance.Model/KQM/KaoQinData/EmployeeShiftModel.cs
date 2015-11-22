/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： ModuleModel.cs
 * 檔功能描述： 排班作業實體類
 * 
 * 版本：1.0
 * 創建標識： 昝望 2011.12.27
 * 
 */

using System;
using GDSBG.MiABU.Attendance.Common.Attributes;
namespace GDSBG.MiABU.Attendance.Model.KQM.KaoQinData
{
    /// <summary>
    /// 排班作業實體類
    /// </summary>
    [Serializable, TableName("gds_att_employeeshift", SelectTable = "gds_att_employeeshift_v")]
    public class EmployeeShiftModel : ModelBase
    {
        private string iD;
        private string workNo;
        private string localName;
        private string depCode;
        private string depName;
        private string shiftType;
        private string shiftDate;
        private string shift;
        private string startenddate;
        private string errorMsg;
        private string startDate;
        private string endDate;
        private string updateUser;
        private string syc;
        private Nullable<DateTime> updateDate;



   
  
  
        /// <summary>
        /// ID
        /// </summary>
        [Column("ID")]
        public string ID
        {
            get { return iD; }
            set { iD = value; }
        }


        /// <summary>
        /// 工號
        /// </summary>
        [Column("WorkNo")]
        public string WorkNo
        {
            get { return workNo; }
            set { workNo = value; }
        }

        /// <summary>
        /// 姓名
        /// </summary>
        [Column("LocalName")]
        public string LocalName
        {
            get { return localName; }
            set { localName = value; }
        }

        /// <summary>
        /// 部門代碼
        /// </summary>
        [Column("DCode")]
        public string DepCode
        {
            get { return depCode; }
            set { depCode = value; }
        }

        /// <summary>
        /// 部門名稱
        /// </summary>
        [Column("DName")]
        public string DepName
        {
            get { return depName; }
            set { depName = value; }
        }

        /// <summary>
        /// 類別
        /// </summary>
        [Column("ShiftType")]
        public string ShiftType
        {
            get { return shiftType; }
            set { shiftType = value; }
        }


        /// <summary>
        /// 班別
        /// </summary>
        [Column("ShiftNo")]
        public string Shift
        {
            get { return shift; }
            set { shift = value; }
        }

        /// <summary>
        /// 起止時間
        /// </summary>
        [Column("Startenddate")]
        public string Startenddate
        {
            get { return startenddate; }
            set { startenddate = value; }
        }

        /// <summary>
        /// 錯誤信息
        /// </summary>
        [Column("Errormsg")]
        public string ErrorMsg
        {
            get { return errorMsg; }
            set { errorMsg = value; }
        }

        /// <summary>
        /// 開始時間
        /// </summary>
        [Column("StartDate")]
        public string StartDate
        {
            get { return startDate; }
            set { startDate = value; }
        }

        /// <summary>
        /// 結束時間
        /// </summary>
        [Column("EndDate")]
        public string EndDate
        {
            get { return endDate; }
            set { endDate = value; }
        }

        [Column("Update_User")]
        public string UpdateUser
        {
            get { return updateUser; }
            set { updateUser = value; }
        }

        [Column("Update_Date")]
        public Nullable<DateTime> UpdateDate
        {
            get { return updateDate; }
            set { updateDate = value; }
        }

        /// <summary>
        /// 事業群
        /// </summary>
        public string Syc
        {
            get { return syc; }
            set { syc = value; }
        }
    }
}
