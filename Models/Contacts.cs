using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIFCore.Models
{
    public class Contacts
    {      

        public int Id { get; set; }

        public  string FullName { get { return string.Format("{0} {1}", FirstName, LastName); } }

        public  string FullNameLastFirst { get { return string.Format("{0}, {1} ", LastName, FirstName); } }

        [Required]
        [StringLength(50)]
        [Display(Name="First Name")]
        public  string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name="Last Name")]
        public  string LastName { get; set; }

        
        [StringLength(50)]
        [Required]
        public  string Email { get; set; }        

        [StringLength(50)]
        public  string Phone { get; set; }

        public Byte[] Password { get; set; }
        
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Byte[] Salt { get; set; }

        [StringLength(50)]
        public  string Fax { get; set; }

        public  IList<ShippingAddresses> ShippingAddresses { get; set; }
        public  IList<BillingAddresses> BillingAddresses { get; set; }
        //public  IList<Role> Roles { get; set; }
    }
}