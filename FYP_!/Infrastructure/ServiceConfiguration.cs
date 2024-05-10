using Core.Data.Entities;
using Service.IService;
using Service.Service;
using UnitOfWork;

namespace FYP__.Infrastructure
{
    public class ServiceConfiguration
    {
        public ServiceConfiguration(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();
            services.AddScoped<IFastService,FastService>();
            services.AddScoped<IComsatService,ComsatsService>();
            services.AddScoped<IKIETService, KIETService>();
            services.AddScoped<ILUMSService, LUMSService>();
            services.AddScoped<IBahriaService, BahriaService>();
            services.AddScoped<IFilterService, FilterService>();
            services.AddScoped<ConsultationService>();
            services.AddSignalR();
            services.AddSingleton<IDictionary<string, UserConnection>>(opt =>
    new Dictionary<string, UserConnection>());

            //CORS POLICY TO ALLOW FETCHING WITH AXIOS IN REACT
            //services.AddCors(options =>
            //{
            //     options.SetIsOriginAllowed(origin => origin == "http://localhost:3000" || origin == "http://localhost:7100")
            //        .AllowCredentials()                                          // Allow credentials (cookies, authorization headers)
            //        .AllowAnyMethod()                                            // Allow any HTTP method
            //        .AllowAnyHeader();
            
            //});
        }
    }
}
