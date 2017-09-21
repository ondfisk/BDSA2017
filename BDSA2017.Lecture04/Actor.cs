using System.Collections.Generic;

namespace BDSA2017.Lecture04
{
    public class Actor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Character> Characters { get; set; }
    }
}
