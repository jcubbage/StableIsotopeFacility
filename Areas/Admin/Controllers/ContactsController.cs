using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIFCore.Models;

namespace SIFCore.Controllers.Admin
{
    public class ContactsController : AdminController
    {

         private readonly SIFContext _dbContext;

        public ContactsController(SIFContext dbContext)
        {            
            _dbContext = dbContext;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _dbContext.Contacts
                .Include(c => c.ShippingAddresses)
                .Include(c => c.BillingAddresses)
                .ToListAsync();
            return View(model);
        }

       public IActionResult New()
       {
           var model = new Contacts();
           return View(model);
       }

       [HttpPost]
       public async Task<IActionResult> New(Contacts newContact)
       {
           var check = await _dbContext.Contacts.Where(c => c.Email == newContact.Email).AnyAsync();
           if(check)
           {
               ErrorMessage = "Contact with that email already exists!";
               return View(newContact);
           }
           var contactToCreate = new Contacts();
           contactToCreate.FirstName = newContact.FirstName;
           contactToCreate.LastName = newContact.LastName;
           contactToCreate.Phone = newContact.Phone;
           contactToCreate.Email = newContact.Email;

           if(ModelState.IsValid){
               _dbContext.Add(contactToCreate);
                await _dbContext.SaveChangesAsync();
                Message = "Contact Created";
            } else {
                ErrorMessage = "Something went wrong.";                
                return View(newContact); 
            }

            return RedirectToAction("Create", nameof(Orders), new { id = contactToCreate.Id });  
       }

       public async Task<IActionResult> Edit(int id)
       {
           var model = await _dbContext.Contacts.Where(c => c.Id == id).FirstOrDefaultAsync();
           if(model == null)
           {
               ErrorMessage = "Contact not found";
               return RedirectToAction(nameof(Index));
           }
           return View(model);
       }

       [HttpPost]
       public async Task<IActionResult> Edit(int id, Contacts updatedContact)
       {           
           var contactToUpdate = await _dbContext.Contacts.Where(c => c.Id == id).FirstOrDefaultAsync();
           if(contactToUpdate == null || contactToUpdate.Id != updatedContact.Id)
           {
               ErrorMessage = "Contact not found";
               return RedirectToAction(nameof(Index));
           }
           contactToUpdate.FirstName = updatedContact.FirstName;
           contactToUpdate.LastName = updatedContact.LastName;
           contactToUpdate.Phone = updatedContact.Phone;
           contactToUpdate.Email = updatedContact.Email;

           if(ModelState.IsValid){              
                await _dbContext.SaveChangesAsync();
                Message = "Contact Updated";
            } else {
                ErrorMessage = "Something went wrong.";                
                return View(updatedContact); 
            }

            return RedirectToAction(nameof(Index));  
       }

    }
}
