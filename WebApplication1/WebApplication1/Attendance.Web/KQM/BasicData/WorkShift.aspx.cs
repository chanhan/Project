/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： WorkShift.cs
 * 檔功能描述： 班別定義UI層
 * 
 * 版本：1.0
 * 創建標識： 高子焱 2012.01.09
 * 
 */
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using GDSBG.MiABU.Attendance.BLL.KQM.BasicData;
using GDSBG.MiABU.Attendance.BLL.SystemManage.SystemData;
using GDSBG.MiABU.Attendance.Common;
using GDSBG.MiABU.Attendance.Model.KQM.BasicData;
using Infragistics.WebUI.UltraWebGrid;
using Resources;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;
namespace GDSBG.MiABU.Attendance.Web.KQM.BasicData
{
    public partial class WorkShift : BasePage
    {
        TypeDataBll bllData = new TypeDataBll();
        WorkShiftBll bll = new WorkShiftBll();
        ParameterBll PraBll = new ParameterBll();
        WorkShiftModel model = new WorkShiftModel();
        JavaScriptSerializer JsSerializer = new JavaScriptSerializer();
        Dictionary<string, string> ClientMessage = null;
        static DataTable dtSelectAll = new DataTable();
        static SynclogModel logmodel = new SynclogModel();
        #region 頁面加載
        protected void Page_Load(object sender, EventArgs e)
        {
            hidSetFlag.Value = "Success";
            PageHelper.ButtonControls(base.FuncList, pnlShowPanel.Controls, base.FuncListModule);
            SetCalendar(txtEffectDate, txtExpireDate);
            if (!IsPostBack)
            {
                logmodel.ProcessOwner = CurrentUserInfo.Personcode;
                logmodel.TransactionType = Request.QueryString["ModuleCode"];
                logmodel.LevelNo = "2";
                logmodel.FromHost = Request.UserHostAddress;
                this.DropDownListBind(this.ddlShiftNoType, "KqmWorkShiftType", true);
                this.DropDownListBind(this.ddlShiftType, "ShiftType", false);
                pager.CurrentPageIndex = 1;
                SetSelector(imgDepCode, txtOrgCode, txtDepName, Request.QueryString["ModuleCode"].ToString());
                SetSelector(imgShareDepcode, txtShareDepcode, txtShareOrgName, Request.QueryString["ModuleCode"].ToString());
                this.ddlIsLactation.Items.Insert(0, new ListItem("", ""));
                this.ddlIsLactation.Items.Insert(1, new ListItem("N", "N"));
                this.ddlIsLactation.Items.Insert(2, new ListItem("Y", "Y"));
                dataBind();
            }
            if (ClientMessage == null)
            {
                ClientMessage = new Dictionary<string, string>();
                ClientMessage.Add("PMMoreThanOneH", Message.PMMoreThanOneH);
                ClientMessage.Add("OnlyNightTwoDays", Message.OnlyNightTwoDays);
                ClientMessage.Add("NightMustTwoDays", Message.NightMustTwoDays);
                ClientMessage.Add("PMMoreThanFiveH", Message.PMMoreThanFiveH);
                ClientMessage.Add("WorkMoreThanEightH", Message.WorkMoreThanEightH);
                ClientMessage.Add("WorkMustEightH", Message.WorkMustEightH);
                ClientMessage.Add("CantChangToLac", Message.CantChangToLac);
                ClientMessage.Add("PMLaterThanOffTime", Message.PMLaterThanOffTime);
                ClientMessage.Add("MustBwtOnTime", Message.MustBwtOnTime);
                ClientMessage.Add("AtLastOneChoose", Message.AtLastOneChoose);
                ClientMessage.Add("TextBoxNotNull", Message.TextBoxNotNull);
                ClientMessage.Add("DeleteConfirm", Message.DeleteConfirm);
                ClientMessage.Add("SaveConfirm", Message.SaveConfirm);
                ClientMessage.Add("ShiftIsUsedNoDelete", Message.ShiftIsUsedNoDelete);
                ClientMessage.Add("WrongDate", Message.WrongDate);
                ClientMessage.Add("ToLaterThanFrom", Message.ToLaterThanFrom);
                ClientMessage.Add("CanntLowerThanIsLan", Message.CanntLowerThanIsLan);

            }
            string clientmsg = JsSerializer.Serialize(ClientMessage);
            Page.ClientScript.RegisterStartupScript(GetType(), "ClientMessage", "var Message=" + clientmsg + ";", true);
        }
        #endregion
        #region 查詢代碼
        private void dataBind()
        {
            string activeFlag = hidOperate.Value.ToString().Trim();
            string orderType = "";
            string fromDate = "";
            string toDate = "";
            int totalCount;
            //if (activeFlag == "Query" || activeFlag == "" || string.IsNullOrEmpty(activeFlag))
            //{
            //    orderType = "1";
            //}
            //else if (activeFlag == "Edit")
            //{
            //    orderType = "2";
            //}
            string SQLDep = base.SqlDep;
            string deptCode = model.OrgCode;
            model.OrgCode = null;
            model.OrgName = null;
            string shiftNo = ddlShiftNoType.SelectedValue;
            //string fromDate = this.txtEffectDate.Text.Trim();
            //string toDate = this.txtExpireDate.Text.Trim();
            DataTable dt = bll.GetWorkShiftList(model, deptCode, orderType, SqlDep, fromDate, toDate, pager.CurrentPageIndex, pager.PageSize, out totalCount);
            dtSelectAll = bll.GetWorkShiftList(model, deptCode, SQLDep, fromDate, toDate);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["OnDutyTime"].ToString() == "00:01")
                {
                    dt.Rows[i]["OnDutyTime"] = "00:00";
                }
                if (dt.Rows[i]["OffDutyTime"].ToString() == "23:59")
                {
                    dt.Rows[i]["OffDutyTime"] = "00:00";
                }
            }
            pager.RecordCount = totalCount;
            UltraWebGrid.DataSource = dt;
            UltraWebGrid.DataBind();
            pager.TextAfterPageIndexBox = "/" + pager.PageCount.ToString();
        }
        #endregion
        #region 綁定下拉框的值
        protected void DropDownListBind(DropDownList List, string DataTypeValue, bool js)
        {
            DataTable dt = new DataTable();
            dt = bllData.GetTypeDataList(DataTypeValue);
            List.DataSource = dt;
            List.DataTextField = "DataValue";
            List.DataValueField = "DataCode";
            List.DataBind();
            List.Items.Insert(0, new ListItem("", ""));
            if (js)
            {
                this.hidShiftNoType1.Value = List.Items[1].ToString();
                this.hidShiftNoType2.Value = List.Items[2].ToString();
                this.hidShiftNoType3.Value = List.Items[3].ToString();
            }

        }
        #endregion
        #region 查詢
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            if (this.hidOperate.Value == "Condition")
            {
                ProcessFlag.Value = "Condition";
                model = PageHelper.GetModel<WorkShiftModel>(pnlContent.Controls, txtShiftDesc, txtRemark, txtEffectDate, txtExpireDate);
            }
            else
            {
                model = new WorkShiftModel();
            }
            pager.CurrentPageIndex = 1;
            dataBind();
            hidOperate.Value = "";
        }
        #endregion
        #region 分頁
        protected void pager_PageChanged(object sender, EventArgs e)
        {
            if (this.ProcessFlag.Value == "Condition")
            {
                model = PageHelper.GetModel<WorkShiftModel>(pnlContent.Controls, txtShiftDesc, txtRemark, txtEffectDate, txtExpireDate);
            }
            else
            {
                model = new WorkShiftModel();
            }
            dataBind();
        }
        #endregion
        #region 放大鏡綁定
        /// <summary>
        /// 設置Selector
        /// </summary>
        /// <param name="ctrlTrigger">控件ID--按鈕</param>
        /// <param name="ctrlCode">控件ID--文本框1</param>
        /// <param name="ctrlName">控件ID--文本框2</param>
        public void SetSelector(WebControl ctrlTrigger, Control ctrlCode, Control ctrlName, string moduleCode)
        {
            if (ctrlCode is TextBox) { (ctrlCode as TextBox).Attributes.Add("readonly", "readonly"); }
            if (ctrlName is TextBox) { (ctrlName as TextBox).Attributes.Add("readonly", "readonly"); }
            ctrlTrigger.Attributes.Add("onclick", string.Format("return setSelector('{0}','{1}','{2}')",
                ctrlCode.ClientID, ctrlName.ClientID, moduleCode));
        }
        #endregion
        #region 保存事件
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string activeFlag = hidOperate.Value.ToString().Trim();
            hidOperate.Value = "Edit";
            if (!string.IsNullOrEmpty(txtOrgCode.Text))
            {
                BUCalendarBll bllBU = new BUCalendarBll();
                string LevelCode = bllBU.GetValue(txtOrgCode.Text.ToString());
                int DepLevel = 10;
                if (CurrentUserInfo.DepLevel.ToString().Length > 0)
                {
                    DepLevel = Convert.ToInt32(CurrentUserInfo.DepLevel.ToString());
                }
                switch (LevelCode)
                {
                    case "0":
                        if (DepLevel <= 0)
                        {
                            break;
                        }
                        Page.ClientScript.RegisterStartupScript(GetType(), "show", "alert('" + Message.NoAuthority + "')", true);
                        hidSetFlag.Value = "Fail";
                        return;

                    case "1":
                        if (DepLevel <= 1)
                        {
                            break;
                        }
                        Page.ClientScript.RegisterStartupScript(GetType(), "show", "alert('" + Message.NoAuthority + "')", true);
                        hidSetFlag.Value = "Fail";
                        return;

                    case "2":
                        if (DepLevel <= 2)
                        {
                            break;
                        }
                        Page.ClientScript.RegisterStartupScript(GetType(), "show", "alert('" + Message.NoAuthority + "')", true);
                        hidSetFlag.Value = "Fail";
                        return;

                    case "3":
                        if ((DepLevel <= 3) || (DepLevel == 7))
                        {
                            break;
                        }
                        Page.ClientScript.RegisterStartupScript(GetType(), "show", "alert('" + Message.NoAuthority + "')", true);
                        hidSetFlag.Value = "Fail";
                        return;

                    case "4":
                        if ((DepLevel <= 3) || (DepLevel == 7))
                        {
                            break;
                        }
                        Page.ClientScript.RegisterStartupScript(GetType(), "show", "alert('" + Message.NoAuthority + "')", true);
                        hidSetFlag.Value = "Fail";
                        return;

                    case "5":
                        if ((DepLevel <= 5) || (DepLevel == 7))
                        {
                            break;
                        }
                        Page.ClientScript.RegisterStartupScript(GetType(), "show", "alert('" + Message.NoAuthority + "')", true);
                        hidSetFlag.Value = "Fail";
                        return;

                    case "6":
                        if ((DepLevel <= 6) || (DepLevel == 7))
                        {
                            break;
                        }
                        Page.ClientScript.RegisterStartupScript(GetType(), "show", "alert('" + Message.NoAuthority + "')", true);
                        hidSetFlag.Value = "Fail";
                        return;

                    case "7":
                        if (DepLevel <= 7)
                        {
                            break;
                        }
                        Page.ClientScript.RegisterStartupScript(GetType(), "show", "alert('" + Message.NoAuthority + "')", true);
                        hidSetFlag.Value = "Fail";
                        return;

                    case "8":
                        if (DepLevel <= 8)
                        {
                            break;
                        }
                        Page.ClientScript.RegisterStartupScript(GetType(), "show", "alert('" + Message.NoAuthority + "')", true);
                        hidSetFlag.Value = "Fail";
                        return;
                }
            }
            if (activeFlag == "Add")
            {
                logmodel.ProcessFlag = "insert";
                WorkShiftModel model = PageHelper.GetModel<WorkShiftModel>(pnlContent.Controls);
                string strMax = "";
                string shiftType = ddlShiftNoType.SelectedValue;
                if (!string.IsNullOrEmpty(ddlShiftType.SelectedValue))
                {
                    strMax = bll.SelectMaxShiftNo(shiftType);
                }
                if (strMax.Length == 0)
                {
                    strMax = shiftType + "001";
                }
                else
                {
                    int i = Convert.ToInt32(strMax.Substring(1)) + 1;
                    if (i < 0x3e8)
                    {
                        strMax = i.ToString().PadLeft(3, '0');
                    }
                    else
                    {
                        strMax = i.ToString();
                    }
                    strMax = shiftType + strMax;
                }
                model.ShiftNo = strMax;
                model.CreateDate = DateTime.Now;
                model.CreateUser = CurrentUserInfo.Personcode;
                model.UpdateDate = DateTime.Now;
                model.UpdateUser = CurrentUserInfo.Personcode;
                if (txtOnDutyTime.Text.ToString().Trim() == "00:00")
                {
                    model.OnDutyTime = "00:01";
                }
                else
                {
                    model.OnDutyTime = txtOnDutyTime.Text.ToString().Trim();
                }
                if (txtOffDutyTime.Text.ToString().Trim() == "00:00")
                {
                    model.OffDutyTime = "23:59";
                }
                else
                {
                    model.OffDutyTime = txtOffDutyTime.Text.ToString().Trim();
                }
                model.AMRestSTime = txtAMRestSTime.Text.ToString().Trim();
                model.AMRestETime = txtAMRestETime.Text.ToString().Trim();
                model.PMRestSTime = txtPMRestSTime.Text.ToString().Trim();
                model.PMRestETime = txtPMRestETime.Text.ToString().Trim();
                model.PMRestQty = Convert.ToDecimal((this.NEHours3.Text.Trim().Equals("") ? 0 : this.NEHours3.Value).ToString());
                model.TimeQty = Convert.ToDecimal((this.NETimeQty.Text.Trim().Equals("") ? 0 : this.NETimeQty.Value).ToString());
                int flag = bll.InsertShiftByKey(model, logmodel);
                if (flag > 0)
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "show", "alert('" + Message.AddSuccess + "')", true);
                    hidSetFlag.Value = "Success";
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "show", "alert('" + Message.AddFailed + "')", true);
                }
            }
            if (activeFlag == "Modify")
            {
                logmodel.ProcessFlag = "update";
                WorkShiftModel model = PageHelper.GetModel<WorkShiftModel>(pnlContent.Controls);
                model.CreateDate = DateTime.Now;
                model.CreateUser = CurrentUserInfo.Personcode;
                model.UpdateDate = DateTime.Now;
                model.UpdateUser = CurrentUserInfo.Personcode;
                if (txtOnDutyTime.Text.ToString().Trim() == "00:00")
                {
                    model.OnDutyTime = "00:01";
                }
                else
                {
                    model.OnDutyTime = txtOnDutyTime.Text.ToString().Trim();
                }
                if (txtOffDutyTime.Text.ToString().Trim() == "00:00")
                {
                    model.OffDutyTime = "23:59";
                }
                else
                {
                    model.OffDutyTime = txtOffDutyTime.Text.ToString().Trim();
                }
                model.AMRestSTime = txtAMRestSTime.Text.ToString().Trim();
                model.AMRestETime = txtAMRestETime.Text.ToString().Trim();
                model.PMRestSTime = txtPMRestSTime.Text.ToString().Trim();
                model.ShiftType = ddlShiftType.SelectedValue;
                model.IsLactation = ddlIsLactation.SelectedValue;
                model.PMRestETime = txtPMRestETime.Text.ToString().Trim();
                model.PMRestQty = Convert.ToDecimal((this.NEHours3.Text.Trim().Equals("") ? 0 : this.NEHours3.Value).ToString());
                model.TimeQty = Convert.ToDecimal((this.NETimeQty.Text.Trim().Equals("") ?0 : this.NETimeQty.Value).ToString());
                int flag = bll.UpdateShiftByKey(model, logmodel);
                if (flag > 0)
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "show", "alert('" + Message.UpdateSuccess + "')", true);
                    hidSetFlag.Value = "Success";
                    //ClearControls();

                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "show", "alert('" + Message.UpdateFailed + "')", true);
                }
            }
            pager.CurrentPageIndex = 1;
            dataBind();
            hidOperate.Value = "";
        }
        #endregion
        #region 清空所有控件的值
        private void ClearControls()
        {
            PageHelper.CleanControlsValue(pnlContent.Controls);
            txtOnDutyTime.Text = null;
            txtOffDutyTime.Text = null;
            txtAMRestSTime.Text = null;
            txtAMRestETime.Text = null;
            txtPMRestSTime.Text = null;
            txtPMRestETime.Text = null;
            NETimeQty.Text = null;
            NEHours1.Text = null;
            NEHours2.Text = null;
            NEHours3.Text = null;
        }
        #endregion

        #region 刪除事件
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string alert = "";
            string actionFlag = this.hidOperate.Value.ToString().Trim();
            //刪除
            if (actionFlag == "Delete")
            {
                logmodel.ProcessFlag = "delete";
                model.ShiftNo = txtShiftNo.Text.ToString().Trim();
                if (bll.DeleteShiftByKey(model, logmodel) > 0)
                {
                    alert = "alert('" + Message.DeleteSuccess + "')";
                }
                else
                {
                    alert = "alert('" + Message.DeleteFailed + "')";
                }
                this.hidOperate.Value = "";
            }
            Page.ClientScript.RegisterStartupScript(GetType(), "show", alert, true);
            pager.CurrentPageIndex = 1;
            //ClearControls();
            model = new WorkShiftModel();
            dataBind();
        }
        #endregion
        #region Ajax事件
        protected override void AjaxProcess()
        {
            bool blPro = false;
            int result = 0;
            if (!string.IsNullOrEmpty(Request.Form["IsLactation"]) && (Request.Form["ActiveFlag"] == "Modify" || Request.Form["ActiveFlag"] == "Add"))
            {
                string personCode = CurrentUserInfo.Personcode;
                DataTable dtDepLevel = bll.GetDepLevel(personCode);
                DataTable dt = PraBll.GetTypeDataDayWork();
                DataTable dtEfefctDay = bll.GetTypeDay(Request.Form["ShiftNo"]);
                double WorkOTRestHours = Convert.ToDouble(dt.Rows[0]["WorkOTRestHours"]);
                double DayWorkHoursLactation = Convert.ToDouble(dt.Rows[0]["upperlimithourslactation"]);
                double DayWorkHours = Convert.ToDouble(dt.Rows[0]["DayWorkHours"]);
                double DayWorkLowerLimitHoursLactation = Convert.ToDouble(dt.Rows[0]["lowerlimithourslactation"]);
                if (Convert.ToInt32(dtDepLevel.Rows[0]["deplevel"]) != 0)
                {
                    if (Request.Form["IsLactation"] == "Y")
                    {
                        if (Convert.ToDouble(Request.Form["NETimeQty"]) > DayWorkHoursLactation)
                        {
                            result = 1;//彈窗不能超過上限時間
                        }
                        if (Convert.ToDouble(Request.Form["NETimeQty"]) < DayWorkLowerLimitHoursLactation)
                        {
                            result = 4;//不能小於最小時間
                        }
                        if ((Request.Form["ActiveFlag"] == "Modify" && Request.Form["IsHidLactation"] == "N") && (Convert.ToDouble(dtEfefctDay.Rows[0]["v_num"]) > 0.0))
                        {
                            result = 3;
                        }
                    }
                    else if (Convert.ToDouble(Request.Form["NETimeQty"]) != DayWorkHours)
                    {
                        result = 2;//普通用戶管控必須等於8小時
                    }
                }
                else
                {
                    if (Request.Form["IsLactation"] == "Y")
                    {
                        if (Convert.ToDouble(Request.Form["NETimeQty"]) < DayWorkHoursLactation)
                        {
                            result = 1;//彈窗不能超過上限時間
                        }
                        else
                        {
                            result = 0;
                        }
                        if ((Request.Form["ActiveFlag"] == "Modify" && Request.Form["IsHidLactation"] == "N") && (Convert.ToDouble(dtEfefctDay.Rows[0]["v_num"]) > 0.0))
                        {
                            result = 3;
                        }
                    }
                    else
                    {
                        result = 0;
                    }
                }
                blPro = true;
            }
            if (Request.Form["ActiveFlag"] == "Delete" && !string.IsNullOrEmpty(Request.Form["ShiftNo"]))
            {
                DataTable dtShift = bll.GetTypeShift(Request.Form["ShiftNo"]);
                for (int i = 0; i < dtShift.Rows.Count; i++)
                {
                    if (Convert.ToDouble(dtShift.Rows[i]["tmp"]) > 0.0)
                    {
                        result = 0;
                    }
                    else
                    {
                        result = 1;
                    }
                }
                blPro = true;
            }
            if (blPro)
            {
                Response.Clear();
                Response.Write(result.ToString());
                Response.End();
            }
        }
        #endregion
        #region WebGrid的綁定時間
        protected void UltraWebGrid_DataBound(object sender, EventArgs e)
        {
            for (int i = 0; i < this.UltraWebGrid.Rows.Count; i++)
            {
                if ((this.UltraWebGrid.Rows[i].Cells.FromKey("ExpireDate").Value != null) && (Convert.ToDateTime(this.UltraWebGrid.Rows[i].Cells.FromKey("ExpireDate").Value) < DateTime.Now))
                {
                    this.UltraWebGrid.Rows[i].Style.ForeColor = Color.Red;
                }
            }
        }
        #endregion

        #region 導出報表
        protected void btnExport_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dtSelectAll.Rows.Count; i++)
            {
                if (dtSelectAll.Rows[i]["OnDutyTime"].ToString() == "00:01")
                {
                    dtSelectAll.Rows[i]["OnDutyTime"] = "00:00";
                }
                if (dtSelectAll.Rows[i]["OffDutyTime"].ToString() == "23:59")
                {
                    dtSelectAll.Rows[i]["OffDutyTime"] = "00:00";
                }
            }
            List<WorkShiftModel> list = bll.GetList(dtSelectAll);
            string[] header = { ControlText.gvShiftNo, ControlText.gvShiftTypeName, ControlText.gvIsLactation, ControlText.gvShiftDesc, ControlText.gvOnDutyTime, ControlText.gvOffDutyTime, ControlText.gvAMRestSTime, ControlText.gvAMRestETime, ControlText.gvPMRestSTime, ControlText.gvPMRestETime, ControlText.gvPMRestQty, ControlText.gvOrgName, ControlText.gvTimeQty, ControlText.gvEffectDate, ControlText.gvExpireDate, ControlText.gvModifier, ControlText.gvModifyDate, ControlText.gvRemark, ControlText.gvShareOrgCode };
            string[] properties = { "ShiftNo", "ShiftTypeName", "IsLactation", "ShiftDesc", "OnDutyTime", "OffDutyTime", "AMRestSTime", "AMRestETime", "PMRestSTime", "PMRestETime", "PMRestQty", "OrgName", "TimeQty", "EffectDate", "ExpireDate", "UpdateUser", "UpdateDate", "Remark", "ShareDepcode" };
            string filePath = MapPath("~/ExportFileTemp/") + DateTime.Now.Ticks + ".xls";
            NPOIHelper.ExportExcel(list, header, properties, 5000, filePath);
            PageHelper.ReturnHTTPStream(filePath, true);
        }
        #endregion
    }
}
