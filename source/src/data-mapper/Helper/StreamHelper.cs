namespace Buhler.DataMapper.Helper
{
    using System;
    using System.IO;
    using System.IO.Abstractions;
    using System.Threading.Tasks;

    public class StreamHelper : IStreamHelper
    {
        public IFileSystem _fileSystem;

        public StreamHelper(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }

        public async Task<string> ToStringAsync(Stream stream)
        {
            RequireArgument(stream);
            using var reader = new StreamReader(stream);
            return await reader.ReadToEndAsync().ConfigureAwait(false);
        }

        public string ReadFileToString(string filename)
        {
            RequireArgument(filename);
            return _fileSystem.File.ReadAllText(filename);
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
