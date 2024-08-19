using dotenv.net;
using Google.Cloud.Vision.V1;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        DotEnv.Load();
        // Load Google Cloud Vision API credentials from the JSON file
    string credentialsPath = Path.Combine(Directory.GetCurrentDirectory(), "Secrets", "imageprocessingapp-11ff9726346d.json");
        Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", credentialsPath);

        // Add services to the container.
        builder.Services.AddControllersWithViews();

        builder.Services.AddControllersWithViews();
        builder.Services.AddSingleton<ImageAnnotatorClient>(ImageAnnotatorClient.Create());
        builder.Services.AddScoped<IVisionService, VisionService>();
        builder.Services.AddScoped<IImageRepository, ImageRepository>();

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

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=VisionTest}/{action=Index}/{id?}");

        app.Run();
    }
}
