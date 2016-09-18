
using Exceptionless;
using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace IdSrv.App
{
    class Program
    {
        static void Main(string[] args)
        {
            var currentDir =
                 new FileInfo(Uri.UnescapeDataString(new Uri(Assembly.GetExecutingAssembly().CodeBase).AbsolutePath));
            NLog.LogManager.Configuration = new NLog.Config.XmlLoggingConfiguration(currentDir.DirectoryName + "//" + "NLog.config", true);
            ExceptionlessClient.Default.Startup();
            //set logger
         
         var t=   IdentityServer3.Core.Logging.LogProvider.GetCurrentClassLogger();
            
            HostFactory.Run(x =>
            {
                x.Service<IdSrvService>(s =>
                {
                    s.ConstructUsing(name => new IdSrvService());
                    s.WhenStarted(svc => svc.Start());
                    s.WhenStopped(svc => svc.Stop());
                });
                x.RunAsLocalSystem();
                x.SetDescription("IdSrvService");
                x.SetDisplayName("IdSrvService");
                x.SetServiceName("IdSrvService");
            });
        }
    }
}
