/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： OTMRemainModel.cs
 * 檔功能描述： 剩餘加班導入實體類
 * 
 * 版本：1.0
 * 創建標識： 顧陳偉 2011.12.22
 * 
 */
using System;
using System.Collections.Generic;
using System.Text;
using GDSBG.MiABU.Attendance.Common.Attributes;

namespace GDSBG.MiABU.Attendance.Model.KQM.OTM
{
    [Serializable, TableName("gds_att_remain", SelectTable = "gds_att_remain_v")]
    public class OTMRemainModel : ModelBase
    {
        private string workno;
        private string yearmonth;
        private Nullable<Decimal> g1remain;
        private Nullable<Decimal> g23remain;
        private string updateUser;
        private Nullable<DateTime> updateDate;
        private Nullable<DateTime> createDate;
        private string createUser;
        private string iscurrent;
        private string remark;
        private string localname;
        private string depname;
        private string overtimetype;

        #region 工號
        /// <summary>
        /// 工號
        /// </summary>
        [Column("workno", IsPrimaryKey = true)]
        public string WorkNo
        {
            get { return workno; }
            set { workno = value; }
        }
        #endregion

        #region 年度月份
        /// <summary>
        /// 年度月份
        /// </summary>
        [Column("yearmonth")]
        public string YearMonth
        {
            get { return yearmonth; }
            set { yearmonth = value; }
        }
        #endregion

        #region G1剩餘加班時數
        /// <summary>
        /// G1剩餘加班時數
        /// </summary>
        [Column("g1remain")]
        public Nullable<Decimal> G1Remain
        {
            get { return g1remain; }
            set { g1remain = value; }
        }
        #endregion

        #region G2+G3剩餘加班時數
        /// <summary>
        /// G2+G3剩餘加班時數
        /// </summary>
        [Column("g23remain")]
        public Nullable<Decimal> G23Remain
        {
            get { return g23remain; }
            set { g23remain = value; }
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
        /// 創建日期
        /// </summary>
        [Column("create_date")]
        public Nullable<DateTime> CreateDate
        {
            get { return createDate; }
            set { createDate = value; }
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

        #region iscurrent
        /// <summary>
        /// iscurrent
        /// </summary>
        [Column("iscurrent")]
        public string IsCurrent
        {
            get { return iscurrent; }
            set { iscurrent = value; }
        }
        #endregion

        #region remark
        /// <summary>
        /// remark
        /// </summary>
        [Column("remark")]
        public string Remark
        {
            get { return remark; }
            set { remark = value; }
        }
        #endregion

        #region 姓名
        /// <summary>
        /// 姓名
        /// </summary>

        [Column("localname", OnlySelect = true)]
        public string LocalName
        {
            get { return localname; }
            set { localname = value; }
        }
        #endregion

        #region 部門名稱
        /// <summary>
        /// 部門名稱
        /// </summary>

        [Column("depname", OnlySelect = true)]
        public string DepName
        {
            get { return depname; }
            set { depname = value; }
        }
        #endregion

        #region 加班類型
        /// <summary>
        /// 加班類型
        /// </summary>

        [Column("overtimetype", OnlySelect = true)]
        public string OverTimeType
        {
            get { return overtimetype; }
            set { overtimetype = value; }
        }
        #endregion
    }
}
