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
                .Include(o => o.Charges)
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

        [HttpPost]
        public async Task<IActionResult> New( CreateOrdersViewModel vm)
        {           
            var orderToCreate = new Orders();
            orderToCreate.ContactId = vm.ContactId;
            orderToCreate.ShippingAddress = vm.Order.ShippingAddress;
            orderToCreate.SIFCustomerID = vm.Order.SIFCustomerID;
            orderToCreate.ProjectName = vm.Order.ProjectName;
            orderToCreate.PO = vm.Order.PO;
            orderToCreate.PONumber = vm.Order.PONumber;
            orderToCreate.PaymentMethod = vm.Order.PaymentMethod;
            orderToCreate.OrderComments = vm.Order.OrderComments;
            orderToCreate.Submitted = true;

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
                var model = await CreateOrdersViewModel.Create(_dbContext, 0, vm.ContactId);
                return View("Create", model);
            }

            return RedirectToAction(nameof(Index)); 
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

        public async Task<IActionResult> Edit(int id)
        {
            var model = await CreateOrdersViewModel.EditViewModel(_dbContext, id);
            if(model.Order == null)
            {
                ErrorMessage = "Order not found!";
                return RedirectToAction(nameof(Index));
            }
            return View(model);

        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, CreateOrdersViewModel vm)
        {
            var orderToUpdate = await _dbContext.Orders.Where(o => o.Id == id).FirstOrDefaultAsync();
            var vmOrder = vm.Order;
            if(orderToUpdate == null || orderToUpdate.Id != vm.Order.Id)
            {
                ErrorMessage = "Order not found or match existing order!";
                return RedirectToAction(nameof(Details), new {  id = id});
            }
            orderToUpdate.ProjectName = vmOrder.ProjectName;
            orderToUpdate.ShippingAddress = vmOrder.ShippingAddress;
            orderToUpdate.SIFCustomerID = vmOrder.SIFCustomerID;
            orderToUpdate.PO = vmOrder.PO;
            orderToUpdate.PONumber = vmOrder.PONumber;
            orderToUpdate.PaymentMethod = vmOrder.PaymentMethod;
            orderToUpdate.OrderComments = vmOrder.OrderComments;

            if(ModelState.IsValid){                 
                await _dbContext.SaveChangesAsync();
                Message = "Order Updated";
            } else {
                ErrorMessage = "Something went wrong"; 
                var model = await CreateOrdersViewModel.EditViewModel(_dbContext, id);
                return View(model);
            }           
            
            return RedirectToAction(nameof(Details), new {  id = id});
        }


    }
}
