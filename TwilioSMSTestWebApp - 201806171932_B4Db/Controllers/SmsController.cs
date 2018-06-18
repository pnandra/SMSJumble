// Code sample for ASP.NET MVC on .NET Framework 4.6.1+
// In Package Manager, run:
// Install-Package Twilio.AspNet.Mvc -DependencyVersion HighestMinor

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SD = System.Diagnostics;
// Twilio libraries.
using Twilio.AspNet.Mvc;
using Twilio.TwiML;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;
using EX = TwilioSMSTestWebApp.ExceptionHandler;
using System.Configuration;

namespace TwilioSMSTestWebApp.Controllers
{
    public class SmsController : TwilioController
    {
        //// GET: Sms
        //public ActionResult Index()
        //{
        //    return View();
        //}

        [HttpPost]
        public TwiMLResult Index()
        {
            try
            {
                // capture message
                // Your Account Sid and Token at twilio.com/console
                string accountSid = GetAccountSid(); // this is my accountSID
                string authToken = GetAuthToken(); // this si my authToken

                //initialize account
                TwilioClient.Init(accountSid, authToken);

                // get a list (collection) of messages 
                var messages = MessageResource.Read(pathAccountSid: accountSid);

                //based on understanding from documentation first message is the one that sent from phone and recevied on API end
                MessageResource mr = messages.FirstOrDefault<MessageResource>();

                var message = MessageResource.Fetch(pathAccountSid: accountSid, pathSid: mr.Sid); //message.sid is message unique identifier

                // reverse the message string.
                string rvMsg = ReverseMessageBody(message);

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

    //    private void WriteMessageToConsole(MessageResource message)
    //    {
    //        string msg = string.Empty;
    //        msg += "Status: " + message.Status + "\n";
    //        msg += "DateSent: " + message.DateSent + "\n";
    //        msg += "DateUpdated: " + message.DateUpdated + "\n";
    //        msg += "DateCreated: " + message.DateCreated + "\n";
    //        msg += "Body: " + message.Body + "\n";
    //        msg += "ApiVersion: " + message.ApiVersion + "\n";
    //        msg += "AccountSid: " + message.AccountSid + "\n";
    //        msg += "ErrorMessage: " + message.ErrorMessage + "\n";
    //        msg += "From: " + message.From + "\n";
    //        msg += "MessagingServiceSid: " + message.MessagingServiceSid + "\n";
    //        msg += "NumMedia: " + message.NumMedia + "\n";
    //        msg += "NumSegments: " + message.NumSegments + "\n";
    //        msg += "Price: " + message.Price + "\n";
    //        msg += "PriceUnit: " + message.PriceUnit + "\n";
    //        msg += "Sid: " + message.Sid + "\n";
    //        msg += "ErrorCode: " + message.ErrorCode + "\n";
    //        msg += "SubresourceUris: " + message.SubresourceUris + "\n";
    //        msg += "To: " + message.To + "\n";
    //        msg += "Uri: " + message.Uri + "\n";
    //        msg += "Direction: " + message.Direction + "\n";

    //        System.Diagnostics.Debug.WriteLine(msg);
    //}

        private string ReverseMessageBody (MessageResource message)
        {
            string origMsg = message.Body;
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

        private string GetAccountSid()
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

        private string GetAuthToken()
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