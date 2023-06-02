using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace ZSCourse.FileService;

public class FileServiceInitializer
{
    public static void Init(Microsoft.AspNetCore.Builder.WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<FSDbContext>(options =>
        {
            var dbconfig = builder.Configuration.GetSection("Database");
            string connStr = dbconfig.GetSection("ConnStr").Value;
            options.UseNpgsql(connStr);
        });
        builder.Services.AddScoped<IFileService, FileService>();
    }
}
