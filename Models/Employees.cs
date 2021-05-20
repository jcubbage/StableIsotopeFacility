using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIFCore.Models
{
    public class Employees
    {      

        public int Id { get; set; }

        public  string FullName { get { return $"{FirstName} {LastName}";} }

        public  string FullNameLastFirst { get { return $"{LastName}, {FirstName}"; } }        

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
        public  string KerberosId { get; set; }  

        public string Email { get; set; } 

        public bool AllowAccess { get; set; }     

        
    }
}