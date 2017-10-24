using System;

namespace BDSA2017.Lecture08.Models.Animals
{
    public interface IAnimalService : IDisposable
    {
        void Speak();
    }
}