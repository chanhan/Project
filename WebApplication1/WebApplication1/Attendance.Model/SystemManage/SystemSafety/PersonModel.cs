/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： ModuleModel.cs
 * 檔功能描述： 用戶實體類
 * 
 * 版本：1.0
 * 創建標識： 昝望 2011.11.30
 * 
 */

using System;
using GDSBG.MiABU.Attendance.Common.Attributes;

namespace GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety
{
    /// <summary>
    /// 用戶實體類
    /// </summary>
    [Serializable, TableName("gds_sc_person", SelectTable = "gds_sc_person_v")]
    public class PersonModel : ModelBase
    {
        private string personcode;
        private string titleName;
        private string cname;
        private string appellation;
        private string ename;
        private string companyId;
        private string companyName;
        private string depCode;
        private string depName;
        private string sexId;
        private string roleCode;
        private string roleName;
        private string ifAdmin;
        private string tel;
        private string mobile;
        private string mail;
        private string passwd;
        private string extension;
        private string language;
        private string languageName;
        private string fax;
        private Nullable<int> loginTimes;
        private Nullable<DateTime> loginTime;
        private string dateFormat;
        private string dateTimeformat;
        private string numberFormat;
        private string amountFormat;
        private string decimalSeparator;
        private string colorsCheme;
        private string groupSeparator;
        private string mainBkcolorred;
        private string mainBkcolorgreen;
        private string mainBkcolorblue;
        private string treeMenubkcolorred;
        private string treeMenubkcolorgreen;
        private string treeMenubkcolorblue;
        private string windowBkcolorred;
        private string windowBkcolorgreen;
        private string windowBkcolorblue;
        private string fontColorred;
        private string fontColorgreen;
        private string fontColorblue;
        private string gridHeaderbkcolorred;
        private string gridHeaderbkcolorgreen;
        private string gridHeaderbkcolorblue;
        private string gridBkcolorred;
        private string gridBkcolorgreen;
        private string gridBkcolorblue;
        private string deleted;
        private string defaultRecords;
        private string gridAltbkcolorred;
        private string splitTercolorred;
        private string gridAltbkcolorgreen;
        private string splitTercolorgreen;
        private string gridAltbkcolorblue;
        private string splitTercolorblue;
        private string ifonLine;
        private string mdtColorred;
        private string ipAddress;
        private string mdtColorgreen;
        private string defaultModule;
        private string hostName;
        private string mdtColorblue;
        private string mdtChar;
        private string defaultMenu;
        private string areaCode;
        private string depLevel;
        private string levelName;
        private string modifier;
        private string empNo;
        private string userChangePwd;
        private string userStartPwd;
        private string userComment;
        private string salaryLogonEnabled;
        private string ipControlFlag;
        private string adminPwd;
        private string empType;
        private int num;
        private Nullable<DateTime> modifyDate;
        private string updateUser;
        private Nullable<DateTime> updateDate;
        private string createUser;
        private Nullable<DateTime> createDate;


        #region 工號
        /// <summary>
        /// 工號
        /// </summary>
        [Column("personcode", IsPrimaryKey = true)]
        public string Personcode
        {
            get { return personcode; }
            set { personcode = value; }
        }
        #endregion
        #region
        /// <summary>
        /// 
        /// </summary>
        [Column("TITLENAME")]
        public string TitleName
        {
            get { return titleName; }
            set { titleName = value; }
        }
        #endregion
        #region 中文名稱
        /// <summary>
        /// 中文名稱
        /// </summary>
        [Column("cname")]
        public string Cname
        {
            get { return cname; }
            set { cname = value; }
        }
        #endregion
        #region
        /// <summary>
        /// 
        /// </summary>
        [Column("Appellation")]
        public string Appellation
        {
            get { return appellation; }
            set { appellation = value; }
        }
        #endregion
        #region 英文名稱
        /// <summary>
        /// 英文名稱
        /// </summary>
        [Column("Ename")]
        public string Ename
        {
            get { return ename; }
            set { ename = value; }
        }
        #endregion
        #region 公司代碼
        /// <summary>
        /// 模組代碼
        /// </summary>
        [Column("CompanyId")]
        public string CompanyId
        {
            get { return companyId; }
            set { companyId = value; }
        }
        #endregion

