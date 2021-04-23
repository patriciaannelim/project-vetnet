using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;
using Vet.Net.Data;
using Vet.Net.Models;

namespace Vet.Net.Controllers
{
    [Authorize]
    public class ReservationFormController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReservationFormController(ApplicationDbContext context)
        {
            _context = context;
        }

  
        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = _context.Users.Where(u => u.Id == userId).SingleOrDefault();
            if (user.UserType != UserTypes.Admin)
            {
                return RedirectToAction("Index", "Home");
            }

            var list = _context.Reservations.Include(p => p.Medications).ToList();
            return View(list);
        }

        public IActionResult Create(/*bool success = false*/)
        {
            //var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //var user = _context.Users.Where(u => u.Id == userId).SingleOrDefault();
            //if (user.UserType != UserTypes.PetOwner)
            //{
            //    return RedirectToAction("Index", "Home");
            //}
            //ViewBag.success = success;
            return View();
        }

        //[Authorize]
        //public IActionResult AdminCreate()
        //{

        //    return View();
        //}

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

                reservationForm.Name = record.Name;
                reservationForm.Email = record.Email;
                reservationForm.Phone = record.Phone;
                reservationForm.Address = record.Address;
                reservationForm.Type = record.Type;
                reservationForm.CustomerType = record.CustomerType;
                reservationForm.Concerns = record.Concerns;

                reservationForm.DateAdded = DateTime.Now;

            }
         
            _context.Reservations.Add(reservationForm);
            _context.SaveChanges();

           
                using (MailMessage mail = new MailMessage("entprog.vet.net@gmail.com", record.Email))
                {
                    mail.Subject = "Vet.Net Reservation Details";/*record.Subject;*/

                    string message = "Hello, " + record.Name + "!<br/><br/>" +
                    "Your reservation is currently under review. Here is what we got from you: <br/><br/>" +
                    "Date of Appointment: <strong>" + record.DatePicker + "</strong><br/>" +
                    //"Type of Medication: <strong>" + record.Medications + "</strong><br/><br/>" +
                    "Pet Name: <strong>" + record.PetName + "</strong><br/>" +
                    "Pet Owner: <strong>" + record.Name + "</strong><br/>" +
                    "Contact Number: <strong>" + record.Phone + "</strong><br/>" +
                    "Concerns: <strong>" + record.Concerns + "</strong><br/><br/>" +
                    "Please wait for our reply to know if your booking is confirmed. Thank you! <br/><br/>" +
                    "Send us an email through the contact us page of our website if you have any inquries!";
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
                        ViewBag.Message = "This will be sent to your email! Please wait for our booking confirmation.";
                    }
                }
            
            return View();
            //return RedirectToAction("Create", new { success = true });
            //return View("Create",new {success = true });
        }

        //[Authorize]
        //[HttpPost]
        //public IActionResult AdminCreate(ReservationForm record)
        //{
        //    var reservationForm = new ReservationForm();
        //    {
        //        reservationForm.PetName = record.PetName;
        //        reservationForm.Animal = record.Animal;
        //        reservationForm.Medications = record.Medications;
        //        reservationForm.MedicationID = record.MedicationID;
        //        reservationForm.DatePicker = record.DatePicker;

        //        reservationForm.Name = record.Name;
        //        reservationForm.Email = record.Email;
        //        reservationForm.Phone = record.Phone;
        //        reservationForm.Address = record.Address;
        //        reservationForm.Type = record.Type;
        //        reservationForm.CustomerType = record.CustomerType;
        //        reservationForm.Concerns = record.Concerns;

        //        reservationForm.DateAdded = DateTime.Now;

        //    }

        //    _context.Reservations.Add(reservationForm);
        //    _context.SaveChanges();

        //    return RedirectToAction("Index");

        //}


        //[Authorize]
        public IActionResult Edit(int? id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = _context.Users.Where(u => u.Id == userId).SingleOrDefault();
            if (user.UserType != UserTypes.Admin)
            {
                return RedirectToAction("Index", "Home");
            }

            if (id == null) //checks if value is NOT present then direct to Index Action
            {
                return RedirectToAction("Index");
            }

            var reservationForm = _context.Reservations.Where(r => r.ReservationID == id).SingleOrDefault(); //supplier that gets the existing record from the suppliers table
            if (id == null) //checks if value is NOT present then direct to Index Action
            {
                return RedirectToAction("Index");
            }

            return View(reservationForm);
        }

        //[Authorize]
        [HttpPost]
        public IActionResult Edit(int? id, ReservationForm record) //using a nullable integer id and the Product object as parameters
        {

            var reservationForm = _context.Reservations.Where(r => r.ReservationID == id).SingleOrDefault();
            {
                
                reservationForm.PetName = record.PetName;
                reservationForm.Animal = record.Animal;
                reservationForm.Medications = record.Medications;
                reservationForm.MedicationID = record.MedicationID;
                reservationForm.DatePicker = record.DatePicker;

                reservationForm.Name = record.Name;
                reservationForm.Email = record.Email;
                reservationForm.Phone = record.Phone;
                reservationForm.Address = record.Address;
                reservationForm.Type = record.Type;
                reservationForm.CustomerType = record.CustomerType;
                reservationForm.Concerns = record.Concerns;

                reservationForm.DateModified = DateTime.Now;
            }

            _context.Reservations.Update(reservationForm); //update the existing values
            _context.SaveChanges(); //save the record from the database

            return RedirectToAction("Index");
        }

        //[Authorize]

        public IActionResult Delete(int? id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = _context.Users.Where(u => u.Id == userId).SingleOrDefault();
            if (user.UserType != UserTypes.Admin)
            {
                return RedirectToAction("Index", "Home");
            }

            if (id == null) //check if a valid value is not present, if condition is valid the view will redirect to index
            {
                return RedirectToAction("Index");
            }

            var reservationForm = _context.Reservations.Where(r => r.ReservationID == id).SingleOrDefault();//get the existing record from the items table based on the parameter id value.
            if (id == null) // product record is not present, redirect to Index
            {
                return RedirectToAction("Index");
            }

            _context.Reservations.Remove(reservationForm); //remove the existing record from the database table
            _context.SaveChanges();

            return RedirectToAction("Index"); // return view back to the index action
        }

    }
}