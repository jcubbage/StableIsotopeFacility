using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIFCore.Models;

namespace SIFCore.Controllers
{    
    public class OrderController : ClientController
    {
        private readonly SIFContext _dbContext;

        public OrderController(SIFContext dbContext)
        {            
            _dbContext = dbContext;
        }

        public async Task<IActionResult> Index()
        {
            var orders = await _dbContext.Orders.Where(o => o.ContactId.ToString() == User.FindFirstValue("contactId"))
                .Include(o => o.OrderShippingAddress)
                .Include(o => o.OrderBillingAddress)
                .Include(o => o.Analyses)
                .ToListAsync();
            // if (showSubmitted == "No")
            // {
            //     orders = orders.Where(a => !a.Submitted);
            // }
            // if (showSubmitted == "Yes")
            // {
            //     orders = orders.Where(a => a.Submitted);
            // }
            // orders = orders.OrderBy(b => b.Id);
            // ViewBag.Query = showSubmitted;
            
            return View(orders);
        }

        public async Task<IActionResult> New()
        {
            var model = await CreateOrdersViewModel.Create(_dbContext, 0, User.FindFirstValue("contactId"));
            return View(model);
        }
    }

}