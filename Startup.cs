using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using TransactionEntry.Application.Interface;
using TransactionEntry.Infrastructure.Persistance;
using TransactionEntry.Repository;

namespace TransactionEntry
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
            services.AddSingleton<TransactionEntryDBContext>();
            services.AddScoped<ITransactionEntityRepository, EntryRepository>();
            services.AddScoped<IDebitOrCreditRepository, DebitOrCreditRepository>();
            services.AddScoped<ILedgerRepository, LedgerRepository>();
            services.AddScoped<IVoucherRepository, VoucherRepository>();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v3", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "Testing API",
                    Version = "V3"
                });
            });
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseSwagger();
            app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v3/swagger.json", "Swagger file"));

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
