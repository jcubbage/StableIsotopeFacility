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
            var term = "%" + lookup + "%";
            var cust = _dbContext.Customers.Where(c => EF.Functions.Like(c.Name, term) || EF.Functions.Like(c.Address1, term)).AsQueryable();           
            
            int id = 0;
            // Parsing was successful (we have an ID number instead of a name)
            if (Int32.TryParse(lookup, out id))
            {
                cust = cust.Where(c => c.SIFCustomerNumberInt == id);
            }
            var results = await cust.ToListAsync();
                               
            return PartialView("_LookupCustomer", results);

        }

    }
}
