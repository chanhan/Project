using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using GDSBG.MiABU.Attendance.BLL.KQM.BasicData;
using GDSBG.MiABU.Attendance.Model.KQM.BasicData;
using Resources;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;

/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： KQMBellCardForm.aspx.cs
 * 檔功能描述： 卡鐘資料維護
 * 
 * 版本：1.0
 * 創建標識： 昝望 2011.12.20
 */


namespace GDSBG.MiABU.Attendance.Web.KQM.BasicData
{
    public partial class KQMBellCardForm : BasePage
    {
        JavaScriptSerializer JsSerializer = new JavaScriptSerializer();
        Dictionary<string, string> ClientMessage = null;
        int totalCount;
        static DataTable dt_global = new DataTable();
        BellCardBll bll = new BellCardBll();
        static SynclogModel logmodel = new SynclogModel();

        protected void Page_Load(object sender, EventArgs e)
        {
            pager.CurrentPageIndex = 1;
            PageHelper.ButtonControls(base.FuncList, pnlShowPanel.Controls, base.FuncListModule);
            if (ClientMessage == null)
            {
                ClientMessage = new Dictionary<string, string>();
                ClientMessage.Add("BellNoNotNull", Message.BellNoNotNull);
                ClientMessage.Add("PortIPNotNull", Message.PortIPNotNull);
                ClientMessage.Add("ProduceIDNotNull", Message.ProduceIDNotNull);
                ClientMessage.Add("ManufacturerNotNull", Message.ManufacturerNotNull);
                ClientMessage.Add("AddressNotNull", Message.AddressNotNull);
                ClientMessage.Add("BellSizeNotNull", Message.BellSizeNotNull);
                ClientMessage.Add("PickDataIPNotNull", Message.PickDataIPNotNull);
                ClientMessage.Add("BellTypeNotNull", Message.BellTypeNotNull);
                ClientMessage.Add("DeleteConfirm", Message.DeleteConfirm);
                ClientMessage.Add("AtLastOneChoose", Message.AtLastOneChoose);
                ClientMessage.Add("NotOnlyOne", Message.NotOnlyOne);
            }
            string clientmsg = JsSerializer.Serialize(ClientMessage);
            Page.ClientScript.RegisterStartupScript(GetType(), "ClientMessage", "var Message=" + clientmsg + ";", true);
            if (!IsPostBack)
            {
                logmodel.ProcessOwner = CurrentUserInfo.Personcode;
                logmodel.TransactionType = Request.QueryString["ModuleCode"].ToString();
                logmodel.LevelNo = "2";
                logmodel.FromHost = Request.UserHostAddress;
                DataBind();
            }
        }


        #region 綁定數據
        /// <summary>
        /// 綁定數據
        /// </summary>

        private void DataBind()
        {
            txtBellNo.Attributes.Add("readonly", "true");
            txtPortIP.Attributes.Add("readonly", "true");
            txtProduceID.Attributes.Add("readonly", "true");
            txtManufacturer.Attributes.Add("readonly", "true");
            txtAddress.Attributes.Add("readonly", "true");
            txtBellSize.Attributes.Add("readonly", "true");
            txtPickDataIP.Attributes.Add("readonly", "true");
            txtPickComputeUser.Attributes.Add("readonly", "true");
            txtPickComputePW.Attributes.Add("readonly", "true");
            txtContactMan.Attributes.Add("readonly", "true");
            txtContactTel.Attributes.Add("readonly", "true");
            txtUseDept.Attributes.Add("readonly", "true");
            txtUserYM.Attributes.Add("readonly", "true");
            txtRemark.Attributes.Add("readonly", "true");
            ddlBellType.Attributes.Add("disabled", "true");
            btnSave.Attributes.Add("disabled", "true");
            btnCancel.Attributes.Add("disabled", "true");
            ddlBellType.DataSource = bll.GetBellType();
            ddlBellType.DataValueField = "datacode";
            ddlBellType.DataTextField = "datavalue";
            ddlBellType.DataBind();
            this.ddlBellType.Items.Insert(0, new ListItem("", ""));
            this.ddlBellType.SelectedValue = "";
            BellCardModel model = new BellCardModel();
            DataTable dt = bll.GetBellCard(model, pager.CurrentPageIndex, pager.PageSize, out totalCount);
            pager.RecordCount = totalCount;
            dt_global = dt;
            this.UltraWebGridBellCard.DataSource = dt;
            this.UltraWebGridBellCard.DataBind();
            pager.TextAfterPageIndexBox = "/" + pager.PageCount.ToString();

        }

