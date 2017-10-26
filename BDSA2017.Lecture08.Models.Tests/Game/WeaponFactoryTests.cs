using BDSA2017.Lecture08.Models.Game;
using Xunit;

namespace BDSA2017.Lecture08.Models.Tests.Game
{
    public class WeaponFactoryTests
    {
        [Fact]
        public void Make_given_invalid_weapon_returns_null()
        {
            var factory = new WeaponFactory();

            var weapon = factory.Make("Pan Galactic Gargle Blaster");

            Assert.Null(weapon);
        }

        [Fact]
        public void Make_given_grenade_returns_Grenade()
        {
            var factory = new WeaponFactory();

            var weapon = factory.Make("grenade");

            Assert.IsType<Grenade>(weapon);
        }

        [Fact]
        public void Make_given_SWORD_returns_Sword()
        {
            var factory = new WeaponFactory();

            var weapon = factory.Make("SWORD");

            Assert.IsType<Sword>(weapon);
        }

        [Fact]
        public void Make_given_CrossBow_returns_Crossbow()
        {
            var factory = new WeaponFactory();

            var weapon = factory.Make("CrossBow");

            Assert.IsType<Crossbow>(weapon);
        }

        [Fact]
        public void Available_returns_all_weapons()
        {
            var factory = new WeaponFactory();

            var weapons = factory.Available();

            Assert.Equal(new[] { "Crossbow", "Grenade", "Sword", "Wolf" }, weapons);
        }
    }
}
