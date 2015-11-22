/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： IHrmDepartmentDal.cs
 * 檔功能描述：組織擴建數據操作接口
 * 
 * 版本：1.0
 * 創建標識： 顧陳偉 2012.01.02
 * 
 */

using System.Data;
using GDSBG.MiABU.Attendance.Common.Attributes;
using GDSBG.MiABU.Attendance.Model.HRM;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;

namespace GDSBG.MiABU.Attendance.IDAL.HRM
{
    [RefClass("HRM.HrmDepartmentDal")]
    public interface IHrmDepartmentDal
    {
        /// <summary>
        /// 得到組織層級樹
        /// </summary>
        /// <param name="personCode">工號</param>
        /// <param name="companyId">公司ID</param>
        /// <param name="moduleCode">模組代碼</param>
        /// <returns>查詢結果DataTable</returns>
        DataTable getOrgTree(string personCode, string companyId, string moduleCode);
        
        /// <summary>
        /// 獲得層級標識
        /// </summary>
        /// <param name="orderid"></param>
        /// <returns></returns>
        DataTable GetHead(string orderid);

        /// <summary>
        /// 獲得組織資料
        /// </summary>
        /// <param name="companyid"></param>
        /// <param name="depcode"></param>
        /// <returns></returns>
        DataTable GetDepartment(string companyid, string depcode);

        /// <summary>
        /// 獲得組織詳細資料
        /// </summary>
        /// <param name="conditon"></param>
        /// <param name="companyid"></param>
        /// <param name="depcode"></param>
        /// <returns></returns>
        DataTable GetDepartmentDetailData(string conditon, string companyid, string depcode);

        /// <summary>
        ///獲得最大的depcode
        /// </summary>
        /// <param name="depcode"></param>
        /// <returns></returns>
        DataTable GetMaxDepCode(string depcode);

        /// <summary>
        /// 獲得組織下員工
        /// </summary>
        /// <param name="flag"></param>
        /// <param name="depcode"></param>
        /// <returns></returns>
        DataTable GetWorkNoByDepCode(int flag, string depcode);

        /// <summary>
        /// 獲得組織及其子組織
        /// </summary>
        /// <param name="flag"></param>
        /// <param name="depcode"></param>
        /// <returns></returns>
        DataTable GetDeptByParentdepcode(bool flag, string depcode);

        /// <summary>
        /// 查詢組織
        /// </summary>
        /// <param name="newdepname"></param>
        /// <returns></returns>
        DataTable GetDept(string newdepname,string sql);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool UpdateDepartment(DepartmentModel model, SynclogModel logmodel);

        /// <summary>
        /// 刪除
        /// </summary>
        /// <param name="companyid"></param>
        /// <param name="depcode"></param>
        /// <returns></returns>
        int DeleteDepartment(string companyid, string depcode, SynclogModel logmodel);

        /// <summary>
        /// 失效/生效
        /// </summary>
        /// <param name="companyid"></param>
        /// <param name="depcode"></param>
        /// <param name="disableflag"></param>
        /// <returns></returns>
        int DisableOrEnable(string companyid, string depcode, string disableflag, SynclogModel logmodel);

        /// <summary>
        /// 存儲
        /// </summary>
        /// <param name="processFlag"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        int SaveDepartment(string processFlag, DepartmentModel model, SynclogModel logmodel);

        /// <summary>
        /// 組織異動
        /// </summary>
        /// <param name="newdepcode"></param>
        /// <param name="depcode"></param>
        /// <returns></returns>
        int ChangeSave(string newdepcode, string depcode, SynclogModel logmodel);
    }
}
