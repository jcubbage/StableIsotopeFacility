using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIFCore.Helpers;
using SIFCore.Models;

namespace SIFCore.Controllers.Admin
{    
    public class ChargesController : AdminController
    {
         private readonly SIFContext _dbContext;

        public ChargesController(SIFContext dbContext)
        {            
            _dbContext = dbContext;
        }

        
        public async Task<IActionResult> Edit(int id)
        {
           var model = await _dbContext.Charges.Where(c => c.Id == id).FirstOrDefaultAsync();

            if(model == null)
            {
                ErrorMessage = "Charge not found";
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

         [HttpPost]
         public async Task<IActionResult> Edit(int id, Charges charge)
         {
            var chargeToUpdate = await _dbContext.Charges.Where(r => r.Id == id).FirstOrDefaultAsync();
            
            
            if(chargeToUpdate == null || chargeToUpdate.Id != charge.Id)
            {
                ErrorMessage = "Charge not found or doesn't match call";
                return RedirectToAction(nameof(OrdersController.Index),"Orders");
            }
            chargeToUpdate.Description = charge.Description;
            chargeToUpdate.ItemCount = charge.ItemCount;
            chargeToUpdate.Cost = charge.Cost;
            chargeToUpdate.Paid = charge.Paid; 
            chargeToUpdate.ItemCode = charge.ItemCode;             

            if(ModelState.IsValid){                 
                await _dbContext.SaveChangesAsync();
                Message = "Charge Updated";
            } else {
                ErrorMessage = "Something went wrong"; 
                var model = await _dbContext.Charges.Where(c => c.Id == id).FirstOrDefaultAsync();
                return View(model);
            }           
            
            return RedirectToAction(nameof(OrdersController.Details),"Orders", new {  id = chargeToUpdate.OrderId});
            
         }    

         public async Task<IActionResult> Charge(int id)    
         {
            var analysis = await _dbContext.Analysis.Include(a => a.AnalysisRequirement).Where(a => a.OrderId == id).ToListAsync();
            var order = await _dbContext.Orders.Where(o => o.Id == id).FirstOrDefaultAsync();
            if(order == null || analysis.Count < 1)
            {
                ErrorMessage = "Order or Analysis(es) not found";
                return RedirectToAction(nameof(OrdersController.Index),"Orders");
            }
            foreach(var item in analysis)
            {
               var chargeToAdd = new Charges();
               chargeToAdd.OrderId = order.Id;
               chargeToAdd.ContactId = order.ContactId;
               chargeToAdd.Description = item.AnalysisRequirement.Name;
               chargeToAdd.ItemCount = item.NumberAnalyzed.HasValue ?  item.NumberAnalyzed.Value : 0;
               chargeToAdd.ItemCode = item.AnalysisRequirement.ItemCode;

               if(order.PaymentMethod == PaymentTypes.IOC.GetDisplayName())
               {
                   chargeToAdd.Cost = item.AnalysisRequirement.InternalCost;
               } else
               {
                  chargeToAdd.Cost = item.AnalysisRequirement.ExternalCost;
               }
               if(ModelState.IsValid)
               {
                  _dbContext.Add(chargeToAdd);                     
               } else 
               {
                  ErrorMessage = "Something went wrong";
                  return RedirectToAction(nameof(OrdersController.Index),"Orders");
               }
            }
            await _dbContext.SaveChangesAsync();
            Message = "Charges added";
            return RedirectToAction(nameof(OrdersController.Details),"Orders", new {  id = id});
        }

        public async Task<IActionResult> AddAncillary(int id)
        {
            var model = await AdminAddAncillaryViewModel.Create(_dbContext, id);
            if(model.order == null)
            {
                ErrorMessage = "Order not found";
                return RedirectToAction(nameof(OrdersController.Index),"Orders");
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddAncillary(int id, AdminAddAncillaryViewModel vm)
        {
            var order = await _dbContext.Orders.Where(o => o.Id == id).FirstOrDefaultAsync();            
            var newCharge = new Charges();
            newCharge.OrderId = vm.order.Id;
            newCharge.ContactId = order.ContactId;
            newCharge.Description = vm.newCharge.Description;
            newCharge.ItemCount = vm.newCharge.ItemCount;
            newCharge.ItemCode = vm.newCharge.ItemCode;
            newCharge.Cost = vm.newCharge.Cost;

            _dbContext.Add(newCharge);  
            await _dbContext.SaveChangesAsync();                
            Message = "Charge added";
            return RedirectToAction(nameof(OrdersController.Details),"Orders", new {  id = id});
        }

        public async Task<IActionResult> Ancillaries()
        {
            var model = await _dbContext.Ancillary.OrderBy(a => a.Description).ToListAsync();
            return View(model);
        }

        public async Task<IActionResult> PayAll(int orderId)
        {
            var charges = await _dbContext.Charges.Where(c => c.OrderId == orderId).ToListAsync();
            foreach(Charges charge in charges)
            {
                charge.Paid = true;
            }
            await _dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(OrdersController.Details),"Orders", new {id = orderId});
        }

        public async Task<IActionResult> PayLineItem(int chargeId)
        {
             var charge = await _dbContext.Charges.Where(c => c.Id == chargeId).FirstOrDefaultAsync();            
            charge.Paid = true;
            
            await _dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(OrdersController.Details),"Orders", new {id = charge.OrderId});
        }

    }
}