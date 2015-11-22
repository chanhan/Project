/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： HrmEmpOtherMoveForm.cs
 * 檔功能描述：加班類別異動功能模組UI類
 * 
 * 版本：1.0
 * 創建標識： 陈函 2011.12.23
 * 
 */
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using GDSBG.MiABU.Attendance.BLL.HRM.EmployeeData;
using GDSBG.MiABU.Attendance.Model.HRM.EmployeeData;
using Resources;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;

namespace GDSBG.MiABU.Attendance.Web.HRM.EmployeeData
{
    public partial class HrmEmpOtherMoveEditForm : BasePage
    {
        HrmEmpOtherMoveBll hrmEmpOtherMoveBll = new HrmEmpOtherMoveBll();
        bool Privileged = true;
        string EmployeeNo = "";
        string MoveOrder = "";
        string Process = "";
        string moduleCode = "";
        JavaScriptSerializer JsSerializer = new JavaScriptSerializer();
        Dictionary<string, string> ClientMessage = null;
        DataTable dt;
        static SynclogModel logmodel = new SynclogModel();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (ClientMessage == null)
            {
                ClientMessage = new Dictionary<string, string>();
                ClientMessage.Add("EmpNoNotNull", Message.EmpNoNotNull);
                ClientMessage.Add("MoveTypeNotNull", Message.MoveTypeNotNull);
                ClientMessage.Add("EffDateNotNull", Message.EffDateNotNull);
                ClientMessage.Add("AfterValueNameNotNull", Message.AfterValueNameNotNull);
                ClientMessage.Add("WorkNONotExist", Message.WorkNONotExist);
                ClientMessage.Add("ChooseOtherMoveTypeFirst", Message.ChooseOtherMoveTypeFirst);
                ClientMessage.Add("EffDateNowDate", Message.EffDateNowDate);                
            }
            string clientmsg = JsSerializer.Serialize(ClientMessage);
            Page.ClientScript.RegisterStartupScript(GetType(), "ClientMessage", "var Message=" + clientmsg + ";", true);

            SetCalendar(txtEffectDate);
            IsHavePrivileged();
            if (!base.IsPostBack)
            {
                logmodel.ProcessOwner = CurrentUserInfo.Personcode;
                logmodel.TransactionType = Request.QueryString["ModuleCode"] == null ? "" : Request.QueryString["ModuleCode"].ToString();
                logmodel.LevelNo = "2";
                logmodel.FromHost = Request.UserHostAddress;

                ddlMoveType.Attributes.Add("onChange", "onChangeMoveType(this.selectedIndex)");
                ImageAfterValue.Attributes.Add("onclick", "onclickAfterValue(document.all('ddlMoveType').selectedIndex)");
                this.txtEmployeeNo.Attributes.Add("onblur", "GetEmp();");
                EmployeeNo = this.Request.QueryString["EmployeeNo"] == null ? "" : this.Request.QueryString["EmployeeNo"].ToString();
                MoveOrder = this.Request.QueryString["MoveOrder"] == null ? "" : this.Request.QueryString["MoveOrder"].ToString();
                Process = this.Request.QueryString["ProcessFlag"] == null ? "" : this.Request.QueryString["ProcessFlag"].ToString();
                moduleCode = this.Request.QueryString["ModuleCode"] == null ? "" : this.Request.QueryString["ModuleCode"].ToString();
                HiddenMoveOrder.Value = MoveOrder;
                ProcessFlag.Value = Process;
                IsHavePrivileged();
                dt = GetEmp(Privileged);
                InitDropDownList();
                if (Process == "Add")
                {
                    Add();
                }
                else
                {
                    if (EmployeeNo.Length > 0 && MoveOrder.Length > 0)
                    {
                        Modify(EmployeeNo, MoveOrder);
                    }
                    else
                    {
                        Response.Write("<script type='text/javascript'>window.parent.document.all.topTable.style.display='';window.parent.document.all.divEdit.style.display='none';</script>");
                    }
                }
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
                DataTable dt = hrmEmpOtherMoveBll.GetDataByCondition(moduleCode);
                if ((dt != null) && (dt.Rows.Count > 0))
                {
                    Privileged = false;
                }
            }
        }
        #endregion

