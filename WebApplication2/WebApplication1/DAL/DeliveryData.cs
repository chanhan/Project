/**  版本信息模板在安装目录下，可自行修改。
* DeliveryData.cs
*
* 功 能： N/A
* 类 名： DeliveryData
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/11/15 11:43:55   N/A    初版
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
using Maticsoft.DBUtility;
using System.Collections.Generic;//Please add references
namespace Maticsoft.DAL
{
    /// <summary>
    /// 数据访问类:DeliveryData
    /// </summary>
    public partial class DeliveryData
    {
        public DeliveryData()
        { }
        #region  BasicMethod

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string PhoneNumber, DateTime DeliveryDate)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from DeliveryData");
            strSql.Append(" where PhoneNumber=@PhoneNumber and DeliveryDate=@DeliveryDate ");
            SqlParameter[] parameters = {
					new SqlParameter("@PhoneNumber", SqlDbType.NVarChar,50),
					new SqlParameter("@DeliveryDate", SqlDbType.DateTime)			};
            parameters[0].Value = PhoneNumber;
            parameters[1].Value = DeliveryDate;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(Maticsoft.Model.DeliveryData model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into DeliveryData(");
            strSql.Append("PhoneNumber,DeliveryDate,CustomerName,Province,City,Address,CourierID,CourierCompanyName,GiftPacks,ProductPacks)");
            strSql.Append(" values (");
            strSql.Append("@PhoneNumber,@DeliveryDate,@CustomerName,@Province,@City,@Address,@CourierID,@CourierCompanyName,@GiftPacks,@ProductPacks)");
            SqlParameter[] parameters = {
					new SqlParameter("@PhoneNumber", SqlDbType.NVarChar,50),
					new SqlParameter("@DeliveryDate", SqlDbType.DateTime),
					new SqlParameter("@CustomerName", SqlDbType.NVarChar,50),
					new SqlParameter("@Province", SqlDbType.NVarChar,50),
					new SqlParameter("@City", SqlDbType.NVarChar,50),
					new SqlParameter("@Address", SqlDbType.NVarChar,100),
					new SqlParameter("@CourierID", SqlDbType.NVarChar,50),
					new SqlParameter("@CourierCompanyName", SqlDbType.NVarChar,50),
					new SqlParameter("@GiftPacks", SqlDbType.Int,4),
					new SqlParameter("@ProductPacks", SqlDbType.Int,4)};
            parameters[0].Value = model.PhoneNumber;
            parameters[1].Value = model.DeliveryDate;
            parameters[2].Value = model.CustomerName;
            parameters[3].Value = model.Province;
            parameters[4].Value = model.City;
            parameters[5].Value = model.Address;
            parameters[6].Value = model.CourierID;
            parameters[7].Value = model.CourierCompanyName;
            parameters[8].Value = model.GiftPacks;
            parameters[9].Value = model.ProductPacks;

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
        public bool Update(Maticsoft.Model.DeliveryData model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update DeliveryData set ");
            strSql.Append("CustomerName=@CustomerName,");
            strSql.Append("Province=@Province,");
            strSql.Append("City=@City,");
            strSql.Append("Address=@Address,");
            strSql.Append("CourierID=@CourierID,");
            strSql.Append("CourierCompanyName=@CourierCompanyName,");
            strSql.Append("GiftPacks=@GiftPacks,");
            strSql.Append("ProductPacks=@ProductPacks");
            strSql.Append(" where PhoneNumber=@PhoneNumber and DeliveryDate=@DeliveryDate ");
            SqlParameter[] parameters = {
					new SqlParameter("@CustomerName", SqlDbType.NVarChar,50),
					new SqlParameter("@Province", SqlDbType.NVarChar,50),
					new SqlParameter("@City", SqlDbType.NVarChar,50),
					new SqlParameter("@Address", SqlDbType.NVarChar,100),
					new SqlParameter("@CourierID", SqlDbType.NVarChar,50),
					new SqlParameter("@CourierCompanyName", SqlDbType.NVarChar,50),
					new SqlParameter("@GiftPacks", SqlDbType.Int,4),
					new SqlParameter("@ProductPacks", SqlDbType.Int,4),
					new SqlParameter("@PhoneNumber", SqlDbType.NVarChar,50),
					new SqlParameter("@DeliveryDate", SqlDbType.DateTime)};
            parameters[0].Value = model.CustomerName;
            parameters[1].Value = model.Province;
            parameters[2].Value = model.City;
            parameters[3].Value = model.Address;
            parameters[4].Value = model.CourierID;
            parameters[5].Value = model.CourierCompanyName;
            parameters[6].Value = model.GiftPacks;
            parameters[7].Value = model.ProductPacks;
            parameters[8].Value = model.PhoneNumber;
            parameters[9].Value = model.DeliveryDate;

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
        public bool Delete(string PhoneNumber, DateTime DeliveryDate)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from DeliveryData ");
            strSql.Append(" where PhoneNumber=@PhoneNumber and DeliveryDate=@DeliveryDate ");
            SqlParameter[] parameters = {
					new SqlParameter("@PhoneNumber", SqlDbType.NVarChar,50),
					new SqlParameter("@DeliveryDate", SqlDbType.DateTime)			};
            parameters[0].Value = PhoneNumber;
            parameters[1].Value = DeliveryDate;

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
        /// 得到一个对象实体
        /// </summary>
        public Maticsoft.Model.DeliveryData GetModel(string PhoneNumber, DateTime DeliveryDate)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 PhoneNumber,DeliveryDate,CustomerName,Province,City,Address,CourierID,CourierCompanyName,GiftPacks,ProductPacks from DeliveryData ");
            strSql.Append(" where PhoneNumber=@PhoneNumber and DeliveryDate=@DeliveryDate ");
            SqlParameter[] parameters = {
					new SqlParameter("@PhoneNumber", SqlDbType.NVarChar,50),
					new SqlParameter("@DeliveryDate", SqlDbType.DateTime)			};
            parameters[0].Value = PhoneNumber;
            parameters[1].Value = DeliveryDate;

            Maticsoft.Model.DeliveryData model = new Maticsoft.Model.DeliveryData();
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
        public Maticsoft.Model.DeliveryData DataRowToModel(DataRow row)
        {
            Maticsoft.Model.DeliveryData model = new Maticsoft.Model.DeliveryData();
            if (row != null)
            {
                if (row["PhoneNumber"] != null)
                {
                    model.PhoneNumber = row["PhoneNumber"].ToString();
                }
                if (row["DeliveryDate"] != null && row["DeliveryDate"].ToString() != "")
                {
                    model.DeliveryDate = DateTime.Parse(row["DeliveryDate"].ToString());
                }
                if (row["CustomerName"] != null)
                {
                    model.CustomerName = row["CustomerName"].ToString();
                }
                if (row["Province"] != null)
                {
                    model.Province = row["Province"].ToString();
                }
                if (row["City"] != null)
                {
                    model.City = row["City"].ToString();
                }
                if (row["Address"] != null)
                {
                    model.Address = row["Address"].ToString();
                }
                if (row["CourierID"] != null)
                {
                    model.CourierID = row["CourierID"].ToString();
                }
                if (row["CourierCompanyName"] != null)
                {
                    model.CourierCompanyName = row["CourierCompanyName"].ToString();
                }
                if (row["GiftPacks"] != null && row["GiftPacks"].ToString() != "")
                {
                    model.GiftPacks = int.Parse(row["GiftPacks"].ToString());
                }
                if (row["ProductPacks"] != null && row["ProductPacks"].ToString() != "")
                {
                    model.ProductPacks = int.Parse(row["ProductPacks"].ToString());
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
            strSql.Append("select PhoneNumber,DeliveryDate,CustomerName,Province,City,Address,CourierID,CourierCompanyName,GiftPacks,ProductPacks ");
            strSql.Append(" FROM DeliveryData ");
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
            strSql.Append(" PhoneNumber,DeliveryDate,CustomerName,Province,City,Address,CourierID,CourierCompanyName,GiftPacks,ProductPacks ");
            strSql.Append(" FROM DeliveryData ");
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
            strSql.Append("select count(1) FROM DeliveryData ");
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
                strSql.Append("order by T.DeliveryDate desc");
            }
            strSql.Append(")AS Row, T.*  from DeliveryData T ");
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
            parameters[0].Value = "DeliveryData";
            parameters[1].Value = "DeliveryDate";
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

        public List<Model.DeliveryData> Query(string phone)
        {
            string strSql = string.Format("SELECT PhoneNumber,DeliveryDate,CustomerName,Province,City,Address,CourierID,CourierCompanyName ,GiftPacks ,ProductPacks FROM DeliveryData where PhoneNumber='{0}'", phone);
            DataTable dt = DbHelperSQL.Query(strSql).Tables[0];
            List<Model.DeliveryData> list = new List<Model.DeliveryData>();
            foreach (DataRow dr in dt.Rows)
            {
                list.Add(DataRowToModel(dr));
            }
            return list;
        }
        public DataTable QueryDataTable(string phone)
        {
            string strSql = string.Format("SELECT PhoneNumber,DeliveryDate,CustomerName,Province,City,Address,CourierID,CourierCompanyName ,GiftPacks ,ProductPacks FROM DeliveryData where PhoneNumber='{0}'", phone);
            DataTable dt = DbHelperSQL.Query(strSql).Tables[0];
            if (dt!=null)
            {
                return dt;   
            }
            else
            {
                return null;
            }
        }
    }
}

