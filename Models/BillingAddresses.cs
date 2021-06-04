using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIFCore.Models
{
    public class BillingAddresses
    {

        public int Id { get; set; }

       public virtual string FullName { get { return string.Format("{0} {1}", ContactFirstName, ContactLastName); } }

       public virtual string FullNameLastFirst { get { return string.Format("{0}, {1} ", ContactLastName, ContactFirstName); } }

            [StringLength(50)]
            [Display(Name="Address Name")]
            [Required]
            public  string AddressName { get; set; }

            public  string Institution { get; set; }

            public  string Department { get; set; }

            [StringLength(50)]
            [Display(Name="First")]
            [Required]
            public  string ContactFirstName { get; set; }

            [StringLength(50)]
            [Display(Name="Last")]
            [Required]
            public  string ContactLastName { get; set; }

            [Display(Name="Email")]
            [DataType(DataType.EmailAddress)]
            [Required]
            public  string BillingEmailAddress { get; set; }

          

            [DataType(DataType.PhoneNumber)]
            [Display(Name="Phone")]
            [Required]
            public  string BillingPhone { get; set; }

            [StringLength(50)]
            [Required]
            public  string Address1 { get; set; }

            [StringLength(50)]
            public  string Address2 { get; set; }

          
            [StringLength(50)]
            [Required]
            public  string City { get; set; }

            [StringLength(50)]
            [Display(Name="State")]
            public  string State { get; set; }

            [StringLength(50)]
            [Display(Name="Zip")]
            public  string ZipCode { get; set; }

            [StringLength(50)]
            [Required]
            public  string Country { get; set; }

            [Display(Name="Federal ID/VAT#")]            
            public  string FedIDNum { get; set; }

            [ForeignKey("ContactId")]
            public  Contacts Contact { get; set; }
            
            public int ContactId { get; set; }
    }
}