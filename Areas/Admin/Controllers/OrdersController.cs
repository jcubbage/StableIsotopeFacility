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

        public async Task<IActionResult> Index()
        {
            var model = await _dbContext.Orders
                .Include(o => o.Contact)
                .Include(o => o.Analyses)
                .ToListAsync();
            return View(model);
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

        public async Task<IActionResult> Details(int id)
        {
             var model = await CreateOrdersViewModel.EditViewModel(_dbContext, id);
            if(model.Order == null)
            {
                ErrorMessage = "Order not found!";
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

    }
}
