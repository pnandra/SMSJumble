// Code sample for ASP.NET MVC on .NET Framework 4.6.1+
// In Package Manager, run:
// Install-Package Twilio.AspNet.Mvc -DependencyVersion HighestMinor

using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
// Twilio libraries.
using Twilio.AspNet.Mvc;
using Twilio.TwiML;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using EX = TwilioSMSTestWebApp.ExceptionHandler;
using UT = TwilioSMSTestWebApp.UtilityClasses;
using DC = TwilioSMSTestWebApp.DbClasses;

namespace TwilioSMSTestWebApp.Controllers
{
    public class SmsController : TwilioController
    {
        

        [HttpPost]
        public TwiMLResult Index()
        {
            try
            {
                // capture message
                // Your Account Sid and Token at twilio.com/console
                string accountSid = UT.Utility.GetAccountSid(); // this is my accountSID
                string authToken = UT.Utility.GetAuthToken(); // this si my authToken

                //initialize account
                TwilioClient.Init(accountSid, authToken);

                // get a list (collection) of messages 
                var messages = MessageResource.Read(pathAccountSid: accountSid);

                //based on understanding from documentation first message is always the one that sent from phone and recevied on API end
                MessageResource mr = messages.FirstOrDefault<MessageResource>();

                var message = MessageResource.Fetch(pathAccountSid: accountSid, pathSid: mr.Sid); //message.sid is message unique identifier

                // reverse the message string.
                string rvMsg = UT.Utility.StringReversal(message.Body.ToString());

                //write to db.
                WriteMsgToDb(message.Body.ToString(), rvMsg);

                // response section.
                var messagingResponse = new MessagingResponse();
                messagingResponse.Message(rvMsg);
                return TwiML(messagingResponse);
            }
            catch (Exception ex)
            {
                EX.ExceptionHandler.HandleException(ex);
                return null;
            }
        }

        private void WriteMsgToDb(string message, string response)
        {
            try
            {
                DC.DbIOClass db = new DC.DbIOClass();
                db.InsertSMSRecord(message, response, DateTime.Now);
            }
            catch (Exception ex)
            {
                EX.ExceptionHandler.HandleException(ex);
            }
        }
    }
}