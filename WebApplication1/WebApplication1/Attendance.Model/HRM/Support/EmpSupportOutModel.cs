/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： EmpSupportOutModel.cs
 * 檔功能描述： 外部支援資料實體類
 * 
 * 版本：1.0
 * 創建標識： 顧陳偉 2012.01.04
 * 
 */

using System;
using GDSBG.MiABU.Attendance.Common.Attributes;
namespace GDSBG.MiABU.Attendance.Model.HRM.Support
{
    [Serializable, TableName("gds_att_empsupportout", SelectTable = "gds_att_empsupportout_v")]
    public class EmpSupportOutModel : ModelBase
    {
        private string workno;
        private string supportorder;
        private string localname;
        private string sex;
        private string levelcode;
        private string managercode;
        private string depname;
        private string iskaoqin;
        private string overtimetype;
        private string supportdept;
        private Nullable<DateTime> startdate;
        private Nullable<DateTime> prependdate;
        private Nullable<DateTime> enddate;
        private string remark;
        private string state;
        private string byname;
        private string cardno;
        private Nullable<DateTime> joindate;
        private string marrystate;
        private string identityno;
        private string technicalcode;
        private string professtionalcode;
        private string teamcode;
        private string notes;
        private string passwd;
        private string updateuser;
        private Nullable<DateTime> updatedate;
        private Nullable<DateTime> createdate;
        private string createuser;
        private string supportdeptname;
        private string levelname;
        private string managername;
        private string overtimetypename;
        private string statename;
        private string sexname;
        private string costcode;

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

        #region 支援順序
        /// <summary>
        /// 支援順序
        /// </summary>
        [Column("supportorder", IsPrimaryKey = true)]
        public string SupportOrder
        {
            get { return supportorder; }
            set { supportorder = value; }
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

        #region 性別代碼
        /// <summary>
        /// 性別代碼
        /// </summary>
        [Column("sex")]
        public string Sex
        {
            get { return sex; }
            set { sex = value; }
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

        #region 所屬部門
        /// <summary>
        /// 所屬部門
        /// </summary>
        [Column("depname")]
        public string DepName
        {
            get { return depname; }
            set { depname = value; }
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

        #region 加班類別
        /// <summary>
        /// 加班類別
        /// </summary>
        [Column("overtimetype")]
        public string OverTimeType
        {
            get { return overtimetype; }
            set { overtimetype = value; }
        }
        #endregion

        #region 支援單位
        /// <summary>
        /// 支援單位
        /// </summary>
        [Column("supportdept")]
        public string SupportDept
        {
            get { return supportdept; }
            set { supportdept = value; }
        }
        #endregion

        #region 開始日期
        /// <summary>
        /// 開始日期
        /// </summary>
        [Column("startdate")]
        public Nullable<DateTime> StartDate
        {
            get { return startdate; }
            set { startdate = value; }
        }
        #endregion

        #region 預計結束日期
        /// <summary>
        /// 預計結束日期
        /// </summary>
        [Column("prependdate")]
        public Nullable<DateTime> PrepEndDate
        {
            get { return prependdate; }
            set { prependdate = value; }
        }
        #endregion

        #region 實際結束日期
        /// <summary>
        /// 實際結束日期
        /// </summary>
        [Column("enddate")]
        public Nullable<DateTime> EndDate
        {
            get { return enddate; }
            set { enddate = value; }
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

        #region 狀態(0：支援中；1：已返回)
        /// <summary>
        /// 狀態(0：支援中；1：已返回)
        /// </summary>
        [Column("state")]
        public string State
        {
            get { return state; }
            set { state = value; }
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

        #region 入廠日期
        /// <summary>
        /// 入廠日期
        /// </summary>
        [Column("joindate")]
        public Nullable<DateTime> JoinDate
        {
            get { return joindate; }
            set { joindate = value; }
        }
        #endregion

        #region 婚否
        /// <summary>
        /// 婚否
        /// </summary>
        [Column("marrystate")]
        public string MarryState
        {
            get { return marrystate; }
            set { marrystate = value; }
        }
        #endregion

        #region 身份證號
        /// <summary>
        /// 身份證號
        /// </summary>
        [Column("identityno")]
        public string IdentityNo
        {
            get { return identityno; }
            set { identityno = value; }
        }
        #endregion

        #region TechnicalCode
        /// <summary>
        /// TechnicalCode
        /// </summary>
        [Column("technicalcode")]
        public string TechnicalCode
        {
            get { return technicalcode; }
            set { technicalcode = value; }
        }
        #endregion

        #region ProfesstionalCode
        /// <summary>
        /// ProfesstionalCode
        /// </summary>
        [Column("professtionalcode")]
        public string ProfesstionalCode
        {
            get { return professtionalcode; }
            set { professtionalcode = value; }
        }
        #endregion

        #region 擴建組織
        /// <summary>
        /// 擴建組織
        /// </summary>
        [Column("teamcode")]
        public string TeamCode
        {
            get { return teamcode; }
            set { teamcode = value; }
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

        #region Notes
        /// <summary>
        /// Notes
        /// </summary>
        [Column("notes")]
        public string Notes
        {
            get { return notes; }
            set { notes = value; }
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

        #region 支援單位名稱
        /// <summary>
        /// 支援單位名稱
        /// </summary>
        [Column("supportdeptname",OnlySelect=true)]
        public string SupportDeptName
        {
            get { return supportdeptname; }
            set { supportdeptname = value; }
        }
        #endregion

        #region 資位名稱
        /// <summary>
        /// 資位名稱
        /// </summary>
        [Column("levelname",OnlySelect=true)]
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
        [Column("managername",OnlySelect=true)]
        public string ManagerName
        {
            get { return managername; }
            set { managername = value; }
        }
        #endregion

        #region 加班類別名稱
        /// <summary>
        /// 加班類別名稱
        /// </summary>
        [Column("overtimetypename",OnlySelect=true)]
        public string OvertimeTypeName
        {
            get { return overtimetypename; }
            set { overtimetypename = value; }
        }
        #endregion

        #region 支援狀態名稱
        /// <summary>
        /// 支援狀態名稱
        /// </summary>
        [Column("statename",OnlySelect=true)]
        public string StateName
        {
            get { return statename; }
            set { statename = value; }
        }
        #endregion

        #region 性別
        /// <summary>
        /// 性別
        /// </summary>
        [Column("sexname",OnlySelect=true)]
        public string SexName
        {
            get { return sexname; }
            set { sexname = value; }
        }
        #endregion

        #region 費用代碼
        /// <summary>
        /// 費用代碼
        /// </summary>
        [Column("costcode", OnlySelect = true)]
        public string CostCode
        {
            get { return costcode; }
            set { costcode = value; }
        }
        #endregion

    }
}
