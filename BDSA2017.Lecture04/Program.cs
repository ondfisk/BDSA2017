using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace BDSA2017.Lecture04
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Futurama;Integrated Security=True";

            //Console.Write("Enter search string to find character: ");
            //var searchString = Console.ReadLine();

            //var sql = "SELECT Name FROM Characters WHERE Name LIKE '%' + @searchString + '%'";

            //using (var connection = new SqlConnection(connectionString))
            //using (var command = new SqlCommand(sql, connection))
            //{
            //    command.Parameters.AddWithValue("@searchString", searchString);
            //    connection.Open();
            //    using (var reader = command.ExecuteReader())
            //    {
            //        while (reader.Read())
            //        {
            //            Console.WriteLine(reader["Name"]);
            //        }
            //    }
            //}

            var builder = new DbContextOptionsBuilder<FuturamaContext>();
            builder.UseSqlServer(connectionString);

            using (var context = new FuturamaContext(builder.Options))
            {
                //var fs = from c in context.Characters
                //         where c.Name.Contains("f")
                //         select c.Name;

                //foreach (var name in fs)
                //{
                //    Console.WriteLine(name);
                //}

                //var cs = from c in context.Characters
                //         join a in context.Actors on c.ActorId equals a.Id
                //         select new { c.Name, Actor = a.Name };

                var cs = from c in context.Characters
                         select new { c.Name, Actor = c.Actor.Name };

                foreach (var c in cs)
                {
                    Console.WriteLine(c);
                }
            }
        }
    }
}
