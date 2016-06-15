using IdentityServer3.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityServer3.Core.Models;
using IdentityAdmin.Logging;
using Newtonsoft.Json;

namespace IdSrv.Lib
{
    public class SwtichUserService : IUserService
    {
        private readonly static ILog Logger = LogProvider.GetCurrentClassLogger();
        public Task AuthenticateExternalAsync(ExternalAuthenticationContext context)
        {
            Logger.Debug("AuthenticateExternalAsync:"+JsonConvert.SerializeObject(context,new JsonSerializerSettings() {  ReferenceLoopHandling=ReferenceLoopHandling.Ignore}));
            if (context.SignInMessage!=null&&!string.IsNullOrEmpty(context.SignInMessage.ClientId))
            {
                var us = UserServiceFinder.Get(context.SignInMessage.ClientId);
                if (us!=null)
                {
                    return us.AuthenticateExternalAsync(context);
                }
            }
            return Task.FromResult(0);
        }

        public Task AuthenticateLocalAsync(LocalAuthenticationContext context)
        {
            Logger.Debug("AuthenticateLocalAsync:" + JsonConvert.SerializeObject(context, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));

            if (context.SignInMessage != null && !string.IsNullOrEmpty(context.SignInMessage.ClientId))
            {
                var us = UserServiceFinder.Get(context.SignInMessage.ClientId);
                if (us != null)
                {
                    return us.AuthenticateLocalAsync(context);
                }
            }
            return Task.FromResult(0);
        }

        public Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            Logger.Debug("GetProfileDataAsync:" + JsonConvert.SerializeObject(context, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));
            if (context.Client != null && !string.IsNullOrEmpty(context.Client.ClientId))
            {
                var us = UserServiceFinder.Get(context.Client.ClientId);
                if (us != null)
                {
                    return us.GetProfileDataAsync(context);
                }
            }
            return Task.FromResult(0);
        }

        public Task IsActiveAsync(IsActiveContext context)
        {
            Logger.Debug("IsActiveAsync:" + JsonConvert.SerializeObject(context, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));
            if (context.Client != null && !string.IsNullOrEmpty(context.Client.ClientId))
            {
                var us = UserServiceFinder.Get(context.Client.ClientId);
                if (us != null)
                {
                    return us.IsActiveAsync(context);
                }
            }
            return Task.FromResult(0);
        }

        public Task PostAuthenticateAsync(PostAuthenticationContext context)
        {
            Logger.Debug("PostAuthenticateAsync:" + JsonConvert.SerializeObject(context, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));
            if (context.SignInMessage != null && !string.IsNullOrEmpty(context.SignInMessage.ClientId))
            {
                var us = UserServiceFinder.Get(context.SignInMessage.ClientId);
                if (us != null)
                {
                    return us.PostAuthenticateAsync(context);
                }
            }
            return Task.FromResult(0);
        }

        public Task PreAuthenticateAsync(PreAuthenticationContext context)
        {
            Logger.Debug("PreAuthenticateAsync:" + JsonConvert.SerializeObject(context, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));
            if (context.SignInMessage != null && !string.IsNullOrEmpty(context.SignInMessage.ClientId))
            {
                var us = UserServiceFinder.Get(context.SignInMessage.ClientId);
                if (us != null)
                {
                    return us.PreAuthenticateAsync(context);
                }
            }
            return Task.FromResult(0);
        }

        public Task SignOutAsync(SignOutContext context)
        {
            Logger.Debug("SignOutAsync:" + JsonConvert.SerializeObject(context, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));
            if (!string.IsNullOrEmpty(context.ClientId))
            {
                var us = UserServiceFinder.Get(context.ClientId);
                if (us != null)
                {
                    return us.SignOutAsync(context);
                }
            }
            return Task.FromResult(0);
        }
    }
}
