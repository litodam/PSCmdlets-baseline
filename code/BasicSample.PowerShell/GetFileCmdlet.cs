namespace BasicSample.PowerShell
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Management.Automation;
    using BasicSample.DomainServices.Interfaces;
    using BasicSample.PowerShell.Domain;

    [Cmdlet(VerbsCommon.Get, "File", DefaultParameterSetName = "defaultParameterSet"), OutputType(typeof(GetFileResult))]
    public class GetFileCmdlet : CmdletBase
    {
        [Parameter(Mandatory = false, ValueFromPipelineByPropertyName = true, ParameterSetName = "defaultParameterSet",
            HelpMessage = "Pattern of the file or files to retrieve, if empty gets all files in the specified folder.")]
        [ValidateNotNullOrEmpty]
        public string Pattern
        {
            get;
            set;
        }
        
        [Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true, ParameterSetName = "defaultParameterSet",
            HelpMessage = "Folder where the files are located.")]
        [ValidateNotNullOrEmpty]
        public string[] Folder
        {
            get;
            set;
        }

        [Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true, ParameterSetName = "filePathParameterSet",
            HelpMessage = "Path and name of the exact file to retrieve.")]
        [ValidateNotNullOrEmpty]
        [Alias("FilePath")]
        public string[] File
        {
            get;
            set;
        }

        protected override void ProcessRecord()
        {
            try
            {
                base.ProcessRecord();

                var files = this.GetFileProcess();
                WriteObject(files, true);
            }
            catch (Exception ex)
            {
                WriteError(new ErrorRecord(ex, string.Empty, ErrorCategory.NotSpecified, null));
            }
        }

        private IEnumerable<GetFileResult> GetFileProcess()
        {
            try
            {
                var fileFinder = ServicesAbstractFactory.Instance.GetFileFinder();
                var result = new List<IFileData>();

                if (this.ParameterSetName.Equals("defaultParameterSet"))
                {
                    foreach (var folder in this.Folder)
                    {
                        this.WriteVerbose(string.Format("Retrieving files from folder '{0}', using pattern '{1}'", folder, this.Pattern));

                        result.AddRange(fileFinder.GetFileDataByFolderAndPattern(folder, this.Pattern));
                    }
                }
                else
                {
                    foreach (var file in this.File)
                    {
                        this.WriteVerbose(string.Format("Retrieving file '{0}'", file));

                        result.AddRange(fileFinder.GetFileDataByFilePath(file));
                    }
                }

                // in order to avoid duplicating structures, directly exposing the domain entities should be considered
                return result.Select(item => new GetFileResult { FileNfo = item.FileNfo });
            }
            catch (Exception ex)
            {
                if (ex is DirectoryNotFoundException || ex is FileNotFoundException)
                {
                    this.WriteError(new ErrorRecord(ex, string.Empty, ErrorCategory.InvalidArgument, null));
                }
                else
                {
                    this.ThrowTerminatingError(new ErrorRecord(ex, string.Empty, ErrorCategory.NotSpecified, null));
                }

                return new List<GetFileResult>();
            }
        }
    }
}
