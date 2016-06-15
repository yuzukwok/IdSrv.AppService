using IdentityServer3.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdSrv.Lib.Db
{
    public class MyScopeConfigurationDbContext : ScopeConfigurationDbContext
    {
        public MyScopeConfigurationDbContext() : base("Default")
        {

        }
        public MyScopeConfigurationDbContext(EntityFrameworkServiceOptions entityFrameworkConfig) :
            base(entityFrameworkConfig.ConnectionString, entityFrameworkConfig.Schema)
        {

        }

        public MyScopeConfigurationDbContext(string connectionString, string schema) :
            base(connectionString, schema)
        {

        }
    }
}
