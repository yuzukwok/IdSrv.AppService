using IdSrv.Lib.Setting;
using Its.Configuration;
using Microsoft.Owin.Hosting;
using NLog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdSrv.App
{
    public class IdSrvService
    {
        private static ILogger _logger = LogManager.GetLogger("IdSrvService");
        public IdSrvService()
        {

        }
       
        private IDisposable _server = null;

        public void Start()
        {
            _logger.Debug("服务启动");
            var setting = Settings.Get<IdSrvSetting>();
            string baseAddress = "http://*:" + setting.Port.ToString() + "/";
       
            _server = WebApp.Start<Startup>(url: baseAddress);


        }

        public void Stop()
        {
            _logger.Debug("停止启动");

            if (_server != null)
            {
                _server.Dispose();
            }
        }
    }
}
