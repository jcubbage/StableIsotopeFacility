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
            [DisplayName("Address Name")]
            [Required]
            public  string AddressName { get; set; }

            public  string Institution { get; set; }

            public  string Department { get; set; }

            [StringLength(50)]
            [DisplayName("Billing Contact First Name")]
            [Required]
            public  string ContactFirstName { get; set; }

            [StringLength(50)]
            [DisplayName("Billing Contact Last Name")]
            [Required]
            public  string ContactLastName { get; set; }

            [DisplayName("Billing Email Address")]
            [DataType(DataType.EmailAddress)]
            [Required]
            public  string BillingEmailAddress { get; set; }

            [DataType(DataType.PhoneNumber)]
            [DisplayName("Billing Fax")]
            public  string BillingFax { get; set; }

            [DataType(DataType.PhoneNumber)]
            [DisplayName("Billing Phone")]
            [Required]
            public  string BillingPhone { get; set; }

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
            [DisplayName("State/Governing District")]
            public  string State { get; set; }

            [StringLength(50)]
            [DisplayName("Zip/Postal Code")]
            public  string ZipCode { get; set; }

            [StringLength(50)]
            [Required]
            public  string Country { get; set; }

            [DisplayName("Federal ID Number")]
            [Required]
            public  string FedIDNum { get; set; }

            [ForeignKey("ContactId")]
            public  Contacts Contact { get; set; }
            
            public int ContactId { get; set; }
    }
}