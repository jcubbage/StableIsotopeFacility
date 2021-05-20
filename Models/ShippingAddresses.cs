using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIFCore.Models
{
    public class ShippingAddresses
    {
        public int Id { get; set; }

        public  string FullName { get { return string.Format("{0} {1}", PIFirstName, PILastName); } }

        public  string FullNameLastFirst { get { return string.Format("{0}, {1} ", PILastName, PIFirstName); } }

        [StringLength(50)]
        [Display(Name="Address Name")]
        [Required]
        public  string AddressName { get; set; }

       public  string Institution { get; set; }

        public  string Department { get; set; }

        [StringLength(50)]
        [Required]
        [Display(Name="PI First Name")]
        public  String PIFirstName { get; set; }

        [StringLength(50)]
        [Display(Name="PI Last Name")]
        [Required]
        public  string PILastName   { get; set; }

        [Display(Name= "PI Email Address")]
        [Required]
        [DataType(DataType.EmailAddress)]
        public  string PIEmail { get; set; }
        

        [DataType(DataType.PhoneNumber)]
        [Display(Name="PI Fax")]
        public  string PIFax { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name="PI Phone")]
        [Required]
        public  string PIPhone { get; set; }


        [StringLength(50)]
        [Required]
        public  string Address1 { get; set; }

        [StringLength(50)]
        public  string Address2 { get; set; }

        [StringLength(50)]
        public  string Address3 { get; set; }

        [StringLength(50)]
        [Required]
        public  string City { get; set; }

        [StringLength(50)]
        [Display(Name="State/Governing District")]
        public  string State { get; set; }

        [StringLength(50)]
        [Display(Name="Zip/Postal Code")]
        public  string ZipCode { get; set; }

        [StringLength(50)]
        [Required]
        public  string Country { get; set; }
        
        [ForeignKey("ContactId")]
        public  Contacts Contact { get; set; }
        
        public int ContactId { get; set; }

        [StringLength(50)]        
        [Display(Name="Researcher First Name")]
        public string ResearcherFirstName { get; set; }

        [StringLength(50)]        
        [Display(Name="Researcher Last Name")]
        public string ResearcherLastName { get; set; }

        [Display(Name= "Researcher Email Address")]        
        [DataType(DataType.EmailAddress)]
        public  string ResearcherEmail { get; set; }        

        
        [DataType(DataType.PhoneNumber)]
        [Display(Name="Researcher Phone")]
        public  string ResearcherPhone { get; set; }

        [StringLength(5000)]
        public string Notes { get; set; }

        

    }
}