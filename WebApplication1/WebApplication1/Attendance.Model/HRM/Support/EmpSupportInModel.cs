/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： EmpSupportInModel.cs
 * 檔功能描述： 內部支援資料實體類
 * 
 * 版本：1.0
 * 創建標識： 顧陳偉 2012.01.06
 * 
 */

using System;
using GDSBG.MiABU.Attendance.Common.Attributes;
namespace GDSBG.MiABU.Attendance.Model.HRM.Support
{
    [Serializable, TableName("gds_att_empsupportin", SelectTable = "gds_att_empsupportin_v")]
    public class EmpSupportInModel : ModelBase
    {
        private string workno;
        private string supportorder;
        private string supportdept;
        private Nullable<DateTime> startdate;
        private Nullable<DateTime> prependdate;
        private Nullable<DateTime> enddate;
        private string remark;
        private string state;
        private string statename;
        private string sexname;
        private string identityno;
        private string supportdeptname;
        private string levelcode;
        private string levelname;
        private string managercode;
        private string managername;
        private string technicalname;
        private string localname;
        private string sex;
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

        #region 支援單位代碼
        /// <summary>
        /// 支援單位代碼
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

        #region 狀態
        /// <summary>
        /// 狀態
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

        #region 身份證號
        /// <summary>
        /// 身份證號
        /// </summary>
        [Column("identityno",OnlySelect=true)]
        public string IdentityNo
        {
            get { return identityno; }
            set { identityno = value; }
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

        #region 資位代碼
        /// <summary>
        /// 資位代碼
        /// </summary>
        [Column("levelcode", OnlySelect = true)]
        public string LevelCode
        {
            get { return levelcode; }
            set { levelcode = value; }
        }
        #endregion

        #region 資位
        /// <summary>
        /// 資位
        /// </summary>
        [Column("levelname",OnlySelect=true)]
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
        [Column("managercode", OnlySelect = true)]
        public string ManagerCode
        {
            get { return managercode; }
            set { managercode = value; }
        }
        #endregion

        #region 管理職
        /// <summary>
        /// 管理職
        /// </summary>
        [Column("managername",OnlySelect=true)]
        public string ManagerName
        {
            get { return managername; }
            set { managername = value; }
        }
        #endregion

        #region 職系
        /// <summary>
        /// 職系
        /// </summary>
        [Column("technicalname", OnlySelect = true)]
        public string TechnicalName
        {
            get { return technicalname; }
            set { technicalname = value; }
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

        #region 性別代碼
        /// <summary>
        /// 性別代碼
        /// </summary>
        [Column("sex", OnlySelect = true)]
        public string Sex
        {
            get { return sex; }
            set { sex = value; }
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
