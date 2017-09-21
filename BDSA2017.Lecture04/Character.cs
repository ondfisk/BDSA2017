using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BDSA2017.Lecture04
{
    public partial class Character
    {
        public int Id { get; set; }
        public int? ActorId { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        [StringLength(50)]
        public string Species { get; set; }
        [StringLength(50)]
        public string Planet { get; set; }

        public Actor Actor { get; set; }

        public ICollection<EpisodeCharacter> Episodes { get; set; }
    }
}
