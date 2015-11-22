using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace WCFModel
{
    [DataContract]
    public class CalculatorError
    {
        public CalculatorError(int code, string message)
        {
            ErrorCode = code;
            ErrorMessage = message;
        }
        [DataMember]
        public int ErrorCode { get; set; }
        [DataMember]
        public string ErrorMessage { get; set; }
    }
}
