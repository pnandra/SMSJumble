using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EX = TwilioSMSTestWebApp.ExceptionHandler;
using System.Configuration;


namespace TwilioSMSTestWebApp.UtilityClasses
{
    public static class Utility
    {
        public static string StringReversal(string inString)
        {
            try
            {
                string origMsg =inString;
                string reversedMsg = string.Empty;
                string tempStr = string.Empty;

                foreach (var c in origMsg)
                {
                    if (c != ' ')
                    {
                        tempStr += c;
                    }
                    else
                    {
                        reversedMsg = string.Format("{0} {1}", tempStr, reversedMsg);
                        tempStr = string.Empty;
                    }
                }
                reversedMsg = string.Format("{0} {1}", tempStr, reversedMsg);

                return reversedMsg;
            }
            catch (Exception ex)
            {
                EX.ExceptionHandler.HandleException(ex);
                return "String could not be reversed.  Exception occurred.";
            }
        }

        public static string GetAccountSid()
        {
            try
            {
                return ConfigurationManager.AppSettings.Get("TWAccountSid");
            }
            catch (Exception ex)
            {
                EX.ExceptionHandler.HandleException(ex);
                return string.Empty;
            }
        }

        public static string GetAuthToken()
        {
            try
            {
                return ConfigurationManager.AppSettings.Get("TWAuthToken");
            }
            catch (Exception ex)
            {
                EX.ExceptionHandler.HandleException(ex);
                return string.Empty;
            }
        }

    }
}