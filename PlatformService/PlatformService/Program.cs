using Microsoft.EntityFrameworkCore;
using PlatformService.AsyncDataServices;
using PlatformService.Data;
using PlatformService.Repository.Implementation;
using PlatformService.Repository.Interfaces;
using PlatformService.SyncDataServices.Http;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
Console.WriteLine($"--> Command Service Endpoint {builder.Configuration["CommandService"]}");
builder.Services.AddScoped<IPlatformRepository, PlatformRepository>();
builder.Services.AddHttpClient<ICommandDataClient, HttpCommandDataClient>();
builder.Services.AddSingleton<IMessageBusClient, MessageBusClient>();
// Register AppDbContext BEFORE builder.Build()
if (builder.Environment.IsDevelopment())
{
    Console.WriteLine("--> Using In-Memory Database");
    builder.Services.AddDbContext<AppDbContext>(opt =>
        opt.UseInMemoryDatabase("InMem"));
}
else
{
    Console.WriteLine("--> Using SQL Server");
    var connectionString = builder.Configuration.GetConnectionString("PlatformConn");
    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseSqlServer(connectionString));
}

builder.Services.AddControllers();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.WebHost.ConfigureKestrel(options =>
//{
//    options.ListenAnyIP(80); // match targetPort in Kubernetes
//});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

//app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

PrepDB.PrepPopulation(app, app.Environment.IsProduction());

app.Run();
