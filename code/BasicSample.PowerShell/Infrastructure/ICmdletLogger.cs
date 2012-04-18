namespace BasicSample.PowerShell.Infrastructure
{
    using System.Management.Automation;

    public interface ICmdletLogger
    {
        void LogGenericEvent(string eventMessage, PSCmdlet cmdlet);

        void LogParameterSetResolvedEvent(string parameterSetName, PSCmdlet cmdlet);
    }
}