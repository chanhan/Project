/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： BasePageHelper.cs
 * 檔功能描述： 頁面幫助類
 * 
 * 版本：1.0
 * 創建標識： Lucky Lee 2011.10.14
 * 
 */

using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.SessionState;
using GDSBG.MiABU.Attendance.Model;
using GDSBG.MiABU.Attendance.Common;

namespace GDSBG.MiABU.Attendance.Web
{
    public class PageHelper
    {
        #region 映射控件值到Model
        /// <summary>
        /// 映射控件值到Model
        /// </summary>
        /// <typeparam name="T">Model類型</typeparam>
        /// <param name="controls">控件集合</param>
        /// <param name="tModel">要映射到的Model,控件無值時保留Model原值</param>
        /// <param name="skipControls">控件集合中不映射的控件</param>
        /// <returns></returns>
        public static T GetModel<T>(ControlCollection controls, T tModel, params Control[] skipControls) where T : ModelBase, new()
        {
            Type modelType = typeof(T);
            T model = tModel == null ? new T() : tModel;

            foreach (Control control in controls)
            {
                if (string.IsNullOrEmpty(control.ID) || (skipControls != null && skipControls.Contains<Control>(control)))
                {
                    continue;
                }
                string controlValue = null;
                PropertyInfo info = null;
                switch (control.GetType().Name)
                {
                    case "TextBox":
                        controlValue = (control as TextBox).Text.Trim();
                        info = modelType.GetProperty(control.ID.Replace("txt", ""));
                        break;
                    case "DropDownList":
                        controlValue = (control as DropDownList).SelectedValue;
                        info = modelType.GetProperty(control.ID.Replace("ddl", ""));
                        break;
                    case "CheckBox":
                        controlValue = (control as CheckBox).Checked ? "Y" : "N";
                        info = modelType.GetProperty(control.ID.Replace("chk", ""));
                        break;
                    case "RadioButton":
                        controlValue = (control as RadioButton).Checked ? "Y" : "N";
                        info = modelType.GetProperty(control.ID.Replace("rdo", ""));
                        break;
                    case "HiddenField":
                        controlValue = (control as HiddenField).Value.Trim();
                        info = modelType.GetProperty(control.ID.Replace("hid", ""));
                        break;
                    case "RadioButtonList":
                        controlValue = (control as RadioButtonList).SelectedValue;
                        info = modelType.GetProperty(control.ID.Replace("rdolst", ""));
                        break;
                }
                if (string.IsNullOrEmpty(controlValue)) { continue; }
                if (info != null)
                {
                    if (info.PropertyType.IsGenericType && info.PropertyType.IsValueType &&
                        info.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                    {
                        try
                        {
                            info.SetValue(model, Convert.ChangeType(controlValue,
                                info.PropertyType.GetGenericArguments()[0]), null);
                        }
                        catch
                        {
                        }
                    }
                    else
                    {
                        info.SetValue(model, controlValue, null);
                    }
                }
            }
            return model;
        }
        #endregion

        #region 映射控件值產生Model
        /// <summary>
        /// 映射控件值產生Model
        /// </summary>
        /// <typeparam name="T">Model類型</typeparam>
        /// <param name="controls">控件集合</param>
        /// <param name="skipControls">不參與映射的控件</param>
        /// <returns></returns>
        public static T GetModel<T>(ControlCollection controls, params Control[] skipControls) where T : ModelBase, new()
        {
            return GetModel<T>(controls, null, skipControls);
        }
        #endregion

        #region 綁定Model值到控件集合
        /// <summary>
        /// 綁定Model值到控件集合
        /// </summary>
        /// <typeparam name="T">Model類型</typeparam>
        /// <param name="tModel">Model實體</param>
        /// <param name="controls">控件集合</param>
        /// <param name="skipControls">不綁定的控件</param>
        public static void BindControls<T>(T tModel, ControlCollection controls, params Control[] skipControls) where T : ModelBase, new()
        {
            Type modelType = typeof(T);
            T model = tModel == null ? new T() : tModel;
            foreach (Control control in controls)
            {
                if (string.IsNullOrEmpty(control.ID) || (skipControls != null && skipControls.Contains(control)))
                {
                    continue;
                }
                PropertyInfo info = null;
                object val = null;
                switch (control.GetType().Name)
                {
                    case "TextBox":
                        info = modelType.GetProperty(control.ID.Replace("txt", ""));
                        if (info != null)
                        {
                            val = info.GetValue(tModel, null);
                            if (val != null && (info.PropertyType.IsGenericType && info.PropertyType.IsValueType &&
                                info.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) &&
                                info.PropertyType.GetGenericArguments()[0] == typeof(DateTime)))
                            {
                                (control as TextBox).Text = Convert.ToDateTime(val).ToString("yyyy/MM/dd");
                            }
                            else
                            {
                                (control as TextBox).Text = val == null ? "" : val.ToString();
                            }
                        }
                        break;
                    case "DropDownList":
                        info = modelType.GetProperty(control.ID.Replace("ddl", ""));
                        if (info != null)
                        {
                            val = info.GetValue(tModel, null);
                            (control as DropDownList).SelectedValue = val == null ? "" : val.ToString();
                        }
                        break;
                    case "CheckBox":
                        info = modelType.GetProperty(control.ID.Replace("chk", ""));
                        if (info != null)
                        {
                            val = info.GetValue(tModel, null);
                            (control as CheckBox).Checked = val == null ? false : val.ToString() == "Y";
                        }
                        break;
                    case "RadioButton":
                        info = modelType.GetProperty(control.ID.Replace("rdo", ""));
                        if (info != null)
                        {
                            val = info.GetValue(tModel, null);
                            (control as RadioButton).Checked = val == null ? false : val.ToString() == "Y";
                        }
                        break;
                    case "HiddenField":
                        info = modelType.GetProperty(control.ID.Replace("hid", ""));
                        if (info != null)
                        {
                            val = info.GetValue(tModel, null);
                            (control as HiddenField).Value = val == null ? "" : val.ToString();
                        }
                        break;
                    case "RadioButtonList":
                        info = modelType.GetProperty(control.ID.Replace("rdolst", ""));
                        if (info != null)
                        {
                            val = info.GetValue(tModel, null);
                            (control as RadioButtonList).SelectedValue = val == null ? "" : val.ToString();
                        }
                        break;
                }
            }
        }
        #endregion


