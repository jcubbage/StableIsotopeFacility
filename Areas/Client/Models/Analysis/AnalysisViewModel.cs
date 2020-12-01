using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SIFCore.Helpers;

namespace SIFCore.Models
{
     public class AnalysisViewModel
	{

        public Analysis Analysis { get; set; }
        public Requirements Requirement { get; set; }
        public Orders Order { get; set; }

		
	    public static AnalysisViewModel Create()
		{			
            var viewModel = new AnalysisViewModel
            {
               Analysis = new Analysis(),
            };
 
			return viewModel;
		}

        
	}
}