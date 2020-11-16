using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SIFCore.Helpers;

namespace SIFCore.Models
{
     public class CreateOrdersViewModel
	{
		public Orders Order { get; set; }
       // public List<Analysis> Analysises { get; set; }
        // public List<AnalysisTypes> AnalysisTypes { get; set; }
        // public List<Requirements> Requirements { get; set; }
        public List<ShippingAddresses> ShippingAddresses { get; set; }
	    public List<BillingAddresses>   BillingAddresses { get; set; }
        public BillingAddresses Billing { get; set; }
        public ShippingAddresses Shipping { get; set; }

        public List<string> PaymentOptions { get; set; }       

	    public static  async Task<CreateOrdersViewModel> Create(SIFContext _dbContext, int orderId, string contactId)
		{			
            var viewModel = new CreateOrdersViewModel
            {
                Order = await _dbContext.Orders.Where(o => o.Id == orderId).FirstOrDefaultAsync(),
                // Analysises = await _dbContext.Analysis.Where(a => a.OrderId == orderId).ToListAsync(),
                // AnalysisTypes = await _dbContext.AnalysisTypes.OrderBy(at => at.AnalysisOrder).ToListAsync(),
                // Requirements = await _dbContext.Requirements.Where(r => r.CurrentAnalysis).OrderBy(r => r.AnalysisOrder).ToListAsync(),
                ShippingAddresses = await _dbContext.ShippingAddresses.Where(s => s.ContactId.ToString() == contactId).OrderBy(s => s.AddressName).ToListAsync(),
                BillingAddresses = await _dbContext.BillingAddresses.Where(b => b.ContactId.ToString() == contactId).OrderBy(b => b.AddressName).ToListAsync(),
                PaymentOptions = EnumHelper.GetListOfDisplayNames<PaymentTypes>(),
            };
 
			return viewModel;
		}
	}
}