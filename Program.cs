using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using Helpers.Attributes;
using Models;

namespace ReflectionQueryBuilder
{
    class Program
    {
        static void Main(string[] args)
        {

            var models = Assembly
                .GetAssembly(typeof(ModelBase))
                .GetTypes()
                .Where(t => t.IsSubclassOf(typeof(ModelBase)));

            foreach (var model in models)
            {
                Query query = new Query(model);

                Console.WriteLine("------------------------------------------------------------");
                Console.WriteLine("Model: " + model.Name);
                Console.WriteLine("------------------------------------------------------------\n");
                string queryCreate = query.QueryCreateTable(model);
                Console.WriteLine("--Query para criar a tabela---------------------------------");
                Console.WriteLine(queryCreate);
                Console.WriteLine("------------------------------------------------------------");

                string queryInsert = query.QueryInsert(model);
                Console.WriteLine("--Query para inserir registro-------------------------------");
                Console.WriteLine(queryInsert);
                Console.WriteLine("------------------------------------------------------------");

                string queryUpdate = query.QueryUpdate(model);
                Console.WriteLine("--Query para alterar registro-------------------------------");
                Console.WriteLine(queryUpdate);
                Console.WriteLine("------------------------------------------------------------");

                string queryDelete = query.QueryDelete(model);
                Console.WriteLine("--Query para deletar registro-------------------------------");
                Console.WriteLine(queryDelete);
                Console.WriteLine("------------------------------------------------------------ \n\n\n");
            }

        }
    }
}
