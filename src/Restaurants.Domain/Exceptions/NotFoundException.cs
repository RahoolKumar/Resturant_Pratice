using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Domain.Exceptions
{
    public class NotFoundException(string resourceType,string resourceIdentity):
        Exception($"{resourceType} with id: {resourceIdentity} doesn't exist" )
    {
        
    }
}
