/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： GdsAttEmployeesVModel.cs
 * 檔功能描述： 本地基本信息查詢實體
 * 
 * 版本：1.0
 * 創建標識： 何西 2011.12.27
 * 
 */

using System;
using GDSBG.MiABU.Attendance.Common.Attributes;

namespace GDSBG.MiABU.Attendance.Model.HRM.EmployeeData
{
    /// <summary>
    /// 功能實體類
    /// </summary>
    [Serializable, TableName("gds_att_employees", SelectTable = "gds_att_employees_v")]
    public class GdsAttEmployeesVModel : ModelBase
    {
        private string workno;
        private string localname;
        private string levelcode;
        private string inhabitation;
        private string sourcecode;
        private string managercode;
        private string identityno;
        private string identityaddress;
        private string lastschool;
        private string status;
        private string notes;
        private Nullable<DateTime> joinbgdate;
        private string iscontrol;
        private string sex;
        private string sexcode;
        private Nullable<DateTime> graduatedate;
        private string classyear;
        private string is133;
        private string is138;
        private string classyearremark;
        private string statusname;
        private Nullable<DateTime> leavedate;
        private string regtypename;
        private string nationname;
        private string technicalname;
        private string technicaltype;
        private string sourcename;
        private string levelname;
        private string managername;
        private string costcode;
        private string isclassyear;
        private string technicalcode;
        private string depcode;
        private string degreecode;
        private string professionalcode;
        private string depname;
        private string syc;
        private string syccode;
        private string bgname;
        private string cbgname;
        private string cbgcode;
        private string areaname;
        private string degreename;
        private string professionalname;
        private string comeyears;
        private string asseslevel;
        private string leveltype;
        private string subject;
        private Nullable<DateTime> joindate;
        private Nullable<DateTime> borndate;
        private string overtimetype;
        private string persontypecode;
        private string postcode;
        private string overtimetypename;
        private string persontypename;
        private string simplename;
        private string postname;
        private string ename;
        private string technicalnamen;

        #region
        /// <summary>
        /// 模組代碼
        /// </summary>
        [Column("workno")]
        public string Workno
        {
            get { return workno; }
            set { workno = value; }
        }
        #endregion

        #region
        /// <summary>
        /// 模組代碼
        /// </summary>
        [Column("localname")]
        public string Localname
        {
            get { return localname; }
            set { localname = value; }
        }
        #endregion

        #region
        /// <summary>
        /// 模組代碼
        /// </summary>
        [Column("levelcode")]
        public string Levelcode
        {
            get { return levelcode; }
            set { levelcode = value; }
        }
        #endregion

        #region
        /// <summary>
        /// 模組代碼
        /// </summary>
        [Column("inhabitation")]
        public string Inhabitation
        {
            get { return inhabitation; }
            set { inhabitation = value; }
        }
        #endregion

        #region  sourcecode
        /// <summary>
        /// 模組代碼
        /// </summary>
        [Column("sourcecode")]
        public string Sourcecode
        {
            get { return sourcecode; }
            set { sourcecode = value; }
        }
        #endregion

        #region  managercode
        /// <summary>
        /// 模組代碼
        /// </summary>
        [Column("managercode")]
        public string Managercode
        {
            get { return managercode; }
            set { managercode = value; }
        }
        #endregion

        #region  identityno
        /// <summary>
        /// 模組代碼
        /// </summary>
        [Column("identityno")]
        public string Identityno
        {
            get { return identityno; }
            set { identityno = value; }
        }
        #endregion

        #region  identityaddress
        /// <summary>
        /// 模組代碼
        /// </summary>
        [Column("identityaddress")]
        public string Identityaddress
        {
            get { return identityaddress; }
            set { identityaddress = value; }
        }
        #endregion

        #region  lastschool
        /// <summary>
        /// 模組代碼
        /// </summary>
        [Column("lastschool")]
        public string Lastschool
        {
            get { return lastschool; }
            set { lastschool = value; }
        }
        #endregion



        #region  status
        /// <summary>
        /// 模組代碼
        /// </summary>
        [Column("status")]
        public string Status
        {
            get { return status; }
            set { status = value; }
        }
        #endregion
        #region  notes
        /// <summary>
        /// 模組代碼
        /// </summary>
        [Column("notes")]
        public string Notes
        {
            get { return notes; }
            set { notes = value; }
        }
        #endregion

        #region  joinbgdate
        /// <summary>
        /// 模組代碼
        /// </summary>
        [Column("joinbgdate")]
        public Nullable<DateTime> Joinbgdate
        {
            get { return joinbgdate; }
            set { joinbgdate = value; }
        }
        #endregion

        #region  iscontrol
        /// <summary>
        /// 模組代碼
        /// </summary>
        [Column("iscontrol")]
        public string Iscontrol
        {
            get { return iscontrol; }
            set { iscontrol = value; }
        }
        #endregion

        #region  sex
        /// <summary>
        /// 模組代碼
        /// </summary>
        [Column("sex")]
        public string Sex
        {
            get { return sex; }
            set { sex = value; }
        }
        #endregion

