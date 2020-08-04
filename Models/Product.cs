using Helpers.Attributes;

namespace Models
{
    [TableName("Products")]
    public class Product : ModelBase
    {
        public string Name { get; set; }
    }
}