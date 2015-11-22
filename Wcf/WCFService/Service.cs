using IWCFService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using WCFModel;
using DataAccess;
using System.Data;
namespace WCFService
{
    public class Service:IService
    {
        public User GetUser(string nickName)
        {
           DataTable dt=new DBSqlHelper().ExecuteDataTable("select * from [user] where nickname='"+nickName+"'");
           return new User { 
                ID=Convert.ToInt32(dt.Rows[0]["ID"]),
                 Name=dt.Rows[0]["Name"].ToString(),
                 NickName=dt.Rows[0]["NickName"].ToString()
           };
        }

        public int AddUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}
