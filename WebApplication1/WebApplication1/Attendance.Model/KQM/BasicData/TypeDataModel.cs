/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： TypeDataModel.cs
 * 檔功能描述： 固定參數表實體類
 * 
 * 版本：1.0
 * 創建標識： 高子焱 2011.12.06
 * 
 */
using System;
using GDSBG.MiABU.Attendance.Common.Attributes;

namespace GDSBG.MiABU.Attendance.Model.KQM.BasicData
{
    /// <summary>
    /// 班別定義實體類
    /// </summary>
    [Serializable, TableName("gds_att_typedata")]
    public class TypeDataModel : ModelBase
    {
        private string dataType;
        private string dataCode;
        private string dataValue;
        private string orderId;
        private string dataTypeDetail;
        private string language;
        private string createUser;
        private string createDate;
        private string updateUser;
        private string updateDate;

        #region 參數類別
        /// <summary>
        /// 參數類別
        /// </summary>
        [Column("datatype")]
        public string DataType
        {
            get { return dataType; }
            set { dataType = value; }
        }
        #endregion
        #region 參數代碼
        /// <summary>
        /// 參數代碼
        /// </summary>
        [Column("datacode")]
        public string DataCode
        {
            get { return dataCode; }
            set { dataCode = value; }
        }
        #endregion
        #region 參數描述
        /// <summary>
        /// 參數描述
        /// </summary>
        [Column("datavalue")]
        public string DataValue
        {
            get { return dataValue; }
            set { dataValue = value; }
        }
        #endregion
        #region 排列順序
        /// <summary>
        /// 排列順序
        /// </summary>
        [Column("orderid")]
        public string OrderId
        {
            get { return orderId; }
            set { orderId = value; }
        }
        #endregion
        #region 參數類別細節
        /// <summary>
        /// 參數類別細節
        /// </summary>
        [Column("datatypedetail")]
        public string DataTypeDetail
        {
            get { return dataTypeDetail; }
            set { dataTypeDetail = value; }
        }
        #endregion
        #region 語言
        /// <summary>
        /// 語言
        /// </summary>
        [Column("language")]
        public string Language
        {
            get { return language; }
            set { language = value; }
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
        ///創建日期
        /// </summary>
        [Column("create_date")]
        public string CreateDate
        {
            get { return createDate; }
            set { createDate = value; }
        }
        #endregion
        #region 更新日期
        /// <summary>
        /// 更新日期
        /// </summary>
        [Column("update_date")]
        public string UpdateDate
        {
            get { return updateDate; }
            set { updateDate = value; }
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

    }
}
