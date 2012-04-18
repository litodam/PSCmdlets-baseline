namespace BasicSample.PowerShell.Infrastructure
{
    using BasicSample.PowerShell.Helpers;

    public abstract class HelpersAbstractFactory
    {
        private static HelpersAbstractFactory instance;

        public static HelpersAbstractFactory Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = Utils.CreateConcreteFactoryInstance<HelpersAbstractFactory>();
                }

                return instance;
            }

            set
            {
                instance = value;
            }
        }

        public abstract ICmdletLogger GetLogger();
    }
}
