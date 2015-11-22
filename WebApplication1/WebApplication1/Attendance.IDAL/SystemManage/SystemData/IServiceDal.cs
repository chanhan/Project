using System.Data;
using GDSBG.MiABU.Attendance.Common.Attributes;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemData;

namespace GDSBG.MiABU.Attendance.IDAL.SystemManage.SystemData
{
    /// <summary>
    /// 服務熱線數據操作接口
    /// </summary>
    [RefClass("SystemManage.SystemData.ServiceDal")]
    public interface IServiceDal
    {
        /// <summary>
        /// 添加服務熱線
        /// </summary>
        /// <param name="model">服務熱線Model</param>
        /// <returns>是否成功</returns>
        bool AddService(ServiceModel model);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool UpdateServiceByKey(ServiceModel model);

        /// <summary>
        /// 刪除
        /// </summary>
        /// <param name="serviceId">ID</param>
        /// <returns></returns>
        bool DeleteServiceByKey(int serviceId);

        /// <summary>
        /// 檢查服務名是否已存在
        /// </summary>
        /// <param name="serviceName">服務名</param>
        /// <returns>是否存在</returns>
        bool CheckServiceNameExist(string serviceName);

        /// <summary>
        /// 獲得分頁數據
        /// </summary>
        /// <param name="model">條件實體</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        DataTable GetPagerServicies(ServiceModel model, int pageIndex, int pageSize, out int totalCount);

        DataTable GetTopServiceList(int topCount);
    }
}
