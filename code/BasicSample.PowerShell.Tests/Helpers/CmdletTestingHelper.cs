namespace BasicSample.PowerShell.Tests.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Globalization;
    using System.Management.Automation;
    using System.Management.Automation.Runspaces;
    using System.Runtime.Remoting.Messaging;
    using BasicSample.PowerShell.Infrastructure;

    public static class CmdletTestingHelper
    {
        private static RunspaceConfiguration runspaceConfig = RunspaceConfiguration.Create();

        public static Collection<PSObject> RunCmdlet(Dictionary<string, Type> cmdletsRegistration, string command)
        {
            return RunCmdlet(cmdletsRegistration, command, null);
        }

        public static Collection<PSObject> RunCmdlet(Dictionary<string, Type> cmdletsRegistration, string command, CmdletContext context)
        {
            return RunCmdletWithErrors(cmdletsRegistration, command, context).Result;
        }

        public static CmdletResult RunCmdletWithErrors(Dictionary<string, Type> cmdletsRegistration, string command)
        {
            return RunCmdletWithErrors(cmdletsRegistration, command, null);
        }

        public static CmdletResult RunCmdletWithErrors(Dictionary<string, Type> cmdletsRegistration, string command, CmdletContext context)
        {
            // Register cmdlets
            runspaceConfig.Cmdlets.Reset();
            foreach (var cmdlet in cmdletsRegistration)
            {
                var cmdletName = cmdlet.Key;
                var cmdletType = cmdlet.Value;

                runspaceConfig.Cmdlets.Append(
                    new CmdletConfigurationEntry(
                        cmdletName,
                        cmdletType,
                        string.Empty));
            }

            using (Runspace runspace = RunspaceFactory.CreateRunspace(runspaceConfig))
            {
                AddContextToCallContext(context);
                runspace.Open();
                using (Pipeline pipeline = runspace.CreatePipeline(command))
                {
                    var cmdletResult = new CmdletResult()
                    {
                        Result = pipeline.Invoke(),
                        Errors = pipeline.Error.ReadToEnd(),
                    };

                    RemoveContextFromCallContext(context);

                    return cmdletResult;
                }
            }
        }

        public static Collection<PSObject> RunCmdlet(string cmdletName, string cmdletParameters, Type cmdletImplementationType)
        {
            return RunCmdlet(cmdletName, cmdletParameters, cmdletImplementationType, null);
        }

        public static Collection<PSObject> RunCmdlet(string cmdletName, string cmdletParameters, Type cmdletImplementationType, CmdletContext context)
        {
            return RunCmdletWithErrors(cmdletName, cmdletParameters, cmdletImplementationType, context).Result;
        }

        public static CmdletResult RunCmdletWithErrors(string cmdletName, string cmdletParameters, Type cmdletImplementationType)
        {
            return RunCmdletWithErrors(cmdletName, cmdletParameters, cmdletImplementationType, null);
        }

        public static CmdletResult RunCmdletWithErrors(string cmdletName, string cmdletParameters, Type cmdletImplementationType, CmdletContext context)
        {
            var command = string.Format(CultureInfo.InvariantCulture, "{0} {1}", cmdletName, cmdletParameters);

            runspaceConfig.Cmdlets.Reset();
            runspaceConfig.Cmdlets.Append(
                new CmdletConfigurationEntry(
                    cmdletName,
                    cmdletImplementationType,
                    string.Empty));

            using (Runspace runspace = RunspaceFactory.CreateRunspace(runspaceConfig))
            {
                AddContextToCallContext(context);
                runspace.Open();
                using (Pipeline pipeline = runspace.CreatePipeline(command))
                {
                    var cmdletResult = new CmdletResult()
                    {
                        Result = pipeline.Invoke(),
                        Errors = pipeline.Error.ReadToEnd(),
                    };

                    RemoveContextFromCallContext(context);

                    return cmdletResult;
                }
            }
        }

        public static Collection<PSObject> RunCmdlet(Command command, Type cmdletImplementationType, params string[] scripts)
        {
            return RunCmdlet(command, cmdletImplementationType, null, scripts);
        }

        public static Collection<PSObject> RunCmdlet(Command command, Type cmdletImplementationType, CmdletContext context, params string[] scripts)
        {
            return RunCmdletWithErrors(command, cmdletImplementationType, context, scripts).Result;
        }

        public static CmdletResult RunCmdletWithErrors(Command command, Type cmdletImplementationType, params string[] scripts)
        {
            return RunCmdletWithErrors(command, cmdletImplementationType, null, scripts);
        }

        public static CmdletResult RunCmdletWithErrors(Command command, Type cmdletImplementationType, CmdletContext context, params string[] scripts)
        {
            runspaceConfig.Cmdlets.Reset();
            runspaceConfig.Cmdlets.Append(
                new CmdletConfigurationEntry(
                    command.CommandText,
                    cmdletImplementationType,
                    string.Empty));

            using (Runspace runspace = RunspaceFactory.CreateRunspace(runspaceConfig))
            {
                AddContextToCallContext(context);
                runspace.Open();
                using (Pipeline pipeline = runspace.CreatePipeline())
                {
                    foreach (var script in scripts)
                    {
                        pipeline.Commands.AddScript(script);
                    }

                    pipeline.Commands.Add(command);

                    var cmdletResult = new CmdletResult()
                    {
                        Result = pipeline.Invoke(),
                        Errors = pipeline.Error.ReadToEnd(),
                    };

                    RemoveContextFromCallContext(context);

                    return cmdletResult;
                }
            }
        }

        private static void AddContextToCallContext(ICmdletContext context)
        {
            if (context != null)
            {
                CallContext.SetData("TestContext", context);
            }
        }

        private static void RemoveContextFromCallContext(ICmdletContext context)
        {
            if (context != null)
            {
                CallContext.SetData("TestContext", null);
            }
        }
    }
}