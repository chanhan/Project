/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： AuthorityModel.cs
 * 檔功能描述： 加班類別定義實體類
 * 
 * 版本：1.0
 * 創建標識： 陈函 2011.12.10
 * 
 */
using System;
using System.Collections.Generic;
using System.Text;
using GDSBG.MiABU.Attendance.Common.Attributes;

namespace GDSBG.MiABU.Attendance.Model.KQM.BasicData
{
    /// <summary>
    /// 加班類別定義實體類
    /// </summary>
    [Serializable, TableName("gds_att_type", SelectTable = "gds_att_type_v")]
    public class AttTypeModel : ModelBase
    {
        private string orgcode;
        private string ottypecode;
        private string ottypedetail;
        private string g1dlimit;
        private string g2dlimit;
        private string g3dlimit;
        private string g1mlimit;
        private string g2mlimit;
        private string g12mlimit;
        private string remark;
        private string effectflag;
        private string isallowproject;
        private string create_user;
        private Nullable<DateTime> create_date;
        private Nullable<DateTime> update_date;
        private string g13mlimit;
        private string update_user;
        private string g123mlimit;
        private string depname;

        #region 單位代碼
        /// <summary>
        /// 單位代碼
        /// </summary>


        [Column("orgcode", IsPrimaryKey = true)]
        public string OrgCode
        {
            get { return orgcode; }
            set { orgcode = value; }
        }
        #endregion
        #region 加班類別
        /// <summary>
        /// 加班類別
        /// </summary>


        [Column("ottypecode", IsPrimaryKey = true)]
        public string OttypeCode
        {
            get { return ottypecode; }
            set { ottypecode = value; }
        }
        #endregion
        #region 類別定義
        /// <summary>
        /// 類別定義
        /// </summary>


        [Column("ottypedetail")]
        public string OttypeDetail
        {
            get { return ottypedetail; }
            set { ottypedetail = value; }
        }
        #endregion
        #region G1每日上限
        /// <summary>
        /// G1每日上限
        /// </summary>


        [Column("g1dlimit")]
        public string G1dLimit
        {
            get { return g1dlimit; }
            set { g1dlimit = value; }
        }
        #endregion
        #region G2每日上限
        /// <summary>
        /// G2每日上限
        /// </summary>


        [Column("g2dlimit")]
        public string G2dLimit
        {
            get { return g2dlimit; }
            set { g2dlimit = value; }
        }
        #endregion
        #region G3每日上限
        /// <summary>
        /// G3每日上限
        /// </summary>


        [Column("g3dlimit")]
        public string G3dLimit
        {
            get { return g3dlimit; }
            set { g3dlimit = value; }
        }
        #endregion
        #region G1每月上限
        /// <summary>
        /// G1每月上限
        /// </summary>


        [Column("g1mlimit")]
        public string G1mLimit
        {
            get { return g1mlimit; }
            set { g1mlimit = value; }
        }
        #endregion
        #region G2每月上限
        /// <summary>
        /// G2每月上限
        /// </summary>


        [Column("g2mlimit")]
        public string G2mLimit
        {
            get { return g2mlimit; }
            set { g2mlimit = value; }
        }
        #endregion
        #region G1+G2每月上限
        /// <summary>
        /// G1+G2每月上限
        /// </summary>


        [Column("g12mlimit")]
        public string G12mLimit
        {
            get { return g12mlimit; }
            set { g12mlimit = value; }
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
        #region 是否有效
        /// <summary>
        /// 是否有效
        /// </summary>


        [Column("effectflag")]
        public string EffectFlag
        {
            get { return effectflag; }
            set { effectflag = value; }
        }
        #endregion
        #region 是否允許專案申報
        /// <summary>
        /// 是否允許專案申報
        /// </summary>


        [Column("isallowproject")]
        public string IsAllowProject
        {
            get { return isallowproject; }
            set { isallowproject = value; }
        }
        #endregion
        #region 創建用戶
        /// <summary>
        /// 創建用戶
        /// </summary>

        [Column("create_user")]
        public string Create_User
        {
            get { return create_user; }
            set { create_user = value; }
        }
        #endregion
        #region 創建日期
        /// <summary>
        /// 創建日期
        /// </summary>


        [Column("create_date")]
        public Nullable<DateTime> Create_Date
        {
            get { return create_date; }
            set { create_date = value; }
        }
        #endregion
        #region 更新用戶
        /// <summary>
        /// 更新用戶
        /// </summary>

        [Column("update_user")]
        public string Update_User
        {
            get { return update_user; }
            set { update_user = value; }
        }
        #endregion
        #region 更新日期
        /// <summary>
        /// 更新日期
        /// </summary>

        [Column("update_date")]
        public Nullable<DateTime> Update_Date
        {
            get { return update_date; }
            set { update_date = value; }
        }
        #endregion

        #region G1+G3每月上限
        /// <summary>
        /// G1+G3每月上限
        /// </summary>


        [Column("g13mlimit")]
        public string G13mLimit
        {
            get { return g13mlimit; }
            set { g13mlimit = value; }
        }
        #endregion
        #region G1+G2+G3每月上限(含專案)
        /// <summary>
        /// G1+G2+G3每月上限(含專案)
        /// </summary>


        [Column("g123mlimit")]
        public string G123mLimit
        {
            get { return g123mlimit; }
            set { g123mlimit = value; }
        }
        #endregion

        
        #region 部門名稱
        /// <summary>
        /// 部門名稱
        /// </summary>

        [Column("depname",OnlySelect=true)]
        public string Depname
        {
            get { return depname; }
            set { depname = value; }
        }
        #endregion
    }
}