        #region 新增方法
        /// <summary>
        /// 新增方法
        /// </summary>
        protected void Add()
        {

            if (this.HiddenSave.Value != "Save")
            {
                txtEmployeeNo.Text = "";
            }
            this.ProcessFlag.Value = "Add";
            this.TextBoxsReset("Add", false);

        }
        #endregion
        #region 修改方法
        /// <summary>
        ///  修改方法
        /// </summary>
        /// <param name="EmployeeNo"></param>
        /// <param name="MoveOrder"></param>
        protected void Modify(string EmployeeNo, string MoveOrder)
        {
            txtEmployeeNo.Text = EmployeeNo;
            this.ProcessFlag.Value = "Modify";
            EmpQuery(EmployeeNo, MoveOrder);

            this.TextBoxsReset("Modify", false);
        }
        #endregion

        #region 修改時查詢
        /// <summary>
        /// 修改時查詢
        /// </summary>
        /// <param name="EmployeeNo"></param>
        /// <param name="MoveOrder"></param>
        private void EmpQuery(string EmployeeNo, string MoveOrder)
        {
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow newRow in dt.Rows)
                {
                    txtEmployeeNo.Text = newRow["WORKNO"].ToString();
                    txtLocalname.Text = newRow["LOCALNAME"].ToString();
                    txtSex.Text = newRow["SEX"].ToString();
                    txtDepname.Text = newRow["DEPNAME"].ToString();
                    txtLevelname.Text = newRow["LEVELNAME"].ToString();
                    txtManagername.Text = newRow["MANAGERNAME"].ToString();
                    this.HiddenDepCode.Value = newRow["DEPCODE"].ToString();
                    this.HiddenDepName.Value = newRow["DEPNAME"].ToString();
                    this.HiddenOverTimeType.Value = newRow["OVERTIMETYPE"].ToString();
                    this.HiddenOverTimeTypeName.Value = newRow["OVERTIMETYPENAME"].ToString();
                    this.HiddenPostCode.Value = newRow["POSTCODE"].ToString();
                    this.HiddenPostName.Value = newRow["POSTNAME"].ToString();
                    this.HiddenPersonTypeCode.Value = newRow["PERSONTYPECODE"].ToString();
                    this.HiddenPersonTypeName.Value = newRow["PERSONTYPENAME"].ToString();
                }
                Query(EmployeeNo, MoveOrder);
            }
            else
            {
                txtEmployeeNo.Text = "";
                txtLocalname.Text = "";
                txtSex.Text = "";
                txtDepname.Text = "";
                txtLevelname.Text = "";
                txtManagername.Text = "";
                this.ddlMoveType.SelectedValue = "";
                txtBeforeValueName.Text = "";
                this.HiddenAfterValue.Value = "";
                txtAfterValueName.Text = "";
                txtEffectDate.Text = "";
                txtMoveReason.Text = "";
                txtRemark.Text = "";
                this.HiddenMoveOrder.Value = "";
                this.HiddenMoveType.Value = "";
                this.HiddenDepCode.Value = "";
                this.HiddenDepName.Value = "";
                this.HiddenOverTimeType.Value = "";
                this.HiddenOverTimeTypeName.Value = "";
                this.HiddenPostCode.Value = "";
                this.HiddenPostName.Value = "";
                this.HiddenPersonTypeCode.Value = "";
                this.HiddenPersonTypeName.Value = "";

                TextBoxsReset("", true);
                //    this.WriteMessage(1, this.GetResouseValue("bfw.hrm_no_employee_info"));
            }
        }
        #endregion

        #region 處理日期格式
        /// <summary>
        /// 處理日期格式
        /// </summary>
        /// <param name="Date"></param>
        /// <returns></returns>
        private string ChangeDate(string Date)
        {
            string tempDate = Date;
            Date = tempDate.Substring(5) + tempDate.Substring(4, 1) + tempDate.Substring(0, 4);
            return Date;
        }
        #endregion

        #region 修改時查詢
        /// <summary>
        /// 修改時查詢
        /// </summary>
        /// <param name="EmployeeNo"></param>
        /// <param name="MoveOrder"></param>
        private void Query(string EmployeeNo, string MoveOrder)
        {
            DataTable dt = new DataTable();
            dt = hrmEmpOtherMoveBll.SelectEmpMove(MoveOrder, EmployeeNo, Privileged, SqlDep);
            if (dt.Rows.Count > 0)
            {
                this.ddlMoveType.SelectedValue = dt.Rows[0]["MoveTypeCode"] == null ? "" : dt.Rows[0]["MoveTypeCode"].ToString();
                this.HiddenBeforeValue.Value = dt.Rows[0]["BeforeValue"] == null ? "" : dt.Rows[0]["BeforeValue"].ToString();
                txtBeforeValueName.Text = dt.Rows[0]["BeforeValueName"] == null ? "" : dt.Rows[0]["BeforeValueName"].ToString();
                this.HiddenAfterValue.Value = dt.Rows[0]["AfterValue"] == null ? "" : dt.Rows[0]["AfterValue"].ToString();
                txtAfterValueName.Text = dt.Rows[0]["AfterValueName"] == null ? "" : dt.Rows[0]["AfterValueName"].ToString();
             //   txtEffectDate.Text = dt.Rows[0]["EffectDateStr"] == null ? "" : ChangeDate(dt.Rows[0]["EffectDateStr"].ToString());
                txtEffectDate.Text = dt.Rows[0]["EffectDateStr"] == null ? "" : dt.Rows[0]["EffectDateStr"].ToString();
                txtMoveReason.Text = dt.Rows[0]["MoveReason"] == null ? "" : dt.Rows[0]["MoveReason"].ToString();
                txtRemark.Text = dt.Rows[0]["Remark"] == null ? "" : dt.Rows[0]["Remark"].ToString();
                this.HiddenMoveType.Value = dt.Rows[0]["MoveTypeCode"] == null ? "" : dt.Rows[0]["MoveTypeCode"].ToString();
                this.HiddenState.Value = dt.Rows[0]["State"] == null ? "" : dt.Rows[0]["State"].ToString();
                if (this.HiddenState.Value == "1")
                {
                    this.btnSave.Enabled = false;
                }
            }
            else
            {
                this.ddlMoveType.SelectedValue = "";
                this.HiddenBeforeValue.Value = "";
                txtBeforeValueName.Text = "";
                this.HiddenAfterValue.Value = "";
                txtAfterValueName.Text = "";
                txtEffectDate.Text = "";
                txtMoveReason.Text = "";
                txtRemark.Text = "";
                this.HiddenMoveType.Value = "";
                this.HiddenState.Value = "";
            }
            //  this.WriteMessage(0, this.GetResouseValue("common.message.trans.complete"));
        }
        #endregion

        #region 重置TextBox
        /// <summary>
        /// 重置TextBox
        /// </summary>
        /// <param name="buttonText"></param>
        /// <param name="read"></param>
        private void TextBoxsReset(string buttonText, bool read)
        {
            //inalterable
            txtEmployeeNo.BorderStyle = BorderStyle.NotSet;
            txtBeforeValueName.BorderStyle = BorderStyle.None;
            txtLocalname.BorderStyle = System.Web.UI.WebControls.BorderStyle.None;
            txtSex.BorderStyle = System.Web.UI.WebControls.BorderStyle.None;
            txtDepname.BorderStyle = System.Web.UI.WebControls.BorderStyle.None;
            txtLevelname.BorderStyle = System.Web.UI.WebControls.BorderStyle.None;
            txtManagername.BorderStyle = System.Web.UI.WebControls.BorderStyle.None;
            //alterable  
            txtAfterValueName.BorderStyle = read ? BorderStyle.None : BorderStyle.NotSet;
            txtEffectDate.BorderStyle = read ? BorderStyle.None : BorderStyle.NotSet;
            txtMoveReason.BorderStyle = read ? BorderStyle.None : BorderStyle.NotSet;
            txtRemark.BorderStyle = read ? BorderStyle.None : BorderStyle.NotSet;
            if (buttonText.ToLower() == "add")
            {
                this.ddlMoveType.SelectedValue = "";
                this.HiddenBeforeValue.Value = "";
                txtBeforeValueName.Text = "";
                this.HiddenAfterValue.Value = "";
                txtAfterValueName.Text = "";
                txtEffectDate.Text = "";
                txtMoveReason.Text = "";
                txtRemark.Text = "";
                this.HiddenMoveOrder.Value = "";
                this.HiddenMoveType.Value = "";
            }
            else
            {
                txtEmployeeNo.BorderStyle = BorderStyle.None;
            }
        }
        #endregion

        #region 綁定DropDownList異動類別
        /// <summary>
        /// 綁定DropDownList異動類別
        /// </summary>
        private void InitDropDownList()
        {
            DataTable dt1 = hrmEmpOtherMoveBll.InitddlMoveType();
            ddlMoveType.DataSource = dt1;
            ddlMoveType.DataTextField = "DataValue";
            ddlMoveType.DataValueField = "DataCode";
            ddlMoveType.DataBind();
            ddlMoveType.Items.Insert(0, new ListItem("", ""));
        }
        #endregion

        #region AJAX驗證

        /// <summary>
        /// AJAX验证角色代码是否存在
        /// </summary>
        protected override void AjaxProcess()
        {

            if (Request.Form["EmpNO"] != null && Request.Form["hasPrivileged"] != null && Request.Form["flag"] != null)
            {
                string strinfo = null;
                List<GdsAttEmployeesVModel> list = GetEmpList(Request.Form["EmpNO"].ToString(), Request.Form["hasPrivileged"].ToString() == "true");
                string flag = Request.Form["flag"].ToString();
                switch (flag)
                {
                    case "checkUser":
                        if (list != null && list.Count == 1)
                        {
                            strinfo = "Y";
                        }
                        else
                        {
                            strinfo = "N";
                        }
                        break;
                    case "showData":
                        if (list != null && list.Count == 1)
                        {
                            strinfo = JsSerializer.Serialize(list[0]);
                        }
                        else
                        {
                            strinfo = JsSerializer.Serialize(new GdsAttEmployeesVModel());
                        }
                        break;
                }
                Response.Clear();
                Response.Write(strinfo);
                Response.End();
            }

        }
        #endregion

        #region 獲取人員基本信息
        /// <summary>
        /// 獲取人員基本信息
        /// </summary>
        /// <param name="Privileged"></param>
        /// <returns></returns>
        public DataTable GetEmp(bool Privileged)
        {

            DataTable dt = new DataTable();
            dt = hrmEmpOtherMoveBll.GetEmp(EmployeeNo.ToUpper(), Privileged, SqlDep);
            return dt;
        }
        #endregion

        #region 獲取人員基本信息,用於導出Excel
        /// <summary>
        /// 獲取人員基本信息,用於導出Excel
        /// </summary>
        /// <param name="EmployeeNo"></param>
        /// <param name="Privileged"></param>
        /// <returns></returns>
        public List<GdsAttEmployeesVModel> GetEmpList(string EmployeeNo, bool Privileged)
        {
            List<GdsAttEmployeesVModel> list = new List<GdsAttEmployeesVModel>();
            GdsAttEmployeesVBll gdsAttEmployeesVBll = new GdsAttEmployeesVBll();
            list = gdsAttEmployeesVBll.GetEmpList(EmployeeNo.ToUpper(), Privileged,SqlDep);
            return list;
        }
        #endregion

        #region 保存
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string moveTypeValue = ddlMoveType.SelectedValue;
            string moveValueName = txtAfterValueName.Text.Trim();
            string afterDataCode = hrmEmpOtherMoveBll.GetDataCode(moveValueName, moveTypeValue);
            string alert = "";
            if (afterDataCode.Length != 0)
            {
                if (HiddenBeforeValue.Value.Trim() != afterDataCode)
                {
                    if (!hrmEmpOtherMoveBll.IsExistMoveType(ProcessFlag.Value, HiddenBeforeValue.Value, afterDataCode, txtEmployeeNo.Text.Trim(), txtEffectDate.Text.Trim(), HiddenMoveOrder.Value))
                    {
                        if (!hrmEmpOtherMoveBll.checkDate(ProcessFlag.Value, txtEmployeeNo.Text.Trim(), moveTypeValue, txtEffectDate.Text.Trim(), HiddenMoveOrder.Value.Trim()))
                        {
                            bool isSuccess = false;
                            if (hrmEmpOtherMoveBll.SaveData(ProcessFlag.Value, txtEmployeeNo.Text.Trim(), moveTypeValue, HiddenBeforeValue.Value, afterDataCode, txtEffectDate.Text.Trim(), txtMoveReason.Text.Trim(), txtRemark.Text.Trim(), CurrentUserInfo.Personcode, HiddenMoveOrder.Value.Trim(), logmodel))
                            {
                                isSuccess = true;
                            }
                            if (isSuccess)
                            {
                                if (this.ProcessFlag.Value == "Add")
                                {
                                    alert = "alert('" + Message.AddSuccess + "')";
                                    Add();
                                }
                                else
                                {
                                    Response.Write("<script type='text/javascript'>window.parent.document.all.btnQuery.click();</script>");
                                }

                            }
                            else if (this.ProcessFlag.Value == "Add")
                            {
                                alert = "alert('" + Message.AddFailed + "')";
                            }
                            else if (this.ProcessFlag.Value == "Modify")
                            {
                                alert = "alert('" + Message.UpdateFailed + "')";
                            }
                        }
                        else
                        {
                            alert = "alert('" + Message.EffDateNowDate + "')";
                        }
                    }
                    else
                    {
                        alert = "alert('" + Message.recordExists + "')";
                    }
                }
                else
                {
                    alert = "alert('" + Message.BeforeAfterDataCodeSame + "')";
                }
            }
            else
            {
                alert = "alert('" + Message.DataCodeError + "')";
            }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "SaveMoveType", alert, true);
        }
        #endregion
    }
      

}
