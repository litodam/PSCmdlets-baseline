namespace BasicSample.PowerShell
{
    using System;
    using System.IO;

    /// <summary>
    /// Result object for the Get-File cmdlet. Used for storing information of a particular file (using the FileInfo class).
    /// </summary>
    public class GetFileResult : IComparable
    {
        /// <summary>
        /// File information
        /// </summary>
        public FileInfo FileNfo { get; set; }

        public int CompareTo(object obj)
        {
            if (obj is GetFileResult)
            {
                var obj2 = obj as GetFileResult;

                return this.FileNfo.FullName.CompareTo(obj2.FileNfo.FullName);
            }

            throw new ArgumentException("Object is not of type GetFileResult");
        }
    }
}
