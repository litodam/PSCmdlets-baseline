namespace BasicSample.PowerShell.Domain
{
    using BasicSample.DomainServices;
    using BasicSample.DomainServices.Interfaces;

    public class ServicesDefaultFactory : ServicesAbstractFactory
    {
        public override IFileFinder GetFileFinder()
        {
            return new FileFinder();
        }

        public override IAttributeReader GetAttributeReader()
        {
            return new AttributeReader();
        }
    }
}