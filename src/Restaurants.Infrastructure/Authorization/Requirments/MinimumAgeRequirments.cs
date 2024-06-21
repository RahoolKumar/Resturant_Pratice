using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Infrastructure.Authorization.Requirments
{
    public class MinimumAgeRequirments(int minimumAge):IAuthorizationRequirement
    {
        public int MinimumAge { get; } = minimumAge;
    }
}
