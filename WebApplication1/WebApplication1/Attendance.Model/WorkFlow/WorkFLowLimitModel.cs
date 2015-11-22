/*
 * Copyright (C) 2012 GDSBG MIABU 版權所有。
 * 
 * 檔案名： WorkFLowLimitModel.cs
 * 檔功能描述： 流程限定查詢實體
 * 
 * 版本：1.0
 * 創建標識： 劉小明 2012.01.06
 * 
 */
using System;
using GDSBG.MiABU.Attendance.Common.Attributes;

namespace GDSBG.MiABU.Attendance.Model.WorkFlow
{
    /// <summary>
    /// 功能實體類
    /// </summary>
   // [Serializable, TableName("gds_att_employee")]
    public class WorkFLowLimitModel : ModelBase
    { 
        /// <summary>
        /// 工號
        /// </summary>
        [Column("auditman")]
        private string Auditman { get; set; }
  
        /// <summary>
        /// 姓名
        /// </summary>
        [Column("localname")]
        public string Localname{get;set;}


        /// <summary>
        /// Notes
        /// </summary>
        [Column("notes")]
        private string Notes { get; set; }

        /// <summary>
        /// 職位
        /// </summary>
        [Column("managername")]
        private string Managername{ get; set; }
 
         
    }
}
