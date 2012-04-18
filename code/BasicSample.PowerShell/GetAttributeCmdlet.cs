namespace BasicSample.PowerShell
{
    using System;
    using System.IO;
    using System.Management.Automation;
    using BasicSample.DomainServices.Interfaces;
    using BasicSample.PowerShell.Domain;    

    [Cmdlet(VerbsCommon.Get, "Attribute", DefaultParameterSetName = "defaultParameterSet"), OutputType(typeof(GetAttributeResult))]
    public class GetAttributeCmdlet : CmdletBase
    {
        [Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true, ParameterSetName = "defaultParameterSet",
            HelpMessage = "Path and name of the exact file to get attributes from.")]
        [ValidateNotNullOrEmpty]
        [Alias("FilePath")]
        public string File
        {
            get;
            set;
        }

        [Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true, ParameterSetName = "defaultParameterSet",
            HelpMessage = "Attribute to retrieve. Size or IsReadOnly.")]
        [Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true, ParameterSetName = "fileInfoParameterSet",
            HelpMessage = "Attribute to retrieve. Size or IsReadOnly.")]
        [ValidateSet(new string[] { "Size", "IsReadOnly" })]
        [ValidateNotNullOrEmpty]
        [Alias("AttributeName")]
        public string Attribute
        {
            get;
            set;
        }

        [Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true, ParameterSetName = "fileInfoParameterSet",
            HelpMessage = "File information")]
        [ValidateNotNullOrEmpty]
        public FileInfo FileNfo
        {
            get;
            set;
        }

        protected override void ProcessRecord()
        {
            try
            {
                base.ProcessRecord();

                var attrib = this.GetAttributeProcess();

                WriteObject(attrib);
            }
            catch (Exception ex)
            {
                WriteError(new ErrorRecord(ex, string.Empty, ErrorCategory.NotSpecified, null));
            }
        }

        private GetAttributeResult GetAttributeProcess()
        {
            try
            {
                var attributeReader = ServicesAbstractFactory.Instance.GetAttributeReader();
                IAttributeData result;

                if (this.ParameterSetName.Equals("defaultParameterSet"))
                {
                    this.WriteVerbose(string.Format("Retrieving '{0}' attribute from file '{1}'", this.Attribute, this.File));

                    result = attributeReader.GetAttributeDataByFilePath(this.File, this.Attribute);
                }
                else
                {
                    this.WriteVerbose(string.Format("Retrieving '{0}' attribute from file '{1}'", this.Attribute, this.FileNfo.FullName));

                    result = attributeReader.GetAttributeDataByFileInfo(this.FileNfo, this.Attribute);
                }

                // in order to avoid duplicating structures, directly exposing the domain entities should be considered
                return new GetAttributeResult { File = result.File, AttributeName = result.AttributeName, AttributeValue = result.AttributeValue };
            }
            catch (Exception ex)
            {
                if (ex is FileNotFoundException)
                {
                    this.WriteError(new ErrorRecord(ex, string.Empty, ErrorCategory.InvalidArgument, null));
                }
                else
                {
                    this.ThrowTerminatingError(new ErrorRecord(ex, string.Empty, ErrorCategory.NotSpecified, null));
                }

                return null;
            }
        }
    }
}
