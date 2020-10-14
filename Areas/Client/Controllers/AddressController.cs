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
    }

}