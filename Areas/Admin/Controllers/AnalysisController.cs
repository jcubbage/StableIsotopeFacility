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
                .Include(a => a.LeadApprovedEmployee)
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
           
            TransferValues(analysis, analysisToEdit);
            // TODO Make sure user has permission!
            if(analysis.LeadApproved && !analysisToEdit.LeadApproved)
            {
                analysisToEdit.LeadApproved = true;
                analysisToEdit.LeadApprovedDate = DateTime.Now;
                analysisToEdit.LeadApprovedBy = int.Parse(User.Identity.Name);
                // identity.FindFirst(ClaimTypes.Name)
            }
            analysisToEdit.LeadNotes = analysis.LeadNotes;            

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

         public async Task<IActionResult> SplitToDifficult(int numberDifficult, int analysisId)
         {
             var analysisToSplit = await _dbContext.Analysis.Include(a => a.AnalysisRequirement).Where(a => a.Id == analysisId).FirstOrDefaultAsync();
             if(analysisToSplit == null)
             {
                 ErrorMessage = "Analysis to split not found";
                 return RedirectToAction(nameof(OrdersController.Index),"Orders");
             }
             if(!analysisToSplit.AnalysisRequirement.HasDifficult || !analysisToSplit.AnalysisRequirement.DifficultID.HasValue)
             {
                 ErrorMessage = "Analysis does not have a configured difficult equivalent.";
                 return RedirectToAction(nameof(OrdersController.Details),"Orders", new {id = analysisToSplit.OrderId});
             }
             var difficultAnalysis = new Analysis();
              TransferValues(analysisToSplit, difficultAnalysis);
              analysisToSplit.NumberOfSamples = analysisToSplit.NumberOfSamples - numberDifficult;
              difficultAnalysis.NumberOfSamples = numberDifficult;
              analysisToSplit.NumberReceived = analysisToSplit.NumberReceived - numberDifficult;
              difficultAnalysis.NumberReceived = numberDifficult;
              analysisToSplit.NumberAnalyzed = analysisToSplit.NumberAnalyzed - numberDifficult;
              difficultAnalysis.NumberAnalyzed = numberDifficult;
              difficultAnalysis.OrderId = analysisToSplit.OrderId;
              difficultAnalysis.RequirementId = analysisToSplit.AnalysisRequirement.DifficultID.Value;

              if(ModelState.IsValid)
              {
                  _dbContext.Add(difficultAnalysis);
                  await _dbContext.SaveChangesAsync();
                  Message = "Analysis split to difficult";                  
              } else
              {
                  ErrorMessage = "Somthing went wrong";
              }
              return RedirectToAction(nameof(OrdersController.Details),"Orders", new {id = analysisToSplit.OrderId});

         }

        public async Task<IActionResult> Create(int id, int requirementId)
        {            
            var order = await _dbContext.Orders.Where(o => o.Id == id).FirstOrDefaultAsync();
            if (order == null) return RedirectToAction("Index", "Orders", new {Area = "Admin"});
           
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

        public async Task<IActionResult> ReceiveAll(int id)
        {
            var analysisToReceive = await _dbContext.Analysis.Where(a => a.Id == id).FirstOrDefaultAsync();
            if(analysisToReceive == null)
            {
                ErrorMessage = "Analysis not found";
                return RedirectToAction(nameof(OrdersController.Index),"Orders");
            }
            analysisToReceive.NumberReceived = analysisToReceive.NumberOfSamples;
            if(ModelState.IsValid)
            {
                await _dbContext.SaveChangesAsync();
                Message = "Analyssis marked received all";                
            } else
            {
                ErrorMessage = "Something went wrong";                
            }
            return RedirectToAction(nameof(OrdersController.Details), "Orders", new { id = analysisToReceive.OrderId});
        }

        public async Task<IActionResult> AnalyzeAll(int id)
        {
            var analysisToAnalyze = await _dbContext.Analysis.Where(a => a.Id == id).FirstOrDefaultAsync();
            if(analysisToAnalyze == null || analysisToAnalyze.NumberReceived == null)
            {
                ErrorMessage = "Analysis not found or not received";
                return RedirectToAction(nameof(OrdersController.Index),"Orders");
            }
            analysisToAnalyze.NumberAnalyzed = analysisToAnalyze.NumberReceived;
            if(ModelState.IsValid)
            {
                await _dbContext.SaveChangesAsync();
                Message = "Analyssis marked all analyzed";                
            } else
            {
                ErrorMessage = "Something went wrong";                
            }
            return RedirectToAction(nameof(OrdersController.Details), "Orders", new { id = analysisToAnalyze.OrderId});
        }

        private static void TransferValues(Analysis source, Analysis destination)
        {
            // NOTE: Lead approved values not transferred!
            destination.DateNeeded = source.DateNeeded;
            destination.NumberOfSamples = source.NumberOfSamples;
            destination.NumberReceived = source.NumberReceived;
            destination.NumberAnalyzed = source.NumberAnalyzed;
            destination.StorageLocation = source.StorageLocation;
            destination.ShippingCondition = source.ShippingCondition;
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