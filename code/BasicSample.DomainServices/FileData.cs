namespace BasicSample.DomainServices
{
    using System.IO;

    using BasicSample.DomainServices.Interfaces;

    public class FileData : IFileData
    {
        public FileInfo FileNfo { get; set; }
    }
}