/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： PersonLevelModel.cs
 * 檔功能描述： 組織層級實體類
 * 
 * 版本：1.0
 * 創建標識： 高子焱 2011.12.01
 * 
 */
using System;
using GDSBG.MiABU.Attendance.Common.Attributes;

namespace GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety
{    /// <summary>
    /// 組織層級實體類
    /// </summary>
    [Serializable, TableName(" gds_sc_personlevel")]
    public class PersonLevelModel : ModelBase
    {
       private string  personCode;
       private string  levelCode;

       #region 用戶工號
       /// <summary>
       /// 用戶工號
       /// </summary>
       [Column("PERSONCODE")]
       public string PersonCode
       {
           get { return personCode; }
           set { personCode= value; }
       }
       #endregion

       #region 組織層級代碼
       /// <summary>
       /// 組織層級代碼
       /// </summary>
       [Column("LEVELCODE")]
       public string LevelCode
       {
           get { return levelCode; }
           set { levelCode = value; }
       }
       #endregion
    }
}
