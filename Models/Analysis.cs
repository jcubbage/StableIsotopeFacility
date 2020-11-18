using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIFCore.Models
{
    public class Analysis
    {
        public int Id { get; set; }
        [Required]
        public  Orders Order { get; set; }

        public int OrderId { get; set; }

        [ForeignKey("RequirementId")]
        public Requirements AnalysisRequirement { get; set; }
        
        public int RequirementId { get; set; }

        [Required]
        [Display(Name="Date needed")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public  DateTime DateNeeded { get; set; }

        [Required]
        [Display(Name="Number of Samples")]
        public  int NumberOfSamples { get; set; }

        [Display(Name="Tray Names")]
        [DataType(DataType.MultilineText)]
        public  string TrayNames { get; set; }        

        [Required]
        public string Abundance { get; set; }

        [Display(Name="Estimated Enrichment (if enriched)")]
        public  string EstimatedEnrichment { get; set; }

        [Display(Name="Estimated Enrichment N2 (if enriched)")]
        public  string EstimatedEnrichmentN2 { get; set; }

        [Display(Name="Estimated Enrichment N2O (if enriched)")]
        public  string EstimatedEnrichmentN2O { get; set; }

        [Display(Name="Brief description of material")]
        [DataType(DataType.MultilineText)]
        public  string Material { get; set; }

        [Display(Name="Range of sample weights")]
        public  string RangeOfWeights { get; set; }

        [Display(Name="Type of water (ground, seawater, etc.)")]
        public  string TypeOfWater { get; set; }

        [Display(Name="Salinity range")]
        public  string SalinityRange { get; set; }

        [Display(Name="Amount of Oxidant")]
        public  string AmountOfOxidant { get; set; }

        [Display(Name="Type of Oxidant")]
        public  string TypeOfOxidant { get; set; }

        [Display(Name="pH Range")]
        public  string pHRange { get; set; }

        [Display(Name="Container Description (specify volume)")]
        public  string ContainerDescription { get; set; }

        [Display(Name="Vial Type")]
        public  string VialType { get; set; }

        [Display(Name="DIC Container")]
        public  string DICContainer { get; set; }

        [Display(Name="Range of Concentration")]
        public  string RangeOfConcentration { get; set; }

        [Display(Name="Range of Concentration (minimum 1 micromole")]
        public  string RangeOfConcentrationNitrate { get; set; }

        [Display(Name="Range of Concentration (N2)")]
        public  string RangeOfConcentrationN2 { get; set; }

        [Display(Name="Range of Concentration (N2O")]
        public  string RangeOfConcentrationN2O { get; set; }

        [Display(Name="Water samples: I acknowledge $1/sample fee if transfer to 2ml vials is required")]
        public  bool TransferRequired { get; set; }

        [Display(Name="Nitrate samples: I acknowledge that if I do not provide accurate nitrate concentrations, I will be charged for any necessary reruns.")]
        public  bool NitrateStatement { get; set; }

        [Display(Name="How Sterilized")]
        public  string HowSterilized { get; set; }

        [Display(Name="Volume Sent (minimum 10 ml/sample")]
        public  string VolumeSent { get; set; }

        [Display(Name="Have samples been filtered?")]
        public  bool Filtered { get; set; }

        [Display(Name="Are samples in solvent?")]
        public  bool Solvent { get; set; }

        [Display(Name="If so, what solvent?")]
        public  string WhatSolvent { get; set; }

        [Display(Name="What volume of solvent?")]
        public  string SolventVolume { get; set; }

        [Display(Name="Check if irreplaceable")]
        public  bool Irreplaceable { get; set; }

        [DataType(DataType.MultilineText)]
        public  string Comments { get; set; }

        public  string AttachmentName { get; set; }
        
        [StringLength(200)]
        public  string AttachmentContentType { get; set; }

        [Display(Name="Preservative, if any (e.g. zinc chloride)")]
        public  string Preservative { get; set; }



        public enum AbundanceTypes
        {
             [Display(Name="Natural")]
            Natural,
             [Display(Name="Enriched")]
            Enriched
        }

        public enum OxidantTypes
        {
            [Display(Name="No Oxidant Used")]
            None,

            [Display(Name="Niobium pentoxide (Nb2O5)")]
            Nb2O5,

            [Display(Name="Vanadium pentoxide (V2O5)")]
            V2O5
        }

        public enum VialTypes
        {
            [Display(Name="3.7 mL Exetainers")]
            Exetainers3p7,

            [Display(Name="4.5 mL Exetainers")]
            Exetainers4p5,

            [Display(Name="5.9 mL Exetainers")]
            Exetainers5p9,

            [Display(Name="12 mL Exetainers")]
            Exetainers12,

            [Display(Name="20 mL Headspace Vial")]
            HeadspaceVial20

        }

        public enum DicContainerTypes
        {
            [Display(Name="DIC in exetainer, prepped with H3PO4")]
            ExetainerPrepped,

            [Display(Name="DIC in exetainer, unprepared")]
            ExetainerUnprepped,

            [Display(Name="DIC in 40 ml IChem vial")]
            IChemVial40ml
        }

        public enum SterilizationTypes
        {
            [Display(Name="Sterile Filtered")]
            FilteredOnly,
            
            
            [Display(Name="Zinc chloride")]
            ZincChloride
        }
    }

    
}