using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemData;
using GDSBG.MiABU.Attendance.IDAL.SystemManage.SystemData;
using System.Data.OracleClient;

namespace GDSBG.MiABU.Attendance.OracleDAL.SystemManage.SystemData
{
    /// <summary>
    /// 服務熱線數據操作類
    /// </summary>
    public class ServiceDal : DALBase<ServiceModel>, IServiceDal
    {
        /// <summary>
        /// 添加服務熱線
        /// </summary>
        /// <param name="model">服務熱線Model</param>
        /// <returns>是否成功</returns>
        public bool AddService(ServiceModel model)
        {
            return DalHelper.Insert(model) != -1;
        }

        public DataTable GetTopServiceList(int topCount)
        {
            return DalHelper.Select(new ServiceModel() { ActiveFlag = "Y" }, 1, topCount);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateServiceByKey(ServiceModel model)
        {
            return DalHelper.UpdateByKey(model) != -1;
        }

        /// <summary>
        /// 刪除
        /// </summary>
        /// <param name="serviceId">ID</param>
        /// <returns></returns>
        public bool DeleteServiceByKey(int serviceId)
        {
            return DalHelper.Delete(new ServiceModel() { ServiceId = serviceId }) != -1;
        }

        /// <summary>
        /// 檢查服務名是否已存在
        /// </summary>
        /// <param name="serviceName">服務名</param>
        /// <returns>是否存在</returns>
        public bool CheckServiceNameExist(string serviceName)
        {
            string cmdTxt = "SELECT COUNT(1) FROM GDS_ATT_INFO_SERVICES A WHERE A.SERVICE_NAME = :a_serviceName";
            return Convert.ToInt32(DalHelper.ExecuteScalar(cmdTxt, new OracleParameter(":a_serviceName", serviceName))) > 0;
        }

        /// <summary>
        /// 獲得分頁數據
        /// </summary>
        /// <param name="model">條件實體</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public DataTable GetPagerServicies(ServiceModel model, int pageIndex, int pageSize, out int totalCount)
        {
            return DalHelper.Select(model, true, pageIndex, pageSize, out totalCount);
        }
    }
}