        #region  sexcode
        /// <summary>
        /// 模組代碼
        /// </summary>
        [Column("sexcode")]
        public string Sexcode
        {
            get { return sexcode; }
            set { sexcode = value; }
        }
        #endregion

        #region  graduatedate
        /// <summary>
        /// 模組代碼
        /// </summary>
        [Column("graduatedate")]
        public Nullable<DateTime> Graduatedate
        {
            get { return graduatedate; }
            set { graduatedate = value; }
        }
        #endregion

        #region  classyear
        /// <summary>
        /// 模組代碼
        /// </summary>
        [Column("classyear")]
        public string Classyear
        {
            get { return classyear; }
            set { classyear = value; }
        }
        #endregion

        #region  is133
        /// <summary>
        /// 模組代碼
        /// </summary>
        [Column("is133")]
        public string Is133
        {
            get { return is133; }
            set { is133 = value; }
        }
        #endregion

        #region  is138
        /// <summary>
        /// 模組代碼
        /// </summary>
        [Column("is138")]
        public string Is138
        {
            get { return is138; }
            set { is138 = value; }
        }
        #endregion

        #region  classyearremark
        /// <summary>
        /// 模組代碼
        /// </summary>
        [Column("classyearremark")]
        public string Classyearremark
        {
            get { return classyearremark; }
            set { classyearremark = value; }
        }
        #endregion

        #region  statusname
        /// <summary>
        /// 模組代碼
        /// </summary>
        [Column("statusname")]
        public string Statusname
        {
            get { return statusname; }
            set { statusname = value; }
        }
        #endregion

        #region  leavedate
        /// <summary>
        /// 模組代碼
        /// </summary>
        [Column("leavedate")]
        public Nullable<DateTime> Leavedate
        {
            get { return leavedate; }
            set { leavedate = value; }
        }
        #endregion

        #region  regtypename
        /// <summary>
        /// 模組代碼
        /// </summary>
        [Column("regtypename")]
        public string Regtypename
        {
            get { return regtypename; }
            set { regtypename = value; }
        }
        #endregion

        #region  nationname
        /// <summary>
        /// 模組代碼
        /// </summary>
        [Column("nationname")]
        public string Nationname
        {
            get { return nationname; }
            set { nationname = value; }
        }
        #endregion

        #region  technicalname
        /// <summary>
        /// 模組代碼
        /// </summary>
        [Column("technicalname")]
        public string Technicalname
        {
            get { return technicalname; }
            set { technicalname = value; }
        }
        #endregion

        #region  technicaltype
        /// <summary>
        /// 模組代碼
        /// </summary>
        [Column("technicaltype")]
        public string Technicaltype
        {
            get { return technicaltype; }
            set { technicaltype = value; }
        }
        #endregion

        #region  sourcename
        /// <summary>
        /// 模組代碼
        /// </summary>
        [Column("sourcename")]
        public string Sourcename
        {
            get { return sourcename; }
            set { sourcename = value; }
        }
        #endregion

        #region  levelname
        /// <summary>
        /// 模組代碼
        /// </summary>
        [Column("levelname")]
        public string Levelname
        {
            get { return levelname; }
            set { levelname = value; }
        }
        #endregion

        #region  managername
        /// <summary>
        /// 模組代碼
        /// </summary>
        [Column("managername")]
        public string Managername
        {
            get { return managername; }
            set { managername = value; }
        }
        #endregion

        #region 費用代碼 costcode
        /// <summary>
        /// 費用代碼
        /// </summary>
        [Column("costcode")]
        public string Costcode
        {
            get { return costcode; }
            set { costcode = value; }
        }
        #endregion

        #region  isclassyear
        /// <summary>
        /// 模組代碼
        /// </summary>
        [Column("isclassyear")]
        public string Isclassyear
        {
            get { return isclassyear; }
            set { isclassyear = value; }
        }
        #endregion

        #region  technicalcode
        /// <summary>
        /// 模組代碼
        /// </summary>
        [Column("technicalcode")]
        public string Technicalcode
        {
            get { return technicalcode; }
            set { technicalcode = value; }
        }
        #endregion

        #region  depcode
        /// <summary>
        /// 模組代碼
        /// </summary>
        [Column("depcode")]
        public string Depcode
        {
            get { return depcode; }
            set { depcode = value; }
        }
        #endregion

        #region  degreecode
        /// <summary>
        /// 模組代碼
        /// </summary>
        [Column("degreecode")]
        public string Degreecode
        {
            get { return degreecode; }
            set { degreecode = value; }
        }
        #endregion

        #region  professionalcode
        /// <summary>
        /// 模組代碼
        /// </summary>
        [Column("professionalcode")]
        public string Professionalcode
        {
            get { return professionalcode; }
            set { professionalcode = value; }
        }
        #endregion

        #region  comeyears
        /// <summary>
        /// 模組代碼
        /// </summary>
        [Column("comeyears")]
        public string Comeyears
        {
            get { return comeyears; }
            set { comeyears = value; }
        }
        #endregion

