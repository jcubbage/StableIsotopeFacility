using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SIFCore.Models;

namespace SIFCore.Controllers
{
    public class TestController : Controller
    {

        private readonly SIFContext _dbContext;

        public TestController(SIFContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var test = _dbContext.ShippingAddresses.Count();
            return View(test);
        }

       
    }
}
