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

        public ActionResult Index()
        {            
            return View();
        }

        public async Task<IActionResult> Details(int id)
        {
            var model = await _dbContext.Customers.Where(c => c.Id == id).FirstOrDefaultAsync();
            if(model == null)
            {
                ErrorMessage = "Customer not found";
                return RedirectToAction(nameof(Index)); 
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var model = await _dbContext.Customers.Where(c => c.Id == id).FirstOrDefaultAsync();
            if(model == null)
            {
                ErrorMessage = "Customer not found";
                return RedirectToAction(nameof(Index)); 
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Customers cust)
        {
            var customerToUpdate = await _dbContext.Customers.Where(c => c.Id == id).FirstOrDefaultAsync();
            if(customerToUpdate == null || cust.Id != customerToUpdate.Id)
            {
                ErrorMessage = "Customer not found";
                return RedirectToAction(nameof(Index)); 
            }

            customerToUpdate.Name = cust.Name;
            customerToUpdate.Address1 = cust.Address1;
            customerToUpdate.Address2 = cust.Address2;
            customerToUpdate.Type = cust.Type;
            customerToUpdate.KFSCustomerNumber = cust.KFSCustomerNumber;
            customerToUpdate.City = cust.City;
            customerToUpdate.FedIdNumber = cust.FedIdNumber;
            customerToUpdate.State = cust.State;
            customerToUpdate.Zip = cust.Zip;
            customerToUpdate.Country = cust.Country;
            customerToUpdate.Notes = cust.Notes;

             if(ModelState.IsValid){                 
                await _dbContext.SaveChangesAsync();
                Message = "Customer updated";
            } else {
                ErrorMessage = "Something went wrong"; 
                var model = await _dbContext.Customers.Where(c => c.Id == id).FirstOrDefaultAsync();
                return View(model);
            }           
            
            return RedirectToAction(nameof(Details), new {  id = id});
        }

        public ActionResult Create()
        {
            var model = new Customers();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Customers cust)
        {
            var custId = _dbContext.Customers.Max(c => c.SIFCustomerNumberInt) + 1;
            var customerToCreate = new Customers();

            customerToCreate.Name = cust.Name;
            customerToCreate.Address1 = cust.Address1;
            customerToCreate.Address2 = cust.Address2;
            customerToCreate.Type = cust.Type;
            customerToCreate.KFSCustomerNumber = cust.KFSCustomerNumber;
            customerToCreate.City = cust.City;
            customerToCreate.FedIdNumber = cust.FedIdNumber;
            customerToCreate.State = cust.State;
            customerToCreate.Zip = cust.Zip;
            customerToCreate.Country = cust.Country;
            customerToCreate.Notes = cust.Notes;
            customerToCreate.SIFCustomerNumberInt = custId;

             if(ModelState.IsValid){    
                 _dbContext.Add(customerToCreate);
                await _dbContext.SaveChangesAsync();
                Message = "Customer created";
            } else {
                ErrorMessage = "Something went wrong";                 
                return View(cust);
            }           
            
            return RedirectToAction(nameof(Details), new {  id = customerToCreate.Id});

        }

    }
}
