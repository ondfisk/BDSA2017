﻿using System;

namespace BDSA2017.Lecture08.Models.Facade
{
    public class Publisher
    {
        public void PublishOnline(Article article)
        {
            Console.WriteLine($"Published {article.Title} by {article.Author} online");
        }
    }
}
