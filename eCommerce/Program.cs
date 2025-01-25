using eCommerce.Components;
using eCommerce.Components.Account;
using eCommerce.Data;
using eCommerce.Repository;
using eCommerce.Repository.IRepository;
using eCommerce.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Radzen;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<ICategoryRepository,CategoryRepository>();
builder.Services.AddScoped<IProductRepository,ProductRepository>();
builder.Services.AddScoped<ICartRepository,CartRepository>();
builder.Services.AddScoped<IOrderRepository,OrderRepository>();
builder.Services.AddRadzenComponents();
builder.Services.AddSingleton<SharedStateService>();
builder.Services.AddScoped<IdentityUserAccessor>();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();


builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = IdentityConstants.ApplicationScheme;
        options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
    })
    .AddFacebook(facebookOptions =>
    {
        facebookOptions.AppId = "619948340542968";
        facebookOptions.AppSecret = "4b4881e435043799a40a5c594cc10e3c";
    })
    .AddGoogle(googleOptions =>
    {
        googleOptions.ClientId = "115360310888-3gsiibhp7k72i5o8tm1omcash81eivci.apps.googleusercontent.com";
        googleOptions.ClientSecret = "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";
    })
.AddIdentityCookies();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>( )
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// Add additional endpoints required by the Identity /Account Razor components.
app.MapAdditionalIdentityEndpoints();

app.Run();
