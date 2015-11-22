/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： ModuleDal.cs
 * 檔功能描述： 用戶資料數據操作類
 * 
 * 版本：1.0
 * 創建標識： 昝望 2011.12.1
 * 
 */

using System;
using System.Data;
using System.Data.OracleClient;
using GDSBG.MiABU.Attendance.IDAL.SystemManage.SystemSafety;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;
using System.Collections;
using System.Collections.Generic;
using GDSBG.MiABU.Attendance.Model.KQM.BasicData;

namespace GDSBG.MiABU.Attendance.OracleDAL.SystemManage.SystemSafety
{
    public class PersonDal : DALBase<PersonModel>, IPersonDal
    {
        /// <summary>
        /// 根據主鍵獲得用戶Model
        /// </summary>
        /// <param name="model">給出主鍵值的Model</param>
        /// <returns>返回對應主鍵的Model</returns>
        public DataTable GetPerson(PersonModel model, int pageIndex, int pageSize, out int totalCount, string sqlDep)
        {
            //return DalHelper.Select(model, "Create_date desc", pageIndex, pageSize, out totalCount);
            string strCon = "";
            List<OracleParameter> listPara = DalHelper.CreateConditionParameters(model, true, "", out strCon);
            string cmdText = @"select * from gds_sc_person_v  where DepCode IN (" + sqlDep + ")";
            cmdText = cmdText + strCon;
            DataTable dt = DalHelper.ExecutePagerQuery(cmdText, pageIndex, pageSize, out  totalCount, listPara.ToArray());
            return dt;
        }

        /// <summary>
        /// 導出查詢
        /// </summary>
        /// <param name="model">給出主鍵值的Model</param>
        /// <returns>返回對應主鍵的Model</returns>
        public DataTable GetPerson(PersonModel model,string sqlDep)
        {
            string strCon = "";
            List<OracleParameter> listPara = DalHelper.CreateConditionParameters(model, true, "", out strCon);
            string cmdText = @"select * from gds_sc_person_v  where DepCode IN (" + sqlDep + ")";
            cmdText = cmdText + strCon;
            DataTable dt = DalHelper.ExecuteQuery(cmdText, listPara.ToArray());
            return dt;
        }

        /// <summary>
        /// 新增用戶
        /// </summary>
        /// <param name="model">要新增的用戶Model</param>
        /// <returns>是否成功</returns>
        public bool AddPerson(PersonModel model, SynclogModel logmodel)
        {
            return DalHelper.Insert(model,logmodel) != -1;
        }

        /// <summary>
        /// 根據主鍵修改用戶資料
        /// </summary>
        /// <param name="model">要修改的用戶Model</param>
        /// <returns>是否成功</returns>
        public bool UpdatePersonByKey(PersonModel model, SynclogModel logmodel)
        {
            return DalHelper.UpdateByKey(model, true,logmodel) != -1;
        }

        /// <summary>
        /// 刪除一個用戶
        /// </summary>
        /// <param name="functionId">要刪除的用戶Id</param>
        /// <returns>刪除用戶條數</returns>
        public int DeletePersonByKey(string PersonCode,SynclogModel logmodel)
        {
            //OracleParameter outPara = new OracleParameter("p_out", OracleType.Int32);
            //outPara.Direction = ParameterDirection.Output;
            //int i = DalHelper.ExecuteNonQuery("delete_module_pro", CommandType.StoredProcedure,
            //    new OracleParameter("p_modulecode", moduleCode), outPara);
            //return i == -1 ? i : Convert.ToInt32(outPara.Value);
            string str = "delete from GDS_SC_PERSON where personcode='" + PersonCode + "'";
            return DalHelper.ExecuteNonQuery(str, logmodel);


        }

        /// <summary>
        /// 獲得用戶清單
        /// </summary>
        /// <returns>用戶清單PersonModel集</returns>
        public DataTable GetPersonList()
        {
            PersonModel model = new PersonModel();
            return DalHelper.Select(model, "Create_date desc");

        }

        #region 根據Model條件查詢記錄數
        /// <summary>
        /// 根據Model條件查詢記錄數
        /// </summary>
        /// <param name="model">條件Model</param>
        /// <param name="isFuzzy">是否模糊匹配，為true時不區分大小寫并以Like方式查詢</param>
        /// <returns>記錄數</returns>
        public int GetCount(PersonModel model)
        {
            return DalHelper.GetCount(model);
        }
        #endregion

