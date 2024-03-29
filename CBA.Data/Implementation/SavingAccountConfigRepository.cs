﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBA.Core.Implementation;
using System.Data.Entity;

namespace CBA.Data.Implementation
{
   public class SavingAccountConfigRepository:Repository<SavingsAccountConfig>
   {
       private readonly ApplicationDbContext _context;
        public SavingAccountConfigRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
       
        public SavingsAccountConfig GetAllSavings()
        {
            return  _context.SavingsAccountConfigs.Include(c=>c.InterestExpenseGl).Include(c=>c.InterestPayableGl).FirstOrDefault();
           

        }
           
    }
}
