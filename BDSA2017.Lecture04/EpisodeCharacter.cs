using System;
using System.Collections.Generic;
using System.Text;

namespace BDSA2017.Lecture04
{
    public class EpisodeCharacter
    {
        public int CharacterId { get; set; }
        public int EpisodeId { get; set; }

        public Character Characters { get; set; }
        public Episode Episode { get; set; }
    }
}
