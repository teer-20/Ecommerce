using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Helper
{
    public class SqlQueries
    {

        static IConfiguration queriesConfig = new ConfigurationBuilder()
            .AddXmlFile("SqlQueries.xml", true, true)
            .Build();

        public static string AdminRegister { get { return queriesConfig["AdminRegister"]; } }
        public static string AdminLogin { get { return queriesConfig["AdminLogin"]; } }
       // public static string GetAdmin { get { return queriesConfig["GetAdmin"]; } }
       // public static string GetUser { get { return queriesConfig["GetUser"]; } }

    }
}
