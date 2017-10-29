using System.Collections.Generic;

namespace BDSA2017.Lecture09.MVVM.Model
{
    public class AlbumRepository
    {
        public IEnumerable<Album> Read()
        {
            return new[] 
            {
                new Album { Artist = "Morbid Angel", Title = "Altars Of Madness", Year = 1989, Cover = "ms-appx:///Assets/Covers/AltarsOfMadness.jpg" },
                new Album { Artist = "Morbid Angel", Title = "Blessed Are The Sick", Year = 1991, Cover = "ms-appx:///Assets/Covers/BlessedAreTheSick.jpg" },
                new Album { Artist = "Morbid Angel", Title = "Covenant", Year = 1993, Cover = "ms-appx:///Assets/Covers/Covenant.jpg" },
                new Album { Artist = "Morbid Angel", Title = "Domination", Year = 1995, Cover = "ms-appx:///Assets/Covers/Domination.jpg" },
                new Album { Artist = "Morbid Angel", Title = "Formulas Fatal To The Flesh", Year = 1998, Cover = "ms-appx:///Assets/Covers/FormulasFatalToTheFlesh.jpg" },
                new Album { Artist = "Morbid Angel", Title = "Gateways To Annihilation", Year = 2000, Cover = "ms-appx:///Assets/Covers/GatewaysToAnnihilation.jpg" },
                new Album { Artist = "Morbid Angel", Title = "Heretic", Year = 2003, Cover = "ms-appx:///Assets/Covers/Heretic.jpg" },
                new Album { Artist = "Morbid Angel", Title = "Illud Divinum Insanus", Year = 2011, Cover = "ms-appx:///Assets/Covers/IlludDivinumInsanus.jpg" },
                new Album { Artist = "Morbid Angel", Title = "Kingdoms Disdained", Year = 2017, Cover = "ms-appx:///Assets/Covers/KingdomsDisdained.jpg" }
            };
        }
    }
}
