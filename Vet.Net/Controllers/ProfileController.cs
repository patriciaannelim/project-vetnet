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

    public class ProfileController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProfileController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(int? id)
        {
            var list = _context.Profiles.ToList();
            if (id != null)
            {
                list = _context.Profiles.Where(p => p.ProfileID == (int)id).ToList();
                //list = _context.Booklets.Include(p => p.Profile).Where(p => p.Profile.ProfileID == (int)id).ToList();
            }
            var bookletView = new BookletViewModel();
            bookletView.Profiles = list;
            return View(bookletView);
        }

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Create(Profile record)
        {
            var profile = new Profile();

            profile.Name = record.Name;
            profile.Email = record.Email;
            profile.Phone = record.Phone;
            profile.Address = record.Address;
            profile.DateAdded = DateTime.Now;

            _context.Profiles.Add(profile);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int? id)
        {
            if (id == null) //checks if value is NOT present then direct to Index Action
            {
                return RedirectToAction("Index");
            }

            var profile = _context.Profiles.Where(p => p.ProfileID == id).SingleOrDefault(); //supplier that gets the existing record from the suppliers table
            if (id == null) //checks if value is NOT present then direct to Index Action
            {
                return RedirectToAction("Index");
            }

            return View(profile);
        }

        [HttpPost]
        public IActionResult Edit(int? id, Profile record) //using a nullable integer id and the Product object as parameters
        {

            var profile = _context.Profiles.Where(p => p.ProfileID == id).SingleOrDefault();  //get the existing record from the items table based on the parameter id value.
            profile.Name = record.Name;
            profile.Email = record.Email;
            profile.Phone = record.Phone;
            profile.Address = record.Address;
            profile.DateModified = DateTime.Now;

            _context.Profiles.Update(profile); //update the existing values
            _context.SaveChanges(); //save the record from the database

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int? id)
        {
            if (id == null) //check if a valid value is not present, if condition is valid the view will redirect to index
            {
                return RedirectToAction("Index");
            }

            var profile = _context.Profiles.Where(p => p.ProfileID == id).SingleOrDefault(); //get the existing record from the items table based on the parameter id value.
            if (id == null) // product record is not present, redirect to Index
            {
                return RedirectToAction("Index");
            }

            _context.Profiles.Remove(profile); //remove the existing record from the database table
            _context.SaveChanges();

            return RedirectToAction("Index"); // return view back to the index action
        }


    }
}