using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIFCore.Models
{
    public class Customers
    {

        public int Id { get; set; }

        [StringLength(500)]        
        [Required]
        public  string Name { get; set; }

        public string DisplayInfo 
        { 
            get
            {
                return $"{SIFCustomerNumberDisplay} {Name}<br> {Address1}, {City} <br>{Notes.Substring(0, Math.Min(Notes.Length, 30))}";

            }
        }

        [StringLength(50)]
        public string Type { get; set; }


        [StringLength(50)]
        [Display(Name="Fed Id #")]
        public string FedIdNumber { get; set; }

        [StringLength(200)]
        [Required]
        public  string Address1 { get; set; }

        [StringLength(200)]
        public  string Address2 { get; set; }

        public string AddressLines
        {
            get
            {                
                if(!string.IsNullOrWhiteSpace(Address2))
                {
                    return $"{Address1}<br>{Address2}";
                }
                return Address1;
            }
        }

        
        [StringLength(50)]
        [Required]
        public  string City { get; set; }

        [StringLength(50)]
        [Display(Name="State")]
        public  string State { get; set; }

        [StringLength(100)]
        [Display(Name="Zip")]
        public  string Zip { get; set; }

        [StringLength(50)]
        [Required]
        public  string Country { get; set; }

        [StringLength(5000)]
        public string Notes { get; set; }

        [StringLength(50)]
        [Display(Name="KFS Cust#")]
        public string KFSCustomerNumber { get; set; }
        
        public int SIFCustomerNumberInt { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [Display(Name="SIF Cust#")]
        public string SIFCustomerNumberDisplay { get; set; }   
          
        

       
    }
}