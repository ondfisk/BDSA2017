namespace BDSA2017.Lecture08.Lib.Game
{
    public class Sword : IWeapon
    {
        public string Name => nameof(Sword);

        public int Damage => 32;

        public int Range => 0;
    }
}
