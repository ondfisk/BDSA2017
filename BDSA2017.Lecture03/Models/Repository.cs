using System;
using System.Collections.Generic;

namespace BDSA2017.Lecture03.Models
{
    public class Repository
    {
        public ICollection<Superhero> Superheroes { get; }

        public ICollection<City> Cities { get; }

        public Repository()
        {
            Cities = new HashSet<City>()
            {
                new City { Id = 1, Name = "New York" },
                new City { Id = 2, Name = "Metropolis" },
                new City { Id = 3, Name = "Gotham City" },
                new City { Id = 4, Name = "Dayton" },
                new City { Id = 5, Name = "Alberta" }
            };

            Superheroes = new HashSet<Superhero> 
            {
                new Superhero {Id = 1, GivenName = "Clark", Surname = "Kent", AlterEgo = "Superman", Gender = Gender.Male, FirstAppearance = DateTime.Parse("1938-04-18"), CityId = 2 },
                new Superhero {Id = 2, GivenName = "Bruce", Surname = "Wayne", AlterEgo = "Batman", Gender = Gender.Male, FirstAppearance = DateTime.Parse("1939-05-01"), CityId = 3 },
                new Superhero {Id = 4, GivenName = "Bruce", Surname = "Banner", AlterEgo = "Hulk", Gender = Gender.Male, FirstAppearance = DateTime.Parse("1962-05-01"), CityId = 4 },
                new Superhero {Id = 5, GivenName = "Steve", Surname = "Rogers", AlterEgo = "Captain America", Gender = Gender.Male, FirstAppearance = DateTime.Parse("1941-03-01"), CityId = 1 },
                new Superhero {Id = 6, GivenName = "Tony", Surname = "Stark", AlterEgo = "Iron Man", Gender = Gender.Male, FirstAppearance = DateTime.Parse("1963-03-01"), CityId = 1 },
                new Superhero {Id = 7, GivenName = "James", Surname = "Howlett", AlterEgo = "Wolverine", Gender = Gender.Male, FirstAppearance = DateTime.Parse("1974-10-01"), CityId = 5 },
                new Superhero {Id = 8, GivenName = "Selina", Surname = "Kyle", AlterEgo = "Catwoman", Gender = Gender.Female, FirstAppearance = DateTime.Parse("1940-04-01"), CityId = 3 },
            };
        }
    }
}
