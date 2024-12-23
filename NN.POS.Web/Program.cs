using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using NN.POS.Web;
using NN.POS.Web.AppSettings;
using NN.POS.Web.Constants;
using NN.POS.Web.Providers;
using NN.POS.Web.Services;
using NN.POS.Web.States;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var settings = new Setting();

builder.Configuration.GetSection("Setting").Bind(settings);
builder.Services.AddSingleton(settings);

builder.Services.Scan(s =>
    s.FromAssemblies(AppDomain.CurrentDomain.GetAssemblies())
        .AddClasses(c => c.AssignableTo(typeof(IStateBaseService)))
        .AsImplementedInterfaces()
        .WithSingletonLifetime());

builder.Services.Scan(s =>
    s.FromAssemblies(AppDomain.CurrentDomain.GetAssemblies())
        .AddClasses(c => c.AssignableTo(typeof(IBaseService)))
        .AsImplementedInterfaces()
        .WithTransientLifetime());

builder.Services.AddHttpClient(AppConstants.HttpClientName, options =>
{
    options.BaseAddress = new Uri(settings.ApiUrl);
}).AddHttpMessageHandler<CustomHttpHandler>();

builder.Services.AddMudServices();
builder.Services.AddBlazoredLocalStorageAsSingleton();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthProvider>();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<CustomHttpHandler>();

await builder.Build().RunAsync();
