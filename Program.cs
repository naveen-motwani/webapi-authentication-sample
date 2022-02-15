using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.AzureAD.UI;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Identity.Web;

var builder = WebApplication.CreateBuilder(args);
var applicationURL = "weatherapipoc.azurewebsites.net";
var authorization_uri = "https://login.windows.net/85fb7079-89c1-4496-8137-4cc295758328/oauth2/v2.0/authorize";
var clientId = "13c6a116-55ea-462a-a19c-6a493917128b";
var tenantId = "85fb7079-89c1-4496-8137-4cc295758328";
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(opt =>
{
    opt.Challenge = $"Bearer realm=\"{applicationURL}\" authorization_uri=\"{authorization_uri}\" resource_id=\"{clientId}\"";

},
opt =>
{
    opt.TenantId = tenantId;
    opt.ClientId = applicationURL;
    opt.Instance = "https://login.microsoftonline.com";
});



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
