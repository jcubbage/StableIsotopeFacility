using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SIFCore.Models;

namespace SIFCore.Controllers
{
     [Area("Client")]
    public class OrderController : SuperController
    {
        public IActionResult Index()
            {
                return View();
            }
    }

}