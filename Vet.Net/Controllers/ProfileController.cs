using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;
using Vet.Net.Data;
using Vet.Net.Models;

namespace Vet.Net.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProfileController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var list = _context.Users.Where(u => u.UserType == UserTypes.PetOwner).ToList();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = _context.Users.Where(u => u.Id == userId).SingleOrDefault();
            if (user.UserType != UserTypes.Admin)
            {
                return RedirectToAction("Index", "Home");
            }
            var bookletView = new BookletViewModel();
            bookletView.Profiles = list;
            return View(bookletView);
        }

        public IActionResult Create()
        {

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = _context.Users.Where(u => u.Id == userId).SingleOrDefault();
            if (user.UserType != UserTypes.Admin)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }


        [HttpPost]
        public IActionResult Create(ApplicationUser record)
        {
            var profile = new ApplicationUser();
            var passwordHasher = new PasswordHasher<ApplicationUser>();
            profile.FirstName = record.FirstName;
            profile.LastName = record.LastName;
            profile.Email = record.Email;
            profile.PasswordHash = passwordHasher.HashPassword(profile, "temppassword");
            profile.Phone = record.Phone;
            profile.Address = record.Address;
            profile.DateAdded = DateTime.Now;
            profile.UserType = UserTypes.PetOwner;

            _context.Users.Add(profile);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Edit(string id)
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

            var profile = _context.Users.Where(p => p.Id == id).SingleOrDefault(); //supplier that gets the existing record from the suppliers table
            if (id == null) //checks if value is NOT present then direct to Index Action
            {
                return RedirectToAction("Index");
            }

            return View(profile);
        }

        [HttpPost]
        public IActionResult Edit(string id, ApplicationUser record) //using a nullable integer id and the Product object as parameters
        {

            var profile = _context.Users.Where(p => p.Id == id).SingleOrDefault();  //get the existing record from the items table based on the parameter id value.
            profile.FirstName = record.FirstName;
            profile.LastName = record.LastName;
            profile.Email = record.Email;
            profile.Phone = record.Phone;
            profile.Address = record.Address;
            profile.DateModified = DateTime.Now;

            _context.Users.Update(profile); //update the existing values
            _context.SaveChanges(); //save the record from the database

            return RedirectToAction("Index");
        }

        public IActionResult Delete(string id)
        {
            if (id == null) //check if a valid value is not present, if condition is valid the view will redirect to index
            {
                return RedirectToAction("Index");
            }

            var profile = _context.Users.Where(p => p.Id == id).SingleOrDefault(); //get the existing record from the items table based on the parameter id value.
            if (id == null) // product record is not present, redirect to Index
            {
                return RedirectToAction("Index");
            }

            _context.Users.Remove(profile); //remove the existing record from the database table
            _context.SaveChanges();

            return RedirectToAction("Index"); // return view back to the index action
        }


    }
}