/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： BGCalendarModel.cs
 * 檔功能描述： BG行事歷實體類
 * 
 * 版本：1.0
 * 創建標識： 昝望 2011.12.21
 * 
 */

using System;
using GDSBG.MiABU.Attendance.Common.Attributes;

namespace GDSBG.MiABU.Attendance.Model.KQM.BasicData
{


    /// <summary>
    /// 卡鐘信息實體類
    /// </summary>
    [Serializable, TableName("GDS_ATT_BGCALENDAR")]
    public class BGCalendarModel : ModelBase
    {
        private string bgCode;
        private Nullable<DateTime> workDay;
        private string weekNo;
        private string weekDay;
        private string workFlag;
        private string hollDayFlag;
        private string remark;
        private string updateUser;



        #region BG事業群code
        [Column("BgCode", IsPrimaryKey = true)]
        public string BgCode
        {
            get { return bgCode; }
            set { bgCode = value; }
        }
        #endregion

        #region 日期
        [Column("WorkDay", IsPrimaryKey = true)]
        public Nullable<DateTime> WorkDay
        {
            get { return workDay; }
            set { workDay = value; }
        }
        #endregion

        #region 第幾周
        [Column("WeekNo")]
        public string WeekNo
        {
            get { return weekNo; }
            set { weekNo = value; }
        }
        #endregion

        #region 星期幾
        [Column("WeekDay")]
        public string WeekDay
        {
            get { return weekDay; }
            set { weekDay = value; }
        }
        #endregion

        #region 是否工作日
        [Column("WorkFlag")]
        public string WorkFlag
        {
            get { return workFlag; }
            set { workFlag = value; }
        }
        #endregion

        #region 是否節假日
        [Column("HoliDayFlag")]
        public string HollDayFlag
        {
            get { return hollDayFlag; }
            set { hollDayFlag = value; }
        }
        #endregion

        #region 備註
        [Column("Remark")]
        public string Remark
        {
            get { return remark; }
            set { remark = value; }
        }
        #endregion

        #region 修改人
        [Column("Update_User")]
        public string UpdateUser
        {
            get { return updateUser; }
            set { updateUser = value; }
        }
        #endregion
    }
}
