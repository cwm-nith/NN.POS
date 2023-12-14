using NN.POS.API.App;
using NN.POS.API.Core;
using NN.POS.API.Infra;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddNewtonsoftJson();

builder.Services.AddInfrastructure(builder.Configuration)
    .AddRegistrationMediatR();
var app = builder.Build();

var settings = app.Services.GetService<AppSettings>();
if(settings?.Swagger.IsEnable ?? false) app.UseCustomSwagger();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseCors(i => 
        i.AllowCredentials()
        .AllowAnyHeader()
        .AllowAnyMethod()
        .SetIsOriginAllowed(_ => true)
    );

app.UseAuthentication();
app.UseAuthorization();

app.UseInfrastructure();

app.MapControllers();

app.Run();
