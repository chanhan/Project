/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： AuthorityForm.cs
 * 檔功能描述： 角色功能模組子頁面角色授權UI類
 * 
 * 版本：1.0
 * 創建標識： 陈函 2011.12.7
 * 
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web.Script.Serialization;
using System.Web.UI;
using GDSBG.MiABU.Attendance.BLL.SystemManage.SystemSafety;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;
using Infragistics.WebUI.UltraWebNavigator;
using Resources;
using System.Web.UI.WebControls;

namespace GDSBG.MiABU.Attendance.Web.SystemManage.SystemSafety
{
    public partial class AuthorityForm : BasePage
    {
        ModuleBll moduleBll = new ModuleBll();
        RoleBll roleBll = new RoleBll();
        AuthorityBll authorityBll = new AuthorityBll();
        JavaScriptSerializer JsSerializer = new JavaScriptSerializer();
        Dictionary<string, string> ClientMessage = null;
        string roleCode = "";
        static SynclogModel logmodel = new SynclogModel();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (ClientMessage == null)
            {
                ClientMessage = new Dictionary<string, string>();
                ClientMessage.Add("ModuleEditCheck", Message.ModuleEditCheck);
                ClientMessage.Add("EnableAndSaveRole", Message.EnableAndSaveRole);

            }
            string clientmsg = JsSerializer.Serialize(ClientMessage);
            Page.ClientScript.RegisterStartupScript(GetType(), "ClientMessage", "var Message=" + clientmsg + ";", true);


            if (Request.QueryString["rolecode"] != null)
            {
                roleCode = Request.QueryString["rolecode"].ToString();
                hidRoleCode.Value = roleCode;
                AuthorityModel authorityModel = new AuthorityModel();
                authorityModel.RoleCode = roleCode;
                //if (authorityBll.GetAuthority(authorityModel).Rows.Count==0)
                //{
                //    btnSave.Enabled = false;
                //}

                //獲取此角色是否已經失效
                this.DisableInfo.Value = roleBll.GetRoleByKey(roleCode).Deleted;
                
            }
            else
            {
                btnSave.Enabled = false;
            }
            if (!IsPostBack)
            {
                logmodel.ProcessOwner = CurrentUserInfo.Personcode;
                logmodel.TransactionType = Request.QueryString["modulecode"] == null ? "" : Request.QueryString["modulecode"].ToString();
                logmodel.LevelNo = "2";
                logmodel.FromHost = Request.UserHostAddress;

                LoadMoudelTree(roleCode);
            }
        }

        private string CreateFunctionList(string strFunctionList, string strFunctionListed, string strAuthorized)
        {
            string SValue = "";
       //     if ((strFunctionList.Length <= 0) || !strAuthorized.Equals("Y"))
                if ((strFunctionList.Length <= 0))

            {
                return SValue;
            }
            if (strFunctionListed.Length > 0)
            {
                int i;
                SortedList SortedFunctionListed = new SortedList();
                string[] arrB = strFunctionListed.Split(new char[] { ',' });
                for (i = 0; i < arrB.Length; i++)
                {
                    SortedFunctionListed.Add(SortedFunctionListed.Count, arrB[i]);
                }
                string[] arrA = strFunctionList.Split(new char[] { ',' });
                for (i = 0; i < arrA.Length; i++)
                {
                    if (SortedFunctionListed.IndexOfValue(arrA[i].ToString()) >= 0)
                    {
                        SValue = SValue + "<font color=red>" + arrA[i].ToString() + "</font>,";
                    }
                    else
                    {
                        SValue = SValue + "<font color=grey>" + arrA[i].ToString() + "</font>,";
                    }
                }
                return ("(" + SValue.TrimEnd(new char[] { ',' }) + ")");
            }
            return ("(<font color=grey>" + strFunctionList.TrimEnd(new char[] { ',' }) + "</font>)");
        }

