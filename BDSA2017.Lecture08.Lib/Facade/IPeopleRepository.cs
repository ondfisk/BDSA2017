using System.Collections.Generic;

namespace BDSA2017.Lecture08.Lib.Facade
{
    public interface IPeopleRepository
    {
        IEnumerable<Person> All();
    }
}