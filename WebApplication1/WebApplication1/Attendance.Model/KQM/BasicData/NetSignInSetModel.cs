/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： NetSignInSetModel.cs
 * 檔功能描述： 網上簽到名單設定實體類
 * 
 * 版本：1.0
 * 創建標識： 顧陳偉 2011.12.12
 * 
 */
using System;
using GDSBG.MiABU.Attendance.Common.Attributes;

namespace GDSBG.MiABU.Attendance.Model.KQM.BasicData
{
    /// <summary>
    /// 網上簽到名單設定實體類
    /// </summary>
    [Serializable, TableName("gds_att_signemployee", SelectTable = "gds_att_signemployee_v")]
    public class NetSignInSetModel : ModelBase
    {
        private string workno;
        private string flag;
        private Nullable<DateTime> startdate;
        private Nullable<DateTime> enddate;
        private Nullable<DateTime> createDate;
        private string createUser;
        private Nullable<DateTime> updateDate;
        private string updateUser;
        private string startenddate;
        private string localname;
        private string depcode;
        private string depname;
        private string flagname;

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

        #region 是否允許網上簽到
        /// <summary>
        /// 是否允許網上簽到
        /// </summary>
        [Column("flag")]
        public string Flag
        {
            get { return flag; }
            set { flag = value; }
        }
        #endregion

        #region 允許網上簽到開始日期
        /// <summary>
        /// 允許網上簽到開始日期
        /// </summary>
        [Column("startdate")]
        public Nullable<DateTime> StartDate
        {
            get { return startdate; }
            set { startdate = value; }
        }
        #endregion

        #region 允許網上簽到結束日期
        /// <summary>
        /// 允許網上簽到結束日期
        /// </summary>
        [Column("enddate")]
        public Nullable<DateTime> EndDate
        {
            get { return enddate; }
            set { enddate = value; }
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

        #region 開始至結束時間
        /// <summary>
        /// 開始至結束時間
        /// </summary>
        [Column("startenddate",OnlySelect=true)]
        public string Startenddate
        {
            get { return startenddate; }
            set { startenddate = value; }
        }
        #endregion

        #region 姓名
        /// <summary>
        /// 姓名
        /// </summary>
        [Column("localname",OnlySelect=true)]
        public string Localname
        {
            get { return localname; }
            set { localname = value; }
        }
        #endregion

        #region 部門CODE
        /// <summary>
        /// 部門CODE
        /// </summary>
        [Column("depcode", OnlySelect = true)]
        public string DepCode
        {
            get { return depcode; }
            set { depcode = value; }
        }
        #endregion

        #region 部門名稱
        /// <summary>
        /// 部門名稱
        /// </summary>
        [Column("depname",OnlySelect=true)]
        public string DepName
        {
            get { return depname; }
            set { depname = value; }
        }
        #endregion

        #region 是否允許簽到
        /// <summary>
        /// 是否允許簽到
        /// </summary>
        [Column("flagname",OnlySelect=true)]
        public string Flagname
        {
            get { return flagname; }
            set { flagname = value; }
        }
        #endregion
    }
}