        #region 設置頁面的Button是否可用
        /// <summary>
        /// 
        /// </summary>
        /// <param name="funcList">button權限列表</param>
        /// <param name="showControls">要管控的控件id</param>
        /// <param name="skipControls">無需管控的btn id</param>
        public static void ButtonControls(string funcList, ControlCollection showControls, string funcListModule)
        {
            if (funcListModule != null)
            {
                string[] strFunListModule = funcListModule.Split(',');

                foreach (Control control in showControls)
                {
                    if (string.IsNullOrEmpty(control.ID))
                    {
                        continue;
                    }
                    if (control.GetType().Name == "Button" || control.GetType().Name == "LinkButton")
                    {
                        if (strFunListModule.Contains(control.ID.Substring(3, control.ID.Length - 3)))
                        {
                            if (funcList != null)
                            {
                                string[] strFunList = funcList.Split(',');
                                if (strFunList.Contains(control.ID.Substring(3, control.ID.Length - 3)))
                                {
                                    control.Visible = true;
                                }
                                else
                                    control.Visible = false;
                            }
                            else
                            {
                                control.Visible = false;
                            }
                        }
                        else
                            control.Visible = true;
                    }
                }
            }
            else
            {
                foreach (Control control in showControls)
                {
                    control.Visible = true;
                }
            }
        }
        #endregion

        #region 設置頁面的Button是否可用
        /// <summary>
        /// 
        /// </summary>
        /// <param name="funcList">button權限列表</param>
        /// <param name="showControls">要管控的控件id</param>
        /// <param name="skipControls">無需管控的btn id</param>
        public static void ButtonControlsWF(string funcList, ControlCollection showControls, string funcListModule)
        {
            if (funcListModule != null)
            {
                string[] strFunListModule = funcListModule.Split(',');

                foreach (Control control in showControls)
                {
                    if (string.IsNullOrEmpty(control.ID))
                    {
                        continue;
                    }
                    if (control.GetType().Name == "Button" || control.GetType().Name == "LinkButton")
                    {
                        if (strFunListModule.Contains(control.ID.Substring(3, control.ID.Length - 3)) || strFunListModule.Contains(control.ID.Substring(6, control.ID.Length - 6)))
                        {
                            if (funcList != null)
                            {
                                string[] strFunList = funcList.Split(',');
                                if (strFunList.Contains(control.ID.Substring(3, control.ID.Length - 3)) || strFunListModule.Contains(control.ID.Substring(6, control.ID.Length - 6)))
                                {
                                    control.Visible = true;
                                }
                                else
                                    control.Visible = false;
                            }
                            else
                            {
                                control.Visible = false;
                            }
                        }
                        else
                            control.Visible = true;
                    }
                }
            }
            else
            {
                foreach (Control control in showControls)
                {
                    control.Visible = true;
                }
            }
        }
        #endregion



