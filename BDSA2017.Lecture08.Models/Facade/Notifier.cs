using System;
using System.Collections.Generic;

namespace BDSA2017.Lecture08.Models.Facade
{
    public class Notifier : INotifier
    {
        private readonly IPeopleRepository _peopleRepository;

        public Notifier(IPeopleRepository peopleRepository)
        {
            _peopleRepository = peopleRepository;
        }

        public void Notify(Article article)
        {
            Console.WriteLine("Notifying:");
            foreach (var person in _peopleRepository.All())
            {
                Console.WriteLine($"- {person.Name}");
            }
        }
    }
}
