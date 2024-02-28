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
            //CORS POLICY TO ALLOW FETCHING WITH AXIOS IN REACT
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.WithOrigins("http://localhost:3001")
                           .AllowAnyHeader()
                           .AllowAnyMethod();
                });
            });
        }
    }
}
