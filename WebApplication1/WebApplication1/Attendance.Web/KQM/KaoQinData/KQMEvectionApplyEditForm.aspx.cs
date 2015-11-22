/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： KQMEvectionApplyEditForm.cs
 * 檔功能描述： 外出申請頁面
 * 
 * 版本：1.0
 * 創建標識： 高子焱 2012.02.12
 * 
 */
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using GDSBG.MiABU.Attendance.Model.KQM.KaoQinData;
using System.Collections.Generic;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;
using GDSBG.MiABU.Attendance.BLL.SystemManage.SystemSafety;
using System.Web.Script.Serialization;
using Resources;
using GDSBG.MiABU.Attendance.BLL.KQM.KaoQinData;
using GDSBG.MiABU.Attendance.Model.KQM.BasicData;
using GDSBG.MiABU.Attendance.BLL.KQM.BasicData;
namespace GDSBG.MiABU.Attendance.Web.KQM.KaoQinData
{
    public partial class KQMEvectionApplyEditForm : BasePage
    {
        KQMEvectionApplyModel model = new KQMEvectionApplyModel();
        PersonBll bllPerson = new PersonBll();
        KQMEvectionApplyBll bll = new KQMEvectionApplyBll();
        DataTable dt = new DataTable();
        JavaScriptSerializer JsSerializer = new JavaScriptSerializer();
        Dictionary<string, string> ClientMessage = null;
        EmployeeBll bllEmployee = new EmployeeBll();
        static SynclogModel logmodel = new SynclogModel();
        TypeDataBll bllTypeData = new TypeDataBll();
        #region 如果是新增
        protected void Add()
        {
            if (this.HiddenSave.Value != "Save")
            {
                this.txtEmployeeNo.Text = "";
            }
            this.ProcessFlag.Value = "Add";
            this.TextBoxsReset("Add", false);
        }
        #endregion
        #region 如果是修改
        protected void Modify(string BillNo)
        {
            this.Query(BillNo);
            this.ProcessFlag.Value = "Modify";
            this.TextBoxsReset("Modify", false);
        }
        #endregion
        #region  设置textbox的样式等
        private void TextBoxsReset(string buttonText, bool read)
        {
            this.txtEvectionTask.BorderStyle = read ? BorderStyle.None : BorderStyle.NotSet;
            this.txtEvectionObject.BorderStyle = read ? BorderStyle.None : BorderStyle.NotSet;
            this.txtEvectionRoad.BorderStyle = read ? BorderStyle.None : BorderStyle.NotSet;
            this.txtEvectionTel.BorderStyle = read ? BorderStyle.None : BorderStyle.NotSet;
            this.txtEvectionReason.BorderStyle = read ? BorderStyle.None : BorderStyle.NotSet;
            this.txtRealReturnTime.BorderStyle = read ? BorderStyle.None : BorderStyle.NotSet;
            this.txtRemark.BorderStyle = read ? BorderStyle.None : BorderStyle.NotSet;
            this.txtEvectionReason.BorderStyle = read ? BorderStyle.None : BorderStyle.NotSet;
            this.txtReturnTime.BorderStyle = read ? BorderStyle.None : BorderStyle.NotSet;
            this.txtEvectionAddress.BorderStyle = read ? BorderStyle.None : BorderStyle.NotSet;
            this.txtRemark.BorderStyle = read ? BorderStyle.None : BorderStyle.NotSet;
            this.ddlEvectionType.Enabled = !read;
            this.txtSex.BorderStyle = BorderStyle.None;
            this.txtLocalName.BorderStyle = BorderStyle.None;
            this.txtDepName.BorderStyle = BorderStyle.None;
            this.txtBillNo.BorderStyle = BorderStyle.None;
            if (buttonText.ToLower() == "add")
            {
                this.txtBillNo.Text = Message.SystemAuto;
                this.txtEmployeeNo.BorderStyle = BorderStyle.NotSet;
                this.txtSex.Text = "";
                this.txtEvectionTask.Text = "";
                this.txtEvectionReason.Text = "";
                this.txtEvectionAddress.Text = "";
                this.txtRemark.Text = "";
                this.HiddenBillNo.Value = "";
                this.ddlEvectionType.SelectedValue = "";
                this.HiddenEvectionType.Value = "";
            }
            else
            {
                this.txtEmployeeNo.BorderStyle = BorderStyle.NotSet;
            }
        }
        #endregion
        #region 页面加载
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.DropDownListBind(this.ddlEvectionType, "EvectionTypeOut");
                this.RadioListBind(this.rdoEvectionBy, "EvectionByType");
                string BillNo = (base.Request.QueryString["BillNo"] == null) ? "" : base.Request.QueryString["BillNo"].ToString();
                string ProcessFlag = (base.Request.QueryString["ProcessFlag"] == null) ? "" : base.Request.QueryString["ProcessFlag"].ToString();
                this.HiddenBillNo.Value = BillNo;
                this.ProcessFlag.Value = ProcessFlag;
                if (ProcessFlag == "Add")
                {
                    this.Add();
                }
                else if (BillNo.Length > 0)
                {
                    this.Modify(BillNo);
                }
                else
                {
                    base.Response.Write("<script type='text/javascript'>alert('" + Message.AtLastOneChoose + "');window.parent.document.all.topTable.style.display='';window.parent.document.all.divEdit.style.display='none';</script>");
                }
                this.txtEmployeeNo.Attributes.Add("onblur", "GetEmp();");
            }
            if (ClientMessage == null)
            {
                ClientMessage = new Dictionary<string, string>();
                ClientMessage.Add("AtLastOneChoose", Message.AtLastOneChoose);
                ClientMessage.Add("WrongDate", Message.WrongDate);
                ClientMessage.Add("TextBoxNotNull", Message.TextBoxNotNull);
                ClientMessage.Add("PersonInfoNotExist", Message.PersonInfoNotExist);
                ClientMessage.Add("WorkNoNotNull", Message.WorkNoNotNull);

                ClientMessage.Add("OnlySelectByCar", Message.OnlySelectByCar);
                ClientMessage.Add("OnlySelectWalk", Message.OnlySelectWalk);
                ClientMessage.Add("ErrReturnTimeWrong", Message.ErrReturnTimeWrong);
            }
            string clientmsg = JsSerializer.Serialize(ClientMessage);
            Page.ClientScript.RegisterStartupScript(GetType(), "ClientMessage", "var Message=" + clientmsg + ";", true);
        }
        #endregion
        #region 綁定下拉框的值
        protected void RadioListBind(RadioButtonList List, string DataTypeValue)
        {
            DataTable dt = new DataTable();
            dt = bllTypeData.GetTypeDataList(DataTypeValue);
            List.DataSource = dt;
            List.DataTextField = "DataValue";
            List.DataValueField = "DataCode";
            List.DataBind();
        }
        #endregion
        #region 綁定下拉框的值
        protected void DropDownListBind(DropDownList List, string DataTypeValue)
        {
            DataTable dt = new DataTable();
            dt = bllTypeData.GetTypeDataList(DataTypeValue);
            List.DataSource = dt;
            List.DataTextField = "DataValue";
            List.DataValueField = "DataCode";
            List.DataBind();
            List.Items.Insert(0, new ListItem("", ""));
        }
        #endregion
        #region Ajaxs事件
        protected override void AjaxProcess()
        {
            string noticeJson = null;
            if (!string.IsNullOrEmpty(Request.Form["EmployeeNo"]))
            {
                model.WorkNo = Request.Form["EmployeeNo"].ToString();
                //從用戶資料頁面的dal層引入此方法
                DataTable dt = bllPerson.GetEmployeeInfoList(model.WorkNo);
                List<EmployeeModel> list = bllEmployee.GetList(dt);
                if (list != null && list.Count != 0)
                {
                    noticeJson = JsSerializer.Serialize(list);
                }
                else
                {
                    noticeJson = null;
                }
                Response.Clear();
                Response.Write(noticeJson);
                Response.End();
            }
        }
        #endregion
        #region  根据单号查询员工信息
        private void Query(string billNo)
        {
            model.ID = billNo;
            dt = bll.GetEvectionList(model);
            if (dt.Rows.Count > 0)
            {
                this.txtID.Text = dt.Rows[0]["ID"].ToString().Trim();
                this.txtEvectionRoad.Text = dt.Rows[0]["EvectionRoad"].ToString().Trim();
                this.txtReturnTime.Text =Convert.ToDateTime(dt.Rows[0]["ReturnTime"]).ToString("yyyy/MM/dd HH:mm").Trim();
                this.txtEvectionTime.Text = Convert.ToDateTime(dt.Rows[0]["EvectionTime"]).ToString("yyyy/MM/dd HH:mm").Trim();
                this.txtEvectionTask.Text = dt.Rows[0]["EvectionTask"].ToString().Trim();
                this.txtEvectionTel.Text = dt.Rows[0]["EvectionTel"].ToString().Trim();

                this.txtEvectionObject.Text = dt.Rows[0]["EvectionObject"].ToString().Trim();
                this.txtEvectionReason.Text = dt.Rows[0]["EvectionReason"].ToString().Trim();
                this.txtEvectionAddress.Text = dt.Rows[0]["EvectionAddress"].ToString().Trim();
                this.txtRemark.Text = dt.Rows[0]["Remark"].ToString().Trim();
                this.ddlEvectionType.SelectedValue = dt.Rows[0]["EvectionType"].ToString().Trim();
                this.rdoEvectionBy.SelectedValue = dt.Rows[0]["EvectionByOut"].ToString().Trim();
                if (this.rdoEvectionBy.SelectedValue =="1")
                {
                    this.txtRemarkCar.Text = dt.Rows[0]["MotorMan"].ToString().Trim();
                }
                else if (this.rdoEvectionBy.SelectedValue =="5")
                {
                    this.txtRemarkCar.Text = dt.Rows[0]["ApRemark"].ToString().Trim();
                }
                this.HiddenEvectionType.Value = dt.Rows[0]["EvectionType"].ToString().Trim();
                this.HiddenBillNo.Value = dt.Rows[0]["BillNo"].ToString().Trim();
                this.EmpQuery(dt.Rows[0]["WorkNo"].ToString().Trim());
                this.HiddenStatus.Value = dt.Rows[0]["Status"].ToString();
                this.txtBillNo.Text = dt.Rows[0]["BillNo"].ToString().Trim();
                string status = dt.Rows[0]["Status"].ToString();
                if ((status != null) && (((status == "1") || (status == "2")) || (status == "4")))
                {
                    this.btnSave.Enabled = false;
                }
            }
        }
        #endregion
        #region  根據工號查詢用戶信息
        private void EmpQuery(string employeeNo)
        {
            string sqlDep = base.SqlDep;
            DataTable dtEmployee = bllPerson.GetEmployeeInfo(employeeNo, sqlDep);

            if (dtEmployee.Rows.Count > 0)
            {
                foreach (DataRow newRow in dtEmployee.Rows)
                {
                    this.txtEmployeeNo.Text = newRow["WORKNO"].ToString();
                    this.txtLocalName.Text = newRow["LOCALNAME"].ToString();
                    this.txtSex.Text = newRow["SEX"].ToString();
                    this.txtDepName.Text = newRow["DName"].ToString();
                }
            }
            else
            {
                this.txtLocalName.Text = "";
                this.txtDepName.Text = "";
                this.txtSex.Text = "";
                this.txtEvectionTask.Text = "";
                this.txtEvectionReason.Text = "";
                this.txtEvectionAddress.Text = "";
                this.txtRemark.Text = "";
                this.HiddenBillNo.Value = "";
                this.ddlEvectionType.SelectedValue = "";
                this.HiddenEvectionType.Value = "";
                Page.ClientScript.RegisterStartupScript(GetType(), "show", "alert('" + Message.PersonInfoNotExist + "')", true);
            }
        }
        #endregion

        #region 保存按鈕操作
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string activeFlag = ProcessFlag.Value.ToString().Trim();
            if (activeFlag == "Add")
            {
                logmodel.ProcessFlag = "insert";
                lblOthers_.Visible = true;
                txtRemarkCar.Attributes.Add("display", "");
                KQMEvectionApplyModel model = PageHelper.GetModel<KQMEvectionApplyModel>(pnlContent.Controls);
                model.CreateDate = DateTime.Now;
                model.CreateUser = CurrentUserInfo.Personcode;
                model.WorkNo = txtEmployeeNo.Text.ToString();
                model.EvectionBy = rdoEvectionBy.SelectedValue.ToString();
                if (model.EvectionType == "0")
                {
                    model.Status = "4";
                }
                else
                {
                    model.Status = "0";
                }
                if (model.EvectionBy == "1")
                {
                    model.MotorMan = txtRemarkCar.Text.ToString();
                }
                else if (model.EvectionBy == "5")
                {
                    model.ApRemark= txtRemarkCar.Text.ToString();
                }
                model.ReturnTime = Convert.ToDateTime(this.txtReturnTime.Text.ToString().Trim() == "" ? null : this.txtReturnTime.Text.ToString().Trim());
                model.EvectionTime = Convert.ToDateTime(txtEvectionTime.Text.ToString().Trim() == "" ? null : txtEvectionTime.Text.ToString().Trim());
                int flag = bll.InsertEvectionByKey(model, logmodel);
                if (flag > 0)
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "show", "alert('" + Message.AddSuccess + "')", true);
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "show", "alert('" + Message.AddFailed + "')", true);
                }
            }
            if (activeFlag == "Modify")
            {
                logmodel.ProcessFlag = "update";
                lblOthers_.Visible = true;
                txtRemarkCar.Attributes.Add("display", "");
                KQMEvectionApplyModel model = PageHelper.GetModel<KQMEvectionApplyModel>(pnlContent.Controls);
                model.CreateDate = DateTime.Now;
                model.CreateUser = CurrentUserInfo.Personcode;
                if (model.EvectionType == "0")
                {
                    model.Status = "4";
                }
                else
                {
                    model.Status = "0";
                }

                model.WorkNo = txtEmployeeNo.Text.ToString();
                model.EvectionBy = rdoEvectionBy.SelectedValue.ToString();

                if (model.EvectionBy == "1")
                {
                    model.MotorMan = txtRemarkCar.Text.ToString();
                }
                else if (model.EvectionBy == "5")
                {
                    model.ApRemark = txtRemarkCar.Text.ToString();
                }
                model.ReturnTime = Convert.ToDateTime(this.txtReturnTime.Text.ToString().Trim() == "" ? null : this.txtReturnTime.Text.ToString().Trim());
                model.EvectionTime = Convert.ToDateTime(txtEvectionTime.Text.ToString().Trim() == "" ? null : txtEvectionTime.Text.ToString().Trim());
                int flag = bll.UpdateEvctionByKey(model, logmodel);
                if (flag > 0)
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "show", "alert('" + Message.UpdateSuccess + "')", true);
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "show", "alert('" + Message.UpdateFailed + "')", true);
                }
            }
            hidOperate.Value = "";
        }
        #endregion
       
    }
}