        private void DataBind(DataTable dt)
        {
            txtBellNo.Attributes.Add("readonly", "true");
            txtPortIP.Attributes.Add("readonly", "true");
            txtProduceID.Attributes.Add("readonly", "true");
            txtManufacturer.Attributes.Add("readonly", "true");
            txtAddress.Attributes.Add("readonly", "true");
            txtBellSize.Attributes.Add("readonly", "true");
            txtPickDataIP.Attributes.Add("readonly", "true");
            txtPickComputeUser.Attributes.Add("readonly", "true");
            txtPickComputePW.Attributes.Add("readonly", "true");
            txtContactMan.Attributes.Add("readonly", "true");
            txtContactTel.Attributes.Add("readonly", "true");
            txtUseDept.Attributes.Add("readonly", "true");
            txtUserYM.Attributes.Add("readonly", "true");
            txtRemark.Attributes.Add("readonly", "true");
            ddlBellType.Attributes.Add("disabled", "true");
            btnSave.Attributes.Add("disabled", "true");
            btnCancel.Attributes.Add("disabled", "true");
            ddlBellType.DataSource = bll.GetBellType();
            ddlBellType.DataValueField = "datacode";
            ddlBellType.DataTextField = "datavalue";
            ddlBellType.DataBind();
            this.ddlBellType.Items.Insert(0, new ListItem("", ""));
            this.ddlBellType.SelectedValue = "";
            this.UltraWebGridBellCard.DataSource = dt;
            this.UltraWebGridBellCard.DataBind();
            pager.TextAfterPageIndexBox = "/" + pager.PageCount.ToString();

        }

        #endregion



        #region 查詢
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            pager.CurrentPageIndex = 1;
            if (hidOperate.Value == "condition")
            {
                BellCardModel model = PageHelper.GetModel<BellCardModel>(pnlContent.Controls);
                model.BellNo = txtBellNo.Text.ToUpper();
                DataTable dt = bll.GetBellCard(model, pager.CurrentPageIndex, pager.PageSize, out totalCount);
                pager.RecordCount = totalCount;
                dt_global = dt;
                DataBind(dt);
            }
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

