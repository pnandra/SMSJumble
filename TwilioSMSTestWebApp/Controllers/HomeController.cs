using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;



namespace TwilioSMSTestWebApp.Controllers
{
    public class HomeController : Controller
    {
        private Models.SMSDBEntities _db = new Models.SMSDBEntities();


        public ActionResult Index()
        {
            return View();            
        }

        public ActionResult About()
        {
           return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult SMSRecord()
        {
            return View(_db.SMSRecords.ToList());
        }

    }
}