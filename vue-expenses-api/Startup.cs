using System.Collections.Generic;
using System.Data;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using vue_expenses_api.Infrastructure;
using vue_expenses_api.Infrastructure.Security;
using vue_expenses_api.Infrastructure.Validation;

namespace vue_expenses_api
{
    public class Startup
    {
        public Startup(
            IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(
            IServiceCollection services)
        {
            services.AddControllers()
                .AddNewtonsoftJson();

            services.AddMediatR(typeof(Startup));

            //hook up validation into MediatR pipeline
            services.AddTransient(
                typeof(IPipelineBehavior<,>),
                typeof(ValidationPipelineBehavior<,>));
            services.AddTransient<IDbConnection>(
                db => new SqliteConnection(
                    Configuration.GetConnectionString("DefaultConnection")));

            services
                .AddEntityFrameworkSqlite()
                .AddDbContext<ExpensesContext>();

            // Inject an implementation of ISwaggerProvider with defaulted settings applied
            services.AddSwaggerGen(
                x =>
                {
                    x.AddSecurityDefinition(
                        "Bearer",
                        new OpenApiSecurityScheme
                        {
                            Description =
                                "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                            Name = "Authorization",
                            In = ParameterLocation.Header,
                            Type = SecuritySchemeType.ApiKey,
                            Scheme = "bearer"
                        });

                    var requirement = new OpenApiSecurityRequirement();
                    requirement.Add(
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Id = "Bearer", //The name of the previously defined security scheme.
                                Type = ReferenceType.SecurityScheme
                            }
                        },
                        new List<string>());
                    x.AddSecurityRequirement(
                        requirement);
                    x.SwaggerDoc(
                        "v1",
                        new OpenApiInfo {Title = "Expenses API", Version = "v1"});
                    x.CustomSchemaIds(y => y.FullName);
                    x.DocInclusionPredicate(
                        (
                            version,
                            apiDescription) => true);
                });

            //attach the the model validator and define the api grouping convention
            //setup fluent validation for the running assembly
            services.AddMvc(
                    options =>
                    {
                        options.Filters.Add<ValidateModelFilter>();
                        options.Conventions.Add(new GroupByApiRootConvention());
                    })
                .AddJsonOptions(opt => { opt.JsonSerializerOptions.IgnoreNullValues = true; })
                .AddFluentValidation(cfg => { cfg.RegisterValidatorsFromAssemblyContaining<Startup>(); })
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddScoped<IPasswordHasher, PasswordHasher>();
            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<ICurrentUser, CurrentUser>();

            services.AddCors();
            services.AddJwt();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app,
            IWebHostEnvironment env,
            ILoggerFactory loggerFactory)
        {
            loggerFactory.AddSerilogLogging();
            app.UseMiddlewares();

            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            if (!env.IsDevelopment())
            {
                app.UseHttpsRedirection();
            }

            app.UseCors(
                builder =>
                    builder
                        .AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod());

            // Enable middleware to serve generated Swagger as a JSON endpoint
            app.UseSwagger(c => { c.RouteTemplate = "swagger/{documentName}/swagger.json"; });

            // Enable middleware to serve swagger-ui assets(HTML, JS, CSS etc.)
            app.UseSwaggerUI(
                x =>
                {
                    x.SwaggerEndpoint(
                        "/swagger/v1/swagger.json",
                        "Expenses API V1");
                });

            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}