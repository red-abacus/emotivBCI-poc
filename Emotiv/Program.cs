using Emotiv.Components;
using Emotiv.Services.ExpressionsInterpreter;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddSingleton<IExpressionsInterpreterService, ExpressionsInterpreterService>();
builder.Services.AddHttpClient();

var app = builder.Build();
app.UseWebSockets();

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

app.MapPost("/start-analysis", async (IExpressionsInterpreterService expressionInterpreter) =>
{
    new Task(async () =>
    {
        expressionInterpreter.StartAnalizing();
        await Task.Delay(TimeSpan.FromSeconds(Configurations.ProcessingTimeSeconds));
        await expressionInterpreter.StopAnalysisAndSendResult();
    }).Start();
    return;
});

app.Run();
