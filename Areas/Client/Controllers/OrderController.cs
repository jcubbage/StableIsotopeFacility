using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIFCore.Models;

namespace SIFCore.Controllers.Client
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
            var orders = await _dbContext.Orders.Where(o => o.ContactId == Int32.Parse(User.FindFirstValue("contactId")))
                .Include(o => o.OrderShippingAddress)
                .Include(o => o.OrderBillingAddress)
                .Include(o => o.Analyses)
                .ToListAsync();            
            return View(orders);
        }

        public async Task<IActionResult> New()
        {
            var model = await CreateOrdersViewModel.Create(_dbContext, 0, Int32.Parse(User.FindFirstValue("contactId")));
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> New(Orders order)
        {
            int contactId;
            int.TryParse(User.FindFirstValue("contactId"), out contactId);
            var orderToCreate = new Orders();
            orderToCreate.ContactId = contactId;
            orderToCreate.ShippingAddress = order.ShippingAddress;
            orderToCreate.BillingAddress = order.BillingAddress;
            orderToCreate.ProjectName = order.ProjectName;
            orderToCreate.PO = order.PO;
            orderToCreate.PONumber = order.PONumber;
            orderToCreate.PaymentMethod = order.PaymentMethod;
            orderToCreate.Hardcopy = order.Hardcopy;
            orderToCreate.OrderComments = order.OrderComments;
            orderToCreate.Submitted = false;

            if(orderToCreate.PO && string.IsNullOrWhiteSpace(orderToCreate.PONumber))
            {
                 ModelState.AddModelError("Order.PONumber", "Must supply PO Number" );
            }

            if(ModelState.IsValid){
                 _dbContext.Add(orderToCreate);
                await _dbContext.SaveChangesAsync();
                Message = "Order Created";
            } else {
                ErrorMessage = "Something went wrong"; 
                var model = await CreateOrdersViewModel.Create(_dbContext, 0, Int32.Parse(User.FindFirstValue("contactId")));
                return View(model);
            }

            return RedirectToAction(nameof(Details), new { id = orderToCreate.Id}); 
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