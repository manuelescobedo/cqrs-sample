using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Infrastructure.Persistence;
using Application.Commands.AddNewProduct;
using MediatR;
using Application.Repositories;
using Application.Queries.GetProductsByName;
using Application.Queries.FindOutStockProducts;
using AutoMapper;
using API.Filters;
using API.Config;
using Application.Commands.DeleteProduct;
using Application.Commands.UpdateProductCurrentStock;
using Application.Commands.UpdateProductUnitPrice;

namespace API
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
            services.AddMvc(options => 
                options.Filters.Add(
                    new ExceptionFilter()
                )
            ).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            AddDbContext(services);

            AddRepositories(services);

            AddMediatR(services);

            AddMapper(services);
        }

        void AddDbContext(IServiceCollection services)
        {
            services.AddEntityFrameworkInMemoryDatabase()
                .AddDbContext<ApplicationContext>(opt => opt.UseInMemoryDatabase(databaseName: "CQRS-MediatR"));

            services.AddScoped<DbContext, ApplicationContext>();

        }

        void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();
        }

        void AddMediatR(IServiceCollection services)
        {
            services.AddMediatR(new Type[] {
                typeof(AddNewProductCommand),
                typeof(DeleteProductCommand),
                typeof(UpdateProductCurrentStockCommand),
                typeof(UpdateProductUnitPriceCommand),
                typeof(GetProductsByNameQuery),
                typeof(FindOutOfStockProductsQuery)
            });
        }

        void AddMapper(IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();


        }
    }



}
