using Microsoft.EntityFrameworkCore;
using ProjectGPT.Configurations;
using ProjectGPT.Persistence;
using AutoMapper;
using ProjectGPT.Services;
using ProjectGPT.Interfaces;
using ProjectGPT.Persistence.Repositories;
using ProjectGPT.Persistence.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<OpenAIConfig>(builder.Configuration.GetSection("OpenAI"));

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddDbContext<GPTDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IOpenAIService, OpenAIService>();
builder.Services.AddScoped<IConversationService, ConversationService>();
builder.Services.AddScoped<IConversationRepository, ConversationRepository>();
builder.Services.AddScoped<IChatRepository, ChatRepository>();
builder.Services.AddScoped<IChatService, ChatService>();

var app = builder.Build();

app.UseCors(
    options => options.WithOrigins("https://localhost:44486")
        .AllowAnyHeader()
        .AllowAnyMethod()
);

app.UseSwagger();
app.UseSwaggerUI();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html"); ;

app.Run();
