using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

       

        public int Id { get; set; }

        [ForeignKey("ContactId")]
        public  Contacts Contact { get; set; }

        [Required]
        public int ContactId { get; set; }

       
        [ForeignKey("ShippingAddress")]
        public  ShippingAddresses OrderShippingAddress { get; set; }

        [Required]
        [DisplayName("Shipping Address")]
        public int ShippingAddress { get; set; }

        [ForeignKey("BillingAddress")]
        public  BillingAddresses OrderBillingAddress { get; set; }

        [Required]
        [DisplayName("Billing Address")]
        public  int BillingAddress { get; set; }

        

        [StringLength(500)]
        [DisplayName("Project Name")]
        [Required]
        public  string ProjectName { get; set; }

        public  bool PO { get; set; }

        [DisplayName("PO Number")]
        public  string PONumber { get; set; }

        
        [DisplayName("Payment Method")]
        public  string PaymentMethod { get; set; }

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