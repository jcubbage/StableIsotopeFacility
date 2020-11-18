using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SIFCore.Helpers;

namespace SIFCore.Models
{
    public class Orders
    {
 public Orders()
        {
            PO = false;
            Hardcopy = false;
            PaymentMethod = PaymentTypes.Check.GetDisplayName();
            Submitted = false;
            SubmitDate =  DateTime.Parse("1/1/01");
        }

       

        public int Id { get; set; }

        [ForeignKey("ContactId")]
        public  Contacts Contact { get; set; }

        [Required]
        public int ContactId { get; set; }

       
        [ForeignKey("ShippingAddress")]
        public  ShippingAddresses OrderShippingAddress { get; set; }

        [Required]
        [Display(Name="Shipping Address")]
        public int ShippingAddress { get; set; }

        [ForeignKey("BillingAddress")]
        public  BillingAddresses OrderBillingAddress { get; set; }

        [Required]
        [Display(Name="Billing Address")]
        public  int BillingAddress { get; set; }

        

        [StringLength(500)]
        [Display(Name="Project Name")]
        [Required]
        public  string ProjectName { get; set; }

        public  bool PO { get; set; }

        [Display(Name="PO Number")]
        public  string PONumber { get; set; }

        
        [Display(Name="Payment Method")]
        public  string PaymentMethod { get; set; }

        public  bool Hardcopy { get; set; }

        [StringLength(5000)]
        [DataType(DataType.MultilineText)]
        [Display(Name="Order Notes")]
        public  string OrderComments { get; set; }

        [Display(Name="Electronic forms submitted?")]
        public  bool Submitted { get; set; }

        [DataType(DataType.Date)]
        [Display(Name="Submit Date")]
        public  DateTime  SubmitDate { get; set; }

        

        public  IList<Analysis> Analyses { get; set; }


       
    } 
    
    public enum PaymentTypes
    {
        [Display(Name="Check")]
        Check,

        [Display(Name="Bank/Wire Transfer (EFT)")]
        EFT,

        [Display(Name="Credit Card")]
        CreditCard,

        [Display(Name="Recharge/IOC - UC System Only")]
        IOC

    }
}