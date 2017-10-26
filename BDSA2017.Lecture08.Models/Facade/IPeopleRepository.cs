using System.Collections.Generic;

namespace BDSA2017.Lecture08.Models.Facade
{
    public interface IPeopleRepository
    {
        IEnumerable<Person> All();
    }
}