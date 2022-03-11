using Company_List.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Company_List.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

 

        public IActionResult Index()
        {
            return View();
        }

    
      
       
    }
}
