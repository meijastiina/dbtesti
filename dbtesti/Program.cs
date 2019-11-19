using System;
using Npgsql;
using System.IO;
using System.Collections.Generic;

namespace dbtesti
{
    class Program
    {
        static void Main(string[] args)
        {
            // First we need to define where we want to connect
            var connString = "Host=localhost;Username=postgres;Password=postgres;Database=small_company";
            string file = "testfile.txt";
            File.WriteAllText(file, "This is test text");
            List < Person > people = new List<Person>();
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open(); // Here we open connection
                // Here we define our SQL query
                using (var cmd = new NpgsqlCommand("SELECT FirstName, LastName FROM person", conn))
                using (var reader = cmd.ExecuteReader())
                    // Let's loop through all fetched rows
                    while (reader.Read())
                    {
                        people.Add(new Person(reader.GetString(0), reader.GetString(1)));
                        // Let's get the string value in the field 0
                        Console.WriteLine(reader.GetString(0));
                        using (StreamWriter outputFile = new StreamWriter(file, true))
                        {
                            //Execute command and write output to file
                            outputFile.WriteLine(reader.GetString(0));
                        }
                    }

            }
            Console.WriteLine("This has been read from the file:");
            Console.WriteLine(File.ReadAllText(file));

        }
    }
}
