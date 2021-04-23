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
    public class BookletController : Controller
    {

        private readonly ApplicationDbContext _context;

        public BookletController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(int? id)
        {
            var list = _context.Booklets.Include(p => p.Profile).ToList();
            if (id != null)
            {
                list = _context.Booklets.Include(p => p.Profile).Where(p => p.Profile.ProfileID == (int)id).ToList();
            }
            var bookletView = new BookletViewModel();
            bookletView.Booklets = list;
            return View(bookletView);

        }


        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Create(Booklet record)
        {

            var selectedCategory = _context.Profiles.Where(p => p.ProfileID == record.ProfileID).SingleOrDefault();

            var booklet = new Booklet();

            booklet.PetName = record.PetName;
            booklet.Breed = record.Breed;
            booklet.Birthday = record.Birthday;
            booklet.Sex = record.Sex;
            booklet.Neutered = record.Neutered;
            booklet.FoodSensitivities = record.FoodSensitivities;
            booklet.Description = record.Description;
            booklet.Meds = record.Meds;
            booklet.DateAdded = DateTime.Now;
            booklet.ProfileID = record.ProfileID;

            _context.Booklets.Add(booklet);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }


        public IActionResult Edit(int? id)
        {
            if (id == null) //checks if value is NOT present then direct to Index Action
            {
                return RedirectToAction("Index");
            }

            var booklet = _context.Booklets.Where(p => p.BookletID == id).SingleOrDefault(); //supplier that gets the existing record from the suppliers table
            if (id == null) //checks if value is NOT present then direct to Index Action
            {
                return RedirectToAction("Index");
            }

            return View(booklet);
        }

        [HttpPost]
        public IActionResult Edit(int? id, Booklet record) //using a nullable integer id and the Product object as parameters
        {


            var booklet = _context.Booklets.Where(p => p.BookletID == id).SingleOrDefault(); 
            
            var selectedCategory = _context.Profiles.Where(p => p.ProfileID == record.ProfileID).SingleOrDefault();

            booklet.PetName = record.PetName;
            booklet.Breed = record.Breed;
            booklet.Birthday = record.Birthday;
            booklet.Sex = record.Sex;
            booklet.Neutered = record.Neutered;
            booklet.FoodSensitivities = record.FoodSensitivities;
            booklet.Description = record.Description;
            booklet.Meds = record.Meds;
            booklet.DateModified = DateTime.Now;
            booklet.ProfileID = record.ProfileID;

            _context.Booklets.Update(booklet); //update the existing values
            _context.SaveChanges(); //save the record from the database

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int? id)
        {
            if (id == null) //check if a valid value is not present, if condition is valid the view will redirect to index
            {
                return RedirectToAction("Index");
            }

            var booklet = _context.Booklets.Where(p => p.BookletID == id).SingleOrDefault(); //get the existing record from the items table based on the parameter id value.
            if (id == null) // product record is not present, redirect to Index
            {
                return RedirectToAction("Index");
            }

            _context.Booklets.Remove(booklet); //remove the existing record from the database table
            _context.SaveChanges();

            return RedirectToAction("Index"); // return view back to the index action
        }
    }
}