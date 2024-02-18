using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TaskSched.Configuration;
using TaskSched.Data.Context;
using TaskSched.Data.Models;
using TaskSched.Mappings;
using TaskSched.Middlewares;
using TaskSched.Services.CompanyDetailsInfoService;
using TaskSched.Services.ComplaintUserCrudService;
using TaskSched.Services.Interfaces;
using TaskSched.Services.ProjectTaskCrudService;
using TaskSched.Services.SortService;
using TaskScheduler.Services.SortService.Interfaces;
using Task = TaskSched.Data.Models.Task;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLogging(loggingBuilder =>
{
	loggingBuilder.AddSeq(builder.Configuration["ServerUrl"], builder.Configuration["SeqApiKey"]);
});

builder.Services.AddMvc();

builder.Services.AddScoped<IProjectTaskCrudService, ProjectTaskCrudService>();
builder.Services.AddScoped<ISortService<Task>, ProjectTaskSortService<Task>>();
builder.Services.AddScoped<ICompanyDetailsInfoService, CompanyDetailsInfoService>();
builder.Services.AddScoped<IUserComplaintsCrudService, UserComplaintsCrudService>();

builder.Services.AddDbContext<TaskSchedulerContext>(options =>
{
	options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.ConfigureApplicationCookie(options =>
{
	options.Cookie.Name = "TaskSchedulerCookie";
	options.LoginPath = "/Account/Login/";
});

builder.Services.AddAutoMapper(typeof(MappingProfile));


builder.Services.AddIdentity<User, IdentityRole<Guid>>(options =>
{
	options.Password.RequiredLength = 8;
	options.Password.RequireDigit = false;
	options.Password.RequireLowercase = false;
	options.Password.RequireUppercase = false;
	options.Password.RequireNonAlphanumeric = false;
	options.Password.RequiredUniqueChars = 0;
})
	.AddEntityFrameworkStores<TaskSchedulerContext>();

builder.Configuration.Bind("CompanyInfo", new CompanyInfo());

var app = builder.Build();


app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseErrorMiddleware();
app.MapDefaultControllerRoute();

app.Run();

