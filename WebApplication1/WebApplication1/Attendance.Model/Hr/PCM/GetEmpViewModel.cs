/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： GetEmpViewModel.cs
 * 檔功能描述： 請假申請人員基本信息實體類
 * 
 * 版本：1.0
 * 創建標識：陳函  2012.03.10
 * 
 */
using System;
using System.Collections.Generic;
using System.Text;
using GDSBG.MiABU.Attendance.Common.Attributes;

namespace GDSBG.MiABU.Attendance.Model.Hr.PCM
{
    [Serializable, TableName(" gds_att_getEmp_v", SelectTable = "gds_att_getEmp_v")]
    public class GetEmpViewModel : ModelBase
    {
        private string workno;
        #region
        /// <summary>
        /// 
        /// </summary>
        [Column("workno")]
        public string WorkNo
        {
            get { return workno; }
            set { workno = value; }
        }
        #endregion

        private string localname;
        #region
        /// <summary>
        /// 
        /// </summary>
        [Column("localname")]
        public string LocalName
        {
            get { return localname; }
            set { localname = value; }
        }
        #endregion
        //private string sex;
        //#region
        ///// <summary>
        ///// 
        ///// </summary>
        //[Column("sex")]
        //public string Sex
        //{
        //    get { return sex; }
        //    set { sex = value; }
        //}
        //#endregion

        //private string marrystate;
        //#region
        ///// <summary>
        ///// 
        ///// </summary>
        //[Column("marrystate")]
        //public string MarryState
        //{
        //    get { return marrystate; }
        //    set { marrystate = value; }
        //}
        //#endregion

        //private string managercode;
        //#region
        ///// <summary>
        ///// 
        ///// </summary>
        //[Column("managercode")]
        //public string ManagerCode
        //{
        //    get { return managercode; }
        //    set { managercode = value; }
        //}
        //#endregion

        private string notes;
        #region
        /// <summary>
        /// 
        /// </summary>
        [Column("notes")]
        public string Notes
        {
            get { return notes; }
            set { notes = value; }
        }
        #endregion

        //private string depcode;
        //#region
        ///// <summary>
        ///// 
        ///// </summary>
        //[Column("depcode")]
        //public string DepCode
        //{
        //    get { return depcode; }
        //    set { depcode = value; }
        //}
        //#endregion

        private string flag;
        #region
        /// <summary>
        /// 
        /// </summary>
        [Column("flag")]
        public string Flag
        {
            get { return flag; }
            set { flag = value; }
        }
        #endregion
    }
}