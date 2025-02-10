using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
var services = builder.Services;
services.ConfigureApplicationCookie(options =>
{
    options.Cookie.SameSite = SameSiteMode.None;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // This ensures cookies are sent over HTTPS
});


builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
})
.AddCookie(options =>
{
    options.LoginPath = "/Account/Login";  // Redirect here if not authenticated
    options.LogoutPath = "/Account/Logout";  // Redirect here after logout
})
.AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options =>
{
    options.Authority = "https://localhost:5001"; // The URL of your Duende Identity Server
    options.ClientId = "interactive";
    options.ClientSecret = "your-client-secret"; // Ensure your secret is stored securely
    options.ResponseType = "code"; // Use authorization code flow
    options.SaveTokens = true; // Save tokens in the cookie
    options.Scope.Add("openid");
    options.Scope.Add("profile");
    //options.Scope.Add("email");
    options.Scope.Add("scope2");
    options.SignedOutRedirectUri = "https://localhost:7156/signin-oidc"; // Your app's redirect URI
    options.GetClaimsFromUserInfoEndpoint = true;
    options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
});


services.AddSession(opt =>
{
    opt.IdleTimeout = TimeSpan.FromMinutes(30);
    opt.Cookie.HttpOnly = true;
    opt.Cookie.IsEssential = true;
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
//app.Use(async (context, next) =>
//{
//    context.Response.Headers.Append("Content-Security-Policy", "default-src 'self'; script-src 'self' 'unsafe-inline' 'unsafe-eval' https://localhost:5001");
//    await next();
//});
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();


app.MapRazorPages();


app.Run();
