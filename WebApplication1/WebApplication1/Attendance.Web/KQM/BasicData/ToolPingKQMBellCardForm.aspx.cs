using System;
using System.Data;
using System.Diagnostics;
using GDSBG.MiABU.Attendance.BLL.KQM.BasicData;
using GDSBG.MiABU.Attendance.Model.KQM.BasicData;


/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： KQMBellCardForm.aspx.cs
 * 檔功能描述： 卡鐘資料維護--驗證卡鐘網絡狀況
 * 
 * 版本：1.0
 * 創建標識： 昝望 2011.12.20
 */

namespace GDSBG.MiABU.Attendance.Web.KQM.BasicData
{
    public partial class ToolPingKQMBellCardForm : BasePage
    {
        BellCardBll bll = new BellCardBll();
        int totalCount;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
 
            }
        }

        protected void btnCheck_Click(object sender, EventArgs e)
        {
            BellCardModel model = PageHelper.GetModel<BellCardModel>(pnlContent.Controls);
            DataTable dt = bll.GetBellCard(model, 1, 10, out totalCount);
            UltraWebGridPingR.DataSource = DealWithDataTable(dt);
            UltraWebGridPingR.DataBind();
        }

        private DataTable DealWithDataTable(DataTable dt)
        {
            dt.Columns.Add("IsOnline");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string strIP = GetPing(dt.Rows[i]["PortIP"].ToString());
                dt.Rows[i]["IsOnline"] = strIP;
            }
            return dt;
        }

        public string GetPing(string strIP)
        {
            string pingrst = "";
            Process process = new Process
            {
                StartInfo = { FileName = "ping", Arguments = strIP + " -n 1 -w 2", UseShellExecute = false, CreateNoWindow = true, RedirectStandardOutput = true }
            };
            process.Start();
            string strRst = process.StandardOutput.ReadToEnd();
            if (strRst.IndexOf("(0% loss)") != -1)
            {
                pingrst = "Online";
            }
            else if (strRst.IndexOf("Destination host unreachable.") != -1)
            {
                pingrst = "無法達到目的主機";
            }
            else if (strRst.IndexOf("Request timed out.") != -1)
            {
                pingrst = "Offline";
            }
            else if (strRst.IndexOf("Unknown host") != -1)
            {
                pingrst = "無法解析主機";
            }
            else
            {
                pingrst = strRst;
            }
            process.WaitForExit();
            return pingrst;
        }
    }
}
