/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： HrmEmpOtherMoveModel.cs
 * 檔功能描述：加班類別異動功能模組實體類
 * 
 * 版本：1.0
 * 創建標識： 陈函 2011.12.23
 * 
 */
using System;
using System.Collections.Generic;
using System.Text;
using GDSBG.MiABU.Attendance.Common.Attributes;

namespace GDSBG.MiABU.Attendance.Model.HRM.EmployeeData
{
    [Serializable, TableName("gds_att_empmove", SelectTable = "gds_att_movetype_v")]
    public class HrmEmpOtherMoveModel : ModelBase
    {

        #region 工號
        /// <summary>
        /// 工號
        /// </summary>
        string workNo;

        [Column("workno")]
        public string WorkNo
        {
            get { return workNo; }
            set { workNo = value; }
        }
        #endregion

        #region 異動序列
        /// <summary>
        /// 異動序列
        /// </summary>
        string moveOrder;

        [Column("moveorder")]
        public string MoveOrder
        {
            get { return moveOrder; }
            set { moveOrder = value; }
        }
        #endregion

        string moveTypeCode;
        string beforevalue;
        string aftervalue;

        #region  生效日期(yyyy/mm/dd)
        /// <summary>
        /// 生效日期(yyyy/mm/dd)
        /// </summary>
        string effectDateStr;

        [Column("effectdatestr")]
        public string EffectDateStr
        {
            get { return effectDateStr; }
            set { effectDateStr = value; }
        }
        #endregion

        #region 異動原因
        /// <summary>
        /// 異動原因
        /// </summary>
        string moveReason;

        [Column("movereason")]
        public string MoveReason
        {
            get { return moveReason; }
            set { moveReason = value; }
        }
        #endregion

        #region 備註
        /// <summary>
        /// 備註
        /// </summary>
        string remark;
        [Column("remark")]
        public string Remark
        {
            get { return remark; }
            set { remark = value; }
        }
        #endregion

        #region 申請人
        /// <summary>
        /// 申請人
        /// </summary>
        string applyMan;

        [Column("applyman")]
        public string ApplyMan
        {
            get { return applyMan; }
            set { applyMan = value; }
        }
        #endregion

        #region 申請日期(yyyy/mm/dd)
        /// <summary>
        /// 申請日期(yyyy/mm/dd)
        /// </summary>
        string applyDateStr;

        [Column("applydatestr")]
        public string ApplyDateStr
        {
            get { return applyDateStr; }
            set { applyDateStr = value; }
        }
        #endregion

        #region 確認狀態
        /// <summary>
        /// 確認狀態
        /// </summary>
        string state;
        [Column("state")]
        public string State
        {
            get { return state; }
            set { state = value; }
        }
        #endregion

        #region 確認人
        /// <summary>
        /// 確認人
        /// </summary>
        string conFirmMan;

        [Column("confirmman")]
        public string ConfirmMan
        {
            get { return conFirmMan; }
            set { conFirmMan = value; }
        }
        #endregion

        #region 確認日期
        /// <summary>
        /// 確認日期
        /// </summary>
        string conFirmDateStr;

        [Column("confirmdatestr")]
        public string ConfirmDateStr
        {
            get { return conFirmDateStr; }
            set { conFirmDateStr = value; }
        }
        #endregion

        string joindate;
        string graduateschool;
        string graduatedate;
        string subject;
        string beforegraduateschool;
        string beforegraduatedate;
        string beforesubject;
        string hisflag;

        #region 姓名   
        /// <summary>
        /// 姓名
        /// </summary>
        string localName;
        [Column("localname")]
        public string LocalName
        {
            get { return localName; }
            set { localName = value; }
        }
        #endregion

        string sex;
        string levelcode;
        string managercode;
        string identityno;
        string overtimetype;
        string persontypecode;
        string postcode;
        string levelname;
        string managername;
        string overtimetypename;
        string persontypename;
        string postname;

        #region 部門名稱
        /// <summary>
        /// 部門名稱
        /// </summary>
        string depName;

        [Column("depname")]
        public string DepName
        {
            get { return depName; }
            set { depName = value; }
        }
        #endregion
        #region 組織名稱
        /// <summary>
        /// 組織名稱
        /// </summary>
        string buName;

        [Column("buname")]
        public string BuName
        {
            get { return buName; }
            set { buName = value; }
        }
        #endregion
        #region 異動類別名
        /// <summary>
        /// 異動類別名
        /// </summary>
        string moveTypeName;

        [Column("movetypename")]
        public string MoveType
        {
            get { return moveTypeName; }
            set { moveTypeName = value; }
        }
        #endregion
        #region 狀態
        /// <summary>
        /// 狀態
        /// </summary>
        string stateName;
        [Column("statename")]
        public string StateName
        {
            get { return stateName; }
            set { stateName = value; }
        }
        #endregion
        #region 異動前名稱
        /// <summary>
        /// 異動前名稱
        /// </summary>
        string beforeValueName;

        [Column("beforevaluename")]
        public string BeforeValueName
        {
            get { return beforeValueName; }
            set { beforeValueName = value; }
        }
        #endregion
        #region 異動后名稱
        /// <summary>
        /// 異動后名稱
        /// </summary>
        string afterValueName;

        [Column("aftervaluename")]
        public string AfterValueName
        {
            get { return afterValueName; }
            set { afterValueName = value; }
        }
        #endregion
    }
}
