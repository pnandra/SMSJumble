using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Configuration;

namespace TwilioSMSTestWebApp.ExceptionHandler
{
    public class ExceptionHandler
    {
        public static void HandleException(Exception ex)
        {
            string path = Convert.ToString(ConfigurationManager.AppSettings["ExceptionPath"]);
            // notifications will be written to Notification Sub-directory
            bool exists = Directory.Exists(path);
            if (!exists)
                Directory.CreateDirectory(path);
            string fileName = DateTime.Now.ToString("yyyyMMdd-HHmmss");

            File.WriteAllText(path + fileName + ".txt", ex.ToString());
        }
    }
}