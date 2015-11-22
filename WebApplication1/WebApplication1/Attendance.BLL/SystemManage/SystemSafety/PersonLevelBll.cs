/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： PersonLevelBll.cs
 * 檔功能描述： 組織層級設定業務邏輯類
 * 
 * 版本：1.0
 * 創建標識：高子焱 2011.12.01
 * 
 */

using System.Collections.Generic;
using GDSBG.MiABU.Attendance.IDAL.SystemManage.SystemSafety;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;
using System.Data;
using System.Collections;

namespace GDSBG.MiABU.Attendance.BLL.SystemManage.SystemSafety
{
    public class PersonLevelBll : BLLBase<IPersonLevelDal>
    {
        /// <summary>
        /// 根据工號查询員工已擁有的組織層級的DataTable
        /// </summary>
        /// <param name="model"></param>
        /// <returns>DataTable</returns>
        public DataTable GetDeptDataTableByKey(string personCode)
        {
            //PersonLevelModel model = new PersonLevelModel();
            //model.PersonCode = personCode;
            return DAL.GetDeptDataTableByKey(personCode);
        }
        /// <summary>
        /// 根据工號查询員工未選中的組織層級的DataTable
        /// </summary>
        /// <param name="model"></param>
        /// <returns>DataTable</returns>
        public DataTable GetDeptUncheckedByKey(string personCode)
        {
            return DAL.GetDeptUncheckedByKey(personCode);
        }
        /// <summary>
        /// 根據主鍵修改功能
        /// </summary>
        /// <param name="model">要修改的功能Model</param>
        /// <returns>是否成功</returns>
        public int UpdateDeptByKey(string personCode, Hashtable levels, SynclogModel logmodel)
        {
        return DAL.UpdateDeptByKey(personCode,levels,logmodel);
        }
    }
}
