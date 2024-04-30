using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using am.kon.projects.marktguru_test.Data;
using am.kon.projects.marktguru_test.product.abstraction;
using am.kon.projects.marktguru_test.product.business_logic;
using am.kon.projects.marktguru_test.product.storage.memory;
using am.kon.projects.marktguru_test.Workers;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHostedService<MainWorker>();

// Add services to the container.
builder.Services.AddSingleton<ProductManagementService>();
builder.Services.AddSingleton<IProductStorage, ProductMemoryStorage>();

// Init DB
string connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
                       throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Init Identity
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();
    
builder.Services.AddAuthentication(options =>
    {
        // use cookies for web pages default authentication scheme
        options.DefaultAuthenticateScheme = IdentityConstants.ApplicationScheme;
        options.DefaultChallengeScheme = IdentityConstants.ApplicationScheme;
        options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
    })
    .AddJwtBearer(options =>
    {
        // configure bearer authentication for API requests
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"]
        };
    });

builder.Services.AddControllersWithViews();
builder.Services.AddResponseCaching();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please insert JWT with Bearer into field<br />Format is: Bearer generated-token",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});

// separate nonauthorized requests to web pages and for API requests
// so if it is an authorized API request server will return 401 http status code
// and if it is web page request, user will be redirected to log in page
builder.Services.ConfigureApplicationCookie(options =>
{
    options.Events.OnRedirectToLogin = context =>
    {
        // Check if the request is an API call
        if (context.Request.Path.StartsWithSegments("/api")&& context.Request.Headers["Accept"] == "application/json")
        {
            // set status code of API request to Unauthorized
            context.Response.StatusCode = 401;
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
        if (context.Request.Path.StartsWithSegments("/api") && context.Request.Headers["Accept"] == "application/json")
        {
            // set status code of API request to Forbidden
            context.Response.StatusCode = 403;
        }
        else
        {
            // Redirect to login page for non-API calls
            context.Response.Redirect(context.RedirectUri);
        }

        return Task.CompletedTask;
    };
});

builder.Services.AddRazorPages();

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

app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();