/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： IFormManagerDal.cs
 * 檔功能描述： 表單下載數據接口類
 * 
 * 版本：1.0
 * 創建標識： 陳函 2012.01.05
 * 
 */
using System;
using System.Collections.Generic;
using System.Text;
using GDSBG.MiABU.Attendance.Common.Attributes;
using System.Data;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemData;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;

namespace GDSBG.MiABU.Attendance.IDAL.SystemManage.SystemData
{
    /// <summary>
    /// 公告信息操作接口
    /// </summary>
    [RefClass("SystemManage.SystemData.FormManageDal")]
    public interface IFormManageDal
    {
        DataTable GetFormType();

        DataTable GetOrderForm(FormModel model, int pageIndex, int pageSize, out int totalCount);

        FormModel GetFormByKey(FormModel model);

        bool AddForm(FormModel model, SynclogModel logmodel);

        bool UpdateFormByKey(FormModel model, SynclogModel logmodel);

        DataTable GetTopPaperList(int count);

        bool DeleteNotice(FormModel model, SynclogModel logmodel);
    }
}
