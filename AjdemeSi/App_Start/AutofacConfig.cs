using AjdemeSi.Domain;
using AjdemeSi.Domain.Models.Identity;
using AjdemeSi.Domain.Models.Ride;
using AjdemeSi.Domain.Models.Settings;
using AjdemeSi.Domain.Models.UserGroups;
using AjdemeSi.Services.Interfaces;
using AjdemeSi.Services.Logic;
using Autofac;
using Autofac.Integration.Mvc;
using AutoMapper;
using System;
using System.Linq;
using System.Web.Mvc;

namespace AjdemeSi.App_Start
{
    public class AutofacConfig
    {
        public static void Configure()
        {
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            if(AppDomain.CurrentDomain.GetAssemblies().Any(a => a.GetName().Name == "AjdemeSi.Services"))
            {
                builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies().Single(a => a.GetName().Name == "AjdemeSi.Services")).AsImplementedInterfaces();
            }
            builder.RegisterType<DataContext>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<Hubs.UserGroupsHub>().AsImplementedInterfaces().InstancePerLifetimeScope();

            builder.Register<IMapper>(c => new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserGeneralViewModel, AspNetUser>();
                cfg.CreateMap<AspNetUser, UserGeneralViewModel>();

                cfg.CreateMap<IdentityUserViewModel, AspNetUser>();
                cfg.CreateMap<AspNetUser, IdentityUserViewModel>()
                   .ForMember(dest => dest.UserRoles, opt => opt.MapFrom(src => src.AspNetRoles.Select(ur => ur.Name)));

                cfg.CreateMap<RidePassanger, AspNetUser>();
                cfg.CreateMap<RidePassanger, RidePassengerViewModel>()
                   .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.AspNetUser.UserName));
                cfg.CreateMap<RideDriverViewModel, RidePassanger>();
                cfg.CreateMap<RideViewModel, Ride>().ForMember(d => d.RidePassangers, opt => opt.MapFrom(s => s.Passengers));

                cfg.CreateMap<CarViewModel, Car>();
                cfg.CreateMap<Car, CarViewModel>();

                cfg.CreateMap<UsersGroup, UserGroupViewModel>();

                cfg.CreateMap<ChatMessage, ChatMessageViewModel>();
                cfg.CreateMap<ChatMessageViewModel, ChatMessage>();

            }).CreateMapper());

            var container = builder.Build();
            var mvcResolver = new AutofacDependencyResolver(container);
            DependencyResolver.SetResolver(mvcResolver);
        }
    }
}