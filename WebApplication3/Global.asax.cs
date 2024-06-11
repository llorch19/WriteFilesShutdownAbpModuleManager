using Abp.Castle.Logging.Log4Net;
using Abp.Web;
using Castle.Facilities.Logging;
using System;
using System.IO;
using WebApplication3.App_Start;

namespace WebApplication3
{
    public class MvcApplication : AbpWebApplication<WebAppModule>
    {
        protected override void Application_Start(object sender, EventArgs e)
        {
            var basedir = AppDomain.CurrentDomain.BaseDirectory;
#if DEBUG
            AbpBootstrapper.IocManager.IocContainer.AddFacility<LoggingFacility>(
                // Abp.Castle.Log4Net
                f => f.UseAbpLog4Net().WithConfig(Path.Combine(basedir, "log4net.config"))
            );
#else
            AbpBootstrapper.IocManager.IocContainer.AddFacility<LoggingFacility>(
                f => f.UseAbpLog4Net().WithConfig(Path.Combine(basedir, "log4net.Production.config"))
            );
#endif

            base.Application_Start(sender, e);
        }
    }
}
