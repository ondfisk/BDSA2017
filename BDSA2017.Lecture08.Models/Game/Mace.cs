using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BDSA2017.Lecture08.Models.Game
{
    public class Mace : IWeapon
    {
        public int Damage => 64;

        public string Name => nameof(Mace);

        public int Range => 0;
    }
}
