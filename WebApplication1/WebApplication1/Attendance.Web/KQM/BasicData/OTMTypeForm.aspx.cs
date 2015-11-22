/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： OTMTypeForm.cs
 * 檔功能描述： 加班類別定義UI類
 * 
 * 版本：1.0
 * 創建標識： 陈函 2011.12.10
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
using GDSBG.MiABU.Attendance.Model.KQM.BasicData;
using Resources;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;

namespace GDSBG.MiABU.Attendance.Web.KQM.BasicData
{
    public partial class OTMTypeForm : BasePage
    {
        OTMTypeBll otmTypeBll = new OTMTypeBll();
        JavaScriptSerializer JsSerializer = new JavaScriptSerializer();
        string moduleCode;
        Dictionary<string, string> ClientMessage = null;
        static AttTypeModel attTypeModel;
        static SynclogModel logmodel = new SynclogModel();
        protected void Page_Load(object sender, EventArgs e)
        {
            PageHelper.ButtonControls(FuncList, pnlShowPanel.Controls, base.FuncListModule);
            moduleCode = Request.QueryString["ModuleCode"] == null ? "" : Request.QueryString["ModuleCode"].ToString();
            if (ClientMessage == null)
            {
                ClientMessage = new Dictionary<string, string>();
                ClientMessage.Add("G1dNotNumber", Message.G1dNotNumber);
                ClientMessage.Add("G2dNotNumber", Message.G2dNotNumber);
                ClientMessage.Add("G3dNotNumber", Message.G3dNotNumber);
                ClientMessage.Add("G1mNotNumber", Message.G1mNotNumber);
                ClientMessage.Add("G2mNotNumber", Message.G2mNotNumber);
                ClientMessage.Add("G12mNotNumber", Message.G12mNotNumber);
                ClientMessage.Add("G13mNotNumber", Message.G13mNotNumber);
                ClientMessage.Add("G123mNotNumber", Message.G123mNotNumber);
                ClientMessage.Add("G1mAndG1d", Message.G1mAndG1d);
                ClientMessage.Add("G2mAndG2d", Message.G2mAndG2d);
                ClientMessage.Add("G12mAndG1mAndG2d", Message.G12mAndG1mAndG2d);
                ClientMessage.Add("G12mAndG1mAndG2m", Message.G12mAndG1mAndG2m);
                ClientMessage.Add("G13mAndG1dAndG1mAndG3d", Message.G13mAndG1dAndG1mAndG3d);
                ClientMessage.Add("G123mAndG1mAndG2mAndG3d", Message.G123mAndG1mAndG2mAndG3d);
                ClientMessage.Add("SaveConfirm", Message.SaveConfirm);
                ClientMessage.Add("AtLastOneChoose", Message.AtLastOneChoose);
                ClientMessage.Add("DeleteAttTypeConfirm", Message.DeleteAttTypeConfirm);
            }

            if (!base.IsPostBack)
            {
                logmodel.ProcessOwner = CurrentUserInfo.Personcode;
                logmodel.TransactionType = Request.QueryString["ModuleCode"] == null ? "" : Request.QueryString["ModuleCode"].ToString();
                logmodel.LevelNo = "2";
                logmodel.FromHost = Request.UserHostAddress;

                InitDropDownList();
                ddlOttypeCode.Enabled = false;
                this.ImageDepCode.Attributes.Add("onclick", "javascript:getDeptTree('" + moduleCode + "')");
                attTypeModel = new AttTypeModel();
                DataBind();
            }
            string clientmsg = JsSerializer.Serialize(ClientMessage);
            Page.ClientScript.RegisterStartupScript(GetType(), "ClientMessage", "var Message=" + clientmsg + ";", true);
        }
        /// <summary>
        /// 初始化DropDownList
        /// </summary>
        private void InitDropDownList()
        {
            DataTable dt = otmTypeBll.GetAttTypeData();
            ddlOttypeCode.DataSource = dt;
            ddlOttypeCode.DataTextField = "DataCode";
            ddlOttypeCode.DataBind();
            ddlOttypeCode.Items.Insert(0, new ListItem("", ""));
        }


