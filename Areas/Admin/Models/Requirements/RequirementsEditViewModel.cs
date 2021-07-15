using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace SIFCore.Models
{
    public class AdminRequirementEditViewModel
    {
        public Requirements  requirement { get; set; }

        public List<AnalysisTypes> analysisTypes { get; set; }     



        public static async Task<AdminRequirementEditViewModel> Create(SIFContext _dbContext, int id)
        {  

            var viewModel = new AdminRequirementEditViewModel
            {
                requirement = await _dbContext.Requirements.Where(r => r.Id == id).FirstOrDefaultAsync(),
                analysisTypes = await _dbContext.AnalysisTypes.ToListAsync(),
            };

            return viewModel;
        }

       
    }

   
}