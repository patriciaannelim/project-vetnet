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
    public class UserProfileController : Controller
    {

        private readonly ApplicationDbContext _context;

        public UserProfileController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var list = _context.UserProfiles.Include(p => p.PetBooklet).ToList();
            return View(list);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(UserProfile record)
        {
            var userprofile = new UserProfile();

            userprofile.Name = record.Name;
            userprofile.Email = record.Email;
            userprofile.Phone = record.Phone;
            userprofile.Address = record.Address;
            userprofile.PetBooklet = record.PetBooklet;
            userprofile.PetID = record.PetID;
           

            //if (ImagePath != null)
            //{
            //    if (ImagePath.Length > 0)
            //    {
            //        var filePath = Path.Combine(Directory.GetCurrentDirectory(),
            //            "wwwroot/img/products", ImagePath.FileName);

            //        using (var stream = new FileStream(filePath, FileMode.Create))
            //        {
            //            ImagePath.CopyTo(stream);
            //        }
            //        product.ImagePath = ImagePath.FileName;
            //    }
            //}

            _context.UserProfiles.Add(userprofile);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }


        public IActionResult Edit(int? id)
        {
            if (id == null) //checks if value is NOT present then direct to Index Action
            {
                return RedirectToAction("Index");
            }

            var userprofile = _context.UserProfiles.Where(p => p.UserID == id).SingleOrDefault(); //supplier that gets the existing record from the suppliers table
            if (id == null) //checks if value is NOT present then direct to Index Action
            {
                return RedirectToAction("Index");
            }

            return View(userprofile);
        }


        [HttpPost]
        public IActionResult Edit(int? id, UserProfile record) //using a nullable integer id and the Product object as parameters
        {
            var userprofile = _context.UserProfiles.Where(p => p.UserID == id).SingleOrDefault(); //get the existing record from the items table based on the parameter id value.

            userprofile.Name = record.Name;
            userprofile.Email = record.Email;
            userprofile.Phone = record.Phone;
            userprofile.Address = record.Address;
            userprofile.PetBooklet = record.PetBooklet;
            userprofile.PetID = record.PetID;

            _context.UserProfiles.Update(userprofile); //update the existing values
            _context.SaveChanges(); //save the record from the database

            return RedirectToAction("Index");
        }
    }
}