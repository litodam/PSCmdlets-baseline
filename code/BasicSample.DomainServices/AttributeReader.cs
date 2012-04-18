namespace BasicSample.DomainServices
{
    using System;
    using System.IO;

    using BasicSample.DomainServices.Interfaces;

    public class AttributeReader : IAttributeReader
    {
        public IAttributeData GetAttributeDataByFilePath(string filePath, string attribute)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException(string.Format("File not found: {0}", filePath));
            }

            var nfo = new FileInfo(filePath);
            return GetAttribute(nfo, attribute);
        }

        public IAttributeData GetAttributeDataByFileInfo(FileInfo fileInfo, string attribute)
        {
            return GetAttribute(fileInfo, attribute);
        }

        private static IAttributeData GetAttribute(FileInfo nfo, string attribute)
        {
            if (attribute.Equals("Size", StringComparison.OrdinalIgnoreCase))
            {
                return new AttributeData()
                    {
                        File = nfo.FullName,
                        AttributeName = "Size",
                        AttributeValue = nfo.Length
                    };
            }
            else if (attribute.Equals("IsReadOnly", StringComparison.OrdinalIgnoreCase))
            {
                return new AttributeData()
                    {
                        File = nfo.FullName,
                        AttributeName = "IsReadOnly",
                        AttributeValue = nfo.IsReadOnly
                    };
            }
            else
            {
                // should never get here
                throw new NotImplementedException();
            }
        }
    }
}