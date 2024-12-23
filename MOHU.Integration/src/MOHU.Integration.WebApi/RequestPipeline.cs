namespace MOHU.Integration.WebApi;

public static class RequestPipeline
{
    public static WebApplication UseInternalModules(this WebApplication app)
    {
        app.UseHttpLogging();
        app.UseGlobalExceptionHandler();

        app.UseSwagger();
        app.UseSwaggerUI();
           
        app.UseHttpsRedirection();
        app.UseCors("Dev");
        app.UseLanguageMiddleware();
        app.MapControllers();
        
        return app;
    }
}