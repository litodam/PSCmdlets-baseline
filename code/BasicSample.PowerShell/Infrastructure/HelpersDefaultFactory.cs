namespace BasicSample.PowerShell.Infrastructure
{
    using BasicSample.PowerShell.Infrastructure;

    public class HelpersDefaultFactory : HelpersAbstractFactory
    {
        public override ICmdletLogger GetLogger()
        {
            return new CmdletLogger();
        }
    }
}