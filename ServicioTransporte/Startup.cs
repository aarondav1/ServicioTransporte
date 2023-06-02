using Microsoft.EntityFrameworkCore;
using ServicioTransporte.BusinessLogic.Servicios;
using System.Text.Json.Serialization;

namespace ServicioTransporte
{
    public class Startup
    {

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles).AddNewtonsoftJson();

            services.AddEndpointsApiExplorer();
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("defaultConnection")));

            services.AddAutoMapper(typeof(Startup));

            services.AddScoped<ConductorServicios>();
            services.AddScoped<BusServicios>();
            services.AddScoped<RutaServicios>();
            services.AddScoped<ParadaServicios>();
            services.AddScoped<EstadoServicios>();
            services.AddScoped<AsignacionBusConductorServicios>();
            services.AddScoped<AsignacionRutaBusServicios>();
            services.AddScoped<AsignacionRutaParadaServicios>();

            services.AddCors();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(builder => builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

    }
}
