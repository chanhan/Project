/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： TWCadreModel.cs
 * 檔功能描述： 駐派幹部資料實體類
 * 
 * 版本：1.0
 * 創建標識： 顧陳偉 2011.12.16
 * 
 */

using System;
using GDSBG.MiABU.Attendance.Common.Attributes;
namespace GDSBG.MiABU.Attendance.Model.HRM
{
    /// <summary>
    /// 駐派幹部資料實體類
    /// </summary>
    [Serializable, TableName("gds_att_twcadre", SelectTable = "gds_att_twcadre_v")]
    public class TWCadreModel : ModelBase
    {
        private string workno;
        private string localname;
        private string sex;
        private string identityno;
        private string byname;
        private string levelcode;
        private string managercode;
        private string depcode;
        private string extension;
        private string notes;
        private Nullable<DateTime> joindate;
        private Nullable<DateTime> leavedate;
        private string status;
        private string passwd;
        private string teamcode;
        private string iskaoqin;
        private string cardno;
        private string flag;
        private string areacode;
        private string updateuser;
        private Nullable<DateTime> updatedate;
        private Nullable<DateTime> createdate;
        private string createuser;
        private string levelname;
        private string managername;
        private string sexname;
        private string depname;
        private string statusname;

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

        #region 姓名
        /// <summary>
        /// 姓名
        /// </summary>
        [Column("localname")]
        public string LocalName
        {
            get { return localname; }
            set { localname = value; }
        }
        #endregion

        #region 性別
        /// <summary>
        /// 性別
        /// </summary>
        [Column("sex")]
        public string Sex
        {
            get { return sex; }
            set { sex = value; }
        }
        #endregion

        #region 身份證號碼
        /// <summary>
        /// 身份證號碼
        /// </summary>
        [Column("identityno")]
        public string IdentityNo
        {
            get { return identityno; }
            set { identityno = value; }
        }
        #endregion

        #region 別名
        /// <summary>
        /// 別名
        /// </summary>
        [Column("byname")]
        public string ByName
        {
            get { return byname; }
            set { byname = value; }
        }
        #endregion

        #region 資位代碼
        /// <summary>
        /// 資位代碼
        /// </summary>
        [Column("levelcode")]
        public string LevelCode
        {
            get { return levelcode; }
            set { levelcode = value; }
        }
        #endregion

        #region 管理職代碼
        /// <summary>
        /// 管理職代碼
        /// </summary>
        [Column("managercode")]
        public string ManagerCode
        {
            get { return managercode; }
            set { managercode = value; }
        }
        #endregion

        #region 部門Code
        /// <summary>
        /// 部門Code
        /// </summary>
        [Column("depcode")]
        public string DepCode
        {
            get { return depcode; }
            set { depcode = value; }
        }
        #endregion

        #region 分機
        /// <summary>
        /// 分機
        /// </summary>
        [Column("extension")]
        public string Extension
        {
            get { return extension; }
            set { extension = value; }
        }
        #endregion

        #region NOTES
        /// <summary>
        /// NOTES
        /// </summary>
        [Column("notes")]
        public string Notes
        {
            get { return notes; }
            set { notes = value; }
        }
        #endregion

        #region 入職日期
        /// <summary>
        /// 入職日期
        /// </summary>
        [Column("joindate")]
        public Nullable<DateTime> JoinDate
        {
            get { return joindate; }
            set { joindate = value; }
        }
        #endregion

        #region 離職日期
        /// <summary>
        /// 離職日期
        /// </summary>
        [Column("leavedate")]
        public Nullable<DateTime> LeaveDate
        {
            get { return leavedate; }
            set { leavedate = value; }
        }
        #endregion

        #region 在職狀態
        /// <summary>
        /// 在職狀態
        /// </summary>
        [Column("status")]
        public string Status
        {
            get { return status; }
            set { status = value; }
        }
        #endregion

        #region 密碼
        /// <summary>
        /// 密碼
        /// </summary>
        [Column("passwd")]
        public string Passwd
        {
            get { return passwd; }
            set { passwd = value; }
        }
        #endregion

        #region 擴建組織Code
        /// <summary>
        /// 擴建組織Code
        /// </summary>
        [Column("teamcode")]
        public string TeamCode
        {
            get { return teamcode; }
            set { teamcode = value; }
        }
        #endregion

        #region 是否考勤
        /// <summary>
        /// 是否考勤
        /// </summary>
        [Column("iskaoqin")]
        public string IsKaoQin
        {
            get { return iskaoqin; }
            set { iskaoqin = value; }
        }
        #endregion

        #region 一卡通號
        /// <summary>
        /// 一卡通號
        /// </summary>
        [Column("cardno")]
        public string CardNo
        {
            get { return cardno; }
            set { cardno = value; }
        }
        #endregion

        #region 暫時沒用
        /// <summary>
        /// 暫時沒用
        /// </summary>
        [Column("flag")]
        public string Flag
        {
            get { return flag; }
            set { flag = value; }
        }
        #endregion

        #region 暫時沒用
        /// <summary>
        /// 暫時沒用
        /// </summary>
        [Column("areacode")]
        public string AreaCode
        {
            get { return areacode; }
            set { areacode = value; }
        }
        #endregion

        #region 更新用戶
        /// <summary>
        /// 更新用戶
        /// </summary>
        [Column("update_user")]
        public string UpdateUser
        {
            get { return updateuser; }
            set { updateuser = value; }
        }
        #endregion

        #region 創建日期
        /// <summary>
        /// 創建日期
        /// </summary>
        [Column("create_date")]
        public Nullable<DateTime> CreateDate
        {
            get { return createdate; }
            set { createdate = value; }
        }
        #endregion

        #region 更新日期
        /// <summary>
        /// 更新日期
        /// </summary>
        [Column("update_date")]
        public Nullable<DateTime> UpdateDate
        {
            get { return updatedate; }
            set { updatedate = value; }
        }
        #endregion

        #region 創建用戶
        /// <summary>
        /// 創建用戶
        /// </summary>
        [Column("create_user")]
        public string CreateUser
        {
            get { return createuser; }
            set { createuser = value; }
        }
        #endregion


        #region 資位名稱
        /// <summary>
        /// 資位名稱
        /// </summary>
        [Column("levelname", OnlySelect = true)]
        public string LevelName
        {
            get { return levelname; }
            set { levelname = value; }
        }
        #endregion

        #region 管理職名稱
        /// <summary>
        /// 管理職名稱
        /// </summary>
        [Column("managername", OnlySelect = true)]
        public string ManagerName
        {
            get { return managername; }
            set { managername = value; }
        }
        #endregion


        #region 性別類型名稱
        /// <summary>
        /// 性別類型名稱
        /// </summary>
        [Column("sexname", OnlySelect = true)]
        public string SexName
        {
            get { return sexname; }
            set { sexname = value; }
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

        #region 在職狀態名稱
        /// <summary>
        /// 在職狀態名稱
        /// </summary>
        [Column("statusname", OnlySelect = true)]
        public string StatusName
        {
            get { return statusname; }
            set { statusname = value; }
        }
        #endregion
    }
}
