namespace BasicSample.PowerShell.Tests.Helpers
{
    using System.Collections.ObjectModel;
    using System.Management.Automation;

    public class CmdletResult
    {
        public Collection<PSObject> Result { get; set; }

        public Collection<object> Errors { get; set; }
    }
}
