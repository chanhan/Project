/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： PersonLevel.cs
 * 檔功能描述： 組織層級設定UI層
 * 
 * 版本：1.0
 * 創建標識： 高子焱 2012.01.09
 * 
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using GDSBG.MiABU.Attendance.BLL.SystemManage.SystemSafety;
using Infragistics.WebUI.UltraWebNavigator;
using Resources;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;

namespace GDSBG.MiABU.Attendance.Web.SystemManage.SystemSafety
{
    public partial class PersonLevel : BasePage
    {
        protected DataTable dataTable1;
        protected DataTable dataTable2;
        private string PersonCode;
        protected DataRow row;
        protected UltraWebTree UltraWebTreeDeplevel;
        PersonLevelBll levelbll = new PersonLevelBll();

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                logmodel.ProcessFlag = "update";
                Hashtable levels = new Hashtable();
                int i = 0;
                foreach (Node node in this.UltraWebTreeDeplevel.CheckedNodes)
                {
                    i++;
                    levels.Add(i, node.Tag);
                }
                int flag = levelbll.UpdateDeptByKey(PersonCode, levels,logmodel);
                this.QueryData();
                if (flag > 0)
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "show", "alert('" + Message.UpdateSuccess + "')", true);
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "show", "alert('" + Message.UpdateFailed + "')", true);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        JavaScriptSerializer JsSerializer = new JavaScriptSerializer();
        Dictionary<string, string> ClientMessage = null;
        static SynclogModel logmodel = new SynclogModel();
        protected void Page_Load(object sender, EventArgs e)
        {
            PersonCode = Request.QueryString["PersonCode"];
            if (ClientMessage == null)
            {
                ClientMessage = new Dictionary<string, string>();
                ClientMessage.Add("UpdateSuccess", Message.UpdateSuccess);
                ClientMessage.Add("UpdateFailed", Message.UpdateFailed);
            }
            string clientmsg = JsSerializer.Serialize(ClientMessage);
            Page.ClientScript.RegisterStartupScript(GetType(), "ClientMessage", "var Message=" + clientmsg + ";", true);
            if (!IsPostBack)
            {
                logmodel.ProcessOwner = CurrentUserInfo.Personcode;
                logmodel.TransactionType = Request.QueryString["ModuleCode"];
                logmodel.LevelNo = "2";
                logmodel.FromHost = Request.UserHostAddress;
                this.QueryData();
                this.btnSave.Attributes.Add("onclick", "return confirm('您確定要更新該員工的組織層級信息？')");
            }
        }
        private void QueryData()
        {
            Node newNode;
            this.UltraWebTreeDeplevel.Nodes.Clear();
            //按照員工號查找員工對應的組織信息
            this.dataTable1 = levelbll.GetDeptDataTableByKey(PersonCode);
            //將查詢到得員工對應組織信息綁定在treeview上面
            if (dataTable1 != null)
            {
                foreach (DataRow row in dataTable1.Rows)
                {
                    newNode = base.CreateNode(Convert.ToString(row["levelcode"]), Convert.ToString(row["levelname"]), true, true);
                    this.UltraWebTreeDeplevel.Nodes.Add(newNode);
                }
            }
            //將員工信息中沒有的也顯示在treeview中
            this.dataTable2 = levelbll.GetDeptUncheckedByKey(PersonCode);
            if (dataTable2 != null)
            {
                foreach (DataRow row in dataTable2.Rows)
                {
                    newNode = base.CreateNode(Convert.ToString(row["levelcode"]), Convert.ToString(row["levelname"]), false, true);
                    this.UltraWebTreeDeplevel.Nodes.Add(newNode);
                }
            }
            this.UltraWebTreeDeplevel.ExpandAll();
        }
    }
}