        #region 設置控件為不可用
        /// <summary>
        /// 設置控件為不可用
        /// </summary>
        /// <param name="controls">要清空的控件集合</param>
        public static void SetControlsDisabled(ControlCollection controls)
        {
            foreach (Control control in controls)
            {
                switch (control.GetType().Name)
                {
                    case "TextBox":
                        (control as TextBox).ReadOnly = true;
                        break;
                    case "DropDownList":
                        (control as DropDownList).Enabled = false;
                        break;
                    case "CheckBox":
                        (control as CheckBox).Enabled = false;
                        break;
                    case "RadioButton":
                        (control as RadioButton).Enabled = false;
                        break;
                    case "HiddenField":
                        (control as HiddenField).Value = "";
                        break;
                    case "RadioButtonList":
                        (control as RadioButtonList).Enabled = false;
                        break;
                }
            }
        }
        #endregion

        #region 清空控件值
        /// <summary>
        /// 清空控件值
        /// </summary>
        /// <param name="controls">要清空的控件集合</param>
        /// <param name="skipControls">不清空的控件</param>
        public static void CleanControlsValue(ControlCollection controls, params Control[] skipControls)
        {
            foreach (Control control in controls)
            {
                if (skipControls != null && skipControls.Contains(control))
                {
                    continue;
                }
                switch (control.GetType().Name)
                {
                    case "TextBox":
                        (control as TextBox).Text = "";
                        break;
                    case "DropDownList":
                        (control as DropDownList).SelectedIndex = 0;
                        break;
                    case "CheckBox":
                        (control as CheckBox).Checked = false;
                        break;
                    case "RadioButton":
                        (control as RadioButton).Checked = false;
                        break;
                    case "HiddenField":
                        (control as HiddenField).Value = "";
                        break;
                    case "RadioButtonList":
                        (control as RadioButtonList).SelectedValue = "";
                        break;
                }
            }
        }
        #endregion

        #region 返回文件流,提供下載
        /// <summary>
        /// 返回文件流,提供下載
        /// </summary>
        /// <param name="filePath">文件全路經[絕對路徑]</param>
        /// <param name="isDelete">是否需要刪除Server端文件</param>
        public static void ReturnHTTPStream(string filePath, bool isDelete)
        {
            byte[] buffer = File.ReadAllBytes(filePath);
            FileInfo info = new FileInfo(filePath);
            HttpResponse response = HttpContext.Current.Response;
            response.AppendHeader("Content-Disposition", "attachment;filename=" + DateTime.Now.ToString("yyyyMMddHHmmss") + info.Extension);
            response.AddHeader("Content-Length", info.Length.ToString());
            response.AppendHeader("Last-Modified", info.LastWriteTime.ToFileTime().ToString());
            response.AppendHeader("Location", HttpContext.Current.Request.Url.AbsoluteUri);
            response.ContentType = GetResponseContentType(info.Extension);
            if (buffer != null)
            {
                response.BinaryWrite(buffer);
                if (isDelete)
                {
                    File.Delete(filePath);
                }
            }
            response.Flush();
            response.End();
        }
        #endregion

        #region 打開文件類型對應程式
        /// <summary>
        /// 打開文件類型對應程式 
        /// </summary>
        /// <param name="fileType">文件擴展名</param>
        /// <returns></returns>
        private static string GetResponseContentType(string fileType)
        {
            string contentType = "application/unknow";
            switch (fileType.ToLower())
            {
                case ".doc":
                    contentType = "application/msword"; break;
                case ".xls":
                case ".xlt":
                    contentType = "application/msexcel"; break;
                case ".txt":
                    contentType = "text/plain"; break;
                case ".pdf":
                    contentType = "application/pdf"; break;
                case ".ppt":
                    contentType = "appication/powerpoint"; break;
            }
            return contentType;
        }
        #endregion

        public static void SetDepartmentSelector(WebControl ctrlTrigger, Control ctrlDeptId, Control ctrlDeptName)
        {
            if (ctrlDeptId is TextBox) { (ctrlDeptId as TextBox).Attributes.Add("readonly", "readonly"); }
            if (ctrlDeptName is TextBox) { (ctrlDeptName as TextBox).Attributes.Add("readonly", "readonly"); }
            ctrlTrigger.Attributes.Add("onclick", string.Format("return setDeptSelector('{0}','{1}')",
                ctrlDeptId.ClientID, ctrlDeptName.ClientID));
        }


    }
}