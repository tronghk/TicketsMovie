using ZingMp3API.Service.Imployment;
using ZingMp3API.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ZingMp3API.Data;
using ZingMp3API.Service;
using ZingMp3API.Service.Imployment;
using ZingMp3API.Services;
using AppGrIT.Services.Imployement;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddIdentity<AccountIdentity, ApplicationRole>(options =>
{
    //xét password cần có chữ và số
    options.Password.RequireDigit = false;
    //xét password có độ dài trên
    options.Password.RequiredLength = 5;
    // yêu cầu phải có số
    options.Password.RequireNonAlphanumeric = false;
    // yêu cầu có chữ hoa
    options.Password.RequireUppercase = false;
    // yêu cầu có chữ thường
    options.Password.RequireLowercase = false;
    //lock account
    options.Lockout.AllowedForNewUsers = true;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(2);
    options.Lockout.MaxFailedAccessAttempts = 3;

}).AddEntityFrameworkStores<ApplicationDbContext>()
.AddTokenProvider<DataProtectorTokenProvider<AccountIdentity>>(TokenOptions.DefaultProvider);
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
    // khai báo thời gian sống , lưu trữ token
    .AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.RequireHttpsMetadata = false;


        // lấy token
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ClockSkew = TimeSpan.Zero,
            ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
            ValidAudience = builder.Configuration["JWT:ValidAudience"],


            //key
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
            .GetBytes(builder.Configuration["JWT:Secret"]!))
        };
    });


builder.Services.AddScoped<IUsers, UserServices>();
builder.Services.AddScoped<IToken,TokenServices> ();
builder.Services.AddScoped<IRole, RoleService>();
builder.Services.AddScoped<IImages, ImageServices>();
builder.Services.AddTransient<IEmailSender, EmailSenderService>();
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
