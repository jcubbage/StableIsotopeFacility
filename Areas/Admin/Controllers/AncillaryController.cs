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
    public class AncillaryController : AdminController
    {
         private readonly SIFContext _dbContext;

        public AncillaryController(SIFContext dbContext)
        {            
            _dbContext = dbContext;
        }       
        

        public async Task<IActionResult> Index()
        {
            var model = await _dbContext.Ancillary.OrderBy(a => a.Description).ToListAsync();
            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var model = await _dbContext.Ancillary.Where(a => a.Id ==id).FirstOrDefaultAsync();
            if(model == null)
            {
                ErrorMessage = "Ancillary entry not found";
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Ancillary update)
        {
            var ancillaryToUpdate = await _dbContext.Ancillary.Where(a => a.Id == id).FirstOrDefaultAsync();
             if(ancillaryToUpdate == null || ancillaryToUpdate.Id != update.Id)
            {
                ErrorMessage = "Ancillary entry not found";
                return RedirectToAction(nameof(Index));
            }

            ancillaryToUpdate.Description = update.Description;
            ancillaryToUpdate.InternalCost = update.InternalCost;
            ancillaryToUpdate.ExternalCost = update.ExternalCost;
            ancillaryToUpdate.ItemCode = update.ItemCode;

            if(ModelState.IsValid)
            {
                await _dbContext.SaveChangesAsync();
                Message = "Ancillary Updated";
            } else
            {
                ErrorMessage = "Something went wrong";
                return View(ancillaryToUpdate);
            }

            return RedirectToAction(nameof(Index));

        }

    }
}