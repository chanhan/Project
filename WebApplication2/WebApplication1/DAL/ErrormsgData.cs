/**  版本信息模板在安装目录下，可自行修改。
* ErrormsgData.cs
*
* 功 能： N/A
* 类 名： ErrormsgData
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/11/14 16:10:20   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.DBUtility;//Please add references
namespace Maticsoft.DAL
{
    /// <summary>
    /// 数据访问类:ErrormsgData
    /// </summary>
    public partial class ErrormsgData
    {
        public ErrormsgData()
        { }
        #region  BasicMethod

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("ReturnedID", "ErrormsgData");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ReturnedID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from ErrormsgData");
            strSql.Append(" where ReturnedID=@ReturnedID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ReturnedID", SqlDbType.Int,4)			};
            parameters[0].Value = ReturnedID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(Maticsoft.Model.ErrormsgData model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into ErrormsgData(");
            strSql.Append("ReturnTime,PhoneNumber,ReturnedID,Errormsg)");
            strSql.Append(" values (");
            strSql.Append("@ReturnTime,@PhoneNumber,@ReturnedID,@Errormsg)");
            SqlParameter[] parameters = {
					new SqlParameter("@ReturnTime", SqlDbType.DateTime),
					new SqlParameter("@PhoneNumber", SqlDbType.NChar,50),
					new SqlParameter("@ReturnedID", SqlDbType.Int,4),
					new SqlParameter("@Errormsg", SqlDbType.NChar,200)};
            parameters[0].Value = model.ReturnTime;
            parameters[1].Value = model.PhoneNumber;
            parameters[2].Value = model.ReturnedID;
            parameters[3].Value = model.Errormsg;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Maticsoft.Model.ErrormsgData model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update ErrormsgData set ");
            strSql.Append("ReturnTime=@ReturnTime,");
            strSql.Append("PhoneNumber=@PhoneNumber,");
            strSql.Append("Errormsg=@Errormsg");
            strSql.Append(" where ReturnedID=@ReturnedID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ReturnTime", SqlDbType.DateTime),
					new SqlParameter("@PhoneNumber", SqlDbType.NChar,50),
					new SqlParameter("@Errormsg", SqlDbType.NChar,200),
					new SqlParameter("@ReturnedID", SqlDbType.Int,4)};
            parameters[0].Value = model.ReturnTime;
            parameters[1].Value = model.PhoneNumber;
            parameters[2].Value = model.Errormsg;
            parameters[3].Value = model.ReturnedID;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int ReturnedID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from ErrormsgData ");
            strSql.Append(" where ReturnedID=@ReturnedID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ReturnedID", SqlDbType.Int,4)			};
            parameters[0].Value = ReturnedID;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 批量删除数据
        /// </summary>
        public bool DeleteList(string ReturnedIDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from ErrormsgData ");
            strSql.Append(" where ReturnedID in (" + ReturnedIDlist + ")  ");
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Maticsoft.Model.ErrormsgData GetModel(int ReturnedID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ReturnTime,PhoneNumber,ReturnedID,Errormsg from ErrormsgData ");
            strSql.Append(" where ReturnedID=@ReturnedID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ReturnedID", SqlDbType.Int,4)			};
            parameters[0].Value = ReturnedID;

            Maticsoft.Model.ErrormsgData model = new Maticsoft.Model.ErrormsgData();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Maticsoft.Model.ErrormsgData DataRowToModel(DataRow row)
        {
            Maticsoft.Model.ErrormsgData model = new Maticsoft.Model.ErrormsgData();
            if (row != null)
            {
                if (row["ReturnTime"] != null && row["ReturnTime"].ToString() != "")
                {
                    model.ReturnTime = DateTime.Parse(row["ReturnTime"].ToString());
                }
                if (row["PhoneNumber"] != null)
                {
                    model.PhoneNumber = row["PhoneNumber"].ToString();
                }
                if (row["ReturnedID"] != null && row["ReturnedID"].ToString() != "")
                {
                    model.ReturnedID = int.Parse(row["ReturnedID"].ToString());
                }
                if (row["Errormsg"] != null)
                {
                    model.Errormsg = row["Errormsg"].ToString();
                }
            }
            return model;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ReturnTime,PhoneNumber,ReturnedID,Errormsg ");
            strSql.Append(" FROM ErrormsgData ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" ReturnTime,PhoneNumber,ReturnedID,Errormsg ");
            strSql.Append(" FROM ErrormsgData ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM ErrormsgData ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            object obj = DbHelperSQL.GetSingle(strSql.ToString());
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.ReturnedID desc");
            }
            strSql.Append(")AS Row, T.*  from ErrormsgData T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /*
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        {
            SqlParameter[] parameters = {
                    new SqlParameter("@tblName", SqlDbType.VarChar, 255),
                    new SqlParameter("@fldName", SqlDbType.VarChar, 255),
                    new SqlParameter("@PageSize", SqlDbType.Int),
                    new SqlParameter("@PageIndex", SqlDbType.Int),
                    new SqlParameter("@IsReCount", SqlDbType.Bit),
                    new SqlParameter("@OrderType", SqlDbType.Bit),
                    new SqlParameter("@strWhere", SqlDbType.VarChar,1000),
                    };
            parameters[0].Value = "ErrormsgData";
            parameters[1].Value = "ReturnedID";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        #endregion  BasicMethod
        #region  ExtensionMethod

        #endregion  ExtensionMethod
    }
}

