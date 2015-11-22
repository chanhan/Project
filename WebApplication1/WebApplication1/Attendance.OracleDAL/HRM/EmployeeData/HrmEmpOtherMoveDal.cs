/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： HrmEmpOtherMoveDal.cs
 * 檔功能描述：加班類別異動功能模組操作類
 * 
 * 版本：1.0
 * 創建標識： 陈函 2011.12.23
 * 
 */
using System;
using System.Data;
using System.Data.OracleClient;
using GDSBG.MiABU.Attendance.IDAL.HRM.EmployeeData;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;

namespace GDSBG.MiABU.Attendance.OracleDAL.HRM.EmployeeData
{
    public class HrmEmpOtherMoveDal : DALBase<ModuleModel>, IHrmEmpOtherMoveDal
    {
        /// <summary>
        /// 判斷是否有組織權限
        /// </summary>
        /// <param name="moduleCode">模組代碼</param>
        /// <returns>查詢結果DataTable</returns>
        public DataTable GetDataByCondition(string moduleCode)
        {
            ModuleModel moduleModel = new ModuleModel();
            moduleModel.ModuleCode = moduleCode;
            moduleModel.Privileged = "N";
            return DalHelper.Select(moduleModel);
        }
        /// <summary>
        /// 查詢
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
        /// <param name="pageIndex">分頁索引</param>
        /// <param name="pageSize">每頁顯示的記錄數</param>
        /// <param name="totalCount">總頁數</param>
        /// <returns>查詢結果DataTable</returns>
        public DataTable SelectEmpMove(bool Privileged, string sqlDep, string depName, string historyMove, string workNo, string localName, string applyMan, string moveType, string beforeValueName, string afterValueName, string moveState, string effectDateFrom, string effectDateTo, string moveReason, int pageIndex, int pageSize, out int totalCount)
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
                sqlstr += " and applyMan='" + applyMan + "'";
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
            DataTable dt = DalHelper.ExecutePagerQuery(sqlstr, pageIndex, pageSize, out totalCount);
            return dt;
        }
        /// <summary>
        /// 初始化異動類型
        /// </summary>
        /// 
        public DataTable InitddlMoveType()
        {
            string cmdText = "SELECT DataType,DataCode,DataValue,DataTypeDetail,(DataCode||'?B'||DataValue)as newDataValue FROM gds_att_TYPEDATA WHERE DataType='MoveType' and DataCode ='T01' ORDER BY OrderId";
            return DalHelper.ExecuteQuery(cmdText);
        }
        /// <summary>
        /// 初始化狀態
        /// </summary>
        public DataTable InitddlddlMoveState()
        {
            string cmdText = "SELECT DataType,DataCode,DataValue,DataTypeDetail,(DataCode||'?B'||DataValue)as newDataValue FROM gds_att_TYPEDATA WHERE DataType='MoveState' ORDER BY OrderId";
            return DalHelper.ExecuteQuery(cmdText);
        }
        /// <summary>
        /// 返回員工基本資料
        /// </summary>
        /// <param name="EmployeeNo">工號</param>
        /// <param name="Privileged">是否有組織權限</param>
        /// <returns>員工基本資料查詢結果</returns>
        public DataTable GetEmp(string EmployeeNo, bool Privileged, string sqlDep)
        {
            string condition = @"SELECT *
  FROM gds_att_employees_v a WHERE a.WorkNO=:p_EmployeeNo and a.status='0'  ";
            if (Privileged)
            {
                //if (moduleCode.Length != 0)
                //{
                //    condition += " AND a.depCode IN(SELECT depcode FROM gds_sc_persondept where personcode=:p_personcode and modulecode=:p_modulecode and companyid=:p_companyid)";

                //    return DalHelper.ExecuteQuery(condition, new OracleParameter(":p_EmployeeNo", EmployeeNo), new OracleParameter(":p_personcode", personCode), new OracleParameter(":p_modulecode", moduleCode), new OracleParameter(":p_companyid", companyID));
                //}
                //else
                //{
                //    condition += " AND a.depCode IN(SELECT 1 FROM dual )";

                //    return DalHelper.ExecuteQuery(condition, new OracleParameter(":p_EmployeeNo", EmployeeNo));
                //}
                condition += "AND a.depCode IN(" + sqlDep + ")";
            }

            return DalHelper.ExecuteQuery(condition, new OracleParameter(":p_EmployeeNo", EmployeeNo));
        }

