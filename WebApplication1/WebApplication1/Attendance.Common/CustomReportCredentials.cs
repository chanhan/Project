using Microsoft.Reporting.WebForms;
using System;
using System.Configuration;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Principal;
using GDSBG.MiABU.Attendance.Common;

namespace GDSBG.MiABU.Attendance.Common
{
    [Serializable]
    public class CustomReportCredentials : IReportServerCredentials
    {
        public bool GetFormsCredentials(out Cookie authCookie, out string userName, out string password, out string authority)
        {
            authCookie = null;
            userName = null;
            password = null;
            authority = null;
            return false;
        }

        public WindowsIdentity ImpersonationUser
        {
            get
            {
                return null;
            }
        }

        public ICredentials NetworkCredentials
        {
            get
            {
                string userName = EncryptClass.Instance.Decrypt(ConfigurationManager.AppSettings["ReportUser"]);
                if (string.IsNullOrEmpty(userName))
                {
                    throw new Exception("Missing user name from web.config file");
                }
                string password = EncryptClass.Instance.Decrypt(ConfigurationManager.AppSettings["ReportPassword"]);
                if (string.IsNullOrEmpty(password))
                {
                    throw new Exception("Missing password from web.config file");
                }
                string domain = ConfigurationManager.AppSettings["ReportServerDomain"];

                return new NetworkCredential(userName, password, domain);
            }
        }
    }

}
