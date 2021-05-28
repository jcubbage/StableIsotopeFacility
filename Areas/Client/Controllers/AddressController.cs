using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SIFCore.Models;
using Microsoft.EntityFrameworkCore;

namespace SIFCore.Controllers.Client
{    
    public class AddressController : ClientController
    {
        private readonly SIFContext _dbContext;

        public AddressController(SIFContext dbContext)
        {            
            _dbContext = dbContext;
        }

        public async Task<IActionResult> Index()
        {
            var contacts = await _dbContext.Contacts.Where(c => c.Id == int.Parse(User.FindFirstValue("contactId")))
                .Include(c => c.BillingAddresses)
                .Include(c => c.ShippingAddresses)
                .FirstAsync();
            return View(contacts);
        }

        public async Task<IActionResult> BillingDetails(int id)
        {
            var address = await _dbContext.BillingAddresses.Where(a => a.Id == id && a.Contact.Id == int.Parse(User.FindFirstValue("contactId"))).FirstOrDefaultAsync();
            if(address == null)
            {
                ErrorMessage = "Address not found or you do not have access to it";
                return RedirectToAction(nameof(Index)); 
            }
            return View(address);
        }

        public IActionResult BillingCreate()
        {
            var address = new BillingAddresses(); 
            address.Country = "U.S.A.";          
            return View(address);
        }

        [HttpPost]
        public async Task<IActionResult> BillingCreate(BillingAddresses address)
        {
            var contact = await _dbContext.Contacts.Where(c => c.Id == int.Parse(User.FindFirstValue("contactId"))).FirstAsync();
            var addressToAdd = new BillingAddresses();            
            addressToAdd.AddressName = address.AddressName;
            addressToAdd.Institution = address.Institution;
            addressToAdd.Department = address.Department;
            addressToAdd.ContactFirstName = address.ContactFirstName;
            addressToAdd.ContactLastName = address.ContactLastName;
            addressToAdd.BillingEmailAddress = address.BillingEmailAddress;
            addressToAdd.BillingFax = address.BillingFax;
            addressToAdd.BillingPhone = address.BillingPhone;
            addressToAdd.Address1 = address.Address1;
            addressToAdd.Address2 = address.Address2;
            addressToAdd.Address3 = address.Address3;
            addressToAdd.City = address.City;
            addressToAdd.State = address.State;
            addressToAdd.ZipCode = address.ZipCode;
            addressToAdd.Country = address.Country;
            addressToAdd.FedIDNum = address.FedIDNum;
            addressToAdd.Contact = contact;

             if(ModelState.IsValid){
                 _dbContext.Add(addressToAdd);
                await _dbContext.SaveChangesAsync();
                Message = "Billing Address Added";
            } else {
                ErrorMessage = "Something went wrong";         
                return View(address);
            }

            return RedirectToAction(nameof(BillingDetails), new { id = addressToAdd.Id}); 
        }

         public async Task<IActionResult> BillingEdit(int id)
        {
            var address = await _dbContext.BillingAddresses.Where(a => a.Id == id && a.Contact.Id == int.Parse(User.FindFirstValue("contactId"))).FirstOrDefaultAsync();
            if(address == null)
            {
                ErrorMessage = "Address not found or you do not have access to it";
                return RedirectToAction(nameof(Index)); 
            }
            return View(address);
        }

        [HttpPost]
        public async Task<IActionResult> BillingEdit(int id, BillingAddresses address)
        {
            var addressToUpdate = await _dbContext.BillingAddresses.Where(a => a.Id == id && a.Contact.Id == int.Parse(User.FindFirstValue("contactId"))).FirstOrDefaultAsync();
            if(addressToUpdate == null)
            {
                ErrorMessage = "Address not found or you do not have access to it";
                return RedirectToAction(nameof(Index)); 
            }
            addressToUpdate.AddressName = address.AddressName;
            addressToUpdate.Institution = address.Institution;
            addressToUpdate.Department = address.Department;
            addressToUpdate.ContactFirstName = address.ContactFirstName;
            addressToUpdate.ContactLastName = address.ContactLastName;
            addressToUpdate.BillingEmailAddress = address.BillingEmailAddress;
            addressToUpdate.BillingFax = address.BillingFax;
            addressToUpdate.BillingPhone = address.BillingPhone;
            addressToUpdate.Address1 = address.Address1;
            addressToUpdate.Address2 = address.Address2;
            addressToUpdate.Address3 = address.Address3;
            addressToUpdate.City = address.City;
            addressToUpdate.State = address.State;
            addressToUpdate.ZipCode = address.ZipCode;
            addressToUpdate.Country = address.Country;
            addressToUpdate.FedIDNum = address.FedIDNum;

             if(ModelState.IsValid){
                await _dbContext.SaveChangesAsync();
                Message = "Billing Address updated";
            } else {
                ErrorMessage = "Something went wrong";         
                return View(address);
            }

            return RedirectToAction(nameof(BillingDetails), new { id = id }); 
        }


