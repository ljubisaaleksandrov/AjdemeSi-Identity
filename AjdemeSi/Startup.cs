using AjdemeSi.Hubs;
using AjdemeSi.Services.Interfaces.Identity;
using AjdemeSi.Services.Logic.Identity;
using Microsoft.AspNet.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AjdemeSi.Startup))]
namespace AjdemeSi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            GlobalHost.DependencyResolver.Register(
            typeof(UserGroupsHub),
            () => new UserGroupsHub(new Services.Logic.UserGroupsService()));

            app.MapSignalR();
            log4net.Config.XmlConfigurator.Configure();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IAspNetUsersService>(sp => new AspNetUsersService());
        }
    }

}
