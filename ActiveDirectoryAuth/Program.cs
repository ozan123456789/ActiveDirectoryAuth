var builder = WebApplication.CreateBuilder(args);

// Kimlik do�rulama ekleme
builder.Services.AddAuthentication("Negotiate")
    .AddNegotiate();

// Yetkilendirme ekleme
builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = options.DefaultPolicy;
});

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Kimlik do�rulama ekleme
app.UseAuthentication();
app.UseAuthorization();

app.UseStaticFiles();
app.MapDefaultControllerRoute();

app.Run();
