using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace AccServerAdmin.Infrastructure.IO
{
    /// <summary>
    /// Wrapper for the .net directory functions to allow for unit testing
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class DirectoryApiWrapper : IDirectory
    {
        /// <inheritdoc />
        public bool Exists(string path)
        {
            return Directory.Exists(path);
        }

        /// <inheritdoc />
        public void CreateDirectory(string path)
        {
            Directory.CreateDirectory(path);
        }

        /// <inheritdoc />
        public void Delete(string path, bool recursive)
        {
            Directory.Delete(path, recursive);
        }

        /// <inheritdoc />
        public IEnumerable<string> GetDirectories(string path)
        {
            return Directory.GetDirectories(path);
        }

        /// <inheritdoc />
        public IEnumerable<string> GetFiles(string path)
        {
            return Directory.GetFiles(path);
        }

    }
}
