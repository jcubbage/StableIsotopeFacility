using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIFCore.Models;

namespace SIFCore.Controllers
{    
    public class AnalysisController : ClientController
    {
         private readonly SIFContext _dbContext;

        public AnalysisController(SIFContext dbContext)
        {            
            _dbContext = dbContext;
        }


        public async Task<IActionResult> DetailsPartial(int id)
        {
            var analysis = await _dbContext.Analysis
                .Include(a => a.AnalysisRequirement)
                .Where(a => a.Id == id).FirstOrDefaultAsync();
            if (analysis == null) return RedirectToAction("Index", "Order", new {Area = "Client"});

            return PartialView(analysis);
        }

        public async Task<IActionResult> Create(int id, int requirementId)
        {
            // var requirementId = 1;
            // int id = 1;
            var order = await _dbContext.Orders.Where(o => o.Id == id).FirstOrDefaultAsync();
            if (order == null) return RedirectToAction("Index", "Order", new {Area = "Client"});
            if (order.Submitted)
            {
                ErrorMessage = "You cannot edit a submitted order.";
                return RedirectToAction("Index", "Order", new {Area = "Client"});
            }
            var requirement = await _dbContext.Requirements.Where(r => r.Id == requirementId).FirstOrDefaultAsync();
            if (requirement == null) return RedirectToAction("Details", "Order", new { id = id, Area = "Client" });

            var existingAnalysis = await _dbContext.Analysis
                .Include(o => o.Order)
                .Include(o => o.AnalysisRequirement)
                .Where(a => a.Order.Id == id && a.AnalysisRequirement.Id == requirementId).FirstOrDefaultAsync();

            if (existingAnalysis != null)
            {
                ErrorMessage = "Analysis already exists for this project. Please edit existing one rather than create new analysis.";
                return RedirectToAction("Details", "Order", new { id = id, Area = "Client" });
            }
            
			var viewModel = AnalysisViewModel.Create();
            viewModel.Requirement = requirement;
            viewModel.Order = order;

            return View(viewModel);
        }

    }
}