        protected void pager_PageChanged(object sender, EventArgs e)
        {
            DataBind();
        }
        /// <summary>
        /// 查詢事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            pager.CurrentPageIndex = 1;
            attTypeModel = PageHelper.GetModel<AttTypeModel>(inputPanel.Controls, txtEffectFlag);
            DataBind();
            PageHelper.CleanControlsValue(inputPanel.Controls);
        }
        /// <summary>
        /// 數據綁定
        /// </summary>
        private void DataBind()
        {

            int totalCount;
            //    AttTypeModel attTypeModel = PageHelper.GetModel<AttTypeModel>(inputPanel.Controls, txtEffectFlag);
            DataTable dt = otmTypeBll.GetAttType(attTypeModel, pager.CurrentPageIndex, pager.PageSize, out totalCount, base.SqlDep);
            UltraWebGridOTMType.DataSource = dt.DefaultView;
            pager.RecordCount = totalCount;
            pager.TextAfterPageIndexBox = "/" + pager.PageCount.ToString();
            UltraWebGridOTMType.DataBind();
            for (int i = 0; i < this.UltraWebGridOTMType.Rows.Count; i++)
            {
                if ((this.UltraWebGridOTMType.Rows[i].Cells.FromKey("EFFECTFLAG").Value != null) && Convert.ToString(this.UltraWebGridOTMType.Rows[i].Cells.FromKey("EFFECTFLAG").Value).Equals("N"))
                {
                    this.UltraWebGridOTMType.Rows[i].Style.ForeColor = Color.Red;
                }
            }
        }
        /// <summary>
        /// 保存事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            AttTypeModel attTypeModel = PageHelper.GetModel<AttTypeModel>(inputPanel.Controls, txtDepname);
            string alert = "";
            if (hidOperate.Value == "Add")
            {
                bool isAlowInsert = true;
                string personCode = CurrentUserInfo.Personcode;
                if (!otmTypeBll.IslevelUser(personCode))
                {
                    if (Convert.ToDouble(txtG13mLimit.Text) <= 36)
                    {
                        if (Convert.ToDouble(txtG123mLimit.Text) <= 60)
                        {

                        }
                        else
                        {
                            alert = "alert('" + Message.G123NumberControl + "')";
                            isAlowInsert = false;
                        }

                    }
                    else
                    {
                        alert = "alert('" + Message.G13NumberControl + "')";
                        isAlowInsert = false;
                    }
                }
                if (isAlowInsert)
                {
                    AttTypeModel OTMTypeModel = new AttTypeModel();
                    OTMTypeModel.OrgCode = attTypeModel.OrgCode;
                    OTMTypeModel.OttypeCode = attTypeModel.OttypeCode;
                    attTypeModel.EffectFlag = "Y";
                    if (!otmTypeBll.isExistsOTM(OTMTypeModel))
                    {
                        logmodel.ProcessFlag = "insert";
                        bool b = otmTypeBll.AddAttType(attTypeModel, logmodel);
                        if (b)
                        {
                            alert = "alert('" + Message.AddSuccess + "')";
                        }
                        else
                        {
                            alert = "alert('" + Message.AddFailed + "')";
                        }
                    }
                    else
                    {
                        alert = "alert('" + Message.NotOnlyOne + "')";
                    }
                  
                }
            }
            else if (hidOperate.Value == "Modify")
            {
                logmodel.ProcessFlag = "update";
                attTypeModel.OttypeCode = HiddenOTTypeCode.Value;
                if (otmTypeBll.UpDateAttType(attTypeModel, logmodel))
                {
                    alert = "alert('" + Message.UpdateSuccess + "')";
                }
                else
                {
                    alert = "alert('" + Message.UpdateFailed + "')";
                }
            }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "addOrUpDateAttType", alert, true);
            PageHelper.CleanControlsValue(inputPanel.Controls);
            DataBind();
        }
        /// <summary>
        /// 刪除事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            AttTypeModel attTypeModel = new AttTypeModel();
            attTypeModel.OrgCode = txtOrgCode.Text;
            attTypeModel.OttypeCode = HiddenOTTypeCode.Value;
            string alert = "";
            logmodel.ProcessFlag = "delete";
            if (otmTypeBll.DeleteAttType(attTypeModel, logmodel))
            {
                alert = "alert('" + Message.DeleteSuccess + "')";
            }
            else
            {
                alert = "alert('" + Message.DeleteFailed + "')";
            }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DeleteAttType", alert, true);
            PageHelper.CleanControlsValue(inputPanel.Controls);
            DataBind();
        }
        /// <summary>
        /// 生效事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnEnable_Click(object sender, EventArgs e)
        {
            AttTypeModel attTypeModel = new AttTypeModel();
            attTypeModel.OrgCode = txtOrgCode.Text;
            attTypeModel.OttypeCode = HiddenOTTypeCode.Value;
            attTypeModel.EffectFlag = "Y";
            string alert = "";
            logmodel.ProcessFlag = "update";
            if (otmTypeBll.EnableAttType(attTypeModel, logmodel))
            {
                alert = "alert('" + Message.EnableSuccess + "')";
            }
            else
            {
                alert = "alert('" + Message.EnableFailed + "')";
            }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EnableAttType", alert, true);
            PageHelper.CleanControlsValue(inputPanel.Controls);
            DataBind();
            ProcessFlag.Value = "N";
        }
        /// <summary>
        /// 失效時間
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDisable_Click(object sender, EventArgs e)
        {
            AttTypeModel attTypeModel = new AttTypeModel();
            attTypeModel.OrgCode = txtOrgCode.Text;
            attTypeModel.OttypeCode = HiddenOTTypeCode.Value;
            attTypeModel.EffectFlag = "N";
            string alert = "";
            logmodel.ProcessFlag = "update";
            if (otmTypeBll.EnableAttType(attTypeModel,logmodel))
            {
                alert = "alert('" + Message.DisableSuccess + "')";
            }
            else
            {
                alert = "alert('" + Message.DisableFailed + "')";
            }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DisableAttType", alert, true);
            PageHelper.CleanControlsValue(inputPanel.Controls);
            DataBind();
            ProcessFlag.Value = "N";
        }
    }
}
