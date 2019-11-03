﻿using System;
using System.IO;
using System.Reflection;
using System.Text;
using AutoMapper;
using DormyWebService.Entities;
using DormyWebService.Repositories;
using DormyWebService.Services.ContractServices;
using DormyWebService.Services.EquipmentServices;
using DormyWebService.Services.HomeService;
using DormyWebService.Services.MoneyServices;
using DormyWebService.Services.NewFolder;
using DormyWebService.Services.NewsServices;
using DormyWebService.Services.ParamServices;
using DormyWebService.Services.RoomServices;
using DormyWebService.Services.TicketServices;
using DormyWebService.Services.UserServices;
using DormyWebService.Utilities;
using FluentValidation.AspNetCore;
using Hangfire;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Sieve.Services;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.Swagger;

namespace DormyWebService
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
            //SQL server for HangFire
            services.AddHangfire(x => x.UseSqlServerStorage(Configuration["ConnectionString:HangFireDB"]));
            services.AddHangfireServer();

            //Allow Cross Origins
            services.AddCors();

            //Configure AutoMapper
            services.AddAutoMapper(typeof(Startup));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            //Register DBContext and database connection string
            services.AddDbContext<DormyDbContext>(op => op.UseSqlServer(Configuration["ConnectionString:DormyDB"]));

            // Register the Swagger generator, defining 1 or more Swagger documents, v1 is for version 
            services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("oauth2", new ApiKeyScheme
                {
                    Description = "Standard Authorization header using the Bearer scheme. Example: \"bearer {token}\"",
                    In = "header",
                    Name = "Authorization",
                    Type = "apiKey"
                });

                options.SwaggerDoc("v1", new Info { 
                    Title = "Dormy API", 
                    Version = "v1" , 
                    Description = "This API helps manage a dormitory"}
                );

                options.OperationFilter<SecurityRequirementsOperationFilter>();

                //Locate the XML file being generated by ASP.NET...
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.XML";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                //... and tell Swagger to use those XML comments.
                options.IncludeXmlComments(xmlPath);
            });

            //Setup JWT
            var authenticationSettingSection = Configuration.GetSection("AuthenticationSetting");
            services.Configure<AuthenticationSetting>(authenticationSettingSection);
            var appSettings = authenticationSettingSection.Get<AuthenticationSetting>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            //Dependency Injection for Sieve for pagination, sorting, filtering
            services.AddScoped<ISieveProcessor,SieveProcessor>();

            // Configure Dependency Injection
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAdminService, AdminService>();
            services.AddScoped<IStaffService, StaffService>();
            services.AddScoped<IParamService, ParamService>();
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<INewsServices, NewsService>();
            services.AddScoped<IEquipmentService, EquipmentService>();
            services.AddScoped<IRoomService, RoomService>();
            services.AddScoped<IBuildingService, BuildingService>();
            services.AddScoped<IRoomGroupService, RoomGroupService>();
            services.AddScoped<IRoomTypesAndEquipmentTypesService, RoomTypesAndEquipmentTypesService>();
            services.AddScoped<IRoomGroupsAndStaffService, RoomGroupsAndStaffService>();
            services.AddScoped<IRoomsAndEquipmentTypesService, RoomsAndEquipmentTypesService>();
            services.AddScoped<IContractService, ContractService>();
            services.AddScoped<IRenewContractService, RenewContractService>();
            services.AddScoped<ICancelContractService, CancelContractService>();
            services.AddScoped<IParamTypeService, ParamTypeService>();
            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
            services.AddScoped<IRoomBookingService, RoomBookingService>();
            services.AddScoped<IRoomTransferService, RoomTransferService>();
            services.AddScoped<IIssueTicketService, IssueTicketService>();
            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
            services.AddScoped<IHomeService, HomeService>();
            services.AddScoped<IMoneyTransactionService, MoneyTransactionService>();
            services.AddScoped<IRoomMonthlyBillService, RoomMonthlyBillService>();
            services.AddScoped<IStudentMonthlyBillService, StudentMonthlyBillService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // Global cross origins policy
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            //Exception Handler
            app.UseMiddleware(typeof(ExceptionHandlingMiddleware));

            app.UseHangfireDashboard("/hangfire", new DashboardOptions
            {
                Authorization = new[] { new MyAuthorizationFilter() }
            });

            //For JWT
            app.UseAuthentication();
            app.UseHttpsRedirection();

            

            app.UseMvc();
        }
    }
}
