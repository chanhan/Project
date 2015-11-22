/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： OrgmanagerBll.cs
 * 檔功能描述： 月加班匯總查詢業務邏輯類
 * 
 * 版本：1.0
 * 創建標識： 昝望 2012.1.6
 * 
 */

using System;
using System.Collections.Generic;
using System.Text;
using GDSBG.MiABU.Attendance.IDAL.Hr.PCM;
using System.Data;

namespace GDSBG.MiABU.Attendance.BLL.Hr.PCM
{
    public class MonthtotalBll : BLLBase<IMonthtotalDal>
    {
         /// <summary>
        /// 查詢
        /// </summary>
        /// <param name="personcode"></param>
        /// <param name="yearmonth"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public DataTable GetMonthtotal(string personcode, string yearmonth, int pageIndex, int pageSize, out int totalCount)
        {
            return DAL.GetMonthtotal( personcode,  yearmonth,  pageIndex,  pageSize, out  totalCount);
        }

        /// <summary>
        /// 導出查詢
        /// </summary>
        /// <param name="personcode"></param>
        /// <param name="yearmonth"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public DataTable GetMonthtotal(string personcode, string yearmonth)
        {
            return DAL.GetMonthtotal(personcode,yearmonth);
        }
    }
}
