using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SIFCore.Models
{
    public class AnalysisTypes
    {

        public int Id { get; set; }

       public  string CategoryName { get; set; }

        public  int AnalysisOrder { get; set; }

        //public virtual IList<Requirement> Requirements { get; set; }
    }
}