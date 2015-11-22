/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： EmployeeModel.cs
 * 檔功能描述： 員工基本資料表實體類
 * 
 * 版本：1.0
 * 創建標識： 顧陳偉 2011.12.13
 * 
 */
using System;
using System.Collections.Generic;
using System.Text;
using GDSBG.MiABU.Attendance.Common.Attributes;

namespace GDSBG.MiABU.Attendance.Model.KQM.BasicData
{
    /// <summary>
    /// 員工基本資料表實體類
    /// </summary>
    [Serializable, TableName("gds_att_employee")]//, SelectTable = "gds_att_employee_v")]
    public class EmployeeModel : ModelBase
    {
        private string workno;
        private string localname;
        private string byname;
        private string sex;
        private string identityno;
        private Nullable<DateTime> joindate;
        private string cardno;
        private Nullable<DateTime> leavedate;
        private string marrystate;
        private string technicalcode;
        private string technicalname;
        private string professionalcode;
        private string professionalname;
        private string levelcode;
        private string levelname;
        private string managercode;
        private string managername;
        private string overtimetype;
        private string depcode;
        private string depname;
        private string teamcode;
        private string teamname;
        private string dcode;
        private string dname;
        private string status;
        private string flag;
        private string notes;
        private int deductyears;
        private string degreecode;
        private string degreename;
        private string regtypecode;
        private string regtypename;
        private string subject;
        private Nullable<DateTime> borndate;
        private Nullable<DateTime> joindeptdate;
        private Nullable<DateTime> joinbgdate;

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

        #region 入集團日期
        /// <summary>
        /// 入集團日期
        /// </summary>
        [Column("joindate")]
        public Nullable<DateTime> JoinDate
        {
            get { return joindate; }
            set { joindate = value; }
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

        #region 婚姻狀態
        /// <summary>
        /// 婚姻狀態
        /// </summary>
        [Column("marrystate")]
        public string MarryState
        {
            get { return marrystate; }
            set { marrystate = value; }
        }
        #endregion

        #region 職系代碼
        /// <summary>
        /// 職系代碼
        /// </summary>
        [Column("technicalcode")]
        public string TechnicalCode
        {
            get { return technicalcode; }
            set { technicalcode = value; }
        }
        #endregion

        #region 職系名稱
        /// <summary>
        /// 職系名稱
        /// </summary>
        [Column("technicalname")]
        public string TechnicalName
        {
            get { return technicalname; }
            set { technicalname = value; }
        }
        #endregion

        #region 職稱代碼
        /// <summary>
        /// 職稱代碼
        /// </summary>
        [Column("professionalcode")]
        public string ProfessionalCode
        {
            get { return professionalcode; }
            set { professionalcode = value; }
        }
        #endregion

        #region 職稱名稱
        /// <summary>
        /// 職稱名稱
        /// </summary>
        [Column("professionalname")]
        public string ProfessionalName
        {
            get { return professionalname; }
            set { professionalname = value; }
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

        #region 資位名稱
        /// <summary>
        /// 資位名稱
        /// </summary>
        [Column("levelname")]
        public string LevelName
        {
            get { return levelname; }
            set { levelname = value; }
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

        #region 管理職名稱
        /// <summary>
        /// 管理職名稱
        /// </summary>
        [Column("managername")]
        public string ManagerName
        {
            get { return managername; }
            set { managername = value; }
        }
        #endregion

        #region 加班類別
        /// <summary>
        /// 加班類別
        /// </summary>
        [Column("overtimetype")]
        public string OvertimeType
        {
            get { return overtimetype; }
            set { overtimetype = value; }
        }
        #endregion

        #region 部門代碼
        /// <summary>
        /// 部門代碼
        /// </summary>
        [Column("depcode")]
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
        [Column("depname")]
        public string DepName
        {
            get { return depname; }
            set { depname = value; }
        }
        #endregion

        #region 擴建組織代碼
        /// <summary>
        /// 擴建組織代碼
        /// </summary>
        [Column("teamcode")]
        public string TeamCode
        {
            get { return teamcode; }
            set { teamcode = value; }
        }
        #endregion

        #region 擴建組織名稱
        /// <summary>
        /// 擴建組織名稱
        /// </summary>
        [Column("teamname")]
        public string TeamName
        {
            get { return teamname; }
            set { teamname = value; }
        }
        #endregion

        #region 如果TEAMCODE為空或Null：DCODE=DEPCODE；否則：DCODE=TEAMCODE
        /// <summary>
        /// 如果TEAMCODE為空或Null：DCODE=DEPCODE；否則：DCODE=TEAMCODE
        /// </summary>
        [Column("dcode")]
        public string Dcode
        {
            get { return dcode; }
            set { dcode = value; }
        }
        #endregion

        #region 如果TEAMCODE為空或Null：DNAME=DEPNAME；否則：DNAME=DEPNAME(TEAMNAME)
        /// <summary>
        /// 如果TEAMCODE為空或Null：DNAME=DEPNAME；否則：DNAME=DEPNAME(TEAMNAME)
        /// </summary>
        [Column("dname")]
        public string Dname
        {
            get { return dname; }
            set { dname = value; }
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

        #region 人員類別：本土、派駐、外部支援
        /// <summary>
        /// 人員類別：本土、派駐、外部支援
        /// </summary>
        [Column("flag")]
        public string Flag
        {
            get { return flag; }
            set { flag = value; }
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

        #region 扣減年資
        /// <summary>
        /// 扣減年資
        /// </summary>
        [Column("deductyears")]
        public int DeductYears
        {
            get { return deductyears; }
            set { deductyears = value; }
        }
        #endregion

        #region 學歷代碼
        /// <summary>
        /// 學歷代碼
        /// </summary>
        [Column("degreecode")]
        public string DegreeCode
        {
            get { return degreecode; }
            set { degreecode = value; }
        }
        #endregion

        #region 學歷名稱
        /// <summary>
        /// 學歷名稱
        /// </summary>
        [Column("degreename")]
        public string DDegreeName
        {
            get { return degreename; }
            set { degreename = value; }
        }
        #endregion

        #region 戶籍類型代碼
        /// <summary>
        /// 戶籍類型代碼
        /// </summary>
        [Column("regtypecode")]
        public string RegtypeCode
        {
            get { return regtypecode; }
            set { regtypecode = value; }
        }
        #endregion

        #region 戶籍類型名稱
        /// <summary>
        /// 戶籍類型名稱
        /// </summary>
        [Column("regtypename")]
        public string RegtypeName
        {
            get { return regtypename; }
            set { regtypename = value; }
        }
        #endregion

        #region 專業
        /// <summary>
        /// 專業
        /// </summary>
        [Column("subject")]
        public string Subject
        {
            get { return subject; }
            set { subject = value; }
        }
        #endregion

        #region 出生日期
        /// <summary>
        /// 出生日期
        /// </summary>
        [Column("borndate")]
        public Nullable<DateTime> BornDate
        {
            get { return borndate; }
            set { borndate = value; }
        }
        #endregion

        #region 最後調入日期
        /// <summary>
        /// 最後調入日期
        /// </summary>
        [Column("joindeptdate")]
        public Nullable<DateTime> JoindeptDate
        {
            get { return joindeptdate; }
            set { joindeptdate = value; }
        }
        #endregion

        #region 入廠日期
        /// <summary>
        /// 入廠日期
        /// </summary>
        [Column("joinbgdate")]
        public Nullable<DateTime> JoinbgDate
        {
            get { return joinbgdate; }
            set { joinbgdate = value; }
        }
        #endregion

    }
}
