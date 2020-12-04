using SIFCore.Models;

namespace SIFCore.Helpers
{
    public static class ValidationHelper
    {
       public static void CheckAnalysisErrors(Analysis analysis, Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary state)
        {
            if (analysis.NumberOfSamples==0)
            {
               state.AddModelError("Analysis.NumberOfSamples", "Please provide the number of samples");
            }
            else
            {
                if(analysis.NumberOfSamples < analysis.AnalysisRequirement.MinimumSampleCount)
                {
                   state.AddModelError("Analysis.NumberOfSamples", string.Format("Minimum number of samples is {0}", analysis.AnalysisRequirement.MinimumSampleCount));
                }
            }



            if (analysis.AnalysisRequirement.TrayNames)
            {
                if (string.IsNullOrWhiteSpace(analysis.TrayNames))
                {
                    state.AddModelError("Analysis.TrayNames", "Tray names must be provided");
                }
            }

            if(!analysis.AnalysisRequirement.Enriched)
            {
                analysis.Abundance = AbundanceTypes.Natural.GetDisplayName();
            }

           if (analysis.Abundance == AbundanceTypes.Natural.GetDisplayName())
           {
               analysis.EstimatedEnrichment = string.Empty;
               analysis.EstimatedEnrichmentN2 = string.Empty;
               analysis.EstimatedEnrichmentN2O = string.Empty;
           } else
           {
               if (analysis.AnalysisRequirement.EstimatedEnrichment)
               {
                   if (string.IsNullOrWhiteSpace(analysis.EstimatedEnrichment))
                   {
                       state.AddModelError("Analysis.EstimatedEnrichment", "Estimated Enrichment must be provided if sample is enriched");
                   }
               }
               if (analysis.AnalysisRequirement.EstimatedEnrichmentN2N2O)
               {
                   if (string.IsNullOrWhiteSpace(analysis.EstimatedEnrichmentN2))
                   {
                       state.AddModelError("Analysis.EstimatedEnrichmentN2", "Estimated Enrichment N2 must be provided if sample is enriched");
                   }
                   if (string.IsNullOrWhiteSpace(analysis.EstimatedEnrichmentN2O))
                   {
                       state.AddModelError("Analysis.EstimatedEnrichmentN2O", "Estimated Enrichment N2O must be provided if sample is enriched");
                   }
               }
           }

           if(analysis.AnalysisRequirement.Material)
           {
               if (string.IsNullOrWhiteSpace(analysis.Material))
               {
                   state.AddModelError("Analysis.Material", "Material field must be provided");
               }
           }

           if (analysis.AnalysisRequirement.Material)
           {
               if (string.IsNullOrWhiteSpace(analysis.Material))
               {
                   state.AddModelError("Analysis.Material", "Material field must be provided");
               }
           }

           if (analysis.AnalysisRequirement.RangeOfWeights)
           {
               if (string.IsNullOrWhiteSpace(analysis.RangeOfWeights))
               {
                   state.AddModelError("Analysis.RangeOfWeights", "Range of weights field must be provided");
               }
           }


           if (analysis.AnalysisRequirement.AmountOfOxidant)
           {
               if(string.IsNullOrWhiteSpace(analysis.TypeOfOxidant))
               {
                   state.AddModelError("Analysis.TypeOfOxidant", "Please select type of Oxidant or select 'No Oxidant Used'");
               } else
               {
                   if (analysis.TypeOfOxidant != OxidantTypes.None.GetDisplayName())
                   {
                       if (string.IsNullOrWhiteSpace(analysis.AmountOfOxidant))
                       {
                           state.AddModelError("Analysis.AmountOfOxidant","Amount of oxidant field must be provided");
                       }
                   }
                   else
                   {
                       analysis.AmountOfOxidant = string.Empty;
                   }
               }
           }

           if(analysis.AnalysisRequirement.VialType)
           {
               if(string.IsNullOrWhiteSpace(analysis.VialType))
               {
                   state.AddModelError("Analysis.VialType", "Please select Vial Type");
               }
           }

           if(analysis.AnalysisRequirement.DICContainer)
           {
               if(string.IsNullOrWhiteSpace(analysis.DICContainer))
               {
                   state.AddModelError("Analysis.DICContainer", "Please select DIC Container Type");
               }
           }

           if(analysis.AnalysisRequirement.HowSterilized)
           {
               if(string.IsNullOrWhiteSpace(analysis.HowSterilized))
               {
                   state.AddModelError("Analysis.HowSterilized", "Please select how sterilized");
               }
           }

           if (analysis.AnalysisRequirement.ContainerDescription)
           {
               if (string.IsNullOrWhiteSpace(analysis.ContainerDescription))
               {
                   state.AddModelError("Analysis.ContainerDescription", "Container description field must be provided");
               }
           }

           if (analysis.AnalysisRequirement.TypeOfWater)
           {
               if (string.IsNullOrWhiteSpace(analysis.TypeOfWater))
               {
                   state.AddModelError("Analysis.TypeOfWater", "Type of water field must be provided");
               }
           }

           if (analysis.AnalysisRequirement.SalinityRange)
           {
               if (string.IsNullOrWhiteSpace(analysis.SalinityRange))
               {
                   state.AddModelError("Analysis.SalinityRange", "Salinity range field must be provided");
               }
           }

           if (analysis.AnalysisRequirement.SalinityRangeDOC)
           {
               if (string.IsNullOrWhiteSpace(analysis.SalinityRange))
               {
                   state.AddModelError("Analysis.SalinityRange", "Salinity range field must be provided");
               }
           }

           if (analysis.AnalysisRequirement.pHRange)
           {
               if (string.IsNullOrWhiteSpace(analysis.pHRange))
               {
                   state.AddModelError("Analysis.pHRange", "pH range field must be provided");
               }
           }

           if (analysis.AnalysisRequirement.pHRangeDH18O)
           {
               if (string.IsNullOrWhiteSpace(analysis.pHRange))
               {
                   state.AddModelError("Analysis.pHRange", "pH range field must be provided");
               }
           }

           if (analysis.AnalysisRequirement.TransferRequiredWater)
           {
               if (!analysis.TransferRequired)
               {
                   state.AddModelError("Analysis.TransferRequired", "You must acknowledge the water sample transfer statement");
               }
           }

           if (analysis.AnalysisRequirement.TransferRequiredDOC)
           {
               if (!analysis.TransferRequired)
               {
                   state.AddModelError("Analysis.TransferRequired", "You must acknowledge the water sample transfer statement");
               }
           }

           if (analysis.AnalysisRequirement.NitrateStatement)
           {
               if (!analysis.NitrateStatement)
               {
                   state.AddModelError("Analysis.NitrateStatement", "You must acknowledge the nitrate statement");
               }
           }

           if (analysis.AnalysisRequirement.RangeOfConcentration)
           {
               if (string.IsNullOrWhiteSpace(analysis.RangeOfConcentration))
               {
                   state.AddModelError("Analysis.RangeOfConcentration", "Range of concentration field must be provided");
               }
           }

           if (analysis.AnalysisRequirement.RangeOfConcentrationNitrate)
           {
               if (string.IsNullOrWhiteSpace(analysis.RangeOfConcentrationNitrate))
               {
                   state.AddModelError("Analysis.RangeOfConcentrationNitrate", "Range of concentration field must be provided");
               }
           }

           if (analysis.AnalysisRequirement.RangeOfConcentrationN2N2O)
           {
               if (string.IsNullOrWhiteSpace(analysis.RangeOfConcentrationN2))
               {
                   state.AddModelError("Analysis.RangeOfConcentrationN2", "Range of concentration N2 field must be provided");
               }
               if (string.IsNullOrWhiteSpace(analysis.RangeOfConcentrationN2O))
               {
                   state.AddModelError("Analysis.RangeOfConcentrationN2O", "Range of concentration N2O field must be provided");
               }
           }

           if (analysis.AnalysisRequirement.VolumeSent)
           {
               if (string.IsNullOrWhiteSpace(analysis.VolumeSent))
               {
                   state.AddModelError("Analysis.VolumeSent", "Volume sent field must be provided");
               }
           }

           if (analysis.AnalysisRequirement.WhatSolvent)
           {
               if (analysis.Solvent && string.IsNullOrWhiteSpace(analysis.WhatSolvent))
               {
                   state.AddModelError("Analysis.WhatSolvent", "What solvent field must be provided if samples are in solvent");
               }
           }

           if (analysis.AnalysisRequirement.SolventVolume)
           {
               if (analysis.Solvent && string.IsNullOrWhiteSpace(analysis.SolventVolume))
               {
                   state.AddModelError("Analysis.SolventVolume", "Solvent volume field must be provided if samples are in solvent");
               }
           }

        }
    }
}