        /// <summary>
        /// 根據異動類別點擊放大鏡，彈出窗口
        /// </summary>
        /// <param name="typeName">加班類別</param>
        /// <param name="typeCode">類別代碼</param>
        /// <param name="moveType">異動類型</param>
        /// <returns>查詢結果DataTable</returns>
        public DataTable GetDataByCondition(string typeName, string typeCode, string moveType)
        {
            string condition = "";
            condition = "SELECT DataType,DataCode,DataValue,DataTypeDetail,(DataCode||'?B'||DataValue)as newDataValue FROM GDS_ATT_TYPEDATA  WHERE DataType='OverTimeType' ";
            if (typeCode.Length != 0)
            {
                condition = condition + " and DataCode like '%" + typeCode + "%'";
            }
            if (typeName.Length != 0)
            {
                condition = condition + " and DataValue like '%" + typeName + "%'";
            }
            condition = condition + " ORDER BY OrderId ";
            DataTable dt = DalHelper.ExecuteQuery(condition);
            return dt;
        }
        /// <summary>
        /// 根據參數描述返回參數代碼
        /// </summary>
        /// <param name="moveValueName">參數描述</param>
        /// <param name="moveTypeValue">異動類型</param>
        /// <returns>參數代碼</returns>
        public DataTable GetDataCode(string moveValueName, string moveTypeValue)
        {
            string cmdText = "";
            DataTable dt = new DataTable();
            switch (moveTypeValue)
            {

                case "T01":
                    cmdText = "select DataCode,DataValue from gds_att_typedata where DataType='OverTimeType' and DataValue=:p_moveValueName";
                    dt = DalHelper.ExecuteQuery(cmdText, new OracleParameter(":p_moveValueName", moveValueName));
                    break;
            }
            return dt;
        }
        /// <summary>
        /// 查詢某工號是否已存在相同的異動記錄
        /// </summary>
        /// <param name="processFlag">標誌</param>
        /// <param name="beforeDataCode">異動前</param>
        /// <param name="afterDataCode">異動后</param>
        /// <param name="employeeNo">工號</param>
        /// <param name="effectDate">生效日期</param>
        /// <returns>是否已存在相同的異動記錄</returns>
        public DataTable IsExistMoveType(string processFlag, string beforeDataCode, string afterDataCode, string employeeNo, string effectDate, string moveOrder)
        {
            string cmdText = " SELECT workno FROM gds_att_empmove WHERE workno=:p_employeeNo AND aftervalue=:p_afterDataCode AND beforevalue=:p_beforeDataCode AND effectdate>=TO_DATE(:p_effectDate,'yyyy/mm/dd')";
            DataTable dt = new DataTable();
            if (processFlag == "Modify")
            {
                cmdText += " AND NOT moveorder=:p_moveOrder";
                dt = DalHelper.ExecuteQuery(cmdText, new OracleParameter(":p_moveOrder", moveOrder), new OracleParameter(":p_beforeDataCode", beforeDataCode), new OracleParameter(":p_afterDataCode", afterDataCode), new OracleParameter(":p_effectDate", effectDate), new OracleParameter(":p_employeeNo", employeeNo));
            }
            else
            {
                dt = DalHelper.ExecuteQuery(cmdText, new OracleParameter(":p_beforeDataCode", beforeDataCode), new OracleParameter(":p_afterDataCode", afterDataCode), new OracleParameter(":p_effectDate", effectDate), new OracleParameter(":p_employeeNo", employeeNo));
            }
            return dt;

        }
        /// <summary>
        /// 判斷新增生效日是否小于當前生效日
        /// </summary>
        /// <param name="employeeNo">工號</param>
        /// <param name="moveTypeValue">異動類別</param>
        /// <param name="effectDate">生效日期</param>
        /// <param name="moveOrder"></param>
        /// <returns>新增生效日是否小于當前生效日DataTable</returns>
        public DataTable checkDate(string processFlag, string employeeNo, string moveTypeValue, string effectDate, string moveOrder)
        {
            string cmdText = "  SELECT count(1) FROM gds_att_empmove WHERE WorkNo=:p_employeeNo  AND MoveTypeCode=:p_moveTypeValue AND effectDate>=TO_DATE(:p_effectDate,'yyyy/mm/dd')";
            DataTable dt = new DataTable();
            if (processFlag == "Modify")
            {
                cmdText += " AND NOT moveorder=:p_moveOrder";
                dt = DalHelper.ExecuteQuery(cmdText, new OracleParameter(":p_moveOrder", moveOrder), new OracleParameter(":p_employeeNo", employeeNo), new OracleParameter(":p_moveTypeValue", moveTypeValue), new OracleParameter(":p_effectDate", effectDate));
            }
            else
            {
                dt = DalHelper.ExecuteQuery(cmdText, new OracleParameter(":p_employeeNo", employeeNo), new OracleParameter(":p_moveTypeValue", moveTypeValue), new OracleParameter(":p_effectDate", effectDate));
            }
            return dt;
        }
        /// <summary>
        /// 新增修改異動類別
        /// </summary>
        /// <param name="processFlag">新增修改標誌</param>
        /// <param name="employeeNo">工號</param>
        /// <param name="moveTypeValue">異動值</param>
        /// <param name="beforeDataCode">異動前</param>
        /// <param name="afterDataCode">異動后</param>
        /// <param name="effectDate">生效日期</param>
        /// <param name="moveReason">異動原因</param>
        /// <param name="remark">備註</param>
        /// <param name="personCode">登陸工號</param>
        /// <param name="MoveOrder">異動序號</param>
        /// <returns>保存成功標誌</returns>
        public int SaveData(string processFlag, string employeeNo, string moveTypeValue, string beforeDataCode, string afterDataCode, string effectDate, string moveReason, string remark, string personCode, string moveOrder, SynclogModel logmodel)
        {
            string dtEffectDate = DateTime.Parse(effectDate).ToString("yyyy/MM/dd");
            int a = 0;
            string cmdText;
            if (processFlag == "Add")
            {
                logmodel.ProcessFlag = "insert";
                cmdText = "insert into gds_att_empmove(WorkNo,MoveOrder,MoveTypeCode,BeforeValue,AfterValue,EffectDate,MoveReason,Remark,ApplyMan,ApplyDate,State) values (:p_employeeNo,(select NVL(max(MoveOrder)+1,1) from gds_att_empmove where WorkNO=:p_employeeNo),:p_moveTypeValue,:p_beforeDataCode,:p_afterDataCode,to_date(:p_effectDate,'yyyy/mm/dd'),:p_moveReason,:p_remark,:p_personCode,sysdate,'0')";
                a = DalHelper.ExecuteNonQuery(cmdText, logmodel, new OracleParameter(":p_employeeNo", employeeNo), new OracleParameter(":p_moveTypeValue", moveTypeValue), new OracleParameter(":p_beforeDataCode", beforeDataCode), new OracleParameter(":p_afterDataCode", afterDataCode), new OracleParameter(":p_effectDate", dtEffectDate), new OracleParameter(":p_moveReason", moveReason), new OracleParameter(":p_remark", remark), new OracleParameter(":p_personCode", personCode));
            }
            else if (processFlag == "Modify")
            {
                logmodel.ProcessFlag = "update";
                cmdText = "update gds_att_empmove set MoveTypeCode=:p_moveTypeValue , BeforeValue=:p_beforeDataCode,AfterValue=:p_afterDataCode,EffectDate=to_date(:p_effectDate,'yyyy/mm/dd') ,MoveReason=:p_moveReason,Remark= :p_remark,ApplyMan=:p_personCode,ApplyDate=sysdate  where WorkNo=:p_employeeNo and MoveOrder =:p_moveOrder";
                a = DalHelper.ExecuteNonQuery(cmdText, logmodel, new OracleParameter(":p_employeeNo", employeeNo), new OracleParameter(":p_moveTypeValue", moveTypeValue), new OracleParameter(":p_beforeDataCode", beforeDataCode), new OracleParameter(":p_afterDataCode", afterDataCode), new OracleParameter(":p_effectDate", dtEffectDate), new OracleParameter(":p_moveReason", moveReason), new OracleParameter(":p_remark", remark), new OracleParameter(":p_personCode", personCode), new OracleParameter(":p_moveOrder", moveOrder));
            }
            return a;
        }
        /// <summary>
        /// 生成組織樹
        /// </summary>
        /// <param name="personCode">登陸工號</param>
        /// <param name="companID">公司ID</param>
        /// <param name="moudelCode">模組代碼</param>
        /// <returns>查詢結果DataTable</returns>
        public DataTable GetAuthorizedTreeDept(string personCode, string companID, string moudelCode)
        {
            string cmdText = @"SELECT  a.*
      FROM gds_sc_department a, gds_sc_deplevel b
     WHERE a.levelcode = b.levelcode
       AND a.depcode IN (
              SELECT depcode
                FROM gds_sc_persondept b
               WHERE b.personcode = :p_personCode
                 AND b.companyid = :p_companID
                 AND b.modulecode = :p_moudelCode) and  companyid=:p_companID ";

            if ((((moudelCode.StartsWith("HRM") && !moudelCode.Equals("HRMSYS137")) && (moudelCode.Equals("HRMSYS122") && !moudelCode.Equals("HRMSYS123"))) || ((moudelCode.Equals("PRTSYS301") || moudelCode.Equals("RPTSYS200")) || (moudelCode.Equals("RPTSYS203") || moudelCode.Equals("ETMSYS305")))) || moudelCode.Equals("ETMSYS319"))
            {
                cmdText = cmdText + " AND NOT b.head='-'  ";
            }
            cmdText += " START WITH a.levelcode = '0' CONNECT BY PRIOR a.depcode = a.parentdepcode ORDER SIBLINGS BY a.orderid";

            return DalHelper.ExecuteQuery(cmdText, new OracleParameter(":p_personCode", personCode), new OracleParameter(":p_companID", companID), new OracleParameter(":p_moudelCode", moudelCode));
        }
        /// <summary>
        /// 查詢異動
        /// </summary>
        /// <param name="MoveOrder">異動序號</param>
        /// <param name="EmployeeNo">工號</param>
        /// <param name="Privileged">是否有組織權限</param>
        /// <param name="moduleCode">模組代碼</param>
        /// <param name="personCode">登陸工號</param>
        /// <param name="companyID">公司ID</param>
        /// <returns>查詢結果DataTable</returns>
        public DataTable SelectEmpMove(string MoveOrder, string EmployeeNo, bool Privileged, string sqlDep)
        {
            string cmdText = @"select * from gds_att_movetype_v  a  where  a.WorkNO=:p_EmployeeNo and a.MoveOrder =:p_MoveOrder";
            //       string sqlDep = "";
            //if (moduleCode.Length > 0)
            //{
            //    sqlDep = "SELECT depcode FROM gds_sc_persondept where personcode=:p_personCode  and modulecode=:p_moduleCode and companyid=:p_companyID";
            //}
            //else
            //{
            //    sqlDep = "SELECT 1 FROM dual";
            //}
            if (Privileged)
            {
                cmdText += " AND exists (SELECT 1 FROM (" + sqlDep + ") e where e.DepCode=a.DepCode)";
            }
            DataTable dt = new DataTable();
            //if (moduleCode.Length > 0 && Privileged)
            //{
            //    dt = DalHelper.ExecuteQuery(cmdText, new OracleParameter(":p_EmployeeNo", EmployeeNo), new OracleParameter(":p_personCode", personCode), new OracleParameter(":p_MoveOrder", MoveOrder), new OracleParameter(":p_moduleCode", moduleCode), new OracleParameter(":p_companyID", companyID));
            //}
            //else
            //{
            //    dt = DalHelper.ExecuteQuery(cmdText, new OracleParameter(":p_EmployeeNo", EmployeeNo), new OracleParameter(":p_MoveOrder", MoveOrder));
            //}
            dt = DalHelper.ExecuteQuery(cmdText, new OracleParameter(":p_EmployeeNo", EmployeeNo), new OracleParameter(":p_MoveOrder", MoveOrder));
            return dt;
        }
        /// <summary>
        /// 導入
        /// </summary>
        /// <param name="createUser">登陸工號</param>
        /// <param name="moduleCode">模組代碼</param>
        /// <param name="companyID">公司ID</param>
        /// <param name="successnum">導入成功記錄數</param>
        /// <param name="errornum">導入失敗記錄數</param>
        /// <returns>導入失敗數據DataTable</returns>
        public DataTable ImpoertExcel(string createUser, string moduleCode, string companyID, out int successnum, out int errornum, SynclogModel logmodel)
        {
            OracleParameter outCursor = new OracleParameter("p_coursor", OracleType.Cursor);
            OracleParameter outSuccess = new OracleParameter("p_success", OracleType.Int32);
            OracleParameter outError = new OracleParameter("p_error", OracleType.Int32);
            outCursor.Direction = ParameterDirection.Output;
            outSuccess.Direction = ParameterDirection.Output;
            outError.Direction = ParameterDirection.Output;
            DataTable dt = DalHelper.ExecuteQuery("gds_sc_empmove_vaildata", CommandType.StoredProcedure,
                new OracleParameter("p_personcode", createUser),
                new OracleParameter("p_modulecode", moduleCode),
                new OracleParameter("p_companyid", companyID),
                outCursor, outSuccess, outError,
                new OracleParameter("p_transactiontype", logmodel.TransactionType == null ? "" : logmodel.TransactionType.ToString()),
                   new OracleParameter("p_levelno", logmodel.LevelNo == null ? "" : logmodel.LevelNo.ToString()),
                   new OracleParameter("p_fromhost", logmodel.FromHost == null ? "" : logmodel.FromHost.ToString()),
                   new OracleParameter("p_tohost", logmodel.ToHost == null ? "" : logmodel.ToHost.ToString()),
                   new OracleParameter("p_docno", logmodel.DocNo == null ? "" : logmodel.DocNo.ToString()),
                   new OracleParameter("p_processflag", logmodel.ProcessFlag == null ? "" : logmodel.ProcessFlag.ToString()),
                   new OracleParameter("p_processowner", logmodel.ProcessOwner == null ? "" : logmodel.ProcessOwner.ToString())
                );
            successnum = Convert.ToInt32(outSuccess.Value);
            errornum = Convert.ToInt32(outError.Value);
            return dt;
        }
        /// <summary>
        /// 刪除異動類別
        /// </summary>
        /// <param name="WorkNoMoveOrder">工號與異動序號字符串</param>
        /// <returns>刪除是否成功</returns>
        public int DeleteEmpMove(string WorkNoMoveOrder, SynclogModel logmodel)
        {
            OracleParameter outPara = new OracleParameter("RESULT", OracleType.Int32);
            outPara.Direction = ParameterDirection.Output;
            DalHelper.ExecuteQuery("gds_att_deleteempmove", CommandType.StoredProcedure,
                new OracleParameter("p_WorkNoMoveOrder", WorkNoMoveOrder),
                outPara,
                new OracleParameter("p_transactiontype", logmodel.TransactionType == null ? "" : logmodel.TransactionType.ToString()),
                new OracleParameter("p_levelno", logmodel.LevelNo == null ? "" : logmodel.LevelNo.ToString()),
                   new OracleParameter("p_fromhost", logmodel.FromHost == null ? "" : logmodel.FromHost.ToString()),
                   new OracleParameter("p_tohost", logmodel.ToHost == null ? "" : logmodel.ToHost.ToString()),
                   new OracleParameter("p_docno", logmodel.DocNo == null ? "" : logmodel.DocNo.ToString()),
                   new OracleParameter("p_processflag", logmodel.ProcessFlag == null ? "" : logmodel.ProcessFlag.ToString()),
                   new OracleParameter("p_processowner", logmodel.ProcessOwner == null ? "" : logmodel.ProcessOwner.ToString()));
            return Convert.ToInt32(outPara.Value);
        }
        /// <summary>
        /// 確認
        /// </summary>
        /// <param name="WorkNoMoveOrderAfterValue">工號與異動序號確認后字符串</param>
        /// <param name="personCode">登陸工號</param>
        /// <returns>確認是否成功</returns>
        public int ConfirmData(string WorkNoMoveOrderAfterValue, string personCode, SynclogModel logmodel)
        {
            OracleParameter outPara = new OracleParameter("RESULT", OracleType.Int32);
            outPara.Direction = ParameterDirection.Output;
            DalHelper.ExecuteQuery("gds_att_confirmempmove",
                CommandType.StoredProcedure,
                new OracleParameter("p_worknomoveorderaftervalue", WorkNoMoveOrderAfterValue),
                new OracleParameter("p_personcode", personCode),
                outPara, new OracleParameter("p_transactiontype", logmodel.TransactionType == null ? "" : logmodel.TransactionType.ToString()),
                   new OracleParameter("p_levelno", logmodel.LevelNo == null ? "" : logmodel.LevelNo.ToString()),
                   new OracleParameter("p_fromhost", logmodel.FromHost == null ? "" : logmodel.FromHost.ToString()),
                   new OracleParameter("p_tohost", logmodel.ToHost == null ? "" : logmodel.ToHost.ToString()),
                   new OracleParameter("p_docno", logmodel.DocNo == null ? "" : logmodel.DocNo.ToString()),
                   new OracleParameter("p_processflag", logmodel.ProcessFlag == null ? "" : logmodel.ProcessFlag.ToString()),
                   new OracleParameter("p_processowner", logmodel.ProcessOwner == null ? "" : logmodel.ProcessOwner.ToString())

                );
            return Convert.ToInt32(outPara.Value);
        }
        /// <summary>
        /// 取消確認
        /// </summary>
        /// <param name="WorkNoMoveOrderBeforeValue">工號與異動序號確認前字符串</param>
        /// <param name="personCode">登陸工號</param>
        /// <returns>取消確認是否成功</returns>
        public int UnConfirmData(string WorkNoMoveOrderBeforeValue, string personCode, SynclogModel logmodel)
        {
            OracleParameter outPara = new OracleParameter("RESULT", OracleType.Int32);
            outPara.Direction = ParameterDirection.Output;
            DalHelper.ExecuteQuery("gds_att_unconfirmempmove",
                CommandType.StoredProcedure,
                new OracleParameter("p_worknomoveorderbeforevalue", WorkNoMoveOrderBeforeValue),
                new OracleParameter("p_personcode", personCode),
                outPara,
                new OracleParameter("p_transactiontype", logmodel.TransactionType == null ? "" : logmodel.TransactionType.ToString()),
                   new OracleParameter("p_levelno", logmodel.LevelNo == null ? "" : logmodel.LevelNo.ToString()),
                   new OracleParameter("p_fromhost", logmodel.FromHost == null ? "" : logmodel.FromHost.ToString()),
                   new OracleParameter("p_tohost", logmodel.ToHost == null ? "" : logmodel.ToHost.ToString()),
                   new OracleParameter("p_docno", logmodel.DocNo == null ? "" : logmodel.DocNo.ToString()),
                   new OracleParameter("p_processflag", logmodel.ProcessFlag == null ? "" : logmodel.ProcessFlag.ToString()),
                   new OracleParameter("p_processowner", logmodel.ProcessOwner == null ? "" : logmodel.ProcessOwner.ToString())
                );
            return Convert.ToInt32(outPara.Value);
        }
        /// <summary>
        /// 查詢所有異動，用於批量確認
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
        /// <returns>查詢結果DataTable</returns>
        public DataTable SelectEmpMove(bool Privileged, string sqlDep, string depName, string historyMove, string workNo, string localName, string applyMan, string moveType, string beforeValueName, string afterValueName, string moveState, string effectDateFrom, string effectDateTo, string moveReason)
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
                    sqlstr += " and exists (SELECT 1 FROM (" + sqlDep + ") e where e.DepCode=a.DepCode)";
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
                sqlstr += " and applyMan='" + applyMan + "'";
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
            return dt;
        }
        /// <summary>
        /// 顯示失效的組織
        /// </summary>
        /// <param name="Personcode">工號</param>
        /// <param name="moudelCode">模組代碼</param>
        /// <param name="CompanyId">公司ID</param>
        /// <param name="levelcode">組織級別</param>
        /// <param name="abate">是否顯示失效組織</param>
        /// <param name="logmodel">日誌管理實體類</param>
        public void GetDepCodeTable(string Personcode, string moudelCode, string CompanyId, string levelcode, string abate, SynclogModel logmodel)
        {
            DalHelper.ExecuteQuery("gds_att_getdepsql", CommandType.StoredProcedure, new OracleParameter("v_personcode", Personcode), new OracleParameter("v_modulecode", moudelCode), new OracleParameter("v_companyid", CompanyId), new OracleParameter("v_levelcode", levelcode), new OracleParameter("v_abate", abate), new OracleParameter("p_transactiontype", logmodel.TransactionType == null ? "" : logmodel.TransactionType.ToString()),
                   new OracleParameter("p_levelno", logmodel.LevelNo == null ? "" : logmodel.LevelNo.ToString()),
                   new OracleParameter("p_fromhost", logmodel.FromHost == null ? "" : logmodel.FromHost.ToString()),
                   new OracleParameter("p_tohost", logmodel.ToHost == null ? "" : logmodel.ToHost.ToString()),
                   new OracleParameter("p_docno", logmodel.DocNo == null ? "" : logmodel.DocNo.ToString()),
                   new OracleParameter("p_processflag", logmodel.ProcessFlag == null ? "" : logmodel.ProcessFlag.ToString()),
                   new OracleParameter("p_processowner", logmodel.ProcessOwner == null ? "" : logmodel.ProcessOwner.ToString()));
        }

    }
}
