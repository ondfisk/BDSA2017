using System.Collections.Generic;

namespace BDSA2017.Lecture08.Lib.Facade
{
    public interface INotifier
    {
        void Notify(Article article, IEnumerable<Person> people);
    }
}