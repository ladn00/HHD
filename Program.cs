using HHD.BL.Auth;
using HHD.BL.Resume;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IAuth, Auth>();
builder.Services.AddSingleton<HHD.DAL.IAuthDAL, HHD.DAL.AuthDAL>();
builder.Services.AddSingleton<HHD.DAL.IDbSessionDAL, HHD.DAL.DbSessionDAL>();
builder.Services.AddSingleton<HHD.DAL.IUserTokenDAL, HHD.DAL.UserTokenDAL>();
builder.Services.AddSingleton<HHD.DAL.IProfileDAL, HHD.DAL.ProfileDAL>();
builder.Services.AddSingleton<HHD.DAL.ISkillDAL, HHD.DAL.SkillDAL>();

builder.Services.AddSingleton<IEncrypt, Encrypt>();
builder.Services.AddScoped<ICurrentUser, CurrentUser>();
builder.Services.AddScoped<IDbSession, DbSession>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddScoped<HHD.BL.General.IWebCookie, HHD.BL.General.WebCookie>();
builder.Services.AddSingleton<HHD.BL.Profile.IProfile, HHD.BL.Profile.Profile>();
builder.Services.AddSingleton<HHD.BL.Resume.IResume, HHD.BL.Resume.Resume>();
builder.Services.AddSingleton<HHD.BL.Profile.ISkill, HHD.BL.Profile.Skill>();
builder.Services.AddMvc();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
    app.UseWebAssemblyDebugging();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthorization();
app.UseBlazorFrameworkFiles();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
