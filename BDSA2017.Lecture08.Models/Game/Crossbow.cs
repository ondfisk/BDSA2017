namespace BDSA2017.Lecture08.Models.Game
{
    public class Crossbow : IWeapon
    {
        public string Name => nameof(Crossbow);

        public int Damage => 12;

        public int Range => 12;
    }
}
