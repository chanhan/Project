using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using GDSBG.MiABU.Attendance.BLL.SystemManage.SystemData;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemData;

namespace GDSBG.MiABU.Attendance.Web.HomePage
{
    public partial class NoticeDetail : BasePage
    {
        InfoNoticesBll infoNoticesBll = new InfoNoticesBll();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["path"]))
            {
                PageHelper.ReturnHTTPStream(MapPath("~/AnnexFiles/" + Request.QueryString["path"]), false);
            }
            if (!IsPostBack)
            {
                NoticeControlBind();
            }          
        }

        /// <summary>
        /// 綁定控件
        /// </summary>
        private void NoticeControlBind()
        {

            if (!string.IsNullOrEmpty(Request.QueryString["noticeId"]))
            {
                string noticeId = Request.QueryString["noticeId"];
                infoNoticesBll.AddBrowseTimes(noticeId);
                InfoNoticesModel model = infoNoticesBll.GetNoticeByKey(noticeId);
                litAuthorTel.Text = model.AuthorTel;
                litBrowseTimes.Text = model.BrowseTimes.ToString();
                litNoticeAuthor.Text = model.NoticeAuthor;
                litNoticeContent.Text = model.NoticeContent.Replace("\r", "").Replace("\n", "<br/>").Replace(" ", "&nbsp;&nbsp;").Replace("\t", "&nbsp;&nbsp;&nbsp;&nbsp;");
                litNoticeDate.Text = model.NoticeDate.Value.ToString("yyyy/MM/dd");
                litNoticeDept.Text = model.NoticeDept;
                litNoticeTitle.Text = model.NoticeTitle;
                AddFileLinks(model.AnnexFilePath);
            }
        }

        /// <summary>
        /// 添加附件下載連結
        /// </summary>
        /// <param name="annexFile"></param>
        private void AddFileLinks(string annexFile)
        {
            if (!string.IsNullOrEmpty(annexFile))
            {
                string[] files = annexFile.Split('|');
                foreach (string item in files)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        HyperLink lnk = new HyperLink();
                        lnk.Text = item;
                        lnk.NavigateUrl = "javascript:down(\"" + item + "\")";
                        phFiles.Controls.Add(lnk);
                    }
                }
            }
        }
    }
}
