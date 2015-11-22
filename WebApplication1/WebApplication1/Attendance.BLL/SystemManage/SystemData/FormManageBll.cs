/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： FormManagerBll.cs
 * 檔功能描述： 表單下載業務邏輯類
 * 
 * 版本：1.0
 * 創建標識： 陳函 2012.01.05
 * 
 */
using System;
using System.Collections.Generic;
using System.Text;
using GDSBG.MiABU.Attendance.IDAL.SystemManage.SystemData;
using System.Data;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemData;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;

namespace GDSBG.MiABU.Attendance.BLL.SystemManage.SystemData
{
    public class FormManageBll:BLLBase<IFormManageDal>
    {
        public DataTable GetFormType()
        {
            return DAL.GetFormType();
        }

        public DataTable GetOrderForm(FormModel model, int pageIndex, int pageSize, out int totalCount)
        {
            return DAL.GetOrderForm(model, pageIndex, pageSize, out  totalCount);
        }

        public FormModel GetFormByKey(string formID)
        {
            FormModel model = new FormModel();
            model.FormSeq = Convert.ToInt32(formID);
            return DAL.GetFormByKey(model);
        }

        public bool DeleteNotice(FormModel model, SynclogModel logmodel)
        {
            return DAL.DeleteNotice(model, logmodel);
        }

        public bool AddForm(FormModel model, SynclogModel logmodel)
        {
            return DAL.AddForm(model,logmodel);
        }

        public bool UpdateFormByKey(FormModel model, SynclogModel logmodel)
        {
            return DAL.UpdateFormByKey(model, logmodel);
        }

        public DataTable GetTopPaperList(int count)
        {
            return DAL.GetTopPaperList(count);
        }
    }
}
