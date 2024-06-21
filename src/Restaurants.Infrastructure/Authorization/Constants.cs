﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Infrastructure.Authorization
{
    public static class PolicyName
    {
        public const string HasNationality = "HasNationality";
        public const string AtLeast20 = "AtLeast20";
        public const string CreatedAtLeast2Restaurant = "CreatedAtLeast2Restaurant";
    }
    public static class AppClaimTypes
    {
        public const string Nationality = "Nationality";
        public const string DateOfBirth = "DateOfBirth";
    }
}
