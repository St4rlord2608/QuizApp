using QuizApp.Components;
using Microsoft.AspNetCore.ResponseCompression;
using QuizApp.Hubs;
using QuizApp.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddResponseCompression(opts =>
{
    opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
          new[] { "application/octet-stream" });
});

builder.Services.AddHttpContextAccessor();
builder.Services.AddSignalR();
builder.Services.AddControllers();
builder.Services.AddTransient<BuzzerLobbyManagementService>();
builder.Services.AddTransient<SkyjoService>();
builder.Services.AddTransient<UserService>();
builder.Services.AddTransient<MainService>();
builder.Services.AddTransient<GeneralLobbyService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseResponseCompression();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();


app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();
app.MapHub<ChatHub>("/chathub");
app.MapHub<BuzzHub>("/buzzhub");

app.MapControllers();

app.Run();
