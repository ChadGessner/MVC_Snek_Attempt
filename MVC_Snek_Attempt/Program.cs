using MVC_Snek_Attempt.MisterService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy => policy
    .AllowAnyOrigin()
    .AllowAnyHeader()
    .AllowAnyMethod());
}); ;
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<IGameService, GameService>();
builder.Services.AddSingleton<ISnekService, SnekService>();
builder.Services.AddSingleton<IGridService, GridService>();
builder.Services.AddMemoryCache();
builder.Services.AddSingleton<ICachingService, CachingService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseCors();
app.UseAuthorization();
app.UseEndpoints(endpoints => endpoints.MapControllers());
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
