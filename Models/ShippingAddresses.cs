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
        [DisplayName("Address Name")]
        [Required]
        public  string AddressName { get; set; }

       public  string Institution { get; set; }

        public  string Department { get; set; }

        [StringLength(50)]
        [Required]
        [DisplayName("PI First Name")]
        public  String PIFirstName { get; set; }

        [StringLength(50)]
        [DisplayName("PI Last Name")]
        [Required]
        public  string PILastName   { get; set; }

        [Display(Name= "PI Email Address")]
        [Required]
        [DataType(DataType.EmailAddress)]
        public  string PIEmail { get; set; }

        [DisplayName("PI Additional Email Address")]
        [DataType(DataType.EmailAddress)]
        public  string PIEmailAdditional { get; set; }

        [DataType(DataType.PhoneNumber)]
        [DisplayName("PI Fax")]
        public  string PIFax { get; set; }

        [DataType(DataType.PhoneNumber)]
        [DisplayName("PI Phone")]
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
        [DisplayName("State/Governing District")]
        public  string State { get; set; }

        [StringLength(50)]
        [DisplayName("Zip/Postal Code")]
        public  string ZipCode { get; set; }

        [StringLength(50)]
        [Required]
        public  string Country { get; set; }
        
        [ForeignKey("ContactId")]
        public  Contacts Contact { get; set; }
        
        public int ContactId { get; set; }

    }
}