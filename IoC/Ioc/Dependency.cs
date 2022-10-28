using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interface;
using Application.Sender;
using Application.Services;
using DataLayer.Repositories;
using Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace IoC.Ioc
{
    public class Dependency
    {
        public static void RegisterServices(IServiceCollection service)
        {
            service.AddTransient<IUserService, UserService>();
            service.AddTransient<IUserRepository, UserRepository>();
            service.AddTransient<IViewRenderService, RenderViewToString>();
            service.AddTransient<IContactUssRepository, ContactUssRepository>();
            service.AddTransient<IContactUssService, ContactUssService>();
            service.AddTransient<ITicketRepository, TicketRepository>();
            service.AddTransient<ITicketService, TicketServices>();
            service.AddTransient<IProductRepository, ProductRepository>();
            service.AddTransient<IProductService, ProductService>();
            service.AddTransient<IOrderRepository, OrderRepository>();
            service.AddTransient<IOrderService, OrderService>();
            service.AddTransient<IDynamicLinkService, DynamicLinkService>();
            service.AddTransient<IDynamicRepository, DynamicLinkRepository>();
            service.AddTransient<ISocialMediaRepository, SocialMediaRepository>();
            service.AddTransient<ISocialMediaService, SocialMediaService>();
            service.AddTransient<IPermissionRoleRepository, PermissionRoleRepository>();
            service.AddTransient<IPermissionService, PermissionService>();
            service.AddTransient<IFaqRepository, FaqRepository>();
            service.AddTransient<IFaqService, FaqService>();
            service.AddTransient<IDynamicPageRepository, DynamicPageRepository>();
            service.AddTransient<IDynamicPageService, DynamicPageService>();
            service.AddTransient<IBannerRepository, BannerRepository>();
            service.AddTransient<IBannerService, BannerService>();
            service.AddTransient<ILoggerRepository, LoggerRepository>();
            service.AddTransient<ILoggerService, LoggerService>();
            service.AddTransient<IVoteRepository, VoteRepository>();
            service.AddTransient<IVoteService, VoteService>();
        }
    }
}