        #region 公司名稱
        /// <summary>
        /// 公司名稱
        /// </summary>
        [Column("CompanyName")]
        public string CompanyName
        {
            get { return companyName; }
            set { companyName = value; }
        }
        #endregion
        #region 部門代碼
        /// <summary>
        /// 部門代碼
        /// </summary>
        [Column("DepCode")]
        public string DepCode
        {
            get { return depCode; }
            set { depCode = value; }
        }
        #endregion
        #region 部門名稱
        /// <summary>
        /// 部門名稱
        /// </summary>
        [Column("DepName")]
        public string DepName
        {
            get { return depName; }
            set { depName = value; }
        }
        #endregion

        #region
        /// <summary>
        /// 
        /// </summary>
        [Column("SexId")]
        public string SexId
        {
            get { return sexId; }
            set { sexId = value; }
        }
        #endregion
        #region 群組代碼
        /// <summary>
        /// 群組代碼
        /// </summary>
        [Column("RoleCode")]
        public string RoleCode
        {
            get { return roleCode; }
            set { roleCode = value; }
        }
        #endregion
        #region 群組名稱
        /// <summary>
        /// 群組名稱
        /// </summary>
        [Column("RolesName")]
        public string RoleName
        {
            get { return roleName; }
            set { roleName = value; }
        }
        #endregion
        #region 是否管理員
        /// <summary>
        /// 是否管理員
        /// </summary>
        [Column("IfAdmin")]
        public string IfAdmin
        {
            get { return ifAdmin; }
            set { ifAdmin = value; }
        }
        #endregion
        #region 電話
        /// <summary>
        /// 電話
        /// </summary>
        [Column("Tel")]
        public string Tel
        {
            get { return tel; }
            set { tel = value; }
        }
        #endregion
        #region 手機
        /// <summary>
        /// 手機
        /// </summary>
        [Column("Mobile")]
        public string Mobile
        {
            get { return mobile; }
            set { mobile = value; }
        }
        #endregion
        #region 郵箱
        /// <summary>
        /// 郵箱
        /// </summary>
        [Column("Mail")]
        public string Mail
        {
            get { return mail; }
            set { mail = value; }
        }
        #endregion
        #region 密碼
        /// <summary>
        /// 密碼
        /// </summary>
        [Column("Passwd")]
        public string Passwd
        {
            get { return passwd; }
            set { passwd = value; }
        }
        #endregion
        #region 分機
        /// <summary>
        /// 分機
        /// </summary>
        [Column("Extension")]
        public string Extension
        {
            get { return extension; }
            set { extension = value; }
        }
        #endregion
        #region 語言
        /// <summary>
        /// 語言
        /// </summary>
        [Column("Language")]
        public string Language
        {
            get { return language; }
            set { language = value; }
        }
        #endregion
        #region 語言名稱
        /// <summary>
        /// 語言名稱
        /// </summary>
        [Column("LanguageName")]
        public string LanguageName
        {
            get { return languageName; }
            set { languageName = value; }
        }
        #endregion
        #region 傳真
        /// <summary>
        /// 傳真
        /// </summary>
        [Column("Fax")]
        public string Fax
        {
            get { return fax; }
            set { fax = value; }
        }
        #endregion
        #region 登錄次數
        /// <summary>
        /// 登錄次數
        /// </summary>
        [Column("LoginTimes")]
        public Nullable<int> LoginTimes
        {
            get { return loginTimes; }
            set { loginTimes = value; }
        }
        #endregion
        #region 登錄時間
        /// <summary>
        /// 登錄時間
        /// </summary>
        [Column("LoginTime")]
        public Nullable<DateTime> LoginTime
        {
            get { return loginTime; }
            set { loginTime = value; }
        }
        #endregion
        #region 日期格式
        /// <summary>
        /// 日期格式
        /// </summary>
        [Column("DateFormat")]
        public string DateFormat
        {
            get { return dateFormat; }
            set { dateFormat = value; }
        }
        #endregion
        #region 日期時間格式
        /// <summary>
        /// 日期時間格式
        /// </summary>
        [Column("DateTimeformat")]
        public string DateTimeformat
        {
            get { return dateTimeformat; }
            set { dateTimeformat = value; }
        }
        #endregion
        #region 數字格式
        /// <summary>
        /// 數字格式
        /// </summary>
        [Column("NumberFormat")]
        public string NumberFormat
        {
            get { return numberFormat; }
            set { numberFormat = value; }
        }
        #endregion
        #region 總匯格式
        /// <summary>
        /// 總匯格式
        /// </summary>
        [Column("AmountFormat")]
        public string AmountFormat
        {
            get { return amountFormat; }
            set { amountFormat = value; }
        }
        #endregion
        #region 分隔符
        /// <summary>
        /// 分隔符
        /// </summary>
        [Column("DecimalSeparator")]
        public string DecimalSeparator
        {
            get { return decimalSeparator; }
            set { decimalSeparator = value; }
        }
        #endregion
        #region 色系
        /// <summary>
        /// 色系
        /// </summary>
        [Column("ColorsCheme")]
        public string ColorsCheme
        {
            get { return colorsCheme; }
            set { colorsCheme = value; }
        }
        #endregion
        #region 群組分隔符
        /// <summary>
        /// 群組分隔符
        /// </summary>
        [Column("GroupSeparator")]
        public string GroupSeparator
        {
            get { return groupSeparator; }
            set { groupSeparator = value; }
        }
        #endregion
        #region
        /// <summary>
        /// 
        /// </summary>
        [Column("MainBkcolorred")]
        public string MainBkcolorred
        {
            get { return mainBkcolorred; }
            set { mainBkcolorred = value; }
        }
        #endregion
        #region
        /// <summary>
        /// 
        /// </summary>
        [Column("MainBkcolorgreen")]
        public string MainBkcolorgreen
        {
            get { return mainBkcolorgreen; }
            set { mainBkcolorgreen = value; }
        }
        #endregion
        #region
        /// <summary>
        /// 
        /// </summary>
        [Column("MainBkcolorblue")]
        public string MainBkcolorblue
        {
            get { return mainBkcolorblue; }
            set { mainBkcolorblue = value; }
        }
        #endregion
        #region
        /// <summary>
        /// 
        /// </summary>
        [Column("TreeMenubkcolorred")]
        public string TreeMenubkcolorred
        {
            get { return treeMenubkcolorred; }
            set { treeMenubkcolorred = value; }
        }
        #endregion
        #region
        /// <summary>
        /// 
        /// </summary>
        [Column("TreeMenubkcolorgreen")]
        public string TreeMenubkcolorgreen
        {
            get { return treeMenubkcolorgreen; }
            set { treeMenubkcolorgreen = value; }
        }
        #endregion
        #region
        /// <summary>
        /// 
        /// </summary>
        [Column("TreeMenubkcolorblue")]
        public string TreeMenubkcolorblue
        {
            get { return treeMenubkcolorblue; }
            set { treeMenubkcolorblue = value; }
        }
        #endregion
        #region
        /// <summary>
        /// 
        /// </summary>
        [Column("WindowBkcolorred")]
        public string WindowBkcolorred
        {
            get { return windowBkcolorred; }
            set { windowBkcolorred = value; }
        }
        #endregion
        #region
        /// <summary>
        /// 
        /// </summary>
        [Column("WindowBkcolorgreen")]
        public string WindowBkcolorgreen
        {
            get { return windowBkcolorgreen; }
            set { windowBkcolorgreen = value; }
        }
        #endregion
        #region
        /// <summary>
        /// 
        /// </summary>
        [Column("WindowBkcolorblue")]
        public string WindowBkcolorblue
        {
            get { return windowBkcolorblue; }
            set { windowBkcolorblue = value; }
        }
        #endregion
        #region
        /// <summary>
        /// 
        /// </summary>
        [Column("FontColorred")]
        public string FontColorred
        {
            get { return fontColorred; }
            set { fontColorred = value; }
        }
        #endregion
        #region
        /// <summary>
        /// 
        /// </summary>
        [Column("FontColorgreen")]
        public string FontColorgreen
        {
            get { return fontColorgreen; }
            set { fontColorgreen = value; }
        }
        #endregion
        #region
        /// <summary>
        /// 
        /// </summary>
        [Column("FontColorblue")]
        public string FontColorblue
        {
            get { return fontColorblue; }
            set { fontColorblue = value; }
        }
        #endregion
        #region
        /// <summary>
        /// 
        /// </summary>
        [Column("GridHeaderbkcolorred")]
        public string GridHeaderbkcolorred
        {
            get { return gridHeaderbkcolorred; }
            set { gridHeaderbkcolorred = value; }
        }
        #endregion
        #region
        /// <summary>
        /// 
        /// </summary>
        [Column("GridHeaderbkcolorgreen")]
        public string GridHeaderbkcolorgreen
        {
            get { return gridHeaderbkcolorgreen; }
            set { gridHeaderbkcolorgreen = value; }
        }
        #endregion
        #region
        /// <summary>
        /// 
        /// </summary>
        [Column("GridHeaderbkcolorblue")]
        public string GridHeaderbkcolorblue
        {
            get { return gridHeaderbkcolorblue; }
            set { gridHeaderbkcolorblue = value; }
        }
        #endregion
        #region
        /// <summary>
        /// 
        /// </summary>
        [Column("GridBkcolorred")]
        public string GridBkcolorred
        {
            get { return gridBkcolorred; }
            set { gridBkcolorred = value; }
        }
        #endregion
        #region
        /// <summary>
        /// 
        /// </summary>
        [Column("GridBkcolorgreen")]
        public string GridBkcolorgreen
        {
            get { return gridBkcolorgreen; }
            set { gridBkcolorgreen = value; }
        }
        #endregion
        #region
        /// <summary>
        /// 
        /// </summary>
        [Column("GridBkcolorblue")]
        public string GridBkcolorblue
        {
            get { return gridBkcolorblue; }
            set { gridBkcolorblue = value; }
        }
        #endregion
        #region 是否有效
        /// <summary>
        /// 是否有效
        /// </summary>
        [Column("Deleted")]
        public string Deleted
        {
            get { return deleted; }
            set { deleted = value; }
        }
        #endregion
        #region
        /// <summary>
        /// 
        /// </summary>
        [Column("DefaultRecords")]
        public string DefaultRecords
        {
            get { return defaultRecords; }
            set { defaultRecords = value; }
        }
        #endregion
        #region
        /// <summary>
        /// 
        /// </summary>
        [Column("GridAltbkcolorred")]
        public string GridAltbkcolorred
        {
            get { return gridAltbkcolorred; }
            set { gridAltbkcolorred = value; }
        }
        #endregion
        #region
        /// <summary>
        /// 
        /// </summary>
        [Column("SplitTercolorred")]
        public string SplitTercolorred
        {
            get { return splitTercolorred; }
            set { splitTercolorred = value; }
        }
        #endregion
        #region
        /// <summary>
        /// 
        /// </summary>
        [Column("GridAltbkcolorgreen")]
        public string GridAltbkcolorgreen
        {
            get { return gridAltbkcolorgreen; }
            set { gridAltbkcolorgreen = value; }
        }
        #endregion
        #region
        /// <summary>
        /// 
        /// </summary>
        [Column("SplitTercolorgreen")]
        public string SplitTercolorgreen
        {
            get { return splitTercolorgreen; }
            set { splitTercolorgreen = value; }
        }
        #endregion
        #region
        /// <summary>
        /// 
        /// </summary>
        [Column("GridAltbkcolorblue")]
        public string GridAltbkcolorblue
        {
            get { return gridAltbkcolorblue; }
            set { gridAltbkcolorblue = value; }
        }
        #endregion
        #region
        /// <summary>
        /// 
        /// </summary>
        [Column("SplitTercolorblue")]
        public string SplitTercolorblue
        {
            get { return splitTercolorblue; }
            set { splitTercolorblue = value; }
        }
        #endregion
        #region
        /// <summary>
        /// 
        /// </summary>
        [Column("IfonLine")]
        public string IfonLine
        {
            get { return ifonLine; }
            set { ifonLine = value; }
        }
        #endregion
        #region
        /// <summary>
        /// 
        /// </summary>
        [Column("MdtColorred")]
        public string MdtColorred
        {
            get { return mdtColorred; }
            set { mdtColorred = value; }
        }
        #endregion
        #region
        /// <summary>
        /// 
        /// </summary>
        [Column("IpAddress")]
        public string IpAddress
        {
            get { return ipAddress; }
            set { ipAddress = value; }
        }
        #endregion
        #region
        /// <summary>
        /// 
        /// </summary>
        [Column("MdtColorgreen")]
        public string MdtColorgreen
        {
            get { return mdtColorgreen; }
            set { mdtColorgreen = value; }
        }
        #endregion
        #region
        /// <summary>
        /// 
        /// </summary>
        [Column("DefaultModule")]
        public string DefaultModule
        {
            get { return defaultModule; }
            set { defaultModule = value; }
        }
        #endregion
        #region
        /// <summary>
        /// 
        /// </summary>
        [Column("HostName")]
        public string HostName
        {
            get { return hostName; }
            set { hostName = value; }
        }
        #endregion
        #region
        /// <summary>
        /// 
        /// </summary>
        [Column("MdtColorblue")]
        public string MdtColorblue
        {
            get { return mdtColorblue; }
            set { mdtColorblue = value; }
        }
        #endregion
        #region
        /// <summary>
        /// 
        /// </summary>
        [Column("MdtChar")]
        public string MdtChar
        {
            get { return mdtChar; }
            set { mdtChar = value; }
        }
        #endregion
        #region
        /// <summary>
        /// 
        /// </summary>
        [Column("DefaultMenu")]
        public string DefaultMenu
        {
            get { return defaultMenu; }
            set { defaultMenu = value; }
        }
        #endregion
        #region
        /// <summary>
        /// 
        /// </summary>
        [Column("AreaCode")]
        public string AreaCode
        {
            get { return areaCode; }
            set { areaCode = value; }
        }
        #endregion
        #region
        /// <summary>
        /// 層級
        /// </summary>
        [Column("DepLevel")]
        public string DepLevel
        {
            get { return depLevel; }
            set { depLevel = value; }
        }
        #endregion
        #region 層級名稱
        /// <summary>
        /// 層級名稱
        /// </summary>
        [Column("LevelName")]
        public string LevelName
        {
            get { return levelName; }
            set { levelName = value; }
        }
        #endregion
        //#region
        ///// <summary>
        ///// 
        ///// </summary>
        //[Column("ModifyDate")]
        //public Nullable<DateTime> ModifyDate
        //{
        //    get { return modifyDate; }
        //    set { modifyDate = value; }
        //}
        //#endregion

