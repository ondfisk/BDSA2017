namespace BDSA2017.Lecture08.Lib.Bridge
{
    public class Character
    { 
        public int Id { get; set; }

        public string GivenName { get; set; }

        public string Surname { get; set; }

        public string Species { get; set; }
        
        public string Origin { get; set; }
        
        public int? Year { get; set; }

        public override string ToString() => $"{GivenName} {Surname} - a {Species} from {Origin}, year: {Year}";
    }
}