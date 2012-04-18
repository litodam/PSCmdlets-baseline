namespace BasicSample.PowerShell.Tests.Helpers
{
    using System;
    using BasicSample.PowerShell.Infrastructure;

    [Serializable]
    public class CmdletContext : ICmdletContext
    {
        public object Channel { get; set; }
    }
}
