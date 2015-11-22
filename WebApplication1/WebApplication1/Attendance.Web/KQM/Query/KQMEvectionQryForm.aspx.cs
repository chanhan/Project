/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： KQMEvectionQryFormBll.cs
 * 檔功能描述：出差明細查詢功能模組UI類
 * 
 * 版本：1.0
 * 創建標識： 陈函 2011.12.30
 * 
 */
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Web.UI.WebControls;
using GDSBG.MiABU.Attendance.BLL.HRM.EmployeeData;
using GDSBG.MiABU.Attendance.BLL.KQM.Query;
using GDSBG.MiABU.Attendance.Model.KQM.Query;
using Resources;

namespace GDSBG.MiABU.Attendance.Web.KQM.Query
{
    public partial class KQMEvectionQryForm : BasePage
    {
        HrmEmpOtherMoveBll hrmEmpOtherMoveBll = new HrmEmpOtherMoveBll();
        KQMEvectionQryFormBll kQMEvectionQryFormBll = new KQMEvectionQryFormBll();
        bool Privileged = true;
        string strModuleCode;
        protected void Page_Load(object sender, EventArgs e)
        {
            PageHelper.ButtonControls(FuncList, pnlShowPanel.Controls, base.FuncListModule);
            strModuleCode = Request.QueryString["ModuleCode"] == null ? "" : Request.QueryString["ModuleCode"].ToString();
            SetCalendar(txtStartDate, txtEndDate);
            if (!base.IsPostBack)
            {
                IsHavePrivileged();
                pager.CurrentPageIndex = 1;
                DataBind();
                InitDropDownList();
                this.ImageDepCode.Attributes.Add("onclick", "javascript:GetTreeDataValue(\"txtDcode\",'" + strModuleCode + "','txtDepName')");
            }
        }
        #region  是否有組織權限
        /// <summary>
        /// 是否有組織權限
        /// </summary>
        private void IsHavePrivileged()
        {

            if (CurrentUserInfo.Personcode.Equals("internal") || CurrentUserInfo.RoleCode.Equals("Person"))
            {
                Privileged = false;
            }
            else
            {
                DataTable dt = hrmEmpOtherMoveBll.GetDataByCondition(strModuleCode);
                if ((dt != null) && (dt.Rows.Count > 0))
                {
                    Privileged = false;
                }
            }
        }
        #endregion

        #region 綁定DropDownList出差類別、表單狀態
        /// <summary>
        /// 綁定DropDownList出差類別、表單狀態
        /// </summary>
        private void InitDropDownList()
        {
            this.ddlEvectiontype.DataSource = DropDownListDataBound("EvectionType");
            this.ddlEvectiontype.DataTextField = "DataValue";
            this.ddlEvectiontype.DataValueField = "DataCode";
            this.ddlEvectiontype.DataBind();
            this.ddlEvectiontype.Items.Insert(0, new ListItem("", ""));
            this.ddlStatus.DataSource = DropDownListDataBound("EvectionApplyState");
            this.ddlStatus.DataTextField = "DataValue";
            this.ddlStatus.DataValueField = "DataCode";
            this.ddlStatus.DataBind();
            this.ddlStatus.Items.Insert(0, new ListItem("", ""));
        }
        #endregion

        #region 數據綁定
        /// <summary>
        /// 數據綁定
        /// </summary>
        private void DataBind()
        {
            int totalCount = 0;

            DataTable dt = kQMEvectionQryFormBll.SelectEvectionData(Privileged, SqlDep, txtDepName.Text.Trim(), txtBillno.Text.Trim(), txtWorkno.Text.Trim().ToUpper(), txtLocalname.Text.Trim(), ddlEvectiontype.SelectedValue.ToString(), ddlStatus.SelectedValue.ToString(), txtStartDate.Text.Trim(), txtEndDate.Text.Trim(), txtEvectionaddress.Text.ToString(), pager.CurrentPageIndex, pager.PageSize, out totalCount);
            this.UltraWebGridEvectionApply.DataSource = dt;
            pager.RecordCount = totalCount;
            this.UltraWebGridEvectionApply.DataBind();
            pager.TextAfterPageIndexBox = "/" + pager.PageCount.ToString();
        }
        #endregion

