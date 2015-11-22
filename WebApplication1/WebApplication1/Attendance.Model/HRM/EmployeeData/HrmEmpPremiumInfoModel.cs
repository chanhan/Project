/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： HrmEmpPremiumInfoModel.cs
 * 檔功能描述：員工行政處分實體類
 * 
 * 版本：1.0
 * 創建標識： 劉炎 2012.03.12
 * 
 */
using System;
using GDSBG.MiABU.Attendance.Common.Attributes;

namespace GDSBG.MiABU.Attendance.Model.HRM.EmployeeData
{   
    /// <summary>
    /// 員工行政處分實體類
    /// </summary>
    [Serializable, TableName("HRM_EMP_PREMIUMS_SV")]
    public class HrmEmpPremiumInfoModel:ModelBase
    {
        private string colBuName;
        private string deptName;
        private string empNo;
        private string empName;
        private string premiumName;
        private Nullable<DateTime> premiumDate;
        private Nullable<int> premiumNum;
        private string premiumTitle;
        private string premiumComment;

        #region 事業處
        /// <summary>
        /// 事業處
        /// </summary>
        [Column("COL_BU_NAME")]

        public string ColBuName
        {
            get { return colBuName; }
            set { colBuName = value; }
        }
        #endregion

        #region 部門
        /// <summary>
        /// 部門
        /// </summary>
        [Column("DEPT_NAME")]

        public string DeptName
        {
            get { return deptName; }
            set { deptName = value; }
        }
        #endregion

        #region 工號
        /// <summary>
        /// 工號
        /// </summary>
        [Column("EMP_NO")]
        public string EmpNo
        {
            get { return empNo; }
            set { empNo = value; }
        }
        #endregion

        #region 姓名
        /// <summary>
        /// 姓名
        /// </summary>
        [Column("EMP_NAME")]

        public string EmpName
        {
            get { return empName; }
            set { empName = value; }
        }
        #endregion

        #region 獎懲類型
        /// <summary>
        /// 獎懲類型
        /// </summary>
        [Column("PREMIUM_NAME")]

        public string PremiumName
        {
            get { return premiumName; }
            set { premiumName = value; }
        }
        #endregion

        #region 獎懲日期
        /// <summary>
        /// 獎懲日期
        /// </summary>
        [Column("PREMIUM_DATE")]
        public Nullable<DateTime> PremiumDate
        {
            get { return premiumDate; }
            set { premiumDate = value; }
        }
        #endregion

        #region 次數
        /// <summary>
        /// 次數
        /// </summary>
        [Column("PREMIUM_NUM")]

        public Nullable<int> PremiumNum
        {
            get { return premiumNum; }
            set { premiumNum = value; }
        }
        #endregion

        #region 獎懲種類
        /// <summary>
        /// 獎懲種類
        /// </summary>
        [Column("PREMIUM_TITLE")]

        public string PremiumTitle
        {
            get { return premiumTitle; }
            set { premiumTitle = value; }
        }
        #endregion

        #region 獎懲事由
        /// <summary>
        /// 獎懲事由
        /// </summary>
        [Column("PREMIUM_COMMENT")]

        public string PremiumComment
        {
            get { return premiumComment; }
            set { premiumComment = value; }
        }
        #endregion
    }
}
