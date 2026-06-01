using InternFlow.BLL.Interfaces;
using InternFlow.BLL.Services;
using InternFlow.DAL.Context;
using InternFlow.DAL.Interfaces;
using InternFlow.DAL.Repositories;
using InternFlow.EL.DBContextModels;
using InternFlow.MVC.Hubs;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Services
builder.Services.AddControllersWithViews();

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

//SignalR hub'ını Ekler
app.MapHub<NotificationHub>("/notificationHub");

app.UseAuthorization();

// Routes
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();