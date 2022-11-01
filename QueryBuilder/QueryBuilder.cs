using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using QueryBuilder.Models;

namespace QueryBuilder
{
    public class QueryBuilder : IDisposable
    {
        // db connection referenced by the 'connection' field
        private SqliteConnection connection;

        /// <summary>
        /// Constructor will set up our connection to a given SQLite database file and open it.
        /// </summary>
        /// <param name="databaseLocation">File path to a .db file</param>
        public QueryBuilder(string databaseLocation)
        {
            connection = new SqliteConnection("Data Source=" + databaseLocation);
            connection.Open();
        }

        /// <summary>
        /// By implementing IDisposable, we have the capability to 
        /// use a QueryBuilder object in a 'using' statement in our
        /// driver; when that using statement is complete, our Sqlite
        /// connection will be closed automatically
        /// </summary>

        public List<T> ReadAll<T>() where T : IClassModel, new()
        {
            var command = connection.CreateCommand();
            command.CommandText = $"SELECT * FROM {typeof(T).Name}";
            var reader = command.ExecuteReader();
            T data;
            var datas = new List<T>();
            while (reader.Read())
            {
                data = new T();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    if (typeof(T).GetProperty(reader.GetName(i)).PropertyType == typeof(int))
                        typeof(T).GetProperty(reader.GetName(i)).SetValue(data, Convert.ToInt32(reader.GetValue(i)));
                    else
                        typeof(T).GetProperty(reader.GetName(i)).SetValue(data, reader.GetValue(i));
                }
                datas.Add(data);
            }
            return datas;
        }

        /*public T Read<T>(int id) where T : IClassModel, new()
        {
            var command = connection.CreateCommand();
            command.CommandText = $"SELECT * FROM {typeof(T).Name}";
            var reader = command.ExecuteReader();
            T data;
            
        }*/

        //Create
        public void Create<T>(T obj)
        {
            PropertyInfo[] properties = typeof(T).GetProperties();

            List<string> values = new List<string>();
            List<string> names = new List<string>();

            foreach(PropertyInfo property in properties)
            {
                if (property.PropertyType == typeof(string))
                {
                    values.Add("\"" + property.GetValue(obj) + "\"");
                }
                else
                    values.Add(property.GetValue(obj).ToString());

                names.Add(property.Name);
            }

            StringBuilder sbValues = new StringBuilder();
            StringBuilder sbNames = new StringBuilder();

            for(int i = 0; i < values.Count; i++)
            {
                if(i == values.Count - 1)
                {
                    sbValues.Append($"{values[i]}");
                    sbNames.Append($"{names[i]}");
                }
                else
                {
                    sbValues.Append($"{values[i]}, ");
                    sbNames.Append($"{names[i]}, ");
                }
            }

            var command = connection.CreateCommand();
            command.CommandText = $"INSERT INTO {typeof(T).Name} ({sbNames}) Values ({sbValues})";

            var insert = command.ExecuteNonQuery();


        }

        //Update
        public void Update<T>(T obj) where T : IClassModel
        {
            PropertyInfo[] properties = typeof(T).GetProperties();

            List<string> values = new List<string>();
            List<string> names = new List<string>();

            foreach (PropertyInfo property in properties)
            {
                if (property.PropertyType == typeof(string))
                {
                    values.Add("\"" + property.GetValue(obj) + "\"");
                }
                else
                    values.Add(property.GetValue(obj).ToString());

                names.Add(property.Name);
            }

            StringBuilder sbValues = new StringBuilder();
            StringBuilder sbNames = new StringBuilder();

            for (int i = 0; i < values.Count; i++)
            {
                if (i == values.Count - 1)
                {
                    sbValues.Append($"{properties[i].Name} = {values[i]}");
                }
                else
                {
                    sbValues.Append($"{properties[i].Name} = {values[i]}, ");
                }
            }

            var command = connection.CreateCommand();
           
            command.CommandText = $"UPDATE {typeof(T).Name} SET ({sbValues}) WHERE Id = {obj.Id}";

            var update = command.ExecuteNonQuery();


        }

        //Delete
        public void Delete<T>(T obj) where T : IClassModel
        {
            var command = connection.CreateCommand();
            command.CommandText = $"DELETE FROM {typeof(T).Name} WHERE Id = {obj.Id}";

            var delete = command.ExecuteNonQuery();
        }

        public void Dispose()
        {
            connection.Dispose();
        }
    }
}
