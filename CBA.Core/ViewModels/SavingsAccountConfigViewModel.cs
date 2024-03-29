﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBA.Core.Implementation;

namespace CBA.Core.ViewModels
{
   public class SavingsAccountConfigViewModel
    {
        public int Id   { get; set; }
        public decimal CInterestRate { get; set; }
        public IEnumerable<GlAccount> InterestExpenseGls { get; set; }
        public IEnumerable<GlAccount> InterestPayableGls { get; set; }
        public GlAccount InterestExpenseGl { get; set; }
        public int InterestExpenseGlId { get; set; }
        public GlAccount InterestPayableGl { get; set; }
        public int InterestPayableGlId { get; set; }
        public decimal MinBalance { get; set; }
        public bool Status { get; set; }
        
        public SavingsAccountConfigViewModel( SavingsAccountConfig config)
        {
            if (config == null)
            {
                Id = 0;
            }
            else
            {
                Id = config.Id;
                CInterestRate = config.CInterestRate;
                InterestExpenseGlId = config.InterestExpenseGlId;
                InterestPayableGlId = (int)config.InterestPayableGlId;
                MinBalance = config.MinBalance;
                Status = config.Status;
                InterestExpenseGl = config.InterestExpenseGl;
            }
        }
    }
}
