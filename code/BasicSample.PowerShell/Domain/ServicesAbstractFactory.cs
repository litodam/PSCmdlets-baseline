namespace BasicSample.PowerShell.Domain
{
    using BasicSample.DomainServices.Interfaces;
    using BasicSample.PowerShell.Helpers;    

    public abstract class ServicesAbstractFactory
    {
        private static ServicesAbstractFactory instance;

        public static ServicesAbstractFactory Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = Utils.CreateConcreteFactoryInstance<ServicesAbstractFactory>();
                }

                return instance;
            }

            set
            {
                instance = value;
            }
        }

        public abstract IFileFinder GetFileFinder();

        public abstract IAttributeReader GetAttributeReader();
    }
}
