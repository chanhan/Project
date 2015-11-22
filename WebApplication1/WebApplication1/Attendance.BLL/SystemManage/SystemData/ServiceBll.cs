using System.Data;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemData;
using GDSBG.MiABU.Attendance.IDAL.SystemManage.SystemData;

namespace GDSBG.MiABU.Attendance.BLL.SystemManage.SystemData
{
    /// <summary>
    /// 服務熱線業務邏輯類
    /// </summary>
    public class ServiceBll : BLLBase<IServiceDal>
    {
        /// <summary>
        /// 添加服務熱線
        /// </summary>
        /// <param name="model">服務熱線Model</param>
        /// <returns>是否成功</returns>
        public bool AddService(ServiceModel model)
        {
            return DAL.AddService(model);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateServiceByKey(ServiceModel model)
        {
            return DAL.UpdateServiceByKey(model);
        }

        /// <summary>
        /// 刪除
        /// </summary>
        /// <param name="serviceId">ID</param>
        /// <returns></returns>
        public bool DeleteServiceByKey(int serviceId)
        {
            return DAL.DeleteServiceByKey(serviceId);
        }

        /// <summary>
        /// 檢查服務名是否已存在
        /// </summary>
        /// <param name="serviceName">服務名</param>
        /// <returns>是否存在</returns>
        public bool CheckServiceNameExist(string serviceName)
        {
            return DAL.CheckServiceNameExist(serviceName);
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
            return DAL.GetPagerServicies(model, pageIndex, pageSize, out totalCount);
        }

        public DataTable GetTopServiceList(int topCount)
        {
            return DAL.GetTopServiceList(topCount);
        }
    }
}
