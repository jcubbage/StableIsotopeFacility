using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SIFCore.Models;
using Microsoft.EntityFrameworkCore;

namespace SIFCore.Controllers
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
                .Include(c => c.BillingAddresseses)
                .Include(c => c.ShippingAddresseses)
                .FirstAsync();
            return View(contacts);
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
            if(address == null)
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
            addressToUpdate.PIEmailAdditional = address.PIEmailAdditional;
            addressToUpdate.PIFax = address.PIFax;
            addressToUpdate.PIPhone = address.PIPhone;
            addressToUpdate.Address1 = address.Address1;
            addressToUpdate.Address2 = address.Address2;
            addressToUpdate.Address3 = address.Address3;
            addressToUpdate.City = address.City;
            addressToUpdate.State = address.State;
            addressToUpdate.ZipCode = address.ZipCode;
            addressToUpdate.Country = address.Country;

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

    }

}