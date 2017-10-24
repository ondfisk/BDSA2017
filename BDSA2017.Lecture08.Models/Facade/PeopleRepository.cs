using System.Collections.Generic;

namespace BDSA2017.Lecture08.Models.Facade
{
    public class PeopleRepository : IPeopleRepository
    {
        public IEnumerable<Person> All()
        {
            return new Person[0];
        }
    }
}
