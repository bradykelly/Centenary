using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    }).AddCookie()
    .AddOpenIdConnect("Auth0", options =>
    {
        options.Authority = $"https://{builder.Configuration["Authentication:Auth0:Domain"]}";
        options.ClientId = builder.Configuration["Authentication:Auth0:ClientId"];
        options.ClientSecret = builder.Configuration["Authentication:Auth0:ClientSecret"];

        options.ResponseMode = OpenIdConnectResponseType.Code;
        
        options.Scope.Clear();
        options.Scope.Add(("openid"));
        
        options.CallbackPath = new PathString("/signin-auth0");
        options.ClaimsIssuer = "Auth0";

        options.Events = new OpenIdConnectEvents
        {
            OnRedirectToIdentityProviderForSignOut = (context =>
            {
                var logoutUri = $"https://{builder.Configuration["Athentication::Auth0:Domain"]}/v2/logout?client_id={builder.Configuration["Athentication::Auth0:ClientId"]}";
                var postLogoutUri = context.Properties.RedirectUri;

                if (!string.IsNullOrWhiteSpace(postLogoutUri))
                {
                    if (postLogoutUri.EndsWith("/"))
                    {
                        var request = context.Request;
                        postLogoutUri = request.Scheme + "://" + request.Host + request.PathBase + postLogoutUri;
                    }
                    logoutUri += $"&returnTo={Uri.EscapeDataString(postLogoutUri)}";                
                }
                
                context.Response.Redirect(logoutUri);
                context.HandleResponse();

                return Task.CompletedTask;
            })
        };
    });

builder.Services.AddControllersWithViews();

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

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();