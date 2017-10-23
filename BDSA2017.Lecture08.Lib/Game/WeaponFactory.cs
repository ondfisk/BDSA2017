using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace BDSA2017.Lecture08.Lib.Game
{
    public class WeaponFactory
    {
        public IWeapon Make(string name)
        {
            var w = typeof(IWeapon).GetTypeInfo();

            var type = w.Assembly
                        .GetTypes()
                        .Where(t => t.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                        .FirstOrDefault();

            if (type == null)
            {
                return null;
            }

            return Activator.CreateInstance(type) as IWeapon;
        }

        public IEnumerable<string> Available()
        {
            var w = typeof(IWeapon).GetTypeInfo();

            return w.Assembly
                    .GetTypes()
                    .Where(t => t != typeof(IWeapon))
                    .Where(t => w.IsAssignableFrom(t))
                    .OrderBy(t => t.Name)
                    .Select(t => t.Name);
        }
    }
}
