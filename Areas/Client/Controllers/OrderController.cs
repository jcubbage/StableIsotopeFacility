using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SIFCore.Models;

namespace SIFCore.Controllers
{    
    public class OrderController : ClientController
    {
        public IActionResult Index()
            {
                return View();
            }
    }

}