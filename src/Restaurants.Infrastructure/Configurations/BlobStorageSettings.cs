using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Infrastructure.Configurations
{
    public class BlobStorageSettings
    {
        public string ConnectionString { get; set; } = default!;
        public string LogosContainerName { get; set; } = default!;
    }
}
