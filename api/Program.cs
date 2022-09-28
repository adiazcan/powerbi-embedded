using API.Model;
using API.Services;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped(typeof(AadService))
                .AddScoped(typeof(PbiEmbedService));

builder.Services.Configure<AzureAd>(builder.Configuration.GetSection("AzureAd"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/reporttoken", ([FromServices] PbiEmbedService pbiService, string report, string workspace) => 
{
    var reportId = Guid.Parse(report);
    var workspaceId = Guid.Parse(workspace);

    var token = pbiService.GetEmbedToken(reportId, workspaceId);

    return token;
})
.WithName("Get report token");

app.Run();