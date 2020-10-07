using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SIFCore.Models
{
    public class Analysis
    {
        public int Id { get; set; }
        [Required]
        public  Orders Order { get; set; }

        //public  Requirement Requirement { get; set; }

        //public  Requirements Requirement { get; set; }

        [Required]
        [DisplayName("Date needed")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public  DateTime DateNeeded { get; set; }

        [Required]
        [DisplayName("Number of Samples")]
        public  int NumberOfSamples { get; set; }

        [DisplayName("Tray Names")]
        [DataType(DataType.MultilineText)]
        public  string TrayNames { get; set; }

        [Required]
        public  AbundanceTypes Abundance { get; set; }

        [DisplayName("Estimated Enrichment (if enriched)")]
        public  string EstimatedEnrichment { get; set; }

        [DisplayName("Estimated Enrichment N2 (if enriched)")]
        public  string EstimatedEnrichmentN2 { get; set; }

        [DisplayName("Estimated Enrichment N2O (if enriched)")]
        public  string EstimatedEnrichmentN2O { get; set; }

        [DisplayName("Brief description of material")]
        [DataType(DataType.MultilineText)]
        public  string Material { get; set; }

        [DisplayName("Range of sample weights")]
        public  string RangeOfWeights { get; set; }

        [DisplayName("Type of water (ground, seawater, etc.)")]
        public  string TypeOfWater { get; set; }

        [DisplayName("Salinity range")]
        public  string SalinityRange { get; set; }

        [DisplayName("Amount of Oxidant")]
        public  string AmountOfOxidant { get; set; }

        [DisplayName("Type of Oxidant")]
        public  OxidantTypes TypeOfOxidant { get; set; }

        [DisplayName("pH Range")]
        public  string pHRange { get; set; }

        [DisplayName("Container Description (specify volume)")]
        public  string ContainerDescription { get; set; }

        [DisplayName("Vial Type")]
        public  VialTypes VialType { get; set; }

        [DisplayName("DIC Container")]
        public  DicContainerTypes DICContainer { get; set; }

        [DisplayName("Range of Concentration")]
        public  string RangeOfConcentration { get; set; }

        [DisplayName("Range of Concentration (minimum 1 micromole")]
        public  string RangeOfConcentrationNitrate { get; set; }

        [DisplayName("Range of Concentration (N2)")]
        public  string RangeOfConcentrationN2 { get; set; }

        [DisplayName("Range of Concentration (N2O")]
        public  string RangeOfConcentrationN2O { get; set; }

        [DisplayName("Water samples: I acknowledge $1/sample fee if transfer to 2ml vials is required")]
        public  bool TransferRequired { get; set; }

        [DisplayName("Nitrate samples: I acknowledge that if I do not provide accurate nitrate concentrations, I will be charged for any necessary reruns.")]
        public  bool NitrateStatement { get; set; }

        [DisplayName("How Sterilized")]
        public  SterilizationTypes HowSterilized { get; set; }

        [DisplayName("Volume Sent (minimum 10 ml/sample")]
        public  string VolumeSent { get; set; }

        [DisplayName("Have samples been filtered?")]
        public  bool Filtered { get; set; }

        [DisplayName("Are samples in solvent?")]
        public  bool Solvent { get; set; }

        [DisplayName("If so, what solvent?")]
        public  string WhatSolvent { get; set; }

        [DisplayName("What volume of solvent?")]
        public  string SolventVolume { get; set; }

        [DisplayName("Check if irreplacable")]
        public  bool Irreplaceable { get; set; }

        [DataType(DataType.MultilineText)]
        public  string Comments { get; set; }

        public  string AttachmentName { get; set; }
        
        [StringLength(200)]
        public  string AttachmentContentType { get; set; }

        [DisplayName("Preservative, if any (e.g. zinc chloride)")]
        public  string Preservative { get; set; }



        public enum AbundanceTypes
        {
            Natural,
            Enriched
        }

        public enum OxidantTypes
        {
            [DisplayName("No Oxidant Used")]
            None,

            [DisplayName("Niobium pentoxide (Nb2O5)")]
            Nb2O5,

            [DisplayName("Vanadium pentoxide (V2O5)")]
            V2O5
        }

        public enum VialTypes
        {
            [DisplayName("3.7 mL Exetainers")]
            Exetainers3p7,

            [DisplayName("4.5 mL Exetainers")]
            Exetainers4p5,

            [DisplayName("5.9 mL Exetainers")]
            Exetainers5p9,

            [DisplayName("12 mL Exetainers")]
            Exetainers12,

            [DisplayName("20 mL Headspace Vial")]
            HeadspaceVial20

        }

        public enum DicContainerTypes
        {
            [DisplayName("DIC in exetainer, prepped with H3PO4")]
            ExetainerPrepped,

            [DisplayName("DIC in exetainer, unprepared")]
            ExetainerUnprepped,

            [DisplayName("DIC in 40 ml IChem vial")]
            IChemVial40ml
        }

        public enum SterilizationTypes
        {
            [DisplayName("Sterile Filtered")]
            FilteredOnly,
            
            
            [DisplayName("Zinc chloride")]
            ZincChloride
        }
    }

    
}