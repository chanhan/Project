using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Maticsoft.DAL
{
  public  class ReciveMsgDAL
    {
        public string ReciveMsg(string phonenumber, string codes)
        {
            return ""; 
        }

        public System.Data.DataTable ReciveMsg(string phonenumber, string[] code)
        {
            return new Maticsoft.DAL.CodeData().GetList("").Tables[0];
        }
    }
}