            BellCardModel model = PageHelper.GetModel<BellCardModel>(pnlContent.Controls);
            bool succeed;
            if (hidOperate.Value == "modify")
            {
                logmodel.ProcessFlag = "update";
                succeed = bll.UpdateBellCardByKey(model, logmodel);
                if (succeed)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "isnumber", "alert('" + Message.UpdateSuccess + "');", true);
                    DataBind();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "isnumber", "alert('" + Message.UpdateFailed + "');", true);

                }
            }
            if (hidOperate.Value == "add")
            {
                logmodel.ProcessFlag = "insert";
                BellCardModel model1 = new BellCardModel();
                model1.BellNo = txtBellNo.Text.ToUpper();
                if (bll.GetBellCard(model1, pager.CurrentPageIndex, pager.PageSize, out totalCount).Rows.Count == 0)
                {
                    succeed = bll.AddBellCard(model, logmodel);
                    if (succeed)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "isnumber", "alert('" + Message.AddSuccess + "');", true);
                        DataBind();
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "isnumber", "alert('" + Message.AddFailed + "');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "isnumber", "alert('" + Message.NotOnlyOne + "');", true);
                }

            }


        }
        #endregion


        #region UltraWebGridBellCard的DataBound事件
        /// <summary>
        /// UltraWebGrid的DataBound事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void UltraWebGridBellCard_DataBound(object sender, EventArgs e)
        {
            for (int i = 0; i < UltraWebGridBellCard.Rows.Count; i++)
            {
                if (UltraWebGridBellCard.Rows[i].Cells.FromKey("EffectFlag").Text.Trim() == "N")
                {
                    for (int j = 0; j < UltraWebGridBellCard.Columns.Count; j++)
                    {
                        UltraWebGridBellCard.Rows[i].Cells[j].Style.ForeColor = System.Drawing.Color.Red;
                    }
                }
            }
        }
        #endregion

        #region 刪除
        /// <summary>
        /// 刪除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            logmodel.ProcessFlag = "delete";
            BellCardModel model = PageHelper.GetModel<BellCardModel>(pnlContent.Controls);
            if (model.BellNo != null)
            {
                int flag = bll.DeleteBellCardByKey(model.BellNo, logmodel);
                if (flag == 1)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "isnumber", "alert('" + Message.DeleteSuccess + "');", true);
                    DataBind();
                    Text_Reset();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "isnumber", "alert('" + Message.DeleteFailed + "');", true);
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "isnumber", "alert('" + Message.AtLastOneChoose + "');", true);
            }
        }
        #endregion

        #region 失效
        /// <summary>
        /// 失效
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDisable_Click(object sender, EventArgs e)
        {
            logmodel.ProcessFlag = "update";
            BellCardModel model = PageHelper.GetModel<BellCardModel>(pnlContent.Controls);
            bool succeed = false;
            if (model.BellNo != null)
            {
                model.EffectFlag = "N";
                succeed = bll.UpdateBellCardByKey(model, logmodel);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "isnumber", "alert('" + Message.AtLastOneChoose + "');", true);
            }
            if (succeed)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "isnumber", "alert('" + Message.DisableSuccess + "');", true);
                DataBind();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "isnumber", "alert('" + Message.DisableFailed + "');", true);
            }
        }

        #endregion

        #region 生效

        protected void btnEnable_Click(object sender, EventArgs e)
        {
            logmodel.ProcessFlag = "update";
            BellCardModel model = PageHelper.GetModel<BellCardModel>(pnlContent.Controls);
            bool succeed = false;
            if (model.BellNo != null)
            {
                model.EffectFlag = "Y";
                succeed = bll.UpdateBellCardByKey(model, logmodel);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "isnumber", "alert('" + Message.AtLastOneChoose + "');", true);
            }
            if (succeed)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "isnumber", "alert('" + Message.EnableSuccess + "');", true);
                DataBind();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "isnumber", "alert('" + Message.EnableFailed + "');", true);
            }

        }

        #endregion

        #region 分頁
        protected void pager_PageChanged(object sender, EventArgs e)
        {
            DataBind();
        }
        #endregion


        #region Ajax
        protected override void AjaxProcess()
        {
            BellCardModel model = new BellCardModel();
            string noticeJson = null;
            DataTable dt = new DataTable();
            if (!string.IsNullOrEmpty(Request.Form["BellNo"]))
            {
                model.BellNo = Request.Form["BellNo"];
                dt = bll.GetBellCard(model, pager.CurrentPageIndex, pager.PageSize, out totalCount);

                if (dt != null)
                {
                    noticeJson = dt.Rows.Count.ToString();
                }
                Response.Clear();
                Response.Write(noticeJson);
                Response.End();
            }

        }
        #endregion

        #region 清空文本框
        protected void Text_Reset()
        {
            this.txtBellNo.Text = "";
            this.txtPortIP.Text = "";
            this.txtProduceID.Text = "";
            this.txtManufacturer.Text = "";
            txtAddress.Text = "";
            txtBellSize.Text = "";
            txtPickDataIP.Text = "";
            txtPickComputeUser.Text = "";
            txtPickComputePW.Text = "";
            txtContactMan.Text = "";
            txtContactTel.Text = "";
            txtUseDept.Text = "";
            txtUserYM.Text = "";
            ddlBellType.SelectedValue = "";
            txtRemark.Text = "";
        }
        #endregion
    }
}
