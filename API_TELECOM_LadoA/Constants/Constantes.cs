using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;

namespace API_TELECOM_LadoA.Constants
{
    public class Constantes
    {

        public static string connectionString()
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json");

            IConfigurationRoot configuration = builder.Build();
            string server = configuration["databaseName"];
            return Environment.GetEnvironmentVariable(server);
        }
    }
}
