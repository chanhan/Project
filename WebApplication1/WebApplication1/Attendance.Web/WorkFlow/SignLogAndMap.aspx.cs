/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： SignLogAndMap.cs
 * 檔功能描述： 表單流程記錄以及表單流程流轉
 * 
 * 版本：1.0
 * 創建標識： 何偉 2012.1.12
 * 
 */
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
using System.Text;
using Resources;
using GDSBG.MiABU.Attendance.BLL.WorkFlow;

namespace GDSBG.MiABU.Attendance.Web.WorkFlow
{
    public partial class SignLogAndMap : BasePage
    {
        WFSignLogAndMap wf = new WFSignLogAndMap();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string Doc = (Request.QueryString["Doc"] == null) ? "" : Request.QueryString["Doc"].ToString();
                Internationalization();
                if (!string.IsNullOrEmpty(Doc))
                {
                    BindData(Doc);
                    MapBind(Doc);
                }
            }
        }
        //綁定流程圖
        protected void MapBind(string doc)
        {
            StringBuilder sb = new StringBuilder();
            DataTable dt = new DataTable();
            dt = wf.GetFlowMap(doc);
            //dt.Columns.Add("LEVEL_NAME");
            //dt.Columns.Add("VERIFY_NAME");
            //dt.Rows.Add("開始","admin(管理員)");
            //dt.Rows.Add("組長", "F7400141(何偉)");
            //dt.Rows.Add("科長", "F7400188(楊政)");
            //dt.Rows.Add("部長", "F7400143(小季)");
            //dt.Rows.Add("局長", "F7400125(小張)");
            //dt.Rows.Add("秘書長", "F7400125(小楊)");
            //dt.Rows.Add("書記", "F7400125(小李)");
            //dt.Rows.Add("結束", "");
            if (dt != null)
            {
                for (int i = 1; i <= dt.Rows.Count; i++)
                {
                    if (i == 1)
                    {
                        sb.Append("<DIV class='bigDiv' style='padding-top:15px;'>");
                        sb.Append("<div class='smallDiv'><img src='../../CSS/Images_new/MapStart.gif' /><span class='flow_title'>" + dt.Rows[i - 1]["LEVEL_NAME"].ToString() + "</span><br /><span class='flow_emp'>" + dt.Rows[i - 1]["VERIFY_NAME"].ToString() + "</span></div>");
                    }
                    else if (i == dt.Rows.Count)
                    {
                        sb.Append("<div class='smallDiv'><img src='../../CSS/Images_new/MapEnd.gif' /><br /><span class='flow_title'>" + dt.Rows[i - 1]["LEVEL_NAME"].ToString() + "</span><span class='flow_emp'>" + dt.Rows[i - 1]["VERIFY_NAME"].ToString() + "</span></div>");
                        sb.Append("</DIV>");
                    }
                    else
                    {
                        if (i % 5 == 0)
                        {
                            sb.Append("<div class='smallDiv'><img src='../../CSS/Images_new/MapSup.gif' /><span class='flow_title'>" + dt.Rows[i - 1]["LEVEL_NAME"].ToString() + "</span><br /><span class='flow_emp'>" + dt.Rows[i - 1]["VERIFY_NAME"].ToString() + "</span></div>");
                            sb.Append("</DIV><DIV class='bigDiv'>");
                        }
                        else
                        {
                            sb.Append("<div class='smallDiv'><img src='../../CSS/Images_new/MapSup.gif' /><span class='flow_title'>" + dt.Rows[i - 1]["LEVEL_NAME"].ToString() + "</span><br /><span class='flow_emp'>" + dt.Rows[i - 1]["VERIFY_NAME"].ToString() + "</span></div>");
                        }
                    }
                }
            }
            this.lbl_Process.Text = sb.ToString();
        }
        //綁定Log
        public void BindData(string doc)
        {
            DataTable dt = new DataTable();
            //dt.Columns.Add("Deal_person");
            //dt.Columns.Add("start_time");
            //dt.Columns.Add("end_time");
            //dt.Columns.Add("ismail");
            //dt.Columns.Add("status");
            //dt.Rows.Add("admin(管理員)", "2011/10/26 10:28:26", "2011/10/26 11:28:26","是","已簽核");
            //dt.Rows.Add("F7400141(何偉)", "2011/10/26 11:28:26", "", "是", "未簽核");
            dt = wf.GetFlowLog(doc);
            UltraWebGridFlowLog.DataSource = dt;
            UltraWebGridFlowLog.DataBind();
        }
        //數據初始化
        private void Internationalization()
        {
            UltraWebGridFlowLog.Bands[0].Columns.FromKey("start_time").Format = "yyyy-MM-dd HH:mm:ss";
            UltraWebGridFlowLog.Bands[0].Columns.FromKey("end_time").Format = "yyyy-MM-dd HH:mm:ss";
        }
    }
}