        #region 查詢DropDownList出差類別、表單狀態
        /// <summary>
        ///查詢DropDownList出差類別、表單狀態
        /// </summary>
        /// <param name="dataType"></param>
        /// <returns></returns>
        private DataTable DropDownListDataBound(string dataType)
        {
            return kQMEvectionQryFormBll.getData(dataType);
        }
        #endregion

        #region 查询
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            pager.CurrentPageIndex = 1;
            DataBind();
            PageHelper.CleanControlsValue(inputPanel.Controls);
        }
        #endregion

        #region 導出
        /// <summary>
        /// 導出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnExport_Click(object sender, EventArgs e)
        {
            List<EvectionApplyVModel> list = kQMEvectionQryFormBll.SelectEvectionData(Privileged, SqlDep,  txtDepName.Text.Trim(), txtBillno.Text.Trim(), txtWorkno.Text.Trim().ToUpper(), txtLocalname.Text.Trim(), ddlEvectiontype.SelectedValue.ToString(), ddlStatus.SelectedValue.ToString(), txtStartDate.Text.Trim(), txtEndDate.Text.Trim(), txtEvectionaddress.Text.ToString());
            string[] header = { ControlText.gvHeaderWorkNo, ControlText.gvHeaderLocalName, 
                                  ControlText.gvHeaderSex, ControlText.gvHeaderBUName, 
                                  ControlText.gvHeaderDepName, ControlText.gvHeaderEvectionTypeName, 
                                   ControlText.gvHeaderStartDate, ControlText.gvHeaderEndDate,
                                   ControlText.gvHeaderEvectionTask, ControlText.gvHeaderEvectionDetail, 
                                   ControlText.gvHeaderEvectionAddress, ControlText.gvHeaderProxy, 
                                   ControlText.gvHeaderRemark, ControlText.gvHeaderStatusName, 
                                   ControlText.gvHeaderModifier,ControlText.gvHeaderModifyDate};
            string[] properties = { "Workno", "Localname", "Sex", "Buname", "Depname", "Evectiontypename",
                                      "Startdatestr", "Enddatestr", "Evectiontask", "Evectiondetail","Evectionaddress","Proxy","Remark","Statusname","Modifier","Modifydatestr"};
            string filePath = MapPath("~/ExportFileTemp/") + DateTime.Now.Ticks + ".xls";
            NPOIHelper.ExportExcel(list, header, properties, 5000, filePath);
            PageHelper.ReturnHTTPStream(filePath, true);
        }
        #endregion

        #region 分頁
        /// <summary>
        /// 分頁
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void pager_PageChanged(object sender, EventArgs e)
        {
            DataBind();
        }
        #endregion

        #region UltraWebGrid數據綁定
        protected void UltraWebGridEvectionApply_DataBound(object sender, EventArgs e)
        {
            for (int i = 0; i < this.UltraWebGridEvectionApply.Rows.Count; i++)
            {
                string style = this.UltraWebGridEvectionApply.Rows[i].Cells.FromKey("Status").Text.Trim();
                if (style == null)
                {
                    goto Label_015B;
                }
                if (!(style == "0"))
                {
                    if (style == "1")
                    {
                        goto Label_00BC;
                    }
                    if (style == "3")
                    {
                        goto Label_00F1;
                    }
                    if (style == "4")
                    {
                        goto Label_0126;
                    }
                    goto Label_015B;
                }
                this.UltraWebGridEvectionApply.Rows[i].Cells.FromKey("StatusName").Style.ForeColor = Color.Green;
                continue;
            Label_00BC:
                this.UltraWebGridEvectionApply.Rows[i].Cells.FromKey("StatusName").Style.ForeColor = Color.Blue;
                continue;
            Label_00F1:
                this.UltraWebGridEvectionApply.Rows[i].Cells.FromKey("StatusName").Style.ForeColor = Color.Maroon;
                continue;
            Label_0126:
                this.UltraWebGridEvectionApply.Rows[i].Cells.FromKey("StatusName").Style.ForeColor = Color.Red;
                continue;
            Label_015B:
                this.UltraWebGridEvectionApply.Rows[i].Cells.FromKey("StatusName").Style.ForeColor = Color.Green;
            }
        #endregion
        }
    }
}
