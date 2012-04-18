namespace BasicSample.DomainServices.Interfaces
{
    public interface IAttributeData
    {
        string File { get; set; }

        string AttributeName { get; set; }

        object AttributeValue { get; set; }
    }
}