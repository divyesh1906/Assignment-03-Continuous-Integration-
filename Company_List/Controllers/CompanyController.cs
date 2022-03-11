using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Company_List.Models;

namespace EmployeesApplication_Week5.Controllers
{
    public class CompanyController : Controller
    {
        private readonly TransactionContext _context;

        // Prepare controller for database context communication
        public CompanyController(TransactionContext context)
        {
            _context = context;
        }

        // GET: Company/Index
        public IActionResult Index()
        {
            return View(_context.Company.ToList());
        }
        
        // get company details using id 
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var company = _context.Company.FirstOrDefault(m => m.CompanyID == id);
            
            if (company == null)
            {
                return NotFound();
            }

            return View(company);
        }

        // GET: Company/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // Create the data for Company 
        [HttpPost]
        public IActionResult Create(Company company)
        {
            if (ModelState.IsValid)
            {
                _context.Add(company);
                _context.SaveChanges();

                //Temp data is used in header for infomation is added.
                TempData["action"] = company.CompanyName + " has been added.";
                TempData["isSuccessful"] = "Success";

                return RedirectToAction(nameof(Index));
            }
            
           
            TempData["action"] = company.CompanyName + " was not added.";
            TempData["isSuccessful"] = "Failed";

            return View(company);
        }

        // GET: Company/Edit
        //Edit method is done by id which allows to do edit on click event of Edit
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Find the value of Edit using Id
            var company = _context.Company.Find(id);

            if (company == null)
            {
                return NotFound();
            }
            return View(company);
        }

        //If modelstate is valid then it allow to do Update
        [HttpPost]
        public IActionResult Edit(Company company)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(company);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompanyExists(company.CompanyID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

               //Called Event is it Succesful or Not
                TempData["action"] = company.CompanyName + " was updated.";
                TempData["isSuccessful"] = "Success";

                return RedirectToAction(nameof(Index));
            }

            
            TempData["action"] = company.CompanyName + " was not updated.";
            TempData["isSuccessful"] = "Failed";

            return View(company);
        }

        // GET: Company/Delete
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

          //Attemo to match the data using Company id
            var company = _context.Company.FirstOrDefault(m => m.CompanyID == id);

            if (company == null)
            {
                return NotFound();
            }
            
            return View(company);
        }


        //Remove data from the database 
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(Company company)
        {
           
            TempData["action"] = company.CompanyName + " was deleted.";
            TempData["isSuccessful"] = "Success";

            _context.Company.Remove(company);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        // Local method to assist with the process of identifying if the company entry exists in the 
        // database based on whether or not the provided CompanyID matches a record in the Company
        // table.
        private bool CompanyExists(int id)
        {
            return _context.Company.Any(e => e.CompanyID == id);
        }
    }
}
