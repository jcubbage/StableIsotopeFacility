using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SIFCore.Models
{
    public class Contacts
    {

        public int Id { get; set; }

        public  string FullName { get { return string.Format("{0} {1}", FirstName, LastName); } }

        public  string FullNameLastFirst { get { return string.Format("{0}, {1} ", LastName, FirstName); } }

        [Required]
        [StringLength(50)]
        [DisplayName("First Name")]
        public  string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("Last Name")]
        public  string LastName { get; set; }

        
        [StringLength(50)]
        [Required]
        public  string Login { get; set; }

        public  string EmailAddress
        {
            get
            {
                if (Login.Contains("@")) return Login;
                return Login + "@ucdavis.edu";
            }
        }

        [StringLength(50)]
        public  string Phone { get; set; }

        [StringLength(50)]
        public  string Fax { get; set; }

        //public  IList<ShippingAddresses> ShippingAddresseses { get; set; }
        public  IList<BillingAddresses> BillingAddresseses { get; set; }
        //public  IList<Role> Roles { get; set; }
    }
}