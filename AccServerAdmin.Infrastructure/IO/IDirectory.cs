using System;
using System.Collections.Generic;
using System.Text;

namespace AccServerAdmin.Infrastructure.IO
{    
    /// <summary>
    /// Abstracts the directory interface
    /// </summary>
    public interface IDirectory
    {
        /// <summary>
        /// Checks for the existence of the directory specified by the path
        /// </summary>
        /// <param name="path">Path to the directory to check for existence</param>        
        bool Exists(string path);

        /// <summary>
        /// Creates the directory specified
        /// </summary>
        /// <param name="path">Absolute path</param>
        void CreateDirectory(string path);

        /// <summary>
        /// Gets a list of files in a directory
        /// </summary>
        /// <param name="path">Absolute path</param>
        IEnumerable<string> GetFiles(string path);
    }
}
