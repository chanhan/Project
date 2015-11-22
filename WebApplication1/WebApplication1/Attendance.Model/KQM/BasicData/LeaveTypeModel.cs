/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： LeaveTypeModel.cs
 * 檔功能描述：請假類別定義實體類
 * 
 * 版本：1.0
 * 創建標識： 陈函 2011.12.13
 * 
 */
using System;
using GDSBG.MiABU.Attendance.Common.Attributes;

namespace GDSBG.MiABU.Attendance.Model.KQM.BasicData
{
    /// <summary>
    /// 請假類別定義實體類
    /// </summary>
    [Serializable, TableName("gds_att_leavetype", SelectTable = "gds_att_leavetype_v")]
    public class LeaveTypeModel : ModelBase
    {
        private string lvtypecode;
        private string lvtypename;
        private string usestate;
        private string limitdays;
        private string hasmoney;
        private string prove;
        private string remark;
        private string effectflag;
        private string modifier;
        private Nullable<DateTime> modifydate;
        private string iscludeholiday;
        private string istestify;
        private string isallowpcm;
        private string minhours;
        private string standardhours;
        private string fitsex;
        private string allowdeplevel;
        private string billtypecode;
        private string fitsexname;

        #region 請假類別代碼
        /// <summary>
        /// 請假類別代碼
        /// </summary>


        [Column("lvtypecode",IsPrimaryKey=true)]
        public string LvTypeCode
        {
            get { return lvtypecode; }
            set { lvtypecode = value; }
        }
        #endregion

        #region 請假類別名稱
        /// <summary>
        /// 請假類別名稱
        /// </summary>


        [Column("lvtypename")]
        public string LvTypeName
        {
            get { return lvtypename; }
            set { lvtypename = value; }
        }
        #endregion

        #region 使用範圍的描述
        /// <summary>
        /// 使用範圍的描述
        /// </summary>


        [Column("usestate")]
        public string UseState
        {
            get { return usestate; }
            set { usestate = value; }
        }
        #endregion

        #region 限制天數(天/年)
        /// <summary>
        /// 限制天數(天/年)
        /// </summary>


        [Column("limitdays")]
        public string LimitDays
        {
            get { return limitdays; }
            set { limitdays = value; }
        }
        #endregion

        #region 薪資給支(是否有薪)
        /// <summary>
        /// 薪資給支(是否有薪)
        /// </summary>


        [Column("hasmoney")]
        public string HasMoney
        {
            get { return hasmoney; }
            set { hasmoney = value; }
        }
        #endregion

        #region 需要證件證明的描述
        /// <summary>
        /// 需要證件證明的描述
        /// </summary>


        [Column("prove")]
        public string Prove
        {
            get { return prove; }
            set { prove = value; }
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
        #region 維護人
        /// <summary>
        /// 維護人
        /// </summary>


        [Column("modifier")]
        public string Modifier
        {
            get { return modifier; }
            set { modifier = value; }
        }
        #endregion

        #region 維護日期
        /// <summary>
        /// 維護日期
        /// </summary>


        [Column("modifydate")]
        public Nullable<DateTime> ModifyDate
        {
            get { return modifydate; }
            set { modifydate = value; }
        }
        #endregion

        #region 是否含節假日
        /// <summary>
        /// 是否含節假日
        /// </summary>


        [Column("iscludeholiday")]
        public string IscludeHoliday
        {
            get { return iscludeholiday; }
            set { iscludeholiday = value; }
        }
        #endregion

        #region 是否允許個人申請
        /// <summary>
        /// 是否允許個人申請
        /// </summary>


        [Column("isallowpcm")]
        public string IsAllowPCM
        {
            get { return isallowpcm; }
            set { isallowpcm = value; }
        }
        #endregion

        #region 最小時數
        /// <summary>
        /// 最小時數
        /// </summary>


        [Column("minhours")]
        public string MinHours
        {
            get { return minhours; }
            set { minhours = value; }
        }
        #endregion

        #region 標準時數
        /// <summary>
        /// 標準時數
        /// </summary>


        [Column("standardhours")]
        public string StandardHours
        {
            get { return standardhours; }
            set { standardhours = value; }
        }
        #endregion

        #region 適用性別
        /// <summary>
        /// 適用性別
        /// </summary>


        [Column("fitsex")]
        public string FitSex
        {
            get { return fitsex; }
            set { fitsex = value; }
        }
        #endregion

        #region 是否有證明
        /// <summary>
        /// 是否有證明
        /// </summary>


        [Column("istestify")]
        public string Istesify
        {
            get { return istestify; }
            set { istestify = value; }
        }
        #endregion

        #region 
        /// <summary>
        /// 
        /// </summary>


        [Column("allowdeplevel")]
        public string AllowdepLevel
        {
            get { return allowdeplevel; }
            set { allowdeplevel = value; }
        }
        #endregion

        #region 
        /// <summary>
        /// 
        /// </summary>


        [Column("billtypecode")]
        public string BilltypeCode
        {
            get { return billtypecode; }
            set { billtypecode = value; }
        }
        #endregion

        #region 適用性別名稱
        /// <summary>
        /// 適用性別名稱
        /// </summary>


        [Column("fitsexname",OnlySelect=true)]
        public string FitSexName
        {
            get { return fitsexname; }
            set { fitsexname = value; }
        }
        #endregion


    }
}
