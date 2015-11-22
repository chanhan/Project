/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： OTMTypeDal.cs
 * 檔功能描述： 加班類別定義功能模組操作類
 * 
 * 版本：1.0
 * 創建標識： 陈函 2011.12.13
 * 
 */
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using GDSBG.MiABU.Attendance.IDAL.KQM.BasicData;
using GDSBG.MiABU.Attendance.Model.KQM.BasicData;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;

namespace GDSBG.MiABU.Attendance.OracleDAL.KQM.BasicData
{
    public class OTMTypeDal : DALBase<AttTypeModel>, IOTMTypeDal
    {
        /// <summary>
        /// 根據條件查詢
        /// </summary>
        /// <param name="attTypeModel">加班類別定義Model</param>
        /// <param name="deptName">查詢結果DataTable</param>
        public DataTable GetAttType(AttTypeModel attTypeModel, int currentPageIndex, int pageSize, out int totalCount,string sqlDep)
        {
            string strCon = "";
            List<OracleParameter> listPara = DalHelper.CreateConditionParameters(attTypeModel, true, "a", out strCon);
            string cmdText = @"SELECT orgcode, ottypecode,ottypedetail, g1dlimit,g2dlimit,g3dlimit, g1mlimit,g2mlimit,g12mlimit,
                                                       remark, effectflag, isallowproject,create_user,create_date,update_user,update_date,g13mlimit,g123mlimit,
                                                       depname  FROM gds_att_type_v a where  orgcode in  (" + sqlDep + ") ";
            cmdText = cmdText + strCon + " order by depname desc";

            return  DalHelper.ExecutePagerQuery(cmdText, currentPageIndex, pageSize, out  totalCount, listPara.ToArray());
            //return DalHelper.Select(attTypeModel, "depname desc", currentPageIndex, pageSize, out totalCount);
        }
        /// <summary>
        /// 綁定加班類別DropDownList
        /// </summary>
        /// <returns>查詢結果DataTable</returns>
        public DataTable GetAttTypeData()
        {
            string cmdText = "select DataCode,DataValue from gds_att_typedata WHERE DataType='OverTimeType' ORDER BY OrderId";
            return DalHelper.ExecuteQuery(cmdText);
        }

        /// <summary>
        /// 新增加班類別定義
        /// </summary>
        /// <param name="attTypeModel">加班類別定義Model</param>
        /// <returns>新增的行數</returns>
        public int AddAttType(AttTypeModel attTypeModel, SynclogModel logmodel)
        {
            return DalHelper.Insert(attTypeModel,logmodel);
        }
        /// <summary>
        /// 是否為事業群用戶
        /// </summary>
        /// <param name="personCode">用戶代碼</param>
        /// <returns>查詢結果</returns>
        public DataTable IslevelUser(string personCode)
        {
            string cmdText = "SELECT COUNT (1) FROM gds_sc_personlevel a, gds_sc_deplevel b  WHERE a.levelcode = b.levelcode AND b.levelname = '事業群' and A.PERSONCODE=:person_code";
            return DalHelper.ExecuteQuery(cmdText, new OracleParameter(":person_code", personCode));
        }

        /// <summary>
        /// 更新加班類別定義
        /// </summary>
        /// <param name="attTypeModel">加班類別定義Model</param>
        /// <returns>更新的行數</returns>
        public int UpDateAttType(AttTypeModel attTypeModel, SynclogModel logmodel)
        {
            return DalHelper.UpdateByKey(attTypeModel, true, logmodel);
        }

        /// <summary>
        /// 根據Model刪除加班類別定義表的數據
        /// </summary>
        /// <param name="attTypeModel">加班類別定義Model</param>
        /// <returns>刪除的行數</returns>
        public int DeleteAttType(AttTypeModel attTypeModel, SynclogModel logmodel)
        {
            return DalHelper.Delete(attTypeModel, logmodel);
        }

        /// <summary>
        /// 生效加班類別定義
        /// </summary>
        /// <param name="attTypeModel">加班類別定義Model</param>
        /// <returns>生效是否成功</returns>
        public int EnableAttType(AttTypeModel attTypeModel, SynclogModel logmodel)
        {
            return DalHelper.UpdateByKey(attTypeModel, true,logmodel);
        }

        public DataTable isExistsOTM(AttTypeModel attTypeModel)
        {
            return DalHelper.Select(attTypeModel);
        }
    }



}
