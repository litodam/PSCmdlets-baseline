namespace BasicSample.DomainServices.Interfaces
{
    using System.Collections.Generic;

    public interface IFileFinder
    {
        IEnumerable<IFileData> GetFileDataByFilePath(string filePath);

        IEnumerable<IFileData> GetFileDataByFolderAndPattern(string folder, string pattern);
    }
}