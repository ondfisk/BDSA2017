using System;

namespace BDSA2017.Lecture03
{
    public class Superhero
    {
        public int Id { get; set; }
        public string GivenName { get; set; }
        public string Surname { get; set; }
        public string AlterEgo { get; set; }
        public Gender Gender { get; set; }
        public DateTime FirstAppearance { get; set; }
        public int? CityId { get; set; }
        public City City { get; set; }

        public override string ToString()
        {
            return $"{GivenName} {Surname} aka {AlterEgo}";
        }
    }
}
