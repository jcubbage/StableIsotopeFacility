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
    public class AnalysisController : AdminController
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
            if (analysis == null) return RedirectToAction("Index", "Orders", new {Area = "Admin"});

            return PartialView(analysis);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var model = await AnalysisViewModel.Edit(_dbContext, id);
            return View(model);
        }

         [HttpPost]
         public async Task<IActionResult> Edit(int id, AnalysisViewModel vm)
         {
            var analysisToEdit = await _dbContext.Analysis
                .Include(a => a.AnalysisRequirement).Where(a => a.Id == id).FirstAsync();
            Analysis analysis = vm.Analysis;
            var order = await _dbContext.Orders.Where(o => o.Id == analysisToEdit.OrderId).FirstOrDefaultAsync();
            if (order == null) return RedirectToAction("Index", "Orders", new {Area = "Admin"});
            if (order.Submitted)
            {
                ErrorMessage = "You cannot edit a submitted order.";
                return RedirectToAction("Index", "Orders", new {Area = "Admin"});
            }
            // TODO Make sure user has permission!

            TransferValues(analysis, analysisToEdit);

            ModelState.Clear();
            ValidationHelper.CheckAnalysisErrors(analysisToEdit, ModelState);
           
            if (ModelState.IsValid)
            {                
                await _dbContext.SaveChangesAsync();
                Message = "Analysis Created Successfully";
                return RedirectToAction("Details", "Orders", new { id = analysisToEdit.OrderId, Area = "Admin" });
            }
            else
            {
                ErrorMessage = "Please see errors.";
                vm.Order = order;
                var requirement = analysisToEdit.AnalysisRequirement;
                vm.Requirement = requirement;
                return View(vm);
            }
         }

        public async Task<IActionResult> Create(int id, int requirementId)
        {            
            var order = await _dbContext.Orders.Where(o => o.Id == id).FirstOrDefaultAsync();
            if (order == null) return RedirectToAction("Index", "Orders", new {Area = "Admin"});
            if (order.Submitted)
            {
                ErrorMessage = "You cannot edit a submitted order.";
                return RedirectToAction("Index", "Orders", new {Area = "Admin"});
            }
            var requirement = await _dbContext.Requirements.Where(r => r.Id == requirementId).FirstOrDefaultAsync();
            if (requirement == null) return RedirectToAction("Details", "Orders", new { id = id, Area = "Admin" });

            var existingAnalysis = await _dbContext.Analysis
                .Include(o => o.Order)
                .Include(o => o.AnalysisRequirement)
                .Where(a => a.Order.Id == id && a.AnalysisRequirement.Id == requirementId).FirstOrDefaultAsync();

            if (existingAnalysis != null)
            {
                ErrorMessage = "Analysis already exists for this project. Please edit existing one rather than create new analysis.";
                return RedirectToAction("Details", "Orders", new { id = id, Area = "Admin" });
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
            if (order == null) return RedirectToAction("Index", "Orders", new {Area = "Admin"});
            if (order.Submitted)
            {
                ErrorMessage = "You cannot edit a submitted order.";
                return RedirectToAction("Index", "Orders", new {Area = "Admin"});
            }
            var requirement = await _dbContext.Requirements.Where(r => r.Id == vm.Requirement.Id).FirstOrDefaultAsync();
            if (requirement == null) return RedirectToAction("Index", "Orders", new {Area = "Admin"});
            var analysisToCreate = new Analysis();

            TransferValues(analysis, analysisToCreate);
            
            analysisToCreate.AnalysisRequirement = requirement;            
            analysisToCreate.Order = order;            
            
            ModelState.Clear();
            ValidationHelper.CheckAnalysisErrors(analysisToCreate, ModelState);
           
            if (ModelState.IsValid)
            {
                _dbContext.Add(analysisToCreate);
                await _dbContext.SaveChangesAsync();
                Message = "Analysis Created Successfully";
                return RedirectToAction("Details", "Orders", new { id = analysisToCreate.OrderId, Area = "Admin" });
            }
            else
            {
                ErrorMessage = "Please see errors.";
                vm.Order = order;
                vm.Requirement = requirement;
                return View(vm);
            }
        }

        private static void TransferValues(Analysis source, Analysis destination)
        {
            destination.DateNeeded = source.DateNeeded;
            destination.NumberOfSamples = source.NumberOfSamples;
            destination.TrayNames = source.TrayNames;
            destination.Abundance = source.Abundance;
            destination.EstimatedEnrichment = source.EstimatedEnrichment;
            destination.EstimatedEnrichmentN2 = source.EstimatedEnrichmentN2;
            destination.EstimatedEnrichmentN2O = source.EstimatedEnrichmentN2O;
            destination.Material = source.Material;
            destination.RangeOfWeights = source.RangeOfWeights;
            destination.TypeOfWater = source.TypeOfWater;
            destination.SalinityRange = source.SalinityRange;
            destination.AmountOfOxidant = source.AmountOfOxidant;
            destination.TypeOfOxidant = source.TypeOfOxidant;
            destination.pHRange = source.pHRange;
            destination.ContainerDescription = source.ContainerDescription;
            destination.VialType = source.VialType;
            destination.DICContainer = source.DICContainer;
            destination.RangeOfConcentration = source.RangeOfConcentration;
            destination.RangeOfConcentrationN2 = source.RangeOfConcentrationN2;
            destination.RangeOfConcentrationN2O = source.RangeOfConcentrationN2O;
            destination.RangeOfConcentrationNitrate = source.RangeOfConcentrationNitrate;
            destination.TransferRequired = source.TransferRequired;
            destination.NitrateStatement = source.NitrateStatement;
            destination.HowSterilized = source.HowSterilized;
            destination.VolumeSent = source.VolumeSent;
            destination.Filtered = source.Filtered;
            destination.Solvent = source.Solvent;
            destination.SolventVolume = source.SolventVolume;
            destination.WhatSolvent = source.WhatSolvent;
            destination.Irreplaceable = source.Irreplaceable;
            destination.Comments = source.Comments;
            destination.Preservative = source.Preservative;

        }

    }
}