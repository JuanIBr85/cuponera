using ApiServicioCupones.Data;
using ApiServicioCupones.Interfaces;
using ApiServicioCupones.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<DataBaseContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection"))); // falta colocar la connecction string

builder.Services.AddControllers().AddJsonOptions(optios=>
{
    optios.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    optios.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
}
);

builder.Services.AddScoped<ICuponesService, CuponesService>();
builder.Services.AddScoped<ISendEmailService, SendEmailService>();

Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Logger(l =>
        l.Filter.ByIncludingOnly(evt => evt.Level == Serilog.Events.LogEventLevel.Error)
        .WriteTo.File("Logs/Log-Error-.txt", rollingInterval: RollingInterval.Day)
    )
      .Enrich.FromLogContext()
       .WriteTo.Logger(l =>
        l.Filter.ByIncludingOnly(evt => evt.Level == Serilog.Events.LogEventLevel.Information)
        .WriteTo.File("Logs/Log-Information-.txt", rollingInterval: RollingInterval.Day)
    )

    .CreateLogger();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
