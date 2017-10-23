namespace BDSA2017.Lecture08.Lib.Game
{
    public interface IWeapon
    {
        string Name { get; }

        int Damage { get; }

        int Range { get; }
    }
}
