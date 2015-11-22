/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： OrgmanagerModel.cs
 * 檔功能描述： 組織管理者資料實體類
 * 
 * 版本：1.0
 * 創建標識： 昝望 2011.12.12
 * 
 */

using System;
using GDSBG.MiABU.Attendance.Common.Attributes;

namespace GDSBG.MiABU.Attendance.Model.HRM
{
    [Serializable, TableName("GDS_ATT_ORGMANAGER", SelectTable = "GDS_ATT_ORGMANAGER_V")]
    public class OrgmanagerModel : ModelBase
    {
        private string depCode;

        [Column("DepCode", IsPrimaryKey = true)]
        public string DepCode
        {
            get { return depCode; }
            set { depCode = value; }
        }
        private string depName;

        [Column("DepName", OnlySelect = true)]
        public string DepName
        {
            get { return depName; }
            set { depName = value; }
        }
        private string workNo;

        [Column("workNo", IsPrimaryKey = true)]
        public string WorkNo
        {
            get { return workNo; }
            set { workNo = value; }
        }
        private string localName;

        [Column("localName", OnlySelect = true)]
        public string LocalName
        {
            get { return localName; }
            set { localName = value; }
        }

        private string manager;

        [Column("managercode")]
        public string Manager
        {
            get { return manager; }
            set { manager = value; }
        }

        private string managerName;

        [Column("managername")]
        public string ManagerName
        {
            get { return managerName; }
            set { managerName = value; }
        }

        private string notes;

        [Column("notes", OnlySelect = true)]
        public string Notes
        {
            get { return notes; }
            set { notes = value; }
        }


        private string deputy;

        [Column("depuTy")]
        public string Deputy
        {
            get { return deputy; }
            set { deputy = value; }
        }
        private string deputyName;

        [Column("deputyName", OnlySelect = true)]
        public string DeputyName
        {
            get { return deputyName; }
            set { deputyName = value; }
        }

        private string deputyNotes;

        [Column("deputyNotes")]
        public string DeputyNotes
        {
            get { return deputyNotes; }
            set { deputyNotes = value; }
        }

        private string isDirectlyUnder;

        [Column("isDirectlyUnder")]
        public string IsDirectlyUnder
        {
            get { return isDirectlyUnder; }
            set { isDirectlyUnder = value; }
        }
        private string isBGAudit;

        [Column("isBGAudit")]
        public string IsBGAudit
        {
            get { return isBGAudit; }
            set { isBGAudit = value; }
        }


        private string errorMsg;

        [Column("ErrorMsg")]
        public string ErrorMsg
        {
            get { return errorMsg; }
            set { errorMsg = value; }
        }

        private string isTW;

        [Column("IsTW")]
        public string IsTW
        {
            get { return isTW; }
            set { isTW = value; }
        }
    }
}