        #region  asseslevel
        /// <summary>
        /// 模組代碼
        /// </summary>
        [Column("asseslevel")]
        public string Asseslevel
        {
            get { return asseslevel; }
            set { asseslevel = value; }
        }
        #endregion

        #region  leveltype
        /// <summary>
        /// 模組代碼
        /// </summary>
        [Column("leveltype")]
        public string Leveltype
        {
            get { return leveltype; }
            set { leveltype = value; }
        }
        #endregion

        #region  subject
        /// <summary>
        /// 模組代碼
        /// </summary>
        [Column("subject")]
        public string Subject
        {
            get { return subject; }
            set { subject = value; }
        }
        #endregion

        #region  joindate
        /// <summary>
        /// 模組代碼
        /// </summary>
        [Column("joindate")]
        public Nullable<DateTime> Joindate
        {
            get { return joindate; }
            set { joindate = value; }
        }
        #endregion

        #region  borndate
        /// <summary>
        /// 模組代碼
        /// </summary>
        [Column("borndate")]
        public Nullable<DateTime> Borndate
        {
            get { return borndate; }
            set { borndate = value; }
        }
        #endregion


        #region  overtimetype
        /// <summary>
        /// 模組代碼
        /// </summary>
        [Column("overtimetype")]
        public string Overtimetype
        {
            get { return overtimetype; }
            set { overtimetype = value; }
        }
        #endregion

        #region  persontypecode
        /// <summary>
        /// 模組代碼
        /// </summary>
        [Column("persontypecode")]
        public string Persontypecode
        {
            get { return persontypecode; }
            set { persontypecode = value; }
        }
        #endregion

        #region  postcode
        /// <summary>
        /// 模組代碼
        /// </summary>
        [Column("postcode")]
        public string Postcode
        {
            get { return postcode; }
            set { postcode = value; }
        }
        #endregion

        #region  overtimetypename
        /// <summary>
        /// 模組代碼
        /// </summary>
        [Column("overtimetypename")]
        public string Overtimetypename
        {
            get { return overtimetypename; }
            set { overtimetypename = value; }
        }
        #endregion

        #region  persontypename
        /// <summary>
        /// 模組代碼
        /// </summary>
        [Column("persontypename")]
        public string Persontypename
        {
            get { return persontypename; }
            set { persontypename = value; }
        }
        #endregion


        #region  simplename
        /// <summary>
        /// 模組代碼
        /// </summary>
        [Column("simplename")]
        public string Simplename
        {
            get { return simplename; }
            set { simplename = value; }
        }
        #endregion

        #region  postname
        /// <summary>
        /// 模組代碼
        /// </summary>
        [Column("postname")]
        public string Postname
        {
            get { return postname; }
            set { postname = value; }
        }
        #endregion

        #region  ename
        /// <summary>
        /// 模組代碼
        /// </summary>
        [Column("ename")]
        public string Ename
        {
            get { return ename; }
            set { ename = value; }
        }
        #endregion

        #region  technicalnamen
        /// <summary>
        /// 模組代碼
        /// </summary>
        [Column("technicalnamen")]
        public string Technicalnamen
        {
            get { return technicalnamen; }
            set { technicalnamen = value; }
        }
        #endregion

        #region  depname
        /// <summary>
        /// 模組代碼
        /// </summary>
        [Column("depname")]
        public string Depname
        {
            get { return depname; }
            set { depname = value; }
        }
        #endregion


        #region  syc
        /// <summary>
        /// 模組代碼
        /// </summary>
        [Column("syc")]
        public string Syc
        {
            get { return syc; }
            set { syc = value; }
        }
        #endregion

        #region  syccode
        /// <summary>
        /// 模組代碼
        /// </summary>
        [Column("syccode")]
        public string Syccode
        {
            get { return syccode; }
            set { syccode = value; }
        }
        #endregion

        #region  bgname
        /// <summary>
        /// 模組代碼
        /// </summary>
        [Column("bgname")]
        public string Bgname
        {
            get { return bgname; }
            set { bgname = value; }
        }
        #endregion

        #region  cbgname
        /// <summary>
        /// 模組代碼
        /// </summary>
        [Column("cbgname")]
        public string Cbgname
        {
            get { return cbgname; }
            set { cbgname = value; }
        }
        #endregion

        #region cbgcode
        /// <summary>
        /// 模組代碼
        /// </summary>
        [Column("cbgcode")]
        public string Cbgcode
        {
            get { return cbgcode; }
            set { cbgcode = value; }
        }
        #endregion


        #region  areaname
        /// <summary>
        /// 模組代碼
        /// </summary>
        [Column("areaname")]
        public string Areaname
        {
            get { return areaname; }
            set { areaname = value; }
        }
        #endregion

        #region  degreename
        /// <summary>
        /// 模組代碼
        /// </summary>
        [Column("degreename")]
        public string Degreename
        {
            get { return degreename; }
            set { degreename = value; }
        }
        #endregion

        #region
        /// <summary>
        /// 模組代碼
        /// </summary>
        [Column("professionalname")]
        public string Professionalname
        {
            get { return professionalname; }
            set { professionalname = value; }
        }
        #endregion





    }
}
