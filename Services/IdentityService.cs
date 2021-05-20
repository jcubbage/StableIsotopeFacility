using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using System.Text;
using SIFCore.Models;


namespace SIFCore.Services
{
    public interface IIdentityService
    {
       
        Task<Employees> GetByKerberos(string kerb);
       
    }

    public class IdentityService : IIdentityService
    {       
        private readonly SIFContext _context;
       


        public IdentityService(SIFContext context)
        {           
            _context = context;            
        }

        

        public async Task<Employees> GetByKerberos(string kerb)
        {
            var rtValue = await _context.Employees.Where(e => e.KerberosId == kerb && e.AllowAccess).FirstOrDefaultAsync();
            return rtValue;
        }

       
    }
}