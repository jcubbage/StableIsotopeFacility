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
    public class CustomersController : AdminController
    {

         private readonly SIFContext _dbContext;

        public CustomersController(SIFContext dbContext)
        {            
            _dbContext = dbContext;
        }

        public async Task<IActionResult> LookupCustomer (string lookup)
        {
            lookup = lookup.Trim();
            var cust = new List<Customers>();
            int id = 0;
            var term = "%" + lookup + "%";
            if (Int32.TryParse(lookup, out id))
            {
                cust = await _dbContext.Customers.Where(c => c.SIFCustomerNumberInt == id || EF.Functions.Like(c.Name, term) || EF.Functions.Like(c.Address1, term) || EF.Functions.Like(c.City, term)).ToListAsync();
            } else
            {                 
                cust = await _dbContext.Customers.Where(c => EF.Functions.Like(c.Name, term) || EF.Functions.Like(c.Address1, term) || EF.Functions.Like(c.City, term)).ToListAsync(); 
            }

            return PartialView("_LookupCustomer", cust);
        }

    }
}
