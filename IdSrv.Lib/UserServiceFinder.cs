using IdentityServer3.Core.Services;
using IdSrv.Lib.Setting;
using Its.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdSrv.Lib
{
  public  class UserServiceFinder
    {
        public UserServiceFinder()
        {

        }
       static IDictionary<string, IUserService> Dict { get; set; }
        public void Scan()
        {
            Dict = new Dictionary<string, IUserService>();
            var setting = Settings.Get<IdSrvSetting>();

            foreach (var item in setting.UserServiceMappings)
            {
              
                var type = new TypeFinder().Find(p => p.GetInterface("IUserService") != null&&p.FullName==item.UserServiceClassFullName).FirstOrDefault();
                if (string.IsNullOrEmpty(item.Extparam))
                {
                    var userservice = (IUserService)(type.Assembly.CreateInstance(item.UserServiceClassFullName));
                    Dict.Add(item.ClientId, userservice);
                }
                else
                {
                    var userservice = (IUserService)(type.Assembly.CreateInstance(item.UserServiceClassFullName,true,System.Reflection.BindingFlags.CreateInstance,null,new[] { item.Extparam },null,null));
                    Dict.Add(item.ClientId, userservice);
                }
              
               
                
            }

        }

        public static IUserService Get(string name)
        {
            return Dict[name];
        }
    }
}
