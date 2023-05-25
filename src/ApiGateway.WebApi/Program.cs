using Microsoft.AspNetCore.Mvc.ApiExplorer;

var builder = WebApplication.CreateBuilder(args);
var webApiCorsPolicyName =
    builder.Configuration.GetValue<string>("WebApiCorsConfig:PolicyName") ??
    throw new ArgumentNullException("Cors configuration could not be found.");

#region Add to the container.
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddWebApiServices(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();// To verify
builder.Services.AddSwaggerGen();
#endregion Add to the container.

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

        foreach (var desc in apiVersionDescriptionProvider.ApiVersionDescriptions)
        {
            options.SwaggerEndpoint($"../swagger/{desc.GroupName}/swagger.json", desc.ApiVersion.ToString());
            options.DefaultModelsExpandDepth(-1);
            options.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
        }
    });
}

#region Middleware 
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseCors(webApiCorsPolicyName);
#endregion Middleware 

app.MapControllers();

app.Run();
