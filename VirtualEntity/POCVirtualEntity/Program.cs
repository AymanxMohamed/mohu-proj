using Microsoft.AspNetCore.OData;
using Microsoft.OData.ModelBuilder;
using POCVirtualEntity.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container with OData
builder.Services.AddControllers().AddOData(options =>
{
    // Configure OData with the EDM model
    var odataBuilder = new ODataConventionModelBuilder();
    odataBuilder.EntitySet<Contact>("Contacts"); // "Contacts" is the route
    options.AddRouteComponents("odata", odataBuilder.GetEdmModel())
           .Select()
           .Expand()
           .Filter()
           .OrderBy()
           .Count()
           .SetMaxTop(100);
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

// Enable routing and map controllers
app.UseRouting();

app.MapControllers();

app.Run();
