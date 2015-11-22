/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： EvectionApplyVModel.cs
 * 檔功能描述：出差明細查詢實體
 * 
 * 版本：1.0
 * 創建標識： 陳函 2011.12.30
 * 
 */
using System;
using System.Collections.Generic;
using System.Text;
using GDSBG.MiABU.Attendance.Common.Attributes;

namespace GDSBG.MiABU.Attendance.Model.KQM.Query
{
    [Serializable, TableName("gds_att_evectionapply", SelectTable = "gds_att_evectionapply_v")]
    public class EvectionApplyVModel : ModelBase
    {
        private string id;

        #region ID
        /// <summary>
        /// ID
        /// </summary>
        [Column("ID")]
        public string ID
        {
            get { return id; }
            set { id = value; }
        }
        #endregion

        private string workno;


        #region 工號
        /// <summary>
        /// 工號
        /// </summary>
        [Column("workno")]
        public string Workno
        {
            get { return workno; }
            set { workno = value; }
        }
        #endregion

        private string evectiontype;

        #region 出差類型
        /// <summary>
        /// 出差類型
        /// </summary>
        [Column("evectiontype")]
        public string Evectiontype
        {
            get { return evectiontype; }
            set { evectiontype = value; }
        }
        #endregion

        private string evectionaddress;


        #region 出差地址
        /// <summary>
        /// 出差地址
        /// </summary>
        [Column("evectionaddress")]
        public string Evectionaddress
        {
            get { return evectionaddress; }
            set { evectionaddress = value; }
        }
        #endregion
        private Nullable<DateTime> startdate;


        #region 開始日期
        /// <summary>
        /// 開始日期
        /// </summary>
        [Column("startdate")]
        public Nullable<DateTime> Startdate
        {
            get { return startdate; }
            set { startdate = value; }
        }
        #endregion
        private Nullable<DateTime> enddate;


        #region 截止日期
        /// <summary>
        /// 截止日期
        /// </summary>
        [Column("enddate")]
        public Nullable<DateTime> Enddate
        {
            get { return enddate; }
            set { enddate = value; }
        }
        #endregion
        private string proxy;


        #region 代理
        /// <summary>
        /// 代理
        /// </summary>
        [Column("proxy")]
        public string Proxy
        {
            get { return proxy; }
            set { proxy = value; }
        }
        #endregion
        private string evectiontask;


        #region 出差任務
        /// <summary>
        /// 出差任務
        /// </summary>
        [Column("evectiontask")]
        public string Evectiontask
        {
            get { return evectiontask; }
            set { evectiontask = value; }
        }
        #endregion
        private string evectiondetail;


        #region 出差明細
        /// <summary>
        /// 出差明細
        /// </summary>
        [Column("evectiondetail")]
        public string Evectiondetail
        {
            get { return evectiondetail; }
            set { evectiondetail = value; }
        }
        #endregion
        private string remark;


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
        private string status;


        #region 狀態
        /// <summary>
        /// 狀態
        /// </summary>
        [Column("status")]
        public string Status
        {
            get { return status; }
            set { status = value; }
        }
        #endregion
        private string billno;


        #region 
        /// <summary>
        ///
        /// </summary>
        [Column("billno")]
        public string Billno
        {
            get { return billno; }
            set { billno = value; }
        }
        #endregion
        private string approver;


        #region 审批人
        /// <summary>
        ///  审批人
        /// </summary>
        [Column("approver")]
        public string Approver
        {
            get { return approver; }
            set { approver = value; }
        }
        #endregion

        private Nullable<DateTime> approvedate;


        #region  审批日期
        /// <summary>
        /// 审批日期
        /// </summary>
        [Column("approvedate")]
        public Nullable<DateTime> Approvedate
        {
            get { return approvedate; }
            set { approvedate = value; }
        }
        #endregion
        private string modifier;


