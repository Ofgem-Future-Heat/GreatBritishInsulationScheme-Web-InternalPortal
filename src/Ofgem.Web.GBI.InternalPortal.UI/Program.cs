using Azure.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;
using Ofgem.Web.GBI.InternalPortal.UI.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddHttpClient();


if (!builder.Environment.IsDevelopment())
{
    builder.Services.AddLogsConfiguration(builder.Configuration);
}

//Azure Key Vault Configuration
builder.Configuration.AddAzureKeyVault(
new Uri($"https://{builder.Configuration["KeyVaultName"]}.vault.azure.net/"),
new DefaultAzureCredential());

// Authentication
//Internal Portal Session
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromHours(10);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.Cookie.Name = ".gbi.int.session";
});

builder.Services.AddAuthorization(options =>
{
	var roles = new string[]
    {
	    "Advanced",
	    "Basic",
	    "Standard",
	    "InternalAdmin"
    };

	options.AddPolicy("AtLeastOneRole", policy =>
        policy.RequireRole(roles));
});

//Adding Microsoft Web App Auth
builder.Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApp(builder.Configuration, "AAD:InternalPortal")
    .EnableTokenAcquisitionToCallDownstreamApi()
    .AddInMemoryTokenCaches();

builder.Services.Configure<CookieAuthenticationOptions>(CookieAuthenticationDefaults.AuthenticationScheme, options =>
{
    options.AccessDeniedPath = new PathString("/Errors/403");
});

builder.Services.AddHttpContextAccessor();

//Adding and configuring Razor Pages
builder.Services.AddRazorPages()
    .AddRazorPagesOptions(options =>
    {
        options.Conventions.AuthorizePage("/Index", "AtLeastOneRole");
        options.Conventions.AuthorizeFolder("/ExternalUsers", "AtLeastOneRole");
    })
    .AddViewOptions(options =>
    {
        options.HtmlHelperOptions.ClientValidationEnabled = false;
    })
    .AddMicrosoftIdentityUI();

builder.Services.ConfigureApplicationServices(builder.Configuration);

builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
});

var app = builder.Build();

app.UseForwardedHeaders();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseSession();
app.MapRazorPages();
app.UseStatusCodePagesWithReExecute("/Errors/{0}");

app.Run();
