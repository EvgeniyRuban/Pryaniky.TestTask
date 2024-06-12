using Microsoft.EntityFrameworkCore;
using Pryaniky.TestTask.DAL;

namespace Pryaniky.TestTask.API.Extentions
{
    public static class ServiceCollectionExtentions
    {
        public static void ConfigureDbContext(this IServiceCollection configuration, WebApplicationBuilder builder)
        {
            var connectionString = $"Data Source={builder.Environment.ContentRootPath}{builder.Configuration["DBLocalPath"]};";
            builder.Services.AddDbContext<ApplicationContext>(options =>
            {
                options.UseSqlite(connectionString);
            });
        }
    }
}
