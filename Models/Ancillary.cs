using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SIFCore.Helpers;

namespace SIFCore.Models
{
    public class Ancillary
    {

         
        public int Id { get; set; }

        public string Description { get; set; }

        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public  decimal InternalCost  { get; set; }

        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public  decimal ExternalCost  { get; set; }
       
        public string ItemCode { get; set; }

         public string Summary
        {
            get
            {  
                return $"{Description}//{InternalCost:n2}//{ExternalCost:n2}//{ItemCode}";
            }
        }
       
    }     
    
}