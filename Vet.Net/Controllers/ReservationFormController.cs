using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;
using Vet.Net.Data;
using Vet.Net.Models;

namespace Vet.Net.Controllers
{
    public class ReservationFormController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReservationFormController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var list = _context.Reservations.Include(p => p.Medications).ToList();
            return View(list);
        }

        public IActionResult Create(bool success = false)
        {
            ViewBag.success = success;
            return View();
        }

        [HttpPost]
        public IActionResult Create(ReservationForm record)
        {
            var reservationForm = new ReservationForm();
            {
                reservationForm.PetName = record.PetName;
                reservationForm.Animal = record.Animal;
                reservationForm.Medications = record.Medications;
                reservationForm.MedicationID = record.MedicationID;
                reservationForm.DatePicker = record.DatePicker;
                reservationForm.Type = record.Type;
                reservationForm.Name = record.Name;
                reservationForm.Email = record.Email;
                reservationForm.Phone = record.Phone;
                reservationForm.City = record.City;
                reservationForm.Concerns = record.Concerns;

            }
         
            _context.Reservations.Add(reservationForm);
            _context.SaveChanges();


            using (MailMessage mail = new MailMessage("entprog.vet.net@gmail.com", record.Email))
            {

                mail.Subject = "Booking [to edit]";/*record.Subject;*/

                string message = "Hello, " + record.Name + "!<br/><br/>" +
                "We have received you Reservation Form. Here are the details: <br/><br/>" +
                "Contact Number: <strong>" + record.Phone + "</strong><br/>" +
                "Pet Name: <strong>" + record.PetName + "</strong><br/>" +
                "Type of Pet: <strong>" + record.Animal + "</strong><br/>" +
                "Message: <br/><strong>" + record.Concerns + "</strong><br/><br/>" +
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
                }
            }
                    return RedirectToAction("Create", new { success = true });
            //return View("Create",new {success = true });
        }

    }
}