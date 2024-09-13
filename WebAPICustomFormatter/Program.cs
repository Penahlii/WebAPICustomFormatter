using Microsoft.EntityFrameworkCore;
using WebAPICustomFormatter.Data;
using WebAPICustomFormatter.Formatters;
using WebAPICustomFormatter.Repository.Abstract;
using WebAPICustomFormatter.Repository.Concrete;
using WebAPICustomFormatter.Services.Abstract;
using WebAPICustomFormatter.Services.Concrete;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.OutputFormatters.Insert(0, new TextCsvOutputFormatter());
    options.InputFormatters.Insert(0, new TextCsvInputFormatter());
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped<IPersonService, PersonService>();

var connectionString = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<PersonDbContext>(opt =>
{
    opt.UseSqlServer(connectionString);
});

builder.Services.AddAutoMapper(typeof(Program).Assembly);

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
