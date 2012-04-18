namespace BasicSample.PowerShell
{
    using System;

    /// <summary>
    /// Result object for the Get-Attribute cmdlet. Used for storing the name and value of a file's attribute.
    /// </summary>
    public class GetAttributeResult : IComparable
    {
        /// <summary>
        /// Attribute's file path
        /// </summary>
        public string File { get; set; }

        /// <summary>
        /// Name of the attribute
        /// </summary>
        public string AttributeName { get; set; }

        /// <summary>
        /// Value of the attribute
        /// </summary>
        public object AttributeValue { get; set; }

        public int CompareTo(object obj)
        {
            if (obj is GetAttributeResult)
            {
                var obj2 = obj as GetAttributeResult;

                if (obj2.AttributeName.Equals(this.AttributeName))
                {
                    if (obj2.AttributeName.Equals("Size", StringComparison.OrdinalIgnoreCase))
                    {
                        return Convert.ToInt32(obj2.AttributeValue).CompareTo(Convert.ToInt32(this.AttributeValue));
                    }
                    else if (obj2.AttributeName.Equals("IsReadOnly", StringComparison.OrdinalIgnoreCase))
                    {
                        return Convert.ToBoolean(obj2.AttributeValue).CompareTo(Convert.ToBoolean(this.AttributeValue));
                    }
                }
        
                throw new Exception("Can't compare");
            }

            throw new ArgumentException("Object is not of type GetFileResult");
        }
    }
}