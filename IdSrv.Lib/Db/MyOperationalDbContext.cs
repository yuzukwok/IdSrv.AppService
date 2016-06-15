using IdentityServer3.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdSrv.Lib.Db
{
    public class MyOperationalDbContext : OperationalDbContext
    {
        public MyOperationalDbContext() : base("Default")
        {

        }
        public MyOperationalDbContext(EntityFrameworkServiceOptions entityFrameworkConfig) :
            base(entityFrameworkConfig.ConnectionString, entityFrameworkConfig.Schema)
        {

        }

        public MyOperationalDbContext(string connectionString, string schema) :
            base(connectionString, schema)
        {

        }
    }
}
