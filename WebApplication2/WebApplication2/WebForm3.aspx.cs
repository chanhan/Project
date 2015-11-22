using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication2
{
    public partial class WebForm3 : System.Web.UI.Page
    {
        string connStr = ConfigurationManager.ConnectionStrings["SportInfoV5"].ToString();
        bool isWrite = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            DirectoryInfo dir = Directory.CreateDirectory(@"D:\速报\Table");
            foreach (var item in dir.GetFiles())
            {
                StreamReader sr = new StreamReader(item.FullName);
                if (item.Name == "dbo.FootballSchedules.Table.sql")
                { 
                    isWrite = true;
                    continue;
                }
                if (isWrite&&ExecuteSql(connStr, sr.ReadToEnd()))
                Label1.Text += item.FullName;
                else
                    Label2.Text += item.FullName;
	
            }
        }
        private bool ExecuteSql(string conn, string Sql)
        {
            System.Data.SqlClient.SqlConnection mySqlConnection = new System.Data.SqlClient.SqlConnection(conn);
            System.Data.SqlClient.SqlCommand Command = new System.Data.SqlClient.SqlCommand(Sql.Replace("GO",string.Empty), mySqlConnection);
            Command.Connection.Open();
            try
            {
                Command.ExecuteNonQuery();
            }
            catch
            {
                return false;
            }
            finally
            {
                  Command.Connection.Close();
            }
            return true;
        }  

    }
}