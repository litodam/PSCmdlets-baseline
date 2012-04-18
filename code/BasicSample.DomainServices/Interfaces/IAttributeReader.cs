namespace BasicSample.DomainServices.Interfaces
{
    using System.IO;

    public interface IAttributeReader
    {
        IAttributeData GetAttributeDataByFilePath(string filePath, string attribute);

        IAttributeData GetAttributeDataByFileInfo(FileInfo fileInfo, string attribute);
    }
}