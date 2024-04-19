using Cinema.DataContext;
using Cinema.Payloads.Converters;
using Cinema.Payloads.DTO;
using Cinema.Payloads.DTO.DTO_Cinema;
using Cinema.Payloads.DTO.DTO_Food;
using Cinema.Payloads.DTO.Users_DTO;
using Cinema.Payloads.Response;
using Cinema.Services;
using Cinema.Services.Implements;
using Cinema.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Text;
using WebCourseManagement_Business.Implements;
using WebCourseManagement_Business.Interfaces;
//ver 1.1
//ver 1.2
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();


builder.Services.AddSwaggerGen(x =>
{
    x.SwaggerDoc("v1",new Microsoft.OpenApi.Models.OpenApiInfo { Title ="Swagger eShop Solution",Version ="v1"});
    x.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Description = "Làm theo mẫu này. Example: Bearer {Token} ",
        Name = "Authorization",
        In= Microsoft.OpenApi.Models.ParameterLocation.Header,
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    x.AddSecurityRequirement(new OpenApiSecurityRequirement()
                  {
                    {
                      new OpenApiSecurityScheme
                      {
                        Reference = new OpenApiReference
                          {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                          },
                          Scheme = "oauth2",
                          Name = "Bearer",
                          In = ParameterLocation.Header,
                        },
                        new List<string>()
                      }
                    });
    //x.OperationFilter<SecurityRequirementsOperationFilter>();
});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => {
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateAudience = false,
        ValidateIssuer = false,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
            builder.Configuration.GetSection("AppSettings:SecretKey").Value!))
    };
});
builder.Services.AddHttpContextAccessor();
builder.Services.AddCors();
builder.Services.AddHttpClient();

builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddScoped<IService_Authen, Service_Authen>();
builder.Services.AddScoped<IService_GeneralSettings, Service_GeneralSettings>();
builder.Services.AddScoped<IService_Cinema, Service_Cinema>();
builder.Services.AddScoped<IService_Room, Service_Room>();
builder.Services.AddScoped<IService_Seat, Service_Seat>();
builder.Services.AddScoped<IService_Bill, Service_Bill>();
builder.Services.AddScoped<IService_Food,Service_Food>();
builder.Services.AddScoped<IService_Movie,Service_Movie>();
builder.Services.AddScoped<IVNPayService,VNPayService>();
builder.Services.AddScoped<IService_Banner,Service_Banner>();
builder.Services.AddScoped<IService_Schedules,Service_Schedules>();
builder.Services.AddScoped<IManagerMember,Service_ManagerMember>();
builder.Services.AddScoped<ResponseObject<DTO_Token>>();
builder.Services.AddScoped<Converter_DangKi>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ResponseObject<DTO_Bill>>();
builder.Services.AddScoped<Converter_Bill>();
builder.Services.AddScoped<ResponseObject<DTO_DangKi>>();
builder.Services.AddScoped<ResponseObject<DTO_Cinema>>();
builder.Services.AddScoped<Converter_Cinema>();
builder.Services.AddScoped<ResponseObject<DTO_Food>>();
builder.Services.AddScoped<Converter_Food>();
builder.Services.AddScoped<ResponseObject<DTO_Movie>>();
builder.Services.AddScoped<Converter_Movie>();
builder.Services.AddScoped<ResponseObject<DTO_Room>>();
builder.Services.AddScoped<Converter_Room>();
builder.Services.AddScoped<Converter_Schedules>();
builder.Services.AddScoped<ResponseObject<DTO_Schedules>>();
builder.Services.AddScoped<ResponseObject<DTO_Seat>>();
builder.Services.AddScoped<Converter_Seat>();

builder.Services.AddScoped<ResponseObject<DTO_Banner>>();
builder.Services.AddScoped<ResponseObject<DTO_ThongKeDoanhSo_Cinema>>();
builder.Services.AddScoped<Converter_Banner>();
builder.Services.AddScoped<DTO_Banner>();
builder.Services.AddScoped<DTO_ThongKeDoanhSo_Cinema>();
builder.Services.AddScoped<ResponseObject<DTO_GeneralSettings>>();
//builder.Services.AddScoped<DTO_GeneralSettings>();
builder.Services.AddScoped<Converter_GeneralSettings>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
