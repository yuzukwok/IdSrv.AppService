using IdentityAdmin.Configuration;
using IdentityAdmin.Core;

using IdentityServer3.Core.Configuration;
using IdentityServer3.Core.Logging;
using IdentityServer3.Core.Services;
using IdentityServer3.EntityFramework;
using Owin;

namespace IdSrv.Lib
{
    public static  class BootStartup
    {
        private static readonly ILog Logger = LogProvider.GetCurrentClassLogger();
        public static IdentityServerOptions Init(this IdentityServerOptions option,string signfilename,string pwd, IdentityServerServiceFactory factory,string sitename="IdSrv")
        {
            option.SigningCertificate = CertificateHelper.Load(signfilename, pwd);
            option.Factory = factory;
            option.RequireSsl = false;
            option.SiteName = sitename;
            option.EnableWelcomePage = false;

            return option;
        }

        public static void UseEfStore(this IdentityServerServiceFactory factory,string dbcontectionstr,bool usetokenclean=true)
        {
            var efConfig = new EntityFrameworkServiceOptions
            {
                ConnectionString = dbcontectionstr,                
            };

            if (usetokenclean)
            {
                var cleanup = new TokenCleanup(efConfig, 60 * 10);
                cleanup.Start();
            }
            factory.RegisterConfigurationServices(efConfig);
            factory.RegisterOperationalServices(efConfig);
            
        }


        public static void UseIdSrvAdmin(this IAppBuilder app,bool usehttps=false)
        {
          
            app.Map("/admin", adminApp =>
            {
                var factory = new IdentityAdminServiceFactory
                {
                    IdentityAdminService = new IdentityAdmin.Configuration.Registration<IIdentityAdminService, IdentityAdminManagerService>()
               
                };
                var adminoption = new IdentityAdminOptions()
                {
                    Factory = factory,
                };
                adminoption.AdminSecurityConfiguration.RequireSsl = usehttps;
                adminApp.UseIdentityAdmin(adminoption);
            });
        }


        public static void ScanUserServiceDLL(this IdentityServerServiceFactory factory)
        {
            IUserService userservice =new  SwtichUserService();          

            factory.UserService = new IdentityServer3.Core.Configuration.Registration<IUserService>(userservice);
            
            factory.ClaimsProvider = new IdentityServer3.Core.Configuration.Registration<IClaimsProvider>(new AuthClaimsProvider(userservice));

            //scan
            new UserServiceFinder().Scan();
        }

        public static IdentityServerOptions UseLog(this IdentityServerOptions option,bool uselog) {
            Logger.Debug("开启日志" + uselog);
            LoggingOptions opt = new LoggingOptions()
            {
                EnableHttpLogging = uselog,
                EnableKatanaLogging = uselog,
                EnableWebApiDiagnostics = uselog,
                WebApiDiagnosticsIsVerbose = uselog,

            };
            option.LoggingOptions = opt;
            return option;
        }
    }
}
