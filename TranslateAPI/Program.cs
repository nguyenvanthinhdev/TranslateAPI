using FluentValidation.AspNetCore;
using System.Reflection;
using TranslateAPI.ConText;
using TranslateAPI.InterFaces;
using TranslateAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddFluentValidation(c=>
    c.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly()));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//
builder.Services.AddTransient<IUser, UserService>();
builder.Services.AddTransient<ITranslate, TranslateService>();
builder.Services.AddTransient<IManager, ManagerService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddTransient<AppDbContext>();
builder.Services.AddTransient<ServiceExtension, UserService>();



//
builder.Services.AddControllers().AddNewtonsoftJson(options
    => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);//fix kiểu đệ quy object 1 lop có nhiều hoc sinh 1 học sinh nhiều lớp ....//Newtonsoft
builder.Services.AddControllers(
options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);//fix bắt buộc object lop 


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
