/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： SynclogModel.cs
 * 檔功能描述： 日誌管理實體類
 * 
 * 版本：1.0
 * 創建標識： 高子焱 2011.12.02
 * 
 */
using System;
using GDSBG.MiABU.Attendance.Common.Attributes;

namespace GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety
{
    /// <summary>
    /// 日誌管理實體類
    /// </summary>
    [Serializable, TableName(" gds_sc_personlevel")]
    public class SynclogModel:ModelBase
    {
        private string id;
        private string transactionType;
        private string levelNo;
        private string fromHost;
        private string toHost;
        private string docNo;
        private string text;
        private string logTime;
        private string processFlag;
        private string processOwner;
        #region 序號
        /// <summary>
        /// 序號
        /// </summary>
        [Column("id")]
        public string Id
        {
            get { return id; }
            set { id = value; }
        }
        #endregion
        #region 單據類型
        /// <summary>
        /// 單據類型
        /// </summary>
        [Column("transactiontype")]
        public string TransactionType
        {
            get { return transactionType; }
            set { transactionType = value; }
        }
        #endregion
        #region 層級編號？
        /// <summary>
        /// 層級編號
        /// </summary>
        [Column("levelno")]
        public string LevelNo
        {
            get { return levelNo; }
            set { levelNo = value; }
        }
        #endregion
        #region 來源server
        /// <summary>
        /// 來源server
        /// </summary>
        [Column("fromhost")]
        public string FromHost
        {
            get { return fromHost; }
            set { fromHost = value; }
        }
        #endregion
        #region 目的server
        /// <summary>
        /// 目的server
        /// </summary>
        [Column("tohost")]
        public string ToHost
        {
            get { return toHost; }
            set { toHost = value; }
        }
        #endregion
        #region 單據編號
        /// <summary>
        /// 單據編號
        /// </summary>
        [Column("docno")]
        public string DocNo
        {
            get { return docNo; }
            set { docNo = value; }
        }
        #endregion
        #region 描述說明
        /// <summary>
        /// 描述說明
        /// </summary>
        [Column("text")]
        public string Text
        {
            get { return text; }
            set { text = value; }
        }
        #endregion
        #region 處理時間
        /// <summary>
        /// 處理時間
        /// </summary>
        [Column("logTime")]
        public string LogTime
        {
            get { return logTime; }
            set { logTime = value; }
        }
        #endregion
        #region 處理狀態
        /// <summary>
        /// 處理狀態
        /// </summary>
        [Column("processflag")]
        public string ProcessFlag
        {
            get { return processFlag; }
            set { processFlag = value; }
        }
        #endregion
        #region 處理人員
        /// <summary>
        /// 處理人員
        /// </summary>
        [Column("processowner")]
        public string ProcessOwner
        {
            get { return processOwner; }
            set { processOwner = value; }
        }
        #endregion
    }
}
