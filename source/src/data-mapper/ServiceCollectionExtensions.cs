namespace Buhler.DataMapper
{
    using Buhler.DataMapper.Helper;
    using Buhler.DataMapper.Validation;
    using Microsoft.Extensions.DependencyInjection;

    public static class ServiceCollectionExtensions
    {
        public static void AddDataMapper(this IServiceCollection services)
        {
            services.AddScoped<IMapper, Mapper>();
            services.AddScoped<IStreamHelper, StreamHelper>();
            services.AddScoped<IFieldValidation, FieldValidation>();
        }
    }
}
