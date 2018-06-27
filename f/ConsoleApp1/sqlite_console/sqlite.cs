using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;

using System.Threading.Tasks;
using System.IO;

namespace sqlite_console
{
    class sqlite
    {


        static void create_db(string dbpath)
        {
            if (!File.Exists(dbpath))
            {
                SQLiteConnection.CreateFile(dbpath);

            }
            using (SQLiteConnection db = new SQLiteConnection(@"Data Source=D:/cache/quora.sqlite;Version=3"))
            {
                db.Open();

                SQLiteCommand command = new SQLiteCommand("create table quora (name varchar(20), link varchar(200));", db);

                command.ExecuteNonQuery();


            }
        }

        static List<T> query_get_names_and_link<T>()
        {
            List<T> output = new List<T>();
            using (SQLiteConnection db = new SQLiteConnection(@"Data Source=D:/cache/quora.sqlite;Version=3"))
            {
                db.Open();
                SQLiteCommand command = new SQLiteCommand(@"select * from quora", db);
                SQLiteDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {

                    while (reader.Read())
                    {
                        output.Add(  (T) Convert.ChangeType( (reader["name"] + "," + reader["link"]).ToString(), typeof(T)   ));
                    }
                }
            }
            return output;
        }

        static List<T> query_get_distinct_names<T>()
        {
            List<T> output = new List<T>();
            using (SQLiteConnection db = new SQLiteConnection(@"Data Source=D:/cache/quora.sqlite;Version=3"))
            {
                db.Open();
                SQLiteCommand command = new SQLiteCommand(@"select distinct name from quora", db);
                SQLiteDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {

                    while (reader.Read())
                    {
                        output.Add((T)Convert.ChangeType(reader["name"].ToString(), typeof(T)));
                    }
                }
            }
            return output;
        }

        static void Log(string data)
        {
            Console.WriteLine(DateTime.Now.ToString().ToUpper().PadRight(20) +" " + data);
            //Console.WriteLine(
            //    DateTime.Now.ToString().ToUpper().PadRight(20) + "\t" + (data.PadRight(Console.BufferWidth - data.Length)).Replace(Environment.NewLine, "")

            //    );

        }
        static void Print<T>(List<T> list)
        {
            foreach (var line in list)
            {
                Log(line.ToString());
            }
            Log("Total records: "+list.Count());
        }
        public static void getColumnNames()
        {
            using (SQLiteConnection db = new SQLiteConnection(@"Data Source=D:/cache/quora.sqlite;Version=3"))
            {
                db.Open();
                var cmd = new SQLiteCommand("select * from quora", db);
                var dr = cmd.ExecuteReader();
                for (var i = 0; i < dr.FieldCount; i++)
                {
                    Console.WriteLine(dr.GetName(i));
                }
            }
        }

        static void Main(string[] args)
        {



            //create_db(@"D:/cache/quora.sqlite");
            Print<string>(query_get_names_and_link<string>());

            //Print<string>(query_get_distinct_names<string>());
            //query_get_all();

        }
    }
}
