using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Vet.Net.Models;

using System.Net;
using System.Net.Mail;

namespace Vet.Net.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult ReserveForm()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Contact(Contact record)
        {
            using (MailMessage mail = new MailMessage("entprog.vet.net@gmail.com", record.Email))
            {
                mail.Subject = record.Subject;

                string message = "Hello, " + record.SenderName + "!<br/><br/>" +
                "We have received you inquiry. Here are the details: <br/><br/>" +
                "Contact Number: <strong>" + record.ContactNo + "</strong><br/>" +
                "Message: <br/><strong>" + record.Message + "</strong><br/><br/>" +
                "Please wait for our reply. Thank you!";
                mail.Body = message;
                mail.IsBodyHtml = true;

                using (SmtpClient smtp = new SmtpClient())
                {
                    smtp.Host = "smtp.gmail.com";
                    smtp.EnableSsl = true;
                    NetworkCredential NetworkCred =
                        new NetworkCredential("entprog.vet.net@gmail.com", "entprogproject");
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = NetworkCred;
                    smtp.Port = 587;
                    smtp.Send(mail);
                    ViewBag.Message = "Your Inquiry Has Been Sent! We will get in touch with you shortly.";
                }
            }
            return View();
        }

    }
}
