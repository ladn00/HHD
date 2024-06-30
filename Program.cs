var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSingleton<HHD.BL.Auth.IAuthBL, HHD.BL.Auth.AuthBL>();
builder.Services.AddSingleton<HHD.DAL.IAuthDAL, HHD.DAL.AuthDAL>();
builder.Services.AddSingleton<HHD.BL.Auth.IEncrypt, HHD.BL.Auth.Encrypt>();
builder.Services.AddScoped<HHD.BL.Auth.ICurrentUser, HHD.BL.Auth.CurrentUser>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddMvc().AddSessionStateTempDataProvider();
builder.Services.AddSession();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
