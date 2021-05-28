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

       public IActionResult New()
       {
           var model = new Contacts();
           return View(model);
       }

       [HttpPost]
       public async Task<IActionResult> New(Contacts newContact)
       {
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

    }
}
