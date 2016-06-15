using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdSrv.Lib.Setting
{
    public class IdSrvSetting
    {
        public int Port { get; set; }
        public string CertFileName { get; set; }
        public string CertFileKeyPwd { get; set; }
        public bool UseLog { get; set; }

        public string EfConnectionString { get; set; }
        public IList<ClientIdUserServiceMapping> UserServiceMappings { get; set; }
    }

    public class ClientIdUserServiceMapping
    {
        public string ClientId { get; set; }
        public string UserServiceClassFullName { get; set; }
        public string Extparam { get; set; }
    }
}
