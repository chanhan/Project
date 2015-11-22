using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Runtime.Serialization;
namespace WCFData
{
    [DataContract]
    public class UserInfo
    {
        [DataMember]
        public int UserID { get; set; }
        [DataMember]
        public string UserName { get; set; }
        public override string ToString()
        {
            return this.UserID +"------" +this.UserName;
        }
    }
}
