﻿namespace BDSA2017.Lecture11.Entities
{
    public class EpisodeCharacter
    {
        public int CharacterId { get; set; }

        public int EpisodeId { get; set; }

        public Character Characters { get; set; }

        public Episode Episode { get; set; }
    }
}
