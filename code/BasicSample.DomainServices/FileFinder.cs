namespace BasicSample.DomainServices
{
    using System.Collections.Generic;
    using System.IO;

    using BasicSample.DomainServices.Interfaces;

    public class FileFinder : IFileFinder
    {
        public IEnumerable<IFileData> GetFileDataByFilePath(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException(string.Format("File not found: {0}", filePath));
            }

            return new List<IFileData>
                {
                    new FileData { FileNfo = new FileInfo(filePath) }
                };
        }

        public IEnumerable<IFileData> GetFileDataByFolderAndPattern(string folder, string pattern)
        {
            if (!Directory.Exists(folder))
            {
                throw new DirectoryNotFoundException(string.Format("Folder not found: {0}", folder));
            }

            var result = new List<IFileData>();
            foreach (var file in Directory.GetFiles(folder, (string.IsNullOrEmpty(pattern) ? "*" : pattern)))
            {
                result.Add(new FileData { FileNfo = new FileInfo(file) });
            }

            return result;
        }
    }
}