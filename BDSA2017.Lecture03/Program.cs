using BDSA2017.Lecture03.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace BDSA2017.Lecture03
{
    class Program
    {
        static Func<Superhero, bool> b = s => s.AlterEgo.StartsWith("B");

        static void Main(string[] args)
        {
            IEnumerable<int> numbers = new[] { 1, 2, 3 };

            var sorted = numbers.OrderByDescending(i => i);

            //sorted.Print();

            var repo = new Repository();

            var bs = repo.Superheroes.Where(b);

            //bs.Print();

            var ss = repo.Superheroes.Where(s => s.AlterEgo.ToLowerInvariant().StartsWith("s"));

            //repo.Superheroes.Where(s => s.Gender == Gender.Male)
            //                .OrderBy(s => s.GivenName)
            //                .ThenBy(s => s.Surname)
            //                .Print();

            //repo.Superheroes.OrderBy(r => r.AlterEgo).SkipWhile(r => r.AlterEgo[0] <= 'C').Print();

            var heros = repo.Superheroes.Select(s => (id: s.Id, alterEgo: s.AlterEgo));

            //heros.Print();

            var cities = from c in repo.Cities
                         orderby c.Name
                         select c;

            var h1 = from c in repo.Superheroes
                     select (id: c.Id, alterEgo: c.AlterEgo);

            var h2 = from c in repo.Superheroes
                     where c.AlterEgo.StartsWith("b", StringComparison.OrdinalIgnoreCase)
                       || c.Gender == Gender.Female
                     select c;

            //h2.Print();

            var h3 = from s in repo.Superheroes
                     join c in repo.Cities on s.CityId equals c.Id
                     select new { s.AlterEgo, City = c.Name };

            var h4 = repo.Superheroes.Join(
                    repo.Cities, 
                    s => s.CityId, 
                    s => s.Id, 
                    (s, c) => new { s.AlterEgo, City = c.Name });

            //h4.Print();

            var group = from s in repo.Superheroes
                        group s by s.GivenName[0] into g
                        select new
                        {
                            g.Key,
                            Count = g.Count()
                        };

            var group2 = repo.Superheroes.GroupBy(s => s.GivenName[0])
                             .Select(g => new { g.Key, Count = g.Count() });

            //group2.Print();

            var text = File.ReadAllText("Hamlet.txt");

            var words = Regex.Split(text, @"\P{L}+");

            var histogram = from l in words
                            let w = l.ToLower()
                            group w by w into h
                            let c = h.Count()
                            orderby c descending
                            select new { Word = h.Key, Count = c };

            //histogram.Take(5).Print();

            var dict = histogram.ToDictionary(c => c.Word);

            var lookup = histogram.ToLookup(c => c.Count);

            lookup[160].Print();
        }
    }
}