        #region 修改人
        /// <summary>
        /// 修改人
        /// </summary>
        [Column("modifier")]
        public string Modifier
        {
            get { return modifier; }
            set { modifier = value; }
        }
        #endregion

        private Nullable<DateTime> modifydate;

        #region 修改日期
        /// <summary>
        /// 修改日期
        /// </summary>
        [Column("modifydate")]
        public Nullable<DateTime> Modifydate
        {
            get { return modifydate; }
            set { modifydate = value; }
        }
        #endregion

        private string apremark;
        #region
        /// <summary>
        /// 
        /// </summary>
        [Column("apremark")]
        public string Apremark
        {
            get { return apremark; }
            set { apremark = value; }
        }
        #endregion
        private string isnotkaoqin;


        #region 是否為考勤
        /// <summary>
        /// 是否為考勤
        /// </summary>
        [Column("isnotkaoqin")]
        public string Isnotkaoqin
        {
            get { return isnotkaoqin; }
            set { isnotkaoqin = value; }
        }
        #endregion
        private string localname;


        #region 姓名
        /// <summary>
        /// 姓名
        /// </summary>
        [Column("localname")]
        public string Localname
        {
            get { return localname; }
            set { localname = value; }
        }
        #endregion
        private string dcode;


        #region 部門代碼
        /// <summary>
        /// 部門代碼
        /// </summary>
        [Column("dcode")]
        public string Dcode
        {
            get { return dcode; }
            set { dcode = value; }
        }
        #endregion
        private string sex;


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
        private string depname;


        #region 部門名稱
        /// <summary>
        /// 部門名稱
        /// </summary>
        [Column("depname")]
        public string Depname
        {
            get { return depname; }
            set { depname = value; }
        }
        #endregion
        private string buname;


        #region  組織名稱
        /// <summary>
        /// 組織名稱
        /// </summary>
        [Column("buname")]
        public string Buname
        {
            get { return buname; }
            set { buname = value; }
        }
        #endregion
        private string statusname;


        #region 狀態名稱
        /// <summary>
        /// 狀態名稱
        /// </summary>
        [Column("statusname")]
        public string Statusname
        {
            get { return statusname; }
            set { statusname = value; }
        }
        #endregion
        private string evectiontypename;


        #region 出差類別名稱
        /// <summary>
        /// 出差類別名稱
        /// </summary>
        [Column("evectiontypename")]
        public string Evectiontypename
        {
            get { return evectiontypename; }
            set { evectiontypename = value; }
        }
        #endregion
        private string comeyears;


        #region 入場年數
        /// <summary>
        /// 入場年數
        /// </summary>
        [Column("comeyears")]
        public string Comeyears
        {
            get { return comeyears; }
            set { comeyears = value; }
        }
        #endregion

        private string startdatestr;

        #region 開始日期（字符串類型）
        /// <summary>
        /// 開始日期（字符串類型）
        /// </summary>
        [Column("startdatestr")]
        public string Startdatestr
        {
            get { return startdatestr; }
            set { startdatestr = value; }
        }
        #endregion

        private string enddatestr;

        #region 截止日期（字符串類型）
        /// <summary>
        /// 截止日期（字符串類型）
        /// </summary>
        [Column("enddatestr")]
        public string Enddatestr
        {
            get { return enddatestr; }
            set { enddatestr = value; }
        }
        #endregion

        private string approvedatestr;

        #region 审批日期（字符串類型）
        /// <summary>
        /// 审批日期（字符串類型）
        /// </summary>
        [Column("approvedatestr")]
        public string Approvedatestr
        {
            get { return approvedatestr; }
            set { approvedatestr = value; }
        }
        #endregion

        private string modifydatestr;

        #region 修改日期（字符串類型）
        /// <summary>
        /// 修改日期（字符串類型）
        /// </summary>
        [Column("modifydatestr")]
        public string Modifydatestr
        {
            get { return modifydatestr; }
            set { modifydatestr = value; }
        }
        #endregion
    }
}
