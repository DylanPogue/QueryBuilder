using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using QueryBuilder.Models;

namespace QueryBuilder.Models
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
        public void Dispose()
        {
            connection.Dispose();
        }

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
    }
}
