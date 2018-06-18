using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EX = TwilioSMSTestWebApp.ExceptionHandler;


namespace TwilioSMSTestWebApp.DbClasses
{
    public class DbIOClass
    {
        private Models.SMSDBEntities _db = new Models.SMSDBEntities();

        public void InsertSMSRecord(string message, string response, DateTime timeStamp)
        {
            try
            {
                Models.SMSRecord record = new Models.SMSRecord
                {
                    ID = 0, // this will be ignored
                    vchMessageIn = message,
                    vchMessageOut = response,
                    dttmTimeStamp = timeStamp
                };

                _db.SMSRecords.Add(record);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                EX.ExceptionHandler.HandleException(ex);
            }
        }
    }
}