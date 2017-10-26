using System;

namespace BDSA2017.Lecture08.Models.Facade
{
    public class Facade
    {
        private readonly INotifier _notifier;
        private readonly IPublisher _publisher;
        private readonly IArchiver _archiver;

        public Facade(INotifier notifier, IPublisher publisher, IArchiver archiver)
        {
            _notifier = notifier;
            _publisher = publisher;
            _archiver = archiver;
        }

        public void Publish(Article article)
        {
            Console.WriteLine("Publishing");
            _publisher.PublishOnline(article);

            Console.WriteLine("Archiving");
            _archiver.Archive(article);

            Console.WriteLine("Notifying");
            _notifier.Notify(article);
        }
    }
}
