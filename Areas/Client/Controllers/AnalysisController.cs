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
            if (analysis == null) return RedirectToAction("Index", "Orders");

            return PartialView(analysis);
        }

    }
}