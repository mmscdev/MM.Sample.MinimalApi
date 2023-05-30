using System.Data.SqlClient;

namespace MM.MinimalApi.Package.Extensions
{
    public static class DapperExtensions
    {
        public static WebApplicationBuilder AddDapper(this  WebApplicationBuilder builder)
        {
            builder.Services.AddScoped(_ => new SqlConnection(builder.Configuration.GetConnectionString("sql")));
            return builder;
        }
    }
}
