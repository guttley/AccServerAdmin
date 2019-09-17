namespace AccServerAdmin.Infrastructure.IO
{
    /// <summary>
    /// Abstracts the reading/writing of files
    /// </summary>
    public interface IFile
    {
        /// <summary>
        /// Copies the the source to destination
        /// </summary>
        /// <param name="source">Source file path</param>
        /// <param name="destination">Destination file path</param>
        void Copy(string source, string destination);

        /// <summary>
        /// Checks for the existence of the file specified by the path
        /// </summary>
        /// <param name="path">Path to the file to check for existence</param>        
        bool Exists(string path);

        /// <summary>
        /// Reads all the text from a file
        /// </summary>
        /// <param name="path">Path to the file to read</param>
        string ReadAllText(string path);

        /// <summary>
        /// Overwrites all content text to the specified file, overwriting an existing file
        /// </summary>
        /// <param name="path">Path to the file to read</param>
        /// <param name="contents">String content of the file</param>
        void WriteAllText(string path, string contents);
    }
}