        /// <summary>
        /// 更新密碼
        /// </summary>
        /// <param name="userno"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public int UpdatePWDByKey(string userno, string pwd, SynclogModel logmodel)
        {

            OracleParameter outPara = new OracleParameter("a_out", OracleType.Int32);
            outPara.Direction = ParameterDirection.Output;
            int i = DalHelper.ExecuteNonQuery("sc_pwd_chang.Reset_pwd", CommandType.StoredProcedure,
                new OracleParameter("v_pwd", pwd),new OracleParameter("v_user", userno),  outPara,
                new OracleParameter("p_transactiontype", logmodel.TransactionType), new OracleParameter("p_levelno", logmodel.LevelNo), new OracleParameter("p_fromhost", logmodel.FromHost),
                new OracleParameter("p_tohost", logmodel.ToHost == null ? "" : logmodel.ToHost.ToString()), new OracleParameter("p_docno", logmodel.DocNo == null ? "" : logmodel.DocNo.ToString()), new OracleParameter("p_processflag", logmodel.ProcessFlag),
                new OracleParameter("p_processowner", logmodel.ProcessOwner));
            return i = Convert.ToInt32(outPara.Value);
        }

        /// <summary>
        /// 保存用戶組織關聯信息
        /// </summary>
        /// <param name="personcode"></param>
        /// <param name="moduleCode"></param>
        /// <param name="companyId"></param>
        /// <param name="depts"></param>
        /// <param name="p_user"></param>
        /// <returns></returns>
        public int SavePersonDeptData(string personcode, string rolecode, string companyId, string depts, string p_user)
        {
            OracleParameter outPara = new OracleParameter("p_out", OracleType.Int32);
            outPara.Direction = ParameterDirection.Output;
            int i = DalHelper.ExecuteNonQuery("gds_sc_savepersondept", CommandType.StoredProcedure,
                new OracleParameter("p_personcode", personcode), new OracleParameter("p_rolecode", rolecode), new OracleParameter("p_companyid", companyId),
                new OracleParameter("p_deptlist", depts), new OracleParameter("p_user", p_user), outPara);
            return i = Convert.ToInt32(outPara.Value);
        }

        /// <summary>
        /// 查找語言類別
        /// </summary>
        /// <returns></returns>
        public DataTable selectLanguage()
        {
            string str = "select * from GDS_ATT_LANGUAGE where EFFECTFLAG='Y' ";
            return DalHelper.ExecuteQuery(str);
        }

        /// <summary>
        /// 查找層級類別
        /// </summary>
        /// <returns></returns>
        public DataTable selectDepLevel()
        {
            string str = "select * from GDS_SC_DEPLEVEL order by levelcode ";
            return DalHelper.ExecuteQuery(str);
        }

        /// <summary>
        /// 查找群組
        /// </summary>
        /// <returns></returns>
        public DataTable selectRoles()
        {
            string str = "select * from gds_sc_roles where DELETED='N'";
            return DalHelper.ExecuteQuery(str);
        }

        /// <summary>
        /// 將datatable轉換為List
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public List<PersonModel> GetList(DataTable dt)
        {
            return OrmHelper.SetDataTableToList(dt);
        }

        /// <summary>
        /// 根據用戶名獲取人員信息
        /// </summary>
        /// <param name="model">人員信息Model</param>
        /// <returns>人員信息集</returns>
        public List<PersonModel> GetPersonUserId(string userId)
        {
            PersonModel model = new PersonModel();
            model.Personcode = userId;
            DataTable dtbl = DalHelper.Select(model);
            return OrmHelper.SetDataTableToList(dtbl);
        }


        /// <summary>
        /// 按登陸用戶信息查詢數據庫(用戶登陸)
        /// </summary>
        /// <param name="userId">用戶名</param>
        /// <param name="password">密碼</param>
        /// <returns></returns>
        public List<PersonModel> GetPersonUserId(string userId, string password)
        {
            string str = @"SELECT personcode, titlename, cname, appellation, ename, companyid, depcode,sexid, 
                    rolecode, ifadmin, tel, mobile, mail, passwd, extension, LANGUAGE, fax, logintimes, logintime, 
                    DATEFORMAT, datetimeformat,numberformat, amountformat, decimalseparator, colorscheme,groupseparator,
                    mainbkcolorred, mainbkcolorgreen, mainbkcolorblue,treemenubkcolorred, treemenubkcolorgreen, treemenubkcolorblue,
                    windowbkcolorred, windowbkcolorgreen, windowbkcolorblue, fontcolorred,fontcolorgreen, fontcolorblue, gridheaderbkcolorred,
                    gridheaderbkcolorgreen, gridheaderbkcolorblue, gridbkcolorred,gridbkcolorgreen, gridbkcolorblue, deleted, defaultrecords,
                    gridaltbkcolorred, splittercolorred, gridaltbkcolorgreen,splittercolorgreen, gridaltbkcolorblue, splittercolorblue, ifonline,
                    mdtcolorred, ipaddress, mdtcolorgreen, defaultmodule, hostname,mdtcolorblue, mdtchar, defaultmenu, areacode, deplevel
                    FROM gds_sc_person WHERE personcode = :personcode AND passwd = sc_pwd_chang.ins_pwd (:pwd)";
            return OrmHelper.SetDataTableToList(DalHelper.ExecuteQuery(str, new OracleParameter(":personcode", userId), new OracleParameter(":pwd", password)));
        }

