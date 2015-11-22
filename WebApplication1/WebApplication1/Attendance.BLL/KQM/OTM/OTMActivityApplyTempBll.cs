
/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： OTMActivityApplyTempBll.cs
 * 檔功能描述：免卡人員導入功能業務邏輯類
 * 
 * 版本：1.0
 * 創建標識： 張明強 2011.12.8
 * 
 */

using System.Collections.Generic;
using GDSBG.MiABU.Attendance.IDAL.KQM.OTM;
using GDSBG.MiABU.Attendance.Model.KQM.OTM;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;
using System.Data;

namespace GDSBG.MiABU.Attendance.BLL.KQM.OTM
{
    public class OTMActivityApplyTempBll : BLLBase<IOTMActivityApplyTempDal>
    {
        /// <summary>
        /// 導入信息,正確信息插入正式表,錯誤信息返回datatable
        /// </summary>
        /// <param name="createUser">創建人</param>
        /// <returns>返回的datatable</returns>
        public DataTable GetTempTableErrorData(string createUser, out int successnum, out int errornum,SynclogModel logmodel)
        {
            return DAL.GetTempTableErrorData(createUser, out successnum, out  errornum,logmodel);
        }

        /// <summary>
        /// 將datatable轉換為List
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public List<ActivityTempModel> GetList(DataTable dt)
        {
            return DAL.GetList(dt);
        }


    }
}
