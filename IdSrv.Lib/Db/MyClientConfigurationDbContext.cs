using IdentityServer3.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdSrv.Lib.Db
{
    public class MyClientConfigurationDbContext : ClientConfigurationDbContext
    {
        public MyClientConfigurationDbContext():base("Default")
        {

        }

        public MyClientConfigurationDbContext(EntityFrameworkServiceOptions entityFrameworkConfig) :
                base(entityFrameworkConfig.ConnectionString, entityFrameworkConfig.Schema)
        {

        }
        public MyClientConfigurationDbContext(string connectionString, string schema) :
            base(connectionString, schema)
        {

        }
    }
}
