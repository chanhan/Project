using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication2
{
    public partial class Contact : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            FileAccess();
            DataBaseAccess();
            HttpWebRequest req = WebRequest.Create("http://www.sina.com.cn") as HttpWebRequest;
            HttpWebResponse res = req.GetResponse() as HttpWebResponse;
            Stream sr = res.GetResponseStream();
            StreamReader reader = new StreamReader(sr, Encoding.GetEncoding("UTF-8"));
            string str = reader.ReadToEnd();
            WebHeaderCollection headers = res.Headers;
            CookieCollection cookies = res.Cookies;
        }
        private void DataBaseAccess()
        {
            string connstr = ConfigurationManager.ConnectionStrings["hello"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connstr))
            {
                string sql = "select * from [User]";
                SqlCommand command = new SqlCommand(sql, conn);
                //conn.Open();
                //int i = command.ExecuteNonQuery();
                //conn.Close();
                SqlDataAdapter da = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                da.Fill(dt);
            }
        }
        private void FileAccess()
        {
            StreamReader sr = new StreamReader(@"E:\\VPN.txt");
            string str = sr.ReadToEnd();
            sr.Close();
            StreamWriter sw = new StreamWriter(@"E:\\VPN.txt");
            sw.Write("ok");
            System.Threading.Tasks.Task t= sw.WriteAsync("OK");
            sw.Flush();
            sw.Close();
        }
    }
}