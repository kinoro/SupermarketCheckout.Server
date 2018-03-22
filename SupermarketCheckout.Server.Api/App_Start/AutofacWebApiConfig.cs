using Autofac;
using Autofac.Integration.WebApi;
using SupermarketCheckout.Server.IRepositories;
using SupermarketCheckout.Server.IServices;
using SupermarketCheckout.Server.Repositories;
using SupermarketCheckout.Server.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;

namespace SupermarketCheckout.Server.Api.App_Start
{
    public class AutofacWebApiConfig
    {
        public static IContainer Container;

        public static void Initialize(HttpConfiguration config)
        {
            Initialize(config, RegisterServices(new ContainerBuilder()));
        }


        public static void Initialize(HttpConfiguration config, IContainer container)
        {
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        private static IContainer RegisterServices(ContainerBuilder builder)
        {
            //Register your Web API controllers.  
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterType<ProductService>()
                .As<IProductService>()
                .InstancePerRequest();
            builder.RegisterType<BasketService>()
                .As<IBasketService>()
                .InstancePerRequest();
            builder.RegisterType<DiscountService>()
                .As<IDiscountService>()
                .InstancePerRequest();
            builder.RegisterType<AppliedDiscountService>()
                .As<IAppliedDiscountService>()
                .InstancePerRequest();

            builder.RegisterType<ProductRepository>()
                .As<IProductRepository>()
                .InstancePerRequest();
            builder.RegisterType<DiscountRepository>()
                .As<IDiscountRepository>()
                .InstancePerRequest();

            //Set the dependency resolver to be Autofac.  
            Container = builder.Build();

            return Container;
        }
    }
}