using System;
using GDSBG.MiABU.Attendance.Common.Attributes;

namespace GDSBG.MiABU.Attendance.Model.SystemManage.SystemData
{
    /// <summary>
    /// 服務熱線實體類
    /// </summary>
    [TableName("gds_att_info_services")]
    public class ServiceModel : ModelBase
    {
        Nullable<int> serviceId;
        string serviceName;
        string phone;
        string activeFlag;
        string createUser;
        Nullable<DateTime> createDate;
        string updateUser;
        Nullable<DateTime> updateDate;

        /// <summary>
        /// 服務熱線ID
        /// </summary>
        [Column("SERVICE_ID", IsPrimaryKey = true)]
        public Nullable<int> ServiceId
        {
            get { return serviceId; }
            set { serviceId = value; }
        }

        /// <summary>
        /// 服務名稱
        /// </summary>
        [Column("SERVICE_NAME")]
        public string ServiceName
        {
            get { return serviceName; }
            set { serviceName = value; }
        }

        /// <summary>
        /// 服務電話
        /// </summary>
        [Column("SERVICE_PHONE")]
        public string Phone
        {
            get { return phone; }
            set { phone = value; }
        }

        /// <summary>
        /// 是否有效(Y/N)
        /// </summary>
        [Column("ACTIVE_FLAG")]
        public string ActiveFlag
        {
            get { return activeFlag; }
            set { activeFlag = value; }
        }

        /// <summary>
        /// 創建用戶
        /// </summary>
        [Column("CREATE_USER")]
        public string CreateUser
        {
            get { return createUser; }
            set { createUser = value; }
        }

        /// <summary>
        /// 創建日期
        /// </summary>
        [Column("CREATE_DATE")]
        public Nullable<DateTime> CreateDate
        {
            get { return createDate; }
            set { createDate = value; }
        }

        /// <summary>
        /// 更新用戶
        /// </summary>
        [Column("UPDATE_USER")]
        public string UpdateUser
        {
            get { return updateUser; }
            set { updateUser = value; }
        }

        /// <summary>
        /// 更新日期
        /// </summary>
        [Column("UPDATE_DATE")]
        public Nullable<DateTime> UpdateDate
        {
            get { return updateDate; }
            set { updateDate = value; }
        }
    }
}
