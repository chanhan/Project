/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： TeacherInModel.cs
 * 檔功能描述： 內部講師費實體類
 * 
 * 版本：1.0
 * 創建標識： 張明強 2012.03.19
 * 
 */
using System;
using System.Collections.Generic;
using System.Text;
using GDSBG.MiABU.Attendance.Common.Attributes;

namespace GDSBG.MiABU.Attendance.Model.WFReporter
{
    [Serializable, TableName("gds_att_teacher_in", SelectTable = "gds_att_teacherin_v")]
    public class TeacherInModel:ModelBase
    {
        private string monthYear;
        private string workNo;
        private Nullable<DateTime> lessonDate;
        private string startTime;
        private string endTime;
        private Nullable<Decimal> lessonHours;
        private string lessonName;
        private string teacherSalary;
        private string remark;
        private Nullable<DateTime> createDate;
        private string createUser;
        private Nullable<DateTime> updateDate;
        private string updateUser;
        private string lessonTimeSpan;

        #region 作業年月
        /// <summary>
        /// 作業年月
        /// </summary>
        [Column("monthYear")]
        public string MonthYear
        {
            get { return monthYear; }
            set { monthYear = value; }
        }
        #endregion

        #region 工號
        /// <summary>
        /// 工號
        /// </summary>
        [Column("workno")]
        public string WorkNo
        {
            get { return workNo; }
            set { workNo = value; }
        }
        #endregion

        #region 講課日期
        /// <summary>
        /// 講課日期
        /// </summary>
        [Column("lessondate")]
        public Nullable<DateTime> LessonDate
        {
            get { return lessonDate; }
            set { lessonDate = value; }
        }
        #endregion

        #region 開始時間
        /// <summary>
        /// 開始時間
        /// </summary>
        [Column("starttime")]
        public string StartTime
        {
            get { return startTime; }
            set { startTime = value; }
        }
        #endregion


        #region 結束時間
        /// <summary>
        /// 結束時間
        /// </summary>
        [Column("endtime")]
        public string EndTime
        {
            get { return endTime; }
            set { endTime = value; }
        }
        #endregion

        #region 講課時數
        /// <summary>
        /// 講課時數
        /// </summary>
        [Column("lessonhours")]
        public Nullable<Decimal> LessonHours
        {
            get { return lessonHours; }
            set { lessonHours = value; }
        }
        #endregion

        #region 課程名稱
        /// <summary>
        /// 課程名稱
        /// </summary>
        [Column("lessonname")]
        public string LessonName
        {
            get { return lessonName; }
            set { lessonName = value; }
        }
        #endregion

        #region 講師費(元)
        /// <summary>
        /// 講師費(元)
        /// </summary>
        [Column("teachersalary")]
        public string TeacherSalary
        {
            get { return teacherSalary; }
            set { teacherSalary = value; }
        }
        #endregion

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



        #region 創建日期
        /// <summary>
        /// 創建日期
        /// </summary>
        [Column("create_date")]
        public Nullable<DateTime> CreateDate
        {
            get { return createDate; }
            set { createDate = value; }
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

        #region 更新日期
        /// <summary>
        /// 更新日期
        /// </summary>
        [Column("update_date")]
        public Nullable<DateTime> UpdateDate
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


        #region 講課時段
        /// <summary>
        /// 講課時段
        /// </summary>
        [Column("lessontimespan", OnlySelect=true)]
        public string LessonTimeSpan
        {
            get { return lessonTimeSpan; }
            set { lessonTimeSpan = value; }
        }
        #endregion
    }
}
