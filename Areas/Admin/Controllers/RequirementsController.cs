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
    public class RequirementsController : AdminController
    {
         private readonly SIFContext _dbContext;

        public RequirementsController(SIFContext dbContext)
        {            
            _dbContext = dbContext;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _dbContext.Requirements.Include(r => r.AnalysisType).ToListAsync();
            return View(model);
        }

        public async Task<IActionResult> Details(int id)
        {
            var model = await _dbContext.Requirements.Where(r => r.Id == id).FirstOrDefaultAsync();
            if(model == null)
            {
                ErrorMessage = "Analysis Requirement not found";
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }


        public async Task<IActionResult> Edit(int id)
        {
            var model = await AnalysisViewModel.Edit(_dbContext, id);
            return View(model);
        }

         [HttpPost]
         public async Task<IActionResult> Edit(int id, AnalysisViewModel vm)
         {
             return View();
            
         }

        public async Task<IActionResult> Create()
        {            
            var model = new Analysis();
            
            return View(model);
        }

        // POST: /Analysis/Create
        [HttpPost]
        public async Task<IActionResult> Create(int id, AnalysisViewModel vm)
        {
            return View();
        }

        

    }
}