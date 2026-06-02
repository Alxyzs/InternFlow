using InternFlow.BLL.Interfaces;
using InternFlow.BLL.Services;
using InternFlow.DAL.Context;
using InternFlow.DAL.Interfaces;
using InternFlow.DAL.Repositories;
using InternFlow.EL.DBContextModels;
using InternFlow.MVC.Hubs;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

//appsettingsden gelen Jwt bilgileri
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var secretKey = jwtSettings["SecretKey"];

// Services
builder.Services.AddControllersWithViews();

//JWT için
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = "MvcCookie";
    options.DefaultChallengeScheme = "MvcCookie";
})
.AddCookie("MvcCookie", options =>
{
    options.LoginPath = "/Account/Login";
    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
    };
    //cookie kısmı authorize icin 
    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            context.Token = context.Request.Cookies["AuthToken"];
            return Task.CompletedTask;
        }
    };
});

//DbContext sınıfım için .
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection") ?? "" ));

// Repository
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

// Services
    builder.Services.AddScoped<IProjectService, ProjectService>();
    builder.Services.AddScoped<IUserService, UserService>();
    builder.Services.AddScoped<ITaskService, TaskService>();
    builder.Services.AddScoped<ICommentService, CommentService>();
    builder.Services.AddScoped<IActivityLogService, ActivityLogService>();
    builder.Services.AddScoped<IProjectMemberService, ProjectMemberService>();
    builder.Services.AddScoped<IAuthService, AuthService>();
//

//Viewler kullanmak yerine Swagger olusutruldu
builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
//

// SignalR
builder.Services.AddSignalR();

var app = builder.Build();

// Error handling
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

//Swagger icin buda
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
//

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

//Jwt AUTH İCİN
app.UseAuthentication();

//SignalR hub'ını Ekler
app.MapHub<NotificationHub>("/notificationHub");

app.UseAuthorization();

// Routes
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

//admin rolunde bırı yok ise otomatik olusturma.
using (var scope = app.Services.CreateScope())
{
    var userService = scope.ServiceProvider.GetRequiredService<IAuthService>();
    try
    {
        userService.Register(new InternFlow.EL.DBContextModels.User
        {
            FullName = "Admin",
            Username = "admin",
            Email = "admin@internflow.com",
            Role = "Admin"
        }, "1234!");
    }
    catch { }
}

app.Run();