        public async Task<IActionResult> ShippingDetails(int id)
        {
            var address = await _dbContext.ShippingAddresses.Where(a => a.Id == id && a.Contact.Id == int.Parse(User.FindFirstValue("contactId"))).FirstOrDefaultAsync();
            if(address == null)
            {
                ErrorMessage = "Address not found or you do not have access to it";
                return RedirectToAction(nameof(Index)); 
            }
            return View(address);
        }
        

        public async Task<IActionResult> ShippingEdit(int id)
        {
            var address = await _dbContext.ShippingAddresses.Where(a => a.Id == id && a.Contact.Id == int.Parse(User.FindFirstValue("contactId"))).FirstOrDefaultAsync();
            if(address == null)
            {
                ErrorMessage = "Address not found or you do not have access to it";
                return RedirectToAction(nameof(Index)); 
            }
            return View(address);
        }

        [HttpPost]
        public async Task<IActionResult> ShippingEdit(int id, ShippingAddresses address)
        {
            var addressToUpdate = await _dbContext.ShippingAddresses.Where(a => a.Id == id && a.Contact.Id == int.Parse(User.FindFirstValue("contactId"))).FirstOrDefaultAsync();
            if(addressToUpdate == null)
            {
                ErrorMessage = "Address not found or you do not have access to it";
                return RedirectToAction(nameof(Index)); 
            }
            addressToUpdate.AddressName = address.AddressName;
            addressToUpdate.Institution = address.Institution;
            addressToUpdate.Department = address.Department;
            addressToUpdate.PIFirstName = address.PIFirstName;
            addressToUpdate.PILastName = address.PILastName;
            addressToUpdate.PIEmail = address.PIEmail;
            addressToUpdate.PIFax = address.PIFax;
            addressToUpdate.PIPhone = address.PIPhone;
            addressToUpdate.Address1 = address.Address1;
            addressToUpdate.Address2 = address.Address2;
            addressToUpdate.Address3 = address.Address3;
            addressToUpdate.City = address.City;
            addressToUpdate.State = address.State;
            addressToUpdate.ZipCode = address.ZipCode;
            addressToUpdate.Country = address.Country;
            addressToUpdate.ResearcherFirstName = address.ResearcherFirstName;
            addressToUpdate.ResearcherLastName = address.ResearcherLastName;
            addressToUpdate.ResearcherEmail = address.ResearcherEmail;
            addressToUpdate.ResearcherPhone = address.ResearcherPhone;
            addressToUpdate.Notes = address.Notes;

             if(ModelState.IsValid){
                await _dbContext.SaveChangesAsync();
                Message = "Shipping Address updated";
            } else {
                ErrorMessage = "Something went wrong";         
                return View(address);
            }

            return RedirectToAction(nameof(ShippingDetails), new { id = id }); 
        }
        
        public IActionResult ShippingCreate()
        {
            var address = new ShippingAddresses(); 
            address.Country = "U.S.A.";          
            return View(address);
        }

        [HttpPost]
        public async Task<IActionResult> ShippingCreate(ShippingAddresses address)
        {
            var contact = await _dbContext.Contacts.Where(c => c.Id == int.Parse(User.FindFirstValue("contactId"))).FirstAsync();
            var addressToAdd = new ShippingAddresses();            
            addressToAdd.AddressName = address.AddressName;
            addressToAdd.Institution = address.Institution;
            addressToAdd.Department = address.Department;
            addressToAdd.PIFirstName = address.PIFirstName;
            addressToAdd.PILastName = address.PILastName;
            addressToAdd.PIEmail = address.PIEmail;
            addressToAdd.PIFax = address.PIFax;
            addressToAdd.PIPhone = address.PIPhone;
            addressToAdd.Address1 = address.Address1;
            addressToAdd.Address2 = address.Address2;
            addressToAdd.Address3 = address.Address3;
            addressToAdd.City = address.City;
            addressToAdd.State = address.State;
            addressToAdd.ZipCode = address.ZipCode;
            addressToAdd.Country = address.Country;
            addressToAdd.Contact = contact;
            addressToAdd.ResearcherFirstName = address.ResearcherFirstName;
            addressToAdd.ResearcherLastName = address.ResearcherLastName;
            addressToAdd.ResearcherEmail = address.ResearcherEmail;
            addressToAdd.ResearcherPhone = address.ResearcherPhone;
            addressToAdd.Notes = address.Notes;

             if(ModelState.IsValid){
                 _dbContext.Add(addressToAdd);
                await _dbContext.SaveChangesAsync();
                Message = "Shipping Address Added";
            } else {
                ErrorMessage = "Something went wrong";         
                return View(address);
            }

            return RedirectToAction(nameof(ShippingDetails), new { id = addressToAdd.Id}); 
        }

    }

}