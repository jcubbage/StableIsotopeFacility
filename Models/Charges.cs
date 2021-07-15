using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SIFCore.Helpers;

namespace SIFCore.Models
{
    public class Charges
    {
        public Charges()
        {   
            Paid = false;
            DateCharged =  DateTime.Now.Date;
        }       

        public int Id { get; set; }

        [ForeignKey("ContactId")]
        public  Contacts Contact { get; set; }

        [Required]
        public int ContactId { get; set; }

        public int OrderId { get; set; }

        [ForeignKey("OrderId")]
        public Orders Order { get; set; }

        public string Description { get; set; }

        public int ItemCount { get; set; }

        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public  decimal Cost  { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public decimal LineTotal { get; set; }
        

        [DataType(DataType.Date)]
        [Display(Name="Date Charged")]
        public  DateTime  DateCharged { get; set; }

        public bool Paid { get; set; }

        public string ItemCode { get; set; }

       
    }     
    
}