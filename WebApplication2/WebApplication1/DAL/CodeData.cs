using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.DBUtility;//Please add references
namespace Maticsoft.DAL
{
	/// <summary>
	/// 数据访问类:CodeData
	/// </summary>
	public partial class CodeData
	{
		public CodeData()
		{}
		#region  BasicMethod

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string Code)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from CodeData");
			strSql.Append(" where Code=@Code ");
			SqlParameter[] parameters = {
					new SqlParameter("@Code", SqlDbType.NVarChar,50)			};
			parameters[0].Value = Code;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(Maticsoft.Model.CodeData model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into CodeData(");
			strSql.Append("Code,Remark,IsValidated,ValidatedTime,IsExchanged,ExchangeTime,PhoneNumber)");
			strSql.Append(" values (");
			strSql.Append("@Code,@Remark,@IsValidated,@ValidatedTime,@IsExchanged,@ExchangeTime,@PhoneNumber)");
			SqlParameter[] parameters = {
					new SqlParameter("@Code", SqlDbType.NVarChar,50),
					new SqlParameter("@Remark", SqlDbType.NVarChar,50),
					new SqlParameter("@IsValidated", SqlDbType.Bit,1),
					new SqlParameter("@ValidatedTime", SqlDbType.DateTime),
					new SqlParameter("@IsExchanged", SqlDbType.Bit,1),
					new SqlParameter("@ExchangeTime", SqlDbType.DateTime),
					new SqlParameter("@PhoneNumber", SqlDbType.NVarChar,50)};
			parameters[0].Value = model.Code;
			parameters[1].Value = model.Remark;
			parameters[2].Value = model.IsValidated;
			parameters[3].Value = model.ValidatedTime;
			parameters[4].Value = model.IsExchanged;
			parameters[5].Value = model.ExchangeTime;
			parameters[6].Value = model.PhoneNumber;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
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
		public bool Update(Maticsoft.Model.CodeData model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update CodeData set ");
			strSql.Append("Remark=@Remark,");
			strSql.Append("IsValidated=@IsValidated,");
			strSql.Append("ValidatedTime=@ValidatedTime,");
			strSql.Append("IsExchanged=@IsExchanged,");
			strSql.Append("ExchangeTime=@ExchangeTime,");
			strSql.Append("PhoneNumber=@PhoneNumber");
			strSql.Append(" where Code=@Code ");
			SqlParameter[] parameters = {
					new SqlParameter("@Remark", SqlDbType.NVarChar,50),
					new SqlParameter("@IsValidated", SqlDbType.Bit,1),
					new SqlParameter("@ValidatedTime", SqlDbType.DateTime),
					new SqlParameter("@IsExchanged", SqlDbType.Bit,1),
					new SqlParameter("@ExchangeTime", SqlDbType.DateTime),
					new SqlParameter("@PhoneNumber", SqlDbType.NVarChar,50),
					new SqlParameter("@Code", SqlDbType.NVarChar,50)};
			parameters[0].Value = model.Remark;
			parameters[1].Value = model.IsValidated;
			parameters[2].Value = model.ValidatedTime;
			parameters[3].Value = model.IsExchanged;
			parameters[4].Value = model.ExchangeTime;
			parameters[5].Value = model.PhoneNumber;
			parameters[6].Value = model.Code;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
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
		public bool Delete(string Code)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from CodeData ");
			strSql.Append(" where Code=@Code ");
			SqlParameter[] parameters = {
					new SqlParameter("@Code", SqlDbType.NVarChar,50)			};
			parameters[0].Value = Code;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
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
		public bool DeleteList(string Codelist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from CodeData ");
			strSql.Append(" where Code in ("+Codelist + ")  ");
			int rows=DbHelperSQL.ExecuteSql(strSql.ToString());
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
		public Maticsoft.Model.CodeData GetModel(string Code)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 Code,Remark,IsValidated,ValidatedTime,IsExchanged,ExchangeTime,PhoneNumber from CodeData ");
			strSql.Append(" where Code=@Code ");
			SqlParameter[] parameters = {
					new SqlParameter("@Code", SqlDbType.NVarChar,50)			};
			parameters[0].Value = Code;

			Maticsoft.Model.CodeData model=new Maticsoft.Model.CodeData();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
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
		public Maticsoft.Model.CodeData DataRowToModel(DataRow row)
		{
			Maticsoft.Model.CodeData model=new Maticsoft.Model.CodeData();
			if (row != null)
			{
				if(row["Code"]!=null)
				{
					model.Code=row["Code"].ToString();
				}
				if(row["Remark"]!=null)
				{
					model.Remark=row["Remark"].ToString();
				}
				if(row["IsValidated"]!=null && row["IsValidated"].ToString()!="")
				{
					if((row["IsValidated"].ToString()=="1")||(row["IsValidated"].ToString().ToLower()=="true"))
					{
						model.IsValidated=true;
					}
					else
					{
						model.IsValidated=false;
					}
				}
				if(row["ValidatedTime"]!=null && row["ValidatedTime"].ToString()!="")
				{
					model.ValidatedTime=DateTime.Parse(row["ValidatedTime"].ToString());
				}
				if(row["IsExchanged"]!=null && row["IsExchanged"].ToString()!="")
				{
					if((row["IsExchanged"].ToString()=="1")||(row["IsExchanged"].ToString().ToLower()=="true"))
					{
						model.IsExchanged=true;
					}
					else
					{
						model.IsExchanged=false;
					}
				}
				if(row["ExchangeTime"]!=null && row["ExchangeTime"].ToString()!="")
				{
					model.ExchangeTime=DateTime.Parse(row["ExchangeTime"].ToString());
				}
				if(row["PhoneNumber"]!=null)
				{
					model.PhoneNumber=row["PhoneNumber"].ToString();
				}
			}
			return model;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select Code,Remark,IsValidated,ValidatedTime,IsExchanged,ExchangeTime,PhoneNumber ");
			strSql.Append(" FROM CodeData ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
			strSql.Append(" Code,Remark,IsValidated,ValidatedTime,IsExchanged,ExchangeTime,PhoneNumber ");
			strSql.Append(" FROM CodeData ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获取记录总数
		/// </summary>
		public int GetRecordCount(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) FROM CodeData ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
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
			StringBuilder strSql=new StringBuilder();
			strSql.Append("SELECT * FROM ( ");
			strSql.Append(" SELECT ROW_NUMBER() OVER (");
			if (!string.IsNullOrEmpty(orderby.Trim()))
			{
				strSql.Append("order by T." + orderby );
			}
			else
			{
				strSql.Append("order by T.Code desc");
			}
			strSql.Append(")AS Row, T.*  from CodeData T ");
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
			parameters[0].Value = "CodeData";
			parameters[1].Value = "Code";
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

        public DataTable QueryDataTableByPhonenumber(string phonenumber)
        {
            string sql = string.Format("SELECT Code,Remark ,case when IsValidated=1 then '已上传' else '未上传' end IsValidated,ValidatedTime,case when IsExchanged=1 then '已兑换' else '未兑换' end IsExchanged,ExchangeTime,PhoneNumber FROM CodeData where PhoneNumber='{0}'",phonenumber);
            DataTable dt = DbHelperSQL.Query(sql).Tables[0];
            if (dt != null)
            {
                return dt;
            }
            else
            {
                return null;
            }
        }

        public DataTable QueryDataTableByCode(string code)
        {
            string sql = string.Format("SELECT Code,Remark ,case when IsValidated=1 then '已上传' else '未上传' end IsValidated,ValidatedTime,case when IsExchanged=1 then '已兑换' else '未兑换' end IsExchanged,ExchangeTime,PhoneNumber FROM CodeData where Code='{0}'", code);
            DataTable dt = DbHelperSQL.Query(sql).Tables[0];
            if (dt != null)
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

