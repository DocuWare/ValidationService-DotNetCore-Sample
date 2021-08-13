using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ValidationServiceDotNetCoreSample.Helpers;
using ValidationServiceDotNetCoreSample.Implementations.RootValueChecks;
using ValidationServiceDotNetCoreSample.Implementations.ValueChecks;
using ValidationServiceDotNetCoreSample.Interfaces;
using ValidationServiceDotNetCoreSample.Services;

namespace ValidationServiceDotNetCoreSample
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            // configure basic authentication 
            services.AddAuthentication("BasicAuthentication")
                .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);

            // configure DI for application services
            services.AddScoped<IUserService, FakeUserService>();
            services.AddSingleton<IDocuWareConnectionService, DocuWareConnectionService>();
            services.AddScoped<IInputModelValidationService, InputModelValidationService>();
            services.AddScoped<IValueCheck, CompanyFieldContainsCorrectValueCheck>();
            services.AddScoped<IRootValueCheck, TriggeredByAdminUserCheck>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();

            // global cors policy
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
