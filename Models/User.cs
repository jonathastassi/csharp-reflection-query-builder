using Helpers.Attributes;

namespace Models
{
    public class User : ModelBase
    {
        public string Name { get; set; }
        public string Email { get; set; }
    }
}