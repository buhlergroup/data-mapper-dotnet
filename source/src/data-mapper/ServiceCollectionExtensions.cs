namespace Buhlergroup.DataMapper
{
    using System.IO.Abstractions;
    using Buhlergroup.DataMapper.Helper;
    using Buhlergroup.DataMapper.Validation;
    using Microsoft.Extensions.DependencyInjection;

    public static class ServiceCollectionExtensions
    {
        public static void AddDataMapper(this IServiceCollection services)
        {
            services.AddScoped<IMapper, Mapper>();
            services.AddScoped<IStreamHelper, StreamHelper>();
            services.AddScoped<IFieldValidation, FieldValidation>();
            services.AddScoped<IFileSystem, FileSystem>();
        }
    }
}
