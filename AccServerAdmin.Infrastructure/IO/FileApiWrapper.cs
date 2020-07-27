using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;

namespace AccServerAdmin.Infrastructure.IO
{
    /// <summary>
    /// Wrapper for the .net file functions to allow for unit testing
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class FileApiWrapper : IFile
    {
        /// <inheritdoc/>
        public void Copy(string source, string destination)
        {
            File.Copy(source, destination, true);
        }

        /// <inheritdoc/>
        public bool Exists(string path)
        {
            return File.Exists(path);
        }

        /// <inheritdoc/>
        public void Delete(string path)
        {
            File.Delete(path);
        }

        /// <inheritdoc/>
        public string ReadAllText(string path)
        {
            return File.ReadAllText(path, Encoding.Unicode);
        }

        /// <inheritdoc/>
        public void WriteAllText(string path, string contents)
        {
            File.WriteAllText(path, contents, Encoding.Unicode);
        }
    }
}
