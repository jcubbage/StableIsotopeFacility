using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIFCore.Models;

namespace SIFCore.Controllers.Admin
{
    public class OrdersController : AdminController
    {

         private readonly SIFContext _dbContext;

        public OrdersController(SIFContext dbContext)
        {            
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> StartNew()
        {
            var model = await _dbContext.Contacts.OrderBy(c => c.LastName).ThenBy(c => c.FirstName).ToListAsync();
            return View(model);
        }

        public async Task<IActionResult> Create(int id)
        {
            var model = await CreateOrdersViewModel.Create(_dbContext, 0, id);
            return View(model);
        }

    }
}
