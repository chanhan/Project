/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： BellCardDataModel.cs
 * 檔功能描述： 原始刷卡數據實體類
 * 
 * 版本：1.0
 * 創建標識： 顧陳偉 2011.12.26
 * 
 */
using System;
using System.Collections.Generic;
using System.Text;
using GDSBG.MiABU.Attendance.Common.Attributes;

namespace GDSBG.MiABU.Attendance.Model.KQM.Query
{
    [Serializable, TableName("gds_att_bellcarddata", SelectTable = "gds_att_bellcarddata_v")]
    public class BellCardDataModel : ModelBase
    {
        private string cardno;
        private Nullable<DateTime> cardtime;
        private string bellno;
        private Nullable<DateTime> readtime;
        private string processflag;
        private string workno;
        private string localname;
        private string cardtimemm;
        private string depname;
        private string depcode;
        private string address;

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

        #region 刷卡時間
        /// <summary>
        /// 刷卡時間
        /// </summary>
        [Column("cardtime")]
        public Nullable<DateTime> CardTime
        {
            get { return cardtime; }
            set { cardtime = value; }
        }
        #endregion

        #region 卡機號
        /// <summary>
        /// 卡機號
        /// </summary>

        [Column("bellno")]
        public string BellNo
        {
            get { return bellno; }
            set { bellno = value; }
        }
        #endregion

        #region 采集時間
        /// <summary>
        /// 采集時間
        /// </summary>

        [Column("readtime")]
        public Nullable<DateTime> ReadTime
        {
            get { return readtime; }
            set { readtime = value; }
        }
        #endregion

        #region 是否有效卡
        /// <summary>
        /// 是否有效卡
        /// </summary>
        [Column("processflag")]
        public string ProcessFlag
        {
            get { return processflag; }
            set { processflag = value; }
        }
        #endregion

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

        [Column("localname", OnlySelect = true)]
        public string LocalName
        {
            get { return localname; }
            set { localname = value; }
        }
        #endregion

        #region 時間
        /// <summary>
        /// 時間
        /// </summary>
        [Column("cardtimemm",OnlySelect=true)]
        public string CardTimeMM
        {
            get { return cardtimemm; }
            set { cardtimemm = value; }
        }
        #endregion

        #region 部門名稱
        /// <summary>
        /// 部門名稱
        /// </summary>
        [Column("depname",OnlySelect=true)]
        public string DepName
        {
            get { return depname; }
            set { depname = value; }
        }
        #endregion

        #region 部門CODE
        /// <summary>
        /// 部門CODE
        /// </summary>
        [Column("depcode",OnlySelect=true)]
        public string DepCode
        {
            get { return depcode; }
            set { depcode = value; }
        }
        #endregion

        #region 地址
        /// <summary>
        /// 地址
        /// </summary>
        [Column("address",OnlySelect=true)]
        public string AddRess
        {
            get { return address; }
            set { address = value; }
        }
        #endregion
    }
}
