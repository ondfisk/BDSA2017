using System;
using System.Collections.Generic;
using System.Text;

namespace BDSA2017.Lecture08.Models.Game
{
    public class Wolf : IWeapon
    {
        public string Name => nameof(Wolf);

        public int Damage => 256;

        public int Range => 4;
    }
}
