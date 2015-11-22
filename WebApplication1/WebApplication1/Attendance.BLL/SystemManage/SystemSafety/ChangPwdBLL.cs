using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GDSBG.MiABU.Attendance.IDAL.SystemManage.SystemSafety;
using GDSBG.MiABU.Attendance.Model.SystemManage;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;
using System.Data;

namespace GDSBG.MiABU.Attendance.BLL
{
    public class ChangPwdBLL : BLLBase<IChangPwdDal>
    {
        /// <summary>
        /// 更新用戶信息
        /// </summary>
        /// <param name="model">用戶Model</param>
        /// <returns>是否成功</returns>
        public int UpdateUserByKey(string userno, string oldpwd, string newpwd, SynclogModel logmodel)
        {
            return DAL.UpdateUserByKey(userno, oldpwd, newpwd, logmodel);
        }
        /// <summary>
        /// 更新用戶信息
        /// </summary>
        /// <param name="model">用戶Model</param>
        /// <returns>是否成功</returns>
        public bool UpdateMailByKey(PersonModel model, SynclogModel logmodel)
        {
            return DAL.UpdateMailByKey(model, logmodel);
        }
        /// <summary>
        /// 根據登錄人的工號查詢登錄人的信息（郵件、分機和手機）
        /// </summary>
        /// <param name="personCode"></param>
        /// <returns></returns>
        public DataTable GetPerInfo(string personCode)
        {
            return DAL.GetPerInfo(personCode);
        }
    }
}
