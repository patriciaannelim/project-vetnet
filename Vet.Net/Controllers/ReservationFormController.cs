using System;
using System.Collections.Generic;
using System.Linq;
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
        //public IActionResult Index()
        //{
        //    var list = _context.Reservations.Include(p => p.Medications).ToList();
        //    return View(list);
        //}

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(ReservationForm record)
        {
            var reservationForm = new ReservationForm();

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


         
            _context.Reservations.Add(reservationForm);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

    }
}