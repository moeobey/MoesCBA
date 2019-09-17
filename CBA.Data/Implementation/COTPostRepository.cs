﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBA.Core.Implementation;

namespace CBA.Data.Implementation
{
    public class COTPostRepository:Repository<COTPost>
    {
        public COTPostRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
