using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vet.Net.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Vet.Net.Data;


namespace Vet.Net.Controllers
{
    public class ReserveController : Controller
    {
        private readonly ReserveDbContext _context;
        public ReserveController(ReserveDbContext context)
        {
            _context = context;
        }
        public IActionResult Display()
        {
            var list = _context.Names.ToList();
            return View(list);
        }
        public IActionResult Create()
        {
            return View();
        }
        public IActionResult Create(ReserveUser record)
        {
            var name = new ReserveUser();
            name.petid = record.petid;
            name.petname = record.petname;
            name.datepicker = record.datepicker;
            name.type = record.type;
            name.name = record.name;
            name.email = record.email;
            name.phone = record.phone;
            name.city = record.city;
            name.textarea = record.textarea;

            _context.Names.Add(name);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }



    }
}
