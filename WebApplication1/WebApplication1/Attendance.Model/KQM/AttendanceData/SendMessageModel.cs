/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名：SendMessageModel.cs
 * 檔功能描述：短信提醒實體類
 * 
 * 版本：1.0
 * 創建標識： 高子焱 2012.01.04
 * 
 */
using System;
using GDSBG.MiABU.Attendance.Common.Attributes;

namespace GDSBG.MiABU.Attendance.Model.KQM.AttendanceData
{
    /// <summary>
    /// 短信提醒實體類
    /// </summary>
    [Serializable, TableName(" gds_att_kaoqindata", SelectTable = "gds_att_kaoqindata_v")]
    public class SendMessageModel:ModelBase
    {
    }
}
