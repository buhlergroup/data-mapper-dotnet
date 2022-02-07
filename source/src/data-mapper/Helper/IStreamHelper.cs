namespace Buhlergroup.DataMapper.Helper
{
    using System.IO;
    using System.Threading.Tasks;

    public interface IStreamHelper
    {
        /// <summary>
        /// Reads a stream into a string asynchronous
        /// </summary>
        /// <param name="stream">to read</param>
        /// <returns>Stream content as a string</returns>
        Task<string> ToStringAsync(Stream stream);

        /// <summary>
        /// Reads a file to a string
        /// </summary>
        /// <param name="filename">to read from</param>
        /// <returns>Content of the file</returns>
        string ReadFileToString(string filename);
    }
}
