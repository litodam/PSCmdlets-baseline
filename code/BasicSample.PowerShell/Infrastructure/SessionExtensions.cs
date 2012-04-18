namespace BasicSample.PowerShell.Infrastructure
{
    using System.Collections.Generic;
    using System.Management.Automation;

    public static class SessionExtensions
    {
        private const string DefaultSessionName = "__default";
        private const string CurrentSessionNameIdentifier = "__currentSessionNameId";
        private const string SessionPropertySetsIdentifier = "__sessionPropertySetsId";
        
        public static SessionPropertySet GetPropertySet(this PSCmdlet cmdlet)
        {
            string sessionName = DefaultSessionName;

            PSVariable psvarSessionName = cmdlet.SessionState.PSVariable.Get(CurrentSessionNameIdentifier);
            if (psvarSessionName != null)
            {
                sessionName = psvarSessionName.Value as string;
            }

            return GetPropertySet(cmdlet, sessionName);
        }

        public static SessionPropertySet GetPropertySet(this PSCmdlet cmdlet, string sessionName)
        {
            Dictionary<string, SessionPropertySet> propertySets = GetPropertySets(cmdlet);

            if (sessionName == null)
            {
                sessionName = DefaultSessionName;
            }

            SessionPropertySet sessionPropertySet;
            string key = sessionName.ToUpperInvariant();
            if (!propertySets.TryGetValue(key, out sessionPropertySet))
            {
                sessionPropertySet = new SessionPropertySet()
                {
                    SessionName = sessionName
                };

                propertySets.Add(key, sessionPropertySet);
            }

            return sessionPropertySet;
        }

        public static Dictionary<string, SessionPropertySet> GetPropertySets(this PSCmdlet cmdlet)
        {
            Dictionary<string, SessionPropertySet> propertySets = null;
            PSVariable psvarPropertySets = cmdlet.SessionState.PSVariable.Get(SessionPropertySetsIdentifier);
            if (psvarPropertySets != null)
            {
                propertySets = psvarPropertySets.Value as Dictionary<string, SessionPropertySet>;
            }

            if (propertySets == null)
            {
                propertySets = new Dictionary<string, SessionPropertySet>();
                cmdlet.SessionState.PSVariable.Set(SessionPropertySetsIdentifier, propertySets);
                propertySets.Add(DefaultSessionName.ToUpperInvariant(), new SessionPropertySet() { SessionName = DefaultSessionName });
            }

            return propertySets;
        }
    }
}
