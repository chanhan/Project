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

using GDSBG.MiABU.Attendance.Common;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;
using GDSBG.MiABU.Attendance.BLL.SystemManage.SystemSafety;
using System.Collections.Generic;
using System.Text;
using Infragistics.WebUI.UltraWebNavigator;
using Resources;

namespace GDSBG.MiABU.Attendance.Web
{
    public partial class LeftMenu : BasePage
    {
        ModuleBll moduleBll = new ModuleBll();

        /// <summary>
        /// 當前用戶功能清單
        /// </summary>
        private List<ModuleModel> UserFunctionList
        {
            get
            {
                if (Session[GlobalData.UserFunctionsSessionKey] != null)
                {
                    return Session[GlobalData.UserFunctionsSessionKey] as List<ModuleModel>;
                }
                return null;
            }
            set { Session[GlobalData.UserFunctionsSessionKey] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadMenu();
        }
        /// <summary>
        /// 加載功能模組
        /// </summary>
        private void LoadMenu()
        {
            DataTable tempTable = moduleBll.GetUserModuleList();



            this.UltraWebTreeStandardMenu.Nodes.Clear();
            SortedList allTreeNodes = new SortedList();
            //string SignFlag = ((ServiceLocator)this.Session["serviceLocator"]).GetFunctionData().GetValue("SELECT 1 FROM kqm_signemployee WHERE workno='" + this.Session["appUser"] + "' AND Flag='Y' and startdate<=trunc(sysdate) and enddate>=trunc(sysdate)");
            //bool boolRPM = false;
            //if (!((ServiceLocator)this.Session["serviceLocator"]).GetFunctionData().GetValue("Select count(1) from RPM_Employees where Status='0' and WorkNO='" + base.appUser + "'").Equals("0"))
            //{
            //    boolRPM = true;
            //}
            //bool boolPAM = false;
            //if (!((ServiceLocator)this.Session["serviceLocator"]).GetFunctionData().GetValue("SELECT count(1) from PAM_EmpAssess a,PAM_EmpAssessFlow b where a.Status<>'0' and a.workno=b.workno and a.workno='" + base.appUser + "' and b.assesstype='E' and a.AnnualCode in(SELECT AnnualCode from PAM_ANNUAL where assessstartdate<=trunc(sysdate) and assessenddate>=trunc(sysdate))").Equals("0"))
            //{
            //    boolPAM = true;
            //}
            //bool bPCMSYS29 = !((ServiceLocator)this.Session["serviceLocator"]).GetFunctionData().GetValue("SELECT count(1) from HRM_EmpLeaveDetail a,HRM_Employees b,HRM_EmpLeaveMaster c where b.WorkNo=a.WorkNo and c.BillNo =a.BillNo AND c.status='A00' and a.workno='" + base.appUser + "' ").Equals("0");
            foreach (DataRow row in tempTable.Rows)
            {
                if (((Convert.ToString(row["modulecode"]).Equals("PCMSYS13") || Convert.ToString(row["modulecode"]).Equals("PCMSYS24")) || (Convert.ToString(row["modulecode"]).Equals("PCMSYS26") || Convert.ToString(row["modulecode"]).Equals("PCMSYS29"))) || (((!Convert.ToString(row["modulecode"]).Equals("PCMSYS13") && !Convert.ToString(row["modulecode"]).Equals("PCMSYS24")) && !Convert.ToString(row["modulecode"]).Equals("PCMSYS26")) && !Convert.ToString(row["modulecode"]).Equals("PCMSYS29")))
                {
                    Node newNode = base.CreateNode(Convert.ToString(row["modulecode"]), FunctionText.ResourceManager.GetString(row["LANGUAGE_KEY"].ToString()), false, Convert.ToDecimal(tempTable.Compute("count(modulecode)", "parentmodulecode='" + row["modulecode"] + "'")) == 0M);
                    if (Convert.ToDecimal(tempTable.Compute("count(modulecode)", "parentmodulecode='" + row["modulecode"] + "'")) == 0M)
                    {
                        if (Convert.ToString(row["url"]).ToLower().StartsWith("http") || Convert.ToString(row["url"]).ToLower().StartsWith("ftp"))
                        {
                            newNode.TargetUrl = Convert.ToString(row["url"]);
                            newNode.TargetFrame = "_blank";
                        }
                        else
                        {
                            if (row["url"].ToString().IndexOf("?") >= 0)
                            {
                                newNode.TargetUrl = BasePage.sAppPath + Convert.ToString(row["url"]) + "&ModuleCode=" + Convert.ToString(row["modulecode"]) + "&SqlDep=true";
                            }
                            else
                            {
                                newNode.TargetUrl = BasePage.sAppPath + Convert.ToString(row["url"]) + "?ModuleCode=" + Convert.ToString(row["modulecode"]) + "&SqlDep=true";
                            }
                            newNode.TargetFrame = "fMain";
                        }
                    }
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
                        this.UltraWebTreeStandardMenu.Nodes.Add((Node)allTreeNodes.GetByIndex(allTreeNodes.IndexOfKey(Convert.ToString(row["modulecode"]))));
                    }
                }
            }



            
        }
    }
}
