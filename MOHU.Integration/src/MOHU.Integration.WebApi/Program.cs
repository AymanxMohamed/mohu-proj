
using Microsoft.AspNetCore.HttpLogging;
using MOHU.Integration.Application;
using MOHU.Integration.Contracts.Dto.Config;
using MOHU.Integration.Infrastructure;
using MOHU.Integration.WebApi.Extension;
using MOHU.Integration.WebApi.HttpInterceptor;
using MOHU.Integration.WebApi.SwaggerFilter;
using System.Net;

namespace MOHU.Integration.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddInfrastructure(builder.Configuration);
            builder.Services.AddApplication();
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c=>c.OperationFilter<AddHeaderParameter>());

            builder.Services.AddHttpLogging(logging =>
            {
                logging.LoggingFields = HttpLoggingFields.All;
                logging.RequestBodyLogLimit = 4096;
                logging.ResponseBodyLogLimit = 4096;
                logging.CombineLogs = true;
            });
            builder.Services.Configure<MemoryCacheConfig>(builder.Configuration.GetSection(nameof(MemoryCacheConfig)));
            builder.Services.AddHttpLoggingInterceptor<CorrelationIdHttpLoggingInterceptor>();
            builder.Services.AddHttpClient();
            //builder.Services.AddHttpClient("ServiceDesk").ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
            //{
            //  ServerCertificateCustomValidationCallback =
            //(httpRequestMessage, cert, cetChain, policyErrors) =>
            //{
            //    return true;
            //}
            //});
            var app = builder.Build();
            app.UseHttpLogging();
            app.UseGlobalExceptionHandler();

            // Configure the HTTP request pipeline.
            //if (app.Environment.IsDevelopment())
            //{
                app.UseSwagger();
                app.UseSwaggerUI();
            //}
           
            app.UseHttpsRedirection();
           
            app.UseLanguageMiddleware();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