        #region
        public int Num
        {
            get { return num; }
            set { num = value; }
        }
        #endregion

        #region
        /// <summary>
        /// 
        /// </summary>
        [Column("Update_User")]
        public string UpdateUser
        {
            get { return updateUser; }
            set { updateUser = value; }
        }
        #endregion
        #region
        /// <summary>
        /// 
        /// </summary>
        [Column("Update_Date")]
        public Nullable<DateTime> UpdateDate
        {
            get { return updateDate; }
            set { updateDate = value; }
        }
        #endregion
        #region
        /// <summary>
        /// 
        /// </summary>
        [Column("Create_User")]
        public string CreateUser
        {
            get { return createUser; }
            set { createUser = value; }
        }
        #endregion
        #region
        /// <summary>
        /// 
        /// </summary>
        [Column("Create_Date")]
        public Nullable<DateTime> CreateDate
        {
            get { return createDate; }
            set { createDate = value; }
        }
        #endregion

        #region 與人事系統整合后新增的整合系統內的欄位

        [Column("Emp_No")]
        public string EmpNo
        {
            get { return empNo; }
            set { empNo = value; }
        }


        [Column("USER_CHANGE_PWD")]
        public string UserChangePwd
        {
            get { return userChangePwd; }
            set { userChangePwd = value; }
        }

        [Column("USER_START_PWD")]
        public string UserStartPwd
        {
            get { return userStartPwd; }
            set { userStartPwd = value; }
        }

        [Column("USER_COMMENT")]
        public string UserComment
        {
            get { return userComment; }
            set { userComment = value; }
        }

        [Column("SALARY_LOGON_ENABLED")]
        public string SalaryLogonEnabled
        {
            get { return salaryLogonEnabled; }
            set { salaryLogonEnabled = value; }
        }

        [Column("IP_CONTROL_FLAG")]
        public string IpControlFlag
        {
            get { return ipControlFlag; }
            set { ipControlFlag = value; }
        }


        [Column("ADMIN_PASSWORD")]
        public string AdminPwd
        {
            get { return adminPwd; }
            set { adminPwd = value; }
        }

        [Column("EMP_TYPE")]
        public string EmpType
        {
            get { return empType; }
            set { empType = value; }
        }

        #endregion
    }
}
