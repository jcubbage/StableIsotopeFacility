using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIFCore.Helpers;
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
            viewModel.Analysis.DateNeeded = DateTime.Now.AddDays(requirement.DateDelay);

            return View(viewModel);
        }

        // POST: /Analysis/Create
        [HttpPost]
        public async Task<IActionResult> Create(int id, AnalysisViewModel vm)
        {
            Analysis analysis = vm.Analysis;
            var order = await _dbContext.Orders.Where(o => o.Id == id).FirstOrDefaultAsync();
            if (order == null) return RedirectToAction("Index", "Orders", new {Area = "Client"});
            if (order.Submitted)
            {
                ErrorMessage = "You cannot edit a submitted order.";
                return RedirectToAction("Index", "Orders", new {Area = "Client"});
            }
            var requirement = await _dbContext.Requirements.Where(r => r.Id == vm.Requirement.Id).FirstOrDefaultAsync();
            if (requirement == null) return RedirectToAction("Index", "Orders", new {Area = "Client"});
            var analysisToCreate = new Analysis();
            
            analysisToCreate.AnalysisRequirement = requirement;
            analysisToCreate.DateNeeded = analysis.DateNeeded;
            analysisToCreate.NumberOfSamples = analysis.NumberOfSamples;
            analysisToCreate.TrayNames = analysis.TrayNames;
            analysisToCreate.Abundance = analysis.Abundance;            
            analysisToCreate.EstimatedEnrichment = analysis.EstimatedEnrichment;
            analysisToCreate.EstimatedEnrichmentN2 = analysis.EstimatedEnrichmentN2;
            analysisToCreate.EstimatedEnrichmentN2O = analysis.EstimatedEnrichmentN2O;
            analysisToCreate.Material = analysis.Material;
            analysisToCreate.RangeOfWeights = analysis.RangeOfWeights;
            analysisToCreate.TypeOfWater = analysis.TypeOfWater;
            analysisToCreate.SalinityRange = analysis.SalinityRange;
            analysisToCreate.AmountOfOxidant = analysis.AmountOfOxidant;
            analysisToCreate.TypeOfOxidant = analysis.TypeOfOxidant;
            analysisToCreate.pHRange = analysis.pHRange;
            analysisToCreate.ContainerDescription = analysis.ContainerDescription;
            analysisToCreate.VialType = analysis.VialType;
            analysisToCreate.DICContainer = analysis.DICContainer;
            analysisToCreate.RangeOfConcentration = analysis.RangeOfConcentration;
            analysisToCreate.RangeOfConcentrationN2 = analysis.RangeOfConcentrationN2;
            analysisToCreate.RangeOfConcentrationN2O = analysis.RangeOfConcentrationN2O;
            analysisToCreate.RangeOfConcentrationNitrate = analysis.RangeOfConcentrationNitrate;
            analysisToCreate.TransferRequired = analysis.TransferRequired;
            analysisToCreate.NitrateStatement = analysis.NitrateStatement;
            analysisToCreate.HowSterilized = analysis.HowSterilized;
            analysisToCreate.VolumeSent = analysis.VolumeSent;
            analysisToCreate.Filtered = analysis.Filtered;
            analysisToCreate.Solvent = analysis.Solvent;
            analysisToCreate.SolventVolume = analysis.SolventVolume;
            analysisToCreate.WhatSolvent = analysis.WhatSolvent;
            analysisToCreate.Irreplaceable = analysis.Irreplaceable;
            analysisToCreate.Comments = analysis.Comments;
            analysisToCreate.Preservative = analysis.Preservative;
            analysisToCreate.Order = order;
            
            
            ModelState.Clear();
            ValidationHelper.CheckAnalysisErrors(analysisToCreate, ModelState);
           

            if (ModelState.IsValid)
            {
                _dbContext.Add(analysisToCreate);
                await _dbContext.SaveChangesAsync();
                Message = "Analysis Created Successfully";
                return RedirectToAction("Details", "Orders", new { id = analysisToCreate.Order.Id, Area = "Client" });
            }
            else
            {
                ErrorMessage = "Please see errors.";
                vm.Order = order;
                vm.Requirement = requirement;
                return View(vm);
            }
        }

    }
}