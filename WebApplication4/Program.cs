using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebApplication4.Models;
using WebApplication4.Models.Data;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();

#region ����������� DbContext
string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(connectionString);

});
#endregion

#region ���������� �����
///������� ������ ���������� �����, ����� ��������� ����� ��������� � ������
builder.Services.AddIdentity<User, IdentityRole>(options =>
                {
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequiredUniqueChars = 0;
                    options.Password.RequireDigit = false;
                    options.Password.RequiredLength = 7;
                })
                    .AddEntityFrameworkStores<AppDbContext>();
#endregion

#region ���������� ����

///������ ��� ����� �������� ���� ����� ����� �� ����������� ��� ������������� Identity User'a

builder.Services.ConfigureApplicationCookie(options =>
{

    // options.ExpireTimeSpan = TimeSpan.FromMinutes(1);
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/AccessDenied";
    
});

#endregion

#region ���������� ����������� �� ������� Claims

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Administrator", builder =>
    {
        builder.RequireClaim(ClaimTypes.Role, "Administrator");
    });

    options.AddPolicy("Manager", builder =>
    {
        builder.RequireAssertion(x =>
        x.User.HasClaim(ClaimTypes.Role, "Manager") || x.User.HasClaim(ClaimTypes.Role, "Administrator"));
      
    });
});

#endregion

var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

///������ ����� ������������������ ������
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
