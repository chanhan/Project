using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication2
{
    public partial class WebForm2 : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            string connStr = ConfigurationManager.ConnectionStrings["SportInfoV5"].ToString();
            if (!IsPostBack)
            {
               // Demo();
              //  Response.Write("Success");
               
                //if (execfile())
                //{
                //    Response.Write("Success");
                //}
                 StreamWriter sw = new StreamWriter(@"E:\newScript.sql",true);
                 StreamWriter sw2 = new StreamWriter(@"E:\wrongScript.sql", true);
                 StreamReader sr = new StreamReader(@"E:\script.sql");
                 StringBuilder sb = new StringBuilder();
                 string text=sr.ReadLine();

                 while (!sr.EndOfStream)
                 {
                     if (text != "GO")
                     {
                         sb.Append(text+"\r\n");
                     }
                     else
                     {

                         if (ExecuteSql(connStr, sb.ToString()))
                         {
                             sw.Write(sb.ToString() + "\r\nGO\r\n");
                             sw.Flush();
                         }
                         else
                         {
                             sw2.Write(sb.ToString() + "\r\nGO\r\n");
                             sw2.Flush();
                         }
                         Label1.Text = sb.ToString();
                         sb.Clear();
                     }
                     text=sr.ReadLine();
                 }
            }
        }
        private void Demo()
        {
            // 调用sqlcmd
            ProcessStartInfo info = new ProcessStartInfo("sqlcmd", @" -S .\MSSQLSERVER -i E:\script.sql");
            //禁用OS Shell
            info.UseShellExecute = false;
            //禁止弹出新窗口
            info.CreateNoWindow = true;
            //隐藏windows style
            info.WindowStyle = ProcessWindowStyle.Hidden;
            //标准输出
            info.RedirectStandardOutput = true;

            Process proc = new Process();
            proc.StartInfo = info;
            //启动进程
           bool b= proc.Start();
        }
         /// <summary>  
    /// 创建连接起用进程建立数据库  
    /// </summary>  
    /// <returns></returns>  
//    private bool execfile()  
//    {  
//        try  
//        {
//            string connStr = ConfigurationManager.AppSettings["SportInfoV7"].ToString();
//;  

//            ExecuteSql(connStr, "master", "CREATE DATABASE" + " SqlTest");//调用ExecuteNonQuery()来创建数据库  

//            System.Diagnostics.Process sqlProcess = new System.Diagnostics.Process();//创建一个进程  

//            sqlProcess.StartInfo.FileName = "osql.exe";//OSQL基于ODBC驱动连接服务器的一个实用工具（可查阅SQL帮助手册）  
//            //string str = @"C:\Program Files\Microsoft SQL Server\MSSQL\Data";  

//            sqlProcess.StartInfo.Arguments = " -U sa -P sa -d SqlTest -i C:\\Program Files\\Microsoft SQL Server\\MSSQL\\Data";//获取启动程序时的参数  
//            sqlProcess.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;//调用进程的窗口状态，隐藏为后台 
//            sqlProcess.Start();  
//            sqlProcess.WaitForExit();  
//            sqlProcess.Close();  
//            return true;  
//        }  
//        catch (Exception ex)  
//        {  
//            throw ex;  
//        }  
//    }  

    /// <summary>  
    /// 创建数据库，调用ExecuteNonQuery()执行  
    /// </summary>  
    /// <param name="conn"></param>  
    /// <param name="DatabaseName"></param>  
    /// <param name="Sql"></param>  
    private bool ExecuteSql(string conn, string Sql)  
    {  
        System.Data.SqlClient.SqlConnection mySqlConnection = new System.Data.SqlClient.SqlConnection(conn);  
        System.Data.SqlClient.SqlCommand Command = new System.Data.SqlClient.SqlCommand(Sql, mySqlConnection);  
        Command.Connection.Open();  
        try  
        {  
            Command.ExecuteNonQuery();  
        }
            catch{
                return false;
            }
        finally  
        {  
         //  Command.Connection.Close();
        }
        return true;
    }  
}
    }

