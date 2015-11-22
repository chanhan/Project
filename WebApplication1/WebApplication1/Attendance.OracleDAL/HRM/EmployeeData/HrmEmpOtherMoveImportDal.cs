/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： HrmEmpOtherMoveImportDal.cs
 * 檔功能描述：加班類別異動功能模組操作類
 * 
 * 版本：1.0
 * 創建標識： 陈函 2011.12.23
 * 
 */
using System.Collections.Generic;
using System.Data;
using GDSBG.MiABU.Attendance.IDAL.HRM.EmployeeData;
using GDSBG.MiABU.Attendance.Model.HRM.EmployeeData;

namespace GDSBG.MiABU.Attendance.OracleDAL.HRM.EmployeeData
{
    public class HrmEmpOtherMoveImportDal : DALBase<HrmEmpOtherMoveModel>, IHrmEmpOtherMoveImportDal
    {
        /// <summary>
        /// 查詢所有異動，用於導出Excel
        /// </summary>
        /// <param name="Privileged">是否有組織權限</param>
        /// <param name="depName">部門名稱</param>
        /// <param name="historyMove">歷史異動</param>
        /// <param name="workNo">工號</param>
        /// <param name="localName">姓名</param>
        /// <param name="applyMan">申請人</param>
        /// <param name="moveType">異動類別</param>
        /// <param name="beforeValueName">異動前</param>
        /// <param name="afterValueName">異動后</param>
        /// <param name="moveState">異動狀態</param>
        /// <param name="effectDateFrom">生效起始日期</param>
        /// <param name="effectDateTo">生效截止日期</param>
        /// <param name="moveReason">異動原因</param>
        /// <returns>查詢結果List</returns>
       public List<HrmEmpOtherMoveModel> SelectEmpMove(bool Privileged, string sqlDep, string depName, string historyMove, string workNo, string localName, string applyMan, string moveType, string beforeValueName, string afterValueName, string moveState, string effectDateFrom, string effectDateTo, string moveReason)
        {
            string sqlstr = @"select * from gds_att_movetype_v  a  where 1=1 ";
            if (depName.Length > 0)
            {
                if (Privileged)
                {
                    sqlstr += " and a.DepCode IN ((" + sqlDep + ") INTERSECT SELECT DepCode FROM gds_sc_department START WITH depname = '"
                                + depName + "' CONNECT BY PRIOR depcode = parentdepcode) ";
                }
                else
                {
                    sqlstr += " and a.DepCode IN (SELECT DepCode FROM gds_sc_department START WITH depname = '"
                                + depName + "' CONNECT BY PRIOR depcode = parentdepcode) ";
                }
            }
            else
            {
                if (Privileged)
                {
                    sqlstr += " and a.DepCode in  (" + sqlDep + ")";
                }
            }
            if (workNo.Length != 0)
            {
                sqlstr += " AND a.WorkNO like '" + workNo.ToUpper() + "%'";
            }
            if (localName.Length != 0)
            {
                sqlstr += " AND a.LocalName like '" + localName + "%'";
            }
            if (applyMan.Length != 0)
            {
                sqlstr += " and a.applyMan='" + applyMan + "'";
            }
            if (moveType != "")
            {
                sqlstr += " AND a.MoveTypeCode='" + moveType + "'";
            }
            if (beforeValueName.Length != 0)
            {
                switch (moveType)
                {
                    case "T03":
                        sqlstr += " and exists (SELECT PostName FROM gds_att_OfficePost e where e.PostCode=a.beforevalue and e.PostName like '" + beforeValueName + "%')";
                        break;
                    case "T02":
                        sqlstr += " and exists (SELECT datavalue FROM gds_att_typedata e where e.datatype='PersonType' and e.datacode=a.beforevalue and e.datavalue like '" + beforeValueName + "%')";
                        break;
                    default:
                        sqlstr += " and a.beforevalue like '" + beforeValueName + "%'";
                        break;
                }
            }
            if (afterValueName.Length != 0)
            {
                switch (moveType)
                {
                    case "T03":
                        sqlstr += " and exists (SELECT PostName FROM gds_att_OfficePost e where e.PostCode=a.AfterValue and e.PostName like '" + afterValueName + "%')";
                        break;
                    case "T02":
                        sqlstr += " and exists (SELECT datavalue FROM gds_att_typedata e where e.datatype='PersonType' and e.datacode=a.AfterValueName and e.datavalue like '" + afterValueName + "%')";
                        break;
                    default:
                        sqlstr += " and a.AfterValue like '" + afterValueName + "%'";
                        break;
                }
            }
            if (moveState != "")
            {
                sqlstr += " AND a.State='" + moveState + "'";
            }
            if (effectDateFrom.Length > 0)
            {
                sqlstr += " AND trunc(a.EffectDate)>=to_date('" + effectDateFrom + "','yyyy/mm/dd')";
            }
            if (effectDateTo.Length > 0)
            {
                sqlstr += " AND trunc(a.EffectDate)<=to_date('" + effectDateTo + "','yyyy/mm/dd')";
            }
            if (moveReason.Length != 0)
            {
                sqlstr += " AND a.MoveReason like '" + moveReason + "%'";
            }
            if (historyMove.Length != 0)
            {
                sqlstr += " AND a.HISFLAG  ='" + historyMove + "'";
            }
            DataTable dt = DalHelper.ExecuteQuery(sqlstr);
           return  OrmHelper.SetDataTableToList(dt);
        }
    }
}
