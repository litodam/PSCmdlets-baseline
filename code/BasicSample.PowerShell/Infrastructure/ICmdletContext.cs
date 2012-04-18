namespace BasicSample.PowerShell.Infrastructure
{
    using System.Runtime.Remoting.Messaging;

    public interface ICmdletContext : ILogicalThreadAffinative
    {
        object Channel
        {
            get;
        }
    }
}
