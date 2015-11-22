using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.DBUtility;//Please add references
namespace Maticsoft.DAL
{
	/// <summary>
	/// 数据访问类:CustomerData
	/// </summary>
	public partial class CustomerData
	{
		public CustomerData()
		{}
		#region  BasicMethod

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string PhoneNumber)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from CustomerData");
			strSql.Append(" where PhoneNumber=@PhoneNumber ");
			SqlParameter[] parameters = {
					new SqlParameter("@PhoneNumber", SqlDbType.NVarChar,50)			};
			parameters[0].Value = PhoneNumber;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(Maticsoft.Model.CustomerData model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into CustomerData(");
			strSql.Append("PhoneNumber,CustomerName,Address,MiAvailable,JingAvailable,ZhunAvailable,BaoAvaivable,GiftPacks,ProductPacks)");
			strSql.Append(" values (");
			strSql.Append("@PhoneNumber,@CustomerName,@Address,@MiAvailable,@JingAvailable,@ZhunAvailable,@BaoAvaivable,@GiftPacks,@ProductPacks)");
			SqlParameter[] parameters = {
					new SqlParameter("@PhoneNumber", SqlDbType.NVarChar,50),
					new SqlParameter("@CustomerName", SqlDbType.NVarChar,50),
					new SqlParameter("@Address", SqlDbType.NVarChar,100),
					new SqlParameter("@MiAvailable", SqlDbType.Int,4),
					new SqlParameter("@JingAvailable", SqlDbType.Int,4),
					new SqlParameter("@ZhunAvailable", SqlDbType.Int,4),
					new SqlParameter("@BaoAvaivable", SqlDbType.Int,4),
					new SqlParameter("@GiftPacks", SqlDbType.Int,4),
					new SqlParameter("@ProductPacks", SqlDbType.Int,4)};
			parameters[0].Value = model.PhoneNumber;
			parameters[1].Value = model.CustomerName;
			parameters[2].Value = model.Address;
			parameters[3].Value = model.MiAvailable;
			parameters[4].Value = model.JingAvailable;
			parameters[5].Value = model.ZhunAvailable;
			parameters[6].Value = model.BaoAvaivable;
			parameters[7].Value = model.GiftPacks;
			parameters[8].Value = model.ProductPacks;

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
		public bool Update(Maticsoft.Model.CustomerData model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update CustomerData set ");
			strSql.Append("CustomerName=@CustomerName,");
			strSql.Append("Address=@Address,");
			strSql.Append("MiAvailable=@MiAvailable,");
			strSql.Append("JingAvailable=@JingAvailable,");
			strSql.Append("ZhunAvailable=@ZhunAvailable,");
			strSql.Append("BaoAvaivable=@BaoAvaivable,");
			strSql.Append("GiftPacks=@GiftPacks,");
			strSql.Append("ProductPacks=@ProductPacks");
			strSql.Append(" where PhoneNumber=@PhoneNumber ");
			SqlParameter[] parameters = {
					new SqlParameter("@CustomerName", SqlDbType.NVarChar,50),
					new SqlParameter("@Address", SqlDbType.NVarChar,100),
					new SqlParameter("@MiAvailable", SqlDbType.Int,4),
					new SqlParameter("@JingAvailable", SqlDbType.Int,4),
					new SqlParameter("@ZhunAvailable", SqlDbType.Int,4),
					new SqlParameter("@BaoAvaivable", SqlDbType.Int,4),
					new SqlParameter("@GiftPacks", SqlDbType.Int,4),
					new SqlParameter("@ProductPacks", SqlDbType.Int,4),
					new SqlParameter("@PhoneNumber", SqlDbType.NVarChar,50)};
			parameters[0].Value = model.CustomerName;
			parameters[1].Value = model.Address;
			parameters[2].Value = model.MiAvailable;
			parameters[3].Value = model.JingAvailable;
			parameters[4].Value = model.ZhunAvailable;
			parameters[5].Value = model.BaoAvaivable;
			parameters[6].Value = model.GiftPacks;
			parameters[7].Value = model.ProductPacks;
			parameters[8].Value = model.PhoneNumber;

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
		public bool Delete(string PhoneNumber)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from CustomerData ");
			strSql.Append(" where PhoneNumber=@PhoneNumber ");
			SqlParameter[] parameters = {
					new SqlParameter("@PhoneNumber", SqlDbType.NVarChar,50)			};
			parameters[0].Value = PhoneNumber;

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
		public bool DeleteList(string PhoneNumberlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from CustomerData ");
			strSql.Append(" where PhoneNumber in ("+PhoneNumberlist + ")  ");
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
		public Maticsoft.Model.CustomerData GetModel(string PhoneNumber)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 PhoneNumber,CustomerName,Address,MiAvailable,JingAvailable,ZhunAvailable,BaoAvaivable,GiftPacks,ProductPacks from CustomerData ");
			strSql.Append(" where PhoneNumber=@PhoneNumber ");
			SqlParameter[] parameters = {
					new SqlParameter("@PhoneNumber", SqlDbType.NVarChar,50)			};
			parameters[0].Value = PhoneNumber;

			Maticsoft.Model.CustomerData model=new Maticsoft.Model.CustomerData();
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
		public Maticsoft.Model.CustomerData DataRowToModel(DataRow row)
		{
			Maticsoft.Model.CustomerData model=new Maticsoft.Model.CustomerData();
			if (row != null)
			{
				if(row["PhoneNumber"]!=null)
				{
					model.PhoneNumber=row["PhoneNumber"].ToString();
				}
				if(row["CustomerName"]!=null)
				{
					model.CustomerName=row["CustomerName"].ToString();
				}
				if(row["Address"]!=null)
				{
					model.Address=row["Address"].ToString();
				}
				if(row["MiAvailable"]!=null && row["MiAvailable"].ToString()!="")
				{
					model.MiAvailable=int.Parse(row["MiAvailable"].ToString());
				}
				if(row["JingAvailable"]!=null && row["JingAvailable"].ToString()!="")
				{
					model.JingAvailable=int.Parse(row["JingAvailable"].ToString());
				}
				if(row["ZhunAvailable"]!=null && row["ZhunAvailable"].ToString()!="")
				{
					model.ZhunAvailable=int.Parse(row["ZhunAvailable"].ToString());
				}
				if(row["BaoAvaivable"]!=null && row["BaoAvaivable"].ToString()!="")
				{
					model.BaoAvaivable=int.Parse(row["BaoAvaivable"].ToString());
				}
				if(row["GiftPacks"]!=null && row["GiftPacks"].ToString()!="")
				{
					model.GiftPacks=int.Parse(row["GiftPacks"].ToString());
				}
				if(row["ProductPacks"]!=null && row["ProductPacks"].ToString()!="")
				{
					model.ProductPacks=int.Parse(row["ProductPacks"].ToString());
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
			strSql.Append("select PhoneNumber,CustomerName,Address,MiAvailable,JingAvailable,ZhunAvailable,BaoAvaivable,GiftPacks,ProductPacks ");
			strSql.Append(" FROM CustomerData ");
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
			strSql.Append(" PhoneNumber,CustomerName,Address,MiAvailable,JingAvailable,ZhunAvailable,BaoAvaivable,GiftPacks,ProductPacks ");
			strSql.Append(" FROM CustomerData ");
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
			strSql.Append("select count(1) FROM CustomerData ");
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
				strSql.Append("order by T.PhoneNumber desc");
			}
			strSql.Append(")AS Row, T.*  from CustomerData T ");
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
			parameters[0].Value = "CustomerData";
			parameters[1].Value = "PhoneNumber";
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

