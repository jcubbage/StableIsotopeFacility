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
            var model = await _dbContext.Requirements.Include(r => r.AnalysisType).Where(r => r.Id == id).FirstOrDefaultAsync();
            if(model == null)
            {
                ErrorMessage = "Analysis Requirement not found";
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> Create()
        {
            var model = await AdminRequirementEditViewModel.Create(_dbContext, 0);
            return View(model);
        }

        [HttpPost]
         public async Task<IActionResult> Create(AdminRequirementEditViewModel vm)
         {
            var reqToCreate = new Requirements();
             
            var editedReq = vm.requirement;  
            reqToCreate.CurrentAnalysis = true;          
            reqToCreate.Name = editedReq.Name;
            reqToCreate.ListName  = editedReq.ListName;
            reqToCreate.FormattedName = editedReq.FormattedName;           
            reqToCreate.Subtitle = editedReq.Subtitle;
            reqToCreate.AnalysisTypeId = editedReq.AnalysisTypeId;
            reqToCreate.CurrentAnalysis = editedReq.CurrentAnalysis;
            reqToCreate.TrayNames = editedReq.TrayNames;
            reqToCreate.Enriched = editedReq.Enriched;
            reqToCreate.EstimatedEnrichment = editedReq.EstimatedEnrichment;
            reqToCreate.EstimatedEnrichmentN2N2O = editedReq.EstimatedEnrichmentN2N2O;
            reqToCreate.Material = editedReq.Material;
            reqToCreate.RangeOfWeights = editedReq.RangeOfWeights;
            reqToCreate.TypeOfWater = editedReq.TypeOfWater;
            reqToCreate.SalinityRange = editedReq.SalinityRange;
            reqToCreate.SalinityRangeDOC = editedReq.SalinityRangeDOC;
            reqToCreate.pHRange = editedReq.pHRange;
            reqToCreate.pHRangeDH18O = editedReq.pHRangeDH18O;
            reqToCreate.TransferRequiredWater = editedReq.TransferRequiredWater;
            reqToCreate.TransferRequiredDOC = editedReq.TransferRequiredDOC;
            reqToCreate.NitrateStatement = editedReq.NitrateStatement;
            reqToCreate.DICContainer = editedReq.DICContainer;
            reqToCreate.RangeOfConcentration = editedReq.RangeOfConcentration;
            reqToCreate.RangeOfConcentrationN2N2O = editedReq.RangeOfConcentrationN2N2O;
            reqToCreate.RangeOfConcentrationNitrate = editedReq.RangeOfConcentrationNitrate;
            reqToCreate.HowSterilized = editedReq.HowSterilized;
            reqToCreate.VolumeSent = editedReq.VolumeSent;
            reqToCreate.Filtered = editedReq.Filtered;
            reqToCreate.Solvent = editedReq.Solvent;
            reqToCreate.WhatSolvent = editedReq.WhatSolvent;
            reqToCreate.SolventVolume = editedReq.SolventVolume;
            reqToCreate.VialType = editedReq.VialType;
            reqToCreate.TypeOfOxidant = editedReq.TypeOfOxidant;
            reqToCreate.AmountOfOxidant = editedReq.AmountOfOxidant;
            reqToCreate.ContainerDescription = editedReq.ContainerDescription;
            reqToCreate.DateDelay = editedReq.DateDelay;
            reqToCreate.Irreplaceable = editedReq.Irreplaceable;
            reqToCreate.Preservative = editedReq.Preservative;
            reqToCreate.InternalCost = editedReq.InternalCost;
            reqToCreate.ExternalCost = editedReq.ExternalCost;
            reqToCreate.MinimumSampleCount = editedReq.MinimumSampleCount;
            reqToCreate.AnalysisOrder = editedReq.AnalysisOrder;
            reqToCreate.Terms = editedReq.Terms;      
            reqToCreate.ItemCode = editedReq.ItemCode;       

            if(ModelState.IsValid){    
                _dbContext.Add(reqToCreate);             
                await _dbContext.SaveChangesAsync();
                Message = "Created new Analysis Requirement";
            } else {
                ErrorMessage = "Something went wrong"; 
                var model = await AdminRequirementEditViewModel.Create(_dbContext, 0);
                return View(model);
            }           
            
            return RedirectToAction(nameof(Details), new {  id = reqToCreate.Id});
            
         }  


        public async Task<IActionResult> Edit(int id)
        {
           var model = await AdminRequirementEditViewModel.Create(_dbContext, id);
            if(model.requirement == null)
            {
                ErrorMessage = "Analysis Requirement not found";
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

         [HttpPost]
         public async Task<IActionResult> Edit(int id, AdminRequirementEditViewModel vm)
         {
            var reqToUpdate = await _dbContext.Requirements.Where(r => r.Id == id).FirstOrDefaultAsync();
             
            var editedReq = vm.requirement;
            if(reqToUpdate == null || reqToUpdate.Id != vm.requirement.Id)
            {
                ErrorMessage = "Analysis not found or doesn't match call";
                return RedirectToAction(nameof(Index));
            }
            reqToUpdate.Name = editedReq.Name;
            reqToUpdate.ListName  = editedReq.ListName;
            reqToUpdate.FormattedName = editedReq.FormattedName;           
            reqToUpdate.Subtitle = editedReq.Subtitle;
            reqToUpdate.AnalysisTypeId = editedReq.AnalysisTypeId;
            reqToUpdate.CurrentAnalysis = editedReq.CurrentAnalysis;
            reqToUpdate.TrayNames = editedReq.TrayNames;
            reqToUpdate.Enriched = editedReq.Enriched;
            reqToUpdate.EstimatedEnrichment = editedReq.EstimatedEnrichment;
            reqToUpdate.EstimatedEnrichmentN2N2O = editedReq.EstimatedEnrichmentN2N2O;
            reqToUpdate.Material = editedReq.Material;
            reqToUpdate.RangeOfWeights = editedReq.RangeOfWeights;
            reqToUpdate.TypeOfWater = editedReq.TypeOfWater;
            reqToUpdate.SalinityRange = editedReq.SalinityRange;
            reqToUpdate.SalinityRangeDOC = editedReq.SalinityRangeDOC;
            reqToUpdate.pHRange = editedReq.pHRange;
            reqToUpdate.pHRangeDH18O = editedReq.pHRangeDH18O;
            reqToUpdate.TransferRequiredWater = editedReq.TransferRequiredWater;
            reqToUpdate.TransferRequiredDOC = editedReq.TransferRequiredDOC;
            reqToUpdate.NitrateStatement = editedReq.NitrateStatement;
            reqToUpdate.DICContainer = editedReq.DICContainer;
            reqToUpdate.RangeOfConcentration = editedReq.RangeOfConcentration;
            reqToUpdate.RangeOfConcentrationN2N2O = editedReq.RangeOfConcentrationN2N2O;
            reqToUpdate.RangeOfConcentrationNitrate = editedReq.RangeOfConcentrationNitrate;
            reqToUpdate.HowSterilized = editedReq.HowSterilized;
            reqToUpdate.VolumeSent = editedReq.VolumeSent;
            reqToUpdate.Filtered = editedReq.Filtered;
            reqToUpdate.Solvent = editedReq.Solvent;
            reqToUpdate.WhatSolvent = editedReq.WhatSolvent;
            reqToUpdate.SolventVolume = editedReq.SolventVolume;
            reqToUpdate.VialType = editedReq.VialType;
            reqToUpdate.TypeOfOxidant = editedReq.TypeOfOxidant;
            reqToUpdate.AmountOfOxidant = editedReq.AmountOfOxidant;
            reqToUpdate.ContainerDescription = editedReq.ContainerDescription;
            reqToUpdate.DateDelay = editedReq.DateDelay;
            reqToUpdate.Irreplaceable = editedReq.Irreplaceable;
            reqToUpdate.Preservative = editedReq.Preservative;
            reqToUpdate.InternalCost = editedReq.InternalCost;
            reqToUpdate.ExternalCost = editedReq.ExternalCost;
            reqToUpdate.MinimumSampleCount = editedReq.MinimumSampleCount;
            reqToUpdate.AnalysisOrder = editedReq.AnalysisOrder;
            reqToUpdate.Terms = editedReq.Terms;      
            reqToUpdate.ItemCode = editedReq.ItemCode;       

            if(ModelState.IsValid){                 
                await _dbContext.SaveChangesAsync();
                Message = "Analysis Requirements Updated";
            } else {
                ErrorMessage = "Something went wrong"; 
                var model = await AdminRequirementEditViewModel.Create(_dbContext, id);
                return View(model);
            }           
            
            return RedirectToAction(nameof(Details), new {  id = id});
            
         }        

    }
}