using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Company_List.Models;

namespace EmployeesApplication_Week5.Controllers
{
    public class TransactionController : Controller
    {
        private readonly TransactionContext _context;

        //Controller for database context
        public TransactionController(TransactionContext context)
        {
            _context = context;
        }


        // GET: Transaction/Index/sortOrder
        //Sort the data of transaction in the Ascending and Desending Style Format
        public IActionResult Index(string sortOrder)
        {
           ///Set the cookies data of user
            CookieOptions cookieOptions = new CookieOptions { Expires = DateTime.Now.AddSeconds(20) };
            HttpContext.Response.Cookies.Append("newUser", "Divyesh", cookieOptions);

         
            ViewBag.SortOrder = sortOrder == "SortByAsc" || sortOrder == null ? 
                "SortByDesc" : "SortByAsc";

            var transactions = _context.Transactions
                .Include(i => i.TransactionType)
                .Include(i => i.Company)
                .ToList();

           //switch order the data 
            switch (sortOrder)
            {
                case "SortByAsc":
                    transactions = transactions.OrderBy(ob => ob.Company.CompanyName).ToList();
                    break;

                case "SortByDesc":
                    transactions = transactions.OrderByDescending(obd => obd.Company.CompanyName).ToList();
                    break;

                default:
                    transactions = transactions.OrderBy(ob => ob.Company.CompanyName).ToList();
                    break;
            }

           
            return View(transactions);
        }


        // GET: Transaction/TransactionList
        //Diplay all the transactions which is added by the user
        //for that called the compnay and transaction database set in to variablr
        public IActionResult TransactionList(int id)
        {
            Company company = _context.Company.Where(w => w.CompanyID == id).FirstOrDefault();

            ViewBag.CompanyName = company.CompanyName;
            ViewBag.TickerSymbol = company.TickerSymbol;

            var transactionsList = _context.Transactions
                .Include(i => i.TransactionType)
                .Include(i => i.Company)
                .Where(w => w.CompanyID == id)
                .ToList();

            return View(transactionsList);
        }


        // GET: Transaction/Details  
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

          
            var transaction = _context.Transactions.FirstOrDefault(m => m.TransactionId == id);

            if (transaction == null)
            {
                return NotFound();
            }

            return View(transaction);
        }

        // Return to the Create Method 
        public IActionResult Create()
        {
            ViewBag.TransactionType = _context.TransactionTypes.ToList();
            ViewBag.Company = _context.Company.ToList();
            return View();
        }

       //Trabnsaction/Create
       //Add new data into  the transction database
        [HttpPost]
        public IActionResult Create(Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                _context.Add(transaction);
                _context.SaveChanges();
                var company = _context.Company.Find(transaction.CompanyID);

                TempData["action"] = company.CompanyName + " " +    " Data was added to the Transaction database.";
                TempData["isSuccessful"] = "Success";

                return RedirectToAction("Index");
            }

           //Tempdata which saws the data has store Successfully
            TempData["action"] = transaction.Company.CompanyName + " " + " was NOT added to the Transaction database.";
            TempData["isSuccessful"] = "Failed";

            return View(transaction);
        }

      
      /// <summary>
      /// 
      /// </summary>
      /// <param name="id"></param>
      /// <returns></returns>
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ViewBag.TransactionType = _context.TransactionTypes.ToList();
            ViewBag.Company = _context.Company.ToList();

        
            var transaction = _context.Transactions.Find(id);

            if (transaction == null)
            {
                return NotFound();
            }
            return View(transaction);
        }

      
        [HttpPost]
        // GET: Transaction/Edit
        //Edit method is done by id which allows to do edit on click event of Edit
        //Id find the value which will be depends on the modelstate if its true then its will do updte
        public IActionResult Edit(Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(transaction);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TransactionExists(transaction.TransactionId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

              //Tempdata which saws the result or transaction process is done
                TempData["action"] = transaction.Company.CompanyName + " " + transaction.Company.TickerSymbol + " was updated.";
                TempData["isSuccessful"] = "Success";

                return RedirectToAction(nameof(Index));
            }
            
            TempData["action"] = transaction.Company.CompanyName + " " + transaction.Company.TickerSymbol + " was NOT updated.";
            TempData["isSuccessful"] = "Failed";

            return View(transaction);
        }

       /// <summary>
       /// Find the result using Id and allow it to delete the data
       /// </summary>
       /// <param name="id"></param>
       /// <returns></returns>
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

          
            var transaction = _context.Transactions.FirstOrDefault(m => m.TransactionId == id);
            ViewBag.Company = _context.Company.ToList();

            if (transaction == null)
            {
                return NotFound();
            }

            return View(transaction);
        }

        /// <summary>
        /// deleteConfird method is used to delete the data by find thr the particular Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //Remove the data from the database and update the data
        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            var transaction = _context.Transactions.Find(id);
            var company = _context.Company.Find(transaction.CompanyID);

            _context.Transactions.Remove(transaction);
            _context.SaveChanges();

            TempData["action"] = company.CompanyName + " " + "  was deleted into the database.";
            TempData["isSuccessful"] = "Success";

            return RedirectToAction(nameof(Index));
        }

        
        private bool TransactionExists(int id)
        {
            return _context.Transactions.Any(e => e.TransactionId == id);
        }
    }
}
