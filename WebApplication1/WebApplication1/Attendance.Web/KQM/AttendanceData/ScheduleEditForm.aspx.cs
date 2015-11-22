using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using GDSBG.MiABU.Attendance.BLL.KQM.AttendanceData;
using GDSBG.MiABU.Attendance.Model.KQM.AttendanceData;
using Resources;

namespace GDSBG.MiABU.Attendance.Web.KQM.AttendanceData
{
    public partial class ScheduleEditForm : BasePage
    {
        SchduleDataBll scheduleBll = new SchduleDataBll();
        protected void Page_Load(object sender, EventArgs e)
        {
            ShowDataBind();
        }

        /// <summary>
        /// 
        /// </summary>
        private void ShowDataBind()
        {
            //ScheduleDataModel model = PageHelper.GetModel<ScheduleDataModel>();
            
           
        }

        protected void ButtonQuery_Click(object sender, EventArgs e)
        {

        }

        protected void ButtonAdd_Click(object sender, EventArgs e)
        {
  
        }

        protected void ButtonModify_Click(object sender, EventArgs e)
        {

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

        }
    }
}
