using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ZSCourse.ListeningService;

public class ListeningServiceInitializer
{
    public static void Init(WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<LSDbContext>(options =>
        {
            var dbconfig = builder.Configuration.GetSection("Database");
            string connStr = dbconfig.GetSection("ConnStr").Value;
            options.UseNpgsql(connStr);
        });
    }
}
