using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.DBUtility;//Please add references
namespace Maticsoft.DAL
{
	/// <summary>
	/// 数据访问类:MsgData
	/// </summary>
	public partial class MsgData
	{
		public MsgData()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("MsgID", "MsgData"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int MsgID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from MsgData");
			strSql.Append(" where MsgID=@MsgID");
			SqlParameter[] parameters = {
					new SqlParameter("@MsgID", SqlDbType.Int,4)
			};
			parameters[0].Value = MsgID;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(Maticsoft.Model.MsgData model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into MsgData(");
			strSql.Append("PhoneNumber,Msg,MsgTime,IsRecivedMsg,ReciveMsgStatus,ReturnedID)");
			strSql.Append(" values (");
			strSql.Append("@PhoneNumber,@Msg,@MsgTime,@IsRecivedMsg,@ReciveMsgStatus,@ReturnedID)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@PhoneNumber", SqlDbType.NVarChar,50),
					new SqlParameter("@Msg", SqlDbType.NVarChar,200),
					new SqlParameter("@MsgTime", SqlDbType.DateTime),
					new SqlParameter("@IsRecivedMsg", SqlDbType.Bit,1),
					new SqlParameter("@ReciveMsgStatus", SqlDbType.NVarChar,50),
					new SqlParameter("@ReturnedID", SqlDbType.NVarChar,50)};
			parameters[0].Value = model.PhoneNumber;
			parameters[1].Value = model.Msg;
			parameters[2].Value = model.MsgTime;
			parameters[3].Value = model.IsRecivedMsg;
			parameters[4].Value = model.ReciveMsgStatus;
			parameters[5].Value = model.ReturnedID;

			object obj = DbHelperSQL.GetSingle(strSql.ToString(),parameters);
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
		/// 更新一条数据
		/// </summary>
		public bool Update(Maticsoft.Model.MsgData model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update MsgData set ");
			strSql.Append("PhoneNumber=@PhoneNumber,");
			strSql.Append("Msg=@Msg,");
			strSql.Append("MsgTime=@MsgTime,");
			strSql.Append("IsRecivedMsg=@IsRecivedMsg,");
			strSql.Append("ReciveMsgStatus=@ReciveMsgStatus,");
			strSql.Append("ReturnedID=@ReturnedID");
			strSql.Append(" where MsgID=@MsgID");
			SqlParameter[] parameters = {
					new SqlParameter("@PhoneNumber", SqlDbType.NVarChar,50),
					new SqlParameter("@Msg", SqlDbType.NVarChar,200),
					new SqlParameter("@MsgTime", SqlDbType.DateTime),
					new SqlParameter("@IsRecivedMsg", SqlDbType.Bit,1),
					new SqlParameter("@ReciveMsgStatus", SqlDbType.NVarChar,50),
					new SqlParameter("@ReturnedID", SqlDbType.NVarChar,50),
					new SqlParameter("@MsgID", SqlDbType.Int,4)};
			parameters[0].Value = model.PhoneNumber;
			parameters[1].Value = model.Msg;
			parameters[2].Value = model.MsgTime;
			parameters[3].Value = model.IsRecivedMsg;
			parameters[4].Value = model.ReciveMsgStatus;
			parameters[5].Value = model.ReturnedID;
			parameters[6].Value = model.MsgID;

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
		public bool Delete(int MsgID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from MsgData ");
			strSql.Append(" where MsgID=@MsgID");
			SqlParameter[] parameters = {
					new SqlParameter("@MsgID", SqlDbType.Int,4)
			};
			parameters[0].Value = MsgID;

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
		public bool DeleteList(string MsgIDlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from MsgData ");
			strSql.Append(" where MsgID in ("+MsgIDlist + ")  ");
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
		public Maticsoft.Model.MsgData GetModel(int MsgID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 MsgID,PhoneNumber,Msg,MsgTime,IsRecivedMsg,ReciveMsgStatus,ReturnedID from MsgData ");
			strSql.Append(" where MsgID=@MsgID");
			SqlParameter[] parameters = {
					new SqlParameter("@MsgID", SqlDbType.Int,4)
			};
			parameters[0].Value = MsgID;

			Maticsoft.Model.MsgData model=new Maticsoft.Model.MsgData();
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
		public Maticsoft.Model.MsgData DataRowToModel(DataRow row)
		{
			Maticsoft.Model.MsgData model=new Maticsoft.Model.MsgData();
			if (row != null)
			{
				if(row["MsgID"]!=null && row["MsgID"].ToString()!="")
				{
					model.MsgID=int.Parse(row["MsgID"].ToString());
				}
				if(row["PhoneNumber"]!=null)
				{
					model.PhoneNumber=row["PhoneNumber"].ToString();
				}
				if(row["Msg"]!=null)
				{
					model.Msg=row["Msg"].ToString();
				}
				if(row["MsgTime"]!=null && row["MsgTime"].ToString()!="")
				{
					model.MsgTime=DateTime.Parse(row["MsgTime"].ToString());
				}
				if(row["IsRecivedMsg"]!=null && row["IsRecivedMsg"].ToString()!="")
				{
					if((row["IsRecivedMsg"].ToString()=="1")||(row["IsRecivedMsg"].ToString().ToLower()=="true"))
					{
						model.IsRecivedMsg=true;
					}
					else
					{
						model.IsRecivedMsg=false;
					}
				}
				if(row["ReciveMsgStatus"]!=null)
				{
					model.ReciveMsgStatus=row["ReciveMsgStatus"].ToString();
				}
				if(row["ReturnedID"]!=null)
				{
					model.ReturnedID=row["ReturnedID"].ToString();
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
			strSql.Append("select MsgID,PhoneNumber,Msg,MsgTime,IsRecivedMsg,ReciveMsgStatus,ReturnedID ");
			strSql.Append(" FROM MsgData ");
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
			strSql.Append(" MsgID,PhoneNumber,Msg,MsgTime,IsRecivedMsg,ReciveMsgStatus,ReturnedID ");
			strSql.Append(" FROM MsgData ");
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
			strSql.Append("select count(1) FROM MsgData ");
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
				strSql.Append("order by T.MsgID desc");
			}
			strSql.Append(")AS Row, T.*  from MsgData T ");
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
			parameters[0].Value = "MsgData";
			parameters[1].Value = "MsgID";
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

