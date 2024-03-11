
using MOHU.Integration.Application;
using MOHU.Integration.Infrastructure;
using MOHU.Integration.WebApi.Extension;

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
            builder.Services.AddSwaggerGen();

            var app = builder.Build();
            app.UseGlobalExceptionHandler();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
           
            app.UseHttpsRedirection();
           
            app.UseLanguageMiddleware();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