        /// <summary>
        /// 按登陸用戶信息查詢數據庫(用戶登陸)
        /// </summary>
        /// <param name="userId">用戶名</param>
        /// <param name="password">密碼</param>
        /// <returns></returns>
        public List<PersonModel> GetPersonUserId(string userId, string password,string ismail)
        {
            List<PersonModel> list = new List<PersonModel>();
            if (ismail == "Y")
            {
                string str = @"SELECT personcode, titlename, cname, appellation, ename, companyid, depcode,sexid, 
                    rolecode, ifadmin, tel, mobile, mail, passwd, extension, LANGUAGE, fax, logintimes, logintime, 
                    DATEFORMAT, datetimeformat,numberformat, amountformat, decimalseparator, colorscheme,groupseparator,
                    mainbkcolorred, mainbkcolorgreen, mainbkcolorblue,treemenubkcolorred, treemenubkcolorgreen, treemenubkcolorblue,
                    windowbkcolorred, windowbkcolorgreen, windowbkcolorblue, fontcolorred,fontcolorgreen, fontcolorblue, gridheaderbkcolorred,
                    gridheaderbkcolorgreen, gridheaderbkcolorblue, gridbkcolorred,gridbkcolorgreen, gridbkcolorblue, deleted, defaultrecords,
                    gridaltbkcolorred, splittercolorred, gridaltbkcolorgreen,splittercolorgreen, gridaltbkcolorblue, splittercolorblue, ifonline,
                    mdtcolorred, ipaddress, mdtcolorgreen, defaultmodule, hostname,mdtcolorblue, mdtchar, defaultmenu, areacode, deplevel
                    FROM gds_sc_person WHERE personcode = :personcode AND passwd = :pwd";
                list = OrmHelper.SetDataTableToList(DalHelper.ExecuteQuery(str, new OracleParameter(":personcode", userId), new OracleParameter(":pwd", password)));
            }
            return list;
        }


        /// <summary>
        /// 根據登陸用戶查詢該用戶是否登陸并返回登陸用戶資料
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<PersonModel> GetPersonLoginId(string userId)
        {
           // string str = "select gds_sc_login_fun(:f_personcode) from dual";
            
            OracleParameter outCursor = new OracleParameter("p_coursor", OracleType.Cursor);
            outCursor.Direction = ParameterDirection.Output;
            OracleCommand cmd = DalHelper.CreateCommand("gds_sc_login_fun", CommandType.StoredProcedure, new OracleParameter("f_personcode", userId), outCursor);
            using (cmd.Connection)
            {
                DalHelper.ExecuteNonQueryReader(cmd);

                OracleDataReader dr = outCursor.Value as OracleDataReader;
                if (dr != null)
                {
                    return OrmHelper.SetDataReaderToList(dr);
                }
            }
            return null;
            //return OrmHelper.SetDataTableToList(DT);

        }

        /// <summary>
        /// 根據用戶名查詢工號信息
        /// </summary>
        /// <param name="workno"></param>
        /// <returns></returns>
        public DataTable GetEmployeeInfo(string workno)
        {
            string str = "select * from GDS_ATT_EMPLOYEE where WORKNO='" + workno + "'";
            return DalHelper.ExecuteQuery(str);
        }
        /// <summary>
        /// 根據用戶名查詢工號信息(在外出申請編輯（新增/修改頁面使用））——————madeby 子焱
        /// </summary>
        /// <param name="workno"></param>
        /// <returns></returns>
        public DataTable GetEmployeeInfoList(string workno)
        {
            string str = @"SELECT a.workno, a.localname,(SELECT datavalue FROM gds_att_typedata c WHERE c.datatype = 'Sex' 
                           AND c.datacode = a.sex) AS sex,a.dname, a.depname FROM gds_att_employee a  where WORKNO='" + workno + "'";
            DataTable dt= DalHelper.ExecuteQuery(str);
            return dt;
        }
        /// <summary>
        /// 根據用戶名和權限查詢用戶信息———外出申請修改功能使用
        /// </summary>
        /// <param name="workno"></param>
        /// <returns></returns>
        public DataTable GetEmployeeInfo(string workNo, string sqlDep)
        {
            string str = @"SELECT a.workno, a.localname,a.dcode,(SELECT datavalue FROM gds_att_typedata c WHERE c.datatype = 'Sex' 
                           AND c.datacode = a.sex) AS sex,a.dname, a.depname FROM gds_att_employee a  ";
            string cmdText =str + "WHERE a.WorkNO='" + workNo.ToUpper() + "'";
            cmdText = cmdText + @" AND exists (SELECT 1 FROM (" + sqlDep + ") e where e.DepCode=a.DCode)"; ;
            DataTable dt = DalHelper.ExecuteQuery(cmdText);
            return dt;
        }
    }
}
