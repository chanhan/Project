/*
 * Copyright (C) 2012 GDSBG MIABU 版權所有。
 * 
 * 檔案名： WorkFLowLimitModel.cs
 * 檔功能描述： 未刷補卡實體
 * 
 * 版本：1.0
 * 創建標識： 劉小明 2012.02.01
 * 
 */
using System;
using System.Collections.Generic;
using System.Text;
using GDSBG.MiABU.Attendance.Common.Attributes;

/**
 * 屬性方法不能私有否則外部無法進行訪問
 * */
namespace GDSBG.MiABU.Attendance.Model.WorkFlow
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable, TableName("gds_att_makeup", SelectTable = "gds_att_makeup_v")]
    public class WorkFlowCardMakeupModel:ModelBase
    {
      
        /// <summary>
        /// ID
        /// </summary>
        [Column("ID")]
        public string Id { get; set; }

        
        /// <summary>
        /// 
        /// </summary>
        [Column("WORKNO")]
        public string Workno { get; set; }

        /// <summary>
        /// 考勤日期
        /// </summary>
        [Column("KQDATE")]
        public Nullable<DateTime>  Kqdate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column("CARDTIME")]
        public Nullable<DateTime>  Cardtime { get; set; }

        /// <summary>
        /// 補卡類型
        /// </summary>
        [Column("MAKEUPTYPE")]
        public string  Makeuptype { get; set; }

        /// <summary>
        /// 狀態
        /// </summary>
        [Column("STATUS")]
        public string Status { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column("APPROVER")]
        public string Approver { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column("APREMARK")]
        public string Apremark { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column("APPROVEDATE")]
        public Nullable<DateTime> Approvedate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column("BILLNO")]
        public string Billno { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column("MODIFIER")]
        public string Modifier { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column("MODIFYDATE")]
        public Nullable<DateTime> Modifydate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column("REASONTYPE")]
        public string Reasontype { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column("REASONREMARK")]
        public string Reasonremark { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column("DECSALARY")]
        public string Decsalary { get; set; }


        [Column("DISSIGNRMARK")]
        public string Dissignrmark {get;set;}


        [Column("SHIFTDESC")]
        public string Shiftdesc { get; set; }

        [Column("BUNAME")]
        public string Buname { get; set; }

        [Column("REASONNAME")]
        public string Reasonname { get; set; }

        [Column("SALARYFLAG")]
        public string Salaryflag { get; set; }

        [Column("MAKEUPTYPENAME")]
        public string Makeuptypename { get; set; }

        [Column("STATUSNAME")]
        public string Statusname { get; set; }


        [Column("APPROVERNAME")]
        public string Approvername { get; set; } 
         

        [Column("LOCALNAME")]
        public string Localname { get; set; }

        [Column("DEPNAME")]
        public string Depname { get; set; }

        [Column("DNAME")]
        public string Dname { get; set; }

        [Column("DCODE")]
        public string Dcode{get;set;}

        [Column("DEPCODE")]
        public string Depcode { get; set; }

    
    }

}
