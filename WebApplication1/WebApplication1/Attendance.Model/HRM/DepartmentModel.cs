/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： DepartmentModel.cs
 * 檔功能描述： 組織資料實體類
 * 
 * 版本：1.0
 * 創建標識： 顧陳偉 2012.01.03
 * 
 */

using System;
using GDSBG.MiABU.Attendance.Common.Attributes;

namespace GDSBG.MiABU.Attendance.Model.HRM
{
    [Serializable, TableName("gds_sc_department", SelectTable = "gds_att_department_v")]
    public class DepartmentModel : ModelBase
    {
        private string companyid;
        private string depcode;
        private string depname;
        private string costcode;
        private string deleted;
        private string leadercode;
        private string assistantcode;
        private string parentdepcode;
        private string levelcode;
        private string orderid;
        private string corporationid;
        private string dephead;
        private string departno;
        private string areacode;
        private string accountentity;
        private Nullable<DateTime> createdate;
        private string createuser;
        private Nullable<DateTime> updatedate;
        private string updateuser;
        private string depname1;
        private string levelname;
        private string parentdepname;
        private string corporationname;
        private string areaname;
        private string siteid;
        private string depshortname;
        private string factorycode;
        private string depalias;

        #region 公司ID
        /// <summary>
        /// 公司ID
        /// </summary>
        [Column("companyid", IsPrimaryKey = true)]
        public string CompanyId
        {
            get { return companyid; }
            set { companyid = value; }
        }
        #endregion

        #region 組織代碼
        /// <summary>
        /// 組織代碼
        /// </summary>
        [Column("depcode", IsPrimaryKey = true)]
        public string DepCode
        {
            get { return depcode; }
            set { depcode = value; }
        }
        #endregion

        #region 組織名稱
        /// <summary>
        /// 組織名稱
        /// </summary>
        [Column("depname")]
        public string DepName
        {
            get { return depname; }
            set { depname = value; }
        }
        #endregion

        #region 費用代碼
        /// <summary>
        /// 費用代碼
        /// </summary>
        [Column("costcode")]
        public string CostCode
        {
            get { return costcode; }
            set { costcode = value; }
        }
        #endregion

        #region 是否失效
        /// <summary>
        /// 是否失效
        /// </summary>
        [Column("deleted")]
        public string Deleted
        {
            get { return deleted; }
            set { deleted = value; }
        }
        #endregion

        #region LeaderCode
        /// <summary>
        /// LeaderCode
        /// </summary>
        [Column("leadercode")]
        public string LeaderCode
        {
            get { return leadercode; }
            set { leadercode = value; }
        }
        #endregion

        #region AssistantCode
        /// <summary>
        /// AssistantCode
        /// </summary>
        [Column("assistantcode")]
        public string AssistantCode
        {
            get { return assistantcode; }
            set { assistantcode = value; }
        }
        #endregion

        #region 父階組織CODE
        /// <summary>
        /// 父階組織CODE
        /// </summary>
        [Column("parentdepcode")]
        public string ParentDepCode
        {
            get { return parentdepcode; }
            set { parentdepcode = value; }
        }
        #endregion

        #region 組織層級CODE
        /// <summary>
        /// 組織層級CODE
        /// </summary>
        [Column("levelcode")]
        public string LevelCode
        {
            get { return levelcode; }
            set { levelcode = value; }
        }
        #endregion

        #region 排列循序
        /// <summary>
        /// 排列循序
        /// </summary>
        [Column("orderid")]
        public string OrderId
        {
            get { return orderid; }
            set { orderid = value; }
        }
        #endregion

        #region 與SIDC人事系統對應
        /// <summary>
        /// 與SIDC人事系統對應
        /// </summary>
        [Column("corporationid")]
        public string CorporationId
        {
            get { return corporationid; }
            set { corporationid = value; }
        }
        #endregion

        #region DepHead
        /// <summary>
        /// DepHead
        /// </summary>
        [Column("dephead")]
        public string DepHead
        {
            get { return dephead; }
            set { dephead = value; }
        }
        #endregion

        #region DepartNo
        /// <summary>
        /// DepartNo
        /// </summary>
        [Column("depart_no")]
        public string DepartNo
        {
            get { return departno; }
            set { departno = value; }
        }
        #endregion

        #region AreaCode
        /// <summary>
        /// AreaCode
        /// </summary>
        [Column("areacode")]
        public string AreaCode
        {
            get { return areacode; }
            set { areacode = value; }
        }
        #endregion

        #region AccountEntity
        /// <summary>
        /// AccountEntity
        /// </summary>
        [Column("accountentity")]
        public string AccountEntity
        {
            get { return accountentity; }
            set { accountentity = value; }
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

        #region 層級顯示組織名稱
        [Column("depname1",OnlySelect=true)]
        public string DepName1
        {
            get { return depname1; }
            set { depname1 = value; }
        }
        #endregion

        #region 層級名稱
        [Column("levelname",OnlySelect=true)]
        public string LevelName
        {
            get { return levelname; }
            set { levelname = value; }
        }
        #endregion

        #region 父組織名稱
        [Column("parentdepname",OnlySelect=true)]
        public string ParentDepName
        {
            get { return parentdepname; }
            set { parentdepname = value; }
        }
        #endregion

        #region 法人名稱
        [Column("corporationname",OnlySelect=true)]
        public string CorporationName
        {
            get { return corporationname; }
            set { corporationname = value; }
        }
        #endregion

        #region 區域名稱
        [Column("areaname",OnlySelect=true)]
        public string AreaName
        {
            get { return areaname; }
            set { areaname = value; }
        }
        #endregion

        #region siteid
        [Column("siteid",OnlySelect=true)]
        public string SiteId
        {
            get { return siteid; }
            set { siteid = value; }
        }
        #endregion

        #region 組織簡稱
        [Column("dept_short_name")]
        public string DepShortName
        {
            get { return depshortname; }
            set { depshortname = value; }
        }
        #endregion

        #region 所屬廠區
        [Column("factory_code")]
        public string FactoryCode
        {
            get { return factorycode; }
            set { factorycode = value; }
        }
        #endregion

        #region 組織別名
        [Column("dept_alias")]
        public string DepAlias
        {
            get { return depalias; }
            set { depalias = value; }
        }
        #endregion
    }
}
