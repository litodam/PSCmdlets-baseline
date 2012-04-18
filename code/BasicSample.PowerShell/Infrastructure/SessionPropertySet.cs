namespace BasicSample.PowerShell.Infrastructure
{
    using System.Collections.Generic;

    public class SessionPropertySet : Dictionary<string, object>
    {
        public string SessionName { get; set; }
    }
}