        public void LoadMoudelTree(string roleCode)
        {
            DataTable tempTable = roleBll.GetUserModuleListByRoleCode(roleCode);
            this.UltraWebTreeModule.Nodes.Clear();
            SortedList allTreeNodes = new SortedList();
            foreach (DataRow row in tempTable.Rows)
            {
                Node newNode = base.CreateNode(Convert.ToString(row["modulecode"]), FunctionText.ResourceManager.GetString(row["LANGUAGE_KEY"].ToString()) + this.CreateFunctionList(Convert.ToString(row["FunctionList"]), Convert.ToString(row["FunctionListed"]), Convert.ToString(row["Authorized"])), Convert.ToString(row["authorized"]).Equals("Y"), Convert.ToDecimal(tempTable.Compute("count(modulecode)", "parentmodulecode='" + row["modulecode"] + "'")) == 0M);
                allTreeNodes.Add(Convert.ToString(row["modulecode"]), newNode);
                if (row["parentmodulecode"].ToString().Trim().Length > 0)
                {
                    if (allTreeNodes.IndexOfKey(row["parentmodulecode"]) >= 0)
                    {
                        ((Node)allTreeNodes.GetByIndex(allTreeNodes.IndexOfKey(row["parentmodulecode"]))).Nodes.Add((Node)allTreeNodes.GetByIndex(allTreeNodes.IndexOfKey(row["modulecode"])));
                    }
                }
                else
                {
                    this.UltraWebTreeModule.Nodes.Add((Node)allTreeNodes.GetByIndex(allTreeNodes.IndexOfKey(Convert.ToString(row["modulecode"]))));
                }
                foreach (Node node in this.UltraWebTreeModule.Nodes)
                {
                    node.Expand(true);

                }
            }
        }
        /// <summary>
        /// 保存角色模組關聯
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            StringBuilder moduleList = new StringBuilder();
            string[] mouFun = hidFuncList.Value.Split('§');
            int nM = mouFun.Length;

            //判定父節點是否應選中  2012.02.17  F3228823
            foreach (Node node in UltraWebTreeModule.Nodes)
            {
                ParaentNodeCheck(node);
            }



            foreach (Node node in UltraWebTreeModule.CheckedNodes)
            {

               string funcList ;
                for (int m = 0; m < nM; m++)
                {
                    if (node.Tag.ToString() == mouFun[m].Split('|')[0])
                    {
                        moduleList.Append(node.Tag.ToString()+"|");
                        funcList = mouFun[m].Split('|')[1] + "§";
                        moduleList.Append(funcList);
                    }
                    //if (m == (nM - 1))
                    //{
                    //    funcList = funcList + "§";
                    //    moduleList.Append(funcList);
                    //}
                }
            }
            foreach (Node node in UltraWebTreeModule.CheckedNodes)
            {

                hidSelectModuleCode.Value += node.Tag.ToString() + "§";

            }
            bool blSave = authorityBll.SaveRoleModule(moduleList.ToString(), roleCode, CurrentUserInfo.Personcode, logmodel, hidSelectModuleCode.Value);
            string alert = "";
            if (blSave)
            {
                alert = "alert('" + Message.SaveSuccess + "');";
            }
            else
            {
                alert = "alert('" + Message.SaveFailed + "');";
            }
            hidFuncList.Value = "";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "saveRoleModule", alert, true);
            LoadMoudelTree(roleCode);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "reload", "window.parent.location.href=window.parent.location.href;", true);
        }



        /// <summary>
        /// 判斷該父節點是否應被選中 2012.02.17  F3228823
        /// </summary>
        /// <param name="node"></param>
        protected void ParaentNodeCheck(Node node)
        {
            if (node.FirstNode != null)
            {
                if (node.FirstNode.FirstNode != null)
                {
                    foreach (Node nodeSub in node.Nodes)
                    {
                        ParaentNodeCheck(nodeSub);
                    }
                }
                Node nodeFlag = node.FirstNode;
                if (nodeFlag.Checked == true)
                {
                    node.Checked = true;
                }
                else
                {
                    int checkNum = 0;
                    while (nodeFlag.NextNode != null)
                    {
                        if (nodeFlag.NextNode.Checked == true)
                        {
                            checkNum++;
                        }
                        nodeFlag = nodeFlag.NextNode;
                    }
                    node.Checked = checkNum >= 1 ? true : false;
                }
                while (node.Parent != null)
                {
                    node.Parent.Checked = node.Checked == true ? true : false;
                    node = node.Parent;
                }
            }
        }
    }



}
