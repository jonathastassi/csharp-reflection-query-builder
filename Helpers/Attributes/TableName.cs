namespace Helpers.Attributes
{
    [System.AttributeUsage(System.AttributeTargets.Class) ]
    public class TableName : System.Attribute
    {
        private string tableName;

        public TableName(string tableName)
        {
            this.tableName = tableName;
        }

        public string GetName()
        {
            return this.tableName;
        }
    }
}