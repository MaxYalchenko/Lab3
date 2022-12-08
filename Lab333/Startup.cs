using Lab333.Data;
using Lab333.Models;
using Lab333.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab333
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
            string connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<Context>(options => options.UseSqlServer(connection));
            services.AddControllersWithViews();
            services.AddMemoryCache();
            services.AddDistributedMemoryCache();
            services.AddSession();

            services.AddScoped<ICached<Workpeople>, CachedWorkpeople>();
            services.AddScoped<ICached<Subdivision>, CachedSubdivision>();

            services.AddScoped<ICached<PeoplePlan>, CachedPeoplePlan>();
            services.AddScoped<ICached<SubdivisionPlan>, CachedSubdivisionPlan>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseSession();
            app.UseAuthorization();

            app.Map("/info", (appBuilder) =>
            {
                appBuilder.Run(async (context) =>
                {
                    string response = "<HTML><HEAD><TITLE>Информация</TITLE></HEAD>" +
                    "<META http-equiv='Content-Type' content='text/html; charset=utf-8'/>" +
                    "<BODY><H1>Информация:</H1>";
                    response += "<BR> Сервер: " + context.Request.Host;
                    response += "<BR> Путь: " + context.Request.PathBase;
                    response += "<BR> Протокол: " + context.Request.Protocol;
                    response += "<BR><A href='/'>Главная</A></BODY></HTML>";

                    await context.Response.WriteAsync(response);
                });
            });

            app.Map("/workpeople", (appBuilder) =>
            {
                appBuilder.Run(async (context) =>
                {
                    IEnumerable<Workpeople> workpeoples = context.RequestServices.GetService<ICached<Workpeople>>().GetList("Workpeople17");

                    string htmlString1 = "<HTML><HEAD><TITLE>Люди</TITLE></HEAD>" +
                    "<META http-equiv='Content-Type' content='text/html; charset=utf-8'/>" +
                    "<BODY><H1>Список рабочих людей</H1>" +
                    "<TABLE BORDER=1>";
                    htmlString1 += "<TR>";
                    htmlString1 += "<TH>Код людей</TH>";
                    htmlString1 += "<TH>Имена Людей</TH>";
                    htmlString1 += "<TH>Рейтинг</TH>";
                    htmlString1 += "<TH>Достижения</TH>";

                    htmlString1 += "</TR>";
                    foreach (Workpeople workpeople in workpeoples)
                    {
                        htmlString1 += "<TR>";
                        htmlString1 += "<TD>" + workpeople.workpeopleId + "</TD>";
                        htmlString1 += "<TD>" + workpeople.peopleName + "</TD>";
                        htmlString1 += "<TD>" + workpeople.amountPeople + "</TD>";
                        htmlString1 += "<TD>" + workpeople.Achievements + "</TD>";

                        htmlString1 += "</TR>";
                    }
                    htmlString1 += "</TABLE>";
                    htmlString1 += "<BR><A href='/'>Главная</A></BR>";
                    htmlString1 += "</BODY></HTML>";
                    await context.Response.WriteAsync(htmlString1);
                  
                });
            });

            app.Map("/subdivision", (appBuilder) =>
            {
                appBuilder.Run(async (context) =>
                {
                    IEnumerable<Subdivision> subdivisions = context.RequestServices.GetService<ICached<Subdivision>>().GetList("Subdivision17");

                    string htmlString2 = "<HTML><HEAD><TITLE>Предприятия</TITLE></HEAD>" +
                    "<META http-equiv='Content-Type' content='text/html; charset=utf-8'/>" +
                    "<BODY><H1>Список предприятий</H1>" +
                    "<TABLE BORDER=1>";
                    htmlString2 += "<TR>";
                    htmlString2 += "<TH>Код предприятия</TH>";
                    htmlString2 += "<TH>Название предприятия</TH>";
                    htmlString2 += "<TH>Рейтинг предприятия</TH>";


                    htmlString2 += "</TR>";
                    foreach (Subdivision subdivision in subdivisions)
                    {
                        htmlString2 += "<TR>";
                        htmlString2 += "<TD>" + subdivision.subdivisionId + "</TD>";
                        htmlString2 += "<TD>" + subdivision.subdivisonName + "</TD>";
                        htmlString2 += "<TD>" + subdivision.amountSubdivision + "</TD>";
                        htmlString2 += "</TR>";
                    }
                    htmlString2 += "</TABLE>";
                    htmlString2 += "<BR><A href='/'>Главная</A></BR>";
                    htmlString2 += "</BODY></HTML>";
                    await context.Response.WriteAsync(htmlString2);
                });
            });

            app.Run((context) =>
            {  
                context.RequestServices.GetService<ICached<Workpeople>>().AddList("Worpeople17");
                context.RequestServices.GetService<ICached<Subdivision>>().AddList("Worpeople17");

                string htmlString = "<HTML><HEAD><TITLE>Компании</TITLE></HEAD>" +
                "<META http-equiv='Content-Type' content='text/html; charset=utf-8'/>" +
                "<BODY><H1>Главная</H1>";
                htmlString += "<BR><A href='/'>Главная</A></BR>";
                htmlString += "<BR><A href='/workpeople'>Список сотрудников</A></BR>";
                htmlString += "<BR><A href='/subdivision'>Список предприятий</A></BR>";
                htmlString += "<BR><A href='/info'>Данные о сервере</A></BR>";
                htmlString += "</BODY></HTML>";
                return context.Response.WriteAsync(htmlString);
            });
        }
    }
}














