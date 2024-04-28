using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using am.kon.projects.marktguru_test.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
                       throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen(options =>
// {
//     //options.SwaggerGeneratorOptions.
// });
builder.Services.AddSwaggerGen();

// separate nonauthorized requests to web pages and for API requests
// so if it is an authorized API request server will return 401 http status code
// and if it is web page request, user will be redirected to log in page
builder.Services.ConfigureApplicationCookie(options =>
{
    options.Events.OnRedirectToLogin = context =>
    {
        // Check if the request is an API call
        if (context.Request.Path.StartsWithSegments("/api"))
        {
            context.Response.StatusCode = 401; // Unauthorized
            context.Response.Headers["Content-Type"] = "application/json";
            context.Response.WriteAsync("{\"error\":\"Not authorized.\"}");
        }
        else
        {
            // Redirect to login page for non-API calls
            context.Response.Redirect(context.RedirectUri);
        }

        return Task.CompletedTask;
    };

    options.Events.OnRedirectToAccessDenied = context =>
    {
        if (context.Request.Path.StartsWithSegments("/api"))
        {
            context.Response.StatusCode = 403; // Forbidden
            context.Response.Headers["Content-Type"] = "application/json";
            context.Response.WriteAsync("{\"error\":\"Access denied.\"}");
        }
        else
        {
            context.Response.Redirect(context.RedirectUri);
        }

        return Task.CompletedTask;
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();