﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModels
{
    public class CookingRecipe
    {
        public int recipe_Id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string instructions { get; set; }
    }
}
