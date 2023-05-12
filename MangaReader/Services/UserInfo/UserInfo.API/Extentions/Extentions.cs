namespace UserInfo.API.Extentions
{
    public static class Extentions
    {
        public static IServiceCollection ConfigureReddisDataBase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration.GetValue<string>("CacheSettings:ConnectionString");
            });

            return services;
        }
    }
}
