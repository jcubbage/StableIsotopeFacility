using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace SIFCore.Models
{
    public class AdminAddAncillaryViewModel
    {
        public Orders  order { get; set; }

        public List<Ancillary> ancillaries { get; set; }  

        public Charges  newCharge {get ; set;}   


        public static async Task<AdminAddAncillaryViewModel> Create(SIFContext _dbContext, int id)
        {
            var viewModel = new AdminAddAncillaryViewModel
            {
                order = await _dbContext.Orders.Where(o => o.Id == id).FirstOrDefaultAsync(),
                ancillaries = await _dbContext.Ancillary.OrderBy(a => a.Description).ToListAsync(),
                newCharge = new Charges(),
            };

            return viewModel;
        }

        public static async Task<AdminAddAncillaryViewModel> CreatePayment(SIFContext _dbContext, int id)
        {
            var viewModel = new AdminAddAncillaryViewModel
            {
                order = await _dbContext.Orders.Where(o => o.Id == id).FirstOrDefaultAsync(),
                newCharge = new Charges(),
            };

            return viewModel;
        }

       
    }

   
}
