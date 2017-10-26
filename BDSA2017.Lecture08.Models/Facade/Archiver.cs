using System;

namespace BDSA2017.Lecture08.Models.Facade
{
    public class Archiver
    {
        public void Archive(Article article)
        {
            Console.WriteLine($"Archived {article.Title} by {article.Author}");
        }
    }
}
