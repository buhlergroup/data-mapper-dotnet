namespace Buhler.DataMapper.Helper
{
    using System;
    using System.IO;
    using System.Threading.Tasks;

    public class StreamHelper : IStreamHelper
    {
        public async Task<string> ToStringAsync(Stream stream)
        {
            RequireArgument(stream);
            using var reader = new StreamReader(stream);
            return await reader.ReadToEndAsync().ConfigureAwait(false);
        }

        public async Task<string> FileToStringAsync(string filename)
        {
            RequireArgument(filename);
            using var reader = new StreamReader(filename);
            return await reader.ReadToEndAsync().ConfigureAwait(false);
        }

        private static void RequireArgument(object stream)
        {
            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream));
            }
        }
    }
}
