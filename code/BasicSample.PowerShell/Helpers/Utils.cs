namespace BasicSample.PowerShell.Helpers
{
    using System;

    public class Utils
    {
        public static BaseFactoryType CreateConcreteFactoryInstance<BaseFactoryType>() where BaseFactoryType : class
        {
            var assembly = typeof(BaseFactoryType).Assembly;

            foreach (var t in assembly.GetTypes())
            {
                if ((t.BaseType != null) && t.BaseType.Equals(typeof(BaseFactoryType)))
                {
                    return (BaseFactoryType)Activator.CreateInstance(t);
                }
            }

            return null;
        }
    }
}
