/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： RefClassAttribute.cs
 * 檔功能描述： 數據接口對應到的實體類
 * 
 * 版本：1.0
 * 創建標識： Lucky Lee 2011.09.20
 * 
 */

using System;

namespace GDSBG.MiABU.Attendance.Common.Attributes
{
    /// <summary>
    /// 數據接口對應到的實體類
    /// </summary>
    [AttributeUsage(AttributeTargets.Interface, AllowMultiple = false, Inherited = false)]
    public class RefClassAttribute : Attribute
    {
        string refClassName;
        /// <summary>
        /// 對應到的實體類
        /// </summary>
        /// <param name="RefClassName">相對于配置命名空間下的實體類名</param>
        public RefClassAttribute(string RefClassName)
        {
            refClassName = RefClassName;
        }

        /// <summary>
        /// 相對于配置命名空間下的實體類名
        /// </summary>
        public string RefClassName
        {
            get { return refClassName; }
        }
    }
}