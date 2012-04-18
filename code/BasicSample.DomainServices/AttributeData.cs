namespace BasicSample.DomainServices
{
    using BasicSample.DomainServices.Interfaces;

    public class AttributeData : IAttributeData
    {
        public string File { get; set; }

        public string AttributeName { get; set; }

        public object AttributeValue { get; set; }
    }
}