using System.ComponentModel.DataAnnotations;

namespace BDSA2017.Lecture10.Common
{
    public class CharacterDTO
    {
        public int Id { get; set; }

        public int? ActorId { get; set; }

        public string Name { get; set; }

        public string Species { get; set; }

        public string Planet { get; set; }

        public string Image { get; set; }

        public string ActorName { get; set; }

        public int NumberOfEpisodes { get; set; }
    }
}
