using System;
using System.Data;
using GDSBG.MiABU.Attendance.BLL.SystemManage.SystemData;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemData;

namespace GDSBG.MiABU.Attendance.Web.HomePage
{
    public partial class Service : BasePage
    {
        ServiceBll serviceBll = new ServiceBll();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGridView();
            }
        }

        /// <summary>
        /// 綁定GridView數據
        /// </summary>
        private void BindGridView()
        {
            int totalCount;
            ServiceModel model = PageHelper.GetModel<ServiceModel>(pnlService.Controls);
            model.ActiveFlag = "Y";
            DataTable serviceDt = serviceBll.GetPagerServicies(model, pager.CurrentPageIndex, pager.PageSize, out totalCount);
            pager.RecordCount = totalCount;
            UltraWebGridServiceHotline.DataSource = serviceDt;
            UltraWebGridServiceHotline.DataBind();
        }

        /// <summary>
        /// 查詢
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            pager.CurrentPageIndex = 1;
            BindGridView();
        }

        /// <summary>
        /// 分頁
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void pager_PageChanged(object sender, EventArgs e)
        {
            BindGridView();
        }
    }
}
