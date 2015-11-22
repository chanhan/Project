/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： FormManagerDal.cs
 * 檔功能描述： 表單下載數據操作類
 * 
 * 版本：1.0
 * 創建標識： 陳函 2012.01.05
 * 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemData;
using GDSBG.MiABU.Attendance.IDAL.SystemManage.SystemData;
using System.Data;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;

namespace GDSBG.MiABU.Attendance.OracleDAL.SystemManage.SystemData
{
    public class FormManageDal : DALBase<FormModel>, IFormManageDal
    {
        public DataTable GetFormType()
        {
            string cmdText = "select * from gds_att_form_types where active_flag = 'Y'";
            return DalHelper.ExecuteQuery(cmdText);
        }

        public DataTable GetOrderForm(FormModel model, int pageIndex, int pageSize, out int totalCount)
        {
            return DalHelper.Select(model, true, "UPLOAD_DATE DESC", pageIndex, pageSize, out totalCount);
        }

        public FormModel GetFormByKey(FormModel model)
        {
            return DalHelper.SelectByKey(model);
        }
        public bool DeleteNotice(FormModel model, SynclogModel logmodel)
        {
            return DalHelper.Delete(model, logmodel) == 1;
        }
        public bool AddForm(FormModel model, SynclogModel logmodel)
        {
            return DalHelper.Insert(model, logmodel) == 1;
        }

        public bool UpdateFormByKey(FormModel model, SynclogModel logmodel)
        {
            return DalHelper.UpdateByKey(model, logmodel) == 1;
        }

        public DataTable GetTopPaperList(int count)
        {
            return DalHelper.Select(new FormModel() { ActiveFlag = "Y" }, 1, count);
        }
    }
}
