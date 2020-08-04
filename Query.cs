using System;
using System.Linq;
using System.Reflection;
using Helpers.Attributes;

namespace ReflectionQueryBuilder
{
    public class Query
    {
        public Type model { get; private set; }

        public Query(Type model)
        {
            this.model = model;
        }

        private string GetTableName(Type model)
        {
            TableName attr = model.GetCustomAttribute<TableName>();
            if (attr == null)
                return model.Name;

            return attr.GetName();
        }

        private string GetTypeDatabase(Type type)
        {
            if (type.Name == "String")
                return "varchar(200)";

            if (type.Name == "Int32")
            {
                return "int";
            }


            return "varchar(50)";
        }

        private string IsIdentity(PropertyInfo property)
        {
            if (property.Name.ToUpper() == "ID" && property.PropertyType.Name == "Int32")
            {
                return "identity";
            }
            return "";
        }

        private string GetFields(Type model, string prefix = "")
        {
            string fields = string.Empty;
            model.GetProperties().ToList().ForEach(property =>
            {
                if (property.Name != "Id")
                    fields += $"{prefix}{property.Name},";
            });

            fields = fields.Substring(0, fields.Length - 1);

            return fields;
        }

        public string QueryCreateTable(Type model)
        {
            string tableName = GetTableName(model);

            var properties = model.GetProperties();
            string fields = "";
            foreach (var property in properties)
            {
                fields += $"        {property.Name} { GetTypeDatabase(property.PropertyType)} {IsIdentity(property)}, \n";
            }

            string query = $@"create table {tableName} {{ 
                              {fields}
                            }}";

            return query.ToUpper();
        }

        public string QueryInsert(Type model)
        {
            string query = $@"insert into {GetTableName(model)} ({GetFields(model)}) values ({GetFields(model, "@")})";
            return query.ToUpper();
        }        

        public string QueryUpdate(Type model)
        {
            string fields = string.Empty;
            
            var fieldsProp = model.GetProperties();
            for (int i = 0; i < fieldsProp.Length; i++)
            {
                if (fieldsProp[i].Name!= "Id")
                    fields += $"{fieldsProp[i].Name} = @{fieldsProp[i].Name},";                
            }
            fields = fields.Substring(0, fields.Length - 1);

            string query = $@"update {GetTableName(model)} set {fields} where id = @ID";
            
            return query.ToUpper();
        }

        public string QueryDelete(Type model)
        {
            return $"delete from {GetTableName(model)} where id = @id";
        }
    }
}