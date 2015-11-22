/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： HrmDepartmentBll.cs
 * 檔功能描述： 組織擴建業務邏輯類
 * 
 * 版本：1.0
 * 創建標識： 顧陳偉 2012.01.02
 * 
 */

using System.Data;
using GDSBG.MiABU.Attendance.IDAL.HRM;
using GDSBG.MiABU.Attendance.Model.HRM;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;

namespace GDSBG.MiABU.Attendance.BLL.HRM
{
    public class HrmDepartmentBll : BLLBase<IHrmDepartmentDal>
    {
        /// <summary>
        /// 得到組織層級樹
        /// </summary>
        /// <param name="personCode">工號</param>
        /// <param name="companyId">公司ID</param>
        /// <param name="moduleCode">模組代碼</param>
        /// <returns>查詢結果DataTable</returns>
        public DataTable getOrgTree(string personCode, string companyId, string moduleCode)
        {
            return DAL.getOrgTree(personCode, companyId, moduleCode);
        }

        /// <summary>
        /// 獲得層級標識
        /// </summary>
        /// <param name="orderid"></param>
        /// <returns></returns>
        public DataTable GetHead(string orderid)
        {
            return DAL.GetHead(orderid);
        }

        /// <summary>
        /// 獲得組織資料
        /// </summary>
        /// <param name="companyid"></param>
        /// <param name="depcode"></param>
        /// <returns></returns>
        public DataTable GetDepartment(string companyid, string depcode)
        {
            return DAL.GetDepartment(companyid, depcode);
        }

        /// <summary>
        /// 獲得組織詳細資料
        /// </summary>
        /// <param name="conditon"></param>
        /// <param name="companyid"></param>
        /// <param name="depcode"></param>
        /// <returns></returns>
        public DataTable GetDepartmentDetailData(string conditon, string companyid, string depcode)
        {
            return DAL.GetDepartmentDetailData(conditon, companyid, depcode);
        }

        /// <summary>
        ///獲得最大的depcode
        /// </summary>
        /// <param name="depcode"></param>
        /// <returns></returns>
        public string GetMaxDepCode(string depcode)
        {
            DataTable dt = DAL.GetMaxDepCode(depcode);
            string strMax = "";
            strMax = dt.Rows[0][0].ToString();
            int BillNo = 0;
            if (strMax != "")
            {
                BillNo = int.Parse(strMax.Substring(6, strMax.Length - 6)) + 1;
            }
            if (BillNo < 0x3e7)
            {
                strMax = BillNo.ToString().PadLeft(3, '0');
                strMax = depcode + strMax;
            }
            return strMax;
        }

        /// <summary>
        /// 獲得組織下員工
        /// </summary>
        /// <param name="flag"></param>
        /// <param name="depcode"></param>
        /// <returns></returns>
        public DataTable GetWorkNoByDepCode(int flag, string depcode)
        {
            return DAL.GetWorkNoByDepCode(flag, depcode);
        }

        /// <summary>
        /// 獲得組織及其子組織
        /// </summary>
        /// <param name="flag"></param>
        /// <param name="depcode"></param>
        /// <returns></returns>
        public DataTable GetDeptByParentdepcode(bool flag, string depcode)
        {
            return DAL.GetDeptByParentdepcode(flag,depcode);
        }

        /// <summary>
        /// 查詢組織
        /// </summary>
        /// <param name="newdepname"></param>
        /// <returns></returns>
        public DataTable GetDept(string newdepname,string sql)
        {
            return DAL.GetDept(newdepname,sql);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateDepartment(DepartmentModel model, SynclogModel logmodel)
        {
            return DAL.UpdateDepartment(model, logmodel);
        }

        /// <summary>
        /// 刪除
        /// </summary>
        /// <param name="companyid"></param>
        /// <param name="depcode"></param>
        /// <returns></returns>
        public int DeleteDepartment(string companyid, string depcode, SynclogModel logmodel)
        {
            return DAL.DeleteDepartment(companyid, depcode,logmodel);
        }

        /// <summary>
        /// 失效/生效
        /// </summary>
        /// <param name="companyid"></param>
        /// <param name="depcode"></param>
        /// <param name="disableflag"></param>
        /// <returns></returns>
        public int DisableOrEnable(string companyid, string depcode, string disableflag, SynclogModel logmodel)
        {
            return DAL.DisableOrEnable(companyid, depcode, disableflag,logmodel);
        }

        /// <summary>
        /// 存儲
        /// </summary>
        /// <param name="processFlag"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public int SaveDepartment(string processFlag, DepartmentModel model, SynclogModel logmodel)
        {
            return DAL.SaveDepartment(processFlag, model,logmodel);
        }

        /// <summary>
        /// 組織異動
        /// </summary>
        /// <param name="newdepcode"></param>
        /// <param name="depcode"></param>
        /// <returns></returns>
        public bool ChangeSave(string newdepcode, string depcode, SynclogModel logmodel)
        {
            if (DAL.ChangeSave(newdepcode, depcode,logmodel) == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
