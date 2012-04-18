namespace BasicSample.PowerShell
{
    using System.Management.Automation;
    using BasicSample.PowerShell.Infrastructure;

    public abstract class CmdletBase : PSCmdlet
    {
        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            var logger = HelpersAbstractFactory.Instance.GetLogger();

            if (logger != null)
            {
                logger.LogParameterSetResolvedEvent(this.ParameterSetName, this);
            }
        }
    }
}