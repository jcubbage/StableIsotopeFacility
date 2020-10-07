using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SIFCore.Models
{
    public class Requirements
    {

        public int Id { get; set; }

       public  string Name { get; set; }
        public  string ListName { get; set; }
        [DisplayName("Analysis Name")]
        public  string FormattedName { get; set; }
        public  string FolderName { get; set; }
        public  string Subtitle { get; set; }
        public  string AnalysisTypeName { get; set; }
        public   AnalysisTypes AnalysisType { get; set; }
        public  bool CurrentAnalysis { get; set; }
        public  bool TrayNames { get; set; }
        public  bool Enriched { get; set; }
        public  bool EstimatedEnrichment { get; set; }
        public  bool EstimatedEnrichmentN2N2O { get; set; }
        public  bool Material { get; set; }
        public  bool RangeOfWeights { get; set; }
        public  bool TypeOfWater { get; set; }
        public  bool SalinityRange { get; set; }
        public  bool SalinityRangeDOC { get; set; }
        public  bool pHRange { get; set; }
        public  bool pHRangeDH18O { get; set; }
        public  bool TransferRequiredWater { get; set; }
        public  bool TransferRequiredDOC { get; set; }
        public  bool NitrateStatement { get; set; }
        public  bool DICContainer { get; set; }
        public  bool RangeOfConcentration { get; set; }
        public  bool RangeOfConcentrationN2N2O { get; set; }
        public  bool RangeOfConcentrationNitrate { get; set; }
        public  bool HowSterilized { get; set; }
        public  bool VolumeSent { get; set; }
        public  bool Filtered { get; set; }
        public  bool Solvent { get; set; }
        public  bool WhatSolvent { get; set; }
        public  bool SolventVolume { get; set; }
        public  bool TypeOfOxidant { get; set; }
        public  bool AmountOfOxidant { get; set; }
        public  bool ContainerDescription { get; set; }
        public  bool VialType { get; set; }
        public  bool Irreplaceable { get; set; }
        public  int DateDelay { get; set; }
        public  decimal Cost  { get; set; }
        public  byte AnalysisOrder { get; set; }
        public  int MinimumSampleCount { get; set; }
        public  bool Preservative { get; set; }

        [Display(Name="Terms & Conditions")]
        public  string Terms { get; set; }

        public  IList<Analysis> Analyses { get; set; }
    }
}