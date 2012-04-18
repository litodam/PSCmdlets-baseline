namespace BasicSample.PowerShell.Infrastructure
{
    using System.Management.Automation;
    using BasicSample.PowerShell.Infrastructure;

    public class CmdletLogger : ICmdletLogger
    {
        public void LogGenericEvent(string eventMessage, PSCmdlet cmdlet)
        {
            cmdlet.WriteVerbose(eventMessage);
        }

        public void LogParameterSetResolvedEvent(string parameterSetName, PSCmdlet cmdlet)
        {
            cmdlet.WriteDebug(string.Format("ParameterSet was resolved to '{0}'", parameterSetName));
        }
    }
}