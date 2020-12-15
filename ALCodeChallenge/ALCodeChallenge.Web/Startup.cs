using ALCodeChallenge.Data;
using ALCodeChallenge.Data.DataEntities;
using ALCodeChallenge.Data.Interfaces;
using ALCodeChallenge.Logic;
using ALCodeChallenge.Logic.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


namespace ALCodeChallenge.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IQuestionLogic, QuestionLogic>();
            services.AddTransient<IAnswerLogic, AnswerLogic>();
            services.AddScoped<IQuestionRepository, QuestionRepository>();
            services.AddScoped<IAnswerRepository, AnswerRepository>();
            services.AddScoped<IQuestionDataContext, QuestionDataContext>();
            services.AddScoped<IAnswerDataContext, AnswerDataContext>();
            services.AddScoped<IResponse<Question>, Response<Question>>();
            services.AddScoped<IResponse<Answer>, Response<Answer>>();

            services.AddSpaStaticFiles(configuration: options => { options.RootPath = "wwwroot"; });
            services.AddControllers();            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseRouting();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            app.UseSpaStaticFiles();
            app.UseSpa(configuration: builder =>
            {
                if (env.IsDevelopment())
                {
                    builder.UseProxyToSpaDevelopmentServer(baseUri: "http://localhost:8080");
                }
            });
        }
    }
}
