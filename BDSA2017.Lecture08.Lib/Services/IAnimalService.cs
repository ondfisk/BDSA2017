using System;

namespace BDSA2017.Lecture08.Lib.Animals
{
    public interface IAnimalService : IDisposable
    {
        void Speak();
    }
}