using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SIFCore.Models
{
    public class Orders
    {
 public Orders()
        {
            PO = false;
            Hardcopy = false;
            //PaymentMethod = PaymentTypes.Check;
            Submitted = false;
        }

        // [Required]
        // public  Contacts Contact { get; set; }

        // [Required]
        // [DisplayName("Shipping Address")]
        // public  ShippingAddresses ShippingAddress { get; set; }

        // [Required]
        // [DisplayName("Billing Address")]
        // public  BillingAddresses BillingAddress { get; set; }

        public int Id { get; set; }

        [StringLength(500)]
        [DisplayName("Project Name")]
        [Required]
        public  string ProjectName { get; set; }

        public  bool PO { get; set; }

        [DisplayName("PO Number")]
        public  string PONumber { get; set; }

        
        // [DisplayName("Payment Method")]
        // public  PaymentTypes PaymentMethod { get; set; }

        public  bool Hardcopy { get; set; }

        [StringLength(5000)]
        [DataType(DataType.MultilineText)]
        [DisplayName("Order Notes")]
        public  string OrderComments { get; set; }

        [DisplayName("Electronic forms submitted?")]
        public  bool Submitted { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Submit Date")]
        public  DateTime  SubmitDate { get; set; }

        // public  IList<Analysis> Analyses { get; set; }


        public enum PaymentTypes
        {
            [DisplayName("Check (default)")]
            Check,

            [DisplayName("Bank/Wire Transfer (EFT)")]
            EFT,

            [DisplayName("Credit Card")]
            CreditCard,

            [DisplayName("Recharge/IOC - UC System Only")]
            IOC

        }
    }
}