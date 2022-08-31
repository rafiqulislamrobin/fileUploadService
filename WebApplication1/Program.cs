using Microsoft.EntityFrameworkCore;
using WebApplication1;
using WebApplication1.FileService;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
var dbContext = new FileDbContext(connectionString);
builder.Services.AddScoped<FileDbContext>(_ => dbContext);
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<FileDbContext>(
  
    optionsBuilder => optionsBuilder.UseNpgsql(connectionString)
);

builder.Services.AddScoped<IFileService>(x => new FileService(
        context: dbContext,
        directory: builder.Configuration.GetSection("Directory").Value,
        folderName: builder.Configuration.GetSection("FolderName").Value));


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
